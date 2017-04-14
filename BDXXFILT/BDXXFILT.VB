Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Public Class CLDXXFILT
  Inherits CLD__BASE

  Public Overridable Function CaricaCampiTabelle(ByVal strDitta As String, ByVal arTabelle As ArrayList, _
    ByRef dttOut As DataTable) As Boolean
    Dim bRigheAggiunte As Boolean = False
    Dim i As Integer = 0
    Dim nOrdinalPosition As Integer = 0
    Dim strSQL As String = ""
    Dim strTabelle As String = ""
    Dim strNomProgramma As String = ""
    Dim dttTmp As New DataTable
    Dim dtrRow() As DataRow

    Try
      '-------------------------------------------------------------------------------------------------------------
      For Each strTabella As String In arTabelle
        strTabelle &= CStrSQL(strTabella) & ","
      Next
      '-------------------------------------------------------------------------------------------------------------
      If strTabelle.Length > 0 Then
        strTabelle = strTabelle.Remove(strTabelle.Length - 1)
      Else
        strTabelle = "''" 'Così la query non da errore ma non ritorna neanche nessun valore.
      End If
      '-------------------------------------------------------------------------------------------------------------
      '--- Tipo Campo = 11 -> Oggetti Ole
      '-------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT campi.*, tabelle.cb_destab FROM campi " & _
               " INNER JOIN tabelle ON campi.cb_nomtab = tabelle.cb_nomtab " & _
               " WHERE campi.cb_nomtab IN (" & strTabelle & ") " & _
               "   AND cb_tipocampo <> 11 "
      If FRIENDLY = True Then
        strSQL &= " AND (cb_desgrid IS NULL OR cb_desgrid IN ('F', ''))"
      Else
        strSQL &= " AND (cb_desgrid IS NULL OR cb_desgrid IN ('B', ''))"
      End If
      strSQL &= " ORDER BY campi.cb_nomtab, cb_ordinalposition "
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      '-------------------------------------------------------------------------------------------------------------
      '--- Se presente la tabella 'ARTICO', aggiunge i campi relativi alle estensioni anagrafiche
      '--- (dipendentemente la tabella TABAEXT)
      '-------------------------------------------------------------------------------------------------------------
      If InStr(strTabelle.ToLower, "artico") > 0 Then
        '-----------------------------------------------------------------------------------------------------------
        strSQL = "SELECT MAX(cb_ordinalposition) AS Num FROM campi" & _
          " WHERE LOWER(cb_nomtab) = 'artico'"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
        If dttTmp.Rows.Count > 0 Then nOrdinalPosition = NTSCInt(dttTmp.Rows(0)!Num)
        dttTmp.Clear()
        dttTmp.Dispose()
        '-----------------------------------------------------------------------------------------------------------
        strSQL = "SELECT * FROM tabaext" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND tb_tipork = 'A'"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          '---------------------------------------------------------------------------------------------------------
          strNomProgramma = "Anagrafica Articoli"
          bRigheAggiunte = True
          '---------------------------------------------------------------------------------------------------------
          For i = 1 To 3
            If NTSCStr(dttTmp.Rows(0)("tb_tipo" & i.ToString)).Trim <> "" Then
              AggiungiRighe(10, 30, nOrdinalPosition, "artico", NTSCStr(dttTmp.Rows(0)("tb_tipo" & i.ToString)), _
                "ar_xtipo" & i.ToString, "N", "N", strNomProgramma, dttOut)
            End If
          Next
          '---------------------------------------------------------------------------------------------------------
          For i = 1 To 10
            If NTSCStr(dttTmp.Rows(0)("tb_descr" & i.ToString)).Trim <> "" Then
              AggiungiRighe(10, 30, nOrdinalPosition, "artico", NTSCStr(dttTmp.Rows(0)("tb_descr" & i.ToString)), _
                "ar_xdescr" & i.ToString, "N", "N", strNomProgramma, dttOut)
            End If
          Next
          '---------------------------------------------------------------------------------------------------------
          For i = 1 To 3
            If NTSCStr(dttTmp.Rows(0)("tb_desext" & i.ToString)).Trim <> "" Then
              AggiungiRighe(10, 30, nOrdinalPosition, "artico", NTSCStr(dttTmp.Rows(0)("tb_desext" & i.ToString)), _
                "ar_xdesext" & i.ToString, "N", "N", strNomProgramma, dttOut)
            End If
          Next
          '---------------------------------------------------------------------------------------------------------
          For i = 1 To 2
            If NTSCStr(dttTmp.Rows(0)("tb_memo" & i.ToString)).Trim <> "" Then
              AggiungiRighe(12, 0, nOrdinalPosition, "artico", NTSCStr(dttTmp.Rows(0)("tb_memo" & i.ToString)), _
                "ar_xmemo" & i.ToString, "N", "N", strNomProgramma, dttOut)
            End If
          Next
          '---------------------------------------------------------------------------------------------------------
          For i = 1 To 5
            If NTSCStr(dttTmp.Rows(0)("tb_data" & i.ToString)).Trim <> "" Then
              AggiungiRighe(8, 8, nOrdinalPosition, "artico", NTSCStr(dttTmp.Rows(0)("tb_data" & i.ToString)), _
                "ar_xdata" & i.ToString, "N", "N", strNomProgramma, dttOut)
            End If
          Next
          '---------------------------------------------------------------------------------------------------------
          For i = 1 To 10
            If NTSCStr(dttTmp.Rows(0)("tb_num" & i.ToString)).Trim <> "" Then
              AggiungiRighe(7, 8, nOrdinalPosition, "artico", NTSCStr(dttTmp.Rows(0)("tb_num" & i.ToString)), _
                "ar_xnum" & i.ToString, "N", "N", strNomProgramma, dttOut)
            End If
          Next
          '---------------------------------------------------------------------------------------------------------
          For i = 1 To 10
            If NTSCStr(dttTmp.Rows(0)("tb_check" & i.ToString)).Trim <> "" Then
              AggiungiRighe(10, 1, nOrdinalPosition, "artico", NTSCStr(dttTmp.Rows(0)("tb_check" & i.ToString)), _
                "ar_xcheck" & i.ToString, "N", "N", strNomProgramma, dttOut)
            End If
          Next
          '---------------------------------------------------------------------------------------------------------
          For i = 1 To 3
            If NTSCStr(dttTmp.Rows(0)("tb_combo" & i.ToString)).Trim <> "" Then
              AggiungiRighe(10, 1, nOrdinalPosition, "artico", NTSCStr(dttTmp.Rows(0)("tb_combo" & i.ToString)), _
                "ar_xcombo" & i.ToString, "N", "N", strNomProgramma, dttOut)
            End If
          Next
          '---------------------------------------------------------------------------------------------------------
        End If
        dttOut.AcceptChanges()
      End If
      '-------------------------------------------------------------------------------------------------------------
      If bRigheAggiunte = True Then
        dttTmp = dttOut.Copy
        dtrRow = dttTmp.Select("", "cb_nomtab, cb_ordinalposition")
        dttOut.Clear()
        For i = 0 To (dtrRow.Length - 1)
          dttOut.ImportRow(dtrRow(i))
        Next
        dttOut.AcceptChanges()
      End If
      '-------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function GeneraQuerySQL(ByVal dtrFiltri() As DataRow) As String
    Dim strSQL As String = ""
    Try
      For Each dtrFiltro As DataRow In dtrFiltri
        If NTSCStr(dtrFiltro!xx_valore) = "" Then Continue For

        strSQL &= NTSCStr(dtrFiltro!xx_nome)

        'Pepara la query per il report, in base ai filtri selezionati, al tipo di filtro e al report
        Select Case NTSCStr(dtrFiltro!xx_tipo)
          Case "=", "<>", "<", "<=", ">", ">="
            strSQL &= " " & NTSCStr(dtrFiltro!xx_tipo) & " " & ConvertiValoreSQL(NTSCStr(dtrFiltro!xx_valore), NTSCInt(dtrFiltro!xx_tipocampo))
          Case "IN", "NOT IN"
            strSQL &= " " & NTSCStr(dtrFiltro!xx_tipo) & " ("
            For Each strValore As String In NTSCStr(dtrFiltro!xx_valore).Split(";"c)
              strSQL &= ConvertiValoreSQL(strValore.Trim, NTSCInt(dtrFiltro!xx_tipocampo)) & ","
            Next
            strSQL = strSQL.Remove(strSQL.Length - 1) 'Rimuove l'ultima virgola
            strSQL &= ")"
          Case "BET"
            strSQL &= " BETWEEN " & ConvertiValoreSQL(NTSCStr(dtrFiltro!xx_valore), NTSCInt(dtrFiltro!xx_tipocampo)) & _
                          " AND " & ConvertiValoreSQL(NTSCStr(dtrFiltro!xx_valorea), NTSCInt(dtrFiltro!xx_tipocampo))
          Case "NOT BET"
            strSQL &= " NOT BETWEEN " & ConvertiValoreSQL(NTSCStr(dtrFiltro!xx_valore), NTSCInt(dtrFiltro!xx_tipocampo)) & _
                              " AND " & ConvertiValoreSQL(NTSCStr(dtrFiltro!xx_valorea), NTSCInt(dtrFiltro!xx_tipocampo))
          Case "START"
            strSQL &= " LIKE " & ConvertiValoreSQL(NTSCStr(dtrFiltro!xx_valore) & "%", NTSCInt(dtrFiltro!xx_tipocampo))
          Case "NOT START"
            strSQL &= " NOT LIKE " & ConvertiValoreSQL(NTSCStr(dtrFiltro!xx_valore) & "%", NTSCInt(dtrFiltro!xx_tipocampo))
          Case "LIKE", "NOT LIKE"
            strSQL &= " " & NTSCStr(dtrFiltro!xx_tipo) & " " & ConvertiValoreSQL("%" & NTSCStr(dtrFiltro!xx_valore) & "%", NTSCInt(dtrFiltro!xx_tipocampo))
        End Select

        strSQL &= " AND "
      Next

      If strSQL.Length > 0 Then strSQL = strSQL.Remove(strSQL.Length - 4)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return strSQL
  End Function

  Public Overridable Function ConvertiValoreSQL(ByVal strValore As String, ByVal lTipo As Integer) As String
    Dim strSQL As String = ""
    Try
      Select Case lTipo
        Case 3, 4 'SmallInt/Int
          Return strValore
        Case 5, 6, 7 'Money,Real e Float
          Return CDblSQL(strValore).ToString
        Case 8 'Data
          Return CDataSQL(strValore).ToString
        Case 10, 12 'Stringa, Memo
          Return CStrSQL(strValore)
        Case Else
          Return strValore
      End Select
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return ""
  End Function

  Public Overridable Function AggiungiRighe(ByVal nTipoCampo As Integer, ByVal nSize As Integer, _
    ByRef nOrdinalPosition As Integer, ByVal strNomTab As String, ByVal strDescrCampo As String, _
    ByVal strNomCampo As String, ByVal strPk As String, ByVal strPers As String, ByVal strNomProgramma As String, _
    ByRef dttOut As DataTable) As Boolean
    Try
      '-------------------------------------------------------------------------------------------------------------
      nOrdinalPosition += 1
      '-------------------------------------------------------------------------------------------------------------
      dttOut.Rows.Add(dttOut.NewRow)
      '-------------------------------------------------------------------------------------------------------------
      With dttOut.Rows(dttOut.Rows.Count - 1)
        !cb_tipocampo = nTipoCampo
        !cb_size = nSize
        !cb_required = -1
        !cb_ordinalposition = nOrdinalPosition
        !cb_descampo = strNomTab & " - " & strDescrCampo
        !cb_desgrid = ""
        !cb_nomcampo = strNomTab & "." & strNomCampo
        !cb_nomtab = strNomTab
        !cb_pk = strPk
        !cb_pers = strPers
        !cb_destab = strNomTab & " - " & strNomProgramma
      End With
      '-------------------------------------------------------------------------------------------------------------
      dttOut.AcceptChanges()
      '-------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

End Class
