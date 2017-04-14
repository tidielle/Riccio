Imports NTSInformatica.CLN__STD
Imports System.Data.Common

Imports NTSInformatica
Imports System.IO

Public Class CLDMGARMD
  Inherits CLD__BASE

  Public Overridable Function GetDataArma(ByVal strDitta As String, ByVal strArmaCodart As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artmaga.*, tabmaga.tb_desmaga as xx_codmaga, artfasi.af_descr as xx_fase" & _
        " FROM (artmaga" & _
        " LEFT JOIN tabmaga ON artmaga.codditt = tabmaga.codditt AND artmaga.am_codmaga = tabmaga.tb_codmaga)" & _
        " LEFT JOIN artfasi ON artmaga.codditt = artfasi.codditt AND artmaga.am_codart = artfasi.af_codart AND artmaga.am_fase = artfasi.af_fase" & _
        " WHERE artmaga.codditt = " & CStrSQL(strDitta) & _
        " AND am_codart = " & CStrSQL(strArmaCodart) & _
        " ORDER BY artmaga.codditt, am_codart, am_codmaga"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTMAGA")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataCkit(ByVal strDitta As String, ByVal strCkitCodart As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artkit.*, artico.ar_descr as xx_codfigli" & _
        " FROM artkit" & _
        " LEFT JOIN artico ON artkit.codditt = artico.codditt AND artkit.ak_codfigli = artico.ar_codart" & _
        " WHERE artkit.codditt = " & CStrSQL(strDitta) & _
        " AND ak_codart = " & CStrSQL(strCkitCodart) & _
        " ORDER BY artkit.codditt, ak_riga"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTKIT")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetMaxArtkit(ByVal strDitta As String, ByVal strCodkit As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Max(ak_riga) As Riga FROM artkit" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ak_codart = " & CStrSQL(strCodkit)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTKIT")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataFasi(ByVal strDitta As String, ByVal strFasiCodart As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artfasi.*, tablavo.tb_deslavo as xx_codlavo," & _
        " tabtcdc.tb_destcdc AS xx_codtcdc, tabdica.tb_desdica AS xx_coddica, tabdicv.tb_desdicv AS xx_coddicv " & _
        " FROM artfasi" & _
        " LEFT JOIN tablavo ON artfasi.codditt = tablavo.codditt AND artfasi.af_codlavo = tablavo.tb_codlavo" & _
        " LEFT JOIN tabtcdc ON artfasi.af_codtcdc = tabtcdc.tb_codtcdc" & _
        " LEFT JOIN tabdica ON artfasi.af_coddica = tabdica.tb_coddica" & _
        " LEFT JOIN tabdicv ON artfasi.codditt = tabdicv.codditt AND artfasi.af_coddica = tabdicv.tb_coddica AND artfasi.af_coddicv = tabdicv.tb_coddicv" & _
        " WHERE artfasi.codditt = " & CStrSQL(strDitta) & _
        " AND af_codart = " & CStrSQL(strFasiCodart) & _
        " ORDER BY artfasi.codditt, af_fase"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTFASI")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function IsArtFasiDeletable(ByVal strDitta As String, ByVal strCodart As String, _
                                               ByVal nFase As Integer, ByVal bRoot As Boolean, _
                                               ByRef strMsgOut As String) As Boolean

    'im vb6 IsArtFasiDeletableDitt
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim i As Integer
    Dim strCampo As String
    Try
      If bRoot = False Then
        strSQL = "SELECT TOP 1 adx_codart FROM artdefx" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND adx_codart = " & CStrSQL(strCodart) & _
          " AND adx_fase = " & nFase
      Else
        strSQL = "SELECT TOP 1 adx_codart" & _
          " FROM (artdefx INNER JOIN artfasi ON artdefx.codditt = artfasi.codditt AND artdefx.adx_codart = artfasi.af_codart AND artdefx.adx_fase = artfasi.af_fase)" & _
          " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE artdefx.codditt = " & CStrSQL(strDitta) & _
          " AND ar_codroot = " & CStrSQL(strCodart) & _
          " AND af_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566160710230016, "Attenzione!" & vbCrLf & _
            "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
            "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
            "'Progressivi definitivi articoli'")
        Return False
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 lp_codart FROM listprod" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND lp_codart = " & CStrSQL(strCodart) & _
          " AND lp_fase = " & nFase
      Else
        strSQL = "SELECT TOP 1 lp_codart" & _
          " FROM (listprod INNER JOIN artfasi ON listprod.codditt = artfasi.codditt AND listprod.lp_codart = artfasi.af_codart AND listprod.lp_fase = artfasi.af_fase)" & _
          " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE listprod.codditt = " & CStrSQL(strDitta) & _
          " AND ar_codroot = " & CStrSQL(strCodart) & _
          " AND af_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566160745689913, "Attenzione!" & vbCrLf & _
            "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
            "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
            "'Archivio Liste di Produzione'")
        Return False
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 md_codfigli FROM movdis" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND md_codfigli = " & CStrSQL(strCodart) & _
          " AND md_fasefigli = " & nFase
      Else
        strSQL = "SELECT TOP 1 md_codfigli" & _
          " FROM (movdis INNER JOIN artfasi ON movdis.codditt = artfasi.codditt AND movdis.md_codfigli = artfasi.af_codart AND movdis.md_fasefigli = artfasi.af_fase)" & _
          " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE movdis.codditt = " & CStrSQL(strDitta) & _
          " AND ar_codroot = " & CStrSQL(strCodart) & _
          " AND af_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566160784586452, "Attenzione!" & vbCrLf & _
          "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
          "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
          "'Archivio Distinta Base'")
        Return False
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 mm_codart FROM movmag" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND mm_codart = " & CStrSQL(strCodart) & _
          " AND mm_fase = " & nFase
      Else
        strSQL = "SELECT TOP 1 mm_codart" & _
          " FROM (movmag INNER JOIN artfasi ON movmag.codditt = artfasi.codditt AND movmag.mm_codart = artfasi.af_codart AND movmag.mm_fase = artfasi.af_fase)" & _
          " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE movmag.codditt = " & CStrSQL(strDitta) & _
          " AND ar_codroot = " & CStrSQL(strCodart) & _
          " AND af_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566160819265294, "Attenzione!" & vbCrLf & _
          "L'articolo '|" & strCodart & "|' - Fase '|" & CStr(nFase) & "|'" & vbCrLf & _
          "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
          "'Archivio Documenti di Magazzino'")
        Return False
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 mo_codart FROM movord" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND mo_codart = " & CStrSQL(strCodart) & _
          " AND mo_fase = " & nFase
      Else
        strSQL = "SELECT TOP 1 mo_codart" & _
          " FROM (movord INNER JOIN artfasi ON movord.codditt = artfasi.codditt AND movord.mo_codart = artfasi.af_codart AND movord.mo_fase = artfasi.af_fase)" & _
          " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE movord.codditt = " & CStrSQL(strDitta) & _
          " AND ar_codroot = " & CStrSQL(strCodart) & _
          " AND af_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566160861286053, "Attenzione!" & vbCrLf & _
          "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
          "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
          "'Archivio Ordini/Impegni/Preventivi'")
        Return False
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 mm_codart FROM movprb" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND mm_codart = " & CStrSQL(strCodart) & _
          " AND mm_fase = " & nFase
      Else
        strSQL = "SELECT TOP 1 mm_codart" & _
          " FROM (movprb INNER JOIN artfasi ON movprb.codditt = artfasi.codditt AND movprb.mm_codart = artfasi.af_codart AND movprb.mm_fase = artfasi.af_fase)" & _
          " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE movprb.codditt = " & CStrSQL(strDitta) & _
          " AND ar_codroot = " & CStrSQL(strCodart) & _
          " AND af_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566160887998134, "Attenzione!" & vbCrLf & _
          "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
          "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
          "'Archivio Documenti di Carico/Scarico Magazzino'")
        Return False
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 ol_codart FROM ordlist" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ol_codart = " & CStrSQL(strCodart) & _
          " AND ol_fase = " & nFase
      Else
        strSQL = "SELECT TOP 1 ol_codart" & _
          " FROM (ordlist INNER JOIN artfasi ON ordlist.codditt = artfasi.codditt AND ordlist.ol_codart = artfasi.af_codart AND ordlist.ol_fase = artfasi.af_fase)" & _
          " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE ordlist.codditt = " & CStrSQL(strDitta) & _
          " AND ar_codroot = " & CStrSQL(strCodart) & _
          " AND af_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566160941422296, "Attenzione!" & vbCrLf & _
         "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
         "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
         "'Archivio Proposte d'Ordine'")
        Return False
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 nc_codart FROM rappnc" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND nc_codart = " & CStrSQL(strCodart) & _
          " AND nc_fase = " & nFase
      Else
        strSQL = "SELECT TOP 1 nc_codart" & _
          " FROM (rappnc INNER JOIN artfasi ON rappnc.codditt = artfasi.codditt AND rappnc.nc_codart = artfasi.af_codart AND rappnc.nc_fase = artfasi.af_fase)" & _
          " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE rappnc.codditt = " & CStrSQL(strDitta) & _
          " AND ar_codroot = " & CStrSQL(strCodart) & _
          " AND af_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566160974382817, "Attenzione!" & vbCrLf & _
          "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
          "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
          "'Archivio Rapporti di Non Conformità (Q)'")
        Return False
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 sv_codart FROM statven" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND sv_codart = " & CStrSQL(strCodart) & _
          " AND sv_fase = " & nFase
      Else
        strSQL = "SELECT TOP 1 sv_codart" & _
          " FROM (statven INNER JOIN artfasi ON statven.codditt = artfasi.codditt AND statven.sv_codart = artfasi.af_codart AND statven.sv_fase = artfasi.af_fase)" & _
          " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE statven.codditt = " & CStrSQL(strDitta) & _
          " AND ar_codroot = " & CStrSQL(strCodart) & _
          " AND af_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566161014529044, "Attenzione!" & vbCrLf & _
          "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
          "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
          "'Archivio Statistiche Vendite/Acquisti'")
        Return False
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 tsk_codart FROM tasks" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND tsk_codart = " & CStrSQL(strCodart) & _
          " AND tsk_fase = " & nFase
      Else
        strSQL = "SELECT TOP 1 tsk_codart" & _
          " FROM (tasks INNER JOIN artfasi ON tasks.codditt = artfasi.codditt AND tasks.tsk_codart = artfasi.af_codart AND tasks.tsk_fase = artfasi.af_fase)" & _
          " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE tasks.codditt = " & CStrSQL(strDitta) & _
          " AND ar_codroot = " & CStrSQL(strCodart) & _
          " AND af_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566161052332106, "Attenzione!" & vbCrLf & _
          "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
          "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
          "'Archivio Tasks delle Commesse (Gestione Progetti)'")
        Return False
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 mo_codart FROM movoff" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND mo_codart = " & CStrSQL(strCodart) & _
          " AND mo_fase = " & nFase
      Else
        strSQL = "SELECT TOP 1 mo_codart" & _
          " FROM (movoff INNER JOIN artfasi ON movoff.codditt = artfasi.codditt AND movoff.mo_codart = artfasi.af_codart AND movoff.mo_fase = artfasi.af_fase)" & _
          " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE movoff.codditt = " & CStrSQL(strDitta) & _
          " AND ar_codroot = " & CStrSQL(strCodart) & _
          " AND af_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566161091384856, "Attenzione!" & vbCrLf & _
          "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
          "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
          "'Archivio Offerte'")
        Return False
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 lc_codart FROM listini" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND lc_codart = " & CStrSQL(strCodart) & _
          " AND lc_fase = " & nFase
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
        If dsTmp.Tables("TMP").Rows.Count > 0 Then
          dsTmp.Tables.Clear()
          strMsgOut = oApp.Tr(Me, 128566161117784515, "Attenzione!" & vbCrLf & _
            "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
            "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
            "'Archivio Listini'")
          Return False
        End If
      Else
        For i = 1 To 2
          If i = 1 Then strCampo = "ar_codart" Else strCampo = "ar_codroot"
          strSQL = "SELECT TOP 1 lc_codart" & _
            " FROM (listini INNER JOIN artfasi ON listini.codditt = artfasi.codditt AND listini.lc_codart = artfasi.af_codart AND listini.lc_fase = artfasi.af_fase)" & _
            " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico." & strCampo & " = artfasi.af_codart" & _
            " WHERE listini.codditt = " & CStrSQL(strDitta) & _
            " AND ar_codroot = " & CStrSQL(strCodart) & _
            " AND af_fase = " & nFase
          dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
          If dsTmp.Tables("TMP").Rows.Count > 0 Then
            dsTmp.Tables.Clear()
            strMsgOut = oApp.Tr(Me, 128566161146214917, "Attenzione!" & vbCrLf & _
              "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
              "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
              "'Archivio Listini'")
            Return False
          End If
        Next
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 bc_codart FROM barcode" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND bc_codart = " & CStrSQL(strCodart) & _
          " AND bc_fase = " & nFase
      Else
        strSQL = "SELECT TOP 1 bc_codart" & _
          " FROM (barcode INNER JOIN artfasi ON barcode.codditt = artfasi.codditt AND barcode.bc_codart = artfasi.af_codart AND barcode.bc_fase = artfasi.af_fase)" & _
          " INNER JOIN artico ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE barcode.codditt = " & CStrSQL(strDitta) & _
          " AND ar_codroot = " & CStrSQL(strCodart) & _
          " AND af_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566161181674814, "Attenzione!" & vbCrLf & _
          "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
          "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
          "'Archivio Barcode'")
        Return False
      End If

      If bRoot = False Then
        strSQL = "SELECT TOP 1 dd_coddb" & _
          " FROM (cicli INNER JOIN artico ON cicli.codditt = artico.codditt AND cicli.dd_coddb = artico.ar_coddb)" & _
          " INNER JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart" & _
          " WHERE cicli.codditt = " & CStrSQL(strDitta) & _
          " AND artico.ar_codart = " & CStrSQL(strCodart) & _
          " AND cicli.dd_fase = " & nFase
      Else
        strSQL = "SELECT TOP 1 dd_coddb" & _
          " FROM (cicli INNER JOIN artico ON cicli.codditt = artico.codditt AND cicli.dd_coddb = artico.ar_coddb)" & _
          " INNER JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codroot = artfasi.af_codart" & _
          " WHERE cicli.codditt = " & CStrSQL(strDitta) & _
          " AND artico.ar_codroot = " & CStrSQL(strCodart) & _
          " AND cicli.dd_fase = " & nFase
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        dsTmp.Tables.Clear()
        strMsgOut = oApp.Tr(Me, 128566161228538114, "Attenzione!" & vbCrLf & _
          "L'articolo '|" & strCodart & "|' - Fase '|" & NTSCStr(nFase) & "|'" & vbCrLf & _
          "NON può essere cancellato in quanto presente in:" & vbCrLf & " --> " & _
          "'Fasi di Lavorazione'")
        Return False
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetDataHlap(ByVal strDitta As String, ByVal strCkitCodart As String, _
                                          ByVal nHlapFase As Decimal, ByVal strElencoCodici As String, _
                                          ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artpro.*, tabmaga.tb_desmaga as xx_magaz" & _
        " FROM artpro" & _
        " LEFT JOIN tabmaga ON artpro.codditt = tabmaga.codditt AND artpro.ap_magaz = tabmaga.tb_codmaga" & _
        " WHERE artpro.codditt = " & CStrSQL(strDitta) & _
        " AND ap_codart = " & CStrSQL(strCkitCodart)
      If nHlapFase >= 0 Then strSQL += " AND ap_fase = " & nHlapFase
      If strElencoCodici <> "" Then strSQL = strSQL & " AND ap_magaz IN (" & strElencoCodici & ")"
      strSQL = strSQL & " ORDER BY artpro.codditt, ap_codart, ap_magaz"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPRO")

      With ds.Tables("ARTPRO").Columns
        .Add(New DataColumn("xx_disp", GetType(Decimal)))
        .Add(New DataColumn("xx_dispnet", GetType(Decimal)))
        .Add(New DataColumn("xx_totcarichi", GetType(Decimal)))
        .Add(New DataColumn("xx_totscarichi", GetType(Decimal)))
        .Add(New DataColumn("xx_ordes", GetType(Decimal)))
        .Add(New DataColumn("xx_ordap", GetType(Decimal)))
        .Add(New DataColumn("xx_impeges", GetType(Decimal)))
        .Add(New DataColumn("xx_impegap", GetType(Decimal)))
        .Add(New DataColumn("xx_dispones", GetType(Decimal)))
      End With

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function HlapGetQta(ByVal strDitta As String, ByVal strHlapCodart As String, _
                                        ByVal strMo_flevas As String, ByVal strMo_tipork As String, _
                                        ByVal strAp_magaz As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Sum(mo_quant - mo_quaeva) As Quantita FROM movord" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mo_codart = " & CStrSQL(strHlapCodart) & _
        " AND mo_flevas = " & CStrSQL(strMo_flevas) & _
        " AND mo_tipork = " & CStrSQL(strMo_tipork) & _
        " AND mo_magaz = " & strAp_magaz

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORD")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataHlat(ByVal strDitta As String, ByVal strHlatCodart As String, _
                                        ByVal dHlatFase As Decimal, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artprox.* FROM artprox" & _
        " WHERE artprox.codditt = " & CStrSQL(strDitta) & _
        " AND apx_codart = " & CStrSQL(strHlatCodart) & _
        " AND apx_fase = " & dHlatFase & _
        " ORDER BY artprox.codditt, apx_codart"
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPROX")

      With ds.Tables("ARTPROX").Columns
        .Add(New DataColumn("xx_dispnet", GetType(Decimal)))
        .Add(New DataColumn("xx_dispesec", GetType(Decimal)))
        .Add(New DataColumn("xx_disp", GetType(Decimal)))
        .Add(New DataColumn("xx_costmed", GetType(Decimal)))
        .Add(New DataColumn("xx_vimpegap", GetType(Decimal)))
        .Add(New DataColumn("xx_vimpeg", GetType(Decimal)))
        .Add(New DataColumn("xx_vordinap", GetType(Decimal)))
        .Add(New DataColumn("xx_vordin", GetType(Decimal)))
        .Add(New DataColumn("xx_impegap", GetType(Decimal)))
        .Add(New DataColumn("xx_impeg", GetType(Decimal)))
        .Add(New DataColumn("xx_ordinap", GetType(Decimal)))
        .Add(New DataColumn("xx_ordin", GetType(Decimal)))
      End With

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function HlatGetQta(ByVal strDitta As String, ByVal strHlatCodart As String, _
                                      ByVal strMo_flevas As String, ByVal strMo_tipork As String, _
                                      ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT Sum(mo_quant - mo_quaeva) As Quantita, Sum(mo_valore) As Valore FROM movord" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mo_codart = " & CStrSQL(strHlatCodart) & _
        " AND mo_flevas = " & CStrSQL(strMo_flevas) & _
        " AND mo_tipork = " & CStrSQL(strMo_tipork)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MOVORD")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataBarc(ByVal strDitta As String, ByVal strBarcCodart As String, _
                                          ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT barcode.*, artfasi.af_descr as xx_fase" & _
        " FROM barcode" & _
        " LEFT JOIN artfasi ON barcode.codditt = artfasi.codditt AND barcode.bc_codart = artfasi.af_codart AND barcode.bc_fase = artfasi.af_fase" & _
        " WHERE barcode.codditt = " & CStrSQL(strDitta) & _
        " AND bc_codart = " & CStrSQL(strBarcCodart) & _
        " ORDER BY barcode.codditt, bc_datins"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "BARCODE")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetTabtagl(ByVal strDitta As String, ByVal nBarcCodtagl As Integer, _
                                        ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tabtagl.* FROM tabtagl" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_codtagl = " & nBarcCodtagl

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABTAGL")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function InsertBarcode(ByVal strDitta As String, ByVal strBarcCodart As String, _
                                         ByVal strDummy As String, ByVal strBarcUnmis As String, _
                                         ByVal lOrins As Integer, ByVal strQuale As String, _
                                         ByVal strCampo As String, ByRef nRec As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO BARCODE (codditt, bc_codart, bc_datins, bc_code," & _
        " bc_unmis, bc_quant, bc_orins, bc_datagg, bc_oragg, bc_tipo, bc_tagl)" & _
        " VALUES (" & CStrSQL(strDitta) & ", " & _
          CStrSQL(strBarcCodart) & ", " & CDataOraSQL(Now) & ", " & _
          CStrSQL(strDummy) & ", " & CStrSQL(strBarcUnmis) & ", 1, " & _
          lOrins & ", " & CDataOraSQL(Now) & ", " & lOrins & ", " & _
          CStrSQL(strQuale) & ", " & _
          CStrSQL(strCampo) & ")"

      nRec = Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataHlav(ByVal strDitta As String, ByVal strHlavCodart As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artval.*, tabling.tb_desling as xx_codvalu" & _
        " FROM artval" & _
        " LEFT JOIN tabling ON artval.ax_codvalu = tabling.tb_codling" & _
        " WHERE artval.codditt = " & CStrSQL(strDitta) & _
        " AND ax_codart = " & CStrSQL(strHlavCodart) & _
        " ORDER BY artval.codditt, ax_codart, ax_codvalu"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAL")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataCacf(ByVal strDitta As String, ByVal strCacfCodart As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT codarfo.*, anagra.an_descr1 as xx_conto" & _
        " FROM codarfo" & _
        " LEFT JOIN anagra ON codarfo.codditt = anagra.codditt AND codarfo.caf_conto = anagra.an_conto" & _
        " WHERE codarfo.codditt = " & CStrSQL(strDitta) & _
        " AND caf_codart = " & CStrSQL(strCacfCodart) & _
        " ORDER BY codarfo.codditt, caf_conto"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CODARFO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function CacfInsertBarcode(ByVal strDitta As String, ByVal strCacfCodart As String, _
                                       ByVal strDesnote As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO barcode (codditt, bc_codart, bc_datins, bc_code, bc_unmis," & _
        " bc_quant, bc_orins, bc_datagg, bc_oragg, bc_tipo)" & _
        " SELECT codditt, ar_codart, " & CDataSQL(Now.ToShortDateString) & ", " & _
        CStrSQL(strDesnote) & ", ar_unmis, 1, " & _
        DatePart("h", Now) & "." & DatePart("n", Now) & ", " & _
        CDataSQL(Now) & ", " & _
        DatePart("h", Now) & "." & DatePart("n", Now) & ", 'E'" & _
        " FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCacfCodart)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function UpdateTimeStampConto(ByVal strDitta As String, ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE anagra SET an_ultagg = GETDATE() " & _
               "WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & lConto

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function BarcCaricaUnitaMisura(ByVal strDitta As String, ByVal strCodart As String, ByRef dttOut As DataTable) As Boolean
    'ottengo l'elenco delle unità di misura del articolo
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dtrT1() As DataRow = Nothing
    Try
      strSQL = "SELECT TOP 0 * FROM tabumis"
      dttOut.Clear()
      dttOut.Columns.Clear()
      dttOut.Columns.Add("tb_codumis", GetType(String))

      strSQL = "SELECT ar_unmis AS ar_unmis, convert(binary(5), ar_unmis) FROM artico WHERE codditt = " & CStrSQL(strDitta) & " AND ar_codart = " & CStrSQL(strCodart) & " AND NOT ar_unmis IS NULL GROUP BY ar_unmis, convert(binary(5), ar_unmis) " & _
               " UNION " & _
               "SELECT ar_unmis2 AS ar_unmis, convert(binary(5), ar_unmis2) FROM artico WHERE codditt = " & CStrSQL(strDitta) & " AND ar_codart = " & CStrSQL(strCodart) & " AND NOT ar_unmis2 IS NULL GROUP BY ar_unmis2, convert(binary(5), ar_unmis2) " & _
               " UNION " & _
               "SELECT ar_confez2 AS ar_unmis, convert(binary(5), ar_confez2) FROM artico WHERE codditt = " & CStrSQL(strDitta) & " AND ar_codart = " & CStrSQL(strCodart) & " AND NOT ar_confez2 IS NULL GROUP BY ar_confez2, convert(binary(5), ar_confez2) "
      '" UNION " & _
      '"SELECT ar_um4 AS ar_unmis, convert(binary(5), ar_um4) FROM artico WHERE codditt = " & CStrSQL(strDitta) & " AND ar_codart = " & CStrSQL(strCodart) & " AND NOT ar_um4 IS NULL GROUP BY ar_um4, convert(binary(5), ar_um4) "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      For Each dtrT As DataRow In dttTmp.Rows
        If NTSCStr(dtrT!ar_unmis) <> "" Then
          dtrT1 = dttOut.Select("tb_codumis = " & CStrSQL(NTSCStr(dtrT!ar_unmis)))
          If dtrT1.Length = 0 Then
            dttOut.Rows.Add(New Object() {NTSCStr(dtrT!ar_unmis)})
          Else
            If NTSCStr(dtrT1(0)!tb_codumis).Equals(NTSCStr(dtrT!ar_unmis)) = Nothing Then
              dttOut.Rows.Add(New Object() {NTSCStr(dtrT!ar_unmis)})
            End If
          End If
        End If
      Next
      dttOut.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function AggiornaArtmatc(ByVal strDitta As String, ByVal strCodart As String, _
                                              ByVal strCodmaga As String, ByVal strFase As String, _
                                              ByVal bDelete As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim strCampiARTMATC As String = ""
    Dim i As Integer
    Dim ds As DataSet = Nothing
    Try
      strCampiARTMATC = "codditt, amt_codart, amt_codmaga,"
      For i = 1 To 24
        strCampiARTMATC = strCampiARTMATC & " amt_sm" & Right("0" & NTSCStr(i), 2) & ","
      Next
      For i = 1 To 24
        strCampiARTMATC = strCampiARTMATC & " amt_sx" & Right("0" & NTSCStr(i), 2) & ","
      Next
      For i = 1 To 24
        strCampiARTMATC = strCampiARTMATC & " amt_mo" & Right("0" & NTSCStr(i), 2) & ","
      Next
      For i = 1 To 24
        strCampiARTMATC = strCampiARTMATC & " amt_ss" & Right("0" & NTSCStr(i), 2) & ","
      Next
      strCampiARTMATC = strCampiARTMATC & " amt_fase"

      If bDelete Then
        strSQL = "DELETE FROM ARTMATC" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND amt_codart = " & CStrSQL(strCodart) & _
          " AND amt_codmaga = " & strCodmaga & _
          " AND amt_fase = " & strFase

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Else
        strSQL = "SELECT ARTMATC.*" & _
          " FROM ARTMATC" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND amt_codart = " & CStrSQL(strCodart) & _
          " AND amt_codmaga = " & strCodmaga & _
          " AND amt_fase = " & strFase

        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTMATC")

        If ds.Tables("ARTMATC").Rows.Count = 0 Then
          strSQL = "INSERT INTO ARTMATC (" & strCampiARTMATC & ")" & _
              " VALUES (" & CStrSQL(strDitta) & ", " & _
              CStrSQL(strCodart) & ", " & strCodmaga & ", "
          For i = 1 To 96
            strSQL = strSQL & " 0" & ","
          Next
          strSQL = strSQL & strFase & ")"

          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDataScor(ByVal strDitta As String, ByVal strScorCodart As String, _
                                          ByVal strScorChiamante As String, ByVal nScorCodmaga As Integer, _
                                          ByVal nScorFase As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      If UCase(strScorChiamante) = "BNMGARMA" Then
        strSQL = "SELECT ARTMATC.* FROM ARTMATC" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND amt_codart = " & CStrSQL(strScorCodart) & _
          " AND amt_codmaga = " & nScorCodmaga & _
          " AND amt_fase = " & nScorFase
      Else
        strSQL = "SELECT ARTICOTC.* FROM ARTICOTC" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND art_codart = " & CStrSQL(strScorCodart)
      End If

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMPTCO")

      If UCase(strScorChiamante) = "BNMGARMA" Then
        CambiaPrefissoCampiDatatable(ds.Tables("TEMPTCO"), "TEMPTCO", "TEMPTCO", "amt_", "xx_")
      Else
        CambiaPrefissoCampiDatatable(ds.Tables("TEMPTCO"), "TEMPTCO", "TEMPTCO", "art_", "xx_")
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function ScorSalva(ByVal strDitta As String, ByVal strScorCodart As String, _
                                        ByVal strScorChiamante As String, ByVal nScorCodmaga As Integer, _
                                        ByVal nScorFase As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim strPrefisso As String = ""
    Dim strTabella As String = ""
    Dim strWhere As String = ""
    Try
      If UCase(strScorChiamante) = "BNMGARMA" Then
        strPrefisso = "amt_"
        strTabella = "artmatc"
        strWhere = " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND amt_codart = " & CStrSQL(strScorCodart) & _
          " AND amt_codmaga = " & nScorCodmaga & _
          " AND amt_fase = " & nScorFase
        CambiaPrefissoCampiDatatable(ds.Tables("TEMPTCO"), "TEMPTCO", "TEMPTCO", "xx_", "amt_")
      Else
        strPrefisso = "art_"
        strTabella = "articotc"
        strWhere = " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND art_codart = " & CStrSQL(strScorCodart)
        CambiaPrefissoCampiDatatable(ds.Tables("TEMPTCO"), "TEMPTCO", "TEMPTCO", "xx_", "art_")
      End If

      strSQL = "UPDATE " & strTabella & " SET " & GetQueryUpdate(ds.Tables("TEMPTCO"), ds.Tables("TEMPTCO").Rows(0), strPrefisso) & strWhere

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      If UCase(strScorChiamante) = "BNMGARMA" Then
        CambiaPrefissoCampiDatatable(ds.Tables("TEMPTCO"), "TEMPTCO", "TEMPTCO", "amt_", "xx_")
      Else
        CambiaPrefissoCampiDatatable(ds.Tables("TEMPTCO"), "TEMPTCO", "TEMPTCO", "art_", "xx_")
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function VerificaBarcodeAltriArticoli(ByVal strDitta As String, ByVal strCode As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT barcode.* FROM barcode " & _
               "WHERE codditt = " & CStrSQL(strDitta) & _
               " AND bc_code = " & CStrSQL(strCode)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CheckRangeBarcode(ByVal strDitta As String, ByVal strBarcodeStart As String, ByVal strBarcodeEnd As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM barcode " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND bc_code BETWEEN " & CStrSQL(strBarcodeStart & "0") & " AND " & CStrSQL(strBarcodeEnd & "Z")

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaBarcode(ByVal strDitta As String, ByVal strCodart As String, ByVal strUnmis As String, ByVal strCode As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 codditt FROM barcode " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND bc_code = " & CStrSQL(strCode)

      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count = 0 Then
        strSQL = "INSERT INTO barcode (codditt, bc_codart, bc_datins, bc_code, bc_unmis, bc_quant, bc_orins, bc_datagg, bc_oragg, bc_tipo) " & _
                 " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodart) & ", " & CDataSQL(Now) & ", " & CStrSQL(strCode) & ", " & _
                 CStrSQL(strUnmis) & ", 1, " & Now.Hour & Now.Minute.ToString("00") & Now.Second.ToString("00") & ", " & CDataSQL(Now) & ", " & _
                 Now.Hour & Now.Minute.ToString("00") & Now.Second.ToString("00") & ", 'E')"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


#Region "MGGIFT"
  Public Overridable Function GiftSalva(ByVal strDitta As String, ByVal dttGift As DataTable) As Boolean
    Dim dbConn As DbConnection = Nothing
    Dim strSQL As String = ""
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      strSQL = "DELETE FROM articogift " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND ag_codart = " & CStrSQL(dttGift.Rows(0)!ag_codart)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "INSERT INTO articogift " & GetQueryInsertField(dttGift, "ag_") & _
               " VALUES " & GetQueryInsertValue(dttGift, dttGift.Rows(0), "ag_")

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

  Public Overridable Function CheckGiftMovimentata(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim dttTmp As New DataTable
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT COUNT(*) AS tot FROM movgift " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mg_codart = " & CStrSQL(strCodart)

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If NTSCInt(dttTmp.Rows(0)!tot) = 0 Then Return False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "MGHLAV"
  Public Overridable Function AggiornaDescrizioniInLingua(ByVal strDitta As String, ByVal strCodart As String, ByVal strCartella As String, ByVal nCodling As Integer, ByVal strDescr As String, ByVal bCancella As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dttOut As New DataTable
    Dim dttOut2 As New DataTable
    Try

      If bCancella Then

        strSQL = " DELETE from artval WHERE codditt = " & CStrSQL(strDitta) & _
                                          " And ax_codart IN (SELECT ar_codart FROM artico " & _
                                                            " WHERE codditt = " & CStrSQL(strDitta) & _
                                                            " AND ar_codroot = " & CStrSQL(strCodart) & _
                                                            " AND ar_flmod = " & CStrSQL(strCartella) & ")" & _
                                          " And ax_codvalu NOT IN (SELECT ax_codvalu " & _
                                                                 " FROM artval" & _
                                                                 " WHERE codditt = " & CStrSQL(strDitta) & _
                                                                 " AND ax_codart = " & CStrSQL(strCodart) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Else

        strSQL = " SELECT artico.codditt, ar_codart, " & nCodling & ", RTrim(Left(" & CStrSQL(strDescr) & " + ' ' + (CASE WHEN vvl_desvvar IS NULL THEN '' ELSE vvl_desvvar END), 40))," & _
                 " CASE WHEN Substring(" & CStrSQL(strDescr) & " + ' ' + (CASE WHEN vvl_desvvar IS NULL THEN '' ELSE vvl_desvvar END), 41, 40) = '' Then Null " & _
                 " Else RTrim(substring(" & CStrSQL(strDescr) & " + ' ' + (CASE WHEN vvl_desvvar IS NULL THEN '' ELSE vvl_desvvar END), 41, 40)) End, " & _
                 " " & CDataSQL(Now) & ", Null" & _
                 " FROM artico INNER JOIN tabvari ON artico.codditt = tabvari.codditt And artico.ar_flmod = tabvari.tb_codvari" & _
                 " INNER JOIN valvari ON artico.codditt = valvari.codditt And artico.ar_flmod = valvari.vv_codvari And artico.ar_codvar1 = valvari.vv_valvari" & _
                 " LEFT JOIN varilin ON artico.codditt = varilin.codditt And artico.ar_flmod = varilin.vvl_codvari And artico.ar_codvar1 = varilin.vvl_valvari" & _
                 " AND " & nCodling & " = varilin.vvl_codling " & _
                 " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                 " And ar_codroot = " & CStrSQL(strCodart) & _
                 " And ar_flmod = " & CStrSQL(strCartella)
        dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        For Each dtrRow As DataRow In dttOut.Rows
          strSQL = " SELECT * " & _
                   " FROM artval " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND ax_codart = " & CStrSQL(dtrRow!ar_codart) & _
                   " AND ax_codvalu = " & nCodling
          dttOut2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttOut2.Rows.Count > 0 Then
            'UPDATE
            strSQL = " UPDATE artval " & _
                     " SET ax_descr = RTrim(Left(" & CStrSQL(strDescr) & " + ' ' + (CASE WHEN vvl_desvvar IS NULL THEN '' ELSE vvl_desvvar END), 40)), " & _
                     " ax_desint = CASE WHEN Substring(" & CStrSQL(strDescr) & " + ' ' + (CASE WHEN vvl_desvvar IS NULL THEN '' ELSE vvl_desvvar END), 41, 40) = '' Then Null " & _
                     " Else RTrim(substring(" & CStrSQL(strDescr) & " + ' ' + (CASE WHEN vvl_desvvar IS NULL THEN '' ELSE vvl_desvvar END), 41, 40)) End, " & _
                     " ax_ultagg = " & CDataSQL(Now) & _
                     " FROM artval INNER JOIN artico ON artval.codditt = artico.codditt AND artval.ax_codart = artico.ar_codart " & _
                     " INNER JOIN tabvari ON artico.codditt = tabvari.codditt And artico.ar_flmod = tabvari.tb_codvari" & _
                     " INNER JOIN valvari ON artico.codditt = valvari.codditt And artico.ar_flmod = valvari.vv_codvari And artico.ar_codvar1 = valvari.vv_valvari" & _
                     " LEFT JOIN varilin ON artico.codditt = varilin.codditt And artico.ar_flmod = varilin.vvl_codvari And artico.ar_codvar1 = varilin.vvl_valvari" & _
                     " WHERE artval.codditt = " & CStrSQL(strDitta) & _
                     " And ax_codart = " & CStrSQL(dtrRow!ar_codart) & _
                     " AND ax_codvalu = " & nCodling
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          Else
            'INSERT
            strSQL = " INSERT INTO artval (codditt, ax_codart, ax_codvalu, ax_descr, ax_desint, ax_ultagg, ax_note)" & _
                     " SELECT artico.codditt, ar_codart, " & nCodling & ", RTrim(Left(" & CStrSQL(strDescr) & " + ' ' + (CASE WHEN vvl_desvvar IS NULL THEN '' ELSE vvl_desvvar END), 40))," & _
                     " CASE WHEN Substring(" & CStrSQL(strDescr) & " + ' ' + (CASE WHEN vvl_desvvar IS NULL THEN '' ELSE vvl_desvvar END), 41, 40) = '' Then Null " & _
                     " Else RTrim(substring(" & CStrSQL(strDescr) & " + ' ' + (CASE WHEN vvl_desvvar IS NULL THEN '' ELSE vvl_desvvar END), 41, 40)) End, " & _
                     " " & CDataSQL(Now) & ", Null" & _
                     " FROM artico INNER JOIN tabvari ON artico.codditt = tabvari.codditt And artico.ar_flmod = tabvari.tb_codvari" & _
                     " INNER JOIN valvari ON artico.codditt = valvari.codditt And artico.ar_flmod = valvari.vv_codvari And artico.ar_codvar1 = valvari.vv_valvari" & _
                     " LEFT JOIN varilin ON artico.codditt = varilin.codditt And artico.ar_flmod = varilin.vvl_codvari And artico.ar_codvar1 = varilin.vvl_valvari" & _
                     " AND " & nCodling & " = varilin.vvl_codling " & _
                     " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                     " And ar_codroot = " & CStrSQL(strCodart) & _
                     " And ar_flmod = " & CStrSQL(strCartella) & _
                     " And ar_codart = " & CStrSQL(dtrRow!ar_codart)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
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
#End Region

#Region "MGSIMU"
  Public Overridable Function PrimoMagazzino(ByVal strDitta As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tb_codmaga FROM tabmaga" & _
        " ORDER BY codditt, tb_codmaga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return 0
      '--------------------------------------------------------------------------------------------------------------
      Return NTSCInt(dttTmp.Rows(0)!tb_codmaga)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function ProgressiviMagazzino(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal nMagaz As Integer, ByVal nFase As Integer, ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM artpro" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ap_codart = " & CStrSQL(strCodart) & _
        " AND ap_magaz = " & nMagaz & _
        " AND ap_fase = " & nFase
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      Return True
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
#End Region

#Region "MGHLAP"
  Public Overridable Function ArticoloGestitoAMatricole(ByVal strDitta As String, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT ar_codart FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart) & _
        " AND ar_gestmatr = 'S'"
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
#End Region

#Region "MGHDEPR"
  Public Overridable Function DecodificaFaseArticolo(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal nFase As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If nFase = 0 Then Return ""
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT af_descr FROM artfasi" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND af_codart = " & CStrSQL(strCodart) & _
        " AND af_fase = " & nFase
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then
        If dttTmp.Rows(0)!af_descr.Equals(DBNull.Value) = False Then Return NTSCStr(dttTmp.Rows(0)!af_descr).Trim
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return ""
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return ""
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function GetDataDepr(ByVal strDitta As String, _
    ByVal strCodart As String, ByVal nMagaz As Decimal, ByVal nFase As Integer, _
    ByVal bCommeca As Boolean, ByVal bLotto As Boolean, ByVal bUbicaz As Boolean, _
    ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If bCommeca = False And bLotto = False And bUbicaz = False Then
        strSQL = "SELECT ar_gescomm, ar_geslotti FROM artico" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ar_codart = " & CStrSQL(strCodart)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "SELECT lotcpro.*" & _
          ", " & IIf(NTSCStr(dttTmp.Rows(0)!ar_gescomm) <> "N", "co_descr1", "''").ToString & " AS xx_commeca" & _
          ", " & IIf(NTSCStr(dttTmp.Rows(0)!ar_geslotti) <> "N", "alo_descr", "''").ToString & " AS xx_lotto" & _
          " FROM lotcpro" & _
          IIf(NTSCStr(dttTmp.Rows(0)!ar_gescomm) <> "N", _
            " LEFT JOIN commess ON lotcpro.codditt = commess.codditt" & _
                             " AND lotcpro.lp_commeca = commess.co_comme", "").ToString & _
          IIf(NTSCStr(dttTmp.Rows(0)!ar_geslotti) <> "N", _
            " LEFT JOIN analotti ON lotcpro.codditt = analotti.codditt" & _
                              " AND lotcpro.lp_codart = analotti.alo_codart" & _
                              " AND lotcpro.lp_lotto = analotti.alo_lotto", "").ToString & _
          " WHERE lotcpro.codditt = " & CStrSQL(strDitta) & _
          " AND lp_codart = " & CStrSQL(strCodart) & _
          " AND lp_magaz = " & nMagaz & _
          " AND lp_fase = " & nFase & _
          " ORDER BY lotcpro.codditt, lp_codart, lp_magaz, lp_commeca, lp_lotto, lp_fase, lp_ubicaz"
      Else
        strSQL = "SELECT" & _
          IIf(bCommeca = True, " lp_commeca,", "").ToString & _
          IIf(bLotto = True, " lp_lotto,", "").ToString & _
          IIf(bUbicaz = True, " lp_ubicaz,", "").ToString & _
          " SUM(lp_esist) AS lp_esist, SUM(lp_qtalif) AS lp_qtalif, SUM(lp_vqtalif) AS lp_vqtalif," & _
          " SUM(lp_giaini) AS lp_giaini, SUM(lp_vgiaini) AS lp_vgiaini," & _
          " SUM(lp_prenot) AS lp_prenot, SUM(lp_vprenot) AS lp_vprenot," & _
          " SUM(lp_ordin) AS lp_ordin, SUM(lp_vordin) AS lp_vordin," & _
          " SUM(lp_impeg) AS lp_impeg, SUM(lp_vimpeg) AS lp_vimpeg," & _
          IIf(bCommeca = True, "MIN(co_descr1)", "''").ToString & " AS xx_commeca, " & _
          IIf(bLotto = True, "MIN(alo_descr)", "''").ToString & " AS xx_lotto" & _
          " FROM lotcpro" & _
          IIf(bCommeca = True, _
            " LEFT JOIN commess ON lotcpro.codditt = commess.codditt" & _
                             " AND lotcpro.lp_commeca = commess.co_comme", "").ToString & _
          IIf(bLotto = True, _
            " LEFT JOIN analotti ON lotcpro.codditt = analotti.codditt" & _
                              " AND lotcpro.lp_codart = analotti.alo_codart" & _
                              " AND lotcpro.lp_lotto = analotti.alo_lotto", "").ToString & _
          " WHERE lotcpro.codditt = " & CStrSQL(strDitta) & _
          " AND lp_codart = " & CStrSQL(strCodart) & _
          " AND lp_magaz = " & nMagaz & _
          " AND lp_fase = " & nFase & _
          " GROUP BY lp_codart, lp_magaz, lp_fase" & _
            IIf(bCommeca = True, ", lp_commeca", "").ToString & _
            IIf(bLotto = True, ", lp_lotto", "").ToString & _
            IIf(bUbicaz = True, ", lp_ubicaz", "").ToString
      End If
      '--------------------------------------------------------------------------------------------------------------
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LOTCPRO")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
#End Region

#Region "MGHLOTC"
  Public Overridable Function DecodificaLottoArticolo(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal lLotto As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If lLotto = 0 Then Return ""
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT alo_descr FROM analotti" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND alo_codart = " & CStrSQL(strCodart) & _
        " AND alo_lotto = " & lLotto
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count > 0 Then
        If dttTmp.Rows(0)!alo_descr.Equals(DBNull.Value) = False Then Return NTSCStr(dttTmp.Rows(0)!alo_descr).Trim
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return ""
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return ""
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function GetDataLotc(ByVal strDitta As String, _
    ByVal strCodart As String, ByVal nMagaz As Decimal, ByVal nFase As Integer, _
    ByVal bCommeca As Boolean, ByVal bLotto As Boolean, ByVal bUbicaz As Boolean, _
    ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Apre un dataSet su LOTCPROTC
      '--------------------------------------------------------------------------------------------------------------
      If bCommeca = False And bLotto = False And bUbicaz = False Then
        strSQL = "SELECT ar_gescomm, ar_geslotti FROM artico" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ar_codart = " & CStrSQL(strCodart)
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "SELECT lotcprotc.*" & _
          ", " & IIf(NTSCStr(dttTmp.Rows(0)!ar_gescomm) <> "N", "co_descr1", "''").ToString & " AS xx_commeca" & _
          ", " & IIf(NTSCStr(dttTmp.Rows(0)!ar_geslotti) <> "N", "alo_descr", "''").ToString & " AS xx_lotto" & _
          " FROM lotcprotc" & _
          IIf(NTSCStr(dttTmp.Rows(0)!ar_gescomm) <> "N", _
            " LEFT JOIN commess ON lotcprotc.codditt = commess.codditt" & _
                             " AND lotcprotc.apt_commeca = commess.co_comme", "").ToString & _
          IIf(NTSCStr(dttTmp.Rows(0)!ar_geslotti) <> "N", _
            " LEFT JOIN analotti ON lotcprotc.codditt = analotti.codditt" & _
                              " AND lotcprotc.apt_codart = analotti.alo_codart" & _
                              " AND lotcprotc.apt_lotto = analotti.alo_lotto", "").ToString & _
          " WHERE lotcprotc.codditt = " & CStrSQL(strDitta) & _
          " AND apt_codart = " & CStrSQL(strCodart) & _
          " AND apt_magaz = " & nMagaz & _
          " AND apt_fase = " & nFase & _
          " ORDER BY lotcprotc.codditt, apt_codart, apt_magaz, apt_commeca, apt_lotto, apt_fase, apt_ubicaz"
      Else
        strSQL = "SELECT" & _
          IIf(bCommeca = True, " apt_commeca,", "").ToString & _
          IIf(bLotto = True, " apt_lotto,", "").ToString & _
          IIf(bUbicaz = True, " apt_ubicaz,", "").ToString
        For i As Integer = 1 To 24
          strSQL += " SUM(apt_esist" & Right("0" & i.ToString, 2) & ") AS apt_esist" & Right("0" & i.ToString, 2) & "," & _
            " SUM(apt_prenot" & Right("0" & i.ToString, 2) & ") AS apt_prenot" & Right("0" & i.ToString, 2) & "," & _
            " SUM(apt_ordin" & Right("0" & i.ToString, 2) & ") AS apt_ordin" & Right("0" & i.ToString, 2) & "," & _
            " SUM(apt_impeg" & Right("0" & i.ToString, 2) & ") AS apt_impeg" & Right("0" & i.ToString, 2) & ","
        Next i
        strSQL += IIf(bCommeca = True, "MIN(co_descr1)", "''").ToString & " AS xx_commeca, " & _
          IIf(bLotto = True, "MIN(alo_descr)", "''").ToString & " AS xx_lotto" & _
          " FROM lotcprotc" & _
          IIf(bCommeca = True, _
            " LEFT JOIN commess ON lotcprotc.codditt = commess.codditt" & _
                             " AND lotcprotc.apt_commeca = commess.co_comme", "").ToString & _
          IIf(bLotto = True, _
            " LEFT JOIN analotti ON lotcprotc.codditt = analotti.codditt" & _
                              " AND lotcprotc.apt_codart = analotti.alo_codart" & _
                              " AND lotcprotc.apt_lotto = analotti.alo_lotto", "").ToString & _
            " WHERE lotcprotc.codditt = " & CStrSQL(strDitta) & _
            " AND apt_codart = " & CStrSQL(strCodart) & _
            " AND apt_magaz = " & nMagaz & _
            " AND apt_fase = " & nFase & _
            " GROUP BY apt_codart, apt_magaz, apt_fase" & _
            IIf(bCommeca = True, ", apt_commeca", "").ToString & _
            IIf(bLotto = True, ", apt_lotto", "").ToString & _
            IIf(bUbicaz = True, ", apt_ubicaz", "").ToString
      End If
      '--------------------------------------------------------------------------------------------------------------
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LOTCPROTC")
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function RitornaTagliaArticolo(ByVal strDitta As String, ByVal strCodart As String, _
    ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tabtagl.*" & _
        " FROM artico INNER JOIN tabtagl ON artico.codditt = tabtagl.codditt" & _
                                      " AND artico.ar_codtagl = tabtagl.tb_codtagl" & _
        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart)
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Rows.Count = 0 Then
        dttOut.Clear()
        dttOut.Dispose()
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If dttOut.Rows(0)!tb_codtagl.Equals(DBNull.Value) = True Then
        dttOut.Clear()
        dttOut.Dispose()
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
