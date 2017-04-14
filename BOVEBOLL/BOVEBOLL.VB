Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Runtime.InteropServices
'iniziato a tradurre il 08/09/08
'finito                 16/11/08
Imports System.IO

Imports System
Imports System.Data.SqlClient

Public Class FROVEBOLL

    Public Overrides Sub FRMVEBOLL_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.FRMVEBOLL_Load(sender, e)
        ' oApp.Batch = True
        'cmdProgtot.Text = "CREA DOC."
        tlb_hhnewdoc = CType(NTSFindControlByName(Me, "tlb_hhnewdoc"), NTSBarMenuItem)
    End Sub
    Public Overridable Sub tlb_hhnewdoc_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlb_hhnewdoc.ItemClick
        Dim dttTmp As New DataTable
        Dim dr As New DataTable
        Dim dr2 As New DataTable
        Dim dr3 As New DataTable
        Dim dbConn As SqlConnection = Nothing
        Dim dbCmd As SqlCommand = Nothing
        Dim dbCmd1 As SqlCommand = Nothing
        'Dim dbCmd2 As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim da1 As SqlDataAdapter = Nothing
        'Dim da2 As SqlDataAdapter = Nothing
        Dim conto As Integer
        Try
            dbConn = New SqlConnection(oApp.Db.Connect)
            dbCmd = New SqlCommand("SELECT * FROM anagra where an_conto =" & Me.edEt_conto.Text.ToString & " and an_omocodice = 'S'", dbConn)
                da = New SqlDataAdapter(dbCmd)
                da.Fill(dttTmp)
                dr2 = oCleBoll.dttEC.Copy()
                dr3 = oCleBoll.dttEC.Copy()

                CreaDoc(oCleBoll.dttET, oCleBoll.dttEC)


                If (dttTmp.Rows.Count > 0) Then
                conto = NTSCInt("904" & Mid(Me.edEt_conto.Text.ToString, 4, 4))
                CreaDocW(conto, oCleBoll.dttET, dr2)
                    CreaDoc2(conto, oCleBoll.dttET, dr3)
                End If

            'CreaDoc2(oCleBoll.dttET, oCleBoll.dttEC)

        Catch ex As Exception
            '------------------------------------------------- 
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
            '------------------------------------------------- 
        End Try
    End Sub
    'CREAZIONE DI DOCUMENTI 
    'Public oCleBoll As CLEVEBOLL = Nothing
    Public lNumTmpDoc As Integer
    Public lNumTmpDoc1 As Integer
    'Public strTipoDoc As String = "A"
    Public strSerieDoc As String = " "
    Public nAnnoDoc As Integer = Date.Now.Year
    Public i As Integer

    Public Overridable Function CreaDoc(ByVal dt As DataTable, ByVal dr As DataTable) As Boolean
        'Public Overridable Function CreaDoc() As Boolean
        Try
            '----------------------------------------------------------------------------------------
            '--- Inizializzo BEVEBOLL
            '----------------------------------------------------------------------------------------
            '  If Not InizializzaBeveboll() Then Return False

            '----------------------------------------------------------------------------------------
            '--- Legge il progressivo in TABNUMA
            '----------------------------------------------------------------------------------------
            lNumTmpDoc = oCleBoll.LegNuma("A", strSerieDoc, nAnnoDoc)

            '----------------------------
            'preparo l'ambiente
            Dim ds As New DataSet
            If Not oCleBoll.ApriDoc(oApp.Ditta, False, "A", nAnnoDoc, strSerieDoc, lNumTmpDoc, ds) Then Return False
            oCleBoll.bInApriDocSilent = True
            If oCleBoll.dsShared.Tables("TESTA").Rows.Count > 0 Then
                oApp.MsgBoxErr("Controllare le numerazioni!")
                Return False
            End If
            oCleBoll.ResetVar()
            oCleBoll.strVisNoteConto = "N"
            oCleBoll.NuovoDocumento(oApp.Ditta, "A", nAnnoDoc, strSerieDoc, lNumTmpDoc)
            oCleBoll.bInNuovoDocSilent = True

            CreaTestataDoc(dt)

            CreaRigaDoc(dr)

            SettaPiedeDoc()

            oCleBoll.bCreaFilePick = False 'non faccio generare il piking dal salvataggio del documento
            If Not oCleBoll.SalvaDocumento("N") Then
                oApp.MsgBoxErr("Errore al salvataggio!")
                Return False
            End If

            Stampa(0)
            Return True
        Catch ex As Exception
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
        End Try
    End Function
    Public Overridable Function CreaTestataDoc(ByVal dt As DataTable) As Boolean
        Try
            'MsgBox(dt.Rows(0)!et_conto.ToString)
            With oCleBoll.dttET.Rows(0)
                'faccio scatenare la onaddnew della testata dell'ordine
                !codditt = oApp.Ditta
                !et_conto = dt.Rows(0)!et_conto
                !et_tipork = "A"
                !et_anno = Now.Date.Year
                !et_serie = strSerieDoc
                !et_numdoc = lNumTmpDoc
                !et_datdoc = Now.Date
                !et_tipobf = 1
                !et_codpaga = NTSCInt(dt.Rows(0)!et_codpaga)
            End With
            dsBoll.Tables.Remove("TESTA")
            dsBoll.Tables.Add(oCleBoll.dttET.Copy)

            If Not oCleBoll.OkTestata Then Return False

            Return True
        Catch ex As Exception
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
        End Try
    End Function
    Public Overridable Function CreaRigaDoc(ByVal dr As DataTable) As Boolean
        Try
            'Creo una nuova riga di corpo setto i principali campi poi setto tutti gli altri
            'If Not oCleBoll.AggiungiRigaCorpo(False, NTSCStr(edAr_codart.Text),
            '                                         NTSCInt(edAr_ultfase.Text),
            '                                         0) Then Return False
            i = 0
            Dim foundRows As DataRow()

            ' Use the Select method to find all rows matching the filter.
            foundRows = dr.Select("ec_magaz = 1 AND ec_controp = 1007")
            i = 0
            For Each row As DataRow In foundRows
                If NTSCInt(dr.Rows(i)!ec_magaz) = 1 Then
                    'MsgBox(dt.Rows(i)!ec_codart.ToString)
                    ' strDetail = row("Detail")
                    If Not oCleBoll.AggiungiRigaCorpo(False, dr.Rows(i)!ec_codart.ToString,
                                                         0,
                                                         0) Then Return False


                    With oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)
                        !ec_magaz = dr.Rows(i)!ec_magaz
                        '!ec_causale2 = 0
                        !ec_unmis = dr.Rows(i)!ec_unmis
                        '!ec_descr = "a piacere"
                        !ec_colli = dr.Rows(i)!ec_colli
                        !ec_controp = 1007 ' dt.Rows(i)!ec_controp
                        !ec_quant = dr.Rows(i)!ec_quant
                    End With
                End If
                i = i + 1
                Next row


                If Not oCleBoll.RecordSalva(oCleBoll.dttEC.Rows.Count - 1, False, Nothing) Then
                    oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1).Delete()
                    Return False
                End If

            Return True
        Catch ex As Exception
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
        End Try

    End Function
    Public Overridable Function SettaPiedeDoc() As Boolean
        Try
            oCleBoll.CalcolaTotali()
            Return True
        Catch ex As Exception
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
        End Try
    End Function

    Public Overridable Function CreaDocW(ByVal conto As Integer, ByVal dt As DataTable, ByVal dr As DataTable) As Boolean
        Try
            '----------------------------------------------------------------------------------------
            '--- Inizializzo BEVEBOLL
            '----------------------------------------------------------------------------------------
            '  If Not InizializzaBeveboll() Then Return False

            '----------------------------------------------------------------------------------------
            '--- Legge il progressivo in TABNUMA
            '----------------------------------------------------------------------------------------
            lNumTmpDoc1 = oCleBoll.LegNuma("C", strSerieDoc, nAnnoDoc)

            '----------------------------
            'preparo l'ambiente
            Dim ds1 As New DataSet
            If Not oCleBoll.ApriDoc(oApp.Ditta, False, "C", nAnnoDoc, strSerieDoc, lNumTmpDoc1, ds1) Then Return False
            oCleBoll.bInApriDocSilent = True
            If oCleBoll.dsShared.Tables("TESTA").Rows.Count > 0 Then
                oApp.MsgBoxErr("Controllare le numerazioni!")
                Return False
            End If
            oCleBoll.ResetVar()
            oCleBoll.strVisNoteConto = "N"
            oCleBoll.NuovoDocumento(oApp.Ditta, "C", nAnnoDoc, strSerieDoc, lNumTmpDoc1)
            oCleBoll.bInNuovoDocSilent = True


            CreaTestataDocW(conto, dt)

            CreaRigaDocW(dr)

            SettaPiedeDoc()

            oCleBoll.bCreaFilePick = False 'non faccio generare il piking dal salvataggio del documento
            If Not oCleBoll.SalvaDocumento("N") Then
                oApp.MsgBoxErr("Errore al salvataggio!")
                Return False
            End If
            Stampa(0)

            Return True
        Catch ex As Exception
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
        End Try
    End Function
    Public Overridable Function CreaTestataDocW(ByVal conto As Integer, ByVal dt As DataTable) As Boolean
        Try
            With oCleBoll.dttET.Rows(0)
                'faccio scatenare la onaddnew della testata dell'ordine
                !codditt = oApp.Ditta
                !et_conto = conto
                !et_tipork = "C"
                !et_anno = Now.Date.Year
                !et_serie = strSerieDoc
                !et_numdoc = lNumTmpDoc1
                !et_datdoc = Now.Date
                !et_tipobf = 1
                !et_codpaga = NTSCInt(dt.Rows(0)!et_codpaga)
            End With
            dsBoll.Tables.Remove("TESTA")
            dsBoll.Tables.Add(oCleBoll.dttET.Copy)

            If Not oCleBoll.OkTestata Then Return False
            Return True
        Catch ex As Exception
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
        End Try
    End Function
    Public Overridable Function CreaRigaDocW(ByVal dr As DataTable) As Boolean
        Try
            'Creo una nuova riga di corpo setto i principali campi poi setto tutti gli altri
            'If Not oCleBoll.AggiungiRigaCorpo(False, NTSCStr(edAr_codart.Text),
            '                                         NTSCInt(edAr_ultfase.Text),
            '                                         0) Then Return False

            Dim foundRows As DataRow()

            ' Use the Select method to find all rows matching the filter.
            foundRows = dr.Select("ec_magaz = 1 AND ec_controp = 1020")
            i = 0
            For Each row As DataRow In foundRows
                If Not oCleBoll.AggiungiRigaCorpo(False, dr.Rows(i)!ec_codart.ToString,
                                                                 0,
                                                                 0) Then Return False


                With oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)
                    !ec_magaz = dr.Rows(i)!ec_magaz
                    '!ec_causale2 = 0
                    !ec_unmis = dr.Rows(i)!ec_unmis
                    '!ec_descr = "a piacere"
                    !ec_colli = dr.Rows(i)!ec_colli
                    !ec_controp = 1007 ' dt.Rows(i)!ec_controp
                    !ec_quant = dr.Rows(i)!ec_quant
                End With
                'End If
                i = i + 1
            Next row

            If Not oCleBoll.RecordSalva(oCleBoll.dttEC.Rows.Count - 1, False, Nothing) Then
                    oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1).Delete()
                    Return False
                End If
            Return True
        Catch ex As Exception
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
        End Try

    End Function
    'Public Overridable Function SettaPiedeDocW() As Boolean
    '    Try
    '        oCleBoll.CalcolaTotali()
    '        Return True
    '    Catch ex As Exception
    '        Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    '    End Try
    'End Function


    Public Overridable Function CreaDoc2(ByVal conto As Integer, ByVal dt As DataTable, ByVal dr As DataTable) As Boolean
        'Public Overridable Function CreaDoc() As Boolean
        Try
            lNumTmpDoc1 = oCleBoll.LegNuma("B", "N", nAnnoDoc)

            'preparo l'ambiente
            Dim ds1 As New DataSet
            If Not oCleBoll.ApriDoc(oApp.Ditta, False, "B", nAnnoDoc, "N", lNumTmpDoc1, ds1) Then Return False
            oCleBoll.bInApriDocSilent = True
            If oCleBoll.dsShared.Tables("TESTA").Rows.Count > 0 Then
                oApp.MsgBoxErr("Controllare le numerazioni!")
                Return False
            End If
            oCleBoll.ResetVar()
            oCleBoll.strVisNoteConto = "N"
            oCleBoll.NuovoDocumento(oApp.Ditta, "B", nAnnoDoc, strSerieDoc, lNumTmpDoc1)
            oCleBoll.bInNuovoDocSilent = True

            CreaTestataDoc2(dt)

            CreaRigaDoc2(dr)

            SettaPiedeDoc()

            oCleBoll.bCreaFilePick = False 'non faccio generare il piking dal salvataggio del documento
            If Not oCleBoll.SalvaDocumento("N") Then
                oApp.MsgBoxErr("Errore al salvataggio!")
                Return False
            End If

            Stampa(0)
            Return True
        Catch ex As Exception
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
        End Try
    End Function
    Public Overridable Function CreaTestataDoc2(ByVal dt As DataTable) As Boolean
        Try
            'MsgBox(dt.Rows(0)!et_conto.ToString)
            With oCleBoll.dttET.Rows(0)
                'faccio scatenare la onaddnew della testata dell'ordine
                !codditt = oApp.Ditta
                !et_conto = dt.Rows(0)!et_conto
                !et_tipork = "B"
                !et_anno = Now.Date.Year
                !et_serie = "N"
                !et_numdoc = lNumTmpDoc
                !et_datdoc = Now.Date
                !et_tipobf = 101
                !et_codpaga = NTSCInt(dt.Rows(0)!et_codpaga)
                !xx_przbol = "S"
            End With
            dsBoll.Tables.Remove("TESTA")
            dsBoll.Tables.Add(oCleBoll.dttET.Copy)

            If Not oCleBoll.OkTestata Then Return False

            Return True
        Catch ex As Exception
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
        End Try
    End Function
    Public Overridable Function CreaRigaDoc2(ByVal dr As DataTable) As Boolean
        Try
            'Creo una nuova riga di corpo setto i principali campi poi setto tutti gli altri
            'If Not oCleBoll.AggiungiRigaCorpo(False, NTSCStr(edAr_codart.Text),
            '                                         NTSCInt(edAr_ultfase.Text),
            '                                         0) Then Return False

            Dim foundRows As DataRow()

            ' Use the Select method to find all rows matching the filter.
            foundRows = dr.Select("ec_magaz = 2")
            i = 0
            For Each row As DataRow In foundRows
                If Not oCleBoll.AggiungiRigaCorpo(False, dr.Rows(i)!ec_codart.ToString,
                                                                 0,
                                                                 0) Then Return False


                With oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)
                    !ec_magaz = dr.Rows(i)!ec_magaz
                    '!ec_causale2 = 0
                    !ec_unmis = dr.Rows(i)!ec_unmis
                    '!ec_descr = "a piacere"
                    !ec_colli = dr.Rows(i)!ec_colli
                    !ec_controp = 1007 ' dt.Rows(i)!ec_controp
                    !ec_quant = dr.Rows(i)!ec_quant
                End With
                'End If
                i = i + 1
            Next row

            If Not oCleBoll.RecordSalva(oCleBoll.dttEC.Rows.Count - 1, False, Nothing) Then
                oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1).Delete()
                Return False
            End If
            Return True
        Catch ex As Exception
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
        End Try

    End Function


    Public Overridable Function InizializzaBeveboll() As Boolean
        Try
            If Not oCleBoll Is Nothing Then Return True
            '------------------------
            'inizializzo BEVEBOLL
            Dim strErr As String = ""
            Dim oTmp As Object = Nothing
            If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGARTI", "BEVEBOLL", oTmp, strErr, False, "", "") = False Then
                Throw New NTSException(oApp.Tr(Me, 128607611686875006, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
                Return False
            End If
            oCleBoll = CType(oTmp, CLEVEBOLL)
            '------------------------------------------------
            AddHandler oCleBoll.RemoteEvent, AddressOf GestisciEventiEntityBOLL
            If oCleBoll.Init(oApp, oScript, oMenu.oCleComm, "", False, "", "") = False Then Return False
            If Not oCleBoll.InitExt() Then Return False
            oCleBoll.bModuloCRM = False
            oCleBoll.bIsCRMUser = False
            Return True
        Catch ex As Exception
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
        End Try
    End Function
    'il componente usato entity di VEBOLL (ma anche tutti gli altri) non mostrano messaggi a video,
    'in quanto non hanno necessariamente un livello di UI, bensi inviano dei remoteevent che solitamente
    'vengono intercettati dalla UI e mostrati a video (nella maggior parte dei casi) grazie all'istruzione 
    'AddHandler oCleBoll.RemoteEvent, AddressOf GestisciEventiEntity
    'Per intercettarli si potrebbe realizzare una gestiscieventientity specifica per VEBOLL
    'cambiare la voce che dovreste già avere: AddHandler oCleBoll.RemoteEvent, AddressOf GestisciEventiEntity
    'con: AddHandler oCleBoll.RemoteEvent, AddressOf GestisciEventiEntityBOLL
    'poi aggiungere la seguente:
    Public Overridable Sub GestisciEventiEntityBOLL(ByVal sender As Object, ByRef e As NTSEventArgs)
        Dim fErr As StreamWriter = Nothing
        Dim fiErr As FileInfo = Nothing
        Dim strNomeFileLog As String = ""
        Try
            strNomeFileLog = oApp.Dir & "\crea_carico_produzione.LOG"
            fiErr = New FileInfo(strNomeFileLog)
            If fiErr.Exists Then
                If fiErr.Length > 500000 Then fiErr.Delete()
            End If
            fErr = New StreamWriter(strNomeFileLog, True)
            fErr.Write(e.Message & vbCrLf)
            fErr.Flush()
            fErr.Close()

            If e.TipoEvento = "DataValDB.:" Then
                'Questa gestione serve per creare i carichi di produzione
                e.RetValue = NTSCDate(oCleBoll.dttET.Rows(0)!et_datdoc.ToString).ToShortDateString
            Else
                'magari poi inserire anche l'istruzione sotto, così un volta loggati li giriamo comunque alla UI
                GestisciEventiEntity(sender, e)
            End If

        Catch ex As Exception
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
        End Try
    End Sub

    Public Overrides Sub Stampa(ByVal nDestin As Integer)
        'nDestin: 0 = video, 1 = carta
        Dim nPjob As Object
        Dim nRis As Integer = 0
        Dim strCrpe As String = ""
        Dim i As Integer
        Dim j As Integer
        Dim strNumcliPVR As String

        Dim strKey2 As String = ""
        Dim strReportName As String = "BSVEBOLL.rpt"

        Dim strNumDocAggiuntivo As String = ""
        Try
            If oCleBoll.lDeteriorNumDoc <> 0 Then strNumDocAggiuntivo = ", " & oCleBoll.lDeteriorNumDoc

            '--------------------------------------------------
            'Non si possono stampare gli impegni di produzione
            If dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "U" Then
                oApp.MsgBoxErr(oApp.Tr(Me, 128659597701562500, "Non si possono stampare documenti di tipo 'Scarichi a produzione'"))
                Return
            End If

            If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) <> 0 Then
                strKey2 = "Reports3"
            ElseIf dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
                strKey2 = "Reports2"
            Else
                strKey2 = "Reports1"
            End If

            '--------------------------------------------------
            'Preimposta il nome del report da stampare
            Select Case dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString
                Case "A", "N", "E", "C", "J", "L"
                    If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) <> 0 Then
                        strReportName = "BSVEFATV.RPT"
                    ElseIf dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
                        strReportName = "BSVEFATC.RPT"
                    Else
                        strReportName = "BSVEFATI.RPT"
                    End If
                Case "B", "M", "Z", "T"
                    If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) <> 0 Then
                        strReportName = "BSVEBOLV.RPT"
                    ElseIf dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
                        strReportName = "BSVEBOLC.RPT"
                    Else
                        strReportName = "BSVEBOLL.RPT"
                    End If
                Case "W"
                    If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) <> 0 Then
                        strReportName = "BSVEPRBV.RPT"
                    ElseIf dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
                        strReportName = "BSVEPRBC.RPT"
                    Else
                        strReportName = "BSVEPRBN.RPT"
                    End If
                Case "F", "I"
                    If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) <> 0 Then
                        strReportName = "BSVERIFV.RPT"
                    ElseIf dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
                        strReportName = "BSVERIFC.RPT"
                    Else
                        strReportName = "BSVERIFI.RPT"
                    End If
                Case "S"
                    If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) <> 0 Then
                        strReportName = "BSVEFRFV.RPT"
                    ElseIf dsBoll.Tables("TESTA").Rows(0)!et_scorpo.ToString = "S" Then
                        strReportName = "BSVEFRFC.RPT"
                    Else
                        strReportName = "BSVEFRFI.RPT"
                    End If
            End Select

            '--------------------------------------------------
            strNumcliPVR = ""
            If dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "A" Then
                If oCleBoll.bGestPVR = True Then
                    strNumcliPVR = oMenu.GetSettingBusDitt(DittaCorrente, "OPZIONI", ".", ".", "NsNumCliPVR", "", " ", "")
                    For i = 1 To (6 - Len(strNumcliPVR))
                        strNumcliPVR = "0" & strNumcliPVR
                    Next
                    'per un fattore di velocità non testo la tabella valuta:
                    'se sono in valuta presuppongo che sia l'unica ammessa
                    If NTSCInt(dsBoll.Tables("TESTA").Rows(0)!et_valuta) = 0 Then
                        strNumcliPVR = IIf(oApp.ValutaCorrente = "EUR", "03", "01").ToString & strNumcliPVR
                    Else
                        strNumcliPVR = IIf(oApp.ValutaCorrente = "EUR", "01", "03").ToString & strNumcliPVR
                    End If
                    strNumcliPVR = strNumcliPVR & CLN__STD.calcolaPVRCheck(strNumcliPVR)
                End If
            End If

            '--------------------------------------------------
            'eseguo delle query libere prima della stampa (query memorizzate in regprop di arcproc)
            oCleBoll.RunQueryBeforePrint("BSVEBOLL")

            '--------------------------------------------------
            'preparo il motore di stampa
            strCrpe = "{MOVMAG.codditt} = '" & DittaCorrente & "'" &
                 " AND {MOVMAG.mm_anno} = " & dsBoll.Tables("TESTA").Rows(0)!et_anno.ToString &
                 " AND {MOVMAG.mm_numdoc} IN [" & dsBoll.Tables("TESTA").Rows(0)!et_numdoc.ToString & strNumDocAggiuntivo & "]" &
                 " AND {MOVMAG.mm_serie} = '" & dsBoll.Tables("TESTA").Rows(0)!et_serie.ToString & "'" &
                 " AND {MOVMAG.mm_tipork} = '" & dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString & "'" &
                 " AND {MOVMAG.mm_stasino} <> 'N'"
            If oCleBoll.bUsaKeyMag Then strCrpe = strCrpe & " AND {KEYMAG.km_magaz} = {MOVMAG.mm_magaz}"
            If oCleBoll.dttET.Rows(0)!et_tipork.ToString = "B" Then strCrpe = strCrpe & " AND {MOVMAG.mm_stasino}<>'D'"

            nPjob = oMenu.ReportPEInit(DittaCorrente, Me, "BSVEBOLL", strKey2, dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString,
                                            0, nDestin, strReportName, False, "Stampa documento", False)
            If nPjob Is Nothing Then Return

            '--------------------------------------------------
            'lancio tutti gli eventuali reports (le righe che seguono gestiscono gi il multireport)
            For i = 1 To UBound(CType(nPjob, Array), 2)
                If dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "B" Then
                    nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "PREZZOINBOLLA", "'" & strLastPrezzoInbolla & "'")
                End If
                If dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString = "A" Then
                    nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "NUMCLIPVR", "'" & strNumcliPVR & "'")
                End If

                nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
                'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in BSVEBOLL, BSVEBOLL e pochi altri programmi
                For j = 3 To 12
                    If Trim(CStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
                        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(j, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(j + 10, i))))
                    End If
                Next j
                nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
            Next

            '--------------------------------------------------
            'stampo su reg. di cassa se è stata settata l'opzione di registro in BSVEBOLL/opzioni o BSVEBOLL/opzionidoc
            If oCleBoll.bDopoStpChiamaStpRegCassa Then FileRegcassa(True)

            If tlbStampaEtichetteFinale.Checked Then ApriEtichette(1)
            If tlbStampaEtichetteSovrac.Checked Then ApriEtichetteSovracollo()

            'stampo la scheda di trasporto se presente
            If oCleBoll.dttSCHETRASP.Rows.Count > 0 Then
                strKey2 = "Reports4"
                strReportName = "BSVESCTR.RPT"
                strCrpe = "{SCHETRASP.codditt} = '" & DittaCorrente & "'" &
                     " AND {SCHETRASP.sct_anno} = " & dsBoll.Tables("TESTA").Rows(0)!et_anno.ToString &
                     " AND {SCHETRASP.sct_numdoc} IN [" & dsBoll.Tables("TESTA").Rows(0)!et_numdoc.ToString & strNumDocAggiuntivo & "]" &
                     " AND {SCHETRASP.sct_serie} = '" & dsBoll.Tables("TESTA").Rows(0)!et_serie.ToString & "'" &
                     " AND {SCHETRASP.sct_tipork} = '" & dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString & "'"

                nPjob = oMenu.ReportPEInit(DittaCorrente, Me, "BSVEBOLL", strKey2, dsBoll.Tables("TESTA").Rows(0)!et_tipork.ToString,
                                                0, nDestin, strReportName, False, "Stampa scheda di trasporto", False)
                If nPjob Is Nothing Then Return

                '--------------------------------------------------
                'lancio tutti gli eventuali reports (le righe che seguono gestiscono gi il multireport)
                For i = 1 To UBound(CType(nPjob, Array), 2)
                    nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
                    'le formule particolari calcolate da 'CrpeResolveFormula' (ci sono solo in BSVEBOLL, BSVEBOLL e pochi altri programmi
                    For j = 3 To 12
                        If Trim(CStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
                            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(j, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(j + 10, i))))
                        End If
                    Next j
                    nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
                Next
            End If

        Catch ex As Exception
            '-------------------------------------------------
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
            '-------------------------------------------------
        End Try
    End Sub


End Class
