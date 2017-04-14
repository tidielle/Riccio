Imports System.Data
Imports NTSInformatica.CLN__STD
Public Class CLFVEFDIN
    Inherits CLEVEFDIN
    Public Overrides Function CalcolaTotali(ByVal bRicalcolaDoc As Boolean, ByRef oTestata As CLELBMENU.OutTestata) As Boolean
        Dim oclPar As CLELBMENU.Parametri = New CLELBMENU.Parametri
        Dim strError As String = ""
        Dim bResult As Boolean = False

        Dim dTotSpese As Decimal = 0
        Dim dTotSpesev As Decimal = 0
        Dim dTotImpon As Decimal = 0
        Dim dTotImponv As Decimal = 0
        Dim dTotIva As Decimal = 0
        Dim dTotIvav As Decimal = 0
        Dim dTotDoc As Decimal = 0
        Dim dTotDocv As Decimal = 0
        Dim dRimanenza As Decimal = 0
        Dim dRimanenzav As Decimal = 0
        Dim dTotQuant As Decimal = 0
        Dim i As Integer = 0

        Try
            '----------------
            'per compatibilita' con funzioni ereditate da rive in versioni precedenti
            Dim oOut As Object = Nothing
            Dim oIn As New ArrayList(New Object() {bRicalcolaDoc, oTestata})
            If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
                oTestata = CType(oIn(1), CLELBMENU.OutTestata)
                Return CBool(oOut)
            End If
            '----------------

            If dsShared.Tables("TESTMAG").Rows.Count = 0 Then Return True
            If bInImportDDT Then Return True

            If bRicalcolaDoc Then
                bInCalcolaTotali = True
                'compilo i parametri che piloteranno il ricalcolo del documento

                With oclPar
                    .strNomProg = "BSVEFDIN"
                    .bNew = bNew
                    .bDocEmesso = IsDocumentoEmesso(dttET.Rows(0)!tm_tipork.ToString)
                    .bCalcolaBolli = CBool(IIf(dttET.Rows(0)!tm_flbolli.ToString = "S", True, False))
                    .bCalcolaColli = False
                    .bCalcolaColliPesiSuDocAperti = False
                    .bCalcolaPesoLordo = False
                    .bCalcolaPesoNetto = False
                    .bCalcPesi = True
                    .bNoPesiSuRigheKitFittizie = bNoPesiSuRigheKitFittizie
                    .bNonCalcolaProvvig = False
                    .strCalcolaSpeseTrasp = "N"
                    .bSbloccaIva = CBool(IIf(dttET.Rows(0)!tm_flscdb.ToString = "S", True, False))
                    .bSegueFatt = False
                    .nGestScostAcconti = nGestScostAcconti
                    .dImpoScostAcconti = dImpoScostAcconti
                    .nCodese = NTSCInt(dttET.Rows(0)!tm_codese)
                    .nPeacIva15 = nPeacIva15
                    If NTSCDate(dttET.Rows(0)!tm_datdoc) < NTSCDate(strPeacIvaInc3Fine) Then
                        .nPeacIvainc = nPeacIvaInc3
                    ElseIf NTSCDate(dttET.Rows(0)!tm_datdoc) < NTSCDate(strPeacIvaInc2Fine) Then
                        .nPeacIvainc = nPeacIvaInc2
                    Else
                        .nPeacIvainc = nPeacIvainc
                    End If
                    .nPeveIva15 = nPeveIva15
                    If NTSCDate(dttET.Rows(0)!tm_datdoc) < NTSCDate(strPeveIvaInc3Fine) Then
                        .nPeveIvaInc = nPeveIvaInc3
                    ElseIf NTSCDate(dttET.Rows(0)!tm_datdoc) < NTSCDate(strPeveIvaInc2Fine) Then
                        .nPeveIvaInc = nPeveIvaInc2
                    Else
                        .nPeveIvaInc = nPeveIvaInc
                    End If
                    .bDeterminaBolliSuOperazEsenti = bDeterminaBolliSuOperazEsenti
                    .bNonCalcolaProvvig = True
                End With

                '---------------------------
                'ricalcolo il documento: in fdin vb6 calcola1 e calcola2
                bResult = CType(oCleComm, CLELBMENU).CalcolaDocFattureRieplig(strDittaCorrente, oclPar,
                                        dttET.Rows(0), dttETC.Rows(0), dsShared.Tables("ELENCODDT"),
                                        oTestata, strError,
                                        CBool(IIf(IsDocumentoEmesso(dttET.Rows(0)!tm_tipork.ToString), bCalcolaRagg, False)))
                If strError <> "" Then ThrowRemoteEvent(New NTSEventArgs("", strError))
                If oTestata.nCcontr(20) = -1 Then Return False

                ' adesso visualizza tutto ....
                If bResult Then
                    With dttET.Rows(0)
                        For i = 1 To 8
                            dttET.Rows(0)("tm_codiva_" & i.ToString) = oTestata.nCodiva(i - 1)
                            dttET.Rows(0)("tm_imponib_" & i.ToString) = oTestata.dImponib(i - 1)
                            dttET.Rows(0)("tm_imposta_" & i.ToString) = oTestata.dImposta(i - 1)
                            dttET.Rows(0)("tm_imponibv_" & i.ToString) = oTestata.dImponibv(i - 1)
                            dttET.Rows(0)("tm_impostav_" & i.ToString) = oTestata.dImpostav(i - 1)
                        Next
                        For i = 1 To 20
                            dttETC.Rows(0)("tm_ccontr_" & i.ToString) = oTestata.nCcontr(i - 1)
                            dttETC.Rows(0)("tm_impcont_" & i.ToString) = oTestata.dImpocont(i - 1)
                            dttETC.Rows(0)("tm_impcontv_" & i.ToString) = oTestata.dImpcontv(i - 1)
                        Next
                        !tm_totcoll = ArrDblEcc(oTestata.dTotcoll, 0)
                        !tm_peso = oTestata.dPeso
                        !tm_pesonetto = oTestata.dPesonetto

                        !tm_impprov = oTestata.dImpprov
                        !tm_totprov = oTestata.dTotprov
                        !tm_totprov2 = oTestata.dTotprov2

                        !tm_speinc = oTestata.dSpeinc
                        !tm_speincv = oTestata.dSpeincv
                        !tm_speacc = oTestata.dSpeacc
                        !tm_speaccv = oTestata.dSpeaccv
                        !tm_bolli = oTestata.dBolli
                        !tm_bolliv = oTestata.dBolliv

                        !tm_speimb = oTestata.dSpeimb + 5.0
                        !tm_speimbv = oTestata.dSpeimbv + 5.0
                        !tm_totlordo = oTestata.dTotlordo
                        !tm_totlordov = oTestata.dTotlordov
                        !tm_totmerce = oTestata.dTotMerce
                        !tm_totmercev = oTestata.dTotmercev
                        !tm_totomag = oTestata.dTotomag
                        !tm_totomagv = oTestata.dTotomagv
                        !tm_totdoc = oTestata.dTotdoc
                        !tm_totdocv = oTestata.dTotdocv
                        !tm_abbuono = oTestata.dAbbuono
                        !tm_abbuonov = oTestata.dAbbuonov
                        !tm_pagato = oTestata.dPagato
                        !tm_pagato2 = oTestata.dPagato2
                        !tm_resto = oTestata.dResto
                        !tm_pagatov = oTestata.dPagatov
                        !tm_diffiva = oTestata.dDiffIva
                        !tm_diffda = oTestata.dDiffDA
                        dTotQuant = oTestata.dTotquant
                    End With
                End If
                bInCalcolaTotali = False
            End If    'If bRicalcolaDoc Then

            'devo solo aggiornare la UI per i campi unbound
            With dsShared.Tables("TESTMAG").Rows(0)
                dTotSpese = NTSCDec(!tm_speacc) + NTSCDec(!tm_speinc) + NTSCDec(!tm_bolli) + NTSCDec(!tm_speimb)
                dTotSpesev = NTSCDec(!tm_speaccv) + NTSCDec(!tm_speincv) + NTSCDec(!tm_bolliv) + NTSCDec(!tm_speimbv)
                'dTotImpon = NTSCDec(!tm_totmerce) + dTotSpese
                'dTotImponv = NTSCDec(!tm_totmercev) + dTotSpesev
                For i = 1 To 8
                    dTotImpon += NTSCDec(dsShared.Tables("TESTMAG").Rows(0)("tm_imponib_" & i.ToString))
                    dTotImponv += NTSCDec(dsShared.Tables("TESTMAG").Rows(0)("tm_imponibv_" & i.ToString))
                    dTotIva += NTSCDec(dsShared.Tables("TESTMAG").Rows(0)("tm_imposta_" & i.ToString))
                    dTotIvav += NTSCDec(dsShared.Tables("TESTMAG").Rows(0)("tm_impostav_" & i.ToString))
                Next
                dTotDoc = NTSCDec(!tm_totdoc) - NTSCDec(!tm_totomag)
                dTotDocv = NTSCDec(!tm_totdocv) - NTSCDec(!tm_totomagv)
                dRimanenza = dTotDoc - NTSCDec(!tm_pagato) - NTSCDec(!tm_pagato2) + NTSCDec(!tm_resto) - NTSCDec(!tm_abbuono)
                dRimanenzav = dTotDocv - NTSCDec(!tm_pagatov) - NTSCDec(!tm_abbuonov)
            End With

            'avviso l'UI di aggiornare i totali dei campi UNBOUND
            ThrowRemoteEvent(New NTSEventArgs("AggTotali.:" &
                              dTotSpese.ToString & "§" &
                              dTotSpesev.ToString & "§" &
                              dTotImpon.ToString & "§" &
                              dTotImponv.ToString & "§" &
                              dTotIva.ToString & "§" &
                              dTotIvav.ToString & "§" &
                              dTotDoc.ToString & "§" &
                              dTotDocv.ToString & "§" &
                              dRimanenza.ToString & "§" &
                              dRimanenzav.ToString & "§" &
                              dTotQuant.ToString, ""))
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
            bInCalcolaTotali = False
        End Try
    End Function
End Class
