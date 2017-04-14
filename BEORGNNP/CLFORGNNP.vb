Imports System.Data
Imports NTSInformatica.CLN__STD
Public Class CLFORGNNP
    Inherits CLEORGNNP
    Public Overloads Function ComponiQuerySeic(ByVal strTipo As String, ByVal stredADataCons As String,
                                                  ByVal stredCodmaga As String, ByVal stredSerie As String,
                                                  ByVal bckTutto As Boolean, ByVal bopTuttiClienti As Boolean,
                                                  ByVal stredDalcliente As String,
                                                  ByVal stredAlcliente As String, ByVal bopTutteDate As Boolean,
                                                  ByVal stredDalladata As String, ByVal stredAlladata As String,
                                                  ByVal bopTutteBolle As Boolean, ByVal stredTipobf As String,
                                                  ByVal bopTuttiAgenti As Boolean, ByVal stredCodcage As String,
                                                  ByVal bopTutteZone As Boolean, ByVal stredCodzona As String,
                                                  ByVal bckConf As Boolean, ByVal strTipoConferma As String,
                                                  ByVal stredCommecaini As String, ByVal stredCommecafin As String,
                                                  ByRef nSenuAnno As Integer, ByRef strSenuSerie As String,
                                                  ByVal strDitta As String, ByVal lIITTOltomo As Integer,
                                                  ByRef bDaSelezioneArticoli As Boolean, ByVal strWhereFiar As String,
                                                  ByRef strSeicQuery As String, ByVal bEscudiRetail As Boolean) As Boolean

        Dim strSQL As String = ""
        Dim strQuery As String = ""
        Dim dsTmp As DataSet = Nothing
        Dim i As Integer

        Try

            '----------------
            'per compatibilita' con funzioni ereditate da rive in versioni precedenti
            Dim oOut As Object = Nothing
            Dim oIn As New ArrayList(New Object() {strTipo, stredADataCons, stredCodmaga, stredSerie, bckTutto,
                                                   bopTuttiClienti, stredDalcliente, stredAlcliente, bopTutteDate,
                                                   stredDalladata, stredAlladata, bopTutteBolle, stredTipobf,
                                                   bopTuttiAgenti, stredCodcage, bopTutteZone, stredCodzona,
                                                   bckConf, strTipoConferma, stredCommecaini, stredCommecafin,
                                                   nSenuAnno, strSenuSerie, strDitta, lIITTOltomo, bDaSelezioneArticoli,
                                                   strWhereFiar, strSeicQuery, bEscudiRetail})
            If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
                nSenuAnno = NTSCInt(oIn(21))
                strSenuSerie = NTSCStr(oIn(22))
                bDaSelezioneArticoli = CBool(oIn(25))
                strSeicQuery = NTSCStr(oIn(27))
                Return CBool(oOut)
            End If
            '----------------

            strSeicQuery = "td_tipork = '" & strTipo & "' AND movord.mo_datcons <= " & CDataSQL(stredADataCons) & " and movord.mo_magaz = " & stredCodmaga

            MsgBox(strSeicQuery)

            If bEscudiRetail Then strSeicQuery += " AND td_retail <> 'S'" 'se ic fatto da retail con modulo promozioni abilitato, posso evaderli solo da programma di retail
            If stredSerie <> "*" Then
                strSeicQuery += " AND td_serie = '" & stredSerie & "'"
            End If
            If bckTutto = False Then
                'DAL CLIENTE AL CLIENTE
                If bopTuttiClienti = False Then
                    strSeicQuery += " AND (td_conto BETWEEN " & stredDalcliente & " AND " & stredAlcliente & ")"
                End If
                'Data bolla
                If bopTutteDate = False Then
                    strSeicQuery += " AND (td_datord BETWEEN " & CDataSQL(stredDalladata) & " AND " & CDataSQL(stredAlladata) & ")"
                End If
                'tipobf
                If bopTutteBolle = False Then
                    strSeicQuery += " AND (td_tipobf = " & stredTipobf & ")"
                End If
                'agente
                If bopTuttiAgenti = False Then
                    strSeicQuery += " AND (td_codagen = " & stredCodcage & ")"
                End If
                'zona
                If bopTutteZone = False Then
                    strSeicQuery += " AND (an_zona = " & stredCodzona & ")"
                End If
                'Confermato
                If bckConf = True Then
                    If strTipoConferma = "T" Then
                        strSeicQuery += " AND (td_confermato = 'S')"
                    Else
                        strSeicQuery += " AND (movord.mo_confermato = 'S')"
                    End If
                End If
                'Commessa
                If (NTSCInt(stredCommecaini) <> 0) Or (NTSCInt(stredCommecafin) <> 999999999) Then
                    strSeicQuery += " AND (movord.mo_commeca BETWEEN " & NTSCInt(stredCommecaini) & " AND " & NTSCInt(stredCommecafin) & ")"
                End If
            End If

            '------------------------------------------------------------------------------------
            '--- Se è stata chiamata la selezione sul Numero Ordine aggiunge alla query
            '------------------------------------------------------------------------------------
            If (nSenuAnno > 0) And (strSenuSerie <> "") Then
                strSeicQuery += " AND movord.mo_anno = " & nSenuAnno
                strSQL = "SELECT * FROM TTOLTOMO " &
                         " WHERE codditt = " & CStrSQL(strDitta) &
                         " AND instid = " & lIITTOltomo '& _
                '" ORDER BY instid, tt_moserie, tt_monumrod"

                dsTmp = oCldGnnp.OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TTOLTOMO")

                For i = 0 To dsTmp.Tables("TTOLTOMO").Rows.Count - 1
                    strQuery = strQuery & " ( movord.mo_serie = " & CStrSQL(dsTmp.Tables("TTOLTOMO").Rows(i)!tt_moserie) & " AND " &
                             "movord.mo_numord = " & NTSCStr(dsTmp.Tables("TTOLTOMO").Rows(i)!tt_monumord) & " ) OR "
                Next

                strQuery = Left(strQuery, (Len(strQuery) - 4))
                If Trim(strQuery) <> "" Then
                    strSeicQuery += " AND (" & strQuery & ") "
                End If
                'Azzera i campi di riferimento x le prossime volte.
                nSenuAnno = 0
                strSenuSerie = " "
            End If
            '----------------------------------------------------------------------------------------
            '--- Se è stata chiamata la selezione per articolo
            '----------------------------------------------------------------------------------------
            If bDaSelezioneArticoli = True Then
                oCldGnnp.TraduciWhere(strWhereFiar, strSeicQuery)
                bDaSelezioneArticoli = False
            End If

            Return True
        Catch ex As Exception
            '--------------------------------------------------------------
            Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
            '--------------------------------------------------------------
        End Try
    End Function
End Class
