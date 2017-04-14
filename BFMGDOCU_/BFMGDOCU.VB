Imports System.Data
Imports NTSInformatica.CLN__STD

'31/03/08: E' la base di partenza per tutti i tipi di documenti di magazzino
Imports System
Public Class CLFMGDOCU
    Inherits CLEMGDOCU

    Public Overrides Function CheckFidoInsol(ByVal bDopo As Boolean, ByVal lConto As Integer, ByRef dttEt As DataTable, ByRef bBloccaCli As Boolean) As Boolean
        '-------------------------
        'utilizzabile sia per ORDINI CHE PER DOCUMENTI DI MAGAZZINO
        Dim oDcst As New CLE__DCST
        Dim bBloc As Boolean = False
        Dim oCleDcst As CLECGDCST = Nothing
        Dim nEscomp As Integer
        Try
            bBloc = Not bDopo

            oCldDocu.EscompFromDate(strDittaCorrente, NTSCDate(dttEt.Rows(0)!et_datdoc), nEscomp, Nothing)

            With oDcst
                .lConto = lConto
                .nEscomp = nEscomp
                .strData = NTSCDate(dttEt.Rows(0)!et_datdoc).ToShortDateString
                .bControllaFido = True
                .bControllaInsoluti = True
                .bStatPagamenti = True
                If BORDINI Then
                    .dttMotrans = dttEC
                Else
                    .dttMmtrans = dttEC
                End If
                .dImpOdierno = NTSCDec(dttEt.Rows(0)!et_totdoc)
                .bOpendoc = Not bNew
                .strTipork = dttEt.Rows(0)!et_tipork.ToString
                .nAnno = NTSCInt(dttEt.Rows(0)!et_anno)
                .strSerie = dttEt.Rows(0)!et_serie.ToString
                .lNumdoc = NTSCInt(dttEt.Rows(0)!et_numdoc)
                .bBlocca = bBloc
                .bVisForm = False
                If bInCreaDocDaGnor Then .bVismess = False Else .bVismess = True
            End With

            '------------------------------------------------
            'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
            Dim strErr As String = ""
            Dim oTmp As Object = Nothing
            If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEMGDOCU", "BECGDCST", oTmp, strErr, False, "", "") = False Then
                Throw New NTSException(oApp.Tr(Me, 130597607173640172, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
                Return False
            End If
            oCleDcst = CType(oTmp, CLECGDCST)
            '------------------------------------------------
            AddHandler oCleDcst.RemoteEvent, AddressOf GestisciEventiEntityDcst
            If oCleDcst.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then Return True
            oCleDcst.oDcst = oDcst
            If Not oCleDcst.Esegui(strDittaCorrente) Then Return False
            bBloccaCli = oDcst.bClienteBloccatoOut      'se serve avviso di bloccare il cliente 

            Return True

        Catch ex As Exception
            '--------------------------------------------------------------
            If GestErrorCallThrow() Then
                Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
            Else
                ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
            End If
            '--------------------------------------------------------------	
        Finally
            If Not oCleDcst Is Nothing Then RemoveHandler oCleDcst.RemoteEvent, AddressOf GestisciEventiEntityDcst
        End Try
    End Function

End Class