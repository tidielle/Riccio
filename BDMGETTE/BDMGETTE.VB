Imports System.IO
Imports NTSInformatica.CLN__STD
Imports NTSInformatica
Imports System.Data.SqlClient
Imports System.Data.OleDb


Public Class CLDMGETTE
  Inherits CLD__BASE

  Public Overridable Function LeggiDatiMagaz(ByVal strDitta As String, ByVal strDoc As String, ByVal lAnno As Integer, _
                                             ByVal strSerie As String, ByVal lNumDoc As Integer, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strTesta As String = ""
    Try
      If strDoc = "W" Then
        strTesta = "testprb"
      Else
        strTesta = "testmag"
      End If

      strSQL = "SELECT tm_conto, tm_datdoc FROM " & strTesta & _
                         " WHERE codditt = " & CStrSQL(strDitta) & _
                         " AND tm_tipork = " & CStrSQL(strDoc) & _
                         " AND tm_anno = " & lAnno & _
                         " AND tm_serie = " & CStrSQL(strSerie) & _
                         " AND tm_numdoc = " & lNumDoc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function LeggiDatiOrdini(ByVal strDitta As String, ByVal strDoc As String, ByVal lAnno As Integer, _
                                             ByVal strSerie As String, ByVal lNumDoc As Integer, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT td_conto, td_datord FROM testord" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND td_tipork = " & CStrSQL(strDoc) & _
               " AND td_anno = " & lAnno & _
               " AND td_serie = " & CStrSQL(strSerie) & _
               " AND td_numord = " & lNumDoc

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SelezionaArtico(ByVal strDitta As String, ByVal bBD_DaCodarfo As Boolean, ByVal strArtico As String, _
                                              ByVal strArticoli As String, ByVal strWhereArtico As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      If bBD_DaCodarfo Then
        'Seleziona codarfo
        strSQL = "SELECT caf_codarfo AS Codart, caf_desnote AS Barcode, caf_codart" & _
                 " FROM codarfo" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND caf_codarfo = " & CStrSQL(strArtico) & _
                 " AND caf_codart = " & CStrSQL(strArticoli)
      Else
        'Selezione di articoli
        strSQL = "SELECT ar_codart AS Codart, ar_codtagl AS CodTagl, ar_unmis AS UMP, ar_unmis2 AS UMS, ar_confez2 AS UMC  " & _
                 " FROM artico " & _
                 " WHERE artico.codditt = " & CStrSQL(strDitta)

        TraduciWhere(strWhereArtico, strSQL)
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelezionaLista(ByVal strDitta As String, ByVal lLista As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT lsa_codart AS Codart, lsa_tctaglia, Min(ar_codtagl) AS CodTagl, Min(ar_unmis) AS UMP, Min(ar_unmis2) AS UMS, Min(ar_confez2) AS UMC " & _
               " FROM listsar INNER JOIN artico ON listsar.codditt = artico.codditt AND listsar.lsa_codart = artico.ar_codart" & _
               " WHERE listsar.codditt = " & CStrSQL(strDitta) & _
               " AND listsar.lsa_codlsar = " & lLista & _
               " GROUP BY lsa_codart, lsa_tctaglia"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelezionaMagazzino(ByVal strDitta As String, ByVal strTipodoc As String, ByVal lDaclie As Integer, _
                                                 ByVal lAclie As Integer, ByVal dtDadata As Date, ByVal dtAdata As Date, _
                                                 ByVal lDaanno As Integer, ByVal lAanno As Integer, ByVal strDaserie As String, _
                                                 ByVal strAserie As String, ByVal lDanumero As Integer, ByVal lAnumero As Integer, _
                                                 ByVal lDariga As Integer, ByVal lAriga As Integer, ByVal bDocMagazz As Boolean, _
                                                 ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strTableTesta As String
    Dim strTableCorpo As String
    Try
      If bDocMagazz = True Then
        'magazzino
        If strTipodoc = "W" Then
          strTableTesta = "testprb"
          strTableCorpo = "movprb"
        Else
          strTableTesta = "testmag"
          strTableCorpo = "movmag"
        End If

        strSQL = "SELECT mm_codart AS Codart, Min(tm_datdoc) AS Dtdoc, Min(ar_qtacon2) As Qtacon2," & _
          " Min(ar_unmis) As UMP" & _
          " FROM " & strTableTesta & " INNER JOIN " & strTableCorpo & _
          " ON " & strTableTesta & ".codditt = " & strTableCorpo & ".codditt" & _
          " AND " & strTableTesta & ".tm_tipork = " & strTableCorpo & ".mm_tipork" & _
          " AND " & strTableTesta & ".tm_anno = " & strTableCorpo & ".mm_anno" & _
          " AND " & strTableTesta & ".tm_serie = " & strTableCorpo & ".mm_serie" & _
          " AND " & strTableTesta & ".tm_numdoc = " & strTableCorpo & ".mm_numdoc" & _
          " INNER JOIN artico ON " & strTableCorpo & ".codditt = artico.codditt" & _
          " AND " & strTableCorpo & ".mm_codart = artico.ar_codart" & _
          " WHERE " & strTableCorpo & ".codditt = " & CStrSQL(strDitta) & _
          " AND mm_tipork = " & CStrSQL(strTipodoc) & _
          " AND tm_conto >= " & lDaclie & _
          " AND tm_conto <= " & lAclie & _
          " AND tm_datdoc >= " & CDataSQL(dtDadata) & _
          " AND tm_datdoc <= " & CDataSQL(dtAdata) & _
          " AND mm_anno >= " & lDaanno & _
          " AND mm_anno <= " & lAanno & _
          " AND mm_serie >= " & CStrSQL(strDaserie) & _
          " AND mm_serie <= " & CStrSQL(strAserie) & _
          " AND mm_numdoc >= " & lDanumero & _
          " AND mm_numdoc <= " & lAnumero & _
          " AND mm_riga >= " & lDariga & _
          " AND mm_riga <= " & lAriga & _
          " AND ar_stainv = " & CStrSQL("S") & _
          " GROUP BY mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_codart"
      Else
        'ordini
        strSQL = "SELECT mo_codart AS Codart, Min(td_datord) AS Dtdoc, Min(ar_qtacon2) As Qtacon2," & _
          " Min(ar_unmis) As UMP" & _
          " FROM testord INNER JOIN movord ON testord.codditt = movord.codditt" & _
          " AND testord.td_tipork = movord.mo_tipork" & _
          " AND testord.td_serie = movord.mo_serie" & _
          " AND testord.td_anno = movord.mo_anno" & _
          " AND testord.td_numord = movord.mo_numord" & _
          " INNER JOIN artico ON movord.codditt = artico.codditt" & _
          " AND artico.ar_codart = movord.mo_codart" & _
          " WHERE movord.codditt = " & CStrSQL(strDitta) & _
          " AND mo_tipork = " & CStrSQL(strTipodoc) & _
          " AND td_conto >= " & lDaclie & _
          " AND td_conto <= " & lAclie & _
          " AND td_datord >= " & CDataSQL(dtDadata) & _
          " AND td_datord <= " & CDataSQL(dtAdata) & _
          " AND mo_anno >= " & lDaanno & _
          " AND mo_anno <= " & lAanno & _
          " AND mo_serie >= " & CStrSQL(strDaserie) & _
          " AND mo_serie <= " & CStrSQL(strAserie) & _
          " AND mo_numord >= " & lDanumero & _
          " AND mo_numord <= " & lAnumero & _
          " AND mo_riga >= " & lDariga & _
          " AND mo_riga <= " & lAriga & _
          " AND ar_stainv = " & CStrSQL("S") & _
          " GROUP BY mo_tipork, mo_anno, mo_serie, mo_numord, mo_codart"
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelezionaDaDocMaga(ByVal strDitta As String, ByVal strTipodoc As String, ByVal lDaclie As Integer, _
                                                 ByVal lAclie As Integer, ByVal dtDadata As Date, ByVal dtAdata As Date, _
                                                 ByVal lDaanno As Integer, ByVal lAanno As Integer, ByVal strDaserie As String, _
                                                 ByVal strAserie As String, ByVal lDanumero As Integer, ByVal lAnumero As Integer, _
                                                 ByVal lDariga As Integer, ByVal lAriga As Integer, ByVal bDocMagazz As Boolean, _
                                                 ByVal bColli As Boolean, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strTableTesta As String
    Dim strTableCorpo As String
    Try
      If bDocMagazz = True Then
        'magazzino
        If strTipodoc = "W" Then
          strTableTesta = "testprb"
          strTableCorpo = "movprb"
        Else
          strTableTesta = "testmag"
          strTableCorpo = "movmag"
        End If

        'da documenti di magazzino
        strSQL = "SELECT mm_codart AS Codart, " & NTSCStr(IIf(bColli, "mm_colli", "mm_quant")) & " AS Quant, ar_codtagl AS CodTagl, mm_tipork AS Tipork, mm_anno AS Anno, mm_serie AS Serie, mm_numdoc AS Numero, mm_riga AS Riga, tm_datdoc AS Dtdoc, tm_conto AS Conto, ar_qtacon2 As Qtacon2, mm_ump AS UmQuant, mm_unmis AS UmColli, ar_misura1 As Misura1 " & _
          " FROM " & strTableTesta & " INNER JOIN (artico INNER JOIN " & strTableCorpo & " ON (" & strTableCorpo & ".codditt = artico.codditt) AND (artico.ar_codart = mm_codart)) ON (" & strTableTesta & ".codditt = " & strTableCorpo & ".codditt) AND (tm_tipork = mm_tipork) AND (tm_serie = mm_serie) AND (tm_anno = mm_anno) AND (tm_numdoc = mm_numdoc)" & _
          " WHERE " & strTableCorpo & ".codditt = " & CStrSQL(strDitta) & _
          " AND mm_tipork = " & CStrSQL(strTipodoc) & _
          " AND tm_conto >= " & lDaclie & _
          " AND tm_conto <= " & lAclie & _
          " AND tm_datdoc >= " & CDataSQL(dtDadata) & _
          " AND tm_datdoc <= " & CDataSQL(dtAdata) & _
          " AND mm_anno >= " & lDaanno & _
          " AND mm_anno <= " & lAanno & _
          " AND mm_serie >= " & CStrSQL(strDaserie) & _
          " AND mm_serie <= " & CStrSQL(strAserie) & _
          " AND mm_numdoc >= " & lDanumero & _
          " AND mm_numdoc <= " & lAnumero & _
          " AND mm_riga >= " & lDariga & _
          " AND mm_riga <= " & lAriga
      Else
        'da ordini
        strSQL = "SELECT mo_codart AS Codart," & _
          " " & NTSCStr(IIf(bColli, "mo_colli", "mo_quant")) & " AS Quant," & _
          " ar_codtagl AS CodTagl, mo_tipork AS Tipork, mo_anno AS Anno," & _
          " mo_serie AS Serie, mo_numord AS Numero, mo_riga AS Riga," & _
          " td_datord AS Dtdoc, td_conto AS Conto, ar_qtacon2 As Qtacon2, mo_ump AS UmQuant, mo_unmis AS UmColli, ar_misura1 As Misura1 " & _
          " FROM testord INNER JOIN (artico INNER JOIN movord  ON (movord.codditt = artico.codditt) AND (artico.ar_codart = movord.mo_codart)) ON (testord.codditt = movord.codditt) AND (testord.td_tipork = movord.mo_tipork) AND (testord.td_serie = movord.mo_serie) AND (testord.td_anno = movord.mo_anno) AND (testord.td_numord = movord.mo_numord)" & _
          " WHERE movord.codditt = " & CStrSQL(strDitta) & _
          " AND mo_tipork = " & CStrSQL(strTipodoc) & _
          " AND td_conto >= " & lDaclie & _
          " AND td_conto <= " & lAclie & _
          " AND td_datord >= " & CDataSQL(dtDadata) & _
          " AND td_datord <= " & CDataSQL(dtAdata) & _
          " AND mo_anno >= " & lDaanno & _
          " AND mo_anno <= " & lAanno & _
          " AND mo_serie >= " & CStrSQL(strDaserie) & _
          " AND mo_serie <= " & CStrSQL(strAserie) & _
          " AND mo_numord >= " & lDanumero & _
          " AND mo_numord <= " & lAnumero & _
          " AND mo_riga >= " & lDariga & _
          " AND mo_riga <= " & lAriga
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SelectBarcode(ByVal strDitta As String, ByVal bTipobc As Boolean, ByVal strTipoBc As String, _
                                            ByVal strCodart As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM barcode" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND bc_codart = " & CStrSQL(strCodart) & _
               " " & NTSCStr(IIf(bTipobc, " AND bc_tipo = " & CStrSQL(strTipoBc), ""))

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelectTTstmatr(ByVal strDitta As String, ByVal lIIstMatr As Integer, ByVal strCodart As String, _
                                             ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM ttstmatr" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tt_codart = " & CStrSQL(strCodart) & _
               " AND instid = " & lIIstMatr

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelectFasi(ByVal strCodartList As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ar_gesfasi, ar_ultfase FROM artico WHERE ar_codart = " & CStrSQL(strCodartList)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function InsertBarcodeTTstmatr(ByVal strDitta As String, ByVal lIIstMatr As Integer, ByVal strCodart As String, _
                                                    ByVal strBarcode As String, ByVal dBarqta As Decimal, ByVal strBarUM As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO ttstmatr (codditt, tt_codart, instid, tt_matric, tt_quant, tt_qtacar, tt_riferim ) " & _
               " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodart) & ", " & _
               lIIstMatr & ", " & CStrSQL(strBarcode) & ", 0, " & dBarqta & ", " & _
               CStrSQL(strBarUM) & ")"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SelectEtichetteStp(ByVal strDitta As String, ByVal strCodart As String, ByVal lIIstMatrTmp As Integer, _
                                                 ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tt_codart, tt_matric, tt_quant FROM ttstmatr " & _
                   " WHERE codditt =" & CStrSQL(strDitta) & " AND tt_codart = " & CStrSQL(strCodart) & _
                   " AND instid = " & lIIstMatrTmp
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function InsertAggancioMovmag(ByVal strDitta As String, ByVal lIIstMatr As Integer, ByVal InstMats As Integer, _
                                                   ByVal lCounter As Integer, ByVal i As Integer, ByVal strCodart As String, _
                                                   ByVal dPrezzo As Decimal, ByVal dPrezval As Decimal, ByVal strCode As String, _
                                                   ByVal strOrigine As String, ByVal bUsaTtstmatr As Boolean, ByVal dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      If bUsaTtstmatr And strOrigine = "M" Then
        strSQL = "INSERT INTO TTSTMATR (codditt, instid, tt_codart, tt_magaz, tt_matric," & _
          " tt_aammgg, tt_causale, tt_conto, tt_riferim, tt_tipork, tt_anno, tt_serie," & _
          " tt_numdoc, tt_riga, tt_quant, tt_prezzo, tt_valore, tt_carscar, tt_qtacar," & _
          " tt_valcar)" & _
          " VALUES (" & CStrSQL(strDitta) & ", " & lIIstMatr & ", " & _
          CStrSQL(strCodart) & ", " & lCounter & ", " & CStrSQL(strCode) & ", " & _
          CDataSQL(NTSCStr(dttOut.Rows(i)!DtDoc)) & ", 0, " & NTSCInt(dttOut.Rows(i)!Conto) & ", NULL, " & _
          CStrSQL(dttOut.Rows(i)!Tipork) & ", " & NTSCInt(dttOut.Rows(i)!Anno) & ", " & _
          CStrSQL(dttOut.Rows(i)!Serie) & ", " & NTSCInt(dttOut.Rows(i)!Numero) & ", " & NTSCInt(dttOut.Rows(i)!RIGA) & ", 0, " & _
          CDblSQL(dPrezzo) & ", " & CDblSQL(dPrezval) & ", 0, 0, 0)"
      Else
        strSQL = "INSERT INTO TTSTMATS (codditt, instid, tt_codart, tt_magaz, tt_matric," & _
          " tt_valore, tt_valcar)" & _
          " VALUES (" & CStrSQL(strDitta) & ", " & InstMats & ", " & _
          CStrSQL(strCodart) & ", " & lCounter & ", " & _
          CStrSQL(strCode) & ", " & CDblSQL(dPrezzo) & ", " & _
          CDblSQL(dPrezval) & ")"
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DeleteTTstmatr(ByVal strDitta As String, ByVal lIIstMatrTmp As Integer, ByVal strCodart As String, _
                                             ByVal i As Integer, ByVal dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM ttstmatr " & _
              " WHERE codditt =" & CStrSQL(strDitta) & " AND tt_codart = " & CStrSQL(strCodart) & _
              " AND instid = " & lIIstMatrTmp

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SelectBarcodeStp(ByVal strDitta As String, ByVal strCodart As String, ByVal strTipoBc As String, _
                                               ByVal bTipobc As Boolean, ByRef dttTmp2 As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM barcode" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND bc_codart = " & CStrSQL(strCodart) & _
                    " " & NTSCStr(IIf(bTipobc, " AND bc_tipo = " & CStrSQL(strTipoBc) & "", ""))

      dttTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function NumeroEtichette(ByVal strDitta As String, ByVal strCodart As String, ByVal lLista As Integer, _
                                              ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT lsa_note FROM listsar" & _
             " WHERE codditt = " & CStrSQL(strDitta) & _
             " AND lsa_codlsar = " & NTSCInt(lLista) & _
             " AND lsa_codart = " & CStrSQL(strCodart)

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DeterminaFasi(ByVal strCodartList As String, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ar_gesfasi, ar_ultfase FROM artico WHERE ar_codart = " & CStrSQL(strCodartList)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SelectMovJoin(ByVal strDitta As String, ByVal bDocMagazz As Boolean, ByVal dttIn As DataTable, _
                                            ByVal i As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim strFrom As String
    Dim j As Integer
    Try
      If Not bDocMagazz Then
        strSQL = "SELECT"
        For j = 1 To 24
          strSQL &= " mo_quant" & Right("0" & CStr(j), 2) & " As Quant" & Right("0" & CStr(j), 2) & ","
        Next j
        strSQL &= " tabtagl.* FROM" & _
          " ((movord INNER JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga)" & _
          " INNER JOIN artico ON artico.codditt = movord.codditt AND artico.ar_codart = movord.mo_codart)" & _
          " INNER JOIN tabtagl ON tabtagl.codditt = artico.codditt AND tabtagl.tb_codtagl = artico.ar_codtagl" & _
          " WHERE movord.codditt = " & CStrSQL(strDitta) & _
          " AND movord.mo_tipork = " & CStrSQL(dttIn.Rows(i)!Tipork) & _
          " AND movord.mo_anno = " & NTSCInt(dttIn.Rows(i)!Anno) & _
          " AND movord.mo_serie = " & CStrSQL(dttIn.Rows(i)!Serie) & _
          " AND movord.mo_numord = " & NTSCInt(dttIn.Rows(i)!Numero) & _
          " AND movord.mo_riga = " & NTSCInt(dttIn.Rows(i)!RIGA) & _
          " AND ("
        For j = 1 To 24
          strSQL &= " mo_quant" & Right("0" & CStr(j), 2) & NTSCStr(IIf(j <> 24, " +", ") <> 0"))
        Next j
      Else
        If ntscstr(dttIn.Rows(i)!Tipork) = "W" Then
          strFrom = " movprb "
        Else
          strFrom = " movmag "
        End If

        strSQL = "SELECT"
        For j = 1 To 24
          strSQL &= " mm_quant" & Right("0" & CStr(j), 2) & " As Quant" & Right("0" & CStr(j), 2) & ","
        Next j
        strSQL &= " tabtagl.* FROM" & _
          " ((" & strFrom & " INNER JOIN movmagtc ON " & strFrom & ".codditt = movmagtc.codditt AND " & strFrom & ".mm_tipork = movmagtc.mm_tipork AND " & strFrom & ".mm_anno = movmagtc.mm_anno AND " & strFrom & ".mm_serie = movmagtc.mm_serie AND " & strFrom & ".mm_numdoc = movmagtc.mm_numdoc AND " & strFrom & ".mm_riga = movmagtc.mm_riga)" & _
          " INNER JOIN artico ON artico.codditt = " & strFrom & ".codditt AND artico.ar_codart = " & strFrom & ".mm_codart)" & _
          " INNER JOIN tabtagl ON tabtagl.codditt = artico.codditt AND tabtagl.tb_codtagl = artico.ar_codtagl" & _
          " WHERE " & strFrom & ".codditt = " & CStrSQL(strDitta) & _
          " AND " & strFrom & ".mm_tipork = " & CStrSQL(dttIn.Rows(i)!Tipork) & _
          " AND " & strFrom & ".mm_anno = " & NTSCInt(dttIn.Rows(i)!Anno) & _
          " AND " & strFrom & ".mm_serie = " & CStrSQL(dttIn.Rows(i)!Serie) & _
          " AND " & strFrom & ".mm_numdoc = " & NTSCInt(dttIn.Rows(i)!Numero) & _
          " AND " & strFrom & ".mm_riga = " & NTSCInt(dttIn.Rows(i)!RIGA) & _
          " AND ("
        For j = 1 To 24
          strSQL &= " mm_quant" & Right("0" & CStr(j), 2) & NTSCStr(IIf(j <> 24, " +", ") <> 0"))
        Next j
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function BarcodeTaglia(ByVal strDitta As String, ByVal strCodart As String, ByVal strTipoBc As String, _
                                            ByVal strUM As String, ByVal strDest As String, ByVal bTipobc As Boolean, _
                                            ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT bc_code FROM barcode" & _
                          " WHERE codditt = " & CStrSQL(strDitta) & _
                          " AND bc_codart = " & CStrSQL(strCodart) & _
                          " AND bc_tagl = " & CStrSQL(strDest) & _
                          " " & NTSCStr(IIf(bTipobc, " AND bc_tipo = " & CStrSQL(strTipoBc), "")) & _
                          " AND bc_unmis = " & CStrSQL(strUM) & " " & _
                          " ORDER BY bc_datins DESC, bc_code"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ScriviEtichette(ByVal strDitta As String, ByVal dttIn As DataTable, ByVal dttTmp As DataTable, _
                                              ByVal bUsaTtstMatr As Boolean, ByVal strCodart As String, ByVal lCounter As Integer, _
                                              ByVal strCode As String, ByVal dPrezzo As Decimal, ByVal dPrezval As Decimal, _
                                              ByVal lIIstMatr As Integer, ByVal lInstmats As Integer, ByVal i As Integer, _
                                              ByVal j As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      If bUsaTtstMatr Then
        strSQL = "INSERT INTO TTSTMATR (codditt, instid, tt_codart, tt_magaz," & _
                 " tt_matric, tt_aammgg, tt_causale, tt_conto, tt_riferim, tt_tipork," & _
                 " tt_anno, tt_serie, tt_numdoc, tt_riga, tt_quant, tt_prezzo," & _
                 " tt_valore, tt_carscar, tt_qtacar, tt_valcar, tt_fase)" & _
                 " VALUES (" & CStrSQL(strDitta) & ", " & lIIstMatr & ", " & _
                 CStrSQL(strCodart) & ", " & lCounter & ", " & _
                 CStrSQL(strCode) & ", " & _
                 CDataSQL(NTSCDate(dttIn.Rows(i)!DtDoc)) & ", 0, " & NTSCInt(dttIn.Rows(i)!Conto) & ", " & _
                 CStrSQL(dttTmp.Rows(0).Item("tb_dest" & Right("0" & CStr(j), 2))) & ", " & _
                 CStrSQL(dttIn.Rows(i)!Tipork) & ", " & NTSCInt(dttIn.Rows(i)!Anno) & ", " & _
                 CStrSQL(dttIn.Rows(i)!Serie) & ", " & NTSCInt(dttIn.Rows(i)!Numero) & ", " & NTSCInt(dttIn.Rows(i)!RIGA) & "," & _
                 " 0, " & CDblSQL(dPrezzo) & ", " & CDblSQL(dPrezval) & "," & " 0, 0, 0, 0)"
      Else
        strSQL = "INSERT INTO TTSTMATS (codditt, instid, tt_codart, tt_magaz," & _
                 " tt_matric, tt_quant, tt_valore, tt_qtacar, tt_valcar, tt_fase," & _
                 " tt_destagl)" & _
                 " VALUES (" & CStrSQL(strDitta) & ", " & lInstmats & ", " & _
                 CStrSQL(strCodart) & ", " & lCounter & ", " & _
                 CStrSQL(strCode) & ", 0, " & CDblSQL(dPrezzo) & ", 0, " & _
                 CDblSQL(dPrezval) & ", 0, " & _
                 CStrSQL(dttTmp.Rows(0).Item("tb_dest" & Right("0" & CStr(j), 2))) & ")"
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SelezionaBarcodeTCO(ByVal strDitta As String, ByVal dttIn As DataTable, ByVal i As Integer, _
                                                ByVal strCodart As String, ByVal bTipobc As Boolean, ByVal strOrigine As String, _
                                                ByVal bArticoloTeC As Boolean, ByVal strTipoBC As String, ByVal bColli As Boolean, _
                                                ByVal bModTCO As Boolean, ByVal strTaglia As String, ByVal strUmBC As String, _
                                                ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, dttIn, i, _
                                              strCodart, bTipobc, strOrigine, _
                                              bArticoloTeC, strTipoBC, bColli, _
                                              bModTCO, strTaglia, strUmBC, _
                                              dttOut})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(12), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      Return SelezionaBarcodeTCO(strDitta, dttIn, i, _
                                  strCodart, bTipobc, strOrigine, _
                                  bArticoloTeC, strTipoBC, bColli, _
                                  bModTCO, strTaglia, strUmBC, _
                                  dttOut, False)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelezionaBarcodeTCO(ByVal strDitta As String, ByVal dttIn As DataTable, ByVal i As Integer, _
                                                  ByVal strCodart As String, ByVal bTipobc As Boolean, ByVal strOrigine As String, _
                                                  ByVal bArticoloTeC As Boolean, ByVal strTipoBC As String, ByVal bColli As Boolean, _
                                                  ByVal bModTCO As Boolean, ByVal strTaglia As String, ByVal strUmBC As String, _
                                                  ByRef dttOut As DataTable, ByVal bDaTc As Boolean) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT bc_code FROM barcode" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND bc_codart = " & CStrSQL(strCodart) & _
               NTSCStr(IIf(bModTCO AndAlso bArticoloTeC AndAlso Not bDaTc AndAlso strTaglia <> "", " AND bc_tagl = " & CStrSQL(strTaglia), "")) & _
               NTSCStr(IIf(bTipobc, " And bc_tipo = " & CStrSQL(strTipoBC), ""))
      If strOrigine = "M" Then
        If bColli Then
          'Colli
          strSQL &= " AND bc_unmis = " & CStrSQL(dttIn.Rows(i)!UMColli)
        Else
          'Quantità
          strSQL &= " AND bc_unmis = " & CStrSQL(dttIn.Rows(i)!UMQuant)
        End If
      Else
        Select Case strUmBC
          Case "P"
            strSQL &= " AND bc_unmis = " & CStrSQL(dttIn.Rows(i)!UMP)
          Case "S"
            strSQL &= " AND bc_unmis = " & CStrSQL(dttIn.Rows(i)!UMS)
          Case "C"
            strSQL &= " AND bc_unmis = " & CStrSQL(dttIn.Rows(i)!UMC)
        End Select
      End If
      If strOrigine = "L" Then
        If bModTCO And bArticoloTeC Then
          strSQL &= " AND bc_tagl = " & CStrSQL(dttIn.Rows(i)!lsa_tctaglia)
        End If
      End If
      If strOrigine = "A" And bDaTc Then
        If bModTCO And bArticoloTeC Then
          strSQL &= " AND bc_tagl = " & CStrSQL(dttIn.Rows(i)!xx_tagl)
        End If
      End If
      strSQL &= " ORDER BY bc_datins DESC, bc_code"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function InsertTlbTT(ByVal strDitta As String, ByVal bUsattstMatr As Boolean, ByVal strOrigine As String, _
                                          ByVal lIIstMatr As Integer, ByVal lIIstMats As Integer, ByVal strCodart As String, _
                                          ByVal strCode As String, ByVal dPrezzo As Decimal, ByVal dPrezval As Decimal, _
                                          ByVal dttIn As DataTable, ByVal i As Integer, ByVal bModTCO As Boolean, _
                                          ByVal strTaglia As String, ByVal lCounter As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      If bUsattstMatr And strOrigine = "M" Then
        strSQL = "INSERT INTO TTSTMATR (codditt, instid, tt_codart, tt_magaz, tt_matric," & _
                 " tt_aammgg, tt_causale, tt_conto, tt_riferim, tt_tipork, tt_anno, tt_serie," & _
                 " tt_numdoc, tt_riga, tt_quant, tt_prezzo, tt_valore, tt_carscar, tt_qtacar, tt_valcar)" & _
                 " VALUES (" & CStrSQL(strDitta) & ", " & lIIstMatr & ", " & _
                 CStrSQL(strCodart) & ", " & lCounter & ", " & CStrSQL(strCode) & ", " & _
                 CDataSQL(NTSCStr(dttIn.Rows(i)!DtDoc)) & ", 0, " & NTSCInt(dttIn.Rows(i)!Conto) & ", NULL, " & _
                 CStrSQL(dttIn.Rows(i)!Tipork) & ", " & NTSCInt(dttIn.Rows(i)!Anno) & ", " & _
                 CStrSQL(dttIn.Rows(i)!Serie) & ", " & NTSCInt(dttIn.Rows(i)!Numero) & ", " & NTSCInt(dttIn.Rows(i)!RIGA) & ", 0, " & _
                 CDblSQL(dPrezzo) & ", " & CDblSQL(dPrezval) & ", 0, 0, 0)"
      Else
        strSQL = "INSERT INTO TTSTMATS (codditt, instid, tt_codart, tt_magaz, tt_matric," & _
                 " tt_valore, tt_valcar, tt_destagl)" & _
                 " VALUES (" & CStrSQL(strDitta) & ", " & lIIstMats & ", " & CStrSQL(strCodart) & ", " & lCounter & ", " & _
                 CStrSQL(strCode) & ", " & CDblSQL(dPrezzo) & ", " & CDblSQL(dPrezval) & ", " & _
                 CStrSQL(IIf(bModTCO = True, strTaglia, "")) & ")"
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function MakeBarcode(ByVal strDitta As String, ByVal strCodart As String, ByVal dtDatdoc As Date, _
                                          ByVal strCode As String, ByVal strUmp As String, ByVal dQtaCon2 As Decimal) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO barcode (codditt, bc_codart, bc_datins, bc_code, bc_unmis," & _
               " bc_quant, bc_orins, bc_datagg, bc_oragg, bc_tipo)" & _
               " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodart) & ", " & _
               CDataSQL(dtDatdoc) & ", " & CStrSQL(strCode) & ", " & _
               CStrSQL(strUmp) & ", " & CDblSQL(dQtaCon2) & ", 0, " & _
               CDataSQL(dtDatdoc) & ", 0, " & CStrSQL("C") & ")"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function IsInBarcode(ByVal strDitta As String, ByVal strCode As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT bc_codart FROM barcode" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND bc_code = " & CStrSQL(strCode)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function DatiTermica(ByVal strDitta As String, ByVal bUsaTtstmatr As Boolean, ByVal strOrigine As String, ByVal lIIstMatr As Integer, _
                                          ByVal lIIstMats As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      If bUsaTtstmatr And strOrigine = "M" Then
        strSQL = "SELECT count(tt_matric) as NREC, tt_matric, tt_codart, ar_descr, tt_valore, tt_valcar" & _
                 " FROM TTSTMATR LEFT JOIN artico ON TTSTMATR.codditt = artico.codditt AND TTSTMATR.tt_codart = artico.ar_codart" & _
                 " WHERE TTSTMATR.codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & lIIstmatr & _
                 " GROUP BY tt_matric, tt_codart, ar_descr, tt_valore, tt_valcar"
      Else
        strSQL = "SELECT count(tt_matric) as NREC, tt_matric, tt_codart, ar_descr, tt_valore, tt_valcar" & _
                 " FROM TTSTMATS LEFT JOIN artico ON TTSTMATS.codditt = artico.codditt AND TTSTMATS.tt_codart = artico.ar_codart" & _
                 " WHERE TTSTMATS.codditt = " & CStrSQL(strDitta) & _
                 " AND instid = " & lIIstMats & _
                 " GROUP BY tt_matric, tt_codart, ar_descr, tt_valore, tt_valcar"
      End If
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function DatiTermicaDometic(ByVal strDitta As String, ByVal strOrigine As String, ByVal bUsaTtstMatr As Boolean, ByVal lIIstMatr As Integer, _
                                                 ByVal lIIstMats As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      'elaboro i dati
      If bUsaTtstMatr And strOrigine <> "L" Then
        strSQL = "SELECT count(tt_matric) as NREC, tt_matric, tt_codart, ar_descr, tt_valore, tt_valcar, tt_anno, tt_serie, tt_numdoc, tt_riga" & _
          " FROM TTSTMATR LEFT JOIN artico ON TTSTMATR.codditt = artico.codditt AND TTSTMATR.tt_codart = artico.ar_codart" & _
          " WHERE TTSTMATR.codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIIstMatr & _
          " GROUP BY tt_matric, tt_codart, ar_descr, tt_valore, tt_valcar, tt_anno, tt_serie, tt_numdoc, tt_riga"
      Else
        strSQL = "SELECT count(tt_matric) as NREC, tt_matric, tt_codart, ar_descr, tt_valore, tt_valcar" & _
          " FROM TTSTMATS LEFT JOIN artico ON TTSTMATS.codditt = artico.codditt AND TTSTMATS.tt_codart = artico.ar_codart" & _
          " WHERE TTSTMATS.codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lIIstMats & _
          " GROUP BY tt_matric, tt_codart, ar_descr, tt_valore, tt_valcar"
      End If
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "ETBA"
  Public Overridable Function ApriEtba(ByVal strDitta As String, ByVal lEtbaIITTBarcode As Integer, ByVal strCodart As String, _
                                       ByRef dsOut As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM ttstmatr" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND instid = " & lEtbaIITTBarcode & _
               " AND tt_codart= " & CStrSQL(strCodart) & _
               " ORDER BY tt_matric"

      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTSTMATR")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ScriviTabellaTTstMatr(ByVal strDitta As String, ByVal dsEtba As DataSet, ByVal lIIstMatrTmp As Integer) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer
    Try
      Dim dtrModified As DataRow() = dsEtba.Tables("TTSTMATR").Select(Nothing, Nothing, DataViewRowState.ModifiedCurrent)
      Dim dtrDeleted As DataRow() = dsEtba.Tables("TTSTMATR").Select(Nothing, Nothing, DataViewRowState.Deleted)

      For i = 0 To dtrModified.Length - 1
        strSQL = "UPDATE TTSTMATR" & _
          " SET tt_quant = " & NTSCDec(dtrModified(i)!tt_quant) & _
          " WHERE instid = " & lIIstMatrTmp & _
          " AND codditt = " & CStrSQL(dtrModified(i)!codditt) & _
          " AND tt_codart = " & CStrSQL(dtrModified(i)!tt_codart) & _
          " AND tt_matric = " & CStrSQL(dtrModified(i)!tt_matric)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      For i = 0 To dtrDeleted.Length - 1
        strSQL = "DELETE FROM TTSTMATR" & _
          " WHERE instid = " & lIIstMatrTmp & _
          " AND codditt = " & CStrSQL(dtrDeleted(i)("codditt", DataRowVersion.Original)) & _
          " AND tt_codart = " & CStrSQL(dtrDeleted(i)("tt_codart", DataRowVersion.Original)) & _
          " AND tt_matric = " & CStrSQL(dtrDeleted(i)("tt_matric", DataRowVersion.Original))
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region
End Class