Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDORGNOR
  Inherits CLD__BASE


  Public Overridable Function CheckPropValuta(ByVal strDitta As String, ByVal strTipork As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 ol_stato FROM ordlist WHERE codditt = " & CStrSQL(strDitta) & " AND ol_stato <> 'T' AND ol_codvalu <> 0 AND ol_tipork = " & CStrSQL(strTipork)
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

  Public Overridable Function GetData(ByVal strDitta As String, ByVal strOrdlistTipork As String, _
                                      ByVal strWhereOutArtico As String, ByVal bModRA As Boolean, _
                                      ByVal strOrdlistProgressiviDaElab As String, _
                                      ByVal lConto As Integer, ByVal lCommeca As Integer, _
                                      ByVal nMagaz As Integer, _
                                      ByVal strDatordini As String, ByVal strDatordfin As String, _
                                      ByVal strDatconsini As String, ByVal strDatconsfin As String, _
                                      ByVal bGenerato As Boolean, ByVal bConfermato As Boolean, _
                                      ByVal bCongelato As Boolean, ByVal bEmRDA As Boolean, _
                                      ByVal bAppRDA As Boolean, ByVal bEmRDO As Boolean, _
                                      ByVal bRaggruppa As Boolean, ByVal bRaggrRigheComm As Boolean, _
                                      ByVal nCodStabilimento As Integer, ByRef dttOut As DataTable) As Boolean
    Try

      Return GetData(strDitta, strOrdlistTipork, strWhereOutArtico, bModRA, strOrdlistProgressiviDaElab, _
                     lConto, lCommeca, nMagaz, strDatordini, strDatordfin, strDatconsini, strDatconsfin, _
                     bGenerato, bConfermato, bCongelato, bEmRDA, bAppRDA, bEmRDO, bRaggruppa, bRaggrRigheComm, _
                     nCodStabilimento, dttOut, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetData(ByVal strDitta As String, ByVal strOrdlistTipork As String, _
                                      ByVal strWhereOutArtico As String, ByVal bModRA As Boolean, _
                                      ByVal strOrdlistProgressiviDaElab As String, _
                                      ByVal lConto As Integer, ByVal lCommeca As Integer, _
                                      ByVal nMagaz As Integer, _
                                      ByVal strDatordini As String, ByVal strDatordfin As String, _
                                      ByVal strDatconsini As String, ByVal strDatconsfin As String, _
                                      ByVal bGenerato As Boolean, ByVal bConfermato As Boolean, _
                                      ByVal bCongelato As Boolean, ByVal bEmRDA As Boolean, _
                                      ByVal bAppRDA As Boolean, ByVal bEmRDO As Boolean, _
                                      ByVal bRaggruppa As Boolean, ByVal bRaggrRigheComm As Boolean, _
                                      ByVal nCodStabilimento As Integer, ByRef dttOut As DataTable, _
                                      ByVal nSvalOpzione As Integer) As Boolean
    '-------------------
    'ottengo l'elenco dei documenti da generare
    'se nSvalOpzione = 1 devo raggruppare anche per cambio di ordlist
    Dim strSQL As String = ""
    Dim strWhere As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strOrdlistTipork, strWhereOutArtico, bModRA, _
                                             strOrdlistProgressiviDaElab, lConto, lCommeca, nMagaz, _
                                             strDatordini, strDatordfin, strDatconsini, strDatconsfin, _
                                             bGenerato, bConfermato, bCongelato, bEmRDA, bAppRDA, bEmRDO, _
                                             bRaggruppa, bRaggrRigheComm, nCodStabilimento, dttOut, nSvalOpzione})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(21), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      '------------------------
      'compongo la where
      If Not GetData_CreaWhere(strDitta, strOrdlistTipork, strWhereOutArtico, bModRA, _
                               strOrdlistProgressiviDaElab, lConto, lCommeca, nMagaz, _
                               strDatordini, strDatordfin, strDatconsini, strDatconsfin, _
                               bGenerato, bConfermato, bCongelato, bEmRDA, bAppRDA, bEmRDO, _
                               nCodStabilimento, strWhere) Then Return False

      '------------------------
      'compone la select
      If bRaggruppa = False Then
        If bModRA Then
          strSQL = "SELECT DISTINCT movrdo.rdo_progr As ol_progr, movrdo.rdo_conto As ol_conto, ol_magaz, ol_magaz2, ol_magimp, ol_commeca, ol_subcommeca," & _
            " CASE WHEN testrdo.to_valuta IS NULL THEN 0 ELSE testrdo.to_valuta END AS ol_codvalu, '' as ol_netpid"
        Else
          strSQL = "SELECT DISTINCT ol_progr, ol_conto, ol_magaz, ol_magaz2, ol_magimp, ol_commeca, ol_subcommeca, ol_codvalu, ol_netpid"
        End If
      Else
        If bRaggrRigheComm = False Then
          If bModRA Then
            strSQL = "SELECT DISTINCT movrdo.rdo_conto As ol_conto, ol_magaz, ol_magaz2, ol_magimp, ol_commeca, ol_subcommeca," & _
              " CASE WHEN testrdo.to_valuta IS NULL THEN 0 ELSE testrdo.to_valuta END AS ol_codvalu, '' as ol_netpid"
          Else
            strSQL = "SELECT DISTINCT ol_conto, ol_magaz, ol_magaz2, ol_magimp, ol_commeca, ol_subcommeca, ol_codvalu, ol_netpid"
          End If
        Else
          If bModRA Then
            strSQL = "SELECT DISTINCT movrdo.rdo_conto As ol_conto, ol_magaz, ol_magaz2, ol_magimp," & _
              " CASE WHEN testrdo.to_valuta IS NULL THEN 0 ELSE testrdo.to_valuta END AS ol_codvalu, '' as ol_netpid "
          Else
            strSQL = "SELECT DISTINCT ol_conto, ol_magaz, ol_magaz2, ol_magimp, ol_codvalu, ol_netpid "
          End If
        End If
      End If
      If bModRA Then
        strSQL += IIf(nSvalOpzione = 1, ", CASE WHEN testrdo.to_cambio IS NULL THEN 0.0 ELSE testrdo.to_cambio END AS ol_cambio ", ", 0.0 as ol_cambio ").ToString
      Else
        strSQL += IIf(nSvalOpzione = 1, ", ol_cambio ", ", 0.0 as ol_cambio ").ToString
      End If

      strSQL += " FROM " & strWhere

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      dttOut.TableName = "ORDLIST"

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetData_CreaWhere(ByVal strDitta As String, ByVal strOrdlistTipork As String, _
                                                ByVal strWhereOutArtico As String, ByVal bModRA As Boolean, _
                                                ByVal strOrdlistProgressiviDaElab As String, _
                                                ByVal lConto As Integer, ByVal lCommeca As Integer, _
                                                ByVal nMagaz As Integer, _
                                                ByVal strDatordini As String, ByVal strDatordfin As String, _
                                                ByVal strDatconsini As String, ByVal strDatconsfin As String, _
                                                ByVal bGenerato As Boolean, ByVal bConfermato As Boolean, _
                                                ByVal bCongelato As Boolean, ByVal bEmRDA As Boolean, _
                                                ByVal bAppRDA As Boolean, ByVal bEmRDO As Boolean, _
                                                ByVal nCodStabilimento As Integer, ByRef strWhereOut As String) As Boolean
    Dim strSQL As String = ""
    Try

      '-----------------------------
      'Se chiamato da programma esterno (BSORGSOL) crea la query di selezione in base
      'all'elenco dei progressivi passato
      If strOrdlistProgressiviDaElab <> "" Then
        strWhereOut = "ordlist" & _
          " WHERE ordlist.codditt = " & CStrSQL(strDitta) & _
          " AND ol_tipork = " & CStrSQL(strOrdlistTipork) & _
          " AND ol_progr IN (" & strOrdlistProgressiviDaElab & ")" & _
          " AND ol_conto <> 0"
        If nCodStabilimento <> 0 Then strWhereOut += " AND ordlist.ol_commen = " & nCodStabilimento
        Return True
      End If

      If bModRA Then
        strWhereOut = "movrdo INNER JOIN ordlist ON movrdo.codditt = ordlist.codditt" & _
                                              " AND movrdo.rdo_progr = ordlist.ol_progr" & _
                      " LEFT JOIN testrdo ON movrdo.codditt = testrdo.codditt" & _
                                       " AND movrdo.rdo_rdotipork = testrdo.to_rdotipork" & _
                                       " AND movrdo.rdo_rdotipork = testrdo.to_rdotipork" & _
                                       " AND movrdo.rdo_rdoanno = testrdo.to_rdoanno" & _
                                       " AND movrdo.rdo_rdoserie = testrdo.to_rdoserie" & _
                                       " AND movrdo.rdo_rdonum = testrdo.to_rdonum"
      Else
        strWhereOut = "ordlist "
      End If
      If strWhereOutArtico <> "" Then
        strWhereOut += " INNER JOIN artico ON ordlist.codditt = artico.codditt AND ordlist.ol_codart = artico.ar_codart "
      End If

      strWhereOut += " WHERE ordlist.codditt = " & CStrSQL(strDitta) & _
          " AND ordlist.ol_tipork = " & CStrSQL(strOrdlistTipork) & _
          " AND ordlist.ol_datord BETWEEN " & CDataSQL(strDatordini) & " AND " & CDataSQL(strDatordfin)
      If bModRA Then
        strWhereOut += " AND movrdo.rdo_datcons BETWEEN " & CDataSQL(strDatconsini) & " AND " & CDataSQL(strDatconsfin)
      Else
        strWhereOut += " AND ordlist.ol_datcons BETWEEN " & CDataSQL(strDatconsini) & " AND " & CDataSQL(strDatconsfin)
      End If

      If bModRA Then
        strWhereOut += " AND ((ol_offreq = 'S' and rdo_flappr = 'S') or (ol_offreq = 'N' and ol_stato = 'Q' )) "
      Else
        strWhereOut += " AND ol_conto <> 0"
      End If

      If strWhereOutArtico <> "" Then
        TraduciWhere(strWhereOutArtico, strWhereOut)
      End If

      If bModRA Then
        If lConto <> 0 Then strWhereOut += " AND movrdo.rdo_conto = " & lConto
      Else
        If lConto <> 0 Then strWhereOut += " AND ordlist.ol_conto = " & lConto.ToString
      End If

      If lCommeca <> 0 Then strWhereOut += " AND ordlist.ol_commeca = " & lCommeca.ToString
      If nMagaz <> 0 Then strWhereOut += " AND ordlist.ol_magaz = " & nMagaz.ToString

      If bModRA Then
        strWhereOut += " AND ((ol_offreq = 'S' and ol_stato = 'S') or (ol_offreq = 'N' and ol_stato = 'Q' )) "
      Else
        If bGenerato = False Then strWhereOut += " AND ordlist.ol_stato <> ' '"
        If bConfermato = False Then strWhereOut += " AND ordlist.ol_stato <> 'S'"
        If bCongelato = False Then strWhereOut += " AND ordlist.ol_stato <> 'F'"
        If bEmRDA = False Then strWhereOut += " AND ordlist.ol_stato <> 'P'"
        If bAppRDA = False Then strWhereOut += " AND ordlist.ol_stato <> 'Q'"
        If bEmRDO = False Then strWhereOut += " AND ordlist.ol_stato <> 'R'"
      End If
      If nCodStabilimento <> 0 Then strWhereOut += " AND ordlist.ol_commen = " & nCodStabilimento

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function EsisteOrdine(ByVal strDitta As String, ByVal strTipoDoc As String, _
                         ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer) As Boolean

    'restituisce true se l'ordine esiste, diversamente false
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim bResult As Boolean = False

    Try
      strSQL = "SELECT top 1 testord.td_numord FROM testord" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND td_tipork = " & CStrSQL(strTipoDoc) & _
               " AND td_anno = " & nAnno & _
               " AND td_serie = " & CStrSQL(strSerie) & _
               " AND td_numord = " & lNumdoc.ToString
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEST")
      If dsTmp.Tables("TEST").Rows.Count > 0 Then bResult = True
      dsTmp.Dispose()

      Return bResult

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetOrdlistPerDoc(ByVal strDitta As String, ByVal strOrdlistTipork As String, _
                                                ByVal strWhereOutArtico As String, ByVal bModRA As Boolean, _
                                                ByVal strOrdlistProgressiviDaElab As String, _
                                                ByVal lConto As Integer, ByVal lCommeca As Integer, _
                                                ByVal nMagaz As Integer, ByVal bRaggruppa As Boolean, _
                                                ByVal strDatordini As String, ByVal strDatordfin As String, _
                                                ByVal strDatconsini As String, ByVal strDatconsfin As String, _
                                                ByVal bGenerato As Boolean, ByVal bConfermato As Boolean, _
                                                ByVal bCongelato As Boolean, ByVal bEmRDA As Boolean, _
                                                ByVal bAppRDA As Boolean, ByVal bEmRDO As Boolean, _
                                                ByVal bRaggrRigheComm As Boolean, ByRef dtrOl As DataRow, _
                                                ByVal bOrdinaperprogrRDO As Boolean, ByRef dttOut As DataTable) As Boolean
    Try

      Return GetOrdlistPerDoc(strDitta, strOrdlistTipork, strWhereOutArtico, bModRA, strOrdlistProgressiviDaElab, _
                             lConto, lCommeca, nMagaz, bRaggruppa, strDatordini, strDatordfin, strDatconsini, strDatconsfin, _
                             bGenerato, bConfermato, bCongelato, bEmRDA, bAppRDA, bEmRDO, bRaggrRigheComm, dtrOl, _
                             bOrdinaperprogrRDO, dttOut, 3)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetOrdlistPerDoc(ByVal strDitta As String, ByVal strOrdlistTipork As String, _
                                                ByVal strWhereOutArtico As String, ByVal bModRA As Boolean, _
                                                ByVal strOrdlistProgressiviDaElab As String, _
                                                ByVal lConto As Integer, ByVal lCommeca As Integer, _
                                                ByVal nMagaz As Integer, ByVal bRaggruppa As Boolean, _
                                                ByVal strDatordini As String, ByVal strDatordfin As String, _
                                                ByVal strDatconsini As String, ByVal strDatconsfin As String, _
                                                ByVal bGenerato As Boolean, ByVal bConfermato As Boolean, _
                                                ByVal bCongelato As Boolean, ByVal bEmRDA As Boolean, _
                                                ByVal bAppRDA As Boolean, ByVal bEmRDO As Boolean, _
                                                ByVal bRaggrRigheComm As Boolean, ByRef dtrOl As DataRow, _
                                                ByVal bOrdinaperprogrRDO As Boolean, ByRef dttOut As DataTable, _
                                                ByVal nSvalOpzione As Integer) As Boolean
    '-------------------
    'ottengo le righe da inserire in un ordine
    Dim strSQL As String = ""
    Dim strW As String = ""
    Dim strW1 As String = ""
    Dim strW2 As String = ""
    Dim strOrder As String = ""
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strOrdlistTipork, strWhereOutArtico, bModRA, _
                                             strOrdlistProgressiviDaElab, lConto, lCommeca, nMagaz, bRaggruppa, _
                                             strDatordini, strDatordfin, strDatconsini, strDatconsfin, _
                                             bGenerato, bConfermato, bCongelato, bEmRDA, bAppRDA, bEmRDO, _
                                             bRaggrRigheComm, dtrOl, bOrdinaperprogrRDO, dttOut, nSvalOpzione})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dtrOl = CType(oIn(20), DataRow)
        dttOut = CType(oIn(22), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      '-------------------------------------------------------------------------------------
      '--- Adesso crea il recordset su ORDLIST (analitico) e per ciascuno
      '--- creerà poi un record in MOVORD
      '-------------------------------------------------------------------------------------
      If bRaggruppa = False Then
        'non raggruppa righe H
        If bModRA Then
          strW1 = " AND movrdo.codditt = " & CStrSQL(strDitta) & " AND movrdo.rdo_progr = " & dtrOl!ol_progr.ToString
        Else
          strW1 = " AND ordlist.codditt = " & CStrSQL(strDitta) & " AND ol_progr = " & dtrOl!ol_progr.ToString
        End If
      Else
        'raggruppa righe H
        If bRaggrRigheComm = False Then
          'non raggruppa righe di commesse diverse
          If bModRA Then
            strW1 = " AND movrdo.codditt = " & CStrSQL(strDitta) & _
              " AND movrdo.rdo_conto = " & dtrOl!ol_conto.ToString
          Else
            strW1 = " AND ordlist.codditt = " & CStrSQL(strDitta) & _
              " AND ol_conto = " & dtrOl!ol_conto.ToString
          End If
          strW1 += " AND ol_magaz = " & dtrOl!ol_magaz.ToString & _
            " AND ol_magaz2 = " & dtrOl!ol_magaz2.ToString & _
            " AND ol_magimp = " & dtrOl!ol_magimp.ToString & _
            " AND ol_commeca = " & dtrOl!ol_commeca.ToString & _
            " AND ol_subcommeca = " & CStrSQL(dtrOl!ol_subcommeca) & _
            " AND ol_codvalu = " & dtrOl!ol_codvalu.ToString
          If nSvalOpzione = 1 Then strW1 += " AND ol_cambio = " & CDblSQL(NTSCDec(dtrOl!ol_cambio))
        Else
          'raggruppa righe di commesse diverse
          If bModRA Then
            strW1 = " AND movrdo.codditt = " & CStrSQL(strDitta) & _
              " AND movrdo.rdo_conto = " & dtrOl!ol_conto.ToString
          Else
            strW1 = " AND ordlist.codditt = " & CStrSQL(strDitta) & _
              " AND ol_conto = " & dtrOl!ol_conto.ToString
          End If
          strW1 += " AND ol_magaz = " & dtrOl!ol_magaz.ToString & _
            " AND ol_magaz2 = " & dtrOl!ol_magaz2.ToString & _
            " AND ol_magimp = " & dtrOl!ol_magimp.ToString & _
            " AND ol_codvalu = " & dtrOl!ol_codvalu.ToString
          If nSvalOpzione = 1 Then strW1 += " AND ol_cambio = " & CDblSQL(NTSCDec(dtrOl!ol_cambio))
          If lCommeca <> 0 Then
            strW1 += " AND ol_commeca = " & lCommeca.ToString
          End If
        End If
      End If
      strW = " ol_tipork = " & CStrSQL(strOrdlistTipork) & _
        " AND ol_datord BETWEEN " & CDataSQL(strDatordini) & " AND " & CDataSQL(strDatordfin) & _
        " AND isnull(ol_netpid, '') = " & CStrSQL(NTSCStr(dtrOl!ol_netpid))
      If bModRA Then
        strW += " AND rdo_datcons BETWEEN " & CDataSQL(strDatconsini) & " AND " & CDataSQL(strDatconsfin) & _
          " AND  ((ol_offreq = 'S' and rdo_flappr = 'S') or (ol_offreq = 'N' and ol_stato = 'Q' )) "
      Else
        strW += " AND ol_datcons BETWEEN " & CDataSQL(strDatconsini) & " AND " & CDataSQL(strDatconsfin)
      End If
      If bModRA Then
        strW += " AND ((ol_offreq = 'S' and ol_stato = 'S') or (ol_offreq = 'N' and ol_stato = 'Q' )) "
      Else
        If bGenerato = False Then strW += " AND ordlist.ol_stato <> ' '"
        If bConfermato = False Then strW += " AND ordlist.ol_stato <> 'S'"
        If bCongelato = False Then strW += " AND ordlist.ol_stato <> 'F'"
        If bEmRDA = False Then strW += " AND ordlist.ol_stato <> 'P'"
        If bAppRDA = False Then strW += " AND ordlist.ol_stato <> 'Q'"
        If bEmRDO = False Then strW += " AND ordlist.ol_stato <> 'R'"
      End If
      If strWhereOutArtico = "" Then
        If bModRA Then
          strSQL = "SELECT ordlist.*, rdo_progr, rdo_conto, rdo_datcons, rdo_prezzo, rdo_prezvalc, rdo_scont1, rdo_scont2, rdo_scont3, rdo_scont4, rdo_scont5, rdo_scont6, rdo_note, rdo_flprznet" & _
                   " FROM movrdo INNER JOIN ordlist ON movrdo.codditt = ordlist.codditt AND movrdo.rdo_progr = ordlist.ol_progr"
        Else
          strSQL = "SELECT * FROM ordlist"
        End If
        strW2 = ""
      Else
        If bModRA Then
          strSQL = "SELECT ordlist.*, rdo_progr, rdo_conto, rdo_datcons, rdo_prezzo, rdo_prezvalc, rdo_scont1, rdo_scont2, rdo_scont3, rdo_scont4, rdo_scont5, rdo_scont6, rdo_note, rdo_flprznet" & _
                   " FROM (movrdo INNER JOIN ordlist ON movrdo.codditt = ordlist.codditt AND movrdo.rdo_progr = ordlist.ol_progr) INNER JOIN artico ON ordlist.codditt = artico.codditt AND ordlist.ol_codart = artico.ar_codart"
        Else
          strSQL = "SELECT * FROM ordlist INNER JOIN artico ON ordlist.codditt = artico.codditt AND ordlist.ol_codart = artico.ar_codart"
        End If

        If strWhereOutArtico <> "" Then
          TraduciWhere(strWhereOutArtico, strW2)
        End If

      End If
      If bModRA Then
        If Not bOrdinaperprogrRDO Then
          strOrder = " ORDER BY ol_codart, rdo_datcons"
        Else
          strOrder = " ORDER BY ol_progr, ol_codart, rdo_datcons"
        End If
      Else
        strOrder = " ORDER BY ol_codart, ol_datcons"
      End If
      strSQL += " WHERE " & strW & strW1 & strW2
      If strOrdlistProgressiviDaElab <> "" Then strSQL += " AND ol_progr IN (" & strOrdlistProgressiviDaElab & ")"
      strSQL += strOrder

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetOrdlistY(ByVal strDitta As String, ByVal lOlProgr As Integer, ByRef dttOutY As DataTable, _
                                          ByRef dttOutAtt As DataTable, ByRef dttOutAss As DataTable) As Boolean
    '------------------------------
    'per gli ordini di prod, leggo anche gli impegni, zzattivit e zzassris
    Dim strSQL As String = ""
    Dim nTcIndTaglF As Integer
    Dim nRiga As Integer
    Dim dttTmp As New DataTable
    Dim dtrRow As DataRow
    Dim dttDestagl As New DataTable
    Try
      strSQL = " SELECT ordlist.*, ar_codtagl as xxo_codtagl, 0 as xxo_tcindtaglf, ' ' as xxo_tctagliaf " & _
               " FROM ordlist INNER JOIN artico ON ordlist.codditt = artico.codditt AND ordlist.ol_codart = artico.ar_codart " & _
               " WHERE ordlist.codditt  =" & CStrSQL(strDitta) & " AND ol_olprogr = " & lOlProgr.ToString
      dttOutY = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      For Each dtrRow2 As DataRow In dttOutY.Rows

        strSQL = " SELECT * FROM ordlisttc" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND ol_progr = " & NTSCInt(dtrRow2!ol_progr)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          dtrRow = dttTmp.Rows(0)

          'xxo_tcindtaglf
          nTcIndTaglF = 0

          nRiga = NTSCInt(dtrRow!ol_progr)
          If NTSCDec(dtrRow!ol_quant01) <> 0 Then
            nTcIndTaglF = 1
          ElseIf NTSCDec(dtrRow!ol_quant02) <> 0 Then
            nTcIndTaglF = 2
          ElseIf NTSCDec(dtrRow!ol_quant03) <> 0 Then
            nTcIndTaglF = 3
          ElseIf NTSCDec(dtrRow!ol_quant04) <> 0 Then
            nTcIndTaglF = 4
          ElseIf NTSCDec(dtrRow!ol_quant05) <> 0 Then
            nTcIndTaglF = 5
          ElseIf NTSCDec(dtrRow!ol_quant06) <> 0 Then
            nTcIndTaglF = 6
          ElseIf NTSCDec(dtrRow!ol_quant07) <> 0 Then
            nTcIndTaglF = 7
          ElseIf NTSCDec(dtrRow!ol_quant08) <> 0 Then
            nTcIndTaglF = 8
          ElseIf NTSCDec(dtrRow!ol_quant09) <> 0 Then
            nTcIndTaglF = 9
          ElseIf NTSCDec(dtrRow!ol_quant10) <> 0 Then
            nTcIndTaglF = 10
          ElseIf NTSCDec(dtrRow!ol_quant11) <> 0 Then
            nTcIndTaglF = 11
          ElseIf NTSCDec(dtrRow!ol_quant12) <> 0 Then
            nTcIndTaglF = 12
          ElseIf NTSCDec(dtrRow!ol_quant13) <> 0 Then
            nTcIndTaglF = 13
          ElseIf NTSCDec(dtrRow!ol_quant14) <> 0 Then
            nTcIndTaglF = 14
          ElseIf NTSCDec(dtrRow!ol_quant15) <> 0 Then
            nTcIndTaglF = 15
          ElseIf NTSCDec(dtrRow!ol_quant16) <> 0 Then
            nTcIndTaglF = 16
          ElseIf NTSCDec(dtrRow!ol_quant17) <> 0 Then
            nTcIndTaglF = 17
          ElseIf NTSCDec(dtrRow!ol_quant18) <> 0 Then
            nTcIndTaglF = 18
          ElseIf NTSCDec(dtrRow!ol_quant19) <> 0 Then
            nTcIndTaglF = 19
          ElseIf NTSCDec(dtrRow!ol_quant20) <> 0 Then
            nTcIndTaglF = 20
          ElseIf NTSCDec(dtrRow!ol_quant21) <> 0 Then
            nTcIndTaglF = 21
          ElseIf NTSCDec(dtrRow!ol_quant22) <> 0 Then
            nTcIndTaglF = 22
          ElseIf NTSCDec(dtrRow!ol_quant23) <> 0 Then
            nTcIndTaglF = 23
          ElseIf NTSCDec(dtrRow!ol_quant24) <> 0 Then
            nTcIndTaglF = 24
          End If

          dtrRow2!xxo_tcindtaglf = NTSCInt(IIf(nTcIndTaglF = 0, dtrRow2!ol_tcindtagl, nTcIndTaglF))
          ValCodiceDb(NTSCInt(dtrRow2!xxo_codtagl).ToString, strDitta, "TABTAGL", "N", "", dttDestagl)
          dtrRow2!xxo_tctagliaf = NTSCStr(dttDestagl.Rows(0)("tb_dest" & dtrRow2!xxo_tcindtaglf.ToString.PadLeft(2, "0"c))).ToUpper.Trim
          dtrRow2.AcceptChanges()
        End If
      Next

      strSQL = "SELECT * FROM zzattivit WHERE codditt  =" & CStrSQL(strDitta) & " AND at_progr = " & lOlProgr.ToString
      dttOutAtt = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "SELECT * FROM zzassris WHERE codditt  =" & CStrSQL(strDitta) & " AND as_progr = " & lOlProgr.ToString
      dttOutAss = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetOrdlistTc(ByVal strDitta As String, ByVal lOlProgr As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM ordlisttc WHERE codditt  =" & CStrSQL(strDitta) & " AND ol_progr = " & lOlProgr.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function AggAttivitColonneSu(ByVal strDitta As String, ByRef dttTTOLTOMO As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim lIITToltomo As Integer = 0
    Try
      lIITToltomo = GetTblInstId("TTOLTOMO", False)
      ResetTblInstId("TTOLTOMO", False, lIITToltomo)

      For Each dtrT As DataRow In dttTTOLTOMO.Rows
        strSQL = "INSERT INTO TTOLTOMO (codditt, instid, tt_olprogr, tt_motipork," & _
                " tt_moanno, tt_moserie, tt_monumord, tt_moriga)" & _
                " VALUES (" & CStrSQL(strDitta) & ", " & lIITToltomo & ", " & NTSCInt(dtrT!ol_progr) & ", '" & NTSCStr(dtrT!ol_ortipork) & "', " & _
                NTSCInt(dtrT!ol_oranno) & ", " & CStrSQL(NTSCStr(dtrT!ol_orserie)) & ", " & NTSCInt(dtrT!ol_ornum) & ", " & NTSCInt(dtrT!ol_orriga) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      strSQL = "UPDATE attivit " & _
                " SET at_sunumord = tt_monumord, at_suriga = tt_moriga," & _
                " at_sutipork = tt_motipork," & _
                " at_suserie = tt_moserie," & _
                " at_suanno = tt_moanno," & _
                " at_suolprogr = 0" & _
                " FROM attivit INNER JOIN TTOLTOMO ON attivit.codditt = TTOLTOMO.codditt AND attivit.at_suolprogr = TTOLTOMO.tt_olprogr " & _
                " WHERE TTOLTOMO.codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITToltomo
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "UPDATE zzattivit " & _
                " SET at_sunumord = tt_monumord," & _
                " at_suriga = tt_moriga," & _
                " at_sutipork = tt_motipork," & _
                " at_suserie = tt_moserie," & _
                " at_suanno = tt_moanno," & _
                " at_suolprogr = 0" & _
                " FROM zzattivit INNER JOIN TTOLTOMO ON zzattivit.codditt = TTOLTOMO.codditt AND zzattivit.at_suolprogr = TTOLTOMO.tt_olprogr " & _
                " WHERE TTOLTOMO.codditt = " & CStrSQL(strDitta) & _
                " AND instid = " & lIITToltomo
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      ResetTblInstId("TTOLTOMO", False, lIITToltomo)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


End Class
