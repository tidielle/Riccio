Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Globalization
Imports System

Public Class CLFORGSOR
    Inherits CLEORGSOR
    Public Overrides Function ElaboraImportazione(strRiga As String, strSep As String, strDec As String, strTracciato() As String, bTipoRiga As Boolean, bRicPrz As Boolean, bRicScn As Boolean, bRicPrv As Boolean, strChiamante As String) As Boolean
        Dim strPart(), strTmp, strArt, strBCUnmis As String
        Dim strDescr As String = ""
        Dim dtrRow() As DataRow
        Dim dBCQuant As Decimal
        Dim dttTmp, dttTagl As New DataTable
        Dim lStart As Integer
        Dim i As Integer
        Dim nFase As Integer = 0
        Dim strTaglia As String = ""
        Dim x As Integer
        Dim marca As String = ""
        'Dim ret As Boolean
        Dim bDisabilitaMsgPrezzoZeroTmp As Boolean = bDisabilitaMsgPrezzoZero
        'oClhveboll = CType(oClhveboll, CLHVEBOLL)
        '        oClhveboll.Init(oApp)
        Try
            strArt = ""
            dBCQuant = 0
            strBCUnmis = ""

            strPart = strRiga.Split(CType(strSep, Char))


            oCldDocu.ValCodiceDb(CType(NTSCInt(dttET.Rows(0)!et_conto), String), strDittaCorrente, "ANAGRA", "S", "", dttTmp)
            If dttTmp.Rows.Count > 0 Then
                marca = NTSCStr(dttTmp.Rows(0)!an_siglaric)
            End If
            If marca = "" Then
                Dim resp As Integer = MsgBox("Il fornitore non ha sigla codificata.Continuare?", MsgBoxStyle.YesNo)
                If resp = 7 Then
                    Exit Function
                End If
            End If
            'Se c'è il tipo riga devo prendere la cella successiva
            If bTipoRiga Then
                lStart = 1
                If strPart(0) = "2" Then 'è una riga con matricola 
                    If strChiamante = "BSORGSOR" Then Return True
                    If dttEC.Rows.Count = 0 Then
                        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129856882071375592, "Errore durante l'importazione. Il file non è corretto.")))
                        Return False
                    End If
                    dttMOVMATR.Rows.Add(dttMOVMATR.NewRow)
                    With dttMOVMATR.Rows(dttMOVMATR.Rows.Count - 1)
                        !codditt = strDittaCorrente
                        'Considero che appartengano all'ultima riga importata
                        !mma_tipork = dttEC.Rows(dttEC.Rows.Count - 1)!ec_tipork
                        !mma_anno = dttEC.Rows(dttEC.Rows.Count - 1)!ec_anno
                        !mma_serie = dttEC.Rows(dttEC.Rows.Count - 1)!ec_serie
                        !mma_numdoc = dttEC.Rows(dttEC.Rows.Count - 1)!ec_numdoc
                        !mma_riga = dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga
                        !mma_rigaa = NTSCInt(dttMOVMATR.Compute("MAX(mma_rigaa)", "mma_riga = " & NTSCInt(!mma_riga))) + 1
                        !mma_matric = strPart(1)
                        !mma_quant = strPart(2)
                        Try
                            !mma_quant = NTSCDec(strPart(2).Trim().Replace(strDec, ","))
                        Catch ex As Exception
                            LogWrite(oApp.Tr(Me, 129252972890937500, "Valore '|" & strPart(2).Trim & "| non valido per le quantità, verrà inserito 1."), True)
                            !mma_quant = 1
                        End Try
                    End With

                    Return True
                ElseIf strPart(0) = "3" Then 'è una riga di taglia
                    'Considero che appartengano all'ultima riga importata
                    oCldDocu.ValCodiceDb(NTSCStr(dttEC.Rows(dttEC.Rows.Count - 1)!xxo_codtagl), strDittaCorrente, "TABTAGL", "N", "", dttTagl)
                    'Aggiungo subito la nuova riga (controllando che non ci sia già una esistente)
                    dtrRow = dttECTC.Select("ec_riga = " & NTSCInt(dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga))
                    If dtrRow.Length = 0 Then
                        dttECTC.Rows.Add(dttECTC.NewRow)
                        With dttECTC.Rows(dttECTC.Rows.Count - 1)
                            !codditt = strDittaCorrente
                            !ec_tipork = dttEC.Rows(dttEC.Rows.Count - 1)!ec_tipork
                            !ec_anno = dttEC.Rows(dttEC.Rows.Count - 1)!ec_anno
                            !ec_serie = dttEC.Rows(dttEC.Rows.Count - 1)!ec_serie
                            !ec_numdoc = dttEC.Rows(dttEC.Rows.Count - 1)!ec_numdoc
                            !ec_riga = dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga
                        End With
                        dtrRow = dttECTC.Select("ec_riga = " & NTSCInt(dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga))
                    End If

                    For z As Integer = 1 To strPart.Length - 1 Step 2
                        For i = 1 To 24
                            'Se trovo la taglia la assegno 
                            If NTSCStr(dttTagl.Rows(0)("tb_dest" & i.ToString("00"))) = strPart(z).Trim Then
                                dtrRow(0)("ec_quant" & i.ToString("00")) = NTSCDec(strPart(z + 1).Trim().Replace(strDec, ","))
                            End If
                        Next
                    Next

                    Return True
                End If
            Else
                lStart = 0
            End If

            If (strPart.Length <> strTracciato.Length And Not bTipoRiga) Or (strPart.Length - 1 <> strTracciato.Length And bTipoRiga) Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129248667436327086, "Il tracciato non corrisponde con il formato della riga da importare. Elaborazione annullata.")))
                Return False
            End If

            'Scorre i vettori 2 volte. La prima per cercare il codice articolo (o equivalente, se lo trova genera la riga ed effettua una serie di controlli)
            For i = 0 To strTracciato.Length - 1
                strArt = ""
                dBCQuant = 0
                strBCUnmis = ""

                Select Case strTracciato(i).Substring(0, 3)
                    Case "art" ' Articolo
                        oCldDocu.ValCodiceDb(marca & strPart(i + lStart).Trim, strDittaCorrente, "ARTICO", "S", "", dttTmp)
                        If dttTmp.Rows.Count = 0 Then
                            'LogWrite(oApp.Tr(Me, 129248817998684936, "Articolo |" & strPart(i + lStart).Trim & "| non trovato. Verrà importato come articolo 'D'."), True)
                            'strDescr = oApp.Tr(Me, 129597071865468750, "Articolo |" & strPart(i + lStart).Trim & "| non trovato")
                            'strArt = "D"
                            For x = 0 To strTracciato.Length - 1
                                Select Case strTracciato(x).Substring(0, 3)
                                    Case "des"
                                        CType(oCldGsor, CLHORGSOR).AggiungiArticolo_import(strDittaCorrente, marca & strPart(i + lStart).Trim, Left(strPart(x + lStart).Trim.Replace(Chr(34), ""), 40))
                                        strArt = marca & strPart(i + lStart).Trim
                                End Select
                            Next
                        Else
                            strArt = marca & strPart(i + lStart).Trim

                        End If
                        Exit For
                    Case "acf" ' Art. C/F
                        strTmp = CType(oCleComm, CLELBMENU).TrovaCodartDaCodarfo(strPart(i + lStart).Trim, NTSCInt(dttET.Rows(0)!et_conto), strDittaCorrente, "")
                        If strTmp = "" Then
                            LogWrite(oApp.Tr(Me, 129248818010091697, "Articolo cliente/fornitore |" & strPart(i + lStart).Trim & "| (conto |" & lHlbcConto & "|) non associato ad alcun articolo. Verrà scartato."), True)
                            Return False
                        Else
                            strArt = strTmp
                            Exit For
                        End If
                    Case "bar" ' Barcode
                        oCldDocu.ValCodiceDb(strPart(i + lStart).Trim, strDittaCorrente, "BARCODE", "S", "", dttTmp)
                        If dttTmp.Rows.Count = 0 Then
                            LogWrite(oApp.Tr(Me, 129248818031030135, "Barcode |" & strPart(i + lStart).Trim & "| non trovato. Verrà scartato."), True)
                            Return False
                        Else
                            strBCUnmis = NTSCStr(dttTmp.Rows(0)!bc_unmis)
                            dBCQuant = NTSCDec(dttTmp.Rows(0)!bc_quant)
                            strArt = NTSCStr(dttTmp.Rows(0)!bc_codart)
                            nFase = NTSCInt(dttTmp.Rows(0)!bc_fase)
                            strTaglia = NTSCStr(dttTmp.Rows(0)!bc_tagl)
                            Exit For
                        End If
                    Case "all" ' Art. o Barcode o Art. C/F
                        oCldDocu.ValCodiceDb(strPart(i + lStart).Trim, strDittaCorrente, "ARTICO", "S", "", dttTmp)
                        If dttTmp.Rows.Count <> 0 Then
                            strArt = strPart(i + lStart).Trim
                            Exit For
                        Else
                            strTmp = CType(oCleComm, CLELBMENU).TrovaCodartDaCodarfo(strPart(i + lStart).Trim, lHlbcConto, strDittaCorrente, "")
                            If strTmp <> "" Then
                                strArt = strTmp
                                Exit For
                            Else
                                oCldDocu.ValCodiceDb(strPart(i + lStart).Trim, strDittaCorrente, "BARCODE", "S", "", dttTmp)
                                If dttTmp.Rows.Count <> 0 Then
                                    strBCUnmis = NTSCStr(dttTmp.Rows(0)!bc_unmis)
                                    dBCQuant = NTSCDec(dttTmp.Rows(0)!bc_quant)
                                    strArt = NTSCStr(dttTmp.Rows(0)!bc_codart)
                                    nFase = NTSCInt(dttTmp.Rows(0)!bc_fase)
                                    strTaglia = NTSCStr(dttTmp.Rows(0)!bc_tagl)
                                    Exit For
                                Else
                                    LogWrite(oApp.Tr(Me, 129882210269846975, "Articolo |" & strPart(i + lStart).Trim & "| non trovato. Verrà importato come articolo 'D'."), True)
                                    strDescr = oApp.Tr(Me, 129882210290879023, "Articolo |" & strPart(i + lStart).Trim & "| non trovato")
                                    strArt = "D"
                                    Exit For
                                End If
                            End If
                        End If
                End Select
            Next

            'Creo la nuova riga con i dati trovati
            dttEC.Rows.Add(dttEC.NewRow)
            With dttEC.Rows(dttEC.Rows.Count - 1)
                '------------------------------------------------------------------------------------------------------------  
                '--- Forzo la MovordOnAddNewRow
                '------------------------------------------------------------------------------------------------------------
                !codditt = "."
                !codditt = strDittaCorrente
                !ec_codart = strArt
                If strDescr <> "" Then !ec_descr = strDescr
                !ec_quant = 1 ' di base lascio 1, poi in caso modifico dopo colli o quantità
                If nFase <> 0 Then !ec_fase = nFase
                Dim qta As Decimal = 0
                'Sovrascrivo i dati caricati di default con quelli passati.
                For i = 0 To strTracciato.Length - 1
                    Select Case strTracciato(i).Substring(0, 3) ' i primi 3 caratteri indicano il tipo di campo
                        Case "   " ' Non deve fare nulla, quella riga non interessa a business.
                        'Case "des" 'Descrizione
                        '    If strDescr = "" Then !ec_descr = strPart(i + lStart).Trim()
                        Case "dei" 'Desc. interna
                            !ec_desint = strPart(i + lStart).Trim()
                        Case "unm" 'Unità di misura
                            !ec_unmis = strPart(i + lStart).Trim()
                        Case "col" 'Colli
                            Try
                                If strBCUnmis <> "" Then !ec_unmis = strBCUnmis 'può essere solo l'UM indicata nella tabella barcode!
                                !ec_colli = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249473075325890, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per i colli, verrà inserito 1."), True)
                                !ec_colli = 1
                            End Try
                        Case "qta" 'Quantità
                            Try
                                !ec_quant = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                                qta = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249474664711062, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per le quantità, verrà inserito 1."), True)
                                !ec_quant = 1
                            End Try
                        Case "prz" 'Prezzo
                            If Not bRicPrz Then
                                Try
                                    !ec_prezzo = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                                Catch ex As Exception
                                    LogWrite(oApp.Tr(Me, 129249474994088170, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per il prezzo, verrà lasciato quello attuale."), True)
                                End Try
                            End If
                        Case "sc1" 'Sconto 1
                            If Not bRicScn Then
                                Try
                                    Dim ec_scont1 As String = strPart(i + lStart).Trim().Replace(strDec, ",") 'CType(NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)), String) 'Dividi per
                                    Dim result As String() = ec_scont1.Split(New String() {"+"}, StringSplitOptions.None)
                                    Dim y As Integer = 1
                                    For Each s As String In result
                                        Select Case y
                                            Case 1
                                                !ec_scont1 = CType(NTSCDec(s), String)
                                            Case 2
                                                !ec_scont2 = CType(NTSCDec(s), String)
                                            Case 3
                                                !ec_scont3 = CType(NTSCDec(s), String)
                                            Case 4
                                                !ec_scont4 = CType(NTSCDec(s), String)
                                            Case 5
                                                !ec_scont5 = CType(NTSCDec(s), String)
                                            Case 6
                                                !ec_scont6 = CType(NTSCDec(s), String)
                                        End Select
                                        y += 1
                                    Next

                                Catch ex As Exception
                                    LogWrite(oApp.Tr(Me, 129249476158780996, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per lo sconto 1, verrà lasciato quello attuale."), True)
                                End Try
                            End If
                        Case "sc2" 'Sconto 2
                            If Not bRicScn Then
                                Try
                                    !ec_scont2 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                                Catch ex As Exception
                                    LogWrite(oApp.Tr(Me, 129249480825655996, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per lo sconto 2, verrà lasciato quello attuale."), True)
                                End Try
                            End If
                        Case "sc3" 'Sconto 3
                            If Not bRicScn Then
                                Try
                                    !ec_scont3 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                                Catch ex As Exception
                                    LogWrite(oApp.Tr(Me, 129249480806593496, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per lo sconto 3, verrà lasciato quello attuale."), True)
                                End Try
                            End If
                        Case "sc4" 'Sconto 4
                            If Not bRicScn Then
                                Try
                                    !ec_scont4 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                                Catch ex As Exception
                                    LogWrite(oApp.Tr(Me, 129249480791280996, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per lo sconto 4, verrà lasciato quello attuale."), True)
                                End Try
                            End If
                        Case "sc5" 'Sconto 5
                            If Not bRicScn Then
                                Try
                                    !ec_scont5 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                                Catch ex As Exception
                                    LogWrite(oApp.Tr(Me, 129249480775187246, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per lo sconto 5, verrà lasciato quello attuale."), True)
                                End Try
                            End If
                        Case "sc6" 'Sconto 6
                            If Not bRicScn Then
                                Try
                                    !ec_scont6 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                                Catch ex As Exception
                                    LogWrite(oApp.Tr(Me, 129249480762999746, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per lo sconto 6, verrà lasciato quello attuale."), True)
                                End Try
                            End If
                        Case "pr1" 'Perc. provvigioni 1
                            If Not bRicPrv Then
                                Try
                                    !ec_provv = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                                Catch ex As Exception
                                    LogWrite(oApp.Tr(Me, 129249490225549446, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la provvigiorne 1, verrà lasciato quello attuale."), True)
                                End Try
                            End If
                        Case "pr2" 'Perc. provvigioni 2
                            If Not bRicPrv Then
                                Try
                                    !ec_provv2 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                                Catch ex As Exception
                                    LogWrite(oApp.Tr(Me, 129249490212891171, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la provvigione 2, verrà lasciato quello attuale."), True)
                                End Try
                            End If
                        Case "ma1" 'Magazzino 1
                            Try
                                !ec_magaz = NTSCInt(strPart(i + lStart).Trim())
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249490202733296, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per il magazzino 1, verrà lasciato quello attuale."), True)
                            End Try
                        Case "ma2" 'Magazzino 2
                            Try
                                !ec_magaz2 = NTSCInt(strPart(i + lStart).Trim())
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249490193356796, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per il magazzino 2, verrà lasciato quello attuale."), True)
                            End Try
                        Case "not" 'Note
                            !ec_note = strPart(i + lStart).Trim()
                        Case "cau" 'Causale
                            Try
                                !ec_causale = NTSCInt(strPart(i + lStart).Trim())
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249490182730096, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la causale, verrà lasciato quello attuale."), True)
                            End Try
                        Case "cnp" 'Contropartita
                            Try
                                !ec_controp = NTSCInt(strPart(i + lStart).Trim())
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249490172103396, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la contropartita, verrà lasciato quello attuale."), True)
                            End Try
                        Case "iva" 'Codice iva
                            Try
                                !ec_codiva = NTSCInt(strPart(i + lStart).Trim())
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249491557602206, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per il codice iva, verrà lasciato quello attuale."), True)
                            End Try
                        Case "str" 'Stampa riga
                            !ec_stasino = strPart(i + lStart).Trim()
                        Case "com" 'Commessa
                            Try
                                !ec_commeca = NTSCInt(strPart(i + lStart).Trim())
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249491567759106, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la commessa, verrà lasciato quello attuale."), True)
                            End Try
                        Case "lot" 'Lotto
                            Try
                                !xxo_lottox = NTSCStr(strPart(i + lStart).Trim())
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249491579009826, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per il lotto, verrà lasciato quello attuale."), True)
                            End Try
                        Case "cda" 'Centro di CA
                            Try
                                !ec_codcena = NTSCInt(strPart(i + lStart).Trim())
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249491589791766, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per il centro, verrà lasciato quello attuale."), True)
                            End Try
                        Case "cca" 'Conto contropartita CA
                            Try
                                !ec_contocontr = NTSCInt(strPart(i + lStart).Trim())
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249480688624746, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la contropartita di CA, verrà lasciato quello attuale."), True)
                            End Try
                        Case "mi1" 'Misura 1
                            Try
                                !ec_misura1 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249489386508971, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la misura 1, verrà lasciato quello attuale."), True)
                            End Try
                        Case "mi2" 'Misura 2
                            Try
                                !ec_misura2 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249489377445021, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la misura 2, verrà lasciato quello attuale."), True)
                            End Try
                        Case "mi3" 'Misura 3
                            Try
                                !ec_misura3 = NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249489366193221, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la misura 3, verrà lasciato quello attuale."), True)
                            End Try
                        Case "fas" 'Fase
                            Try
                                !ec_fase = NTSCInt(strPart(i + lStart).Trim())
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249489351190821, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la fase, verrà lasciato quello attuale."), True)
                            End Try
                        Case "ubi" 'Ubicazione
                            !ec_ubicaz = strPart(i + lStart).Trim()
                        Case "prn" 'Prezzo netto
                            '!ec_flprznet = strPart(i + lStart).Trim()

                            !ec_prezzo = NTSCDec(strPart(i + lStart).Trim()) / qta
                            'NTSCDec(strPart(i + lStart).Trim().Replace(strDec, ",")) / NTSCDec(strTracciato(i).Split("-"c)(3)) 'Dividi per

                        Case "pro" 'Promozione
                            !ec_codtpro = strPart(i + lStart).Trim()
                        Case "dic" 'Data inizio competenza
                            Try
                                !ec_datini = NTSCDate(strPart(i + lStart).Trim())
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249489320404646, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la data inizio competenza, verrà lasciato quello attuale."), True)
                            End Try
                        Case "dif" 'Data fine competenza
                            Try
                                !ec_datfin = NTSCDate(strPart(i + lStart).Trim())
                            Catch ex As Exception
                                LogWrite(oApp.Tr(Me, 129249491679797526, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la data fine competenza, verrà lasciato quello attuale."), True)
                            End Try
                        Case "dtc" 'Data consegna
                            If strChiamante = "BSORGSOR" Then
                                Try
                                    !ec_datcons = NTSCDate(strPart(i + lStart).Trim())
                                Catch ex As Exception
                                    LogWrite(oApp.Tr(Me, 129249489613732821, "Valore '|" & strPart(i + lStart).Trim & "|' non valido per la data consegna, verrà lasciato quello attuale."), True)
                                End Try
                            End If
                        Case "con" 'Confermato
                            If strChiamante = "BSORGSOR" Then !ec_confermato = strPart(i + lStart).Trim()
                        Case "ril" 'Rilasciato
                            If strChiamante = "BSORGSOR" Then !ec_rilasciato = strPart(i + lStart).Trim()
                    End Select
                Next

                'Se non ho indicato il tipo riga, allora prendo la taglia dal barcode
                If Not bTipoRiga AndAlso strTaglia <> "" Then
                    'Considero che appartengano all'ultima riga importata
                    oCldDocu.ValCodiceDb(NTSCStr(dttEC.Rows(dttEC.Rows.Count - 1)!xxo_codtagl), strDittaCorrente, "TABTAGL", "N", "", dttTagl)
                    'Aggiungo subito la nuova riga (controllando che non ci sia già una esistente)
                    dtrRow = dttECTC.Select("ec_riga = " & NTSCInt(dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga))
                    If dtrRow.Length = 0 Then
                        dttECTC.Rows.Add(dttECTC.NewRow)
                        With dttECTC.Rows(dttECTC.Rows.Count - 1)
                            !codditt = strDittaCorrente
                            !ec_tipork = dttEC.Rows(dttEC.Rows.Count - 1)!ec_tipork
                            !ec_anno = dttEC.Rows(dttEC.Rows.Count - 1)!ec_anno
                            !ec_serie = dttEC.Rows(dttEC.Rows.Count - 1)!ec_serie
                            !ec_numdoc = dttEC.Rows(dttEC.Rows.Count - 1)!ec_numdoc
                            !ec_riga = dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga
                        End With
                        dtrRow = dttECTC.Select("ec_riga = " & NTSCInt(dttEC.Rows(dttEC.Rows.Count - 1)!ec_riga))
                    End If

                    For i = 1 To 24
                        'Se trovo la taglia la assegno 
                        If NTSCStr(dttTagl.Rows(0)("tb_dest" & i.ToString("00"))) = strTaglia Then
                            dtrRow(0)("ec_quant" & i.ToString("00")) = dttEC.Rows(dttEC.Rows.Count - 1)!ec_quant
                            Exit For
                        End If
                    Next
                End If
            End With

            '--------------------------------------------------------------------------------------------------------------
            bDisabilitaMsgPrezzoZero = True
            RecordSalva(dttEC.Rows.Count - 1, False, Nothing)
            '--------------------------------------------------------------------------------------------------------------

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
            bDisabilitaMsgPrezzoZero = bDisabilitaMsgPrezzoZeroTmp
        End Try
    End Function

    'Public Overrides Function beforeColUpdate_CORPO_ec_quant(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs) As Boolean
    '    Dim dttTmp As New DataTable
    '    Dim dDisponibilita As Decimal = 0
    '    Dim dDisponibilitaNetta As Decimal = 0
    '    Dim dOrdinato As Decimal = 0
    '    Dim dImpegnato As Decimal = 0
    '    Dim dUltCost As Decimal = 0
    '    Try
    '        ''--------------------------------------------------------------------------------------------------------------
    '        'If bNew = True Then e.Row!ec_datconsor = e.ProposedValue
    '        ''--------------------------------------------------------------------------------------------------------------
    '        'If MyBase.AfterColUpdate_CORPO_ec_codart(sender, e) Then

    '        If NTSCStr(e.ProposedValue).Trim <> "" Then
    '                oCldGsor.ValCodiceDb(dttEt_conto.Rows(0)!an_conto.ToString,
    '                        strDittaCorrente, "ANAGRA", "N", "", dttTmp)
    '                Leggidisponibilita(e.Row!ec_codart.ToString, 2, 0, 0, dDisponibilita, dDisponibilitaNetta, dUltCost, dOrdinato, dImpegnato, 0, "")
    '                'MsgBox(dDisponibilitaNetta.ToString)
    '                If dttTmp.Rows(0)!an_omocodice.ToString = "S" And dDisponibilitaNetta > 0 And dDisponibilitaNetta >= NTSCInt(e.Row!ec_quant.ToString) Then
    '                    e.Row!ec_magaz = 2
    '                End If
    '            End If
    '        ' End If

    '        Return True
    '    Catch ex As Exception
    '        If GestErrorCallThrow() Then
    '            Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
    '        Else
    '            ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
    '        End If
    '    End Try
    'End Function
End Class
