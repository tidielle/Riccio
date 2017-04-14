#Region "Imports"
Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO
#End Region

Public Class CLDMGELCT
  Inherits CLD__BASE

  Public Overridable Function LeggiTabella(ByVal strDitta As String, ByVal strNomeTabella As String, _
    ByRef dsOut As System.Data.DataSet, ByVal nConto As Integer, ByVal nMarca As Integer, _
    ByVal dateDataAgg As Date) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      LeggiTabella(strDitta, strNomeTabella, dsOut, nConto, nMarca, dateDataAgg, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function LeggiTabella(ByVal strDitta As String, ByVal strNomeTabella As String, _
    ByRef dsOut As System.Data.DataSet, ByVal nConto As Integer, ByVal nMarca As Integer, _
    ByVal dateDataAgg As Date, ByVal bMarca As Boolean) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strNomeTabella, dsOut, nConto, nMarca, dateDataAgg, bMarca})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsOut = CType(oIn(2), DataSet)
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM artest" & _
        " WHERE ae_codart IS NOT NULL" & _
        " AND ae_codart <> ''" & _
        " AND ae_forn = " & nConto & _
        IIf(bMarca = True, " AND ae_codmarc = " & nMarca, "").ToString & _
        " AND ae_ultagg >= " & CDataSQL(dateDataAgg) & _
        " ORDER BY codditt, ae_codartf"
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strNomeTabella)
      '--------------------------------------------------------------------------------------------------------------
      If dsOut.Tables(strNomeTabella).Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function Elaborazione(ByVal strDitta As String, ByRef dtrIn As DataRow, _
                                           ByVal bDescrizione As Boolean, ByVal bClasseSconto As Boolean, _
                                           ByVal bStatus As Boolean, ByVal bGruppo As Boolean, _
                                           ByVal bFamiglia As Boolean, ByVal bNomenclatura As Boolean, _
                                           ByVal bNote1 As Boolean, ByVal bRrfence As Boolean, _
                                           ByVal bPesi As Boolean, ByVal bLotto As Boolean, _
                                           ByVal bBarcode As Boolean, ByRef strErrore As String, ByVal nInstid As Integer) As Boolean

    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dttTmp As New DataTable

    Try

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      'verifica di artico
      If Not ValCodiceDb(NTSCStr(dtrIn!ae_codart), strDitta, "ARTICO", "S", "", dttTmp) Then
        AnnullaTrans()
        Return True
      End If

      'aggiornamento artico
      strSQL = "UPDATE artico SET"

      If bDescrizione Then
        strSQL &= " ar_descr = " & CStrSQL(dtrIn!ae_descr) & "," & _
                  " ar_desint = " & CStrSQL(dtrIn!ae_desint) & ","
      End If
      If bClasseSconto Then strSQL &= " ar_clascon = " & NTSCInt(dtrIn!ae_clascon) & ","

      If bStatus Then  'setta i campi nuovi senza resettare quelli vecchi
        Select Case NTSCStr(dtrIn!ae_status)
          Case "D"
            strSQL &= " ar_stalist = 'N'," & _
                      " ar_stainv = 'N'," & _
                      " ar_stasche = 'N'," & _
                      " ar_consmrp = 'N',"
          Case "E"
            strSQL &= " ar_inesaur = 'S',"
          Case "S"
            strSQL &= " ar_stalist = 'N',"
          Case "V"
            strSQL &= " ar_stalist = 'N'," & _
                      " ar_blocco = 'S',"
        End Select
      End If
      If bGruppo Then
        strSQL &= " ar_gruppo = " & NTSCInt(dtrIn!ae_gruppo) & "," & _
                  " ar_sotgru = " & NTSCInt(dtrIn!ae_sotgru) & ","
      End If
      If bFamiglia Then strSQL &= " ar_famprod = " & CStrSQL(dtrIn!ae_famprod) & ","
      If bNomenclatura Then strSQL &= " ar_codnomc = " & CStrSQL(dtrIn!ae_codnomc) & ","
      If bNote1 Then strSQL &= " ar_note = " & CStrSQL(dtrIn!ae_note) & ","
      If bRrfence Then strSQL &= " ar_rrfence = " & NTSCInt(dtrIn!ae_rrfence) & ","
      If bPesi Then
        strSQL &= " ar_pesolor = " & CDblSQL(NTSCDec(dtrIn!ae_pesolor)) & "," & _
                  " ar_pesonet = " & CDblSQL(NTSCDec(dtrIn!ae_pesonet)) & "," & _
                  " ar_volume = " & CDblSQL(NTSCDec(dtrIn!ae_volume)) & ","
      End If
      If bLotto Then
        strSQL &= " ar_minord = " & CDblSQL(NTSCDec(dtrIn!ae_minordin)) & "," & _
                  " ar_sublotto = " & CDblSQL(NTSCDec(dtrIn!ae_sublotto)) & ","
      End If

      strSQL &= " ar_ultagg = " & CDataSQL(NTSCStr(dtrIn!ae_ultagg)) & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND ar_codart = " & CStrSQL(dtrIn!ae_codart)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      If bDescrizione Then

        'inserimento/aggiornamento artval ling1
        If (Trim(NTSCStr(dtrIn!ae_descrl1)) <> "") Or (Trim(NTSCStr(dtrIn!ae_desintl1)) <> "") Then

          strSQL = "SELECT * FROM artval WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND ax_codart = " & CStrSQL(dtrIn!ae_codart) & " AND ax_codvalu = 1"

          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count > 0 Then
            strSQL = "UPDATE artval SET codditt = " & CStrSQL(strDitta) & "," & _
            " ax_codart = " & CStrSQL(dtrIn!ae_codart) & "," & _
            " ax_codvalu = 1," & _
            " ax_descr = " & CStrSQL(dtrIn!ae_descrl1) & ", " & _
            " ax_desint = " & CStrSQL(dtrIn!ae_desintl1) & ", " & _
            " ax_ultagg = " & CDataSQL(Date.Now) & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ax_codart = " & CStrSQL(dtrIn!ae_codart) & _
            " AND ax_codvalu = 1"
          Else
            strSQL = "INSERT INTO artval (codditt, ax_codart, ax_codvalu, ax_descr," & _
            " ax_desint, ax_ultagg, ax_note)" & _
            " VALUES (" & CStrSQL(strDitta) & "," & _
            " " & CStrSQL(dtrIn!ae_codart) & "," & _
            " " & 1 & "," & _
            " " & CStrSQL(dtrIn!ae_descrl1) & ", " & CStrSQL(dtrIn!ae_desintl1) & "," & _
            " " & CDataSQL(Date.Now) & ", NULL)"
          End If
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        End If

        'inserimento/aggiornamento artval ling2
        If (Trim(NTSCStr(dtrIn!ae_descrl2)) <> "") Or (Trim(NTSCStr(dtrIn!ae_desintl2)) <> "") Then

          strSQL = "SELECT * FROM artval WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND ax_codart = " & CStrSQL(dtrIn!ae_codart) & " AND ax_codvalu = 2"

          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count > 0 Then
            strSQL = "UPDATE artval SET codditt = " & CStrSQL(strDitta) & "," & _
            " ax_codart = " & CStrSQL(dtrIn!ae_codart) & "," & _
            " ax_codvalu = 2," & _
            " ax_descr = " & CStrSQL(dtrIn!ae_descrl2) & ", " & _
            " ax_desint = " & CStrSQL(dtrIn!ae_desintl2) & ", " & _
            " ax_ultagg = " & CDataSQL(Date.Now) & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ax_codart = " & CStrSQL(dtrIn!ae_codart) & _
            " AND ax_codvalu = 2"
          Else
            strSQL = "INSERT INTO artval (codditt, ax_codart, ax_codvalu, ax_descr," & _
            " ax_desint, ax_ultagg, ax_note)" & _
            " VALUES (" & CStrSQL(strDitta) & "," & _
            " " & CStrSQL(dtrIn!ae_codart) & "," & _
            " " & 2 & "," & _
            " " & CStrSQL(dtrIn!ae_descrl2) & ", " & CStrSQL(dtrIn!ae_desintl2) & "," & _
            " " & CDataSQL(Date.Now) & ", NULL)"
          End If
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        End If

        'inserimento/aggiornamento artval ling3
        If (Trim(NTSCStr(dtrIn!ae_descrl3)) <> "") Or (Trim(NTSCStr(dtrIn!ae_desintl3)) <> "") Then

          strSQL = "SELECT * FROM artval WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND ax_codart = " & CStrSQL(dtrIn!ae_codart) & " AND ax_codvalu = 3"

          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count > 0 Then
            strSQL = "UPDATE artval SET codditt = " & CStrSQL(strDitta) & "," & _
                     " ax_codart = " & CStrSQL(dtrIn!ae_codart) & "," & _
                     " ax_codvalu = 3," & _
                     " ax_descr = " & CStrSQL(dtrIn!ae_descrl3) & ", " & _
                     " ax_desint = " & CStrSQL(dtrIn!ae_desintl3) & ", " & _
                     " ax_ultagg = " & CDataSQL(Date.Now) & _
                     " WHERE codditt = " & CStrSQL(strDitta) & _
                     " AND ax_codart = " & CStrSQL(dtrIn!ae_codart) & _
                     " AND ax_codvalu = 3"
          Else
            strSQL = "INSERT INTO artval (codditt, ax_codart, ax_codvalu, ax_descr," & _
                     " ax_desint, ax_ultagg, ax_note)" & _
                     " VALUES (" & CStrSQL(strDitta) & "," & _
                     " " & CStrSQL(dtrIn!ae_codart) & "," & _
                     " " & 3 & "," & _
                     " " & CStrSQL(dtrIn!ae_descrl3) & ", " & CStrSQL(dtrIn!ae_desintl3) & "," & _
                     " " & CDataSQL(Date.Now) & ", NULL)"
          End If
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        End If

      End If

      'aggiornamento barcode
      If bBarcode Then

        'inserimento primo barcode

        If Trim(NTSCStr(dtrIn!ae_codeump)) <> "" Then

          'controllo presenza primo barcode per quel articolo

          strSQL = "SELECT bc_code FROM barcode WHERE bc_code = " & CStrSQL(dtrIn!ae_codeump) & _
                   " AND bc_codart = " & CStrSQL(dtrIn!ae_codart) & _
                   " AND codditt = " & CStrSQL(strDitta)

          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          If Not dttTmp.Rows.Count > 0 Then
            'controllo presenza primo barcode per altri articoli
            strSQL = "SELECT bc_code FROM barcode WHERE bc_code = " & CStrSQL(dtrIn!ae_codeump) & _
                     " AND bc_codart <> " & CStrSQL(dtrIn!ae_codart) & _
                     " AND codditt = " & CStrSQL(strDitta)

            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            'se presente annulla transazione
            If dttTmp.Rows.Count > 0 Then
              strErrore = "Codice barcode: |" & NTSCStr(dtrIn!ae_codeump) & "| già presente per la ditta |" & _
                          strDitta & "|" & vbCrLf & "Generazione articolo non possibile."

              'passa successivo
              AnnullaTrans()
              Return True
            End If

            'inserimento primo barcode
            strSQL = "INSERT INTO barcode (codditt, bc_codart, bc_datins, bc_code, bc_unmis," & _
            " bc_quant, bc_orins, bc_datagg, bc_oragg, bc_tipo, bc_tagl, bc_fase)" & _
            " VALUES (" & CStrSQL(strDitta) & "," & _
            " " & CStrSQL(dtrIn!ae_codart) & ","
            strSQL &= " " & CDataSQL(Date.Now) & ","
            strSQL &= " " & CStrSQL(dtrIn!ae_codeump) & "," & _
            " " & CStrSQL(dtrIn!ae_unmis) & "," & _
            " 1, 0,"
            strSQL &= " " & CDataSQL(Date.Now) & ","
            strSQL &= " 0, 'E', NULL, 0)"

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        End If

        'inserimento secondo barcode

        If Trim(NTSCStr(dtrIn!ae_codeconf)) <> "" Then

          'controllo presenza secondo barcode per quel articolo

          strSQL = "SELECT bc_code FROM barcode WHERE bc_code = " & CStrSQL(dtrIn!ae_codeconf) & _
                   " AND bc_codart = " & CStrSQL(dtrIn!ae_codart) & _
                   " AND codditt = " & CStrSQL(strDitta)

          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'se presente barcode per quel articolo salta fine secondo barcode
          If Not dttTmp.Rows.Count > 0 Then

            'controllo presenza primo barcode per altri articoli
            strSQL = "SELECT bc_code FROM barcode WHERE bc_code = " & CStrSQL(dtrIn!ae_codeconf) & _
              " AND bc_codart <> " & CStrSQL(dtrIn!ae_codart) & _
              " AND codditt = " & CStrSQL(strDitta)

            dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

            'se presente annulla transazione
            If dttTmp.Rows.Count > 0 Then
              strErrore = "Codice barcode: |" & NTSCStr(dtrIn!ae_codeconf) & "| già presente per la ditta |" & _
                          strDitta & "|" & vbCrLf & "Generazione articolo non possibile."

              'passa successivo
              AnnullaTrans()
              Return True
            End If

            'inserimento secondo barcode
            strSQL = "INSERT INTO barcode (codditt, bc_codart, bc_datins, bc_code, bc_unmis," & _
            " bc_quant, bc_orins, bc_datagg, bc_oragg, bc_tipo, bc_tagl, bc_fase)" & _
            " VALUES (" & CStrSQL(strDitta) & "," & _
            " " & CStrSQL(dtrIn!ae_codart) & ","
            strSQL &= " " & CDataSQL(Date.Now) & ","
            strSQL &= " " & CStrSQL(dtrIn!ae_codeconf) & "," & _
            " " & CStrSQL(dtrIn!ae_confez2) & "," & _
            " " & NTSCInt(dtrIn!ae_qtacon2) & "," & _
            " 0,"
            strSQL &= " " & CDataSQL(Date.Now) & ","
            strSQL &= " 0, 'E', NULL, 0)"

            Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          End If
        End If
      End If

      'lettura di artico

      strSQL = "SELECT * FROM artico WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(dtrIn!ae_codart)

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'inserimento di TTARTICOX

      strSQL = "INSERT INTO TTARTICOX (instid," & _
        " codditt, ar_codart, ar_codalt, ar_descr, ar_desint, ar_tipo, ar_unmis, ar_unmis2," & _
        " ar_conver , ar_confez2, ar_qtacon2, ar_forn, ar_forn2, ar_codiva, ar_gruppo, ar_sotgru," & _
        " ar_controp , ar_catlifo, ar_claprov, ar_clascon, ar_ubicaz, ar_scomin, ar_scomax," & _
        " ar_minord , ar_ggrior, ar_sostit, ar_controa, ar_reparto, ar_stalist, ar_ultagg," & _
        " ar_contriva, ar_oragg, ar_datins, ar_orins, ar_tipprom, ar_famprod, ar_numecr," & _
        " ar_areacm3, ar_codvuo, ar_flricmar, ar_codpdon, ar_ricar1, ar_ricar2, ar_garacq," & _
        " ar_garven, ar_gesvar, ar_prevar, ar_prorig, ar_percvst, ar_codnomc, ar_pesolor," & _
        " ar_pesonet, ar_paeorig, ar_livmindb, ar_coddb, ar_stainv, ar_stasche, ar_geslotti," & _
        " ar_inesaur, ar_note, ar_tipoopz, ar_polriord, ar_gescomm, ar_fpfence, ar_rrfence," & _
        " ar_sostituito, ar_critico, ar_codroot, ar_codvar1, ar_codvar2, ar_codvar3, ar_formula," & _
        " ar_nummisure, ar_contros, ar_pesoca, ar_gestmatr, ar_umdapr, ar_magstock, ar_magprod," & _
        " ar_magvend, ar_umintra2, ar_codappr, ar_codmarc, ar_tipokit, ar_perqta, ar_fcorrlt," & _
        " ar_magnoconf, ar_codtipa, ar_verdb , ar_blocco, ar_um4, ar_umdapra, ar_umpdapr," & _
        " ar_umpdapra, ar_opnome, ar_gif1 , ar_gif2, ar_ggant, ar_ggpost, ar_gescon, ar_flmod," & _
        " ar_codimba , ar_misura1, ar_misura2, ar_misura3, ar_gesubic, ar_gesfasi, ar_ultfase," & _
        " ar_volume, ar_makebuy, ar_sublotto, ar_maxlotto, ar_ggragg, ar_ripriord, ar_perragg," & _
        " ar_gestser, ar_tipitemcp3, ar_codtagl, ar_anno, ar_codstag, ar_cartric, ar_cartcanas," & _
        " ar_cartcanol, ar_scosic)"

      strSQL &= " VALUES (" & nInstid & ", " & _
        CStrSQL(strDitta) & ", " & CStrSQL(dttTmp.Rows(0)!ar_codart) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_codalt) & ", " & CStrSQL(dttTmp.Rows(0)!ar_descr) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_desint) & ", " & CStrSQL(dttTmp.Rows(0)!ar_tipo) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_unmis) & ", " & CStrSQL(dttTmp.Rows(0)!ar_unmis2) & ", " & _
        CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_conver)) & ", " & CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_confez2)) & ", " & _
        CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_qtacon2)) & ", " & NTSCInt(dttTmp.Rows(0)!ar_forn) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_forn2) & ", " & NTSCInt(dttTmp.Rows(0)!ar_codiva) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_gruppo) & ", " & NTSCInt(dttTmp.Rows(0)!ar_sotgru) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_controp) & ", " & NTSCInt(dttTmp.Rows(0)!ar_catlifo) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_claprov) & ", " & NTSCInt(dttTmp.Rows(0)!ar_clascon) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_ubicaz) & ", " & CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_scomin)) & ", " & _
        CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_scomax)) & ", " & CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_minord)) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_ggrior) & ", " & CStrSQL(dttTmp.Rows(0)!ar_sostit) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_controa) & ", " & NTSCInt(dttTmp.Rows(0)!ar_reparto) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_stalist) & ", " & CDataSQL(NTSCStr(dttTmp.Rows(0)!ar_ultagg)) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_contriva) & ", " & NTSCInt(dttTmp.Rows(0)!ar_oragg) & ", " & _
        CDataSQL(NTSCStr(dttTmp.Rows(0)!ar_datins)) & ", " & NTSCInt(dttTmp.Rows(0)!ar_orins) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_tipprom) & ", " & CStrSQL(dttTmp.Rows(0)!ar_famprod) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_numecr) & ", " & NTSCInt(dttTmp.Rows(0)!ar_areacm3) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_codvuo) & ", " & CStrSQL(dttTmp.Rows(0)!ar_flricmar) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_codpdon) & ", " & CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_ricar1)) & ", " & _
        CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_ricar2)) & ", " & NTSCInt(dttTmp.Rows(0)!ar_garacq) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_garven) & ", " & CStrSQL(dttTmp.Rows(0)!ar_gesvar) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_prevar) & ", " & CStrSQL(dttTmp.Rows(0)!ar_prorig) & ", " & _
        CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_percvst)) & ", " & CStrSQL(dttTmp.Rows(0)!ar_codnomc) & ", " & _
        CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_pesolor)) & ", " & CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_pesonet)) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_paeorig) & ", " & NTSCInt(dttTmp.Rows(0)!ar_livmindb) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_coddb) & ", " & CStrSQL(dttTmp.Rows(0)!ar_stainv) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_stasche) & ", " & CStrSQL(dttTmp.Rows(0)!ar_geslotti) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_inesaur) & ", " & CStrSQL(dttTmp.Rows(0)!ar_note) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_tipoopz) & ", " & CStrSQL(dttTmp.Rows(0)!ar_polriord) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_gescomm) & ", " & NTSCInt(dttTmp.Rows(0)!ar_fpfence) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_rrfence) & ", " & CStrSQL(dttTmp.Rows(0)!ar_sostituito) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_critico) & ", " & CStrSQL(dttTmp.Rows(0)!ar_codroot) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_codvar1) & ", " & CStrSQL(dttTmp.Rows(0)!ar_codvar2) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_codvar3) & ", " & CStrSQL(dttTmp.Rows(0)!ar_formula) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_nummisure) & ", " & NTSCInt(dttTmp.Rows(0)!ar_contros) & ", " & _
        CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_pesoca)) & ", " & CStrSQL(dttTmp.Rows(0)!ar_gestmatr) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_umdapr) & ", " & NTSCInt(dttTmp.Rows(0)!ar_magstock) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_magprod) & ", " & NTSCInt(dttTmp.Rows(0)!ar_magvend) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_umintra2) & ", " & NTSCInt(dttTmp.Rows(0)!ar_codappr) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_codmarc) & ", " & CStrSQL(dttTmp.Rows(0)!ar_tipokit) & ", " & _
        CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_perqta)) & ", " & CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_fcorrlt)) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_magnoconf) & ", " & NTSCInt(dttTmp.Rows(0)!ar_codtipa) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_verdb) & ", " & CStrSQL(dttTmp.Rows(0)!ar_blocco) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_um4) & ", " & CStrSQL(dttTmp.Rows(0)!ar_umdapra) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_umpdapr) & ", " & CStrSQL(dttTmp.Rows(0)!ar_umpdapra) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_opnome) & ", " & CStrSQL(dttTmp.Rows(0)!ar_gif1) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_gif2) & ", " & NTSCInt(dttTmp.Rows(0)!ar_ggant) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_ggpost) & ", " & CStrSQL(dttTmp.Rows(0)!ar_gescon) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_flmod) & ", " & NTSCInt(dttTmp.Rows(0)!ar_codimba) & ", " & _
        CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_misura1)) & ", " & CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_misura2)) & ", " & _
        CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_misura3)) & ", " & CStrSQL(dttTmp.Rows(0)!ar_gesubic) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_gesfasi) & ", " & NTSCInt(dttTmp.Rows(0)!ar_ultfase) & ", " & _
        CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_volume)) & ", " & CStrSQL(dttTmp.Rows(0)!ar_makebuy) & ", " & _
        CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_sublotto)) & ", " & CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_maxlotto)) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_ggragg) & ", " & CStrSQL(dttTmp.Rows(0)!ar_ripriord) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_perragg) & ", " & CStrSQL(dttTmp.Rows(0)!ar_gestser) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_tipitemcp3) & ", " & NTSCInt(dttTmp.Rows(0)!ar_codtagl) & ", " & _
        NTSCInt(dttTmp.Rows(0)!ar_anno) & ", " & NTSCInt(dttTmp.Rows(0)!ar_codstag) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_cartric) & ", " & CStrSQL(dttTmp.Rows(0)!ar_cartcanas) & ", " & _
        CStrSQL(dttTmp.Rows(0)!ar_cartcanol) & ", " & CDblSQL(NTSCDec(dttTmp.Rows(0)!ar_scosic)) & ")"

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

  Public Overridable Function VerificaPresenzaDatiDaStampare(ByVal strDittaCorrente As String, ByVal strTabellaTemporanea As String, ByVal nInstid As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 instid FROM " & strTabellaTemporanea & " WHERE instid = " & nInstid
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strTabellaTemporanea).Tables(strTabellaTemporanea).Rows.Count > 0 Then Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

End Class
