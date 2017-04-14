Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Public Class CLDXXLIST
  Inherits CLD__BASE

  Public Overridable Function GetData(ByVal strDitta As String, ByVal lConto As Integer, ByVal strCodart As String, _
    ByVal nFase As Integer, ByVal bNoTestCodvalu As Boolean, ByVal nCodvalu As Integer, ByVal bInpromo As Boolean, _
    ByVal bNoTestValidoil As Boolean, ByVal strValidoil As String, ByVal strLctipo As String, _
    ByRef dsList As DataSet, ByVal bVisUltcost As Boolean, ByVal bVisUltcostoneri As Boolean, _
    ByVal bVisCostomedio As Boolean, ByVal bVisspeciale As Boolean, ByVal strListiniAbilitati As String, _
    ByVal strListiniDaVisualizzare As String, ByVal bEscludiCosti As Boolean, _
    ByVal bVisTuttiListiniSpeciali As Boolean, ByVal bVisCodarfo As Boolean) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return GetData(strDitta, lConto, strCodart, nFase, bNoTestCodvalu, nCodvalu, bInpromo, bNoTestValidoil, _
        strValidoil, strLctipo, dsList, bVisUltcost, bVisUltcostoneri, bVisCostomedio, bVisspeciale, _
        strListiniAbilitati, strListiniDaVisualizzare, bEscludiCosti, bVisTuttiListiniSpeciali, bVisCodarfo, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function GetData(ByVal strDitta As String, ByVal lConto As Integer, _
                                      ByVal strCodart As String, ByVal nFase As Integer, ByVal bNoTestCodvalu As Boolean, _
                                      ByVal nCodvalu As Integer, ByVal bInpromo As Boolean, _
                                      ByVal bNoTestValidoil As Boolean, ByVal strValidoil As String, _
                                      ByVal strLctipo As String, ByRef dsList As DataSet, _
                                      ByVal bVisUltcost As Boolean, ByVal bVisUltcostoneri As Boolean, _
                                      ByVal bVisCostomedio As Boolean, ByVal bVisspeciale As Boolean, _
                                      ByVal strListiniAbilitati As String, ByVal strListiniDaVisualizzare As String, _
                                      ByVal bEscludiCosti As Boolean, ByVal bVisTuttiListiniSpeciali As Boolean, _
                                      ByVal bVisCodarfo As Boolean, ByVal bLeggiCodarfoSingolarmente As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim strCodartVar As String = ""
    Dim dUltcost As Decimal = 0
    Dim dUltcostacc As Decimal = 0
    Dim dCostomedio As Decimal = 0
    Dim lPerqta As Integer = 0
    Dim lForn1 As Integer = 0
    Dim lForn2 As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, lConto, strCodart, nFase, bNoTestCodvalu, nCodvalu, _
        bInpromo, bNoTestValidoil, strValidoil, strLctipo, dsList, bVisUltcost, bVisUltcostoneri, _
        bVisCostomedio, bVisspeciale, strListiniAbilitati, strListiniDaVisualizzare, bEscludiCosti, _
        bVisTuttiListiniSpeciali, bVisCodarfo, bLeggiCodarfoSingolarmente})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsList = CType(oIn(10), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------

      'per articoli a variante devo cercare il listino per codroot o per codroot + cod prima variante
      If strCodart.Trim <> "" Then
        strCodartVar = strCodart
        strSQL = "SELECT ar_codart, ar_codroot, ar_codvar1, ar_gesvar, ar_prevar, ar_forn, ar_forn2, ar_ultfase, ar_perqta FROM artico " & _
                 "WHERE codditt = " & CStrSQL(strDitta) & " AND ar_codart = " & CStrSQL(strCodart)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          If nFase < 0 Then nFase = NTSCInt(dttTmp.Rows(0)!ar_ultfase)
          lPerqta = NTSCInt(dttTmp.Rows(0)!ar_perqta)
          lForn1 = NTSCInt(dttTmp.Rows(0)!ar_forn)
          lForn2 = NTSCInt(dttTmp.Rows(0)!ar_forn2)
          If NTSCStr(dttTmp.Rows(0)!ar_gesvar) = "S" Then
            Select Case NTSCStr(dttTmp.Rows(0)!ar_prevar)
              Case "N" : strCodartVar = NTSCStr(dttTmp.Rows(0)!ar_codroot)
              Case "S" : strCodartVar = NTSCStr(dttTmp.Rows(0)!ar_codart)
              Case "1" : strCodartVar = NTSCStr(dttTmp.Rows(0)!ar_codroot) & NTSCStr(dttTmp.Rows(0)!ar_codvar1)
            End Select
            If strCodartVar = "" Then strCodartVar = "^§ç°" 'se ho passato il padre della variante non devo restituire nulla (devo passare l'articolo effettivo)
          End If
        End If
        dttTmp.Clear()
        If nFase < 0 Then nFase = 0

        If lForn1 = 0 And lForn2 = 0 Then bVisTuttiListiniSpeciali = False
      End If

      strSQL = "SELECT listini.*, an_descr1 as xx_conto, dd_nomdest as xx_coddest, ar_descr as xx_codart, ar_desint as xx_desint," & _
        " CASE WHEN lc_codlavo <> 0 THEN tb_deslavo ELSE 'Acquisto/Cessione' END as xx_codlavo," & _
        " tb_desvalu as xx_codvalu, tb_destpro as xx_codtpro," & _
        " af_descr as xx_fase, tb_deslist as xx_listino," & _
        IIf((bVisCodarfo = True) And (bLeggiCodarfoSingolarmente = False), " caf_codarfo", " ''").ToString & " AS xx_codarfo" & _
        " FROM listini LEFT JOIN anagra ON listini.codditt = anagra.codditt AND listini.lc_conto = anagra.an_conto" & _
        " LEFT JOIN destdiv ON listini.codditt = destdiv.codditt AND listini.lc_conto = destdiv.dd_conto AND listini.lc_coddest = destdiv.dd_coddest" & _
        " LEFT JOIN artico ON listini.codditt = artico.codditt AND listini.lc_codart = artico.ar_codart" & _
        " LEFT JOIN tabvalu ON listini.lc_codvalu = tabvalu.tb_codvalu" & _
        " LEFT JOIN tabtpro ON listini.codditt = tabtpro.codditt AND listini.lc_codtpro = tabtpro.tb_codtpro" & _
        " LEFT JOIN tablist ON listini.codditt = tablist.codditt AND listini.lc_listino = tablist.tb_codlist" & _
        " LEFT JOIN tablavo ON listini.codditt = tablavo.codditt AND listini.lc_codlavo = tablavo.tb_codlavo" & _
        " LEFT JOIN artfasi ON listini.codditt = artfasi.codditt AND listini.lc_codart = artfasi.af_codart AND listini.lc_fase = artfasi.af_fase"
      If (bVisCodarfo = True) And (bLeggiCodarfoSingolarmente = False) Then
        strSQL += " LEFT JOIN codarfo ON listini.codditt = codarfo.codditt AND listini.lc_conto = codarfo.caf_conto AND listini.lc_codart = codarfo.caf_codart"
      End If
      strSQL += " WHERE listini.codditt = " & CStrSQL(strDitta) & _
        " AND lc_codcas = ' ' "

      'se non sono stato chiamato da bsvebolx / bsmghlar applico il filtro cli/forn normalmente (vedi bn__clie)
      If bVisTuttiListiniSpeciali = False And bVisspeciale = False And strListiniDaVisualizzare.Trim = "" Then
        strSQL += " AND lc_tipo = " & CStrSQL(strLctipo)
        If lConto <> 0 Then strSQL += "AND lc_conto = " & lConto.ToString
      End If

      If strCodartVar.Trim <> "" Then strSQL += "AND lc_codart = " & CStrSQL(strCodartVar)
      If bNoTestCodvalu = False Then
        If nCodvalu = 0 Then
          strSQL += " AND lc_codvalu <> 0"
        Else
          strSQL += " AND lc_codvalu = " & nCodvalu.ToString
        End If
      Else
        strSQL += " AND lc_codvalu = 0"
      End If
      If bNoTestValidoil = False Then
        strSQL += " AND lc_datagg <= " & CDataSQL(strValidoil) & " AND lc_datscad >= " & CDataSQL(strValidoil)
      End If
      If bInpromo Then
        strSQL += " AND lc_codtpro > 0 "
      Else
        strSQL += " AND lc_codtpro = 0 "
      End If

      '------------------------------------
      'da bsmgarti e solo per i listini generici
      If strLctipo = " " AndAlso strListiniAbilitati.Trim <> "" Then strSQL += " AND lc_listino IN (" & strListiniAbilitati & ")"

      '------------------------------------
      'carico listini speciali (bsvebolx, bsmghlar)
      'ed eventuali altri filtri su articoli
      If bVisTuttiListiniSpeciali Or bVisspeciale Or strListiniDaVisualizzare.Trim <> "" Then
        strSQL += " AND ("

        If strListiniDaVisualizzare.Trim <> "" Then
          strSQL += " lc_listino IN (" & strListiniDaVisualizzare & ")"
        Else
          strSQL += " lc_listino > 0 "
        End If
        If bVisTuttiListiniSpeciali Or bVisspeciale Then strSQL += " OR "

        If bVisTuttiListiniSpeciali Or bVisspeciale Then
          If bVisTuttiListiniSpeciali Then
            strSQL += " (lc_listino = 0 AND lc_conto <> 0)"
          Else
            strSQL += " (lc_listino = 0 AND lc_conto IN (" & lConto.ToString
            If bEscludiCosti = False Then
              If lForn1 <> 0 Then strSQL += ", " & lForn1.ToString
              If lForn2 <> 0 Then strSQL += ", " & lForn2.ToString
            End If
            strSQL += ")) "
          End If
        End If

        strSQL += ") "
      End If    'If bVisTuttiListiniSpeciali Or bVisspeciale Or strListiniDaVisualizzare.Trim <> "" Then

      strSQL += " ORDER BY lc_codart, lc_codlavo, lc_conto, lc_coddest," & _
                " lc_listino, lc_codvalu, lc_codtpro, lc_datagg DESC, lc_daquant"

      dsList = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

      '------------------------------------
      'carico codarfo
      If (bVisCodarfo = True) And (bLeggiCodarfoSingolarmente = True) Then
        For i = 0 To dsList.Tables("LISTINI").Rows.Count - 1
          If NTSCInt(dsList.Tables("LISTINI").Rows(i)!lc_conto) <> 0 Then
            dsList.Tables("LISTINI").Rows(i)!xx_codarfo = GetCodarfo(strDitta, CStrSQL(dsList.Tables("LISTINI").Rows(i)!lc_codart), NTSCInt(dsList.Tables("LISTINI").Rows(i)!lc_conto))
          End If
        Next
      End If

      '------------------------------------
      'carico il listino speciale (bsvebolx, bsmghlar)
      If bEscludiCosti = False Then
        If bVisUltcost Or bVisUltcostoneri Or bVisCostomedio Then
          strSQL = "SELECT apx_ultcos, apx_peucos, apx_qtalif, apx_vqtalif, apx_giaini, apx_vgiaini FROM artprox" & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND apx_codart = " & CStrSQL(strCodartVar) & _
                   " AND apx_fase = " & nFase.ToString
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then
            dUltcost = NTSCDec(dttTmp.Rows(0)!apx_ultcos)
            dUltcostacc = NTSCDec(dttTmp.Rows(0)!apx_peucos)

            If NTSCDec(dttTmp.Rows(0)!apx_qtalif) <> 0 Then
              dCostomedio = ((NTSCDec(dttTmp.Rows(0)!apx_vqtalif) / NTSCDec(dttTmp.Rows(0)!apx_qtalif)) * lPerqta)
            Else
              If NTSCDec(dttTmp.Rows(0)!apx_giaini) <> 0 Then
                dCostomedio = ((NTSCDec(dttTmp.Rows(0)!apx_vgiaini) / NTSCDec(dttTmp.Rows(0)!apx_giaini)) * lPerqta)
              End If
            End If
          End If
          dttTmp.Clear()
        End If

        'ULTIMO COSTO 
        If bVisUltcost Then
          dsList.Tables("LISTINI").Rows.Add(dsList.Tables("LISTINI").NewRow())
          With dsList.Tables("LISTINI").Rows(dsList.Tables("LISTINI").Rows.Count - 1)
            !codditt = strDitta
            !lc_codart = strCodartVar
            !lc_listino = 0
            !lc_prezzo = dUltcost
            !xx_listino = "Ultimo costo"
            !xx_fase = nFase
          End With
        End If
        'ULTIMO COSTO CON ONERI ACCESSORI
        If bVisUltcostoneri Then
          dsList.Tables("LISTINI").Rows.Add(dsList.Tables("LISTINI").NewRow())
          With dsList.Tables("LISTINI").Rows(dsList.Tables("LISTINI").Rows.Count - 1)
            !codditt = strDitta
            !lc_codart = strCodartVar
            !lc_listino = -1
            !lc_prezzo = dUltcostacc
            !xx_listino = "Ultimo costo + oneri"
            !xx_fase = nFase
          End With
        End If
        'COSTO MEDIO
        If bVisCostomedio Then
          dsList.Tables("LISTINI").Rows.Add(dsList.Tables("LISTINI").NewRow())
          With dsList.Tables("LISTINI").Rows(dsList.Tables("LISTINI").Rows.Count - 1)
            !codditt = strDitta
            !lc_codart = strCodartVar
            !lc_listino = -2
            !lc_prezzo = dCostomedio
            !xx_listino = "Costo medio"
            !xx_fase = nFase
          End With
        End If
        dsList.Tables("LISTINI").AcceptChanges()
      End If    'If bEscludiCosti = False Then

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetCodarfo(ByVal strDitta As String, ByVal strCodart As String, ByVal lConto As Integer) As String
    Dim strSQL As String = ""
    Dim strOut As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT TOP 1 caf_codarfo FROM codarfo" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND caf_conto = " & lConto.ToString & _
               " AND caf_codart = " & CStrSQL(strCodart) & _
               " ORDER BY codditt, caf_conto, caf_codart"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then strOut = NTSCStr(dttTmp.Rows(0)!caf_codarfo)

      Return strOut

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return ""
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function CheckListinoEsistente(ByVal strDitta As String, ByRef dtrIn As DataRow) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try

      strSQL = "SELECT TOP 1 lc_listino FROM listini" & _
                " WHERE lc_progr <> " & NTSCInt(dtrIn!lc_progr).ToString & _
                " AND codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart = " & CStrSQL(dtrIn!lc_codart.ToString) & _
                " AND lc_codlavo = " & dtrIn!lc_codlavo.ToString & _
                " AND lc_conto = " & dtrIn!lc_conto.ToString & _
                " AND lc_coddest = " & dtrIn!lc_coddest.ToString & _
                " AND lc_listino = " & dtrIn!lc_listino.ToString & _
                " AND lc_codvalu = " & dtrIn!lc_codvalu.ToString & _
                " AND lc_codtpro = " & dtrIn!lc_codtpro.ToString & _
                " AND lc_datagg = " & CDataSQL(NTSCDate(dtrIn!lc_datagg)) & _
                " AND lc_daquant = " & CDblSQL(NTSCDec(dtrIn!lc_daquant)) & _
                " AND lc_unmis = " & CStrSQL(dtrIn!lc_unmis.ToString) & _
                " AND lc_fase = " & dtrIn!lc_fase.ToString & _
                " AND lc_codcas = ' '"
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
  Public Overridable Function TrovaListinoEsistente(ByVal strDitta As String, ByRef dtrIn As DataRow, _
    ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 * FROM listini" & _
               " WHERE lc_progr <> " & NTSCInt(dtrIn!lc_progr).ToString & _
               " AND codditt = " & CStrSQL(strDitta) & _
               " AND lc_codart = " & CStrSQL(dtrIn!lc_codart.ToString) & _
               " AND lc_codlavo = " & dtrIn!lc_codlavo.ToString & _
               " AND lc_conto = " & dtrIn!lc_conto.ToString & _
               " AND lc_coddest = " & dtrIn!lc_coddest.ToString & _
               " AND lc_listino = " & dtrIn!lc_listino.ToString & _
               " AND lc_codvalu = " & dtrIn!lc_codvalu.ToString & _
               " AND lc_codtpro = " & dtrIn!lc_codtpro.ToString & _
               " AND lc_daquant = " & CDblSQL(NTSCDec(dtrIn!lc_daquant)) & _
               " AND lc_unmis = " & CStrSQL(dtrIn!lc_unmis.ToString) & _
               " AND lc_fase = " & dtrIn!lc_fase.ToString & _
               " AND lc_codcas = ' '" & _
               " AND (lc_datagg BETWEEN " & CDataSQL(NTSCDate(dtrIn!lc_datagg)) & " AND " & CDataSQL(NTSCDate(dtrIn!lc_datscad)) & _
               "  OR lc_datscad BETWEEN " & CDataSQL(NTSCDate(dtrIn!lc_datagg)) & " AND " & CDataSQL(NTSCDate(dtrIn!lc_datscad)) & ") "
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function Salva(ByVal strDitta As String, ByRef dttTable As DataTable, _
                                    ByVal strActLogProg As String) As Boolean
    Dim strSQL As String = ""
    Dim bInDelete As Boolean = False
    Dim dtrChange() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strSQLVal As String = ""

    Dim strSqlWhere As String = ""
    Dim strDesogglog As String = ""
    Dim dttTmp As New DataTable
    Dim dtrT() As DataRow = Nothing

    Try

      '-----------------------------
      'delete
      bInDelete = True
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Deleted)
      For i = 0 To dtrChange.Length - 1

        strSqlWhere = CStrSQL(strDitta) & ", " & NTSCInt(CDblSQL(dtrChange(i)("lc_progr", DataRowVersion.Original).ToString()))
        strDesogglog = "Cancellazione dati relativi al listino con progressivo '" & NTSCInt(dtrChange(i)("lc_progr", DataRowVersion.Original).ToString()) & "'"
        If Not ScriviActLog(strDitta, strActLogProg, "listini", "listini", strSqlWhere, "A", "D", strDesogglog, False) Then Return False

        strSQL = "DELETE FROM listini WHERE codditt = " & CStrSQL(strDitta) & " AND lc_progr = " & NTSCInt(dtrChange(i)("lc_progr", DataRowVersion.Original).ToString())
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        If NTSCInt(dtrChange(i)("lc_conto", DataRowVersion.Original)) <> 0 Then UpdateAnagraUltagg(strDitta, NTSCInt(dtrChange(i)("lc_conto", DataRowVersion.Original)))

        dtrChange(i).AcceptChanges()
      Next
      bInDelete = False

      '-----------------------------
      'insert: devo farmi ritornare lc_progr, 
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.Added)
      strSQL = "INSERT INTO listini " & GetQueryInsertField(dttTable, "lc_")
      For i = 0 To dtrChange.Length - 1
        strSQLVal = GetQueryInsertValue(dttTable, dtrChange(i), "lc_")
        Execute(strSQL & " VALUES " & strSQLVal, CLE__APP.DBTIPO.DBAZI)

        If NTSCInt(dtrChange(i)!lc_conto) <> 0 Then UpdateAnagraUltagg(strDitta, NTSCInt(dtrChange(i)!lc_conto))

        'mi faccio restituire lc_progr
        strSQL = "SELECT lc_progr FROM listini " & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart = " & CStrSQL(dtrChange(i)!lc_codart) & _
                " AND lc_conto = " & dtrChange(i)!lc_conto.ToString & _
                " AND lc_coddest = " & dtrChange(i)!lc_coddest.ToString & _
                " AND lc_codlavo = " & dtrChange(i)!lc_codlavo.ToString & _
                " AND lc_codvalu = " & dtrChange(i)!lc_codvalu.ToString & _
                " AND lc_listino = " & dtrChange(i)!lc_listino.ToString & _
                " AND lc_codtpro = " & dtrChange(i)!lc_codtpro.ToString & _
                " AND lc_datagg = " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                " AND lc_daquant = " & CDblSQL(dtrChange(i)!lc_daquant.ToString) & _
                " AND lc_unmis = " & CStrSQL(dtrChange(i)!lc_unmis.ToString) & _
                " AND lc_fase = " & dtrChange(i)!lc_fase.ToString & _
                " AND lc_codcas = ' '"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        dtrChange(i)!lc_progr = NTSCInt(dttTmp.Rows(0)!lc_progr)
        dttTmp.Clear()
        dtrChange(i).AcceptChanges()

        'Aggiorna la data di scadenza dell'eventuale listino immediatamente precedente
        strSQL = "UPDATE listini" & _
                " SET lc_datscad = " & CDataSQL(DateAdd("d", -1, dtrChange(i)!lc_datagg)) & "," & _
                " lc_ultagg = " & CDataOraSQL(DateTime.Now) & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart = " & CStrSQL(dtrChange(i)!lc_codart) & _
                " AND lc_conto = " & dtrChange(i)!lc_conto.ToString & _
                " AND lc_coddest = " & dtrChange(i)!lc_coddest.ToString & _
                " AND lc_codlavo = " & dtrChange(i)!lc_codlavo.ToString & _
                " AND lc_codvalu = " & dtrChange(i)!lc_codvalu.ToString & _
                " AND lc_listino = " & dtrChange(i)!lc_listino.ToString & _
                " AND lc_codtpro = " & dtrChange(i)!lc_codtpro.ToString & _
                " AND lc_datagg < " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                " AND lc_daquant = " & CDblSQL(dtrChange(i)!lc_daquant.ToString) & _
                " AND lc_datscad >= " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                " AND lc_unmis = " & CStrSQL(dtrChange(i)!lc_unmis.ToString) & _
                " AND lc_fase = " & dtrChange(i)!lc_fase.ToString & _
                " AND lc_codcas = ' '"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        dtrT = dttTable.Select("codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart = " & CStrSQL(dtrChange(i)!lc_codart) & _
                " AND lc_conto = " & dtrChange(i)!lc_conto.ToString & _
                " AND lc_coddest = " & dtrChange(i)!lc_coddest.ToString & _
                " AND lc_codlavo = " & dtrChange(i)!lc_codlavo.ToString & _
                " AND lc_codvalu = " & dtrChange(i)!lc_codvalu.ToString & _
                " AND lc_listino = " & dtrChange(i)!lc_listino.ToString & _
                " AND lc_codtpro = " & dtrChange(i)!lc_codtpro.ToString & _
                " AND lc_datagg < " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                " AND lc_daquant = " & CDblSQL(dtrChange(i)!lc_daquant.ToString) & _
                " AND lc_datscad >= " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                " AND lc_unmis = " & CStrSQL(dtrChange(i)!lc_unmis.ToString) & _
                " AND lc_fase = " & dtrChange(i)!lc_fase.ToString & _
                " AND lc_codcas = ' '")
        For n As Integer = 0 To dtrT.Length - 1
          dtrT(n)!lc_datscad = DateAdd("d", -1, dtrChange(i)!lc_datagg)
          dtrT(n)!lc_ultagg = DateTime.Now
          dtrT(n).AcceptChanges()
        Next

        'Aggiorna la data di scadenza del record appena inserito se esiste un record con data di aggiornamento posteriore
        strSQL = "SELECT TOP 1 lc_datagg FROM listini" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart = " & CStrSQL(dtrChange(i)!lc_codart) & _
                " AND lc_conto = " & dtrChange(i)!lc_conto.ToString & _
                " AND lc_coddest = " & dtrChange(i)!lc_coddest.ToString & _
                " AND lc_codlavo = " & dtrChange(i)!lc_codlavo.ToString & _
                " AND lc_codvalu = " & dtrChange(i)!lc_codvalu.ToString & _
                " AND lc_listino = " & dtrChange(i)!lc_listino.ToString & _
                " AND lc_codtpro = " & dtrChange(i)!lc_codtpro.ToString & _
                " AND lc_datagg > " & CDataSQL(NTSCDate(dtrChange(i)!lc_datagg)) & _
                " AND lc_daquant = " & CDblSQL(dtrChange(i)!lc_daquant.ToString) & _
                " AND lc_unmis = " & CStrSQL(dtrChange(i)!lc_unmis.ToString) & _
                " AND lc_fase = " & dtrChange(i)!lc_fase.ToString & _
                " AND lc_codcas = ' '" & _
                " ORDER BY codditt, lc_codart, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino," & _
                " lc_datagg, lc_daquant, lc_unmis, lc_fase"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          strSQL = "UPDATE listini" & _
                  " SET lc_datscad = " & CDataSQL(DateAdd("d", -1, dttTmp.Rows(i)!lc_datagg)) & "," & _
                  " lc_ultagg = " & CDataOraSQL(DateTime.Now) & _
                  " WHERE codditt = " & CStrSQL(strDitta) & " AND lc_progr = " & NTSCInt(dtrChange(i)!lc_progr).ToString
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          dtrChange(i)!lc_datscad = DateAdd("d", -1, dttTmp.Rows(i)!lc_datagg)
          dtrChange(i)!lc_ultagg = DateTime.Now
          dtrChange(i).AcceptChanges()
        End If
        dttTmp.Clear()
      Next

      '-----------------------------
      'update
      dtrChange = dttTable.Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      For i = 0 To dtrChange.Length - 1

        strSqlWhere = CStrSQL(strDitta) & ", " & NTSCInt(dtrChange(i)!lc_progr).ToString
        strDesogglog = "Modifica dati relativi al listino con progressivo '" & NTSCInt(dtrChange(i)!lc_progr).ToString & "'"
        If Not ScriviActLog(strDitta, strActLogProg, "listini", "listini", strSqlWhere, "M", "D", strDesogglog, False) Then Return False

        strSQL = "UPDATE listini SET " & GetQueryUpdate(dttTable, dtrChange(i), "lc_")
        strSQL += " WHERE codditt = " & CStrSQL(strDitta) & " AND lc_progr = " & NTSCInt(dtrChange(i)!lc_progr).ToString
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

        If NTSCInt(dtrChange(i)!lc_conto) <> 0 Then UpdateAnagraUltagg(strDitta, NTSCInt(dtrChange(i)!lc_conto))
        dtrChange(i).AcceptChanges()
      Next

      Return True

    Catch ex As Exception
      Try
        'se in fase di cancellaz di una riga si verifica un errore ripristino la riga corrente
        If bInDelete Then dtrChange(i).RejectChanges()
      Catch
      End Try
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function UpdateAnagraUltagg(ByVal strDitta As String, ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      'La aggiornava solo su Anagra e non su artico o altre eventuali tabelle, o si fa per tutte o per nessuno e al momento si è deciso per non farlo mai.
      'Il problema capitava quando in anagrafica clienti si modificavano i listini\sconti\provvigioni, dopo di che non era + possibile fare altre modifiche
      'alla stessa anagrafica e salvare, visto che avvisa che l'anagrafica è stata cambiata da un altro utente (a causa dell'aggiornamneto della data
      'di ultimo aggiornamento)
      'strSQL = "UPDATE anagra SET an_ultagg = GETDATE() " & _
      '          "WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & lConto.ToString
      'Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
End Class
