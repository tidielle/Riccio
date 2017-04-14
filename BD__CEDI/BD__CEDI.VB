Imports NTSInformatica.CLN__STD
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports NTSInformatica
Imports System.IO

Public Class CLD__CEDI
  Inherits CLD__BASE

  Public Overridable Function GetDataApri(ByVal bMultiditta As Boolean, ByVal strCoddittTmp As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT codditt, tb_azrags1, tb_azrags2, tb_azindir, tb_azcap, tb_azcitta," & _
        " tb_azprov, tb_azcodf, tb_azpiva, tb_azcodpcon FROM tabanaz"
      If bMultiditta = False Then
        strSQL = strSQL & " WHERE codditt = " & CStrSQL(strCoddittTmp)
      End If
      strSQL = strSQL & " ORDER BY codditt"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function EliminaDatiUpdateTabNuma(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE tabnuma" & _
        " SET tb_numprog = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo IN ('IP', 'NS', 'SG')"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function EliminaCG_1(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Dim ds As DataSet = Nothing
    Dim i As Integer
    Try
      strSQL = "DELETE FROM priana WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM priana2 WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM pria2comp WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM movusat WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM princpag WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM prfatt WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM pnnote WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM moviva WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM scaden WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM prinot WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM princomp WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM lavmano WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movf24 WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testf24 WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM rsmriel WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Non cancella i dati in TABDURI ma ne azzera i valori di contatori/date
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT codditt, tb_utipreg, tb_unumreg, tb_anno FROM tabduri" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " ORDER BY codditt, tb_utipreg, tb_unumreg, tb_anno"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABDURI")

      For i = 0 To ds.Tables("TABDURI").Rows.Count - 1
        strSQL = "UPDATE tabduri" & _
          " SET tb_udatreg = " & CDataSQL(IntSetDate("01/01/" & NTSCStr(ds.Tables("TABDURI").Rows(i)!tb_anno))) & "," & _
          " tb_unureg = 0," & _
          " tb_ualfdoc = ' '," & _
          " tb_unumpro = 0," & _
          " tb_unumpag = 0" & _
          " WHERE codditt = " & CStrSQL(NTSCStr(ds.Tables("TABDURI").Rows(i)!codditt)) & _
          " AND tb_utipreg = " & CStrSQL(NTSCStr(ds.Tables("TABDURI").Rows(i)!tb_utipreg)) & _
          " AND tb_unumreg = " & NTSCStr(ds.Tables("TABDURI").Rows(i)!tb_unumreg) & _
          " AND tb_anno = " & NTSCStr(ds.Tables("TABDURI").Rows(i)!tb_anno)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM tabpriv WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM tabregb WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM tabprve WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM tablipe WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)


      strSQL = "DELETE FROM tabprcr WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM estromis WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "DELETE FROM princomp WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM priana2 WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM pria2comp WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM cacoefcdc WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM capesidr WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Aggiorna TABANAZ
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabanaz" & _
        " SET tb_dtulliq = " & CDataSQL(IntSetDate("01/01/1900")) & "," & _
        " tb_dtullg = " & CDataSQL(IntSetDate("01/01/1900")) & "," & _
        " tb_rgullg = 0, tb_riullg = 0, tb_prdalg = 0, tb_pravlg = 0," & _
        " tb_crediva = 0, tb_ac12iva = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Cancella progressivi, distinte, effetti, protocolli
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabnuma" & _
        " SET tb_numprog = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo IN ('EA', 'DA', 'P', 'PA')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaCG_2(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM intrac WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM ritenut WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM pagcomp WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM flussi WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movfpr WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM tabmfpr WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaMG(ByVal strDitta As String, ByVal bMantieni As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM mpallet WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM tpallet WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movubic WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movpic WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testpic WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM tabpick WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM schetrasp WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM lavcent WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM keymag WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movmatr WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movcona WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movmagtc WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movmags WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movmag WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testmagc WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testmags WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testmag WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM oresol WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Cancella i records di REINCPAG collegati a TESTMAGS e quelli non contestulali a
      '--- documenti/ordini (con il campo REINCPAG.mmp_tipork = ' ')
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM reincpag" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mmp_tipork IN (' ', 'A', 'B', 'C', 'D', 'E', 'F', 'I', 'J', 'K', 'L', 'M', 'N'," & _
                            "'P', 'S', 'T', 'U', 'Z', '£', '(')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Cancella RECHICAS
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM rechicas WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Se selezionato o meno il CheckBox relativo a 'Mantieni ultimo costo d'acquisto'
      '-----------------------------------------------------------------------------------------
      If bMantieni = True Then
        strSQL = "UPDATE artprox" & _
          " SET artprox.apx_esist = 0, artprox.apx_prenot = 0," & _
          " artprox.apx_ordin = 0, artprox.apx_impeg = 0," & _
          " artprox.apx_giaini = 0, artprox.apx_vprenot = 0," & _
          " artprox.apx_vordin = 0, artprox.apx_vimpeg = 0," & _
          " artprox.apx_vgiaini = 0, artprox.apx_qtalif = 0," & _
          " artprox.apx_vqtalif = 0, artprox.apx_daordi = 0, artprox.apx_vdaordi = 0" & _
          " WHERE codditt = " & CStrSQL(strDitta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "UPDATE artproxtc SET"
        For i = 1 To 24
          strSQL = strSQL & " apt_esist" & Right("0" & NTSCStr(i), 2) & " = 0," & _
            " apt_prenot" & Right("0" & NTSCStr(i), 2) & " = 0," & _
            " apt_ordin" & Right("0" & NTSCStr(i), 2) & " = 0," & _
            " apt_impeg" & Right("0" & NTSCStr(i), 2) & " = 0" & NTSCStr(IIf(i <> 24, ",", ""))
        Next i
        strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "UPDATE artpro" & _
          " SET artpro.ap_esist = 0, artpro.ap_prenot = 0," & _
          " artpro.ap_ordin = 0, artpro.ap_impeg = 0," & _
          " artpro.ap_carfor = 0, artpro.ap_carpro = 0," & _
          " artpro.ap_carvar = 0, artpro.ap_rescli = 0," & _
          " artpro.ap_scacli = 0, artpro.ap_scapro = 0," & _
          " artpro.ap_scavar = 0, artpro.ap_resfor = 0," & _
          " artpro.ap_giaini = 0, artpro.ap_vprenot = 0," & _
          " artpro.ap_vordin = 0, artpro.ap_vimpeg = 0," & _
          " artpro.ap_vcarfor = 0, artpro.ap_vcarpro = 0," & _
          " artpro.ap_vcarvar = 0, artpro.ap_vrescli = 0," & _
          " artpro.ap_vscacli = 0, artpro.ap_vscapro = 0," & _
          " artpro.ap_vscavar = 0, artpro.ap_vresfor = 0," & _
          " artpro.ap_vgiaini = 0, artpro.ap_sommat = 0," & _
          " artpro.ap_daordi = 0, artpro.ap_vdaordi = 0" & _
          " WHERE codditt = " & CStrSQL(strDitta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "UPDATE artprotc SET"
        For i = 1 To 24
          strSQL = strSQL & " apt_esist" & Right("0" & NTSCStr(i), 2) & " = 0," & _
            " apt_prenot" & Right("0" & NTSCStr(i), 2) & " = 0," & _
            " apt_ordin" & Right("0" & NTSCStr(i), 2) & " = 0," & _
            " apt_impeg" & Right("0" & NTSCStr(i), 2) & " = 0" & NTSCStr(IIf(i <> 24, ",", ""))
        Next i
        strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Else
        strSQL = "DELETE FROM artprox WHERE codditt = " & CStrSQL(strDitta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE FROM artproxtc WHERE codditt = " & CStrSQL(strDitta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE FROM artpro WHERE codditt = " & CStrSQL(strDitta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE FROM artprotc WHERE codditt = " & CStrSQL(strDitta)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM artdefxo WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM artdefo WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM lotcdefo WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM artdefxtco WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM artdeftco WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM lotcdeftco WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM artdeftc WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM artdef WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM artdefxtc WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM artdefx WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM artlif WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM lotcdeftc WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM lotcdef WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movpac WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testpac WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM movcla WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM avlavp WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Aggiorna TABANAZ
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabanaz" & _
        " SET tb_dtulap = " & CDataSQL(IntSetDate("01/01/1900")) & "," & _
        " tb_dtulst = " & CDataSQL(IntSetDate("01/01/1900")) & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Cancella progressivi documenti magazzino
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabnuma" & _
        " SET tb_numprog = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo IN ('A', 'B', 'C', 'F', 'G', 'L', 'M', 'P', 'S', 'T', 'U', 'Z')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Cancella il contenuto delle tabelle del Sistema Qualità
      '-----------------------------------------------------------------------------------------      
      strSQL = "DELETE FROM valcoll WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM collsotl WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM collaudi WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM valvalu WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM valutaz WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM rappdif WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM rappnc WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM keycoll WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM lotcpro WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM lotcprotc WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM cvprevmov WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM cvpreven WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Azzera i progressivi per Collaudi, Rapporti di non Conformità, Preventivi
      '--- (COLLAUDI, RAPPNC, CVPREVEN)
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabnuma SET tb_numprog = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo IN ('CL', 'NC', 'PR')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Azzera i progressivi per LOGISTICA DI MAGAZZINO SU PALMARE
      '--- UC unità di carico
      '--- PK ID di picking
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabnuma SET tb_numprog = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo IN ('UC', 'PK')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaPRB(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM keyprb WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movprb WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testprbc WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testprb WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Cancella progressivi documenti di magazzino
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabnuma" & _
        " SET tb_numprog = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo IN ('W', 'PK')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaOR(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM assris WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM attivit WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM keyord WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movords WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movord WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movordtc WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testords WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testord WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM zzassris WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM zzattivit WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM ordlist WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM listprod WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM tablpro WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movrdo WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testrda WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testrdo WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM movoff WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM testoff WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM ordlisttc WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM listprodtc WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Cancella i records di REINCPAG collegati a TESTORDS
      '--- N.B.: quelli non contestulali a docuemnti/ordini (REINCPAG.mmp_tipork = ' ')
      '--- sono cancellati in caso di eliminazione dei dati di magazzino (routine EliminaMG)
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM reincpag" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mmp_tipork IN ('$', 'H', 'O', 'Q', 'R', 'V', 'X', 'Y')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Cancella progressivi documenti di magazzino
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabnuma" & _
        " SET tb_numprog = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo IN ('R', 'O', 'Q', 'H', 'Y', 'X', '$', 'VV', 'RO', 'RH', 'SO', 'SH'," & _
        " 'OL', '!', '#')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaBUD(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM budget WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM budgetmen WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM budgetd WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM budgetdmen WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM budvenp WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM budvend WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM budven WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM cabudget WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaPR(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM provvig WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabcage" & _
        " SET tb_datpro = " & CDataSQL(IntSetDate("01/01/1900")) & "," & _
        " tb_fatpro = 0," & _
        " tb_propro = 0," & _
        " tb_endtpro = 0," & _
        " tb_enagpro = 0," & _
        " tb_incapro = 0," & _
        " tb_casspro = 0," & _
        " tb_minrecu = 0," & _
        " tb_datper = " & CDataSQL(IntSetDate("01/01/1900")) & "," & _
        " tb_fatper = 0," & _
        " tb_proper = 0," & _
        " tb_endtper = 0," & _
        " tb_enagper = 0," & _
        " tb_incaper = 0," & _
        " tb_cassper = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function EliminaPM_1(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM proeseb WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM proesec WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM comchiu WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Adesso azzera/ripristina i campi su TASKS
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tasks" & _
        " SET tsk_actwork = 0, tsk_work = tsk_basework, tsk_remwork = tsk_basework," & _
        " tsk_actqta = 0, tsk_qta = tsk_baseqta, tsk_remqta = tsk_baseqta, tsk_flevas = 'C'," & _
        " tsk_actcost = 0, tsk_cost = tsk_basecost, tsk_remcost = tsk_basecost," & _
        " tsk_pctworkcomp = 0, tsk_ordcost = 0, tsk_ordflevas = 'C'," & _
        " tsk_ordremcost = tsk_basecost, tsk_diffwork = 0, tsk_diffqta = 0, tsk_diffcost = 0," & _
        " tsk_ordcostprg = tsk_basecost, tsk_orddiffcost = 0, tsk_totcost = tsk_basecost," & _
        " tsk_totdiffcost = 0, tsk_ordqta = 0, tsk_ordqtaprg = tsk_baseqta," & _
        " tsk_orddiffqta = 0, tsk_ordremqta = tsk_baseqta, tsk_ordwork = 0," & _
        " tsk_ordworkprg = tsk_basework, tsk_orddiffwork = 0, tsk_ordremwork = tsk_basework," & _
        " tsk_totqta = tsk_baseqta, tsk_totdiffqta = 0, tsk_totwork = tsk_basework," & _
        " tsk_totdiffwork = 0, tsk_actdur = 0, tsk_dur = tsk_basedur, tsk_remdur = tsk_basedur," & _
        " tsk_diffdur = 0, tsk_pctordcomp = 0, tsk_totactcost = 0," & _
        " tsk_totremcost = tsk_basecost" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Adesso sistema i dati su commess e su prevent
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE prevent" & _
        " SET pre_actcost = 0, pre_remcost = pre_costotot, pre_diffcost = 0," & _
        " pre_totcost = pre_costotot, pre_totdiffcost = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "UPDATE commess" & _
        " SET co_dataggb = " & CDataSQL(IntSetDate("01/01/1900")) & "," & _
        " co_dataggc = " & CDataSQL(IntSetDate("01/01/1900")) & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaPM_2(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM comop WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM documtas WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM valtask WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM tasks WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM prevent WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM subcomm WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM commess WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM tabprog WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Dovrebbe eliminare anche i record di budget e budgetd budgetmen e budgetdmen correlati
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaCRM(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      '--- OPPORTUN
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM opportun WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- CRACT
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM cract WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- CRACTOPP
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM cractopp WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- CRMULTS
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM crmults WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Cancella progressivi, distinte, effetti, protocolli
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabnuma" & _
        " SET tb_numprog = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo IN ('C7', 'C9')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaDWH(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      '--- DWAREH
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM dwareh WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "DELETE FROM dwarehe WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaStatistiche(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM statven WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function EliminaAS_1(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      '--- NNPREQPRO Tariffe professionali associate al prepagato (CS)
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM nnpreqpro" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- NNPREPAG Prepagati/monte ore
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM nnprepag" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- NNMATRMAN Matricole, manutenzione programmata
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM nnmatrman" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- TABPROB Problemi
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM tabprob" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- NNMOVCTRA Righe contratti di assistenza
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM nnmovctra" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- NNRINNOVI Canoni/rinnovi contratti di assistenza
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM nnrinnovi" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- NNCONTRAS Testate contratti di assistenza
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM nncontras" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- NNMATCONT rilevamento contatori
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM nnmatrcont" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- NNMATRICS Matricole, anagrafica/parco inst.
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM nnmatrics" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- NNMATRCOM Matricole, componenti
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM nnmatrcom" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Cancella progressivi documenti magazzino
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabnuma" & _
        " SET tb_numprog = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo IN ('GP', 'C1', 'C6')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaAS_2(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      '--- ATTCONSCD Dettaglio attività eseguite
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM attconscd" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- ATTPREVCD Dettaglio attività da svolgere
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM attprevcd" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- NNRICAMBI Ricambi associato all'intervento
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM nnricambi" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- ATTCONSC Attività eseguite
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM attconsc" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- ATTPREVC Attività da svolgere
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM attprevc" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- NNCHIMAT Matricole associate a interventi/chiamate
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM nnchimat" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- NNCHIAM Interventi/chiamate
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM nnchiam" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Cancella progressivi documenti magazzino
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabnuma" & _
        " SET tb_numprog = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo IN ('C2', 'C3', 'C4', 'C5')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaTT(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Dim ds As DataSet = Nothing
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Apre uno Snapshot su ORDERTBL sulle tabelle che hanno il campo ot_istmp = "S"
      '--- (le tabelle temporanee)  e che sono gestite per ditta (ot_perditta = "S")
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM ordertbl" & _
        " WHERE ot_istmp = 'S'" & _
        " AND ot_perditta = 'S'"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORDERTBL")

      '--- Inizia il ciclo su ORDERTBL
      '--- cancellando il contenuto di tutte le tabelle temporanee
      For i = 0 To ds.Tables("ORDERTBL").Rows.Count - 1
        strSQL = "DELETE FROM " & NTSCStr(ds.Tables("ORDERTBL").Rows(i)!ot_nometab) & _
          " WHERE codditt = " & CStrSQL(strDitta)
        'Allegati IVA cli/forn
        Select Case NTSCStr(ds.Tables("ORDERTBL").Rows(i)!ot_nometab).ToLower
          Case "ttbudven" : strSQL = strSQL & " AND instid <> 0"
          Case "mmtrans", "mmtranstc" : strSQL = strSQL & " AND instid <> -1000000000" & _
                                                          " AND instid <> -2000000000"
          Case "tscaden", "ttscaden2" : strSQL = strSQL & " AND instid <> -2000000000"
        End Select
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaMovc(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      '--- MOVCESP Movimenti cespiti
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM movcesp" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaAnac(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Try
      '-----------------------------------------------------------------------------------------
      '--- ANACCEN Ripartiz. costi cespiti per centro/linea
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM anaccen" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- ANACESP Anagr. cespiti
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM anacesp" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------
      '--- Cancella progressivi anagrafica cespiti
      '-----------------------------------------------------------------------------------------
      strSQL = "UPDATE tabnuma" & _
        " SET tb_numprog = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_numtipo IN ('CE')"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaEmail(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- EMAILS
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM emails WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- EMAILALL
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM emailall WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- EMAILDES
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM emaildes WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- EMAILDIR
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM emaildir WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- EMAILDAC
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM emaildac WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function EliminaScadex(ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- SCADENEC
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM scadenec WHERE codditt = " & CStrSQL(strDitta)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ControllaEsistenzaMovcesp(ByVal strCodditt As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      strSQL = "SELECT TOP 1 me_codcesp FROM movcesp" & _
        " WHERE codditt = " & CStrSQL(strCodditt) & _
        " ORDER BY codditt, me_codcesp, me_datreg, me_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return True
      Return False
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function VerificaDatabaseUnicode(ByVal bDatabaseCorrente As Boolean, ByVal strImportServer As String, ByVal strImportDatabase As String, _
                                                      ByVal strImportUtente As String, ByVal strImportPassword As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = "SELECT data_type FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = 'RELEASE' AND column_name = 'rel_dbpers'"
    Try
      If bDatabaseCorrente Then
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "RELEASE")
      Else
        ds = ImportRecordset(strSQL, "RELEASE", strImportServer, strImportDatabase, strImportUtente, strImportPassword)
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


#Region "funzioni specifiche per BN__CEDI.BN__ELDA"

#End Region

#Region "funzioni specifiche per BN__CEDI.BN__IMPD"

  Public Overridable Function CheckCoddittOr(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_azrags1 FROM tabanaz" & _
        " WHERE codditt = " & CStrSQL(strDitta)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckDatabase(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = " SELECT Count(*) As Totale FROM aziende WHERE azcodaz = " & CStrSQL(strDitta)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC, "AZIENDE")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckCoddittOrAltroDb(ByVal strDitta As String, ByVal strImportServer As String, ByVal strImportDatabase As String, ByVal strImportUtente As String, ByVal strImportPassword As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_azrags1 FROM tabanaz WHERE codditt = " & CStrSQL(strDitta)

      ds = ImportRecordset(strSQL, "TABANAZ", strImportServer, strImportDatabase, strImportUtente, strImportPassword)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CheckConnectionAltroDB(ByVal strServer As String, ByVal strDatabase As String, ByVal strUtente As String, ByVal strPassword As String) As Boolean
    Dim strSQL As String = ""
    Dim dbImportConn As SqlClient.SqlConnection = Nothing
    Try
      dbImportConn = ImportConnettiSql(strServer, strDatabase, strUtente, strPassword)

      If dbImportConn Is Nothing Then
        Return False
      Else
        Return True
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CopiaDatiEsegui(ByVal strSQL As String) As Boolean
    Try
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      Dim strError() As String
      Dim lPosErr As Integer
      lPosErr = ex.Message.LastIndexOf("SqlException: ") + Len("SqlException: ")
      strError = (ex.Message.Substring(lPosErr)).Split(CChar(" "))
      Select Case strError(0)
        Case "2627" 'violation of primary key
          Return False
      End Select
    End Try
  End Function
  Public Overridable Function CopiaDati(ByVal bOverwrite As Boolean, ByVal strElenco As String, ByVal strTabella As String, ByVal strTipo As String, ByVal stredCoddittOr As String, ByVal bopAltrodb As Boolean, ByVal strlbCoddittDest As String, ByVal strImportServer As String, ByVal strImportDatabase As String, ByVal strImportUtente As String, ByVal strImportPassword As String) As Boolean
    Dim dsOrigine As DataSet = Nothing
    Dim i As Integer
    Dim strInsert As String
    Dim strValues As String
    Dim strSQLCopia As String
    Dim strWhere As String
    Dim dttTabella As DataTable = Nothing
    Dim strSet As String
    Dim j As Integer
    Dim dsDestinazione As DataSet = Nothing
    Dim strSQL As String = ""
    Dim strTabellaArr() As String
    Try
      '-----------------------------------------------------------------------------------------
      CopiaDati = False
      '-----------------------------------------------------------------------------------------
      strTabellaArr = strTabella.Split(CChar("|"))
      If strTabellaArr.Length = 1 Then
        strSQL = "SELECT " & strElenco & " FROM " & strTabella
      ElseIf strTabellaArr.Length > 1 Then
        Select Case strTabellaArr(0)
          Case "destdiv"
            strSQL = "SELECT " & strElenco & _
                " FROM destdiv INNER JOIN anagra ON destdiv.codditt = anagra.codditt AND destdiv.dd_conto = anagra.an_conto "
          Case "organig"
            Select Case strTabellaArr(1)
              Case "1"
                strSQL = "SELECT " & strElenco & _
                  " FROM organig INNER JOIN anagra ON organig.codditt = anagra.codditt AND organig.og_conto = anagra.an_conto "
              Case "2"
                strSQL = "SELECT " & strElenco & _
                  " FROM organig INNER JOIN leads ON organig.codditt = leads.codditt AND organig.og_codlead = leads.le_codlead "
            End Select
        End Select
      End If
      strTabella = strTabellaArr(0)
      '-----------------------------------------------------------------------------------------
      '--- Apre un DataSet sulla tabella di origine
      '-----------------------------------------------------------------------------------------
      GetTabellaOrigine(strSQL, strTabella, stredCoddittOr, strTipo, bopAltrodb, strImportServer, strImportDatabase, strImportUtente, strImportPassword, dsOrigine)
      '-----------------------------------------------------------------------------------------
      '--- Ciclo sul recordset origine
      '-----------------------------------------------------------------------------------------
      strInsert = GetQueryInsertField(dsOrigine.Tables("TABELLA"), "")
      For i = 0 To dsOrigine.Tables("TABELLA").Rows.Count - 1
        strValues = GetQueryInsertValue(dsOrigine.Tables("TABELLA"), dsOrigine.Tables("TABELLA").Rows(i), "")
        strSQLCopia = "INSERT INTO " & strTabella & strInsert & " VALUES " & strValues
        'effettua la ricerca del codice ditta di origine anche se maiuscolo e minuscolo
        strSQLCopia = strSQLCopia.Replace("'" & stredCoddittOr & "'", "'" & strlbCoddittDest & "'")
        strSQLCopia = strSQLCopia.Replace("'" & stredCoddittOr.ToUpper & "'", "'" & strlbCoddittDest & "'")
        strSQLCopia = strSQLCopia.Replace("'" & stredCoddittOr.ToLower & "'", "'" & strlbCoddittDest & "'")
        If Not CopiaDatiEsegui(strSQLCopia) Then
          If bOverwrite = True Then
            '-----------------------------------------------------------------------------------------
            '--- Gestisco l'errore di chiave duplicata
            '--- Apre un DataSet sulla tabella di destinazione
            '-----------------------------------------------------------------------------------------
            GetTabellaDestinazione(strSQL, strTabella, strlbCoddittDest, strTipo, bopAltrodb, strImportServer, strImportDatabase, strImportUtente, strImportPassword, dsDestinazione)

            '-----------------------------------------------------------------------------------------
            '--- Ricerca la chiave primaria costruzione where
            '-----------------------------------------------------------------------------------------
            If bopAltrodb = True Then
              ElencoCampiStructDb(strTabella, strImportServer, strImportDatabase, strImportUtente, strImportPassword, 1, dttTabella)
            Else
              ElencoCampiStructDb(strTabella, oApp.Db.ServerDB, oApp.Db.Nome, oApp.Db.NomeUt, oApp.Db.PwdUt, 1, dttTabella)
            End If

            strWhere = ""

            For j = 0 To dttTabella.Rows.Count - 1
              If (CBool(dttTabella.Rows(j)("IsKey")) = True) Then
                strWhere = strWhere & " AND " & NTSCStr(dttTabella.Rows(j)("ColumnName")) & _
                " = " & CStrSQL(NTSCStr(dsOrigine.Tables("TABELLA").Rows(i)(NTSCStr(dttTabella.Rows(j)("ColumnName")))))
              End If
            Next
            If Len(strWhere) > 0 Then
              strWhere = Mid(strWhere, 6)
            End If
            strSet = GetQueryUpdate(dsOrigine.Tables("TABELLA"), dsOrigine.Tables("TABELLA").Rows(i), "")

            strSQLCopia = "UPDATE " & strTabella & " SET " & strSet & " WHERE " & strWhere

            strSQLCopia = strSQLCopia.Replace("'" & stredCoddittOr & "'", "'" & strlbCoddittDest & "'")
            strSQLCopia = strSQLCopia.Replace("'" & stredCoddittOr.ToUpper & "'", "'" & strlbCoddittDest & "'")
            strSQLCopia = strSQLCopia.Replace("'" & stredCoddittOr.ToLower & "'", "'" & strlbCoddittDest & "'")
            If Not CopiaDatiEsegui(strSQLCopia) Then
            End If
          End If
        End If
        '---------------------------------------------------------------------------------------
        SistemaPianoDeiConti(strlbCoddittDest, strTabella, dsOrigine.Tables("TABELLA").Rows(i))
        '---------------------------------------------------------------------------------------
      Next
      '-----------------------------------------------------------------------------------------
      CopiaDati = True
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CopiaDatiTabanaz(ByVal stredCoddittOr As String, ByVal strlbCoddittDest As String, ByVal bopAltrodb As Boolean, ByVal strImportServer As String, ByVal strImportDatabase As String, ByVal strImportUtente As String, ByVal strImportPassword As String) As Boolean
    Dim strSQL As String = ""
    Dim ds As DataSet = Nothing
    Try
      '-----------------------------------------------------------------------------------------
      '--- Apre uno Snapshot sulla ditta di origine
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM tabanaz" & _
        " WHERE codditt = " & CStrSQL(stredCoddittOr)
      If bopAltrodb = True Then
        ds = ImportRecordset(strSQL, "TABANAZ", strImportServer, strImportDatabase, strImportUtente, strImportPassword)
      Else
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")
      End If
      If ds.Tables("TABANAZ").Rows.Count = 0 Then
        Exit Function
      End If
      '-----------------------------------------------------------------------------------------
      '--- Copia i dati da una ditta all'altra
      '--- (spezza l'UPDATE in due altrimenti dà errore 'Too many fields...)
      '-----------------------------------------------------------------------------------------
      With ds.Tables("TABANAZ").Rows(0)
        '---------------------------------------------------------------------------------------
        '--- Prima parte dell'UPDATE
        '---------------------------------------------------------------------------------------
        strSQL = "UPDATE tabanaz" & _
          " SET tb_codanaz = " & NTSCStr(!tb_codanaz) & "," & _
          " tb_mascli_1 = " & NTSCStr(!tb_mascli_1) & "," & _
          " tb_mascli_2 = " & NTSCStr(!tb_mascli_2) & "," & _
          " tb_mascli_3 = " & NTSCStr(!tb_mascli_3) & "," & _
          " tb_mascli_4 = " & NTSCStr(!tb_mascli_4) & "," & _
          " tb_mascli_5 = " & NTSCStr(!tb_mascli_5) & "," & _
          " tb_mascli_6 = " & NTSCStr(!tb_mascli_6) & "," & _
          " tb_mascli_7 = " & NTSCStr(!tb_mascli_7) & "," & _
          " tb_mascli_8 = " & NTSCStr(!tb_mascli_8) & "," & _
          " tb_mascli_9 = " & NTSCStr(!tb_mascli_9) & "," & _
          " tb_mascli_10 = " & NTSCStr(!tb_mascli_10) & ","
        strSQL = strSQL & " tb_masfor_1 = " & NTSCStr(!tb_masfor_1) & "," & _
          " tb_masfor_2 = " & NTSCStr(!tb_masfor_2) & "," & _
          " tb_masfor_3 = " & NTSCStr(!tb_masfor_3) & "," & _
          " tb_masfor_4 = " & NTSCStr(!tb_masfor_4) & "," & _
          " tb_masfor_5 = " & NTSCStr(!tb_masfor_5) & "," & _
          " tb_masfor_6 = " & NTSCStr(!tb_masfor_6) & "," & _
          " tb_masfor_7 = " & NTSCStr(!tb_masfor_7) & "," & _
          " tb_masfor_8 = " & NTSCStr(!tb_masfor_8) & "," & _
          " tb_masfor_9 = " & NTSCStr(!tb_masfor_9) & "," & _
          " tb_masfor_10 = " & NTSCStr(!tb_masfor_10) & "," & _
          " tb_mesech = " & NTSCStr(!tb_mesech) & "," & _
          " tb_crediva = " & CDblSQL(NTSCStr(!tb_crediva)) & "," & _
          " tb_uffiva = " & CStrSQL(NTSCStr(!tb_uffiva)) & "," & _
          " tb_escomp = " & NTSCStr(!tb_escomp) & "," & _
          " tb_codatt = " & CStrSQL(NTSCStr(!tb_codatt)) & "," & _
          " tb_mentri = " & CStrSQL(NTSCStr(!tb_mentri)) & "," & _
          " tb_dtulliq = " & CDataSQL(NTSCStr(!tb_dtulliq)) & "," & _
          " tb_plafond = " & CDblSQL(NTSCStr(!tb_plafond)) & "," & _
          " tb_ventil = " & CStrSQL(NTSCStr(!tb_ventil)) & ","
        strSQL = strSQL & " tb_aztmacq_1 = " & NTSCStr(!tb_aztmacq_1) & "," & _
          " tb_aztmacq_2 = " & NTSCStr(!tb_aztmacq_2) & "," & _
          " tb_aztmacq_3 = " & NTSCStr(!tb_aztmacq_3) & "," & _
          " tb_aztmacq_4 = " & NTSCStr(!tb_aztmacq_4) & "," & _
          " tb_aztmacq_5 = " & NTSCStr(!tb_aztmacq_5) & "," & _
          " tb_aztmacq_6 = " & NTSCStr(!tb_aztmacq_6) & "," & _
          " tb_aztmacq_7 = " & NTSCStr(!tb_aztmacq_7) & "," & _
          " tb_aztmacq_8 = " & NTSCStr(!tb_aztmacq_8) & "," & _
          " tb_aztmacq_9 = " & NTSCStr(!tb_aztmacq_9) & "," & _
          " tb_dtullg = " & CDataSQL(NTSCStr(!tb_dtullg)) & "," & _
          " tb_rgullg = " & NTSCStr(!tb_rgullg) & "," & _
          " tb_riullg = " & NTSCStr(!tb_riullg) & "," & _
          " tb_prdalg = " & CDblSQL(NTSCStr(!tb_prdalg)) & "," & _
          " tb_pravlg = " & CDblSQL(NTSCStr(!tb_pravlg)) & "," & _
          " tb_dtulel = " & CDataSQL(NTSCStr(!tb_dtulel)) & "," & _
          " tb_rgulel = " & NTSCStr(!tb_rgulel) & "," & _
          " tb_dtulgm = " & CDataSQL(NTSCStr(!tb_dtulgm)) & "," & _
          " tb_riulgm = " & NTSCStr(!tb_riulgm) & "," & _
          " tb_prcagm = " & CDblSQL(NTSCStr(!tb_prcagm)) & "," & _
          " tb_prscgm = " & CDblSQL(NTSCStr(!tb_prscgm)) & "," & _
          " tb_dtulap = " & CDataSQL(NTSCStr(!tb_dtulap)) & "," & _
          " tb_dtulst = " & CDataSQL(NTSCStr(!tb_dtulst)) & "," & _
          " tb_dtatsw = " & CDataSQL(NTSCStr(!tb_dtatsw)) & "," & _
          " tb_oratsw = " & NTSCStr(!tb_oratsw) & _
          " WHERE codditt = " & CStrSQL(strlbCoddittDest)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        '---------------------------------------------------------------------------------------
        '--- Seconda parte dell'UPDATE
        '---------------------------------------------------------------------------------------
        strSQL = "UPDATE tabanaz" & _
          " SET tb_dtavsw = " & CDataSQL(NTSCStr(!tb_dtavsw)) & "," & _
          " tb_oravsw = " & NTSCStr(!tb_oravsw) & "," & _
          " tb_dtulrv = " & CDataSQL(NTSCStr(!tb_dtulrv)) & "," & _
          " tb_dtulsv = " & CDataSQL(NTSCStr(!tb_dtulsv)) & "," & _
          " tb_recuiva = " & CDblSQL(NTSCStr(!tb_recuiva)) & "," & _
          " tb_ac12iva = " & CDblSQL(NTSCStr(!tb_ac12iva)) & "," & _
          " tb_dtulrci = " & CDataSQL(NTSCStr(!tb_dtulrci)) & "," & _
          " tb_dtulrcv = " & CDataSQL(NTSCStr(!tb_dtulrcv)) & "," & _
          " tb_rgulrci = " & NTSCStr(!tb_rgulrci) & "," & _
          " tb_rgulrcv = " & NTSCStr(!tb_rgulrcv) & "," & _
          " tb_dtulaca = " & CDataSQL(NTSCStr(!tb_dtulaca)) & "," & _
          " tb_natura = " & NTSCStr(!tb_natura) & "," & _
          " tb_descatt = " & CStrSQL(NTSCStr(!tb_descatt)) & "," & _
          " tb_subcdat = " & NTSCStr(!tb_subcdat) & "," & _
          " tb_dtulppj = " & CDataSQL(NTSCStr(!tb_dtulppj)) & "," & _
          " tb_dtulmrp = " & CDataSQL(NTSCStr(!tb_dtulmrp)) & "," & _
          " tb_plafond2 = " & CDblSQL(NTSCStr(!tb_plafond2)) & "," & _
          " tb_plafond3 = " & CDblSQL(NTSCStr(!tb_plafond3)) & ","
        strSQL = strSQL & " tb_dtulplaf = " & CDataSQL(NTSCStr(!tb_dtulplaf)) & "," & _
          " tb_dtulplaf2 = " & CDataSQL(NTSCStr(!tb_dtulplaf2)) & "," & _
          " tb_dtulplaf3 = " & CDataSQL(NTSCStr(!tb_dtulplaf3)) & "," & _
          " tb_codattx = " & CStrSQL(NTSCStr(!tb_codattx)) & "," & _
          " tb_multatt = " & CStrSQL(NTSCStr(!tb_multatt)) & "," & _
          " tb_regautotr = " & CStrSQL(NTSCStr(!tb_regautotr)) & "," & _
          " tb_crivacomp = " & CDblSQL(NTSCStr(!tb_crivacomp)) & "," & _
          " tb_crivacut = " & CDblSQL(NTSCStr(!tb_crivacut)) & "," & _
          " tb_crivacompa = " & CDblSQL(NTSCStr(!tb_crivacompa)) & "," & _
          " tb_crivacuta = " & CDblSQL(NTSCStr(!tb_crivacuta)) & "," & _
          " tb_intraper = " & CStrSQL(NTSCStr(!tb_intraper)) & "," & _
          " tb_intraperacq = " & CStrSQL(NTSCStr(!tb_intraperacq)) & "," & _
          " tb_azcodpcon = " & CStrSQL(NTSCStr(!tb_azcodpcon)) & "," & _
          " tb_azgestscad = " & CStrSQL(NTSCStr(!tb_azgestscad)) & "," & _
          " tb_azcodstud = " & NTSCStr(!tb_azcodstud) & "," & _
          " tb_azcodgrua = " & NTSCStr(!tb_azcodgrua) & ","
        strSQL = strSQL & "  " & _
          " tb_azcoddpr = " & CStrSQL(NTSCStr(!tb_azcoddpr)) & "," & _
          " tb_azsolo740 = " & CStrSQL(NTSCStr(!tb_azsolo740)) & "," & _
          " tb_pgullg = " & NTSCStr(!tb_pgullg) & "," & _
          " tb_azdoppes = " & CStrSQL(NTSCStr(!tb_azdoppes)) & ","
        strSQL = strSQL & " tb_escompp = " & NTSCStr(!tb_escompp) & "," & _
          " tb_dtullgp = " & CDataSQL(NTSCStr(!tb_dtullgp)) & "," & _
          " tb_rgullgp = " & NTSCStr(!tb_rgullgp) & "," & _
          " tb_riullgp = " & NTSCStr(!tb_riullgp) & "," & _
          " tb_prdalgp = " & CDblSQL(NTSCStr(!tb_prdalgp)) & "," & _
          " tb_pravlgp = " & CDblSQL(NTSCStr(!tb_pravlgp)) & "," & _
          " tb_pgullgp = " & NTSCStr(!tb_pgullgp) & "," & _
          " tb_aznrtipo = " & CStrSQL(NTSCStr(!tb_aznrtipo)) & "," & _
          " tb_aznranid = " & NTSCStr(!tb_aznranid) & "," & _
          " tb_aznranrf = " & NTSCStr(!tb_aznranrf) & "," & _
          " tb_codstatos = " & NTSCStr(!tb_codstatos) & "," & _
          " tb_azesconc = " & CStrSQL(NTSCStr(!tb_azesconc)) & "," & _
          " tb_datascad = " & CDataSQL(NTSCStr(!tb_datascad)) & "," & _
          " tb_siglasoc = " & CStrSQL(NTSCStr(!tb_siglasoc)) & "," & _
          " tb_azcodabi = " & NTSCStr(!tb_azcodabi) & "," & _
          " tb_azcodcab = " & NTSCStr(!tb_azcodcab) & "," & _
          " tb_azrifriba = " & CStrSQL(NTSCStr(!tb_azrifriba)) & "," & _
          " tb_azcodcc = " & CStrSQL(NTSCStr(!tb_azcodcc)) & "," & _
          " tb_azcodcaf = " & NTSCStr(!tb_azcodcaf) & "," & _
          " tb_azdichuni = " & CStrSQL(NTSCStr(!tb_azdichuni)) & ","
        strSQL = strSQL & " tb_flriccf = " & CStrSQL(NTSCStr(!tb_flriccf)) & "," & _
          " tb_capdelco = " & CDblSQL(NTSCStr(!tb_capdelco)) & "," & _
          " tb_capsotco = " & CDblSQL(NTSCStr(!tb_capsotco)) & "," & _
          " tb_capverco = " & CDblSQL(NTSCStr(!tb_capverco)) & "," & _
          " tb_capvalaz = " & CDblSQL(NTSCStr(!tb_capvalaz)) & "," & _
          " tb_capnumaz = " & CDblSQL(NTSCStr(!tb_capnumaz)) & "," & _
          " tb_dtulvcs = " & CDataSQL(NTSCStr(!tb_dtulvcs)) & "," & _
          " tb_dtulvat = " & CDataSQL(NTSCStr(!tb_dtulvat)) & "," & _
          " tb_dtulvng = " & CDataSQL(NTSCStr(!tb_dtulvng)) & "," & _
          " tb_dtulliqtri = " & CDataSQL(NTSCStr(!tb_dtulliqtri)) & "," & _
          " tb_dtulcalamm = " & CDataSQL(NTSCStr(!tb_dtulcalamm)) & "," & _
          " tb_dtulcontce = " & CDataSQL(NTSCStr(!tb_dtulcontce)) & "," & _
          " tb_dtulregce = " & CDataSQL(NTSCStr(!tb_dtulregce)) & "," & _
          " tb_pgulregce = " & CDblSQL(NTSCDec(!tb_pgulregce)) & "," & _
          " tb_azcodpcca = " & CStrSQL(NTSCStr(!tb_azcodpcca)) & "," & _
          " tb_codtcdc = " & CDblSQL(NTSCInt(!tb_codtcdc)) & _
          " WHERE codditt = " & CStrSQL(strlbCoddittDest)
      End With
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ImportaSelectTabanaz(ByVal strDitta As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_azcodpcon FROM tabanaz" & _
        " WHERE codditt = " & CStrSQL(strDitta)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ImportaGetOrderTbl(ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ot_order, ot_nometab FROM ordertbl" & _
        " WHERE ot_perditta = 'S'" & _
        " AND ot_istmp = 'N'" & _
        " AND UPPER(ot_nometab) <> 'TABANAZ' AND UPPER(ot_nometab) <> 'ANAZUL' " & _
        " ORDER BY ot_order"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORDERTBL")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ImportaDeleteTabella(ByVal strDitta As String, ByVal strot_nometab As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM " & strot_nometab & _
        " WHERE codditt = " & CStrSQL(strDitta)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ImportaInsertIntoTabella(ByVal strot_nometab As String, ByVal strElenco As String, ByVal strElenco1 As String, ByVal stredCoddittOr As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO " & strot_nometab & " (" & strElenco & ")  " & _
        " SELECT " & strElenco1 & _
        " FROM " & strot_nometab & _
        " WHERE codditt = " & CStrSQL(stredCoddittOr)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ImportaElaborazioniParticolari(ByVal bckScollegaAG As Boolean, ByVal bckAbilPS As Boolean, ByVal bckDisabPS As Boolean, ByVal bckSvuotaPN As Boolean, ByVal strlbCoddittDest As String) As Boolean
    Dim strSQL As String = ""
    Try
      '---------------------------------------------------------------------------------------
      '--- Adesso esegue elaborazioni particolari
      '---------------------------------------------------------------------------------------
      If bckScollegaAG Then
        strSQL = "UPDATE anagra set an_codanag = 0 where codditt = " & CStrSQL(strlbCoddittDest) & " and an_tipo <> 'S' "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      If bckAbilPS Then
        strSQL = "UPDATE anagra set an_partite = 'S', an_scaden = 'S' where codditt = " & CStrSQL(strlbCoddittDest) & " and an_tipo <> 'S' "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        '-------------------------------------------------------------------------------------
        '--- Setta i flag anche in tabanaz
        '-------------------------------------------------------------------------------------
        strSQL = "UPDATE tabanaz set tb_ventil = 'S', tb_azgestscad = 'S' where codditt = " & CStrSQL(strlbCoddittDest) & "  "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      If bckDisabPS Then
        strSQL = "UPDATE anagra set an_partite = 'N', an_scaden = 'N' where codditt = " & CStrSQL(strlbCoddittDest) & " and an_tipo <> 'S' "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        '-------------------------------------------------------------------------------------
        '--- Setta i flag anche in tabanaz
        '-------------------------------------------------------------------------------------
        strSQL = "UPDATE tabanaz set tb_ventil = 'N', tb_azgestscad = 'N' where codditt = " & CStrSQL(strlbCoddittDest) & "  "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If
      If bckSvuotaPN Then '--- Svuota gli archivi della contabilità...
        strSQL = "DELETE from PRINCPAG where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from PRFATT where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from PNNOTE where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from MOVUSAT where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from MOVIVA where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from SCADEN where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        ' PER ORA NON CANCELLA RITENUTE...
        '      strSQL = "DELETE from PAGCOMP where codditt = " & CStrSql(strlbCoddittDest) & " "
        '      gdb.Execute strSQL
        '      strSQL = "DELETE from RITENUT where codditt = " & CStrSql(strlbCoddittDest) & " "
        '      gdb.Execute strSQL
        strSQL = "DELETE from PRINOT where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from PRINCOMP where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from TABDURI where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from TABPRIV where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from TABREGB where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from TABPRVE where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from TABLIPE where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from TABPRCR where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        strSQL = "DELETE from ESTROMIS where codditt = " & CStrSQL(strlbCoddittDest) & " "
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ImportaSelectPoiInsertIntoTabella(ByVal strot_nometab As String, ByVal strElenco As String, ByVal strElenco1 As String, ByVal stredCoddittOr As String) As Boolean
    Dim strSQL As String = ""
    Dim dsOrigine As DataSet = Nothing
    Dim i As Integer
    Dim strValues As String
    Try
      strSQL = " SELECT " & strElenco1 & _
          " FROM " & strot_nometab & _
          " WHERE codditt = " & CStrSQL(stredCoddittOr)

      dsOrigine = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABELLA")

      If strot_nometab.ToUpper = "TABNUMA" Then
        'controllo che la serie sia valorizzata se no la forzo a blank se no da errore
        For i = 0 To dsOrigine.Tables("TABELLA").Rows.Count - 1
          If NTSCStr(dsOrigine.Tables("TABELLA").Rows(i)!tb_numserie) = "" Then
            dsOrigine.Tables("TABELLA").Rows(i)!tb_numserie = " "
          End If
        Next
      End If

      For i = 0 To dsOrigine.Tables("TABELLA").Rows.Count - 1
        strValues = GetQueryInsertValue(dsOrigine.Tables("TABELLA"), dsOrigine.Tables("TABELLA").Rows(i), "")
        strSQL = "INSERT INTO " & strot_nometab & " (" & strElenco & ") VALUES " & strValues

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CopiaDatiLis(ByVal strWhere As String, ByVal strlbCoddittDest As String, _
                                         ByVal bopAltrodb As Boolean, ByVal stredCoddittOr As String, _
                                         ByVal strImportServer As String, ByVal strImportDatabase As String, _
                                         ByVal strImportUtente As String, ByVal strImportPassword As String) As Boolean
    Dim strSQL As String = ""
    Dim ds As DataSet = Nothing
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Apre uno Dataset sulla ditta di origine
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT listini.* FROM listini"
      strSQL = strSQL & " WHERE listini.codditt = " & CStrSQL(stredCoddittOr) & _
      " AND " & strWhere
      If bopAltrodb = True Then
        ds = ImportRecordset(strSQL, "LISTINI", strImportServer, strImportDatabase, strImportUtente, strImportPassword)
      Else
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")
      End If
      If ds.Tables("LISTINI").Rows.Count = 0 Then
        Return True
      End If
      '-----------------------------------------------------------------------------------------
      '--- Cancella e Copia i dati da una ditta all'altra
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM listini" & _
        " WHERE listini.codditt = " & CStrSQL(strlbCoddittDest) & _
        " AND " & strWhere
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      For i = 0 To ds.Tables("LISTINI").Rows.Count - 1
        With ds.Tables("LISTINI").Rows(i)
          strSQL = "INSERT INTO listini" & _
          " (codditt, lc_codart, lc_codlavo, lc_conto, lc_codvalu, lc_codtpro, lc_listino," & _
          " lc_datagg, lc_tipo, lc_prezzo, lc_datscad, lc_daquant, lc_aquant, lc_perqta, lc_unmis," & _
          " lc_note, lc_netto, lc_fase, lc_ultagg, lc_codcas, lc_coddest)" & _
          " VALUES (" & CStrSQL(strlbCoddittDest) & "," & CStrSQL(!lc_codart) & "," & NTSCStr(!lc_codlavo) & "," & NTSCStr(!lc_conto) & "," & _
          NTSCStr(!lc_codvalu) & "," & NTSCStr(!lc_codtpro) & "," & NTSCStr(!lc_listino) & "," & CDataSQL(NTSCDate(!lc_datagg)) & "," & CStrSQL(!lc_tipo) & "," & _
          CDblSQL(NTSCDec(!lc_prezzo)) & "," & CDataSQL(NTSCDate(!lc_datscad)) & "," & CDblSQL(NTSCDec(!lc_daquant)) & "," & CDblSQL(NTSCDec(!lc_aquant)) & "," & CDblSQL(NTSCDec(!lc_perqta)) & "," & _
          CStrSQL(!lc_unmis) & "," & CStrSQL(!lc_note) & "," & CStrSQL(!lc_netto) & "," & NTSCStr(!lc_fase) & "," & CDataSQL(NTSCDate(!lc_ultagg)) & "," & _
          CStrSQL(!lc_codcas) & ", " & NTSCStr(!lc_coddest) & ")"
        End With
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CopiaDatiProv(ByVal strWhere As String, ByVal strlbCoddittDest As String, _
                                           ByVal bopAltrodb As Boolean, ByVal stredCoddittOr As String, _
                                           ByVal strImportServer As String, ByVal strImportDatabase As String, _
                                           ByVal strImportUtente As String, ByVal strImportPassword As String) As Boolean
    Dim strSQL As String = ""
    Dim ds As DataSet = Nothing
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Apre uno Snapshot sulla ditta di origine
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT perprov.* FROM perprov"
      strSQL = strSQL & " WHERE perprov.codditt = " & CStrSQL(stredCoddittOr) & _
      " AND " & strWhere
      If bopAltrodb = True Then
        ds = ImportRecordset(strSQL, "PERPROV", strImportServer, strImportDatabase, strImportUtente, strImportPassword)
      Else
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "PERPROV")
      End If
      If ds.Tables("PERPROV").Rows.Count = 0 Then
        Return True
      End If
      '-----------------------------------------------------------------------------------------
      '--- Cancella e Copia i dati da una ditta all'altra
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM perprov" & _
        " WHERE codditt = " & CStrSQL(strlbCoddittDest) & _
        " AND " & strWhere
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      For i = 0 To ds.Tables("PERPROV").Rows.Count - 1
        With ds.Tables("PERPROV").Rows(i)
          strSQL = "INSERT INTO perprov" & _
          " (codditt, per_codcage, per_codart, per_conto, per_clpran, per_clprar, per_provv," & _
          " per_datagg, per_datscad, per_codtpro, per_vprovv, per_unmis, per_ultagg, per_coddest)" & _
          " VALUES (" & CStrSQL(strlbCoddittDest) & "," & NTSCStr(!per_codcage) & "," & CStrSQL(!per_codart) & "," & NTSCStr(!per_conto) & "," & _
          NTSCStr(!per_clpran) & "," & NTSCStr(!per_clprar) & "," & CDblSQL(NTSCDec(!per_provv)) & "," & CDataSQL(NTSCDate(!per_datagg)) & "," & CDataSQL(NTSCDate(!per_datscad)) & "," & _
          NTSCStr(!per_codtpro) & "," & CDblSQL(NTSCDec(!per_vprovv)) & "," & CStrSQL(!per_unmis) & "," & CDataSQL(NTSCDate(!per_ultagg)) & "," & NTSCStr(!per_coddest) & ")"
        End With
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CopiaDatiScon(ByVal strWhere As String, ByVal strlbCoddittDest As String, _
                                           ByVal bopAltrodb As Boolean, ByVal stredCoddittOr As String, _
                                           ByVal strImportServer As String, ByVal strImportDatabase As String, _
                                           ByVal strImportUtente As String, ByVal strImportPassword As String) As Boolean
    Dim strSQL As String = ""
    Dim ds As DataSet = Nothing
    Dim i As Integer
    Try
      '-----------------------------------------------------------------------------------------
      '--- Apre uno Dataset sulla ditta di origine
      '-----------------------------------------------------------------------------------------
      strSQL = "SELECT sconti.* FROM sconti"
      strSQL = strSQL & " WHERE sconti.codditt = " & CStrSQL(stredCoddittOr) & _
      " AND " & strWhere
      If bopAltrodb = True Then
        ds = ImportRecordset(strSQL, "SCONTI", strImportServer, strImportDatabase, strImportUtente, strImportPassword)
      Else
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SCONTI")
      End If
      If ds.Tables("SCONTI").Rows.Count = 0 Then
        Return True
      End If
      '-----------------------------------------------------------------------------------------
      '--- Cancella e Copia i dati da una ditta all'altra
      '-----------------------------------------------------------------------------------------
      strSQL = "DELETE FROM sconti" & _
        " WHERE sconti.codditt = " & CStrSQL(strlbCoddittDest) & _
        " AND " & strWhere
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      For i = 0 To ds.Tables("SCONTI").Rows.Count - 1
        With ds.Tables("SCONTI").Rows(i)
          strSQL = "INSERT INTO sconti" & _
          " (codditt, so_codart, so_conto, so_clscan, so_clscar, so_scont1, so_scont2, so_scont3," & _
          " so_datagg, so_datscad, so_codtpro, so_daquant, so_aquant, so_scont4, so_scont5, so_scont6," & _
          " so_unmis, so_fase, so_ultagg, so_coddest)" & _
          " VALUES (" & CStrSQL(strlbCoddittDest) & "," & CStrSQL(!so_codart) & "," & NTSCStr(!so_conto) & "," & NTSCStr(!so_clscan) & "," & _
          NTSCStr(!so_clscar) & "," & CDblSQL(NTSCDec(!so_scont1)) & "," & CDblSQL(NTSCDec(!so_scont2)) & "," & CDblSQL(NTSCDec(!so_scont3)) & "," & CDataSQL(NTSCDate(!so_datagg)) & "," & _
          CDataSQL(NTSCDate(!so_datscad)) & "," & NTSCStr(!so_codtpro) & "," & CDblSQL(NTSCDec(!so_daquant)) & "," & CDblSQL(NTSCDec(!so_aquant)) & "," & CDblSQL(NTSCDec(!so_scont4)) & "," & _
          CDblSQL(NTSCDec(!so_scont5)) & "," & CDblSQL(NTSCDec(!so_scont6)) & "," & CStrSQL(!so_unmis) & "," & NTSCStr(!so_fase) & "," & CDataSQL(NTSCDate(!so_ultagg)) & "," & NTSCStr(!so_coddest) & ")"
        End With
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  'funzioni per lettura da altro SqlServer
  Public Overridable Function ImportConnettiSql(ByVal strImportServer As String, ByVal strImportDatabase As String, ByVal strImportUtente As String, ByVal strImportPassword As String) As SqlConnection
    Dim dbConn As SqlConnection = Nothing
    Dim strConnect As String
    Try
      If strImportUtente = "*" And strImportPassword = "*" Then
        'se sicurezza integrata (non mi è stato passato il flag di sicurezza integrata, ma se lo è in be__menu utente + pwd vengono impostati = '*'
        strConnect = "Server=" & strImportServer & ";Database=" & strImportDatabase & ";Trusted_Connection=Yes;LANGUAGE=us_english;APP=Business"
      Else
        strConnect = "Server=" & strImportServer & ";Database=" & strImportDatabase & ";UID=" & strImportUtente & ";pwd=" & strImportPassword & ";LANGUAGE=us_english;APP=Business"
      End If

      dbConn = New SqlConnection(strConnect)

      Try
        dbConn.Open()
      Catch ex As Exception
        Return Nothing
      End Try

      Return dbConn

    Catch ex As Exception
      '---------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '---------------------------------------------------------
      Return Nothing
    End Try
  End Function
  Public Overridable Function ImportRecordset(ByVal strQuery As String, ByVal strNomeTabella As String, ByVal strImportServer As String, ByVal strImportDatabase As String, ByVal strImportUtente As String, ByVal strImportPassword As String) As DataSet
    Dim dbImportConn As SqlClient.SqlConnection = Nothing
    Dim dbImportCmd As SqlClient.SqlCommand = Nothing
    Dim dbImportAdapt As SqlClient.SqlDataAdapter = Nothing
    Dim dsImportOut As New DataSet
    Dim i As Integer
    Try
      'apro il database
      If dbImportConn Is Nothing Then dbImportConn = ImportConnettiSql(strImportServer, strImportDatabase, strImportUtente, strImportPassword)
      dbImportCmd = New SqlCommand(strQuery, dbImportConn)
      dbImportCmd.CommandTimeout = oApp.Db.QueryTimeOut
      dbImportAdapt = New SqlDataAdapter(dbImportCmd)
      'ottengo i dati
      dbImportAdapt.Fill(dsImportOut)
      dsImportOut.Tables(dsImportOut.Tables.Count - 1).TableName = strNomeTabella
      'chiudo il database
      dbImportConn.Close()

      '------------------------------------
      'traduco i nomi dell'intestazioni delle colonne tutto in minuscolo.
      'diversamente se devo collegare la tabella ad una griglia ed i nomi colonne sono diversi (case sensitive) 
      'con i FieldName delle colonne della griglia la colonna rimane vuota
      For i = 0 To dsImportOut.Tables(strNomeTabella).Columns.Count - 1
        dsImportOut.Tables(strNomeTabella).Columns(i).ColumnName = dsImportOut.Tables(strNomeTabella).Columns(i).ColumnName.ToLower
      Next

      Return dsImportOut

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato,
      'passando come informazione aggiuntiva la query che ho eseguito
      Throw (New NTSException(GestError(ex, Me, strQuery, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return New DataSet
    End Try
  End Function
  Public Overridable Function ElencoCampiStructDb(ByVal strTabella As String, ByVal strImportServer As String, ByVal strImportDatabase As String, ByVal strImportUtente As String, ByVal strImportPassword As String, ByVal lTipoReader As Integer, ByRef dttImport As DataTable) As Boolean
    Dim strSQL As String = ""

    Dim drRead As SqlDataReader
    Dim dbImportConn As SqlClient.SqlConnection = Nothing
    Dim dbImportCmd As SqlClient.SqlCommand = Nothing
    Dim dbImportAdapt As SqlDataAdapter = Nothing
    Try
      strSQL = "SELECT * FROM " & strTabella
      If dbImportConn Is Nothing Then dbImportConn = ImportConnettiSql(strImportServer, strImportDatabase, strImportUtente, strImportPassword)
      dbImportCmd = New SqlCommand(strSQL, dbImportConn)
      dbImportCmd.CommandTimeout = oApp.Db.QueryTimeOut
      If lTipoReader = 1 Then
        drRead = dbImportCmd.ExecuteReader(CommandBehavior.KeyInfo) 'attenzione restituisce chiave primaria forse rallentare
      Else
        drRead = dbImportCmd.ExecuteReader() 'attenzione restituisce chiave primaria forse rallentare
      End If
      dttImport = drRead.GetSchemaTable()

      dbImportConn.Close()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ControllaTabAnaz(ByVal strDitta As String, ByVal bopAltrodb As Boolean, ByVal strImportServer As String, ByVal strImportDatabase As String, ByVal strImportUtente As String, ByVal strImportPassword As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_azcodpcon FROM tabanaz" & _
        " WHERE codditt = " & CStrSQL(strDitta)
      If bopAltrodb = True Then
        ds = ImportRecordset(strSQL, "TABELLA", strImportServer, strImportDatabase, strImportUtente, strImportPassword)
      Else
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABELLA")
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ImportaDaAltroDb(ByVal strDitta As String, ByVal strTabella As String, ByVal strElenco As String, ByVal strElenco1 As String, ByVal strDbdest As String, ByVal strImportServer As String, ByVal strImportDatabase As String, ByVal strImportUtente As String, ByVal strImportPassword As String) As Boolean
    Try
      'obsoleta
      Return ImportaDaAltroDb(strDitta, strTabella, strElenco, strElenco1, strDbdest, strImportServer, strImportDatabase, strImportUtente, strImportPassword, "")
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ImportaDaAltroDb(ByVal strDitta As String, ByVal strTabella As String, _
    ByVal strElenco As String, ByVal strElenco1 As String, ByVal strDbdest As String, _
    ByVal strImportServer As String, ByVal strImportDatabase As String, ByVal strImportUtente As String, _
    ByVal strImportPassword As String, ByRef strErrorOut As String) As Boolean
    Dim strSQL As String = ""
    Dim dsOrigine As DataSet = Nothing
    Dim i As Integer
    Dim strValues As String
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strTabella, strElenco, strElenco1, strDbdest, _
                                             strImportServer, strImportDatabase, strImportUtente, _
                                             strImportPassword, strErrorOut})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strErrorOut = NTSCStr(oIn(9))
        Return CBool(oOut)
      End If
      '----------------

      strSQL = " SELECT " & strElenco1 & _
          " FROM " & strTabella & _
          " WHERE codditt = " & CStrSQL(strDitta)

      dsOrigine = ImportRecordset(strSQL, "TABELLA", strImportServer, strImportDatabase, strImportUtente, strImportPassword)

      Select Case strTabella.ToUpper
        Case "TABNUMA"
          'controllo che la serie sia valorizzata se no la forzo a blank se no da errore
          For i = 0 To dsOrigine.Tables("TABELLA").Rows.Count - 1
            If NTSCStr(dsOrigine.Tables("TABELLA").Rows(i)!tb_numserie) = "" Then
              dsOrigine.Tables("TABELLA").Rows(i)!tb_numserie = " "
            End If
          Next
        Case "ALLOLE"
          Dim sqlConn As New SqlConnection(oApp.Db.Connect)
          sqlConn.Open()
          Dim cmdConn As SqlCommand = Nothing
          Dim gByte() As Byte = Nothing
          For i = 0 To (dsOrigine.Tables("TABELLA").Rows.Count - 1)
            strValues = GetQueryInsertValue(dsOrigine.Tables("TABELLA"), dsOrigine.Tables("TABELLA").Rows(i), "")
            strSQL = "INSERT INTO " & strTabella & " (" & strElenco & ") VALUES " & strValues
            strSQL = strSQL.Replace("SystemByte[]", "@binaryValue")
            If Microsoft.VisualBasic.InStr(strSQL, "@binaryValue") <> 0 Then
              cmdConn = New SqlCommand("", sqlConn)
              cmdConn.CommandText = strSQL
              gByte = CType(dsOrigine.Tables("TABELLA").Rows(i)!ao_ole, Byte())
              cmdConn.Parameters.Add("@binaryValue", SqlDbType.VarBinary, gByte.Length).Value = CType(dsOrigine.Tables("TABELLA").Rows(i)!ao_ole, Byte())
              cmdConn.ExecuteNonQuery()
            Else
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
            End If
          Next
          sqlConn.Close()
          Return True
        Case "MOVMAG"
          For i = 0 To (dsOrigine.Tables("TABELLA").Rows.Count - 1)
            With dsOrigine.Tables("TABELLA").Rows(i)
              If NTSCInt(!mm_ccnum) > 0 And NTSCStr(!mm_ccserie) = "" Then !mm_ccserie = " "
              If NTSCInt(!mm_clnum) > 0 And NTSCStr(!mm_clserie) = "" Then !mm_clserie = " "
            End With
          Next
          dsOrigine.Tables("TABELLA").AcceptChanges()
      End Select

      If strTabella.ToUpper = "TABNUMA" Then
        'controllo che la serie sia valorizzata se no la forzo a blank se no da errore
        For i = 0 To dsOrigine.Tables("TABELLA").Rows.Count - 1
          If NTSCStr(dsOrigine.Tables("TABELLA").Rows(i)!tb_numserie) = "" Then
            dsOrigine.Tables("TABELLA").Rows(i)!tb_numserie = " "
          End If
        Next
      End If

      For i = 0 To dsOrigine.Tables("TABELLA").Rows.Count - 1
        strValues = GetQueryInsertValue(dsOrigine.Tables("TABELLA"), dsOrigine.Tables("TABELLA").Rows(i), "")
        strSQL = "INSERT INTO " & strTabella & " (" & strElenco & ") VALUES " & strValues

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Next

      Return True

    Catch ex As Exception
      strErrorOut = vbCrLf & ex.Message & " " & oApp.Tr(Me, 129616696689560547, "QUERY: ") & vbCrLf & strSQL
      Dim strError() As String
      Dim lPosErr As Integer
      lPosErr = ex.Message.LastIndexOf("SqlException: ") + Len("SqlException: ")
      strError = (ex.Message.Substring(lPosErr)).Split(CChar(" "))
      Select Case strError(0)
        Case "2627" 'violation of primary key
          Return False
        Case "547" 'delete reference
          Return False
      End Select
    End Try
  End Function
  Public Overridable Function GetTabellaOrigine(ByVal strSQL As String, ByVal strTabella As String, ByVal stredCoddittOr As String, ByVal strTipo As String, ByVal bopAltrodb As Boolean, ByVal strImportServer As String, ByVal strImportDatabase As String, ByVal strImportUtente As String, ByVal strImportPassword As String, ByRef ds As DataSet) As Boolean
    Try
      strSQL = strSQL & " WHERE " & (strTabella).ToLower & ".codditt = " & CStrSQL(stredCoddittOr)
      If strTipo <> "" Then
        Select Case (strTabella).ToLower
          Case "anagra", "destdiv"
            strSQL = strSQL & " AND an_tipo = " & CStrSQL(strTipo)
          Case "organig"
            If strTipo <> "L" Then strSQL = strSQL & " AND an_tipo = " & CStrSQL(strTipo)
          Case "anaxet"
            Select Case CStrSQL(strTipo)
              Case "C" : strSQL = strSQL & " AND ax_tipork IN ('C', 'D', 'L')"
              Case "F" : strSQL = strSQL & " AND ax_tipork IN ('F', 'E')"
            End Select
        End Select
      End If
      If bopAltrodb = True Then
        ds = ImportRecordset(strSQL, "TABELLA", strImportServer, strImportDatabase, strImportUtente, strImportPassword)
      Else
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABELLA")
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetTabellaDestinazione(ByVal strSQL As String, ByVal strTabella As String, ByVal strlbCoddittDest As String, ByVal strTipo As String, ByVal bopAltrodb As Boolean, ByVal strImportServer As String, ByVal strImportDatabase As String, ByVal strImportUtente As String, ByVal strImportPassword As String, ByRef ds As DataSet) As Boolean
    Try
      strSQL = strSQL & " WHERE " & (strTabella).ToLower & ".codditt = " & CStrSQL(strlbCoddittDest)
      If strTipo <> "" Then
        Select Case (strTabella).ToLower
          Case "anagra", "destdiv"
            strSQL = strSQL & " AND an_tipo = " & CStrSQL(strTipo)
          Case "organig"
            If strTipo <> "L" Then strSQL = strSQL & " AND an_tipo = " & CStrSQL(strTipo)
          Case "anaext"
            Select Case CStrSQL(strTipo)
              Case "C" : strSQL = strSQL & " AND ax_tipork IN ('C', 'D', 'L')"
              Case "F" : strSQL = strSQL & " AND ax_tipork IN ('F', 'E')"
            End Select
        End Select
      End If

      If bopAltrodb = True Then
        ds = ImportRecordset(strSQL, "TABELLA", strImportServer, strImportDatabase, strImportUtente, strImportPassword)
      Else
        ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABELLA")
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ElencoCampiGetStruttura(ByVal strModel As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 * FROM " & strModel

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, strModel)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function DelDataGetTabella(ByVal strNomeTabella As String, ByVal strlbCoddittDest As String, ByVal strParam As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM " & strNomeTabella & _
        " WHERE codditt = " & CStrSQL(strlbCoddittDest)
      If strParam <> "" Then
        Select Case strNomeTabella
          Case "anagra" : strSQL = strSQL & " AND an_tipo = " & CStrSQL(strParam)
        End Select
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      Dim strError() As String
      Dim lPosErr As Integer
      lPosErr = ex.Message.LastIndexOf("SqlException: ") + Len("SqlException: ")
      strError = (ex.Message.Substring(lPosErr)).Split(CChar(" "))
      Select Case strError(0)
        Case "547" 'delete reference
          Return False
      End Select
    End Try
  End Function

  Public Overridable Function SistemaPianoDeiConti(ByVal strCoddittDest As String, ByVal strTabella As String, _
      ByVal dtrT As DataRow) As Boolean
    Dim strSQL As String = ""
    Dim strCodpcon As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tb_azcodpcon FROM tabanaz" & _
        " WHERE codditt = " & CStrSQL(strCoddittDest)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then strCodpcon = NTSCStr(dttTmp.Rows(0)!tb_azcodpcon)
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      If strCodpcon.Trim = "" Then Return True
      '--------------------------------------------------------------------------------------------------------------
      Select Case strTabella.ToLower
        Case "anagra"
          strSQL = "UPDATE anagra" & _
            " SET an_codpcon = " & CStrSQL(strCodpcon) & _
            " WHERE codditt = " & CStrSQL(strCoddittDest) & _
            " AND an_conto = " & NTSCInt(dtrT!an_conto)
        Case Else : Return True
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strError() As String
      Dim lPosErr As Integer
      lPosErr = ex.Message.LastIndexOf("SqlException: ") + Len("SqlException: ")
      strError = (ex.Message.Substring(lPosErr)).Split(CChar(" "))
      Select Case strError(0)
        Case "2627" 'violation of primary key
          Return False
      End Select
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

#End Region

End Class
