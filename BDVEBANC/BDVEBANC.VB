Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Public Class CLDVEBANC
  Inherits CLD__BASE

  Public Overridable Function GetCondBan(ByVal strDitta As String, ByVal lCodBanc As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT condban.*, tb_descauc as xx_descauc, tb_desbanc as xx_desbanc FROM (condban" & _
        " LEFT JOIN tabbanc ON condban.codditt = tabbanc.codditt AND condban.cb_codbanc = tabbanc.tb_codbanc)" & _
        " LEFT JOIN tabcauc ON condban.cb_causale = tabcauc.tb_codcauc" & _
        " WHERE condban.codditt = " & CStrSQL(strDitta) & _
        " AND cb_codbanc = " & lCodBanc & _
        " ORDER BY cb_codbanc, cb_causale"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CONDBAN")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetCheckCondBan(ByVal strDitta As String, ByVal lCodBanc As Integer, ByVal lCodCauc As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT cb_codbanc, cb_causale FROM condban" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND cb_codbanc = " & lCodBanc & _
        " AND cb_causale = " & lCodCauc & _
        " ORDER BY cb_codbanc, cb_causale"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CONDBAN")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataValidita(ByVal lCodbanc As Integer, ByVal strDtvalid As String, ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tx_codbanc, tx_dtvalid FROM tassi" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tx_codbanc = " & lCodbanc & _
        " AND tx_dtvalid = " & CDataSQL(strDtvalid) & _
        " ORDER BY tx_codbanc, tx_dtvalid"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASSI")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetTassi(ByVal lCodbanc As Integer, ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM tassi" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tx_codbanc = " & lCodbanc & _
        " ORDER BY tx_codbanc, tx_dtvalid"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TASSI")
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaTassiCondbanc(ByVal lCodBanc As Integer, ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '----------------------------
      'cancello tutti i record non collegati a tabbanc


      strSQL = "DELETE FROM tassi" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tx_codbanc = " & lCodBanc
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM condban" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND cb_codbanc = " & lCodBanc
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreCancella(ByVal strDitta As String, ByVal nCodBanc As Integer, _
      ByRef strMsg As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strMsg = oApp.Tr(Me, 130649321264458964, "Attenzione!" & vbCrLf & _
        "Cancellazione Codice Banca '|" & nCodBanc.ToString & "|' NON possibile," & vbCrLf & _
        "perché presente in" & vbCrLf)
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 an_conto, an_descr1, an_tipo FROM anagra" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND an_codbanc = " & nCodBanc & _
        " AND an_tipo IN ('C', 'F')"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          Select Case NTSCStr(!an_tipo).ToUpper
            Case "C" : strMsg += oApp.Tr(Me, 130649322165431879, " . 'Anagrafica Clienti'" & vbCrLf)
            Case "F" : strMsg += oApp.Tr(Me, 130649323395284042, " . 'Anagrafica Fornitori'" & vbCrLf)
          End Select
          strMsg += oApp.Tr(Me, 130649323631980624, "   conto '|" & NTSCStr(!an_conto) & "|'" & _
            IIf(NTSCStr(!an_descr1).Trim <> "", " - |" & NTSCStr(!an_descr1).Trim & "|", "").ToString)
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tm_tipork, tm_anno, tm_serie, tm_numdoc FROM testmag" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tm_codbanc = " & nCodBanc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          Select Case NTSCStr(!tm_tipork).ToUpper
            Case "(" : strMsg += oApp.Tr(Me, 130649368411560596, " . 'Nota di Accredito Differita Ricevuta'" & vbCrLf)
            Case "£" : strMsg += oApp.Tr(Me, 130649368341715208, " . 'Nota di Accredito Differita Emessa'" & vbCrLf)
            Case "A" : strMsg += oApp.Tr(Me, 130649368357577740, " . 'Fattura Immediata Emessa'" & vbCrLf)
            Case "B" : strMsg += oApp.Tr(Me, 130649368387660528, " . 'D.D.T. Emesso'" & vbCrLf)
            Case "C" : strMsg += oApp.Tr(Me, 130649368434301408, " . 'Corrispettivo Emesso'" & vbCrLf)
            Case "D" : strMsg += oApp.Tr(Me, 130649368449391100, " . 'Fatture Differita Emessa'" & vbCrLf)
            Case "E" : strMsg += oApp.Tr(Me, 130649368464017088, " . 'Note di Addebito Emessa'" & vbCrLf)
            Case "F" : strMsg += oApp.Tr(Me, 130649368478488508, " . 'Ricevuta Fiscale Emessa'" & vbCrLf)
            Case "I" : strMsg += oApp.Tr(Me, 130649368492959928, " . 'Riemissione Ricevuta Fiscale'" & vbCrLf)
            Case "J" : strMsg += oApp.Tr(Me, 130649368507354060, " . 'Nota di Accredito Ricevuta'" & vbCrLf)
            Case "K" : strMsg += oApp.Tr(Me, 130649368521516344, " . 'Fattura Differita Ricevuta'" & vbCrLf)
            Case "L" : strMsg += oApp.Tr(Me, 130649368539233692, " . 'Fattura Immediata Ricevuta'" & vbCrLf)
            Case "M" : strMsg += oApp.Tr(Me, 130649368556178200, " . 'D.D.T. Ricevuto'" & vbCrLf)
            Case "N" : strMsg += oApp.Tr(Me, 130649368584174320, " . 'Nota di Accredito Emessa'" & vbCrLf)
            Case "P" : strMsg += oApp.Tr(Me, 130649368600345988, " . 'Fattura/Ricevuta Fiscale Differita'" & vbCrLf)
            Case "S" : strMsg += oApp.Tr(Me, 130649368616053952, " . 'Fattura/Ricevuta Fiscale Emessa'" & vbCrLf)
            Case "T" : strMsg += oApp.Tr(Me, 130649368630834508, " . 'Carico da Produzione'" & vbCrLf)
            Case "U" : strMsg += oApp.Tr(Me, 130649368645924200, " . 'Scarico a Produzione'" & vbCrLf)
            Case "Z" : strMsg += oApp.Tr(Me, 130649368660936612, " . 'Bolla di Movimentazione Interna'" & vbCrLf)
          End Select
          strMsg += oApp.Tr(Me, 130649368745813756, "   N° |" & NTSCStr(!tm_numdoc) & "|" & _
            IIf(NTSCStr(!tm_serie).Trim <> "", "/|" & NTSCStr(!tm_serie).Trim & "|", "").ToString & _
            " del |" & NTSCStr(!tm_anno) & "|")
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 td_tipork, td_anno, td_serie, td_numord FROM testord" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND td_codbanc = " & nCodBanc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          Select Case NTSCStr(!td_tipork).ToUpper
            Case "#" : strMsg += oApp.Tr(Me, 130649373768111021, " . 'Impegno di Commessa'" & vbCrLf)
            Case "$" : strMsg += oApp.Tr(Me, 130649373783627121, " . 'Ordine Fornitore Aperto'" & vbCrLf)
            Case "H" : strMsg += oApp.Tr(Me, 130649373800389181, " . 'Ordine di Produzione'" & vbCrLf)
            Case "O" : strMsg += oApp.Tr(Me, 130649373814970810, " . 'Ordine Fornitore'" & vbCrLf)
            Case "Q" : strMsg += oApp.Tr(Me, 130649373830253291, " . 'Preventivo'" & vbCrLf)
            Case "R" : strMsg += oApp.Tr(Me, 130649373845224283, " . 'Impegno Cliente'" & vbCrLf)
            Case "V" : strMsg += oApp.Tr(Me, 130649373860273146, " . 'Impegno cliente Aperto'" & vbCrLf)
            Case "X" : strMsg += oApp.Tr(Me, 130649373876723715, " . 'Impegno di Trasferimento'" & vbCrLf)
            Case "Y" : strMsg += oApp.Tr(Me, 130649373892006198, " . 'Impegno di Produzione'" & vbCrLf)
          End Select
          strMsg += oApp.Tr(Me, 130649373920001369, "   N° |" & NTSCStr(!td_numord) & "|" & _
            IIf(NTSCStr(!td_serie).Trim <> "", "/|" & NTSCStr(!td_serie).Trim & "|", "").ToString & _
            " del |" & NTSCStr(!td_anno) & "|")
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 sc_conto, sc_annpar, sc_alfpar, sc_numpar FROM scaden" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND sc_codbanc = " & nCodBanc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg += oApp.Tr(Me, 130649375640837257, " . 'Scadenza' N° |" & NTSCStr(!sc_numpar) & "|" & _
            IIf(NTSCStr(!sc_alfpar).Trim <> "", "/|" & NTSCStr(!sc_alfpar).Trim & "|", "").ToString & _
            " del |" & NTSCStr(!sc_annpar) & "|, conto |" & NTSCStr(!sc_conto) & "|")
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 sc_conto, sc_annpar, sc_alfpar, sc_numpar FROM scaden" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND sc_codbanc = " & nCodBanc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg += oApp.Tr(Me, 130649377565854649, " . 'Scadenza Extra contabile'" & vbCrLf & _
            "   N° |" & NTSCStr(!sc_numpar) & "|" & _
            IIf(NTSCStr(!sc_alfpar).Trim <> "", "/|" & NTSCStr(!sc_alfpar).Trim & "|", "").ToString & _
            " del |" & NTSCStr(!sc_annpar) & "|, conto |" & NTSCStr(!sc_conto) & "|")
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 pn_datreg, pn_numreg FROM imppn" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND pn_codbanc = " & nCodBanc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg += oApp.Tr(Me, 130649378151779048, " . 'Registrazione di Prima Nota'" & vbCrLf & _
            "   N° |" & NTSCStr(!pn_numreg) & "| del |" & NTSCStr(!pn_datreg) & "|")
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tm_anno, tm_serie, tm_numdoc FROM testpar" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tm_codbanc = " & nCodBanc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg += oApp.Tr(Me, 130649380201688668, " . 'Parcella'" & vbCrLf & _
            "   N° |" & NTSCStr(!tm_numdoc) & "|" & _
            IIf(NTSCStr(!tm_serie).Trim <> "", "/|" & NTSCStr(!tm_serie).Trim & "|", "").ToString & _
            " del |" & NTSCStr(!tm_anno) & "|")
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tf_anno, tf_mese, tf_num FROM testf24" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tf_codbanc = " & nCodBanc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg += oApp.Tr(Me, 130649383316769597, " . 'F24'" & vbCrLf & _
            "   N° |" & NTSCStr(!tf_num) & "| del |" & NTSCStr(!tf_anno) & "| mese di ")
          Select Case NTSCInt(!tf_mese)
            Case 1 : strMsg += oApp.Tr(Me, 130649384444984844, "Gennaio" & vbCrLf)
            Case 2 : strMsg += oApp.Tr(Me, 130649384461752591, "Febbraio" & vbCrLf)
            Case 3 : strMsg += oApp.Tr(Me, 130649384476754293, "Marzo" & vbCrLf)
            Case 4 : strMsg += oApp.Tr(Me, 130649384490669196, "Aprile" & vbCrLf)
            Case 5 : strMsg += oApp.Tr(Me, 130649384505049870, "Maggio" & vbCrLf)
            Case 6 : strMsg += oApp.Tr(Me, 130649384519585801, "Giugno" & vbCrLf)
            Case 7 : strMsg += oApp.Tr(Me, 130649384539400470, "Luglio" & vbCrLf)
            Case 8 : strMsg += oApp.Tr(Me, 130649384553703520, "Agosto" & vbCrLf)
            Case 9 : strMsg += oApp.Tr(Me, 130649384571887992, "Settembre" & vbCrLf)
            Case 10 : strMsg += oApp.Tr(Me, 130649384591178663, "Ottobre" & vbCrLf)
            Case 11 : strMsg += oApp.Tr(Me, 130649384610139420, "Novembre" & vbCrLf)
            Case 12 : strMsg += oApp.Tr(Me, 130649384626305550, "Dicembre" & vbCrLf)
          End Select
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tc_anno, tc_numcom FROM testcom" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tc_codbanc = " & nCodBanc
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        With dttTmp.Rows(0)
          strMsg += oApp.Tr(Me, 130649386977680223, " . 'Comanda GPV'" & vbCrLf & _
            "   N° |" & NTSCStr(!tc_numcom) & "| del |" & NTSCStr(!tc_anno) & "|")
        End With
        Return False
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

End Class
