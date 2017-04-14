Imports System.Data
Imports NTSInformatica.CLN__STD
'Imports NTSInformatica.CLEORGNNP
'Imports NTSInformatica.FROORSEDO
Imports NTSInformatica.CLE__BASN
Imports NTSInformatica.CLDLBBASE
Public Class FROORSEDO
    Public oCldGnnp As CLHORGNNP
    Public oCleBoll As CLEVEBOLL
    Public oCleGnnp As CLEORGNNP
    Public oCldBase As CLD__BASE

    Public Overrides Sub FRMORSEDO_Load(sender As Object, e As EventArgs)
        MyBase.FRMORSEDO_Load(sender, e)
        cmd_hhcreanota = CType(NTSFindControlByName(Me, "cmd_hhcreanota"), NTSButton)
    End Sub
    Public Overridable Sub cmd_hhcreanota_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_hhcreanota.Click
        MsgBox("Hello world!")

        Dim bs As BindingSource = DirectCast(grvSeor.DataSource, BindingSource)
        Dim tCxC As DataTable = DirectCast(bs.DataSource, DataTable)
        Dim dr As DataRow


        For Each dr In tCxC.Rows
            CreaDocDaImpCli()
        Next



    End Sub


    'Public Overridable Function CreaDocDaImpCli(ByVal dtrImpCli As DataRow) As Boolean
    Public Overridable Function CreaDocDaImpCli() As Boolean
        Dim i, x As Integer
        Dim dttMovMag, dttTmp As New DataTable
        Dim lNumDoc As Integer
        Dim strTmp As String = ""
        Dim bSalvato As Boolean = True
        Dim strDittaCorrente As String = Me.oApp.Ditta
        Dim lIITTMoPernp As Integer = oMenu.GetTblInstId("TTMOPERNP", False)

        'Dim dttTmp As New DataTable


        Try

            'oCldGnnp = CType(CType(oCldBase, CLDORGNNP), CLHORGNNP)
            'oCldGnnp.Init(oApp)
            dttTmp = oCldGnnp.GetTestateTemp1(strDittaCorrente, lIITTMoPernp, True)

            For x = 0 To dttTmp.Rows.Count - 1



                'oCldGnnp.GetMovmag(strDittaCorrente, dtrImpCli, oCleGnnp.lIITTMoPernp, oCleGnnp.bModExtTCO,
                '             oCleGnnp.bDtacUnaNotaPerImpegno, dttMovMag)

                oCldGnnp.GetMovmag(strDittaCorrente, dttTmp.Rows(x), lIITTMoPernp, False, True, dttMovMag)
                If dttMovMag.Rows.Count = 0 Then Return True

                strTmp = ""
                '  If bDtacUnaNotaPerImpegno = True Then
                strTmp += ": " & dttTmp.Rows(x)!mn_tipork.ToString & "-" & dttTmp.Rows(x)!mn_anno.ToString & "-" & dttTmp.Rows(x)!mn_serie.ToString & "-" & dttTmp.Rows(x)!mn_numord.ToString & " Riferim: " & dttTmp.Rows(x)!Riferim.ToString
                'Else
                'strTmp += "Riferim: " & dtrImpCli!Riferim.ToString
                'End If

                'LogWrite(oApp.Tr(Me, 128843579324644000, "Impegni coinvolti") & strTmp, False)

                lNumDoc = oCleGnnp.lDtacNumdoc
                Do While oCldGnnp.IsInTestmag(New NTSDoc(oCleGnnp.strDittaCorrente, oCleGnnp.strDtacTipork, oCleGnnp.nDtacAnno, oCleGnnp.strDtacSerie, lNumDoc))
                    lNumDoc = lNumDoc + 1
                Loop
                'lNumDoc = LegNuma(strDtacTipork, strDtacSerie, nDtacAnno)
                'If lNumDoc = 0 Then
                '  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128699231787692325, "Prima di creare un nuovo documento è necessario attivare la numerazione del documento")))
                '  Return False
                'End If

                If Not oCleBoll.NuovoDocumento(oCleGnnp.strDittaCorrente, oCleGnnp.strDtacTipork, oCleGnnp.nDtacAnno, oCleGnnp.strDtacSerie, lNumDoc) Then Return False
                oCleBoll.bInNuovoDocSilent = True
                oCleGnnp.SettaTestata(dttTmp.Rows(x))
                For i = 0 To dttMovMag.Rows.Count - 1
                    If oCleGnnp.ScriviRigaDocDaImpCli(i, dttMovMag.Rows(i), dttTmp.Rows(x), lNumDoc) = False Then Return False
                Next
                oCleGnnp.SettaPiede(dttTmp.Rows(x))

                oCleBoll.bCreaFilePick = False 'non faccio generare il piking dal salvataggio del documento

                oCleBoll.bAccontiVerificati = True  'anche se uso tipobf che evadono acconti faccio continuare, visto che gli acconti possono essere agganciati solo da gestione manuale
                oCleBoll.bDisabilitaCheckUbicaz = True  'come in VB6, per gli articoli gestiti ad ubicazione non controllo se l'ubicazione è stata inserita
                If Not oCleBoll.SalvaDocumento("N") Then
                    'LogWrite(oApp.Tr(Me, 128843594045134000, "Documento non salvato"), True)
                    bSalvato = False
                End If

                If bSalvato And oCleGnnp.bDtacCreaPicking = True Then 'genero 1 unico file di picking else uno per ogni nota
                    If oCleGnnp.bDtacPickingDistinti = False Then
                        CType(oCleGnnp.oCleComm, CLELBMENU).CreaPicking("BSORGNNP", oCleGnnp.strDittaCorrente, oCleGnnp.strDtacTipork, oCleGnnp.nDtacAnno, oCleGnnp.strDtacSerie, lNumDoc, oCleGnnp.lIdpick, oCleGnnp.bAggiungiPick, Not oCleGnnp.bUltimaNotaGenerataPick)
                    Else
                        CType(oCleGnnp.oCleComm, CLELBMENU).CreaPicking("BSORGNNP", oCleGnnp.strDittaCorrente, oCleGnnp.strDtacTipork, oCleGnnp.nDtacAnno, oCleGnnp.strDtacSerie, lNumDoc, 0)
                    End If
                End If

                If Not oCldGnnp.CancellaRigheElaborate(dttMovMag) Then Return False

                oCleGnnp.lANumero = lNumDoc
                If bSalvato Then oCleGnnp.nNumFat = oCleGnnp.nNumFat + 1

                Return True
            Next
        Catch ex As Exception
            '--------------------------------------------------------------
            If GestErrorCallThrow() Then
                Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
            Else
                'ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
                MsgBox(oApp.InfoError)
            End If
            '--------------------------------------------------------------
            Return False

        End Try
    End Function
    'Public Overridable Function SettaPiede(ByVal dtrImpCli As DataRow) As Boolean
    '    Dim dttTmp As New DataTable
    '    Try
    '        dttTmp = Nothing
    '        oCldGnnp.SettaPiede(oCleGnnp.strDittaCorrente, bDtacUnaNotaPerImpegno, nSeicTipo, lIITTMoPernp,
    '                      dtrImpCli, dttTmp)

    '        If Not dttTmp Is Nothing Then
    '            If dttTmp.Rows.Count > 0 Then
    '                If bDtacUnaNotaPerImpegno = True Then
    '                    oCleBoll.dttET.Rows(0)!et_speacc = dttTmp.Rows(0)!td_speacc
    '                    oCleBoll.dttET.Rows(0)!et_speimb = dttTmp.Rows(0)!td_speimb
    '                    oCleBoll.dttET.Rows(0)!et_speaccv = dttTmp.Rows(0)!td_speaccv
    '                    oCleBoll.dttET.Rows(0)!et_speimbv = dttTmp.Rows(0)!td_speimbv
    '                Else
    '                    oCleBoll.dttET.Rows(0)!et_speacc = dttTmp.Rows(0)!mn_speacc
    '                    oCleBoll.dttET.Rows(0)!et_speimb = dttTmp.Rows(0)!mn_speimb
    '                    oCleBoll.dttET.Rows(0)!et_speaccv = dttTmp.Rows(0)!mn_speaccv
    '                    oCleBoll.dttET.Rows(0)!et_speimbv = dttTmp.Rows(0)!mn_speimbv
    '                End If
    '            End If
    '        End If

    '        oCleBoll.CalcolaTotali()

    '        Return True
    '    Catch ex As Exception
    '        '--------------------------------------------------------------
    '        If GestErrorCallThrow() Then
    '            Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    '        Else
    '            ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
    '        End If
    '        '--------------------------------------------------------------
    '        Return False
    '    End Try
    'End Function

End Class
