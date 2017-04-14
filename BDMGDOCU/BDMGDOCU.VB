Imports System.Data
Imports System.Data.Common
Imports NTSInformatica.CLN__STD
Imports System.Runtime.InteropServices

Imports System
Public Class CLDMGDOCU
  Inherits CLD__BASE

  Public Overridable Function TabEmpty(ByVal strDitta As String, ByRef bAgenti As Boolean, ByRef bNoteprel As Boolean) As Boolean
    '---------------------------------
    'ritorna false se la tabella è compilata
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      bAgenti = True
      bNoteprel = True

      strSQL = "SELECT TOP 1 tb_codcage FROM tabcage WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then bAgenti = False
      dttTmp.Clear()

      strSQL = "SELECT TOP 1 tm_tipork FROM testprb WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then bNoteprel = False
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub RunQueryBeforePrint(ByVal strDitta As String, ByVal strChild As String)
    '----------------------------------------
    'prima di stampare il documento eseguo delle query libere, lette da arcproc
    Dim strQuery As String = ""
    Dim i As Integer
    Try
      For i = 1 To 20
        strQuery = Trim(GetSettingBusDitt(strDitta, strChild, "Opzioni", ".", "QueryStampa" & i, " ", " ", ""))       'NON DOCUMENTARE
        If strQuery <> "" Then
          Try
            Execute(strQuery, CLE__APP.DBTIPO.DBAZI)
          Catch ex As Exception
            'non gestisco l'errore
          End Try
        Else
          Exit For
        End If
      Next
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function DeleteDocAllole(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, _
                                       ByVal lNumdoc As Integer, ByVal bOrdini As Boolean, _
                                       ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      '-------------------------
      'cancello allole: 
      strSQL = "DELETE FROM allole" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND ao_tipo = '" & IIf(bOrdini, "O", "M").ToString & "'" & _
                " AND ao_tipodoc = " & CStrSQL(strTipoDoc) & _
                " AND ao_annodoc = " & nAnno & _
                " AND ao_seriedoc = " & CStrSQL(strSerie) & _
                " AND ao_numdoc = " & lNumdoc
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTabcentTabcoveFromMagaz(ByVal strDitta As String, ByVal nMagaz As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      '----------------------------
      'dato un magazzino restituisce un datatable con il centro di lavoro associato e tabcove relativo
      strSQL = "SELECT tb_codcent, tb_ccodcontr, tb_ccodcena, tb_concova" & _
             " FROM tabcent LEFT JOIN tabcove ON tabcent.codditt = tabcove.codditt AND tabcent.tb_ccodcontr = tabcove.tb_codcove" & _
             " WHERE tabcent.codditt = " & CStrSQL(strDitta) & _
             " AND tb_magterz = " & nMagaz.ToString & " ORDER BY tb_codcent"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetScominArtmaga(ByVal strDitta As String, ByVal strCodart As String, ByVal nMagaz As Integer) As Decimal
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      GetScominArtmaga = -1

      strSQL = "SELECT am_scomin FROM artmaga" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND am_codart = " & CStrSQL(strCodart) & _
              " AND am_codmaga = " & nMagaz
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        GetScominArtmaga = NTSCDec(dttTmp.Rows(0)!am_scomin)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CaricaTabelleCache(ByVal strDitta As String, ByRef dsMaga As DataSet, ByRef dsCiva As DataSet, ByRef dsCove As DataSet) As Boolean
    Try
      Return CaricaTabelleCache(strDitta, dsMaga, dsCiva, dsCove, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CaricaTabelleCache(ByVal strDitta As String, ByRef dsMaga As DataSet, _
                                                 ByRef dsCiva As DataSet, ByRef dsCove As DataSet, _
                                                 ByRef dsCena As DataSet) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dsMaga, dsCiva, dsCove, dsCena})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsMaga = CType(oIn(1), DataSet)
        dsCiva = CType(oIn(2), DataSet)
        dsCove = CType(oIn(3), DataSet)
        dsCena = CType(oIn(4), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      Return CaricaTabelleCache(strDitta, dsMaga, dsCiva, dsCove, Nothing, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CaricaTabelleCache(ByVal strDitta As String, ByRef dsMaga As DataSet, _
                                                 ByRef dsCiva As DataSet, ByRef dsCove As DataSet, _
                                                 ByRef dsCena As DataSet, ByRef dttPecx As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dsMaga, dsCiva, dsCove, dsCena, dttPecx})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsMaga = CType(oIn(1), DataSet)
        dsCiva = CType(oIn(2), DataSet)
        dsCove = CType(oIn(3), DataSet)
        dsCena = CType(oIn(4), DataSet)
        dttPecx = CType(oIn(5), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT * FROM tabciva ORDER BY tb_codciva"
      dsCiva = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABCIVA")
      If Not dsCiva.Tables("TABCIVA").Columns.Contains("tb_revcharge") Then
        'tb_revcharge sarà presente da Net 2013
        dsCiva.Tables("TABCIVA").Columns.Add("tb_revcharge", GetType(String))
        For Each dtrT As DataRow In dsCiva.Tables("TABCIVA").Rows
          dtrT!tb_revcharge = "N"
        Next
        dsCiva.Tables("TABCIVA").AcceptChanges()
      End If

      strSQL = "SELECT * FROM tabmaga WHERE codditt = " & CStrSQL(strDitta) & " ORDER BY tb_codmaga"
      dsMaga = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABMAGA")
      strSQL = "SELECT tabcove.*, isnull(an_accperi, 'N') as xx_accperi, isnull(an_flci, ' ') as xx_flci " & _
               " FROM TABCOVE LEFT JOIN ANAGRA ON tabcove.codditt = anagra.codditt AND tabcove.tb_concove = anagra.an_conto " & _
               " WHERE tabcove.codditt = " & CStrSQL(strDitta) & " ORDER BY tb_codcove"
      dsCove = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABCOVE")
      If Not dsCena Is Nothing Then
        strSQL = "SELECT * FROM TABCENA WHERE codditt = " & CStrSQL(strDitta) & " ORDER BY tb_codcena"
        dsCena = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABCENA")
      End If
      If Not dttPecx Is Nothing Then
        strSQL = "SELECT * FROM TABPECX WHERE codditt = " & CStrSQL(strDitta)
        dttPecx = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetPevePeac(ByRef dttPeve As DataTable, ByRef dttPeac As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 * FROM tabpeve WHERE tb_codpeve = 1"
      dttPeve = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "SELECT TOP 1 * FROM tabpeac WHERE tb_codpeac = 1"
      dttPeac = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function ZoomSedoc(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                           ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                           ByVal strDataA As String, ByVal nTipobfDa As Integer, ByVal nTipobfA As Integer, _
                           ByVal strRiferim As String, ByVal lNumpar As Integer, ByVal strDitta As String) As DataSet
    Dim dsTmp As DataSet = New DataSet
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strTipork, nAnno, strSerie, _
                                             lContoDa, lContoA, strDataDa, _
                                             strDataA, nTipobfDa, nTipobfA, _
                                             strRiferim, lNumpar, strDitta})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CType(oOut, DataSet)
      End If
      '----------------

      ZoomSedoc(strTipork, nAnno, strSerie, _
                lContoDa, lContoA, strDataDa, _
                strDataA, nTipobfDa, nTipobfA, _
                strRiferim, lNumpar, strDitta, _
                "", "")

      Return dsTmp
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return Nothing
    End Try
  End Function
  Public Overridable Function ZoomSedoc(ByVal strTipork As String, ByVal nAnno As Integer, ByVal strSerie As String, _
                           ByVal lContoDa As Integer, ByVal lContoA As Integer, ByVal strDataDa As String, _
                           ByVal strDataA As String, ByVal nTipobfDa As Integer, ByVal nTipobfA As Integer, _
                           ByVal strRiferim As String, ByVal lNumpar As Integer, ByVal strDitta As String, _
                           ByVal strWhereFiltriEstesi As String, ByVal strWhereFiltriEstesiMovmag As String) As DataSet
    Dim dsTmp As DataSet = New DataSet
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT testmag.*, tmp.* " & _
               " FROM testmag " & _
               "  INNER JOIN (" & _
               " SELECT DISTINCT testmag.codditt, tm_tipork, tm_anno, tm_serie, tm_numdoc, " & _
               " an_descr1 as xx_conto, tm_totmerce - tm_totacceva AS xx_residuo, " & _
               " dd_nomdest as xx_destin, co_descr1 as xx_commeca, tb_desstag as xx_codstag, " & _
               " tb_destpbf as xx_tipobf, tb_despaga as xx_codpaga " & _
               "FROM testmag " & _
               "  INNER JOIN anagra ON testmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto " & _
               "  LEFT JOIN destdiv ON testmag.codditt = destdiv.codditt AND testmag.tm_conto = destdiv.dd_conto AND testmag.tm_coddest = destdiv.dd_coddest " & _
               "  LEFT JOIN commess ON testmag.codditt = commess.codditt AND testmag.tm_commeca = commess.co_comme " & _
               "  LEFT JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf " & _
               "  LEFT JOIN tabstag ON testmag.codditt = tabstag.codditt AND testmag.tm_codstag = tabstag.tb_codstag " & _
               "  LEFT JOIN tabpaga ON testmag.tm_codpaga = tabpaga.tb_codpaga "

      If strWhereFiltriEstesi.IndexOf("movmag", StringComparison.CurrentCultureIgnoreCase) > -1 Then
        strSQL &= "  LEFT JOIN movmag ON movmag.codditt = testmag.codditt AND mm_tipork = tm_tipork AND mm_anno = tm_anno " & _
                  "                  AND mm_serie = tm_serie AND tm_numdoc = mm_numdoc "
      End If


      strSQL &= " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
                "  AND tm_tipork = " & CStrSQL(strTipork) & " AND tm_anno = " & nAnno
      If strSerie <> "" Then strSQL += " AND tm_serie = '" & strSerie & "'"
      If lContoDa <> 0 Or lContoA <> 999999999 Then strSQL += " AND tm_conto BETWEEN " & lContoDa.ToString & " AND " & lContoA.ToString
      If NTSCDate(strDataDa) <> New Date(nAnno, 1, 1) OrElse NTSCDate(strDataA) <> New Date(2099, 12, 31) Then
        strSQL += " AND tm_datdoc BETWEEN " & CDataSQL(strDataDa) & " AND " & CDataSQL(strDataA)
      End If
      If nTipobfDa <> 0 OrElse nTipobfA <> 9999 Then
        strSQL += " AND tm_tipobf BETWEEN " & nTipobfDa.ToString & " AND " & nTipobfA.ToString
      End If
      If strRiferim <> "" Then
        strRiferim = "%" & strRiferim.Replace("*", "%") & "%"
        strSQL += " AND tm_riferim like " & CStrSQL(strRiferim.Replace("%%", "%"))
      End If
      If lNumpar <> 0 Then strSQL += " AND tm_numpar = " & lNumpar.ToString

      TraduciWhere(strWhereFiltriEstesi, strSQL)

      strSQL &= ") AS tmp ON testmag.codditt = tmp.codditt AND testmag.tm_tipork = tmp.tm_tipork AND testmag.tm_anno = tmp.tm_anno " & _
                "        AND testmag.tm_serie = tmp.tm_serie AND testmag.tm_numdoc = tmp.tm_numdoc "

      If strTipork = "W" Then strSQL = strSQL.Replace("testmag", "testprb").Replace("movmag", "movprb")
      
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SEDOC")

      Return dsTmp
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return Nothing
    End Try
  End Function

  Public Overridable Function VerificaPresenzaPromozioni(ByVal strDitta As String, ByVal strCodart As String, ByVal strDatDoc As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = " SELECT top 1 tabtpro.* " & _
         " FROM tabtpro INNER JOIN artprom ON tabtpro.codditt = artprom.codditt AND tabtpro.tb_codtpro = artprom.apr_codtpro" & _
         " WHERE tabtpro.codditt = " & CStrSQL(strDitta) & _
         " AND tb_datagg <= " & CDataSQL(strDatDoc) & _
         " AND tb_datscad >= " & CDataSQL(strDatDoc) & _
         " AND apr_codart = " & CStrSQL(strCodart)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataInizioEsercizioCorrente(ByVal strDitta As String) As DateTime
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try

      strSQL = "SELECT tb_dtineser" & _
              " FROM tabesco INNER JOIN tabanaz ON tabesco.codditt = tabanaz.codditt AND tabesco.tb_codesco = tabanaz.tb_escomp" & _
              " WHERE tabanaz.codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCDate(dttTmp.Rows(0)!tb_dtineser)

      Return NTSCDate(IntSetDate("01/01/1900"))

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetNrecTot(ByVal strDitta As String, ByVal bMovord As Boolean) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try

      strSQL = "SELECT Count(*) as NREC FROM " & IIf(bMovord, "movord", "movmag").ToString & _
            " WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!NREC)

      Return 0

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArticoFigliKit(ByVal strDitta As String, ByVal strCodart As String, _
                                                ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM artkit" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND ak_codart = " & CStrSQL(strCodart) & " ORDER BY ak_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CheckBarcodeCodarfo(ByVal strDitta As String, ByRef bTableBarcodeEmpty As Boolean, ByRef bTableCodarfoEmpty As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      bTableBarcodeEmpty = False
      bTableCodarfoEmpty = False

      strSQL = "SELECT top 1 * FROM barcode WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then bTableBarcodeEmpty = True
      dttTmp.Clear()

      strSQL = "SELECT top 1 * FROM codarfo WHERE codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then bTableCodarfoEmpty = True
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function AggArtproTC(ByVal strDitta As String, ByVal strTipork As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, _
                                          ByVal lNumdoc As Integer, ByVal nOperaz As Integer, _
                                          ByRef dbConn As DbConnection) As Boolean
    '-----------------------------------
    ' AGGIORNA ARTPROTC E ARTPROXTC PARTENDO DA MOVMAG E KEYMAG o MOVORD KEYORD
    ' IN:  strTipork  --> Tipo documento
    '      nAnno      --> Anno d'esercizio
    '      strSerie   --> Serie documento
    '      lNumDoc    --> Numero documento
    '      nOperaz    --> Tipo di operazione da compiere (0 = Aggiorna, 1 = Annulla agg)
    ' OU:  AggArtProTC--> True se OK, False se Errore
    Dim strSQL As String = ""
    Dim strSQL1 As String = ""
    Dim i As Integer = 0
    Dim strKeym As String = ""
    Dim bDaMovord As Boolean = False
    Try
      If strTipork = "Q" Then Return True

      Select Case strTipork
        Case "#", "$", "H", "O", "Q", "R", "V", "X", "Y"
          bDaMovord = True
        Case "W"
          strKeym = "keyprb"
        Case Else
          strKeym = "keymag"
      End Select

      If nOperaz = 0 Then
        If Not AggArtproTC_CreaRk(strDitta, strTipork, nAnno, strSerie, lNumdoc, bDaMovord, strKeym, dbConn) Then Return False

        If bDaMovord Then
          If Not AggArtproTC_AggDaMovord(strDitta, strTipork, nAnno, strSerie, lNumdoc, False, dbConn) Then Return False
        Else
          If strTipork = "W" Then
            If Not AggArtproTC_AggDaMovprb(strDitta, strTipork, nAnno, strSerie, lNumdoc, False, dbConn) Then Return False
          Else
            If Not AggArtproTC_AggDaMovmag(strDitta, strTipork, nAnno, strSerie, lNumdoc, False, dbConn) Then Return False

          End If
        End If    'If bDaMovord Then

      Else
        'Storna gli aggiornamenti del apportati dal documento
        If bDaMovord Then
          If Not AggArtproTC_AggDaMovord(strDitta, strTipork, nAnno, strSerie, lNumdoc, True, dbConn) Then Return False
        Else
          If strTipork = "W" Then
            If Not AggArtproTC_AggDaMovprb(strDitta, strTipork, nAnno, strSerie, lNumdoc, True, dbConn) Then Return False
          Else
            If Not AggArtproTC_AggDaMovmag(strDitta, strTipork, nAnno, strSerie, lNumdoc, True, dbConn) Then Return False
          End If
        End If
      End If    'If nOperaz = 0 Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggArtproTC_CreaRk(ByVal strDitta As String, ByVal strTipork As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, _
                                          ByVal lNumdoc As Integer, ByVal bdaMovord As Boolean, _
                                          ByVal strKeym As String, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      '------------------------------
      'Crea, se non esistono, i record relativi a articoli e magazzino in ARTPROTC
      strSQL = "INSERT INTO artprotc (codditt, apt_codart, apt_magaz, apt_fase," & _
              " apt_esist01, apt_esist02, apt_esist03, apt_esist04, apt_esist05, apt_esist06, apt_esist07, apt_esist08, apt_esist09, apt_esist10, apt_esist11, apt_esist12, apt_esist13, apt_esist14, apt_esist15, apt_esist16, apt_esist17, apt_esist18, apt_esist19, apt_esist20, apt_esist21, apt_esist22, apt_esist23, apt_esist24," & _
              " apt_prenot01, apt_prenot02, apt_prenot03, apt_prenot04, apt_prenot05, apt_prenot06, apt_prenot07, apt_prenot08, apt_prenot09, apt_prenot10, apt_prenot11, apt_prenot12, apt_prenot13, apt_prenot14, apt_prenot15, apt_prenot16, apt_prenot17, apt_prenot18, apt_prenot19, apt_prenot20, apt_prenot21, apt_prenot22, apt_prenot23, apt_prenot24," & _
              " apt_ordin01, apt_ordin02, apt_ordin03, apt_ordin04, apt_ordin05, apt_ordin06, apt_ordin07, apt_ordin08, apt_ordin09, apt_ordin10, apt_ordin11, apt_ordin12, apt_ordin13, apt_ordin14, apt_ordin15, apt_ordin16, apt_ordin17, apt_ordin18, apt_ordin19, apt_ordin20 , apt_ordin21, apt_ordin22, apt_ordin23, apt_ordin24," & _
              " apt_impeg01, apt_impeg02, apt_impeg03, apt_impeg04, apt_impeg05, apt_impeg06, apt_impeg07, apt_impeg08, apt_impeg09, apt_impeg10, apt_impeg11, apt_impeg12, apt_impeg13, apt_impeg14, apt_impeg15, apt_impeg16, apt_impeg17, apt_impeg18, apt_impeg19, apt_impeg20, apt_impeg21, apt_impeg22, apt_impeg23, apt_impeg24)"
      If bdaMovord Then
        strSQL = strSQL & " SELECT DISTINCT " & CStrSQL(strDitta) & "," & _
              " ko_codart, ko_magaz, ko_fase," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0" & _
              " FROM (keyord INNER JOIN artico ON keyord.codditt = artico.codditt AND keyord.ko_codart = artico.ar_codart)" & _
              " LEFT JOIN artprotc ON keyord.codditt = artprotc.codditt AND keyord.ko_codart = artprotc.apt_codart AND keyord.ko_magaz = artprotc.apt_magaz AND keyord.ko_fase = artprotc.apt_fase" & _
              " WHERE artprotc.codditt IS NULL" & _
              " AND artprotc.apt_codart IS NULL" & _
              " AND artprotc.apt_magaz IS NULL" & _
              " AND artprotc.apt_fase IS NULL" & _
              " AND keyord.codditt = " & CStrSQL(strDitta) & _
              " AND keyord.ko_tipork = " & CStrSQL(strTipork) & _
              " AND keyord.ko_anno = " & nAnno & _
              " AND keyord.ko_serie = " & CStrSQL(strSerie) & _
              " AND keyord.ko_numord = " & lNumdoc & _
              " AND artico.ar_codtagl <> 0"
      Else
        strSQL = strSQL & " SELECT DISTINCT " & CStrSQL(strDitta) & "," & _
              " km_codart, km_magaz, km_fase," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0" & _
              " FROM (" & strKeym & " INNER JOIN artico ON " & strKeym & ".codditt = artico.codditt AND " & strKeym & ".km_codart = artico.ar_codart)" & _
              " LEFT JOIN artprotc ON " & strKeym & ".codditt = artprotc.codditt AND " & strKeym & ".km_codart = artprotc.apt_codart AND " & strKeym & ".km_magaz = artprotc.apt_magaz AND " & strKeym & ".km_fase = artprotc.apt_fase" & _
              " WHERE artprotc.codditt IS NULL" & _
              " AND artprotc.apt_codart IS NULL" & _
              " AND artprotc.apt_magaz IS NULL" & _
              " AND artprotc.apt_fase IS NULL" & _
              " AND " & strKeym & ".codditt = " & CStrSQL(strDitta) & _
              " AND " & strKeym & ".km_tipork = " & CStrSQL(strTipork) & _
              " AND " & strKeym & ".km_anno = " & nAnno & _
              " AND " & strKeym & ".km_serie = " & CStrSQL(strSerie) & _
              " AND " & strKeym & ".km_numdoc = " & lNumdoc & _
              " AND artico.ar_codtagl <> 0"
      End If
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '------------------------------
      'Crea, se non esistono, i record relativi a articoli in ARTPROXTC
      strSQL = "INSERT INTO artproxtc (codditt, apt_codart, apt_fase," & _
              " apt_esist01, apt_esist02, apt_esist03, apt_esist04, apt_esist05, apt_esist06, apt_esist07, apt_esist08, apt_esist09 , apt_esist10, apt_esist11, apt_esist12, apt_esist13, apt_esist14, apt_esist15, apt_esist16, apt_esist17, apt_esist18, apt_esist19, apt_esist20, apt_esist21, apt_esist22, apt_esist23, apt_esist24," & _
              " apt_prenot01, apt_prenot02, apt_prenot03, apt_prenot04, apt_prenot05, apt_prenot06, apt_prenot07, apt_prenot08, apt_prenot09, apt_prenot10, apt_prenot11, apt_prenot12, apt_prenot13, apt_prenot14, apt_prenot15, apt_prenot16, apt_prenot17, apt_prenot18, apt_prenot19, apt_prenot20, apt_prenot21, apt_prenot22, apt_prenot23, apt_prenot24," & _
              " apt_ordin01, apt_ordin02, apt_ordin03, apt_ordin04, apt_ordin05, apt_ordin06, apt_ordin07, apt_ordin08, apt_ordin09, apt_ordin10, apt_ordin11, apt_ordin12, apt_ordin13, apt_ordin14, apt_ordin15, apt_ordin16, apt_ordin17, apt_ordin18, apt_ordin19, apt_ordin20, apt_ordin21, apt_ordin22, apt_ordin23, apt_ordin24," & _
              " apt_impeg01, apt_impeg02, apt_impeg03, apt_impeg04, apt_impeg05, apt_impeg06, apt_impeg07, apt_impeg08, apt_impeg09, apt_impeg10, apt_impeg11, apt_impeg12, apt_impeg13, apt_impeg14, apt_impeg15, apt_impeg16, apt_impeg17, apt_impeg18, apt_impeg19, apt_impeg20, apt_impeg21, apt_impeg22, apt_impeg23, apt_impeg24)"
      If bdaMovord Then
        strSQL = strSQL & " SELECT DISTINCT " & CStrSQL(strDitta) & "," & _
              " ko_codart, ko_fase," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0" & _
              " FROM (keyord INNER JOIN artico ON keyord.codditt = artico.codditt AND keyord.ko_codart = artico.ar_codart)" & _
              " LEFT JOIN artproxtc ON keyord.codditt = artproxtc.codditt AND keyord.ko_codart = artproxtc.apt_codart AND keyord.ko_fase = artproxtc.apt_fase" & _
              " WHERE artproxtc.codditt IS NULL" & _
              " AND artproxtc.apt_codart IS NULL" & _
              " AND artproxtc.apt_fase IS NULL" & _
              " AND keyord.codditt = " & CStrSQL(strDitta) & _
              " AND keyord.ko_tipork = " & CStrSQL(strTipork) & _
              " AND keyord.ko_anno = " & nAnno & _
              " AND keyord.ko_serie = " & CStrSQL(strSerie) & _
              " AND keyord.ko_numord = " & lNumdoc & _
              " AND artico.ar_codtagl <> 0"
      Else
        strSQL = strSQL & " SELECT DISTINCT " & CStrSQL(strDitta) & "," & _
              " km_codart, km_fase," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0" & _
              " FROM (" & strKeym & " INNER JOIN artico ON " & strKeym & ".codditt = artico.codditt AND " & strKeym & ".km_codart = artico.ar_codart)" & _
              " LEFT JOIN artproxtc ON " & strKeym & ".codditt = artproxtc.codditt AND " & strKeym & ".km_codart = artproxtc.apt_codart AND " & strKeym & ".km_fase = artproxtc.apt_fase" & _
              " WHERE artproxtc.codditt IS NULL" & _
              " AND artproxtc.apt_codart IS NULL" & _
              " AND artproxtc.apt_fase IS NULL" & _
              " AND " & strKeym & ".codditt = " & CStrSQL(strDitta) & _
              " AND " & strKeym & ".km_tipork = " & CStrSQL(strTipork) & _
              " AND " & strKeym & ".km_anno = " & nAnno & _
              " AND " & strKeym & ".km_serie = " & CStrSQL(strSerie) & _
              " AND " & strKeym & ".km_numdoc = " & lNumdoc & _
              " AND artico.ar_codtagl <> 0"
      End If
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '------------------------------
      'Crea, se non esistono, i record relativi a articoli e magazzino in LOTCPROTC
      strSQL = "INSERT INTO lotcprotc (codditt, apt_codart, apt_magaz, apt_commeca, apt_lotto, apt_fase, apt_ubicaz," & _
              " apt_esist01 , apt_esist02, apt_esist03, apt_esist04, apt_esist05, apt_esist06, apt_esist07, apt_esist08, apt_esist09, apt_esist10, apt_esist11, apt_esist12, apt_esist13, apt_esist14, apt_esist15, apt_esist16, apt_esist17, apt_esist18, apt_esist19, apt_esist20, apt_esist21, apt_esist22, apt_esist23, apt_esist24," & _
              " apt_prenot01, apt_prenot02, apt_prenot03, apt_prenot04, apt_prenot05, apt_prenot06, apt_prenot07, apt_prenot08, apt_prenot09, apt_prenot10, apt_prenot11, apt_prenot12, apt_prenot13, apt_prenot14, apt_prenot15, apt_prenot16, apt_prenot17, apt_prenot18, apt_prenot19, apt_prenot20, apt_prenot21, apt_prenot22, apt_prenot23, apt_prenot24," & _
              " apt_ordin01, apt_ordin02, apt_ordin03, apt_ordin04, apt_ordin05, apt_ordin06, apt_ordin07, apt_ordin08, apt_ordin09, apt_ordin10, apt_ordin11, apt_ordin12, apt_ordin13, apt_ordin14, apt_ordin15, apt_ordin16, apt_ordin17, apt_ordin18, apt_ordin19, apt_ordin20, apt_ordin21, apt_ordin22, apt_ordin23, apt_ordin24," & _
              " apt_impeg01, apt_impeg02, apt_impeg03, apt_impeg04, apt_impeg05, apt_impeg06, apt_impeg07, apt_impeg08, apt_impeg09, apt_impeg10, apt_impeg11, apt_impeg12, apt_impeg13, apt_impeg14, apt_impeg15, apt_impeg16, apt_impeg17, apt_impeg18, apt_impeg19, apt_impeg20, apt_impeg21, apt_impeg22, apt_impeg23, apt_impeg24)"
      If bdaMovord Then
        strSQL = strSQL & " SELECT DISTINCT " & CStrSQL(strDitta) & "," & _
              " ko_codart, ko_magaz, ko_commecap, ko_lottop, ko_fase, ko_ubicaz," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0" & _
              " FROM (keyord INNER JOIN artico ON keyord.codditt = artico.codditt AND keyord.ko_codart = artico.ar_codart)" & _
              " LEFT JOIN lotcprotc ON keyord.codditt = lotcprotc.codditt AND keyord.ko_codart = lotcprotc.apt_codart AND keyord.ko_magaz = lotcprotc.apt_magaz AND keyord.ko_commecap = lotcprotc.apt_commeca AND keyord.ko_lottop = lotcprotc.apt_lotto AND keyord.ko_fase = lotcprotc.apt_fase AND keyord.ko_ubicaz = lotcprotc.apt_ubicaz" & _
              " WHERE lotcprotc.codditt IS NULL" & _
              " AND lotcprotc.apt_codart IS NULL" & _
              " AND lotcprotc.apt_magaz IS NULL" & _
              " AND lotcprotc.apt_commeca IS NULL" & _
              " AND lotcprotc.apt_lotto IS NULL" & _
              " AND lotcprotc.apt_fase IS NULL" & _
              " AND lotcprotc.apt_ubicaz IS NULL" & _
              " AND keyord.codditt = " & CStrSQL(strDitta) & _
              " AND keyord.ko_tipork = " & CStrSQL(strTipork) & _
              " AND keyord.ko_anno = " & nAnno & _
              " AND keyord.ko_serie = " & CStrSQL(strSerie) & _
              " AND keyord.ko_numord = " & lNumdoc & _
              " AND artico.ar_codtagl <> 0"
      Else
        strSQL = strSQL & " SELECT DISTINCT " & CStrSQL(strDitta) & ", km_codart, km_magaz, km_commecap," & _
              " km_lottop, km_fase, km_ubicaz," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," & _
              " 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0" & _
              " FROM (keymag INNER JOIN artico ON keymag.codditt = artico.codditt AND keymag.km_codart = artico.ar_codart)" & _
              " LEFT JOIN lotcprotc ON keymag.codditt = lotcprotc.codditt AND keymag.km_codart = lotcprotc.apt_codart AND keymag.km_magaz = lotcprotc.apt_magaz AND keymag.km_commecap = lotcprotc.apt_commeca AND keymag.km_lottop = lotcprotc.apt_lotto AND keymag.km_fase = lotcprotc.apt_fase AND keymag.km_ubicaz = lotcprotc.apt_ubicaz" & _
              " WHERE lotcprotc.codditt IS NULL" & _
              " AND lotcprotc.apt_codart IS NULL" & _
              " AND lotcprotc.apt_magaz IS NULL" & _
              " AND lotcprotc.apt_commeca IS NULL" & _
              " AND lotcprotc.apt_lotto IS NULL" & _
              " AND lotcprotc.apt_fase IS NULL" & _
              " AND lotcprotc.apt_ubicaz IS NULL" & _
              " AND keymag.codditt = " & CStrSQL(strDitta) & _
              " AND keymag.km_tipork = " & CStrSQL(strTipork) & _
              " AND keymag.km_anno = " & nAnno & _
              " AND keymag.km_serie = " & CStrSQL(strSerie) & _
              " AND keymag.km_numdoc = " & lNumdoc & _
              " AND artico.ar_codtagl <> 0"
      End If
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggArtproTC_AggDaMovord(ByVal strDitta As String, ByVal strTipork As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, _
                                          ByVal lNumdoc As Integer, ByVal bStorna As Boolean, _
                                          ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dttMo As New DataTable
    Dim i As Integer = 0
    Dim strSegno As String = " + "
    Dim strBase As String = ""
    Dim dttMotc As New DataTable

    Try
      If bStorna Then strSegno = " - "

      strSQL = "SELECT ko_codart, ko_magaz, ko_commecap, ko_lottop, ko_fase, ko_ubicaz," & _
              " (mo_quant01 - mo_quaeva01) * ko_ordin As Ordin01, (mo_quant02 - mo_quaeva02) * ko_ordin As Ordin02, (mo_quant03 - mo_quaeva03) * ko_ordin As Ordin03, (mo_quant04 - mo_quaeva04) * ko_ordin As Ordin04, (mo_quant05 - mo_quaeva05) * ko_ordin As Ordin05, (mo_quant06 - mo_quaeva06) * ko_ordin As Ordin06, (mo_quant07 - mo_quaeva07) * ko_ordin As Ordin07, (mo_quant08 - mo_quaeva08) * ko_ordin As Ordin08, (mo_quant09 - mo_quaeva09) * ko_ordin As Ordin09, (mo_quant10 - mo_quaeva10) * ko_ordin As Ordin10, (mo_quant11 - mo_quaeva11) * ko_ordin As Ordin11, (mo_quant12 - mo_quaeva12) * ko_ordin As Ordin12," & _
              " (mo_quant13 - mo_quaeva13) * ko_ordin As Ordin13, (mo_quant14 - mo_quaeva14) * ko_ordin As Ordin14, (mo_quant15 - mo_quaeva15) * ko_ordin As Ordin15, (mo_quant16 - mo_quaeva16) * ko_ordin As Ordin16, (mo_quant17 - mo_quaeva17) * ko_ordin As Ordin17, (mo_quant18 - mo_quaeva18) * ko_ordin As Ordin18, (mo_quant19 - mo_quaeva19) * ko_ordin As Ordin19, (mo_quant20 - mo_quaeva20) * ko_ordin As Ordin20, (mo_quant21 - mo_quaeva21) * ko_ordin As Ordin21, (mo_quant22 - mo_quaeva22) * ko_ordin As Ordin22, (mo_quant23 - mo_quaeva23) * ko_ordin As Ordin23, (mo_quant24 - mo_quaeva24) * ko_ordin As Ordin24," & _
              " (mo_quant01 - mo_quaeva01) * ko_impeg As Impeg01, (mo_quant02 - mo_quaeva02) * ko_impeg As Impeg02, (mo_quant03 - mo_quaeva03) * ko_impeg As Impeg03, (mo_quant04 - mo_quaeva04) * ko_impeg As Impeg04, (mo_quant05 - mo_quaeva05) * ko_impeg As Impeg05, (mo_quant06 - mo_quaeva06) * ko_impeg As Impeg06, (mo_quant07 - mo_quaeva07) * ko_impeg As Impeg07, (mo_quant08 - mo_quaeva08) * ko_impeg As Impeg08, (mo_quant09 - mo_quaeva09) * ko_impeg As Impeg09, (mo_quant10 - mo_quaeva10) * ko_impeg As Impeg10, (mo_quant11 - mo_quaeva11) * ko_impeg As Impeg11, (mo_quant12 - mo_quaeva12) * ko_impeg As Impeg12," & _
              " (mo_quant13 - mo_quaeva13) * ko_impeg As Impeg13, (mo_quant14 - mo_quaeva14) * ko_impeg As Impeg14, (mo_quant15 - mo_quaeva15) * ko_impeg As Impeg15, (mo_quant16 - mo_quaeva16) * ko_impeg As Impeg16, (mo_quant17 - mo_quaeva17) * ko_impeg As Impeg17, (mo_quant18 - mo_quaeva18) * ko_impeg As Impeg18, (mo_quant19 - mo_quaeva19) * ko_impeg As Impeg19, (mo_quant20 - mo_quaeva20) * ko_impeg As Impeg20, (mo_quant21 - mo_quaeva21) * ko_impeg As Impeg21, (mo_quant22 - mo_quaeva22) * ko_impeg As Impeg22, (mo_quant23 - mo_quaeva23) * ko_impeg As Impeg23, (mo_quant24 - mo_quaeva24) * ko_impeg As Impeg24" & _
              " FROM (keyord INNER JOIN movordtc ON keyord.codditt = movordtc.codditt AND keyord.ko_tipork = movordtc.mo_tipork AND keyord.ko_anno = movordtc.mo_anno AND keyord.ko_serie = movordtc.mo_serie AND keyord.ko_numord = movordtc.mo_numord AND keyord.ko_riga = movordtc.mo_riga)" & _
              " INNER JOIN movord ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga" & _
              " WHERE keyord.codditt = " & CStrSQL(strDitta) & _
              " AND keyord.ko_tipork = " & CStrSQL(strTipork) & _
              " AND keyord.ko_anno = " & nAnno & _
              " AND keyord.ko_serie = " & CStrSQL(strSerie) & _
              " AND keyord.ko_numord = " & lNumdoc & _
              " AND movord.mo_flevas = 'C'"
      '" AND movord.mo_quant <> 0" 'non filtrare per questo campo: potrei avere delle qta per taglia con segno + ed altre con segno -, la cui somma = 0!
      dttMo = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      For Each dtrT As DataRow In dttMo.Rows
        strBase = ""
        For i = 1 To 24
          If NTSCDec(dtrT("Ordin" & Right("0" & CStr(i), 2)).ToString) <> 0 Then
            strBase += " apt_ordin" & Right("0" & CStr(i), 2) & " = convert(decimal(17, 3), apt_ordin" & Right("0" & CStr(i), 2) & strSegno & CDblSQL(dtrT("Ordin" & Right("0" & CStr(i), 2)).ToString) & "), "
          End If
        Next
        For i = 1 To 24
          If NTSCDec(dtrT("Impeg" & Right("0" & CStr(i), 2)).ToString) <> 0 Then
            strBase += " apt_impeg" & Right("0" & CStr(i), 2) & " = convert(decimal(17, 3), apt_impeg" & Right("0" & CStr(i), 2) & strSegno & CDblSQL(dtrT("Impeg" & Right("0" & CStr(i), 2)).ToString) & "), "
          End If
        Next i
        If strBase.Trim <> "" Then strBase = strBase.Substring(0, strBase.Length - 2)

        If strBase.Trim <> "" Then
          '-----------------------
          'Aggiornamento ordinato/impegnato di ARTPROTC
          strSQL = "UPDATE artprotc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!ko_codart) & _
                  " AND apt_magaz = " & dtrT!ko_magaz.ToString & _
                  " AND apt_fase = " & dtrT!ko_fase.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '-----------------------
          'Aggiornamento ordinato/impegnato di ARTPROXTC
          strSQL = "UPDATE artproxtc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!ko_codart) & _
                  " AND apt_fase = " & dtrT!ko_fase.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '-----------------------
          'Aggiornamento ordinato/impegnato di LOTCPROTC
          strSQL = "UPDATE lotcprotc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!ko_codart) & _
                  " AND apt_magaz = " & dtrT!ko_magaz.ToString & _
                  " AND apt_commeca = " & dtrT!ko_commecap.ToString & _
                  " AND apt_lotto = " & dtrT!ko_lottop.ToString & _
                  " AND apt_fase = " & dtrT!ko_fase.ToString & _
                  " AND apt_ubicaz = " & CStrSQL(dtrT!ko_ubicaz)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      Next    'For Each dtrT As DataRow In dttMo.Rows

      dttMo.Clear()



      dttMotc = OpenRecordset("SELECT TOP 1 * FROM MOVORDTC", CLE__APP.DBTIPO.DBAZI, dbConn)
      If dttMotc.Columns.Contains("mo_qtadis01") Then
        '--------------------------
        'Aggiorna ARTPROTC, ARTPROXTC per la quantità e il valore disimpegnato (ORDINI)
        strSQL = " SELECT ko_codart, ko_magaz, ko_fase, ko_commecap, ko_lottop, ko_ubicaz, " & _
                 " (mo_qtadis01 * ko_ordin) As Ordin01, (mo_qtadis02 * ko_ordin) As Ordin02, (mo_qtadis03 * ko_ordin) As Ordin03, (mo_qtadis04 * ko_ordin) As Ordin04, (mo_qtadis05 * ko_ordin) As Ordin05, (mo_qtadis06 * ko_ordin) As Ordin06, (mo_qtadis07 * ko_ordin) As Ordin07, (mo_qtadis08 * ko_ordin) As Ordin08, (mo_qtadis09 * ko_ordin) As Ordin09, (mo_qtadis10 * ko_ordin) As Ordin10, (mo_qtadis11 * ko_ordin) As Ordin11, (mo_qtadis12 * ko_ordin) As Ordin12, (mo_qtadis13 * ko_ordin) As Ordin13, (mo_qtadis14 * ko_ordin) As Ordin14, (mo_qtadis15 * ko_ordin) As Ordin15, (mo_qtadis16 * ko_ordin) As Ordin16, (mo_qtadis17 * ko_ordin) As Ordin17, (mo_qtadis18 * ko_ordin) As Ordin18, (mo_qtadis19 * ko_ordin) As Ordin19, (mo_qtadis20 * ko_ordin) As Ordin20, (mo_qtadis21 * ko_ordin) As Ordin21, (mo_qtadis22 * ko_ordin) As Ordin22, (mo_qtadis23 * ko_ordin) As Ordin23, (mo_qtadis24 * ko_ordin) As Ordin24," & _
                 " (mo_qtadis01 * ko_impeg) As Impeg01, (mo_qtadis02 * ko_impeg) As Impeg02, (mo_qtadis03 * ko_impeg) As Impeg03, (mo_qtadis04 * ko_impeg) As Impeg04, (mo_qtadis05 * ko_impeg) As Impeg05, (mo_qtadis06 * ko_impeg) As Impeg06, (mo_qtadis07 * ko_impeg) As Impeg07, (mo_qtadis08 * ko_impeg) As Impeg08, (mo_qtadis09 * ko_impeg) As Impeg09, (mo_qtadis10 * ko_impeg) As Impeg10, (mo_qtadis11 * ko_impeg) As Impeg11, (mo_qtadis12 * ko_impeg) As Impeg12, (mo_qtadis13 * ko_impeg) As Impeg13, (mo_qtadis14 * ko_impeg) As Impeg14, (mo_qtadis15 * ko_impeg) As Impeg15, (mo_qtadis16 * ko_impeg) As Impeg16, (mo_qtadis17 * ko_impeg) As Impeg17, (mo_qtadis18 * ko_impeg) As Impeg18, (mo_qtadis19 * ko_impeg) As Impeg19, (mo_qtadis20 * ko_impeg) As Impeg20, (mo_qtadis21 * ko_impeg) As Impeg21, (mo_qtadis22 * ko_impeg) As Impeg22, (mo_qtadis23 * ko_impeg) As Impeg23, (mo_qtadis24 * ko_impeg) As Impeg24" & _
                 " FROM (movord INNER JOIN keyord ON movord.codditt = keyord.codditt AND movord.mo_oatipo = keyord.ko_tipork AND movord.mo_oaanno = keyord.ko_anno AND movord.mo_oaserie = keyord.ko_serie AND movord.mo_oanum = keyord.ko_numord AND movord.mo_oariga = keyord.ko_riga)" & _
                 " INNER JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga" & _
                 " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                 " AND movord.mo_tipork = " & CStrSQL(strTipork) & _
                 " AND movord.mo_anno = " & nAnno & _
                 " AND movord.mo_serie = " & CStrSQL(strSerie) & _
                 " AND movord.mo_numord = " & lNumdoc
        dttMo = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        For Each dtrT As DataRow In dttMo.Rows
          strBase = ""
          For i = 1 To 24
            If NTSCDec(dtrT("Ordin" & Right("0" & CStr(i), 2)).ToString) <> 0 Then
              strBase += " apt_ordin" & Right("0" & CStr(i), 2) & " = convert(decimal(17, 3), apt_ordin" & Right("0" & CStr(i), 2) & IIf(bStorna, " + ", " - ").ToString & CDblSQL(dtrT("Ordin" & Right("0" & CStr(i), 2)).ToString) & "), "
            End If
          Next
          For i = 1 To 24
            If NTSCDec(dtrT("Impeg" & Right("0" & CStr(i), 2)).ToString) <> 0 Then
              strBase += " apt_impeg" & Right("0" & CStr(i), 2) & " = convert(decimal(17, 3), apt_impeg" & Right("0" & CStr(i), 2) & IIf(bStorna, " + ", " - ").ToString & CDblSQL(dtrT("Impeg" & Right("0" & CStr(i), 2)).ToString) & "), "
            End If
          Next
          If strBase.Trim <> "" Then strBase = strBase.Substring(0, strBase.Length - 2)

          If strBase.Trim <> "" Then
            '--------------------------
            'ORDINI: Aggiornamento ARTPROTC per la quantità e il valore disimpegnato
            strSQL = "UPDATE artprotc SET " & strBase & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND apt_codart = " & CStrSQL(dtrT!ko_codart) & _
                    " AND apt_magaz = " & dtrT!ko_magaz.ToString & _
                    " AND apt_fase = " & dtrT!ko_fase.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            '--------------------------
            'ORDINI: Aggiornamento ARTPROXTC per la quantità e il valore disimpegnato
            strSQL = "UPDATE artproxtc SET " & strBase & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND apt_codart = " & CStrSQL(dtrT!ko_codart) & _
                    " AND apt_fase = " & dtrT!ko_fase.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            '--------------------------
            'ORDINI: Aggiornamento LOTCPROTC per la quantità e il valore disimpegnato
            strSQL = "UPDATE lotcprotc SET " & strBase & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND apt_codart = " & CStrSQL(dtrT!ko_codart) & _
                    " AND apt_magaz = " & dtrT!ko_magaz.ToString & _
                    " AND apt_commeca = " & dtrT!ko_commecap.ToString & _
                    " AND apt_lotto = " & dtrT!ko_lottop.ToString & _
                    " AND apt_fase = " & dtrT!ko_fase.ToString & _
                    " AND apt_ubicaz = " & CStrSQL(dtrT!ko_ubicaz)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If    'If strBase.Trim <> "" Then
        Next
        dttMo.Clear()
      End If

      Return True

    Catch ex As Exception
      dttMo.Clear()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggArtproTC_AggDaMovprb(ByVal strDitta As String, ByVal strTipork As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, _
                                          ByVal lNumdoc As Integer, ByVal bStorna As Boolean, _
                                          ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dttMm As New DataTable
    Dim i As Integer = 0
    Dim strSegno As String = " + "
    Dim strBase As String = ""
    Try
      If bStorna Then strSegno = " - "

      strSQL = "SELECT keyprb.*," & _
                " ((mm_quant01 - mm_nprquae01) * km_carscar) As Prenot01, ((mm_quant02 - mm_nprquae02) * km_carscar) As Prenot02, ((mm_quant03 - mm_nprquae03) * km_carscar) As Prenot03, ((mm_quant04 - mm_nprquae04) * km_carscar) As Prenot04, ((mm_quant05 - mm_nprquae05) * km_carscar) As Prenot05, ((mm_quant06 - mm_nprquae06) * km_carscar) As Prenot06," & _
                " ((mm_quant07 - mm_nprquae07) * km_carscar) As Prenot07, ((mm_quant08 - mm_nprquae08) * km_carscar) As Prenot08, ((mm_quant09 - mm_nprquae09) * km_carscar) As Prenot09, ((mm_quant10 - mm_nprquae10) * km_carscar) As Prenot10, ((mm_quant11 - mm_nprquae11) * km_carscar) As Prenot11, ((mm_quant12 - mm_nprquae12) * km_carscar) As Prenot12," & _
                " ((mm_quant13 - mm_nprquae13) * km_carscar) As Prenot13, ((mm_quant14 - mm_nprquae14) * km_carscar) As Prenot14, ((mm_quant15 - mm_nprquae15) * km_carscar) As Prenot15, ((mm_quant16 - mm_nprquae16) * km_carscar) As Prenot16, ((mm_quant17 - mm_nprquae17) * km_carscar) As Prenot17, ((mm_quant18 - mm_nprquae18) * km_carscar) As Prenot18," & _
                " ((mm_quant19 - mm_nprquae19) * km_carscar) As Prenot19, ((mm_quant20 - mm_nprquae20) * km_carscar) As Prenot20, ((mm_quant21 - mm_nprquae21) * km_carscar) As Prenot21, ((mm_quant22 - mm_nprquae22) * km_carscar) As Prenot22, ((mm_quant23 - mm_nprquae23) * km_carscar) As Prenot23, ((mm_quant24 - mm_nprquae24) * km_carscar) As Prenot24" & _
                " FROM (movprb INNER JOIN keyprb ON movprb.codditt = keyprb.codditt AND movprb.mm_tipork = keyprb.km_tipork AND movprb.mm_anno = keyprb.km_anno AND movprb.mm_serie = keyprb.km_serie AND movprb.mm_numdoc = keyprb.km_numdoc AND movprb.mm_riga = keyprb.km_riga)" & _
                " INNER JOIN movmagtc ON movprb.codditt = movmagtc.codditt AND movprb.mm_tipork = movmagtc.mm_tipork AND movprb.mm_anno = movmagtc.mm_anno AND movprb.mm_serie = movmagtc.mm_serie AND movprb.mm_numdoc = movmagtc.mm_numdoc AND movprb.mm_riga = movmagtc.mm_riga" & _
                " WHERE keyprb.codditt = " & CStrSQL(strDitta) & _
                " AND keyprb.km_tipork = " & CStrSQL(strTipork) & _
                " AND keyprb.km_anno = " & nAnno & _
                " AND keyprb.km_serie = " & CStrSQL(strSerie) & _
                " AND keyprb.km_numdoc = " & lNumdoc & _
                " AND movprb.mm_nprflevas = 'C'"
      dttMm = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      For Each dtrT As DataRow In dttMm.Rows
        strBase = ""
        For i = 1 To 24
          If NTSCDec(dtrT("Prenot" & Right("0" & CStr(i), 2)).ToString) <> 0 Then
            strBase += " apt_prenot" & Right("0" & CStr(i), 2) & " = convert(decimal(17, 3), apt_prenot" & Right("0" & CStr(i), 2) & strSegno & CDblSQL(dtrT("Prenot" & Right("0" & CStr(i), 2)).ToString) & "), "
          End If
        Next
        If strBase.Trim <> "" Then strBase = strBase.Substring(0, strBase.Length - 2)

        If strBase.Trim <> "" Then
          '--------------------------
          'Aggiornamento prenotato ARTPROTC
          strSQL = "UPDATE artprotc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!km_codart) & _
                  " AND apt_magaz = " & dtrT!km_magaz.ToString & _
                  " AND apt_fase = " & dtrT!km_fase.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '--------------------------
          'Aggiornamento prenotato ARTPROXTC
          strSQL = "UPDATE artproxtc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!km_codart) & _
                  " AND apt_fase = " & dtrT!km_fase.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '--------------------------
          'Aggiornamento prenotato LOTCPROTC
          strSQL = "UPDATE lotcprotc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!km_codart) & _
                  " AND apt_magaz = " & dtrT!km_magaz.ToString & _
                  " AND apt_commeca = " & dtrT!km_commecap.ToString & _
                  " AND apt_lotto = " & dtrT!km_lottop.ToString & _
                  " AND apt_fase = " & dtrT!km_fase.ToString & _
                  " AND apt_ubicaz = " & CStrSQL(dtrT!km_ubicaz)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If    'If strBase.Trim <> "" Then
      Next
      dttMm.Clear()

      Return True

    Catch ex As Exception
      dttMm.Clear()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function AggArtproTC_AggDaMovmag(ByVal strDitta As String, ByVal strTipork As String, _
                                        ByVal nAnno As Integer, ByVal strSerie As String, _
                                        ByVal lNumdoc As Integer, ByVal bStorna As Boolean, _
                                        ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dttMm As New DataTable
    Dim i As Integer = 0
    Dim strSegno As String = " + "
    Dim strBase As String = ""
    Try
      If bStorna Then strSegno = " - "

      strSQL = "SELECT km_codart, km_magaz, km_fase, km_commecap, km_lottop, km_ubicaz, " & _
              " (mm_quant01 * tb_esist) As Esist01, (mm_quant02 * tb_esist) As Esist02, (mm_quant03 * tb_esist) As Esist03, (mm_quant04 * tb_esist) As Esist04, (mm_quant05 * tb_esist) As Esist05, (mm_quant06 * tb_esist) As Esist06," & _
              " (mm_quant07 * tb_esist) As Esist07, (mm_quant08 * tb_esist) As Esist08, (mm_quant09 * tb_esist) As Esist09, (mm_quant10 * tb_esist) As Esist10, (mm_quant11 * tb_esist) As Esist11, (mm_quant12 * tb_esist) As Esist12," & _
              " (mm_quant13 * tb_esist) As Esist13, (mm_quant14 * tb_esist) As Esist14, (mm_quant15 * tb_esist) As Esist15, (mm_quant16 * tb_esist) As Esist16, (mm_quant17 * tb_esist) As Esist17, (mm_quant18 * tb_esist) As Esist18," & _
              " (mm_quant19 * tb_esist) As Esist19, (mm_quant20 * tb_esist) As Esist20, (mm_quant21 * tb_esist) As Esist21, (mm_quant22 * tb_esist) As Esist22, (mm_quant23 * tb_esist) As Esist23, (mm_quant24 * tb_esist) As Esist24" & _
              " FROM ((movmag INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_tipork = keymag.km_tipork AND movmag.mm_anno = keymag.km_anno AND movmag.mm_serie = keymag.km_serie AND movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_riga = keymag.km_riga)" & _
              " INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga)" & _
              " INNER JOIN tabcaum ON tabcaum.tb_codcaum = keymag.km_causale" & _
              " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
              " AND keymag.km_tipork = '" & strTipork & "'" & _
              " AND keymag.km_anno = " & nAnno & _
              " AND keymag.km_serie = '" & strSerie & "'" & _
              " AND keymag.km_numdoc = " & lNumdoc
      '" AND movmag.mm_quant <> 0" 'non filtrare per questo campo: potrei avere delle qta per taglia con segno + ed altre con segno -, la cui somma = 0!
      dttMm = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      For Each dtrT As DataRow In dttMm.Rows
        strBase = ""
        For i = 1 To 24
          If NTSCDec(dtrT("Esist" & Right("0" & CStr(i), 2)).ToString) <> 0 Then
            strBase += " apt_esist" & Right("0" & CStr(i), 2) & " = convert(decimal(17, 3), apt_esist" & Right("0" & CStr(i), 2) & strSegno & CDblSQL(dtrT("Esist" & Right("0" & CStr(i), 2)).ToString) & "), "
          End If
        Next
        If strBase.Trim <> "" Then strBase = strBase.Substring(0, strBase.Length - 2)

        If strBase.Trim <> "" Then
          '--------------------------
          'Aggiornamento esistenza ARTPROTC
          strSQL = "UPDATE artprotc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!km_codart) & _
                  " AND apt_magaz = " & dtrT!km_magaz.ToString & _
                  " AND apt_fase = " & dtrT!km_fase.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '--------------------------
          'Aggiornamento esistenza ARTPROXTC
          strSQL = "UPDATE artproxtc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!km_codart) & _
                  " AND apt_fase = " & dtrT!km_fase.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '--------------------------
          'Aggiornamento esistenza LOTCPROTC
          strSQL = "UPDATE lotcprotc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!km_codart) & _
                  " AND apt_magaz = " & dtrT!km_magaz.ToString & _
                  " AND apt_commeca = " & dtrT!km_commecap.ToString & _
                  " AND apt_lotto = " & dtrT!km_lottop.ToString & _
                  " AND apt_fase = " & dtrT!km_fase.ToString & _
                  " AND apt_ubicaz = " & CStrSQL(dtrT!km_ubicaz)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If    'If strBase.Trim <> "" Then
      Next
      dttMm.Clear()

      '--------------------------
      'Aggiorna ARTPROTC, ARTPROXTC per la quantità e il valore disimpegnato (ORDINI)
      strSQL = "SELECT ko_codart, ko_magaz, ko_fase, ko_commecap, ko_lottop, ko_ubicaz, " & _
                      " (mm_qtadis01 * ko_ordin) As Ordin01, (mm_qtadis02 * ko_ordin) As Ordin02, (mm_qtadis03 * ko_ordin) As Ordin03, (mm_qtadis04 * ko_ordin) As Ordin04, (mm_qtadis05 * ko_ordin) As Ordin05, (mm_qtadis06 * ko_ordin) As Ordin06, (mm_qtadis07 * ko_ordin) As Ordin07, (mm_qtadis08 * ko_ordin) As Ordin08, (mm_qtadis09 * ko_ordin) As Ordin09, (mm_qtadis10 * ko_ordin) As Ordin10, (mm_qtadis11 * ko_ordin) As Ordin11, (mm_qtadis12 * ko_ordin) As Ordin12, (mm_qtadis13 * ko_ordin) As Ordin13, (mm_qtadis14 * ko_ordin) As Ordin14, (mm_qtadis15 * ko_ordin) As Ordin15, (mm_qtadis16 * ko_ordin) As Ordin16, (mm_qtadis17 * ko_ordin) As Ordin17, (mm_qtadis18 * ko_ordin) As Ordin18, (mm_qtadis19 * ko_ordin) As Ordin19, (mm_qtadis20 * ko_ordin) As Ordin20, (mm_qtadis21 * ko_ordin) As Ordin21, (mm_qtadis22 * ko_ordin) As Ordin22, (mm_qtadis23 * ko_ordin) As Ordin23, (mm_qtadis24 * ko_ordin) As Ordin24," & _
                      " (mm_qtadis01 * ko_impeg) As Impeg01, (mm_qtadis02 * ko_impeg) As Impeg02, (mm_qtadis03 * ko_impeg) As Impeg03, (mm_qtadis04 * ko_impeg) As Impeg04, (mm_qtadis05 * ko_impeg) As Impeg05, (mm_qtadis06 * ko_impeg) As Impeg06, (mm_qtadis07 * ko_impeg) As Impeg07, (mm_qtadis08 * ko_impeg) As Impeg08, (mm_qtadis09 * ko_impeg) As Impeg09, (mm_qtadis10 * ko_impeg) As Impeg10, (mm_qtadis11 * ko_impeg) As Impeg11, (mm_qtadis12 * ko_impeg) As Impeg12, (mm_qtadis13 * ko_impeg) As Impeg13, (mm_qtadis14 * ko_impeg) As Impeg14, (mm_qtadis15 * ko_impeg) As Impeg15, (mm_qtadis16 * ko_impeg) As Impeg16, (mm_qtadis17 * ko_impeg) As Impeg17, (mm_qtadis18 * ko_impeg) As Impeg18, (mm_qtadis19 * ko_impeg) As Impeg19, (mm_qtadis20 * ko_impeg) As Impeg20, (mm_qtadis21 * ko_impeg) As Impeg21, (mm_qtadis22 * ko_impeg) As Impeg22, (mm_qtadis23 * ko_impeg) As Impeg23, (mm_qtadis24 * ko_impeg) As Impeg24" & _
                      " FROM (movmag INNER JOIN keyord ON movmag.codditt = keyord.codditt AND movmag.mm_ortipo = keyord.ko_tipork AND movmag.mm_oranno = keyord.ko_anno AND movmag.mm_orserie = keyord.ko_serie AND movmag.mm_ornum = keyord.ko_numord AND movmag.mm_orriga = keyord.ko_riga)" & _
                      " INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
                      " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                      " AND movmag.mm_tipork = " & CStrSQL(strTipork) & _
                      " AND movmag.mm_anno = " & nAnno & _
                      " AND movmag.mm_serie = " & CStrSQL(strSerie) & _
                      " AND movmag.mm_numdoc = " & lNumdoc
      dttMm = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      For Each dtrT As DataRow In dttMm.Rows
        strBase = ""
        For i = 1 To 24
          If NTSCDec(dtrT("Ordin" & Right("0" & CStr(i), 2)).ToString) <> 0 Then
            strBase += " apt_ordin" & Right("0" & CStr(i), 2) & " = convert(decimal(17, 3), apt_ordin" & Right("0" & CStr(i), 2) & IIf(bStorna, " + ", " - ").ToString & CDblSQL(dtrT("Ordin" & Right("0" & CStr(i), 2)).ToString) & "), "
          End If
        Next
        For i = 1 To 24
          If NTSCDec(dtrT("Impeg" & Right("0" & CStr(i), 2)).ToString) <> 0 Then
            strBase += " apt_impeg" & Right("0" & CStr(i), 2) & " = convert(decimal(17, 3), apt_impeg" & Right("0" & CStr(i), 2) & IIf(bStorna, " + ", " - ").ToString & CDblSQL(dtrT("Impeg" & Right("0" & CStr(i), 2)).ToString) & "), "
          End If
        Next
        If strBase.Trim <> "" Then strBase = strBase.Substring(0, strBase.Length - 2)

        If strBase.Trim <> "" Then
          '--------------------------
          'ORDINI: Aggiornamento ARTPROTC per la quantità e il valore disimpegnato
          strSQL = "UPDATE artprotc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!ko_codart) & _
                  " AND apt_magaz = " & dtrT!ko_magaz.ToString & _
                  " AND apt_fase = " & dtrT!ko_fase.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '--------------------------
          'ORDINI: Aggiornamento ARTPROXTC per la quantità e il valore disimpegnato
          strSQL = "UPDATE artproxtc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!ko_codart) & _
                  " AND apt_fase = " & dtrT!ko_fase.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '--------------------------
          'ORDINI: Aggiornamento LOTCPROTC per la quantità e il valore disimpegnato
          strSQL = "UPDATE lotcprotc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!ko_codart) & _
                  " AND apt_magaz = " & dtrT!ko_magaz.ToString & _
                  " AND apt_commeca = " & dtrT!ko_commecap.ToString & _
                  " AND apt_lotto = " & dtrT!ko_lottop.ToString & _
                  " AND apt_fase = " & dtrT!ko_fase.ToString & _
                  " AND apt_ubicaz = " & CStrSQL(dtrT!ko_ubicaz)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If    'If strBase.Trim <> "" Then
      Next
      dttMm.Clear()

      '--------------------------
      'Aggiorna ARTPROTC, ARTPROXTC per la quantità e il valore prenotato (NOTE PREL)
      strSQL = "SELECT km_codart, km_magaz, km_fase, km_commecap, km_lottop, km_ubicaz, " & _
              " (mm_npqtadis01 * km_carscar) As Prenot01, (mm_npqtadis02 * km_carscar) As Prenot02, (mm_npqtadis03 * km_carscar) As Prenot03, (mm_npqtadis04 * km_carscar) As Prenot04, (mm_npqtadis05 * km_carscar) As Prenot05, (mm_npqtadis06 * km_carscar) As Prenot06," & _
              " (mm_npqtadis07 * km_carscar) As Prenot07, (mm_npqtadis08 * km_carscar) As Prenot08, (mm_npqtadis09 * km_carscar) As Prenot09, (mm_npqtadis10 * km_carscar) As Prenot10, (mm_npqtadis11 * km_carscar) As Prenot11, (mm_npqtadis12 * km_carscar) As Prenot12," & _
              " (mm_npqtadis13 * km_carscar) As Prenot13, (mm_npqtadis14 * km_carscar) As Prenot14, (mm_npqtadis15 * km_carscar) As Prenot15, (mm_npqtadis16 * km_carscar) As Prenot16, (mm_npqtadis17 * km_carscar) As Prenot17, (mm_npqtadis18 * km_carscar) As Prenot18," & _
              " (mm_npqtadis19 * km_carscar) As Prenot19, (mm_npqtadis20 * km_carscar) As Prenot20, (mm_npqtadis21 * km_carscar) As Prenot21, (mm_npqtadis22 * km_carscar) As Prenot22, (mm_npqtadis23 * km_carscar) As Prenot23, (mm_npqtadis24 * km_carscar) As Prenot24" & _
              " FROM (movmag INNER JOIN keyprb ON movmag.codditt = keyprb.codditt AND movmag.mm_nptipo = keyprb.km_tipork AND movmag.mm_npanno = keyprb.km_anno AND movmag.mm_npserie = keyprb.km_serie AND movmag.mm_npnum = keyprb.km_numdoc AND movmag.mm_npriga = keyprb.km_riga)" & _
              " INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
              " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
              " AND movmag.mm_tipork = " & CStrSQL(strTipork) & _
              " AND movmag.mm_anno = " & nAnno & _
              " AND movmag.mm_serie = " & CStrSQL(strSerie) & _
              " AND movmag.mm_numdoc = " & lNumdoc
      dttMm = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      For Each dtrT As DataRow In dttMm.Rows
        strBase = ""
        For i = 1 To 24
          If NTSCDec(dtrT("Prenot" & Right("0" & CStr(i), 2)).ToString) <> 0 Then
            strBase += " apt_prenot" & Right("0" & CStr(i), 2) & " = convert(decimal(17, 3), apt_prenot" & Right("0" & CStr(i), 2) & IIf(bStorna, " + ", " - ").ToString & CDblSQL(dtrT("Prenot" & Right("0" & CStr(i), 2)).ToString) & "), "
          End If
        Next
        If strBase.Trim <> "" Then strBase = strBase.Substring(0, strBase.Length - 2)

        If strBase.Trim <> "" Then
          '--------------------------
          'NOTE: Aggiornamento ARTPROTC per la quantità e il valore disimpegnato
          strSQL = "UPDATE artprotc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!km_codart) & _
                  " AND apt_magaz = " & dtrT!km_magaz.ToString & _
                  " AND apt_fase = " & dtrT!km_fase.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '--------------------------
          'NOTE: Aggiornamento ARTPROXTC per la quantità e il valore disimpegnato
          strSQL = "UPDATE artproxtc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!km_codart) & _
                  " AND apt_fase = " & dtrT!km_fase.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          '--------------------------
          'NOTE: Aggiornamento LOTCPROTC per la quantità e il valore disimpegnato
          strSQL = "UPDATE lotcprotc SET " & strBase & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND apt_codart = " & CStrSQL(dtrT!km_codart) & _
                  " AND apt_magaz = " & dtrT!km_magaz.ToString & _
                  " AND apt_commeca = " & dtrT!km_commecap.ToString & _
                  " AND apt_lotto = " & dtrT!km_lottop.ToString & _
                  " AND apt_fase = " & dtrT!km_fase.ToString & _
                  " AND apt_ubicaz = " & CStrSQL(dtrT!km_ubicaz)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If    ' If strBase.Trim <> "" Then
      Next
      dttMm.Clear()

      Return True

    Catch ex As Exception
      dttMm.Clear()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggMovOrdTC(ByVal strDitta As String, ByVal strTipork As String, _
                                          ByVal nAnno As Integer, ByVal strSerie As String, _
                                          ByVal lNumdoc As Integer, ByVal nOperaz As Integer, _
                                          ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim n As Integer = 0
    Try
      If nOperaz = 0 Then
        If strTipork = "W" Then
          '------------------------
          'Aggiorna le quantità prenotate sull'ordine
          strSQL = "SELECT movprb.*," & _
                  " (mm_quant01 - mm_nprquae01) As Quapre01, (mm_quant02 - mm_nprquae02) As Quapre02, (mm_quant03 - mm_nprquae03) As Quapre03, (mm_quant04 - mm_nprquae04) As Quapre04, (mm_quant05 - mm_nprquae05) As Quapre05, (mm_quant06 - mm_nprquae06) As Quapre06, (mm_quant07 - mm_nprquae07) As Quapre07, (mm_quant08 - mm_nprquae08) As Quapre08, (mm_quant09 - mm_nprquae09) As Quapre09, (mm_quant10 - mm_nprquae10) As Quapre10, (mm_quant11 - mm_nprquae11) As Quapre11, (mm_quant12 - mm_nprquae12) As Quapre12, (mm_quant13 - mm_nprquae13) As Quapre13, (mm_quant14 - mm_nprquae14) As Quapre14, (mm_quant15 - mm_nprquae15) As Quapre15, (mm_quant16 - mm_nprquae16) As Quapre16, (mm_quant17 - mm_nprquae17) As Quapre17, (mm_quant18 - mm_nprquae18) As Quapre18, (mm_quant19 - mm_nprquae19) As Quapre19, (mm_quant20 - mm_nprquae20) As Quapre20, (mm_quant21 - mm_nprquae21) As Quapre21, (mm_quant22 - mm_nprquae22) As Quapre22, (mm_quant23 - mm_nprquae23) As Quapre23, (mm_quant24 - mm_nprquae24) As Quapre24" & _
                  " FROM movprb INNER JOIN movmagtc ON movprb.codditt = movmagtc.codditt AND movprb.mm_tipork = movmagtc.mm_tipork AND movprb.mm_anno = movmagtc.mm_anno AND movprb.mm_serie = movmagtc.mm_serie AND movprb.mm_numdoc = movmagtc.mm_numdoc AND movprb.mm_riga = movmagtc.mm_riga" & _
                  " WHERE movprb.codditt = " & CStrSQL(strDitta) & _
                  " AND movprb.mm_tipork = " & CStrSQL(strTipork) & _
                  " AND movprb.mm_anno = " & nAnno & _
                  " AND movprb.mm_serie = " & CStrSQL(strSerie) & _
                  " AND movprb.mm_numdoc = " & lNumdoc & " " & _
                  " AND movprb.mm_quant >= movprb.mm_nprquaeva" & _
                  " AND movprb.mm_ornum > 0"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          For n = 0 To dttTmp.Rows.Count - 1
            strSQL = "UPDATE movordtc SET"
            For i = 1 To 24
              strSQL = strSQL & " mo_quapre" & i.ToString("00") & " = mo_quapre" & i.ToString("00") & " + " & CDblSQL(dttTmp.Rows(n)("Quapre" & i.ToString("00")).ToString) & ", "
            Next
            strSQL = strSQL.Substring(0, strSQL.Length - 2)
            strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mo_tipork = " & CStrSQL(dttTmp.Rows(n)!mm_ortipo) & _
                            " AND mo_anno = " & dttTmp.Rows(n)!mm_oranno.ToString & _
                            " AND mo_serie = " & CStrSQL(dttTmp.Rows(n)!mm_orserie) & _
                            " AND mo_numord = " & dttTmp.Rows(n)!mm_ornum.ToString & _
                            " AND mo_riga = " & dttTmp.Rows(n)!mm_orriga.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
          dttTmp.Clear()
        Else
          '------------------------
          'Aggiorna le quantità evase sull'ordine
          strSQL = "SELECT movmag.*," & _
                  " mm_quant01 As Quant01, mm_quant02 As Quant02, mm_quant03 As Quant03, mm_quant04 As Quant04, mm_quant05 As Quant05, mm_quant06 As Quant06, mm_quant07 As Quant07, mm_quant08 As Quant08, mm_quant09 As Quant09, mm_quant10 As Quant10, mm_quant11 As Quant11, mm_quant12 As Quant12, mm_quant13 As Quant13, mm_quant14 As Quant14, mm_quant15 As Quant15, mm_quant16 As Quant16, mm_quant17 As Quant17, mm_quant18 As Quant18, mm_quant19 As Quant19, mm_quant20 As Quant20, mm_quant21 As Quant21, mm_quant22 As Quant22, mm_quant23 As Quant23, mm_quant24 As Quant24" & _
                  " FROM movmag INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
                  " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                  " AND movmag.mm_tipork = " & CStrSQL(strTipork) & _
                  " AND movmag.mm_anno = " & nAnno & _
                  " AND movmag.mm_serie = " & CStrSQL(strSerie) & _
                  " AND movmag.mm_numdoc = " & lNumdoc & _
                  " AND movmag.mm_salcon = 'C'" & _
                  " AND movmag.mm_ornum > 0"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          For n = 0 To dttTmp.Rows.Count - 1
            strSQL = "UPDATE movordtc SET"
            For i = 1 To 24
              strSQL = strSQL & " mo_quaeva" & i.ToString("00") & " = mo_quaeva" & i.ToString("00") & " + " & CDblSQL(dttTmp.Rows(n)("Quant" & i.ToString("00")).ToString) & ", "
            Next
            strSQL = strSQL.Substring(0, strSQL.Length - 2)
            strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mo_tipork = " & CStrSQL(dttTmp.Rows(n)!mm_ortipo) & _
                            " AND mo_anno = " & dttTmp.Rows(n)!mm_oranno.ToString & _
                            " AND mo_serie = " & CStrSQL(dttTmp.Rows(n)!mm_orserie) & _
                            " AND mo_numord = " & dttTmp.Rows(n)!mm_ornum.ToString & _
                            " AND mo_riga = " & dttTmp.Rows(n)!mm_orriga.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
          '------------------------
          strSQL = "SELECT movmag.*," & _
                  " mm_quant01 As Quant01, mm_quant02 As Quant02, mm_quant03 As Quant03, mm_quant04 As Quant04, mm_quant05 As Quant05, mm_quant06 As Quant06, mm_quant07 As Quant07, mm_quant08 As Quant08, mm_quant09 As Quant09, mm_quant10 As Quant10, mm_quant11 As Quant11, mm_quant12 As Quant12, mm_quant13 As Quant13, mm_quant14 As Quant14, mm_quant15 As Quant15, mm_quant16 As Quant16, mm_quant17 As Quant17, mm_quant18 As Quant18, mm_quant19 As Quant19, mm_quant20 As Quant20, mm_quant21 As Quant21, mm_quant22 As Quant22, mm_quant23 As Quant23, mm_quant24 As Quant24" & _
                  " FROM movmag INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
                  " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                  " AND movmag.mm_tipork = " & CStrSQL(strTipork) & _
                  " AND movmag.mm_anno = " & nAnno & _
                  " AND movmag.mm_serie = " & CStrSQL(strSerie) & _
                  " AND movmag.mm_numdoc = " & lNumdoc & _
                  " AND movmag.mm_salcon = 'S'" & _
                  " AND movmag.mm_ornum > 0"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          For n = 0 To dttTmp.Rows.Count - 1
            strSQL = "UPDATE movordtc SET"
            For i = 1 To 24
              strSQL = strSQL & " mo_quaeva" & i.ToString("00") & " = mo_quaeva" & i.ToString("00") & " + " & CDblSQL(dttTmp.Rows(n)("Quant" & i.ToString("00")).ToString) & ", "
            Next
            strSQL = strSQL.Substring(0, strSQL.Length - 2)
            strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mo_tipork = " & CStrSQL(dttTmp.Rows(n)!mm_ortipo) & _
                            " AND mo_anno = " & dttTmp.Rows(n)!mm_oranno.ToString & _
                            " AND mo_serie = " & CStrSQL(dttTmp.Rows(n)!mm_orserie) & _
                            " AND mo_numord = " & dttTmp.Rows(n)!mm_ornum.ToString & _
                            " AND mo_riga = " & dttTmp.Rows(n)!mm_orriga.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
          '------------------------
          'Aggiorna le quantità evase sulla nota
          strSQL = "SELECT movmag.*," & _
                  " mm_quant01 As Quant01, mm_quant02 As Quant02, mm_quant03 As Quant03, mm_quant04 As Quant04, mm_quant05 As Quant05, mm_quant06 As Quant06, mm_quant07 As Quant07, mm_quant08 As Quant08, mm_quant09 As Quant09, mm_quant10 As Quant10, mm_quant11 As Quant11, mm_quant12 As Quant12, mm_quant13 As Quant13, mm_quant14 As Quant14, mm_quant15 As Quant15, mm_quant16 As Quant16, mm_quant17 As Quant17, mm_quant18 As Quant18, mm_quant19 As Quant19, mm_quant20 As Quant20, mm_quant21 As Quant21, mm_quant22 As Quant22, mm_quant23 As Quant23, mm_quant24 As Quant24" & _
                  " FROM movmag INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
                  " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                  " AND movmag.mm_tipork = '" & strTipork & "'" & _
                  " AND movmag.mm_anno = " & nAnno & _
                  " AND movmag.mm_serie = '" & strSerie & "'" & _
                  " AND movmag.mm_numdoc = " & lNumdoc & " " & _
                  " AND movmag.mm_npsalcon = 'C'" & _
                  " AND movmag.mm_npnum > 0"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          For n = 0 To dttTmp.Rows.Count - 1
            strSQL = "UPDATE movmagtc SET"
            For i = 1 To 24
              strSQL = strSQL & " mm_nprquae" & i.ToString("00") & " = mm_nprquae" & i.ToString("00") & " + " & CDblSQL(dttTmp.Rows(n)("Quant" & i.ToString("00")).ToString) & ", "
            Next
            strSQL = strSQL.Substring(0, strSQL.Length - 2)
            strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mm_tipork = " & CStrSQL(dttTmp.Rows(n)!mm_nptipo) & _
                            " AND mm_anno = " & dttTmp.Rows(n)!mm_npanno.ToString & _
                            " AND mm_serie = " & CStrSQL(dttTmp.Rows(n)!mm_npserie) & _
                            " AND mm_numdoc = " & dttTmp.Rows(n)!mm_npnum.ToString & _
                            " AND mm_riga = " & dttTmp.Rows(n)!mm_npriga.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
          '------------------------
          strSQL = "SELECT movmag.*," & _
                  " mm_quant01 As Quant01, mm_quant02 As Quant02, mm_quant03 As Quant03, mm_quant04 As Quant04, mm_quant05 As Quant05, mm_quant06 As Quant06, mm_quant07 As Quant07, mm_quant08 As Quant08, mm_quant09 As Quant09, mm_quant10 As Quant10, mm_quant11 As Quant11, mm_quant12 As Quant12, mm_quant13 As Quant13, mm_quant14 As Quant14, mm_quant15 As Quant15, mm_quant16 As Quant16, mm_quant17 As Quant17, mm_quant18 As Quant18, mm_quant19 As Quant19, mm_quant20 As Quant20, mm_quant21 As Quant21, mm_quant22 As Quant22, mm_quant23 As Quant23, mm_quant24 As Quant24" & _
                  " FROM movmag INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
                  " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                  " AND movmag.mm_tipork = " & CStrSQL(strTipork) & _
                  " AND movmag.mm_anno = " & nAnno & _
                  " AND movmag.mm_serie = " & CStrSQL(strSerie) & _
                  " AND movmag.mm_numdoc = " & lNumdoc & _
                  " AND movmag.mm_npsalcon = 'S'" & _
                  " AND movmag.mm_npnum > 0"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          For n = 0 To dttTmp.Rows.Count - 1
            strSQL = "UPDATE movmagtc SET"
            For i = 1 To 24
              strSQL = strSQL & " mm_nprquae" & i.ToString("00") & " = mm_nprquae" & i.ToString("00") & " + " & CDblSQL(dttTmp.Rows(n)("Quant" & i.ToString("00")).ToString) & ", "
            Next
            strSQL = strSQL.Substring(0, strSQL.Length - 2)
            strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mm_tipork = " & CStrSQL(dttTmp.Rows(n)!mm_nptipo) & _
                            " AND mm_anno = " & dttTmp.Rows(n)!mm_npanno.ToString & _
                            " AND mm_serie = " & CStrSQL(dttTmp.Rows(n)!mm_npserie) & _
                            " AND mm_numdoc = " & dttTmp.Rows(n)!mm_npnum.ToString & _
                            " AND mm_riga = " & dttTmp.Rows(n)!mm_npriga.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
          '------------------------
          strSQL = "SELECT movmag.*," & _
                  " mm_npqtadis01 As Quapre01, mm_npqtadis02 As Quapre02, mm_npqtadis03 As Quapre03, mm_npqtadis04 As Quapre04, mm_npqtadis05 As Quapre05, mm_npqtadis06 As Quapre06, mm_npqtadis07 As Quapre07, mm_npqtadis08 As Quapre08, mm_npqtadis09 As Quapre09, mm_npqtadis10 As Quapre10, mm_npqtadis11 As Quapre11, mm_npqtadis12 As Quapre12, mm_npqtadis13 As Quapre13, mm_npqtadis14 As Quapre14, mm_npqtadis15 As Quapre15, mm_npqtadis16 As Quapre16, mm_npqtadis17 As Quapre17, mm_npqtadis18 As Quapre18, mm_npqtadis19 As Quapre19, mm_npqtadis20 As Quapre20, mm_npqtadis21 As Quapre21, mm_npqtadis22 As Quapre22, mm_npqtadis23 As Quapre23, mm_npqtadis24 As Quapre24" & _
                  " FROM movmag INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
                  " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                  " AND movmag.mm_tipork = " & CStrSQL(strTipork) & _
                  " AND movmag.mm_anno = " & nAnno & _
                  " AND movmag.mm_serie = " & CStrSQL(strSerie) & _
                  " AND movmag.mm_numdoc = " & lNumdoc & _
                  " AND movmag.mm_ornum > 0" & _
                  " AND movmag.mm_npnum > 0"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          For n = 0 To dttTmp.Rows.Count - 1
            strSQL = "UPDATE movordtc SET"
            For i = 1 To 24
              strSQL = strSQL & " mo_quapre" & i.ToString("00") & " = mo_quapre" & i.ToString("00") & " - " & CDblSQL(dttTmp.Rows(n)("Quapre" & i.ToString("00")).ToString) & ", "
            Next
            strSQL = strSQL.Substring(0, strSQL.Length - 2)
            strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mo_tipork = " & CStrSQL(dttTmp.Rows(n)!mm_ortipo) & _
                            " AND mo_anno = " & dttTmp.Rows(n)!mm_oranno.ToString & _
                            " AND mo_serie = " & CStrSQL(dttTmp.Rows(n)!mm_orserie) & _
                            " AND mo_numord = " & dttTmp.Rows(n)!mm_ornum.ToString & _
                            " AND mo_riga = " & dttTmp.Rows(n)!mm_orriga.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
        End If    'If strTipork = "W" Then
      Else
        If strTipork = "W" Then
          '------------------------
          'Storna le quantità prenotate sull'ordine
          strSQL = "SELECT movprb.*," & _
                  " (mm_quant01 - mm_nprquae01) As Quapre01, (mm_quant02 - mm_nprquae02) As Quapre02, (mm_quant03 - mm_nprquae03) As Quapre03, (mm_quant04 - mm_nprquae04) As Quapre04, (mm_quant05 - mm_nprquae05) As Quapre05, (mm_quant06 - mm_nprquae06) As Quapre06, (mm_quant07 - mm_nprquae07) As Quapre07, (mm_quant08 - mm_nprquae08) As Quapre08, (mm_quant09 - mm_nprquae09) As Quapre09, (mm_quant10 - mm_nprquae10) As Quapre10, (mm_quant11 - mm_nprquae11) As Quapre11, (mm_quant12 - mm_nprquae12) As Quapre12, (mm_quant13 - mm_nprquae13) As Quapre13, (mm_quant14 - mm_nprquae14) As Quapre14, (mm_quant15 - mm_nprquae15) As Quapre15, (mm_quant16 - mm_nprquae16) As Quapre16, (mm_quant17 - mm_nprquae17) As Quapre17, (mm_quant18 - mm_nprquae18) As Quapre18, (mm_quant19 - mm_nprquae19) As Quapre19, (mm_quant20 - mm_nprquae20) As Quapre20, (mm_quant21 - mm_nprquae21) As Quapre21, (mm_quant22 - mm_nprquae22) As Quapre22, (mm_quant23 - mm_nprquae23) As Quapre23, (mm_quant24 - mm_nprquae24) As Quapre24" & _
                  " FROM movprb INNER JOIN movmagtc ON movprb.codditt = movmagtc.codditt AND movprb.mm_tipork = movmagtc.mm_tipork AND movprb.mm_anno = movmagtc.mm_anno AND movprb.mm_serie = movmagtc.mm_serie AND movprb.mm_numdoc = movmagtc.mm_numdoc AND movprb.mm_riga = movmagtc.mm_riga" & _
                  " WHERE movprb.codditt = " & CStrSQL(strDitta) & _
                  " AND movprb.mm_tipork = " & CStrSQL(strTipork) & _
                  " AND movprb.mm_anno = " & nAnno & _
                  " AND movprb.mm_serie = " & CStrSQL(strSerie) & _
                  " AND movprb.mm_numdoc = " & lNumdoc & _
                  " AND movprb.mm_quant >= mm_nprquaeva" & _
                  " AND movprb.mm_ornum > 0"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          For n = 0 To dttTmp.Rows.Count - 1
            strSQL = "UPDATE movordtc SET"
            For i = 1 To 24
              strSQL = strSQL & " mo_quapre" & i.ToString("00") & " = mo_quapre" & i.ToString("00") & " - " & CDblSQL(dttTmp.Rows(n)("Quapre" & i.ToString("00")).ToString) & ", "
            Next
            strSQL = strSQL.Substring(0, strSQL.Length - 2)
            strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mo_tipork = " & CStrSQL(dttTmp.Rows(n)!mm_ortipo) & _
                            " AND mo_anno = " & dttTmp.Rows(n)!mm_oranno.ToString & _
                            " AND mo_serie = " & CStrSQL(dttTmp.Rows(n)!mm_orserie) & _
                            " AND mo_numord = " & dttTmp.Rows(n)!mm_ornum.ToString & _
                            " AND mo_riga = " & dttTmp.Rows(n)!mm_orriga.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
        Else
          '------------------------
          strSQL = "SELECT movmag.*," & _
                  " mm_quant01 As Quaeva01, mm_quant02 As Quaeva02, mm_quant03 As Quaeva03, mm_quant04 As Quaeva04, mm_quant05 As Quaeva05, mm_quant06 As Quaeva06, mm_quant07 As Quaeva07, mm_quant08 As Quaeva08, mm_quant09 As Quaeva09, mm_quant10 As Quaeva10, mm_quant11 As Quaeva11, mm_quant12 As Quaeva12, mm_quant13 As Quaeva13, mm_quant14 As Quaeva14, mm_quant15 As Quaeva15, mm_quant16 As Quaeva16, mm_quant17 As Quaeva17, mm_quant18 As Quaeva18, mm_quant19 As Quaeva19, mm_quant20 As Quaeva20, mm_quant21 As Quaeva21, mm_quant22 As Quaeva22, mm_quant23 As Quaeva23, mm_quant24 As Quaeva24" & _
                  " FROM movmag INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
                  " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                  " AND movmag.mm_tipork = " & CStrSQL(strTipork) & _
                  " AND movmag.mm_anno = " & nAnno & _
                  " AND movmag.mm_serie = " & CStrSQL(strSerie) & _
                  " AND movmag.mm_numdoc = " & lNumdoc & _
                  " AND movmag.mm_ornum > 0"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          For n = 0 To dttTmp.Rows.Count - 1
            strSQL = "UPDATE movordtc SET"
            For i = 1 To 24
              strSQL = strSQL & " mo_quaeva" & i.ToString("00") & " = mo_quaeva" & i.ToString("00") & " - " & CDblSQL(dttTmp.Rows(n)("Quaeva" & i.ToString("00")).ToString) & ", "
            Next
            strSQL = strSQL.Substring(0, strSQL.Length - 2)
            strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mo_tipork = " & CStrSQL(dttTmp.Rows(n)!mm_ortipo) & _
                            " AND mo_anno = " & dttTmp.Rows(n)!mm_oranno.ToString & _
                            " AND mo_serie = " & CStrSQL(dttTmp.Rows(n)!mm_orserie) & _
                            " AND mo_numord = " & dttTmp.Rows(n)!mm_ornum.ToString & _
                            " AND mo_riga = " & dttTmp.Rows(n)!mm_orriga.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
          '------------------------
          'Storna le quantità evase sulla nota
          strSQL = "SELECT movmag.*," & _
                  " mm_quant01 As Quaeva01, mm_quant02 As Quaeva02, mm_quant03 As Quaeva03, mm_quant04 As Quaeva04, mm_quant05 As Quaeva05, mm_quant06 As Quaeva06, mm_quant07 As Quaeva07, mm_quant08 As Quaeva08, mm_quant09 As Quaeva09, mm_quant10 As Quaeva10, mm_quant11 As Quaeva11, mm_quant12 As Quaeva12, mm_quant13 As Quaeva13, mm_quant14 As Quaeva14, mm_quant15 As Quaeva15, mm_quant16 As Quaeva16, mm_quant17 As Quaeva17, mm_quant18 As Quaeva18, mm_quant19 As Quaeva19, mm_quant20 As Quaeva20, mm_quant21 As Quaeva21, mm_quant22 As Quaeva22, mm_quant23 As Quaeva23, mm_quant24 As Quaeva24" & _
                  " FROM movmag INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
                  " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                  " AND movmag.mm_tipork = " & CStrSQL(strTipork) & _
                  " AND movmag.mm_anno = " & nAnno & _
                  " AND movmag.mm_serie = " & CStrSQL(strSerie) & _
                  " AND movmag.mm_numdoc = " & lNumdoc & _
                  " AND movmag.mm_npnum > 0"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          For n = 0 To dttTmp.Rows.Count - 1
            strSQL = "UPDATE movmagtc SET"
            For i = 1 To 24
              strSQL = strSQL & " mm_nprquae" & i.ToString("00") & " = mm_nprquae" & i.ToString("00") & " - " & CDblSQL(dttTmp.Rows(n)("Quaeva" & i.ToString("00")).ToString) & ", "
            Next
            strSQL = strSQL.Substring(0, strSQL.Length - 2)
            strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mm_tipork = " & CStrSQL(dttTmp.Rows(n)!mm_nptipo) & _
                            " AND mm_anno = " & dttTmp.Rows(n)!mm_npanno.ToString & _
                            " AND mm_serie = " & CStrSQL(dttTmp.Rows(n)!mm_npserie) & _
                            " AND mm_numdoc = " & dttTmp.Rows(n)!mm_npnum.ToString & _
                            " AND mm_riga = " & dttTmp.Rows(n)!mm_npriga.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
          '------------------------
          strSQL = "SELECT movmag.*," & _
                  " mm_npqtadis01 As Quapre01, mm_npqtadis02 As Quapre02, mm_npqtadis03 As Quapre03, mm_npqtadis04 As Quapre04, mm_npqtadis05 As Quapre05, mm_npqtadis06 As Quapre06, mm_npqtadis07 As Quapre07, mm_npqtadis08 As Quapre08, mm_npqtadis09 As Quapre09, mm_npqtadis10 As Quapre10, mm_npqtadis11 As Quapre11, mm_npqtadis12 As Quapre12, mm_npqtadis13 As Quapre13, mm_npqtadis14 As Quapre14, mm_npqtadis15 As Quapre15, mm_npqtadis16 As Quapre16, mm_npqtadis17 As Quapre17, mm_npqtadis18 As Quapre18, mm_npqtadis19 As Quapre19, mm_npqtadis20 As Quapre20, mm_npqtadis21 As Quapre21, mm_npqtadis22 As Quapre22, mm_npqtadis23 As Quapre23, mm_npqtadis24 As Quapre24" & _
                  " FROM movmag INNER JOIN movmagtc ON movmag.codditt = movmagtc.codditt AND movmag.mm_tipork = movmagtc.mm_tipork AND movmag.mm_anno = movmagtc.mm_anno AND movmag.mm_serie = movmagtc.mm_serie AND movmag.mm_numdoc = movmagtc.mm_numdoc AND movmag.mm_riga = movmagtc.mm_riga" & _
                  " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
                  " AND movmag.mm_tipork = " & CStrSQL(strTipork) & _
                  " AND movmag.mm_anno = " & nAnno & _
                  " AND movmag.mm_serie = " & CStrSQL(strSerie) & _
                  " AND movmag.mm_numdoc = " & lNumdoc & _
                  " AND movmag.mm_ornum > 0" & _
                  " AND movmag.mm_npnum > 0"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          For n = 0 To dttTmp.Rows.Count - 1
            strSQL = "UPDATE movordtc SET"
            For i = 1 To 24
              strSQL = strSQL & " mo_quapre" & i.ToString("00") & " = mo_quapre" & i.ToString("00") & " + " & CDblSQL(dttTmp.Rows(n)("Quapre" & i.ToString("00")).ToString) & ", "
            Next
            strSQL = strSQL.Substring(0, strSQL.Length - 2)
            strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mo_tipork = " & CStrSQL(dttTmp.Rows(n)!mm_ortipo) & _
                            " AND mo_anno = " & dttTmp.Rows(n)!mm_oranno.ToString & _
                            " AND mo_serie = " & CStrSQL(dttTmp.Rows(n)!mm_orserie) & _
                            " AND mo_numord = " & dttTmp.Rows(n)!mm_ornum.ToString & _
                            " AND mo_riga = " & dttTmp.Rows(n)!mm_orriga.ToString
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          Next
        End If    'If strTipork = "W" Then
      End If    'If nOperaz = 0 Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AggMovOrdApertoTC(ByVal strDitta As String, ByVal strTipork As String, _
                                                ByVal nAnno As Integer, ByVal strSerie As String, _
                                                ByVal lNumdoc As Integer, ByVal nOperaz As Integer, _
                                                ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim n As Integer = 0
    Try
      If nOperaz = 0 Then
        '------------------------
        'Aggiorna le quantità evase sull'ordine
        strSQL = " SELECT movord.*," & _
                 " mo_quant01 As Quant01, mo_quant02 As Quant02, mo_quant03 As Quant03, mo_quant04 As Quant04, mo_quant05 As Quant05, mo_quant06 As Quant06, mo_quant07 As Quant07, mo_quant08 As Quant08, mo_quant09 As Quant09, mo_quant10 As Quant10, mo_quant11 As Quant11, mo_quant12 As Quant12, mo_quant13 As Quant13, mo_quant14 As Quant14, mo_quant15 As Quant15, mo_quant16 As Quant16, mo_quant17 As Quant17, mo_quant18 As Quant18, mo_quant19 As Quant19, mo_quant20 As Quant20, mo_quant21 As Quant21, mo_quant22 As Quant22, mo_quant23 As Quant23, mo_quant24 As Quant24" & _
                 " FROM movord INNER JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga" & _
                 " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                 " AND movord.mo_tipork = " & CStrSQL(strTipork) & _
                 " AND movord.mo_anno = " & nAnno & _
                 " AND movord.mo_serie = " & CStrSQL(strSerie) & _
                 " AND movord.mo_numord = " & lNumdoc & _
                 " AND movord.mo_oasalcon = 'C'" & _
                 " AND movord.mo_oanum > 0"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        For n = 0 To dttTmp.Rows.Count - 1
          strSQL = "UPDATE movordtc SET"
          For i = 1 To 24
            strSQL = strSQL & " mo_quaeva" & i.ToString("00") & " = mo_quaeva" & i.ToString("00") & " + " & CDblSQL(dttTmp.Rows(n)("Quant" & i.ToString("00")).ToString) & ", "
          Next
          strSQL = strSQL.Substring(0, strSQL.Length - 2)
          strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mo_tipork = " & CStrSQL(dttTmp.Rows(n)!mo_oatipo) & _
                            " AND mo_anno = " & dttTmp.Rows(n)!mo_oaanno.ToString & _
                            " AND mo_serie = " & CStrSQL(dttTmp.Rows(n)!mo_oaserie) & _
                            " AND mo_numord = " & dttTmp.Rows(n)!mo_oanum.ToString & _
                            " AND mo_riga = " & dttTmp.Rows(n)!mo_oariga.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
        '------------------------
        strSQL = " SELECT movord.*," & _
                 " mo_quant01 As Quant01, mo_quant02 As Quant02, mo_quant03 As Quant03, mo_quant04 As Quant04, mo_quant05 As Quant05, mo_quant06 As Quant06, mo_quant07 As Quant07, mo_quant08 As Quant08, mo_quant09 As Quant09, mo_quant10 As Quant10, mo_quant11 As Quant11, mo_quant12 As Quant12, mo_quant13 As Quant13, mo_quant14 As Quant14, mo_quant15 As Quant15, mo_quant16 As Quant16, mo_quant17 As Quant17, mo_quant18 As Quant18, mo_quant19 As Quant19, mo_quant20 As Quant20, mo_quant21 As Quant21, mo_quant22 As Quant22, mo_quant23 As Quant23, mo_quant24 As Quant24" & _
                 " FROM movord INNER JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga" & _
                 " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                 " AND movord.mo_tipork = " & CStrSQL(strTipork) & _
                 " AND movord.mo_anno = " & nAnno & _
                 " AND movord.mo_serie = " & CStrSQL(strSerie) & _
                 " AND movord.mo_numord = " & lNumdoc & _
                 " AND movord.mo_oasalcon = 'S'" & _
                 " AND movord.mo_oanum > 0"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        For n = 0 To dttTmp.Rows.Count - 1
          strSQL = "UPDATE movordtc SET"
          For i = 1 To 24
            strSQL = strSQL & " mo_quaeva" & i.ToString("00") & " = mo_quaeva" & i.ToString("00") & " + " & CDblSQL(dttTmp.Rows(n)("Quant" & i.ToString("00")).ToString) & ", "
          Next
          strSQL = strSQL.Substring(0, strSQL.Length - 2)
          strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mo_tipork = " & CStrSQL(dttTmp.Rows(n)!mo_oatipo) & _
                            " AND mo_anno = " & dttTmp.Rows(n)!mo_oaanno.ToString & _
                            " AND mo_serie = " & CStrSQL(dttTmp.Rows(n)!mo_oaserie) & _
                            " AND mo_numord = " & dttTmp.Rows(n)!mo_oanum.ToString & _
                            " AND mo_riga = " & dttTmp.Rows(n)!mo_oariga.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next

      Else

        '------------------------
          strSQL = " SELECT movord.*," & _
                 " mo_quant01 As Quant01, mo_quant02 As Quant02, mo_quant03 As Quant03, mo_quant04 As Quant04, mo_quant05 As Quant05, mo_quant06 As Quant06, mo_quant07 As Quant07, mo_quant08 As Quant08, mo_quant09 As Quant09, mo_quant10 As Quant10, mo_quant11 As Quant11, mo_quant12 As Quant12, mo_quant13 As Quant13, mo_quant14 As Quant14, mo_quant15 As Quant15, mo_quant16 As Quant16, mo_quant17 As Quant17, mo_quant18 As Quant18, mo_quant19 As Quant19, mo_quant20 As Quant20, mo_quant21 As Quant21, mo_quant22 As Quant22, mo_quant23 As Quant23, mo_quant24 As Quant24" & _
                 " FROM movord INNER JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga" & _
                 " WHERE movord.codditt = " & CStrSQL(strDitta) & _
                 " AND movord.mo_tipork = " & CStrSQL(strTipork) & _
                 " AND movord.mo_anno = " & nAnno & _
                 " AND movord.mo_serie = " & CStrSQL(strSerie) & _
                 " AND movord.mo_numord = " & lNumdoc & _
                 " AND movord.mo_oanum > 0"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        For n = 0 To dttTmp.Rows.Count - 1
          strSQL = "UPDATE movordtc SET"
          For i = 1 To 24
            strSQL = strSQL & " mo_quaeva" & i.ToString("00") & " = mo_quaeva" & i.ToString("00") & " - " & CDblSQL(dttTmp.Rows(n)("Quant" & i.ToString("00")).ToString) & ", "
          Next
          strSQL = strSQL.Substring(0, strSQL.Length - 2)
          strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta) & _
                            " AND mo_tipork = " & CStrSQL(dttTmp.Rows(n)!mo_oatipo) & _
                            " AND mo_anno = " & dttTmp.Rows(n)!mo_oaanno.ToString & _
                            " AND mo_serie = " & CStrSQL(dttTmp.Rows(n)!mo_oaserie) & _
                            " AND mo_numord = " & dttTmp.Rows(n)!mo_oanum.ToString & _
                            " AND mo_riga = " & dttTmp.Rows(n)!mo_oariga.ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Next
        
      End If    'If nOperaz = 0 Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetPrecedentiMovimenti(ByVal strDitta As String, ByRef dsOut As DataSet, _
                                                    ByVal strCodart As String, ByVal nFase As Integer, _
                                                    ByVal nMagaz As Integer, ByVal lConto As Integer) As Boolean
    Try

      Return GetPrecedentiMovimenti(strDitta, dsOut, strCodart, nFase, nMagaz, lConto, False, False, "T", 0, "T", True)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetPrecedentiMovimenti(ByVal strDitta As String, ByRef dsOut As DataSet, _
                                                    ByVal strCodart As String, ByVal nFase As Integer, _
                                                    ByVal nMagaz As Integer, ByVal lConto As Integer, _
                                                    ByVal bModuloCRM As Boolean, ByVal bIsCRMUser As Boolean, _
                                                    ByVal strAccvis As String, ByVal lCodorgaOperat As Integer, _
                                                    ByVal strRegvis As String, ByVal bAmm As Boolean) As Boolean
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dsOut, strCodart, nFase, nMagaz, lConto, bModuloCRM, _
                                             bIsCRMUser, strAccvis, lCodorgaOperat, strRegvis, bAmm})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsOut = CType(oIn(1), DataSet)
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT DISTINCT keymag.*, mm_quant, mm_prezzo, mm_valore, an_descr1 as xx_conto, " & _
              " mm_ornum, mm_scont1, mm_scont2, mm_scont3, mm_scont4, mm_scont5, mm_scont6, mm_prelist, tm_riferim, mm_prezvalc," & _
              " mm_colli, mm_misura1, mm_misura2, mm_misura3, mm_controp, mm_commeca, mm_codcena, mm_descr, " & _
              " mm_codcfam, mm_codnomc, mm_provv, mm_vprovv, mm_provv2, mm_vprovv2, mm_perqta, tb_descaum, mm_preziva, mm_codiva, tm_scorpo, tm_valuta, " & _
              " CASE WHEN (keymag.km_carscar = 0 OR keymag.km_carscar = 1) THEN movmag.mm_quant ELSE 0 END As xx_carichi," & _
              " CASE WHEN (keymag.km_carscar = 0 OR keymag.km_carscar = -1) THEN movmag.mm_quant ELSE 0 END As xx_scarichi," & _
              " CASE WHEN mm_quant = 0 THEN 0 ELSE ((movmag.mm_valore / movmag.mm_quant) * movmag.mm_perqta) END As xx_prezzo, (anagra.an_descr1) As xx_conto, " & _
              " analotti.alo_lottox as xx_lottox "
      strSQL += " FROM (((((testmag INNER JOIN movmag ON (testmag.codditt = movmag.codditt) AND " & _
              " (testmag.tm_numdoc = movmag.mm_numdoc) AND (testmag.tm_serie = movmag.mm_serie) AND " & _
              " (testmag.tm_anno = movmag.mm_anno) AND (testmag.tm_tipork = movmag.mm_tipork)) " & _
              " INNER JOIN keymag ON (movmag.codditt = keymag.codditt) AND (movmag.mm_riga = keymag.km_riga) AND " & _
              " (movmag.mm_numdoc = keymag.km_numdoc) AND (movmag.mm_serie = keymag.km_serie) AND " & _
              " (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_tipork = keymag.km_tipork)) " & _
              " INNER JOIN artico ON movmag.codditt = artico.codditt AND movmag.mm_codart = artico.ar_codart) " & _
              " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto) " & _
              " LEFT JOIN tabcaum ON keymag.km_causale = tabcaum.tb_codcaum) " & _
              " LEFT JOIN anagra ON keymag.codditt = anagra.codditt AND keymag.km_conto = anagra.an_conto" & _
              " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
              " AND km_codart = " & CStrSQL(strCodart) & _
              " AND km_fase = " & nFase
      If nMagaz <> 0 Then strSQL += " AND km_magaz = " & nMagaz
      If lConto <> 0 Then strSQL += " AND km_conto = " & lConto
      If (bModuloCRM = True) And (bIsCRMUser = True) And Not (strAccvis = "T" And bAmm = True) Then
        strSQL = strSQL & " AND ("
        If strAccvis <> "T" Then
          strSQL = strSQL & "(an_tipo = 'C' AND an_conto IN (SELECT an_conto FROM anagra INNER JOIN leads ON anagra.codditt = leads.codditt AND anagra.an_conto = leads.le_conto" & _
                                                       " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
                                                       " AND leads.le_coddest = 0"
          Select Case strAccvis
            Case "P" : strSQL = strSQL & " AND le_opinc = " & lCodorgaOperat
            Case "C" : strSQL = strSQL & " AND le_opinc IN (" & strRegvis & ")"
          End Select
          strSQL = strSQL & "))"
        Else
          strSQL = strSQL & " anagra.an_tipo = 'C'"
        End If
        If bAmm = False Then
          strSQL = strSQL & " AND an_tipo <> 'F'"
        Else
          strSQL = strSQL & " OR an_tipo <> 'C'"
        End If
        strSQL = strSQL & ")"
      End If
      strSQL = strSQL & " ORDER BY km_aammgg DESC, km_tipork, km_serie, km_numdoc, km_riga"
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVIM")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetPrecedentiOrdini(ByVal strDitta As String, ByRef dsOut As DataSet, _
                                                  ByVal strCodart As String, ByVal nFase As Integer, _
                                                  ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mo_tipork AS tt_tipork, mo_anno AS tt_anno, mo_serie AS tt_serie, mo_numord AS tt_numero, " & _
               "mo_riga AS tt_riga, td_conto AS tt_conto, an_descr1 AS tt_desconto, mo_codart AS tt_codart, " & _
               "mo_descr AS tt_desart, mo_magaz AS tt_magaz, tb_desmaga AS tt_desmaga, td_datord AS tt_datord, " & _
               "mo_datcons AS tt_datcons, mo_quant AS tt_quant, mo_quapre AS tt_quapre, mo_quaeva AS tt_quaeva, " & _
               "td_annpar AS tt_annpar, td_alfpar AS tt_alfpar, td_numpar AS tt_numpar, td_riferim AS tt_riferim, " & _
               "mo_note AS tt_note, mo_prezzo As tt_prezzo, mo_scont1 As tt_scont1, mo_scont2 As tt_scont2, " & _
               "mo_scont3 As tt_scont3, mo_valore As tt_valres, (mo_quant - mo_quaeva) As tt_qtares, " & _
               "mo_commeca As tt_commeca, mo_subcommeca As tt_subcommeca, mo_scont4 As tt_scont4, mo_scont5 As tt_scont5, " & _
               "mo_scont6 As tt_scont6, mo_fase as tt_fase,ar_codappr as tt_codappr "
      strSQL += "FROM (((testord INNER JOIN movord ON testord.codditt = movord.codditt AND " & _
               "testord.td_tipork = movord.mo_tipork AND testord.td_anno = movord.mo_anno AND " & _
               "testord.td_serie = movord.mo_serie AND testord.td_numord = movord.mo_numord) " & _
               "INNER JOIN artico ON artico.codditt = movord.codditt AND artico.ar_codart = movord.mo_codart) " & _
               "INNER JOIN anagra ON anagra.codditt = testord.codditt AND anagra.an_conto = testord.td_conto) " & _
               "INNER JOIN tabmaga ON movord.codditt = tabmaga.codditt AND movord.mo_magaz = tabmaga.tb_codmaga " & _
               "WHERE testord.codditt = " & CStrSQL(strDitta) & _
               "AND td_conto = " & lConto & _
               "AND mo_flevas <> 'S' "
      If CBool(GetSettingBusDitt(strDitta, "BSORGSOR", "OPZIONI", ".", "IncludiPrevSuPrecOrdini", "0", " ", "0")) = False Then
        strSQL += " AND td_tipork <> 'Q' "
      End If
      If strCodart <> "" Then strSQL += " AND mo_codart = " & CStrSQL(strCodart) & " AND mo_fase = " & nFase
      strSQL += " ORDER BY mo_datcons, mo_codart, mo_fase, td_tipork, td_anno, td_serie, td_numord"
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORDIN")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ZoomListini(ByVal strDitta As String, ByRef dsOut As DataSet, _
                                         ByVal strCodart As String, ByVal nFase As Integer, _
                                         ByVal lConto As Integer, ByVal bSoloEuro As Boolean, _
                                         ByVal nValuta As Integer, _
                                         ByVal bListinoCorrente As Boolean, ByVal strDtValid As String, _
                                         ByVal lFornitore1 As Integer, ByVal lFornitore2 As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Dim strCoda As String = ""
    Dim dCostoMedio As Decimal = 0
    Dim dttTmp As New DataTable
    Dim bSpecifici As Boolean = False
    Dim strListiniDaVisualizzare As String = ""
    Dim bVisTuttiListiniSpeciali As Boolean = False
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Dim bEscludiCosti As Boolean = False
    Try
      strListiniDaVisualizzare = GetSettingBusDitt(strDitta, "Bsvebolx", "Opzioni", ".", "ListiniDaVisualizzare", " ", " ", " ")
      bVisTuttiListiniSpeciali = CBool(GetSettingBusDitt(strDitta, "BSVEBOLX", "OPZIONI", ".", "VisTuttiListiniSpeciali", "0", " ", "0"))
      bEscludiCosti = CBool(GetSettingBusDitt(strDitta, "BSVEBOLX", "OPZIONI", ".", "EscludiCosti", "0", " ", "0"))

      '--------------------------
      strCoda = strCodart
      strSQL = "SELECT ar_codart, ar_gesvar, ar_codroot, ar_codvar1, ar_prevar FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows(0)!ar_gesvar.ToString = "S" Then
        Select Case dttTmp.Rows(0)!ar_prevar.ToString
          Case "N" : strCoda = NTSCStr(dttTmp.Rows(0)!ar_codroot)
          Case "S" : strCoda = NTSCStr(dttTmp.Rows(0)!ar_codart)
          Case "1" : strCoda = NTSCStr(dttTmp.Rows(0)!ar_codroot) & NTSCStr(dttTmp.Rows(0)!ar_codvar1)
        End Select
      End If
      dttTmp.Clear()

      '--------------------------
      'Preleva l'eventuale costo medio, calcolandolo da ARTPROX, per l'articolo specificato
      If bEscludiCosti = False Then
        strSQL = "SELECT apx_qtalif, apx_vqtalif, apx_giaini, apx_vgiaini, ar_perqta " & _
                 " FROM artprox INNER JOIN artico ON artprox.codditt = artico.codditt " & _
                 " AND artprox.apx_codart = artico.ar_codart " & _
                 " WHERE artprox.codditt = " & CStrSQL(strDitta) & _
                 " AND apx_codart = " & CStrSQL(strCodart) & _
                 " AND apx_fase = " & nFase
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          If NTSCDec(dttTmp.Rows(0)!apx_qtalif) <> 0 Then
            dCostoMedio = ArrDbl(((NTSCDec(dttTmp.Rows(0)!apx_vqtalif) / NTSCDec(dttTmp.Rows(0)!apx_qtalif)) * NTSCDec(dttTmp.Rows(0)!ar_perqta)), TrovaNdecSuPrzUn(0))
          Else
            If NTSCDec(dttTmp.Rows(0)!apx_giaini) <> 0 Then
              dCostoMedio = ArrDbl(((NTSCDec(dttTmp.Rows(0)!apx_vgiaini) / NTSCDec(dttTmp.Rows(0)!apx_giaini)) * NTSCDec(dttTmp.Rows(0)!ar_perqta)), TrovaNdecSuPrzUn(0))
            End If
          End If
        End If
        dttTmp.Clear()
      End If

      '----------------------------
      strSQL = "SELECT listini.codditt, listini.lc_progr, listini.lc_codart, listini.lc_codlavo," & _
                " listini.lc_conto, listini.lc_codvalu, listini.lc_codtpro, listini.lc_listino," & _
                " listini.lc_datagg, listini.lc_tipo, listini.lc_prezzo, listini.lc_datscad," & _
                " listini.lc_daquant, listini.lc_aquant, listini.lc_perqta, listini.lc_unmis," & _
                " listini.lc_note, listini.lc_netto, listini.lc_fase, listini.lc_ultagg," & _
                " listini.lc_codcas, tb_desvalu as xx_codvalu, tb_deslavo as xx_codlavo, tb_destpro as xx_codtpro, " & _
                " tablist.tb_codlist, " & _
                " CASE lc_conto WHEN 0 THEN tablist.tb_deslist "
      If lFornitore1 <> 0 Then strSQL += " WHEN " & lFornitore1 & " THEN 'Speciale fornitore 1'"
      If lFornitore2 <> 0 Then strSQL += " WHEN " & lFornitore2 & " THEN 'Speciale fornitore 2'"
      strSQL += " ELSE 'Speciale' END as xx_listino, "
      strSQL += " tablist.tb_ivato, anagra.an_descr1 as xx_conto, listini.lc_coddest, dd_nomdest as xx_coddest " & _
                " FROM listini LEFT JOIN tablavo ON listini.codditt = tablavo.codditt AND listini.lc_codlavo = tablavo.tb_codlavo" & _
                " LEFT JOIN tabvalu ON listini.lc_codvalu = tabvalu.tb_codvalu" & _
                " LEFT JOIN tabtpro ON listini.codditt = tabtpro.codditt AND listini.lc_codtpro = tabtpro.tb_codtpro" & _
                " LEFT JOIN tablist ON listini.codditt = tablist.codditt AND listini.lc_listino = tablist.tb_codlist" & _
                " LEFT JOIN anagra ON listini.codditt = anagra.codditt AND listini.lc_conto = anagra.an_conto" & _
                " LEFT JOIN destdiv ON listini.codditt = destdiv.codditt AND listini.lc_conto = destdiv.dd_conto AND listini.lc_coddest = destdiv.dd_coddest"
      '-----------------------------
      strWhere = " WHERE listini.codditt = " & CStrSQL(strDitta) & _
                " AND listini.lc_codart = " & CStrSQL(strCoda) & _
                " AND lc_fase = " & nFase
      '-----------------------------
      If bSoloEuro Then
        strWhere = strWhere & " AND lc_codvalu = 0"
      Else
        If nValuta <> 0 Then
          strWhere = strWhere & " AND lc_codvalu = " & nValuta
        Else
          strWhere = strWhere & " AND lc_codvalu > 0"
        End If
      End If
      '-----------------------------
      If bListinoCorrente Then
        strWhere = strWhere & " AND listini.lc_datagg <= " & CDataSQL(strDtValid) & " AND listini.lc_datscad >= " & CDataSQL(strDtValid)
      End If
      '-----------------------------
      If Trim(strListiniDaVisualizzare) <> "" Then
        'ggiungo, se serve, l'ultimo;
        If Right(strListiniDaVisualizzare, 1) <> ";" Then
          strListiniDaVisualizzare = strListiniDaVisualizzare & ";"
        End If
        'Verifico se devo consideare il listino specifico
        If InStr(1, strListiniDaVisualizzare, "S", vbTextCompare) > 0 Then
          If bVisTuttiListiniSpeciali = False Then
            If bEscludiCosti = True Then
              strWhere = strWhere & " AND (lc_conto = 0 OR lc_conto = " & lConto & ")"
            Else
              strWhere = strWhere & " AND (lc_conto = 0 OR lc_conto IN (" & lConto
              If lFornitore1 <> 0 Then strWhere = strWhere & ", " & lFornitore1
              If lFornitore2 <> 0 Then strWhere = strWhere & ", " & lFornitore2
              strWhere = strWhere & "))"
            End If
          End If
          bSpecifici = True
        Else
          strWhere = strWhere & " AND lc_conto = 0 "
          bSpecifici = False
        End If
        If Not (Len(strListiniDaVisualizzare) < 3 And bSpecifici = True) Then
          strT = strListiniDaVisualizzare.Split(";"c)
          strWhere = strWhere & " AND ("
          For i = 0 To strT.Length - 1
            If (strT(i) <> "S") And (strT(i) <> "s") And (strT(i) <> "") Then
              If IsNumeric(strT(i)) Then strWhere = strWhere & " lc_listino = " & strT(i) & " OR"
            Else
              strWhere = strWhere & " lc_listino = 0 OR"
            End If
          Next
          strWhere = Left(strWhere, Len(strWhere) - 2) & ")"
        Else
          strWhere = strWhere & " AND lc_listino = 0"
        End If
      Else
        If bEscludiCosti = True Then
          strWhere = strWhere & " AND (lc_conto = 0 OR lc_conto = " & lConto & ")"
        Else
          strWhere = strWhere & " AND (lc_conto = 0 OR lc_conto IN (" & lConto
          If lFornitore1 <> 0 Then strWhere = strWhere & ", " & lFornitore1
          If lFornitore2 <> 0 Then strWhere = strWhere & ", " & lFornitore2
          strWhere = strWhere & "))"
        End If
      End If
      '--------------------------
      strSQL = strSQL & strWhere

      'Se non è attiva l'opzione di registro 'EscludiCosti', preleva anche:
      'Ultimo costo
      'Ultimo costo + spese accessorie
      'Costo medio
      'dai progressivi totali (ARTPROX)
      If bEscludiCosti = False Then
        '---------------------------
        'UNION per 'Ultimo costo' (ARTPROX.apx_ultcos)
        strSQL = strSQL & " UNION" & _
                " SELECT " & CStrSQL(strDitta) & " As codditt, 0 As lc_progr, " & _
                CStrSQL(strCoda) & " As lc_codart, '' As lc_codlavo," & _
                " '' As lc_conto, '' As lc_codvalu, '' As lc_codtpro, 0 As lc_listino," & _
                " apx_dtulcar As lc_datagg, '1' As lc_tipo, apx_ultcos As lc_prezzo, '' As lc_datscad," & _
                " '' As lc_daquant, '' As lc_aquant, '' As lc_perqta, '' As lc_unmis," & _
                " NULL As lc_note, 'N' As lc_netto, " & nFase & " As lc_fase, '' As lc_ultagg," & _
                " '' As lc_codcas, NULL As tb_desvalu, NULL As tb_deslavo, NULL As tb_destpro," & _
                " 0 As tb_codlist, 'Ultimo costo' As tb_deslist, '' As tb_ivato, NULL As an_descr1, 0, NULL" & _
                " FROM artprox" & _
                " WHERE artprox.codditt = " & CStrSQL(strDitta) & _
                " AND apx_codart = " & CStrSQL(strCodart) & _
                " AND apx_fase = " & nFase
        '---------------------------
        'UNION per 'Ultimo costo con oneri accessori' (ARTPROX.apx_peucos)
        strSQL = strSQL & " UNION" & _
                " SELECT " & CStrSQL(strDitta) & " As codditt, 0 As lc_progr, " & _
                CStrSQL(strCoda) & " As lc_codart, '' As lc_codlavo," & _
                " '' As lc_conto, '' As lc_codvalu, '' As lc_codtpro, -1 As lc_listino," & _
                " apx_dtulcar As lc_datagg, '2' As lc_tipo, apx_peucos As lc_prezzo, '' As lc_datscad," & _
                " '' As lc_daquant, '' As lc_aquant, '' As lc_perqta, '' As lc_unmis," & _
                " NULL As lc_note, 'N' As lc_netto, " & nFase & " As lc_fase, '' As lc_ultagg," & _
                " '' As lc_codcas, NULL As tb_desvalu, NULL As tb_deslavo, NULL As tb_destpro," & _
                " 0 As tb_codlist, 'Ultimo costo + oneri acc' As tb_deslist, '' As tb_ivato, NULL As an_descr1, 0, NULL" & _
                " FROM artprox" & _
                " WHERE artprox.codditt = " & CStrSQL(strDitta) & _
                " AND apx_codart = " & CStrSQL(strCodart) & _
                " AND apx_fase = " & nFase
        '--------------------------
        'UNION per 'Costo medio' (calcolo da valori di ARTPROX)
        strSQL = strSQL & " UNION" & _
                " SELECT " & CStrSQL(strDitta) & " As codditt, 0 As lc_progr, " & _
                CStrSQL(strCoda) & " As lc_codart, '' As lc_codlavo," & _
                " '' As lc_conto, '' As lc_codvalu, '' As lc_codtpro, -2 As lc_listino," & _
                " apx_dtulcar As lc_datagg, '3' As lc_tipo," & CDblSQL(dCostoMedio) & " As lc_prezzo," & _
                " '' As lc_datscad, '' As lc_daquant, '' As lc_aquant, '' As lc_perqta," & _
                " '' As lc_unmis, NULL As lc_note, 'N' As lc_netto, " & nFase & " As lc_fase," & _
                " '' As lc_ultagg, '' As lc_codcas, NULL As tb_desvalu, NULL As tb_deslavo," & _
                " NULL As tb_destpro, 0 As tb_codlist, 'Costo medio' As tb_deslist, '' As tb_ivato, NULL As an_descr1, 0, NULL" & _
                " FROM artprox" & _
                " WHERE artprox.codditt = " & CStrSQL(strDitta) & _
                " AND apx_codart = " & CStrSQL(strCodart) & _
                " AND apx_fase = " & nFase
      End If
      '-----------------------------
      'Se lo stesso recordset non è già aperto, ne riapre un'altro
      'Orinamento: TIPO DESC, ,  lc_listino, lc_codlavo, lc_codtpro DESC, lc_datagg, lc_daquant
      strSQL = strSQL & " ORDER BY lc_tipo DESC, lc_coddest ASC, lc_listino, lc_codlavo, lc_codtpro DESC, lc_datagg, lc_daquant"
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

      If oApp.oGvar.strSconClCliDaList = "S" Then
        dsOut.Tables("LISTINI").Columns.Add("so_scont1", GetType(Decimal))
        dsOut.Tables("LISTINI").Columns.Add("so_scont2", GetType(Decimal))
        dsOut.Tables("LISTINI").Columns.Add("so_scont3", GetType(Decimal))
        dsOut.Tables("LISTINI").Columns.Add("so_scont4", GetType(Decimal))
        dsOut.Tables("LISTINI").Columns.Add("so_scont5", GetType(Decimal))
        dsOut.Tables("LISTINI").Columns.Add("so_scont6", GetType(Decimal))
        dsOut.Tables("LISTINI").Columns.Add("xx_prznetto", GetType(Decimal))

        For Each dtrRow As DataRow In dsOut.Tables("LISTINI").Rows
          If NTSCInt(dtrRow!lc_listino) <= 0 Then
            dtrRow!so_scont1 = 0
            dtrRow!so_scont2 = 0
            dtrRow!so_scont3 = 0
            dtrRow!so_scont4 = 0
            dtrRow!so_scont5 = 0
            dtrRow!so_scont6 = 0
            dtrRow!xx_prznetto = 0
            Continue For
          End If

          strSQL = "SELECT so_scont1, so_scont2, so_scont3, so_scont4, so_scont5, so_scont6 FROM sconti " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   "   AND so_codart = " & CStrSQL(strCodart) & _
                   "   AND so_fase = " & nFase & _
                   "   AND so_clscan = " & NTSCInt(dtrRow!lc_listino)

          Dim dttSconti As DataTable = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttSconti.Rows.Count = 0 Then
            dtrRow!so_scont1 = 0
            dtrRow!so_scont2 = 0
            dtrRow!so_scont3 = 0
            dtrRow!so_scont4 = 0
            dtrRow!so_scont5 = 0
            dtrRow!so_scont6 = 0
            dtrRow!xx_prznetto = 0
          Else
            With dttSconti.Rows(0)
              dtrRow!so_scont1 = !so_scont1
              dtrRow!so_scont2 = !so_scont2
              dtrRow!so_scont3 = !so_scont3
              dtrRow!so_scont4 = !so_scont4
              dtrRow!so_scont5 = !so_scont5
              dtrRow!so_scont6 = !so_scont6
              dtrRow!xx_prznetto = NTSCDec(dtrRow!lc_prezzo) * ((100 - NTSCDec(dtrRow!so_scont1)) / 100) * ((100 - NTSCDec(dtrRow!so_scont2)) / 100) * _
                                                               ((100 - NTSCDec(dtrRow!so_scont3)) / 100) * ((100 - NTSCDec(dtrRow!so_scont4)) / 100) * _
                                                               ((100 - NTSCDec(dtrRow!so_scont5)) / 100) * ((100 - NTSCDec(dtrRow!so_scont6)) / 100)
            End With
          End If
        Next
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaPromozioniPerArticoloCliente(ByVal strDitta As String, ByVal strCodart As String, ByVal lConto As Integer, _
                                                                ByVal bValidoGiorno As Boolean, ByVal dtDataVal As Date, ByRef dttPromo As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dttArt As New DataTable
    Try
      ValCodiceDb(strCodart, strDitta, "ARTICO", "S", , dttArt)
      If dttArt.Rows.Count = 0 Then Return False 'non dovrebbe mai capitare, il codice articolo viene controllato a monte

      strSQL = "SELECT DISTINCT tabrepr.* " & _
               " FROM tabrepr " & _
               "  INNER JOIN relinkpt ON relinkpt.codditt = tabrepr.codditt AND tb_codrepr = rel_codrepr " & _
               "  INNER JOIN refites ON refites.codditt = relinkpt.codditt AND rel_codrett = ret_codrett " & _
               " WHERE tabrepr.codditt = " & CStrSQL(strDitta) & _
               "   AND rel_abilitato = 'S'"

      'Controlla se l'articolo indicato è previsto nella promozioni
      With dttArt.Rows(0)
        strSQL &= " AND (tb_fltutti = 'S' " & _
                  "   OR tb_codart = " & CStrSQL(strCodart) & _
                  "   OR tb_codlsar IN (SELECT lsa_codlsar FROM listsar  " & _
                  "                     WHERE codditt = " & CStrSQL(strDitta) & _
                  "                       AND lsa_codart = " & CStrSQL(strCodart) & ") " & _
                  "   OR  (tb_codart IS NULL " & _
                  "    AND tb_codlsar = 0 " & _
                  "    AND tb_gruppo IN (0, " & NTSCInt(!ar_gruppo) & ") " & _
                  "    AND tb_sotgru IN (0, " & NTSCInt(!ar_sotgru) & ") " & _
                  "    AND tb_codcfam IN (null, " & CStrSQL(!ar_famprod) & "))) "
      End With

      'Vincoli sul cliente (ignora la destinazione che viene mostrata in griglia)
      If oApp.oGvar.bRilevaContrattiDaContoFatt Then
        Dim dttClie As New DataTable
        ValCodiceDb(lConto.ToString, strDitta, "ANAGRA", "N", , dttClie)
        If dttClie.Rows.Count > 0 AndAlso NTSCInt(dttClie.Rows(0)!an_contfatt) <> 0 Then lConto = NTSCInt(dttClie.Rows(0)!an_contfatt)
      End If
      strSQL &= " AND ret_conto = " & lConto


      'Filtro per periodo validità, o un giorno fisso o prende tutte le promozioni
      If bValidoGiorno Then
        strSQL &= " AND " & CDataSQL(dtDataVal) & " BETWEEN tb_datini AND tb_datfin " & _
                  " AND ret_dtscad >= " & CDataSQL(dtDataVal)
      End If


      dttPromo = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function



  Public Overridable Function GetArticoFromArttest(ByVal strDitta As String, ByVal strCodArtf As String, ByRef dtOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      strSQL = "SELECT * FROM artest" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND " & CStrSQL(strCodArtf) & " IN (ae_codartf, ae_codeump, ae_codeconf) " & _
              " AND ae_codart IS NULL " & _
              " ORDER BY codditt, ae_codartf, ae_forn, ae_codmarc"
      dtOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTabcentFromMagaz(ByVal strDitta As String, ByVal nCodmaga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_codcent, tb_ccodcontr, tb_ccodcena FROM tabcent" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tb_magterz = " & nCodmaga.ToString & " ORDER BY tb_codcent"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DuplicaDocAllole(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                       ByVal nAnno As Integer, ByVal strSerie As String, _
                                       ByVal lNumdoc As Integer, ByVal strNewTipoDoc As String, _
                                       ByVal nNewAnno As Integer, ByVal strNewSerie As String, _
                                       ByVal lNewNumdoc As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dbConn As DbConnection = Nothing
    Dim lNumOgg As Integer = 0
    Dim strErr As String = ""

    Try
      '-------------------------
      'cancello allole: 
      strSQL = "SELECT * FROM allole" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND ao_tipo = 'O'" & _
                " AND ao_tipodoc = " & CStrSQL(strTipoDoc) & _
                " AND ao_annodoc = " & nAnno & _
                " AND ao_seriedoc = " & CStrSQL(strSerie) & _
                " AND ao_numdoc = " & lNumdoc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return True

      If dttTmp.Columns.CanRemove(dttTmp.Columns("ao_ole")) Then dttTmp.Columns.Remove("ao_ole")

      For Each dtrT As DataRow In dttTmp.Rows
        lNumOgg = LegNuma(strDitta, "OO", "", 0, True)
        lNumOgg = AggNuma(strDitta, "OO", "", 0, lNumOgg, False, True, strErr)
        If strErr <> "" Then
          Throw New NTSException(strErr)
          Return False
        End If
        dtrT.SetAdded()
        dtrT!ao_progress = lNumOgg
        dtrT!ao_progresl = lNumOgg
        dtrT!ao_tipodoc = strNewTipoDoc
        dtrT!ao_annodoc = nNewAnno
        dtrT!ao_seriedoc = strNewSerie
        dtrT!ao_numdoc = lNewNumdoc
        dtrT!ao_ultagg = DateTime.Now
        dtrT!ao_datins = DateTime.Now
      Next

      ScriviTabellaSemplice(strDitta, "ALLOLE", dttTmp, "", "", "")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function ApriSaldiClav(ByVal strDitta As String, ByVal strCodart As String, ByVal bNew As Boolean, _
                                            ByVal nMagClav As Integer, ByVal strTipo As String, ByVal nAnno As Integer, _
                                            ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lFase As Integer, _
                                            ByRef dttOut As DataTable) As Boolean
    Try
      'non più utilizzata. mantenuta solo per compatibilità con versioni precedenti
      Return ApriSaldiClav(strDitta, strCodart, bNew, nMagClav, strTipo, nAnno, strSerie, lNumdoc, lFase, dttOut, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ApriSaldiClav(ByVal strDitta As String, ByVal strCodart As String, ByVal bNew As Boolean, _
                                            ByVal nMagClav As Integer, ByVal strTipo As String, ByVal nAnno As Integer, _
                                            ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lFase As Integer, _
                                            ByRef dttOut As DataTable, ByVal lCommeca As Integer) As Boolean
    Dim strSQL As String = ""
    Dim lIIMMSaldoBoll As Integer = 0
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, bNew, nMagClav, strTipo, nAnno, strSerie, _
                                             lNumdoc, lFase, dttOut, lCommeca})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(9), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      '------------------------------
      'creo i saldi: non posso usare la 'bussp_bsveboll9_creasalcl', perchè si appoggia a mmtrans e mmtrasca
      'a questi saldi poi vanno aggiunti quelli del documento in modifica, se non documento nuovo
      strSQL = "SELECT " & CStrSQL(strCodart) & " as mm_codart, " & nMagClav & " as mm_magaz, movmag.mm_cltipo, " & _
               " movmag.mm_clanno, movmag.mm_clserie, movmag.mm_clnum, movmag.mm_clriga, min(tm_datdoc) as Datdoc, " & _
               " Sum(movmag.mm_quant*keymag.km_carscar) as Somma, min(mIT.mm_ornum) as NumIT, " & _
               " min(movmag.mm_commeca) as mm_commeca, min(co_descr1) as xx_commeca, " & _
               " min(movmag.mm_subcommeca) as mm_subcommeca, min(movmag.mm_lotto) as mm_lotto, min(analotti.alo_lottox) as xx_lottox, " & _
               " min(movmag.mm_descr) As mm_descr, min(movmag.mm_desint) as mm_desint, min(movmag.mm_note) as mm_note " & _
               " FROM ((((movmag INNER JOIN keymag ON movmag.codditt = keymag.codditt AND movmag.mm_riga = keymag.km_riga AND " & _
               " movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_serie = keymag.km_serie AND " & _
               " movmag.mm_anno = keymag.km_anno AND movmag.mm_tipork = keymag.km_tipork) " & _
               " LEFT JOIN testmag ON movmag.mm_cltipo = testmag.tm_tipork AND " & _
               " movmag.mm_clanno = testmag.tm_anno AND movmag.mm_clserie = testmag.tm_serie AND " & _
               " movmag.mm_clnum = testmag.tm_numdoc AND movmag.codditt = testmag.codditt) " & _
               " LEFT JOIN movmag as mIT ON movmag.codditt = mIT.codditt AND movmag.mm_cltipo = mIT.mm_tipork AND " & _
               " movmag.mm_clanno = mIT.mm_anno AND movmag.mm_clserie = mIT.mm_serie AND " & _
               " movmag.mm_clnum = mIT.mm_numdoc AND movmag.mm_clriga = mIT.mm_riga) " & _
               " LEFT JOIN commess ON movmag.codditt = commess.codditt " & _
               " AND movmag.mm_commeca = commess.co_comme) " & _
               " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto " & _
               " WHERE km_codart = " & CStrSQL(strCodart) & " AND km_magaz = " & nMagClav.ToString & _
               " AND km_fase = " & lFase.ToString & " AND keymag.codditt = " & CStrSQL(strDitta) & _
               " AND movmag.mm_clnum <> 0 "
      If lCommeca <> 0 Then strSQL += " AND movmag.mm_commeca = " & lCommeca.ToString
      If bNew = False Then
        strSQL += " AND NOT (movmag.mm_tipork = " & CStrSQL(IIf(strTipo = "T", "U", strTipo)) & _
                            " AND movmag.mm_anno = " & nAnno.ToString & _
                            " AND movmag.mm_serie = " & CStrSQL(strSerie) & _
                            " AND movmag.mm_numdoc = " & lNumdoc.ToString & ")"
      End If
      strSQL += " GROUP BY movmag.mm_cltipo, movmag.mm_clanno, movmag.mm_clserie, movmag.mm_clnum, movmag.mm_clriga " & _
               " HAVING(Sum(movmag.mm_quant * keymag.km_carscar) <> 0)"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ApriSaldiClavProd(ByVal strDitta As String, ByVal strCodart As String, ByVal bNew As Boolean, _
                                          ByVal nMagClav As Integer, ByVal strTipo As String, ByVal nAnno As Integer, _
                                          ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lFase As Integer, _
                                          ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim lIIMMSaldoBoll As Integer = 0
    Try
      '------------------------------
      'creo i saldi: non posso usare la 'bussp_bsveboll9_creasalcp', perchè si appoggia a mmtrans e mmtrasca
      'a questi saldi poi vanno aggiunti quelli del documento in modifica, se non documento nuovo
      strSQL = "SELECT " & CStrSQL(strCodart) & " as mm_codart, " & nMagClav & " as mm_magaz, movmag.mm_cctipo, " & _
               " movmag.mm_ccanno, movmag.mm_ccserie, movmag.mm_ccnum, movmag.mm_ccriga, min(tm_datdoc) as Datdoc, " & _
               " Sum(movmag.mm_quant*keymag.km_carscar) as Somma, min(mIT.mm_ornum) as NumIT " & _
               " FROM ((movmag INNER JOIN keymag ON movmag.mm_riga = keymag.km_riga AND " & _
               " movmag.mm_numdoc = keymag.km_numdoc AND movmag.mm_serie = keymag.km_serie AND " & _
               " movmag.mm_anno = keymag.km_anno AND movmag.mm_tipork = keymag.km_tipork) " & _
               " LEFT JOIN testmag ON movmag.mm_cctipo = testmag.tm_tipork AND " & _
               " movmag.mm_ccanno = testmag.tm_anno AND movmag.mm_ccserie = testmag.tm_serie AND " & _
               " movmag.mm_ccnum = testmag.tm_numdoc AND movmag.codditt = testmag.codditt) " & _
               " LEFT JOIN movmag as mIT ON movmag.codditt = mIT.codditt AND movmag.mm_cctipo = mIT.mm_tipork AND " & _
               " movmag.mm_ccanno = mIT.mm_anno AND movmag.mm_ccserie = mIT.mm_serie AND " & _
               " movmag.mm_ccnum = mIT.mm_numdoc AND movmag.mm_ccriga = mIT.mm_riga " & _
               " WHERE km_codart = " & CStrSQL(strCodart) & " AND km_magaz = " & nMagClav.ToString & _
               " AND km_fase = " & lFase.ToString & " AND keymag.codditt = " & CStrSQL(strDitta) & _
               " AND movmag.mm_ccnum <> 0 "
      If bNew = False Then
        strSQL += " AND NOT (movmag.mm_tipork = " & CStrSQL(IIf(strTipo = "T", "U", strTipo)) & _
                            " AND movmag.mm_anno = " & nAnno.ToString & _
                            " AND movmag.mm_serie = " & CStrSQL(strTipo) & _
                            " AND movmag.mm_numdoc = " & lNumdoc.ToString & ")"
      End If
      strSQL += " GROUP BY movmag.mm_cctipo, movmag.mm_ccanno, movmag.mm_ccserie, movmag.mm_ccnum, movmag.mm_ccriga " & _
               " HAVING(Sum(movmag.mm_quant * keymag.km_carscar) <> 0)"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ClavGetTmDatdoc(ByVal strDitta As String, ByVal strClTipoDoc As String, _
                                           ByVal nClAnno As Integer, ByVal strClSerie As String, _
                                           ByVal lClNumdoc As Integer, Optional ByRef dttOut As DataTable = Nothing) As DateTime
    Try
      'non obsoleta!!!!
      Return ClavGetTmDatdoc(strDitta, strClTipoDoc, nClAnno, strClSerie, lClNumdoc, -1, dttOut)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ClavGetTmDatdoc(ByVal strDitta As String, ByVal strClTipoDoc As String, _
                                              ByVal nClAnno As Integer, ByVal strClSerie As String, _
                                              ByVal lClNumdoc As Integer, ByVal lClRiga As Integer, _
                                              ByRef dttOut As DataTable) As DateTime

    'restituisce true se il documento esiste, diversamente false
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strClTipoDoc, nClAnno, strClSerie, lClNumdoc, lClRiga, dttOut})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(6), DataTable)
        Return CType(oOut, DateTime)
      End If
      '----------------

      If dttOut Is Nothing Then dttOut = New DataTable
      ClavGetTmDatdoc = Nothing
      strSQL = "SELECT * FROM testmag"
      If lClRiga <> -1 Then
        strSQL = "SELECT testmag.*, mm_lotto, alo_lottox as xx_lottox, mm_commeca, mm_subcommeca, mm_ubicaz, mm_descr, mm_desint, mm_note, co_descr1 as xx_commeca, an_descr1 as xx_desconto " & _
                 " FROM " & strJoinTestmagMovmag & _
                 " LEFT JOIN commess ON movmag.codditt = commess.codditt AND movmag.mm_commeca = commess.co_comme" & _
                 " LEFT JOIN anagra ON movmag.codditt = anagra.codditt AND testmag.tm_conto = anagra.an_conto" & _
                 " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto"
      Else
        strSQL = "SELECT *, '' as xx_desconto FROM testmag"
      End If

      strSQL += " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
               " AND tm_tipork = " & CStrSQL(strClTipoDoc) & _
               " AND tm_anno = " & nClAnno & _
               " AND tm_serie = " & CStrSQL(strClSerie) & _
               " AND tm_numdoc = " & lClNumdoc.ToString
      If lClRiga <> -1 Then strSQL += " AND mm_riga = " & lClRiga.ToString

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttOut.Rows.Count > 0 Then
        ClavGetTmDatdoc = NTSCDate(dttOut.Rows(0)!tm_datdoc)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ClavGetDatiRiga(ByVal strDitta As String, ByVal strClTipoDoc As String, _
                                              ByVal nClAnno As Integer, ByVal strClSerie As String, _
                                              ByVal lClNumdoc As Integer, ByVal lClRiga As Integer, _
                                              ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If dttOut Is Nothing Then dttOut = New DataTable
      '--------------------------------------------------------------------------------------------------------------
      If lClRiga = -1 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT movmag.*, co_descr1 AS xx_commeca, alo_lottox AS xx_lottox" & _
        " FROM movmag LEFT JOIN commess ON movmag.codditt = commess.codditt AND movmag.mm_commeca = commess.co_comme" & _
        " LEFT JOIN analotti ON movmag.codditt = analotti.codditt AND movmag.mm_codart = analotti.alo_codart AND movmag.mm_lotto = analotti.alo_lotto" & _
        " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
        " AND mm_tipork = " & CStrSQL(strClTipoDoc) & _
        " AND mm_anno = " & nClAnno & _
        " AND mm_serie = " & CStrSQL(strClSerie) & _
        " AND mm_numdoc = " & lClNumdoc.ToString & _
        " AND mm_riga = " & lClRiga.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttOut.Rows.Count = 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function LeggiLottoRiga(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                           ByVal nAnno As Integer, ByVal strSerie As String, _
                                           ByVal lNumdoc As Integer, ByVal lRiga As Integer) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As DataTable

    Try
      LeggiLottoRiga = 0
      strSQL = "SELECT mm_lotto FROM movmag" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mm_tipork = " & CStrSQL(strTipoDoc) & _
               " AND mm_anno = " & nAnno & _
               " AND mm_serie = " & CStrSQL(strSerie) & _
               " AND mm_numdoc = " & lNumdoc.ToString & _
               " AND mm_riga = " & lRiga.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        LeggiLottoRiga = NTSCInt(dttTmp.Rows(0)!mm_lotto)
      End If
      dttTmp.Clear()

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetWhereStampaRegCassa(ByVal strDitta As String, ByVal strTipoDoc As String, _
                               ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                               ByVal bOrdini As Boolean) As String
    Dim strSQL As String = ""
    Try
      If bOrdini Then
        Return " testord.td_tipork = " & CStrSQL(strTipoDoc) & _
               " AND testord.td_anno = " & nAnno & _
               " AND testord.td_serie = " & CStrSQL(strSerie) & _
               " AND testord.td_numord = " & lNumdoc.ToString
      Else
        Return " testmag.tm_tipork = " & CStrSQL(strTipoDoc) & _
               " AND testmag.tm_anno = " & nAnno & _
               " AND testmag.tm_serie = " & CStrSQL(strSerie) & _
               " AND testmag.tm_numdoc = " & lNumdoc.ToString
      End If
      Return ""

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTotincaFromReincpag(ByVal strDitta As String, ByVal strTipoDoc As String, _
                               ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Decimal
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      GetTotincaFromReincpag = -999999999
      strSQL = "SELECT mmp_importoda FROM reincpag " & _
               "WHERE codditt = " & CStrSQL(strDitta) & " AND mmp_tipork = '" & strTipoDoc & _
               "' AND mmp_anno = " & nAnno & " AND mmp_serie = '" & strSerie & _
               "' AND mmp_numdoc = " & lNumdoc & " AND mmp_tipoentusc = 'C' " & _
               "ORDER BY mmp_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCDec(dttTmp.Rows(0)!mmp_importoda)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function GetWhereHltd(ByVal strDitta As String, ByVal strTipork As String, _
                                          ByVal bNonProporreEvasioneIPSuNotePrel As Boolean, _
                                          ByVal bNonMostrareImpegniPrenotASaldo As Boolean, _
                                          ByVal bDocEmesso As Boolean, ByVal bNoEvasioneOrdSospesi As Boolean, _
                                          ByVal nValuta As Integer, ByVal lConto As Integer, _
                                          ByVal strScorpo As String) As String
    Try
      'obsoleta
      Return GetWhereHltd(strDitta, strTipork, bNonProporreEvasioneIPSuNotePrel, bNonMostrareImpegniPrenotASaldo, _
                          bDocEmesso, bNoEvasioneOrdSospesi, nValuta, lConto, strScorpo, False)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetWhereHltd(ByVal strDitta As String, ByVal strTipork As String, _
                                           ByVal bNonProporreEvasioneIPSuNotePrel As Boolean, _
                                           ByVal bNonMostrareImpegniPrenotASaldo As Boolean, _
                                           ByVal bDocEmesso As Boolean, ByVal bNoEvasioneOrdSospesi As Boolean, _
                                           ByVal nValuta As Integer, ByVal lConto As Integer, _
                                           ByVal strScorpo As String, ByVal bEscudiDocRetail As Boolean) As String
    Dim strSQL As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, bNonProporreEvasioneIPSuNotePrel, _
                                             bNonMostrareImpegniPrenotASaldo, bDocEmesso, bNoEvasioneOrdSospesi, _
                                             nValuta, lConto, strScorpo, bEscudiDocRetail})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '----------------

      Select Case strTipork
        Case "T"
          strSQL = "td_tipork = 'H'"
        Case "U"
          strSQL = "td_tipork = 'Y'"
        Case "W"
          If bNonProporreEvasioneIPSuNotePrel Then
            strSQL = "(td_tipork = 'R' OR td_tipork = 'X')"
          Else
            strSQL = "(td_tipork = 'R' OR td_tipork = 'Y' OR td_tipork = 'X')"
          End If
          If bNonMostrareImpegniPrenotASaldo Then
            'non visualizza le testate degli ordini prenotati a saldo
            strSQL = strSQL & " AND EXISTS ( SELECT mo_flevapre FROM movord WHERE movord.codditt = " & _
                     CStrSQL(strDitta) & " AND testord.td_tipork = movord.mo_tipork AND " & _
                     " testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie AND " & _
                     " testord.td_numord = movord.mo_numord AND movord.mo_flevapre = 'C')"
          End If
        Case "Z" ' doc. uso interno : per scarico da commessa evade anche impegni di prod. e di trasf.
          strSQL = "(td_tipork = 'O' OR td_tipork = 'Y' OR td_tipork = 'X' OR td_tipork = '#')"
        Case Else
          If Not bDocEmesso Then
            strSQL = "(td_tipork = 'O' OR td_tipork = '#')"
          Else
            strSQL = "(td_tipork = 'R' OR td_tipork = 'X' OR td_tipork = '#')"
          End If
      End Select
      If bEscudiDocRetail Then strSQL += " AND td_retail <> 'S'" 'se ic fatto da retail con modulo promozioni abilitato, posso evaderli solo da programma di retail
      If bNoEvasioneOrdSospesi Then
        strSQL = strSQL & " AND td_sospeso = 'N' "
      End If
      If lConto <> 0 Then strSQL += " AND td_conto = " & lConto
      strSQL = strSQL & " AND td_flevas <> 'S' AND td_valuta = " & nValuta & _
                " AND td_scorpo = " & CStrSQL(strScorpo)

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetWhereHlmo(ByVal strDitta As String, ByVal strTipork As String, _
                                       ByVal strScorpo As String, ByVal lConto As Integer, ByVal nValuta As Integer, _
                                       ByVal bNonProporreEvasioneIPSuNotePrel As Boolean, _
                                       ByVal bDocEmesso As Boolean, ByVal bFiltroMagazRigheOrdini As Boolean, _
                                       ByVal nFiltroMagazRigheOrdiniEscludiTipobf As Integer, _
                                       ByVal bNoEvasioneOrdSospesi As Boolean, _
                                       ByVal nMagaz As Integer, ByVal nMagaz2 As Integer, _
                                       ByVal nTipobf As Integer) As String
    Try
      'obsoleta
      Return GetWhereHlmo(strDitta, strTipork, strScorpo, lConto, nValuta, bNonProporreEvasioneIPSuNotePrel, _
                           bDocEmesso, bFiltroMagazRigheOrdini, nFiltroMagazRigheOrdiniEscludiTipobf, _
                           bNoEvasioneOrdSospesi, nMagaz, nMagaz2, nTipobf, False)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetWhereHlmo(ByVal strDitta As String, ByVal strTipork As String, _
                                           ByVal strScorpo As String, ByVal lConto As Integer, ByVal nValuta As Integer, _
                                           ByVal bNonProporreEvasioneIPSuNotePrel As Boolean, _
                                           ByVal bDocEmesso As Boolean, ByVal bFiltroMagazRigheOrdini As Boolean, _
                                           ByVal nFiltroMagazRigheOrdiniEscludiTipobf As Integer, _
                                           ByVal bNoEvasioneOrdSospesi As Boolean, _
                                           ByVal nMagaz As Integer, ByVal nMagaz2 As Integer, _
                                           ByVal nTipobf As Integer, ByVal bEscudiDocRetail As Boolean) As String
    Dim strSQL As String = ""
    Dim strTipord As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, strScorpo, lConto, nValuta, _
                                             bNonProporreEvasioneIPSuNotePrel, bDocEmesso, bFiltroMagazRigheOrdini, _
                                             nFiltroMagazRigheOrdiniEscludiTipobf, bNoEvasioneOrdSospesi, _
                                             nMagaz, nMagaz2, nTipobf, bEscudiDocRetail})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '----------------

      GetWhereHlmo = ""

      'Costruisce la WHERE della query
      Select Case strTipork
        Case "T"
          strTipord = "td_tipork = 'H'"
        Case "U"
          strTipord = "td_tipork = 'Y'"
        Case "W"
          If bNonProporreEvasioneIPSuNotePrel Then
            strTipord = "(td_tipork = 'R' OR td_tipork = 'X' ) AND mo_flevapre <> 'S'"
          Else
            strTipord = "(td_tipork = 'R' OR td_tipork = 'Y' OR td_tipork = 'X' ) AND mo_flevapre <> 'S'"
          End If
        Case "Z"
          strTipord = "(td_tipork = 'O' OR td_tipork = 'Y' OR td_tipork = 'X' OR td_tipork = '#') AND mo_flevapre <> 'S'"
        Case Else
          If Not bDocEmesso Then
            strTipord = "(td_tipork = 'O' OR td_tipork = '#')"
          Else
            strTipord = "(td_tipork = 'R' OR td_tipork = 'X' OR td_tipork = '#')"
          End If
      End Select

      strSQL = strTipord & " AND td_conto = " & lConto & " AND td_valuta = " & _
               nValuta & " AND td_scorpo = '" & strScorpo & "' AND mo_flevas <> 'S'"

      If bEscudiDocRetail Then strSQL += " AND td_retail <> 'S'" 'se ic fatto da retail con modulo promozioni abilitato, posso evaderli solo da programma di retail

      '-----------------------------------
      'se il tipo bolla/fattura inserita nell'opzione di registro
      '"nFiltroMagazRigheOrdiniEscludiTipobf" è uguale al tipo bolla/fattura indicato
      'in testata, NON applica il filtro sui magazzini
      If bFiltroMagazRigheOrdini = True Then
        strSQL = strSQL & " AND mo_magaz = " & nMagaz
        If nFiltroMagazRigheOrdiniEscludiTipobf <> nTipobf Then
          strSQL = strSQL & " AND mo_magaz2 = " & nMagaz2
        End If
      End If
      If bNoEvasioneOrdSospesi Then
        strSQL = strSQL & " AND td_sospeso = 'N' "
      End If

      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetWhereHltm(ByVal strDitta As String, ByVal strTipork As String, ByVal nValuta As Integer, ByVal lConto As Integer, _
                                         ByVal strScorpo As String) As String
    Dim strSQL As String = ""
    Try
      strSQL = " tm_tipork = " & CStrSQL(strTipork) & _
              " AND tm_valuta = " & nValuta.ToString & _
              " AND tm_flevas <> 'S'" & _
              " AND tm_scorpo = " & CStrSQL(strScorpo)
      If lConto <> 0 Then strSQL += " AND tm_conto = " & lConto.ToString
      Return strSQL

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function HlbcGetData(ByVal strDitta As String, ByVal strQuery As String, ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga, td_conto as xx_conto, " & _
               " mo_codart, mo_fase, mo_descr, mo_magaz, tb_desmaga as xx_magaz, td_datord as xx_datord, mo_datcons, " & _
               " mo_quant, mo_quapre, mo_quaeva, td_codpaga as xx_codpaga, td_alfpar as xx_alfpar, " & _
               " td_coddest as xx_coddest, td_riferim as xx_riferim, mo_note, " & _
               " mo_prezzo, mo_scont1, mo_scont2, mo_scont3, mo_valore, (mo_quant - mo_quaeva) As xx_qtares, " & _
               " 0.0 as xx_quadaeva, 'C' as xx_flevasdaeva, 'N' as xx_status, " & _
               " mo_commeca, mo_subcommeca, mo_scont4, mo_scont5, mo_scont6, 0 as xx_prefer " & _
               " FROM (testord INNER JOIN movord ON (testord.codditt = movord.codditt) AND " & _
               " (testord.td_numord = movord.mo_numord) AND (testord.td_serie = movord.mo_serie) AND " & _
               " (testord.td_anno = movord.mo_anno) AND (testord.td_tipork = movord.mo_tipork))" & _
               " LEFT JOIN tabmaga ON movord.codditt = tabmaga.codditt AND movord.mo_magaz = tabmaga.tb_codmaga " & _
               " WHERE testord.codditt = " & CStrSQL(strDitta) & _
               " AND " & strQuery
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SELDOC")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function RitornaNoteEstensioni(ByVal strDitta As String, ByVal lConto As Integer, _
    ByVal lCoddest As Integer, ByVal lCodlead As Integer, ByVal strTipork As String, _
    ByVal nRiportaNoteDaAnaext As Integer, ByRef strNoteOut As String) As Boolean
    Dim strSQL As String = ""
    Dim strCampoAnaext As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strNoteOut = ""
      '--------------------------------------------------------------------------------------------------------------
      Select Case nRiportaNoteDaAnaext
        Case 1 : strCampoAnaext = "ax_descr1"
        Case 2 : strCampoAnaext = "ax_descr2"
        Case 3 : strCampoAnaext = "ax_descr3"
        Case 4 : strCampoAnaext = "ax_descr4"
        Case 5 : strCampoAnaext = "ax_descr5"
        Case 6 : strCampoAnaext = "ax_descr6"
        Case 7 : strCampoAnaext = "ax_descr7"
        Case 8 : strCampoAnaext = "ax_descr8"
        Case 9 : strCampoAnaext = "ax_descr9"
        Case 10 : strCampoAnaext = "ax_descr10"
        Case 11 : strCampoAnaext = "ax_desext1"
        Case 12 : strCampoAnaext = "ax_desext2"
        Case 13 : strCampoAnaext = "ax_desext3"
        Case 14 : strCampoAnaext = "ax_memo1"
        Case Else : strCampoAnaext = "ax_memo2"
      End Select
      strSQL = "SELECT " & strCampoAnaext & " As Note FROM anaext" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ax_conto = " & lConto & _
        " AND ax_coddest = " & lCoddest & _
        " AND ax_codlead = " & lCodlead & _
        " AND ax_tipork = " & CStrSQL(strTipork)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      strNoteOut = dttTmp.Rows(0)!Note.ToString
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function RitornaArticoloLotto(ByVal strDitta As String, ByVal strIn As String, _
                                                   ByRef strCodart As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strCodart = ""
      '--------------------------------------------------------------------------------------------------------------
      '--- Esce se il valore di ingresso é vuoto
      '--------------------------------------------------------------------------------------------------------------
      If strIn.Trim = "" Then Return False
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 2 alo_codart FROM analotti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND alo_lottox = " & CStrSQL(strIn) & _
        " ORDER BY codditt, alo_codart, alo_lotto"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non esistono records o esiste più di un record riferito al lotto passato, esce
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count <> 1 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      '--- Ritorna articolo e lotto
      '--------------------------------------------------------------------------------------------------------------
      strCodart = NTSCStr(dttTmp.Rows(0)!alo_codart)
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function IsContoWithDestdiv(ByVal strDitta As String, ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      IsContoWithDestdiv = False

      strSQL = "SELECT top 1 dd_conto FROM destdiv" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND dd_conto = " & lConto.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


  Public Overridable Function SelezionaRigheLista(ByVal strDitta As String, ByVal lLista As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT listsar.* FROM listsar " & _
               "WHERE codditt = " & CStrSQL(strDitta) & " AND lsa_codlsar = " & lLista

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaListe(ByVal strDitta As String, ByVal lMaga As Integer, ByVal strDescr As String, ByVal strOperat As String, _
                                          ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tablsar.* FROM tablsar " & _
               " WHERE tb_status = 'C' " & _
               " AND codditt = " & CStrSQL(strDitta)

      If lMaga <> 0 Then
        strSQL &= " AND tb_codmagap = " & lMaga
      End If

      If strDescr.Trim <> "" Then
        strSQL &= " AND tb_deslsar LIKE " & CStrSQL("%" & strDescr & "%")
      End If

      If strOperat.Trim <> "" Then
        strSQL &= " AND tb_opnomest LIKE " & CStrSQL("%" & strOperat & "%")
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CheckInPromozione(ByVal strDitta As String, ByVal lLista As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_codrepr, tb_desrepr FROM tabrepr " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND (tb_codlsar = " & lLista & _
               "   OR tb_lsaromg = " & lLista & ")"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CancellaLista(ByVal strDitta As String, ByVal lLista As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      strSQL = "DELETE FROM listsar " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND lsa_codlsar = " & lLista

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM tablsar " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tb_codlsar = " & lLista

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

  Public Overridable Function CheckSubcomm(ByVal strDitta As String, ByVal lCommeca As Integer, ByVal strSubcomm As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT sco_commeca FROM subcomm" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND sco_commeca = " & lCommeca & _
               " AND sco_subcommeca = " & CStrSQL(strSubcomm)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


  Public Overridable Function GetTaskPadre(ByVal strDitta As String, ByVal lCommeca As Integer, ByVal lTaskId As Integer) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    GetTaskPadre = 0

    Try
      strSQL = " SELECT tsk_uidpadre FROM tasks " & _
                         " WHERE codditt = " & CStrSQL(strDitta) & _
                         " And tsk_commeca = " & lCommeca & _
                         " And tsk_taskid = " & lTaskId
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!tsk_uidpadre)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function GetTaskFromTaskIdETaskIdPadre(ByVal strDitta As String, ByVal lCommeca As Integer, ByVal lTaskId As Integer, ByVal lTaskIdPadre As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = " SELECT * FROM tasks " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " and tsk_commeca = " & lCommeca & _
               " and tsk_taskid = " & lTaskId & _
               " and tsk_uidpadre = " & lTaskIdPadre
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If Not dttTmp.Rows.Count = 0 Then Return True

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function CheckTaskVebollttproesec(ByVal strDitta As String, ByVal strTipork As String, ByVal nAnno As Integer, _
                                                       ByVal strSerie As String, ByVal lNumdoc As Integer, ByVal lRiga As Integer, _
                                                       ByVal lCommeca As Integer, ByVal lTaskid As Integer, ByVal lttProeseb As Integer, _
                                                       ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM ttproesec WHERE codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lttProeseb & _
               " AND pes_commeca = " & lCommeca & _
               " AND pes_taskid = " & lTaskid & _
               " AND pes_mmtipork = " & CStrSQL(strTipork) & _
               " AND pes_mmanno = " & nAnno & _
               " AND pes_mmserie = " & CStrSQL(strSerie) & _
               " AND pes_mmnum = " & lNumdoc & _
               " AND pes_mmriga = " & lRiga
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckEsercizioInCa2(ByVal strDitta As String, ByVal strDatdoc As String) As Boolean
    'ritorna false se per l'esercizio che contiene la data del documento NON è gestita la CA DC
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    CheckEsercizioInCa2 = True
    Try
      strSQL = "SELECT tb_gestcadp" & _
              " FROM tabesco " & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tb_dtineser <= " & CDataSQL(strDatdoc) & _
              " AND tb_dtfieser >= " & CDataSQL(strDatdoc) & _
              " AND tb_gestcadp = 'S'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function


  Public Overridable Function ChekEsistenzaFinale(ByVal strDitta As String, ByVal strCodart As String, _
                                                 ByVal nFase As Integer, ByVal nMagaz As Integer, _
                                                 ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ISNULL(ap_esist, 0) AS ap_esist, ISNULL(ap_prenot, 0) AS ap_prenot, ar_gescomm, ar_stainv, ISNULL(ap_magaz, 0) AS ap_magaz," & _
               " ISNULL(ap_impeg, 0) AS ap_impeg, ISNULL(ap_ordin, 0) AS ap_ordin" & _
               " FROM artico LEFT JOIN artpro ON artpro.codditt = artico.codditt AND  artpro.ap_codart = artico.ar_codart " & _
               "                             AND ap_fase = " & nFase.ToString & _
               "                             AND ap_magaz = " & nMagaz.ToString & _
               " WHERE artico.codditt = " & CStrSQL(strDitta) & _
               " AND ar_codart = " & CStrSQL(strCodart)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)


      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ChekEsistenzaFinaleTC(ByVal strDitta As String, ByVal strCodart As String, _
                                                    ByVal nFase As Integer, ByVal nMagaz As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strSuffTc As String = ""
    Try
      strSQL = "SELECT artprotc.*, ar_gescomm, ar_stainv " & _
               " FROM artico LEFT JOIN artprotc ON artprotc.codditt = artico.codditt AND  artprotc.apt_codart = artico.ar_codart " & _
               "                                    AND apt_fase = " & nFase.ToString & _
               "                                    AND apt_magaz = " & nMagaz.ToString & _
               " WHERE artico.codditt = " & CStrSQL(strDitta) & _
               " AND ar_codart = " & CStrSQL(strCodart)
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ProponiTipoBFdaTRKTPBF(ByVal strDitta As String, ByVal strTipork As String, _
    ByVal strSerie As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      ProponiTipoBFdaTRKTPBF = 0
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tkt_tipobf" & _
        " FROM trktpbf INNER JOIN tabtpbf ON trktpbf.codditt = tabtpbf.codditt AND trktpbf.tkt_tipobf = tabtpbf.tb_codtpbf" & _
        " WHERE trktpbf.codditt = " & CStrSQL(strDitta) & _
        " AND tkt_tipork = " & CStrSQL(strTipork) & _
        " AND tkt_serie = " & CStrSQL(strSerie) & _
        " AND tkt_conto = 0" & _
        " AND tkt_tipobf <> 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!tkt_tipobf)
      '--------------------------------------------------------------------------------------------------------------
      Return 0
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
  Public Overridable Function CheckSoloSerieInTRKTPBF(ByVal strDitta As String, ByVal strTipork As String, _
                                                      ByVal strSerie As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tkt_tipobf" & _
        " FROM trktpbf INNER JOIN tabtpbf ON trktpbf.codditt = tabtpbf.codditt AND trktpbf.tkt_tipobf = tabtpbf.tb_codtpbf" & _
        " WHERE trktpbf.codditt = " & CStrSQL(strDitta) & _
        " AND tkt_tipork = " & CStrSQL(strTipork) & _
        " AND tkt_serie = " & CStrSQL(strSerie) & _
        " AND tkt_conto = 0" & _
        " AND tkt_tipobf <> 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then
        Return True
      Else
        Return False
      End If

      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function RitornaPrezzoPerCalcoloRic_Marg(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal nCodlavo As Integer, ByVal lConto As Integer, ByVal nListino As Integer, ByVal nCodvalu As Integer, _
    ByVal nCodtpro As Integer, ByVal strDatagg As String, ByVal dDaquant As Decimal, ByVal nFase As Integer, _
    ByVal strUnmis As String) As Decimal
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      RitornaPrezzoPerCalcoloRic_Marg = 0
      '--------------------------------------------------------------------------------------------------------------
      If nListino > 0 Then
        strSQL = "SELECT TOP 1 lc_prezzo AS prezzo FROM listini" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND lc_codart = " & CStrSQL(strCodart) & _
                 " AND lc_codlavo = " & nCodlavo & _
                 " AND lc_conto = " & lConto & _
                 " AND lc_listino = " & nListino & _
                 " AND lc_codvalu = " & nCodvalu & _
                 " AND lc_codtpro = " & nCodtpro & _
                 " AND " & CDataSQL(NTSCDate(strDatagg).ToShortDateString) & " BETWEEN lc_datagg AND lc_datscad" & _
                 " AND " & CDblSQL(dDaquant) & " BETWEEN lc_daquant AND lc_aquant" & _
                 " AND lc_fase = " & nFase & _
                 " AND lc_unmis = " & CStrSQL(strUnmis) & _
                 " ORDER BY lc_datagg DESC, lc_daquant"
      ElseIf nListino >= -1 Then
        strSQL = "SELECT " & IIf(nListino = 0, "apx_ultcos", "apx_peucos").ToString & " AS prezzo" & _
                 " FROM artprox" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND apx_codart = " & CStrSQL(strCodart) & _
                 " AND apx_fase = " & nFase
      Else
        strSQL = "SELECT CASE WHEN apx_qtalif <> 0 THEN apx_vqtalif / apx_qtalif ELSE " & _
                 "         CASE WHEN apx_giaini <> 0 THEN apx_vgiaini / apx_giaini ELSE 0 END " & _
                 "       END * ar_perqta AS prezzo " & _
                 " FROM artprox" & _
                 "  INNER JOIN artico ON artico.codditt = artprox.codditt AND apx_codart = ar_codart " & _
                 " WHERE artprox.codditt = " & CStrSQL(strDitta) & _
                 " AND apx_codart = " & CStrSQL(strCodart) & _
                 " AND apx_fase = " & nFase
      End If
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then
        If dttTmp.Rows(0)!prezzo.Equals(DBNull.Value) = False Then Return NTSCDec(dttTmp.Rows(0)!prezzo)
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
    Return 0
  End Function

#Region "Logistica di magazzino su palmare"
  Public Overridable Function TrascoUbicaz(ByVal strDitta As String, ByVal strUbicaz As String, ByVal lMaga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT au_ubicaz FROM anaubic WHERE codditt = " & CStrSQL(strDitta) & " AND au_barcode = " & CStrSQL(strUbicaz) & _
               " AND au_magaz = " & lMaga

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ValUbicaz(ByVal strDitta As String, ByVal strUbicaz As String, ByVal lMaga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT anaubic.* FROM anaubic WHERE codditt = " & CStrSQL(strDitta) & " AND au_ubicaz = " & CStrSQL(strUbicaz) & _
               " AND au_magaz = " & lMaga

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function FigliUbicazione(ByVal strDitta As String, ByVal strL1 As String, ByVal strL2 As String, ByVal strL3 As String, _
                                              ByVal strL4 As String, ByVal strL5 As String, ByVal lMaga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT anaubic.* FROM anaubic WHERE codditt = " & CStrSQL(strDitta) & _
               NTSCStr(IIf(strL1.Trim("-"c) <> "", " AND au_zona = " & CStrSQL(strL1), "")) & _
               NTSCStr(IIf(strL2.Trim("-"c) <> "", " AND au_scaff = " & CStrSQL(strL2), "")) & _
               NTSCStr(IIf(strL3.Trim("-"c) <> "", " AND au_posiz = " & CStrSQL(strL3), "")) & _
               NTSCStr(IIf(strL4.Trim("-"c) <> "", " AND au_piano = " & CStrSQL(strL4), "")) & _
               NTSCStr(IIf(strL5.Trim("-"c) <> "", " AND au_cella = " & CStrSQL(strL5), "")) & _
               " AND au_magaz = " & lMaga

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  '----------------------------------------------
  Public Overridable Function TrovaDatiOrdine(ByVal strDitta As String, ByVal strTipork As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                              ByVal lNumord As Integer, ByVal lRiga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mo_lotto, mo_ubicaz FROM movord " & _
               "WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mo_tipork = " & CStrSQL(strTipork) & _
               " AND mo_anno = " & lAnno & _
               " AND mo_serie = " & CStrSQL(strSerie) & _
               " AND mo_numord = " & lNumord & _
               " AND mo_riga = " & lRiga

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrovaDatiNotaDiPrelievo(ByVal strDitta As String, ByVal strTipork As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                                      ByVal lNumdoc As Integer, ByVal lRiga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mm_lotto, mm_ubicaz FROM movprb " & _
               "WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mm_tipork = " & CStrSQL(strTipork) & _
               " AND mm_anno = " & lAnno & _
               " AND mm_serie = " & CStrSQL(strSerie) & _
               " AND mm_numdoc = " & lNumdoc & _
               " AND mm_riga = " & lRiga

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  '----------------------------------------------
#End Region

#Region "funzioni specifiche per BNDBSECE.VB"
  Public Overridable Function GetDataSece(ByVal strDitta As String, ByVal lSeceIITTTabcent As Integer, _
                                          ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM TTTABCENT" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lSeceIITTTabcent

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTTABCENT")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNDBLOMA.VB"
  Public Overridable Function GetDataLoma(ByVal strDitta As String, ByVal lIITTartico As Integer, _
                                          ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT ttartico.*, alo_lottox AS xx_lottox" & _
        " FROM TTARTICO LEFT JOIN analotti ON TTARTICO.codditt = analotti.codditt AND TTARTICO.ar_codart = analotti.alo_codart AND TTARTICO.ar_maxlotto = analotti.alo_lotto" & _
        " WHERE TTARTICO.codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lIITTartico
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTARTICO")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function LomaUpdateTTArtico(ByVal strDitta As String, ByVal lIITTartico As Integer, _
                                                 ByVal lHlloLotto As Integer, ByVal strRIGAU As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE ttartico SET ar_maxlotto = " & lHlloLotto & "," & _
              " ar_scosic = -1" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lIITTartico & _
              " AND ar_codvar1 = " & CStrSQL(strRIGAU)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNMGHLLO.VB"
  Public Overridable Function GetDataHllo(ByVal strDitta As String, ByVal strHlloCodart As String, _
                                          ByVal nHlloMagaz As Integer, ByVal nHlloFase As Integer, _
                                          ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Min(lp_ubicaz) As xx_ubicaz, Min(lp_lotto) As xx_lotto," & _
        " Min(alo_descr) As xx_descr, Min(alo_codalf) As xx_codalf, Sum(lp_esist) As xx_esist," & _
        " Sum(lp_esist - lp_prenot) As xx_dispnetta, Min(alo_dtprep) As xx_dtprep," & _
        " Min(alo_dtscad) As xx_dtscad, Sum(lp_esist - lp_impeg + lp_ordin) As xx_disponibilita," & _
        " '' as xx_note, Min(alo_lottox) as xx_lottox, 0.0 as xx_qtaeva" & _
        " FROM lotcpro LEFT JOIN analotti ON lotcpro.codditt = analotti.codditt AND lotcpro.lp_codart = analotti.alo_codart AND lotcpro.lp_lotto = analotti.alo_lotto" & _
        " WHERE lotcpro.codditt = " & CStrSQL(strDitta) & _
        " AND lp_codart = " & CStrSQL(strHlloCodart) & _
        " AND lp_magaz = " & nHlloMagaz & _
        " AND lp_fase = " & nHlloFase & _
        " GROUP BY lp_lotto, lp_ubicaz" & _
        " HAVING Abs(Sum(lp_esist)) > 0.001" & _
        " ORDER BY lp_lotto, lp_ubicaz"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LOTCPRO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNMGHLA2.VB"
  Public Overridable Function GetDataHla2(ByVal strDitta As String, ByVal stredCod As String, _
                                          ByVal stredDescr As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM artico " & _
              " WHERE codditt = " & CStrSQL(strDitta)

      If Trim(stredCod) <> "" Then
        strSQL = strSQL & " AND ar_codart like " & CStrSQL(Trim(stredCod) & "%")
      End If

      If Trim(stredDescr) <> "" Then
        strSQL = strSQL & " AND ar_descr like " & CStrSQL(Trim(stredDescr) & "%")
      End If

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNMGSCTR.VB"
  Public Overridable Function GetOperat(ByVal strDitta As String, ByVal strOperat As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT top 1 (og_descont2 + ' ' + og_descont) as xx_operat FROM organig" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND og_coperat = " & CStrSQL(strOperat)
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "OPERAT")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNMGTERM.VB"
  Public Overridable Function GetImpostazioniFile(ByVal strLiv1 As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT rp_liv2 FROM REGPROP" & _
        " WHERE UPPER(rp_liv1) = " & CStrSQL(strLiv1.ToUpper) & _
        " AND rp_liv2 LIKE 'Terminale%'" & _
        " AND LOWER(rp_liv2) <> 'terminale'" & _
        " GROUP BY rp_liv1, rp_liv2"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ImpostazioneFileGiaEsistente(ByVal strLiv1 As String, ByVal strNome As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 rp_liv2 FROM REGPROP" & _
        " WHERE rp_liv1 = " & CStrSQL(strLiv1.ToUpper) & _
        " AND LOWER(rp_liv2) = " & CStrSQL(strNome.ToLower) & _
        " GROUP BY rp_liv1, rp_liv2"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function DeleteImpostazioniFile(ByVal strLiv1 As String, ByVal strTmp As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM REGPROP" & _
        " WHERE UPPER(rp_liv1) = " & CStrSQL(strLiv1.ToUpper) & _
        " AND LOWER(rp_liv2) = " & CStrSQL(strTmp.ToLower)
      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM REGDIR" & _
        " WHERE UPPER(rd_liv1) = " & CStrSQL(strLiv1.ToUpper) & _
        " AND LOWER(rd_liv2) = " & CStrSQL(strTmp.ToLower)
      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
#End Region

#Region "funzioni specifiche per BNMGCACL.VB"
  Public Overridable Function GetDataCacl(ByVal strDitta As String, ByVal strCodartf As String, _
    ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT artest.*, an_descr1 AS xx_forn, tb_desmarc AS xx_codmarc" & _
        " FROM artest LEFT JOIN anagra ON artest.codditt = anagra.codditt AND artest.ae_forn = anagra.an_conto" & _
        " LEFT JOIN tabmarc ON artest.codditt = tabmarc.codditt AND artest.ae_codmarc = tabmarc.tb_codmarc" & _
        " WHERE artest.codditt = " & CStrSQL(strDitta) & _
        " AND ae_codartf = " & CStrSQL(strCodartf) & _
        " AND ae_codart IS NULL" & _
        " ORDER BY artest.codditt, ae_codartf, ae_forn, ae_codmarc"
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTEST")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
#End Region

#Region "Net@Pro"
  Public Overridable Function SalvaOrdineAggNetPro(ByRef ds As DataSet, ByVal strState As String, _
                                                 ByVal strNetProDB As String, ByRef strErr As String, _
                                                 ByRef dbConn As DbConnection) As Boolean
    Try
      Return MyBase.SalvaOrdineAggNetPro(ds, strState, strNetProDB, strErr, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaOrdineAggNetPro_CaricaOrDaMovmag(ByRef ds As DataSet, ByRef dbConn As DbConnection, _
                                                                    ByRef dttOrd As DataTable, ByRef dttOrdImp As DataTable, _
                                                                    ByRef dttOrdAtt As DataTable, ByRef dttOrdAss As DataTable) As Boolean
    Try
      Return MyBase.SalvaOrdineAggNetPro_CaricaOrDaMovmag(ds, dbConn, dttOrd, dttOrdImp, dttOrdAtt, dttOrdAss)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DeleteDocAggNetPro(ByRef ds As DataSet, ByVal strState As String, _
                                                 ByVal strNetProDB As String, ByRef dbConn As DbConnection) As Boolean
    Try
      Return MyBase.DeleteDocAggNetPro(ds, strState, strNetProDB, dbConn)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetStatusEXEOrdineNetpro(ByVal strDitta As String, ByVal strTipork As String, _
                                                      ByVal nAnno As Integer, ByVal strSerie As String, _
                                                      ByVal lNumord As Integer, ByVal nRiga As Integer) As Boolean
    Try
      Return MyBase.GetStatusEXEOrdineNetpro(strDitta, strTipork, nAnno, strSerie, lNumord, nRiga)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function NetProCallSocket(ByVal strDitta As String, ByVal strOperaz As String, ByVal strBatch As String) As Boolean
    Try
      Return MyBase.NetProCallSocket(strDitta, strOperaz, strBatch)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

End Class

