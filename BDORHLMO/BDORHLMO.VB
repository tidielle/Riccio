Imports NTSInformatica.CLN__STD
Imports System.Data.Common
Imports NTSInformatica
Imports System.IO

Public Class CLDORHLMO
  Inherits CLD__BASE

  Public Overrides Sub Init(ByVal Applic As CLE__APP)
    MyBase.Init(Applic)


  End Sub

  Public Overridable Function GetData(ByVal strDitta As String, ByVal strTipork As String, ByVal bModTCO As Boolean, _
                                      ByVal lCommeca As Integer, ByVal nApprovvig As Integer, ByVal bConfermato As Boolean, _
                                      ByVal strQuery As String, ByVal strCodart As String, ByVal nFase As Integer, ByRef dsOut As DataSet) As Boolean
    Try
      Return GetData(strDitta, strTipork, bModTCO, lCommeca, nApprovvig, bConfermato, _
                     strQuery, strCodart, nFase, dsOut, False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function GetData(ByVal strDitta As String, ByVal strTipork As String, ByVal bModTCO As Boolean, _
                                      ByVal lCommeca As Integer, ByVal nApprovvig As Integer, ByVal bConfermato As Boolean, _
                                      ByVal strQuery As String, ByVal strCodart As String, ByVal nFase As Integer, ByRef dsOut As DataSet, _
                                      ByVal bNoFigliKit As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim bBD_BarcodeInDesmaga As Boolean = False
    Dim bNonApplicareFiltroFaseArticolo As Boolean = False
    Dim bIncludiPrevSuPrecOrdini As Boolean = False
    Dim i As Integer = 0
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTipork, bModTCO, lCommeca, nApprovvig, bConfermato, _
                                             strQuery, strCodart, nFase, dsOut, bNoFigliKit})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dsOut = CType(oIn(9), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      bNonApplicareFiltroFaseArticolo = CBool(Val(GetSettingBus("BSORHLMO", "Opzioni", ".", "NonApplicareFiltroFaseArticolo", "0", " ", "0")))
      bIncludiPrevSuPrecOrdini = CBool(GetSettingBusDitt(strDitta, "BSORGSOR", "OPZIONI", ".", "IncludiPrevSuPrecOrdini", "0", " ", "0"))

      strSQL = "SELECT " & GeneraCorpoQuery(bModTCO, strTipork) & _
               " WHERE " & strQuery & _
               "   AND testord.codditt = " & CStrSQL(strDitta)

      If strCodart.Trim <> "" Then
        strSQL += " AND mo_codart = " & CStrSQL(strCodart)
        If (bNonApplicareFiltroFaseArticolo = False) And (nFase >= 0) Then
          strSQL += " AND mo_fase = " & nFase.ToString
        End If
      End If
      If lCommeca <> 0 Then strSQL += " AND mo_commeca = " & lCommeca.ToString
      If nApprovvig <> 0 Then strSQL += " AND ar_codappr = " & nApprovvig.ToString
      If bConfermato Then strSQL += " AND mo_confermato = 'S'"

      If Not bIncludiPrevSuPrecOrdini Then strSQL += " AND td_tipork <> 'Q' "

      If bNoFigliKit Then strSQL &= " AND mo_ktriga = 0 "

      strSQL += " ORDER BY movord.mo_datcons, movord.mo_codart, movord.mo_fase," & _
                "          movord.mo_tipork, movord.mo_anno, movord.mo_serie, movord.mo_numord"
      dsOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORD")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetOrdiniEvasiDaMovmag(ByVal strDitta As String, ByVal strTipork As String, _
                                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                                    ByVal lNumdoc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mm_quant, mm_ortipo, mm_oranno, mm_orserie, mm_ornum, mm_orriga," & _
              " mm_salcon, mm_npnum" & _
              " FROM " & IIf(strTipork = "W", "movprb", "movmag").ToString & _
              " WHERE mm_tipork = " & CStrSQL(strTipork) & _
              " AND mm_anno = " & nAnno & _
              " AND mm_serie = " & CStrSQL(strSerie) & _
              " AND mm_numdoc = " & lNumdoc & _
              " AND codditt = " & CStrSQL(strDitta) & _
              " AND mm_ornum <> 0" & _
              " ORDER BY mm_ortipo, mm_oranno, mm_orserie, mm_ornum, mm_orriga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetArticoPadre(ByVal strDitta As String, _
                                      ByVal nAnno As Integer, ByVal strSerie As String, _
                                      ByVal lNumord As Integer, ByVal lRiga As Integer, _
                                      ByRef strCodart As String, ByRef strDesart As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strCodart = ""
      strDesart = ""
      strSQL = "SELECT mo_codart, mo_descr FROM movord" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mo_tipork = 'H'" & _
              " AND mo_anno = " & nAnno.ToString & _
              " AND mo_serie = " & CStrSQL(strSerie) & _
              " AND mo_numord = " & lNumord.ToString & _
              " AND mo_riga = " & lRiga.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strCodart = dttTmp.Rows(0)!mo_codart.ToString
        strDesart = dttTmp.Rows(0)!mo_descr.ToString
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function EsisteOrdineImpegno(ByVal strDitta As String, ByVal strTipork As String, _
    ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumord As Integer, ByVal lRiga As Integer, _
    ByRef lRigaOut As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      lRigaOut = 0
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT mo_riga FROM movord" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mo_tipork = " & CStrSQL(strTipork) & _
        " AND mo_anno = " & nAnno.ToString & _
        " AND mo_serie = " & CStrSQL(strSerie) & _
        " AND mo_numord = " & lNumord.ToString & _
        " ORDER BY codditt, mo_tipork, mo_anno, mo_serie, mo_numord, mo_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dttTmp.Rows.Count - 1)
        If NTSCInt(dttTmp.Rows(i)!mo_riga) = lRiga Then
          lRigaOut = lRiga
          Exit For
        End If
      Next
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


  Public Overridable Function TrovaFigliKit(ByVal dtrRow As DataRow, ByVal bModTco As Boolean, ByVal strTipoRk As String) As DataTable
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT " & GeneraCorpoQuery(bModTco, strTipoRk) & _
               " WHERE movord.codditt = " & CStrSQL(dtrRow!codditt) & _
               "   AND movord.mo_tipork = " & CStrSQL(dtrRow!mo_tipork) & _
               "   AND movord.mo_anno = " & NTSCInt(dtrRow!mo_anno) & _
               "   AND movord.mo_serie = " & CStrSQL(dtrRow!mo_serie) & _
               "   AND movord.mo_numord = " & NTSCInt(dtrRow!mo_numord) & _
               "   AND movord.mo_ktriga = " & NTSCInt(dtrRow!mo_riga)

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function



  Public Overridable Function GeneraCorpoQuery(ByVal bModTco As Boolean, ByVal strTipoRk As String) As String
    Dim strSQL As String = ""
    Dim i As Integer
    Dim bBD_BarcodeInDesmaga As Boolean
    Try
      bBD_BarcodeInDesmaga = CBool(Val(GetSettingBus("BSORHLMO", "Opzioni", ".", "BD_BarcodeInDesmaga", "0", " ", "0")))

      strSQL = " movord.*, td_conto as xx_conto, td_datord as xx_datord, td_annpar as xx_annpar, " & _
               " td_alfpar as xx_alfpar, td_numpar as xx_numpar, td_riferim as xx_riferim, td_codagen as xx_agente, " & _
               " ar_codappr as xx_codappr, an_descr1 as xx_desconto, ar_codalt AS xx_codalt, tb_descage as xx_desagente, tb_desappr as xx_desappr, " & _
               " tabmaga.tb_desmaga as xx_magaz, tabmaga2.tb_desmaga as xx_magaz2, co_descr1 as xx_commeca, " & _
               " mo_prezzo as xx_prezzonetto, '' as xx_articopadre, '' as xx_desarticopadre, " & _
               IIf(strTipoRk = "W", " (mo_quant - mo_quaeva - mo_quapre)", " (mo_quant - mo_quaeva)").ToString & " as xx_quadaeva, " & _
               IIf(strTipoRk = "W", " (mo_quant - mo_quaeva - mo_quapre)", " (mo_quant - mo_quaeva)").ToString & " as xx_qtares, " & _
               IIf(bBD_BarcodeInDesmaga, " mo_matric", " tabmaga.tb_desmaga").ToString & " as xx_desmaga, " & _
               " 'S' as xx_flevasdaeva, 'N' as xx_seleziona, ar_codtagl as xx_codtagl "

      If bModTco Then
        If strTipoRk = "W" Then
          'siamo su una nota di prelievo
          For i = 1 To 24
            strSQL += ", (mo_quant" & i.ToString.PadLeft(2, "0"c) & " - mo_quaeva" & i.ToString.PadLeft(2, "0"c) & "  - mo_quapre" & i.ToString.PadLeft(2, "0"c) & ") as xx_quadaeva" & i.ToString.PadLeft(2, "0"c)
          Next
        Else
          'siamo su un documento
          For i = 1 To 24
            strSQL += ", (mo_quant" & i.ToString.PadLeft(2, "0"c) & " - mo_quaeva" & i.ToString.PadLeft(2, "0"c) & ") as xx_quadaeva" & i.ToString.PadLeft(2, "0"c)
          Next
        End If
        For i = 1 To 24
          strSQL += ", mo_quant" & i.ToString.PadLeft(2, "0"c)
        Next
        For i = 1 To 24
          strSQL += ", mo_quaeva" & i.ToString.PadLeft(2, "0"c)
        Next
        For i = 1 To 24
          strSQL += ", mo_quapre" & i.ToString.PadLeft(2, "0"c)
        Next
   
        strSQL += " FROM movord " & _
                  "  LEFT JOIN movordtc ON movord.codditt = movordtc.codditt AND movord.mo_tipork = movordtc.mo_tipork " & _
                  "                    AND movord.mo_anno = movordtc.mo_anno AND movord.mo_serie = movordtc.mo_serie " & _
                  "                    AND movord.mo_numord = movordtc.mo_numord AND movord.mo_riga = movordtc.mo_riga "
      Else
        strSQL += " FROM movord"
      End If

      strSQL += " INNER JOIN testord ON testord.codditt = movord.codditt AND testord.td_tipork = movord.mo_tipork " & _
                "                   AND testord.td_anno = movord.mo_anno AND testord.td_serie = movord.mo_serie " & _
                "                   AND testord.td_numord = movord.mo_numord " & _
                " INNER JOIN anagra ON testord.codditt = anagra.codditt AND testord.td_conto = anagra.an_conto " & _
                " INNER JOIN artico ON artico.codditt = movord.codditt AND artico.ar_codart = movord.mo_codart " & _
                "  LEFT JOIN commess ON movord.codditt = commess.codditt AND movord.mo_commeca = commess.co_comme " & _
                "  LEFT JOIN tabappr ON artico.codditt = tabappr.codditt AND artico.ar_codappr = tabappr.tb_codappr " & _
                "  LEFT JOIN tabcage ON testord.codditt = tabcage.codditt AND testord.td_codagen = tabcage.tb_codcage " & _
                "  LEFT JOIN tabmaga ON movord.codditt = tabmaga.codditt AND movord.mo_magaz = tabmaga.tb_codmaga " & _
                "  LEFT JOIN tabmaga AS tabmaga2 ON movord.codditt = tabmaga2.codditt AND movord.mo_magaz2 = tabmaga2.tb_codmaga"

      Return strSQL
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
End Class
