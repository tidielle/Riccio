Imports System
Imports System.Data
Imports System.Data.Common
Imports NTSInformatica.CLN__STD

Public Class CLDLBMENU
  Inherits CLD__BASE

  Dim oScriptFormulaNet As Object = Nothing
  Dim dtLastScriptNet As DateTime = DateTime.Now
  Dim strFormulaNetFormula As String = ""

  Public Overrides Sub Init(ByVal Applic As CLE__APP)
    MyBase.Init(Applic)
  End Sub

  '------------------------------------------------------------------------
  ' Passato il nome del programma chiamante, il tipo del documento in esame,
  ' l'articolo indicato nella riga del movimento e la sua unità di misura
  ' ci dice se siamo nella condizioni di applicabilit per i
  ' PREZZI RIFERITI AD unità' DI MISURA DIVERSA DALLA PRINCIPALE
  '
  ' IN: bGestisciPrezzi = gestione o meno dei prezzi riferiti ad u.d.m. diversa dalla principale
  '     strNomeprog = programma chiamante
  '     strTipork = tipo documento
  '     bTerzista = specifica se ordine/carico di produzione da terzista o interno
  '     strCodart = articolo movimentato
  '     strUnmis = unità di misura per l'articolo movimentato
  '
  ' OU: True =  i prezzi sono riferiti ad una u.d.m. diversa dalla principale
  '     False = i prezzi sono riferiti alla u.d.m. principale
  '------------------------------------------------------------------------
  Public Overridable Function GestioneUMPrezzo(ByVal bGestisciPrezzi As Boolean, _
                                  ByVal strNomeprog As String, _
                                  ByVal strTipork As String, _
                                  ByVal bTerzista As Boolean, _
                                  ByVal strCodart As String, _
                                  ByVal strUnmis As String, _
                                  ByVal strDitta As String) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim bOut As Boolean = False

    Try
      If bGestisciPrezzi = False Then Return False
      If (strCodart = "D" Or strCodart = "M") Then Return False

      strSQL = " SELECT ar_umpdapr, ar_umpdapra, ar_confez2, ar_unmis2, ar_um4 FROM artico " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " And ar_codart = " & CStrSQL(strCodart)

      Select Case strNomeprog.ToUpper
        Case "BSORGSOR"
          Select Case strTipork
            Case "$", "O" 'ORDINE FORNITORE APERT0, ORDINE FORNITORE
              dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

              If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
                Select Case dsTmp.Tables("ARTICO").Rows(0)!ar_umpdapra.ToString
                  Case "C"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_confez2.ToString Then bOut = True
                  Case "S"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_unmis2.ToString Then bOut = True
                  Case "Q"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_um4.ToString Then bOut = True
                End Select
              End If
              dsTmp.Tables.Clear()
              dsTmp.Dispose()
            Case "#", "R", "Q", "V"
              dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

              If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
                Select Case dsTmp.Tables("ARTICO").Rows(0)!ar_umpdapr.ToString
                  Case "C"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_confez2.ToString Then bOut = True
                  Case "S"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_unmis2.ToString Then bOut = True
                  Case "Q"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_um4.ToString Then bOut = True
                End Select
              End If
              dsTmp.Tables.Clear()
              dsTmp.Dispose()
            Case "H" 'ORDINE DI PRODUZIONE (solo esterno)
              If bTerzista Then
                dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

                If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
                  Select Case dsTmp.Tables("ARTICO").Rows(0)!ar_umpdapra.ToString
                    Case "C"
                      If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_confez2.ToString Then bOut = True

                    Case "S"
                      If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_unmis2.ToString Then bOut = True

                      'prezzi per UM formula non sono gestiti/sono bloccatti anche in bemgdocu
                      'Case "Q"
                      '  If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_um4.ToString Then bOut = True
                  End Select
                End If
                dsTmp.Tables.Clear()
                dsTmp.Dispose()
              End If
          End Select

        Case "BSORGNOR"
          'Non serve...
        Case "BSORGSOL"
          Select Case strTipork
            Case "O" 'PROPOSTA D'ORDINE A FORNITORE
              dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

              If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
                Select Case dsTmp.Tables("ARTICO").Rows(0)!ar_umpdapra.ToString
                  Case "C"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_confez2.ToString Then bOut = True

                  Case "S"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_unmis2.ToString Then bOut = True

                    'per ora non gestiti prezzi per um formula: altrimenti andrebbe modificato anche l'mrp in vb6
                End Select
              End If
              dsTmp.Tables.Clear()
              dsTmp.Dispose()

            Case "H" 'PROPOSTA D'ORDINE DI PRODUZIONE (solo esterno)
              If bTerzista Then
                dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

                If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
                  Select Case dsTmp.Tables("ARTICO").Rows(0)!ar_umpdapra.ToString
                    Case "C"
                      If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_confez2.ToString Then bOut = True
                    Case "S"
                      If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_unmis2.ToString Then bOut = True

                      'per ora non gestiti prezzi per um formula: altrimenti andrebbe modificato anche l'mrp in vb6
                  End Select
                End If
                dsTmp.Tables.Clear()
                dsTmp.Dispose()
              End If
          End Select

        Case "BSVEBOLL"
          Select Case strTipork
            Case "J", "L", "M" 'NOTA DI ACCREDITO RICEVUTA, FATTURA IMMEDIATA RICEVUTA, DDT RICEVUTO
              dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

              If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
                Select Case dsTmp.Tables("ARTICO").Rows(0)!ar_umpdapra.ToString
                  Case "C"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_confez2.ToString Then bOut = True
                  Case "S"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_unmis2.ToString Then bOut = True
                  Case "Q"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_um4.ToString Then bOut = True
                End Select
              End If
              dsTmp.Tables.Clear()
              dsTmp.Dispose()
            Case "A", "B", "C", "D", "E", "F", "I", "N", "S", "W", "Z"
              dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

              If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
                Select Case dsTmp.Tables("ARTICO").Rows(0)!ar_umpdapr.ToString
                  Case "C"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_confez2.ToString Then bOut = True
                  Case "S"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_unmis2.ToString Then bOut = True
                  Case "Q"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_um4.ToString Then bOut = True
                End Select
              End If
              dsTmp.Tables.Clear()
              dsTmp.Dispose()
            Case "T" 'CARICO DI PRODUZIONE (solo esterno)
              dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

              If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
                Select Case dsTmp.Tables("ARTICO").Rows(0)!ar_umpdapra.ToString
                  Case "C"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_confez2.ToString Then bOut = True
                  Case "S"
                    If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_unmis2.ToString Then bOut = True
                    'prezzi per UM formula non sono gestiti/sono bloccatti anche in bemgdocu
                    'Case "Q"
                    '  If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_um4.ToString Then bOut = True
                End Select
              End If
              dsTmp.Tables.Clear()
              dsTmp.Dispose()
          End Select
        Case "BSVEFDIN"
          'non serve
        Case "BSDBEMRP"
          'non serve, gli passo "BSORGSOL"... tanto le tipologie di documenti sono le stesse
        Case "BSCRGSOF"
          dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

          If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
            Select Case dsTmp.Tables("ARTICO").Rows(0)!ar_umpdapr.ToString
              Case "C"
                If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_confez2.ToString Then bOut = True
              Case "S"
                If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_unmis2.ToString Then bOut = True
              Case "Q"
                If strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_um4.ToString Then bOut = True
            End Select
          End If
          dsTmp.Tables.Clear()
          dsTmp.Dispose()
      End Select

      Return bOut

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function IsTerzista(ByVal strDitta As String, ByVal nMaga As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim bOut As Boolean = False

    Try
      strSQL = "SELECT tb_flclavo FROM tabmaga" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tb_codmaga = " & nMaga.ToString

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TMP")
      If dsTmp.Tables("TMP").Rows.Count > 0 Then
        If (dsTmp.Tables("TMP").Rows(0)!tb_flclavo.ToString = "F" Or _
            dsTmp.Tables("TMP").Rows(0)!tb_flclavo.ToString = "X") Then bOut = True
      End If

      dsTmp.Tables.Clear()
      dsTmp.Dispose()

      Return bOut

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  '------------------------------------------------------------------------
  ' Passata l'unità di misura e la sua quantità restituisce (in dQtaUMP)
  ' la quantità in unità di misura principale
  '
  ' IN: strCodart = codice dell'articolo, se viene indicato ignora i para-
  '                 metri opzionali e legge da ARTICO le inf. sulle U.M.;
  '                 se non  indicato ("") sono obbligatori i parametri
  '                 opzionali.
  '     strUM = unità di misura da convertire
  '     dQtaUM = quantità in strUM da convertire
  '     dMis1, dMis2, dMis3 = Misure
  '     nRound = n decimali di arrotondamento (se manca 3)
  '
  ' OU: dQtaUMP = quantità in UMP convertita
  '     bDaMisure = se true  stata applicata la formula
  '     False se si  verificato un errore, True se Tutto OK
  '------------------------------------------------------------------------
  Public Overridable Function ConvQuantUMP(ByVal strDitta As String, _
                              ByVal strCodart As String, _
                              ByVal strUM As String, _
                              ByVal dQtaUM As Decimal, _
                              ByVal dMis1 As Decimal, _
                              ByVal dMis2 As Decimal, _
                              ByVal dMis3 As Decimal, _
                              ByRef dQtaUMP As Decimal, _
                              ByRef bDaMisure As Boolean, _
                              ByRef strError As String, _
                              Optional ByVal nRound As Integer = -1, _
                              Optional ByVal strUnmis As String = ".", _
                              Optional ByVal strConfez2 As String = ".", _
                              Optional ByVal dQtacon2 As Decimal = -1, _
                              Optional ByVal strUnmis2 As String = ".", _
                              Optional ByVal dConver As Decimal = -1, _
                              Optional ByVal strFormula As String = ".", _
                              Optional ByVal strUnmis4 As String = ".") As Boolean
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim bOut As Boolean = False

    Try
      strError = ""
      dQtaUMP = 0
      strCodart = strCodart.ToUpper
      bDaMisure = False

      '-------------------------------------
      'Esce se il codice articolo e D o M
      If strCodart = "D" Or strCodart = "M" Then
        dQtaUMP = dQtaUM
        Return True
      End If

      '-------------------------------------
      'Esce se non sono passati
      If strCodart = "" And (strConfez2 = "." Or dQtacon2 = -1 Or strUnmis2 = "." Or dConver = -1 Or strFormula = "." Or strUnmis4 = ".") Then
        strError = oApp.Tr(Me, 127792184730781250, "Funzione ConvQuantUMP: se non è passato il Codice dell'articolo sono obbligatori tutti i parametri opzionali.")
        Return False
      End If

      If nRound = -1 Then nRound = oApp.NDecQta
      If strConfez2 = "." Then strConfez2 = ""
      If strUnmis2 = "." Then strUnmis2 = ""
      If strFormula = "." Then strFormula = ""
      If strUnmis4 = "." Then strUnmis4 = ""
      If dQtacon2 = -1 Then dQtacon2 = 0
      If dConver = -1 Then dConver = 0

      '-------------------------------------
      'Se viene passato l'articolo legge le informazioni su ARTICO
      If strCodart <> "" Then
        strSQL = "SELECT ar_unmis, ar_confez2, ar_qtacon2, ar_unmis2, ar_conver, ar_formula, ar_um4 FROM artico" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & " AND ar_codart = " & CStrSQL(strCodart)

        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
        If dsTmp.Tables("ARTICO").Rows.Count = 0 Then
          strError = oApp.Tr(Me, 127792185334531250, "Funzione ConvQuantUMP: Articolo '|" & strCodart & "|' inesistente.")
          Return False
        Else
          strUnmis = dsTmp.Tables("ARTICO").Rows(0)!ar_unmis.ToString
          strConfez2 = dsTmp.Tables("ARTICO").Rows(0)!ar_confez2.ToString
          dQtacon2 = NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!ar_qtacon2)
          strUnmis2 = dsTmp.Tables("ARTICO").Rows(0)!ar_unmis2.ToString
          dConver = NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!ar_conver)
          strFormula = dsTmp.Tables("ARTICO").Rows(0)!ar_formula.ToString
          strUnmis4 = dsTmp.Tables("ARTICO").Rows(0)!ar_um4.ToString
        End If
        dsTmp.Tables.Clear()
        dsTmp.Dispose()
      End If    'If strCodart <> "" Then

      strUM = strUM.ToUpper
      strFormula = strFormula.Trim
      strUnmis = strUnmis.ToUpper
      strConfez2 = strConfez2.ToUpper
      strUnmis2 = strUnmis2.ToUpper
      strUnmis4 = strUnmis4.ToUpper

      '-------------------------------------
      'Converte le quantità
      Select Case strUM
        Case strUnmis
          dQtaUMP = dQtaUM
        Case strConfez2
          dQtaUMP = ArrDbl(dQtaUM * dQtacon2, nRound)
        Case strUnmis2
          If dConver = 0 Then
            strError = oApp.Tr(Me, 127792185379531250, "Articolo '|" & strCodart & "|': quantità per confezione e Conversione unità di misura devono essere diversi da 0.")
            Return False
          End If
          dQtaUMP = ArrDbl(dQtaUM / dConver, nRound)
        Case strUnmis4
          If strFormula <> "" Then
            dQtaUMP = ArrDbl(FormulaVal(strFormula, dMis1, dMis2, dMis3, dQtaUM, dConver), nRound)
            bDaMisure = True
          Else
            'Se manca la formula la rende uguale alla unità di misura principale
            dQtaUMP = dQtaUM
          End If
        Case Else
          strError = oApp.Tr(Me, 127792185421718750, "Articolo '|" & strCodart & "|': l'unità di misura |" & strUM & "| non consentita.")
          Return False
      End Select

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strCodart & " - " & strFormula, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function

  '------------------------------------------------------------------------
  ' Passata l'unità di misura e la sua quantità restituisce (in dQtaUMP2)
  ' la quantità espressa in unità di misura strUM2
  '
  ' IN: strCodart = codice dell'articolo, se viene indicato ignora i para-
  '                 metri opzionali e legge da ARTICO le inf. sulle U.M.;
  '                 se non  indicato ("") sono obbligatori i parametri
  '                 opzionali.
  '     strUM = unità di misura da convertire
  '     dQtaUM = quantità in strUM da convertire
  '     dMis1, dMis2, dMis3 = Misure
  '     strUM2 = unità di misura richiesta
  '     nRound = arrotondamento n decimali (se mancante 3)
  '
  ' OU: dQtaUM2 = quantità espressa in strUM2 convertita
  '     False se si  verificato un errore, True se Tutto OK
  '------------------------------------------------------------------------
  Public Overridable Function ConvQuantUM(ByVal strDitta As String, _
                              ByVal strCodart As String, _
                              ByVal strUM As String, _
                              ByVal dQtaUM As Decimal, _
                              ByVal dMis1 As Decimal, _
                              ByVal dMis2 As Decimal, _
                              ByVal dMis3 As Decimal, _
                              ByVal strUM2 As String, _
                              ByRef dQtaUM2 As Decimal, _
                              ByRef strError As String, _
                              Optional ByVal nRound As Integer = -1, _
                              Optional ByVal strUnmis As String = ".", _
                              Optional ByVal strConfez2 As String = ".", _
                              Optional ByVal dQtacon2 As Decimal = -1, _
                              Optional ByVal strUnmis2 As String = ".", _
                              Optional ByVal dConver As Decimal = -1, _
                              Optional ByVal strFormula As String = ".", _
                              Optional ByVal strUnmis4 As String = ".") As Boolean
    Dim snaArti As DataTable
    Dim strSQL As String = ""
    Dim dRapp As Decimal
    Dim dTemp As Decimal

    Try

      If nRound = -1 Then nRound = oApp.NDecQta

      dQtaUM2 = 0
      strCodart = UCase(strCodart)
      strUM = UCase(strUM)
      strUM2 = UCase(strUM2)

      '----------------------
      'L'UM di origine  uguale alla destinazione
      If strUM = strUM2 Then
        dQtaUM2 = ArrDbl(dQtaUM, nRound)
        Return True
      End If

      '----------------------
      'Esce se il codice articolo e D o M
      If strCodart = "D" Or strCodart = "M" Then
        dQtaUM2 = dQtaUM
        Return True
      End If

      '----------------------
      'Esce se non sono passati i dati minimi
      If strCodart = "" And (strConfez2 = "." Or dQtacon2 = -1 Or strUnmis2 = "." Or dConver = -1 Or strFormula = "." Or strUnmis4 = ".") Then
        Throw New NTSException("Funzione ConvQuantUM: se non è passato il Codice dell'articolo sono obbligatori tutti i parametri opzionali.")
        Return False
      End If

      If strConfez2 = "." Then strConfez2 = ""
      If strUnmis2 = "." Then strUnmis2 = ""
      If strFormula = "." Then strFormula = ""
      If strUnmis4 = "." Then strUnmis4 = ""
      If dQtacon2 = -1 Then dQtacon2 = 0
      If dConver = -1 Then dConver = 0

      '----------------------
      'Se viene passato l'articolo legge le informazioni su ARTICO
      If strCodart <> "" Then
        strSQL = "SELECT ar_unmis, ar_confez2, ar_qtacon2, ar_unmis2, ar_conver, ar_formula, ar_um4 FROM artico" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND ar_codart = " & CStrSQL(strCodart)
        snaArti = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaArti").Tables(0)

        If snaArti.Rows.Count = 0 Then
          snaArti.Clear()
          strError = oApp.Tr(Me, 127792186092968750, "Articolo '|" & strCodart & "|' inesistente.")
          Return False
        End If
        strUnmis = snaArti.Rows(0)!ar_unmis.ToString
        strConfez2 = snaArti.Rows(0)!ar_confez2.ToString
        dQtacon2 = NTSCDec(snaArti.Rows(0)!ar_qtacon2)
        strUnmis2 = snaArti.Rows(0)!ar_unmis2.ToString
        dConver = NTSCDec(snaArti.Rows(0)!ar_conver)
        strFormula = snaArti.Rows(0)!ar_formula.ToString
        strUnmis4 = snaArti.Rows(0)!ar_um4.ToString
        snaArti.Clear()
      End If

      strFormula = Trim(strFormula)
      strUnmis = UCase(strUnmis)
      strConfez2 = UCase(strConfez2)
      strUnmis2 = UCase(strUnmis2)
      strUnmis4 = UCase(strUnmis4)

      '----------------------
      'Controllo validit unità di misura
      If strUM <> strUnmis And strUM <> strConfez2 And strUM <> strUnmis2 And strUM <> strUnmis4 Then
        strError = oApp.Tr(Me, 127792187173437500, "Articolo '|" & strCodart & "|': l'unità di misura di origine |" & strUM & "| non consentita.")
        Return False
      End If
      If strUM2 <> strUnmis And strUM2 <> strConfez2 And strUM2 <> strUnmis2 And strUM2 <> strUnmis4 Then
        strError = oApp.Tr(Me, 127792187214843750, "Articolo '|" & strCodart & "|': l'unità di misura di destinazione |" & strUM2 & "| non consentita.")
        Return False
      End If

      '----------------------
      'Converte le quantità
      Select Case strUM
        Case strUnmis
          '-------------------------------------------------------------------
          Select Case strUM2
            Case strConfez2
              'UMP => Confez2
              If dQtacon2 = 0 Then
                strError = oApp.Tr(Me, 127792187273593750, "Articolo '|" & strCodart & "|': quantità per confezione e Conversione unità di misura devono essere diversi da 0.")
                Return False
              End If
              dQtaUM2 = ArrDbl(dQtaUM / dQtacon2, nRound)
            Case strUnmis2
              'UMP => Unmis2
              dQtaUM2 = ArrDbl(dQtaUM * dConver, nRound)
            Case Else
              'UMP => Unmis4
              If strFormula <> "" Then
                'Valuta la formula (calcola l'inverso, non  il massimo...)
                dTemp = FormulaVal(strFormula, dMis1, dMis2, dMis3, 10, dConver)
                If dTemp = 0 Then
                  dRapp = 0
                Else
                  dRapp = (10 / dTemp)
                End If
                dQtaUM2 = ArrDbl(dQtaUM * dRapp, nRound)
              Else
                strError = oApp.Tr(Me, 127792187304062500, "Articolo '|" & strCodart & "|': formula obbligatoria se si usa l'unità di misura 4.")
                Return False
              End If
          End Select
          '-------------------------------------------------------------------
        Case strConfez2
          '-------------------------------------------------------------------
          Select Case strUM2
            Case strUnmis
              'Confez2 => UMP
              dQtaUM2 = ArrDbl(dQtaUM * dQtacon2, nRound)
            Case strUnmis2
              'Confez2 => Unmis2
              dQtaUM2 = ArrDbl((dQtaUM * dQtacon2) * dConver, nRound)
            Case Else
              'Confez2 => Unmis4
              If strFormula <> "" Then
                If dQtacon2 = 0 Then
                  strError = oApp.Tr(Me, 127792187385625000, "Articolo '|" & strCodart & "|': quantità per confezione e Conversione unità di misura devono essere diversi da 0.")
                  Return False
                End If
                'Valuta la formula (calcola l'inverso, non  il massimo...)
                dTemp = FormulaVal(strFormula, dMis1, dMis2, dMis3, 10, dConver) / dQtacon2
                If dTemp = 0 Then
                  dRapp = 0
                Else
                  dRapp = (10 / dTemp)
                End If
                dQtaUM2 = ArrDbl(dQtaUM * dRapp, nRound)
              Else
                strError = oApp.Tr(Me, 127792187433750000, "Articolo '|" & strCodart & "|': formula obbligatoria se si usa l'unità di misura 4.")
                Return False
              End If
          End Select
          '-------------------------------------------------------------------
        Case strUnmis2
          '-------------------------------------------------------------------
          Select Case strUM2
            Case strUnmis
              'Unmis2 => UMP
              If dConver = 0 Then
                strError = oApp.Tr(Me, 127792187482968750, "Articolo '|" & strCodart & "|': quantità per confezione e Conversione unità di misura devono essere diversi da 0.")
                Return False
              End If
              dQtaUM2 = ArrDbl(dQtaUM / dConver, nRound)
            Case strConfez2
              'Unmis2 => Confez2
              If dQtacon2 = 0 Or dConver = 0 Then
                strError = oApp.Tr(Me, 127792187518125000, "Articolo '|" & strCodart & "|': quantità per confezione e Conversione unità di misura devono essere diversi da 0.")
                Return False
              End If
              dQtaUM2 = ArrDbl((dQtaUM / dConver) / dQtacon2, nRound)
            Case Else
              'Unmis2 => Unmis4
              If strFormula <> "" Then
                If dConver = 0 Then
                  strError = oApp.Tr(Me, 127792187566875000, "Articolo '|" & strCodart & "|': quantità per confezione e Conversione unità di misura devono essere diversi da 0.")
                  Return False
                End If
                'Valuta la formula (calcola l'inverso, non  il massimo...)
                dTemp = FormulaVal(strFormula, dMis1, dMis2, dMis3, 10, dConver)
                If dTemp = 0 Then
                  dRapp = 0
                Else
                  dRapp = (10 / dTemp)
                End If
                dQtaUM2 = ArrDbl((dQtaUM / dConver) * dRapp, nRound)
              Else
                strError = oApp.Tr(Me, 127792187610312500, "Articolo '|" & strCodart & "|': formula obbligatoria se si usa l'unità di misura 4.")
                Return False
              End If
          End Select
          '-------------------------------------------------------------------
        Case Else 'Unmis4
          If strFormula = "" Then
            strError = oApp.Tr(Me, 127792187645000000, "Articolo '|" & strCodart & "|': formula obbligatoria se si usa l'unità di misura 4.")
            Return False
          End If
          Select Case strUM2
            Case strUnmis
              'Unmis4 => UMP
              dQtaUM2 = ArrDbl(FormulaVal(strFormula, dMis1, dMis2, dMis3, dQtaUM, dConver), nRound)
            Case strConfez2
              'Unmis4 => Confez2
              If dQtacon2 = 0 Then
                strError = oApp.Tr(Me, 127792187685937500, "Articolo '|" & strCodart & "|': quantità per confezione e Conversione unità di misura devono essere diversi da 0.")
                Return False
              End If
              dQtaUM2 = ArrDbl((FormulaVal(strFormula, dMis1, dMis2, dMis3, dQtaUM, dConver) / dQtacon2), nRound)
            Case Else
              'Unmis4 => Unmis2
              dQtaUM2 = ArrDbl((FormulaVal(strFormula, dMis1, dMis2, dMis3, dQtaUM, dConver) * dConver), nRound)
          End Select
      End Select

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function

  '------------------------------------------------------------------------
  ' Passata l'Unità di misura e la sua quantità restituisce (in dQtaUMP2)
  ' la quantità espressa in Unità di misura secondaria per l'INTARSTAT
  '
  ' IN: strCodart = codice dell'articolo
  '     strUM = Unità di misura da convertire
  '     dQtaUM = Quantità in strUM da convertire
  '     dMis1, dMis2, dMis3 = Misure
  '     nRound = arrotondamento n° decimali (se mancante 3)
  '
  ' OU: dQtaUM2 = Quantità espressa in strUM2 convertita
  '     False se si è verificato un errore, True se Tutto OK
  '------------------------------------------------------------------------
  Public Overridable Function ConvQuantUMIntra2(ByVal strDitta As String, ByVal strCodart As String, _
                                                ByVal strUM As String, ByVal dQtaUM As Decimal, _
                                                ByVal dMis1 As Decimal, ByVal dMis2 As Decimal, _
                                                ByVal dMis3 As Decimal, ByRef dQtaUM2 As Decimal, _
                                                Optional ByVal nRound As Integer = 3) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dRapp As Decimal = 0
    Dim dTemp As Decimal = 0
    Dim strUnmis As String
    Dim strConfez2 As String
    Dim dQtacon2 As Decimal
    Dim strUnmis2 As String
    Dim dConver As Decimal = 0
    Dim strFormula As String
    Dim strUnmis4 As String
    Dim strUmIntra2 As String
    Dim strErr As String = ""

    Try
      dQtaUM2 = 0
      strCodart = UCase(strCodart)

      'Esce se non sono passati
      If strCodart = "" Then
        Throw New NTSException(oApp.Tr(Me, 128678680445468750, "Funzione ConvQuantUMIntra2: codice articolo obbligatorio."))
        Return False
      End If

      'Esce se il codice articolo e D o M
      If strCodart = "D" Or strCodart = "M" Then
        dQtaUM2 = dQtaUM
        Return True
      End If

      'Legge le informazioni su ARTICO
      strSQL = "SELECT ar_unmis, ar_confez2, ar_qtacon2, ar_unmis2, ar_conver, ar_umintra2, ar_formula, ar_um4 FROM artico" & _
               " WHERE codditt = " & CStrSQL(strDitta) & " AND ar_codart = " & CStrSQL(strCodart)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        Throw New NTSException(oApp.Tr(Me, 128678681191250000, "Funzione ConvQuantUMIntra2: articolo '|" & strCodart & "|' inesistente."))
        Return False
      End If
      strUnmis = UCase(NTSCStr(dttTmp.Rows(0)!ar_unmis))
      strConfez2 = UCase(NTSCStr(dttTmp.Rows(0)!ar_confez2))
      dQtacon2 = NTSCDec(dttTmp.Rows(0)!ar_qtacon2)
      strUnmis2 = UCase(NTSCStr(dttTmp.Rows(0)!ar_unmis2))
      dConver = NTSCDec(dttTmp.Rows(0)!ar_conver)
      strFormula = Trim(NTSCStr(dttTmp.Rows(0)!ar_formula))
      strUmIntra2 = UCase(NTSCStr(dttTmp.Rows(0)!ar_umintra2))
      strUnmis4 = UCase(NTSCStr(dttTmp.Rows(0)!ar_um4))
      dttTmp.Clear()
      strUM = UCase(strUM)
      'Controllo validità unità di misura
      If strUM <> strUnmis And strUM <> strConfez2 And strUM <> strUnmis2 And strUM <> strUnmis4 Then
        Throw New NTSException(oApp.Tr(Me, 128678682904062500, "Funzione ConvQuantUMIntra2: articolo '|" & strCodart & "|', unità di misura di origine |" & strUM & "| non consentita."))
        Return False
      End If
      Select Case strUmIntra2
        Case "N"
          'Nessuna U.M. agganciata
          Return True
        Case "P"
          'U.M. principale
          If strUM = strUnmis Then
            dQtaUM2 = ArrDbl(dQtaUM, nRound)
          Else
            If Not ConvQuantUM(strDitta, "", strUM, dQtaUM, dMis1, dMis2, dMis3, strUnmis, dQtaUM2, strErr, 3, strUnmis, strConfez2, dQtacon2, strUnmis2, dConver, strFormula, strUnmis4) Then
              Return False
            End If
            If strErr <> "" Then Throw New NTSException(strErr)
          End If
        Case "C"
          'U.M. confezione
          If strConfez2 = "" Then
            Return True
          End If
          If strUM = strConfez2 Then
            dQtaUM2 = ArrDbl(dQtaUM, nRound)
          Else
            If Not ConvQuantUM(strDitta, "", strUM, dQtaUM, dMis1, dMis2, dMis3, strConfez2, dQtaUM2, strErr, 3, strUnmis, strConfez2, dQtacon2, strUnmis2, dConver, strFormula, strUnmis4) Then
              Return False
            End If
            If strErr <> "" Then Throw New NTSException(strErr)
          End If
        Case "S"
          'U.M. secondaria
          If strUnmis2 = "" Then
            Return True
          End If
          If strUM = strUnmis2 Then
            dQtaUM2 = ArrDbl(dQtaUM, nRound)
          Else
            If Not ConvQuantUM(strDitta, "", strUM, dQtaUM, dMis1, dMis2, dMis3, strUnmis2, dQtaUM2, strErr, 3, strUnmis, strConfez2, dQtacon2, strUnmis2, dConver, strFormula, strUnmis4) Then
              Return False
            End If
            If strErr <> "" Then Throw New NTSException(strErr)
          End If
        Case "Q"
          'Quantà unità di misura
          If strUnmis4 = "" Then
            Return True
          End If
          If strUM = strUnmis4 Then
            dQtaUM2 = ArrDbl(dQtaUM, nRound)
          Else
            If Not ConvQuantUM(strDitta, "", strUM, dQtaUM, dMis1, dMis2, dMis3, strUnmis4, dQtaUM2, strErr, 3, strUnmis, strConfez2, dQtacon2, strUnmis2, dConver, strFormula, strUnmis4) Then
              Return False
            End If
            If strErr <> "" Then Throw New NTSException(strErr)
          End If
      End Select

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function


  '------------------------------------------------------------------------
  ' Passato un articolo, una unità di misura, il prezzo dell'articolo nella
  ' sua unità di misura principale; restituisce il prezzo dell'articolo
  ' nella unità di misura passata.
  '(Supportato per solo il caso in cui l'u.d.m. passata  la unità di misura
  ' confezione o secondaria dell'articolo)
  '
  ' IN: strCodart = articolo
  '     strUnmis = unità di misura dell'articolo
  '     dPrezzo = prezzo dell'articolo riferito all'u.d.m. principale
  '     nCodvalu = valuta in cui  indicato il prezzo di cui sopra
  '
  ' OU: dPrezzo = prezzo dell'articolo (nella valuta nCodvalu) riferito all'u.d.m. strUnmis
  '------------------------------------------------------------------------
  Public Overridable Sub ConvertiPrezzoperUM(ByVal strDitta As String, ByVal strCodart As String, ByVal strUnMisin As String, ByRef dPrezzo As Decimal, ByVal nCodValu As Integer)
    Dim strSQL As String = ""
    Dim dsTmp As DataSet

    Try
      strSQL = " SELECT ar_confez2, ar_qtacon2, ar_unmis2, ar_conver FROM artico" & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND ar_codart = " & CStrSQL(strCodart)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

      If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
        Select Case strUnMisin
          Case dsTmp.Tables("ARTICO").Rows(0)!ar_confez2.ToString
            dPrezzo = ArrDbl(dPrezzo * NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!ar_qtacon2), TrovaNdecSuPrzUn(nCodValu))
          Case dsTmp.Tables("ARTICO").Rows(0)!ar_unmis2.ToString
            dPrezzo = ArrDbl(dPrezzo / NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!ar_conver), TrovaNdecSuPrzUn(nCodValu))
        End Select
      End If    'If dsTmp.Tables("ARTICO").Rows.Count > 0 Then

      dsTmp.Tables.Clear()
      dsTmp.Dispose()

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function FormulaVal(ByVal strFormula As String, ByVal dMisura1 As Decimal, ByVal dMisura2 As Decimal, _
                             ByVal dMisura3 As Decimal, ByVal dNpezzi As Decimal, ByVal dConver As Decimal) As Decimal
    ' input formula e valori delle misure (a,b,c), K=conver, n=numero pezzi, restituisce il valore
    Dim bOk As Boolean = False
    Dim Risultato As Decimal = 0
    Dim scrCtl As MSScriptControl.ScriptControl = Nothing
    Dim vRet As Object = Nothing
    Dim strMsg As String = ""

    FormulaVal = 0
    Try

      If oApp.oGvar.bUm4UsaMSScriptControl = False Or CLN__STD.IsWin64Bit Then
        Return FormulaValNET(strFormula, dMisura1, dMisura2, dMisura3, dNpezzi, dConver)
      End If

      scrCtl = New MSScriptControl.ScriptControl
      scrCtl.AllowUI = True
      scrCtl.Language = "VBScript"

      strMsg = "a (misura1) = " & CDblSQL(dMisura1.ToString).ToString & vbCrLf & _
               "b (misura2) = " & CDblSQL(dMisura2.ToString).ToString & vbCrLf & _
               "c (misura3) = " & CDblSQL(dMisura3.ToString).ToString & vbCrLf & _
               "n (colli) = " & CDblSQL(dNpezzi.ToString).ToString & vbCrLf & _
               "k (Anag.artic. MU second.) = " & CDblSQL(dConver.ToString).ToString & vbCrLf & _
               "Verrà impostato come risultato formula 0°°°"

      scrCtl.AddCode("Function NTSIIf(i, j, k)" & vbCrLf & "If i Then NTSIIf = j Else NTSIIf = k" & vbCrLf & "End Function")
      scrCtl.AddCode("a=" & CDblSQL(dMisura1.ToString)) ' sostituisce ev. virgole decimali con punti
      scrCtl.AddCode("b=" & CDblSQL(dMisura2.ToString))
      scrCtl.AddCode("c=" & CDblSQL(dMisura3.ToString))
      scrCtl.AddCode("k=" & CDblSQL(dConver.ToString))
      scrCtl.AddCode("n=" & CDblSQL(dNpezzi.ToString))

      Try
        vRet = scrCtl.Eval(strFormula)
      Catch ex As Exception
        If scrCtl.Error.Description.ToUpper.Contains("OVERFLOW") Or _
           scrCtl.Error.Description.ToUpper.Contains("DIVISIONE PER") Or _
           scrCtl.Error.Description.ToUpper.Contains("DIVISION BY") Then
          vRet = 0
        Else
          Throw ex
        End If
      End Try

      If vRet.GetType.ToString.ToLower = "system.double" Or vRet.GetType.ToString.ToLower = "system.long" Or _
         vRet.GetType.ToString.ToLower = "system.integer" Or vRet.GetType.ToString.ToLower = "system.decimal" Or _
         vRet.GetType.ToString.ToLower = "system.int16" Or vRet.GetType.ToString.ToLower = "system.int32" Then
        Risultato = NTSCDec(vRet)
      Else
        Throw (New NTSException(oApp.Tr(Me, 130415203587786867, "Errore : risultato formula non numerico , verificatosi quando era corrente la Formula |" & strFormula & vbCrLf & strMsg & "|")))
      End If

      Return Risultato

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "°°°Errore verificatosi in fase di valutazione Formula '" & strFormula & "'" & vbCrLf & strMsg, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return 0
    End Try
  End Function
  Public Overridable Function FormulaValNET(ByVal strFormula As String, ByVal dMisura1 As Decimal, ByVal dMisura2 As Decimal, _
                             ByVal dMisura3 As Decimal, ByVal dNpezzi As Decimal, ByVal dConver As Decimal) As Decimal

    'se non devo usare l'MSScriptControl creo la dll in memoria con tutte le formule
    'la ricreo ogni 10 secondi dall'ultimo utilizzo, diversamente sarebbe lentissimo
    Dim Risultato As Decimal = 0
    Dim vRet As Object = Nothing
    Dim strMsg As String = ""
    Dim strCode As String = ""
    Dim strError As String = ""

    Dim cpar As New System.CodeDom.Compiler.CompilerParameters()

    Try
      strMsg = "a (misura1) = " & CDec(dMisura1.ToString).ToString & vbCrLf & _
               "b (misura2) = " & CDec(dMisura2.ToString).ToString & vbCrLf & _
               "c (misura3) = " & CDec(dMisura3.ToString).ToString & vbCrLf & _
               "n (colli) = " & CDec(dNpezzi.ToString).ToString & vbCrLf & _
               "k (Anag.artic. MU second.) = " & CDec(dConver.ToString).ToString & vbCrLf & _
               "Verrà impostato come risultato formula 0°°°"

      If Not oScriptFormulaNet Is Nothing And strFormulaNetFormula.ToLower = strFormula.ToLower Then
        If DateTime.Now.Subtract(dtLastScriptNet).Seconds > 10 Then
          oScriptFormulaNet = Nothing
        Else
          GoTo ESEGUI
        End If
      End If

      cpar.GenerateInMemory = True
      cpar.GenerateExecutable = False
      cpar.IncludeDebugInformation = True
      cpar.ReferencedAssemblies.Add("mscorlib.dll")
      cpar.ReferencedAssemblies.Add("system.dll")
      cpar.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll")

      strCode = "Imports Microsoft.VisualBasic" & vbCrLf & _
                "Imports System" & vbCrLf & _
                "imports System.Math" & vbCrLf & _
                "Public class FORMULAEVAL " & vbCrLf & _
                "Shared Function NTSIIf(ByVal i As Boolean, ByVal j As Object, ByVal k As Object) As Object" & vbCrLf & _
                " Return IIf(i, j, k)" & vbCrLf & _
                "End Function " & vbCrLf & _
                "Public Shared Function EXEC(ByVal a as Double, ByVal b as Double, ByVal c as Double, ByVal k as Double, ByVal n as Double) as Double" & vbCrLf & _
                " return " & strFormula & vbCrLf & _
                "End Function " & vbCrLf & _
                "End Class"

      Try
        '----------------------------------------------
        'definisco il compilatore del linguaggio
        Dim cp As New Microsoft.VisualBasic.VBCodeProvider

        '----------------------------------------------
        'preparo l'oggetto che conterrà il risultato della compilazione (con gli eventuali errori)
        Dim cr As System.CodeDom.Compiler.CompilerResults

        '----------------------------------------------
        'eseguo la compilazione
        cr = cp.CompileAssemblyFromSource(cpar, strCode)

        '----------------------------------------------
        'testo gli errori di compilazione
        For Each ce As System.CodeDom.Compiler.CompilerError In cr.Errors
          If Not ce.IsWarning Then
            strError += "Linea " & ce.Line.ToString & " colonna " & ce.Column & ": errore " & ce.ErrorNumber.ToString & " - " & ce.ErrorText + vbCrLf
          End If
        Next
        If strError.Trim <> "" Then
          Throw New Exception(strError & vbCrLf & strCode)
        End If


        '----------------------------------------------
        'se tutto ok eseguo il codice
        If cr.Errors.Count = 0 And Not cr.CompiledAssembly Is Nothing Then
          Dim objType As Object = cr.CompiledAssembly.GetType("FORMULAEVAL")
          Try
            oScriptFormulaNet = Activator.CreateInstance(objType)

          Catch ex As Exception
            Throw ex
          End Try
        End If
      Catch ex As Exception
        Throw ex
      End Try

ESEGUI:
      vRet = CDec(oScriptFormulaNet.exec(dMisura1, dMisura2, dMisura3, dConver, dNpezzi))

      strFormulaNetFormula = strFormula
      dtLastScriptNet = DateTime.Now


      If vRet.GetType.ToString.ToLower = "system.double" Or vRet.GetType.ToString.ToLower = "system.long" Or _
         vRet.GetType.ToString.ToLower = "system.integer" Or vRet.GetType.ToString.ToLower = "system.decimal" Or _
         vRet.GetType.ToString.ToLower = "system.int16" Or vRet.GetType.ToString.ToLower = "system.int32" Then
        Risultato = NTSCDec(vRet)
      Else
        Throw (New NTSException(oApp.Tr(Me, 127791221367187500, "Errore : risultato formula non numerico , verificatosi quando era corrente la Formula |" & strFormula & vbCrLf & strMsg & "|")))
      End If

      Return Risultato

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "°°°Errore verificatosi in fase di valutazione Formula '" & strFormula & "'" & vbCrLf & strMsg, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function TrovaCodartDaBarcode(ByVal strChild As String, ByVal strBarcode As String, ByVal strDitta As String, _
                                       ByRef strUnmis As String, ByRef dQuant As Decimal, _
                                       ByRef dColli As Decimal, ByRef nFaseBC As Integer, ByRef strError As String) As String
    Dim dsTmp As New DataSet
    Try
      Return TrovaCodartDaBarcode(strChild, strBarcode, strDitta, strUnmis, dQuant, dColli, nFaseBC, strError, dsTmp)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dsTmp.Clear()
    End Try
  End Function
  Public Overridable Function TrovaCodartDaBarcode(ByVal strChild As String, ByVal strBarcode As String, ByVal strDitta As String, _
                                       ByRef strUnmis As String, ByRef dQuant As Decimal, _
                                       ByRef dColli As Decimal, ByRef nFaseBC As Integer, ByRef strError As String, _
                                       ByRef dsBarcode As DataSet) As String
    Dim strCodart As String = ""
    Dim strSQL As String = ""
    Dim bBarcodeConQta As Boolean             'se attiva (-1) rileva la quantità indicata nei barcode
    Dim bBarcodeDerogaQta As Boolean = False  'se attiva (-1) rileva la quantità indicata nei barcode pero' in deroga a BarcodeConQta (sono alternative)

    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strChild, strBarcode, strDitta, strUnmis, dQuant, dColli, nFaseBC, strError, dsBarcode})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strUnmis = NTSCStr(oIn(3))
        dQuant = NTSCDec(oIn(4))
        dColli = NTSCDec(oIn(5))
        nFaseBC = NTSCInt(oIn(6))
        strError = NTSCStr(oIn(7))
        dsBarcode = CType(oIn(8), DataSet)
        Return NTSCStr(oOut)
      End If
      '----------------

      strError = ""
      strSQL = "SELECT bc_unmis, bc_codart, bc_quant, bc_fase, bc_tagl, bc_tipo, ar_unmis, ar_confez2, ar_unmis2, ar_qtacon2, ar_conver, ar_um4, ar_codtagl " & _
               "FROM barcode INNER JOIN artico ON barcode.codditt = artico.codditt AND barcode.bc_codart = artico.ar_codart " & _
               " WHERE barcode.codditt = " & CStrSQL(strDitta) & _
               " AND bc_code = " & CStrSQL(strBarcode)
      dsBarcode = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "BARCODE")

      If dsBarcode.Tables("BARCODE").Rows.Count = 0 Then
        strError = oApp.Tr(Me, 127792188743750000, "Codice a barre inserito non presente o articolo associato al codice a barre inesistente")
      Else
        bBarcodeConQta = CBool(GetSettingBusDitt(strDitta, strChild, "Opzioni", ".", "BarcodeConQta", "0", " ", "0"))
        bBarcodeDerogaQta = CBool(GetSettingBusDitt(strDitta, strChild, "Opzioni", ".", "BarcodeDerogaQta", "0", " ", "0"))


        With dsBarcode.Tables("BARCODE").Rows(0)
          strCodart = !bc_codart.ToString
          nFaseBC = NTSCInt(!bc_fase)
          strUnmis = !bc_unmis.ToString
          'dQuant = NTSCDec(!bc_quant)

          '---------------------------------------
          'verifico l'unità di misura
          If !bc_unmis.ToString = !ar_unmis.ToString Or !bc_unmis.ToString = !ar_unmis2.ToString Or _
             !bc_unmis.ToString = !ar_confez2.ToString Or !bc_unmis.ToString = !ar_um4.ToString Then
          Else
            strError = oApp.Tr(Me, 127792188799843750, "L'unità di misura associata al codice a barre |" & strBarcode & "| non  una di quelle consentite per l'articolo. Verrà utilizzata l'unità di misura principale")
            strUnmis = !ar_unmis.ToString
          End If

          '---------------------------------------
          'determino la quantità
          If strUnmis = !ar_unmis.ToString Then
            If bBarcodeConQta Then
              dColli = NTSCDec(!bc_quant)
              If NTSCInt(!ar_codtagl) = 0 Then dQuant = NTSCDec(!bc_quant)
            Else
              If bBarcodeDerogaQta Then
                If NTSCInt(!ar_codtagl) = 0 Then dColli = 1
                dQuant = NTSCDec(!bc_quant)
              Else
                'standard
                If NTSCInt(!ar_codtagl) = 0 Then dColli = 1
                dQuant = 1
              End If
            End If    'If bBarcodeConQta Then
          Else
            If bBarcodeConQta Then
              dColli = NTSCDec(!bc_quant)
            Else
              If bBarcodeDerogaQta Then
                dColli = 1
                dQuant = NTSCDec(!bc_quant)
              Else
                'standard
                dColli = 1
              End If
            End If    'If bBarcodeConQta Then
          End If    'If strUnmis = !ar_unmis.ToString Then

        End With
      End If

      Return strCodart
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrovaBarcodeEAN14(ByVal strDitta As String, ByVal strBarcode As String, ByRef dsBarcode As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      'Lasciare la query in LIKE!!! In alcuni casi si passano delle condizioni particolare del barcode con _ e %
      strSQL = "SELECT * FROM barcode " & _
               " WHERE barcode.codditt = " & CStrSQL(strDitta) & _
               "   AND bc_code LIKE " & CStrSQL(strBarcode)

      dsBarcode = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "BARCODE")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaCodartDaCodarfo(ByVal strCodarfo As String, ByVal lContoCli As Integer, _
                                       ByVal strDitta As String, ByRef strError As String) As String
    Dim strCodart As String = ""
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Try
      strSQL = "SELECT caf_codart, caf_codarfo " & _
               " FROM codarfo INNER JOIN artico ON codarfo.codditt = artico.codditt AND codarfo.caf_codart = artico.ar_codart " & _
               " WHERE codarfo.codditt = " & CStrSQL(strDitta) & _
               " AND caf_codarfo = " & CStrSQL(strCodarfo) & " and caf_conto = " & lContoCli

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CODARFO")

      If dsTmp.Tables("CODARFO").Rows.Count = 0 Then
        strError = oApp.Tr(Me, 127792189506250000, "Codice art. del cliente/forn. inserito non presente o articolo associato al codice cliente inesistente ")
      Else
        strCodart = dsTmp.Tables("CODARFO").Rows(0)!caf_codart.ToString
      End If

      Return strCodart
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaCodartDescrLingua(ByVal strDitta As String, ByVal strCodart As String, ByVal nLingua As Integer, _
                                         ByRef strDescr As String, ByRef strDesint As String, ByRef strNote As String) As Boolean
    Dim strSQL As String = ""
    Try

      Dim dsTmp As DataSet

      If nLingua = 0 Then Return False

      strSQL = "SELECT ax_descr, ax_desint, ax_note FROM ARTVAL" & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND ax_codart = " & CStrSQL(strCodart) & _
            " AND ax_codvalu = " & nLingua
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTVAL")

      If dsTmp.Tables("ARTVAL").Rows.Count = 0 Then Return False

      strDescr = dsTmp.Tables("ARTVAL").Rows(0)!ax_descr.ToString
      strDesint = dsTmp.Tables("ARTVAL").Rows(0)!ax_desint.ToString
      strNote = dsTmp.Tables("ARTVAL").Rows(0)!ax_note.ToString

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function TrovaCodarfoDaCodart(ByVal strCodArt As String, ByVal lContoCli As Integer, _
                                       ByVal strDitta As String, ByVal bCodarfoDaBarcode As Boolean, _
                                       ByVal strBarcode As String) As String
    Dim strCodarfo As String = ""
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Try
      strSQL = "SELECT caf_codarfo FROM codarfo " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND caf_codart = " & CStrSQL(strCodArt) & _
               " AND caf_conto = " & lContoCli.ToString
      If bCodarfoDaBarcode Then strSQL = strSQL & " AND caf_desnote=" & CStrSQL(strBarcode)

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CODARFO")

      If dsTmp.Tables("CODARFO").Rows.Count > 0 Then
        strCodarfo = dsTmp.Tables("CODARFO").Rows(0)!caf_codarfo.ToString
      End If
      If strCodarfo = "" Then strCodarfo = " "

      Return strCodarfo
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaArticoUnMis(ByVal strDitta As String, ByVal strCodart As String) As String
    '--------------------------------------------
    'dato un articolo ne restituisce le unità di misura gestite
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim strOut As String = ""
    Dim dttTmp As New DataTable
    Dim dtrT As DataRow = Nothing
    Try

      If strCodart.ToUpper = "D" Or strCodart.ToUpper = "M" Then
        CaricaUmMis(strDitta, dttTmp)
        For Each dtrT In dttTmp.Rows
          strOut = strOut & dtrT!tb_codumis.ToString & "§"
        Next
        If strOut.Length > 0 Then strOut = strOut.Substring(0, strOut.Length - 1)
        dttTmp.Clear()
        Return strOut
      End If

      strSQL = "SELECT ar_unmis, ar_unmis2, ar_confez2, ar_um4 " & _
               " FROM artico WHERE codditt = " & CStrSQL(strDitta) & _
               " AND ar_codart = " & CStrSQL(strCodart)

      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CODARFO")

      If dsTmp.Tables("CODARFO").Rows.Count = 0 Then
        Throw (New NTSException(oApp.Tr(Me, 127791221367343750, "Codice articolo '|" & strCodart & "|' non presente nella ditta '|" & strDitta & "|'")))
      Else
        If dsTmp.Tables("CODARFO").Rows(0)!ar_unmis.ToString <> "" Then strOut = strOut & dsTmp.Tables("CODARFO").Rows(0)!ar_unmis.ToString & "§"
        If dsTmp.Tables("CODARFO").Rows(0)!ar_unmis2.ToString <> "" Then strOut = strOut & dsTmp.Tables("CODARFO").Rows(0)!ar_unmis2.ToString & "§"
        If dsTmp.Tables("CODARFO").Rows(0)!ar_confez2.ToString <> "" Then strOut = strOut & dsTmp.Tables("CODARFO").Rows(0)!ar_confez2.ToString & "§"
        If dsTmp.Tables("CODARFO").Rows(0)!ar_um4.ToString <> "" Then strOut = strOut & dsTmp.Tables("CODARFO").Rows(0)!ar_um4.ToString & "§"
        If strOut.Length > 0 Then strOut = strOut.Substring(0, strOut.Length - 1)
      End If

      Return strOut
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiCodpagaCltipb(ByVal strDitta As String, ByVal lConto As Integer, ByVal nTpbf As Integer) As Integer
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Try
      strSQL = "SELECT ctp_codpaga FROM clitipb" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND ctp_conto = " & lConto.ToString & " AND ctp_codtpbf = " & nTpbf.ToString
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "CLITPBF")
      If dsTmp.Tables("CLITPBF").Rows.Count > 0 Then
        Return NTSCInt(dsTmp.Tables("CLITPBF").Rows(0)!ctp_codpaga)
        dsTmp.Dispose()
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaUbicazionePerMagazzino(ByVal strDitta As String, ByVal strCodart As String, ByVal nMagaz As Integer) As String
    Dim nFase As Integer = 0
    Dim strSQL As String = ""
    Dim strOut As String = ""
    Dim dttTmp As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      If ValCodiceDb(strCodart, strDitta, "ARTICO", "S", "", dttTmp) Then nFase = NTSCInt(dttTmp.Rows(0)!ar_ultfase)
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT am_ubicaz FROM artmaga" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND am_codart = " & CStrSQL(strCodart) & _
               " AND am_codmaga = " & nMagaz.ToString & _
               " AND am_fase = " & nFase & _
               " ORDER BY codditt, am_codart, am_codmaga, am_fase"

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCStr(dttTmp.Rows(0)!am_ubicaz)
    Catch ex As Exception
      '--------------------------------------------------------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------------------------------------------------------
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  '--------------------------------------------------------------------------
  ' Legge la disponibilit e la disponibilit netta corrente per
  ' articolo/magazzino o articolo/magazzino/commessa e la scrive
  ' nelle label del corpo del documento
  '
  ' IN:   strCodart   Codice dell'articolo da trattare
  '       nMagaz      Codice magazzino da trattare
  '       nFase       Fase, se articolo a fasi
  '       lCommeca    Codice commessa da trattare
  '       strGescomm  Tipo gestione commessa articolo (N,O,S), opzionale
  '
  ' Utilizza le variabili esterne strFormatQtaEsistCorpo e
  ' bRilevaDisponibPerCommessa
  '--------------------------------------------------------------------------
  Public Overridable Sub LeggiDisponibilita(ByVal strDitta As String, ByVal strCodart As String, ByVal nMagaz As Integer, _
                              ByVal nFase As Integer, ByVal lCommeca As Integer, ByRef dDisponibilita As Decimal, _
                              ByRef dDisponibilitaNetta As Decimal, ByRef dUltCost As Decimal, _
                              ByVal bRilevaDisponibPerCommessa As Boolean, ByVal strDispMultiMag As String, _
                              ByVal strTestScominCome As String, Optional ByVal strGescomm As String = "?")
    Try
      LeggiDisponibilita(strDitta, strCodart, nMagaz, nFase, lCommeca, dDisponibilita, dDisponibilitaNetta, dUltCost, 0, 0, 0, bRilevaDisponibPerCommessa, strDispMultiMag, strTestScominCome, strGescomm)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub LeggiDisponibilita(ByVal strDitta As String, ByVal strCodart As String, ByVal nMagaz As Integer, _
                                ByVal nFase As Integer, ByVal lCommeca As Integer, ByRef dDisponibilita As Decimal, _
                                ByRef dDisponibilitaNetta As Decimal, ByRef dUltCost As Decimal, _
                                ByRef dOrdin As Decimal, ByRef dImpeg As Decimal, ByRef dPrenot As Decimal, _
                                ByVal bRilevaDisponibPerCommessa As Boolean, ByVal strDispMultiMag As String, _
                                ByVal strTestScominCome As String, Optional ByVal strGescomm As String = "?")


    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim dttTmp As DataTable = New DataTable

    Dim dEsist As Decimal = 0

    Dim bMovim As Boolean = False
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nMagaz, nFase, lCommeca, dDisponibilita, _
                                             dDisponibilitaNetta, dUltCost, dOrdin, dImpeg, dPrenot, _
                                             bRilevaDisponibPerCommessa, strDispMultiMag, strTestScominCome, _
                                             strGescomm})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dDisponibilita = NTSCDec(oIn(5))
        dDisponibilitaNetta = NTSCDec(oIn(6))
        dUltCost = NTSCDec(oIn(7))
        dOrdin = NTSCDec(oIn(8))
        dImpeg = NTSCDec(oIn(9))
        dPrenot = NTSCDec(oIn(10))
        Return
      End If
      '----------------

      '-------------------------------------
      'controllo i dati minimi indispensabili
      If strCodart.Trim = "" Or nMagaz = 0 Then Return

      '-------------------------------------
      'ultimo costo
      strSQL = "SELECT apx_ultcos FROM artprox" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND apx_codart = " & CStrSQL(strCodart)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPROX")
      If dsTmp.Tables("ARTPROX").Rows.Count > 0 Then dUltCost = NTSCDec(dsTmp.Tables("ARTPROX").Rows(0)!apx_ultcos)
      dsTmp.Tables.Clear()

      '-------------------------------------
      'Legge il tipo di gestione commessa dell'articolo se non passato
      If bRilevaDisponibPerCommessa And lCommeca > 0 And strGescomm = "?" Then
        strSQL = "SELECT ar_gescomm FROM artico" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND ar_codart = " & CStrSQL(strCodart)
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
        If dsTmp.Tables("ARTICO").Rows.Count > 0 Then strGescomm = dsTmp.Tables("ARTICO").Rows(0)!ar_gescomm.ToString
        dsTmp.Tables.Clear()
      End If

      '-------------------------------------
      'disponibilit e disponibilit netta
      If bRilevaDisponibPerCommessa And lCommeca > 0 And strGescomm = "S" Then
        'Legge le quantità per commessa
        dEsist = TrovaEsistDaXpro(strDitta, strCodart, nMagaz, lCommeca, 0, nFase, "", dOrdin, dImpeg, dPrenot)
        dDisponibilita = ArrDbl(dEsist + dOrdin - dImpeg, oApp.NDecQta)
        dDisponibilitaNetta = ArrDbl(dEsist - dPrenot, oApp.NDecQta)
      Else
        'Legge le quantità dai progressivi correnti
        If LeggiArtpro(strDitta, strCodart, nFase, nMagaz, strDispMultiMag, dttTmp) Then
          dEsist = NTSCDec(dttTmp.Rows(0)!Esistenza)
          dPrenot = NTSCDec(dttTmp.Rows(0)!Prenotato)
          dImpeg = NTSCDec(dttTmp.Rows(0)!Impegnato)
          dOrdin = NTSCDec(dttTmp.Rows(0)!Ordinato)

          'si riutilizza la stessa opz di registro della scorta minima visto che lavora assieme
          If strTestScominCome <> "A" And strDispMultiMag <> "N" Then
            dDisponibilita = ArrDbl(dEsist - dImpeg, oApp.NDecQta)
          Else
            'normale
            dDisponibilita = ArrDbl(dEsist + dOrdin - dImpeg, oApp.NDecQta)
          End If
          dDisponibilitaNetta = ArrDbl(dEsist - dPrenot, oApp.NDecQta)
        End If
        dsTmp.Clear()
        dsTmp.Dispose()

      End If    'If bRilevaDisponibPerCommessa And lCommeca > 0 And strGescomm = "S" Then

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function LeggiArtpro(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, _
                              ByVal nMagaz As Integer, ByVal strDispMultiMag As String, ByRef dttArtPro As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      'Legge le quantità dai progressivi correnti
      If strDispMultiMag = "N" Or strDispMultiMag.Trim() = "" Then
        strSQL = "SELECT ap_esist As Esistenza, ap_prenot As Prenotato, ap_impeg As Impegnato, ap_ordin As Ordinato" & _
          " FROM artpro" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ap_codart = " & CStrSQL(strCodart) & _
          " AND ap_magaz = " & nMagaz.ToString & _
          " AND ap_fase = " & nFase.ToString
      Else
        strSQL = "SELECT Sum(ap_esist) As Esistenza, Sum(ap_prenot) As Prenotato," & _
          " Sum(ap_impeg) As Impegnato, Sum(ap_ordin) As Ordinato" & _
          " FROM artpro" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ap_codart = " & CStrSQL(strCodart) & _
          " AND ap_fase = " & nFase & _
          " AND ap_magaz IN (" & strDispMultiMag.Trim.Replace(";", ",") & ")"
      End If    'If strDispMultiMag = "N" Or strDispMultiMag.Trim() = "" Then

      dttArtPro = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPRO").Tables(0)
      If dttArtPro.Rows.Count = 0 Then Return False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaEsistDaXpro(ByVal strDitta As String, ByVal strCodart As String, ByVal nCodmaga As Integer, _
                                   Optional ByVal lCommeca As Integer = 0, Optional ByVal lLotto As Integer = 0, _
                                   Optional ByVal nFase As Integer = 0, Optional ByVal strUbicaz As String = " ", _
                                   Optional ByRef dOrdin As Decimal = 0, Optional ByRef dImpeg As Decimal = 0, _
                                   Optional ByRef dPrenot As Decimal = 0) As Decimal
    '--- Passati articolo e magazzino (obbligatori)
    '--- e opzionalmnete commessa lotto fase e/o ubicazione,
    '--- restituisce la somma delle giacenze prelevate da LOTCPRO
    '--- (al limite da ARTPRO, se tutti i parametri sono omessi o sono a zero/blank)
    '--- opzionalmente ritorna anche le qta impegnate prenotate e ordinate
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim dEsist As Decimal = 0

    Try
      dOrdin = 0
      dImpeg = 0
      dPrenot = 0
      If strUbicaz = "" Then strUbicaz = " "

      '---------------------------------------------------------------------------------------
      'Se commessa, lotto e fase sono pari a 0 e ubicazione = blank preleva i dati da ARTPRO
      If lCommeca = 0 And lLotto = 0 And nFase = 0 And strUbicaz = " " Then
        strSQL = "SELECT ap_esist as Esist, ap_ordin as Ordin, ap_impeg as Impegn, ap_prenot as Prenot FROM artpro" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND ap_codart = " & CStrSQL(strCodart) & _
                " AND ap_magaz = " & nCodmaga.ToString
      Else
        strSQL = "SELECT Sum(lp_esist) As Esist, Sum(lp_ordin) As Ordin, " & _
                " Sum(lp_impeg) As Impegn, Sum(lp_prenot) As Prenot FROM lotcpro" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lp_codart = " & CStrSQL(strCodart) & _
                " AND lp_magaz = " & nCodmaga.ToString
        If lCommeca <> 0 Then strSQL = strSQL & " AND lp_commeca = " & lCommeca.ToString
        If lLotto <> 0 Then strSQL = strSQL & " AND lp_lotto = " & lLotto.ToString
        If nFase <> 0 Then strSQL = strSQL & " AND lp_fase = " & nFase.ToString
        If strUbicaz <> " " Then strSQL = strSQL & " AND lp_ubicaz = " & CStrSQL(strUbicaz)
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPRO")
      If dsTmp.Tables("ARTPRO").Rows.Count > 0 Then
        dEsist = ArrDbl(NTSCDec(dsTmp.Tables("ARTPRO").Rows(0)!Esist), oApp.NDecQta)
        dOrdin = ArrDbl(NTSCDec(dsTmp.Tables("ARTPRO").Rows(0)!Ordin), oApp.NDecQta)
        dImpeg = ArrDbl(NTSCDec(dsTmp.Tables("ARTPRO").Rows(0)!Impegn), oApp.NDecQta)
        dPrenot = ArrDbl(NTSCDec(dsTmp.Tables("ARTPRO").Rows(0)!Prenot), oApp.NDecQta)
      End If

      Return dEsist

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function
  Public Overridable Function TrovaEsistDaXproTC(ByVal strDitta As String, ByVal strCodart As String, ByVal nCodmaga As Integer, ByVal strTaglia As String, _
                                   Optional ByVal lCommeca As Integer = 0, Optional ByVal lLotto As Integer = 0, _
                                   Optional ByVal nFase As Integer = 0, Optional ByVal strUbicaz As String = " ", _
                                   Optional ByRef dOrdin As Decimal = 0, Optional ByRef dImpeg As Decimal = 0, _
                                   Optional ByRef dPrenot As Decimal = 0) As Decimal
    '--- Passati articolo e magazzino (obbligatori)
    '--- e opzionalmnete commessa lotto fase e/o ubicazione,
    '--- restituisce la somma delle giacenze prelevate da LOTCPRO
    '--- (al limite da ARTPRO, se tutti i parametri sono omessi o sono a zero/blank)
    '--- opzionalmente ritorna anche le qta impegnate prenotate e ordinate
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim dEsist As Decimal = 0
    Dim strSuffTc As String = ""
    Dim dttArt, dttTagl As New DataTable
    Try
      dOrdin = 0
      dImpeg = 0
      dPrenot = 0
      If strUbicaz = "" Then strUbicaz = " "

      'Cerca il codice della taglia
      ValCodiceDb(strCodart, strDitta, "ARTICO", "S", "", dttArt)
      If dttArt.Rows.Count = 0 Then Return 0

      ValCodiceDb(NTSCStr(dttArt.Rows(0)!ar_codtagl), strDitta, "TABTAGL", "N", "", dttTagl)

      For i As Integer = 1 To 24
        If NTSCStr(dttTagl.Rows(0)("tb_dest" & i.ToString("00"))) = strTaglia Then
          strSuffTc = i.ToString("00")
          Exit For
        End If
      Next

      '---------------------------------------------------------------------------------------
      'Se commessa, lotto e fase sono pari a 0 e ubicazione = blank preleva i dati da ARTPRO
      If lCommeca = 0 And lLotto = 0 And nFase = 0 And strUbicaz = " " Then
        strSQL = "SELECT apt_esist" & strSuffTc & " as Esist, apt_ordin" & strSuffTc & " as Ordin, " & _
                " apt_impeg" & strSuffTc & " as Impegn, apt_prenot" & strSuffTc & " as Prenot FROM artprotc" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND apt_codart = " & CStrSQL(strCodart) & _
                " AND apt_magaz = " & nCodmaga.ToString
      Else
        strSQL = "SELECT Sum(apt_esist" & strSuffTc & ") As Esist, Sum(apt_ordin" & strSuffTc & ") As Ordin, " & _
                " Sum(apt_impeg" & strSuffTc & ") As Impegn, Sum(apt_prenot" & strSuffTc & ") As Prenot FROM lotcpro" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND apt_codart = " & CStrSQL(strCodart) & _
                " AND apt_magaz = " & nCodmaga.ToString
        If lCommeca <> 0 Then strSQL = strSQL & " AND lp_commeca = " & lCommeca.ToString
        If lLotto <> 0 Then strSQL = strSQL & " AND lp_lotto = " & lLotto.ToString
        If nFase <> 0 Then strSQL = strSQL & " AND lp_fase = " & nFase.ToString
        If strUbicaz <> " " Then strSQL = strSQL & " AND lp_ubicaz = " & CStrSQL(strUbicaz)
      End If
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPRO")
      If dsTmp.Tables("ARTPRO").Rows.Count > 0 Then
        dEsist = ArrDbl(NTSCDec(dsTmp.Tables("ARTPRO").Rows(0)!Esist), oApp.NDecQta)
        dOrdin = ArrDbl(NTSCDec(dsTmp.Tables("ARTPRO").Rows(0)!Ordin), oApp.NDecQta)
        dImpeg = ArrDbl(NTSCDec(dsTmp.Tables("ARTPRO").Rows(0)!Impegn), oApp.NDecQta)
        dPrenot = ArrDbl(NTSCDec(dsTmp.Tables("ARTPRO").Rows(0)!Prenot), oApp.NDecQta)
      End If

      Return dEsist

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function

  Public Overridable Function CercaPrezzo(ByVal strDitta As String, ByVal strCodart As String, ByVal nCodlavo As Integer, _
                            ByVal lConto As Integer, ByVal nListino As Integer, ByVal strUnmisin As String, _
                            ByVal nFase As Integer, ByVal strTipoval As String, ByVal bConspromo As Boolean, _
                            ByVal nCodpromoin As Integer, ByVal lLottoin As Integer, ByVal dtDatdoc As DateTime, _
                            ByVal nCodvalu As Integer, ByVal dQuant As Decimal, _
                            ByRef dPrezzo As Decimal, ByRef dPrelist As Decimal, ByRef nCodpromo As Integer, _
                            ByRef strPrzNet As String, ByRef strTipoOut As String, _
                            Optional ByRef nPerqta As Integer = 1, _
                            Optional ByRef dDaQuant As Decimal = 0, Optional ByRef dAquant As Decimal = 9999999999, _
                            Optional ByRef dPerqta As Decimal = 1, Optional ByRef strUnmisout As String = "", _
                            Optional ByRef strTipovalout As String = "", Optional ByRef strCodCas As String = " ", _
                            Optional ByRef strErr As String = Nothing, Optional ByRef dtDatAggOut As Date = Nothing, _
                            Optional ByRef dtAdata As Date = Nothing, Optional ByRef lProgr As Integer = 0) As Boolean
    Try
      Return CercaPrezzo(strDitta, strCodart, nCodlavo, lConto, nListino, strUnmisin, nFase, strTipoval, bConspromo, _
                         nCodpromoin, lLottoin, dtDatdoc, nCodvalu, dQuant, dPrezzo, dPrelist, nCodpromo, _
                         strPrzNet, strTipoOut, nPerqta, dDaQuant, dAquant, dPerqta, strUnmisout, strTipovalout, strCodCas, _
                         strErr, dtDatAggOut, dtAdata, lProgr, False)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaPrezzo(ByVal strDitta As String, ByVal strCodart As String, ByVal nCodlavo As Integer, _
                             ByVal lConto As Integer, ByVal nListino As Integer, ByVal strUnmisin As String, _
                             ByVal nFase As Integer, ByVal strTipoval As String, ByVal bConspromo As Boolean, _
                             ByVal nCodpromoin As Integer, ByVal lLottoin As Integer, ByVal dtDatdoc As DateTime, _
                             ByVal nCodvalu As Integer, ByVal dQuant As Decimal, _
                             ByRef dPrezzo As Decimal, ByRef dPrelist As Decimal, ByRef nCodpromo As Integer, _
                             ByRef strPrzNet As String, ByRef strTipoOut As String, _
                             ByRef nPerqta As Integer, _
                             ByRef dDaQuant As Decimal, ByRef dAquant As Decimal, _
                             ByRef dPerqta As Decimal, ByRef strUnmisout As String, _
                             ByRef strTipovalout As String, ByRef strCodCas As String, _
                             ByRef strErr As String, ByRef dtDatAggOut As Date, _
                             ByRef dtAdata As Date, ByRef lProgr As Integer, ByVal bNoPrelist As Boolean) As Boolean
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nCodlavo, lConto, nListino, strUnmisin, _
                                             nFase, strTipoval, bConspromo, nCodpromoin, lLottoin, dtDatdoc, _
                                             nCodvalu, dQuant, dPrezzo, dPrelist, nCodpromo, strPrzNet, strTipoOut, _
                                             nPerqta, dDaQuant, dAquant, dPerqta, strUnmisout, strTipovalout, _
                                             strCodCas, strErr, dtDatAggOut, dtAdata, lProgr, bNoPrelist})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dPrezzo = NTSCDec(oIn(14))
        dPrelist = NTSCDec(oIn(15))
        nCodpromo = NTSCInt(oIn(16))
        strPrzNet = NTSCStr(oIn(17))
        strTipoOut = NTSCStr(oIn(18))
        nPerqta = NTSCInt(oIn(19))
        dDaQuant = NTSCDec(oIn(20))
        dAquant = NTSCDec(oIn(21))
        dPerqta = NTSCDec(oIn(22))
        strUnmisout = NTSCStr(oIn(23))
        strTipovalout = NTSCStr(oIn(24))
        strCodCas = NTSCStr(oIn(25))
        strErr = NTSCStr(oIn(26))
        dtDatAggOut = CDate(oIn(27))
        dtAdata = CDate(oIn(28))
        lProgr = NTSCInt(oIn(29))
        Return CBool(oOut)
      End If

      Return CercaPrezzo(strDitta, strCodart, nCodlavo, lConto, nListino, strUnmisin, nFase, strTipoval, bConspromo, _
                         nCodpromoin, lLottoin, dtDatdoc, nCodvalu, dQuant, dPrezzo, dPrelist, nCodpromo, _
                         strPrzNet, strTipoOut, nPerqta, dDaQuant, dAquant, dPerqta, strUnmisout, _
                         strTipovalout, strCodCas, strErr, dtDatAggOut, dtAdata, lProgr, bNoPrelist, _
                         0, "", Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaPrezzo(ByVal strDitta As String, ByVal strCodart As String, ByVal nCodlavo As Integer, _
                              ByVal lConto As Integer, ByVal nListino As Integer, ByVal strUnmisin As String, _
                              ByVal nFase As Integer, ByVal strTipoval As String, ByVal bConspromo As Boolean, _
                              ByVal nCodpromoin As Integer, ByVal lLottoin As Integer, ByVal dtDatdoc As DateTime, _
                              ByVal nCodvalu As Integer, ByVal dQuant As Decimal, _
                              ByRef dPrezzo As Decimal, ByRef dPrelist As Decimal, ByRef nCodpromo As Integer, _
                              ByRef strPrzNet As String, ByRef strTipoOut As String, _
                              ByRef nPerqta As Integer, _
                              ByRef dDaQuant As Decimal, ByRef dAquant As Decimal, _
                              ByRef dPerqta As Decimal, ByRef strUnmisout As String, _
                              ByRef strTipovalout As String, ByRef strCodCas As String, _
                              ByRef strErr As String, ByRef dtDatAggOut As Date, _
                              ByRef dtAdata As Date, ByRef lProgr As Integer, ByVal bNoPrelist As Boolean, _
                              ByVal lListaSel As Integer, ByVal strWhereArtico As String, ByRef dttListini As DataTable) As Boolean
    'CercaPrezzoEx3Ditt di Business
    Dim strSQL As String = ""
    Dim dsArtico As DataSet
    Dim dtrTmp As DataRow

    Dim strCoda As String = ""
    Dim strGesvar As String = "N"
    Dim strCodvar1 As String = ""
    Dim strCodroot As String = ""
    Dim strPrevar As String = "S"
    Dim strUnmis As String = ""
    Dim strConfez2 As String = ""
    Dim strUnMis2 As String = ""
    Dim dQtacon2 As Decimal = 0
    Dim dConver As Decimal = 0
    Dim dQuantabs As Decimal = 0
    Dim dArperqta As Decimal = 0
    Dim strSQLUM As String = ""
    Dim snaTmp As New DataTable
    Dim i As Integer

    Dim bRetail As Boolean = False

    '----------
    Dim bRicercaPrezzoMultipla As Boolean = False
    Dim dsTableInMem As DataSet = Nothing
    Dim nFaseArt As Integer = 0

    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nCodlavo, lConto, nListino, strUnmisin, _
                                             nFase, strTipoval, bConspromo, nCodpromoin, lLottoin, dtDatdoc, _
                                             nCodvalu, dQuant, dPrezzo, dPrelist, nCodpromo, strPrzNet, strTipoOut, _
                                             nPerqta, dDaQuant, dAquant, dPerqta, strUnmisout, strTipovalout, _
                                             strCodCas, strErr, dtDatAggOut, dtAdata, lProgr, bNoPrelist, lListaSel, strWhereArtico, dttListini})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dPrezzo = NTSCDec(oIn(14))
        dPrelist = NTSCDec(oIn(15))
        nCodpromo = NTSCInt(oIn(16))
        strPrzNet = NTSCStr(oIn(17))
        strTipoOut = NTSCStr(oIn(18))
        nPerqta = NTSCInt(oIn(19))
        dDaQuant = NTSCDec(oIn(20))
        dAquant = NTSCDec(oIn(21))
        dPerqta = NTSCDec(oIn(22))
        strUnmisout = NTSCStr(oIn(23))
        strTipovalout = NTSCStr(oIn(24))
        strCodCas = NTSCStr(oIn(25))
        strErr = NTSCStr(oIn(26))
        dtDatAggOut = CDate(oIn(27))
        dtAdata = CDate(oIn(28))
        lProgr = NTSCInt(oIn(29))
        dttListini = CType(oIn(33), DataTable)
        Return CBool(oOut)
      End If
      '----------------

      dQuantabs = Math.Abs(dQuant)
      If dQuantabs = 0 Then dQuantabs = 1
      If dConver = 0 Then dConver = 1
      nPerqta = CLN__STD.bsPrSetnoqta
      'C'è una opzione di registro specifica del retail che va chiamata solo quando il cod. promo è 0.
      If nCodpromoin = -1 Then nCodpromoin = 0 : bRetail = True

      '------------------------------
      'Azzera risultati
      dPrezzo = 0
      dPrelist = 0
      nCodpromo = 0
      dDaQuant = 0
      dAquant = 9999999999
      strPrzNet = "N"
      strUnmisout = strUnmisin
      strTipovalout = strTipoval
      dtDatAggOut = NTSCDate(IntSetDate("01/01/1900")) ''in questo parametro opzionale esce la DATA INIZIO VALIDITA' del listino letto (ossia lc_datagg)
      dtAdata = NTSCDate(IntSetDate("01/01/1900"))

      If strWhereArtico.Trim <> "" Then
        bRicercaPrezzoMultipla = True

        dttListini = New DataTable

        dttListini.Columns.Add("strCodart", GetType(String))
        dttListini.Columns.Add("nFase", GetType(Integer))
        dttListini.Columns.Add("dPrezzo", GetType(Decimal))
        dttListini.Columns.Add("dPrelist", GetType(Decimal))
        dttListini.Columns.Add("nCodpromo", GetType(Integer))
        dttListini.Columns.Add("strPrzNet", GetType(String))
        dttListini.Columns.Add("strTipoOut", GetType(String))
        dttListini.Columns.Add("nPerqta", GetType(Integer))
        dttListini.Columns.Add("dDaQuant", GetType(Decimal))
        dttListini.Columns.Add("dAquant", GetType(Decimal))
        dttListini.Columns.Add("dPerqta", GetType(Decimal))
        dttListini.Columns.Add("strUnmisout", GetType(String))
        dttListini.Columns.Add("strTipovalout", GetType(String))
        dttListini.Columns.Add("strCodCas", GetType(String))
        dttListini.Columns.Add("strErr", GetType(String))
        dttListini.Columns.Add("dtDatAggOut", GetType(Date))
        dttListini.Columns.Add("dtAdata", GetType(Date))
        dttListini.Columns.Add("lProgr", GetType(Integer))
      End If

      '-----------------------------
      'Se abilitata l'opzione gbRilevaPrezziDaContoFatt legge il conto di fatturazione
      If oApp.oGvar.bRilevaPrezziDaContoFatt And lConto > 0 Then
        strSQL = "SELECT an_contfatt FROM anagra WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & lConto.ToString
        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If snaTmp.Rows.Count > 0 Then
          If NTSCInt(snaTmp.Rows(0)!an_contfatt) > 0 Then lConto = NTSCInt(snaTmp.Rows(0)!an_contfatt) 'trovato: lo usa al posto del conto passato alla funzione
        End If
        snaTmp.Clear()
      End If

      '------------------------------
      'verifico se l'articolo  gestito a varianti e ottengo le varie unità di misura 
      If bRicercaPrezzoMultipla Then
        If lListaSel <> 0 Then strWhereArtico &= " AND lsa_codlsar = " & lListaSel

        strSQL = "SELECT DISTINCT ar_codart, ar_gesvar, ar_prevar, ar_codroot, ar_codvar1, ar_perqta, ar_gesfasi, " & _
                 " CASE WHEN ar_gesfasi = 'S' THEN af_fase ELSE ar_ultfase END As Fase, ar_unmis, " & _
                 " ar_confez2, ar_qtacon2, ar_unmis2, ar_conver FROM artico" & _
                 " LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart "

        If lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

        strSQL &= " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                  strWhereArtico
      Else
        strSQL = "SELECT ar_gesvar, ar_prevar, ar_codroot, ar_codvar1, ar_perqta, ar_gesfasi, ar_unmis, " & _
                 "ar_confez2, ar_qtacon2, ar_unmis2, ar_conver FROM artico" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND ar_codart = " & CStrSQL(strCodart)
      End If

      dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

      '----------- Prepara i dati in memoria per limitare il numero di query sul DB
      If bRicercaPrezzoMultipla Then
        If nListino >= 0 Then PreparaDatatableListini(strDitta, nListino, lConto, nCodlavo, dtDatdoc, dQuantabs, strCodCas, lListaSel, nCodvalu, strWhereArtico, dsTableInMem)

        If nListino <= 0 And nListino > -3 Then PreparaDatatableArtprox(strDitta, lListaSel, strWhereArtico, dsTableInMem)

        If nListino = -3 Then PreparaDatatablePrezzoFifo(strDitta, lListaSel, strWhereArtico, dtDatdoc.ToShortDateString, dsTableInMem)

        If nListino = -4 Then PreparaDatatableCostoMedioPond(strDitta, lListaSel, strWhereArtico, dsTableInMem)
      End If

      For i = 0 To dsArtico.Tables("ARTICO").Rows.Count - 1
        dPrezzo = 0
        dPrelist = 0
        nCodpromo = 0
        strPrzNet = "N"
        strTipoOut = " "
        nPerqta = CLN__STD.bsPrSetnoqta
        dDaQuant = 0
        dAquant = 999999999
        dPerqta = 0
        strUnmisout = " "
        strTipovalout = " "
        If strCodCas = "" Then strCodCas = " "
        strErr = ""
        dtDatAggOut = New Date(1900, 1, 1)
        dtAdata = New Date(1900, 1, 1)
        lProgr = 0

        dtrTmp = dsArtico.Tables("ARTICO").Rows(i)
        strGesvar = dtrTmp!ar_gesvar.ToString
        strPrevar = dtrTmp!ar_prevar.ToString
        strCodroot = dtrTmp!ar_codroot.ToString
        strCodvar1 = dtrTmp!ar_codvar1.ToString
        dArperqta = NTSCDec(dtrTmp!ar_perqta)

        If bRicercaPrezzoMultipla Then
          nFaseArt = NTSCInt(dtrTmp!fase)
          strCodart = NTSCStr(dtrTmp!ar_codart)
        Else
          nFaseArt = nFase
        End If

        If strGesvar = "S" Then ' se  a varianti cambia la base di calcolo ..
          Select Case strPrevar
            Case "N" : strCoda = strCodroot
            Case "S" : strCoda = strCodart
            Case "1" : strCoda = strCodroot & strCodvar1
          End Select
        Else
          strCoda = strCodart
        End If

        If dtrTmp!ar_gesfasi.ToString = "N" Then nFaseArt = 0
        '-------
        strUnmis = dtrTmp!ar_unmis.ToString
        strConfez2 = dtrTmp!ar_confez2.ToString
        dQtacon2 = NTSCDec(dtrTmp!ar_qtacon2)
        strUnMis2 = dtrTmp!ar_unmis2.ToString
        dConver = NTSCDec(dtrTmp!ar_conver)

        '------------------------------
        strSQLUM = ""
        If strUnmisin.Trim = "" Then
          strSQLUM = " And (lc_unmis = ' ' Or lc_unmis = " & CStrSQL(strUnmis) & ")"
        Else
          strSQLUM = " And lc_unmis = " & CStrSQL(strUnmisin)
        End If

        '------------------------------
        'Cerca il prezzo di listino nella valuta zero (lire od euro...)(prelist  sempre relativo alla valuta zero!!)
        ' da memorizzare in movmag/movord per statistiche sugli sconti medi applicati
        If Not CercaPrezzo_PreList(strDitta, dsTableInMem, bNoPrelist, nListino, strCoda, nCodlavo, nFaseArt, dtDatdoc, dQuantabs, strCodCas, _
                                   strSQLUM, nPerqta, dPrelist, lProgr, strTipoOut) Then Continue For
        '------------------------------
        ' ora iniza la ricerca del prezzo vero e proprio (SPECIFICO E/O GENERICO)
        ' cerca il prezzo richiesto o ultimo costo (ma nella valuta indicata!!!)
        ' per quel conto , se esiste
        'If nListino > 0 Or lConto > 0 Then 'in questo modo se chiedo un listino minore di 0 (es costo medio) non deve cercare il prezzo spec. per fornitore!!!!
        If Not CercaPrezzo_Listino(strDitta, dsTableInMem, bRetail, nListino, strCoda, nCodlavo, lConto, nCodvalu, dtDatdoc, _
                                   dQuantabs, nFaseArt, strCodCas, bConspromo, nCodpromoin, strSQLUM, strTipoval, dPrezzo, _
                                   nCodpromo, strPrzNet, strTipovalout, nPerqta, dDaQuant, dAquant, dPerqta, strUnmisout, _
                                   dtDatAggOut, dtAdata, lProgr, strTipoOut) Then

          If bRicercaPrezzoMultipla Then
            dttListini.Rows.Add()
            With dttListini.Rows(dttListini.Rows.Count - 1)
              !strCodart = strCodart
              !nFase = nFaseArt
              !dPrezzo = dPrezzo
              !dPrelist = dPrelist
              !nCodpromo = nCodpromo
              !strPrzNet = strPrzNet
              !strTipoOut = strTipoOut
              !nPerqta = nPerqta
              !dDaQuant = dDaQuant
              !dAquant = dAquant
              !dPerqta = dPerqta
              !strUnmisout = strUnmisout
              !strTipovalout = strTipovalout
              !strCodCas = strCodCas
              !strErr = strErr
              !dtDatAggOut = dtDatAggOut
              !dtAdata = dtAdata
              !lProgr = lProgr
            End With
          End If
          Continue For
        End If

        'Listino < 0 (ultimo costo, costo medio, ecc...)
        If Not CercaPrezzo_Costi(strDitta, dsTableInMem, nListino, strCodart, nFaseArt, strUnmisin, strConfez2, strUnMis2, dQtacon2, _
                                 dConver, dArperqta, nCodvalu, dtDatdoc, dQuant, dPrezzo, strErr) Then Continue For

        '------------------------------
        ' adesso se cercavo un prezzo in valuta listino > 0 e non l'ho trovato,
        ' cerca ev. listino in lire/euro corrispondente e lo trasformo, se lo trovo, in valuta
        If Not CercaPrezzo_Valuta(strDitta, dsTableInMem, nListino, nCodvalu, strCoda, nCodlavo, dtDatdoc, dQuantabs, nFaseArt, _
                                  strCodCas, bConspromo, nCodpromoin, strSQLUM, dPrezzo, nCodpromo, strPrzNet, nPerqta, _
                                  dDaQuant, dAquant, dPerqta, strUnmisout, dtDatAggOut, dtAdata, lProgr, strTipoOut) Then Continue For


        If bRicercaPrezzoMultipla Then
          dttListini.Rows.Add()
          With dttListini.Rows(dttListini.Rows.Count - 1)
            !strCodart = strCodart
            !nFase = nFaseArt
            !dPrezzo = dPrezzo
            !dPrelist = dPrelist
            !nCodpromo = nCodpromo
            !strPrzNet = strPrzNet
            !strTipoOut = strTipoOut
            !nPerqta = nPerqta
            !dDaQuant = dDaQuant
            !dAquant = dAquant
            !dPerqta = dPerqta
            !strUnmisout = strUnmisout
            !strTipovalout = strTipovalout
            !strCodCas = strCodCas
            !strErr = strErr
            !dtDatAggOut = dtDatAggOut
            !dtAdata = dtAdata
            !lProgr = lProgr
          End With
        End If
      Next

      If bRicercaPrezzoMultipla Then dttListini.PrimaryKey = New System.Data.DataColumn() {dttListini.Columns("strCodart"), dttListini.Columns("nFase")}

      dsArtico.Tables.Clear()
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function PreparaDatatableListini(ByVal strDitta As String, ByVal nListino As Integer, ByVal lConto As Integer, ByVal nCodlavo As Integer, _
                                                      ByVal dtDatdoc As Date, ByVal dQuantabs As Decimal, ByVal strCodcas As String, ByVal lListaSel As Integer, _
                                                      ByVal nCodvalu As Integer, ByVal strWhereArtico As String, ByRef dsTableInMem As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DISTINCT lc_progr, lc_codart, lc_conto, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_tipo, lc_prezzo, lc_datscad, " & _
               " lc_daquant, lc_aquant, lc_perqta, lc_unmis, lc_note, lc_netto, lc_fase, lc_ultagg, lc_codlavo" & _
               " FROM busvw_listini " & _
               " INNER JOIN artico ON artico.codditt = busvw_listini.codditt AND artico.ar_codart = busvw_listini.ar_codart "
      'La ricerca la fa più ampia di quanto necessario per prendere anche i listini collegati ad articoli a variante con prezzi su codice root o sulla prima variante

      If lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

      strSQL &= " WHERE busvw_listini.codditt = " & CStrSQL(strDitta) & _
                " AND lc_listino IN (0, " & nListino & ")" & _
                " AND lc_conto IN (0, " & lConto & ")" & _
                " AND lc_codlavo = " & nCodlavo.ToString & _
                " AND lc_codvalu IN (0, " & nCodvalu & ")" & _
                " AND " & CDataSQL(dtDatdoc.ToShortDateString) & " BETWEEN lc_datagg AND lc_datscad " & _
                " AND " & CDblSQL(dQuantabs) & " BETWEEN lc_daquant AND lc_aquant " & _
                " AND lc_codcas = " & CStrSQL(strCodcas) & _
                strWhereArtico.Replace("ar_codart", "artico.ar_codart")

      dsTableInMem = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

      With dsTableInMem.Tables("LISTINI")
        .PrimaryKey = New DataColumn() {.Columns("lc_codart"), .Columns("lc_conto"), .Columns("lc_coddest"), _
                                .Columns("lc_listino"), .Columns("lc_codvalu"), .Columns("lc_codtpro"), _
                                .Columns("lc_fase"), .Columns("lc_codlavo"), .Columns("lc_datagg"), _
                                .Columns("lc_daquant"), .Columns("lc_progr")}
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function PreparaDatatableArtprox(ByVal strDitta As String, ByVal lListaSel As Integer, ByVal strWhereArtico As String, _
                                                      ByRef dsTableInMem As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT artprox.* FROM artprox" & _
               " INNER JOIN artico ON artico.codditt = artprox.codditt AND ar_codart = apx_codart "

      If lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

      strSQL &= " WHERE artprox.codditt = " & CStrSQL(strDitta) & _
                strWhereArtico

      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPROX", dsTableInMem)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function PreparaDatatablePrezzoFifo(ByVal strDitta As String, ByVal lListaSel As Integer, ByVal strWhereArtico As String, _
                                                         ByVal dtData As String, ByRef dsTableInMem As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      '-------------------------------------------
      ' Creo il secondo recordset che mi restituisce il valore della giacenza ad oggi
      strSQL = "SELECT ap_codart, ap_fase, Sum(artpro.ap_esist) AS somma" & _
              " FROM tabmaga " & _
              " INNER JOIN ARTPRO ON tabmaga.tb_codmaga = ARTPRO.ap_magaz and tabmaga.codditt = artpro.codditt " & _
              " INNER JOIN artico ON artico.codditt = artpro.codditt AND ar_codart = ap_codart "

      If lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

      strSQL &= " WHERE artpro.codditt = " & CStrSQL(strDitta) & _
                strWhereArtico & _
                "GROUP BY ap_codart, ap_fase"

      dsTableInMem = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPRO")

      '-------------------------------------------
      'giacenza di materiale a magazzino
      ' se calcolo a data , cerco l'esistenza alla data indicata
      If dtData <> "" Then
        strSQL = "SELECT km_codart, km_fase, Sum(mm_quant*km_carscar) AS diff " & _
                 " FROM tabmaga INNER JOIN (movmag INNER JOIN keymag ON (movmag.mm_riga = keymag.km_riga)" & _
                 " AND (movmag.mm_tipork = keymag.km_tipork) AND (movmag.mm_serie = keymag.km_serie)" & _
                 " AND (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_numdoc = keymag.km_numdoc) AND " & _
                 " (movmag.codditt = keymag.codditt))" & _
                 " ON tabmaga.tb_codmaga = keymag.km_magaz and tabmaga.codditt = keymag.codditt " & _
                 " INNER JOIN artico ON artico.codditt = movmag.codditt AND ar_codart = mm_codart "

        If lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

        strSQL &= " WHERE km_aammgg > " & CDataSQL(dtData.ToString) & _
                  " AND keymag.codditt = " & CStrSQL(strDitta) & _
                  strWhereArtico & _
                  "GROUP BY km_codart, km_fase"

        dsTableInMem = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaMovm")
      End If

      '-------------------------------------------
      ' Creo il  recordset dei carichi valorizzanti ..
      strSQL = " SELECT keymag.km_magaz, keymag.km_carscar, keymag.km_aammgg, movmag.mm_codart, km_codart, km_fase, movmag.mm_quant, " & _
               " movmag.mm_perqta, "
      If oApp.oGvar.bUltSpeseInValoriFifo Then strSQL += " movmag.mm_numpex + " 'ulteriori spese di piede indicate in veboll
      If oApp.oGvar.bOneriAccInValoriFifo Then
        strSQL += " movmag.mm_valore + movmag.mm_numpac as valore "
      Else
        strSQL += " movmag.mm_valore as valore "
      End If
      strSQL += " FROM (movmag INNER JOIN keymag ON (movmag.mm_riga = keymag.km_riga) AND " & _
                " (movmag.mm_tipork = keymag.km_tipork) And  (movmag.mm_serie = keymag.km_serie) AND " & _
                " (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_numdoc = keymag.km_numdoc) AND " & _
                " (movmag.codditt = keymag.codditt)) INNER JOIN tabcaum on keymag.km_causale = tabcaum.tb_codcaum " & _
                " INNER JOIN artico ON artico.codditt = keymag.codditt AND ar_codart = km_codart "

      If lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

      strSQL &= " WHERE tabcaum.tb_valoriz <> 0 " & _
                " and tabcaum.tb_vvaloriz <> 0 " & _
                " and keymag.codditt = " & CStrSQL(strDitta) & _
                " AND keymag.km_carscar > 0 " & strWhereArtico
      If Not dtData = "" Then strSQL = strSQL & " AND km_aammgg <= " & CDataSQL(dtData.ToString)
      '-------------------------------------------
      'controllo il codice di magazzino e se  = 0 seleziono tutti i record
      ' NO !! (sempre tutti, solo i carichi valorizzanti indipendenti dal magazzino !)
      'If nCodmaga <> 0 Then
      '   strSQL = strSQL & " AND keymag.km_magaz = " & nCodmaga & ""
      'End If
      strSQL = strSQL & " ORDER BY keymag.km_aammgg DESC, keymag.km_numdoc DESC, keymag.km_riga DESC " 'la routine 'CercaPrezzoFifo' poteva restituire dei risultati diversi rispetto alla versione in vb6 se all'interno dei documenti di carico lo stesso articolo veniva indicato più volte con prezzi diversi

      dsTableInMem = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaFifo")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function PreparaDatatableCostoMedioPond(ByVal strDitta As String, ByVal lListaSel As Integer, ByVal strWhereArtico As String, ByRef dsTableInMem As DataSet) As Boolean
    'Se nCodmaga > 0, allora solo magazzino nCodmaga
    'Se nCodmaga = 0, allora tutti i magazzini
    'Se nCodmaga = -1, allora magazzini merce propria
    'Se nCodmaga = -2, allora magazzini merce altrui
    Dim strSQL As String = ""
    Dim dtUlap As Date
    Dim nCodmaga As Integer = oApp.oGvar.nCostoMedioPondTipoMagaz
    Try

      '------------------------------------
      If oApp.oGvar.bCostoMedioPondNoStorico Then
        'Legge la data ultimo aggiornamento progressivi
        strSQL = "SELECT tb_dtulap FROM tabanaz WHERE codditt = " & CStrSQL(strDitta)
        Dim dttTmp As DataTable = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

        If dttTmp.Rows.Count > 0 Then
          dtUlap = NTSCDate(dttTmp.Rows(0)!tb_dtulap)
        End If
        dttTmp.Clear()
      Else
        dtUlap = NTSCDate(IntSetDate("01/01/1900"))
      End If

      '-----------------------------------
      'Creo il recordset che mi restituir il valore della giacenza ad oggi
      strSQL = "SELECT ap_codart, ap_fase, Sum(artpro.ap_esist) AS somma" & _
              " FROM tabmaga " & _
              " INNER JOIN artpro ON tabmaga.codditt = artpro.codditt AND tabmaga.tb_codmaga = artpro.ap_magaz" & _
              " INNER JOIN artico ON artico.codditt = artpro.codditt AND ar_codart = ap_codart "

      If lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

      strSQL &= " WHERE artpro.codditt = " & CStrSQL(strDitta) & strWhereArtico

      If nCodmaga = -1 Then
        strSQL = strSQL & " AND (tabmaga.tb_flclavo =' ' OR tabmaga.tb_flclavo ='F')"
      ElseIf nCodmaga = -2 Then
        strSQL = strSQL & " AND (tabmaga.tb_flclavo ='C' OR tabmaga.tb_flclavo ='X')"
      ElseIf nCodmaga > 0 Then
        strSQL = strSQL & " and artpro.ap_magaz = " & nCodmaga
      End If

      strSQL &= " GROUP BY ap_codart, ap_fase"

      dsTableInMem = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MAGAPRO")

      '------------------------------------
      'Creo il recordset dei carichi valorizzanti
      strSQL = "SELECT km_codart, km_fase, keymag.km_aammgg, Sum(movmag.mm_quant * tabcaum.tb_valoriz) AS Quant, " & _
               " Sum(movmag.mm_valore * tabcaum.tb_valoriz) AS Valore" & _
               " FROM tabmaga INNER JOIN (tabcaum INNER JOIN (movmag INNER JOIN keymag ON " & _
               " (movmag.codditt = keymag.codditt) AND (movmag.mm_riga = keymag.km_riga) AND " & _
               " (movmag.mm_tipork = keymag.km_tipork) AND (movmag.mm_serie = keymag.km_serie) AND " & _
               " (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_numdoc = keymag.km_numdoc)) ON " & _
               " tabcaum.tb_codcaum = keymag.km_causale) ON tabmaga.codditt = keymag.codditt AND " & _
               " tabmaga.tb_codmaga = keymag.km_magaz " & _
               " INNER JOIN artico ON artico.codditt = keymag.codditt AND ar_codart = km_codart "

      If lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

      strSQL &= " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
                " AND tabcaum.tb_valoriz <> 0" & _
                " AND tabcaum.tb_vvaloriz <> 0 " & strWhereArtico

      If nCodmaga = -1 Then
        strSQL = strSQL & " AND (tabmaga.tb_flclavo = ' ' OR tabmaga.tb_flclavo = 'F') "
      ElseIf nCodmaga = -2 Then
        strSQL = strSQL & " AND (tabmaga.tb_flclavo = 'C' OR tabmaga.tb_flclavo = 'X') "
      ElseIf nCodmaga > 0 Then
        strSQL = strSQL & " AND keymag.km_magaz = " & nCodmaga & " "
      End If
      If oApp.oGvar.bCostoMedioPondNoStorico Then
        strSQL = strSQL & "AND keymag.km_aammgg > " & CDataSQL(dtUlap.ToShortDateString)
      End If
      strSQL = strSQL & " GROUP BY keymag.km_codart, keymag.km_fase, keymag.km_aammgg  "
      strSQL = strSQL & " ORDER BY keymag.km_aammgg DESC "

      dsTableInMem = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPROX")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaPrezzo_PreList(ByVal strDitta As String, ByVal dsTableInMem As DataSet, ByVal bNoPrelist As Boolean, ByVal nListino As Integer, _
                                                  ByVal strCoda As String, ByVal nCodlavo As Integer, ByVal nFase As Integer, ByVal dtDatdoc As Date, _
                                                  ByVal dQuantabs As Decimal, ByVal strCodCas As String, ByVal strSQLUM As String, ByRef nPerqta As Integer, _
                                                  ByRef dPrelist As Decimal, ByRef lProgr As Integer, ByRef strTipoOut As String) As Boolean
    Dim strSQL As String = ""
    Dim dtrList As DataRow = Nothing
    Dim strOrderBy As String
    Try
      '------------------------------
      'Cerca il prezzo di listino nella valuta zero (lire od euro...)(prelist  sempre relativo alla valuta zero!!)
      ' da memorizzare in movmag/movord per statistiche sugli sconti medi applicati
      If bNoPrelist = False Then
        If nListino > 0 Then
          strOrderBy = "lc_datagg DESC, lc_daquant"


          If Not dsTableInMem Is Nothing Then
            'Se ho la tabella in memoria, la query viene fatta sul datatable
            Dim dtrSel() As DataRow = dsTableInMem.Tables("LISTINI").Select(" lc_codart = " & CStrSQL(strCoda) & " AND lc_conto = 0" & _
                                                           " AND lc_listino = " & nListino.ToString & " AND lc_codvalu = 0 AND lc_codtpro = 0" & _
                                                           " AND lc_fase = " & nFase.ToString & strSQLUM, strOrderBy)

            If dtrSel.Length > 0 Then dtrList = dtrSel(0)
          Else
            'Non ho la tabella in memoria, la query viene fatta sul database
            strSQL = "SELECT TOP 1 lc_progr, lc_prezzo, lc_daquant, lc_aquant, lc_tipo FROM listini" & _
                     " WHERE lc_codart = " & CStrSQL(strCoda) & _
                     " AND lc_conto = 0" & _
                     " AND lc_codlavo = " & nCodlavo.ToString & _
                     " AND lc_listino = " & nListino.ToString & _
                     " AND lc_codvalu = 0" & _
                     " AND lc_codtpro = 0" & _
                     " AND " & CDataSQL(dtDatdoc.ToShortDateString) & " BETWEEN lc_datagg AND lc_datscad " & _
                     " AND " & CDblSQL(dQuantabs) & " BETWEEN lc_daquant AND lc_aquant " & _
                     " AND lc_fase = " & nFase.ToString & _
                     " AND lc_codcas = " & CStrSQL(strCodCas) & _
                     " AND codditt = " & CStrSQL(strDitta) & _
                     strSQLUM & _
                     " ORDER BY " & strOrderBy

            Dim dsTmp As DataSet = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

            If dsTmp.Tables("LISTINI").Rows.Count > 0 Then dtrList = dsTmp.Tables("LISTINI").Rows(0)
          End If

          If Not dtrList Is Nothing Then
            If Not (NTSCDec(dtrList!lc_daquant) = 0 And _
                    NTSCDec(dtrList!lc_aquant) = 9999999999) Then
              nPerqta = CLN__STD.bsPrSetsiqta
            End If
            dPrelist = NTSCDec(dtrList!lc_prezzo)
            lProgr = NTSCInt(dtrList!lc_progr)
            strTipoOut = NTSCStr(dtrList!lc_tipo)
          End If
        End If    'If nListino > 0 Then
      End If    'If bNoPrelist = False Then

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaPrezzo_Listino(ByVal strDitta As String, ByVal dsTableInMem As DataSet, ByVal bRetail As Boolean, ByVal nListino As Integer, _
                                                  ByVal strCoda As String, ByVal nCodlavo As Integer, ByVal lConto As Integer, ByVal nCodvalu As Integer, ByVal dtDatDoc As Date, _
                                                  ByVal dQuantAbs As Decimal, ByVal nFase As Integer, ByVal strCodCas As String, ByVal bConspromo As Boolean, _
                                                  ByVal nCodpromoin As Integer, ByVal strSQLUM As String, ByVal strTipoval As String, ByRef dPrezzo As Decimal, _
                                                  ByRef nCodpromo As Integer, ByRef strPrzNet As String, ByRef strTipovalout As String, ByRef nPerQta As Integer, _
                                                  ByRef dDaQuant As Decimal, ByRef dAquant As Decimal, ByRef dPerqta As Decimal, ByRef strUnmisout As String, _
                                                  ByRef dtDatAggOut As Date, ByRef dtAdata As Date, ByRef lProgr As Integer, ByRef strTipoOut As String) As Boolean
    '------------------------------
    ' ora iniza la ricerca del prezzo vero e proprio (SPECIFICO E/O GENERICO)
    ' cerca il prezzo richiesto o ultimo costo (ma nella valuta indicata!!!)
    ' per quel conto , se esiste
    'If nListino > 0 Or lConto > 0 Then 'in questo modo se chiedo un listino minore di 0 (es costo medio) non deve cercare il prezzo spec. per fornitore!!!!
    Dim strSQL As String = ""
    Dim dttResult As New DataTable
    Dim dtrList() As DataRow = Nothing
    Dim strWhere, strOrderBy As String
    Try
      If nListino >= 0 Then
        'Prepara la where e la order by comune
        strWhere = " lc_codart = " & CStrSQL(strCoda) & _
                   " AND ((lc_conto = " & lConto & " AND lc_listino = 0 ) " & _
                   "       OR  " & _
                   "      (lc_conto = 0 AND lc_listino = " & nListino & ")) " & _
                   " AND lc_codvalu = " & nCodvalu & _
                   " AND lc_fase = " & nFase

        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bRetail Or (bRetail And CBool(GetSettingBus("BSREGSRE", "OPZIONI", ".", "Applica_anche_prom_come_Rel10", "0", ".", "0"))) Then
            If Not bConspromo Then
              strWhere &= " AND lc_codtpro = 0"
            Else
              If nCodpromoin > 0 Then
                strWhere &= " AND lc_codtpro = " & nCodpromoin
              End If
            End If
          Else
            strWhere &= " AND lc_codtpro = 0"
          End If
        Else
          strWhere &= " AND (lc_codtpro = " & nCodpromoin & " OR lc_codtpro = 0)"
        End If
        strWhere &= strSQLUM

        strOrderBy = "lc_conto DESC, lc_codtpro DESC, lc_datagg DESC, lc_daquant"

        If Not dsTableInMem Is Nothing Then
          'Se ho la tabella in memoria, la query viene fatta sul datatable
          Dim dtrSel() As DataRow = dsTableInMem.Tables("LISTINI").Select(strWhere, strOrderBy)

          If dtrSel.Length > 0 Then
            'Le righe selezionate le copio in un nuovo datatable per poter fare una nuova select
            dttResult = dsTableInMem.Tables("LISTINI").Clone
            For Each dtrTmp As DataRow In dtrSel
              dttResult.Rows.Add(dtrTmp.ItemArray)
            Next
          End If
        Else
          'Non ho la tabella in memoria, la query viene fatta sul database
          strSQL = "SELECT lc_prezzo, lc_codtpro, lc_daquant, lc_aquant, lc_perqta," & _
                   " lc_unmis, lc_netto, lc_conto, lc_datagg, lc_datscad, lc_progr, lc_tipo FROM listini" & _
                   " WHERE " & strWhere & _
                   " AND codditt = " & CStrSQL(strDitta) & _
                   " AND " & CDataSQL(dtDatDoc.ToShortDateString) & " BETWEEN lc_datagg AND lc_datscad " & _
                   " AND " & CDblSQL(dQuantAbs) & " BETWEEN lc_daquant AND lc_aquant " & _
                   " AND lc_codcas = " & CStrSQL(strCodCas) & _
                   " AND lc_codlavo = " & nCodlavo & _
                   " ORDER BY " & strOrderBy

          dttResult = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        End If

        If dttResult.Rows.Count > 0 Then
          '------------------------------
          'se voglio solo il prezzo generico o solo lo specifico mi sposto sulla prima riga con lc_conto = 0 o lc_conto <> 0
          If strTipoval = "G" Then
            dtrList = dttResult.Select("lc_conto = 0", "lc_conto DESC, lc_codtpro DESC, lc_datagg DESC, lc_daquant")
          ElseIf strTipoval = "S" Then
            dtrList = dttResult.Select("lc_conto <> 0", "lc_conto DESC, lc_codtpro DESC, lc_datagg DESC, lc_daquant")
          Else
            'se abilitata, la cercaprezzo lavora come in VB6, ovvero se il prezzo specifico cliente/articolo è = 0 lo ignora e prende il prezzo generico
            If oApp.oGvar.bCercaPrezzoSoloPrezziDiversiDa0 Then
              dtrList = dttResult.Select("lc_prezzo <> 0", "lc_conto DESC, lc_codtpro DESC, lc_datagg DESC, lc_daquant")
            Else
              dtrList = dttResult.Select(Nothing, "lc_conto DESC, lc_codtpro DESC, lc_datagg DESC, lc_daquant")
            End If
          End If

          'ora ho solo le righe che mi servono: prendo la prima
          If Not dtrList Is Nothing AndAlso dtrList.Length > 0 Then
            dPrezzo = ArrDbl(NTSCDec(dtrList(0)!lc_prezzo), TrovaNdecSuPrzUn(nCodvalu))
            nCodpromo = NTSCInt(dtrList(0)!lc_codtpro)
            strPrzNet = dtrList(0)!lc_netto.ToString
            If NTSCInt(dtrList(0)!lc_conto) <> 0 Then strTipovalout = "S" Else strTipovalout = "G" ' Controllo se è Specifico o Generico
            If Not (NTSCDec(dtrList(0)!lc_daquant) = 0 And NTSCDec(dtrList(0)!lc_aquant) = 9999999999) Then
              nPerQta = CLN__STD.bsPrSetsiqta
            Else
              nPerQta = CLN__STD.bsPrSetnoqta
            End If
            dDaQuant = NTSCDec(dtrList(0)!lc_daquant)
            dAquant = NTSCDec(dtrList(0)!lc_aquant)
            dPerqta = NTSCDec(dtrList(0)!lc_perqta)
            strUnmisout = dtrList(0)!lc_unmis.ToString
            dtDatAggOut = NTSCDate(dtrList(0)!lc_datagg)
            dtAdata = NTSCDate(dtrList(0)!lc_datscad)
            lProgr = NTSCInt(dtrList(0)!lc_progr)
            strTipoOut = NTSCStr(dtrList(0)!lc_tipo)
          End If    'If dttList.Length > 0 Then
        End If    'If dsTmp.Tables("LISTINI").Rows.Count > 0 Then

        If dPrezzo <> 0 Then Return False
        If strTipoval = "S" Then Return False
      End If    'If nListino >= 0 Then

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaPrezzo_Costi(ByVal strDitta As String, ByVal dsTableInMem As DataSet, ByVal nListino As Integer, ByVal strCodart As String, ByVal nFase As Integer, _
                                                ByVal strUnmisin As String, ByVal strConfez2 As String, ByVal strUnMis2 As String, ByVal dQtacon2 As Decimal, _
                                                ByVal dConver As Decimal, ByVal dArperqta As Decimal, ByVal nCodvalu As Integer, ByVal dtDatdoc As Date, _
                                                ByVal dQuant As Decimal, ByRef dPrezzo As Decimal, ByRef strErr As String) As Boolean
    Dim dCambio As Decimal = 0
    Dim dValT As Decimal = 0
    Try
      '------------------------------
      'ULTIMO COSTO
      'se richiesto ultimo costo e non trovato prezzo speciale da fornitore, allora cerco in artprox
      If dPrezzo = 0 And nListino = 0 Then
        CercaPrezzo_ArtProx(strDitta, strCodart, nFase, strUnmisin, strConfez2, strUnMis2, dQtacon2, _
                            dConver, dArperqta, nCodvalu, dtDatdoc, "U", dPrezzo, dsTableInMem)
      End If

      '------------------------------
      'ULTIMO COSTO COMPRENSIVO DI ONERI ACCESSORI
      If nListino = -1 Then
        CercaPrezzo_ArtProx(strDitta, strCodart, nFase, strUnmisin, strConfez2, strUnMis2, dQtacon2, _
                            dConver, dArperqta, nCodvalu, dtDatdoc, "A", dPrezzo, dsTableInMem)
      End If

      '------------------------------
      'COSTO MEDIO
      If nListino = -2 Then
        CercaPrezzo_ArtProx(strDitta, strCodart, nFase, strUnmisin, strConfez2, strUnMis2, dQtacon2, _
                            dConver, dArperqta, nCodvalu, dtDatdoc, "M", dPrezzo, dsTableInMem)
      End If

      '------------------------------
      ' COSTO FIFO (indip. dal cod conto ..)
      If nListino = -3 Then  ' costo medio fifo, su mag. zero = tutti
        dPrezzo = CercaPrezzoFifo(strDitta, strCodart, 0, 0, dtDatdoc.ToShortDateString, dQuant, 0, dValT, strErr)
        dPrezzo = ArrDbl(dPrezzo, TrovaNdecSuPrzUn(0))
        If dPrezzo <> 0 And Trim(strUnmisin) <> "" Then
          Select Case strUnmisin
            Case strConfez2
              dPrezzo = ArrDbl(dPrezzo * dQtacon2, TrovaNdecSuPrzUn(0))
            Case strUnMis2
              dPrezzo = ArrDbl(dPrezzo / dConver, TrovaNdecSuPrzUn(0))
          End Select
        End If
        ' ora se necessario converte col cambio....
        If dPrezzo <> 0 And nCodvalu <> 0 Then
          dCambio = CercaCambioDiOggi(nCodvalu, dtDatdoc.ToShortDateString)
          If dCambio <> 0 Then
            dPrezzo = ConvImpEur(True, dPrezzo, nCodvalu, dtDatdoc, dCambio)
          Else
            'Se il cambio di oggi non esiste (dCambio=0) ritorna comuque zero
            dPrezzo = 0
          End If
        End If
      End If

      '------------------------------
      'COSTO MEDIO PONDERATO ATTUALE (cio determinato sulla esistenza corrente dei magazzini merce propria)
      If nListino = -4 Then
        dPrezzo = CercaCostoMedioPond(strDitta, strCodart, nFase, oApp.oGvar.nCostoMedioPondTipoMagaz, dArperqta)
        dPrezzo = ArrDbl(dPrezzo, TrovaNdecSuPrzUn(0))
        If dPrezzo <> 0 And Trim(strUnmisin) <> "" Then
          Select Case strUnmisin
            Case strConfez2
              dPrezzo = ArrDbl(dPrezzo * dQtacon2, TrovaNdecSuPrzUn(0))
            Case strUnMis2
              dPrezzo = ArrDbl(dPrezzo / dConver, TrovaNdecSuPrzUn(0))
          End Select
        End If
        ' ora se necessario converte col cambio....
        If dPrezzo <> 0 And nCodvalu <> 0 Then
          dCambio = CercaCambioDiOggi(nCodvalu, dtDatdoc.ToShortDateString)
          If dCambio <> 0 Then
            dPrezzo = ConvImpEur(True, dPrezzo, nCodvalu, dtDatdoc, dCambio)
          Else
            'Se il cambio di oggi non esiste (dCambio=0) ritorna comuque zero
            dPrezzo = 0
          End If
        End If
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaPrezzo_Valuta(ByVal strDitta As String, ByVal dsTableInMem As DataSet, ByVal nListino As Integer, ByVal nCodValu As Integer, _
                                                 ByVal strCoda As String, ByVal nCodlavo As Integer, ByVal dtDatDoc As Date, ByVal dQuantabs As Decimal, ByVal nFase As Integer, _
                                                 ByVal strCodCas As String, ByVal bConspromo As Boolean, ByVal nCodpromoin As Integer, ByVal strSQLUM As String, _
                                                 ByRef dPrezzo As Decimal, ByRef nCodpromo As Integer, ByRef strPrzNet As String, ByRef nPerqta As Integer, _
                                                 ByRef dDaQuant As Decimal, ByRef dAquant As Decimal, ByRef dPerqta As Decimal, ByRef strUnmisout As String, _
                                                 ByRef dtDatAggOut As Date, ByRef dtAdata As Date, ByRef lProgr As Integer, ByRef strTipoOut As String) As Boolean
    Dim strSQL As String = ""
    Dim dtrList As DataRow = Nothing
    Dim dCambio As Decimal
    Dim strWhere, strOrderBy As String
    Try
      If dPrezzo = 0 And nListino > 0 And nCodValu > 0 Then
        strWhere = " lc_codart = " & CStrSQL(strCoda) & _
                   " AND (lc_conto = 0)" & _
                   " AND (lc_listino = " & nListino & ")" & _
                   " AND lc_codvalu = 0" & _
                   " AND lc_fase = " & nFase

        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bConspromo Then
            strWhere &= " AND lc_codtpro = 0"
          Else
            If nCodpromoin > 0 Then
              strWhere &= " AND lc_codtpro = " & nCodpromoin
            End If
          End If
        Else
          strWhere &= " AND (lc_codtpro = " & nCodpromoin & " OR lc_codtpro = 0)"
        End If
        strWhere &= strSQLUM

        strOrderBy = " lc_codtpro DESC, lc_datagg DESC, lc_daquant"


        If Not dsTableInMem Is Nothing Then
          'Se ho la tabella in memoria, la query viene fatta sul datatable
          Dim dtrSel() As DataRow = dsTableInMem.Tables("LISTINI").Select(strWhere, strOrderBy)

          If dtrSel.Length > 0 Then dtrList = dtrSel(0)
        Else
          'Non ho la tabella in memoria, la query viene fatta sul database
          strSQL = "SELECT TOP 1 lc_prezzo, lc_codtpro, lc_daquant, lc_aquant, lc_perqta," & _
                   " lc_unmis, lc_fase, lc_netto, lc_datagg, lc_datscad, lc_progr, lc_tipo FROM listini" & _
                   " WHERE " & strWhere & _
                   " AND codditt = " & CStrSQL(strDitta) & _
                   " AND lc_datagg <= " & CDataSQL(dtDatDoc.ToShortDateString) & _
                   " AND lc_datscad >= " & CDataSQL(dtDatDoc.ToShortDateString) & _
                   " AND lc_daquant <= " & CDblSQL(dQuantabs.ToString) & _
                   " AND lc_aquant >= " & CDblSQL(dQuantabs.ToString) & _
                   " AND lc_codlavo = " & nCodlavo & _
                   " AND lc_codcas = " & CStrSQL(strCodCas) & _
                   " ORDER BY " & strOrderBy

          Dim dsTmp As DataSet = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

          If dsTmp.Tables("LISTINI").Rows.Count > 0 Then dtrList = dsTmp.Tables("LISTINI").Rows(0)
        End If


        If Not dtrList Is Nothing Then
          dPrezzo = ArrDbl(NTSCDec(dtrList!lc_prezzo), TrovaNdecSuPrzUn(nCodValu))
          nCodpromo = NTSCInt(dtrList!lc_codtpro)
          strPrzNet = dtrList!lc_netto.ToString
          If Not (NTSCDec(dtrList!lc_daquant) = 0 And NTSCDec(dtrList!lc_aquant) = 9999999999) Then
            nPerqta = CLN__STD.bsPrSetsiqta
          Else
            nPerqta = CLN__STD.bsPrSetnoqta
          End If
          dDaQuant = NTSCDec(dtrList!lc_daquant)
          dAquant = NTSCDec(dtrList!lc_aquant)
          dPerqta = NTSCDec(dtrList!lc_perqta)
          strUnmisout = dtrList!lc_unmis.ToString
          dtDatAggOut = NTSCDate(dtrList!lc_datagg)
          dtAdata = NTSCDate(dtrList!lc_datscad)
          lProgr = NTSCInt(dtrList!lc_progr)
          strTipoOut = NTSCStr(dtrList!lc_tipo)
        End If

        If dPrezzo <> 0 And nCodValu <> 0 Then
          dCambio = CercaCambioDiOggi(nCodValu, dtDatDoc.ToShortDateString)
          If dCambio <> 0 Then
            dPrezzo = ConvImpEur(True, dPrezzo, nCodValu, dtDatDoc, dCambio)
          Else
            'Se il cambio di oggi non esiste (dCambio=0) ritorna comuque zero
            dPrezzo = 0
          End If
        End If
      End If    'If dPrezzo = 0 And nListino > 0 And nCodvalu > 0 Then

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaPrezzo_ArtProx(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, _
                                                  ByVal strUnmisin As String, ByVal strConfez2 As String, _
                                                  ByVal strUnMis2 As String, ByVal dQtacon2 As Decimal, _
                                                  ByVal dConver As Decimal, ByVal dArperqta As Decimal, _
                                                  ByVal nCodvalu As Integer, ByVal dtDatdoc As DateTime, _
                                                  ByVal strTipoCosto As String, ByRef dPrezzo As Decimal) As Boolean
    Try
      Return CercaPrezzo_ArtProx(strDitta, strCodart, nFase, strUnmisin, strConfez2, strUnMis2, dQtacon2, _
                                 dConver, dArperqta, nCodvalu, dtDatdoc, strTipoCosto, dPrezzo, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaPrezzo_ArtProx(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, _
                                                  ByVal strUnmisin As String, ByVal strConfez2 As String, _
                                                  ByVal strUnMis2 As String, ByVal dQtacon2 As Decimal, _
                                                  ByVal dConver As Decimal, ByVal dArperqta As Decimal, _
                                                  ByVal nCodvalu As Integer, ByVal dtDatdoc As DateTime, _
                                                  ByVal strTipoCosto As String, ByRef dPrezzo As Decimal, _
                                                  ByVal dttTableInMem As DataSet) As Boolean
    Dim dtrTmp As DataRow = Nothing
    Dim strSQL As String = ""
    Dim strWhere As String
    Dim dCambio As Decimal
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nFase, strUnmisin, strConfez2, strUnMis2, dQtacon2, _
                                             dConver, dArperqta, nCodvalu, dtDatdoc, strTipoCosto, dPrezzo, dttTableInMem})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dPrezzo = NTSCDec(oIn(12))        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      strWhere = " codditt = " & CStrSQL(strDitta) & _
                 " AND apx_codart = " & CStrSQL(strCodart) & _
                 " AND apx_fase = " & nFase

      If Not dttTableInMem Is Nothing Then
        'Query in memoria
        Dim dtrSel() As DataRow = dttTableInMem.Tables("ARTPROX").Select(strWhere)
        If dtrSel.Length > 0 Then dtrTmp = dtrSel(0)
      Else
        'Query su DB
        strSQL = "SELECT apx_ultcos, apx_peucos, apx_qtalif, apx_vqtalif, apx_giaini, apx_vgiaini FROM artprox" & _
                 " WHERE " & strWhere

        Dim dsTmp As DataSet = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

        If dsTmp.Tables("LISTINI").Rows.Count > 0 Then dtrTmp = dsTmp.Tables("LISTINI").Rows(0)
      End If

      If Not dtrTmp Is Nothing Then
        '-------------------------------------
        'ultimo costo
        If strTipoCosto = "U" Then
          dPrezzo = ArrDbl(NTSCDec(dtrTmp!apx_ultcos), TrovaNdecSuPrzUn(0))
        End If

        '-------------------------------------
        'Ultimo costo comprensivo di oneri accessori
        If strTipoCosto = "A" Then
          dPrezzo = ArrDbl(NTSCDec(dtrTmp!apx_peucos), TrovaNdecSuPrzUn(0))
        End If

        '-------------------------------------
        'Costo medio
        If strTipoCosto = "M" Then
          If NTSCDec(dtrTmp!apx_qtalif) <> 0 Then
            dPrezzo = ArrDbl(NTSCDec(dtrTmp!apx_vqtalif) / _
                                 NTSCDec(dtrTmp!apx_qtalif) * _
                                 dArperqta, TrovaNdecSuPrzUn(0))
          End If
        End If

        '-------------------------------------
        'se non ho trovato nulla, prendo il prezzo inventariale
        If dPrezzo = 0 And NTSCDec(dtrTmp!apx_giaini) <> 0 Then
          dPrezzo = ArrDbl(NTSCDec(dtrTmp!apx_vgiaini) / _
                               NTSCDec(dtrTmp!apx_giaini) * _
                               dArperqta, TrovaNdecSuPrzUn(0))
        End If
      End If

      If dPrezzo <> 0 And strUnmisin.Trim <> "" Then
        Select Case strUnmisin
          Case strConfez2
            dPrezzo = ArrDbl(dPrezzo * dQtacon2, TrovaNdecSuPrzUn(0))
          Case strUnMis2
            dPrezzo = ArrDbl(dPrezzo / dConver, TrovaNdecSuPrzUn(0))
        End Select
      End If
      ' ora se necessario converte col cambio....
      If dPrezzo <> 0 And nCodvalu <> 0 Then
        dCambio = CercaCambioDiOggi(nCodvalu, dtDatdoc.ToShortDateString)
        If dCambio <> 0 Then
          dPrezzo = ConvImpEur(True, dPrezzo, nCodvalu, dtDatdoc, dCambio)
        Else
          'Se il cambio di oggi non esiste (dCambio=0) ritorna comunque zero
          dPrezzo = 0
        End If
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function CercaCondCommerciali_ContoFatturazione(ByVal oCondCommerciali As NTSCondCommerciali) As Integer
    Dim strSQL As String = ""
    Try
      With oCondCommerciali.Input
        '-----------------------------
        'Se abilitata l'opzione gbRilevaPrezziDaContoFatt legge il conto di fatturazione
        If oApp.oGvar.bRilevaScontiDaContoFatt And .lConto > 0 Then
          strSQL = "SELECT an_contfatt FROM anagra WHERE codditt = " & CStrSQL(.strDitta) & " AND an_conto = " & .lConto
          Dim dttContoFatt As DataTable = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttContoFatt.Rows.Count > 0 Then
            If NTSCInt(dttContoFatt.Rows(0)!an_contfatt) > 0 Then Return NTSCInt(dttContoFatt.Rows(0)!an_contfatt) 'trovato: lo usa al posto del conto passato alla funzione
          End If
        End If
      End With
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return oCondCommerciali.Input.lConto
  End Function
  Public Overridable Function CercaCondCommerciali_QueryPromozioni(ByVal oCondCommerciali As NTSCondCommerciali, ByVal strPrefix As String) As String
    Dim strSQL As String = ""
    Try
      With oCondCommerciali.Input
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not .bRetail OrElse (.bRetail AndAlso CBool(GetSettingBus("BSREGSRE", "OPZIONI", ".", "Applica_anche_prom_come_Rel10", "0", ".", "0"))) Then
            If Not .bConspromo Then
              strSQL &= " AND " & strPrefix & "_codtpro = 0"
            Else
              If .nCodpromo > 0 Then strSQL &= " AND " & strPrefix & "_codtpro = " & .nCodpromo
            End If
          Else
            strSQL &= " AND " & strPrefix & "_codtpro = 0"
          End If
        Else
          strSQL &= " AND (" & strPrefix & "_codtpro = " & .nCodpromo & " OR " & strPrefix & "_codtpro = 0)"
        End If
      End With
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return strSQL
  End Function

  Public Overridable Function CercaCondCommerciali_CercaPrezzo(ByVal oCondCommerciali As NTSCondCommerciali) As Boolean
    Dim bRicercaPrezzoMultipla As Boolean = False
    Dim dsTableInMem As DataSet = Nothing
    Dim dsArtico As DataSet
    Dim strSQL As String = ""

    Dim oDatiInput As NTSCondCommerciali.DatiInput = oCondCommerciali.Input
    Try
      With oCondCommerciali
        If Not .bCalcolaPrezzo Then Return True 'non doveva ricalcolare il prezzo

        With .Input
          If .bConsideraPrezziUnmis Then 'Se deve considerare i prezzi per unità di misura, allora deve fare la "tripla" ricerca in base
            If .bPrezziPerUnmis Then
              .dQuant = .dColli
            Else
              If .bGestionePrezzi Then
                .strUnmis = .strUmp
              Else
                .strUnmis = " "
              End If
            End If
          End If

          .dQuant = Math.Abs(.dQuant)
          If .dQuant = 0 Then .dQuant = 1
          'C'è una opzione di registro specifica del retail che va chiamata solo quando il cod. promo è 0.
          If .nCodpromo = -1 Then .nCodpromo = 0 : .bRetail = True
        End With

        '------------------------------
        'Azzera risultati
        With .OutputPrezzo
          .nPerqta = CLN__STD.bsPrSetnoqta
          .dPrezzo = 0
          .dPrelist = 0
          .nCodpromo = 0
          .dDaQuant = 0
          .dAquant = 9999999999
          .strPrzNet = "N"
          .strUnmis = oCondCommerciali.Input.strUnmis
          .strTipoval = oCondCommerciali.Input.strTipoval
          .dtDatAgg = New Date(1900, 1, 1) ''in questo parametro opzionale esce la DATA INIZIO VALIDITA' del listino letto (ossia lc_datagg)
          .dtAData = New Date(1900, 1, 1)
        End With

        If .Input.strWhereArtico.Trim <> "" Then
          bRicercaPrezzoMultipla = True

          With .OutputPrezzo
            .dttListini = New DataTable

            With .dttListini.Columns
              .Add("strCodart", GetType(String))
              .Add("nFase", GetType(Integer))
              .Add("dPrezzo", GetType(Decimal))
              .Add("dPrelist", GetType(Decimal))
              .Add("nCodpromo", GetType(Integer))
              .Add("strPrzNet", GetType(String))
              .Add("strTipoOut", GetType(String))
              .Add("nPerqta", GetType(Integer))
              .Add("dDaQuant", GetType(Decimal))
              .Add("dAquant", GetType(Decimal))
              .Add("dPerqta", GetType(Decimal))
              .Add("strUnmisout", GetType(String))
              .Add("strTipovalout", GetType(String))
              .Add("strCodCas", GetType(String))
              .Add("strErr", GetType(String))
              .Add("dtDatAggOut", GetType(Date))
              .Add("dtAdata", GetType(Date))
              .Add("lProgr", GetType(Integer))
            End With
          End With
        End If

        With .Input
          '-----------------------------
          'Se abilitata l'opzione gbRilevaPrezziDaContoFatt legge il conto di fatturazione
          .lConto = CercaCondCommerciali_ContoFatturazione(oCondCommerciali)

          '------------------------------
          'verifico se l'articolo  gestito a varianti e ottengo le varie unità di misura 
          If bRicercaPrezzoMultipla Then
            If .lListaSel <> 0 Then .strWhereArtico &= " AND lsa_codlsar = " & .lListaSel

            strSQL = "SELECT DISTINCT ar_codart, ar_gesvar, ar_prevar, ar_codroot, ar_codvar1, ar_perqta, ar_gesfasi, " & _
                     " CASE WHEN ar_gesfasi = 'S' THEN af_fase ELSE ar_ultfase END As Fase, ar_unmis, " & _
                     " ar_confez2, ar_qtacon2, ar_unmis2, ar_conver FROM artico" & _
                     " LEFT JOIN artfasi ON artico.codditt = artfasi.codditt AND artico.ar_codart = artfasi.af_codart "

            If .lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

            strSQL &= " WHERE artico.codditt = " & CStrSQL(.strDitta) & _
                      .strWhereArtico
          Else
            strSQL = "SELECT ar_codart, ar_gesvar, ar_prevar, ar_codroot, ar_codvar1, ar_perqta, ar_gesfasi, ar_unmis, " & _
                     "ar_confez2, ar_qtacon2, ar_unmis2, ar_conver FROM artico" & _
                     " WHERE codditt = " & CStrSQL(.strDitta) & _
                     " AND ar_codart = " & CStrSQL(.strCodart)
          End If

          dsArtico = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")

          '----------- Prepara i dati in memoria per limitare il numero di query sul DB
          If bRicercaPrezzoMultipla Then
            If .nListino >= 0 Then CercaCondCommerciali_PreparaDatatableListini(oCondCommerciali, dsTableInMem)

            If .nListino <= 0 And .nListino > -3 Then CercaCondCommerciali_PreparaDatatableArtprox(oCondCommerciali, dsTableInMem)

            If .nListino = -3 Then CercaCondCommerciali_PreparaDatatablePrezzoFifo(oCondCommerciali, dsTableInMem)

            If .nListino = -4 Then CercaCondCommerciali_PreparaDatatableCostoMedioPond(oCondCommerciali, dsTableInMem)
          End If
        End With

        Dim nRow As Integer = 0
        For Each dtrArt As DataRow In dsArtico.Tables("ARTICO").Rows
          nRow += 1
          With .OutputPrezzo
            .dPrezzo = 0
            .dPrelist = 0
            .nCodpromo = 0
            .strPrzNet = "N"
            .strTipo = " "
            .nPerqta = CLN__STD.bsPrSetnoqta
            .dDaQuant = 0
            .dAquant = 999999999
            .dPerqta = 0
            .strUnmis = " "
            .strTipoval = " "
            If .strCodCas = "" Then .strCodCas = " "
            .strErr = ""
            .dtDatAgg = New Date(1900, 1, 1)
            .dtAData = New Date(1900, 1, 1)
            .lProgr = 0
          End With

          With .Input
            If bRicercaPrezzoMultipla Then
              .nFase = NTSCInt(dtrArt!fase)
              .strCodart = NTSCStr(dtrArt!ar_codart)
            End If

            If NTSCStr(dtrArt!ar_gesvar) = "S" Then ' se  a varianti cambia la base di calcolo ..
              Select Case NTSCStr(dtrArt!ar_prevar)
                Case "N" : .strCodart = NTSCStr(dtrArt!ar_codroot)
                Case "1" : .strCodart = NTSCStr(dtrArt!ar_codroot) & NTSCStr(dtrArt!ar_codvar1)
                  'Case "S" : .strCodart = .strCodart
              End Select
            End If

            If NTSCStr(dtrArt!ar_gesfasi) = "N" Then .nFase = 0
          End With
          '------------------------------
          Dim strSQLUM As String = ""
          If .Input.strUnmis.Trim = "" Then
            strSQLUM = " And (lc_unmis = ' ' Or lc_unmis = " & CStrSQL(NTSCStr(dtrArt!ar_unmis)) & ")"
          Else
            strSQLUM = " And lc_unmis = " & CStrSQL(.Input.strUnmis)
          End If

          '------------------------------
          'Cerca il prezzo di listino nella valuta zero (lire od euro...)(prelist  sempre relativo alla valuta zero!!)
          ' da memorizzare in movmag/movord per statistiche sugli sconti medi applicati
          If Not CercaCondCommerciali_PreList(oCondCommerciali, dtrArt, strSQLUM, dsTableInMem) Then Continue For
          '------------------------------
          ' ora iniza la ricerca del prezzo vero e proprio (SPECIFICO E/O GENERICO)
          ' cerca il prezzo richiesto o ultimo costo (ma nella valuta indicata!!!)
          ' per quel conto , se esiste
          'If nListino > 0 Or lConto > 0 Then 'in questo modo se chiedo un listino minore di 0 (es costo medio) non deve cercare il prezzo spec. per fornitore!!!!
          If Not CercaCondCommerciali_Listino(oCondCommerciali, dtrArt, strSQLUM, dsTableInMem) Then
            If bRicercaPrezzoMultipla Then
              .OutputPrezzo.dttListini.Rows.Add()
              With .OutputPrezzo.dttListini.Rows(.OutputPrezzo.dttListini.Rows.Count - 1)
                !strCodart = NTSCStr(dtrArt!ar_codart)
                !nFase = oCondCommerciali.Input.nFase
                !dPrezzo = oCondCommerciali.OutputPrezzo.dPrezzo
                !dPrelist = oCondCommerciali.OutputPrezzo.dPrelist
                !nCodpromo = oCondCommerciali.OutputPrezzo.nCodpromo
                !strPrzNet = oCondCommerciali.OutputPrezzo.strPrzNet
                !strTipoOut = oCondCommerciali.OutputPrezzo.strTipo
                !nPerqta = oCondCommerciali.OutputPrezzo.nPerqta
                !dDaQuant = oCondCommerciali.OutputPrezzo.dDaQuant
                !dAquant = oCondCommerciali.OutputPrezzo.dAquant
                !dPerqta = oCondCommerciali.OutputPrezzo.dPerqta
                !strUnmisout = oCondCommerciali.OutputPrezzo.strUnmis
                !strTipovalout = oCondCommerciali.OutputPrezzo.strTipoval
                !strCodCas = oCondCommerciali.OutputPrezzo.strCodCas
                !strErr = oCondCommerciali.OutputPrezzo.strErr
                !dtDatAggOut = oCondCommerciali.OutputPrezzo.dtDatAgg
                !dtAdata = oCondCommerciali.OutputPrezzo.dtAData
                !lProgr = oCondCommerciali.OutputPrezzo.lProgr
              End With
            End If
            Continue For
          End If

          'Listino < 0 (ultimo costo, costo medio, ecc...)
          If Not CercaCondCommerciali_Costi(oCondCommerciali, dtrArt, strSQLUM, dsTableInMem) Then Continue For

          '------------------------------
          ' adesso se cercavo un prezzo in valuta listino > 0 e non l'ho trovato,
          ' cerca ev. listino in lire/euro corrispondente e lo trasformo, se lo trovo, in valuta
          If Not CercaCondCommerciali_Valuta(oCondCommerciali, dtrArt, strSQLUM, dsTableInMem) Then Continue For


          If bRicercaPrezzoMultipla Then
            .OutputPrezzo.dttListini.Rows.Add()
            With .OutputPrezzo.dttListini.Rows(.OutputPrezzo.dttListini.Rows.Count - 1)
              !strCodart = NTSCStr(dtrArt!ar_codart)
              !nFase = oCondCommerciali.Input.nFase
              !dPrezzo = oCondCommerciali.OutputPrezzo.dPrezzo
              !dPrelist = oCondCommerciali.OutputPrezzo.dPrelist
              !nCodpromo = oCondCommerciali.OutputPrezzo.nCodpromo
              !strPrzNet = oCondCommerciali.OutputPrezzo.strPrzNet
              !strTipoOut = oCondCommerciali.OutputPrezzo.strTipo
              !nPerqta = oCondCommerciali.OutputPrezzo.nPerqta
              !dDaQuant = oCondCommerciali.OutputPrezzo.dDaQuant
              !dAquant = oCondCommerciali.OutputPrezzo.dAquant
              !dPerqta = oCondCommerciali.OutputPrezzo.dPerqta
              !strUnmisout = oCondCommerciali.OutputPrezzo.strUnmis
              !strTipovalout = oCondCommerciali.OutputPrezzo.strTipoval
              !strCodCas = oCondCommerciali.OutputPrezzo.strCodCas
              !strErr = oCondCommerciali.OutputPrezzo.strErr
              !dtDatAggOut = oCondCommerciali.OutputPrezzo.dtDatAgg
              !dtAdata = oCondCommerciali.OutputPrezzo.dtAData
              !lProgr = oCondCommerciali.OutputPrezzo.lProgr
            End With
          End If
        Next

        With .OutputPrezzo.dttListini
          If bRicercaPrezzoMultipla Then .PrimaryKey = New System.Data.DataColumn() {.Columns("strCodart"), .Columns("nFase")}
        End With

        dsArtico.Tables.Clear()

        'In questo caso se il prezzo è 0 lo deve ricalcolare con parametri diversi
        If .Input.bConsideraPrezziUnmis AndAlso .Input.bPrezziPerUnmis AndAlso .OutputPrezzo.dPrezzo = 0 Then
          oCondCommerciali.Input = oDatiInput
          .Input.strUnmis = .Input.strUmp
          .Input.bConsideraPrezziUnmis = False
          .OutputPrezzo.bDoppiaRicerca = True
          CercaCondCommerciali_CercaPrezzo(oCondCommerciali)
        End If
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      oCondCommerciali.Input = oDatiInput 'Ripristino i dati iniziali passati, che nel frattempo sono stati aggiornati dall'elaborazione.
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_PreparaDatatableListini(ByVal oCondCommerciali As NTSCondCommerciali, ByRef dsTableInMem As DataSet) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    '--- Chiamata SOLO da Stampa Listini
    '----------------------------------------------------------------------------------------------------------------
    Dim strSQL As String = ""

    Try
      With oCondCommerciali.Input
        strSQL = "SELECT DISTINCT lc_progr, lc_codart, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino, lc_datagg, lc_tipo, lc_prezzo, lc_datscad, " & _
                 " lc_daquant, lc_aquant, lc_perqta, lc_unmis, lc_note, lc_netto, lc_fase, lc_ultagg, lc_codlavo" & _
                 " FROM busvw_listini " & _
                 " INNER JOIN artico ON artico.codditt = busvw_listini.codditt AND artico.ar_codart = busvw_listini.ar_codart "
        'La ricerca la fa più ampia di quanto necessario per prendere anche i listini collegati ad articoli a variante con prezzi su codice root o sulla prima variante

        If .lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

        strSQL &= " WHERE busvw_listini.codditt = " & CStrSQL(.strDitta) & _
                  " AND lc_listino IN (0, " & .nListino & ")" & _
                  " AND lc_conto IN (0, " & .lConto & ")" & _
                  " AND lc_coddest = " & .lDestdiv & _
                  " AND lc_codlavo = " & .nCodlavo & _
                  " AND lc_codvalu IN (0, " & .nCodvalu & ")" & _
                  " AND " & CDataSQL(.dtDatdoc) & " BETWEEN lc_datagg AND lc_datscad " & _
                  " AND " & CDblSQL(.dQuant) & " BETWEEN lc_daquant AND lc_aquant " & _
                  " AND lc_codcas = " & CStrSQL(.strCodCas) & _
                  .strWhereArtico.Replace("ar_codart", "artico.ar_codart")

        dsTableInMem = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")
      End With

      With dsTableInMem.Tables("LISTINI")
        .PrimaryKey = New DataColumn() {.Columns("lc_codart"), .Columns("lc_conto"), .Columns("lc_coddest"), _
                                        .Columns("lc_listino"), .Columns("lc_codvalu"), .Columns("lc_codtpro"), _
                                        .Columns("lc_fase"), .Columns("lc_codlavo"), .Columns("lc_datagg"), _
                                        .Columns("lc_daquant"), .Columns("lc_progr")}
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_PreparaDatatableArtprox(ByVal oCondCommerciali As NTSCondCommerciali, ByRef dsTableInMem As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      With oCondCommerciali.Input
        strSQL = "SELECT artprox.* FROM artprox" & _
                 " INNER JOIN artico ON artico.codditt = artprox.codditt AND ar_codart = apx_codart "

        If .lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

        strSQL &= " WHERE artprox.codditt = " & CStrSQL(.strDitta) & _
                  .strWhereArtico
      End With

      OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPROX", dsTableInMem)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_PreparaDatatablePrezzoFifo(ByVal oCondCommerciali As NTSCondCommerciali, ByRef dsTableInMem As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      With oCondCommerciali.Input
        '-------------------------------------------
        ' Creo il secondo recordset che mi restituisce il valore della giacenza ad oggi
        strSQL = "SELECT ap_codart, ap_fase, Sum(artpro.ap_esist) AS somma" & _
                " FROM tabmaga " & _
                " INNER JOIN ARTPRO ON tabmaga.tb_codmaga = ARTPRO.ap_magaz and tabmaga.codditt = artpro.codditt " & _
                " INNER JOIN artico ON artico.codditt = artpro.codditt AND ar_codart = ap_codart "

        If .lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

        strSQL &= " WHERE artpro.codditt = " & CStrSQL(.strDitta) & _
                  .strWhereArtico & _
                  "GROUP BY ap_codart, ap_fase"

        dsTableInMem = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPRO")

        '-------------------------------------------
        'giacenza di materiale a magazzino
        ' se calcolo a data , cerco l'esistenza alla data indicata
        If .dtDatdoc <> "" Then
          strSQL = "SELECT km_codart, km_fase, Sum(mm_quant*km_carscar) AS diff " & _
                   " FROM tabmaga INNER JOIN (movmag INNER JOIN keymag ON (movmag.mm_riga = keymag.km_riga)" & _
                   " AND (movmag.mm_tipork = keymag.km_tipork) AND (movmag.mm_serie = keymag.km_serie)" & _
                   " AND (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_numdoc = keymag.km_numdoc) AND " & _
                   " (movmag.codditt = keymag.codditt))" & _
                   " ON tabmaga.tb_codmaga = keymag.km_magaz and tabmaga.codditt = keymag.codditt " & _
                   " INNER JOIN artico ON artico.codditt = movmag.codditt AND ar_codart = mm_codart "

          If .lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

          strSQL &= " WHERE km_aammgg > " & CDataSQL(.dtDatdoc.ToString) & _
                    " AND keymag.codditt = " & CStrSQL(.strDitta) & _
                    .strWhereArtico & _
                    "GROUP BY km_codart, km_fase"

          dsTableInMem = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaMovm")
        End If

        '-------------------------------------------
        ' Creo il  recordset dei carichi valorizzanti ..
        strSQL = " SELECT keymag.km_magaz, keymag.km_carscar, keymag.km_aammgg, movmag.mm_codart, km_codart, km_fase, movmag.mm_quant, " & _
                 " movmag.mm_perqta, "
        If oApp.oGvar.bUltSpeseInValoriFifo Then strSQL += " movmag.mm_numpex + " 'ulteriori spese di piede indicate in veboll
        If oApp.oGvar.bOneriAccInValoriFifo Then
          strSQL += " movmag.mm_valore + movmag.mm_numpac as valore "
        Else
          strSQL += " movmag.mm_valore as valore "
        End If
        strSQL += " FROM (movmag INNER JOIN keymag ON (movmag.mm_riga = keymag.km_riga) AND " & _
                  " (movmag.mm_tipork = keymag.km_tipork) And  (movmag.mm_serie = keymag.km_serie) AND " & _
                  " (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_numdoc = keymag.km_numdoc) AND " & _
                  " (movmag.codditt = keymag.codditt)) INNER JOIN tabcaum on keymag.km_causale = tabcaum.tb_codcaum " & _
                  " INNER JOIN artico ON artico.codditt = keymag.codditt AND ar_codart = km_codart "

        If .lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

        strSQL &= " WHERE tabcaum.tb_valoriz <> 0 " & _
                  " and tabcaum.tb_vvaloriz <> 0 " & _
                  " and keymag.codditt = " & CStrSQL(.strDitta) & _
                  " AND keymag.km_carscar > 0 " & .strWhereArtico
        If Not .dtDatdoc = "" Then strSQL &= " AND km_aammgg <= " & CDataSQL(.dtDatdoc)
        '-------------------------------------------
        'controllo il codice di magazzino e se  = 0 seleziono tutti i record
        ' NO !! (sempre tutti, solo i carichi valorizzanti indipendenti dal magazzino !)
        'If nCodmaga <> 0 Then
        '   strSQL = strSQL & " AND keymag.km_magaz = " & nCodmaga & ""
        'End If
        strSQL &= " ORDER BY keymag.km_aammgg DESC, keymag.km_numdoc DESC, keymag.km_riga DESC " 'la routine 'CercaPrezzoFifo' poteva restituire dei risultati diversi rispetto alla versione in vb6 se all'interno dei documenti di carico lo stesso articolo veniva indicato più volte con prezzi diversi
      End With

      dsTableInMem = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaFifo")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_PreparaDatatableCostoMedioPond(ByVal oCondCommerciali As NTSCondCommerciali, ByRef dsTableInMem As DataSet) As Boolean
    'Se nCodmaga > 0, allora solo magazzino nCodmaga
    'Se nCodmaga = 0, allora tutti i magazzini
    'Se nCodmaga = -1, allora magazzini merce propria
    'Se nCodmaga = -2, allora magazzini merce altrui
    Dim strSQL As String = ""
    Dim dtUlap As Date
    Dim nCodmaga As Integer = oApp.oGvar.nCostoMedioPondTipoMagaz
    Try
      With oCondCommerciali.Input
        '------------------------------------
        If oApp.oGvar.bCostoMedioPondNoStorico Then
          'Legge la data ultimo aggiornamento progressivi
          strSQL = "SELECT tb_dtulap FROM tabanaz WHERE codditt = " & CStrSQL(.strDitta)
          Dim dttTmp As DataTable = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

          If dttTmp.Rows.Count > 0 Then dtUlap = NTSCDate(dttTmp.Rows(0)!tb_dtulap)
          dttTmp.Clear()
        Else
          dtUlap = New Date(1900, 1, 1)
        End If

        '-----------------------------------
        'Creo il recordset che mi restituir il valore della giacenza ad oggi
        strSQL = "SELECT ap_codart, ap_fase, Sum(artpro.ap_esist) AS somma" & _
                " FROM tabmaga " & _
                " INNER JOIN artpro ON tabmaga.codditt = artpro.codditt AND tabmaga.tb_codmaga = artpro.ap_magaz" & _
                " INNER JOIN artico ON artico.codditt = artpro.codditt AND ar_codart = ap_codart "

        If .lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

        strSQL &= " WHERE artpro.codditt = " & CStrSQL(.strDitta) & .strWhereArtico

        If nCodmaga = -1 Then
          strSQL &= " AND (tabmaga.tb_flclavo =' ' OR tabmaga.tb_flclavo ='F')"
        ElseIf nCodmaga = -2 Then
          strSQL &= " AND (tabmaga.tb_flclavo ='C' OR tabmaga.tb_flclavo ='X')"
        ElseIf nCodmaga > 0 Then
          strSQL &= " and artpro.ap_magaz = " & nCodmaga
        End If

        strSQL &= " GROUP BY ap_codart, ap_fase"

        dsTableInMem = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MAGAPRO")

        '------------------------------------
        'Creo il recordset dei carichi valorizzanti
        strSQL = "SELECT km_codart, km_fase, keymag.km_aammgg, Sum(movmag.mm_quant * tabcaum.tb_valoriz) AS Quant, " & _
                 " Sum(movmag.mm_valore * tabcaum.tb_valoriz) AS Valore" & _
                 " FROM tabmaga INNER JOIN (tabcaum INNER JOIN (movmag INNER JOIN keymag ON " & _
                 " (movmag.codditt = keymag.codditt) AND (movmag.mm_riga = keymag.km_riga) AND " & _
                 " (movmag.mm_tipork = keymag.km_tipork) AND (movmag.mm_serie = keymag.km_serie) AND " & _
                 " (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_numdoc = keymag.km_numdoc)) ON " & _
                 " tabcaum.tb_codcaum = keymag.km_causale) ON tabmaga.codditt = keymag.codditt AND " & _
                 " tabmaga.tb_codmaga = keymag.km_magaz " & _
                 " INNER JOIN artico ON artico.codditt = keymag.codditt AND ar_codart = km_codart "

        If .lListaSel <> 0 Then strSQL &= " INNER JOIN listsar ON artico.codditt = listsar.codditt AND artico.ar_codart = listsar.lsa_codart "

        strSQL &= " WHERE keymag.codditt = " & CStrSQL(.strDitta) & _
                  " AND tabcaum.tb_valoriz <> 0" & _
                  " AND tabcaum.tb_vvaloriz <> 0 " & .strWhereArtico

        If nCodmaga = -1 Then
          strSQL &= " AND (tabmaga.tb_flclavo = ' ' OR tabmaga.tb_flclavo = 'F') "
        ElseIf nCodmaga = -2 Then
          strSQL &= " AND (tabmaga.tb_flclavo = 'C' OR tabmaga.tb_flclavo = 'X') "
        ElseIf nCodmaga > 0 Then
          strSQL &= " AND keymag.km_magaz = " & nCodmaga & " "
        End If
        If oApp.oGvar.bCostoMedioPondNoStorico Then
          strSQL &= "AND keymag.km_aammgg > " & CDataSQL(dtUlap.ToShortDateString)
        End If
        strSQL &= " GROUP BY keymag.km_codart, keymag.km_fase, keymag.km_aammgg " & _
                  " ORDER BY keymag.km_aammgg DESC "
      End With

      dsTableInMem = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPROX")

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_PreList(ByVal oCondCommerciali As NTSCondCommerciali, ByVal dtrArtico As DataRow, ByVal strSQLUM As String, ByVal dsTableInMem As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim dtrList As DataRow = Nothing
    Dim strOrderBy As String
    Try
      '------------------------------
      'Cerca il prezzo di listino nella valuta zero (lire od euro...)(prelist  sempre relativo alla valuta zero!!)
      ' da memorizzare in movmag/movord per statistiche sugli sconti medi applicati
      With oCondCommerciali.Input
        If .bNoPrelist = False Then
          If .nListino > 0 Then
            strOrderBy = "lc_datagg DESC, lc_daquant"


            If Not dsTableInMem Is Nothing Then
              'Se ho la tabella in memoria, la query viene fatta sul datatable
              Dim dtrSel() As DataRow = dsTableInMem.Tables("LISTINI").Select(" lc_codart = " & CStrSQL(.strCodart) & _
                                                             " AND lc_conto = 0 AND lc_coddest = 0" & _
                                                             " AND lc_listino = " & .nListino & " AND lc_codvalu = 0 AND lc_codtpro = 0" & _
                                                             " AND lc_fase = " & .nFase & strSQLUM, strOrderBy)

              If dtrSel.Length > 0 Then dtrList = dtrSel(0)
            Else
              'Non ho la tabella in memoria, la query viene fatta sul database
              strSQL = "SELECT TOP 1 lc_progr, lc_prezzo, lc_daquant, lc_aquant, lc_tipo FROM listini" & _
                       " WHERE lc_codart = " & CStrSQL(.strCodart) & _
                       " AND lc_conto = 0" & _
                       " AND lc_coddest = 0" & _
                       " AND lc_codlavo = " & .nCodlavo & _
                       " AND lc_listino = " & .nListino & _
                       " AND lc_codvalu = 0" & _
                       " AND lc_codtpro = 0" & _
                       " AND " & CDataSQL(.dtDatdoc) & " BETWEEN lc_datagg AND lc_datscad " & _
                       " AND " & CDblSQL(.dQuant) & " BETWEEN lc_daquant AND lc_aquant " & _
                       " AND lc_fase = " & .nFase & _
                       " AND lc_codcas = " & CStrSQL(.strCodCas) & _
                       " AND codditt = " & CStrSQL(.strDitta) & _
                       strSQLUM & _
                       " ORDER BY " & strOrderBy

              Dim dsTmp As DataSet = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

              If dsTmp.Tables("LISTINI").Rows.Count > 0 Then dtrList = dsTmp.Tables("LISTINI").Rows(0)
            End If

            If Not dtrList Is Nothing Then
              If Not (NTSCDec(dtrList!lc_daquant) = 0 AndAlso NTSCDec(dtrList!lc_aquant) = 9999999999) Then
                oCondCommerciali.OutputPrezzo.nPerqta = CLN__STD.bsPrSetsiqta
              End If
              oCondCommerciali.OutputPrezzo.dPrelist = NTSCDec(dtrList!lc_prezzo)
              oCondCommerciali.OutputPrezzo.lProgr = NTSCInt(dtrList!lc_progr)
              oCondCommerciali.OutputPrezzo.strTipo = NTSCStr(dtrList!lc_tipo)
            End If
          End If    'If nListino > 0 Then
        End If    'If bNoPrelist = False Then
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_Listino(ByVal oCondCommerciali As NTSCondCommerciali, ByVal dtrArtico As DataRow, ByVal strSQLUM As String, ByVal dsTableInMem As DataSet) As Boolean
    '------------------------------
    ' ora iniza la ricerca del prezzo vero e proprio (SPECIFICO E/O GENERICO)
    ' cerca il prezzo richiesto o ultimo costo (ma nella valuta indicata!!!)
    ' per quel conto , se esiste
    'If nListino > 0 Or lConto > 0 Then 'in questo modo se chiedo un listino minore di 0 (es costo medio) non deve cercare il prezzo spec. per fornitore!!!!
    Dim strSQL As String = ""
    Dim dttResult As New DataTable
    Dim dtrList() As DataRow = Nothing
    Dim strWhere, strOrderBy As String
    Try
      With oCondCommerciali.Input
        If .nListino >= 0 Then
          'Prepara la where e la order by comune
          strWhere = " lc_codart = " & CStrSQL(.strCodart) & _
                     " AND ((lc_conto = " & .lConto & " AND (lc_coddest = 0 OR lc_coddest = " & .lDestdiv & ") AND lc_listino = 0 ) " & _
                     "       OR  " & _
                     "      (lc_conto = 0 AND lc_coddest = 0 AND lc_listino = " & .nListino & ")) " & _
                     " AND lc_codvalu = " & .nCodvalu & _
                     " AND lc_fase = " & .nFase & _
                     CercaCondCommerciali_QueryPromozioni(oCondCommerciali, "lc") & _
                     strSQLUM

          strOrderBy = "lc_conto DESC, lc_coddest DESC, lc_codtpro DESC, lc_datagg DESC, lc_daquant"

          If Not dsTableInMem Is Nothing Then
            'Se ho la tabella in memoria, la query viene fatta sul datatable
            Dim dtrSel() As DataRow = dsTableInMem.Tables("LISTINI").Select(strWhere, strOrderBy)

            If dtrSel.Length > 0 Then
              'Le righe selezionate le copio in un nuovo datatable per poter fare una nuova select
              dttResult = dsTableInMem.Tables("LISTINI").Clone
              For Each dtrTmp As DataRow In dtrSel
                dttResult.Rows.Add(dtrTmp.ItemArray)
                dtrTmp.Delete()
              Next
              dsTableInMem.AcceptChanges()
            End If
          Else
            'Non ho la tabella in memoria, la query viene fatta sul database
            strSQL = "SELECT lc_prezzo, lc_codtpro, lc_daquant, lc_aquant, lc_perqta," & _
                     " lc_unmis, lc_netto, lc_conto, lc_coddest, lc_datagg, lc_datscad, lc_progr, lc_tipo FROM listini" & _
                     " WHERE " & strWhere & _
                     " AND codditt = " & CStrSQL(.strDitta) & _
                     " AND " & CDataSQL(.dtDatdoc) & " BETWEEN lc_datagg AND lc_datscad " & _
                     " AND " & CDblSQL(.dQuant) & " BETWEEN lc_daquant AND lc_aquant " & _
                     " AND lc_codcas = " & CStrSQL(.strCodCas) & _
                     " AND lc_codlavo = " & .nCodlavo & _
                     " ORDER BY " & strOrderBy

            dttResult = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          End If

          If dttResult.Rows.Count > 0 Then
            '------------------------------
            'se voglio solo il prezzo generico o solo lo specifico mi sposto sulla prima riga con lc_conto = 0 o lc_conto <> 0
            If .strTipoval = "G" Then
              dtrList = dttResult.Select("lc_conto = 0", "lc_conto DESC, lc_coddest DESC, lc_codtpro DESC, lc_datagg DESC, lc_daquant")
            ElseIf .strTipoval = "S" Then
              dtrList = dttResult.Select("lc_conto <> 0", "lc_conto DESC, lc_coddest DESC, lc_codtpro DESC, lc_datagg DESC, lc_daquant")
            Else
              'se abilitata, la cercaprezzo lavora come in VB6, ovvero se il prezzo specifico cliente/articolo è = 0 lo ignora e prende il prezzo generico
              If oApp.oGvar.bCercaPrezzoSoloPrezziDiversiDa0 Then
                dtrList = dttResult.Select("lc_prezzo <> 0", "lc_conto DESC, lc_coddest DESC, lc_codtpro DESC, lc_datagg DESC, lc_daquant")
              Else
                dtrList = dttResult.Select(Nothing, "lc_conto DESC, lc_coddest DESC, lc_codtpro DESC, lc_datagg DESC, lc_daquant")
              End If
            End If

            'ora ho solo le righe che mi servono: prendo la prima
            If Not dtrList Is Nothing AndAlso dtrList.Length > 0 Then
              With oCondCommerciali.OutputPrezzo
                .dPrezzo = ArrDbl(NTSCDec(dtrList(0)!lc_prezzo), TrovaNdecSuPrzUn(oCondCommerciali.Input.nCodvalu))
                .nCodpromo = NTSCInt(dtrList(0)!lc_codtpro)
                .strPrzNet = dtrList(0)!lc_netto.ToString
                If NTSCInt(dtrList(0)!lc_conto) <> 0 Then .strTipoval = "S" Else .strTipoval = "G" ' Controllo se è Specifico o Generico
                If Not (NTSCDec(dtrList(0)!lc_daquant) = 0 AndAlso NTSCDec(dtrList(0)!lc_aquant) = 9999999999) Then
                  .nPerqta = CLN__STD.bsPrSetsiqta
                Else
                  .nPerqta = CLN__STD.bsPrSetnoqta
                End If
                .dDaQuant = NTSCDec(dtrList(0)!lc_daquant)
                .dAquant = NTSCDec(dtrList(0)!lc_aquant)
                .dPerqta = NTSCDec(dtrList(0)!lc_perqta)
                .strUnmis = dtrList(0)!lc_unmis.ToString
                .dtDatAgg = NTSCDate(dtrList(0)!lc_datagg)
                .dtAData = NTSCDate(dtrList(0)!lc_datscad)
                .lProgr = NTSCInt(dtrList(0)!lc_progr)
                .strTipo = NTSCStr(dtrList(0)!lc_tipo)
              End With
            End If    'If dttList.Length > 0 Then
          End If    'If dsTmp.Tables("LISTINI").Rows.Count > 0 Then

          If oCondCommerciali.OutputPrezzo.dPrezzo <> 0 Then Return False
          If oCondCommerciali.OutputPrezzo.strTipoval = "S" Then Return False
        End If    'If nListino >= 0 Then
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_Costi(ByVal oCondCommerciali As NTSCondCommerciali, ByVal dtrArtico As DataRow, ByVal strSQLUM As String, ByVal dsTableInMem As DataSet) As Boolean
    Dim dValT As Decimal = 0
    Try
      With oCondCommerciali
        '------------------------------
        'ULTIMO COSTO
        'se richiesto ultimo costo e non trovato prezzo speciale da fornitore, allora cerco in artprox
        If .OutputPrezzo.dPrezzo = 0 AndAlso .Input.nListino = 0 Then
          CercaPrezzo_ArtProx(.Input.strDitta, NTSCStr(dtrArtico!ar_codart), .Input.nFase, .Input.strUnmis, NTSCStr(dtrArtico!ar_confez2), NTSCStr(dtrArtico!ar_unmis2), NTSCDec(dtrArtico!ar_qtacon2), _
                              NTSCDec(dtrArtico!ar_conver), NTSCDec(dtrArtico!ar_perqta), .Input.nCodvalu, .Input.dtDatdoc, "U", .OutputPrezzo.dPrezzo, dsTableInMem)
        End If

        '------------------------------
        'ULTIMO COSTO COMPRENSIVO DI ONERI ACCESSORI
        If .Input.nListino = -1 Then
          CercaPrezzo_ArtProx(.Input.strDitta, NTSCStr(dtrArtico!ar_codart), .Input.nFase, .Input.strUnmis, NTSCStr(dtrArtico!ar_confez2), NTSCStr(dtrArtico!ar_unmis2), NTSCDec(dtrArtico!ar_qtacon2), _
                              NTSCDec(dtrArtico!ar_conver), NTSCDec(dtrArtico!ar_perqta), .Input.nCodvalu, .Input.dtDatdoc, "A", .OutputPrezzo.dPrezzo, dsTableInMem)
        End If

        '------------------------------
        'COSTO MEDIO
        If .Input.nListino = -2 Then
          CercaPrezzo_ArtProx(.Input.strDitta, NTSCStr(dtrArtico!ar_codart), .Input.nFase, .Input.strUnmis, NTSCStr(dtrArtico!ar_confez2), NTSCStr(dtrArtico!ar_unmis2), NTSCDec(dtrArtico!ar_qtacon2), _
                              NTSCDec(dtrArtico!ar_conver), NTSCDec(dtrArtico!ar_perqta), .Input.nCodvalu, .Input.dtDatdoc, "M", .OutputPrezzo.dPrezzo, dsTableInMem)
        End If

        '------------------------------
        ' COSTO FIFO (indip. dal cod conto ..)
        If .Input.nListino = -3 Then  ' costo medio fifo, su mag. zero = tutti
          .OutputPrezzo.dPrezzo = CercaPrezzoFifo(.Input.strDitta, NTSCStr(dtrArtico!ar_codart), 0, 0, .Input.dtDatdoc.ToShortDateString, .Input.dQuant, 0, dValT, .OutputPrezzo.strErr)
          CercaCondCommerciali_ConvertiPrezzo(oCondCommerciali, dtrArtico)
        End If

        '------------------------------
        'COSTO MEDIO PONDERATO ATTUALE (cio determinato sulla esistenza corrente dei magazzini merce propria)
        If .Input.nListino = -4 Then
          .OutputPrezzo.dPrezzo = CercaCostoMedioPond(.Input.strDitta, NTSCStr(dtrArtico!ar_codart), .Input.nFase, oApp.oGvar.nCostoMedioPondTipoMagaz, NTSCDec(dtrArtico!ar_perqta), Nothing, .OutputPrezzo.strErr)
          CercaCondCommerciali_ConvertiPrezzo(oCondCommerciali, dtrArtico)
        End If
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_Valuta(ByVal oCondCommerciali As NTSCondCommerciali, ByVal dtrArtico As DataRow, ByVal strSQLUM As String, ByVal dsTableInMem As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim dtrList As DataRow = Nothing
    Dim dCambio As Decimal
    Dim strWhere, strOrderBy As String
    Try
      With oCondCommerciali
        If .OutputPrezzo.dPrezzo = 0 And .Input.nListino > 0 And .Input.nCodvalu > 0 Then
          strWhere = " lc_codart = " & CStrSQL(.Input.strCodart) & _
                     " AND lc_conto = 0" & _
                     " AND lc_coddest = 0" & _
                     " AND lc_listino = " & .Input.nListino & _
                     " AND lc_codvalu = 0" & _
                     " AND lc_fase = " & .Input.nFase

          If oApp.oGvar.bPromozionicomeRel10 Then
            If Not .Input.bConspromo Then
              strWhere &= " AND lc_codtpro = 0"
            Else
              If .Input.nCodpromo > 0 Then
                strWhere &= " AND lc_codtpro = " & .Input.nCodpromo
              End If
            End If
          Else
            strWhere &= " AND (lc_codtpro = " & .Input.nCodpromo & " OR lc_codtpro = 0)"
          End If
          strWhere &= strSQLUM

          strOrderBy = " lc_codtpro DESC, lc_datagg DESC, lc_daquant"


          If Not dsTableInMem Is Nothing Then
            'Se ho la tabella in memoria, la query viene fatta sul datatable
            Dim dtrSel() As DataRow = dsTableInMem.Tables("LISTINI").Select(strWhere, strOrderBy)

            If dtrSel.Length > 0 Then dtrList = dtrSel(0)
          Else
            'Non ho la tabella in memoria, la query viene fatta sul database
            strSQL = "SELECT TOP 1 lc_prezzo, lc_codtpro, lc_daquant, lc_aquant, lc_perqta," & _
                     "             lc_unmis, lc_fase, lc_netto, lc_datagg, lc_datscad, lc_progr, lc_tipo FROM listini" & _
                     " WHERE " & strWhere & _
                     "   AND codditt = " & CStrSQL(.Input.strDitta) & _
                     "   AND lc_datagg <= " & CDataSQL(.Input.dtDatdoc) & _
                     "   AND lc_datscad >= " & CDataSQL(.Input.dtDatdoc) & _
                     "   AND lc_daquant <= " & CDblSQL(.Input.dQuant) & _
                     "   AND lc_aquant >= " & CDblSQL(.Input.dQuant) & _
                     "   AND lc_codlavo = " & .Input.nCodlavo & _
                     "   AND lc_codcas = " & CStrSQL(.Input.strCodCas) & _
                     " ORDER BY " & strOrderBy

            Dim dsTmp As DataSet = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "LISTINI")

            If dsTmp.Tables("LISTINI").Rows.Count > 0 Then dtrList = dsTmp.Tables("LISTINI").Rows(0)
          End If


          If Not dtrList Is Nothing Then
            .OutputPrezzo.dPrezzo = ArrDbl(NTSCDec(dtrList!lc_prezzo), TrovaNdecSuPrzUn(.Input.nCodvalu))
            .OutputPrezzo.nCodpromo = NTSCInt(dtrList!lc_codtpro)
            .OutputPrezzo.strPrzNet = dtrList!lc_netto.ToString
            If Not (NTSCDec(dtrList!lc_daquant) = 0 And NTSCDec(dtrList!lc_aquant) = 9999999999) Then
              .OutputPrezzo.nPerqta = CLN__STD.bsPrSetsiqta
            Else
              .OutputPrezzo.nPerqta = CLN__STD.bsPrSetnoqta
            End If
            .OutputPrezzo.dDaQuant = NTSCDec(dtrList!lc_daquant)
            .OutputPrezzo.dAquant = NTSCDec(dtrList!lc_aquant)
            .OutputPrezzo.dPerqta = NTSCDec(dtrList!lc_perqta)
            .OutputPrezzo.strUnmis = dtrList!lc_unmis.ToString
            .OutputPrezzo.dtDatAgg = NTSCDate(dtrList!lc_datagg)
            .OutputPrezzo.dtAData = NTSCDate(dtrList!lc_datscad)
            .OutputPrezzo.lProgr = NTSCInt(dtrList!lc_progr)
            .OutputPrezzo.strTipo = NTSCStr(dtrList!lc_tipo)
          End If

          If .OutputPrezzo.dPrezzo <> 0 And .Input.nCodvalu <> 0 Then
            dCambio = CercaCambioDiOggi(.Input.nCodvalu, .Input.dtDatdoc.ToShortDateString)
            If dCambio <> 0 Then
              .OutputPrezzo.dPrezzo = ConvImpEur(True, .OutputPrezzo.dPrezzo, .Input.nCodvalu, .Input.dtDatdoc, dCambio)
            Else
              'Se il cambio di oggi non esiste (dCambio=0) ritorna comuque zero
              .OutputPrezzo.dPrezzo = 0
            End If
          End If
        End If    'If dPrezzo = 0 And nListino > 0 And nCodvalu > 0 Then
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Sub CercaCondCommerciali_ConvertiPrezzo(ByVal oCondCommerciali As NTSCondCommerciali, ByVal dtrArtico As DataRow)
    Try
      With oCondCommerciali
        .OutputPrezzo.dPrezzo = ArrDbl(.OutputPrezzo.dPrezzo, TrovaNdecSuPrzUn(0))
        If .OutputPrezzo.dPrezzo <> 0 And .Input.strUnmis.Trim() <> "" Then
          Select Case .Input.strUnmis
            Case NTSCStr(dtrArtico!ar_confez2)
              .OutputPrezzo.dPrezzo = ArrDbl(.OutputPrezzo.dPrezzo * NTSCDec(dtrArtico!ar_qtacon2), TrovaNdecSuPrzUn(0))
            Case NTSCStr(dtrArtico!ar_unmis2)
              .OutputPrezzo.dPrezzo = ArrDbl(.OutputPrezzo.dPrezzo / NTSCDec(dtrArtico!ar_conver), TrovaNdecSuPrzUn(0))
          End Select
        End If
        ' ora se necessario converte col cambio....
        If .OutputPrezzo.dPrezzo <> 0 And .Input.nCodvalu <> 0 Then
          Dim dCambio As Decimal = CercaCambioDiOggi(.Input.nCodvalu, .Input.dtDatdoc.ToShortDateString)
          If dCambio <> 0 Then
            .OutputPrezzo.dPrezzo = ConvImpEur(True, .OutputPrezzo.dPrezzo, .Input.nCodvalu, .Input.dtDatdoc, dCambio)
          Else
            'Se il cambio di oggi non esiste (dCambio=0) ritorna comuque zero
            .OutputPrezzo.dPrezzo = 0
          End If
        End If
      End With
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CercaCondCommerciali_CercaSconti(ByVal oCondCommerciali As NTSCondCommerciali) As Boolean
    Dim strSQL As String = ""
    Dim bCumulativi As Boolean

    Dim oDatiInput As NTSCondCommerciali.DatiInput = oCondCommerciali.Input
    Try
      With oCondCommerciali
        If Not .bCalcolaSconti Then Return True 'non doveva ricalcolare gli sconti
        'C'è una opzione di registro specifica del retail che va chiamata solo quando il cod. promo è 0.
        If .Input.nCodpromo = -1 Then .Input.nCodpromo = 0 : .Input.bRetail = True

        '-----------------------------
        'Azzera risultati
        With .OutputSconti
          .dSconto1 = 0
          .dSconto2 = 0
          .dSconto3 = 0
          .dSconto4 = 0
          .dSconto5 = 0
          .dSconto6 = 0
          .nPromo = 0
          .nPerqta = CLN__STD.bsPrSetnoqta
          ' setta altre var
          .dDaQuant = 0
          .dAquant = 9999999999
          .strUnmis = ""
          .strTipoval = " "
          .dtDaData = New Date(1900, 1, 1)
          .dtAData = New Date(1900, 1, 1)
        End With

        '-----------------------------
        'se sconti non gestiti esce ...
        'se prezzo netto esce ...
        With .Input
          If oApp.oGvar.strGestioneSconti = "N" Then Return True
          If .strPrzNet = "S" Then Return True

          '-----------------------------
          'Se abilitata l'opzione gbRilevaPrezziDaContoFatt legge il conto di fatturazione
          .lConto = CercaCondCommerciali_ContoFatturazione(oCondCommerciali)

          '-----------------------------
          'se ho impostato che la classe sconto cli viene presa dal n. listino ...
          If .nListino <> 999999999 AndAlso .nListino >= 0 AndAlso oApp.oGvar.strSconClCliDaList = "S" Then .nClscan = .nListino

          '-----------------------------
          ' prende in valore assoluto di dquant
          .dQuant = Math.Abs(.dQuant)
          If .dQuant = 0 Then .dQuant = 1

          '-----------------------------
          'verifica se l'articolo  gestito a varianti .....
          .strCodart = CercaCondCommerciali_CercaSconti_PerVariante(.strDitta, .strCodart)

          ' controlla : se ci sono  sconti aggiuntivi fa un giro pi lungo ...
          bCumulativi = (oApp.oGvar.strTrattaSc1 = "C" OrElse oApp.oGvar.strTrattaSc2 = "C" OrElse oApp.oGvar.strTrattaSc3 = "C" OrElse _
                         oApp.oGvar.strTrattaSc4 = "C" OrElse oApp.oGvar.strTrattaSc5 = "C" OrElse oApp.oGvar.strTrattaSc6 = "C")


          Dim dttSconti As DataTable = CercaCondCommerciali_RitornaListaSconti(oCondCommerciali)

          '-----------------------------
          ' adesso cerca sconto ..
          ' tipo valorizzazione specifica :
          Select Case .strTipoval
            Case "B" : CercaCondCommerciali_CercaSconti_EseguiB(oCondCommerciali, dttSconti, bCumulativi)
            Case "C" : CercaCondCommerciali_CercaSconti_EseguiC(oCondCommerciali, dttSconti, bCumulativi)
            Case "D" : CercaCondCommerciali_CercaSconti_EseguiD(oCondCommerciali, dttSconti, bCumulativi)
            Case "E" : CercaCondCommerciali_CercaSconti_EseguiE(oCondCommerciali, dttSconti, bCumulativi)
            Case "F" : CercaCondCommerciali_CercaSconti_EseguiF(oCondCommerciali, dttSconti, bCumulativi)
            Case Else : CercaCondCommerciali_CercaSconti_EseguiA(oCondCommerciali, dttSconti, bCumulativi)
          End Select
        End With
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      oCondCommerciali.Input = oDatiInput
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_RitornaListaSconti(ByVal oCondCommerciali As NTSCondCommerciali) As DataTable
    Dim strSQL As String = ""
    Try
      With oCondCommerciali.Input
        strSQL = "SELECT * FROM sconti " & _
                 " WHERE codditt = " & CStrSQL(.strDitta) & _
                 "   AND so_codart IN (' ', " & CStrSQL(.strCodart) & ")" & _
                 "   AND so_conto IN (0, " & .lConto & ")" & _
                 "   AND so_coddest IN (0, " & .lDestdiv & ")" & _
                 "   AND so_datagg <= " & CDataSQL(.dtDatdoc) & _
                 "   AND so_datscad >= " & CDataSQL(.dtDatdoc) & _
                 "   AND so_daquant <= " & CDblSQL(.dQuant) & _
                 "   AND so_aquant >= " & CDblSQL(.dQuant) & _
                 CercaCondCommerciali_QueryPromozioni(oCondCommerciali, "so")
      End With

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaSconti_PerVariante(ByVal strDitta As String, ByVal strCodart As String) As String
    Dim strSQL As String = ""
    Dim dttArticolo As DataTable = Nothing
    Try
      '-----------------------------
      'verifica se l'articolo  gestito a varianti .....
      If strCodart <> "" Then
        strSQL = "SELECT ar_gesvar, ar_prevar, ar_codroot, ar_codvar1 FROM artico " & _
                 " WHERE ar_codart = " & CStrSQL(strCodart) & _
                 "   AND codditt = " & CStrSQL(strDitta)
        dttArticolo = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttArticolo.Rows.Count > 0 Then
          If NTSCStr(dttArticolo.Rows(0)!ar_gesvar) = "S" Then ' se  a varianti cambia la base di calcolo ..
            Select Case NTSCStr(dttArticolo.Rows(0)!ar_prevar)
              Case "N" : Return NTSCStr(dttArticolo.Rows(0)!ar_codroot)
              Case "S" : Return strCodart
              Case "1" : Return NTSCStr(dttArticolo.Rows(0)!ar_codroot) & NTSCStr(dttArticolo.Rows(0)!ar_codvar1)
            End Select
          End If
        End If
      End If
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return strCodart
  End Function
  Public Overridable Function CercaCondCommerciali_CercaSconti_EseguiA(ByVal oCondCommerciali As NTSCondCommerciali, ByVal dttSconti As DataTable, ByVal bCumulativi As Boolean) As Boolean
    Try
      ' tratta sconti A : cliente/articolo
      With oCondCommerciali.Input
        If oApp.oGvar.strTrattaScA <> "N" AndAlso .lConto <> 0 Then
          Dim dtrSconti As DataRow() = dttSconti.Select("so_codart = " & CStrSQL(.strCodart) & _
                                                        " AND so_conto = " & .lConto & _
                                                        " AND (so_coddest = 0 OR so_coddest = " & .lDestdiv & ")", _
                                                        "so_codart DESC, so_conto DESC, so_coddest DESC, so_codtpro DESC, so_datagg DESC, so_daquant")
          'se ho sia sconto per liente/destinazione che solo per cliente, prendo solo quello cliente/destinazione!
          If dtrSconti.Length > 0 Then
            CercaCondCommerciali_CercaSconti_ImpostaSconti(oCondCommerciali, dtrSconti(0), "A")

            'se ho settato l'opzione che se ho lo sconto specifico devo ignorare tutti gli altri, anche se cumulativi, esco
            If oApp.oGvar.strSconSpecNoCum = "S" Then Return True
          End If
        End If ' sc A

        If .strTipoval = "A" Then Return True
        If oCondCommerciali.OutputSconti.strTipoval <> " " AndAlso Not bCumulativi Then Return True
        If oApp.oGvar.strPriorSconti = "ACBDFE" Then Return CercaCondCommerciali_CercaSconti_EseguiC(oCondCommerciali, dttSconti, bCumulativi)
      End With

      Return CercaCondCommerciali_CercaSconti_EseguiB(oCondCommerciali, dttSconti, bCumulativi)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaSconti_EseguiB(ByVal oCondCommerciali As NTSCondCommerciali, ByVal dttSconti As DataTable, ByVal bCumulativi As Boolean) As Boolean
    Try
      ' tipo b : articolo/cl.cliente
      With oCondCommerciali.Input
        If oApp.oGvar.strTrattaScB <> "N" AndAlso .nClscan <> 0 Then
          Dim dtrSconti As DataRow() = dttSconti.Select("so_codart = " & CStrSQL(.strCodart) & _
                                                        " AND so_conto = 0 " & _
                                                        " AND so_coddest = 0 " & _
                                                        " AND so_clscan = " & .nClscan, _
                                                        "so_codart DESC, so_clscan DESC, so_codtpro DESC, so_datagg DESC, so_daquant")
          If dtrSconti.Length > 0 Then CercaCondCommerciali_CercaSconti_ImpostaSconti(oCondCommerciali, dtrSconti(0), "B")
        End If ' sc B

        If .strTipoval = "B" Then Return True
        If oCondCommerciali.OutputSconti.strTipoval <> " " AndAlso Not bCumulativi Then Return True
        If oApp.oGvar.strPriorSconti = "ACBDFE" Then Return CercaCondCommerciali_CercaSconti_EseguiD(oCondCommerciali, dttSconti, bCumulativi)
      End With

      Return CercaCondCommerciali_CercaSconti_EseguiC(oCondCommerciali, dttSconti, bCumulativi)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaSconti_EseguiC(ByVal oCondCommerciali As NTSCondCommerciali, ByVal dttSconti As DataTable, ByVal bCumulativi As Boolean) As Boolean
    Try
      ' tratta c : cliente / classe art.
      With oCondCommerciali.Input
        If oApp.oGvar.strTrattaScC <> "N" AndAlso .lConto <> 0 AndAlso .nClscar <> 0 Then
          Dim dtrSconti As DataRow() = dttSconti.Select("so_codart = ' ' " & _
                                                        " AND so_conto = " & .lConto & _
                                                        " AND (so_coddest = 0 OR so_coddest = " & .lDestdiv & ")" & _
                                                        " AND so_clscar = " & .nClscar, _
                                                        "so_conto DESC, so_coddest DESC, so_clscar DESC, so_codtpro DESC, so_datagg DESC, so_daquant")
          'se ho sia sconto per liente/destinazione che solo per cliente, prendo solo quello cliente/destinazione!
          If dtrSconti.Length > 0 Then CercaCondCommerciali_CercaSconti_ImpostaSconti(oCondCommerciali, dtrSconti(0), "C")
        End If ' sc C

        If .strTipoval = "C" Then Return True
        If oCondCommerciali.OutputSconti.strTipoval <> " " AndAlso Not bCumulativi Then Return True
        If oApp.oGvar.strPriorSconti = "ACBDFE" Then Return CercaCondCommerciali_CercaSconti_EseguiB(oCondCommerciali, dttSconti, bCumulativi)
      End With

      Return CercaCondCommerciali_CercaSconti_EseguiD(oCondCommerciali, dttSconti, bCumulativi)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaSconti_EseguiD(ByVal oCondCommerciali As NTSCondCommerciali, ByVal dttSconti As DataTable, ByVal bCumulativi As Boolean) As Boolean
    Try
      ' tipo D : classe art/classe conto
      With oCondCommerciali.Input
        If oApp.oGvar.strTrattaScD <> "N" AndAlso .nClscar <> 0 AndAlso .nClscan <> 0 Then
          Dim dtrSconti As DataRow() = dttSconti.Select("so_codart = ' ' " & _
                                                        " AND so_conto = 0 " & _
                                                        " AND so_coddest = 0" & _
                                                        " AND so_coddest = 0 " & _
                                                        " AND so_clscan = " & .nClscan & _
                                                        " AND so_clscar = " & .nClscar, _
                                                        "so_codart DESC, so_conto DESC, so_coddest DESC, so_clscan DESC, so_clscar DESC, so_codtpro DESC, so_datagg DESC, so_daquant")
          If dtrSconti.Length > 0 Then CercaCondCommerciali_CercaSconti_ImpostaSconti(oCondCommerciali, dtrSconti(0), "D")
        End If ' sc D

        If .strTipoval = "D" Then Return True
        If oCondCommerciali.OutputSconti.strTipoval <> " " AndAlso Not bCumulativi Then Return True
        If oApp.oGvar.strPriorSconti = "ACBDFE" Then Return CercaCondCommerciali_CercaSconti_EseguiF(oCondCommerciali, dttSconti, bCumulativi)
      End With

      Return CercaCondCommerciali_CercaSconti_EseguiE(oCondCommerciali, dttSconti, bCumulativi)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaSconti_EseguiE(ByVal oCondCommerciali As NTSCondCommerciali, ByVal dttSconti As DataTable, ByVal bCumulativi As Boolean) As Boolean
    Dim strSQL As String = ""
    Try
      ' tipo E generico articolo
      With oCondCommerciali.Input
        If oApp.oGvar.strTrattaScA <> "E" Then
          Dim dtrSconti As DataRow() = dttSconti.Select("so_codart = " & CStrSQL(.strCodart) & _
                                                        " AND so_conto = 0 " & _
                                                        " AND so_coddest = 0 " & _
                                                        " AND so_clscan = 0 " & _
                                                        " AND so_clscar = 0 ", _
                                                        "so_codart DESC, so_conto DESC, so_coddest DESC, so_clscan DESC, so_clscar DESC, so_codtpro DESC, so_datagg DESC, so_daquant")
          If dtrSconti.Length > 0 Then CercaCondCommerciali_CercaSconti_ImpostaSconti(oCondCommerciali, dtrSconti(0), "E")
        End If ' sc E

        If .strTipoval = "E" Then Return True
        If oCondCommerciali.OutputSconti.strTipoval <> " " AndAlso Not bCumulativi Then Return True
        If oApp.oGvar.strPriorSconti = "ACBDFE" Then Return True
      End With

      Return CercaCondCommerciali_CercaSconti_EseguiF(oCondCommerciali, dttSconti, bCumulativi)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaSconti_EseguiF(ByVal oCondCommerciali As NTSCondCommerciali, ByVal dttSconti As DataTable, ByVal bCumulativi As Boolean) As Boolean
    Try
      ' tipo F generico cliente ...
      With oCondCommerciali.Input
        If oApp.oGvar.strTrattaScF <> "N" AndAlso .lConto <> 0 Then
          Dim dtrSconti As DataRow() = dttSconti.Select("so_codart = ' ' " & _
                                                        " AND so_conto = " & .lConto & _
                                                        " AND (so_coddest = 0 OR so_coddest = " & .lDestdiv & ")" & _
                                                        " AND so_clscan = 0 " & _
                                                        " AND so_clscar = 0", _
                                                        "so_codart DESC, so_conto DESC, so_coddest DESC, so_clscan DESC, so_clscar DESC, so_codtpro DESC, so_datagg DESC, so_daquant")
          'se ho sia sconto per liente/destinazione che solo per cliente, prendo solo quello cliente/destinazione!
          If dtrSconti.Length > 0 Then CercaCondCommerciali_CercaSconti_ImpostaSconti(oCondCommerciali, dtrSconti(0), "F")
        End If ' sc F
        If oApp.oGvar.strPriorSconti = "ACBDFE" Then Return CercaCondCommerciali_CercaSconti_EseguiE(oCondCommerciali, dttSconti, bCumulativi)
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaSconti_ImpostaSconti(ByVal oCondCommerciali As NTSCondCommerciali, ByVal dtrSconti As DataRow, ByVal strTipo As String) As Boolean
    Dim strSQL As String = ""
    Try
      With oCondCommerciali.OutputSconti
        If .strTipoval = " " OrElse strTipo = "A" Then
          If oApp.oGvar.strTrattaSc1 = "S" Then .dSconto1 = NTSCDec(dtrSconti!so_scont1) Else .dSconto1 += NTSCDec(dtrSconti!so_scont1)
          If oApp.oGvar.strTrattaSc2 = "S" Then .dSconto2 = NTSCDec(dtrSconti!so_scont2) Else .dSconto2 += NTSCDec(dtrSconti!so_scont2)
          If oApp.oGvar.strTrattaSc3 = "S" Then .dSconto3 = NTSCDec(dtrSconti!so_scont3) Else .dSconto3 += NTSCDec(dtrSconti!so_scont3)
          If oApp.oGvar.strTrattaSc4 = "S" Then .dSconto4 = NTSCDec(dtrSconti!so_scont4) Else .dSconto4 += NTSCDec(dtrSconti!so_scont4)
          If oApp.oGvar.strTrattaSc5 = "S" Then .dSconto5 = NTSCDec(dtrSconti!so_scont5) Else .dSconto5 += NTSCDec(dtrSconti!so_scont5)
          If oApp.oGvar.strTrattaSc6 = "S" Then .dSconto6 = NTSCDec(dtrSconti!so_scont6) Else .dSconto6 += NTSCDec(dtrSconti!so_scont6)
          '--------------------------------------
          CercaCondCommerciali_CercaSconti_DescrCalcolo(dtrSconti, NTSCDec(dtrSconti!so_scont1), strTipo, .strDescrCalcoloScont1)
          CercaCondCommerciali_CercaSconti_DescrCalcolo(dtrSconti, NTSCDec(dtrSconti!so_scont2), strTipo, .strDescrCalcoloScont2)
          CercaCondCommerciali_CercaSconti_DescrCalcolo(dtrSconti, NTSCDec(dtrSconti!so_scont3), strTipo, .strDescrCalcoloScont3)
          CercaCondCommerciali_CercaSconti_DescrCalcolo(dtrSconti, NTSCDec(dtrSconti!so_scont4), strTipo, .strDescrCalcoloScont4)
          CercaCondCommerciali_CercaSconti_DescrCalcolo(dtrSconti, NTSCDec(dtrSconti!so_scont5), strTipo, .strDescrCalcoloScont5)
          CercaCondCommerciali_CercaSconti_DescrCalcolo(dtrSconti, NTSCDec(dtrSconti!so_scont6), strTipo, .strDescrCalcoloScont6)
          '--------------------------------------
          .nPromo = NTSCInt(dtrSconti!so_codtpro)
          If Not (NTSCDec(dtrSconti!so_daquant) = 0 And NTSCDec(dtrSconti!so_aquant) = 9999999999) Then
            .nPerqta = CLN__STD.bsPrSetsiqta
          End If
          .dDaQuant = NTSCDec(dtrSconti!so_daquant)
          .dAquant = NTSCDec(dtrSconti!so_aquant)
          .strUnmis = dtrSconti!so_unmis.ToString
          .strTipoval = strTipo
          .dtDaData = NTSCDate(dtrSconti!so_datagg)
          .dtAData = NTSCDate(dtrSconti!so_datscad)
        Else
          If oApp.oGvar.strTrattaSc1 = "C" Then .dSconto1 += NTSCDec(dtrSconti!so_scont1) : CercaCondCommerciali_CercaSconti_DescrCalcolo(dtrSconti, NTSCDec(dtrSconti!so_scont1), strTipo, .strDescrCalcoloScont1)
          If oApp.oGvar.strTrattaSc2 = "C" Then .dSconto2 += NTSCDec(dtrSconti!so_scont2) : CercaCondCommerciali_CercaSconti_DescrCalcolo(dtrSconti, NTSCDec(dtrSconti!so_scont2), strTipo, .strDescrCalcoloScont2)
          If oApp.oGvar.strTrattaSc3 = "C" Then .dSconto3 += NTSCDec(dtrSconti!so_scont3) : CercaCondCommerciali_CercaSconti_DescrCalcolo(dtrSconti, NTSCDec(dtrSconti!so_scont3), strTipo, .strDescrCalcoloScont3)
          If oApp.oGvar.strTrattaSc4 = "C" Then .dSconto4 += NTSCDec(dtrSconti!so_scont4) : CercaCondCommerciali_CercaSconti_DescrCalcolo(dtrSconti, NTSCDec(dtrSconti!so_scont4), strTipo, .strDescrCalcoloScont4)
          If oApp.oGvar.strTrattaSc5 = "C" Then .dSconto5 += NTSCDec(dtrSconti!so_scont5) : CercaCondCommerciali_CercaSconti_DescrCalcolo(dtrSconti, NTSCDec(dtrSconti!so_scont5), strTipo, .strDescrCalcoloScont5)
          If oApp.oGvar.strTrattaSc6 = "C" Then .dSconto6 += NTSCDec(dtrSconti!so_scont6) : CercaCondCommerciali_CercaSconti_DescrCalcolo(dtrSconti, NTSCDec(dtrSconti!so_scont6), strTipo, .strDescrCalcoloScont6)
        End If
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Sub CercaCondCommerciali_CercaSconti_DescrCalcolo(ByVal dtrSconti As DataRow, ByVal dScontoTrovato As Decimal, ByVal strTipo As String, _
                                                                    ByRef strDescrSconto As String)
    Try
      If dScontoTrovato = 0 Then Return

      If strDescrSconto <> "" Then strDescrSconto &= " + "
      strDescrSconto &= dScontoTrovato & "% "

      Select Case strTipo
        Case "A" : strDescrSconto &= oApp.Tr(Me, 130552477135018609, "specifico cliente\articolo")
        Case "B" : strDescrSconto &= oApp.Tr(Me, 130552477713640424, "per articolo\classe conto")
        Case "C" : strDescrSconto &= oApp.Tr(Me, 130552478441922597, "per cliente\classe articolo")
        Case "D" : strDescrSconto &= oApp.Tr(Me, 130552478504124046, "per classe articolo\classe conto")
        Case "E" : strDescrSconto &= oApp.Tr(Me, 130552478524195235, "generico per l'articolo")
        Case "F" : strDescrSconto &= oApp.Tr(Me, 130552478548208935, "generico per il cliente")
      End Select

      If Not (NTSCDec(dtrSconti!so_daquant) = 0 And NTSCDec(dtrSconti!so_aquant) = 9999999999) Then
        strDescrSconto &= oApp.Tr(Me, 130552479781462907, ", per lo scaglione di quantità da |" & NTSCDec(dtrSconti!so_daquant) & "| a |" & NTSCDec(dtrSconti!so_aquant) & "|")
      End If
      If NTSCInt(dtrSconti!so_codtpro) <> 0 Then
        strDescrSconto &= oApp.Tr(Me, 130552483947946121, ", sulla promozione |" & NTSCInt(dtrSconti!so_codtpro) & "|")
      End If
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub


  Public Overridable Function CercaCondCommerciali_CercaProvvigioni(ByVal oCondCommerciali As NTSCondCommerciali) As Boolean
    ' NB : allo stato non ritorna tipovalout, n applica priorità come invece negli sconti ...
    ' e non testa il tipo valorizzazione in input
    Dim strSQL As String = ""
    Dim strTipo() As String

    Dim oDatiInput As NTSCondCommerciali.DatiInput = oCondCommerciali.Input
    Dim bRetail As Boolean = False
    Dim strSQLUM As String = ""
    Try
      'C'è una opzione di registro specifica del retail che va chiamata solo quando il cod. promo è 0.
      With oCondCommerciali
        If .Input.nCodpromo = -1 Then .Input.nCodpromo = 0 : .Input.bRetail = True
        '----------------------------------------
        ' NB : allo stato non ritorna tipovalout, n applica priorità come invece negli sconti ...
        ' e non testa il tipo valorizzazione in input
        '----------------------------------------
        'Azzera risultati
        With .OutputProvvAgente1
          .strError = ""
          .dProvv = 0
          .dVprovv = 0
          .strTipoval = " "
          .nPromo = 0
          .strDescrCalcolo = ""
        End With
        With .OutputProvvAgente2
          .strError = ""
          .dProvv = 0
          .dVprovv = 0
          .strTipoval = " "
          .nPromo = 0
          .strDescrCalcolo = ""
        End With

        With .Input
          ' se provv non gestiti esce ...
          If .nCodage1 = 0 AndAlso .nCodage2 = 0 Then Return True

          If oApp.oGvar.strGestioneProvv = "N" Then Return True
          '----------------------------------------
          'Ignora gli sconti di riga se richiesto con le apposite ozpioni di registro
          If oApp.oGvar.bIgnoraScontR1CercaProvv Then .dSconto1 = 0
          If oApp.oGvar.bIgnoraScontR2CercaProvv Then .dSconto2 = 0
          If oApp.oGvar.bIgnoraScontR3CercaProvv Then .dSconto3 = 0
          If oApp.oGvar.bIgnoraScontR4CercaProvv Then .dSconto4 = 0
          If oApp.oGvar.bIgnoraScontR5CercaProvv Then .dSconto5 = 0
          If oApp.oGvar.bIgnoraScontR6CercaProvv Then .dSconto6 = 0
          If oApp.oGvar.bIgnoraScontT1CercaProvv Then .dScontT1 = 0
          If oApp.oGvar.bIgnoraScontT2CercaProvv Then .dScontT2 = 0

          '----------------------------------------
          'Cerca se esiste il codice dell'agente produttore tipo, nel caso in cui esiste
          'tutte le provvigioni vengono fatte con il codice dell'agente tipo.
          CercaCondCommerciali_CercaAgenteTipo(oCondCommerciali)

          '----------------------------------------
          ' cerca prima se articolo a variante e la sua UMP
          If .strCodart <> "" Then
            strSQL = "SELECT ar_gesvar, ar_prevar, ar_codroot, ar_codvar1, ar_unmis FROM artico " & _
                     " WHERE ar_codart = " & CStrSQL(.strCodart) & _
                     "   AND codditt = " & CStrSQL(.strDitta) & " "
            Dim dttArt As DataTable = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
            If dttArt.Rows.Count <> 0 Then
              If NTSCStr(dttArt.Rows(0)!ar_gesvar) = "S" Then ' se  a varianti cambia la base di calcolo ..
                Select Case NTSCStr(dttArt.Rows(0)!ar_prevar)
                  Case "N" : .strCodart = NTSCStr(dttArt.Rows(0)!ar_codroot)
                  Case "1" : .strCodart = NTSCStr(dttArt.Rows(0)!ar_codroot) & NTSCStr(dttArt.Rows(0)!ar_codvar1)
                    'Case "S" : .strCodart = .strCodart
                End Select
              End If

              'unità di misura sui prezzi
              'Il controllo sotto era inutile! Non esiste alcuna chiamata che passi l'unità di misura al metodo.
              'A causa dell'unificazione della 2015, l'unità di misura arriva qui perchè impostata per la ricerca di prezzi e sconti.
              'If .strUnmis.Trim = "" Then
              strSQLUM = " And (lc_unmis = ' ' Or lc_unmis = " & CStrSQL(NTSCStr(dttArt.Rows(0)!ar_unmis)) & ")"
              'Else
              ' strSQLUM = " And lc_unmis = " & CStrSQL(.strUnmis)
              'End If
            End If
          End If
          '----------------------------------------
          ' adesso cerca sconto ..
          'Cerca il tipo di gestione dell'agente
          strTipo = CercaCondCommerciali_CercaGestioneAgente(oCondCommerciali)

          'Carica tutti i possibili dati delle condizioni commerciali
          Dim dttProvv As DataTable = CercaCondCommerciali_TrovaDati(oCondCommerciali)

          'Esegue il calcolo per entrambi gli agenti
          For i As Integer = 0 To 1
            ' se provv non gestiti esce ...
            If (i = 0 AndAlso .nCodage1 = 0) OrElse (i = 1 AndAlso .nCodage2 = 0) Then Continue For

            ' strTipo fornisce la priorità tra articoli (ABCDEF) e cliente (ACBDFE)
            If strTipo(i) = "T" And Not oApp.oGvar.bScPrconDerogaCliArt Then CercaCondCommerciali_CercaProvvigioni_RelScPr(oCondCommerciali, i, strSQLUM) : Continue For
            ' adesso il caso normale ....
            '------------------------------------
            Select Case .strTipoval
              Case "B" : CercaCondCommerciali_CercaProvvigioni_EseguiB(oCondCommerciali, i, strTipo(i), dttProvv)
              Case "C" : CercaCondCommerciali_CercaProvvigioni_EseguiC(oCondCommerciali, i, strTipo(i), dttProvv)
              Case "D" : CercaCondCommerciali_CercaProvvigioni_EseguiD(oCondCommerciali, i, strTipo(i), dttProvv)
              Case "E" : CercaCondCommerciali_CercaProvvigioni_EseguiE(oCondCommerciali, i, strTipo(i), dttProvv)
              Case "F" : CercaCondCommerciali_CercaProvvigioni_EseguiF(oCondCommerciali, i, strTipo(i), dttProvv)
              Case "G" : CercaCondCommerciali_CercaProvvigioni_EseguiG(oCondCommerciali, i, strTipo(i), dttProvv) ' solo agente
              Case Else : CercaCondCommerciali_CercaProvvigioni_EseguiA(oCondCommerciali, i, strTipo(i), dttProvv)
            End Select

            If strTipo(i) = "T" AndAlso oApp.oGvar.bScPrconDerogaCliArt Then
              'Se non è stata rilevata una provvigione 'classica' (prioritaria rispetto alla relazione
              'conto/provvigioni quando gbScPrconDerogaCliArt=True) ossia strTipovalOut = " " il programma
              'va a leggere le provvigioni a relazioni sconti/provv. Testando strTipovalOut = " " viene coperto
              'anche il caso in cui l'operatore va a definire una provvigione 'classica' con percentuale/valore
              'provvigione pari a zero
              'If dProvv = 0 And dVprovv = 0 Then
              If i = 0 Then
                If oCondCommerciali.OutputProvvAgente1.strTipoval = " " Then If Not CercaCondCommerciali_CercaProvvigioni_RelScPr(oCondCommerciali, i, strSQLUM) Then Return False
              Else
                If oCondCommerciali.OutputProvvAgente2.strTipoval = " " Then If Not CercaCondCommerciali_CercaProvvigioni_RelScPr(oCondCommerciali, i, strSQLUM) Then Return False
              End If
            End If
          Next
        End With
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      oCondCommerciali.Input = oDatiInput
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaAgenteTipo(ByVal oCondCommerciali As NTSCondCommerciali) As Boolean
    Dim strSQL As String = ""
    Try
      With oCondCommerciali.Input
        '----------------------------------------
        'Cerca se esiste il codice dell'agente produttore tipo, nel caso in cui esiste
        'tutte le provvigioni vengono fatte con il codice dell'agente tipo.
        strSQL = "SELECT tb_codcage, tb_agetipodir FROM tabcage " & _
                 " WHERE codditt = " & CStrSQL(.strDitta) & _
                 "   AND tb_codcage IN (" & .nCodage1 & ", " & .nCodage1 & ")"
        Dim dttTmp As DataTable = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          If .nCodage1 <> 0 Then
            Dim dtrRow() As DataRow = dttTmp.Select("tb_codcage = " & .nCodage1)
            If dtrRow.Length > 0 AndAlso NTSCInt(dtrRow(0)!tb_agetipodir) > 0 Then .nCodage1 = NTSCInt(dtrRow(0)!tb_agetipodir)
          End If
          If .nCodage2 <> 0 Then
            Dim dtrRow() As DataRow = dttTmp.Select("tb_codcage = " & .nCodage2)
            If dtrRow.Length > 0 AndAlso NTSCInt(dtrRow(0)!tb_agetipodir) > 0 Then .nCodage2 = NTSCInt(dtrRow(0)!tb_agetipodir)
          End If
        End If
      End With
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaGestioneAgente(ByVal oCondCommerciali As NTSCondCommerciali) As String()
    Dim strSQL As String = ""
    Dim strTipo(2) As String
    Try
      With oCondCommerciali.Input
        'Cerca il tipo di gestione dell'agente
        strSQL = "SELECT tb_codcage, tb_tiptab FROM tabcage " & _
                 " WHERE tb_codcage IN (" & .nCodage1 & ", " & .nCodage2 & ") " & _
                 "   AND codditt = " & CStrSQL(.strDitta)
        Dim dttTipo As DataTable = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTipo.Rows.Count > 0 Then
          If .nCodage1 <> 0 Then
            Dim dtrRow() As DataRow = dttTipo.Select("tb_codcage = " & .nCodage1)
            If dtrRow.Length > 0 Then strTipo(0) = NTSCStr(dtrRow(0)!tb_tiptab)
          End If
          If .nCodage2 <> 0 Then
            Dim dtrRow() As DataRow = dttTipo.Select("tb_codcage = " & .nCodage2)
            If dtrRow.Length > 0 Then strTipo(1) = NTSCStr(dtrRow(0)!tb_tiptab)
          End If
        End If
      End With
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return strTipo
  End Function
  Public Overridable Function CercaCondCommerciali_CercaProvvigioni_EseguiA(ByVal oCondCommerciali As NTSCondCommerciali, ByVal lPos As Integer, _
                                                                            ByVal strTipo As String, ByVal dttProvvigioni As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dtrProv() As DataRow
    Dim nCodage As Integer
    Dim strTipovalout As String = ""
    Try
      With oCondCommerciali
        nCodage = CercaCondCommerciali_CercaProvvigioni_RitornaAgenteCorrente(oCondCommerciali, lPos)
        '----------------------------------------------------------------------
        ' tratta provvigioni A
        If oApp.oGvar.strTrattaPrA <> "N" And .Input.lConto <> 0 And .Input.strCodart.Trim <> "" Then
          With .Input
            dtrProv = dttProvvigioni.Select("(per_codcage = " & nCodage & " OR per_codcage = 0) " & _
                                            " AND per_codart = " & CStrSQL(.strCodart) & _
                                            " AND per_conto = " & .lConto & _
                                            " AND (per_coddest = 0 OR per_coddest = " & .lDestdiv & ")" & _
                                            " AND per_datagg <= " & CDataSQL(.dtDatdoc) & _
                                            " AND per_datscad >= " & CDataSQL(.dtDatdoc) & _
                                            " AND codditt = " & CStrSQL(.strDitta), _
                                            "per_codcage DESC, per_codtpro DESC, per_coddest DESC, per_datagg DESC")
          End With

          If dtrProv.Length > 0 Then
            CercaCondCommerciali_CercaProvvigioni_ImpostaValoriRitorno(oCondCommerciali, lPos, dtrProv(0), "A")
            strTipovalout = "A"
          End If
        End If ' tratta provvigioni A
        '---------------
        If .Input.strTipoval = "A" Then Return True
        If strTipovalout = "A" Then Return True
        If strTipo = "C" Then Return CercaCondCommerciali_CercaProvvigioni_EseguiC(oCondCommerciali, lPos, strTipo, dttProvvigioni)
      End With

      Return CercaCondCommerciali_CercaProvvigioni_EseguiB(oCondCommerciali, lPos, strTipo, dttProvvigioni)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaProvvigioni_EseguiB(ByVal oCondCommerciali As NTSCondCommerciali, ByVal lPos As Integer, _
                                                                            ByVal strTipo As String, ByVal dttProvvigioni As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dtrProv() As DataRow
    Dim nCodage As Integer
    Dim strTipovalout As String = ""
    Try
      With oCondCommerciali
        nCodage = CercaCondCommerciali_CercaProvvigioni_RitornaAgenteCorrente(oCondCommerciali, lPos)
        '----------------------------------------------------------------------
        ' tratta provvigioni B
        If oApp.oGvar.strTrattaPrB <> "N" And .Input.nClpran <> 0 And .Input.strCodart.Trim <> "" Then
          With .Input
            dtrProv = dttProvvigioni.Select("(per_codcage = " & nCodage & " OR per_codcage = 0)" & _
                                            " AND per_codart = " & CStrSQL(.strCodart) & _
                                            " AND per_clpran = " & .nClpran & _
                                            " AND per_conto = 0 ", _
                                            "per_codcage DESC, per_codtpro DESC, per_datagg DESC")
          End With

          If dtrProv.Length > 0 Then
            CercaCondCommerciali_CercaProvvigioni_ImpostaValoriRitorno(oCondCommerciali, lPos, dtrProv(0), "B")
            strTipovalout = "B"
          End If
        End If ' tratta pr B

        If .Input.strTipoval = "B" Then Return True
        If strTipovalout = "B" Then Return True
        If strTipo = "C" Then Return CercaCondCommerciali_CercaProvvigioni_EseguiD(oCondCommerciali, lPos, strTipo, dttProvvigioni)
      End With

      Return CercaCondCommerciali_CercaProvvigioni_EseguiC(oCondCommerciali, lPos, strTipo, dttProvvigioni)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaProvvigioni_EseguiC(ByVal oCondCommerciali As NTSCondCommerciali, ByVal lPos As Integer, _
                                                                            ByVal strTipo As String, ByVal dttProvvigioni As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dtrProv() As DataRow
    Dim nCodage As Integer
    Dim strTipovalout As String = ""
    Try
      ' cliente/classe articolo
      With oCondCommerciali
        nCodage = CercaCondCommerciali_CercaProvvigioni_RitornaAgenteCorrente(oCondCommerciali, lPos)
        '----------------------------------------------------------------------
        ' tratta provvigioni C
        If oApp.oGvar.strTrattaPrC <> "N" And .Input.nClprar <> 0 And .Input.lConto <> 0 Then
          With .Input
            dtrProv = dttProvvigioni.Select("(per_codcage = " & nCodage & " OR per_codcage = 0)" & _
                                            " AND per_codart = ' ' " & _
                                            " AND per_clprar = " & .nClprar & _
                                            " AND per_conto = " & .lConto & _
                                            " AND (per_coddest = 0 OR per_coddest = " & .lDestdiv & ")", _
                                            " per_codcage DESC, per_codtpro DESC, per_coddest DESC, per_datagg DESC")
          End With

          If dtrProv.Length > 0 Then
            CercaCondCommerciali_CercaProvvigioni_ImpostaValoriRitorno(oCondCommerciali, lPos, dtrProv(0), "C")
            strTipovalout = "C"
          End If
        End If ' tratta pr C

        If .Input.strTipoval = "C" Then Return True
        If strTipovalout = "C" Then Return True
        If strTipo = "C" Then Return CercaCondCommerciali_CercaProvvigioni_EseguiB(oCondCommerciali, lPos, strTipo, dttProvvigioni)
      End With

      Return CercaCondCommerciali_CercaProvvigioni_EseguiD(oCondCommerciali, lPos, strTipo, dttProvvigioni)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaProvvigioni_EseguiD(ByVal oCondCommerciali As NTSCondCommerciali, ByVal lPos As Integer, _
                                                                            ByVal strTipo As String, ByVal dttProvvigioni As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dtrProv() As DataRow
    Dim nCodage As Integer
    Dim strTipovalout As String = ""
    Try
      ' classe cliente/classe articolo
      With oCondCommerciali
        nCodage = CercaCondCommerciali_CercaProvvigioni_RitornaAgenteCorrente(oCondCommerciali, lPos)
        '----------------------------------------------------------------------
        ' tratta provvigioni D
        If oApp.oGvar.strTrattaPrD <> "N" And .Input.nClprar <> 0 And .Input.nClpran <> 0 Then
          With .Input
            dtrProv = dttProvvigioni.Select("(per_codcage = " & nCodage & " OR per_codcage = 0)" & _
                                            " AND per_codart = ' ' " & _
                                            " AND per_clprar = " & .nClprar & _
                                            " AND per_clpran = " & .nClpran & _
                                            " AND per_conto = 0", _
                                            "per_codcage DESC, per_codtpro DESC, per_datagg DESC")
          End With

          If dtrProv.Length > 0 Then
            CercaCondCommerciali_CercaProvvigioni_ImpostaValoriRitorno(oCondCommerciali, lPos, dtrProv(0), "D")
            strTipovalout = "D"
          End If
        End If ' tratta pr D
        If .Input.strTipoval = "D" Then Return True
        If strTipovalout = "D" Then Return True
        If strTipo = "C" Then Return CercaCondCommerciali_CercaProvvigioni_EseguiF(oCondCommerciali, lPos, strTipo, dttProvvigioni)
      End With

      Return CercaCondCommerciali_CercaProvvigioni_EseguiE(oCondCommerciali, lPos, strTipo, dttProvvigioni)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaProvvigioni_EseguiE(ByVal oCondCommerciali As NTSCondCommerciali, ByVal lPos As Integer, _
                                                                            ByVal strTipo As String, ByVal dttProvvigioni As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dtrProv() As DataRow
    Dim nCodage As Integer
    Dim strTipovalout As String = ""
    Try
      ' generico articolo
      With oCondCommerciali
        nCodage = CercaCondCommerciali_CercaProvvigioni_RitornaAgenteCorrente(oCondCommerciali, lPos)
        '----------------------------------------------------------------------
        ' tratta provvigioni E
        If oApp.oGvar.strTrattaPrE <> "N" And .Input.strCodart <> " " Then
          With .Input
            dtrProv = dttProvvigioni.Select("(per_codcage = " & nCodage & " OR per_codcage = 0)" & _
                                            " AND per_codart = " & CStrSQL(.strCodart) & _
                                            " AND per_clprar = 0 " & _
                                            " AND per_clpran = 0 " & _
                                            " AND per_conto = 0", _
                                            "per_codcage DESC, per_codtpro DESC, per_datagg DESC")
          End With

          If dtrProv.Length > 0 Then
            CercaCondCommerciali_CercaProvvigioni_ImpostaValoriRitorno(oCondCommerciali, lPos, dtrProv(0), "E")
            strTipovalout = "E"
          End If
        End If ' tratta pr E
        If .Input.strTipoval = "E" Then Return True
        If strTipovalout = "E" Then Return True
        If strTipo = "C" Then Return CercaCondCommerciali_CercaProvvigioni_EseguiG(oCondCommerciali, lPos, strTipo, dttProvvigioni)
      End With

      Return CercaCondCommerciali_CercaProvvigioni_EseguiF(oCondCommerciali, lPos, strTipo, dttProvvigioni)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaProvvigioni_EseguiF(ByVal oCondCommerciali As NTSCondCommerciali, ByVal lPos As Integer, _
                                                                            ByVal strTipo As String, ByVal dttProvvigioni As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dtrProv() As DataRow
    Dim nCodage As Integer
    Dim strTipovalout As String = ""
    Try
      ' generico conto
      With oCondCommerciali
        nCodage = CercaCondCommerciali_CercaProvvigioni_RitornaAgenteCorrente(oCondCommerciali, lPos)
        '----------------------------------------------------------------------
        ' tratta provvigioni F
        If oApp.oGvar.strTrattaPrF <> "N" And .Input.lConto <> 0 Then
          With .Input
            dtrProv = dttProvvigioni.Select("(per_codcage = " & nCodage & " OR per_codcage = 0)" & _
                                            " AND per_codart = ' ' " & _
                                            " AND per_clprar = 0 " & _
                                            " AND per_clpran = 0 " & _
                                            " AND per_conto = " & .lConto & _
                                            " AND (per_coddest = 0 OR per_coddest = " & .lDestdiv & ")", _
                                            " per_codcage DESC, per_codtpro DESC, per_coddest DESC, per_datagg DESC")
          End With

          If dtrProv.Length > 0 Then
            CercaCondCommerciali_CercaProvvigioni_ImpostaValoriRitorno(oCondCommerciali, lPos, dtrProv(0), "F")
            strTipovalout = "F"
          End If

        End If ' tratta pr F
        If .Input.strTipoval = "F" Then Return True
        If strTipovalout = "F" Then Return True
        If strTipo = "C" Then Return CercaCondCommerciali_CercaProvvigioni_EseguiE(oCondCommerciali, lPos, strTipo, dttProvvigioni)
      End With

      Return CercaCondCommerciali_CercaProvvigioni_EseguiG(oCondCommerciali, lPos, strTipo, dttProvvigioni)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaProvvigioni_EseguiG(ByVal oCondCommerciali As NTSCondCommerciali, ByVal lPos As Integer, _
                                                                            ByVal strTipo As String, ByVal dttProvvigioni As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dtrProv() As DataRow
    Dim nCodage As Integer
    Dim strTipovalout As String = ""
    Try
      ' generico agente
      With oCondCommerciali
        nCodage = CercaCondCommerciali_CercaProvvigioni_RitornaAgenteCorrente(oCondCommerciali, lPos)
        '----------------------------------------------------------------------
        ' tratta provvigioni G
        dtrProv = dttProvvigioni.Select("per_codcage = " & nCodage & _
                                        " AND per_codart = ' ' " & _
                                        " AND per_clprar = 0 " & _
                                        " AND per_clpran = 0 " & _
                                        " AND per_conto = 0 ", _
                                        "per_codcage DESC, per_codtpro DESC, per_datagg DESC")

        If dtrProv.Length > 0 Then
          CercaCondCommerciali_CercaProvvigioni_ImpostaValoriRitorno(oCondCommerciali, lPos, dtrProv(0), "G")
          strTipovalout = "G"
        End If
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaProvvigioni_RelScPr(ByVal oCondCommerciali As NTSCondCommerciali, ByVal lPos As Integer, _
                                                                            ByVal strSQLUM As String) As Boolean
    '----------------------------------------------------------------------
    'Provvigioni in base agli sconti
    'Calcola il prezzo di vendita
    Dim dPrezzoApplicato As Decimal
    Dim dPrezzoListino As Decimal
    Dim nLisscpr As Integer
    Dim dttScpr, dttTmp As DataTable
    Dim dScontoApplicato As Decimal

    Dim dQuantabs As Decimal
    Dim strCodScpr As String = ""

    Dim strSQL As String = ""
    Dim strOpzscpr As String
    Dim nCodage As Integer = CercaCondCommerciali_CercaProvvigioni_RitornaAgenteCorrente(oCondCommerciali, lPos)
    Dim dProvv As Decimal
    Dim strError As String = ""
    Try
      With oCondCommerciali
        With .Input
          strOpzscpr = GetSettingBusDitt(.strDitta, "Bsveboll", "OPZIONI", ".", "Opzscpr", "ClasseArticolo", "strDipDoc", "ClasseArticolo")

          dPrezzoApplicato = .dPrezzo * (100 - .dSconto1) / 100 * (100 - .dSconto2) / 100 * (100 - .dSconto3) / 100 * (100 - .dSconto4) / 100 * _
                                        (100 - .dSconto5) / 100 * (100 - .dSconto6) / 100 * (100 - .dScontT1) / 100 * (100 - .dScontT2) / 100
          strSQL = "SELECT * FROM tabscpr WHERE tb_codscpr = "
          Select Case strOpzscpr
            Case "CodiceAgente"
              strSQL &= nCodage
              strCodScpr = NTSCStr(nCodage)
            Case "ClasseArticolo"
              strSQL &= .nClprar
              strCodScpr = NTSCStr(.nClprar)
            Case "AgenteClasseArticolo", "AgenteClasseArticoloAAAACC"
              strSQL &= NTSCStr(nCodage) & .nClprar.ToString("00")
              strCodScpr = NTSCStr(nCodage) & .nClprar.ToString("00")
            Case "AgenteClasseArticoloAAAC", "AgenteClasseArticoloAAAAC"
              strSQL &= NTSCStr(nCodage) & .nClprar.ToString("0")
              strCodScpr = NTSCStr(nCodage) & .nClprar.ToString("0")
            Case "AgenteClasseArticoloACCC", "AgenteClasseArticoloAAAACCC"
              strSQL &= NTSCStr(nCodage) & .nClprar.ToString("000")
              strCodScpr = NTSCStr(nCodage) & .nClprar.ToString("000")
            Case "AgentePromozioneAAAAPPP"
              strSQL &= nCodage & .nCodpromo.ToString("000")
              strCodScpr = nCodage.ToString() & .nCodpromo.ToString("000")
            Case "AgenteClassePromozioneAAACCCPPP"
              strSQL &= NTSCStr(nCodage) & .nClprar.ToString("000") & .nCodpromo.ToString("000")
              strCodScpr = NTSCStr(nCodage) & .nClprar.ToString("000") & .nCodpromo.ToString("000")
            Case Else
              strError = oApp.Tr(Me, 129110401076875000, "Opzione di registro 'Bsveboll\Opzioni\Opzscpr' con valore '|" & strOpzscpr & "|' non ammesso. Provvigioni per l'agente |" & nCodage & "| non rilevate.")
              Return True
          End Select
          strSQL &= " AND codditt = " & CStrSQL(.strDitta)
          dttScpr = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
        If dttScpr.Rows.Count > 0 Then
          nLisscpr = NTSCInt(dttScpr.Rows(0)!tb_lisscpr)
          If oApp.oGvar.bProvvRelazScontiNoListino Then
            'Se abilitata opz di registro globale il prezzo di riferimento non  quello
            'di listino (o ultimo costo) ma quello del momento
            'in tal modo lo sconto aplicato  quello effettivamente indicato sul movimento
            dPrezzoListino = .Input.dPrezzo
          Else
            'Cerca il prezzo di listino su cui calcolare lo sconto applicato
            If nLisscpr > 0 Then
              'Cerca nei listini
              With .Input
                dQuantabs = Math.Abs(.dQuant)
                strSQL = "SELECT TOP 1 lc_prezzo FROM listini " & _
                         " WHERE lc_codart = " & CStrSQL(.strCodart) & _
                         "   AND lc_conto = 0 " & _
                         "   AND lc_coddest = 0 " & _
                         "   AND lc_codvalu = 0 " & _
                         "   AND lc_codtpro = 0 " & _
                         "   AND lc_listino = " & nLisscpr & _
                         "   AND lc_datagg <= " & CDataSQL(.dtDatdoc) & _
                         "   AND lc_datscad >= " & CDataSQL(.dtDatdoc) & _
                         "   AND lc_daquant <= " & CDblSQL(dQuantabs) & _
                         "   AND lc_aquant >= " & CDblSQL(dQuantabs) & _
                         "   AND codditt = " & CStrSQL(.strDitta) & _
                         strSQLUM & _
                         " ORDER BY lc_datagg DESC"
              End With
              dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
              If dttTmp.Rows.Count > 0 Then
                dPrezzoListino = NTSCDec(dttTmp.Rows(0)!lc_prezzo)
              Else
                strError = oApp.Tr(Me, 130553515575513231, "Impossibile trovare il prezzo di listino per calcolare la provvigione di un agente gestito a fasce di sconti.")
                dProvv = 0
                Return True
              End If
            Else
              'Cerca l'ultimo prezzo di acquisto
              strSQL = "SELECT apx_ultcos FROM ARTPROX " & _
                       " WHERE apx_codart = " & CStrSQL(.Input.strCodart) & _
                       "   AND codditt = " & CStrSQL(.Input.strDitta)
              dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
              If dttTmp.Rows.Count > 0 Then
                dPrezzoListino = NTSCDec(dttTmp.Rows(0)!apx_ultcos)
              Else
                strError = oApp.Tr(Me, 130553515625092377, "Impossibile trovare l'ultimo costo di acquisto per calcolare la provvigione di un agente gestito a fasce di sconti.")
                dProvv = 0
                Return True
              End If
            End If
          End If
          'Calcola lo sconto applicato
          If dPrezzoListino <> 0 Then
            dScontoApplicato = ArrDbl(100 - ((100 * dPrezzoApplicato) / dPrezzoListino), 2)
          Else
            dScontoApplicato = 0
          End If
          'Cerca nel record di TABSCPR per trovare la provv.
          Dim bTrov As Boolean = False
          Dim dtrScpr As DataRow = dttScpr.Rows(0)
          For i As Integer = 1 To 12
            If NTSCDec(dtrScpr("tb_dasco_" & i)) <= dScontoApplicato And NTSCDec(dtrScpr("tb_asco_" & i)) >= dScontoApplicato Then
              dProvv = NTSCDec(dtrScpr("tb_pprov_" & i))
              Dim dPripa As Decimal = NTSCDec(dtrScpr("tb_pripa_" & i)) ' percentuale da aggiungere in base allo sconto realmente applicato rispetto allo scaglione previsto
              If dPripa <> 0 And (NTSCDec(dtrScpr("tb_asco_" & i)) - NTSCDec(dtrScpr("tb_dasco_" & i))) > 0 Then ' se non esiste uno 'scaglione' di sconto non prende in considerazione la % da ripartire in quanto non ha senso gestirla
                Dim dScoEsu As Decimal = dScontoApplicato - NTSCDec(dtrScpr("tb_dasco_" & i)) ' parte di sconto applicata in esubero rispetto al minimo delo scaglione
                dProvv = ArrDbl(dProvv + dPripa - ((dScoEsu * dPripa) / (NTSCDec(dtrScpr("tb_asco_" & i)) - NTSCDec(dtrScpr("tb_dasco_" & i)))), 2)
              End If
              bTrov = True
              Exit For
            End If
          Next
          If Not bTrov Then
            strError = oApp.Tr(Me, 127792178488750000, "La percentuale di sconto netta applicata (|" & dScontoApplicato.ToString(oApp.FormatSconti) & "| %) non rientra in una delle fasce di sconto per il calcolo della provvigione (codice tabella relazione sconti-provvigioni |" & strCodScpr & "|).")
            dProvv = 0
            Return True
          End If
        Else
          If strOpzscpr = "CodiceAgente" Then
            strError = oApp.Tr(Me, 127792178841250000, "Codice tabella relazione sconti-provvigioni |" & strCodScpr & "| non trovato: codice agente.")
          Else
            If strOpzscpr = "ClasseArticolo" Then
              strError = oApp.Tr(Me, 127792178875156250, "Codice tabella relazione sconti-provvigioni |" & strCodScpr & "| non trovato: classe provvigione articolo.")
            Else
              strError = oApp.Tr(Me, 127792178903593750, "Codice tabella relazione sconti-provvigioni |" & strCodScpr & "| non trovato: combinazione codice agente - classe provvigione articolo.")
            End If
          End If
        End If
      End With

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      With oCondCommerciali
        If lPos = 0 Then
          With .OutputProvvAgente1
            .dProvv = dProvv
            .strError = strError
          End With
        Else
          With .OutputProvvAgente2
            .dProvv = dProvv
            .strError = strError
          End With
        End If
      End With
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_TrovaDati(ByVal oCondCommerciali As NTSCondCommerciali) As DataTable
    Dim strSQL As String = ""
    Try
      With oCondCommerciali.Input
        strSQL = "SELECT perprov.* FROM perprov " & _
                 " WHERE per_codcage IN (0, " & .nCodage1 & ", " & .nCodage2 & ")" & _
                 "   AND per_conto IN (0, " & .lConto & ")" & _
                 "   AND per_coddest IN (0, " & .lDestdiv & ")" & _
                 "   AND per_codart IN (' ', " & CStrSQL(.strCodart) & ")" & _
                 "   AND per_clprar IN (0, " & .nClprar & ")" & _
                 "   AND per_clpran IN (0, " & .nClpran & ")" & _
                 "   AND per_datagg <= " & CDataSQL(.dtDatdoc) & _
                 "   AND per_datscad >= " & CDataSQL(.dtDatdoc) & " " & _
                 "   AND codditt = " & CStrSQL(.strDitta)

        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not .bRetail Or (.bRetail And CBool(GetSettingBus("BSREGSRE", "OPZIONI", ".", "Applica_anche_prom_come_Rel10", "0", ".", "0"))) Then
            If Not .bConspromo Then
              strSQL &= " AND per_codtpro = 0"
            Else
              If .nCodpromo > 0 Then strSQL &= " AND per_codtpro = " & .nCodpromo
            End If
          Else
            strSQL &= " AND per_codtpro = 0"
          End If
        Else
          If .nCodpromo > 0 Then
            strSQL &= " AND per_codtpro IN (" & .nCodpromo & ", 0)"
          Else
            strSQL &= " AND per_codtpro = 0"
          End If
        End If
      End With

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
      Return Nothing
    End Try
  End Function
  Public Overridable Function CercaCondCommerciali_CercaProvvigioni_RitornaAgenteCorrente(ByVal oCondCommerciali As NTSCondCommerciali, ByVal lPos As Integer) As Integer
    Try
      With oCondCommerciali
        If lPos = 0 Then
          Return .Input.nCodage1
        Else
          Return .Input.nCodage2
        End If
      End With
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return 0
  End Function
  Public Overridable Sub CercaCondCommerciali_CercaProvvigioni_ImpostaValoriRitorno(ByVal oCondCommerciali As NTSCondCommerciali, ByVal lPos As Integer, _
                                                                                    ByVal dtrProv As DataRow, ByVal strTipo As String)
    Try
      With oCondCommerciali
        If lPos = 0 Then
          With .OutputProvvAgente1
            .dProvv = NTSCDec(dtrProv!per_provv)
            .dVprovv = NTSCDec(dtrProv!per_vprovv)
            .nPromo = NTSCInt(dtrProv!per_codtpro)
            .strTipoval = strTipo
          End With
        Else
          With .OutputProvvAgente2
            .dProvv = NTSCDec(dtrProv!per_provv)
            .dVprovv = NTSCDec(dtrProv!per_vprovv)
            .nPromo = NTSCInt(dtrProv!per_codtpro)
            .strTipoval = strTipo
          End With
        End If
      End With
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub


  '--------------------------------------------------------------------------
  ' PASSATO articolo, conto, listino, tipo valorizz (G=Generico, S=Speciale, P=Prevalente), datadoc e codvalu, quantita
  ' listino = 0 = ultimoc costo, -2 = costo medio
  '
  ' RESTITUISCE dPrezzo (in valuta o il lire), dPrelist, e tipo val. restituito (Generico, Speciale, Prevalente)
  ' 
  ' OU: CercaPrezzoVCL = False se si è verificato un errore
  '--------------------------------------------------------------------------
  Public Overridable Function CercaPrezzoVCL(ByVal strDitta As String, ByVal strCodart As String, ByVal lConto As Integer, _
                                             ByVal nListino As Integer, ByVal strTipoval As String, ByVal dtDatdoc As DateTime, _
                                             ByVal nCodvalu As Integer, ByVal dQuant As Decimal, _
                                             ByRef dPrezzo As Decimal, ByRef strTipovalout As String, _
                                             ByRef dDaQuant As Decimal, ByRef dAquant As Decimal) As Boolean
    'CercaPrezzoVCLDitt di Business
    Dim strSQL As String = ""
    Dim strCoda As String
    Dim dQuantabs As Decimal
    Dim dttTmp As New DataTable
    Try

      'Azzera risultati
      dPrezzo = 0
      dDaQuant = 0
      dAquant = 9999999999

      'setta le altre var ..
      strTipovalout = strTipoval 'in via provvisoria

      'per comportmento div. articoli a varianti ..
      strCoda = strCodart

      'prende in valore assoluto di dquant
      dQuantabs = Math.Abs(dQuant)
      If dQuantabs = 0 Then dQuantabs = 1

      ' se val. listino generico salta la valorizz. a prezzo speciale
      If strTipoval = "G" Then GoTo Cerca_gen

      ' ora inizia la ricerca del prezzo vero e proprio ...
      ' cerca il prezzo richiesto o ultimo costo (ma nella valuta indicata!!!)
      ' per quel conto , se esiste

      ' con strTipoval ="P" Prevalente cerca prima il prezzo specifico
      ' se non viene trovato o è zero cerca quello generico
      ' il codice in realtà non compare mai "P" ma i vari goto oltre a complicare la comprensione del codice
      ' hanno come effetto collaterale quello di far funzionare questa cosa in modo corretto

Cerca_spec:
      If (nListino = 0 Or nListino > 0) And lConto > 0 Then
        strSQL = " SELECT TOP 1 lc_prezzo, lc_codtpro, lc_daquant, lc_aquant, lc_perqta, lc_unmis " & _
                 " FROM listini " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & " And lc_codart = " & CStrSQL(strCoda) & _
                 " AND (lc_conto = " & lConto & " ) AND lc_codlavo = 0" & _
                 " AND ( lc_listino = " & nListino & ") AND lc_codvalu = " & nCodvalu & _
                 " AND lc_datagg <= " & CDataSQL(dtDatdoc) & " AND lc_datscad >= " & CDataSQL(dtDatdoc) & _
                 " AND lc_daquant <= " & CDblSQL(dQuantabs) & " AND lc_aquant >= " & CDblSQL(dQuantabs) & _
                 " ORDER BY lc_codtpro DESC, lc_datagg DESC, lc_daquant"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          dPrezzo = NTSCDec(dttTmp.Rows(0)!lc_prezzo)
          strTipovalout = "S"
          dDaQuant = NTSCDec(dttTmp.Rows(0)!lc_daquant)
          dAquant = NTSCDec(dttTmp.Rows(0)!lc_aquant)
        End If
        If dPrezzo <> 0 Then GoTo finito
        If strTipoval = "S" Then GoTo finito
      End If

Cerca_gen:
      ' adesso il listino generico con listino > 0
      If nListino > 0 Then
        strSQL = " SELECT TOP 1 lc_prezzo, lc_codtpro, lc_daquant, lc_aquant, lc_perqta, lc_unmis " & _
                 " FROM listini " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & " And lc_codart = " & CStrSQL(strCoda) & " AND ( lc_conto = 0) AND lc_codlavo = 0" & _
                 " AND (lc_listino = " & nListino & " ) AND lc_codvalu = " & nCodvalu & _
                 " AND lc_datagg <= " & CDataSQL(dtDatdoc) & " AND lc_datscad >= " & CDataSQL(dtDatdoc) & _
                 " AND lc_daquant <= " & CDblSQL(dQuantabs) & " AND lc_aquant >= " & CDblSQL(dQuantabs) & _
                 " ORDER BY lc_codtpro DESC,  lc_datagg DESC, lc_daquant;"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttTmp.Rows.Count > 0 Then
          dPrezzo = NTSCDec(dttTmp.Rows(0)!lc_prezzo)
          strTipovalout = "G"
          dDaQuant = NTSCDec(dttTmp.Rows(0)!lc_daquant)
          dAquant = NTSCDec(dttTmp.Rows(0)!lc_aquant)
        End If
        If dPrezzo <> 0 Then GoTo finito
      End If

finito:
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaCostoMedioPond(ByVal strDitta As String, ByVal strCodart As String, _
                                                  ByVal nFase As Integer, ByVal nCodmaga As Integer, _
                                                  ByVal dPerqta As Decimal) As Decimal

    Try
      Return CercaCostoMedioPond(strDitta, strCodart, nFase, nCodmaga, dPerqta, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCostoMedioPond(ByVal strDitta As String, ByVal strCodart As String, _
                                                  ByVal nFase As Integer, ByVal nCodmaga As Integer, _
                                                  ByVal dPerqta As Decimal, ByVal dsTableInMem As DataSet) As Decimal
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nFase, nCodmaga, dPerqta, dsTableInMem})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCDec(oOut)
      End If
      '----------------

      Return CercaCostoMedioPond(strDitta, strCodart, nFase, nCodmaga, dPerqta, Nothing, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaCostoMedioPond(ByVal strDitta As String, ByVal strCodart As String, _
                                                  ByVal nFase As Integer, ByVal nCodmaga As Integer, _
                                                  ByVal dPerqta As Decimal, ByVal dsTableInMem As DataSet, _
                                                  ByRef strErr As String) As Decimal
    'Se nCodmaga > 0, allora solo magazzino nCodmaga
    'Se nCodmaga = 0, allora tutti i magazzini
    'Se nCodmaga = -1, allora magazzini merce propria
    'Se nCodmaga = -2, allora magazzini merce altrui
    Dim strSQL As String = ""
    Dim dsTmp As DataSet
    Dim dtUlap As Date
    Dim dQtaEsist As Decimal 'Esistenza di magazzino
    Dim dQtaRim As Decimal 'quantità rimanente
    Dim dValTot As Decimal
    Dim dCostoTmp As Decimal
    Dim strMsg As String
    Dim i As Integer = 0
    Dim dtrResult() As DataRow
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nFase, nCodmaga, dPerqta, dsTableInMem, strErr})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strErr = NTSCStr(oIn(6))
        Return NTSCDec(oOut)
      End If
      '----------------

      '------------------------------------
      If oApp.oGvar.bCostoMedioPondNoStorico Then
        'Legge la data ultimo aggiornamento progressivi
        strSQL = "SELECT tb_dtulap FROM tabanaz WHERE codditt = " & CStrSQL(strDitta)
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABANAZ")

        If dsTmp.Tables("TABANAZ").Rows.Count > 0 Then
          dtUlap = NTSCDate(dsTmp.Tables("TABANAZ").Rows(0)!tb_dtulap)
        End If
        dsTmp.Tables.Clear()
      Else
        dtUlap = NTSCDate(IntSetDate("01/01/1900"))
      End If

      '-----------------------------------
      'Creo il recordset che mi restituir il valore della giacenza ad oggi
      dQtaEsist = 0
      If Not dsTableInMem Is Nothing Then
        '--- In memoria ---
        Dim dtrSel() As DataRow = dsTableInMem.Tables("MAGAPRO").Select("artpro.codditt = " & CStrSQL(strDitta) & _
                                                                        " AND artpro.ap_codart = " & CStrSQL(strCodart))

        strMsg = "su tutti i magazzini,"
        If nCodmaga = -1 Then
          strMsg = "sui magazzini merce propria,"
        ElseIf nCodmaga = -2 Then
          strMsg = "sui magazzini merce altrui,"
        ElseIf nCodmaga > 0 Then
          strMsg = "sul magazzino " & nCodmaga & ","
        End If

        If dtrSel.Length > 0 Then
          If IsNumeric(dtrSel(0)!somma) Then
            dQtaEsist = ArrDbl(NTSCDec(dtrSel(0)!somma), oApp.NDecQta)
          End If
        End If
      Else
        '--- Su DB ---
        strSQL = "SELECT Sum(artpro.ap_esist) AS somma" & _
                 " FROM tabmaga INNER JOIN artpro ON tabmaga.codditt = artpro.codditt AND " & _
                 " tabmaga.tb_codmaga = artpro.ap_magaz" & _
                 " WHERE artpro.codditt = " & CStrSQL(strDitta) & _
                 " AND artpro.ap_codart = " & CStrSQL(strCodart) & _
                 " AND artpro.ap_fase = " & nFase & " "
        strMsg = "su tutti i magazzini,"
        If nCodmaga = -1 Then
          strMsg = "sui magazzini merce propria,"
          strSQL = strSQL & " AND (tabmaga.tb_flclavo =' ' OR tabmaga.tb_flclavo ='F')"
        ElseIf nCodmaga = -2 Then
          strMsg = "sui magazzini merce altrui,"
          strSQL = strSQL & " AND (tabmaga.tb_flclavo ='C' OR tabmaga.tb_flclavo ='X')"
        ElseIf nCodmaga > 0 Then
          strMsg = "sul magazzino " & nCodmaga & ","
          strSQL = strSQL & " and artpro.ap_magaz = " & nCodmaga
        End If

        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "MAGAPRO")

        If dsTmp.Tables("MAGAPRO").Rows.Count > 0 Then
          If IsNumeric(dsTmp.Tables("MAGAPRO").Rows(0)!somma) Then
            dQtaEsist = ArrDbl(NTSCDec(dsTmp.Tables("MAGAPRO").Rows(0)!somma), oApp.NDecQta)
          End If
        End If
        dsTmp.Tables.Clear()
      End If

      '-------------------------------------
      If dQtaEsist <= 0 Then
        'L'esistenza  <= a zero, rilevo l'ultimo costo/costo iniziale
        strSQL = "SELECT apx_ultcos, apx_giaini, apx_vgiaini FROM artprox" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND apx_codart = " & CStrSQL(strCodart) & _
                " AND apx_fase = " & nFase
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPROX")

        If dsTmp.Tables("ARTPROX").Rows.Count > 0 Then
          With dsTmp.Tables("ARTPROX").Rows(0)
            dCostoTmp = ArrDbl(NTSCDec(!apx_ultcos), TrovaNdecSuPrzUn(0))
            If dCostoTmp = 0 And NTSCDec(!apx_giaini) <> 0 Then
              dCostoTmp = ArrDbl(NTSCDec(!apx_vgiaini) / NTSCDec(!apx_giaini) * dPerqta, TrovaNdecSuPrzUn(0))
            End If
          End With
        End If
        dsTmp.Tables.Clear()

        Return dCostoTmp
      End If

      '------------------------------------
      'Creo il recordset dei carichi valorizzanti
      dQtaRim = dQtaEsist
      dValTot = 0

      If Not dsTableInMem Is Nothing Then
        dtrResult = dsTableInMem.Tables("ARTPROX").Select("keymag.codditt = " & CStrSQL(strDitta) & " AND keymag.km_codart = " & CStrSQL(strCodart))

      Else
        strSQL = "SELECT keymag.km_aammgg, Sum(movmag.mm_quant * tabcaum.tb_valoriz) AS Quant, " & _
                " Sum(movmag.mm_valore * tabcaum.tb_valoriz) AS Valore" & _
                " FROM tabmaga INNER JOIN (tabcaum INNER JOIN (movmag INNER JOIN keymag ON " & _
                " (movmag.codditt = keymag.codditt) AND (movmag.mm_riga = keymag.km_riga) AND " & _
                " (movmag.mm_tipork = keymag.km_tipork) AND (movmag.mm_serie = keymag.km_serie) AND " & _
                " (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_numdoc = keymag.km_numdoc)) ON " & _
                " tabcaum.tb_codcaum = keymag.km_causale) ON tabmaga.codditt = keymag.codditt AND " & _
                " tabmaga.tb_codmaga = keymag.km_magaz" & _
                " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
                " AND keymag.km_codart = " & CStrSQL(strCodart) & _
                " AND keymag.km_fase = " & nFase & _
                " AND tabcaum.tb_valoriz <> 0" & _
                " AND tabcaum.tb_vvaloriz <> 0 "
        If nCodmaga = -1 Then
          strSQL = strSQL & " AND (tabmaga.tb_flclavo = ' ' OR tabmaga.tb_flclavo = 'F') "
        ElseIf nCodmaga = -2 Then
          strSQL = strSQL & " AND (tabmaga.tb_flclavo = 'C' OR tabmaga.tb_flclavo = 'X') "
        ElseIf nCodmaga > 0 Then
          strSQL = strSQL & " AND keymag.km_magaz = " & nCodmaga & " "
        End If
        If oApp.oGvar.bCostoMedioPondNoStorico Then
          strSQL = strSQL & "AND keymag.km_aammgg > " & CDataSQL(dtUlap.ToShortDateString)
        End If
        strSQL = strSQL & " GROUP BY keymag.km_codart, keymag.km_fase, keymag.km_aammgg  "
        strSQL = strSQL & " ORDER BY keymag.km_aammgg DESC "
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTPROX")

        dtrResult = dsTmp.Tables("ARTPROX").Select()
      End If

      If dtrResult.Length > 0 Then
        i = 0
        Do While Not (i = dtrResult.Length Or dQtaRim <= 0)
          If NTSCDec(dtrResult(i)!Quant) >= dQtaRim Then
            'La quantità del movimento corrente supera la quantità richiesta
            If NTSCDec(dtrResult(i)!Quant) <> 0 Then
              dValTot = dValTot + (dQtaRim * (NTSCDec(dtrResult(i)!Valore) / NTSCDec(dtrResult(i)!Quant)))
            End If
            dQtaRim = 0
          Else
            dValTot = dValTot + NTSCDec(dtrResult(i)!Valore)
            dQtaRim = ArrDbl(NTSCDec(dQtaRim) - NTSCDec(dtrResult(i)!Quant), oApp.NDecQta)
          End If
          i = i + 1
        Loop
      End If

      '---------------------------------------------------------------------------------------------
      'Controlla se c' ancora della quantità da valorizzare, in tal caso rileva i dati della
      'chiusura definitiva
      If dQtaRim > 0 And oApp.oGvar.bCostoMedioPondNoStorico Then
        If nCodmaga = -1 Then
          'merce propria
          strSQL = "SELECT adx_lesist AS Quant, adx_lvalesi AS Valore FROM artdefx" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND adx_codart = " & CStrSQL(strCodart) & _
                    " AND adx_fase = " & nFase
        ElseIf nCodmaga = -2 Then
          'merce altrui
          strSQL = "SELECT (adx_esist - adx_lesist) AS Quant, (adx_valesi - adx_lvalesi) AS Valore FROM artdefx" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND adx_codart = " & CStrSQL(strCodart) & _
                    " AND adx_fase = " & nFase
        ElseIf nCodmaga > 0 Then
          'singolo magazzino
          strSQL = "SELECT ad_esist AS Quant, ad_valesi AS Valore FROM artdef" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND ad_codart = " & CStrSQL(strCodart) & _
                    " AND ad_fase = " & nFase & _
                    " AND ad_magaz = " & nCodmaga
        Else
          'tutti i magazzini
          strSQL = "SELECT adx_esist AS Quant, adx_valesi AS Valore FROM artdefx" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND adx_codart = " & CStrSQL(strCodart) & _
                    " AND adx_fase = " & nFase
        End If
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTDE")

        If dsTmp.Tables("ARTDE").Rows.Count > 0 Then
          With (dsTmp.Tables("ARTDE").Rows(0))
            If NTSCDec(!Quant) >= dQtaRim Then
              'La quantità dei definitivi supera la quantità richiesta
              If NTSCDec(!Quant) <> 0 Then dValTot = dValTot + (dQtaRim * (NTSCDec(!Valore) / NTSCDec(!Quant)))
              dQtaRim = 0
            Else
              dValTot = dValTot + NTSCDec(!Valore)
              dQtaRim = ArrDbl(dQtaRim - NTSCDec(!Quant), oApp.NDecQta)
            End If
          End With
        End If
        dsTmp.Tables.Clear()
      End If
      '---------------------------------------------------------------------------------------------
      If dQtaRim > 0 Then
        strMsg = oApp.Tr(Me, 130697870693087526, "Calcolo del costo medio ponderato attuale(articolo " & UCase(strCodart) & _
                                                 "): impossibile rilevare i carichi valorizzanti, " & strMsg & " relativi a " & _
                                                 Format(dQtaEsist, oApp.FormatQta) & " quantità (costo medio ponderato rilevato solo su " & _
                                                 Format(ArrDbl(dQtaEsist - dQtaRim, oApp.NDecQta), oApp.FormatQta) & " quantità).")
        If strErr Is Nothing Then
          Throw New NTSException(strMsg)
        Else
          strErr = strMsg
          Return 0
        End If
        'If ArrDbl(dQtaEsist - dQtaRim, oApp.NDecQta) <> 0 Then
        '  CercaCostoMedioPond = ArrDbl((dValTot / ArrDbl(dQtaEsist - dQtaRim, oApp.NDecQta)) * dPerqta,TrovaNdecSuPrzUn(0))
        'Else
        '  CercaCostoMedioPond = 0
        'End If
      Else
        'Comunque dQtaEsist  sempre > di zero
        CercaCostoMedioPond = ArrDbl((dValTot / dQtaEsist) * dPerqta, TrovaNdecSuPrzUn(0))
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaPrezzoFifo(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, _
                                              ByVal nCodmaga As Integer, ByVal dtData As String, ByVal dQta As Decimal, _
                                              ByVal nConsEsPrenImp As Integer, ByRef dValoretot As Decimal, ByRef strErr As String) As Decimal

    Try
      Return CercaPrezzoFifo(strDitta, strCodart, nFase, nCodmaga, dtData, dQta, _
                             nConsEsPrenImp, dValoretot, strErr, Nothing)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaPrezzoFifo(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, _
                                              ByVal nCodmaga As Integer, ByVal dtData As String, ByVal dQta As Decimal, _
                                              ByVal nConsEsPrenImp As Integer, ByRef dValoretot As Decimal, ByRef strErr As String, _
                                              ByVal dsTableInMem As DataSet) As Decimal
    'CercaPrezzoFifoEx di Business 10
    ' bConsesprenImp non gestito per ora : 0= esistenza (default), 1=prenotato, 2=impegnato
    ' se dtData = "", ad oggi, altrimenti a data specificata
    ' se cod. magazzino = 0  , allora tutti i magazzini
    ' se cod. magazzino = -1 , allora magazzini merce propria
    ' se cod. magazzino = -2 , allora magazzini merce altrui

    '--------------------------------
    'funziona in questo modo:
    '1: cerco la giacenza ad una determinata data
    '2: cerco i carichi valorizzanti ordinandoli per data DECRESCENTE 
    '3: il principio è che se ho una giacenza ed i primi entrati sono i primi ad uscire,
    '   il prezzo FIFO NON E' UNA MEDIA dei carichi valorizzanti della giacenza a magazzino, ma semplicemente
    '   il prezzo indicato nel carico del primo articolo ancora in giacenza (determinato con il sistema FIFO)

    Dim strSQL As String = ""
    Dim strSQL1 As String
    Dim dPerqta As Decimal
    Dim dsTmp As DataSet
    Dim strMsg As String
    Dim i As Integer

    Dim dGiac As Decimal        'variabile giacenza
    Dim dQtaPrel As Decimal     'quantità prelevata dal carico riguardante
    Dim dScarto As Decimal      'scarto ottenuto sottraendo il carico alla quantità inserita dall'utente
    Dim dValparz As Decimal     'valori parziali calcolati moltiplicando il prezzo del materiale per la quantità
    Dim dQtapa As Decimal       'quantità inserita dall'utente
    Dim dValTot As Decimal      'totali ricavati dalla somma dei dua parziali

    Dim bTrovatiMovimenti As Boolean = False
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nFase, nCodmaga, dtData, dQta, nConsEsPrenImp, dValoretot, strErr, dsTableInMem})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dValoretot = NTSCDec(oIn(7))        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        strErr = NTSCStr(oIn(8))        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return NTSCDec(oOut)
      End If
      '----------------

      strErr = ""
      dValoretot = 0
      ' parte elaborazione
      dQtapa = dQta
      dPerqta = 1

      '-------------------------------------------
      ' Creo il secondo recordset che mi restituisce il valore della giacenza ad oggi
      If Not dsTableInMem Is Nothing Then
        '--- In Memoria ---
        Dim dtrSel() As DataRow = dsTableInMem.Tables("ARTPRO").Select("artpro.codditt = " & CStrSQL(strDitta) & _
                                                                       " AND ARTPRO.ap_codart= " & CStrSQL(strCodart))

        If dtrSel.Length > 0 Then
          If IsNumeric(dtrSel(0)!somma.ToString) Then
            dGiac = ArrDbl(NTSCDec(dtrSel(0)!somma), oApp.NDecQta)
          End If
        End If
      Else
        '--- Su DB ---
        strSQL1 = "SELECT Sum(artpro.ap_esist) AS somma" & _
            " FROM tabmaga INNER JOIN ARTPRO ON tabmaga.tb_codmaga = ARTPRO.ap_magaz and " & _
            " tabmaga.codditt = artpro.codditt " & _
            " WHERE artpro.codditt = " & CStrSQL(strDitta) & _
            " AND ARTPRO.ap_codart= " & CStrSQL(strCodart) & _
            " AND ARTPRO.ap_fase = " & nFase

        If nCodmaga = -1 Then
          strSQL1 = strSQL1 & " AND (tabmaga.tb_flclavo =' ' OR tabmaga.tb_flclavo ='F')"
        ElseIf nCodmaga = -2 Then
          strSQL1 = strSQL1 & " AND (tabmaga.tb_flclavo ='C' OR tabmaga.tb_flclavo ='X')"
        ElseIf nCodmaga > 0 Then
          strSQL1 = strSQL1 & " and artpro.ap_magaz = " & nCodmaga & ""
        End If

        dsTmp = OpenRecordset(strSQL1, CLE__APP.DBTIPO.DBAZI, "ARTPRO")
        If dsTmp.Tables("ARTPRO").Rows.Count > 0 Then
          If IsNumeric(dsTmp.Tables("ARTPRO").Rows(0)!somma.ToString) Then
            dGiac = ArrDbl(NTSCDec(dsTmp.Tables("ARTPRO").Rows(0)!somma), oApp.NDecQta)
          End If
        End If
        dsTmp.Tables.Clear()
      End If

      '-------------------------------------------
      'giacenza di materiale a magazzino
      ' se calcolo a data , cerco l'esistenza alla data indicata
      If dtData <> "" Then
        If Not dsTableInMem Is Nothing Then
          '--- In Memoria ---
          Dim dtrSel() As DataRow = dsTableInMem.Tables("snaMovm").Select("keymag.km_codart = " & CStrSQL(strCodart) & " AND keymag.km_fase = " & nFase)
          If dtrSel.Length > 0 Then
            If IsNumeric(dtrSel(0)!diff.ToString) Then
              dGiac = ArrDbl(dGiac - NTSCDec(dtrSel(0)!diff), oApp.NDecQta)
            End If
          End If
        Else
          '--- Su DB ---
          strSQL1 = "SELECT Sum(mm_quant*km_carscar) AS diff " & _
                    " FROM tabmaga INNER JOIN (movmag INNER JOIN keymag ON (movmag.mm_riga = keymag.km_riga)" & _
                    " AND (movmag.mm_tipork = keymag.km_tipork) AND (movmag.mm_serie = keymag.km_serie)" & _
                    " AND (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_numdoc = keymag.km_numdoc) AND " & _
                    " (movmag.codditt = keymag.codditt))" & _
                    " ON tabmaga.tb_codmaga = keymag.km_magaz and tabmaga.codditt = keymag.codditt " & _
                    " where km_aammgg > " & CDataSQL(dtData.ToString) & _
                    " AND keymag.codditt = " & CStrSQL(strDitta) & _
                    " AND keymag.km_codart = " & CStrSQL(strCodart) & _
                    " AND keymag.km_fase = " & nFase
          If nCodmaga = -1 Then
            strSQL1 = strSQL1 & " AND (tabmaga.tb_flclavo =' ' OR tabmaga.tb_flclavo ='F')"
          ElseIf nCodmaga = -2 Then
            strSQL1 = strSQL1 & " AND (tabmaga.tb_flclavo ='C' OR tabmaga.tb_flclavo ='X')"
          ElseIf nCodmaga > 0 Then
            strSQL1 = strSQL1 & " AND keymag.km_magaz = " & nCodmaga & ""
          End If

          dsTmp = OpenRecordset(strSQL1, CLE__APP.DBTIPO.DBAZI, "snaMovm")
          If dsTmp.Tables("snaMovm").Rows.Count > 0 Then
            If IsNumeric(dsTmp.Tables("snaMovm").Rows(0)!diff.ToString) Then
              dGiac = ArrDbl(dGiac - NTSCDec(dsTmp.Tables("snaMovm").Rows(0)!diff), oApp.NDecQta)
            End If
          End If
          dsTmp.Tables.Clear()
        End If
      End If ' If dtData <> "" Then


      '-------------------------------------------
      ' Creo il  recordset dei carichi valorizzanti ..
      Dim dtrResult() As DataRow = Nothing
      If Not dsTableInMem Is Nothing Then
        Dim dtrSel() As DataRow = dsTableInMem.Tables("snaFifo").Select("keymag.km_codart = " & CStrSQL(strCodart) & " AND keymag.km_fase = " & nFase)
      Else
        strSQL = " SELECT keymag.km_magaz, keymag.km_carscar, keymag.km_aammgg, movmag.mm_codart, movmag.mm_quant, " & _
                 " movmag.mm_perqta, "
        If oApp.oGvar.bUltSpeseInValoriFifo Then strSQL += " movmag.mm_numpex + " 'ulteriori spese di piede indicate in veboll
        If oApp.oGvar.bOneriAccInValoriFifo Then
          strSQL += " movmag.mm_valore + movmag.mm_numpac as valore "
        Else
          strSQL += " movmag.mm_valore as valore "
        End If
        strSQL += " FROM (movmag INNER JOIN keymag ON (movmag.mm_riga = keymag.km_riga) AND " & _
                  " (movmag.mm_tipork = keymag.km_tipork) And  (movmag.mm_serie = keymag.km_serie) AND " & _
                  " (movmag.mm_anno = keymag.km_anno) AND (movmag.mm_numdoc = keymag.km_numdoc) AND " & _
                  " (movmag.codditt = keymag.codditt) ) INNER JOIN tabcaum on keymag.km_causale = tabcaum.tb_codcaum " & _
                  " WHERE tabcaum.tb_valoriz <> 0 " & _
                  " and tabcaum.tb_vvaloriz <> 0 " & _
                  " and keymag.codditt = " & CStrSQL(strDitta) & _
                  " and keymag.km_codart = " & CStrSQL(strCodart) & _
                  " AND keymag.km_fase = " & nFase & _
                  " AND keymag.km_carscar > 0 "
        If Not dtData = "" Then
          strSQL = strSQL & " AND km_aammgg <= " & CDataSQL(dtData.ToString)
        End If
        '-------------------------------------------
        'controllo il codice di magazzino e se  = 0 seleziono tutti i record
        ' NO !! (sempre tutti, solo i carichi valorizzanti indipendenti dal magazzino !)
        'If nCodmaga <> 0 Then
        '   strSQL = strSQL & " AND keymag.km_magaz = " & nCodmaga & ""
        'End If
        strSQL = strSQL & " ORDER BY keymag.km_aammgg DESC, keymag.km_numdoc DESC, keymag.km_riga DESC " 'la routine 'CercaPrezzoFifo' poteva restituire dei risultati diversi rispetto alla versione in vb6 se all'interno dei documenti di carico lo stesso articolo veniva indicato più volte con prezzi diversi

        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaFifo")

        dtrResult = dsTmp.Tables("snaFifo").Select()
      End If

      If dtrResult.Length > 0 Then
        bTrovatiMovimenti = True
        dScarto = 0
        dValTot = 0
        dPerqta = NTSCDec(dtrResult(0)!mm_perqta)
        dScarto = ArrDbl(dGiac - dQtapa, oApp.NDecQta)

        If dScarto < 0 Then
          'scateno l'errore
          strMsg = "Valorizzazione FIFO: articolo " & UCase(strCodart) & ", magazzino "
          Select Case nCodmaga
            Case 0 : strMsg = strMsg & "(tutti)"
            Case -1 : strMsg = strMsg & "(merce propria)"
            Case -2 : strMsg = strMsg & "(merce altrui)"
            Case Else : strMsg = strMsg & nCodmaga.ToString
          End Select
          strMsg = strMsg & ", data "
          If dtData = "" Then
            strMsg = strMsg & "(oggi)"
          Else
            strMsg = strMsg & NTSCDate(dtData).ToShortDateString
          End If
          strMsg = strMsg & ": quantità richiesta " & Format(dQtapa, oApp.FormatQta) & " superiore alla giacenza " & Format(dGiac, oApp.FormatQta) & "."
          If strErr Is Nothing Then
            Throw New NTSException(strMsg)
          Else
            strErr = strMsg
            Return 0
          End If
        End If

        i = 0
        Do While Not (i = dtrResult.Length Or dQtapa <= 0)
          If dScarto <= NTSCDec(dtrResult(i)!mm_quant) Then

            dQtaPrel = NTSCDec(dtrResult(i)!mm_quant) - dScarto
            If dQtaPrel > dQtapa Then dQtaPrel = dQtapa
            dQtapa = dQtapa - dQtaPrel

            If NTSCDec(dtrResult(i)!mm_quant) <> 0 Then
              dValparz = dQtaPrel * _
                        (NTSCDec(dtrResult(i)!valore) / _
                         NTSCDec(dtrResult(i)!mm_quant))
            Else
              dValparz = 0
            End If

            dValTot = dValTot + dValparz

            dScarto = 0

          Else
            dScarto = dScarto - NTSCDec(dtrResult(i)!mm_quant)
          End If
          i = i + 1
        Loop
      End If

      dValoretot = dValTot

      If dQta > 0 Then
        If dQtapa > 0 Then
          If CBool(GetSettingBusDitt(strDitta, "OPZIONI", ".", ".", "SegnalaCarichiFIFOMancanti", "-1", " ", "-1")) Then
            If bTrovatiMovimenti Then
              strMsg = "Valorizzazione FIFO: articolo " & UCase(strCodart) & ", magazzino "
              Select Case nCodmaga
                Case 0 : strMsg = strMsg & "(tutti)"
                Case -1 : strMsg = strMsg & "(merce propria)"
                Case -2 : strMsg = strMsg & "(merce altrui)"
                Case Else : strMsg = strMsg & nCodmaga.ToString
              End Select
              strMsg = strMsg & ", data "
              If dtData = "" Then
                strMsg = strMsg & "(oggi)"
              Else
                strMsg = strMsg & NTSCDate(dtData).ToShortDateString
              End If
              strMsg = strMsg & ": quantità " & Format(dQtapa, oApp.FormatQta) & " di " & Format(dQta, oApp.FormatQta) & " non trovata da valorizzare a FIFO." & vbCrLf & _
                                "L'intera valorizzazione avverrà rilevando il costo della giacenza iniziale."
            Else
              strMsg = "Valorizzazione FIFO: articolo " & UCase(strCodart) & ", magazzino "
              Select Case nCodmaga
                Case 0 : strMsg = strMsg & "(tutti)"
                Case -1 : strMsg = strMsg & "(merce propria)"
                Case -2 : strMsg = strMsg & "(merce altrui)"
                Case Else : strMsg = strMsg & nCodmaga.ToString
              End Select
              strMsg = strMsg & ", data "
              If dtData = "" Then
                strMsg = strMsg & "(oggi)"
              Else
                strMsg = strMsg & NTSCDate(dtData).ToShortDateString
              End If
              strMsg = strMsg & ": nessun movimento individuato per valorizzare a FIFO la quantità " & Format(dQta, oApp.FormatQta) & " in giacenza." & vbCrLf & _
                                "La valorizzazione avverrà rilevando il costo della giacenza iniziale."
            End If
            strErr = strMsg
          Else
            Return 0
          End If
        Else
          Return ArrDbl((dValTot / dQta) * dPerqta, TrovaNdecSuPrzUn(0))
        End If
      Else
        'scateno l'errore
        strMsg = "Valorizzazione FIFO: articolo " & UCase(strCodart) & ", magazzino "
        Select Case nCodmaga
          Case 0 : strMsg = strMsg & "(tutti)"
          Case -1 : strMsg = strMsg & "(merce propria)"
          Case -2 : strMsg = strMsg & "(merce altrui)"
          Case Else : strMsg = strMsg & nCodmaga.ToString
        End Select
        strMsg = strMsg & ", data "
        If dtData = "" Then
          strMsg = strMsg & "(oggi)"
        Else
          strMsg = strMsg & NTSCDate(dtData).ToShortDateString
        End If
        strMsg = strMsg & ": quantità richiesta " & Format(dQtapa, oApp.FormatQta) & " uguale a zero o negativa."
        If strErr Is Nothing Then
          Throw New NTSException(strMsg)
        Else
          strErr = strMsg
          Return 0
        End If
      End If
      dScarto = 0
      dValTot = 0
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiSpeTrasp(ByVal strDitta As String, ByVal nCodvett As Integer, ByVal strCodPort As String) As DataTable
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM spetrasp" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND st_codvett = " & nCodvett & _
              " AND st_codport = " & CStrSQL(strCodPort) & _
              " ORDER BY st_codvett, st_codport"
      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "SPETRASP").Tables("SPETRASP")
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetDDTPerCalcoladoc(ByVal strDitta As String, ByVal strTipoDoc As String, _
                                                 ByVal nAnno As Integer, ByVal strSerie As String, ByVal lNumdoc As Integer, _
                                                 ByRef dttOut As DataTable) As Boolean
    'chiamato da CalcoladocFattureRieplig, restituisce per ogni testata le righe del ddt per il ricalcolo del documento
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT mm_codart, mm_prezzo, mm_preziva, mm_prezvalc, mm_valore, " & _
              "        mm_quant, mm_colli, mm_scont1, mm_scont2, mm_scont3, mm_codiva, mm_controp," & _
              "        mm_stasino, mm_vprovv, mm_vprovv2, mm_perqta, tm_scont1, tm_scont2, tm_scopag," & _
              "        tm_cambio, tm_scorpo, tm_valuta, tm_totcoll, tm_speacc, tm_speimb, tm_pagato," & _
              "        tm_abbuono, tm_totomag, tm_speaccv, tm_speimbv, tm_pagatov, tm_abbuonov," & _
              "        tm_totomagv, tm_impprov, tm_totprov, tm_totprov2, tm_peso, tm_pesonetto," & _
              "        tm_speinc, tm_speincv, mm_scont4, mm_scont5, mm_scont6, mm_acnum, mm_umprz, " & _
              "        mm_scontp, mm_scontv, mm_flkit " & _
              " FROM " & strJoinTestmagMovmag & _
              " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
              "   AND tm_tipork = " & CStrSQL(strTipoDoc) & _
              "   AND tm_anno = " & nAnno & _
              "   AND tm_serie = " & CStrSQL(strSerie) & _
              "   AND tm_numdoc = " & lNumdoc.ToString
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetDDTPerCalcoladocRagg(ByVal strDitta As String, ByVal dttElencoDDT As DataTable, _
                                                      ByRef dttOut As DataTable) As Boolean
    'chiamato da CalcoladocFattureRieplig, restituisce per ogni testata le righe del ddt per il ricalcolo del documento
    Dim strSQL As String = ""
    Dim i As Integer = 0
    Try
      strSQL = "SELECT mm_codart, mm_prezzo, mm_preziva, mm_prezvalc, sum(mm_valore) as mm_valore, " & _
              " sum(mm_quant) as mm_quant, sum(mm_colli) as mm_colli, " & _
              " mm_scont1, mm_scont2, mm_scont3, mm_codiva, mm_controp," & _
              " mm_stasino, sum(mm_vprovv) as mm_vprovv, sum(mm_vprovv2) as mm_vprovv2, mm_perqta," & _
              " tm_scorpo, mm_scont4, mm_scont5, mm_scont6, mm_acnum, mm_umprz, " & _
              " mm_scontp, mm_scontv " & _
              " FROM (" & strJoinTestmagMovmag & ") " & _
              " LEFT JOIN tabtpbf ON testmag.codditt = tabtpbf.codditt AND testmag.tm_tipobf = tabtpbf.tb_codtpbf" & _
              " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
              " AND ("
      If dttElencoDDT.Rows.Count > 0 Then
        For i = 0 To dttElencoDDT.Rows.Count - 1
          strSQL += "(tm_tipork = " & CStrSQL(dttElencoDDT.Rows(i)!tm_tipork) & _
                   " AND tm_anno = " & dttElencoDDT.Rows(i)!tm_anno.ToString & _
                   " AND tm_serie = " & CStrSQL(dttElencoDDT.Rows(i)!tm_serie) & _
                   " AND tm_numdoc = " & dttElencoDDT.Rows(i)!tm_numdoc.ToString & ")"
          If i < dttElencoDDT.Rows.Count - 1 Then strSQL += " OR "
        Next
      Else
        strSQL &= " 1 <> 1 "
      End If
      strSQL += ")" & _
              " GROUP BY mm_codart, mm_descr, mm_prezzo, mm_prezvalc, mm_preziva, mm_scont1, mm_scont2, mm_scont3, " & _
              " mm_scont4, mm_scont5, mm_scont6, mm_codiva, mm_controp, mm_stasino, mm_perqta, mm_acnum, " & _
              " mm_umprz, tm_scorpo, tm_flspinc, tm_flbolli, mm_scontp, mm_scontv "
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaSconti(ByVal strDitta As String, ByVal strCodart As String, ByVal lConto As Integer, _
                               ByVal nClscar As Integer, ByVal nClscan As Integer, ByVal strTipoval As String, _
                               ByVal bConspromo As Boolean, ByVal nCodpromoin As Integer, ByVal dtDatdoc As Date, _
                               ByVal dQuant As Decimal, ByRef dSc1 As Decimal, ByRef dSc2 As Decimal, ByRef dSc3 As Decimal, _
                               ByRef dSc4 As Decimal, ByRef dSc5 As Decimal, ByRef dSc6 As Decimal, _
                               ByRef nPromo As Integer, ByVal strPrzNet As String, Optional ByRef nPerqta As Integer = 1, _
                               Optional ByRef dDaQuant As Decimal = 0, Optional ByRef dAquant As Decimal = 9999999999, _
                               Optional ByRef strUnmisout As String = "", Optional ByRef strTipovalout As String = " ", _
                               Optional ByRef dtDaData As Date = Nothing, Optional ByRef dtAData As Date = Nothing) As Boolean
    Try
      Return CercaSconti(strDitta, strCodart, lConto, nClscar, nClscan, strTipoval, bConspromo, nCodpromoin, dtDatdoc, _
                         dQuant, dSc1, dSc2, dSc3, dSc4, dSc5, dSc6, nPromo, strPrzNet, nPerqta, _
                         dDaQuant, dAquant, strUnmisout, strTipovalout, dtDaData, dtAData, 999999999)


    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaSconti(ByVal strDitta As String, ByVal strCodart As String, ByVal lConto As Integer, _
                              ByVal nClscar As Integer, ByVal nClscan As Integer, ByVal strTipoval As String, _
                              ByVal bConspromo As Boolean, ByVal nCodpromoin As Integer, ByVal dtDatdoc As Date, _
                              ByVal dQuant As Decimal, ByRef dSc1 As Decimal, ByRef dSc2 As Decimal, ByRef dSc3 As Decimal, _
                              ByRef dSc4 As Decimal, ByRef dSc5 As Decimal, ByRef dSc6 As Decimal, _
                              ByRef nPromo As Integer, ByVal strPrzNet As String, ByRef nPerqta As Integer, _
                              ByRef dDaQuant As Decimal, ByRef dAquant As Decimal, _
                              ByRef strUnmisout As String, ByRef strTipovalout As String, _
                              ByRef dtDaData As Date, ByRef dtAData As Date, ByVal nListino As Integer) As Boolean
    'rispetto alla firma precedente riceve in più il numero di listino
    Dim strSQL As String = ""
    Dim bCumulativi As Boolean
    Dim snaTmp As DataTable
    Dim dQuantabs As Decimal
    Dim strCoda As String
    Dim strGesvar As String
    Dim strCodvar1 As String
    Dim strCodroot As String
    Dim strPrevar As String

    Dim bRetail As Boolean = False
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, lConto, nClscar, nClscan, strTipoval, bConspromo, nCodpromoin, dtDatdoc, _
                         dQuant, dSc1, dSc2, dSc3, dSc4, dSc5, dSc6, nPromo, strPrzNet, nPerqta, _
                         dDaQuant, dAquant, strUnmisout, strTipovalout, dtDaData, dtAData})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dSc1 = NTSCDec(oIn(10))
        dSc2 = NTSCDec(oIn(11))
        dSc3 = NTSCDec(oIn(12))
        dSc4 = NTSCDec(oIn(13))
        dSc5 = NTSCDec(oIn(14))
        dSc6 = NTSCDec(oIn(15))
        nPromo = NTSCInt(oIn(16))
        nPerqta = NTSCInt(oIn(17))
        dDaQuant = NTSCDec(oIn(18))
        dAquant = NTSCDec(oIn(19))
        strUnmisout = NTSCStr(oIn(20))
        strTipovalout = NTSCStr(oIn(21))
        dtDaData = CDate(oIn(22))
        dtAData = CDate(oIn(23))
        Return CBool(oOut)
      End If
      '----------------

      'C'è una opzione di registro specifica del retail che va chiamata solo quando il cod. promo è 0.
      If nCodpromoin = -1 Then nCodpromoin = 0 : bRetail = True

      '-----------------------------
      'Azzera risultati
      dSc1 = 0
      dSc2 = 0
      dSc3 = 0
      dSc4 = 0
      dSc5 = 0
      dSc6 = 0
      nPromo = 0
      nPerqta = CLN__STD.bsPrSetnoqta
      ' setta altre var
      dDaQuant = 0
      dAquant = 9999999999
      strUnmisout = ""
      strTipovalout = " "
      dtDaData = New Date(1900, 1, 1)
      dtAData = New Date(1900, 1, 1)

      '-----------------------------
      'se sconti non gestiti esce ...
      'se prezzo netto esce ...
      If oApp.oGvar.strGestioneSconti = "N" Then Return True
      If strPrzNet = "S" Then Return True

      ' sconti gestiti ...
      strCoda = strCodart
      strGesvar = "N"
      strPrevar = "S"
      strCodroot = ""
      strCodvar1 = ""

      '-----------------------------
      'Se abilitata l'opzione gbRilevaPrezziDaContoFatt legge il conto di fatturazione
      If oApp.oGvar.bRilevaScontiDaContoFatt And lConto > 0 Then
        strSQL = "SELECT an_contfatt FROM anagra WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & lConto.ToString
        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If snaTmp.Rows.Count > 0 Then
          If NTSCInt(snaTmp.Rows(0)!an_contfatt) > 0 Then lConto = NTSCInt(snaTmp.Rows(0)!an_contfatt) 'trovato: lo usa al posto del conto passato alla funzione
        End If
        snaTmp.Clear()
      End If

      '-----------------------------
      'se ho impostato che la classe sconto cli viene presa dal n. listino ...
      If nListino <> 999999999 And nListino >= 0 Then
        If oApp.oGvar.strSconClCliDaList = "S" Then nClscan = nListino
      End If

      '-----------------------------
      ' prende in valore assoluto di dquant
      dQuantabs = Math.Abs(dQuant)
      If dQuantabs = 0 Then dQuantabs = 1
      ' e setta a non gestione prezzi per qta ...
      nPerqta = CLN__STD.bsPrSetnoqta

      '-----------------------------
      'verifica se l'articolo  gestito a varianti .....
      If strCoda = "" Then GoTo cerca1
      strSQL = "SELECT ar_gesvar, ar_prevar, ar_codroot, ar_codvar1 FROM artico " & _
                " WHERE ar_codart = " & CStrSQL(strCodart) & _
                " AND codditt = " & CStrSQL(strDitta)
      snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
      If snaTmp.Rows.Count > 0 Then
        strGesvar = snaTmp.Rows(0)!ar_gesvar.ToString
        strPrevar = snaTmp.Rows(0)!ar_prevar.ToString
        strCodroot = NTSCStr(snaTmp.Rows(0)!ar_codroot.ToString)
        strCodvar1 = NTSCStr(snaTmp.Rows(0)!ar_codvar1.ToString)
        If strGesvar = "S" Then ' se  a varianti cambia la base di calcolo ..
          Select Case strPrevar
            Case "N" : strCoda = strCodroot
            Case "S" : strCoda = strCodart
            Case "1" : strCoda = strCodroot & strCodvar1
          End Select
        End If
      End If
      snaTmp.Clear()

      '-----------------------------
      ' adesso cerca sconto ..
cerca1:
      ' controlla : se ci sono  sconti aggiuntivi fa un giro pi lungo ...
      If (oApp.oGvar.strTrattaSc1 = "C" Or oApp.oGvar.strTrattaSc2 = "C" Or oApp.oGvar.strTrattaSc3 = "C" Or _
          oApp.oGvar.strTrattaSc4 = "C" Or oApp.oGvar.strTrattaSc5 = "C" Or oApp.oGvar.strTrattaSc6 = "C") Then
        bCumulativi = True
      Else
        bCumulativi = False
      End If
      ' tipo valorizzazione specifica :
      If strTipoval = "B" Then GoTo EseguiB
      If strTipoval = "C" Then GoTo EseguiC
      If strTipoval = "D" Then GoTo EseguiD
      If strTipoval = "E" Then GoTo EseguiE
      If strTipoval = "F" Then GoTo EseguiF
      ' query originaria

      ' tratta sconti A : cliente/articolo
      If Not (oApp.oGvar.strTrattaScA = "N") And Not (lConto = 0) Then
        strSQL = "SELECT TOP 1 so_scont1, so_scont2, so_scont3, so_scont4, so_scont5, so_scont6, so_unmis, " & _
                " so_codtpro, so_daquant, so_aquant, so_datagg, so_datscad FROM sconti " & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND so_codart = " & CStrSQL(strCoda) & _
                " AND so_conto = " & lConto & _
                " AND so_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                " AND so_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                " AND so_daquant <= " & CDblSQL(dQuantabs.ToString) & _
                " AND so_aquant >= " & CDblSQL(dQuantabs.ToString)
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bRetail Or (bRetail And CBool(GetSettingBus("BSREGSRE", "OPZIONI", ".", "Applica_anche_prom_come_Rel10", "0", ".", "0"))) Then
            If Not bConspromo Then
              strSQL = strSQL & " AND so_codtpro = 0"
            Else
              If nCodpromoin > 0 Then
                strSQL = strSQL & " AND so_codtpro = " & nCodpromoin
              End If
            End If
          Else
            strSQL = strSQL & " AND so_codtpro = 0"
          End If
        Else
          strSQL = strSQL & " AND (so_codtpro = " & nCodpromoin & " OR so_codtpro = 0)"
        End If
        strSQL = strSQL & " ORDER BY so_codart DESC, so_conto DESC,  so_codtpro DESC, so_datagg DESC, so_daquant;"
        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
        If Not snaTmp.Rows.Count = 0 Then
          If oApp.oGvar.strTrattaSc1 = "S" Then dSc1 = NTSCDec(snaTmp.Rows(0)!so_scont1) Else dSc1 = dSc1 + NTSCDec(snaTmp.Rows(0)!so_scont1)
          If oApp.oGvar.strTrattaSc2 = "S" Then dSc2 = NTSCDec(snaTmp.Rows(0)!so_scont2) Else dSc2 = dSc2 + NTSCDec(snaTmp.Rows(0)!so_scont2)
          If oApp.oGvar.strTrattaSc3 = "S" Then dSc3 = NTSCDec(snaTmp.Rows(0)!so_scont3) Else dSc3 = dSc3 + NTSCDec(snaTmp.Rows(0)!so_scont3)
          If oApp.oGvar.strTrattaSc4 = "S" Then dSc4 = NTSCDec(snaTmp.Rows(0)!so_scont4) Else dSc4 = dSc4 + NTSCDec(snaTmp.Rows(0)!so_scont4)
          If oApp.oGvar.strTrattaSc5 = "S" Then dSc5 = NTSCDec(snaTmp.Rows(0)!so_scont5) Else dSc5 = dSc5 + NTSCDec(snaTmp.Rows(0)!so_scont5)
          If oApp.oGvar.strTrattaSc6 = "S" Then dSc6 = NTSCDec(snaTmp.Rows(0)!so_scont6) Else dSc6 = dSc6 + NTSCDec(snaTmp.Rows(0)!so_scont6)
          nPromo = NTSCInt(snaTmp.Rows(0)!so_codtpro)
          If Not (NTSCDec(snaTmp.Rows(0)!so_daquant) = 0 And NTSCDec(snaTmp.Rows(0)!so_aquant) = 9999999999) Then
            nPerqta = CLN__STD.bsPrSetsiqta
          End If
          dDaQuant = NTSCDec(snaTmp.Rows(0)!so_daquant)
          dAquant = NTSCDec(snaTmp.Rows(0)!so_aquant)
          strUnmisout = snaTmp.Rows(0)!so_unmis.ToString
          strTipovalout = "A"
          dtDaData = NTSCDate(snaTmp.Rows(0)!so_datagg)
          dtAData = NTSCDate(snaTmp.Rows(0)!so_datscad)

          If oApp.oGvar.strSconSpecNoCum = "S" Then
            'se ho settato l'opzione che se ho lo sconto specifico devo ignorare tutti gli altri, anche se cumulativi, esco
            snaTmp.Clear()
            GoTo finito
          End If
        End If
        snaTmp.Clear()

      End If ' sc A
      If strTipoval = "A" Then GoTo finito
      If strTipovalout <> " " And Not (bCumulativi) Then GoTo finito

      If oApp.oGvar.strPriorSconti = "ACBDFE" Then GoTo EseguiC
EseguiB:
      ' tipo b : articolo/cl.cliente
      If Not (oApp.oGvar.strTrattaScB = "N") And Not (nClscan = 0) Then
        strSQL = "SELECT TOP 1 so_scont1, so_scont2, so_scont3, so_scont4, so_scont5, so_scont6, so_unmis, " & _
                " so_codtpro, so_daquant, so_aquant, so_datagg, so_datscad FROM sconti " & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND so_codart = " & CStrSQL(strCoda) & _
                " AND so_conto = 0" & _
                " AND so_clscan = " & nClscan & _
                " AND so_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                " AND so_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                " AND so_daquant <= " & CDblSQL(dQuantabs.ToString) & _
                " AND so_aquant >= " & CDblSQL(dQuantabs.ToString)
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bRetail Or (bRetail And CBool(GetSettingBus("BSREGSRE", "OPZIONI", ".", "Applica_anche_prom_come_Rel10", "0", ".", "0"))) Then
            If Not bConspromo Then
              strSQL = strSQL & " AND so_codtpro = 0"
            Else
              If nCodpromoin > 0 Then
                strSQL = strSQL & " AND so_codtpro = " & nCodpromoin
              End If
            End If
          Else
            strSQL = strSQL & " AND so_codtpro = 0"
          End If
        Else
          strSQL = strSQL & " AND (so_codtpro = " & nCodpromoin & " OR so_codtpro = 0)"
        End If

        strSQL = strSQL & " ORDER BY so_codart DESC,  so_clscan DESC,  so_codtpro DESC, so_datagg DESC, so_daquant;"
        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
        If Not snaTmp.Rows.Count = 0 Then
          If strTipovalout = " " Then
            If oApp.oGvar.strTrattaSc1 = "S" Then dSc1 = NTSCDec(snaTmp.Rows(0)!so_scont1) Else dSc1 = dSc1 + NTSCDec(snaTmp.Rows(0)!so_scont1)
            If oApp.oGvar.strTrattaSc2 = "S" Then dSc2 = NTSCDec(snaTmp.Rows(0)!so_scont2) Else dSc2 = dSc2 + NTSCDec(snaTmp.Rows(0)!so_scont2)
            If oApp.oGvar.strTrattaSc3 = "S" Then dSc3 = NTSCDec(snaTmp.Rows(0)!so_scont3) Else dSc3 = dSc3 + NTSCDec(snaTmp.Rows(0)!so_scont3)
            If oApp.oGvar.strTrattaSc4 = "S" Then dSc4 = NTSCDec(snaTmp.Rows(0)!so_scont4) Else dSc4 = dSc4 + NTSCDec(snaTmp.Rows(0)!so_scont4)
            If oApp.oGvar.strTrattaSc5 = "S" Then dSc5 = NTSCDec(snaTmp.Rows(0)!so_scont5) Else dSc5 = dSc5 + NTSCDec(snaTmp.Rows(0)!so_scont5)
            If oApp.oGvar.strTrattaSc6 = "S" Then dSc6 = NTSCDec(snaTmp.Rows(0)!so_scont6) Else dSc6 = dSc6 + NTSCDec(snaTmp.Rows(0)!so_scont6)
            nPromo = NTSCInt(snaTmp.Rows(0)!so_codtpro)
            If Not (NTSCDec(snaTmp.Rows(0)!so_daquant) = 0 And NTSCDec(snaTmp.Rows(0)!so_aquant) = 9999999999) Then
              nPerqta = CLN__STD.bsPrSetsiqta
            End If
            dDaQuant = NTSCDec(snaTmp.Rows(0)!so_daquant)
            dAquant = NTSCDec(snaTmp.Rows(0)!so_aquant)
            strUnmisout = snaTmp.Rows(0)!so_unmis.ToString
            strTipovalout = "B"
            dtDaData = NTSCDate(snaTmp.Rows(0)!so_datagg)
            dtAData = NTSCDate(snaTmp.Rows(0)!so_datscad)
          Else
            If oApp.oGvar.strTrattaSc1 = "C" Then dSc1 = dSc1 + NTSCDec(snaTmp.Rows(0)!so_scont1)
            If oApp.oGvar.strTrattaSc2 = "C" Then dSc2 = dSc2 + NTSCDec(snaTmp.Rows(0)!so_scont2)
            If oApp.oGvar.strTrattaSc3 = "C" Then dSc3 = dSc3 + NTSCDec(snaTmp.Rows(0)!so_scont3)
            If oApp.oGvar.strTrattaSc4 = "C" Then dSc4 = dSc4 + NTSCDec(snaTmp.Rows(0)!so_scont4)
            If oApp.oGvar.strTrattaSc5 = "C" Then dSc5 = dSc5 + NTSCDec(snaTmp.Rows(0)!so_scont5)
            If oApp.oGvar.strTrattaSc6 = "C" Then dSc6 = dSc6 + NTSCDec(snaTmp.Rows(0)!so_scont6)
          End If

        End If
        snaTmp.Clear()


      End If ' sc B
      If strTipoval = "B" Then GoTo finito
      If strTipovalout <> " " And Not (bCumulativi) Then GoTo finito
      If oApp.oGvar.strPriorSconti = "ACBDFE" Then GoTo EseguiD
EseguiC:
      ' tratta c : cliente / classe art.
      If Not (oApp.oGvar.strTrattaScC = "N") And Not (lConto = 0) And Not (nClscar = 0) Then
        strSQL = "SELECT TOP 1 so_scont1, so_scont2, so_scont3, so_scont4, so_scont5, so_scont6, " & _
                " so_unmis, so_codtpro, so_daquant, so_aquant, so_datagg, so_datscad FROM sconti " & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND so_codart = ' ' " & _
                " AND so_conto = " & lConto & _
                " AND so_clscar = " & nClscar & _
                " AND so_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                " AND so_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & " " & _
                " AND so_daquant <= " & CDblSQL(dQuantabs.ToString) & _
                " AND so_aquant >= " & CDblSQL(dQuantabs.ToString)
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bRetail Or (bRetail And CBool(GetSettingBus("BSREGSRE", "OPZIONI", ".", "Applica_anche_prom_come_Rel10", "0", ".", "0"))) Then
            If Not bConspromo Then
              strSQL = strSQL & " AND so_codtpro = 0"
            Else
              If nCodpromoin > 0 Then
                strSQL = strSQL & " AND so_codtpro = " & nCodpromoin
              End If
            End If
          Else
            strSQL = strSQL & " AND so_codtpro = 0"
          End If
        Else
          strSQL = strSQL & " AND (so_codtpro = " & nCodpromoin & " OR so_codtpro = 0)"
        End If
        strSQL = strSQL & " ORDER BY  so_conto DESC,  so_clscar DESC, so_codtpro DESC, so_datagg DESC, so_daquant;"
        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
        If Not snaTmp.Rows.Count = 0 Then
          If strTipovalout = " " Then
            If oApp.oGvar.strTrattaSc1 = "S" Then dSc1 = NTSCDec(snaTmp.Rows(0)!so_scont1) Else dSc1 = dSc1 + NTSCDec(snaTmp.Rows(0)!so_scont1)
            If oApp.oGvar.strTrattaSc2 = "S" Then dSc2 = NTSCDec(snaTmp.Rows(0)!so_scont2) Else dSc2 = dSc2 + NTSCDec(snaTmp.Rows(0)!so_scont2)
            If oApp.oGvar.strTrattaSc3 = "S" Then dSc3 = NTSCDec(snaTmp.Rows(0)!so_scont3) Else dSc3 = dSc3 + NTSCDec(snaTmp.Rows(0)!so_scont3)
            If oApp.oGvar.strTrattaSc4 = "S" Then dSc4 = NTSCDec(snaTmp.Rows(0)!so_scont4) Else dSc4 = dSc4 + NTSCDec(snaTmp.Rows(0)!so_scont4)
            If oApp.oGvar.strTrattaSc5 = "S" Then dSc5 = NTSCDec(snaTmp.Rows(0)!so_scont5) Else dSc5 = dSc5 + NTSCDec(snaTmp.Rows(0)!so_scont5)
            If oApp.oGvar.strTrattaSc6 = "S" Then dSc6 = NTSCDec(snaTmp.Rows(0)!so_scont6) Else dSc6 = dSc6 + NTSCDec(snaTmp.Rows(0)!so_scont6)
            nPromo = NTSCInt(snaTmp.Rows(0)!so_codtpro)
            If Not (NTSCDec(snaTmp.Rows(0)!so_daquant) = 0 And NTSCDec(snaTmp.Rows(0)!so_aquant) = 9999999999) Then
              nPerqta = CLN__STD.bsPrSetsiqta
            End If
            dDaQuant = NTSCDec(snaTmp.Rows(0)!so_daquant)
            dAquant = NTSCDec(snaTmp.Rows(0)!so_aquant)
            strUnmisout = snaTmp.Rows(0)!so_unmis.ToString
            strTipovalout = "C"
            dtDaData = NTSCDate(snaTmp.Rows(0)!so_datagg)
            dtAData = NTSCDate(snaTmp.Rows(0)!so_datscad)
          Else
            If oApp.oGvar.strTrattaSc1 = "C" Then dSc1 = dSc1 + NTSCDec(snaTmp.Rows(0)!so_scont1)
            If oApp.oGvar.strTrattaSc2 = "C" Then dSc2 = dSc2 + NTSCDec(snaTmp.Rows(0)!so_scont2)
            If oApp.oGvar.strTrattaSc3 = "C" Then dSc3 = dSc3 + NTSCDec(snaTmp.Rows(0)!so_scont3)
            If oApp.oGvar.strTrattaSc4 = "C" Then dSc4 = dSc4 + NTSCDec(snaTmp.Rows(0)!so_scont4)
            If oApp.oGvar.strTrattaSc5 = "C" Then dSc5 = dSc5 + NTSCDec(snaTmp.Rows(0)!so_scont5)
            If oApp.oGvar.strTrattaSc6 = "C" Then dSc6 = dSc6 + NTSCDec(snaTmp.Rows(0)!so_scont6)
          End If

        End If
        snaTmp.Clear()


      End If ' sc C
      If strTipoval = "C" Then GoTo finito
      If strTipovalout <> " " And Not (bCumulativi) Then GoTo finito
      If oApp.oGvar.strPriorSconti = "ACBDFE" Then GoTo EseguiB
EseguiD:
      ' tipo D : classe art/classe conto
      If Not (oApp.oGvar.strTrattaScD = "N") And Not (nClscar = 0) And Not (nClscan = 0) Then
        strSQL = "SELECT TOP 1 so_scont1, so_scont2, so_scont3, so_scont4, so_scont5, so_scont6, so_unmis, " & _
                " so_codtpro, so_daquant, so_aquant, so_datagg, so_datscad FROM sconti " & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND so_codart =  ' ' " & _
                " AND so_conto = 0 " & _
                " AND so_clscan = " & nClscan & _
                " AND so_clscar = " & nClscar & _
                " AND so_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                " AND so_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & " " & _
                " AND so_daquant <= " & CDblSQL(dQuantabs.ToString) & _
                " AND so_aquant >= " & CDblSQL(dQuantabs.ToString)
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bRetail Or (bRetail And CBool(GetSettingBus("BSREGSRE", "OPZIONI", ".", "Applica_anche_prom_come_Rel10", "0", ".", "0"))) Then
            If Not bConspromo Then
              strSQL = strSQL & " AND so_codtpro = 0"
            Else
              If nCodpromoin > 0 Then
                strSQL = strSQL & " AND so_codtpro = " & nCodpromoin
              End If
            End If
          Else
            strSQL = strSQL & " AND so_codtpro = 0"
          End If
        Else
          strSQL = strSQL & " AND (so_codtpro = " & nCodpromoin & " OR so_codtpro = 0)"
        End If
        strSQL = strSQL & " ORDER BY so_codart DESC, so_conto DESC, so_clscan DESC, so_clscar DESC, so_codtpro DESC, so_datagg DESC, so_daquant;"
        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
        If Not snaTmp.Rows.Count = 0 Then
          If strTipovalout = " " Then
            If oApp.oGvar.strTrattaSc1 = "S" Then dSc1 = NTSCDec(snaTmp.Rows(0)!so_scont1) Else dSc1 = dSc1 + NTSCDec(snaTmp.Rows(0)!so_scont1)
            If oApp.oGvar.strTrattaSc2 = "S" Then dSc2 = NTSCDec(snaTmp.Rows(0)!so_scont2) Else dSc2 = dSc2 + NTSCDec(snaTmp.Rows(0)!so_scont2)
            If oApp.oGvar.strTrattaSc3 = "S" Then dSc3 = NTSCDec(snaTmp.Rows(0)!so_scont3) Else dSc3 = dSc3 + NTSCDec(snaTmp.Rows(0)!so_scont3)
            If oApp.oGvar.strTrattaSc4 = "S" Then dSc4 = NTSCDec(snaTmp.Rows(0)!so_scont4) Else dSc4 = dSc4 + NTSCDec(snaTmp.Rows(0)!so_scont4)
            If oApp.oGvar.strTrattaSc5 = "S" Then dSc5 = NTSCDec(snaTmp.Rows(0)!so_scont5) Else dSc5 = dSc5 + NTSCDec(snaTmp.Rows(0)!so_scont5)
            If oApp.oGvar.strTrattaSc6 = "S" Then dSc6 = NTSCDec(snaTmp.Rows(0)!so_scont6) Else dSc6 = dSc6 + NTSCDec(snaTmp.Rows(0)!so_scont6)
            nPromo = NTSCInt(snaTmp.Rows(0)!so_codtpro)
            If Not (NTSCDec(snaTmp.Rows(0)!so_daquant) = 0 And NTSCDec(snaTmp.Rows(0)!so_aquant) = 9999999999) Then
              nPerqta = CLN__STD.bsPrSetsiqta
            End If
            dDaQuant = NTSCDec(snaTmp.Rows(0)!so_daquant)
            dAquant = NTSCDec(snaTmp.Rows(0)!so_aquant)
            strUnmisout = snaTmp.Rows(0)!so_unmis.ToString
            strTipovalout = "D"
            dtDaData = NTSCDate(snaTmp.Rows(0)!so_datagg)
            dtAData = NTSCDate(snaTmp.Rows(0)!so_datscad)
          Else
            If oApp.oGvar.strTrattaSc1 = "C" Then dSc1 = dSc1 + NTSCDec(snaTmp.Rows(0)!so_scont1)
            If oApp.oGvar.strTrattaSc2 = "C" Then dSc2 = dSc2 + NTSCDec(snaTmp.Rows(0)!so_scont2)
            If oApp.oGvar.strTrattaSc3 = "C" Then dSc3 = dSc3 + NTSCDec(snaTmp.Rows(0)!so_scont3)
            If oApp.oGvar.strTrattaSc4 = "C" Then dSc4 = dSc4 + NTSCDec(snaTmp.Rows(0)!so_scont4)
            If oApp.oGvar.strTrattaSc5 = "C" Then dSc5 = dSc5 + NTSCDec(snaTmp.Rows(0)!so_scont5)
            If oApp.oGvar.strTrattaSc6 = "C" Then dSc6 = dSc6 + NTSCDec(snaTmp.Rows(0)!so_scont6)
          End If

        End If
        snaTmp.Clear()


      End If ' sc D
      If strTipoval = "D" Then GoTo finito
      If strTipovalout <> " " And Not (bCumulativi) Then GoTo finito
      If oApp.oGvar.strPriorSconti = "ACBDFE" Then GoTo EseguiF
      ' tipo E generico articolo
EseguiE:
      If Not (oApp.oGvar.strTrattaScA = "E") Then
        strSQL = "SELECT TOP 1 so_scont1, so_scont2, so_scont3, so_scont4, so_scont5, so_scont6, so_unmis, " & _
                " so_codtpro, so_daquant, so_aquant, so_datagg, so_datscad FROM sconti " & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND so_codart = " & CStrSQL(strCoda) & _
                " AND so_conto = 0 " & _
                " AND so_clscan = 0 " & _
                " AND so_clscar = 0 " & _
                " AND so_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                " AND so_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & " " & _
                " AND so_daquant <= " & CDblSQL(dQuantabs.ToString) & _
                " AND so_aquant >= " & CDblSQL(dQuantabs.ToString)
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bRetail Or (bRetail And CBool(GetSettingBus("BSREGSRE", "OPZIONI", ".", "Applica_anche_prom_come_Rel10", "0", ".", "0"))) Then
            If Not bConspromo Then
              strSQL = strSQL & " AND so_codtpro = 0"
            Else
              If nCodpromoin > 0 Then
                strSQL = strSQL & " AND so_codtpro = " & nCodpromoin
              End If
            End If
          Else
            strSQL = strSQL & " AND so_codtpro = 0"
          End If
        Else
          strSQL = strSQL & " AND (so_codtpro = " & nCodpromoin & " OR so_codtpro = 0)"
        End If
        strSQL = strSQL & " ORDER BY so_codart DESC, so_conto DESC, so_clscan DESC, so_clscar DESC, so_codtpro DESC, so_datagg DESC, so_daquant;"
        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
        If Not snaTmp.Rows.Count = 0 Then
          If strTipovalout = " " Then
            If oApp.oGvar.strTrattaSc1 = "S" Then dSc1 = NTSCDec(snaTmp.Rows(0)!so_scont1) Else dSc1 = dSc1 + NTSCDec(snaTmp.Rows(0)!so_scont1)
            If oApp.oGvar.strTrattaSc2 = "S" Then dSc2 = NTSCDec(snaTmp.Rows(0)!so_scont2) Else dSc2 = dSc2 + NTSCDec(snaTmp.Rows(0)!so_scont2)
            If oApp.oGvar.strTrattaSc3 = "S" Then dSc3 = NTSCDec(snaTmp.Rows(0)!so_scont3) Else dSc3 = dSc3 + NTSCDec(snaTmp.Rows(0)!so_scont3)
            If oApp.oGvar.strTrattaSc4 = "S" Then dSc4 = NTSCDec(snaTmp.Rows(0)!so_scont4) Else dSc4 = dSc4 + NTSCDec(snaTmp.Rows(0)!so_scont4)
            If oApp.oGvar.strTrattaSc5 = "S" Then dSc5 = NTSCDec(snaTmp.Rows(0)!so_scont5) Else dSc5 = dSc5 + NTSCDec(snaTmp.Rows(0)!so_scont5)
            If oApp.oGvar.strTrattaSc6 = "S" Then dSc6 = NTSCDec(snaTmp.Rows(0)!so_scont6) Else dSc6 = dSc6 + NTSCDec(snaTmp.Rows(0)!so_scont6)
            nPromo = NTSCInt(snaTmp.Rows(0)!so_codtpro)
            If Not (NTSCDec(snaTmp.Rows(0)!so_daquant) = 0 And NTSCDec(snaTmp.Rows(0)!so_aquant) = 9999999999) Then
              nPerqta = CLN__STD.bsPrSetsiqta
            End If
            dDaQuant = NTSCDec(snaTmp.Rows(0)!so_daquant)
            dAquant = NTSCDec(snaTmp.Rows(0)!so_aquant)
            strUnmisout = snaTmp.Rows(0)!so_unmis.ToString
            strTipovalout = "E"
            dtDaData = NTSCDate(snaTmp.Rows(0)!so_datagg)
            dtAData = NTSCDate(snaTmp.Rows(0)!so_datscad)
          Else
            If oApp.oGvar.strTrattaSc1 = "C" Then dSc1 = dSc1 + NTSCDec(snaTmp.Rows(0)!so_scont1)
            If oApp.oGvar.strTrattaSc2 = "C" Then dSc2 = dSc2 + NTSCDec(snaTmp.Rows(0)!so_scont2)
            If oApp.oGvar.strTrattaSc3 = "C" Then dSc3 = dSc3 + NTSCDec(snaTmp.Rows(0)!so_scont3)
            If oApp.oGvar.strTrattaSc4 = "C" Then dSc4 = dSc4 + NTSCDec(snaTmp.Rows(0)!so_scont4)
            If oApp.oGvar.strTrattaSc5 = "C" Then dSc5 = dSc5 + NTSCDec(snaTmp.Rows(0)!so_scont5)
            If oApp.oGvar.strTrattaSc6 = "C" Then dSc6 = dSc6 + NTSCDec(snaTmp.Rows(0)!so_scont6)
          End If

        End If
        snaTmp.Clear()


      End If ' sc E
      If strTipoval = "E" Then GoTo finito
      If strTipovalout <> " " And Not (bCumulativi) Then GoTo finito
      If oApp.oGvar.strPriorSconti = "ACBDFE" Then GoTo finito
EseguiF:
      ' tipo F generico cliente ...
      If Not (oApp.oGvar.strTrattaScF = "N") And Not (lConto = 0) Then
        strSQL = "SELECT TOP 1 so_scont1, so_scont2, so_scont3, so_scont4, so_scont5, so_scont6, so_unmis, " & _
                " so_codtpro, so_daquant, so_aquant, so_datagg, so_datscad FROM sconti " & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND so_codart = ' ' " & _
                " AND so_conto = " & lConto & _
                " AND so_clscan = 0 " & _
                " AND so_clscar = 0 " & _
                " AND so_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                " AND so_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & " " & _
                " AND so_daquant <= " & CDblSQL(dQuantabs.ToString) & _
                " AND so_aquant >= " & CDblSQL(dQuantabs.ToString)
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bRetail Or (bRetail And CBool(GetSettingBus("BSREGSRE", "OPZIONI", ".", "Applica_anche_prom_come_Rel10", "0", ".", "0"))) Then
            If Not bConspromo Then
              strSQL = strSQL & " AND so_codtpro = 0"
            Else
              If nCodpromoin > 0 Then
                strSQL = strSQL & " AND so_codtpro = " & nCodpromoin
              End If
            End If
          Else
            strSQL = strSQL & " AND so_codtpro = 0"
          End If
        Else
          strSQL = strSQL & " AND (so_codtpro = " & nCodpromoin & " OR so_codtpro = 0)"
        End If
        strSQL = strSQL & " ORDER BY so_codart DESC, so_conto DESC, so_clscan DESC, so_clscar DESC, so_codtpro DESC, so_datagg DESC, so_daquant;"
        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
        If Not snaTmp.Rows.Count = 0 Then
          If strTipovalout = " " Then
            If oApp.oGvar.strTrattaSc1 = "S" Then dSc1 = NTSCDec(snaTmp.Rows(0)!so_scont1) Else dSc1 = dSc1 + NTSCDec(snaTmp.Rows(0)!so_scont1)
            If oApp.oGvar.strTrattaSc2 = "S" Then dSc2 = NTSCDec(snaTmp.Rows(0)!so_scont2) Else dSc2 = dSc2 + NTSCDec(snaTmp.Rows(0)!so_scont2)
            If oApp.oGvar.strTrattaSc3 = "S" Then dSc3 = NTSCDec(snaTmp.Rows(0)!so_scont3) Else dSc3 = dSc3 + NTSCDec(snaTmp.Rows(0)!so_scont3)
            If oApp.oGvar.strTrattaSc4 = "S" Then dSc4 = NTSCDec(snaTmp.Rows(0)!so_scont4) Else dSc4 = dSc4 + NTSCDec(snaTmp.Rows(0)!so_scont4)
            If oApp.oGvar.strTrattaSc5 = "S" Then dSc5 = NTSCDec(snaTmp.Rows(0)!so_scont5) Else dSc5 = dSc5 + NTSCDec(snaTmp.Rows(0)!so_scont5)
            If oApp.oGvar.strTrattaSc6 = "S" Then dSc6 = NTSCDec(snaTmp.Rows(0)!so_scont6) Else dSc6 = dSc6 + NTSCDec(snaTmp.Rows(0)!so_scont6)
            nPromo = NTSCInt(snaTmp.Rows(0)!so_codtpro)
            If Not (NTSCDec(snaTmp.Rows(0)!so_daquant) = 0 And NTSCDec(snaTmp.Rows(0)!so_aquant) = 9999999999) Then
              nPerqta = CLN__STD.bsPrSetsiqta
            End If
            dDaQuant = NTSCDec(snaTmp.Rows(0)!so_daquant)
            dAquant = NTSCDec(snaTmp.Rows(0)!so_aquant)
            strUnmisout = snaTmp.Rows(0)!so_unmis.ToString
            strTipovalout = "F"
            dtDaData = NTSCDate(snaTmp.Rows(0)!so_datagg)
            dtAData = NTSCDate(snaTmp.Rows(0)!so_datscad)
          Else
            If oApp.oGvar.strTrattaSc1 = "C" Then dSc1 = dSc1 + NTSCDec(snaTmp.Rows(0)!so_scont1)
            If oApp.oGvar.strTrattaSc2 = "C" Then dSc2 = dSc2 + NTSCDec(snaTmp.Rows(0)!so_scont2)
            If oApp.oGvar.strTrattaSc3 = "C" Then dSc3 = dSc3 + NTSCDec(snaTmp.Rows(0)!so_scont3)
            If oApp.oGvar.strTrattaSc4 = "C" Then dSc4 = dSc4 + NTSCDec(snaTmp.Rows(0)!so_scont4)
            If oApp.oGvar.strTrattaSc5 = "C" Then dSc5 = dSc5 + NTSCDec(snaTmp.Rows(0)!so_scont5)
            If oApp.oGvar.strTrattaSc6 = "C" Then dSc6 = dSc6 + NTSCDec(snaTmp.Rows(0)!so_scont6)
          End If

        End If
        snaTmp.Clear()
      End If ' sc F
      If oApp.oGvar.strPriorSconti = "ACBDFE" Then GoTo EseguiE

finito:
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function AgenteGestitoSCPR(ByVal strDitta As String, ByVal nCodage As Integer) As Boolean
    'ritorna true se l'agnete  gestito a sconti/provvigioni, diversamente false
    Dim strSQL As String = ""
    Dim dtrTmp As DataTable
    Dim bResult As Boolean = False
    Try

      strSQL = "SELECT tb_tiptab FROM tabcage" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND tb_codcage = " & nCodage
      dtrTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
      If dtrTmp.Rows.Count > 0 Then
        If dtrTmp.Rows(0)!tb_tiptab.ToString = "T" Then bResult = True
      End If
      dtrTmp.Clear()

      Return bResult

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  '--------------------------------------------------------------------------
  ' PASSATO articolo, conto, classe articolo, classe conto, tipo valore cercato (G=generico art., H=gen. conto, I=generico agente, A=Articolo/classe conto, C=Conto/classe art, K=classe/classe, S=speciale conto/articolo, P=prevalente), datadoc, codage
  '
  '
  ' RESTITUISCE dProvv, nPromo, dVprovv, Tipo valore trovato
  '
  ' OU: CercaProvv = False se si  verificato un errore
  '--------------------------------------------------------------------------
  Public Overridable Function CercaProvv(ByVal strDitta As String, ByVal strCodart As String, ByVal lConto As Integer, _
                            ByVal nClprar As Integer, ByVal nClpran As Integer, ByVal strTipoval As String, _
                            ByVal bConspromo As Boolean, ByVal nCodpromoin As Integer, ByVal dtDatdoc As Date, _
                            ByVal nCodage As Integer, ByVal dSc1 As Decimal, ByVal dSc2 As Decimal, _
                            ByVal dSc3 As Decimal, ByVal dSc4 As Decimal, ByVal dSc5 As Decimal, ByVal dSc6 As Decimal, _
                            ByVal dSt1 As Decimal, ByVal dSt2 As Decimal, ByVal dPrezzo As Decimal, _
                            ByVal strUnmisin As String, ByVal dQuant As Decimal, ByRef dProvv As Decimal, _
                            Optional ByRef strError As String = "", Optional ByRef nPromo As Integer = 0, _
                            Optional ByRef dVprovv As Decimal = 0, Optional ByRef strTipovalout As String = "") As Boolean
    ' NB : allo stato non ritorna tipovalout, n applica priorità come invece negli sconti ...
    ' e non testa il tipo valorizzazione in input

    'strError = 'errori da restituire all'entity

    Dim strSQL As String = ""
    Dim snaTmp As DataTable
    Dim strCoda As String
    Dim strGesvar As String
    Dim strCodvar1 As String
    Dim strCodroot As String
    Dim strPrevar As String
    Dim strOpzscpr As String
    'Dim snaRegDir As DataTable
    'Dim snaRegProp As DataTable
    Dim strTipo As String
    Dim strUnmis As String
    Dim strSQLUM As String

    Dim bRetail As Boolean = False
    Try
      'C'è una opzione di registro specifica del retail che va chiamata solo quando il cod. promo è 0.
      If nCodpromoin = -1 Then nCodpromoin = 0 : bRetail = True

      '----------------------------------------
      ' NB : allo stato non ritorna tipovalout, n applica priorità come invece negli sconti ...
      ' e non testa il tipo valorizzazione in input

      '----------------------------------------
      'Azzera risultati
      strError = ""
      dProvv = 0
      strCoda = strCodart
      strGesvar = "N"
      strPrevar = "S"
      strCodroot = ""
      strCodvar1 = ""
      strUnmis = ""
      strSQLUM = ""
      ' innanzitutto setto a zero valori assoluti di provvigioni
      dVprovv = 0
      strTipovalout = " "
      ' se provv non gestiti esce ...
      If nCodage = 0 Then Return True

      If oApp.oGvar.strGestioneProvv = "N" Then Return True

      '----------------------------------------
      'Ignora gli sconti di riga se richiesto con le apposite ozpioni di registro
      If oApp.oGvar.bIgnoraScontR1CercaProvv Then dSc1 = 0
      If oApp.oGvar.bIgnoraScontR2CercaProvv Then dSc2 = 0
      If oApp.oGvar.bIgnoraScontR3CercaProvv Then dSc3 = 0
      If oApp.oGvar.bIgnoraScontR4CercaProvv Then dSc4 = 0
      If oApp.oGvar.bIgnoraScontR5CercaProvv Then dSc5 = 0
      If oApp.oGvar.bIgnoraScontR6CercaProvv Then dSc6 = 0
      If oApp.oGvar.bIgnoraScontT1CercaProvv Then dSt1 = 0
      If oApp.oGvar.bIgnoraScontT2CercaProvv Then dSt2 = 0

      '----------------------------------------
      'Cerca se esiste il codice dell'agente produttore tipo, nel caso in cui esiste
      'tutte le provvigioni vengono fatte con il codice dell'agente tipo.
      strSQL = "SELECT tb_agetipodir FROM tabcage " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tb_codcage=" & nCodage
      snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
      If Not snaTmp.Rows.Count = 0 Then
        If NTSCInt(snaTmp.Rows(0)!tb_agetipodir) > 0 Then nCodage = NTSCInt(snaTmp.Rows(0)!tb_agetipodir)
      End If
      snaTmp.Clear()

      '----------------------------------------
      ' cerca prima se articolo a variante e la sua UMP
      If strCoda = "" Then GoTo cerca1
      strSQL = "select ar_gesvar, ar_prevar, ar_codroot, ar_codvar1, ar_unmis  from artico " & _
               " where ar_codart = " & CStrSQL(strCodart) & _
               " AND codditt = " & CStrSQL(strDitta) & " "
      snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
      If Not snaTmp.Rows.Count = 0 Then
        strGesvar = snaTmp.Rows(0)!ar_gesvar.ToString
        strPrevar = snaTmp.Rows(0)!ar_prevar.ToString
        strCodroot = snaTmp.Rows(0)!ar_codroot.ToString
        strCodvar1 = snaTmp.Rows(0)!ar_codvar1.ToString
        strUnmis = snaTmp.Rows(0)!ar_unmis.ToString

        If strGesvar = "S" Then ' se  a varianti cambia la base di calcolo ..
          Select Case strPrevar
            Case "N" : strCoda = strCodroot
            Case "S" : strCoda = strCodart
            Case "1" : strCoda = strCodroot & strCodvar1
          End Select
        End If
      End If
      snaTmp.Clear()


      'unità di misura sui prezzi
      If Trim(strUnmisin) = "" Then
        strSQLUM = " And (lc_unmis = ' ' Or lc_unmis = " & CStrSQL(strUnmis) & ")"
      Else
        strSQLUM = " And lc_unmis = " & CStrSQL(strUnmisin)
      End If

      '----------------------------------------
      ' adesso cerca sconto ..
cerca1:
      'Cerca il tipo di gestione dell'agente
      strSQL = "SELECT tb_tiptab FROM tabcage WHERE tb_codcage=" & nCodage & " AND codditt = " & CStrSQL(strDitta)
      snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
      If Not snaTmp.Rows.Count = 0 Then
        strTipo = snaTmp.Rows(0)!tb_tiptab.ToString
      Else
        snaTmp.Clear()
        Return False
      End If
      snaTmp.Clear()

      ' strTipo fornisce la priorità tra articoli (ABCDEF) e cliente (ACBDFE)
      If strTipo = "T" And Not oApp.oGvar.bScPrconDerogaCliArt Then GoTo relScPr

      ' adesso il caso normale ....
      ' tipo valorizzazione specifica :
      If strTipoval = "B" Then GoTo EseguiB
      If strTipoval = "C" Then GoTo EseguiC
      If strTipoval = "D" Then GoTo EseguiD
      If strTipoval = "E" Then GoTo EseguiE
      If strTipoval = "F" Then GoTo EseguiF
      If strTipoval = "G" Then GoTo EseguiG ' solo agente

      '----------------------------------------------------------------------
      ' tratta provvigioni A
      If Not (oApp.oGvar.strTrattaPrA = "N") And Not (lConto = 0) And Not (Trim(strCodart) = "") Then
        strSQL = " SELECT TOP 1 per_provv, per_codtpro, per_vprovv FROM perprov " & _
                 " WHERE (per_codcage = " & nCodage & " OR per_codcage = 0) " & _
                 " AND per_codart = " & CStrSQL(strCoda) & _
                 " AND per_conto = " & lConto & _
                 " AND per_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                 " AND per_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                 " AND codditt = " & CStrSQL(strDitta)
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bRetail Or (bRetail And CBool(GetSettingBus("BSREGSRE", "OPZIONI", ".", "Applica_anche_prom_come_Rel10", "0", ".", "0"))) Then
            If Not bConspromo Then
              strSQL = strSQL & " AND per_codtpro = 0"
            Else
              If nCodpromoin > 0 Then
                strSQL = strSQL & " AND per_codtpro = " & nCodpromoin
              End If
            End If
          Else
            If nCodpromoin > 0 Then
              strSQL = strSQL & " AND (per_codtpro = " & nCodpromoin & " OR per_codtpro = 0)"
            Else
              strSQL = strSQL & " AND per_codtpro = 0"
            End If
          End If
        Else
          strSQL = strSQL & " AND per_codtpro = 0"
        End If
        strSQL = strSQL & " ORDER BY per_codcage DESC, per_codtpro DESC, per_datagg DESC"

        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
        If Not snaTmp.Rows.Count = 0 Then
          dProvv = NTSCDec(snaTmp.Rows(0)!per_provv)
          nPromo = NTSCInt(snaTmp.Rows(0)!per_codtpro)
          dVprovv = NTSCDec(snaTmp.Rows(0)!per_vprovv)
          strTipovalout = "A"
        End If
        snaTmp.Clear()
      End If ' tratta provvigioni A
      '---------------
      If strTipoval = "A" Then GoTo finito
      If strTipovalout = "A" Then GoTo finito
      If strTipo = "C" Then GoTo EseguiC
EseguiB:

      '----------------------------------------------------------------------
      ' tratta provvigioni B
      If Not (oApp.oGvar.strTrattaPrB = "N") And Not (nClpran = 0) And Not (Trim(strCodart) = "") Then
        strSQL = " SELECT TOP 1 per_provv, per_codtpro, per_vprovv FROM perprov " & _
                 " WHERE (per_codcage = " & nCodage & " OR per_codcage = 0)" & _
                 " AND per_codart = " & CStrSQL(strCoda) & _
                 " AND per_clpran = " & nClpran & _
                 " AND per_conto = 0 " & _
                 " AND per_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                 " AND per_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                 " AND codditt = " & CStrSQL(strDitta)
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bConspromo Then
            strSQL = strSQL & " AND per_codtpro = 0"
          Else
            If nCodpromoin > 0 Then
              strSQL = strSQL & " AND per_codtpro = " & nCodpromoin
            End If
          End If
        Else
          If nCodpromoin > 0 Then
            strSQL = strSQL & " AND (per_codtpro = " & nCodpromoin & " OR per_codtpro = 0)"
          Else
            strSQL = strSQL & " AND per_codtpro = 0"
          End If
        End If
        strSQL = strSQL & " ORDER BY per_codcage DESC, per_codtpro DESC, per_datagg DESC"

        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
        If Not snaTmp.Rows.Count = 0 Then
          dProvv = NTSCDec(snaTmp.Rows(0)!per_provv)
          nPromo = NTSCInt(snaTmp.Rows(0)!per_codtpro)
          dVprovv = NTSCDec(snaTmp.Rows(0)!per_vprovv)
          strTipovalout = "B"
        End If
        snaTmp.Clear()
      End If ' tratta pr B
      If strTipoval = "B" Then GoTo finito
      If strTipovalout = "B" Then GoTo finito
      If strTipo = "C" Then GoTo EseguiD

EseguiC:  ' cliente/classe articolo
      '----------------------------------------------------------------------
      ' tratta provvigioni C
      If Not (oApp.oGvar.strTrattaPrC = "N") And Not (nClprar = 0) And Not (lConto = 0) Then
        strSQL = " SELECT TOP 1 per_provv, per_codtpro, per_vprovv FROM perprov " & _
                 " WHERE (per_codcage = " & nCodage & " OR per_codcage = 0)" & _
                 " AND per_codart = ' ' " & _
                 " AND per_clprar = " & nClprar & _
                 " AND per_conto = " & lConto & _
                 " AND per_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                 " AND per_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                 " AND codditt = " & CStrSQL(strDitta)
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bConspromo Then
            strSQL = strSQL & " AND per_codtpro = 0"
          Else
            If nCodpromoin > 0 Then
              strSQL = strSQL & " AND per_codtpro = " & nCodpromoin
            End If
          End If
        Else
          If nCodpromoin > 0 Then
            strSQL = strSQL & " AND (per_codtpro = " & nCodpromoin & " OR per_codtpro = 0)"
          Else
            strSQL = strSQL & " AND per_codtpro = 0"
          End If
        End If
        strSQL = strSQL & " ORDER BY per_codcage DESC, per_codtpro DESC, per_datagg DESC"

        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
        If Not snaTmp.Rows.Count = 0 Then
          dProvv = NTSCDec(snaTmp.Rows(0)!per_provv)
          nPromo = NTSCInt(snaTmp.Rows(0)!per_codtpro)
          dVprovv = NTSCDec(snaTmp.Rows(0)!per_vprovv)
          strTipovalout = "C"
        End If
        snaTmp.Clear()
      End If ' tratta pr C

      If strTipoval = "C" Then GoTo finito
      If strTipovalout = "C" Then GoTo finito
      If strTipo = "C" Then GoTo EseguiB

EseguiD:  ' classe cliente/classe articolo
      '----------------------------------------------------------------------
      ' tratta provvigioni D
      If Not (oApp.oGvar.strTrattaPrD = "N") And Not (nClprar = 0) And Not (nClpran = 0) Then
        strSQL = " SELECT TOP 1 per_provv, per_codtpro, per_vprovv FROM perprov " & _
                 " WHERE (per_codcage = " & nCodage & " OR per_codcage = 0)" & _
                 " AND per_codart = ' ' " & _
                 " AND per_clprar = " & nClprar & _
                 " AND per_clpran = " & nClpran & _
                 " AND per_conto = 0  " & _
                 " AND per_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                 " AND per_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                 " AND codditt = " & CStrSQL(strDitta)
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bConspromo Then
            strSQL = strSQL & " AND per_codtpro = 0"
          Else
            If nCodpromoin > 0 Then
              strSQL = strSQL & " AND per_codtpro = " & nCodpromoin
            End If
          End If
        Else
          If nCodpromoin > 0 Then
            strSQL = strSQL & " AND (per_codtpro = " & nCodpromoin & " OR per_codtpro = 0)"
          Else
            strSQL = strSQL & " AND per_codtpro = 0"
          End If
        End If
        strSQL = strSQL & " ORDER BY per_codcage DESC, per_codtpro DESC, per_datagg DESC"

        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
        If Not snaTmp.Rows.Count = 0 Then
          dProvv = NTSCDec(snaTmp.Rows(0)!per_provv)
          nPromo = NTSCInt(snaTmp.Rows(0)!per_codtpro)
          dVprovv = NTSCDec(snaTmp.Rows(0)!per_vprovv)
          strTipovalout = "D"
        End If
        snaTmp.Clear()
      End If ' tratta pr D
      If strTipoval = "D" Then GoTo finito
      If strTipovalout = "D" Then GoTo finito
      If strTipo = "C" Then GoTo EseguiF

EseguiE:  ' generico articolo
      '----------------------------------------------------------------------
      ' tratta provvigioni E
      If Not (oApp.oGvar.strTrattaPrE = "N") And Not (strCodart = " ") Then
        strSQL = " SELECT TOP 1 per_provv, per_codtpro, per_vprovv FROM perprov " & _
                 " WHERE (per_codcage = " & nCodage & " OR per_codcage = 0)" & _
                 " AND per_codart = " & CStrSQL(strCoda) & _
                 " AND per_clprar = 0 " & _
                 " AND per_clpran = 0 " & _
                 " AND per_conto = 0 " & _
                 " AND per_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                 " AND per_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                 " AND codditt = " & CStrSQL(strDitta)
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bConspromo Then
            strSQL = strSQL & " AND per_codtpro = 0"
          Else
            If nCodpromoin > 0 Then
              strSQL = strSQL & " AND per_codtpro = " & nCodpromoin
            End If
          End If
        Else
          If nCodpromoin > 0 Then
            strSQL = strSQL & " AND (per_codtpro = " & nCodpromoin & " OR per_codtpro = 0)"
          Else
            strSQL = strSQL & " AND per_codtpro = 0"
          End If
        End If
        strSQL = strSQL & " ORDER BY per_codcage DESC, per_codtpro DESC, per_datagg DESC"

        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
        If Not snaTmp.Rows.Count = 0 Then
          dProvv = NTSCDec(snaTmp.Rows(0)!per_provv)
          nPromo = NTSCInt(snaTmp.Rows(0)!per_codtpro)
          dVprovv = NTSCDec(snaTmp.Rows(0)!per_vprovv)
          strTipovalout = "E"
        End If
        snaTmp.Clear()
      End If ' tratta pr E
      If strTipoval = "E" Then GoTo finito
      If strTipovalout = "E" Then GoTo finito
      If strTipo = "C" Then GoTo EseguiG

EseguiF:  ' generico conto
      '----------------------------------------------------------------------
      ' tratta provvigioni F
      If Not (oApp.oGvar.strTrattaPrF = "N") And Not (lConto = 0) Then
        strSQL = " SELECT TOP 1 per_provv, per_codtpro, per_vprovv FROM perprov " & _
                 " WHERE (per_codcage = " & nCodage & " OR per_codcage = 0)" & _
                 " AND per_codart = ' ' " & _
                 " AND per_clprar = 0 " & _
                 " AND per_clpran = 0 " & _
                 " AND per_conto = " & lConto & _
                 " AND per_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                 " AND per_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                 " AND codditt = " & CStrSQL(strDitta)
        If oApp.oGvar.bPromozionicomeRel10 Then
          If Not bConspromo Then
            strSQL = strSQL & " AND per_codtpro = 0"
          Else
            If nCodpromoin > 0 Then
              strSQL = strSQL & " AND per_codtpro = " & nCodpromoin
            End If
          End If
        Else
          If nCodpromoin > 0 Then
            strSQL = strSQL & " AND (per_codtpro = " & nCodpromoin & " OR per_codtpro = 0)"
          Else
            strSQL = strSQL & " AND per_codtpro = 0"
          End If
        End If
        strSQL = strSQL & " ORDER BY per_codcage DESC, per_codtpro DESC, per_datagg DESC"

        snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
        If Not snaTmp.Rows.Count = 0 Then
          dProvv = NTSCDec(snaTmp.Rows(0)!per_provv)
          nPromo = NTSCInt(snaTmp.Rows(0)!per_codtpro)
          dVprovv = NTSCDec(snaTmp.Rows(0)!per_vprovv)
          strTipovalout = "F"
        End If
        snaTmp.Clear()
      End If ' tratta pr F
      If strTipoval = "F" Then GoTo finito
      If strTipovalout = "F" Then GoTo finito
      If strTipo = "C" Then GoTo EseguiE

EseguiG:  ' generico agente
      '----------------------------------------------------------------------
      ' tratta provvigioni G
      'If Not (oApp.oGvar.strTrattaPrF = "N") And Not (lConto = 0) Then
      strSQL = " SELECT TOP 1 per_provv, per_codtpro, per_vprovv FROM perprov " & _
               " WHERE per_codcage = " & nCodage & _
               " AND per_codart = ' ' " & _
               " AND per_clprar = 0 " & _
               " AND per_clpran = 0 " & _
               " AND per_conto = 0 " & _
               " AND per_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
               " AND per_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & " " & _
               " AND codditt = " & CStrSQL(strDitta)
      If oApp.oGvar.bPromozionicomeRel10 Then
        If Not bConspromo Then
          strSQL = strSQL & " AND per_codtpro = 0"
        Else
          If nCodpromoin > 0 Then
            strSQL = strSQL & " AND per_codtpro = " & nCodpromoin
          End If
        End If
      Else
        If nCodpromoin > 0 Then
          strSQL = strSQL & " AND (per_codtpro = " & nCodpromoin & " OR per_codtpro = 0)"
        Else
          strSQL = strSQL & " AND per_codtpro = 0"
        End If
      End If
      strSQL = strSQL & " ORDER BY per_codcage DESC, per_codtpro DESC, per_datagg DESC"

      snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
      If Not snaTmp.Rows.Count = 0 Then
        dProvv = NTSCDec(snaTmp.Rows(0)!per_provv)
        nPromo = NTSCInt(snaTmp.Rows(0)!per_codtpro)
        dVprovv = NTSCDec(snaTmp.Rows(0)!per_vprovv)
        strTipovalout = "G"
      End If
      snaTmp.Clear()
      'End If ' tratta pr G
      ' in ogni caso finisce qui ... se non  a sconti/provv.
      GoTo finito
      '----------------------------------------------------------------------
relScPr:
      '----------------------------------------------------------------------
      'Provvigioni in base agli sconti
      'Calcola il prezzo di vendita
      Dim dPrezzoApplicato As Decimal
      Dim dPrezzoListino As Decimal
      Dim nLisscpr As Integer
      Dim snaTmp2 As DataTable
      Dim dScontoApplicato As Decimal
      Dim i As Integer
      Dim bTrov As Boolean

      Dim dQuantabs As Decimal
      Dim strCodScpr As String = ""
      Dim dPripa As Decimal ' % di provviglione da valutare in base all'effettivo sconto applicato (% in esubero sul minimo)
      Dim dScoEsu As Decimal ' parte di sconto oltre il limite minimo previsto dallo scaglione (in esubero)

      strOpzscpr = "ClasseArticolo"
      strOpzscpr = GetSettingBusDitt(strDitta, "Bsveboll", "OPZIONI", ".", "Opzscpr", "ClasseArticolo", "strDipDoc", "ClasseArticolo")

      dPrezzoApplicato = dPrezzo * (100 - dSc1) / 100 * (100 - dSc2) / 100 * (100 - dSc3) / 100 * (100 - dSc4) / 100 * (100 - dSc5) / 100 * (100 - dSc6) / 100 * (100 - dSt1) / 100 * (100 - dSt2) / 100
      strSQL = "SELECT * FROM tabscpr WHERE tb_codscpr = "
      Select Case strOpzscpr
        Case "CodiceAgente"
          strSQL = strSQL & nCodage
          strCodScpr = CStr(nCodage)
        Case "ClasseArticolo"
          strSQL = strSQL & nClprar
          strCodScpr = CStr(nClprar)
        Case "AgenteClasseArticolo", "AgenteClasseArticoloAAAACC"
          strSQL = strSQL & CStr(nCodage) & nClprar.ToString("00")
          strCodScpr = CStr(nCodage) & nClprar.ToString("00")
        Case "AgenteClasseArticoloAAAC", "AgenteClasseArticoloAAAAC"
          strSQL = strSQL & CStr(nCodage) & nClprar.ToString("0")
          strCodScpr = CStr(nCodage) & nClprar.ToString("0")
        Case "AgenteClasseArticoloACCC", "AgenteClasseArticoloAAAACCC"
          strSQL = strSQL & CStr(nCodage) & nClprar.ToString("000")
          strCodScpr = CStr(nCodage) & nClprar.ToString("000")
        Case "AgentePromozioneAAAAPPP"
          strSQL = strSQL & nCodage & nCodpromoin.ToString("000")
          strCodScpr = nCodage.ToString() & nCodpromoin.ToString("000")
        Case "AgenteClassePromozioneAAACCCPPP"
          strSQL = strSQL & CStr(nCodage) & nClprar.ToString("000") & nCodpromoin.ToString("000")
          strCodScpr = CStr(nCodage) & nClprar.ToString("000") & nCodpromoin.ToString("000")
        Case Else
          strError = oApp.Tr(Me, 130553515519042232, "Opzione di registro \Bsveboll\Opzioni\Opzscpr con valore '|" & strOpzscpr & "|' non ammesso. Provvigioni per l'agente |" & nCodage & "| non rilevate.")
          Return True
      End Select
      strSQL = strSQL & " AND codditt = " & CStrSQL(strDitta)
      snaTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp").Tables(0)
      If Not snaTmp.Rows.Count = 0 Then
        nLisscpr = NTSCInt(snaTmp.Rows(0)!tb_lisscpr)
        If oApp.oGvar.bProvvRelazScontiNoListino Then
          'Se abilitata opz di registro globale il prezzo di riferimento non  quello
          'di listino (o ultimo costo) ma quello del momento
          'in tal modo lo sconto aplicato  quello effettivamente indicato sul movimento
          dPrezzoListino = dPrezzo
        Else
          'Cerca il prezzo di listino su cui calcolare lo sconto applicato
          If nLisscpr > 0 Then
            'Cerca nei listini
            dQuantabs = Math.Abs(dQuant)
            strSQL = " SELECT TOP 1 lc_prezzo FROM listini WHERE lc_codart = '" & strCoda & "' " & _
                     " AND lc_conto = 0 AND lc_codvalu = 0 " & _
                     " AND lc_codtpro = 0 AND lc_listino = " & nLisscpr & _
                     " AND lc_datagg <= " & CDataSQL(dtDatdoc.ToShortDateString) & _
                     " AND lc_datscad >= " & CDataSQL(dtDatdoc.ToShortDateString) & " " & _
                     " AND lc_daquant <= " & CDblSQL(dQuantabs.ToString) & _
                     " AND lc_aquant >= " & CDblSQL(dQuantabs.ToString) & " " & _
                     strSQLUM & _
                     " AND codditt = " & CStrSQL(strDitta) & _
                     " ORDER BY lc_datagg DESC"
            snaTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp2").Tables(0)
            If Not snaTmp2.Rows.Count = 0 Then
              dPrezzoListino = NTSCDec(snaTmp2.Rows(0)!lc_prezzo)
            Else
              strError = oApp.Tr(Me, 127792177873906250, "Impossibile trovare il prezzo di listino per calcolare la provvigione di un agente gestito a fasce di sconti.")
              dProvv = 0
              snaTmp2.Clear()
              Return True
            End If
            snaTmp2.Clear()
          Else
            'Cerca l'ultimo prezzo di acquisto
            strSQL = "SELECT apx_ultcos FROM ARTPROX WHERE apx_codart = '" & strCodart & "' "
            strSQL = strSQL & " AND codditt = " & CStrSQL(strDitta)
            snaTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "snaTmp2").Tables(0)
            If Not snaTmp2.Rows.Count = 0 Then
              dPrezzoListino = NTSCDec(snaTmp2.Rows(0)!apx_ultcos)
            Else
              strError = oApp.Tr(Me, 127792178232031250, "Impossibile trovare l'ultimo costo di acquisto per calcolare la provvigione di un agente gestito a fasce di sconti.")
              dProvv = 0
              snaTmp2.Clear()
              Return True
            End If
            snaTmp2.Clear()
          End If
        End If
        'Calcola lo sconto applicato
        If dPrezzoListino <> 0 Then
          dScontoApplicato = ArrDbl(100 - ((100 * dPrezzoApplicato) / dPrezzoListino), 2)
        Else
          dScontoApplicato = 0
        End If
        'Cerca nel record di TABSCPR per trovare la provv.
        bTrov = False
        For i = 1 To 12
          If NTSCDec(snaTmp.Rows(0)("tb_dasco_" & i)) <= dScontoApplicato And NTSCDec(snaTmp.Rows(0)("tb_asco_" & i)) >= dScontoApplicato Then
            dProvv = NTSCDec(snaTmp.Rows(0)("tb_pprov_" & i))
            dPripa = NTSCDec(snaTmp.Rows(0)("tb_pripa_" & i)) ' percentuale da aggiungere in base allo sconto realmente applicato rispetto allo scaglione previsto
            If dPripa <> 0 And (NTSCDec(snaTmp.Rows(0)("tb_asco_" & i)) - NTSCDec(snaTmp.Rows(0)("tb_dasco_" & i))) > 0 Then ' se non esiste uno 'scaglione' di sconto non prende in considerazione la % da ripartire in quanto non ha senso gestirla
              dScoEsu = dScontoApplicato - NTSCDec(snaTmp.Rows(0)("tb_dasco_" & i)) ' parte di sconto applicata in esubero rispetto al minimo delo scaglione
              dProvv = ArrDbl(dProvv + dPripa - ((dScoEsu * dPripa) / (NTSCDec(snaTmp.Rows(0)("tb_asco_" & i)) - NTSCDec(snaTmp.Rows(0)("tb_dasco_" & i)))), 2)
            End If
            bTrov = True
            Exit For
          End If
        Next i
        If Not bTrov Then
          strError = oApp.Tr(Me, 130553515675485195, "La percentuale di sconto netta applicata (|" & Format$(dScontoApplicato, oApp.FormatSconti) & "| %) non rientra in una delle fasce di sconto per il calcolo della provvigione (codice tabella relazione sconti-provvigioni |" & strCodScpr & "|).")
          dProvv = 0
          Return True
        End If
      Else
        If strOpzscpr = "CodiceAgente" Then
          strError = oApp.Tr(Me, 130553515694679144, "Codice tabella relazione sconti-provvigioni |" & strCodScpr & "| non trovato: codice agente.")
        Else
          If strOpzscpr = "ClasseArticolo" Then
            strError = oApp.Tr(Me, 130553515713072948, "Codice tabella relazione sconti-provvigioni |" & strCodScpr & "| non trovato: classe provvigione articolo.")
          Else
            strError = oApp.Tr(Me, 130553515728429097, "Codice tabella relazione sconti-provvigioni |" & strCodScpr & "| non trovato: combinazione codice agente - classe provvigione articolo.")
          End If
        End If
      End If
      snaTmp.Clear()
      GoTo finito2
finito:
      If strTipo = "T" And oApp.oGvar.bScPrconDerogaCliArt Then
        'Se non è stata rilevata una provvigione 'classica' (prioritaria rispetto alla relazione
        'conto/provvigioni quando gbScPrconDerogaCliArt=True) ossia strTipovalOut = " " il programma
        'va a leggere le provvigioni a relazioni sconti/provv. Testando strTipovalOut = " " viene coperto
        'anche il caso in cui l'operatore va a definire una provvigione 'classica' con percentuale/valore
        'provvigione pari a zero
        'If dProvv = 0 And dVprovv = 0 Then
        If strTipovalout = " " Then
          GoTo relScPr
        End If
      End If

finito2:
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Sub CercaPesi(ByVal strDitta As String, ByVal strCodart As String, ByRef dPLordo As Decimal, ByRef dPNetto As Decimal)
    Dim strSQL As String = ""
    Dim dsTmp As DataSet

    Try
      strSQL = "SELECT ar_pesolor, ar_pesonet FROM artico" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ar_codart = " & CStrSQL(strCodart)
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTICO")
      If dsTmp.Tables("ARTICO").Rows.Count > 0 Then
        dPLordo = NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!ar_pesolor)
        dPNetto = NTSCDec(dsTmp.Tables("ARTICO").Rows(0)!ar_pesonet)
      End If
      dsTmp.Tables.Clear()
      dsTmp.Dispose()

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function GetBancaFromConto(ByVal strDitta As String, ByVal lContobanca As Integer, ByRef nCodBanca As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      nCodBanca = 0
      strSQL = "select tb_codbanc from tabbanc where tb_conbanc = " & lContobanca
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        nCodBanca = NTSCInt(dttTmp.Rows(0)!tb_codbanc)
      End If
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetCondban(ByVal strDitta As String, ByVal nCodbanc As Integer, ByVal nCodcauc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      dttOut.Clear()

      strSQL = "select cb_avaind, cb_ggvalu, cb_valfiss from condban " & _
                     "where cb_codbanc = " & nCodbanc.ToString & _
                     " and cb_causale = " & nCodcauc.ToString & _
                     " and codditt = " & CStrSQL(strDitta)
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "Routine per cancellare registrazione in CG"
  Public Overridable Function CancellaRegistrazione(ByVal strDitta As String, ByVal strDatreg As String, _
                                                    ByVal lNumreg As Integer, ByVal strPnInt As String) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDatreg, lNumreg, strPnInt})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------
      '--------------------------------------------------------------------------------------------------------------
      Return CancellaRegistrazione(strDitta, strDatreg, lNumreg, strPnInt, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function CancellaRegistrazione(ByVal strDitta As String, ByVal strDatreg As String, _
                                                    ByVal lNumreg As Integer, ByVal strPnInt As String, ByVal bModDII As Boolean) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim dttReg As New DataTable
    Dim dttScad As New DataTable
    Dim dttTmp As New DataTable
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim strTipmov As String = ""
    Dim lContoEffetti As Integer = 0
    Dim lContoClientiCRB As Integer = 0
    Dim bRiapriPartiteRagg As Boolean = False
    Dim strRiapriPartiteRaggPartita As String = ""
    Dim nCaupref As Integer = 0    'causale standard per la presentazione degli effetti in banca
    Dim strSQLClearDist As String = ""    'query per togliere i riferimenti alla distinta di pres. effetti in banca se si cancella la registraz.
    Dim bCorrRaggr As Boolean = False

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDatreg, lNumreg, strPnInt, bModDII})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------
      '--------------------------------------------------------------------------------------------------------------
      bRiapriPartiteRagg = CBool(GetSettingBusDitt(strDitta, "BSCGINSO", "OPZIONI", ".", "RiapriPartiteRagg", "-1", " ", "-1"))
      strRiapriPartiteRaggPartita = GetSettingBusDitt(strDitta, "BSCGINSO", "OPZIONI", ".", "RiapriPartiteRaggPartita", "X", " ", "X")
      '--------------------------------------------------------------------------------------------------------------
      ValCodiceDb("1", strDitta, "TABPECG", "N", "", dttTmp)
      lContoEffetti = TrovaContoDaContrEConto(strDitta, NTSCInt(dttTmp.Rows(0)!tb_contreff), NTSCInt(dttTmp.Rows(0)!tb_coneff))
      lContoClientiCRB = TrovaContoDaContrEConto(strDitta, NTSCInt(dttTmp.Rows(0)!tb_conclabpd), NTSCInt(dttTmp.Rows(0)!tb_conclrb))
      nCaupref = NTSCInt(dttTmp.Rows(0)!tb_caupref)
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      'ottengo le righe della registrazione da cancellare
      strSQL = "SELECT pn_conto, pn_scadenz, pn_annpar, pn_alfpar, pn_numpar, pn_causale," & _
         " pn_datdoc, pn_alfdoc, pn_numdoc" & _
         " FROM prinot WHERE codditt = " & CStrSQL(strDitta) & _
         " AND pn_datreg = " & CDataSQL(strDatreg) & _
         " AND pn_numreg = " & lNumreg.ToString & _
         " AND pn_integr = " & CStrSQL(strPnInt)
      dttReg = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      '---------------------------------
      'SCADEN
      For i = 0 To dttReg.Rows.Count - 1
        'se sto cancellando una distinta di pres. effetti in banca cancello anche i riferim. alla distinta su scaden
        If NTSCInt(dttReg.Rows(i)!pn_causale) = nCaupref And nCaupref <> 0 Then
          strSQLClearDist = ", sc_fldis = 'N', sc_opdist = null, sc_dtdist = null, sc_numdist = 0, sc_anndist = 0"
        Else
          strSQLClearDist = ""
        End If
        If NTSCStr(dttReg.Rows(i)!pn_scadenz) = "S" Then
          strSQL = "SELECT * FROM scaden " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " AND sc_conto = " & dttReg.Rows(i)!pn_conto.ToString & _
                   " AND sc_annpar = " & dttReg.Rows(i)!pn_annpar.ToString & _
                   " AND sc_alfpar = " & CStrSQL(dttReg.Rows(i)!pn_alfpar.ToString) & _
                   " AND sc_numpar = " & dttReg.Rows(i)!pn_numpar.ToString & _
                   " AND sc_integr = " & CStrSQL(strPnInt)
          dttScad = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          For l = 0 To dttScad.Rows.Count - 1
            ValCodiceDb(dttScad.Rows(l)!sc_causale.ToString, strDitta, "TABCAUC", "N", "", dttTmp)
            strTipmov = dttTmp.Rows(0)!tb_tipmov.ToString
            dttTmp.Clear()
            '-------------------------------------
            'la registraz da cancellare saldava una scadenza: tolgo il saldato da scaden 
            If NTSCStr(dttScad.Rows(l)!sc_flsaldato) <> "" Then
              If NTSCDate(dttScad.Rows(l)!sc_dtsaldato) = NTSCDate(strDatreg) And _
                 NTSCInt(dttScad.Rows(l)!sc_rgsaldato) = lNumreg Then

                strSQL = "UPDATE scaden SET sc_dtsaldato = null, sc_rgsaldato = 0, sc_flsaldato = 'N' " & _
                         strSQLClearDist & _
                         " WHERE codditt = " & CStrSQL(strDitta) & _
                         " AND sc_conto = " & dttReg.Rows(i)!pn_conto.ToString & _
                         " AND sc_annpar = " & dttReg.Rows(i)!pn_annpar.ToString & _
                         " AND sc_alfpar = " & CStrSQL(dttReg.Rows(i)!pn_alfpar.ToString) & _
                         " AND sc_numpar = " & dttReg.Rows(i)!pn_numpar.ToString & _
                         " AND sc_integr = " & CStrSQL(strPnInt) & _
                         " AND sc_numrata = " & NTSCStr(dttScad.Rows(l)!sc_numrata)
                Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

                dttScad.Rows(l)!sc_dtsaldato = DBNull.Value
                dttScad.Rows(l)!sc_rgsaldato = 0
                dttScad.Rows(l)!sc_flsaldato = "N"
              End If
            End If

            '-------------------------------------
            'è la registraz che ha generato la scadenza
            If NTSCDate(dttScad.Rows(l)!sc_datreg) = NTSCDate(strDatreg) And NTSCInt(dttScad.Rows(l)!sc_numreg) = lNumreg Then
              If NTSCStr(dttScad.Rows(l)!sc_flsaldato) = "S" Then
                '----------------------------
                'la scadenza è saldata: assegno la scadenza alla registrazione che l'ha saldata
                strSQL = "UPDATE scaden SET sc_datreg = " & CDataSQL(dttScad.Rows(l)!sc_dtsaldato.ToString) & ", " & _
                         " sc_numreg = " & dttScad.Rows(l)!sc_rgsaldato.ToString & ", " & _
                         " sc_dtsaldato = null, " & _
                         " sc_rgsaldato = 0, " & _
                         " sc_flsaldato = 'N', " & _
                         " sc_importo = " & CDblSQL((NTSCDec(dttScad.Rows(l)!sc_importo) * -1).ToString) & ", " & _
                         " sc_importoda = " & CDblSQL((NTSCDec(dttScad.Rows(l)!sc_importoda) * -1).ToString) & ", " & _
                         " sc_impval = " & CDblSQL((NTSCDec(dttScad.Rows(l)!sc_impval) * -1).ToString) & ", " & _
                         " sc_impvalda  = " & CDblSQL((NTSCDec(dttScad.Rows(l)!sc_impvalda) * -1).ToString) & " " & _
                         strSQLClearDist & _
                         " WHERE codditt = " & CStrSQL(strDitta) & _
                         " AND sc_conto = " & dttReg.Rows(i)!pn_conto.ToString & _
                         " AND sc_annpar = " & dttReg.Rows(i)!pn_annpar.ToString & _
                         " AND sc_alfpar = " & CStrSQL(dttReg.Rows(i)!pn_alfpar.ToString) & _
                         " AND sc_numpar = " & dttReg.Rows(i)!pn_numpar.ToString & _
                         " AND sc_integr = " & CStrSQL(strPnInt) & _
                         " AND sc_numrata = " & NTSCStr(dttScad.Rows(l)!sc_numrata)
                Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
              Else
                '----------------------------
                'la scadenza non è saldata
                If strTipmov = "I" Then
                  '----------------------------
                  'tolgo il flag si insoluto
                  'se c'è riapertura scadenze originarie, questa query non fa nulla perchè il conto effetti è aperto sulla partita raggruppata!!!
                  'non fa nulla neanche se ho fatto il raggruppamento da BNCGSCAD->genera partita raggruppata
                  strSQL = "UPDATE scaden SET sc_insolu = 'N'" & _
                           strSQLClearDist & _
                           " WHERE codditt = " & CStrSQL(strDitta) & _
                           " AND sc_controp = " & dttReg.Rows(i)!pn_conto.ToString & _
                           " AND sc_conto = " & lContoEffetti.ToString & _
                           " AND sc_annpar = " & dttReg.Rows(i)!pn_annpar.ToString & _
                           " AND sc_alfpar = " & CStrSQL(dttReg.Rows(i)!pn_alfpar.ToString) & _
                           " AND sc_numpar = " & dttReg.Rows(i)!pn_numpar.ToString & _
                           " AND sc_integr = " & CStrSQL(strPnInt) & _
                           " AND sc_anneff = " & dttScad.Rows(l)!sc_anneff.ToString & _
                           " AND sc_numeff = " & dttScad.Rows(l)!sc_numeff.ToString
                  Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

                  If bRiapriPartiteRagg And strRiapriPartiteRaggPartita <> "" And NTSCInt(dttScad.Rows(l)!sc_numratarif) <> 0 Then
                    'tolgo la spunta di 'insoluto rata originaria' sul conto cliente della partita raggruppata
                    'risalgo alla registrazione che ha fatto il raggruppamento effetti e tolto la spunta di insoluto dalla scadenza che è stata generata dal raggruppamento
                    strSQL = "SELECT sc_dtsaldato, sc_rgsaldato FROM scaden WHERE codditt = " & CStrSQL(strDitta) & _
                             " AND sc_conto = " & dttScad.Rows(l)!sc_conto.ToString & _
                             " AND sc_annpar= " & dttScad.Rows(l)!sc_annpar.ToString & _
                             " AND sc_alfpar= '" & dttScad.Rows(l)!sc_alfpar.ToString & "' " & _
                             " AND sc_numpar=" & dttScad.Rows(l)!sc_numpar.ToString & _
                             " AND sc_numrata = " & dttScad.Rows(l)!sc_numratarif.ToString & _
                             " AND sc_flsaldato = 'S'"
                    dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
                    If dttTmp.Rows.Count > 0 Then
                      strSQL = "UPDATE scaden SET sc_insolu = 'N' WHERE " & _
                               " codditt = " & CStrSQL(strDitta) & _
                               " AND sc_conto = " & dttScad.Rows(l)!sc_conto.ToString & _
                               " AND sc_datreg = " & CDataSQL(NTSCDate(dttTmp.Rows(0)!sc_dtsaldato)) & _
                               " AND sc_numreg = " & dttTmp.Rows(0)!sc_rgsaldato.ToString & _
                               " AND sc_insolu <> 'N'" & _
                               " AND sc_alfpar = " & CStrSQL(strRiapriPartiteRaggPartita)
                      'qui non toglie il flag insoluto nel caso di raggruppamento fatto da BNCGSCAD->genera effetto raggruppato perchè sul cli la partita non è la 'X'
                      'il record del clie verrà comunque defleggato con una query sotto dove viene testato '" AND sc_numrata = " & dttScad.Rows(e)!sc_numratarif.ToString'
                      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

                      'adesso tolgo il flag di insoluto sul conto effetti collegato alla scadenza raggruppata
                      'dalla reg. del raggruppamento devo risalire alla data/numero di reg. di saldo della partira raggruppata e con quelle
                      'risalire alla scadenza generata sul c/to effetti (stessa partita ma controp = conto cliente e conto = conto effetti)
                      strSQL = "SELECT * FROM scaden WHERE " & _
                               " codditt = " & CStrSQL(strDitta) & _
                               " AND sc_conto = " & dttScad.Rows(l)!sc_conto.ToString & _
                               " AND sc_datreg = " & CDataSQL(NTSCDate(dttTmp.Rows(0)!sc_dtsaldato)) & _
                               " AND sc_numreg = " & dttTmp.Rows(0)!sc_rgsaldato.ToString & _
                               " AND sc_alfpar = " & CStrSQL(strRiapriPartiteRaggPartita)
                      dttTmp.Clear()
                      dttTmp.Dispose()
                      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
                      If dttTmp.Rows.Count = 0 Then
                        'non ho trovato la registrazione di raggruppamento. il raggruppam. potrebbe essere stato fatto da BNCGSCAD->genera effetto raggruppato
                        'dove si chiudono le scadenze cli direttametne al conto effetti
                        'in questo caso cerco non il cli ma il c/to effetti
                        strSQL = strSQL.Replace(dttScad.Rows(l)!sc_conto.ToString, lContoEffetti.ToString)
                        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
                      End If
                      If dttTmp.Rows.Count > 0 Then
                        strSQL = "UPDATE scaden SET sc_insolu = 'N' WHERE " & _
                                " codditt = " & CStrSQL(strDitta) & _
                                " AND sc_conto = " & lContoEffetti.ToString & _
                                " AND sc_controp = " & dttScad.Rows(l)!sc_conto.ToString & _
                                " AND sc_annpar= " & dttTmp.Rows(0)!sc_annpar.ToString & _
                                " AND sc_alfpar= '" & dttTmp.Rows(0)!sc_alfpar.ToString & "' " & _
                                " AND sc_numpar=" & dttTmp.Rows(0)!sc_numpar.ToString & _
                                " AND sc_numrata = " & dttTmp.Rows(0)!sc_numrata.ToString & _
                                " AND sc_flsaldato = 'S'"
                        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
                      End If
                    End If
                    dttTmp.Clear()
                    dttTmp.Dispose()
                  End If

                  strSQL = "UPDATE scaden SET sc_insolu = 'N'" & _
                           strSQLClearDist & _
                           " WHERE codditt = " & CStrSQL(strDitta) & _
                           " AND sc_conto = " & dttReg.Rows(i)!pn_conto.ToString & _
                           " AND sc_annpar = " & dttReg.Rows(i)!pn_annpar.ToString & _
                           " AND sc_alfpar = " & CStrSQL(dttReg.Rows(i)!pn_alfpar.ToString) & _
                           " AND sc_numpar = " & dttReg.Rows(i)!pn_numpar.ToString & _
                           " AND sc_integr = " & CStrSQL(strPnInt) & _
                           " AND sc_numrata = " & dttScad.Rows(l)!sc_numratarif.ToString
                  Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
                End If
                '----------------------------
                'cancello il record di scaden
                strSQL = "DELETE scaden  " & _
                         " WHERE codditt = " & CStrSQL(strDitta) & _
                         " AND sc_conto = " & dttReg.Rows(i)!pn_conto.ToString & _
                         " AND sc_annpar = " & dttReg.Rows(i)!pn_annpar.ToString & _
                         " AND sc_alfpar = " & CStrSQL(dttReg.Rows(i)!pn_alfpar.ToString) & _
                         " AND sc_numpar = " & dttReg.Rows(i)!pn_numpar.ToString & _
                         " AND sc_integr = " & CStrSQL(strPnInt) & _
                         " AND sc_numrata = " & NTSCStr(dttScad.Rows(l)!sc_numrata)
                Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

                If lContoClientiCRB = NTSCInt(dttScad.Rows(l)!sc_conto) And NTSCStr(dttScad.Rows(l)!sc_darave) = "A" Then
                  'caso di emissione effetti senza chiusura del cliente: se creo la registrazione di emissione effetti fatta così
                  'effetti attivi a clienti c/rb, faccio una update sul cliente indicato nella contropartita della scadenza del clienti c/rb
                  'per segnalare sul cliente che sulla scadenza è stato emesso l'effetto
                  'quando cancello la registrazione - COME IN QUESTO CASO - tolgo il flag di autorizzato
                  strSQL = "UPDATE scaden" & _
                           " SET sc_fldis = 'N'" & _
                           strSQLClearDist & _
                           " WHERE codditt = " & CStrSQL(strDitta) & _
                           " AND sc_conto = " & dttScad.Rows(l)!sc_controp.ToString & _
                           " AND sc_annpar = " & dttScad.Rows(l)!sc_annpar.ToString & _
                           " AND sc_alfpar = '" & dttScad.Rows(l)!sc_alfpar.ToString & "'" & _
                           " AND sc_numpar = " & dttScad.Rows(l)!sc_numpar.ToString & _
                           " AND sc_numrata = " & dttScad.Rows(l)!sc_numrata.ToString & _
                           " AND sc_integr = '" & dttScad.Rows(l)!sc_integr.ToString & "'"
                  Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
                End If
              End If    'If NTSCStr(dttScad.Rows(l)!sc_flsaldato) = "S" Then
            End If    'If NTSCDate(dttScad.Rows(l)!sc_datreg) = NTSCDate(strDatreg) And NTSCInt(dttScad.Rows(l)!sc_numreg) = lNumreg Then
          Next    ' For l = 0 To dttScad.Rows.Count - 1
          dttScad.Clear()
          '----------------------------------------------------------------------------------------------------------
          If (bRiapriPartiteRagg = True) And _
             (strRiapriPartiteRaggPartita <> "") And _
             (NTSCStr(dttReg.Rows(i)!pn_datdoc).Trim <> "") And _
             (NTSCStr(dttReg.Rows(i)!pn_alfdoc).ToUpper = strRiapriPartiteRaggPartita.ToUpper) And _
             (NTSCInt(dttReg.Rows(i)!pn_numdoc) <> 0) Then
            strSQL = "SELECT * FROM scaden" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND sc_conto = " & lContoEffetti & _
                    " AND sc_datdoc = " & CDataSQL(dttReg.Rows(i)!pn_datdoc.ToString) & _
                    " AND sc_alfdoc = " & CStrSQL(dttReg.Rows(i)!pn_alfdoc) & _
                    " AND sc_numdoc = " & NTSCInt(dttReg.Rows(i)!pn_numdoc) & _
                    " AND sc_integr = " & CStrSQL(strPnInt) & _
                    " AND sc_insolu = 'S'"
            dttScad = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            For l = 0 To (dttScad.Rows.Count - 1)
              strSQL = "UPDATE scaden" & _
                " SET sc_insolu = 'N'" & _
                strSQLClearDist & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND sc_controp = " & dttReg.Rows(i)!pn_conto.ToString & _
                " AND sc_conto = " & lContoEffetti.ToString & _
                " AND sc_datdoc = " & CDataSQL(dttReg.Rows(i)!pn_datdoc.ToString) & _
                " AND sc_alfdoc = " & CStrSQL(dttReg.Rows(i)!pn_alfdoc) & _
                " AND sc_numdoc = " & NTSCInt(dttReg.Rows(i)!pn_numdoc) & _
                " AND sc_integr = " & CStrSQL(strPnInt) & _
                " AND sc_anneff = " & dttScad.Rows(l)!sc_anneff.ToString & _
                " AND sc_numeff = " & dttScad.Rows(l)!sc_numeff.ToString
              Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
            Next
            dttScad.Clear()
            dttScad.Dispose()
          End If
          '----------------------------------------------------------------------------------------------------------
        End If    'If NTSCStr(dttReg.Rows(i)!pn_scadenz) = "S" Then
      Next

      '---------------------------------
      'CANCELLO MOVIVA e invalido liquiva
      strSQL = "DELETE FROM moviva WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND mi_datreg = " & CDataSQL(strDatreg) & _
                 " AND mi_numreg = " & lNumreg & _
                 " AND mi_integr = " & CStrSQL(strPnInt)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      If strPnInt = "N" Then
        InvalidaLiqIVA(strDitta, strDatreg, dbConn)
      End If

      '---------------------------------
      'CANCELLO PRINCOMP
      strSQL = "DELETE FROM princomp WHERE codditt = " & CStrSQL(strDitta) & _
           " AND pnc_datreg = " & CDataSQL(strDatreg) & _
           " AND pnc_numreg = " & lNumreg
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '---------------------------------
      'CANCELLO PNNOTE
      strSQL = "DELETE FROM pnnote WHERE codditt = " & CStrSQL(strDitta) & _
           " AND py_datreg = " & CDataSQL(strDatreg) & _
           " AND py_numreg = " & lNumreg
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '---------------------------------
      'CANCELLO PRIANA
      strSQL = "DELETE FROM priana WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND pa_datreg = " & CDataSQL(strDatreg) & _
                 " AND pa_numreg = " & lNumreg & _
                 " AND pa_integr = " & CStrSQL(strPnInt)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '---------------------------------
      'CANCELLO PRIA2COMP e PRIANA2
      CancellaRegistrazione_CA2(dbConn, strDitta, strDatreg, lNumreg)

      '------------------------------------
      'CANCELLO I RIFERIMENTI SU TESTMAG
      'PERFETTAMENTE FUNZIONANTE MA LENTA!!!! ANDAVA CREATO UN INDICE SPECIFICO DIVERSAMENTE POTEVA IMPIEGARE ANCHE 30 SECONDI 
      'strSQL = " UPDATE testmag SET " & _
      '         " tm_flcont = CASE WHEN tm_datregef = " & CDataSQL(strDatreg) & " AND tm_numregef = " & lNumreg.ToString & " THEN 'N' ELSE tm_flcont END, " & _
      '         " tm_datregef = CASE WHEN tm_datregef = " & CDataSQL(strDatreg) & " AND tm_numregef = " & lNumreg.ToString & " THEN null ELSE tm_datregef END, " & _
      '         " tm_numregef = CASE WHEN tm_datregef = " & CDataSQL(strDatreg) & " AND tm_numregef = " & lNumreg.ToString & " THEN 0 ELSE tm_numregef END, " & _
      '         " tm_datregin = CASE WHEN tm_datregin = " & CDataSQL(strDatreg) & " AND tm_numrgin = " & lNumreg.ToString & " THEN null ELSE tm_datregin END, " & _
      '         " tm_numrgin = CASE WHEN tm_datregin = " & CDataSQL(strDatreg) & " AND tm_numrgin = " & lNumreg.ToString & " THEN 0 ELSE tm_numrgin END, " & _
      '         " tm_datregom = CASE WHEN tm_datregom = " & CDataSQL(strDatreg) & " AND tm_numregom = " & lNumreg.ToString & " THEN null ELSE tm_datregom END, " & _
      '         " tm_numregom = CASE WHEN tm_datregom = " & CDataSQL(strDatreg) & " AND tm_numregom = " & lNumreg.ToString & " THEN 0 ELSE tm_numregom END " & _
      '         " WHERE testmag.codditt = " & CStrSQL(strDitta) & _
      '         " AND ((tm_datregef = " & CDataSQL(strDatreg) & " AND tm_numregef = " & lNumreg.ToString & ") OR " & _
      '         "      (tm_datregin = " & CDataSQL(strDatreg) & " AND tm_numrgin = " & lNumreg.ToString & ") OR " & _
      '         "      (tm_datregom = " & CDataSQL(strDatreg) & " AND tm_numregom = " & lNumreg.ToString & "))"
      'Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 pn_datreg FROM prinot" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND pn_datreg = " & CDataSQL(strDatreg) & _
        " AND pn_numreg = " & lNumreg.ToString & _
        " AND pn_integr = " & CStrSQL(strPnInt) & _
        " AND pn_tmtipork = 'C'" & _
        " AND pn_tmnumdoc = 0"
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn).Rows.Count > 0 Then bCorrRaggr = True
      For i = 1 To 3
        strSQL = "UPDATE testmag SET"
        If i = 1 Then
          strSQL += " tm_flcont = 'N'," & _
            " tm_datregef = NULL," & _
            " tm_numregef = 0"
        ElseIf i = 2 Then
          strSQL += " tm_datregin = NULL," & _
            " tm_numrgin = 0"
        ElseIf i = 3 Then
          strSQL += " tm_datregom = NULL," & _
            " tm_numregom = 0"
        End If
        strSQL += " FROM prinot"
        If bCorrRaggr = True Then
          strSQL += " INNER JOIN testmag ON prinot.codditt = testmag.codditt" & _
                                      " AND prinot.pn_datreg = testmag.tm_datregef" & _
                                      " AND prinot.pn_numreg = testmag.tm_numregef"
        Else
          strSQL += " INNER JOIN testmag ON prinot.codditt = testmag.codditt" & _
                                      " AND prinot.pn_tmtipork = testmag.tm_tipork" & _
                                      " AND prinot.pn_tmanno = testmag.tm_anno" & _
                                      " AND prinot.pn_tmserie = testmag.tm_serie" & _
                                      " AND prinot.pn_tmnumdoc = testmag.tm_numdoc"
        End If
        strSQL += " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
          " AND pn_datreg = " & CDataSQL(strDatreg) & _
          " AND pn_numreg = " & lNumreg.ToString & _
          IIf(bCorrRaggr = False, " AND pn_tmnumdoc <> 0", "").ToString & _
          " AND pn_tmtipreg = "
        If i = 1 Then
          strSQL += "'F'"
        ElseIf i = 2 Then
          strSQL += "'I'"
        ElseIf i = 3 Then
          strSQL += "'O'"
        End If
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next i
      '--------------------------------------------------------------------------------------------------------------
      For i = 1 To 2
        strSQL = "UPDATE testpar SET"
        If i = 1 Then
          strSQL += " tm_flcont = 'N'," & _
            " tm_datregef = NULL," & _
            " tm_numregef = 0"
        Else
          strSQL += " tm_datregin = NULL," & _
            " tm_numrgin = 0"
        End If
        strSQL += " FROM prinot"
        If bCorrRaggr = True Then
          strSQL += " INNER JOIN testpar ON prinot.codditt = testpar.codditt" & _
                                      " AND prinot.pn_datreg = testpar.tm_datregef" & _
                                      " AND prinot.pn_numreg = testpar.tm_numregef"
        Else
          strSQL += " INNER JOIN testpar ON prinot.codditt = testpar.codditt" & _
                                      " AND prinot.pn_tmtipork = testpar.tm_tipork" & _
                                      " AND prinot.pn_tmanno = testpar.tm_anno" & _
                                      " AND prinot.pn_tmserie = testpar.tm_serie" & _
                                      " AND prinot.pn_tmnumdoc = testpar.tm_numdoc"
        End If
        strSQL += " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
          " AND pn_datreg = " & CDataSQL(strDatreg) & _
          " AND pn_numreg = " & lNumreg.ToString & _
          IIf(bCorrRaggr = False, " AND pn_tmnumdoc <> 0", "").ToString & _
          "AND pn_tmtipreg = "
        If i = 1 Then
          strSQL += "'F'"
        Else
          strSQL += "'I'"
        End If
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next i
      '--------------------------------------------------------------------------------------------------------------
      'Dichiarazioni di intento
      If bModDII Then
        'Passo i vecchi valori della dichiarazione...
        'In cancellazione, controlllo e passo i vecchi valori della dichiarazione di intento
        Dim dttOldDich As New DataTable
        strSQL = " SELECT pn_dianno, pn_dinumero, an_tipo" & _
                 " FROM prinot INNER JOIN anagra ON prinot.codditt = anagra.codditt AND prinot.pn_conto = anagra.an_conto" & _
                 " WHERE prinot.codditt = " & CStrSQL(strDitta) & _
                 " AND pn_datreg = " & CDataSQL(strDatreg) & _
                 " AND pn_numreg = " & lNumreg & _
                 " AND pn_integr = " & CStrSQL(strPnInt)
        dttOldDich = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttOldDich.Rows.Count > 0 Then 'potrebbe essere stata cancellata...
          If NTSCInt(dttOldDich.Rows(0)!pn_dianno) <> 0 And NTSCInt(dttOldDich.Rows(0)!pn_dinumero) <> 0 Then
            GestisciFlagChiusura("C", strDitta, dttOldDich.Rows(0)!an_tipo.ToString, NTSCInt(dttOldDich.Rows(0)!pn_dianno), NTSCInt(dttOldDich.Rows(0)!pn_dinumero), dbConn)
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------

      '---------------------------------
      'CANCELLO PRINOT
      strSQL = "DELETE FROM prinot WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND pn_datreg = " & CDataSQL(strDatreg) & _
                 " AND pn_numreg = " & lNumreg & _
                 " AND pn_integr = " & CStrSQL(strPnInt)
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      If strPnInt = "N" Then InvalidaLiqIVA(strDitta, strDatreg, dbConn)

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      Return True
    Catch ex As Exception
      dttReg.Clear()
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CancellaRegistrazione_CA2(ByRef dbConn As DbConnection, ByVal strDitta As String, _
                                                            ByVal strDatreg As String, ByVal lNumreg As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp2 As New DataTable
    Dim dtrT() As DataRow = Nothing
    Dim strProgr As String = "" 'elenco delle righe di priana2 da cancellare
    Dim lTmp As Decimal = 0
    Try

      strSQL = "SELECT DISTINCT pa2_progr, pa2_datreg, pa2_numreg, pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc " & _
               " FROM priana2 " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " And pa2_datreg = " & CDataSQL(strDatreg) & _
               " And pa2_numreg = " & lNumreg.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then Return True

      For Each dtrT1 As DataRow In dttTmp.Rows
        If lTmp <> NTSCDec(dtrT1!pa2_progr) Then
          'il progr delle righe di cadc è doppio: mi basta prendere quello della riga 1
          strProgr += CDblSQL(NTSCDec(dtrT1!pa2_progr)) & ", "
          lTmp = NTSCDec(dtrT1!pa2_progr)
        End If
      Next

      dtrT = dttTmp.Select("pa2_mmtipork = 'T'")
      If dtrT.Length > 0 Then
        'se la registrazione è collegata ad un carico da produzione, 
        'devo rimuovere prima le righe del carico da produzione (scarichi collegati e qta prodotta):
        'su queste righe non c'è il collegamento a pa2_datreg e pa2_numreg, mi baso su pa2_mmtipork, pa2_mmanno, pa2_mmserie, pa2_mmnumdoc
        Dim oDttgr As New CLEGROUPBY
        Dim dttGr As New DataTable
        dttGr.Clear()
        If Not oDttgr.NTSGroupBy(dttTmp, dttGr, "pa2_mmanno, pa2_mmserie, pa2_mmnumdoc", _
                                                "pa2_mmtipork = 'T'", _
                                                "pa2_mmanno, pa2_mmserie, pa2_mmnumdoc") Then
          Return False
        End If
        For Each dtrT1 As DataRow In dttGr.Rows
          strSQL = "SELECT DISTINCT pa2_progr " & _
                   " FROM priana2 " & _
                   " WHERE codditt = " & CStrSQL(strDitta) & _
                   " And pa2_mmtiporkt = 'T'" & _
                   " And pa2_mmanno = " & dtrT1!pa2_mmanno.ToString & _
                   " And pa2_mmserie = " & CStrSQL(dtrT1!pa2_mmserie) & _
                   " And pa2_mmnumdoc = " & dtrT1!pa2_mmnumdoc.ToString
          dttTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          For Each dtrT2 As DataRow In dttTmp2.Rows
            strProgr += CDblSQL(NTSCDec(dtrT2!pa2_progr)) & ", "
          Next
          dttTmp2.Clear()
        Next
      End If

      If strProgr.Trim = "" Then Return True
      strProgr = strProgr.Substring(0, strProgr.Length - 2)

      '------------------------------------
      'CANCELLO LE EVENTUALI RIGHE DI PRIA2COMP
      strSQL = "DELETE pria2comp WHERE codditt = " & CStrSQL(strDitta) & _
               " AND pac_progr IN (" & strProgr & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '------------------------------------
      'CANCELLO LE EVENTUALI RIGHE DI PRIANA2
      strSQL = "DELETE priana2 WHERE codditt = " & CStrSQL(strDitta) & " and pa2_progr IN (" & strProgr & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreCancellaRegInLG(ByVal strDitta As String, _
                                           ByVal strDatreg As String, ByVal lNumreg As Integer, _
                                           ByVal strPnInt As String) As Boolean
    'restituisce true se alcune registrazioni sono state stampate in definitivo sul libro giornale
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 pn_datreg FROM prinot " & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND pn_datreg = " & CDataSQL(strDatreg) & _
                " AND pn_numreg = " & lNumreg.ToString & _
                " AND pn_integr  = " & CStrSQL(strPnInt) & _
                " AND pn_fllg = 'S'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreCancellaRegInRI(ByVal strDitta As String, _
                                           ByVal strDatreg As String, ByVal lNumreg As Integer, _
                                           ByVal strPnInt As String) As Boolean
    'restituisce true se alcune registrazioni sono state stampate in definitivo sui registri IVA
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 mi_datreg FROM moviva" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND mi_datreg = " & CDataSQL(strDatreg) & _
        " AND mi_numreg = " & lNumreg.ToString & _
        " AND mi_integr = " & CStrSQL(strPnInt) & _
        " AND mi_prodef = 'D'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If

      Return False

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function TestPreCancellaRegInRitenut(ByVal strDitta As String, _
                                           ByVal strDatreg As String, ByVal lNumreg As Integer) As Boolean
    'restituisce true se la registrazione è presente nella tabella
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 rt_datreg FROM ritenut" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND rt_datreg = " & CDataSQL(strDatreg) & _
              " AND rt_numreg = " & lNumreg.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If
      Return False
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreCancellaRegInPagcomp(ByVal strDitta As String, _
                                           ByVal strDatreg As String, ByVal lNumreg As Integer) As Boolean
    'restituisce true se la registrazione è presente nella tabella
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 rtp_datreg FROM pagcomp" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND rtp_datreg = " & CDataSQL(strDatreg) & _
              " AND rtp_numreg = " & lNumreg.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If
      Return False
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreCancellaRegInMovcesp(ByVal strDitta As String, _
                                         ByVal strDatreg As String, ByVal lNumreg As Integer) As Boolean
    'restituisce true se la registrazione è presente nella tabella
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 me_datregpn FROM movcesp" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND me_datregpn = " & CDataSQL(strDatreg) & _
              " AND me_numregpn = " & lNumreg.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If
      Return False
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreCancellaRegInPrfatt(ByVal strDitta As String, _
                                         ByVal strDatreg As String, ByVal lNumreg As Integer) As Boolean
    'restituisce true se la registrazione è presente nella tabella
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 rx_datreg FROM prfatt" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND rx_datreg = " & CDataSQL(strDatreg) & _
              " AND rx_numreg = " & lNumreg.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If
      Return False
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreCancellaRegInPrincpag(ByVal strDitta As String, _
                                         ByVal strDatreg As String, ByVal lNumreg As Integer) As Boolean
    'restituisce true se la registrazione è presente nella tabella
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 rxp_datreg FROM princpag" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND rxp_datreg = " & CDataSQL(strDatreg) & _
              " AND rxp_numreg = " & lNumreg.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If
      Return False
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreCancellaRegInMovusat(ByVal strDitta As String, _
                                         ByVal strDatreg As String, ByVal lNumreg As Integer) As Boolean
    'restituisce true se la registrazione è presente nella tabella
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 mu_datregr FROM movusat" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND mu_datregr = " & CDataSQL(strDatreg) & _
              " AND mu_numregr = " & lNumreg.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If
      Return False
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreCancellaRegInTasks(ByVal strDitta As String, _
                                         ByVal strDatreg As String, ByVal lNumreg As Integer) As Boolean
    'restituisce true se la registrazione è presente nella tabella
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 pa_datreg FROM priana" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND pa_datreg = " & CDataSQL(strDatreg) & _
              " AND pa_numreg = " & lNumreg.ToString & _
              " AND pa_pmtaskid <> 0 "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If

      'ora in CA DC 
      strSQL = "SELECT top 1 pa2_datreg FROM priana2" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND pa2_datreg = " & CDataSQL(strDatreg) & _
              " AND pa2_numreg = " & lNumreg.ToString & _
              " AND pa2_idwbs <> 0 "
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If

      Return False
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function TestPreCancellaRegInPriana2(ByVal strDitta As String, ByVal strDatreg As String, ByVal lNumreg As Integer) As Boolean
    'restituisce true se la registrazione è presente nella tabella 
    'e la data di registrazione non è superiore alla data ultimo aggiornamento CA
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 pa2_datreg " & _
              " FROM priana2 INNER JOIN tabanaz ON priana2.codditt = tabanaz.codditt" & _
              " WHERE priana2.codditt = " & CStrSQL(strDitta) & _
              " AND pa2_datreg = " & CDataSQL(strDatreg) & _
              " AND pa2_numreg = " & lNumreg.ToString & _
              " AND pa2_datreg <= tb_dtulaca " & _
              " AND pa2_datreg <> 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If
      Return False
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreCancellaRegInAbpd(ByVal strDitta As String, ByVal strDatreg As String, ByVal lNumreg As Integer) As Boolean
    'restituisce true se la registrazione ha creato una scadenza tuttora non saldata ma collegata ad un ABPD
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      strSQL = "SELECT top 1 sc_datreg FROM scaden" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND sc_datreg = " & CDataSQL(strDatreg) & _
              " AND sc_numreg = " & lNumreg.ToString & _
              " AND sc_flsaldato = 'N'" & _
              " AND sc_codincdiff <> 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        dttTmp.Clear()
        Return True
      End If
      Return False
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TestPreCancellaRegInScaden(ByVal strDitta As String, ByVal strDatreg As String, ByVal lNumreg As Integer) As Boolean
    Dim dttTmp As New DataTable
    Try
      'obsoleta
      Return TestPreCancellaRegInScaden(strDitta, strDatreg, lNumreg, dttTmp)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function TestPreCancellaRegInScaden(ByVal strDitta As String, ByVal strDatreg As String, ByVal lNumreg As Integer, ByRef dttOut As DataTable) As Boolean
    'non faccio cancellare se la registrazione CHE HA GENERATO LE SCADENZE se contiene delle scadenze saldate in data successiva (ovvero non salda degli acconti)
    'perchè se ho fatto la fattura e mi hanno pagato, prima devo cancellare il pagamento, altriemtni avrei dei sondi che non saprei a cosa collegare
    Dim strSQL As String = ""
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strDatreg, lNumreg, dttOut})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(3), DataTable)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      'gestisco anche il caso di emissione effetti senza chiusura cliente
      'o modulo BNIDGEID
      strSQL = "SELECT codditt, sc_conto, sc_annpar, sc_alfpar, sc_numpar, sc_numrata, " & _
              " sc_datreg, sc_numreg, sc_flsaldato, sc_dtsaldato, sc_rgsaldato, sc_tippaga, sc_codincdiff, sc_fldis " & _
              " FROM scaden" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND sc_datreg = " & CDataSQL(strDatreg) & _
              " AND sc_numreg = " & lNumreg.ToString & _
              " AND (" & _
              "     (sc_flsaldato = 'S' AND sc_dtsaldato >= sc_datreg) OR " & _
              "     (sc_flsaldato = 'N' AND sc_fldis = 'S' AND sc_numdist = 0 AND sc_darave = 'D' AND sc_tippaga >= 1 AND sc_tippaga <= 2) OR " & _
              "      sc_codincdiff <> 0" & _
              "      )"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttOut.Rows.Count > 0 Then Return True

      Return False
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "GENERAZIONE FILE MAV/ABI/RID/BONIF per bncgauto, bncgdist, ..."
  Public Overridable Function GenMavGetMovmag(ByVal strDitta As String, ByVal nAnnodoc As Integer, _
                                            ByVal strSeriedoc As String, ByVal lNumdoc As Integer, _
                                            ByRef dttOut As DataTable) As Boolean
    'generazione MAV: prende i primi 20 articoli di movmag per inserirli come descrizione nel file
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 20 mm_descr, mm_ump, mm_quant, mm_valore FROM movmag" & _
              " WHERE mm_tipork = 'A'" & _
              " AND mm_anno = " & nAnnodoc.ToString & _
              " AND mm_serie = " & CStrSQL(strSeriedoc) & _
              " AND mm_numdoc = " & lNumdoc.ToString & _
              " ORDER BY mm_tipork, mm_anno, mm_serie, mm_numdoc, mm_riga"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function GenAbiBIC(ByVal lAbi As Integer, ByVal lCab As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try

      strSQL = "SELECT AbcSwift FROM abicab WHERE abcabi = " & lAbi.ToString & " AND abccab = " & lCab.ToString
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      If dttTmp.Rows.Count > 0 Then
        Return NTSCStr(dttTmp.Rows(0)!AbcSwift)
      Else
        strSQL = "SELECT AbiCodalf FROM ABI WHERE AbiAbi = " & lAbi.ToString
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
        If dttTmp.Rows.Count > 0 Then Return NTSCStr(dttTmp.Rows(0)!AbiCodalf)
      End If

      Return ""

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return ""
    End Try
  End Function

  Public Overridable Function GenAnagraCod770BonifEst(ByVal strDitta As String, ByVal lConto As Integer) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try

      strSQL = "SELECT tb_cod770" & _
              " FROM anagra INNER JOIN tabstat ON anagra.an_stato = tabstat.tb_codstat" & _
              " WHERE anagra.codditt = " & CStrSQL(strDitta) & _
              " AND an_conto = " & lConto.ToString & _
              " AND tb_cod770 <> 0"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!tb_cod770)

      Return 0

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return 0
    End Try
  End Function

  Public Overridable Function GetDataScadenDistinta(ByVal strDitta As String, ByVal strTipPaga As String, _
                                      ByVal nAnndist As Integer, ByVal nNumdist As Integer, ByVal strPnint As String, _
                                      ByVal bRaggruppa As Boolean, ByRef dttOut As DataTable) As Boolean
    'bonifici italia: ritorna le scadenze da inserire nel file raggruppate per fornitore, scadenza, banca
    Dim strSQL As String = ""

    Try
      If bRaggruppa Then
        strSQL = "SELECT count(*) as nrec, sc_conto, sc_datsca, sc_abi, sc_cab, sc_banc1, sc_banc2, sc_numcc, sc_cin, sc_prefiban, sc_swift, sc_coddest, 'D' as sc_darave, " & _
                " min(sc_fldis) as sc_fldis, min(sc_numdoc) as sc_numdoc, min(sc_alfdoc) as sc_alfdoc, min(sc_causale) as sc_causale, min(sc_datdoc) as sc_datdoc, " & _
                " sum(sc_importoda) as sc_importoda, sum(sc_impvalda) as sc_impvalda, 0 as sc_controp, min(sc_codbanc) as sc_codbanc, min(sc_tippaga) as sc_tippaga, " & _
                " 0 as sc_numeff, min(sc_numpar) as sc_numpar, min(sc_alfpar) as sc_alfpar, min(sc_annpar) as sc_annpar, 0 as sc_numrata, '' as sc_descr," & _
                " MIN(sc_iban) AS sc_iban" & _
                " FROM SCADEN " & _
                " WHERE scaden.codditt = " & CStrSQL(strDitta) & _
                " AND sc_fldis = 'S'" & _
                " AND sc_integr = " & CStrSQL(strPnint) & _
                " And sc_tippaga = " & strTipPaga & _
                " AND sc_anndist = " & nAnndist.ToString & _
                " AND sc_numdist = " & nNumdist.ToString & _
                " GROUP BY sc_conto, sc_datsca, sc_abi, sc_cab, sc_banc1, sc_banc2, sc_numcc, sc_coddest, sc_cin, sc_prefiban, sc_swift " & _
                " ORDER BY sc_conto, sc_datsca "
      Else
        strSQL = "SELECT 1 as nrec, scaden.* " & _
                " FROM SCADEN " & _
                " WHERE scaden.codditt = " & CStrSQL(strDitta) & _
                " AND sc_fldis = 'S'" & _
                " AND sc_integr = " & CStrSQL(strPnint) & _
                " And sc_tippaga = " & strTipPaga & _
                " AND sc_anndist = " & nAnndist.ToString & _
                " AND sc_numdist = " & nNumdist.ToString & _
                " ORDER BY sc_conto, sc_datsca "
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function


  Public Overridable Function GetAbiBonifSepaRiferim(ByVal strDitta As String, ByVal bFiltraDatSca As Boolean, _
                                                     ByVal nAnndist As Integer, ByVal nNumdist As Integer, _
                                                     ByVal strPnint As String, ByRef dtrT As DataRow, _
                                                     ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT sc_numdoc, sc_alfdoc, sc_numpar, sc_alfpar, sc_datdoc " & _
                " FROM scaden " & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND sc_fldis = 'S'" & _
                " AND sc_integr = " & CStrSQL(strPnint) & _
                " And sc_tippaga = " & CStrSQL(dtrT!sc_tippaga) & _
                " AND sc_anndist = " & nAnndist.ToString & _
                " AND sc_numdist = " & nNumdist.ToString & _
                " AND sc_conto = " & NTSCStr(dtrT!sc_conto) & _
                " AND sc_abi = " & NTSCStr(dtrT!sc_abi) & _
                " AND sc_cab = " & NTSCStr(dtrT!sc_cab) & _
                " AND sc_banc1 = " & CStrSQL(dtrT!sc_banc1) & _
                " AND sc_banc2 = " & CStrSQL(dtrT!sc_banc2) & _
                " AND sc_numcc = " & CStrSQL(dtrT!sc_numcc) & _
                " AND sc_coddest = " & NTSCStr(dtrT!sc_coddest) & _
                " AND sc_cin = " & CStrSQL(dtrT!sc_cin) & _
                " AND sc_prefiban = " & CStrSQL(dtrT!sc_prefiban) & _
                " AND sc_swift = " & CStrSQL(dtrT!sc_swift)
      If bFiltraDatSca Then strSQL += " AND sc_datsca = " & CDataSQL(NTSCDate(dtrT!sc_datsca))
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#End Region

  Public Overridable Function SalvaListino(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, ByVal strUnmis As String, _
                                         ByVal strTipoListino As String, ByVal nListino As Integer, ByVal lConto As Integer, _
                                         ByVal strDatIni As String, ByVal nCodlavo As Integer, ByVal nCodvalu As Integer, _
                                         ByVal nCodtpro As Integer, ByVal dPerqta As Decimal, ByVal dPrezzo As Decimal, _
                                         Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    Try
      'obsoleta
      Return SalvaListino(strDitta, strCodart, nFase, strUnmis, strTipoListino, nListino, lConto, _
                          strDatIni, nCodlavo, nCodvalu, nCodtpro, dPerqta, dPrezzo, 0, dbConn)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SalvaListino(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, ByVal strUnmis As String, _
                                         ByVal strTipoListino As String, ByVal nListino As Integer, ByVal lConto As Integer, _
                                         ByVal strDatIni As String, ByVal nCodlavo As Integer, ByVal nCodvalu As Integer, _
                                         ByVal nCodtpro As Integer, ByVal dPerqta As Decimal, ByVal dPrezzo As Decimal, _
                                         ByVal lCoddest As Integer, Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    '------------------------------------------
    'dato un listino, provvede ad aggiornare la tabella 'listini' aggiustando 
    'se necessario i record precedenti e successivi a livello di date inizio/fine validità
    'chiamato, ad esempio, da BSMGSTRL

    'ATTENZIONE: VUOLE L'ARTICOLO EFFETTIVO: SE GESTITO A VARIANTI FARE PRIMA QUERY PER TROVARE ARTICOLO GIUSTO 
    '(vedi routine in belbmenu.SalvaListino)
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp2 As New DataTable
    Dim strDatscad As String = IntSetDate("31/12/2099")
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nFase, strUnmis, strTipoListino, nListino, lConto, _
                          strDatIni, nCodlavo, nCodvalu, nCodtpro, dPerqta, dPrezzo, dPrezzo, dbConn})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      If strTipoListino = "" Then strTipoListino = " "

      '-----------------------------
      'verifico se c'è già un listino simile
      strSQL = "SELECT TOP 1 * FROM busvw_listini" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart = " & CStrSQL(strCodart) & _
                " AND lc_fase = " & nFase.ToString & _
                " AND lc_codlavo = " & nCodlavo.ToString & _
                " AND lc_conto = " & lConto.ToString & _
                " AND lc_coddest = " & lCoddest.ToString & _
                " AND lc_tipo = " & CStrSQL(strTipoListino) & _
                " AND lc_codvalu = " & nCodvalu.ToString & _
                " AND lc_codtpro = " & nCodtpro.ToString & _
                " AND lc_listino = " & nListino.ToString & _
                " AND lc_datagg <= " & CDataSQL(strDatIni) & _
                " AND lc_datscad >= " & CDataSQL(strDatIni) & _
                " AND lc_daquant = 0" & _
                " AND lc_unmis = " & CStrSQL(strUnmis) & _
                " ORDER BY lc_codart, lc_fase, lc_codlavo, lc_conto, lc_codvalu, lc_codtpro, lc_listino," & _
                " lc_datagg DESC, lc_daquant"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '-----------------------------
      'se il listino non esiste, lo creo
      If dttTmp.Rows.Count = 0 Then

        'Se esiste un listino successivo prende la data di aggiornamento (- 1)
        'per la data di scadenza sul listino da inserire
        strSQL = "SELECT TOP 1 lc_datagg FROM listini" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart = " & CStrSQL(strCodart) & _
                " AND lc_fase = " & nFase.ToString & _
                " AND lc_codlavo = " & nCodlavo.ToString & _
                " AND lc_conto = " & lConto.ToString & _
                " AND lc_coddest = " & lCoddest.ToString & _
                " AND lc_tipo = " & CStrSQL(strTipoListino) & _
                " AND lc_codvalu = " & nCodvalu.ToString & _
                " AND lc_codtpro = " & nCodtpro.ToString & _
                " AND lc_listino = " & nListino.ToString & _
                " AND lc_datagg > " & CDataSQL(strDatIni) & _
                " AND lc_daquant = 0" & _
                " AND lc_unmis = " & CStrSQL(strUnmis) & _
                " ORDER BY lc_datagg ASC"
        dttTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp2.Rows.Count > 0 Then strDatscad = NTSCDate(dttTmp2.Rows(0)!lc_datagg).AddDays(-1).ToShortDateString
        dttTmp2.Clear()

        strSQL = "INSERT INTO listini (codditt, lc_codart, lc_codlavo, lc_conto, lc_codvalu," & _
                " lc_codtpro, lc_listino, lc_datagg, lc_tipo, lc_prezzo, lc_datscad, lc_daquant," & _
                " lc_aquant, lc_perqta, lc_unmis, lc_note, lc_netto, lc_fase, lc_coddest, lc_ultagg)" & _
                " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodart) & ", " & nCodlavo.ToString & ", " & _
                lConto.ToString & ", " & nCodvalu.ToString & ", " & nCodtpro.ToString & ", " & _
                nListino.ToString & ", " & CDataSQL(strDatIni) & "," & _
                CStrSQL(strTipoListino) & ", " & CDblSQL(dPrezzo) & ", " & _
                CDataSQL(strDatscad) & ", 0, 9999999999, " & CDblSQL(dPerqta) & ", " & _
                CStrSQL(strUnmis) & ", NULL, 'N', " & nFase.ToString & ", " & lCoddest & "," & _
                CDataOraSQL(Now) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'Fa scadere l'eventuale listino precedente
        strSQL = "UPDATE listini" & _
                " SET lc_datscad = " & CDataSQL(NTSCDate(strDatIni).AddDays(-1)) & "," & _
                " lc_ultagg = " & CDataOraSQL(Now) & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart = " & CStrSQL(strCodart) & _
                " AND lc_fase = " & nFase.ToString & _
                " AND lc_codlavo = " & nCodlavo.ToString & _
                " AND lc_conto = " & lConto.ToString & _
                " AND lc_coddest = " & lCoddest.ToString & _
                " AND lc_tipo = " & CStrSQL(strTipoListino) & _
                " AND lc_codvalu = " & nCodvalu.ToString & _
                " AND lc_codtpro = " & nCodtpro.ToString & _
                " AND lc_listino = " & nListino.ToString & _
                " AND lc_datagg < " & CDataSQL(strDatIni) & _
                " AND lc_daquant = 0" & _
                " AND lc_unmis = " & CStrSQL(strUnmis)
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Else
        '-----------------------------
        'il listino esiste già
        If NTSCDate(dttTmp.Rows(0)!lc_datagg) = NTSCDate(strDatIni) Then
          strSQL = "UPDATE listini SET lc_prezzo = " & CDblSQL(dPrezzo) & "," & _
                " lc_ultagg = " & CDataOraSQL(Now) & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart = " & CStrSQL(strCodart) & _
                " AND lc_fase = " & nFase.ToString & _
                " AND lc_codlavo = " & nCodlavo.ToString & _
                " AND lc_conto = " & lConto.ToString & _
                " AND lc_coddest = " & lCoddest.ToString & _
                " AND lc_tipo = " & CStrSQL(strTipoListino) & _
                " AND lc_codvalu = " & nCodvalu.ToString & _
                " AND lc_codtpro = " & nCodtpro.ToString & _
                " AND lc_listino = " & nListino.ToString & _
                " AND lc_datagg = " & CDataSQL(NTSCDate(dttTmp.Rows(0)!lc_datagg)) & _
                " AND lc_daquant = 0" & _
                " AND lc_unmis = " & CStrSQL(strUnmis)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        Else
          strSQL = "SELECT TOP 1 lc_datagg FROM listini" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart = " & CStrSQL(strCodart) & _
                " AND lc_fase = " & nFase.ToString & _
                " AND lc_codlavo = " & nCodlavo.ToString & _
                " AND lc_conto = " & lConto.ToString & _
                " AND lc_coddest = " & lCoddest.ToString & _
                " AND lc_tipo = " & CStrSQL(strTipoListino) & _
                " AND lc_codvalu = " & nCodvalu.ToString & _
                " AND lc_codtpro = " & nCodtpro.ToString & _
                " AND lc_listino = " & nListino.ToString & _
                " AND lc_datagg > " & CDataSQL(NTSCDate(dttTmp.Rows(0)!lc_datagg)) & _
                " AND lc_daquant = 0" & _
                " AND lc_unmis = " & CStrSQL(strUnmis) & _
                " ORDER BY lc_datagg ASC"
          dttTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp2.Rows.Count > 0 Then strDatscad = NTSCDate(dttTmp2.Rows(0)!lc_datagg).AddDays(-1).ToShortDateString
          dttTmp2.Clear()

          'Altrimenti fa scadere il listino vecchio
          strSQL = "UPDATE listini" & _
                " SET lc_datscad = " & CDataSQL(NTSCDate(strDatIni).AddDays(-1)) & "," & _
                " lc_ultagg = " & CDataOraSQL(Now) & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart = " & CStrSQL(strCodart) & _
                " AND lc_fase = " & nFase.ToString & _
                " AND lc_codlavo = " & nCodlavo.ToString & _
                " AND lc_conto = " & lConto.ToString & _
                " AND lc_coddest = " & lCoddest.ToString & _
                " AND lc_tipo = " & CStrSQL(strTipoListino) & _
                " AND lc_codvalu = " & nCodvalu.ToString & _
                " AND lc_codtpro = " & nCodtpro.ToString & _
                " AND lc_listino = " & nListino.ToString & _
                " AND lc_datagg = " & CDataSQL(NTSCDate(dttTmp.Rows(0)!lc_datagg)) & _
                " AND lc_daquant = 0" & _
                " AND lc_unmis = " & CStrSQL(strUnmis)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          strDatscad = IntSetDate("31/12/2099")
          strSQL = "SELECT TOP 1 lc_datagg FROM listini" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND lc_codart = " & CStrSQL(strCodart) & _
                " AND lc_fase = " & nFase.ToString & _
                " AND lc_codlavo = " & nCodlavo.ToString & _
                " AND lc_conto = " & lConto.ToString & _
                " AND lc_coddest = " & lCoddest.ToString & _
                " AND lc_tipo = " & CStrSQL(strTipoListino) & _
                " AND lc_codvalu = " & nCodvalu.ToString & _
                " AND lc_codtpro = " & nCodtpro.ToString & _
                " AND lc_listino = " & nListino.ToString & _
                " AND lc_datagg > " & CDataSQL(NTSCDate(dttTmp.Rows(0)!lc_datagg)) & _
                " AND lc_daquant = 0" & _
                " AND lc_unmis = " & CStrSQL(strUnmis) & _
                " ORDER BY lc_datagg ASC"
          dttTmp2 = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp2.Rows.Count > 0 Then strDatscad = NTSCDate(dttTmp2.Rows(0)!lc_datagg).AddDays(-1).ToShortDateString
          dttTmp2.Clear()

          'E ne crea uno nuovo con il prezzo e la data validità dei controlli
          strSQL = "INSERT INTO listini (codditt, lc_codart, lc_codlavo, lc_conto, lc_codvalu," & _
                " lc_codtpro, lc_listino, lc_datagg, lc_tipo, lc_prezzo, lc_datscad, lc_daquant," & _
                " lc_aquant, lc_perqta, lc_unmis, lc_note, lc_netto, lc_fase, lc_coddest, lc_ultagg)" & _
                " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodart) & ", " & nCodlavo.ToString & ", " & _
                lConto.ToString & ", " & nCodvalu.ToString & ", " & nCodtpro.ToString & ", " & _
                nListino.ToString & ", " & CDataSQL(strDatIni) & "," & _
                CStrSQL(strTipoListino) & ", " & CDblSQL(dPrezzo) & ", " & _
                CDataSQL(strDatscad) & ", 0, 9999999999, " & CDblSQL(dPerqta) & ", " & _
                CStrSQL(strUnmis) & ", NULL, 'N', " & nFase.ToString & ", " & lCoddest & "," & _
                CDataOraSQL(Now) & ")"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        End If
      End If    'If dttTmp.Rows.Count = 0 Then
      dttTmp.Clear()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LegOrgaDitt(ByVal strDitta As String, ByVal strValuein As String, _
                                   ByVal bObbl As Boolean, ByRef strOut As String, ByRef strError As String, _
                                   ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = " SELECT og_codcont, tb_descont, tb_descont2, og_descont, og_descont2 " & _
         " FROM (organig LEFT JOIN tabcont ON organig.codditt = tabcont.codditt AND organig.og_codcont = tabcont.tb_codcont) " & _
         " WHERE organig.codditt = " & CStrSQL(strDitta) & " And og_progr = " & strValuein

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ORGANIG")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SalvaAllole(ByVal strDitta As String, ByVal strTipo As String, _
                                          ByVal lCodice As Integer, ByVal strStrcod As String, _
                                          ByVal strTipodoc As String, ByVal nAnnoDoc As Integer, _
                                          ByVal strSerieDoc As String, ByVal lNumdoc As Integer, _
                                          ByVal lRigadoc As Integer, ByVal lCommeca As Integer, _
                                          ByVal lLotto As Integer, ByVal strMatric As String, _
                                          ByVal strDescr As String, ByVal strCartella As String, _
                                          ByVal strNomedoc As String, ByVal strTipork As String, _
                                          ByVal lControp As Integer, ByVal lCodlead As Integer, _
                                          ByVal lCodoppo As Integer, ByVal lCodchia As Integer, _
                                          ByVal lNumcontr As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strElencoCampi As String
    Dim lNum As Integer
    Try
      strElencoCampi = "codditt, ao_tipo, ao_codice, ao_progress, ao_strcod, ao_tipodoc," & _
        " ao_annodoc, ao_seriedoc, ao_numdoc, ao_rigadoc, ao_commeca, ao_lotto, ao_matric," & _
        " ao_descr, ao_classe, ao_classeole, ao_descr2, ao_argom, ao_ultagg, ao_datins," & _
        " ao_ubicaz, ao_autore, ao_redattore, ao_progresl, ao_cartella, ao_nomedoc, ao_tipork," & _
        " ao_controp, ao_codlead, ao_codoppo, ao_codchia, ao_numcontr"

      lNum = LegNuma(strDitta, "OO", "", 0, True)
      lNum = AggNuma(strDitta, "OO", "", 0, lNum, False, True, "")

      strSQL = "INSERT INTO allole (" & strElencoCampi & ")" & _
        " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strTipo) & ", " & _
        lCodice & ", " & lNum & ", " & _
        CStrSQL(IIf(strStrcod = "", " ", strStrcod)) & ", " & _
        CStrSQL(IIf(strTipodoc = "", " ", strTipodoc)) & ", " & nAnnoDoc & ", " & _
        CStrSQL(IIf(strSerieDoc = "", " ", strSerieDoc)) & ", " & lNumdoc & ", " & _
        lRigadoc & ", " & lCommeca & ", " & lLotto & ", " & _
        CStrSQL(IIf(strMatric = "", " ", strMatric)) & ", " & _
        CStrSQL(strDescr) & ", ' ', ' ', NULL, NULL, " & _
        CDataOraSQL(Now) & ", " & CDataOraSQL(Now) & "," & _
        " NULL, NULL, " & CStrSQL("BUSINESS-" & oApp.User.Nome) & ", " & lNum & ", " & _
        CStrSQL(strCartella) & ", " & CStrSQL(strNomedoc) & ", " & _
        CStrSQL(strTipork) & ", " & lControp & ", " & lCodlead & ", " & lCodoppo & ", " & _
        lCodchia & ", " & lNumcontr & ")"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GetAllole(ByVal strDitta As String, ByVal strTipo As String, _
                                        ByVal strTipork As String, ByVal strNomedoc As String, _
                                        ByVal strStrcod As String, ByVal lCodice As Integer, _
                                        ByVal lCommeca As Integer, ByVal lLotto As Integer, _
                                        ByVal strTipodoc As String, ByVal nAnnoDoc As Integer, _
                                        ByVal strSerieDoc As String, ByVal lNumdoc As Integer, _
                                        ByVal strMatric As String, ByVal lRigadoc As Integer, _
                                        ByVal lCodlead As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT ao_progress FROM allole" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ao_tipo = " & CStrSQL(strTipo) & _
        " AND ao_tipork = " & CStrSQL(strTipork) & _
        " AND UPPER(Left(ao_redattore, 9)) = 'BUSINESS-'" & _
        " AND ao_nomedoc = " & CStrSQL(strNomedoc)
      Select Case strTipo
        Case "A", "D" '--- Articolo/Distinta Base
          strSQL = strSQL & " AND ao_strcod = " & CStrSQL(strStrcod)
        Case "C"      '--- Cliente/Fornitore
          strSQL = strSQL & " AND ao_codice = " & lCodice
        Case "K"      '--- Commessa
          strSQL = strSQL & " AND ao_commeca = " & lCommeca
        Case "L"      '--- Lotto
          strSQL = strSQL & " AND ao_lotto = " & lLotto
        Case "M", "O" '--- Documento di magazzino/Ordine
          strSQL = strSQL & " AND ao_codice = " & lCodice & _
            " AND ao_tipodoc = " & CStrSQL(strTipodoc) & _
            " AND ao_annodoc = " & nAnnoDoc & _
            " AND ao_seriedoc = " & CStrSQL(strSerieDoc) & _
            " AND ao_numdoc = " & lNumdoc
        Case "P"      '--- Partite contabili
          strSQL = strSQL & " AND ao_codice = " & lCodice & _
            " AND ao_annodoc = " & nAnnoDoc & _
            " AND ao_seriedoc = " & CStrSQL(strSerieDoc) & _
            " AND ao_numdoc = " & lNumdoc
        Case "N"      '--- Matricola
          strSQL = strSQL & " AND ao_strcod = " & CStrSQL(strStrcod) & _
            " AND ao_matric = " & CStrSQL(strMatric)
        Case "!"      '--- Offerte
          strSQL = strSQL & " AND ao_codice = " & lCodice & _
            " AND ao_tipodoc = " & CStrSQL(strTipodoc) & _
            " AND ao_annodoc = " & nAnnoDoc & _
            " AND ao_seriedoc = " & CStrSQL(strSerieDoc) & _
            " AND ao_numdoc = " & lNumdoc & _
            " AND ao_rigadoc = " & lRigadoc & _
            " AND ao_codlead = " & lCodlead
      End Select

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ALLOLE")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SovrascriviAllole(ByVal strDitta As String, ByVal bEsisteAllole As Boolean, _
                                                ByVal strTipo As String, ByVal lCodice As Integer, _
                                                ByVal strStrcod As String, ByVal strTipodoc As String, _
                                                ByVal nAnnoDoc As Integer, ByVal strSerieDoc As String, _
                                                ByVal lNumdoc As Integer, ByVal lRigadoc As Integer, _
                                                ByVal lCommeca As Integer, ByVal lLotto As Integer, _
                                                ByVal strMatric As String, ByVal strDescr As String, _
                                                ByVal strCartella As String, ByVal strNomedoc As String, _
                                                ByVal strTipork As String, ByVal lControp As Integer, _
                                                ByVal lCodlead As Integer, ByVal lCodoppo As Integer, _
                                                ByVal lCodchia As Integer, ByVal lNumcontr As Integer, _
                                                ByVal lProgresl As Integer) As Boolean
    Dim strSQL As String = ""
    Dim lNum As Integer
    Dim strElencoCampi As String
    Try
      strElencoCampi = "codditt, ao_tipo, ao_codice, ao_progress, ao_strcod, ao_tipodoc," & _
        " ao_annodoc, ao_seriedoc, ao_numdoc, ao_rigadoc, ao_commeca, ao_lotto, ao_matric," & _
        " ao_descr, ao_classe, ao_classeole, ao_descr2, ao_argom, ao_ultagg, ao_datins," & _
        " ao_ubicaz, ao_autore, ao_redattore, ao_progresl, ao_cartella, ao_nomedoc, ao_tipork," & _
        " ao_controp, ao_codlead, ao_codoppo, ao_codchia, ao_numcontr"

      If bEsisteAllole = False Then
        lNum = LegNuma(strDitta, "OO", "", 0, True)
        lNum = AggNuma(strDitta, "OO", "", 0, lNum, False, True, "")

        strSQL = "INSERT INTO allole (" & strElencoCampi & ")" & _
          " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strTipo) & ", " & _
          lCodice & ", " & lNum & ", " & _
          CStrSQL(IIf(strStrcod = "", " ", strStrcod)) & ", " & _
          CStrSQL(IIf(strTipodoc = "", " ", strTipodoc)) & ", " & nAnnoDoc & ", " & _
          CStrSQL(IIf(strSerieDoc = "", " ", strSerieDoc)) & ", " & lNumdoc & ", " & _
          lRigadoc & ", " & lCommeca & ", " & lLotto & ", " & _
          CStrSQL(IIf(strMatric = "", " ", strMatric)) & ", " & _
          CStrSQL(strDescr) & ", ' ', ' ', NULL, NULL, " & _
          CDataOraSQL(Now) & ", " & CDataOraSQL(Now) & "," & _
          " NULL, NULL, " & CStrSQL("BUSINESS-" & oApp.User.Nome) & ", " & lNum & ", " & _
          CStrSQL(strCartella) & ", " & CStrSQL(strNomedoc) & ", " & _
          CStrSQL(strTipork) & ", " & lControp & ", " & lCodlead & ", " & _
          lCodoppo & ", " & lCodchia & ", " & lNumcontr & ")"
      Else
        strSQL = "UPDATE allole" & _
          " SET ao_ultagg = " & CDataOraSQL(Now) & "," & _
          " ao_redattore = " & CStrSQL("BUSINESS-" & oApp.User.Nome) & "," & _
          " ao_cartella = " & CStrSQL(strCartella) & "," & _
          " ao_nomedoc = " & CStrSQL(strNomedoc) & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND ao_progress = " & lProgresl
      End If

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GeneraNumcommeca_CreaBudget(ByVal strDitta As String, ByVal lComme As Integer) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    '--- Chiamto da GeneraNumCommeca: crea il busget per la commessa appena creata
    '----------------------------------------------------------------------------------------------------------------
    Dim i As Integer = 0
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT pb_riga, pb_subcommeca, pb_conto, pb_origine, pb_darave, pb_unmis FROM probudget" & _
        " WHERE pb_tipork = 'C'" & _
        " AND codditt = " & CStrSQL(strDitta) & _
        " ORDER BY codditt, pb_tipork, pb_riga"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        i = 0
        For i = 0 To (dttTmp.Rows.Count - 1)
          With dttTmp.Rows(i)
            strSQL = "INSERT INTO budget (codditt, bu_tipork, bu_escomp, bu_commeca, bu_codcena, bu_codcfam," & _
              " bu_riga, bu_subcommeca, bu_conto, bu_origine, bu_darave, bu_unmis)" & _
              " VALUES (" & CStrSQL(strDitta) & " , 'C', 0, " & lComme & ", 0, ' ', " & _
              CDblSQL(NTSCDec(!pb_riga)) & ", " & CStrSQL(!pb_subcommeca) & ", " & NTSCInt(!pb_conto) & ", " & _
              CStrSQL(!pb_origine) & ", " & CStrSQL(!pb_darave) & ", " & CStrSQL(!pb_unmis) & ")"
          End With
          Try
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          Catch
            '--- Ignora gli eventuali errori di chiave duplicata, perché in VB6, la gdb.Execute non scatenava erriri
          End Try
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT * FROM probudgetd" & _
        " WHERE pbd_tipork = 'C'" & _
        " AND codditt = " & CStrSQL(strDitta)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        i = 0
        For i = 0 To (dttTmp.Rows.Count - 1)
          With dttTmp.Rows(i)
            strSQL = "INSERT INTO budgetd (codditt, bud_tipork, bud_escomp, bud_commeca, bud_codcena, bud_codcfam," & _
              " bud_riga, bud_rigaa, bud_codart, bud_desart, bud_codlavo, bud_deslavo, bud_unmis)" & _
              " VALUES " & CStrSQL(strDitta) & ", 'C', 0, " & lComme & ", 0, ' ', " & _
              CDblSQL(NTSCDec(!pbd_riga)) & ", " & NTSCInt(!pbd_rigaa) & ", " & CStrSQL(!pbd_codart) & ", " & _
              CStrSQL(!pbd_desart) & ", " & NTSCInt(!pbd_codlavo) & ", " & CStrSQL(!pbd_deslavo) & ", " & _
              CStrSQL(!pbd_unmis) & ")"
          End With
          Try
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          Catch
            '--- Ignora gli eventuali errori di chiave duplicata, perché in VB6, la gdb.Execute non scatenava erriri
          End Try
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

#Region "CATALOGO FORNITORI"
  Public Overridable Function CreaArticoDaArtest(ByVal strDitta As String, ByVal strAe_codartf As String, _
                                                 ByVal strAe_forn As String, ByVal strAe_codmarc As String, _
                                                 ByRef strArticoCodArt As String, ByRef strError As String, _
                                                 ByVal nControa As Integer, ByVal nControp As Integer, _
                                                 ByVal nContros As Integer, ByVal nLungRoot As Integer, _
                                                 ByVal nLungExt As Integer, ByVal nCodArtDaCat As Integer, _
                                                 ByVal nCodArtDaCatNListPubb As Integer, ByVal nCodArtDaCatNListIngr As Integer, _
                                                 ByVal bGestFaseDaCatalogo As Boolean, ByVal nFaseDaCatalogo As Integer, _
                                                 ByVal nLavorazioneDaCatalogo As Integer, ByVal strDesFaseDaCatalogo As String) As Boolean

    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Dim strSiglaForn As String
    Dim lProgr As Integer
    Dim strForn As String
    Dim strRiempimento As String
    Dim i As Integer
    Dim dsBarcode As DataSet = Nothing
    Dim dsCatForn As DataSet = Nothing
    Dim strString As String
    Dim nCodIva As Integer
    Dim strCodartSostitutivo As String = ""
    Dim strCodartSostituito As String = ""
    Try

      strSQL = "SELECT artest.* FROM artest"
      strSQL = strSQL & " WHERE codditt = " & CStrSQL(strDitta)
      strSQL = strSQL & " AND ae_codartf = " & CStrSQL(strAe_codartf)
      strSQL = strSQL & " AND ae_forn = " & strAe_forn
      strSQL = strSQL & " AND ae_codmarc = " & strAe_codmarc

      '--------------------------------------------------
      'Apre il recordset

      dsCatForn = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "ARTEST")

      If dsCatForn.Tables("ARTEST").Rows.Count = 0 Then
        Return False
      End If

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)


      'controllo in artest articolo gia agganciato
      If Not (NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codart) = "") Then
        strError = "Codice articolo |" & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codartf) & "| già importato."
        'passa successivo
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Return False
      End If

      'genera ar_codart
      Select Case nCodArtDaCat
        Case 0

          strSiglaForn = LTrim(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_siglaforn))
          strForn = Trim(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codartf))

          strArticoCodArt = strSiglaForn & strForn

        Case 1

          strForn = Trim(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codartf))

          strArticoCodArt = strForn

        Case 2

          lProgr = LegNuma(strDitta, "AA", "D", 0, True, dbConn)
          lProgr = AggNuma(strDitta, "AA", "D", 0, lProgr, True, False, "", dbConn)

          strString = ""
          For i = 0 To nLungExt - 1
            strString = strString & "0"
          Next

          strArticoCodArt = "".PadLeft(nLungExt - Len(NTSCStr(lProgr)), CChar("0")) & NTSCStr(lProgr)

        Case 3

          strSiglaForn = LTrim(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_siglaforn))
          If nLungRoot > Len(strSiglaForn) Then 'riempio

            strRiempimento = ""
            For i = 0 To nLungRoot - Len(strSiglaForn) - 1
              strRiempimento = strRiempimento & Chr(45)
            Next
            strSiglaForn = strSiglaForn & strRiempimento

          ElseIf nLungRoot < Len(strSiglaForn) Then 'taglio

            strSiglaForn = Left(strSiglaForn, nLungRoot)

          End If

          lProgr = LegNuma(strDitta, "AA", "D", 0, True, dbConn)
          lProgr = AggNuma(strDitta, "AA", "D", 0, lProgr, True, False, "", dbConn)

          strString = ""
          For i = 0 To nLungExt - 1
            strString = strString & "0"
          Next

          strArticoCodArt = strSiglaForn & "".PadLeft(nLungExt - Len(NTSCStr(lProgr)), CChar("0")) & NTSCStr(lProgr)

      End Select

      'porto in maiuscolo il codice articolo

      strArticoCodArt = UCase(strArticoCodArt)

      'controllo ar_codart non superi 18 caratteri
      If Len(strArticoCodArt) > CLN__STD.CodartMaxLen Then
        strError = "Codice: |" & strArticoCodArt & "| superiore ai |" & CLN__STD.CodartMaxLen.ToString & "| caratteri" & vbCrLf & _
        "Generazione articolo non possibile!"
        'passa successivo
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Return False
      End If

      'controllo in artico articolo gia presente
      If ValCodiceDb(strArticoCodArt, strDitta, "ARTICO", "S") Then
        strError = "Codice articolo |" & strArticoCodArt & "| già esistente."
        'passa successivo
        If IsInTrans Then AnnullaTrans()
        dbConn.Close()
        Return False
      End If

      'inserimento artico
      If NTSCInt(dsCatForn.Tables("ARTEST").Rows(0)!ae_codiva) = 0 Then
        nCodIva = NTSCInt(GetSettingBusDitt(strDitta, "BSMGARTI", "OPZIONI", ".", "CodIva", "0", " ", "0"))
      Else
        nCodIva = NTSCInt(dsCatForn.Tables("ARTEST").Rows(0)!ae_codiva)
      End If

      strCodartSostitutivo = NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_sostit)
      If strCodartSostitutivo.Trim.Length = 0 Then
        strCodartSostitutivo = "NULL"
      Else
        strCodartSostitutivo = CStrSQL(strCodartSostitutivo)
      End If

      strCodartSostituito = NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_sostituito)
      If strCodartSostituito.Trim.Length = 0 Then
        strCodartSostituito = "NULL"
      Else
        strCodartSostituito = CStrSQL(strCodartSostituito)
      End If

      strSQL = "INSERT INTO artico (" & _
      "codditt , ar_codart, ar_codalt, ar_descr, ar_desint, ar_tipo, ar_unmis, ar_unmis2," & _
      " ar_conver , ar_confez2, ar_qtacon2, ar_forn, ar_forn2, ar_codiva, ar_gruppo, ar_sotgru," & _
      " ar_controp , ar_catlifo, ar_claprov, ar_clascon, ar_ubicaz, ar_scomin, ar_scomax," & _
      " ar_minord , ar_ggrior, ar_sostit, ar_controa, ar_reparto, ar_stalist, ar_ultagg," & _
      " ar_contriva, ar_oragg, ar_datins, ar_orins, ar_tipprom, ar_famprod, ar_numecr," & _
      " ar_areacm3, ar_codvuo, ar_flricmar, ar_codpdon, ar_ricar1, ar_ricar2, ar_garacq," & _
      " ar_garven, ar_gesvar, ar_prevar, ar_prorig, ar_percvst, ar_codnomc, ar_pesolor," & _
      " ar_pesonet, ar_paeorig, ar_livmindb, ar_coddb, ar_stainv, ar_consmrp, ar_stasche, ar_geslotti," & _
      " ar_inesaur, ar_note, ar_tipoopz, ar_polriord, ar_gescomm, ar_fpfence, ar_rrfence," & _
      " ar_sostituito, ar_critico, ar_codroot, ar_codvar1, ar_codvar2, ar_codvar3, ar_formula," & _
      " ar_nummisure, ar_contros, ar_pesoca, ar_gestmatr, ar_umdapr, ar_magstock, ar_magprod," & _
      " ar_magvend, ar_umintra2, ar_codappr, ar_codmarc, ar_tipokit, ar_perqta, ar_fcorrlt," & _
      " ar_magnoconf, ar_codtipa, ar_verdb , ar_blocco, ar_um4, ar_umdapra, ar_umpdapr," & _
      " ar_umpdapra, ar_opnome, ar_gif1 , ar_gif2, ar_ggant, ar_ggpost, ar_gescon, ar_flmod," & _
      " ar_codimba , ar_misura1, ar_misura2, ar_misura3, ar_gesubic, ar_gesfasi, ar_ultfase," & _
      " ar_volume, ar_makebuy, ar_sublotto, ar_maxlotto, ar_ggragg, ar_ripriord, ar_perragg," & _
      " ar_gestser, ar_tipitemcp3, ar_codtagl, ar_anno, ar_codstag, ar_cartric, ar_cartcanas," & _
      " ar_cartcanol, ar_scosic" & _
      ")"

      strSQL = strSQL & " VALUES (" & _
      CStrSQL(strDitta) & ", " & CStrSQL(strArticoCodArt) & ", NULL," & _
      " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_descr)) & "," & _
      " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_desint)) & "," & _
      " NULL," & _
      " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_unmis)) & "," & _
      " NULL, 0," & _
      " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_confez2)) & "," & _
      " " & CDblSQL(NTSCDec(dsCatForn.Tables("ARTEST").Rows(0)!ae_qtacon2)) & "," & _
      " " & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_forn) & "," & _
      " 0," & _
      " " & nCodIva & "," & _
      " " & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_gruppo) & "," & _
      " " & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_sotgru) & "," & _
      " " & nControp & "," & _
      " 0, 0," & _
      " " & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_clascon) & "," & _
      " NULL, 0, 0," & _
      " " & CDblSQL(NTSCDec(dsCatForn.Tables("ARTEST").Rows(0)!ae_minordin)) & "," & _
      " 0," & _
      " " & strCodartSostitutivo & "," & _
      " " & nControa & "," & _
      " 0,"
      If NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_status) = "D" Then
        strSQL = strSQL & " 'N',"
      ElseIf NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_status) = "S" Then
        strSQL = strSQL & " 'N',"
      ElseIf NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_status) = "V" Then
        strSQL = strSQL & " 'N',"
      Else
        strSQL = strSQL & " 'S',"
      End If
      strSQL = strSQL & " " & CDataSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_ultagg)) & ","
      strSQL = strSQL & " NULL, 0 , NULL, 0, 0," & _
      " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_famprod)) & "," & _
      " 0, 0, 0, 'R', 0, 0, 0, 0, 0, 'N', 'N', NULL, 0," & _
      " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codnomc)) & "," & _
      " " & CDblSQL(NTSCDec(dsCatForn.Tables("ARTEST").Rows(0)!ae_pesolor)) & "," & _
      " " & CDblSQL(NTSCDec(dsCatForn.Tables("ARTEST").Rows(0)!ae_pesonet)) & "," & _
      " NULL, 0, NULL,"
      If NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_status) = "D" Then
        strSQL = strSQL & " 'N',"
      Else
        strSQL = strSQL & " 'S',"
      End If
      If NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_status) = "D" Then
        strSQL = strSQL & " 'N',"
      Else
        strSQL = strSQL & " 'S',"
      End If
      If NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_status) = "D" Then
        strSQL = strSQL & " 'N',"
      Else
        strSQL = strSQL & " 'S',"
      End If
      strSQL = strSQL & " 'N',"
      If NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_status) = "E" Then
        strSQL = strSQL & " 'S',"
      Else
        strSQL = strSQL & " 'N',"
      End If
      strSQL = strSQL & " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_note)) & "," & _
      " ' ', 'G', 'N', 0, " & _
      " " & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_rrfence) & "," & _
      " " & strCodartSostituito & "," & _
      " 'N', NULL, NULL, NULL, NULL, NULL, 0," & _
      " " & nContros & "," & _
      " 1, 'N', 'P', 0, 0, 0, 'N', 0," & _
      " " & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codmarc) & "," & _
      " ' ', 1, 0, 0, 1, 0,"
      If NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_status) = "V" Then
        strSQL = strSQL & " 'S',"
      Else
        strSQL = strSQL & " 'N',"
      End If
      strSQL = strSQL & " NULL, 'P', 'P', 'P', NULL, NULL, NULL, 0, 0, 'N', NULL, 0, 0, 0, 0, 'N'," & _
      " " & NTSCStr(IIf(bGestFaseDaCatalogo = True, "'S'", "'N'")) & "," & _
      " " & NTSCStr(IIf(bGestFaseDaCatalogo = True, NTSCStr(nFaseDaCatalogo), "0")) & "," & _
      " " & CDblSQL(NTSCDec(dsCatForn.Tables("ARTEST").Rows(0)!ae_volume)) & "," & _
      " ' '," & _
      " " & CDblSQL(NTSCDec(dsCatForn.Tables("ARTEST").Rows(0)!ae_sublotto)) & "," & _
      " 0, 1, 'N', 'G', 'N', 'R', 0, 0, 0, NULL, NULL, NULL, 0"

      strSQL = strSQL & ")"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'inserimento fasi
      If bGestFaseDaCatalogo Then

        strSQL = "INSERT INTO artfasi (codditt, af_codart, af_fase, af_descr, af_codlavo," & _
          " af_ultagg, af_opnome, af_livmindb, af_rrfence, af_fcorrlt, af_makebuy)" & _
          " VALUES (" & CStrSQL(strDitta) & "," & _
          " " & CStrSQL(strArticoCodArt) & "," & _
          " " & nFaseDaCatalogo & "," & _
          " " & CStrSQL(strDesFaseDaCatalogo) & "," & _
          " " & nLavorazioneDaCatalogo & "," & _
          " " & CDataSQL(Now.ToShortDateString) & "," & _
          " " & CStrSQL(oApp.User.Nome) & "," & _
          " 0, 0, 0, ' '" & _
          ")"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      End If

      'inserimento codarfo

      If Not Len(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codartf)) > CLN__STD.CodartMaxLen Then

        strSQL = "INSERT INTO codarfo (codditt, caf_conto, caf_codart, caf_codarfo, caf_desnote," & _
        " caf_rrfence, caf_fcorrlt, caf_ultagg, caf_opnome, caf_percrip)" & _
        " VALUES (" & CStrSQL(strDitta) & "," & _
        " " & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_forn) & "," & _
        " " & CStrSQL(strArticoCodArt) & "," & _
        " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codartf)) & "," & _
        " NULL, 0, 0,"
        strSQL = strSQL & " " & CDataSQL(Now.ToShortDateString) & ","
        strSQL = strSQL & " NULL, 0 " & _
        ")"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      End If

      'inserimento artval ling1

      If (Trim(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_descrl1)) <> "") Or (Trim(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_desintl1)) <> "") Then

        strSQL = "INSERT INTO artval (codditt, ax_codart, ax_codvalu, ax_descr," & _
        " ax_desint, ax_ultagg, ax_note)" & _
        " VALUES (" & CStrSQL(strDitta) & "," & _
        " " & CStrSQL(strArticoCodArt) & "," & _
        " " & 1 & "," & _
        " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_descrl1)) & ", " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_desintl1)) & "," & _
        " " & CDataSQL(Now.ToShortDateString) & ", NULL" & _
        ")"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      End If

      'inserimento artval ling2

      If (Trim(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_descrl2)) <> "") Or (Trim(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_desintl2)) <> "") Then

        strSQL = "INSERT INTO artval (codditt, ax_codart, ax_codvalu, ax_descr," & _
        " ax_desint, ax_ultagg, ax_note)" & _
        " VALUES (" & CStrSQL(strDitta) & "," & _
        " " & CStrSQL(strArticoCodArt) & "," & _
        " " & 2 & "," & _
        " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_descrl2)) & ", " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_desintl2)) & "," & _
        " " & CDataSQL(Now.ToShortDateString) & ", NULL" & _
        ")"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      End If

      'inserimento artval ling3

      If (Trim(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_descrl3)) <> "") Or (Trim(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_desintl3)) <> "") Then

        strSQL = "INSERT INTO artval (codditt, ax_codart, ax_codvalu, ax_descr," & _
        " ax_desint, ax_ultagg, ax_note)" & _
        " VALUES (" & CStrSQL(strDitta) & "," & _
        " " & CStrSQL(strArticoCodArt) & "," & _
        " " & 3 & "," & _
        " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_descrl3)) & ", " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_desintl3)) & "," & _
        " " & CDataSQL(Now.ToShortDateString) & ", NULL" & _
        ")"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      End If

      'inserimento primo barcode

      If (Trim(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codeump)) <> "") Then

        'controllo presenza primo barcode

        strSQL = "SELECT bc_code FROM barcode" & _
                    " WHERE bc_code = " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codeump)) & _
                    " AND codditt = " & CStrSQL(strDitta)

        dsBarcode = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "BARCODE")

        If dsBarcode.Tables("BARCODE").Rows.Count > 0 Then
          strError = "Codice barcode: |" & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codeump) & "| già presente per la ditta |" & strDitta & vbCrLf & _
          "|Generazione articolo non possibile!"
          'passa successivo
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If

        'inserimento primo barcode

        strSQL = "INSERT INTO barcode (codditt, bc_codart, bc_datins, bc_code, bc_unmis," & _
        " bc_quant, bc_orins, bc_datagg, bc_oragg, bc_tipo, bc_tagl, bc_fase)" & _
        " VALUES (" & CStrSQL(strDitta) & "," & _
        " " & CStrSQL(strArticoCodArt) & ","
        strSQL = strSQL & " " & CDataSQL(Now.ToShortDateString) & ","
        strSQL = strSQL & " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codeump)) & "," & _
        " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_unmis)) & "," & _
        " 1, 0,"
        strSQL = strSQL & " " & CDataSQL(Now.ToShortDateString) & ","
        strSQL = strSQL & " 0, 'E', NULL, 0)"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      'inserimento secondo barcode

      If (Trim(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codeconf)) <> "") Then

        'controllo presenza secondo barcode

        strSQL = "SELECT bc_code FROM barcode" & _
                    " WHERE bc_code = " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codeconf)) & _
                    " AND codditt = " & CStrSQL(strDitta)

        dsBarcode = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "BARCODE")

        If dsBarcode.Tables("BARCODE").Rows.Count > 0 Then
          strError = "Codice barcode: |" & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codeconf) & "| già presente per la ditta |" & strDitta & vbCrLf & _
          "|Generazione articolo non possibile!"
          'passa successivo
          If IsInTrans Then AnnullaTrans()
          dbConn.Close()
          Return False
        End If

        'inserimento secondo barcode

        strSQL = "INSERT INTO barcode (codditt, bc_codart, bc_datins, bc_code, bc_unmis," & _
        " bc_quant, bc_orins, bc_datagg, bc_oragg, bc_tipo, bc_tagl, bc_fase)" & _
        " VALUES (" & CStrSQL(strDitta) & "," & _
        " " & CStrSQL(strArticoCodArt) & ","
        strSQL = strSQL & " " & CDataSQL(Now.ToShortDateString) & ","
        strSQL = strSQL & " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codeconf)) & "," & _
        " " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_confez2)) & "," & _
        " " & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_qtacon2) & "," & _
        " 0,"
        strSQL = strSQL & " " & CDataSQL(Now.ToShortDateString) & ","
        strSQL = strSQL & " 0, 'E', NULL, 0)"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      'inserimento listini
      If NTSCDec(dsCatForn.Tables("ARTEST").Rows(0)!ae_prezzopu) <> 0 Then
        ScriviPrezzo(strDitta, strArticoCodArt, _
        NTSCInt(IIf(bGestFaseDaCatalogo = True, NTSCStr(nFaseDaCatalogo), "0")), _
        0, _
        " ", nCodArtDaCatNListPubb, 0, 0, NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_unmis), 0, 0, NTSCDate(dsCatForn.Tables("ARTEST").Rows(0)!ae_dataggpu), _
        NTSCDec(dsCatForn.Tables("ARTEST").Rows(0)!ae_prezzopu), NTSCDate("31/12/2099"), 1, 9999999999, "", "N", True, dbConn)
      End If

      If NTSCDec(dsCatForn.Tables("ARTEST").Rows(0)!ae_prezzogr) <> 0 Then
        ScriviPrezzo(strDitta, strArticoCodArt, _
        NTSCInt(IIf(bGestFaseDaCatalogo = True, NTSCStr(nFaseDaCatalogo), "0")), _
        0, _
        " ", nCodArtDaCatNListIngr, 0, 0, NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_unmis), 0, 0, NTSCDate(dsCatForn.Tables("ARTEST").Rows(0)!ae_datagggr), _
        NTSCDec(dsCatForn.Tables("ARTEST").Rows(0)!ae_prezzogr), NTSCDate("31/12/2099"), 1, 9999999999, "", "N", True, dbConn)
      End If

      'update artest del codice di artico creato
      strSQL = "UPDATE artest" & _
        " SET ae_codart = " & CStrSQL(strArticoCodArt) & "," & _
        " ae_ultagg = " & CDataOraSQL(DateTime.Now.ToString) & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ae_codartf = " & CStrSQL(NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codartf)) & _
        " AND ae_forn = " & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_forn) & _
        " AND ae_codmarc = " & NTSCStr(dsCatForn.Tables("ARTEST").Rows(0)!ae_codmarc)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      'non eseguo la gestione errori standard ma giro l'errore 
      'direttamente al componente entity che mi ha chiamato
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

  Public Overridable Function ScriviPrezzo(ByVal strDitta As String, ByVal strCodartListino As String, _
                                           ByVal nFase As Integer, ByVal nCodlavo As Integer, ByVal strTipo As String, _
                                           ByVal nListino As Integer, ByVal lConto As Integer, ByVal nCodvalu As Integer, _
                                           ByVal strUnmis As String, ByVal nCodtpro As Integer, ByVal dDaQuant As Decimal, _
                                           ByVal dtDatAgg As Date, ByVal dPrezzo As Decimal, ByVal dtDatscadPromo As Date, _
                                           ByVal dPerqta As Decimal, ByVal dAquant As Decimal, ByVal strNote As String, _
                                           ByVal strNetto As String, ByVal bUpdateexistingrk As Boolean, _
                                           Optional ByVal dbConn As DbConnection = Nothing) As Integer
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return ScriviPrezzo(strDitta, strCodartListino, nFase, nCodlavo, strTipo, nListino, lConto, nCodvalu, _
        strUnmis, nCodtpro, dDaQuant, dtDatAgg, dPrezzo, dtDatscadPromo, dPerqta, dAquant, strNote, strNetto, _
        bUpdateexistingrk, "", 0, dbConn)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function ScriviPrezzo(ByVal strDitta As String, ByVal strCodartListino As String, _
                                           ByVal nFase As Integer, ByVal nCodlavo As Integer, ByVal strTipo As String, _
                                           ByVal nListino As Integer, ByVal lConto As Integer, ByVal nCodvalu As Integer, _
                                           ByVal strUnmis As String, ByVal nCodtpro As Integer, ByVal dDaQuant As Decimal, _
                                           ByVal dtDatAgg As Date, ByVal dPrezzo As Decimal, ByVal dtDatscadPromo As Date, _
                                           ByVal dPerqta As Decimal, ByVal dAquant As Decimal, ByVal strNote As String, _
                                           ByVal strNetto As String, ByVal bUpdateexistingrk As Boolean, _
                                           ByVal strDataScadenzaListino As String, _
                                           Optional ByVal dbConn As DbConnection = Nothing) As Integer
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodartListino, nFase, nCodlavo, strTipo, nListino, lConto, nCodvalu, _
        strUnmis, nCodtpro, dDaQuant, dtDatAgg, dPrezzo, dtDatscadPromo, dPerqta, dAquant, strNote, strNetto, _
        bUpdateexistingrk, strDataScadenzaListino, dbConn})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      '--------------------------------------------------------------------------------------------------------------
      Return ScriviPrezzo(strDitta, strCodartListino, nFase, nCodlavo, strTipo, nListino, lConto, nCodvalu, _
        strUnmis, nCodtpro, dDaQuant, dtDatAgg, dPrezzo, dtDatscadPromo, dPerqta, dAquant, strNote, strNetto, _
        bUpdateexistingrk, strDataScadenzaListino, 0, dbConn)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function
  Public Overridable Function ScriviPrezzo(ByVal strDitta As String, ByVal strCodartListino As String, _
                                           ByVal nFase As Integer, ByVal nCodlavo As Integer, ByVal strTipo As String, _
                                           ByVal nListino As Integer, ByVal lConto As Integer, ByVal nCodvalu As Integer, _
                                           ByVal strUnmis As String, ByVal nCodtpro As Integer, ByVal dDaQuant As Decimal, _
                                           ByVal dtDatAgg As Date, ByVal dPrezzo As Decimal, ByVal dtDatscadPromo As Date, _
                                           ByVal dPerqta As Decimal, ByVal dAquant As Decimal, ByVal strNote As String, _
                                           ByVal strNetto As String, ByVal bUpdateexistingrk As Boolean, _
                                           ByVal strDataScadenzaListino As String, ByVal lCoddest As Integer, _
                                           Optional ByVal dbConn As DbConnection = Nothing) As Integer
    '----------------------------------
    'Si occupa di inserire un nuovo record se non esiste la stessa combinazione
    'strDitta/strCodartListino/nFase/nCodlavo/strTipo/nListino/lConto/nCodvalu/strUnmis
    'nCodtpro/dDaQuant/dtDatagg
    'altrimenti se esiste ma con data validità anteriore fa scadere il record precedente e ne scrive uno nuovo
    'in caso contrario (data aggiornamento identica) sovrascrive
    'RITORNA: il campo lc_progr del record inserito o aggiornato(se update) 0 = se qualcosa è andato storto

    Dim strSQL As String = ""
    Dim strDataScadenza As String = ""
    Dim dttTmp As New DataTable
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodartListino, nFase, nCodlavo, strTipo, nListino, lConto, nCodvalu, _
        strUnmis, nCodtpro, dDaQuant, dtDatAgg, dPrezzo, dtDatscadPromo, dPerqta, dAquant, strNote, strNetto, _
        bUpdateexistingrk, strDataScadenzaListino, lCoddest, dbConn})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      ScriviPrezzo = 0
      If lConto = 0 Then lCoddest = 0
      '-------------------------------
      ' ATTENZIONE: strCodartListino è già il codice articolo che deve essere scritto nei listini
      ' quindi per gli articli a varianti con prezzi comuni o 1. variante è rispettivamente
      ' il root e root+1variante

      '-------------------------------
      'Apre un recordset su listini con i filtri passati
      strSQL = "SELECT lc_progr FROM listini" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND lc_codart = " & CStrSQL(strCodartListino) & _
                  " AND lc_codlavo = " & nCodlavo & _
                  " AND lc_conto = " & lConto & _
                  " AND lc_coddest = " & lCoddest & _
                  " AND lc_codvalu = " & nCodvalu & _
                  " AND lc_codtpro = " & nCodtpro & _
                  " AND lc_listino = " & nListino & _
                  " AND lc_datagg = " & CDataSQL(dtDatAgg) & _
                  " AND lc_daquant = " & CDblSQL(dDaQuant) & _
                  " AND lc_unmis = " & CStrSQL(strUnmis) & _
                  " AND lc_fase = " & nFase & _
                  " AND lc_codcas = ' '"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      If dttTmp.Rows.Count > 0 Then
        ScriviPrezzo = NTSCInt(dttTmp.Rows(0)!lc_progr)
        If bUpdateexistingrk = False Then
          'Se esiste già un listino con la data di aggiornamento inicata
          'e il flag di aggiornamento è -False-
          'allora ritorna il progressivo del listino ma non fa nessun aggiornamento
          dttTmp.Clear()
          Exit Function
        Else
          'Se esiste già un listino con la data di aggiornamento inicata
          'e il flag di aggiornamento è -True-
          'allora aggiorna semplicemente il listino
          dttTmp.Clear()

          strSQL = "UPDATE listini" & _
                  " SET lc_prezzo = " & CDblSQL(dPrezzo) & "," & _
                  " lc_ultagg = " & CDataOraSQL(Now) & "," & _
                  " lc_netto = " & CStrSQL(strNetto) & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND lc_codart = " & CStrSQL(strCodartListino) & _
                  " AND lc_codlavo = " & nCodlavo & _
                  " AND lc_conto = " & lConto & _
                  " AND lc_coddest = " & lCoddest & _
                  " AND lc_codvalu = " & nCodvalu & _
                  " AND lc_codtpro = " & nCodtpro & _
                  " AND lc_listino = " & nListino & _
                  " AND lc_datagg = " & CDataSQL(dtDatAgg) & _
                  " AND lc_daquant = " & CDblSQL(dDaQuant) & _
                  " AND lc_unmis = " & CStrSQL(strUnmis) & _
                  " AND lc_fase = " & nFase & _
                  " AND lc_codcas = ' '"
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'Aggiorna il timestamp su anagra
          If lConto > 0 Then UpdateTimeStampConto(strDitta, lConto, dbConn)
          Exit Function
        End If
      Else
        dttTmp.Clear()

        '-----------------------------------
        'prima di aggiornare tutto verifico se il listino in vigore alla data di aggiornamento è
        'uguale a quello che dovrei inserire: se lo è non aggiorno nulla (il tutto su opz di reg
        If CBool(GetSettingBusDitt(strDitta, "BSMGELAR", "OPZIONI", ".", "SaltaPrezzoUguale", "0", " ", "0")) Then
          strSQL = "SELECT TOP 1 lc_progr, lc_prezzo FROM listini" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND lc_codart = " & CStrSQL(strCodartListino) & _
                  " AND lc_codlavo = " & nCodlavo & _
                  " AND lc_conto = " & lConto & _
                  " AND lc_coddest = " & lCoddest & _
                  " AND lc_codvalu = " & nCodvalu & _
                  " AND lc_codtpro = " & nCodtpro & _
                  " AND lc_listino = " & nListino & _
                  " AND lc_datagg <= " & CDataSQL(dtDatAgg) & _
                  " AND lc_datscad >= " & CDataSQL(dtDatAgg) & _
                  " AND lc_daquant = " & CDblSQL(dDaQuant) & _
                  " AND lc_unmis = " & CStrSQL(strUnmis) & _
                  " AND lc_fase = " & nFase & _
                  " AND lc_codcas = ' '"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count > 0 Then
            If NTSCDec(dttTmp.Rows(0)!lc_prezzo) = NTSCDec(dPrezzo) Then
              ScriviPrezzo = NTSCInt(dttTmp.Rows(0)!lc_progr)
              dttTmp.Clear()
              Exit Function
            End If
          End If
          dttTmp.Clear()
        End If

        'Aggiorna il timestamp su anagra
        If lConto > 0 Then UpdateTimeStampConto(strDitta, lConto, dbConn)

        '------------------------------
        'Prima di inserire il listino controlla se c'è un listino posteriore
        'se esiste mette lc_datscad = al giorno prima del listino posteriore già esistente
        'altrimenti mette lc_datscad = 31/12/2099
        strDataScadenza = ""
        strSQL = "SELECT TOP 1 lc_datagg FROM listini" & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND lc_codart = " & CStrSQL(strCodartListino) & _
          " AND lc_codlavo = " & nCodlavo & _
          " AND lc_conto = " & lConto & _
          " AND lc_coddest = " & lCoddest & _
          " AND lc_codvalu = " & nCodvalu & _
          " AND lc_codtpro = " & nCodtpro & _
          " AND lc_listino = " & nListino & _
          " AND lc_datagg > " & CDataSQL(dtDatAgg) & _
          " AND lc_daquant = " & CDblSQL(dDaQuant) & _
          " AND lc_unmis = " & CStrSQL(strUnmis) & _
          " AND lc_fase = " & nFase & _
          " AND lc_codcas = ' '" & _
          " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro," & _
          " lc_listino, lc_datagg ASC, lc_daquant, lc_unmis, lc_fase"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count > 0 Then
          strDataScadenza = DateAdd("d", -1, NTSCDate(dttTmp.Rows(0)!lc_datagg)).ToShortDateString
        Else
          If strDataScadenzaListino.Trim <> "" Then
            strDataScadenza = strDataScadenzaListino
          Else
            strDataScadenza = IntSetDate("31/12/2099")
          End If
        End If
        'Se trattasi di listino in PROMOZIONE la data di scadenza viene passata a tale metodo
        If nCodtpro > 0 Then strDataScadenza = dtDatscadPromo.ToShortDateString
        dttTmp.Clear()

        '-----------------------------------
        'Inserisce il listino con i parametri passati
        strSQL = "INSERT INTO listini (codditt, lc_codart, lc_codlavo, lc_conto, lc_codvalu," & _
                  " lc_codtpro, lc_listino, lc_datagg, lc_tipo, lc_prezzo, lc_datscad, lc_daquant," & _
                  " lc_aquant, lc_perqta, lc_unmis, lc_note, lc_netto, lc_fase, lc_ultagg, lc_codcas, lc_coddest)" & _
                  " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodartListino) & ", " & _
                  nCodlavo & ", " & lConto & ", " & nCodvalu & ", " & nCodtpro & ", " & nListino & ", " & _
                  CDataSQL(dtDatAgg) & ", " & CStrSQL(strTipo) & ", " & _
                  CDblSQL(dPrezzo) & ", " & CDataSQL(strDataScadenza) & ", " & _
                  CDblSQL(dDaQuant) & ", " & CDblSQL(dAquant) & ", " & _
                  CDblSQL(dPerqta) & ", " & CStrSQL(strUnmis) & ", " & _
                  CStrSQL(strNote) & ", " & CStrSQL(strNetto) & ", " & nFase & ", " & _
                  CDataOraSQL(Now) & ", ' ', " & lCoddest & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '-------------------------------
        'Aggiorna la data di scadenza dell'eventuale listino immediatamente precedente
        'al giorno prima della data di aggiornamento del listino appena inserito
        strDataScadenza = DateAdd("d", -1, NTSCDate(dtDatAgg)).ToShortDateString
        strSQL = "UPDATE listini" & _
                  " SET lc_datscad = " & CDataSQL(strDataScadenza) & "," & _
                  " lc_ultagg = " & CDataOraSQL(Now) & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND lc_codart = " & CStrSQL(strCodartListino) & _
                  " AND lc_codlavo = " & nCodlavo & _
                  " AND lc_conto = " & lConto & _
                  " AND lc_coddest = " & lCoddest & _
                  " AND lc_codvalu = " & nCodvalu & _
                  " AND lc_codtpro = " & nCodtpro & _
                  " AND lc_listino = " & nListino & _
                  " AND lc_datagg < " & CDataSQL(NTSCDate(dtDatAgg)) & _
                  " AND lc_daquant = " & CDblSQL(dDaQuant) & _
                  " AND lc_unmis = " & CStrSQL(strUnmis) & _
                  " AND lc_fase = " & nFase & _
                  " AND lc_datscad >= " & CDataSQL(NTSCDate(dtDatAgg)) & _
                  " AND lc_codcas = ' '"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '----------------------------
        'Prende il progressivo dell'eventuale listino appena inserito da ritornare
        strSQL = "SELECT lc_progr FROM listini" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND lc_codart = " & CStrSQL(strCodartListino) & _
                  " AND lc_codlavo = " & nCodlavo & _
                  " AND lc_conto = " & lConto & _
                  " AND lc_coddest = " & lCoddest & _
                  " AND lc_codvalu = " & nCodvalu & _
                  " AND lc_codtpro = " & nCodtpro & _
                  " AND lc_listino = " & nListino & _
                  " AND lc_datagg = " & CDataSQL(CDate(dtDatAgg)) & _
                  " AND lc_daquant = " & CDblSQL(dDaQuant) & _
                  " AND lc_unmis = " & CStrSQL(strUnmis) & _
                  " AND lc_fase = " & nFase & _
                  " AND lc_codcas = ' '"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count > 0 Then ScriviPrezzo = NTSCInt(dttTmp.Rows(0)!lc_progr)
        dttTmp.Clear()

      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function ScriviSconto(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, _
                                      ByVal lConto As Integer, ByVal nClscan As Integer, ByVal nClscar As Integer, _
                                      ByVal nCodtpro As Integer, ByVal dDaQuant As Decimal, ByVal dAquant As Decimal, _
                                      ByVal dtDatAgg As Date, ByVal dtDatscadPromo As Date, ByVal strUnmis As String, _
                                      ByVal dScont1 As Decimal, ByVal dScont2 As Decimal, ByVal dScont3 As Decimal, _
                                      ByVal dScont4 As Decimal, ByVal dScont5 As Decimal, ByVal dScont6 As Decimal, _
                                      ByVal bUpdateexistingrk As Boolean, Optional ByVal dbConn As DbConnection = Nothing) As Boolean
    Try
      Return ScriviSconto(strDitta, strCodart, nFase, lConto, nClscan, nClscar, nCodtpro, dDaQuant, dAquant, _
                          dtDatAgg, dtDatscadPromo, strUnmis, dScont1, dScont2, dScont3, dScont4, dScont5, dScont6, _
                          bUpdateexistingrk, 0, dbConn)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ScriviSconto(ByVal strDitta As String, ByVal strCodart As String, ByVal nFase As Integer, _
                                          ByVal lConto As Integer, ByVal nClscan As Integer, ByVal nClscar As Integer, _
                                          ByVal nCodtpro As Integer, ByVal dDaQuant As Decimal, ByVal dAquant As Decimal, _
                                          ByVal dtDatAgg As Date, ByVal dtDatscadPromo As Date, ByVal strUnmis As String, _
                                          ByVal dScont1 As Decimal, ByVal dScont2 As Decimal, ByVal dScont3 As Decimal, _
                                          ByVal dScont4 As Decimal, ByVal dScont5 As Decimal, ByVal dScont6 As Decimal, _
                                          ByVal bUpdateexistingrk As Boolean, ByVal lCoddest As Integer, _
                                          Optional ByVal dbConn As DbConnection = Nothing) As Boolean
    '----------------------------------
    'Si occupa di inserire un nuovo record se non esiste la stessa combinazione
    'altrimenti se esiste ma con data validità anteriore fa scadere il record precedente e ne scrive uno nuovo
    'in caso contrario (data aggiornamento identica) sovrascrive

    Dim strSQL As String = ""
    Dim strDataScadenza As String = ""
    Dim dttTmp As New DataTable
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nFase, lConto, nClscan, nClscar, nCodtpro, dDaQuant, dAquant, _
                                              dtDatAgg, dtDatscadPromo, strUnmis, dScont1, dScont2, dScont3, dScont4, dScont5, dScont6, _
                                              bUpdateexistingrk, lCoddest, dbConn})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      nFase = 0 'La CercaSconti non ha come parametro la fase. Non sono gestiti sconti a fasi!
      If lConto = 0 Then lCoddest = 0

      '-------------------------------
      'Apre un recordset su sconti con i filtri passati
      strSQL = "SELECT so_scont1 FROM sconti" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND so_codart = " & CStrSQL(strCodart) & _
                  " AND so_fase = " & nFase & _
                  " AND so_clscan = " & nClscan & _
                  " AND so_clscar = " & nClscar & _
                  " AND so_conto = " & lConto & _
                  " AND so_coddest = " & lCoddest & _
                  " AND so_codtpro = " & nCodtpro & _
                  " AND so_datagg = " & CDataSQL(dtDatAgg) & _
                  " AND so_daquant = " & CDblSQL(dDaQuant)
      '" AND so_unmis = " & CStrSQL(strUnmis) & _

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      If dttTmp.Rows.Count > 0 Then
        If bUpdateexistingrk = False Then
          'Se esiste già un record con la data di aggiornamento indicata
          'e il flag di aggiornamento è -False-
          'allora non fa nessun aggiornamento
          dttTmp.Clear()
          Return True
        Else
          'Se esiste già un record con la data di aggiornamento indicata
          'e il flag di aggiornamento è -True-
          'allora aggiorna semplicemente
          dttTmp.Clear()

          strSQL = "UPDATE sconti" & _
                  " SET so_scont1 = " & CDblSQL(dScont1) & "," & _
                  " so_scont2 = " & CDblSQL(dScont2) & "," & _
                  " so_scont3 = " & CDblSQL(dScont3) & "," & _
                  " so_scont4 = " & CDblSQL(dScont4) & "," & _
                  " so_scont5 = " & CDblSQL(dScont5) & "," & _
                  " so_scont6 = " & CDblSQL(dScont6) & "," & _
                  " so_ultagg = " & CDataOraSQL(Now) & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND so_codart = " & CStrSQL(strCodart) & _
                  " AND so_fase = " & nFase & _
                  " AND so_clscan = " & nClscan & _
                  " AND so_clscar = " & nClscar & _
                  " AND so_conto = " & lConto & _
                  " AND so_coddest = " & lCoddest & _
                  " AND so_codtpro = " & nCodtpro & _
                  " AND so_datagg = " & CDataSQL(dtDatAgg) & _
                  " AND so_daquant = " & CDblSQL(dDaQuant)
          ' " AND so_unmis = " & CStrSQL(strUnmis) & _
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'Aggiorna il timestamp su anagra
          If lConto > 0 Then UpdateTimeStampConto(strDitta, lConto, dbConn)
          Return True
        End If
      Else
        dttTmp.Clear()

        '-----------------------------------
        'prima di aggiornare tutto verifico se il record in vigore alla data di aggiornamento è
        'uguale a quello che dovrei inserire: se lo è non aggiorno nulla
        If CBool(GetSettingBusDitt(strDitta, "BSVECOPA", "OPZIONI", ".", "SaltaScontiUguali", "0", " ", "0")) Then
          strSQL = "SELECT TOP 1 so_scont1, so_scont2, so_scont3, so_scont4, so_scont5, so_scont6 FROM sconti" & _
                    " WHERE codditt = " & CStrSQL(strDitta) & _
                    " AND so_codart = " & CStrSQL(strCodart) & _
                    " AND so_fase = " & nFase & _
                    " AND so_clscan = " & nClscan & _
                    " AND so_clscar = " & nClscar & _
                    " AND so_conto = " & lConto & _
                    " AND so_coddest = " & lCoddest & _
                    " AND so_codtpro = " & nCodtpro & _
                    " AND so_datagg <= " & CDataSQL(dtDatAgg) & _
                    " AND so_datscad >= " & CDataSQL(dtDatAgg) & _
                    " AND so_daquant = " & CDblSQL(dDaQuant)
          ' " AND so_unmis = " & CStrSQL(strUnmis) & _
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count > 0 Then
            If NTSCDec(dttTmp.Rows(0)!so_scont1) = dScont1 And NTSCDec(dttTmp.Rows(0)!so_scont2) = dScont2 _
            And NTSCDec(dttTmp.Rows(0)!so_scont3) = dScont3 And NTSCDec(dttTmp.Rows(0)!so_scont4) = dScont4 _
            And NTSCDec(dttTmp.Rows(0)!so_scont5) = dScont5 And NTSCDec(dttTmp.Rows(0)!so_scont6) = dScont6 Then
              dttTmp.Clear()
              Return True
            End If
          End If
        End If

        'Aggiorna il timestamp su anagra
        If lConto > 0 Then UpdateTimeStampConto(strDitta, lConto, dbConn)

        '------------------------------
        'Prima di inserire il record controlla se c'è un record posteriore
        'se esiste mette so_datscad = al giorno prima dello sconto posteriore già esistente
        'altrimenti mette so_datscad = 31/12/2099
        strDataScadenza = ""
        strSQL = "SELECT TOP 1 so_datagg FROM sconti" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND so_codart = " & CStrSQL(strCodart) & _
                  " AND so_fase = " & nFase & _
                  " AND so_clscan = " & nClscan & _
                  " AND so_clscar = " & nClscar & _
                  " AND so_conto = " & lConto & _
                  " AND so_coddest = " & lCoddest & _
                  " AND so_codtpro = " & nCodtpro & _
                  " AND so_datagg > " & CDataSQL(dtDatAgg) & _
                  " AND so_daquant = " & CDblSQL(dDaQuant) & _
                  " ORDER BY codditt, so_codart, so_conto, so_clscan, so_clscar, so_codtpro," & _
                  " so_datagg ASC, so_daquant, so_unmis, so_fase"
        '   " AND so_unmis = " & CStrSQL(strUnmis) & _
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count > 0 Then
          strDataScadenza = DateAdd("d", -1, NTSCDate(dttTmp.Rows(0)!so_datagg)).ToShortDateString
        Else
          strDataScadenza = IntSetDate("31/12/2099")
        End If
        'Se trattasi di sconto in PROMOZIONE la data di scadenza viene passata a tale metodo
        If nCodtpro > 0 Then strDataScadenza = dtDatscadPromo.ToShortDateString
        dttTmp.Clear()

        '-----------------------------------
        'Inserisce lo sconto con i parametri passati
        strSQL = "INSERT INTO sconti (codditt, so_codart, so_conto, so_clscan, so_clscar, " & _
                 " so_codtpro, so_scont1, so_scont2, so_scont3, so_scont4, so_scont5, so_scont6, " & _
                 " so_datagg, so_datscad, so_daquant, so_aquant, so_unmis, so_fase, so_ultagg, so_coddest)" & _
                 " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodart) & ", " & _
                 lConto & ", " & nClscan & ", " & nClscar & ", " & nCodtpro & ", " & _
                 CDblSQL(dScont1) & ", " & CDblSQL(dScont2) & ", " & CDblSQL(dScont3) & ", " & _
                 CDblSQL(dScont4) & ", " & CDblSQL(dScont5) & ", " & CDblSQL(dScont6) & ", " & _
                 CDataSQL(dtDatAgg) & ", " & CDataSQL(strDataScadenza) & ", " & _
                 CDblSQL(dDaQuant) & ", " & CDblSQL(dAquant) & ", " & _
                 CStrSQL(strUnmis) & ", " & nFase & ", " & CDataOraSQL(Now) & ", " & lCoddest & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '-------------------------------
        'Aggiorna la data di scadenza dell'eventuale sconto immediatamente precedente
        'al giorno prima della data di aggiornamento dello sconto appena inserito
        strDataScadenza = DateAdd("d", -1, NTSCDate(dtDatAgg)).ToShortDateString
        strSQL = "UPDATE sconti" & _
                  " SET so_datscad = " & CDataSQL(strDataScadenza) & "," & _
                  " so_ultagg = " & CDataOraSQL(Now) & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND so_codart = " & CStrSQL(strCodart) & _
                  " AND so_fase = " & nFase & _
                  " AND so_clscan = " & nClscan & _
                  " AND so_clscar = " & nClscar & _
                  " AND so_conto = " & lConto & _
                  " AND so_coddest = " & lCoddest & _
                  " AND so_codtpro = " & nCodtpro & _
                  " AND so_datagg < " & CDataSQL(NTSCDate(dtDatAgg)) & _
                  " AND so_daquant = " & CDblSQL(dDaQuant) & _
                  " AND so_datscad >= " & CDataSQL(NTSCDate(dtDatAgg))
        '   " AND so_unmis = " & CStrSQL(strUnmis) & _
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function ScriviProvvig(ByVal strDitta As String, ByVal strCodart As String, ByVal lCodcage As Integer, _
                                       ByVal lConto As Integer, ByVal nClpran As Integer, ByVal nClprar As Integer, _
                                       ByVal nCodtpro As Integer, ByVal dtDatAgg As Date, ByVal dtDatscadPromo As Date, _
                                       ByVal dProvv As Decimal, ByVal dVprovv As Decimal, ByVal strUnmis As String, _
                                       ByVal bUpdateexistingrk As Boolean, Optional ByVal dbConn As DbConnection = Nothing) As Boolean
    Try
      'obsoleta
      Return ScriviProvvig(strDitta, strCodart, lCodcage, lConto, nClpran, nClprar, nCodtpro, dtDatAgg, dtDatscadPromo, _
                           dProvv, dVprovv, strUnmis, bUpdateexistingrk, 0, dbConn)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ScriviProvvig(ByVal strDitta As String, ByVal strCodart As String, ByVal lCodcage As Integer, _
                                        ByVal lConto As Integer, ByVal nClpran As Integer, ByVal nClprar As Integer, _
                                        ByVal nCodtpro As Integer, ByVal dtDatAgg As Date, ByVal dtDatscadPromo As Date, _
                                        ByVal dProvv As Decimal, ByVal dVprovv As Decimal, ByVal strUnmis As String, _
                                        ByVal bUpdateexistingrk As Boolean, ByVal lCoddest As Integer, Optional ByVal dbConn As DbConnection = Nothing) As Boolean
    '----------------------------------
    'Si occupa di inserire un nuovo record se non esiste la stessa combinazione
    'altrimenti se esiste ma con data validità anteriore fa scadere il record precedente e ne scrive uno nuovo
    'in caso contrario (data aggiornamento identica) sovrascrive

    Dim strSQL As String = ""
    Dim strDataScadenza As String = ""
    Dim dttTmp As New DataTable
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, lCodcage, lConto, nClpran, nClprar, nCodtpro, dtDatAgg, dtDatscadPromo, _
                           dProvv, dVprovv, strUnmis, bUpdateexistingrk, lCoddest, dbConn})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      If lConto = 0 Then lCoddest = 0
      '-------------------------------
      ' ATTENZIONE: strCodart è già il codice articolo che deve essere scritto nei listini
      ' quindi per gli articli a varianti con prezzi comuni o 1. variante è rispettivamente
      ' il root e root+1variante

      '-------------------------------
      'Apre un recordset su sconti con i filtri passati
      strSQL = "SELECT per_provv FROM perprov" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND per_codart = " & CStrSQL(strCodart) & _
                " AND per_codcage = " & lCodcage & _
                " AND per_clpran = " & nClpran & _
                " AND per_clprar = " & nClprar & _
                " AND per_conto = " & lConto & _
                " AND per_coddest = " & lCoddest & _
                " AND per_codtpro = " & nCodtpro & _
                " AND per_datagg = " & CDataSQL(dtDatAgg)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      If dttTmp.Rows.Count > 0 Then
        If bUpdateexistingrk = False Then
          'Se esiste già un record con la data di aggiornamento indicata
          'e il flag di aggiornamento è -False-
          'allora non fa nessun aggiornamento
          dttTmp.Clear()
          Return True
        Else
          'Se esiste già un record con la data di aggiornamento indicata
          'e il flag di aggiornamento è -True-
          'allora aggiorna semplicemente
          dttTmp.Clear()

          strSQL = "UPDATE perprov" & _
                  " SET per_provv = " & CDblSQL(dProvv) & "," & _
                  " per_vprovv = " & CDblSQL(dVprovv) & "," & _
                  " per_ultagg = " & CDataOraSQL(Now) & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND per_codart = " & CStrSQL(strCodart) & _
                  " AND per_codcage = " & lCodcage & _
                  " AND per_clpran = " & nClpran & _
                  " AND per_clprar = " & nClprar & _
                  " AND per_conto = " & lConto & _
                  " AND per_coddest = " & lCoddest & _
                  " AND per_codtpro = " & nCodtpro & _
                  " AND per_datagg = " & CDataSQL(dtDatAgg)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

          'Aggiorna il timestamp su anagra
          If lConto > 0 Then UpdateTimeStampConto(strDitta, lConto, dbConn)
          Return True
        End If
      Else
        dttTmp.Clear()

        '-----------------------------------
        'prima di aggiornare tutto verifico se il record in vigore alla data di aggiornamento è
        'uguale a quello che dovrei inserire: se lo è non aggiorno nulla
        If CBool(GetSettingBusDitt(strDitta, "BSVECOPA", "OPZIONI", ".", "SaltaProvvUguali", "0", " ", "0")) Then
          strSQL = "SELECT TOP 1 per_provv, per_vprovv FROM perprov" & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND per_codart = " & CStrSQL(strCodart) & _
                  " AND per_codcage = " & lCodcage & _
                  " AND per_clpran = " & nClpran & _
                  " AND per_clprar = " & nClprar & _
                  " AND per_conto = " & lConto & _
                  " AND per_coddest = " & lCoddest & _
                  " AND per_codtpro = " & nCodtpro & _
                  " AND per_datagg <= " & CDataSQL(dtDatAgg) & _
                  " AND per_datscad >= " & CDataSQL(dtDatAgg)
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
          If dttTmp.Rows.Count > 0 Then
            If NTSCDec(dttTmp.Rows(0)!per_provv) = dProvv And NTSCDec(dttTmp.Rows(0)!per_vprovv) = dVprovv Then
              dttTmp.Clear()
              Return True
            End If
          End If
          dttTmp.Clear()
        End If

        'Aggiorna il timestamp su anagra
        If lConto > 0 Then UpdateTimeStampConto(strDitta, lConto, dbConn)

        '------------------------------
        'Prima di inserire il record controlla se c'è un record posteriore
        'se esiste mette lc_datscad = al giorno prima del listino posteriore già esistente
        'altrimenti mette lc_datscad = 31/12/2099
        strDataScadenza = ""
        strSQL = "SELECT TOP 1 per_datagg FROM perprov" & _
                " WHERE codditt = " & CStrSQL(strDitta) & _
                " AND per_codart = " & CStrSQL(strCodart) & _
                " AND per_codcage = " & lCodcage & _
                " AND per_clpran = " & nClpran & _
                " AND per_clprar = " & nClprar & _
                " AND per_conto = " & lConto & _
                " AND per_coddest = " & lCoddest & _
                " AND per_codtpro = " & nCodtpro & _
                " AND per_datagg > " & CDataSQL(dtDatAgg) & _
                " ORDER BY codditt, per_codcage, per_codart, per_conto, per_coddest, per_clpran, per_clprar, per_codtpro," & _
                " per_datagg ASC"
        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If dttTmp.Rows.Count > 0 Then
          strDataScadenza = DateAdd("d", -1, NTSCDate(dttTmp.Rows(0)!per_datagg)).ToShortDateString
        Else
          strDataScadenza = IntSetDate("31/12/2099")
        End If
        'Se trattasi di listino in PROMOZIONE la data di scadenza viene passata a tale metodo
        If nCodtpro > 0 Then strDataScadenza = dtDatscadPromo.ToShortDateString
        dttTmp.Clear()

        '-----------------------------------
        'Inserisce il listino con i parametri passati
        strSQL = "INSERT INTO perprov (codditt, per_codcage, per_codart, per_conto, per_clpran, per_clprar, " & _
                 " per_provv, per_datagg, per_datscad, per_codtpro, per_vprovv, per_unmis, per_ultagg, per_coddest)" & _
                 " VALUES (" & CStrSQL(strDitta) & ", " & lCodcage & ", " & CStrSQL(strCodart) & ", " & _
                 lConto & ", " & nClpran & ", " & nClprar & ", " & CDblSQL(dProvv) & ", " & _
                 CDataSQL(dtDatAgg) & ", " & CDataSQL(strDataScadenza) & ", " & _
                 nCodtpro & ", " & CDblSQL(dVprovv) & ", " & CStrSQL(strUnmis) & ", " & CDataOraSQL(Now) & "," & lCoddest & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        '-------------------------------
        'Aggiorna la data di scadenza dell'eventuale listino immediatamente precedente
        'al giorno prima della data di aggiornamento del listino appena inserito
        strDataScadenza = DateAdd("d", -1, NTSCDate(dtDatAgg)).ToShortDateString
        strSQL = "UPDATE perprov" & _
                  " SET per_datscad = " & CDataSQL(strDataScadenza) & "," & _
                  " per_ultagg = " & CDataOraSQL(Now) & _
                  " WHERE codditt = " & CStrSQL(strDitta) & _
                  " AND per_codart = " & CStrSQL(strCodart) & _
                  " AND per_codcage = " & lCodcage & _
                  " AND per_clpran = " & nClpran & _
                  " AND per_clprar = " & nClprar & _
                  " AND per_conto = " & lConto & _
                  " AND per_coddest = " & lCoddest & _
                  " AND per_codtpro = " & nCodtpro & _
                  " AND per_datagg < " & CDataSQL(NTSCDate(dtDatAgg)) & _
                  " AND per_datscad >= " & CDataSQL(NTSCDate(dtDatAgg))
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function UpdateTimeStampConto(ByVal strDitta As String, ByVal lconto As Integer, _
                                          Optional ByVal dbConn As DbConnection = Nothing) As Boolean
    Dim strSQL As String = ""
    Try
      'Aggiorna il timestamp di anagra (an_ultagg) con la data di sistema
      strSQL = "UPDATE anagra SET an_ultagg = GETDATE() " & _
                "WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & lconto
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "Per creare il file/tabella di picking"
  Public Overridable Function CreaPickingLeggiNota(ByVal strDitta As String, ByVal strTipork As String, _
                                      ByVal nAnno As Integer, ByVal strSerie As String, _
                                      ByVal lNumdoc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT testprb.*, tb_desvett " & _
              " FROM testprb LEFT JOIN tabvett ON testprb.codditt = tabvett.codditt AND testprb.tm_vettor = tabvett.tb_codvett" & _
              " WHERE testprb.codditt = " & CStrSQL(strDitta) & _
              " AND tm_tipork = " & CStrSQL(strTipork) & _
              " AND tm_anno = " & nAnno & _
              " AND tm_serie = " & CStrSQL(strSerie) & _
              " AND tm_numdoc = " & lNumdoc
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaPickingLeggiRigheNota(ByVal strDitta As String, ByVal strTipork As String, _
                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                    ByVal lNumdoc As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movprb.*, movmagtc.*, " & _
              " ar_gestmatr, ar_geslotti, ar_unmis2, ar_conver, ar_confez2, ar_qtacon2, ar_gesubic, ar_ubicaz, ar_codtagl " & _
              " FROM movprb INNER JOIN artico ON movprb.codditt = artico.codditt AND movprb.mm_codart = artico.ar_codart" & _
              " LEFT JOIN movmagtc ON movprb.codditt = movmagtc.codditt AND movprb.mm_tipork = movmagtc.mm_tipork AND movprb.mm_anno = movmagtc.mm_anno " & _
              " AND movprb.mm_serie = movmagtc.mm_serie AND movprb.mm_numdoc = movmagtc.mm_numdoc AND movprb.mm_riga = movmagtc.mm_riga " & _
              " WHERE movprb.codditt = " & CStrSQL(strDitta) & _
              " AND movprb.mm_tipork = " & CStrSQL(strTipork) & _
              " AND movprb.mm_anno = " & nAnno & _
              " AND movprb.mm_serie = " & CStrSQL(strSerie) & _
              " AND movprb.mm_numdoc = " & lNumdoc & _
              " ORDER BY movprb.codditt, movprb.mm_tipork, movprb.mm_anno, movprb.mm_serie," & _
              " movprb.mm_numdoc, movprb.mm_riga"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaPickingGetFirstBarcode(ByVal strDitta As String, ByVal strCodart As String, _
                                  ByVal strUnmis As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 bc_code FROM barcode" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND bc_codart = " & CStrSQL(strCodart)
      If strUnmis <> "" Then strSQL += " AND bc_unmis = " & CStrSQL(strUnmis)
      strSQL += " ORDER BY bc_datins DESC"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaPickingGetMatricole(ByVal strDitta As String, ByVal strTipork As String, _
                                    ByVal nAnno As Integer, ByVal strSerie As String, _
                                    ByVal lNumdoc As Integer, ByVal lRiga As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT movmatr.* FROM movmatr" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mma_tipork = " & CStrSQL(strTipork) & _
               " AND mma_anno = " & nAnno & _
               " AND mma_serie = " & CStrSQL(strSerie) & _
               " AND mma_numdoc = " & lNumdoc & _
               " AND mma_riga = " & lRiga
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaPickingAggIdpickNota(ByVal strDitta As String, ByVal strTipork As String, _
                                      ByVal nAnno As Integer, ByVal strSerie As String, _
                                      ByVal lNumdoc As Integer, ByVal lIdpick As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE testprb" & _
              " SET tm_idpick = " & lIdpick & "," & _
              " tm_vistato = 'T'" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND tm_tipork = " & CStrSQL(strTipork) & _
              " AND tm_anno = " & nAnno & _
              " AND tm_serie = " & CStrSQL(strSerie) & _
              " AND tm_numdoc = " & lNumdoc
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaPickingGetAllBarcode(ByVal strDitta As String, ByVal lIdpick As Integer, ByRef dttOut As DataTable) As Boolean
    'prelevo tutti i barcode degli articoli contenuti nella nota di prelievo con idpick uguale a quello passato
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT codditt, bc_code, bc_codart, bc_fase, bc_unmis, bc_quant FROM barcode" & _
             " WHERE codditt = " & CStrSQL(strDitta) & _
             " AND bc_codart IN (SELECT mm_codart FROM movprb INNER JOIN testprb ON " & _
             " testprb.codditt = movprb.codditt AND testprb.tm_tipork = movprb.mm_tipork AND testprb.tm_anno = movprb.mm_anno AND " & _
             " testprb.tm_serie = movprb.mm_serie AND testprb.tm_numdoc = movprb.mm_numdoc " & _
             " WHERE testprb.codditt = " & CStrSQL(strDitta) & _
             " AND tm_idpick = " & lIdpick & ")" & _
             " UNION " & _
             " SELECT artico.codditt, ar_codart, ar_codart, ar_ultfase, ar_unmis, 1 " & _
             " FROM (movprb INNER JOIN testprb ON " & _
             " testprb.codditt = movprb.codditt AND testprb.tm_tipork = movprb.mm_tipork AND testprb.tm_anno = movprb.mm_anno AND  " & _
             " testprb.tm_serie = movprb.mm_serie AND testprb.tm_numdoc = movprb.mm_numdoc) " & _
             " INNER JOIN artico ON artico.codditt = movprb.codditt AND artico.ar_codart = movprb.mm_codart " & _
             " WHERE testprb.codditt = " & CStrSQL(strDitta) & _
             " AND tm_idpick = " & lIdpick & _
             " GROUP BY artico.codditt, ar_codart, ar_codart, ar_ultfase, ar_unmis"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  'Per il nuovo picking
  Public Overridable Function CreaPickingControllaEsiste(ByVal strDitta As String, ByVal lIdPick As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM testpic WHERE codditt = " & CStrSQL(strDitta) & " AND tp_idpick = " & lIdPick

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaPickingCancellaVecchio(ByVal strDitta As String, ByVal lIdPick As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dbConn As DbConnection = Nothing
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      strSQL = "DELETE FROM testpic WHERE codditt = " & CStrSQL(strDitta) & " AND tp_idpick = " & lIdPick

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      strSQL = "DELETE FROM movpic WHERE codditt = " & CStrSQL(strDitta) & " AND mp_idpick = " & lIdPick

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
  Public Overridable Function CreaPickingSalvaDati(ByVal strDitta As String, ByVal lIdPick As Integer, ByVal strNome As String, ByVal dttTestPic As DataTable, _
                                                   ByVal dttMovPic As DataTable) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lRigaPick, i As Integer
    Dim dbConn As DbConnection = Nothing
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      ' trovo il valore da mettere nella riga di testpic che era stato lasciato in sospeso
      strSQL = "SELECT MAX(tp_rigapick) as xx_riga FROM testpic WHERE codditt = " & CStrSQL(strDitta) & " AND tp_idpick = " & lIdPick

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      If dttTmp.Rows.Count = 0 Then
        lRigaPick = 1
      Else
        lRigaPick = NTSCInt(dttTmp.Rows(0)!xx_riga) + 1
      End If

      ' Verifico se esiste già il tabpick altrimenti lo creo
      strSQL = "SELECT * FROM tabpick WHERE codditt = " & CStrSQL(strDitta) & " AND tb_codpick = " & lIdPick

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      If dttTmp.Rows.Count = 0 Then
        'Scrive tabpick se manca
        strSQL = "INSERT INTO tabpick (codditt, tb_codpick, tb_despick, tb_chiuso, tb_opnomest) " & _
                 "VALUES (" & CStrSQL(strDitta) & ", " & lIdPick & ", " & CStrSQL(" ") & ", " & CStrSQL("G") & ", " & CStrSQL(strNome) & ")"
        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      'scrive i dati nella testata
      dttTestPic.Rows(0)!tp_rigapick = lRigaPick
      strSQL = "INSERT INTO testpic " & GetQueryInsertField(dttTestPic, "tp_") & _
               " VALUES " & GetQueryInsertValue(dttTestPic, dttTestPic.Rows(0), "tp_")

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      'scrive le righe del corpo
      For i = 0 To dttMovPic.Rows.Count - 1
        dttMovPic.Rows(i)!mp_rigapick = lRigaPick
        strSQL = "INSERT INTO movpic " & GetQueryInsertField(dttMovPic, "mp_") & _
                 " VALUES " & GetQueryInsertValue(dttMovPic, dttMovPic.Rows(i), "mp_")

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Next

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
  Public Overridable Function CraePickingGroupage(ByVal strDitta As String, ByVal lIdPick As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE testpic SET tp_tiporkp = " & CStrSQL("G") & _
               " WHERE codditt = " & CStrSQL(strDitta) & " AND tp_idpick = " & lIdPick

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

  Public Overridable Function LeggiCampiPerHlvl(ByVal strTabella As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      Select Case oApp.User.Lingua
        Case 0
          strSQL = "SELECT cb_nomcampo, substring(cb_descampo, len(cb_nomtab) + 4, len(cb_descampo) - (len(cb_nomtab) + 3)) as xx_nome, cb_tipocampo, cb_size, '' as xx_valoreda, '' as xx_valorea FROM CAMPI WHERE cb_nomtab = " & CStrSQL(strTabella) & " AND cb_tipocampo <> 11 ORDER BY xx_nome"
        Case Else ' legge le descrizioni da CAMPLIN
          strSQL = "SELECT CAMPI.cb_nomcampo," & _
                  " CASE WHEN CAMPLIN.cb_descampo IS NOT NULL AND CAMPLIN.cb_descampo <> ''" & _
                  " THEN  CAMPLIN.cb_descampo  " & _
                  " ELSE substring(CAMPI.cb_descampo, len(cb_nomtab) + 4," & _
                  " len(CAMPI.cb_descampo) - (len(cb_nomtab) + 3)) " & _
                  " END AS xx_nome," & _
                  " cb_tipocampo, cb_size, '' as xx_valoreda, '' as xx_valorea" & _
                  " FROM CAMPI" & _
                  " LEFT JOIN CAMPLIN ON CAMPI.cb_nomcampo = CAMPLIN.cb_nomcampo" & _
                  " AND CAMPLIN.cb_codling = " & oApp.User.Lingua & _
                  " WHERE cb_nomtab = 'artico' AND cb_tipocampo <> 11 ORDER BY xx_nome "
      End Select

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)
      Return True

    Catch ex As Exception
      Return False
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function


  Public Overridable Function CaricaCalCentro(ByVal strDitta As String, ByVal nCodCentro As Integer, _
                                              ByVal lNumber As Integer, ByVal dtData As Date, _
                                              ByRef dtTemp As Date) As Boolean
    'utilizzata da belbmenu.DateAddEx
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim dttCalop As New DataTable
    Dim dttCalend As New DataTable
    Dim nCodCal As Integer
    Dim nCodCalColl As Integer
    Dim bBase As Boolean
    Dim i As Integer = 0
    Dim j As Integer = 0
    Dim k As Integer = 0
    Try
      strSQL = "SELECT tb_codcale FROM tabcent WHERE tb_codcent = " & nCodCentro & _
               " AND codditt = " & CStrSQL(strDitta) & " ORDER BY tb_codcale"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        nCodCal = 1
      Else
        nCodCal = NTSCInt(dttTmp.Rows(0)!tb_codcale)
      End If
      dttTmp.Clear()

Ripeti:
      strSQL = "SELECT * FROM calop WHERE tb_codcal = " & nCodCal & _
               " AND codditt = " & CStrSQL(strDitta) & " AND tb_anno <= " & Year(dtData) & " ORDER BY tb_anno DESC"
      dttCalop = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      'Se il calendario non esiste esco dalla funzione
      If dttCalop.Rows.Count = 0 And nCodCal <> 1 Then
        nCodCal = 1
        dttCalop.Clear()
        GoTo Ripeti
      End If
      If dttCalop.Rows.Count = 0 Then Return False

      If dttCalop.Rows(0)!tb_calbase.ToString = "N" Then
        bBase = False
      Else
        bBase = True
      End If
      If lNumber < 0 Then j = -1
      If lNumber = 0 Then j = 0
      If lNumber > 0 Then j = 1

      dtTemp = dtData
      For i = 1 To Math.Abs(lNumber)
Riparti:
        nCodCal = CInt(dttCalop.Rows(0)!tb_codcal)
        If Not bBase Then
          nCodCalColl = CInt(dttCalop.Rows(0)!tb_calstdcoll)
        Else
          nCodCalColl = 0
        End If
        dtTemp = DateAdd("d", j, dtTemp)
        If Not bBase Then
          strSQL = "SELECT ca_tipogio FROM calend" & _
            " WHERE ca_codcal = " & nCodCal & _
            " AND ca_mese = " & Month(dtTemp) & _
            " AND ca_giorno = " & dtTemp.Day & _
            " AND ca_anno = " & NTSCInt(dttCalop.Rows(0)!tb_anno)
          strSQL = strSQL & " and codditt = " & CStrSQL(strDitta)
          dttCalend = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

          If dttCalend.Rows.Count > 0 Then
            If dttCalend.Rows(0)!ca_tipogio.ToString = "F" Then
              dttCalend.Clear()
              GoTo Riparti
            End If
          End If
          nCodCal = nCodCalColl
          dttCalend.Clear()
        End If
        strSQL = "SELECT ca_tipogio FROM calend" & _
          " WHERE ca_codcal = " & nCodCal & _
          " AND ca_mese = " & Month(dtTemp) & _
          " AND ca_giorno = " & dtTemp.Day & _
          " AND ca_anno = " & Year(dtTemp)
        strSQL = strSQL & " and codditt = " & CStrSQL(strDitta)
        dttCalend = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

        If dttCalend.Rows.Count > 0 Then
          If dttCalend.Rows(0)!ca_tipogio.ToString = "F" Then
            dttCalend.Clear()
            GoTo Riparti
          End If
        End If
        dttCalend.Clear()
        k = Weekday(dtTemp)
        k = k - 1
        If k = 0 Then k = 7
        If dttCalop.Rows(0)("tb_giorno" & k).ToString = "F" Then GoTo Riparti
      Next i

      Return True
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try

  End Function


#Region "Esplosione distinta base per documenti"
  Public Overridable Function TrattaDbDoc_TrovaFiglieLav(ByVal strDitta As String, ByVal strCodart As String, _
                                          ByVal strGesvar As String, ByVal dtDatval As Date, _
                                          ByVal strCodartRoot As String, ByVal strTipoArtConf As String, _
                                          ByVal bUsaFiltroPadre As Boolean, ByVal bUsaFiltroCommessa As Boolean, _
                                          ByVal lCodClie As Integer, ByVal lCommeca As Integer, _
                                          ByVal strDescrTagliaPadr As String, ByVal nIndiceTagliaPadr As Integer, _
                                          ByRef dttMovdis As DataTable, ByRef dttCicli As DataTable) As Boolean
    Try
      If Not TrattaDb_TrovaFigli(strDitta, dttMovdis, strTipoArtConf, strCodart, _
                                 strCodartRoot, strGesvar, False, _
                                 bUsaFiltroPadre, " ", dtDatval, 0, True, lCodClie, _
                                 0, bUsaFiltroCommessa, lCommeca, strDescrTagliaPadr, _
                                 nIndiceTagliaPadr, False, 0, True) Then Return False
      Return TrattaDb_TrovaLavEx(strDitta, dttCicli, 0, strTipoArtConf, strCodart, _
                               strCodartRoot, strGesvar, False, _
                               bUsaFiltroPadre, " ", dtDatval, lCodClie, _
                               bUsaFiltroCommessa, lCommeca, False, 0, True)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrattaDbDocFasi_TrovaFiglieLav(ByVal strDitta As String, ByVal strCodart As String, _
                                          ByVal nFase As Integer, ByVal strGesvar As String, ByVal dtDatval As Date, _
                                          ByVal strCodartRoot As String, ByVal strTipoArtConf As String, _
                                          ByVal bUsaFiltroPadre As Boolean, ByVal bUsaFiltroCommessa As Boolean, _
                                          ByVal lCodClie As Integer, ByVal lCommeca As Integer, _
                                          ByVal strDescrTagliaPadr As String, ByVal nIndiceTagliaPadr As Integer, _
                                          ByRef dttMovdis As DataTable, ByRef dttCicli As DataTable) As Boolean
    Try
      If Not TrattaDb_TrovaFigli(strDitta, dttMovdis, strTipoArtConf, strCodart, _
                                 strCodartRoot, strGesvar, False, _
                                 bUsaFiltroPadre, " ", dtDatval, 0, True, lCodClie, _
                                 0, bUsaFiltroCommessa, lCommeca, strDescrTagliaPadr, _
                                 nIndiceTagliaPadr, True, nFase, True) Then Return False
      Return TrattaDb_TrovaLavEx(strDitta, dttCicli, 0, strTipoArtConf, strCodart, _
                               strCodartRoot, strGesvar, False, _
                               bUsaFiltroPadre, " ", dtDatval, lCodClie, _
                               bUsaFiltroCommessa, lCommeca, True, nFase, True)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrattaDb_TrovaFigli(ByVal strDitta As String, ByRef dttMovdis As DataTable, _
                                                 ByVal strTipoArtConf As String, ByVal strCodart As String, _
                                                 ByVal strCodRoot As String, ByVal strGesvarPadr As String, _
                                                 ByVal bNoFiltroDataVal As Boolean, ByVal bUsaFiltroPadre As Boolean, _
                                                 ByVal strTipoRoot As String, ByVal dtDatValConv As Date, _
                                                 ByVal nSortTipoFiglio As Integer, ByVal bEscludiFornitoDaTerz As Boolean, _
                                                 ByVal lCodclie As Integer, ByVal lLotto As Integer, _
                                                 ByVal bUsaFiltroCommessa As Boolean, ByVal lCommeca As Integer, _
                                                 ByVal strDescrTagliaPadr As String, ByVal nIndiceTagliaPadr As Integer, _
                                                 ByVal bGestFase As Boolean, ByVal nFase As Integer, _
                                                 Optional ByVal bDbDoc As Boolean = False) As Boolean
    Dim strSQL As String = ""
    Dim nPrecFase As Integer ' PER GESTIONE A FASI
    Dim dttTmp As New DataTable
    Try

      '---------------------------------------------------------
      'ANALISI FIGLI
      '---------------------------------------------------------

      If strGesvarPadr = "K" Then

        '---------------------------------------------------------
        ' PER GESTIONE A FASI
        If bGestFase Then

          nPrecFase = -1
          'Prima di tutto individua qual'è la fase precedente rispetto a nFaseTerz
          If bDbDoc Then
            strSQL = " SELECT TOP 1 cicli.dd_fase" & _
                     " FROM compchi INNER JOIN ((distbas INNER JOIN artico ON distbas.codditt = artico.codditt AND distbas.db_coddb = artico.ar_coddb) " & _
                     " INNER JOIN (ciclichi INNER JOIN ((cicli INNER JOIN tabcent ON cicli.codditt = tabcent.codditt AND cicli.dd_codcent = tabcent.tb_codcent) " & _
                     " LEFT JOIN tabcove ON tabcent.codditt = tabcove.codditt AND tabcent.tb_ccodcontr = tabcove.tb_codcove) ON (ciclichi.codditt = cicli.codditt) AND (ciclichi.ddc_coddb = cicli.dd_coddb) AND (ciclichi.ddc_riga = cicli.dd_riga)) ON distbas.codditt = cicli.codditt AND distbas.db_coddb = cicli.dd_coddb) ON (compchi.codditt = ciclichi.codditt) AND (compchi.cch_codchia = ciclichi.ddc_codchia) AND (compchi.cch_valchia = ciclichi.ddc_valchia)" & _
                     " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                     " AND ar_codart = " & CStrSQL(strCodart) & _
                     " AND cch_codart = Left(articoPadre.ar_codart, Len(articoPadre.ar_codart) - 8) + '" & strTipoArtConf & "'  + Right(articoPadre.ar_codart, 7)" & _
                     " AND cicli.dd_fase < " & nFase
          Else
            strSQL = " SELECT TOP 1 cicli.dd_fase" & _
                     " FROM compchi INNER JOIN ((distbas INNER JOIN artico ON distbas.codditt = artico.codditt AND distbas.db_coddb = artico.ar_coddb) " & _
                     " INNER JOIN (ciclichi INNER JOIN ((cicli INNER JOIN tabcent ON cicli.codditt = tabcent.codditt AND cicli.dd_codcent = tabcent.tb_codcent) " & _
                     " LEFT JOIN tabcove ON tabcent.codditt = tabcove.codditt AND tabcent.tb_ccodcontr = tabcove.tb_codcove) ON (ciclichi.codditt = cicli.codditt) AND (ciclichi.ddc_coddb = cicli.dd_coddb) AND (ciclichi.ddc_riga = cicli.dd_riga)) ON distbas.codditt = cicli.codditt AND distbas.db_coddb = cicli.dd_coddb) ON (compchi.codditt = ciclichi.codditt) AND (compchi.cch_codchia = ciclichi.ddc_codchia) AND (compchi.cch_valchia = ciclichi.ddc_valchia)" & _
                     " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                     " AND ar_codart = " & CStrSQL(strCodart) & _
                     " AND cch_codart = " & CStrSQL(strCodRoot) & _
                     " AND cicli.dd_fase < " & nFase
          End If
          If Not bNoFiltroDataVal Then
            strSQL &= " AND cicli.dd_dtinval <=" & CDataSQL(dtDatValConv) & _
                      " AND cicli.dd_dtfival >=" & CDataSQL(dtDatValConv)
          End If
          If bUsaFiltroPadre Then
            If (strTipoRoot = " " Or strTipoRoot = "O") Then
              If oApp.Db.SQLUnicode Then
                strSQL &= " AND cast(" & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) LIKE cicli.dd_filtart"
                strSQL &= " AND cast(" & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) LIKE cicli.dd_filtart2"
              Else
                strSQL &= " AND " & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " LIKE cicli.dd_filtart"
                strSQL &= " AND " & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " LIKE cicli.dd_filtart2"
              End If
            End If
          End If
          strSQL &= " ORDER BY cicli.dd_fase DESC"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then nPrecFase = NTSCInt(dttTmp.Rows(0)!dd_fase)
          dttTmp.Clear()
        End If
        '---------------------------------------------------------

        'Se risulta da configuratore la query è diversa
        If bDbDoc Then
          strSQL = " SELECT distbas.db_fascialiv, movdis.md_coddb, movdis.md_riga," & _
                   " (CASE WHEN articoFiglio.ar_tipoopz= 'C' THEN '" & Left(strCodRoot, Len(strCodRoot) - 8) & "' + articoFiglio.ar_tipo + '" & Right(strCodRoot, 7) & "' ELSE movdis.md_codfigli END) as md_codfigli," & _
                   " movdis.md_unmis, movdis.md_quant, movdis.md_ump, movdis.md_quantump," & _
                   " movdis.md_tipofigl, movdis.md_sfrido, movdis.md_sottopr, movdis.md_perpz," & _
                   " movdis.md_note, movdis.md_formula, movdis.md_dtinval, movdis.md_dtfival," & _
                   " movdis.md_proven, movdis.md_codmaga, movdis.md_ultmaga, movdis.md_commeca," & _
                   " movdis.md_filtart, movdis.md_filtart2, movdis.md_perpian, movdis.md_perpian2," & _
                   " movdis.md_ritardo, movdis.md_faseiniz, movdis.md_coclie, movdis.md_lottoin," & _
                   " movdis.md_lottofi, movdis.md_misura1, movdis.md_misura2, movdis.md_misura3," & _
                   " movdis.md_interr, movdis.md_fasefigli, articoPadre.ar_tipoopz AS TipoOpzPadre," & _
                   " articoPadre.ar_rrfence AS RRFence, articoPadre.ar_fcorrlt AS Fcorrlt," & _
                   " articoFiglio.ar_tipoopz AS TipoOpzFiglio, articoFiglio.ar_unmis," & _
                   " articoFiglio.ar_descr, articoFiglio.ar_desint, articoFiglio.ar_codiva," & _
                   " articoFiglio.ar_contros, articoFiglio.ar_note, articoFiglio.ar_forn," & _
                   " articoFiglio.ar_famprod, articoFiglio.ar_perqta, articoFiglio.ar_stainv, articoFiglio.ar_consmrp," & _
                   " articoFiglio.ar_pesoca, 'N' AS FiglioFantasma, articoFiglio.ar_gesfasi as ar_gesfasi "

          strSQL &= " FROM ((distbas INNER JOIN artico AS articoPadre ON distbas.codditt = articoPadre.codditt AND distbas.db_coddb = articoPadre.ar_coddb) " & _
                            " INNER JOIN (movdichi INNER JOIN (movdis INNER JOIN artico AS articoFiglio ON movdis.codditt = articoFiglio.codditt AND movdis.md_codfigli = articoFiglio.ar_codart) " & _
                            " ON (movdichi.codditt = movdis.codditt) AND (movdichi.mdc_coddb = movdis.md_coddb) AND (movdichi.mdc_riga = movdis.md_riga)) " & _
                            " ON distbas.codditt = movdis.codditt AND distbas.db_coddb = movdis.md_coddb) INNER JOIN compchi ON (movdichi.codditt = compchi.codditt) AND (movdichi.mdc_valchia = compchi.cch_valchia) AND (movdichi.mdc_codchia = compchi.cch_codchia)" & _
                            " WHERE articoPadre.codditt = " & CStrSQL(strDitta) & _
                            " AND articoPadre.ar_codart = " & CStrSQL(strCodart) & _
                            " AND cch_codart = Left(articoPadre.ar_codart, Len(articoPadre.ar_codart) - 8) + '" & strTipoArtConf & "'  + Right(articoPadre.ar_codart, 7)"
        Else
          strSQL = " SELECT distbas.db_fascialiv, distbas.db_fantasma, movdis.md_coddb, movdis.md_riga," & _
                   " (CASE WHEN articoFiglio.ar_tipoopz = 'C' THEN " & CStrSQL(Left(strCodRoot, Len(strCodRoot) - 8)) & _
                   " + articoFiglio.ar_tipo + " & CStrSQL(Right(strCodRoot, 7)) & " ELSE movdis.md_codfigli END) as md_codfigli," & _
                   " movdis.md_unmis, movdis.md_quant, movdis.md_ump, movdis.md_quantump," & _
                   " movdis.md_tipofigl, movdis.md_sfrido, movdis.md_sottopr, movdis.md_perpz," & _
                   " movdis.md_note, movdis.md_formula, movdis.md_dtinval, movdis.md_dtfival," & _
                   " movdis.md_proven, movdis.md_codmaga, movdis.md_ultmaga, movdis.md_commeca," & _
                   " movdis.md_filtart, movdis.md_filtart2, movdis.md_perpian, movdis.md_perpian2," & _
                   " movdis.md_ritardo, movdis.md_faseiniz, movdis.md_coclie, movdis.md_lottoin," & _
                   " movdis.md_lottofi, movdis.md_misura1, movdis.md_misura2, movdis.md_misura3," & _
                   " movdis.md_interr, movdis.md_fasefigli, articoPadre.ar_tipoopz AS TipoOpzPadre," & _
                   " articoPadre.ar_rrfence AS RRFence, articoPadre.ar_fcorrlt AS FCorrlt," & _
                   " articoPadre.ar_gesvar AS GesvarPadre," & _
                   " (CASE WHEN articoFiglio.ar_tipoopz= 'C' THEN ' ' ELSE articoFiglio.ar_tipoopz END) AS TipoOpzFiglio," & _
                   " articoFiglio.ar_unmis," & _
                   " (CASE WHEN articoFiglio.ar_tipoopz= 'C' THEN 'K' ELSE articoFiglio.ar_gesvar END) AS GesvarFiglio," & _
                   " articoFiglio.ar_tipo, 'N' AS FiglioFantasma, articoPadre.ar_gescomm AS GescommPadre," & _
                   " articoFiglio.ar_gesfasi AS GesfasiFiglio, articoFiglio.ar_makebuy AS MakeBuyFiglio, 'M' AS TipRiga"

          strSQL &= " FROM ((distbas INNER JOIN artico AS articoPadre ON distbas.codditt = articoPadre.codditt AND distbas.db_coddb = articoPadre.ar_coddb) " & _
                                     " INNER JOIN (movdichi INNER JOIN (movdis INNER JOIN artico AS articoFiglio ON movdis.codditt = articoFiglio.codditt AND movdis.md_codfigli = articoFiglio.ar_codart) " & _
                                     " ON (movdichi.codditt = movdis.codditt) AND (movdichi.mdc_coddb = movdis.md_coddb) AND (movdichi.mdc_riga = movdis.md_riga)) " & _
                                     " ON distbas.codditt = movdis.codditt AND distbas.db_coddb = movdis.md_coddb) INNER JOIN compchi ON (movdichi.codditt = compchi.codditt) AND (movdichi.mdc_valchia = compchi.cch_valchia) AND (movdichi.mdc_codchia = compchi.cch_codchia)" & _
                                     " WHERE articoPadre.codditt = " & CStrSQL(strDitta) & _
                                     " AND articoPadre.ar_codart = " & CStrSQL(strCodart) & _
                                     " AND cch_codart = " & CStrSQL(strCodRoot)
        End If



        '---------------------------------------------------------
        ' PER GESTIONE A FASI
        If bGestFase Then
          strSQL &= " AND movdis.md_faseiniz = " & nFase
        End If
        'If bEscludiFornitoDaTerz Then
        '  strSQL &= " AND movdis.md_proven <> 'U'"
        'End If
        '---------------------------------------------------------
        If Not bNoFiltroDataVal Then
          strSQL &= " AND md_dtinval <= " & CDataSQL(dtDatValConv) & _
                    " AND md_dtfival >= " & CDataSQL(dtDatValConv)
        End If
        If bUsaFiltroPadre Then
          If strTipoRoot = " " Or strTipoRoot = "O" Then
            If oApp.Db.SQLUnicode Then
              strSQL &= " AND cast(" & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) LIKE md_filtart"
              strSQL &= " AND cast(" & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) LIKE md_filtart2"
            Else
              strSQL &= " AND " & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " LIKE md_filtart"
              strSQL &= " AND " & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " LIKE md_filtart2"
            End If
          End If
        End If
        '---------------------------------------------------------
        ' PER GESTIONE A FASI
        If bGestFase Then
          If nPrecFase <> -1 Then
            'Ora inserisce l'articolo relativo alla fase inferiore
            If bDbDoc Then
              strSQL &= " UNION ALL (SELECT TOP 1 distbas.db_fascialiv, cicli.dd_coddb AS md_coddb, 0 AS md_riga, artico.ar_codart AS md_codfigli, artico.ar_unmis AS md_unmis, 1 AS md_quant, artico.ar_unmis AS md_ump, 1 AS md_quantump, Null AS md_tipofigl, 0 AS mf_sfrido, 'N' AS md_sottopr, 1 AS md_perpz, Null AS md_note, Null AS md_formula, cicli.dd_dtinval AS md_dtinval, cicli.dd_dtfival AS md_dtfival, cicli.dd_terzista AS md_proven, cicli.dd_magcent AS md_codmaga, cicli.dd_magtran AS md_ultmaga, cicli.dd_commeca AS md_commeca, cicli.dd_filtart AS md_filtart, cicli.dd_filtart2 AS md_filtart2, 1 AS md_perpian, 1 AS md_perpian2, 0 AS md_ritardo, " & nFase & " AS md_faseiniz, 0 AS md_coclie, 0 As md_lottoin, 0 AS md_lottofi, 0 AS md_misura1, 0 AS md_misura2, 0 AS md_misura3, 'N' AS md_interr, cicli.dd_fase AS md_fasefigli, artico.ar_tipoopz AS TipoOpzPadre, artico.ar_rrfence AS RRFence, artico.ar_fcorrlt AS FCorrlt, artico.ar_tipoopz AS TipoOpzFiglio, artico.ar_unmis, artico.ar_descr, artico.ar_desint, artico.ar_codiva, artico.ar_contros, artico.ar_note, artico.ar_forn, artico.ar_famprod, artico.ar_perqta, artico.ar_stainv, artico.ar_consmrp, artico.ar_pesoca, artico.ar_gesfasi, distbas.db_fantasma AS FiglioFantasma, ' ' AS MakeBuyFiglio "

              strSQL &= " FROM compchi INNER JOIN ((distbas INNER JOIN artico ON distbas.codditt = artico.codditt AND distbas.db_coddb = artico.ar_coddb) " & _
                        " INNER JOIN (ciclichi INNER JOIN ((cicli INNER JOIN tabcent ON cicli.codditt = tabcent.codditt AND cicli.dd_codcent = tabcent.tb_codcent) " & _
                        " LEFT JOIN tabcove ON tabcent.codditt = tabcove.codditt AND tabcent.tb_ccodcontr = tabcove.tb_codcove) ON (ciclichi.codditt = cicli.codditt) AND (ciclichi.ddc_coddb = cicli.dd_coddb) AND (ciclichi.ddc_riga = cicli.dd_riga)) ON distbas.codditt = cicli.codditt AND distbas.db_coddb = cicli.dd_coddb) ON (compchi.codditt = ciclichi.codditt) AND (compchi.cch_codchia = ciclichi.ddc_codchia) AND (compchi.cch_valchia = ciclichi.ddc_valchia)" & _
                        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                        " AND ar_codart = " & CStrSQL(strCodart) & _
                        " AND cch_codart = Left(articoPadre.ar_codart, Len(articoPadre.ar_codart) - 8) + '" & strTipoArtConf & "'  + Right(articoPadre.ar_codart, 7)" & _
                        " AND cicli.dd_fase = " & nPrecFase
            Else
              strSQL &= " UNION ALL (SELECT TOP 1 distbas.db_fascialiv, distbas.db_fantasma, cicli.dd_coddb AS md_coddb, 0 AS md_riga, '" & strCodart & _
                        "' AS md_codfigli, artico.ar_unmis AS md_unmis, 1 AS md_quant, artico.ar_unmis AS md_ump, 1 AS md_quantump, Null AS md_tipofigl, " & _
                        "0 AS mf_sfrido, 'N' AS md_sottopr, 1 AS md_perpz, Null AS md_note, Null AS md_formula, cicli.dd_dtinval AS md_dtinval, cicli.dd_dtfival AS md_dtfival, " & _
                        "cicli.dd_terzista AS md_proven, cicli.dd_magcent AS md_codmaga, cicli.dd_magtran AS md_ultmaga, cicli.dd_commeca AS md_commeca, cicli.dd_filtart AS md_filtart, " & _
                        "cicli.dd_filtart2 AS md_filtart2, 1 AS md_perpian, 1 AS md_perpian2, 0 AS md_ritardo, " & nFase & " AS md_faseiniz, 0 AS md_coclie, 0 As md_lottoin, 0 AS md_lottofi, " & _
                        "0 AS md_misura1, 0 AS md_misura2, 0 AS md_misura3, 'N' AS md_interr, cicli.dd_fase AS md_fasefigli, artico.ar_tipoopz AS TipoOpzPadre, artico.ar_rrfence AS RRFence, " & _
                        "artico.ar_fcorrlt AS FCorrlt, artico.ar_gesvar AS GesvarPadre, artico.ar_tipoopz AS TipoOpzFiglio, artico.ar_unmis, artico.ar_gesvar AS GesvarFiglio, artico.ar_tipo, " & _
                        "distbas.db_fantasma AS FiglioFantasma, artico.ar_gescomm AS GescommPadre, artico.ar_gesfasi AS GesfasiFiglio, ' ' AS MakeBuyFiglio, 'C' AS TipRiga"

              strSQL &= " FROM compchi INNER JOIN ((distbas INNER JOIN artico ON distbas.codditt = artico.codditt AND distbas.db_coddb = artico.ar_coddb) " & _
                        " INNER JOIN (ciclichi INNER JOIN ((cicli INNER JOIN tabcent ON cicli.codditt = tabcent.codditt AND cicli.dd_codcent = tabcent.tb_codcent) " & _
                        " LEFT JOIN tabcove ON tabcent.codditt = tabcove.codditt AND tabcent.tb_ccodcontr = tabcove.tb_codcove) ON (ciclichi.codditt = cicli.codditt) AND (ciclichi.ddc_coddb = cicli.dd_coddb) AND (ciclichi.ddc_riga = cicli.dd_riga)) ON distbas.codditt = cicli.codditt AND distbas.db_coddb = cicli.dd_coddb) ON (compchi.codditt = ciclichi.codditt) AND (compchi.cch_codchia = ciclichi.ddc_codchia) AND (compchi.cch_valchia = ciclichi.ddc_valchia)" & _
                        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                        " AND ar_codart = " & CStrSQL(strCodart) & _
                        " AND cch_codart = " & CStrSQL(strCodRoot) & _
                        " AND cicli.dd_fase = " & nPrecFase
            End If

            If Not bNoFiltroDataVal Then
              strSQL &= " AND cicli.dd_dtinval <=" & CDataSQL(dtDatValConv) & _
                        " AND cicli.dd_dtfival >=" & CDataSQL(dtDatValConv)
            End If
            If bUsaFiltroPadre Then
              If (strTipoRoot = " " Or strTipoRoot = "O") Then
                If oApp.Db.SQLUnicode Then
                  strSQL &= " AND cast(" & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) LIKE cicli.dd_filtart"
                  strSQL &= " AND cast(" & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) LIKE cicli.dd_filtart2"
                Else
                  strSQL &= " AND " & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " LIKE cicli.dd_filtart"
                  strSQL &= " AND " & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " LIKE cicli.dd_filtart2"
                End If
              End If
            End If
            strSQL &= " )"
          End If
        End If
        '---------------------------------------------------------

      Else

        '---------------------------------------------------------
        ' PER GESTIONE A FASI
        If bGestFase Then

          nPrecFase = -1
          'Prima di tutto individua qual'è la fase precedente rispetto a nFaseTerz
          strSQL = " SELECT TOP 1 cicli.dd_fase" & _
                   " FROM cicli INNER JOIN artico ON cicli.codditt = artico.codditt And cicli.dd_coddb = artico.ar_coddb" & _
                   " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                   " AND artico.ar_codart = " & CStrSQL(strCodart) & " AND cicli.dd_fase < " & nFase
          If Not bNoFiltroDataVal Then
            strSQL &= " AND cicli.dd_dtinval <=" & CDataSQL(dtDatValConv) & _
                      " AND cicli.dd_dtfival >=" & CDataSQL(dtDatValConv)
          End If
          If bUsaFiltroPadre Then
            If (strTipoRoot = " " Or strTipoRoot = "O") Then
              If oApp.Db.SQLUnicode Then
                strSQL &= " AND cast(" & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) LIKE cicli.dd_filtart"
                strSQL &= " AND cast(" & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) LIKE cicli.dd_filtart2"
              Else
                strSQL &= " AND " & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " LIKE cicli.dd_filtart"
                strSQL &= " AND " & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " LIKE cicli.dd_filtart2"
              End If
            End If
          End If
          If lCodclie = 0 Then
            strSQL &= " AND cicli.dd_coclie = 0"
          Else
            strSQL &= " AND (( cicli.dd_coclie = " & lCodclie & ") OR ( cicli.dd_coclie = 0 AND cicli.dd_fase NOT IN" & _
                      " (SELECT T1.dd_fase FROM (distbas AS T2 INNER JOIN cicli AS T1 ON T2.codditt = T1.codditt AND T2.db_coddb = T1.dd_coddb) INNER JOIN artico AS T3 ON T2.codditt = T3.codditt AND T2.db_coddb = T3.ar_coddb" & _
                      " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                      " AND ar_codart = " & CStrSQL(strCodart)
            If Not bNoFiltroDataVal Then
              strSQL &= " AND dd_dtinval <=" & CDataSQL(dtDatValConv) & _
                        " AND dd_dtfival >=" & CDataSQL(dtDatValConv)
            End If
            strSQL &= " AND T1.dd_coclie = " & lCodclie & "))   ) "
          End If
          If bUsaFiltroCommessa Then
            If lCommeca = 0 Then
              strSQL &= " AND cicli.dd_commeca = 0 "
            Else
              strSQL &= " AND (( cicli.dd_commeca = " & lCommeca & ") OR ( cicli.dd_commeca = 0 AND cicli.dd_fase NOT IN " & _
                        " (SELECT T1.dd_fase FROM (distbas AS T2 INNER JOIN cicli AS T1 ON T2.codditt = T1.codditt AND T2.db_coddb = T1.dd_coddb) INNER JOIN artico AS T3 ON T2.codditt = T3.codditt AND T2.db_coddb = T3.ar_coddb" & _
                        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                        " AND ar_codart = " & CStrSQL(strCodart)
              If Not bNoFiltroDataVal Then
                strSQL &= " AND dd_dtinval <=" & CDataSQL(dtDatValConv) & _
                          " AND dd_dtfival >=" & CDataSQL(dtDatValConv)
              End If
              strSQL &= " AND T1.dd_commeca = " & lCommeca & " ))   ) "
            End If
          End If
          strSQL &= " ORDER BY cicli.dd_fase DESC"
          dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
          If dttTmp.Rows.Count > 0 Then nPrecFase = NTSCInt(dttTmp.Rows(0)!dd_fase)
          dttTmp.Clear()
        End If
        '---------------------------------------------------------

        'Distinta base normale (NO configuratore)
        If bDbDoc Then
          If bGestFase Then
            strSQL = " SELECT distbas.db_fascialiv, md_coddb, md_riga, md_codfigli, md_unmis, md_quant, md_ump, md_quantump, md_tipofigl, md_sfrido, md_sottopr, md_perpz, md_note, md_formula, md_dtinval, md_dtfival, md_proven, md_codmaga, md_ultmaga, md_commeca, md_filtart, md_filtart2, md_perpian, md_perpian2, md_ritardo, md_faseiniz, md_coclie, md_lottoin, md_lottofi, md_misura1, md_misura2, md_misura3, md_interr, md_fasefigli, md_tctipo, articoPadre.ar_tipoopz AS TipoOpzPadre, articoPadre.ar_rrfence AS RRFence, articoPadre.ar_fcorrlt AS Fcorrlt, " & _
                     " articoPadre.ar_gesvar AS GesvarPadre,articoFiglio.ar_tipoopz AS TipoOpzFiglio, articoFiglio.ar_codtagl AS TaglieColoriFiglio, articoFiglio.ar_unmis, articoFiglio.ar_gesvar AS GesvarFiglio, articoFiglio.ar_descr, articoFiglio.ar_desint, articoFiglio.ar_codiva, articoFiglio.ar_contros, articoFiglio.ar_note, articoFiglio.ar_forn, articoFiglio.ar_famprod, articoFiglio.ar_perqta, articoFiglio.ar_stainv, articoFiglio.ar_consmrp, articoFiglio.ar_pesoca, articoFiglio.ar_gesfasi, distbas_1.db_fantasma AS FiglioFantasma, articoFiglio.ar_makebuy AS MakeBuyFiglio "
          Else
            strSQL = " SELECT distbas.db_fascialiv, movdis.*, articoPadre.ar_tipoopz AS TipoOpzPadre," & _
                     " articoPadre.ar_rrfence AS RRFence, articoPadre.ar_fcorrlt AS Fcorrlt," & _
                     " articoFiglio.ar_tipoopz AS TipoOpzFiglio, articoFiglio.ar_unmis," & _
                     " articoFiglio.ar_descr, articoFiglio.ar_desint, articoFiglio.ar_codiva," & _
                     " articoFiglio.ar_contros, articoFiglio.ar_note, articoFiglio.ar_forn," & _
                     " articoFiglio.ar_famprod, articoFiglio.ar_perqta, articoFiglio.ar_stainv, articoFiglio.ar_consmrp," & _
                     " articoFiglio.ar_pesoca,distbas_1.db_fantasma AS FiglioFantasma," & _
                     " articoPadre.ar_gesvar AS GesvarPadre, articoFiglio.ar_codtagl as TaglieColoriFiglio, " & _
                     " articoFiglio.ar_gesfasi as ar_gesfasi"
          End If
        Else
          strSQL = "SELECT distbas.db_fascialiv, distbas.db_fantasma,"
          If bGestFase Then ' PER GESTIONE A FASI
            strSQL &= " md_coddb, md_riga, md_codfigli, md_unmis, md_quant, md_ump, md_quantump, md_tipofigl, md_sfrido, md_sottopr, md_perpz, md_note, md_formula, md_dtinval, md_dtfival, md_proven, md_codmaga, md_ultmaga, md_commeca, md_filtart, md_filtart2, md_perpian, md_perpian2, md_ritardo, md_faseiniz, md_coclie, md_lottoin, md_lottofi, md_misura1, md_misura2, md_misura3, md_interr, md_fasefigli, md_tctipo,"
          Else
            strSQL &= " movdis.*,"
          End If
          strSQL &= " articoPadre.ar_tipoopz AS TipoOpzPadre, articoPadre.ar_rrfence AS RRFence, articoPadre.ar_fcorrlt AS FCorrlt, articoPadre.ar_gesvar AS GesvarPadre, articoFiglio.ar_tipoopz AS TipoOpzFiglio, articoFiglio.ar_codtagl as TaglieColoriFiglio, articoFiglio.ar_unmis, articoFiglio.ar_gesvar AS GesvarFiglio, articoFiglio.ar_tipo, distbas_1.db_fantasma AS FiglioFantasma, articoPadre.ar_gescomm AS GescommPadre, articoFiglio.ar_gesfasi AS GesfasiFiglio, CASE WHEN artfasi.af_makebuy IS NULL THEN articoFiglio.ar_makebuy ELSE artfasi.af_makebuy END As MakeBuyFiglio, 'M' AS TipRiga"
        End If
        strSQL &= " FROM ((distbas INNER JOIN ((movdis INNER JOIN artico AS articoFiglio ON movdis.codditt = articoFiglio.codditt AND movdis.md_codfigli = articoFiglio.ar_codart) LEFT JOIN artfasi ON movdis.codditt = artfasi.codditt AND movdis.md_codfigli = artfasi.af_codart AND movdis.md_fasefigli = artfasi.af_fase)" & _
                  " ON distbas.codditt = movdis.codditt AND distbas.db_coddb = movdis.md_coddb) INNER JOIN artico AS articoPadre ON distbas.codditt = articoPadre.codditt AND distbas.db_coddb = articoPadre.ar_coddb) " & _
                  " LEFT JOIN distbas AS distbas_1 ON articoFiglio.codditt = distbas_1.codditt AND articoFiglio.ar_coddb = distbas_1.db_coddb" & _
                  " WHERE articoPadre.codditt = " & CStrSQL(strDitta) & _
                  " AND articoPadre.ar_codart = " & CStrSQL(strCodart)
        If bGestFase Then strSQL &= " AND movdis.md_faseiniz = " & nFase ' PER GESTIONE A FASI
        If strDescrTagliaPadr <> "" And strDescrTagliaPadr <> " " Then strSQL &= " AND (movdis.md_tcindtagl = 0 Or movdis.md_tcindtagl = " & nIndiceTagliaPadr & ")"
        If bEscludiFornitoDaTerz Then strSQL &= " AND movdis.md_proven <> 'U'"
        If Not bNoFiltroDataVal Then
          strSQL &= " AND md_dtinval <= " & CDataSQL(dtDatValConv) & _
                    " AND md_dtfival >= " & CDataSQL(dtDatValConv)
        End If
        If bUsaFiltroPadre Then
          If strTipoRoot = " " Or strTipoRoot = "O" Then
            If oApp.Db.SQLUnicode Then
              strSQL &= " AND cast(" & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) LIKE md_filtart"
              strSQL &= " AND cast(" & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) LIKE md_filtart2"
            Else
              strSQL &= " AND " & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " LIKE md_filtart"
              strSQL &= " AND " & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " LIKE md_filtart2"
            End If
          End If
        End If
        If lCodclie = 0 Then
          strSQL &= " AND md_coclie = 0"
        Else
          strSQL &= " AND (( md_coclie = " & lCodclie & " AND md_tipofigl IS NOT Null ) OR  ( md_coclie = 0 AND md_tipofigl IS Null))"
        End If
        If bUsaFiltroCommessa Then
          If lCommeca = 0 Then
            strSQL &= " AND md_commeca = 0 "
          Else
            strSQL &= " AND (( md_commeca = " & lCommeca & " AND md_tipofigl IS NOT Null ) OR ( md_commeca = 0 AND md_tipofigl NOT IN" & _
                      " (SELECT md_tipofigl FROM movdis AS T1 INNER JOIN artico AS T2 ON T1.codditt = T2.codditt AND T1.md_coddb = T2.ar_coddb" & _
                      " WHERE T2.codditt = " & CStrSQL(strDitta) & _
                      " AND T2.ar_codart = " & CStrSQL(strCodart)
            If Not bNoFiltroDataVal Then
              strSQL &= " AND T1.md_dtinval <=" & CDataSQL(dtDatValConv) & _
                        " AND T1.md_dtfival >=" & CDataSQL(dtDatValConv)
            End If
            If Not bDbDoc Then strSQL &= " AND T1.md_lottoin <=" & lLotto & " AND T1.md_lottofi >=" & lLotto
            strSQL &= " AND T1.md_commeca = " & lCommeca & " )) OR ( md_commeca = 0 AND md_tipofigl IS Null))"
          End If
        End If

        '---------------------------------------------------------
        ' PER GESTIONE A FASI
        If bGestFase Then
          If nPrecFase <> -1 Then
            'Ora inserisce l'articolo relativo alla fase inferiore
            If bDbDoc Then
              strSQL &= " UNION ALL (SELECT TOP 1 distbas.db_fascialiv, cicli.dd_coddb AS md_coddb, 0 AS md_riga, artico.ar_codart AS md_codfigli, artico.ar_unmis AS md_unmis, 1 AS md_quant, artico.ar_unmis AS md_ump, 1 AS md_quantump, Null AS md_tipofigl, 0 AS mf_sfrido, 'N' AS md_sottopr, 1 AS md_perpz, Null AS md_note, Null AS md_formula, cicli.dd_dtinval AS md_dtinval, cicli.dd_dtfival AS md_dtfival, cicli.dd_terzista AS md_proven, cicli.dd_magcent AS md_codmaga, cicli.dd_magtran AS md_ultmaga, cicli.dd_commeca AS md_commeca, cicli.dd_filtart AS md_filtart, cicli.dd_filtart2 AS md_filtart2, 1 AS md_perpian, 1 AS md_perpian2, 0 AS md_ritardo, " & nFase & " AS md_faseiniz, 0 AS md_coclie, 0 As md_lottoin, 0 AS md_lottofi, 0 AS md_misura1, 0 AS md_misura2, 0 AS md_misura3, 'N' AS md_interr, cicli.dd_fase AS md_fasefigli, 'F' AS md_tctipo, artico.ar_tipoopz AS TipoOpzPadre, artico.ar_rrfence AS RRFence, artico.ar_fcorrlt AS FCorrlt, artico.ar_gesvar AS GesvarPadre, artico.ar_tipoopz AS TipoOpzFiglio, artico.ar_codtagl AS TaglieColoriFiglio, artico.ar_unmis, artico.ar_gesvar AS GesvarFiglio, artico.ar_descr, artico.ar_desint, artico.ar_codiva, artico.ar_contros, artico.ar_note, artico.ar_forn, artico.ar_famprod, artico.ar_perqta, artico.ar_stainv, artico.ar_consmrp, artico.ar_pesoca, artico.ar_gesfasi, distbas.db_fantasma AS FiglioFantasma, ' ' AS MakeBuyFiglio"
            Else
              strSQL &= " UNION ALL (SELECT TOP 1 distbas.db_fascialiv, distbas.db_fantasma, cicli.dd_coddb AS md_coddb, 0 AS md_riga, " & _
                        " artico.ar_codart AS md_codfigli, artico.ar_unmis AS md_unmis, 1 AS md_quant, artico.ar_unmis AS md_ump, 1 AS md_quantump, " & _
                        " Null AS md_tipofigl, 0 AS mf_sfrido, 'N' AS md_sottopr, 1 AS md_perpz, Null AS md_note, Null AS md_formula, " & _
                        " cicli.dd_dtinval AS md_dtinval, cicli.dd_dtfival AS md_dtfival, cicli.dd_terzista AS md_proven, cicli.dd_magcent AS md_codmaga, " & _
                        " cicli.dd_magtran AS md_ultmaga, cicli.dd_commeca AS md_commeca, cicli.dd_filtart AS md_filtart, cicli.dd_filtart2 AS md_filtart2, " & _
                        " 1 AS md_perpian, 1 AS md_perpian2, 0 AS md_ritardo, " & nFase & " AS md_faseiniz, 0 AS md_coclie, 0 As md_lottoin, 0 AS md_lottofi, " & _
                        " 0 AS md_misura1, 0 AS md_misura2, 0 AS md_misura3, 'N' AS md_interr, cicli.dd_fase AS md_fasefigli, 'F' as md_tctipo, " & _
                        " artico.ar_tipoopz AS TipoOpzPadre, artico.ar_rrfence AS RRFence, artico.ar_fcorrlt AS FCorrlt, artico.ar_gesvar AS GesvarPadre, " & _
                        " artico.ar_tipoopz AS TipoOpzFiglio, artico.ar_codtagl AS TaglieColoriFiglio, artico.ar_unmis, artico.ar_gesvar AS GesvarFiglio, " & _
                        " artico.ar_tipo, distbas.db_fantasma AS FiglioFantasma, artico.ar_gescomm AS GescommPadre, artico.ar_gesfasi AS GesfasiFiglio, " & _
                        " CASE WHEN artfasi.af_makebuy IS NULL THEN ' ' ELSE artfasi.af_makebuy END As MakeBuyFiglio, 'C' AS TipRiga"
            End If
            strSQL &= " FROM ((cicli INNER JOIN artico ON cicli.codditt = artico.codditt AND cicli.dd_coddb = artico.ar_coddb) INNER JOIN distbas ON distbas.codditt = cicli.codditt AND distbas.db_coddb = cicli.dd_coddb) LEFT JOIN artfasi ON cicli.codditt = artfasi.codditt AND cicli.dd_coddb = artfasi.af_codart AND cicli.dd_fase = artfasi.af_fase" & _
                      " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                      " AND artico.ar_codart = " & CStrSQL(strCodart) & " AND cicli.dd_fase = " & nPrecFase
            If Not bNoFiltroDataVal Then
              strSQL &= " AND cicli.dd_dtinval <=" & CDataSQL(dtDatValConv) & _
                        " AND cicli.dd_dtfival >=" & CDataSQL(dtDatValConv)
            End If
            If bUsaFiltroPadre Then
              If (strTipoRoot = " " Or strTipoRoot = "O") Then
                If oApp.Db.SQLUnicode Then
                  strSQL &= " AND cast(" & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) LIKE cicli.dd_filtart "
                  strSQL &= " AND cast(" & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) LIKE cicli.dd_filtart2 "
                Else
                  strSQL &= " AND " & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " LIKE cicli.dd_filtart "
                  strSQL &= " AND " & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " LIKE cicli.dd_filtart2 "
                End If
              End If
            End If
            If lCodclie = 0 Then
              strSQL &= " AND cicli.dd_coclie = 0 "
            Else
              strSQL &= " AND (( cicli.dd_coclie = " & lCodclie & ") OR ( cicli.dd_coclie = 0 AND cicli.dd_fase NOT IN " & _
                        " (SELECT T1.dd_fase FROM (distbas AS T2 INNER JOIN cicli AS T1 ON T2.codditt = T1.codditt AND T2.db_coddb = T1.dd_coddb) INNER JOIN artico AS T3 ON T2.codditt = T3.codditt AND T2.db_coddb = T3.ar_coddb" & _
                        " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                        " AND ar_codart = '" & strCodart & "' "
              If Not bNoFiltroDataVal Then
                strSQL &= " AND dd_dtinval <=" & CDataSQL(dtDatValConv) & _
                          " AND dd_dtfival >=" & CDataSQL(dtDatValConv) & " "
              End If
              strSQL &= " AND T1.dd_coclie = " & lCodclie & "))   ) "
            End If
            If bUsaFiltroCommessa Then
              If lCommeca = 0 Then
                strSQL &= " AND cicli.dd_commeca = 0 "
              Else
                strSQL &= " AND (( cicli.dd_commeca = " & lCommeca & ") OR ( cicli.dd_commeca = 0 AND cicli.dd_fase NOT IN " & _
                          " (SELECT T1.dd_fase FROM (distbas AS T2 INNER JOIN cicli AS T1 ON T2.codditt = T1.codditt AND T2.db_coddb = T1.dd_coddb) INNER JOIN artico AS T3 ON T2.codditt = T3.codditt AND T2.db_coddb = T3.ar_coddb" & _
                          " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                          " AND ar_codart = '" & strCodart & "' "
                If Not bNoFiltroDataVal Then
                  strSQL &= " AND dd_dtinval <=" & CDataSQL(dtDatValConv) & _
                            " AND dd_dtfival >=" & CDataSQL(dtDatValConv) & " "
                End If
                strSQL &= " AND T1.dd_commeca = " & lCommeca & " ))   ) "
              End If
            End If
            strSQL &= " ) "
          End If
        End If
        '---------------------------------------------------------

      End If

      Select Case nSortTipoFiglio
        Case 0 : strSQL &= " ORDER BY md_riga"
        Case 1 : strSQL &= " ORDER BY md_tipofigl"
        Case 2 : strSQL &= " ORDER BY md_codfigli"
      End Select
      'Adesso in base alla query giusta apre snaMovdis
      dttMovdis = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrattaDb_TrovaLav(ByVal strDitta As String, ByRef dttCicli As DataTable, ByVal nTipoOp As Integer, _
                                                ByVal strTipoArtConf As String, ByVal strCodart As String, _
                                                ByVal strCodRoot As String, ByVal strGesvarPadr As String, _
                                                ByVal bNoFiltroDataVal As Boolean, ByVal bUsaFiltroPadre As Boolean, _
                                                ByVal strTipoRoot As String, ByVal dtDatValConv As Date, _
                                                ByVal lCodclie As Integer, _
                                                ByVal bUsaFiltroCommessa As Boolean, ByVal lCommeca As Integer, _
                                                ByVal bGestFase As Boolean, ByVal nFase As Integer) As Boolean
    Return TrattaDb_TrovaLavEx(strDitta, dttCicli, nTipoOp, strTipoArtConf, strCodart, _
                               strCodRoot, strGesvarPadr, bNoFiltroDataVal, bUsaFiltroPadre, _
                               strTipoRoot, dtDatValConv, lCodclie, bUsaFiltroCommessa, lCommeca, _
                               bGestFase, nFase)
  End Function
  Public Overridable Function TrattaDb_TrovaLavEx(ByVal strDitta As String, ByRef dttCicli As DataTable, ByVal nTipoOp As Integer, _
                                                ByVal strTipoArtConf As String, ByVal strCodart As String, _
                                                ByVal strCodRoot As String, ByVal strGesvarPadr As String, _
                                                ByVal bNoFiltroDataVal As Boolean, ByVal bUsaFiltroPadre As Boolean, _
                                                ByVal strTipoRoot As String, ByVal dtDatValConv As Date, _
                                                ByVal lCodclie As Integer, _
                                                ByVal bUsaFiltroCommessa As Boolean, ByVal lCommeca As Integer, _
                                                ByVal bGestFase As Boolean, ByVal nFase As Integer, Optional ByVal bDbDoc As Boolean = False) As Boolean
    Dim strSQL As String = ""
    Try

      '---------------------------------------------------------
      'ANALISI CICLI
      '---------------------------------------------------------

      strSQL = "SELECT cicli.*, tabcent.*, tabcove.tb_concova"
      If strGesvarPadr = "K" Then
        If bDbDoc Then
          'Se deriva da configuratore
          strSQL &= " FROM compchi INNER JOIN ((distbas INNER JOIN artico ON distbas.codditt = artico.codditt AND distbas.db_coddb = artico.ar_coddb) INNER JOIN (ciclichi INNER JOIN ((cicli INNER JOIN tabcent ON cicli.codditt = tabcent.codditt AND cicli.dd_codcent = tabcent.tb_codcent) LEFT JOIN tabcove ON tabcent.codditt = tabcove.codditt AND tabcent.tb_ccodcontr = tabcove.tb_codcove) ON (ciclichi.codditt = cicli.codditt) AND (ciclichi.ddc_riga = cicli.dd_riga) AND (ciclichi.ddc_coddb = cicli.dd_coddb)) ON distbas.codditt = cicli.codditt AND distbas.db_coddb = cicli.dd_coddb) ON (compchi.codditt = ciclichi.codditt) AND (compchi.cch_valchia = ciclichi.ddc_valchia) AND (compchi.cch_codchia = ciclichi.ddc_codchia)" & _
                    " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                    " AND ar_codart = " & CStrSQL(strCodart) & _
                    " AND cch_codart = Left(artico.ar_codart, Len(artico.ar_codart) - 8) + '" & strTipoArtConf & "'  + Right(artico.ar_codart, 7)"
        Else
          'Se deriva da configuratore
          strSQL &= " FROM compchi INNER JOIN ((distbas INNER JOIN artico ON distbas.codditt = artico.codditt AND distbas.db_coddb = artico.ar_coddb) INNER JOIN (ciclichi INNER JOIN ((cicli INNER JOIN tabcent ON cicli.codditt = tabcent.codditt AND cicli.dd_codcent = tabcent.tb_codcent) LEFT JOIN tabcove ON tabcent.codditt = tabcove.codditt AND tabcent.tb_ccodcontr = tabcove.tb_codcove) ON (ciclichi.codditt = cicli.codditt) AND (ciclichi.ddc_riga = cicli.dd_riga) AND (ciclichi.ddc_coddb = cicli.dd_coddb)) ON distbas.codditt = cicli.codditt AND distbas.db_coddb = cicli.dd_coddb) ON (compchi.codditt = ciclichi.codditt) AND (compchi.cch_valchia = ciclichi.ddc_valchia) AND (compchi.cch_codchia = ciclichi.ddc_codchia)" & _
                    " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                    " AND ar_codart = " & CStrSQL(strCodart) & _
                    " AND cch_codart = " & CStrSQL(strCodRoot)
        End If
      Else
        'non deriva da configuratore 
        strSQL &= " FROM (distbas INNER JOIN artico ON distbas.codditt = artico.codditt AND distbas.db_coddb = artico.ar_coddb) INNER JOIN ((cicli INNER JOIN tabcent ON cicli.codditt = tabcent.codditt AND cicli.dd_codcent = tabcent.tb_codcent) LEFT JOIN tabcove ON tabcent.codditt = tabcove.codditt AND tabcent.tb_ccodcontr = tabcove.tb_codcove) ON distbas.codditt = cicli.codditt AND distbas.db_coddb = cicli.dd_coddb" & _
                  " WHERE artico.codditt = " & CStrSQL(strDitta) & _
                  " AND ar_codart = " & CStrSQL(strCodart)
        If lCodclie = 0 Then
          strSQL &= " AND dd_coclie = 0"
        Else
          strSQL &= " AND (( dd_coclie = " & lCodclie & ") OR ( dd_coclie = 0 AND dd_fase NOT IN" & _
                    " (SELECT T1.dd_fase FROM (distbas AS T2 INNER JOIN cicli AS T1 ON T2.codditt = T1.codditt AND T2.db_coddb = T1.dd_coddb) INNER JOIN artico AS T3 ON T2.codditt = T3.codditt AND T2.db_coddb = T3.ar_coddb" & _
                    " WHERE T3.codditt = " & CStrSQL(strDitta) & _
                    " AND ar_codart = " & CStrSQL(strCodart)
          If Not bNoFiltroDataVal Then
            strSQL &= " AND dd_dtinval <=" & CDataSQL(dtDatValConv) & _
                      " AND dd_dtfival >=" & CDataSQL(dtDatValConv)
          End If
          strSQL &= " AND T1.dd_coclie = " & lCodclie & ")))"
        End If
        If bUsaFiltroCommessa Then
          If lCommeca = 0 Then
            strSQL &= " AND dd_commeca = 0"
          Else
            strSQL &= " AND (( dd_commeca = " & lCommeca & ") OR ( dd_commeca = 0 AND dd_fase NOT IN" & _
                      " (SELECT T1.dd_fase FROM (distbas AS T2 INNER JOIN cicli AS T1 ON T2.codditt = T1.codditt AND T2.db_coddb = T1.dd_coddb) INNER JOIN artico AS T3 ON T2.codditt = T3.codditt AND T2.db_coddb = T3.ar_coddb" & _
                      " WHERE T3.codditt = " & CStrSQL(strDitta) & _
                      " AND ar_codart = " & CStrSQL(strCodart)
            If Not bNoFiltroDataVal Then
              strSQL &= " AND dd_dtinval <=" & CDataSQL(dtDatValConv) & _
                        " AND dd_dtfival >=" & CDataSQL(dtDatValConv)
            End If
            strSQL &= " AND T1.dd_commeca = " & lCommeca & ")))"
          End If
        End If
      End If
      If bGestFase Then strSQL &= " AND cicli.dd_fase = " & nFase ' PER GESTIONE A FASI
      If Not bNoFiltroDataVal Then
        strSQL &= " AND dd_dtinval <=" & CDataSQL(dtDatValConv) & _
                  " AND dd_dtfival >=" & CDataSQL(dtDatValConv)
      End If
      If bUsaFiltroPadre Then
        If (strTipoRoot = " " Or strTipoRoot = "O") Then
          If oApp.Db.SQLUnicode Then
            strSQL &= " AND cast(" & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) like dd_filtart"
            strSQL &= " AND cast(" & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " as nvarchar(" & CLN__STD.CodartMaxLen & ")) like dd_filtart2"
          Else
            strSQL &= " AND " & CStrSQL(Left(strCodart + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " like dd_filtart"
            strSQL &= " AND " & CStrSQL(Left(strCodRoot + "".PadRight(CLN__STD.CodartMaxLen), CLN__STD.CodartMaxLen)) & " like dd_filtart2"
          End If
        End If
      End If
      If nTipoOp = 3 Then ' se STDB va in ordine di fase
        strSQL &= " ORDER BY dd_fase"
      Else                ' se EMRP va in ordine inverso di fase
        strSQL &= " ORDER BY dd_fase DESC"
      End If
      ' ora apre finalmente snacicli
      dttCicli = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function TrattaDbDoc_GetVarianteColorefiglio(ByVal strDitta As String, ByVal strCodDb As String, _
                                                                  ByVal lRiga As Integer, ByVal strColorePadr As String) As String
    Return TrattaDb_GetVarianteColorefiglio(strDitta, strCodDb, lRiga.ToString, strColorePadr)
  End Function
  Public Overridable Function TrattaDb_GetVarianteColorefiglio(ByVal strDitta As String, ByVal strCodDb As String, _
                                                               ByVal strRiga As String, ByVal strColorePadr As String) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      TrattaDb_GetVarianteColorefiglio = ""

      strSQL = " SELECT mdc_tccodvarf FROM movdistc" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mdc_coddb = " & CStrSQL(strCodDb) & _
               " AND mdc_riga = " & CDblSQL(strRiga) & _
               " AND mdc_tccodvarp = " & CStrSQL(strColorePadr)
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then TrattaDb_GetVarianteColorefiglio = NTSCStr(dttTmp.Rows(0)!mdc_tccodvarf)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function TrattaDb_GetVarianteTagliafiglio(ByVal strDitta As String, ByVal strCodDb As String, _
                                                               ByVal strRiga As String, ByVal nIndTagliaPadr As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try
      TrattaDb_GetVarianteTagliafiglio = ""

      strSQL = " SELECT mdt_tccodvarf FROM movdistt" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mdt_coddb = " & CStrSQL(strCodDb) & _
               " AND mdt_riga = " & CDblSQL(strRiga) & _
               " AND mdt_tcindtaglp = " & nIndTagliaPadr
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then TrattaDb_GetVarianteTagliafiglio = NTSCStr(dttTmp.Rows(0)!mdt_tccodvarf)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Sub TrattaDb_GetTagliafiglio(ByVal strDitta As String, ByVal strCodDb As String, _
                                                          ByVal strRiga As String, ByVal nIndTagliaPadr As Integer, _
                                                          ByRef strTagliaFigl As String, ByRef nIndTagliaFigl As Integer)
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Try

      strTagliaFigl = ""
      nIndTagliaFigl = 0

      strSQL = " SELECT mdt_tctagliaf, mdt_tcindtaglf FROM movdistt" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND mdt_coddb = " & CStrSQL(strCodDb) & _
               " AND mdt_riga = " & CDblSQL(strRiga) & _
               " AND mdt_tcindtaglp = " & nIndTagliaPadr
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        strTagliaFigl = NTSCStr(dttTmp.Rows(0)!mdt_tctagliaf)
        nIndTagliaFigl = NTSCInt(dttTmp.Rows(0)!mdt_tcindtaglf)
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Sub
#End Region

  'Crttstm.bas
  Public Overridable Function RiempiTTStMatr(ByVal strDitta As String, ByVal strSTMTTStMatr As String, _
                                              ByVal lSTMIITTStMatr As Integer, _
                                              ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                              ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                              ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                              ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                              ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                              ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                              ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                              ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                              ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                              ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                              ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                              ByVal strQuery As String) As Boolean

    Try
      '--------------------------------------------------------------------------------------------------------------
      Return RiempiTTStMatr(strDitta, strSTMTTStMatr, lSTMIITTStMatr, _
                            strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                            nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                            nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                            strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                            nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                            nSTMQualeMatr, strError, strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function RiempiTTStMatr(ByVal strDitta As String, ByVal strSTMTTStMatr As String, _
                                             ByVal lSTMIITTStMatr As Integer, _
                                             ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                             ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                             ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                             ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                             ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                             ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                             ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                             ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                             ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                             ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                             ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                             ByVal strQuery As String, ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strSTMTTStMatr, lSTMIITTStMatr, _
                            strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                            nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                            nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                            strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                            nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                            nSTMQualeMatr, strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                            strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(24))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return RiempiTTStMatr(strDitta, strSTMTTStMatr, lSTMIITTStMatr, strSTMDaMatr, strSTMAMatr, _
                            strSTMDatIni, strSTMDatFin, nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                            nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, strSTMTipork, strSTMSerie, nSTMCausale, _
                            nSTMGruppo, nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, nSTMQualeMatr, _
                            strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                            strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                            "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RiempiTTStMatr(ByVal strDitta As String, ByVal strSTMTTStMatr As String, _
                                             ByVal lSTMIITTStMatr As Integer, _
                                             ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                             ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                             ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                             ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                             ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                             ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                             ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                             ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                             ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                             ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                             ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                             ByVal strQuery As String, ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String, _
                                             ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                             ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    'nSTMQualeMatr Flag di selezione del tipo di matricole
    '0 = Matricole aperte e chiuse
    '1 = Matricole aperte
    '2 = Saldo matricole aperte
    'Const MATRAPCHIU As Integer = 0
    Const MATRAPERTE As Integer = 1
    'Const MATRSALAP As Integer = 2
    Dim strSQL As String
    Dim strSQL1 As String
    Dim strJoin As String
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strSTMTTStMatr, lSTMIITTStMatr, _
                            strSTMDaMatr, strSTMAMatr, strSTMDatIni, strSTMDatFin, _
                            nSTMDaMagaz, nSTMAMagaz, strSTMDaCodart, strSTMACodart, _
                            nSTMDaFase, nSTMAFase, lSTMDaConto, lSTMAConto, _
                            strSTMTipork, strSTMSerie, nSTMCausale, nSTMGruppo, _
                            nSTMSotGru, nSTMCodmarcini, nSTMCodmarcfin, strSTMCodcfam, _
                            nSTMQualeMatr, strError, strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                            strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                            strCodart, nCodlsar, strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(24))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strJoin = CreaJoin()
      '--------------------------------------------------------------------------------------------------------------
      '--- Cancella il contenuto della tebella temporanea TTSTMATR
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM " & strSTMTTStMatr & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lSTMIITTStMatr
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- COMPONE LA STRINGA DI CODA
      '--------------------------------------------------------------------------------------------------------------
      strSQL1 = " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
        " AND movmatr.mma_matric BETWEEN " & CStrSQL(strSTMDaMatr) & " AND " & CStrSQL(strSTMAMatr) & _
        " AND artico.ar_gestmatr = 'S'" & _
        " AND artico.ar_stasche = 'S'" & _
        IIf(nSTMCausale > 0, " AND keymag.km_causale = " & nSTMCausale, "").ToString & _
        IIf(nSTMGruppo > 0, " AND artico.ar_gruppo = " & nSTMGruppo, "").ToString & _
        IIf(nSTMSotGru > 0, " AND artico.ar_sotgru = " & nSTMSotGru, "").ToString & _
        IIf(strSTMCodcfam <> "", " AND artico.ar_famprod = " & CStrSQL(strSTMCodcfam), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
      If (NTSCDate(strSTMDatIni) <> NTSCDate(IntSetDate("01/01/1900"))) Or (NTSCDate(strSTMDatFin) <> NTSCDate(IntSetDate("31/12/2099"))) Then
        strSQL1 += " AND keymag.km_aammgg BETWEEN " & CDataSQL(strSTMDatIni) & " AND " & CDataSQL(strSTMDatFin)
      End If
      If (nSTMDaMagaz <> 0) Or (nSTMAMagaz <> 9999) Then
        strSQL1 += " AND keymag.km_magaz BETWEEN " & nSTMDaMagaz & " AND " & nSTMAMagaz
      End If
      If strSTMTipork <> "" Then
        strSQL1 += " AND keymag.km_tipork = " & CStrSQL(strSTMTipork) & _
          IIf(strSTMSerie <> "", " AND keymag.km_serie = " & CStrSQL(strSTMSerie), "").ToString
      End If
      If (nSTMCodmarcini <> 0) Or (nSTMCodmarcfin <> 999) Then
        strSQL1 += " AND artico.ar_codmarc BETWEEN " & nSTMCodmarcini & " AND " & nSTMCodmarcfin
      End If
      Select Case strCodart
        Case "A"
          If (strSTMDaCodart <> "".PadLeft(CLN__STD.CodartMaxLen, " "c)) Or (strSTMACodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strSQL1 += " AND keymag.km_codart BETWEEN " & CStrSQL(strSTMDaCodart) & " AND " & CStrSQL(strSTMACodart)
          End If
          If (nSTMDaFase <> 0) Or (nSTMAFase <> 9999) Then
            strSQL1 += " AND keymag.km_fase BETWEEN " & nSTMDaFase & " AND " & nSTMAFase
          End If
        Case "B"
          If nCodlsar <> 0 Then
            strSQL1 += _
              " AND CAST(km_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(km_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
      End Select
      Select Case strConto
        Case "A"
          If (lSTMDaConto <> 0) Or (lSTMAConto <> 999999999) Then
            strSQL1 += " AND keymag.km_conto BETWEEN " & lSTMDaConto & " AND " & lSTMAConto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL1 += " AND km_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      TraduciWhere(strQuery, strSQL1)
      '--------------------------------------------------------------------------------------------------------------
      '--- Riempie TTSTMATR da Testmag/Keymag/Movmag/Artico/Movmatr
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO " & strSTMTTStMatr & " (codditt, instid, tt_codart, tt_magaz, tt_matric, tt_aammgg," & _
        " tt_causale, tt_conto, tt_riferim, tt_tipork, tt_anno, tt_serie, tt_numdoc, tt_riga, tt_quant, tt_prezzo," & _
        " tt_valore, tt_carscar, tt_qtacar, tt_valcar, tt_fase)" & _
        " SELECT " & CStrSQL(strDitta) & ", " & lSTMIITTStMatr & ", km_codart, km_magaz, mma_matric, km_aammgg," & _
        " km_causale, km_conto, tm_riferim, km_tipork, km_anno, km_serie, km_numdoc, km_riga, mma_quant," & _
        " case when mm_quant <> 0 then (mm_valore / mm_quant) else 0 end, mm_valore, km_carscar, 0, 0, km_fase" & _
        " FROM " & strJoin & strSQL1
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Cancella le righe relative a matricole chiuse (in caso di 'A MATRICOLE APERTE')
      '--------------------------------------------------------------------------------------------------------------
      If nSTMQualeMatr = MATRAPERTE Then
        strSQL = "SELECT tt_codart, tt_fase, tt_magaz, tt_matric" & _
          " FROM " & strSTMTTStMatr & _
          " WHERE codditt = " & CStrSQL(strDitta) & _
          " AND instid = " & lSTMIITTStMatr & _
          " GROUP BY tt_codart, tt_fase, tt_magaz, tt_matric" & _
          " HAVING Round((Sum(tt_quant * tt_carscar)), 3) = 0"
        dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
        For i = 0 To dsTmp.Tables("TEMP").Rows.Count - 1
          With dsTmp.Tables("TEMP").Rows(i)
            strSQL = "DELETE FROM " & strSTMTTStMatr & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND instid = " & lSTMIITTStMatr & _
              " AND tt_codart = " & CStrSQL(!tt_codart) & _
              " AND tt_magaz = " & NTSCStr(!tt_magaz) & _
              " AND tt_fase = " & NTSCStr(!tt_fase) & _
              " AND tt_matric = " & CStrSQL(!tt_matric)
            Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
          End With
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Routine di raggruppamento
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT tt_codart, tt_fase, tt_magaz, tt_matric," & _
        " Round((Sum(tt_quant)), 3) As Quant, Sum(tt_quant * tt_prezzo) As Valcar" & _
        " FROM " & strSTMTTStMatr & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lSTMIITTStMatr & _
        " AND tt_carscar = 1" & _
        " GROUP BY tt_codart, tt_fase, tt_magaz, tt_matric" & _
        " ORDER BY tt_codart, tt_fase, tt_magaz, tt_matric"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
      For i = 0 To dsTmp.Tables("TEMP").Rows.Count - 1
        With dsTmp.Tables("TEMP").Rows(i)
          strSQL = "UPDATE " & strSTMTTStMatr & _
            " SET tt_qtacar = " & CDblSQL(NTSCDec(!Quant)) & "," & _
            " tt_valcar = " & CDblSQL(NTSCDec(!Valcar)) & _
            " WHERE codditt = " & CStrSQL(strDitta) & _
            " AND instid = " & lSTMIITTStMatr & _
            " AND tt_codart = " & CStrSQL(!tt_codart) & _
            " AND tt_magaz = " & NTSCStr(!tt_magaz) & _
            " AND tt_fase = " & NTSCStr(!tt_fase) & _
            " AND tt_matric = " & CStrSQL(!tt_matric)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla l'esistenza di record nella tabella temporanea TTSTMATR
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tt_codart FROM " & strSTMTTStMatr & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lSTMIITTStMatr & _
        " ORDER BY tt_codart, tt_fase, tt_magaz, tt_matric, tt_aammgg, tt_tipork, tt_anno, tt_serie, tt_numdoc, tt_riga"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
      If dsTmp.Tables("TEMP").Rows.Count = 0 Then
        strError = "Non esistono dati con queste caratteristiche."
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RiempiTTStMats(ByVal strDitta As String, ByVal strSTMTTStMats As String, _
                                              ByVal lSTMIITTStMats As Integer, _
                                              ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                              ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                              ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                              ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                              ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                              ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                              ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                              ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                              ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                              ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                              ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                              ByVal strQuery As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return RiempiTTStMats(strDitta, strSTMTTStMats, lSTMIITTStMats, strSTMDaMatr, strSTMAMatr, _
                            strSTMDatIni, strSTMDatFin, nSTMDaMagaz, nSTMAMagaz, _
                            strSTMDaCodart, strSTMACodart, nSTMDaFase, nSTMAFase, _
                            lSTMDaConto, lSTMAConto, strSTMTipork, strSTMSerie, _
                            nSTMCausale, nSTMGruppo, nSTMSotGru, nSTMCodmarcini, _
                            nSTMCodmarcfin, strSTMCodcfam, nSTMQualeMatr, strError, _
                            strQuery, "", "", "", "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
  Public Overridable Function RiempiTTStMats(ByVal strDitta As String, ByVal strSTMTTStMats As String, _
                                              ByVal lSTMIITTStMats As Integer, _
                                              ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                              ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                              ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                              ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                              ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                              ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                              ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                              ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                              ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                              ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                              ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                              ByVal strQuery As String, ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                              ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                              ByVal strClassificazioneLivello5 As String) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strSTMTTStMats, lSTMIITTStMats, strSTMDaMatr, strSTMAMatr, _
                            strSTMDatIni, strSTMDatFin, nSTMDaMagaz, nSTMAMagaz, _
                            strSTMDaCodart, strSTMACodart, nSTMDaFase, nSTMAFase, _
                            lSTMDaConto, lSTMAConto, strSTMTipork, strSTMSerie, _
                            nSTMCausale, nSTMGruppo, nSTMSotGru, nSTMCodmarcini, _
                            nSTMCodmarcfin, strSTMCodcfam, nSTMQualeMatr, strError, _
                            strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                            strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(24))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return RiempiTTStMats(strDitta, strSTMTTStMats, lSTMIITTStMats, strSTMDaMatr, strSTMAMatr, _
                            strSTMDatIni, strSTMDatFin, nSTMDaMagaz, nSTMAMagaz, _
                            strSTMDaCodart, strSTMACodart, nSTMDaFase, nSTMAFase, _
                            lSTMDaConto, lSTMAConto, strSTMTipork, strSTMSerie, _
                            nSTMCausale, nSTMGruppo, nSTMSotGru, nSTMCodmarcini, _
                            nSTMCodmarcfin, strSTMCodcfam, nSTMQualeMatr, strError, _
                            strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                            strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                            "", 0, "", 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RiempiTTStMats(ByVal strDitta As String, ByVal strSTMTTStMats As String, _
                                             ByVal lSTMIITTStMats As Integer, _
                                             ByVal strSTMDaMatr As String, ByVal strSTMAMatr As String, _
                                             ByVal strSTMDatIni As String, ByVal strSTMDatFin As String, _
                                             ByVal nSTMDaMagaz As Integer, ByVal nSTMAMagaz As Integer, _
                                             ByVal strSTMDaCodart As String, ByVal strSTMACodart As String, _
                                             ByVal nSTMDaFase As Integer, ByVal nSTMAFase As Integer, _
                                             ByVal lSTMDaConto As Integer, ByVal lSTMAConto As Integer, _
                                             ByVal strSTMTipork As String, ByVal strSTMSerie As String, _
                                             ByVal nSTMCausale As Integer, ByVal nSTMGruppo As Integer, _
                                             ByVal nSTMSotGru As Integer, ByVal nSTMCodmarcini As Integer, _
                                             ByVal nSTMCodmarcfin As Integer, ByVal strSTMCodcfam As String, _
                                             ByVal nSTMQualeMatr As Integer, ByRef strError As String, _
                                             ByVal strQuery As String, ByVal strClassificazioneLivello1 As String, ByVal strClassificazioneLivello2 As String, _
                                             ByVal strClassificazioneLivello3 As String, ByVal strClassificazioneLivello4 As String, _
                                             ByVal strClassificazioneLivello5 As String, _
                                             ByVal strCodart As String, ByVal nCodlsar As Integer, _
                                             ByVal strConto As String, ByVal nCodlsel As Integer) As Boolean
    Dim strSQL As String
    Dim strSQL1 As String
    Dim strJoin As String
    Dim dsTmp As DataSet = Nothing
    Dim i As Integer
    'nSTMQualeMatr Flag di selezione del tipo di matricole
    '0 = Matricole aperte e chiuse
    '1 = Matricole aperte
    '2 = Saldo matricole aperte
    'Const MATRAPCHIU As Integer = 0
    'Const MATRAPERTE As Integer = 1
    'Const MATRSALAP As Integer = 2
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strSTMTTStMats, lSTMIITTStMats, strSTMDaMatr, strSTMAMatr, _
                            strSTMDatIni, strSTMDatFin, nSTMDaMagaz, nSTMAMagaz, _
                            strSTMDaCodart, strSTMACodart, nSTMDaFase, nSTMAFase, _
                            lSTMDaConto, lSTMAConto, strSTMTipork, strSTMSerie, _
                            nSTMCausale, nSTMGruppo, nSTMSotGru, nSTMCodmarcini, _
                            nSTMCodmarcfin, strSTMCodcfam, nSTMQualeMatr, strError, _
                            strQuery, strClassificazioneLivello1, strClassificazioneLivello2, _
                            strClassificazioneLivello3, strClassificazioneLivello4, strClassificazioneLivello5, _
                            strCodart, nCodlsar, strConto, nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strError = NTSCStr(oIn(24))
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      strJoin = CreaJoin()
      '--------------------------------------------------------------------------------------------------------------
      '--- Cancella il contenuto della tebella temporanea TTSTMATS
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM " & strSTMTTStMats & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lSTMIITTStMats
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- COMPONE LA STRINGA DI CODA
      '--------------------------------------------------------------------------------------------------------------
      strSQL1 = " WHERE keymag.codditt = " & CStrSQL(strDitta) & _
        " AND movmatr.mma_matric BETWEEN " & CStrSQL(strSTMDaMatr) & " AND " & CStrSQL(strSTMAMatr) & _
        " AND artico.ar_gestmatr = 'S'" & _
        " AND artico.ar_stasche = 'S'" & _
        IIf(nSTMCausale > 0, " AND keymag.km_causale = " & nSTMCausale, "").ToString & _
        IIf(nSTMGruppo > 0, " AND artico.ar_gruppo = " & nSTMGruppo, "").ToString & _
        IIf(nSTMSotGru > 0, " AND artico.ar_sotgru = " & nSTMSotGru, "").ToString & _
        IIf(strSTMCodcfam <> "", " AND artico.ar_famprod = " & CStrSQL(strSTMCodcfam), "").ToString & _
        IIf(strClassificazioneLivello1.Trim <> "", " AND artico.ar_codcla1 = " & CStrSQL(strClassificazioneLivello1), "").ToString & _
        IIf(strClassificazioneLivello2.Trim <> "", " AND artico.ar_codcla2 = " & CStrSQL(strClassificazioneLivello2), "").ToString & _
        IIf(strClassificazioneLivello3.Trim <> "", " AND artico.ar_codcla3 = " & CStrSQL(strClassificazioneLivello3), "").ToString & _
        IIf(strClassificazioneLivello4.Trim <> "", " AND artico.ar_codcla4 = " & CStrSQL(strClassificazioneLivello4), "").ToString & _
        IIf(strClassificazioneLivello5.Trim <> "", " AND artico.ar_codcla5 = " & CStrSQL(strClassificazioneLivello5), "").ToString
      If NTSCDate(strSTMDatIni) <> New Date(1900, 1, 1) Or NTSCDate(strSTMDatFin) <> New Date(2099, 12, 31) Then
        strSQL1 += " AND keymag.km_aammgg BETWEEN " & CDataSQL(strSTMDatIni) & " AND " & CDataSQL(strSTMDatFin)
      End If
      If (nSTMDaMagaz <> 0) Or (nSTMAMagaz <> 9999) Then
        strSQL1 += " AND keymag.km_magaz BETWEEN " & nSTMDaMagaz & " AND " & nSTMAMagaz
      End If
      If strSTMTipork <> "" Then
        strSQL1 += " AND keymag.km_tipork = " & CStrSQL(strSTMTipork) & _
          IIf(strSTMSerie <> "", " AND keymag.km_serie = " & CStrSQL(strSTMSerie), "").ToString
      End If
      If (nSTMCodmarcini <> 0) Or (nSTMCodmarcfin <> 999) Then
        strSQL1 += " AND artico.ar_codmarc BETWEEN " & nSTMCodmarcini & " AND " & nSTMCodmarcfin
      End If
      Select Case strCodart
        Case "B"
          If nCodlsar <> 0 Then
            strSQL1 += _
              " AND CAST(km_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(km_fase AS VARCHAR(4))" & _
              " IN (SELECT CAST(lsa_codart AS VARCHAR(" & CLN__STD.CodartMaxLen & ")) + '.' + CAST(lsa_fase AS VARCHAR(4)) FROM listsar" & _
              " WHERE codditt = " & CStrSQL(strDitta) & _
              " AND lsa_codlsar = " & nCodlsar & ")"
          End If
        Case Else
          If (strSTMDaCodart <> "".PadLeft(CLN__STD.CodartMaxLen, " "c)) Or (strSTMACodart <> "".PadLeft(CLN__STD.CodartMaxLen, "z"c)) Then
            strSQL1 += " AND keymag.km_codart BETWEEN " & CStrSQL(strSTMDaCodart) & " AND " & CStrSQL(strSTMACodart)
          End If
          If (nSTMDaFase <> 0) Or (nSTMAFase <> 9999) Then
            strSQL1 += " AND keymag.km_fase BETWEEN " & nSTMDaFase & " AND " & nSTMAFase
          End If
      End Select
      Select Case strConto
        Case "A"
          If (lSTMDaConto <> 0) Or (lSTMAConto <> 999999999) Then
            strSQL1 += " AND keymag.km_conto BETWEEN " & lSTMDaConto & " AND " & lSTMAConto
          End If
        Case "B"
          If nCodlsel <> 0 Then
            strSQL1 += " AND km_conto IN (SELECT lse_conto FROM listsel" & _
                                       " WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lse_codlsel = " & nCodlsel & ")"
          End If
      End Select
      TraduciWhere(strQuery, strSQL1)
      '--------------------------------------------------------------------------------------------------------------
      '--- INSERT INTO TTSTLOCS da Movmag/Keymag/Testmag/Artico/Matric
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "INSERT INTO " & strSTMTTStMats & " (codditt, instid, tt_codart, tt_magaz, tt_matric, tt_quant," & _
        " tt_valore, tt_qtacar, tt_valcar, tt_fase)" & _
        " SELECT " & CStrSQL(strDitta) & ", " & lSTMIITTStMats & ", km_codart, km_magaz, mma_matric," & _
        " Round((Sum(mma_quant * km_carscar)), 3), 0, 0, 0, km_fase" & _
        " FROM " & strJoin & strSQL1 & _
        " GROUP BY km_codart, km_fase, km_magaz, mma_matric"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiunge i carichi da Movmag/Keymag/Testmag/Artico/Matric
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT mma_quant As Qtacar, ((mm_valore * mma_quant) / mm_quant) As Valcar," & _
        " movmatr.mma_matric, keymag.km_magaz, keymag.km_fase, keymag.km_codart" & _
        " FROM " & strJoin & strSQL1 & _
        " AND km_carscar = 1" & _
        " AND mm_quant <> 0"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
      For i = 0 To dsTmp.Tables("TEMP").Rows.Count - 1
        With dsTmp.Tables("TEMP").Rows(i)
          strSQL = "UPDATE " & strSTMTTStMats & _
            " SET tt_qtacar = Round((tt_qtacar + " & CDblSQL(NTSCDec(!Qtacar)) & "), 3)," & _
            " tt_valcar = tt_valcar + " & CDblSQL(NTSCDec(!Valcar)) & _
            " WHERE " & strSTMTTStMats & ".instid = " & lSTMIITTStMats & _
            " AND " & strSTMTTStMats & ".codditt = " & CStrSQL(strDitta) & _
            " AND " & strSTMTTStMats & ".tt_matric = " & CStrSQL(!mma_matric) & _
            " AND " & strSTMTTStMats & ".tt_magaz = " & NTSCStr(!km_magaz) & _
            " AND " & strSTMTTStMats & ".tt_fase = " & NTSCStr(!km_fase) & _
            " AND " & strSTMTTStMats & ".tt_codart = " & CStrSQL(!km_codart)
          Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      '--- Determina il valore dividendo il totale del valore dei carichi per il totale dei carichi
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "UPDATE " & strSTMTTStMats & _
        " SET tt_valore = (tt_valcar / tt_qtacar)" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lSTMIITTStMats & _
        " AND tt_qtacar <> 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      strSQL = "UPDATE " & strSTMTTStMats & _
        " SET tt_valore = 0" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lSTMIITTStMats & _
        " AND tt_qtacar = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Cancella le matricole che sono a zero
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "DELETE FROM " & strSTMTTStMats & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lSTMIITTStMats & _
        " AND tt_quant = 0"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla l'esistenza di record nella tabella temporanea TTSTMATS
      '--------------------------------------------------------------------------------------------------------------
      strSQL = "SELECT TOP 1 tt_codart FROM " & strSTMTTStMats & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND instid = " & lSTMIITTStMats & _
        " ORDER BY tt_codart, tt_fase, tt_magaz, tt_matric"
      dsTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TEMP")
      If dsTmp.Tables("TEMP").Rows.Count = 0 Then
        strError = "Non esistono dati con queste caratteristiche."
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function CreaJoin() As String
    Try
      Return "(((testmag INNER JOIN movmag ON testmag.codditt = movmag.codditt" & _
        " AND testmag.tm_numdoc = movmag.mm_numdoc" & _
        " AND testmag.tm_serie = movmag.mm_serie" & _
        " AND testmag.tm_anno = movmag.mm_anno" & _
        " AND testmag.tm_tipork = movmag.mm_tipork)" & _
        " INNER JOIN keymag ON movmag.codditt = keymag.codditt" & _
        " AND movmag.mm_riga = keymag.km_riga" & _
        " AND movmag.mm_numdoc = keymag.km_numdoc" & _
        " AND movmag.mm_serie = keymag.km_serie" & _
        " AND movmag.mm_anno = keymag.km_anno" & _
        " AND movmag.mm_tipork = keymag.km_tipork)" & _
        " INNER JOIN movmatr ON keymag.codditt = movmatr.codditt" & _
        " AND keymag.km_riga = movmatr.mma_riga" & _
        " AND keymag.km_numdoc = movmatr.mma_numdoc" & _
        " AND keymag.km_serie = movmatr.mma_serie" & _
        " AND keymag.km_anno = movmatr.mma_anno" & _
        " AND keymag.km_tipork = movmatr.mma_tipork)" & _
        " INNER JOIN artico ON artico.codditt = movmag.codditt AND artico.ar_codart = movmag.mm_codart"

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function

#Region "Datawarehouse"
  Public Overridable Function GetScenari(ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DISTINCT tabdime.tb_scenario as scenario FROM tabdime ORDER BY tb_scenario"
      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABDIME")

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function InsertTabscen(ByVal strScenario As String, ByVal strDescScenari As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO tabscen (tb_codscen, tb_desscen, tb_elabora, tb_da, tb_a) " & _
        " VALUES (" & strScenario & ", " & CStrSQL(strDescScenari) & ", 'S', -60, 0)"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  'strSQLH = SELECT elenco fact dimensioni
  'FROM strSQLANAGRA & strSQLARTICO & strSQLONARTICO & strSQLFINE & strSQLFINE2 & strSQLONANAGRA
  'WHERE strAlimSelwherem 
  'GROUP BY strStringaGroupby
  'HAVING strStringaHaving
  'ORDER BY strStringaOrderby
  'Per personalizzare vedi funzioni che iniziano per RidefinisciQueryXXXXXX
  'Tra cui RidefinisciQuery_ che è una specie del BeforeColUpdate
  Public Overridable Function CreaQuery(ByVal dsAggiorna As DataSet, ByRef dsDwarehe As DataSet, _
                                      ByRef dttPar As DataTable) As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer
    Dim strSQL As String = ""
    Dim strOperat As String = ""
    Dim lScenarioNTS As Integer = 1000

    'dovrebbe essere dat per cli pro doc
    Try
      SvuotaCreaRigadttPar(dttPar, "strSQLANAGRA", "", "R")
      SvuotaCreaRigadttPar(dttPar, "strSQLARTICO", "", "R")
      SvuotaCreaRigadttPar(dttPar, "strSQLONANAGRA", "", "R")
      SvuotaCreaRigadttPar(dttPar, "strSQLONARTICO", "", "R")
      SvuotaCreaRigadttPar(dttPar, "strStringa", "", "R")
      SvuotaCreaRigadttPar(dttPar, "strStringaGroupby", "", "R")
      SvuotaCreaRigadttPar(dttPar, "strStringaOrderby", "", "R")
      SvuotaCreaRigadttPar(dttPar, "strSQLFINE", "", "R")
      SvuotaCreaRigadttPar(dttPar, "strSQLFINE2", "", "R")
      SvuotaCreaRigadttPar(dttPar, "strWHERE2", "", "R")
      SvuotaCreaRigadttPar(dttPar, "strStringaHaving", "", "R")
      SvuotaCreaRigadttPar(dttPar, "strTmpAnaext", "", "R")
      SvuotaCreaRigadttPar(dttPar, "nZeri", "", "R") 'per StringaNumerica
      SvuotaCreaRigadttPar(dttPar, "strTmp", "", "R") 'per StringaNumerica
      SvuotaCreaRigadttPar(dttPar, "strErr", "", "R") 'popolo per stringhe errore specie di log
      SvuotaCreaRigadttPar(dttPar, "bCractOpp", "0", "R")
      SvuotaCreaRigadttPar(dttPar, "strPosSelect", "0", "R")
      SvuotaCreaRigadttPar(dttPar, "strSQLValoriZero", "", "R")

      '-----------------------------------------------------------------------------------------
      If Not QueryGenerica(dsAggiorna, dttPar) Then
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      If NTSCStr(LeggidttPar(dttPar, "strAlimSolofsel")) = "N" Then
        dtrTmp = dsAggiorna.Tables("TABDIME").Select("tb_gruppo = 5")
      Else
        dtrTmp = dsAggiorna.Tables("TABDIME").Select("xx_selez = 'S' AND tb_gruppo = 5")
      End If
      ScrividttPar(dttPar, "strSQLH", "SELECT")
      For i = 0 To dtrTmp.Length - 1
        If Not RidefinisciQuerySelectFieldName(dttPar) Then Continue For
        'modifica per supportare nei dati dwh (gruppo 5) campi che usano le clausole base count(, sum(, min( e max(
        If NTSCStr(dtrTmp(i)!tb_fieldname).ToLower.StartsWith("count(") Or _
           NTSCStr(dtrTmp(i)!tb_fieldname).ToLower.StartsWith("sum(") Or _
           NTSCStr(dtrTmp(i)!tb_fieldname).ToLower.StartsWith("min(") Or _
           NTSCStr(dtrTmp(i)!tb_fieldname).ToLower.StartsWith("max(") Then
          'se NON doppio click aggiunge i count(, ...
          If Not LeggidttPar(dttPar, "bCellDblClick") = "-1" Then
            AccodadttPar(dttPar, "strSQLH", " " & NTSCStr(dtrTmp(i)!tb_fieldname) & " As " & NTSCStr(dtrTmp(i)!tb_alias) & ",")
          End If
        Else
          'se doppio click non aggiunge i Sum
          If LeggidttPar(dttPar, "bCellDblClick") = "-1" Then
            AccodadttPar(dttPar, "strSQLH", " " & NTSCStr(dtrTmp(i)!tb_fieldname) & " As " & NTSCStr(dtrTmp(i)!tb_alias) & ",")
          Else
            strOperat = "Sum"
            If (dttPar.Rows(0).Item(1).ToString = "11") And _
               (NTSCStr(dtrTmp(i)!tb_fieldname).ToLower = "dwarehe.dw_colli") Then strOperat = "MIN"
            AccodadttPar(dttPar, "strSQLH", " " & strOperat & "(" & NTSCStr(dtrTmp(i)!tb_fieldname) & ") As " & NTSCStr(dtrTmp(i)!tb_alias) & ",")
          End If
        End If
      Next
      If LeggidttPar(dttPar, "bEscludiValZero") = "-1" Then
        'filtro solo dati senza valori a zero esclude count(*)
        dtrTmp = dsAggiorna.Tables("TABDIME").Select("xx_selez = 'S' AND tb_gruppo = 5")
        For i = 0 To dtrTmp.Length - 1
          If InStr(1, NTSCStr(dtrTmp(i)!tb_fieldname), "count") = 0 Then 'NON fact count
            If LeggidttPar(dttPar, "strSQLValoriZero") = "" Then ScrividttPar(dttPar, "strSQLValoriZero", " AND NOT (")
            If LeggidttPar(dttPar, "strSQLValoriZero") = " AND NOT (" Then
              AccodadttPar(dttPar, "strSQLValoriZero", NTSCStr(dtrTmp(i)!tb_fieldname) & " = 0 ")
            Else
              AccodadttPar(dttPar, "strSQLValoriZero", "AND " & NTSCStr(dtrTmp(i)!tb_fieldname) & " = 0 ")
            End If
          End If
        Next
        If LeggidttPar(dttPar, "strSQLValoriZero") <> "" Then AccodadttPar(dttPar, "strSQLValoriZero", ")")
      End If

      'aggiungo nel doppioclick in numero di registrazione per analitica vecchia
      If LeggidttPar(dttPar, "bCellDblClick") = "-1" And LeggidttPar(dttPar, "nTipoScenario") = "7" Then
        AccodadttPar(dttPar, "strSQLH", " priana.pa_numreg As numregcg, dwarehe.dw_rigaoff As rigaoff,")
      End If

      'aggiungo nel doppioclick in numero di registrazione per contab generale
      If LeggidttPar(dttPar, "bCellDblClick") = "-1" And LeggidttPar(dttPar, "nTipoScenario") = "8" Then
        AccodadttPar(dttPar, "strSQLH", " prinot.pn_numreg As numregcg,")
      End If

      If LeggidttPar(dttPar, "bCellDblClick") = "-1" And (LeggidttPar(dttPar, "nTipoScenario") = "5" Or LeggidttPar(dttPar, "nTipoScenario") = "10") Then
        AccodadttPar(dttPar, "strSQLH", " leads.le_codlead As cod_lead,")
      End If

      'aggiungo nel doppioclick in numero di registrazione per analitica CADC
      If LeggidttPar(dttPar, "bCellDblClick") = "-1" And LeggidttPar(dttPar, "nTipoScenario") = "12" Then
        AccodadttPar(dttPar, "strSQLH", " priana2.pa2_numreg As numregcg, priana2.pa2_riga as riga, dwarehe.dw_rigaoff As rigaoff, priana2.pa2_mmtipork As tipo_record, priana2.pa2_idlavmano As idlavmano,")
      End If

      'se attivo l'opzione prendo la struttura dei dati per dati storicizzati
      If LeggidttPar(dttPar, "bTop0") = "-1" Then
        ScrividttPar(dttPar, "strPosSelect", NTSCStr(InStr(1, LCase(LeggidttPar(dttPar, "strSQLH")), "select")))
        If NTSCInt(LeggidttPar(dttPar, "strPosSelect")) <> 0 Then
          ScrividttPar(dttPar, "strPosSelect", NTSCStr(NTSCInt(LeggidttPar(dttPar, "strPosSelect")) - 1))
          ScrividttPar(dttPar, "strSQLH", Left(LeggidttPar(dttPar, "strSQLH"), NTSCInt(LeggidttPar(dttPar, "strPosSelect")) + 6) & " TOP 0 " & Right(LeggidttPar(dttPar, "strSQLH"), Len(LeggidttPar(dttPar, "strSQLH")) - (NTSCInt(LeggidttPar(dttPar, "strPosSelect")) + 6)))
        End If
      End If
      RidefinisciQuerySelect(dttPar)

      Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
        Case 1
          AccodadttPar(dttPar, "strSQLH", " " & LeggidttPar(dttPar, "strStringa") & " FROM " & LeggidttPar(dttPar, "strSQLANAGRA") & " INNER JOIN ")
        Case 3, 7, 12, 13
          AccodadttPar(dttPar, "strSQLH", " " & LeggidttPar(dttPar, "strStringa") & " FROM " & LeggidttPar(dttPar, "strSQLANAGRA") & " LEFT JOIN ")
        Case 2, 4, 5, 8, 9, 10, 11, 14
          AccodadttPar(dttPar, "strSQLH", " " & LeggidttPar(dttPar, "strStringa") & " FROM " & LeggidttPar(dttPar, "strSQLANAGRA"))
        Case Else
          AccodadttPar(dttPar, "strSQLH", " " & LeggidttPar(dttPar, "strStringa") & " FROM ")
      End Select
      RidefinisciQueryFrom(dttPar)
      '-----------------------------------------------------------------------------------------
      If NTSCInt(LeggidttPar(dttPar, "nTipoScenario")) <= lScenarioNTS Then
        If Len(LeggidttPar(dttPar, "strSQLONARTICO")) <> 0 Then ScrividttPar(dttPar, "strSQLANAGRA", " (" & LeggidttPar(dttPar, "strSQLANAGRA"))
        Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
          Case 1, 3, 7, 12, 13
            ScrividttPar(dttPar, "strSQLFINE", " ON dwarehe.dw_codart = artico.ar_codart AND dwarehe.codditt = artico.codditt)")
          Case 2, 4, 5, 8, 9, 10, 11, 14
            ScrividttPar(dttPar, "strSQLFINE", "")
        End Select
        RidefinisciQueryOnArtico(dttPar)
        Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
          Case 1
            If Not (CBool(LeggidttPar(dttPar, "bGestAnaextC")) And (InStr(1, LCase(Left(LeggidttPar(dttPar, "strSQLH"), InStr(1, LCase(LeggidttPar(dttPar, "strSQLH")), " from "))), "anaext") <> 0)) Then
              ScrividttPar(dttPar, "strSQLFINE2", " INNER JOIN anagra ON dwarehe.dw_conto = anagra.an_conto AND dwarehe.codditt = anagra.codditt)" & _
                " LEFT JOIN artroot ON artico.codditt = artroot.codditt AND artico.ar_codroot = artroot.arr_codroot")
            Else
              ScrividttPar(dttPar, "strSQLFINE2", " INNER JOIN anagra ON dwarehe.dw_conto = anagra.an_conto AND dwarehe.codditt = anagra.codditt)" & _
                " LEFT JOIN anaext ON anagra.an_conto = anaext.ax_conto AND anagra.codditt = anaext.codditt)" & _
                " LEFT JOIN artroot ON artico.codditt = artroot.codditt AND artico.ar_codroot = artroot.arr_codroot")
            End If
          Case 2
            ScrividttPar(dttPar, "strSQLFINE2", " INNER JOIN leads ON dwarehe.dw_conto = leads.le_codlead AND dwarehe.codditt = leads.codditt ")
          Case 3
            ScrividttPar(dttPar, "strSQLFINE2", " INNER JOIN opportun ON dwarehe.dw_conto = opportun.op_codoppo AND dwarehe.codditt = opportun.codditt)" & _
              " INNER JOIN leads ON opportun.op_codlead = leads.le_codlead AND opportun.codditt = leads.codditt")
          Case 4
            ScrividttPar(dttPar, "strSQLFINE2", " INNER JOIN cract ON dwarehe.dw_conto = cract.ca_codcrac AND dwarehe.codditt = cract.codditt)" & _
              " INNER JOIN leads ON cract.ca_codlead = leads.le_codlead AND cract.codditt = leads.codditt)" & _
              " LEFT JOIN opportun ON cract.codditt = opportun.codditt AND cract.ca_codoppo = opportun.op_codoppo")
          Case 5
            ScrividttPar(dttPar, "strSQLFINE2", " INNER JOIN testoff ON dwarehe.dw_tipork = testoff.td_tipork AND dwarehe.dw_anno = testoff.td_anno AND dwarehe.dw_serie = testoff.td_serie AND dwarehe.dw_numdoc = testoff.td_numord AND dwarehe.dw_riga = testoff.td_vers AND dwarehe.codditt = testoff.codditt)" & _
              " INNER JOIN leads ON testoff.td_codlead = leads.le_codlead AND testoff.codditt = leads.codditt)" & _
              " LEFT JOIN opportun ON testoff.codditt = opportun.codditt AND testoff.td_codoppo = opportun.op_codoppo")
          Case 7
            ScrividttPar(dttPar, "strSQLFINE2", " LEFT JOIN anagra ON dwarehe.codditt = anagra.codditt AND dwarehe.dw_conto = anagra.an_conto)" & _
              " LEFT JOIN commess ON dwarehe.codditt = commess.codditt AND dwarehe.dw_commeca = commess.co_comme) " & _
              " LEFT JOIN priana ON dwarehe.codditt = priana.codditt AND dwarehe.dw_rigaoff = priana.pa_progr) ")
          Case 8
            ScrividttPar(dttPar, "strSQLFINE2", " INNER JOIN anagra ON dwarehe.codditt = anagra.codditt AND dwarehe.dw_conto = anagra.an_conto)" & _
              " INNER JOIN prinot ON dwarehe.codditt = prinot.codditt AND dwarehe.dw_datmov = prinot.pn_datreg AND dwarehe.dw_numdoc = prinot.pn_numreg AND dwarehe.dw_riga = prinot.pn_riga)")
          Case 9
            ScrividttPar(dttPar, "strSQLFINE2", " INNER JOIN attconscd ON dwarehe.codditt = attconscd.codditt AND dwarehe.dw_numdoc = attconscd.acd_codattc AND dwarehe.dw_riga = attconscd.acd_riga)" & _
              " INNER JOIN attconsc ON attconscd.codditt = attconsc.codditt AND attconscd.acd_codattc = attconsc.ac_codattc)" & _
              " INNER JOIN nnchiam ON attconsc.codditt = nnchiam.codditt AND attconsc.ac_codchia = nnchiam.op_codchia)" & _
              " INNER JOIN leads ON nnchiam.op_codlead = leads.le_codlead AND nnchiam.codditt = leads.codditt)" & _
              " LEFT JOIN artico ON nnchiam.op_codart = artico.ar_codart AND nnchiam.codditt = artico.codditt)" & _
              " LEFT JOIN artroot ON artico.codditt = artroot.codditt AND artico.ar_codroot = artroot.arr_codroot)")
          Case 10
            ScrividttPar(dttPar, "strSQLFINE2", " INNER JOIN movoff ON dwarehe.dw_tipork = movoff.mo_tipork AND dwarehe.dw_anno = movoff.mo_anno AND dwarehe.dw_serie = movoff.mo_serie AND dwarehe.dw_numdoc = movoff.mo_numord AND dwarehe.dw_riga = movoff.mo_vers AND dwarehe.dw_rigaoff = movoff.mo_riga AND dwarehe.codditt = movoff.codditt)" & _
              " INNER JOIN testoff ON movoff.mo_tipork = testoff.td_tipork AND movoff.mo_anno = testoff.td_anno AND movoff.mo_serie = testoff.td_serie AND movoff.mo_numord = testoff.td_numord AND movoff.mo_vers = testoff.td_vers AND movoff.codditt = testoff.codditt)" & _
              " INNER JOIN artico ON movoff.mo_codart = artico.ar_codart AND movoff.codditt = artico.codditt)" & _
              " INNER JOIN leads ON testoff.td_codlead = leads.le_codlead AND testoff.codditt = leads.codditt)" & _
              " LEFT JOIN opportun ON testoff.codditt = opportun.codditt AND testoff.td_codoppo = opportun.op_codoppo)" & _
              " LEFT JOIN artroot ON artico.codditt = artroot.codditt AND artico.ar_codroot = artroot.arr_codroot)" & _
              " LEFT JOIN tabstag tabstag2 ON artico.ar_codstag = tabstag2.tb_codstag)")
          Case 11
            ScrividttPar(dttPar, "strSQLFINE2", " INNER JOIN scaden ON dwarehe.codditt = scaden.codditt AND dwarehe.dw_conto = scaden.sc_conto AND dwarehe.dw_annotco = scaden.sc_annpar AND dwarehe.dw_serie = scaden.sc_alfpar AND dwarehe.dw_numdoc = scaden.sc_numpar AND dwarehe.dw_riga = scaden.sc_numrata AND dwarehe.dw_integr = scaden.sc_integr)" & _
              " LEFT JOIN anagra ON dwarehe.codditt = anagra.codditt AND dwarehe.dw_conto = anagra.an_conto)")
          Case 12
            ScrividttPar(dttPar, "strSQLFINE2", " LEFT JOIN anagra ON dwarehe.codditt = anagra.codditt AND dwarehe.dw_conto = anagra.an_conto)" & _
              " LEFT JOIN commess ON dwarehe.codditt = commess.codditt AND dwarehe.dw_commeca = commess.co_comme) " & _
              " LEFT JOIN priana2 ON dwarehe.codditt = priana2.codditt AND dwarehe.dw_idpriana2 = priana2.pa2_progr AND dwarehe.dw_idrigarg = priana2.pa2_rigarg) " & _
              " LEFT JOIN priana2 priana2b ON priana2.codditt = priana2b.codditt AND priana2.pa2_progr = priana2b.pa2_progr AND priana2.pa2_idrigacontrop = priana2b.pa2_rigarg) ")
          Case 13
            ScrividttPar(dttPar, "strSQLFINE2", " LEFT JOIN anagra ON dwarehe.codditt = anagra.codditt AND dwarehe.dw_conto = anagra.an_conto)" & _
              " LEFT JOIN commess ON dwarehe.codditt = commess.codditt AND dwarehe.dw_commeca = commess.co_comme) " & _
              " LEFT JOIN priana2 ON dwarehe.codditt = priana2.codditt AND dwarehe.dw_idpriana2 = priana2.pa2_progr AND dwarehe.dw_idrigarg = priana2.pa2_rigarg) " & _
              " LEFT JOIN priana2 priana2b ON priana2.codditt = priana2b.codditt AND priana2.pa2_progr = priana2b.pa2_progr AND priana2.pa2_idrigacontrop = priana2b.pa2_rigarg) " & _
              " LEFT JOIN lavmano ON priana2.codditt = lavmano.codditt AND priana2.pa2_idlavmano = lavmano.lcm_progr) " & _
              " LEFT JOIN commess commess2 ON lavmano.codditt = commess.codditt AND lavmano.lcm_commeca = commess.co_comme) ")
          Case 14
            ScrividttPar(dttPar, "strSQLFINE2", " LEFT JOIN scaden ON dwarehe.codditt = scaden.codditt AND dwarehe.dw_conto = scaden.sc_conto AND dwarehe.dw_annotco = scaden.sc_annpar AND dwarehe.dw_serie = scaden.sc_alfpar AND dwarehe.dw_numdoc = scaden.sc_numpar AND dwarehe.dw_riga = scaden.sc_numrata AND dwarehe.dw_integr = scaden.sc_integr)" & _
              " LEFT JOIN anagra ON dwarehe.codditt = anagra.codditt AND dwarehe.dw_conto = anagra.an_conto)" & _
              " LEFT JOIN scadenec ON dwarehe.codditt = scadenec.codditt AND dwarehe.dw_coddest2 = scadenec.sc_progr)")
          Case Else
            RidefinisciQueryInnerLeftJoin(dttPar)
        End Select
        If Len(LeggidttPar(dttPar, "strSQLONANAGRA")) <> 0 Then AccodadttPar(dttPar, "strSQLFINE2", ")")

        RidefinisciQueryBeforeComponiSelectFrom(dttPar)
      End If

      '-----------------------------------------------------------------------------------------
      QueryGenericaWhere(dsAggiorna, dttPar)
      '-----------------------------------------------------------------------------------------

      If NTSCInt(LeggidttPar(dttPar, "nTipoScenario")) <= lScenarioNTS Then
        AccodadttPar(dttPar, "strSQLH", _
          LeggidttPar(dttPar, "strSQLARTICO") & _
          LeggidttPar(dttPar, "strSQLONARTICO") & _
          LeggidttPar(dttPar, "strSQLFINE") & _
          LeggidttPar(dttPar, "strSQLFINE2") & _
          LeggidttPar(dttPar, "strSQLONANAGRA"))
      Else
        AccodadttPar(dttPar, "strSQLH", _
          LeggidttPar(dttPar, "strSQLFromScenario"))
      End If
      AccodadttPar(dttPar, "strSQLH", " WHERE dw_scenario = " & LeggidttPar(dttPar, "nTipoScenario"))
      'non più significativo dw_tipo
      'If LeggidttPar(dttPar, "nTipoScenario") <> "1" Then AccodadttPar(dttPar, "strSQLH", " AND dw_tipo = '" & Analisi(NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))) & "'")
      '-----------------------------------------------------------------------------------------
      '--- Controlla se è da filtrare anche la ditta
      '-----------------------------------------------------------------------------------------
      If LeggidttPar(dttPar, "strAlimMultid") = "N" Then AccodadttPar(dttPar, "strSQLH", " AND dwarehe.codditt = " & CStrSQL(LeggidttPar(dttPar, "strDitta")))
      '-----------------------------------------------------------------------------------------
      '--- se nella modale 'Altre impostazioni statistica' sono state inserite delle
      '--- 'Condizioni aggiuntive', le accoda alla stringa di selezione
      '-----------------------------------------------------------------------------------------
      If LeggidttPar(dttPar, "strAlimSelwherem") <> "" Then AccodadttPar(dttPar, "strSQLH", " " & LeggidttPar(dttPar, "strAlimSelwherem"))
      'escludi valori a zero
      If LeggidttPar(dttPar, "bEscludiValZero") = "-1" Then AccodadttPar(dttPar, "strSQLH", " " & LeggidttPar(dttPar, "strSQLValoriZero"))
      '-----------------------------------------------------------------------------------------
      '--- Se esiste la gestione delle estensioni e nella query esiste nella SELECT la tabella
      '--- ANAEXT, aggiunge alla WHERE 'anaext.ax_coddest = 0'
      '-----------------------------------------------------------------------------------------
      If (CBool(LeggidttPar(dttPar, "bGestAnaextC")) = True) _
        And (InStr(1, LCase(Left(LeggidttPar(dttPar, "strSQLH"), InStr(1, LCase(LeggidttPar(dttPar, "strSQLH")), " from "))), "anaext") <> 0) Then
        AccodadttPar(dttPar, "strSQLH", " AND anaext.ax_coddest = 0")
      End If
      '-----------------------------------------------------------------------------------------
      RidefinisciQueryWhere(dttPar)
      '-----------------------------------------------------------------------------------------
      ScrividttPar(dttPar, "strStringaGroupby", Left(LeggidttPar(dttPar, "strStringaGroupby"), Len(LeggidttPar(dttPar, "strStringaGroupby")) - 2))
      ScrividttPar(dttPar, "strStringaOrderby", Left(LeggidttPar(dttPar, "strStringaOrderby"), Len(LeggidttPar(dttPar, "strStringaOrderby")) - 2))
      RidefinisciQueryWhereGroupbyHavingOrderby(dttPar)
      '-----------------------------------------------------------------------------------------
      AccodadttPar(dttPar, "strSQLH", LeggidttPar(dttPar, "strWHERE2") & " GROUP BY " & LeggidttPar(dttPar, "strStringaGroupby"))
      RidefinisciQueryGroupBy(dttPar)
      If LeggidttPar(dttPar, "strStringaHaving") <> " HAVING " Then AccodadttPar(dttPar, "strSQLH", LeggidttPar(dttPar, "strStringaHaving"))
      RidefinisciQueryHaving(dttPar)


      Dim dttTmpScen As New DataTable
      If ValCodiceDb(LeggidttPar(dttPar, "nTipoScenario"), oApp.Ditta, "TABSCEN", "N", "", dttTmpScen) AndAlso _
        Not dttTmpScen Is Nothing AndAlso _
        dttTmpScen.Rows.Count > 0 AndAlso _
        NTSCStr(dttTmpScen.Rows(0)!tb_order).Trim <> "" Then
        AccodadttPar(dttPar, "strSQLH", " ORDER BY " & NTSCStr(dttTmpScen.Rows(0)!tb_order).Trim)
      Else
        AccodadttPar(dttPar, "strSQLH", " ORDER BY " & LeggidttPar(dttPar, "strStringaOrderby"))
        RidefinisciQueryOrderBy(dttPar)
      End If

      '-----------------------------------------------------------------------------------------
      If LeggidttPar(dttPar, "bCellDblClick") = "-1" Then
        ScrividttPar(dttPar, "strSQLV", LeggidttPar(dttPar, "strSQLH"))
        ScrividttPar(dttPar, "strSQLH", "")
      Else
        dsDwarehe = OpenRecordset(LeggidttPar(dttPar, "strSQLH"), CLE__APP.DBTIPO.DBAZI, "DWAREHE")
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function QueryGenerica(ByVal dsAggiorna As DataSet, ByRef dttPar As DataTable) As Boolean
    Dim dtrTmp() As DataRow
    Dim dtrTmp2() As DataRow
    Dim i As Integer
    Dim strSQL As String = ""
    Dim bLotto As Boolean
    Dim bGiàPassato As Boolean = False

    Try
      'non legge dalla temporanea come vb6
      'la temporanea TTDIMD di vb6 contiene i dati letti da tabdime e dwselect
      'vengono utilizzati dtrTmp, dtrTmp2 per leggere i dati da tabdime e dwselect a seconda del campo
      'ATTENZIONE se in anaext ci sono caratteri/stringhe speciali/riservate come / CHECK può dare errore se la dimensione è selezionata
      If LeggidttPar(dttPar, "bCellDblClick") = "-1" Then
        dtrTmp = dsAggiorna.Tables("TABDIME").Select("tb_scenario = " & LeggidttPar(dttPar, "nTipoScenario") & _
           " AND tb_gruppo <> 5", "xx_order, tb_coddimd")
      Else
        dtrTmp = dsAggiorna.Tables("TABDIME").Select("xx_selez = 'S' AND tb_scenario = " & LeggidttPar(dttPar, "nTipoScenario") & _
                 " AND tb_gruppo <> 5", "xx_order, tb_coddimd")
      End If

      If dtrTmp.Length > 0 Then
        Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
          Case 1
            ScrividttPar(dttPar, "strSQLANAGRA", "((dwarehe ")
            ScrividttPar(dttPar, "strSQLARTICO", " artico ")
          Case 2
            ScrividttPar(dttPar, "strSQLANAGRA", "dwarehe ")
          Case 3
            ScrividttPar(dttPar, "strSQLANAGRA", "((dwarehe ")
            ScrividttPar(dttPar, "strSQLARTICO", " artico ")
          Case 4, 5
            ScrividttPar(dttPar, "strSQLANAGRA", "((dwarehe ")
          Case 7
            ScrividttPar(dttPar, "strSQLANAGRA", "((((dwarehe ")
            ScrividttPar(dttPar, "strSQLARTICO", " artico ")
          Case 8
            ScrividttPar(dttPar, "strSQLANAGRA", "((dwarehe ")
          Case 9
            ScrividttPar(dttPar, "strSQLANAGRA", "((((((dwarehe ")
          Case 10
            ScrividttPar(dttPar, "strSQLANAGRA", "(((((((dwarehe ")
          Case 11
            ScrividttPar(dttPar, "strSQLANAGRA", "((dwarehe ")
          Case 12
            ScrividttPar(dttPar, "strSQLANAGRA", "(((((dwarehe ")
            ScrividttPar(dttPar, "strSQLARTICO", " artico ")
          Case 13
            ScrividttPar(dttPar, "strSQLANAGRA", "(((((((dwarehe ")
            ScrividttPar(dttPar, "strSQLARTICO", " artico ")
          Case 14
            ScrividttPar(dttPar, "strSQLANAGRA", "(((dwarehe ")
          Case Else
            RidefinisciQueryGenericaInizioFrom(dttPar)
        End Select
        For i = 0 To dtrTmp.Length - 1
          With dtrTmp(i)
            '-------------------------------------------------------------------------------------
            '--- Caso particolare per id docuemnto perche' e' un campo composto da:
            '--- dw_tipork + dw_anno + dw_serie + dw_numdoc per 'Ciclo attivo/passivo'
            '--- dw_anno + dw_serie + dw_numdoc + dw_riga (revisione) per 'Offerte'
            '-------------------------------------------------------------------------------------
            If NTSCStr(!tb_alias) = "ID_documento" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 1, 7, 12  '--- Ciclo attivo/passivo --- Contabilità Analitica
                  AccodadttPar(dttPar, "strStringa", "( dw_tipork + cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( dw_tipork + cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( dw_tipork + cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9)))" & " , "))
                Case 5   '--- Offerte
                  AccodadttPar(dttPar, "strStringa", "( cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9)) + cast(dw_riga as char(9)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9)) + cast(dw_riga as char(9)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9)) + cast(dw_riga as char(9)))" & " , "))
                Case 8   '--- Contabilita generale
                  AccodadttPar(dttPar, "strStringa", "( cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9)))" & " , "))
                Case 11   '--- Scadenziario
                  AccodadttPar(dttPar, "strStringa", "( sc_alfdoc + cast(sc_numdoc as char(9)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( sc_alfdoc + cast(sc_numdoc as char(9)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( sc_alfdoc + cast(sc_numdoc as char(9)))" & " , "))
                Case 14   '--- Flussi finanziari
                  AccodadttPar(dttPar, "strStringa", "( scaden.sc_alfdoc + cast(scaden.sc_numdoc as char(9)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( scaden.sc_alfdoc + cast(scaden.sc_numdoc as char(9)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( scaden.sc_alfdoc + cast(scaden.sc_numdoc as char(9)))" & " , "))
              End Select
            ElseIf NTSCStr(!tb_alias) = "ID_partita" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 8  '--- Contabilita generale
                  AccodadttPar(dttPar, "strStringa", "( cast(pn_annpar as char(4)) + pn_alfpar + cast(pn_numpar as char(9)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( cast(pn_annpar as char(4)) + pn_alfpar + cast(pn_numpar as char(9)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( cast(pn_annpar as char(4)) + pn_alfpar + cast(pn_numpar as char(9)))" & " , "))
                Case 11  '--- Scadenziario
                  AccodadttPar(dttPar, "strStringa", "( cast(dw_annotco as char(4)) + dw_serie + cast(dw_numdoc as char(9)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( cast(dw_annotco as char(4)) + dw_serie + cast(dw_numdoc as char(9)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( cast(dw_annotco as char(4)) + dw_serie + cast(dw_numdoc as char(9)))" & " , "))
                Case 14  '--- Flussi finanziari
                  AccodadttPar(dttPar, "strStringa", "( cast(dw_annotco as char(4)) + dw_serie + cast(dw_numdoc as char(9)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( cast(dw_annotco as char(4)) + dw_serie + cast(dw_numdoc as char(9)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( cast(dw_annotco as char(4)) + dw_serie + cast(dw_numdoc as char(9)))" & " , "))
              End Select
            ElseIf NTSCStr(!tb_alias) = "ID_protoc" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 8 '--- Contabilita generale
                  AccodadttPar(dttPar, "strStringa", "( cast(pn_nregiva as char(3)) + pn_alfpro + cast(pn_numpro as char(9)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( cast(pn_nregiva as char(3)) + pn_alfpro + cast(pn_numpro as char(9)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( cast(pn_nregiva as char(3)) + pn_alfpro + cast(pn_numpro as char(9)))" & " , "))
                Case 11 '--- Scadenziario
                  AccodadttPar(dttPar, "strStringa", "( sc_alfpro + cast(sc_numprot as char(9)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( sc_alfpro + cast(sc_numprot as char(9)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( sc_alfpro + cast(sc_numprot as char(9)))" & " , "))
                Case 14 '--- Flussi finanziari
                  AccodadttPar(dttPar, "strStringa", "( scaden.sc_alfpro + cast(scaden.sc_numprot as char(9)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( scaden.sc_alfpro + cast(scaden.sc_numprot as char(9)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( scaden.sc_alfpro + cast(scaden.sc_numprot as char(9)))" & " , "))
              End Select
            ElseIf NTSCStr(!tb_alias) = "ID_riga_off" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 10 '--- CRM Offerte - Righe
                  AccodadttPar(dttPar, "strStringa", "( cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9)) + cast(dw_rigaoff as char(5)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9)) + cast(dw_rigaoff as char(5)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9)) + cast(dw_rigaoff as char(5)))" & " , "))
              End Select
            ElseIf NTSCStr(!tb_alias) = "ID_off" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 10, 5 '--- CRM Offerte --- CRM Offerte - Righe
                  AccodadttPar(dttPar, "strStringa", "( cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9))+ cast(dw_riga as char(3)))" & " as " & NTSCStr(!tb_alias) & " , ")
                  AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", "( cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9))+ cast(dw_riga as char(3)))" & " , "))
                  AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", "( cast(dw_anno as char(4)) + dw_serie + cast(dw_numdoc as char(9))+ cast(dw_riga as char(3)))" & " , "))
              End Select
            ElseIf NTSCStr(!tb_alias) = "Lotto_alfanum" Then
              AccodadttPar(dttPar, "strStringa", " CASE WHEN " & NTSCStr(!tb_fieldname) & " IS NULL THEN '***' ELSE CAST(" & NTSCStr(!tb_fieldname) & " AS CHAR) + ' - ' + ISNULL(" & NTSCStr(!tb_fieldnamd) & ", '***')  END AS " & NTSCStr(!tb_alias) & " , ")
              AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", NTSCStr(!tb_fieldname) & " , "))
              AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", NTSCStr(!tb_fieldnamd) & " , "))
              AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", NTSCStr(!tb_fieldname) & " , " & NTSCStr(!tb_fieldnamd) & " , "))
            Else
              '-----------------------------------------------------------------------------------
              '--- Composizine stringa di selezione
              '-----------------------------------------------------------------------------------
              AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", NTSCStr(!tb_fieldname) & " , "))
              If Trim(NTSCStr(!tb_flcombo)) = "N" Then

                If (NTSCStr(!tb_fieldname) = "anaext.ax_combo1") Or (NTSCStr(!tb_fieldname) = "anaext.ax_combo2") Or (NTSCStr(!tb_fieldname) = "anaext.ax_combo3") Then
                  'Anagrafiche estese Clienti/Fornitori (COMBO)
                  ComponiCaseAnaext(dttPar, NTSCStr(!tb_fieldname), NTSCStr(!tb_alias))
                  AccodadttPar(dttPar, "strStringa", LeggidttPar(dttPar, "strTmpAnaext"))
                ElseIf (NTSCStr(!tb_fieldname) = "artico.ar_xcombo1") Or (NTSCStr(!tb_fieldname) = "artico.ar_xcombo2") Or (NTSCStr(!tb_fieldname) = "artico.ar_xcombo3") Then
                  'Anagrafiche estese Articolo (COMBO)
                  ComponiCaseArtiext(dttPar, NTSCStr(!tb_fieldname), NTSCStr(!tb_alias))
                  AccodadttPar(dttPar, "strStringa", LeggidttPar(dttPar, "strTmpAnaext"))
                Else
                  AccodadttPar(dttPar, "strStringa", NTSCStr(!tb_fieldname) & " as " & NTSCStr(!tb_alias) & " , ")
                End If
                AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", NTSCStr(!tb_fieldname) & " , "))
              Else
                dtrTmp2 = dsAggiorna.Tables("DWSELECT").Select("codditt = " & CStrSQL(LeggidttPar(dttPar, "strDitta")) & " AND ds_coddwim = " & NTSCStr(LeggidttPar(dttPar, "lCoddwim")) & " AND ds_coddimd = " & NTSCStr(!tb_coddimd))

                If dtrTmp2.Length > 0 Then
                  If Trim(NTSCStr(dtrTmp2(0)!ds_valcombo)) = "C" Then
                    AccodadttPar(dttPar, "strStringa", NTSCStr(!tb_fieldname) & " as " & NTSCStr(!tb_alias) & " , ")
                    AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", NTSCStr(!tb_fieldname) & " , "))
                  Else
                    If Trim(NTSCStr(dtrTmp2(0)!ds_valcombo)) = "D" Then
                      AccodadttPar(dttPar, "strStringa", NTSCStr(!tb_fieldnamd) & " as " & NTSCStr(!tb_alias) & " , ")
                      AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", NTSCStr(!tb_fieldnamd) & " , "))
                      AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", NTSCStr(!tb_fieldnamd) & " , "))
                    Else
                      If Trim(NTSCStr(dtrTmp2(0)!ds_valcombo)) = "E" Then
                        Select Case NTSCStr(!tb_tipo)
                          Case "S", "D"
                            AccodadttPar(dttPar, "strStringa", " CASE WHEN " & NTSCStr(!tb_fieldnamd) & " IS NULL THEN '***' ELSE CAST(" & NTSCStr(!tb_fieldname) & " AS CHAR) + ' - ' + " & NTSCStr(!tb_fieldnamd) & "  END AS " & NTSCStr(!tb_alias) & " , ")
                          Case "N", "Q"
                            StringaNumerica(dttPar, dtrTmp(i), dtrTmp2(0))
                            'AccodadttPar(dttPar, "strStringa",  " CASE WHEN " & NTSCStr(!tb_fieldnamd) & " IS NULL THEN '***' ELSE LTRIM(RTRIM(STR(" & NTSCStr(!tb_fieldname) & "))) + ' - ' + " & NTSCStr(!tb_fieldnamd) & " END AS " & NTSCStr(!tb_alias) & ", "
                        End Select
                        AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", NTSCStr(!tb_fieldnamd) & " , "))
                        AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", NTSCStr(!tb_fieldname) & " , " & NTSCStr(!tb_fieldnamd) & " , "))
                      Else
                        If Trim(NTSCStr(dtrTmp2(0)!ds_valcombo)) = "F" Then
                          Select Case NTSCStr(!tb_tipo)
                            Case "S", "D"
                              AccodadttPar(dttPar, "strStringa", " CASE WHEN " & NTSCStr(!tb_fieldnamd) & " IS NULL THEN '***' ELSE " & NTSCStr(!tb_fieldnamd) & " + ' - ' + CAST(" & NTSCStr(!tb_fieldname) & " AS CHAR) END AS " & NTSCStr(!tb_alias) & " , ")
                            Case "N", "Q"
                              AccodadttPar(dttPar, "strStringa", " CASE WHEN " & NTSCStr(!tb_fieldnamd) & " IS NULL THEN '***' ELSE " & NTSCStr(!tb_fieldnamd) & " + ' - ' + LTRIM(RTRIM(STR(" & NTSCStr(!tb_fieldname) & "))) END AS " & NTSCStr(!tb_alias) & " , ")
                          End Select
                          AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", NTSCStr(!tb_fieldnamd) & " , "))
                          AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", NTSCStr(!tb_fieldnamd) & " , " & NTSCStr(!tb_fieldname) & " , "))
                        Else
                          Select Case NTSCStr(!tb_tipo)
                            Case "S", "D"
                              AccodadttPar(dttPar, "strStringa", " CASE WHEN " & NTSCStr(!tb_fieldnamd) & " IS NULL THEN '***' ELSE " & NTSCStr(!tb_fieldnamd) & " + ' - ' + CAST(" & NTSCStr(!tb_fieldname) & " AS CHAR) END AS " & NTSCStr(!tb_alias) & " , ")
                            Case "N", "Q"
                              AccodadttPar(dttPar, "strStringa", " CASE WHEN " & NTSCStr(!tb_fieldnamd) & " IS NULL THEN '***' ELSE " & NTSCStr(!tb_fieldnamd) & " + ' - ' + LTRIM(RTRIM(STR(" & NTSCStr(!tb_fieldname) & "))) END AS " & NTSCStr(!tb_alias) & " , ")
                          End Select
                          AccodadttPar(dttPar, "strStringaGroupby", TestEsistenza(dttPar, "GB", NTSCStr(!tb_fieldnamd) & " , "))
                          AccodadttPar(dttPar, "strStringaOrderby", TestEsistenza(dttPar, "OB", NTSCStr(!tb_fieldnamd) & " , "))
                        End If
                      End If
                    End If
                  End If
                End If
              End If
            End If
            '-------------------------------------------------------------------------------------
            '--- Dimensions initially appeared on verical axis
            '-------------------------------------------------------------------------------------
            'ContourCubeX1.Cube.Dims.Add(NTSCStr(!tb_alias), NTSCStr(!tb_alias), , xda_vertical)
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN ANAGRA o LEADS
            '-------------------------------------------------------------------------------------
            If NTSCStr(!tb_alias) = "Zona" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 1, 7, 8, 11, 12, 13, 14 : AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabzone ON anagra.an_zona = tabzone.tb_codzone AND anagra.codditt = tabzone.codditt)")
                Case 2, 3, 4, 5, 9, 10 : AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabzone ON leads.le_zona = tabzone.tb_codzone AND leads.codditt = tabzone.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Categoria" Then

              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 1, 7, 8, 11, 12, 13, 14 : AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcate ON anagra.an_categ = tabcate.tb_codcate AND anagra.codditt = tabcate.codditt)")
                Case 2, 3, 4, 5, 9, 10 : AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcate ON leads.le_categ = tabcate.tb_codcate AND leads.codditt = tabcate.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Mastro_cont" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabmast ON anagra.an_codmast = tabmast.tb_codmast AND anagra.an_codpcon = tabmast.tb_codpcon)")
            End If
            If NTSCStr(!tb_alias) = "Canale" Then

              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 1, 7, 8, 11, 12, 13, 14 : AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcana ON anagra.an_codcana = tabcana.tb_codcana AND anagra.codditt = tabcana.codditt)")
                Case 2, 3, 4, 5, 9, 10 : AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcana ON leads.le_codcana = tabcana.tb_codcana AND leads.codditt = tabcana.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Provvigioni_cli" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcpcl ON anagra.an_claprov = tabcpcl.tb_codcpcl AND anagra.codditt = tabcpcl.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Sconti_cli" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcscl ON anagra.an_clascon = tabcscl.tb_codcscl AND anagra.codditt = tabcscl.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Classe_Sc_L" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcscl ON leads.le_clascon = tabcscl.tb_codcscl AND leads.codditt = tabcscl.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Agente_cli" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcage ON anagra.an_agente = tabcage.tb_codcage AND anagra.codditt = tabcage.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Agente_L" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcage ON leads.le_agente = tabcage.tb_codcage AND leads.codditt = tabcage.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Subagente_cli" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcage tabcage2 ON anagra.an_agente2 = tabcage2.tb_codcage AND anagra.codditt = tabcage2.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Subagente_L" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcage tabcage2 ON leads.le_agente2 = tabcage2.tb_codcage AND leads.codditt = tabcage2.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Conto_fatt" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagra2 ON anagra.an_contfatt = anagra2.an_conto AND anagra.codditt = anagra2.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Mod_perven" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabperv ON leads.le_codperv = tabperv.tb_codperv AND leads.codditt = tabperv.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Op_incar_L" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9 : AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN organig organig1 ON leads.le_opinc = organig1.og_progr AND leads.codditt = organig1.codditt)")
                Case Else : AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN organig ON leads.le_opinc = organig.og_progr AND leads.codditt = organig.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Concorrente_L" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcptr ON leads.le_codcptr = tabcptr.tb_codcptr AND leads.codditt = tabcptr.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Partner_L" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagra1 ON leads.le_codpartn = anagra1.an_conto AND leads.codditt = anagra1.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Op_incar_L_CRM2" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN acclead ON leads.le_codlead = acclead.opcr_codlead AND leads.codditt = acclead.codditt)")
            End If
            '-------------------------------------------------------------------------------------
            '--- JOIN dirette su DWAREHE
            '-------------------------------------------------------------------------------------
            If NTSCStr(!tb_alias) = "Tipo_bf" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabtpbf ON dwarehe.dw_tipobf = tabtpbf.tb_codtpbf  AND dwarehe.codditt = tabtpbf.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Valuta" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabvalu ON dwarehe.dw_codvalu = tabvalu.tb_codvalu)")
            End If
            If NTSCStr(!tb_alias) = "Agente_doc" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcage tabcage3 ON dwarehe.dw_codcage = tabcage3.tb_codcage  AND dwarehe.codditt = tabcage3.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Subagente_doc" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcage tabcage4 ON dwarehe.dw_codcage2 = tabcage4.tb_codcage  AND dwarehe.codditt = tabcage4.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Causale" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 8
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcauc ON prinot.pn_causale = tabcauc.tb_codcauc)")
                Case 11
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcauc ON scaden.sc_causale = tabcauc.tb_codcauc)")
                Case 14
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcauc ON scaden.sc_causale = tabcauc.tb_codcauc)")
                Case Else
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcaum ON dwarehe.dw_causale = tabcaum.tb_codcaum)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Magazzino" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabmaga tabmaga2 ON dwarehe.dw_magaz = tabmaga2.tb_codmaga AND dwarehe.codditt = tabmaga2.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Pagamento" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabpaga ON dwarehe.dw_codpaga = tabpaga.tb_codpaga)")
            End If
            If NTSCInt(LeggidttPar(dttPar, "nTipoScenario")) <> 7 And NTSCInt(LeggidttPar(dttPar, "nTipoScenario")) <> 8 _
              And NTSCInt(LeggidttPar(dttPar, "nTipoScenario")) <> 12 And NTSCInt(LeggidttPar(dttPar, "nTipoScenario")) <> 13 Then
              If NTSCStr(!tb_alias) = "Commessa" Then
                ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN commess ON dwarehe.dw_commeca = commess.co_comme AND dwarehe.codditt = commess.codditt)")
              End If
            End If
            If NTSCStr(!tb_alias) = "Subcommessa" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN subcomm ON dwarehe.dw_subcommeca = subcomm.sco_subcommeca AND dwarehe.codditt = subcomm.codditt AND dwarehe.dw_commeca = subcomm.sco_commeca)")
            End If
            If NTSCStr(!tb_alias) = "Destinazione_merce" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN destdiv ON dwarehe.dw_coddest = destdiv.dd_coddest AND dwarehe.dw_conto = destdiv.dd_conto AND dwarehe.codditt = destdiv.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Destinazione_merce2" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN destdiv destdiv2 ON dwarehe.dw_coddest2 = destdiv2.dd_coddest AND dwarehe.dw_conto = destdiv2.dd_conto )")
            End If
            If NTSCStr(!tb_alias) = "Contropartita" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcove ON dwarehe.dw_controp = tabcove.tb_codcove AND dwarehe.codditt = tabcove.codditt)")
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcovg tabcovg3 ON dwarehe.dw_controp = tabcovg3.tb_codcovg)")
            End If
            If (NTSCStr(!tb_alias) = "Lotto" Or NTSCStr(!tb_alias) = "Lotto_alfanum") And bLotto = False Then
              bLotto = True
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN analotti ON dwarehe.dw_lotto = analotti.alo_lotto AND dwarehe.codditt = analotti.codditt AND dwarehe.dw_codart = analotti.alo_codart)")
            End If
            If NTSCStr(!tb_alias) = "Fase" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN artfasi ON dwarehe.dw_fase = artfasi.af_fase AND dwarehe.codditt = artfasi.codditt AND dwarehe.dw_codart = artfasi.af_codart)")
            End If
            If NTSCStr(!tb_alias) = "Lavorazioni" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tablavo ON dwarehe.dw_codlavo = tablavo.tb_codlavo AND dwarehe.codditt = tablavo.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Promozione" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabtpro ON dwarehe.dw_codtpro = tabtpro.tb_codtpro  AND dwarehe.codditt = tabtpro.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Stagione" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabstag ON dwarehe.dw_codstag = tabstag.tb_codstag  AND dwarehe.codditt = tabstag.codditt)")
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN ARTICO
            '-------------------------------------------------------------------------------------
            If NTSCStr(!tb_alias) = "Gruppo_prod" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabgmer ON artico.ar_gruppo = tabgmer.tb_codgmer AND artico.codditt = tabgmer.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabgmer ON artico.ar_gruppo = tabgmer.tb_codgmer AND artico.codditt = tabgmer.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Sottogruppo_prod" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabsgme ON artico.ar_sotgru = tabsgme.tb_codsgme AND artico.codditt = tabsgme.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabsgme ON artico.ar_sotgru = tabsgme.tb_codsgme AND artico.codditt = tabsgme.codditt)")
              End Select
            End If

            If NTSCStr(!tb_alias) = "Classificazione_art_liv1" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN busvw_artico_cla1 ON artico.ar_codart = busvw_artico_cla1.ar_codart AND artico.codditt = busvw_artico_cla1.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN busvw_artico_cla1 ON artico.ar_codart = busvw_artico_cla1.ar_codart AND artico.codditt = busvw_artico_cla1.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Classificazione_art_liv2" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN busvw_artico_cla2 ON artico.ar_codart = busvw_artico_cla2.ar_codart AND artico.codditt = busvw_artico_cla2.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN busvw_artico_cla2 ON artico.ar_codart = busvw_artico_cla2.ar_codart AND artico.codditt = busvw_artico_cla2.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Classificazione_art_liv3" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN busvw_artico_cla3 ON artico.ar_codart = busvw_artico_cla3.ar_codart AND artico.codditt = busvw_artico_cla3.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN busvw_artico_cla3 ON artico.ar_codart = busvw_artico_cla3.ar_codart AND artico.codditt = busvw_artico_cla3.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Classificazione_art_liv4" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN busvw_artico_cla4 ON artico.ar_codart = busvw_artico_cla4.ar_codart AND artico.codditt = busvw_artico_cla4.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN busvw_artico_cla4 ON artico.ar_codart = busvw_artico_cla4.ar_codart AND artico.codditt = busvw_artico_cla4.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Classificazione_art_liv5" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN busvw_artico_cla5 ON artico.ar_codart = busvw_artico_cla5.ar_codart AND artico.codditt = busvw_artico_cla5.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN busvw_artico_cla5 ON artico.ar_codart = busvw_artico_cla5.ar_codart AND artico.codditt = busvw_artico_cla5.codditt)")
              End Select
            End If

            If NTSCStr(!tb_alias) = "Famiglia_prod" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcfam ON artico.ar_famprod = tabcfam.tb_codcfam AND artico.codditt = tabcfam.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabcfam ON artico.ar_famprod = tabcfam.tb_codcfam AND artico.codditt = tabcfam.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Tipologia_prod" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabtipa ON artico.ar_codtipa = tabtipa.tb_codtipa AND artico.codditt = tabtipa.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabtipa ON artico.ar_codtipa = tabtipa.tb_codtipa AND artico.codditt = tabtipa.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Marca_prod" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabmarc ON artico.ar_codmarc = tabmarc.tb_codmarc AND artico.codditt = tabmarc.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabmarc ON artico.ar_codmarc = tabmarc.tb_codmarc AND artico.codditt = tabmarc.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Scala_taglie" Then
              'solo scenario 1
              ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
              AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabtagl ON artico.ar_codtagl = tabtagl.tb_codtagl AND artico.codditt = tabtagl.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Provvigioni_prod" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcpar ON artico.ar_claprov = tabcpar.tb_codcpar AND artico.codditt = tabcpar.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabcpar ON artico.ar_claprov = tabcpar.tb_codcpar AND artico.codditt = tabcpar.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Sconti_prod" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcsar ON artico.ar_clascon = tabcsar.tb_codcsar AND artico.codditt = tabcsar.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabcsar ON artico.ar_clascon = tabcsar.tb_codcsar AND artico.codditt = tabcsar.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Magazzino_prod" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabmaga ON artico.ar_magstock = tabmaga.tb_codmaga AND artico.codditt = tabmaga.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabmaga ON artico.ar_magstock = tabmaga.tb_codmaga AND artico.codditt = tabmaga.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Approvvigionatore_prod" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabappr ON artico.ar_codappr = tabappr.tb_codappr AND artico.codditt = tabappr.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabappr ON artico.ar_codappr = tabappr.tb_codappr AND artico.codditt = tabappr.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Fornitore_prod" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagra3 ON artico.ar_forn = anagra3.an_conto AND artico.codditt = anagra3.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN anagra anagra3 ON artico.ar_forn = anagra3.an_conto AND artico.codditt = anagra3.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Fornitore2_prod" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagra4 ON artico.ar_forn2 = anagra4.an_conto AND artico.codditt = anagra4.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN anagra anagra4 ON artico.ar_forn2 = anagra4.an_conto AND artico.codditt = anagra4.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Controp_vendite" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcovg ON artico.ar_controp = tabcovg.tb_codcovg)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabcovg ON artico.ar_controp = tabcovg.tb_codcovg)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Controp_acquisti" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcovg tabcovg2 ON artico.ar_controa = tabcovg2.tb_codcovg)")
                Case Else
                  ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                  AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabcovg tabcovg2 ON artico.ar_controa = tabcovg2.tb_codcovg)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Stagione_art" Then
              If NTSCInt(LeggidttPar(dttPar, "nTipoScenario")) <> 10 Then
                ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN tabstag tabstag2 ON artico.ar_codstag = tabstag2.tb_codstag AND artico.codditt = tabstag2.codditt)")
              End If
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN OPPORTUN
            '-------------------------------------------------------------------------------------
            If LCase(NTSCStr(!tb_alias)) = "op_incar_opp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN organig organig1 ON opportun.op_opinc = organig1.og_progr AND opportun.codditt = organig1.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "mod_perven_opp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabperv tabperv1 ON opportun.op_codperv = tabperv1.tb_codperv AND opportun.codditt = tabperv1.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "tipo_opp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabtopp ON opportun.op_codtopp = tabtopp.tb_codtopp AND opportun.codditt = tabtopp.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "cau_chiu_sos" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabmsos ON opportun.op_codmsos = tabmsos.tb_codmsos AND opportun.codditt = tabmsos.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "segnalatore_opp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagra2 ON opportun.op_conseg = anagra2.an_conto AND opportun.codditt = anagra2.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "agente_opp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcage tabcage4 ON opportun.op_agente = tabcage4.tb_codcage AND opportun.codditt = tabcage4.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "subagente_opp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcage tabcage5 ON opportun.op_agente2 = tabcage5.tb_codcage AND opportun.codditt = tabcage5.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "concorrente_opp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcptr tabcptr1 ON opportun.op_codcptr = tabcptr1.tb_codcptr AND opportun.codditt = tabcptr1.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "partner_opp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagra5 ON opportun.op_codpartn = anagra5.an_conto AND opportun.codditt = anagra5.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "campagna" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcamp ON opportun.codditt = tabcamp.codditt AND opportun.op_codcamp = tabcamp.tb_codcamp)")
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN CRACT
            '-------------------------------------------------------------------------------------
            If LCase(NTSCStr(!tb_alias)) = "tipo_att" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabtaco ON cract.ca_codtaco = tabtaco.tb_codtaco AND cract.codditt = tabtaco.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "op_incar_1_a" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN organig organig1 ON cract.ca_opinc = organig1.og_progr AND cract.codditt = organig1.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "op_incar_2_a" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN organig organig2 ON cract.ca_opinc2 = organig2.og_progr AND cract.codditt = organig2.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "op_incar_3_a" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN organig organig3 ON cract.ca_opinc3 = organig3.og_progr AND cract.codditt = organig3.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "op_incar_4_a" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN organig organig4 ON cract.ca_opinc4 = organig4.og_progr AND cract.codditt = organig4.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "op_incar_a_crm2" _
             Or LCase(NTSCStr(!tb_alias)) = "op_incar_a_stat_crm2" _
             Or LCase(NTSCStr(!tb_alias)) = "op_incar_a_dataes_crm2" _
             Or LCase(NTSCStr(!tb_alias)) = "op_incar_a_tempes_crm2" Then
              If CBool(LeggidttPar(dttPar, "bCractOpp")) = False Then
                ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN cractopp ON cract.ca_codcrac = cractopp.cap_codcrac AND cract.codditt = cractopp.codditt)")
                ScrividttPar(dttPar, "bCractOpp", "-1")
              End If
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN TESTOFF
            '-------------------------------------------------------------------------------------
            If LCase(NTSCStr(!tb_alias)) = "op_incar_off" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN organig organig1 ON testoff.td_opinc = organig1.og_progr AND testoff.codditt = organig1.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "tipo_op_off" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabtpbf ON testoff.td_tipobf = tabtpbf.tb_codtpbf AND testoff.codditt = tabtpbf.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "agente_off" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcage tabcage3 ON testoff.td_codagen = tabcage3.tb_codcage AND testoff.codditt = tabcage3.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "subagente_off" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcage tabcage4 ON testoff.td_codagen2 = tabcage4.tb_codcage AND testoff.codditt = tabcage4.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "linea_off" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcfam tabcfam1 ON testoff.td_codcfam = tabcfam1.tb_codcfam AND testoff.codditt = tabcfam1.codditt)")
                Case Else
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcfam ON testoff.td_codcfam = tabcfam.tb_codcfam AND testoff.codditt = tabcfam.codditt)")
              End Select
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN Conatbilità Analitica
            '-------------------------------------------------------------------------------------
            If LCase(NTSCStr(!tb_alias)) = "centro" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcena ON dwarehe.codditt = tabcena.codditt AND dwarehe.dw_codcena = tabcena.tb_codcena)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "linea" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcfam tabcfam2 ON dwarehe.codditt = tabcfam2.codditt AND dwarehe.dw_codcfam = tabcfam2.tb_codcfam)")
            End If
            If (LCase(NTSCStr(!tb_alias)) = "contoca") Or _
               (LCase(NTSCStr(!tb_alias)) = "descr2contoca") Or _
               (LCase(NTSCStr(!tb_alias)) = "grupporiclcosti") Then
              If bGiàPassato = False Then
                bGiàPassato = True
                Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                  Case 7, 12
                    ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                    AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagca ON dwarehe.codditt = anagca.codditt AND dwarehe.dw_contocontr = anagca.ac_conto)")
                    If NTSCInt(LeggidttPar(dttPar, "nTipoScenario")) = 12 Then
                      ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                      AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagca2 ON dwarehe.dw_contocontr = anagca2.ac_conto)")
                    End If
                End Select
              End If
            End If
            If LCase(NTSCStr(!tb_alias)) = "contocg" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra AS anagraCG ON priana.codditt = anagraCG.codditt AND priana.pa_contocg = anagraCG.an_conto)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "mastroca" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 12, 13
                  ScrividttPar(dttPar, "strSQLANAGRA", " (( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagca anagca3 ON dwarehe.codditt = anagca3.codditt AND dwarehe.dw_contocontr = anagca3.ac_conto)")
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabmac2 ON dwarehe.dw_codmaca = tabmac2.tb_codmac2 AND anagca3.ac_codpcca = tabmac2.tb_codpcca)")
                Case Else
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabmaca ON dwarehe.codditt = tabmaca.codditt AND dwarehe.dw_codmaca = tabmaca.tb_codmaca)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Lavorazione" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tablavo ON dwarehe.codditt = tablavo.codditt AND dwarehe.dw_codlavo = tablavo.tb_codlavo)")
            End If
            If NTSCStr(!tb_alias) = "Operaio" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcope ON dwarehe.codditt = tabcope.codditt AND dwarehe.dw_codcope = tabcope.tb_codcope)")
            End If
            If NTSCStr(!tb_alias) = "Settore" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabseta ON commess.codditt = tabseta.codditt AND commess.co_codseta = tabseta.tb_codseta)")
            End If
            If NTSCStr(!tb_alias) = "Area" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabarea ON commess.codditt = tabarea.codditt AND commess.co_codarea = tabarea.tb_codarea)")
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN Conatbilità Generale
            '-------------------------------------------------------------------------------------
            If NTSCStr(!tb_alias) = "Mastro_conto" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabmast ON anagra.an_codmast = tabmast.tb_codmast AND anagra.an_codpcon = tabmast.tb_codpcon)")
            End If
            If NTSCStr(!tb_alias) = "Controp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagra1 ON prinot.pn_controp = anagra1.an_conto AND prinot.codditt = anagra1.codditt)")
            End If
            If NTSCStr(!tb_alias) = "ContoCF" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagra3 ON prinot.pn_contocf = anagra3.an_conto AND prinot.codditt = anagra3.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Cod_iva" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabciva ON prinot.pn_codiva = tabciva.tb_codciva)")
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN Customer Service
            '-------------------------------------------------------------------------------------
            If NTSCStr(!tb_alias) = "Oper_incar_chiam" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN organig organig2 ON nnchiam.op_opinc = organig2.og_progr AND nnchiam.codditt = organig2.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Mod_acq_chiam" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabperv tabperv2 ON nnchiam.op_codperv = tabperv2.tb_codperv AND nnchiam.codditt = tabperv2.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Tipo_chiam" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabtchi ON nnchiam.op_codtchi = tabtchi.tb_codtchi AND nnchiam.codditt = tabtchi.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Caus_chius" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabmsos ON nnchiam.op_codmsos = tabmsos.tb_codmsos AND nnchiam.codditt = tabmsos.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Commessa_chiam" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN commess ON nnchiam.op_commeca = commess.co_comme AND nnchiam.codditt = commess.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Centro_costo" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcena ON nnchiam.op_codcena = tabcena.tb_codcena AND nnchiam.codditt = tabcena.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Operat_resp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN organig organig3 ON nnchiam.op_opresp = organig3.og_progr AND nnchiam.codditt = organig3.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Op_ricev_chiam" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN organig organig4 ON nnchiam.op_opricc = organig4.og_progr AND nnchiam.codditt = organig4.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Cod_tipo_att" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabtaco ON attconsc.ac_codtaco = tabtaco.tb_codtaco AND attconsc.codditt = tabtaco.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Oper_inc_att" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN organig organig5 ON attconsc.ac_opinc = organig5.og_progr AND attconsc.codditt = organig5.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Articolo_fatt" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN artico artico1 ON attconscd.acd_codart = artico1.ar_codart AND attconscd.codditt = artico1.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Conto_CA" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagca ON attconscd.acd_contoca = anagca.ac_conto AND attconscd.codditt = anagca.codditt)")
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN CRM Offerte - Righe
            '-------------------------------------------------------------------------------------
            If NTSCStr(!tb_alias) = "Forn_riga" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagra2 ON movoff.mo_codforn = anagra2.an_conto AND movoff.codditt = anagra2.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Famiglia_riga" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcfam tabcfam2 ON movoff.mo_codcfam = tabcfam2.tb_codcfam AND movoff.codditt = tabcfam2.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Promoz_riga" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabtpro ON movoff.mo_codtpro = tabtpro.tb_codtpro AND movoff.codditt = tabtpro.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Caus_rif_riga" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabmsos ON movoff.mo_codmsos = tabmsos.tb_codmsos AND movoff.codditt = tabmsos.codditt)")
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN Scadenziario
            '-------------------------------------------------------------------------------------
            If NTSCStr(!tb_alias) = "Controp_sca" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagra1 ON scaden.sc_controp = anagra1.an_conto AND scaden.codditt = anagra1.codditt)")
            End If
            If NTSCStr(!tb_alias) = "Cod_pag" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 11
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabpaga ON scaden.sc_codpaga = tabpaga.tb_codpaga)")
                Case 14
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabpaga ON dwarehe.dw_codpaga = tabpaga.tb_codpaga)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Cod_agente" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 11
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcage tabcage6 ON scaden.sc_codcage = tabcage6.tb_codcage AND scaden.codditt = tabcage6.codditt)")
                Case 14
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcage tabcage6 ON dwarehe.dw_codcage = tabcage6.tb_codcage AND dwarehe.codditt = tabcage6.codditt)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Cod_banca" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 11
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabbanc ON scaden.sc_codbanc = tabbanc.tb_codbanc AND scaden.codditt = tabbanc.codditt)")
                Case 14
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabbanc ON dwarehe.dw_codlavo = tabbanc.tb_codbanc AND dwarehe.codditt = tabbanc.codditt)")
              End Select
            End If
            'per import dati
            If NTSCStr(!tb_alias) = "Tipo_importazione" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN dwimpsce ON dwarehe.codditt = dwimpsce.codditt AND dwarehe.dw_scenario = dwimpsce.sce_codscen AND dwarehe.dw_sceprogr = dwimpsce.sce_progr)")
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN Conatbilità Analitica DC
            '-------------------------------------------------------------------------------------
            If LCase(NTSCStr(!tb_alias)) = "divisione" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabdivi ON dwarehe.codditt = tabdivi.codditt AND dwarehe.dw_coddivi = tabdivi.tb_coddivi)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "stab_neg_fil" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabstab ON dwarehe.codditt = tabstab.codditt AND dwarehe.dw_codstab = tabstab.tb_codstab)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "clienteca" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagraCL ON dwarehe.codditt = anagraCL.codditt AND dwarehe.dw_codcli = anagraCL.an_conto)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "caus_ca" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcaca ON dwarehe.dw_livrib = tabcaca.tb_codcaca)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "contocgcoll" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagraCG ON priana2.codditt = anagraCG.codditt AND priana2.pa2_contocg = anagraCG.an_conto)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "centro_cp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcena tabcenab ON priana2b.codditt = tabcenab.codditt AND priana2b.pa2_codcena = tabcenab.tb_codcena)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "commessa_cp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN commess commessb ON priana2b.pa2_commeca = commessb.co_comme AND priana2b.codditt = commessb.codditt)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "subcommessa_cp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN subcomm subcommb ON priana2b.pa2_subcommeca = subcommb.sco_subcommeca AND priana2b.codditt = subcommb.codditt AND priana2b.pa2_commeca = subcommb.sco_commeca)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "linea_cp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcfam tabcfamb ON priana2b.codditt = tabcfamb.codditt AND priana2b.pa2_codcfam = tabcfamb.tb_codcfam)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "divisione_cp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabdivi tabdivib ON priana2b.codditt = tabdivib.codditt AND priana2b.pa2_coddivi = tabdivib.tb_coddivi)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "stab_neg_fil_cp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabstab tabstabb ON priana2b.codditt = tabstabb.codditt AND priana2b.pa2_codstab = tabstabb.tb_codstab)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "clienteca_cp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagraCLb ON priana2b.codditt = anagraCLb.codditt AND priana2b.pa2_codcli = anagraCLb.an_conto)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "contoca_cp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagca anagcab ON priana2b.codditt = anagcab.codditt AND priana2b.pa2_conto = anagcab.ac_conto)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "prodotto_cp" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN artico articob ON priana2b.codditt = articob.codditt AND priana2b.pa2_codart = articob.ar_codart)")
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN Timesheet (CA-DC)
            '-------------------------------------------------------------------------------------
            If LCase(NTSCStr(!tb_alias)) = "lavorazione_lavo" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tablavo tablavoLa ON lavmano.codditt = tablavoLa.codditt AND lavmano.lcm_codlavo = tablavoLa.tb_codlavo)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "operaio_lavo" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcope tabcopeLa ON lavmano.codditt = tabcopeLa.codditt AND lavmano.lcm_codcope = tabcopeLa.tb_codcope)")
            End If
            If LCase(NTSCStr(!tb_alias)) = "conto_di_comm_lavo" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN anagra anagraCOM ON commess2.codditt = anagraCOM.codditt AND commess2.co_conto = anagraCOM.an_conto)")
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN Flussi finanziari
            '-------------------------------------------------------------------------------------
            If NTSCStr(!tb_alias) = "Sottotipo" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 11
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabstpg ON scaden.sc_codstpg = tabstpg.tb_codstpg)")
                Case 14
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabstpg ON dwarehe.dw_magaz = tabstpg.tb_codstpg)")
              End Select
            End If
            If NTSCStr(!tb_alias) = "Nota_aut_cont" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabnaut ON scaden.sc_codnaut = tabnaut.tb_codnaut)")
            End If
            If NTSCStr(!tb_alias) = "Nota_aut_ncont" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabnaut tabnaut2 ON scadenec.sc_codnaut = tabnaut2.tb_codnaut)")
            End If
            If NTSCStr(!tb_alias) = "Causale_ncont" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabcauc tabcauc2 ON scadenec.sc_causale = tabcauc2.tb_codcauc)")
            End If
            '-------------------------------------------------------------------------------------
            '--- Composizione stringa LEFT JOIN GPV
            '-------------------------------------------------------------------------------------
            If NTSCStr(!tb_alias) = "Pagamento2" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabpaga tabpaga2 ON dwarehe.dw_codpaga2 = tabpaga2.tb_codpaga)")
            End If
            If NTSCStr(!tb_alias) = "Stabilimento" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabstab tabstab2 ON dwarehe.codditt = tabstab2.codditt AND dwarehe.dw_codstab = tabstab2.tb_codstab)")
            End If
            If NTSCStr(!tb_alias) = "Tessera" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN refites ON dwarehe.codditt = refites.codditt AND dwarehe.dw_codtes = refites.ret_codtes)")
            End If
            If NTSCStr(!tb_alias) = "Punto_Cassa" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepc ON dwarehe.codditt = tabrepc.codditt AND dwarehe.dw_codrepc = tabrepc.tb_codrepc)")
            End If
            If NTSCStr(!tb_alias) = "Promozione1" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepr ON dwarehe.codditt = tabrepr.codditt AND dwarehe.dw_codrepr1 = tabrepr.tb_codrepr)")
            End If
            If NTSCStr(!tb_alias) = "Promozione2" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepr tabrepr2 ON dwarehe.codditt = tabrepr2.codditt AND dwarehe.dw_codrepr2 = tabrepr2.tb_codrepr)")
            End If
            If NTSCStr(!tb_alias) = "Promozione3" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepr tabrepr3 ON dwarehe.codditt = tabrepr3.codditt AND dwarehe.dw_codrepr3 = tabrepr3.tb_codrepr)")
            End If
            If NTSCStr(!tb_alias) = "Promozione4" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepr tabrepr4 ON dwarehe.codditt = tabrepr4.codditt AND dwarehe.dw_codrepr4 = tabrepr4.tb_codrepr)")
            End If
            If NTSCStr(!tb_alias) = "Promozione5" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepr tabrepr5 ON dwarehe.codditt = tabrepr5.codditt AND dwarehe.dw_codrepr5 = tabrepr5.tb_codrepr)")
            End If
            If NTSCStr(!tb_alias) = "Promozione6" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepr tabrepr6 ON dwarehe.codditt = tabrepr6.codditt AND dwarehe.dw_codrepr6 = tabrepr6.tb_codrepr)")
            End If
            If NTSCStr(!tb_alias) = "Promozione1_Applicata" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepr tabrepr1out ON dwarehe.codditt = tabrepr1out.codditt AND dwarehe.dw_codrepr1 = tabrepr1out.tb_codrepr)")
            End If
            If NTSCStr(!tb_alias) = "Promozione2_Applicata" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepr tabrepr2out ON dwarehe.codditt = tabrepr2out.codditt AND dwarehe.dw_codrepr2 = tabrepr2out.tb_codrepr)")
            End If
            If NTSCStr(!tb_alias) = "Promozione3_Applicata" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepr tabrepr3out ON dwarehe.codditt = tabrepr3out.codditt AND dwarehe.dw_codrepr3 = tabrepr3out.tb_codrepr)")
            End If
            If NTSCStr(!tb_alias) = "Promozione4_Applicata" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepr tabrepr4out ON dwarehe.codditt = tabrepr4out.codditt AND dwarehe.dw_codrepr4 = tabrepr4out.tb_codrepr)")
            End If
            If NTSCStr(!tb_alias) = "Promozione5_Applicata" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepr tabrepr5out ON dwarehe.codditt = tabrepr5out.codditt AND dwarehe.dw_codrepr5 = tabrepr5out.tb_codrepr)")
            End If
            If NTSCStr(!tb_alias) = "Promozione6_Applicata" Then
              ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
              AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabrepr tabrepr6out ON dwarehe.codditt = tabrepr6out.codditt AND dwarehe.dw_codrepr6 = tabrepr6out.tb_codrepr)")
            End If

            If NTSCStr(!tb_alias) = "Stato" Then
              Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
                Case 2, 3, 4, 5, 9, 10
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabstat ON leads.le_stato = tabstat.tb_codstat)")
                Case Else
                  ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                  AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN tabstat ON anagra.an_stato = tabstat.tb_codstat)")
              End Select
            End If

            'per personaliz
            RidefinisciQuery_(NTSCStr(!tb_alias), dttPar)
          End With
        Next
        '-------------------------------------------------------------------------------------
        '--- Composizione stringa LEFT JOIN movord/movmag
        '-------------------------------------------------------------------------------------
        If NTSCInt(LeggidttPar(dttPar, "nTipoScenario")) = 1 Then
          'se scenario1 attiva l'opz bGestJoinMov vecchia from se attiva solo bGestJoinMov2 nuova from
          If CBool(LeggidttPar(dttPar, "bGestJoinMov")) Then
            ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
            AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN movord ON dwarehe.codditt = movord.codditt" & _
              " AND dwarehe.dw_motipork = movord.mo_tipork AND dwarehe.dw_moanno = movord.mo_anno" & _
              " AND dwarehe.dw_moserie = movord.mo_serie AND dwarehe.dw_monumord = movord.mo_numord" & _
              " AND dwarehe.dw_moriga = movord.mo_riga)")
            ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
            AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN movmag ON dwarehe.codditt = movmag.codditt" & _
              " AND dwarehe.dw_tipork = movmag.mm_tipork AND dwarehe.dw_anno = movmag.mm_anno" & _
              " AND dwarehe.dw_serie = movmag.mm_serie AND dwarehe.dw_numdoc = movmag.mm_numdoc" & _
              " AND dwarehe.dw_riga = movmag.mm_riga)")
          ElseIf LeggidttPar(dttPar, "bGestJoinMov2") = "-1" Then
            ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
            AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN movord ON dwarehe.codditt = movord.codditt" & _
              " AND dwarehe.dw_motipork = movord.mo_tipork AND dwarehe.dw_moanno = movord.mo_anno" & _
              " AND dwarehe.dw_moserie = movord.mo_serie AND dwarehe.dw_monumord = movord.mo_numord" & _
              " AND dwarehe.dw_moriga = movord.mo_riga)")
            ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
            AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN movmag ON dwarehe.codditt = movmag.codditt" & _
              " AND dwarehe.dw_origca = movmag.mm_tipork AND dwarehe.dw_coddivi = movmag.mm_anno" & _
              " AND dwarehe.dw_tipore = movmag.mm_serie AND dwarehe.dw_idrigarg = movmag.mm_numdoc" & _
              " AND dwarehe.dw_riga = movmag.mm_riga)")
          End If
        End If
      End If
      RidefinisciQueryAggiungiJoin(dttPar)
      '-----------------------------------------------------------------------------------------
      If Len(LeggidttPar(dttPar, "strStringa")) > 0 Then
        ScrividttPar(dttPar, "strStringa", Mid(LeggidttPar(dttPar, "strStringa"), 1, Len(LeggidttPar(dttPar, "strStringa")) - 3))
      Else
        Return False
      End If
      '-----------------------------------------------------------------------------------------
      If NTSCInt(LeggidttPar(dttPar, "nTipoScenario")) = 1 Then
        If CBool(LeggidttPar(dttPar, "bGestAnaextC")) And (InStr(1, LCase(LeggidttPar(dttPar, "strStringa")), "anaext") <> 0) Then
          ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
        End If
      End If
      '-----------------------------------------------------------------------------------------
      If Len(LeggidttPar(dttPar, "strSQLONANAGRA")) > 0 Then
        ScrividttPar(dttPar, "strSQLONANAGRA", Mid(LeggidttPar(dttPar, "strSQLONANAGRA"), 1, Len(LeggidttPar(dttPar, "strSQLONANAGRA")) - 1))
      End If
      '-----------------------------------------------------------------------------------------
      Return True
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function StringaNumerica(ByRef dttPar As DataTable, ByVal dtrTmp As DataRow, _
                                              ByVal dtrTmp2 As DataRow) As Boolean
    Dim strSQL As String = ""
    Try
      With dtrTmp
        '----------------------------------------------------------------------------------------
        ScrividttPar(dttPar, "nZeri", "0")
        '----------------------------------------------------------------------------------------
        If UCase(NTSCStr(dtrTmp2!ds_valcombo)) = "E" Then
          If NTSCStr(!tb_fieldname) = "prinot.pn_controp" Or NTSCStr(!tb_fieldname) = "scaden.sc_controp" Then 'eccezioni
            ScrividttPar(dttPar, "nZeri", "9")
          Else
            '--------------------------------------------------------------------------------------
            ScrividttPar(dttPar, "strTmp", LCase(Mid(NTSCStr(!tb_fieldname), (InStr(1, NTSCStr(!tb_fieldname), "_") + 1))))
            '--------------------------------------------------------------------------------------
            Select Case LeggidttPar(dttPar, "strTmp")
              Case "gruppo" : ScrividttPar(dttPar, "nZeri", "2")
              Case "agente", "agente2", _
                   "categ", "causale", "claprov", "clascon", "codagen", "codagen2", "codappr", _
                   "codarea", "codcana", "codcage", "codcage2", "codcamp", "codcena", "codcope", _
                   "codcptr", "codlavo", "codmarc", "codperv", "codseta", _
                   "codstag", "codtipa", "codiva", "codperv", "codtaco", "codtchi", _
                   "codmast", "codmsos", "codpaga", "codtopp", "codtpro", "codvalu", _
                   "controa", "controp", _
                   "fase", _
                   "lotto", _
                   "magaz", "magstock", _
                   "sotgru", _
                   "tipobf", _
                   "zona"
                ScrividttPar(dttPar, "nZeri", "4")
              Case "codforn", "codlead", "codoppo", "codpartn", "conseg", "contfatt", "conto", "coddest", "coddest2", _
                   "contoca", "contocf", "contocg", "contocontr", _
                   "forn", "forn2", _
                   "opinc", "opinc2", "opinc3", "opinc4", "opresp", "opricc", "commeca", "codmaca"
                ScrividttPar(dttPar, "nZeri", "9")
              Case Else
                RidefinisciStringaNumerica(dttPar)
            End Select
          End If
        End If
        '----------------------------------------------------------------------------------------
        If NTSCInt(LeggidttPar(dttPar, "nZeri")) = 0 Then
          AccodadttPar(dttPar, "strStringa", " CASE WHEN " & NTSCStr(!tb_fieldnamd) & " IS NULL THEN '***' ELSE LTRIM(RTRIM(STR(" & NTSCStr(!tb_fieldname) & "))) + ' - ' + " & NTSCStr(!tb_fieldnamd) & " END AS " & NTSCStr(!tb_alias) & " , ")
        Else
          AccodadttPar(dttPar, "strStringa", " CASE WHEN " & NTSCStr(!tb_fieldnamd) & " IS NULL THEN '***' ELSE RIGHT('" & "".PadLeft(NTSCInt(LeggidttPar(dttPar, "nZeri")), "0"c) & "' + LTRIM(RTRIM(STR(" & NTSCStr(!tb_fieldname) & "))), " & LeggidttPar(dttPar, "nZeri") & ") + ' - ' + " & NTSCStr(!tb_fieldnamd) & " END AS " & NTSCStr(!tb_alias) & " , ")
        End If
      End With
      'il valore strStringa rimane memorizzato nel datatable dei parametri
      Return True
      '----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function ComponiCaseAnaext(ByRef dttPar As DataTable, ByVal strCombo As String, _
                                                ByVal strAlias As String) As Boolean
    Dim dsAext As DataSet = Nothing
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM tabaext" & _
        " WHERE codditt = " & CStrSQL(LeggidttPar(dttPar, "strDitta")) & _
        " AND tb_tipork = 'C'"
      dsAext = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABAEXT")

      If Not dsAext.Tables("TABAEXT").Rows.Count = 0 Then
        With dsAext.Tables("TABAEXT").Rows(0)
          If strCombo = "anaext.ax_combo1" Then
            ScrividttPar(dttPar, "strTmpAnaext", "CASE WHEN anaext.ax_combo1 = 'A' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_A)) & _
              " WHEN anaext.ax_combo1 = 'B' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_B)) & _
              " WHEN anaext.ax_combo1 = 'C' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_C)) & _
              " WHEN anaext.ax_combo1 = 'D' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_D)) & _
              " WHEN anaext.ax_combo1 = 'E' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_E)) & _
              " WHEN anaext.ax_combo1 = 'F' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_F)) & _
              " WHEN anaext.ax_combo1 = 'G' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_G)) & _
              " WHEN anaext.ax_combo1 = 'H' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_H)) & _
              " WHEN anaext.ax_combo1 = 'I' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_I)) & _
              " END AS " & strAlias & " , ")
          End If
          If strCombo = "anaext.ax_combo2" Then
            ScrividttPar(dttPar, "strTmpAnaext", "CASE WHEN anaext.ax_combo2 = 'A' THEN " & CStrSQL(NTSCStr(!tb_helpcom2_A)) & _
              " WHEN anaext.ax_combo2 = 'B' THEN " & CStrSQL(NTSCStr(!tb_helpcom2_B)) & _
              " WHEN anaext.ax_combo2 = 'C' THEN " & CStrSQL(NTSCStr(!tb_helpcom2_C)) & _
              " WHEN anaext.ax_combo2 = 'D' THEN " & CStrSQL(NTSCStr(!tb_helpcom2_D)) & _
              " WHEN anaext.ax_combo2 = 'E' THEN " & CStrSQL(NTSCStr(!tb_helpcom2_E)) & _
              " END AS " & strAlias & " , ")
          End If
          If strCombo = "anaext.ax_combo3" Then
            ScrividttPar(dttPar, "strTmpAnaext", "CASE WHEN anaext.ax_combo3 = 'A' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_A)) & _
              " WHEN anaext.ax_combo3 = 'B' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_B)) & _
              " WHEN anaext.ax_combo3 = 'C' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_C)) & _
              " WHEN anaext.ax_combo3 = 'D' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_D)) & _
              " WHEN anaext.ax_combo3 = 'E' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_E)) & _
              " WHEN anaext.ax_combo3 = 'F' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_F)) & _
              " WHEN anaext.ax_combo3 = 'G' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_G)) & _
              " WHEN anaext.ax_combo3 = 'H' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_H)) & _
              " WHEN anaext.ax_combo3 = 'I' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_I)) & _
              " WHEN anaext.ax_combo3 = 'L' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_L)) & _
              " WHEN anaext.ax_combo3 = 'M' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_M)) & _
              " WHEN anaext.ax_combo3 = 'N' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_N)) & _
              " WHEN anaext.ax_combo3 = 'O' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_O)) & _
              " WHEN anaext.ax_combo3 = 'P' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_P)) & _
              " WHEN anaext.ax_combo3 = 'Q' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_Q)) & _
              " WHEN anaext.ax_combo3 = 'R' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_R)) & _
              " WHEN anaext.ax_combo3 = 'S' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_S)) & _
              " WHEN anaext.ax_combo3 = 'T' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_T)) & _
              " WHEN anaext.ax_combo3 = 'U' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_U)) & _
              " WHEN anaext.ax_combo3 = 'V' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_V)) & _
              " END AS " & strAlias & " , ")
          End If
        End With
      End If

      'il valore strTmpAnaext rimane memorizzato nel datatable dei parametri
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function ComponiCaseArtiext(ByRef dttPar As DataTable, ByVal strCombo As String, _
                                                ByVal strAlias As String) As Boolean
    Dim dsAext As DataSet = Nothing
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM tabaext" & _
        " WHERE codditt = " & CStrSQL(LeggidttPar(dttPar, "strDitta")) & _
        " AND tb_tipork = 'A'"
      dsAext = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABAEXT")

      If Not dsAext.Tables("TABAEXT").Rows.Count = 0 Then
        With dsAext.Tables("TABAEXT").Rows(0)
          If strCombo = "artico.ar_xcombo1" Then
            ScrividttPar(dttPar, "strTmpAnaext", "CASE WHEN artico.ar_xcombo1 = 'A' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_A)) & _
              " WHEN artico.ar_xcombo1 = 'B' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_B)) & _
              " WHEN artico.ar_xcombo1 = 'C' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_C)) & _
              " WHEN artico.ar_xcombo1 = 'D' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_D)) & _
              " WHEN artico.ar_xcombo1 = 'E' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_E)) & _
              " WHEN artico.ar_xcombo1 = 'F' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_F)) & _
              " WHEN artico.ar_xcombo1 = 'G' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_G)) & _
              " WHEN artico.ar_xcombo1 = 'H' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_H)) & _
              " WHEN artico.ar_xcombo1 = 'I' THEN " & CStrSQL(NTSCStr(!tb_helpcom1_I)) & _
              " END AS " & strAlias & " , ")
          End If
          If strCombo = "artico.ar_xcombo2" Then
            ScrividttPar(dttPar, "strTmpAnaext", "CASE WHEN artico.ar_xcombo2 = 'A' THEN " & CStrSQL(NTSCStr(!tb_helpcom2_A)) & _
              " WHEN artico.ar_xcombo2 = 'B' THEN " & CStrSQL(NTSCStr(!tb_helpcom2_B)) & _
              " WHEN artico.ar_xcombo2 = 'C' THEN " & CStrSQL(NTSCStr(!tb_helpcom2_C)) & _
              " WHEN artico.ar_xcombo2 = 'D' THEN " & CStrSQL(NTSCStr(!tb_helpcom2_D)) & _
              " WHEN artico.ar_xcombo2 = 'E' THEN " & CStrSQL(NTSCStr(!tb_helpcom2_E)) & _
              " END AS " & strAlias & " , ")
          End If
          If strCombo = "artico.ar_xcombo3" Then
            ScrividttPar(dttPar, "strTmpAnaext", "CASE WHEN artico.ar_xcombo3 = 'A' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_A)) & _
              " WHEN artico.ar_xcombo3 = 'B' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_B)) & _
              " WHEN artico.ar_xcombo3 = 'C' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_C)) & _
              " WHEN artico.ar_xcombo3 = 'D' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_D)) & _
              " WHEN artico.ar_xcombo3 = 'E' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_E)) & _
              " WHEN artico.ar_xcombo3 = 'F' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_F)) & _
              " WHEN artico.ar_xcombo3 = 'G' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_G)) & _
              " WHEN artico.ar_xcombo3 = 'H' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_H)) & _
              " WHEN artico.ar_xcombo3 = 'I' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_I)) & _
              " WHEN artico.ar_xcombo3 = 'L' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_L)) & _
              " WHEN artico.ar_xcombo3 = 'M' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_M)) & _
              " WHEN artico.ar_xcombo3 = 'N' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_N)) & _
              " WHEN artico.ar_xcombo3 = 'O' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_O)) & _
              " WHEN artico.ar_xcombo3 = 'P' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_P)) & _
              " WHEN artico.ar_xcombo3 = 'Q' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_Q)) & _
              " WHEN artico.ar_xcombo3 = 'R' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_R)) & _
              " WHEN artico.ar_xcombo3 = 'S' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_S)) & _
              " WHEN artico.ar_xcombo3 = 'T' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_T)) & _
              " WHEN artico.ar_xcombo3 = 'U' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_U)) & _
              " WHEN artico.ar_xcombo3 = 'V' THEN " & CStrSQL(NTSCStr(!tb_helpcom3_V)) & _
              " END AS " & strAlias & " , ")
          End If
        End With
      End If

      'il valore strTmpAnaext rimane memorizzato nel datatable dei parametri
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function QueryGenericaWhere(ByVal dsAggiorna As DataSet, ByRef dttPar As DataTable) As Boolean
    Dim bFine As Boolean
    Dim strAppoggio As String = ""
    Dim strHaving As String = ""
    Dim nGruppo As Integer
    Dim strNull As String = ""
    Dim lCoddimd As Integer
    Dim i As Integer
    Dim strWhere As String = ""
    Dim strSQL As String = ""
    Dim dtrTmp2() As DataRow
    Dim strANDOR As String = ""
    Dim strValore As String = ""
    Dim strCampoTmp As String = ""
    Dim dtrTmpFilt() As DataRow
    Try
      '-----------------------------------------------------------------------------------------
      ScrividttPar(dttPar, "strWHERE2", "")
      strANDOR = ""
      lCoddimd = 0
      '-----------------------------------------------------------------------------------------
      '--- WHERE
      '-----------------------------------------------------------------------------------------
      bFine = False
      '-----------------------------------------------------------------------------------------
      ScrividttPar(dttPar, "strStringaHaving", " HAVING ")
      '-----------------------------------------------------------------------------------------

      dtrTmpFilt = dsAggiorna.Tables("DWFILT").Select("", "fi_coddimd")

      For i = 0 To dtrTmpFilt.Length - 1


        With dtrTmpFilt(i)

          If NTSCStr(!fi_alias) = "Classificazione_art_liv1" Then
            Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
              Case 9, 10
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla1") Then ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla1") Then AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN busvw_artico_cla1 ON artico.ar_codart = busvw_artico_cla1.ar_codart AND artico.codditt = busvw_artico_cla1.codditt)")
              Case Else
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla1") Then ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla1") Then AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN busvw_artico_cla1 ON artico.ar_codart = busvw_artico_cla1.ar_codart AND artico.codditt = busvw_artico_cla1.codditt)")
            End Select
          End If
          If NTSCStr(!fi_alias) = "Classificazione_art_liv2" Then
            Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
              Case 9, 10
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla2") Then ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla2") Then AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN busvw_artico_cla2 ON artico.ar_codart = busvw_artico_cla2.ar_codart AND artico.codditt = busvw_artico_cla2.codditt)")
              Case Else
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla2") Then ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla2") Then AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN busvw_artico_cla2 ON artico.ar_codart = busvw_artico_cla2.ar_codart AND artico.codditt = busvw_artico_cla2.codditt)")
            End Select
          End If
          If NTSCStr(!fi_alias) = "Classificazione_art_liv3" Then
            Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
              Case 9, 10
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla3") Then ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla3") Then AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN busvw_artico_cla3 ON artico.ar_codart = busvw_artico_cla3.ar_codart AND artico.codditt = busvw_artico_cla3.codditt)")
              Case Else
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla3") Then ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla3") Then AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN busvw_artico_cla3 ON artico.ar_codart = busvw_artico_cla3.ar_codart AND artico.codditt = busvw_artico_cla3.codditt)")
            End Select
          End If
          If NTSCStr(!fi_alias) = "Classificazione_art_liv4" Then
            Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
              Case 9, 10
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla4") Then ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla4") Then AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN busvw_artico_cla4 ON artico.ar_codart = busvw_artico_cla4.ar_codart AND artico.codditt = busvw_artico_cla4.codditt)")
              Case Else
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla4") Then ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla4") Then AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN busvw_artico_cla4 ON artico.ar_codart = busvw_artico_cla4.ar_codart AND artico.codditt = busvw_artico_cla4.codditt)")
            End Select
          End If
          If NTSCStr(!fi_alias) = "Classificazione_art_liv5" Then
            Select Case NTSCInt(LeggidttPar(dttPar, "nTipoScenario"))
              Case 9, 10
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla5") Then ScrividttPar(dttPar, "strSQLANAGRA", " ( " & LeggidttPar(dttPar, "strSQLANAGRA"))
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla5") Then AccodadttPar(dttPar, "strSQLONANAGRA", " LEFT JOIN busvw_artico_cla5 ON artico.ar_codart = busvw_artico_cla5.ar_codart AND artico.codditt = busvw_artico_cla5.codditt)")
              Case Else
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla5") Then ScrividttPar(dttPar, "strSQLARTICO", " ( " & LeggidttPar(dttPar, "strSQLARTICO"))
                If Not LeggidttPar(dttPar, "strSQLONARTICO").Contains("busvw_artico_cla5") Then AccodadttPar(dttPar, "strSQLONARTICO", " LEFT JOIN busvw_artico_cla5 ON artico.ar_codart = busvw_artico_cla5.ar_codart AND artico.codditt = busvw_artico_cla5.codditt)")
            End Select
          End If

          If NTSCStr(!fi_not) = "S" Then
            Select Case NTSCStr(!fi_tipocond)
              Case " = " : strAppoggio = " <> "
              Case " <> " : strAppoggio = " = "
              Case " > " : strAppoggio = " <= "
              Case " < " : strAppoggio = " >= "
              Case " >= " : strAppoggio = " < "
              Case " <= " : strAppoggio = " > "
              Case " Like " : strAppoggio = " Not like "
              Case " Compreso " : strAppoggio = " Not Between"
            End Select
          Else
            If Trim(NTSCStr(!fi_tipocond)) = "Compreso" Then
              strAppoggio = "Between"
            Else
              strAppoggio = NTSCStr(!fi_tipocond)
            End If
          End If

          dtrTmp2 = dsAggiorna.Tables("TABDIME").Select("tb_coddimd = " & NTSCStr(!fi_coddimd) & _
                     " AND tb_scenario = " & LeggidttPar(dttPar, "nTipoScenario"))

          If dtrTmp2.Length > 0 Then
            nGruppo = NTSCInt(dtrTmp2(0)!tb_gruppo)
            '-------------------------------------------------------------------------------------
            '--- Facts
            '-------------------------------------------------------------------------------------
            If nGruppo = 5 Then
              If lCoddimd = NTSCInt(!fi_coddimd) Then
                If LeggidttPar(dttPar, "strStringaHaving") = " HAVING " Then
                  strANDOR = " SUM ("
                Else
                  If Trim(strAppoggio) = "<>" Then strANDOR = " AND SUM (" Else strANDOR = " OR SUM ("
                End If
              Else
                If LeggidttPar(dttPar, "strStringaHaving") = " HAVING " Then
                  strANDOR = " SUM ("
                Else
                  strANDOR = " AND SUM ("
                End If
              End If
              If Trim(strAppoggio) = "Like" Or Trim(strAppoggio) = "Not Like" Then 'test like data param??
                strHaving = strANDOR & NTSCStr(dtrTmp2(0)!tb_fieldname) & ") " & strAppoggio & " '" & NTSCStr(IIf(CBool(LeggidttPar(dttPar, "bLikeNonOttimistico")) = False, "%", "")) & NTSCStr(!fi_val1) & NTSCStr(IIf(CBool(LeggidttPar(dttPar, "bLikeNonOttimistico")) = False, "%", "")) & "'"
              Else
                If NTSCStr(dtrTmp2(0)!tb_tipo) = "N" Then
                  strHaving = strANDOR & NTSCStr(dtrTmp2(0)!tb_fieldname) & ") " & strAppoggio & " " & CDblSQL(NTSCStr(!fi_val1))
                Else
                  If NTSCStr(dtrTmp2(0)!tb_tipo) = "D" Then 'date parametriche
                    strValore = ConvertiInData(dttPar, NTSCStr(!fi_val1), "")
                    strHaving = strANDOR & NTSCStr(dtrTmp2(0)!tb_fieldname) & ") " & strAppoggio & " " & CDataSQL(strValore)
                  Else
                    strHaving = strANDOR & NTSCStr(dtrTmp2(0)!tb_fieldname) & ") " & strAppoggio & " " & CStrSQL(NTSCStr(!fi_val1))
                  End If
                End If
                If Trim(strAppoggio) = "Between" Or Trim(strAppoggio) = "Not Between" Then
                  If NTSCStr(dtrTmp2(0)!tb_tipo) = "N" Then
                    strHaving = strHaving & " AND " & CDblSQL(NTSCStr(!fi_val2))
                  Else
                    If NTSCStr(dtrTmp2(0)!tb_tipo) = "D" Then 'date parametriche
                      strValore = ConvertiInData(dttPar, NTSCStr(!fi_val2), "")
                      strHaving = strHaving & " AND " & CDataSQL(strValore)
                    Else
                      strHaving = strHaving & " AND " & CStrSQL(NTSCStr(!fi_val2))
                    End If
                  End If
                End If
              End If
              '-----------------------------------------------------------------------------------
              lCoddimd = NTSCInt(!fi_coddimd)
              '-----------------------------------------------------------------------------------
            Else 'Dims
              If lCoddimd = NTSCInt(!fi_coddimd) Then
                If Len(LeggidttPar(dttPar, "strWHERE2")) > 0 Then
                  ScrividttPar(dttPar, "strWHERE2", Left(LeggidttPar(dttPar, "strWHERE2"), Len(LeggidttPar(dttPar, "strWHERE2")) - 2))
                End If
                If Trim(strAppoggio) = "<>" Then strANDOR = " AND " Else strANDOR = " OR "
              Else
                If Trim(Right(LeggidttPar(dttPar, "strWHERE2"), 3)) <> ")" Then
                  If LeggidttPar(dttPar, "strWHERE2") <> "" Then
                    AccodadttPar(dttPar, "strWHERE2", " ) ")
                  End If
                End If
                strANDOR = " AND ("
              End If
              If Trim(strAppoggio) = "Like" Or Trim(strAppoggio) = "Not Like" Then
                '----------------------------------------------------------------------------------------------------
                strCampoTmp = NTSCStr(dtrTmp2(0)!tb_fieldname)
                If (NTSCStr(dtrTmp2(0)!tb_tipo) = "S") And _
                   (dtrTmp2(0)!tb_fieldnamd.Equals(DBNull.Value) = False) And _
                   (dtrTmp2(0)!xx_valcombo.Equals(DBNull.Value) = False) Then
                  If (NTSCStr(dtrTmp2(0)!tb_fieldnamd).Trim <> "") And _
                     (NTSCStr(dtrTmp2(0)!xx_valcombo).Trim <> "") Then
                    If NTSCStr(dtrTmp2(0)!xx_valcombo).ToUpper <> "C" Then
                      strCampoTmp = NTSCStr(dtrTmp2(0)!tb_fieldnamd)
                    End If
                  End If
                End If
                '----------------------------------------------------------------------------------------------------              
                strWhere = strANDOR & strCampoTmp & " " & strAppoggio & " '" & NTSCStr(IIf(CBool(LeggidttPar(dttPar, "bLikeNonOttimistico")) = False, "%", "")) & NTSCStr(!fi_val1) & NTSCStr(IIf(CBool(LeggidttPar(dttPar, "bLikeNonOttimistico")) = False, "%", "")) & "'"
              Else
                If NTSCStr(dtrTmp2(0)!tb_tipo) = "N" Then
                  strWhere = strANDOR & NTSCStr(dtrTmp2(0)!tb_fieldname) & " " & strAppoggio & " " & CDblSQL(NTSCStr(!fi_val1))
                Else
                  If NTSCStr(dtrTmp2(0)!tb_tipo) = "D" Then 'date parametriche
                    strValore = ConvertiInData(dttPar, NTSCStr(!fi_val1), "")
                    strWhere = strANDOR & NTSCStr(dtrTmp2(0)!tb_fieldname) & " " & strAppoggio & " " & CDataSQL(strValore)
                  Else
                    strWhere = strANDOR & NTSCStr(dtrTmp2(0)!tb_fieldname) & " " & strAppoggio & " " & CStrSQL(NTSCStr(!fi_val1))
                  End If
                End If

                If Trim(strAppoggio) = "Between" Or Trim(strAppoggio) = "Not Between" Then
                  If NTSCStr(dtrTmp2(0)!tb_tipo) = "N" Then
                    strWhere = strWhere & " AND " & CDblSQL(NTSCStr(!fi_val2))
                  Else
                    If NTSCStr(dtrTmp2(0)!tb_tipo) = "D" Then 'date parametriche
                      strValore = ConvertiInData(dttPar, NTSCStr(!fi_val2), "")
                      strWhere = strWhere & " AND " & CDataSQL(strValore)
                    Else
                      strWhere = strWhere & " AND " & CStrSQL(NTSCStr(!fi_val2))
                    End If
                  End If
                End If
              End If
              If lCoddimd = NTSCInt(!fi_coddimd) Then
                bFine = True
              Else
                strWhere = strWhere & " ) "
              End If
              lCoddimd = NTSCInt(!fi_coddimd)
              If NTSCStr(!fi_null) = "S" Then
                strNull = " OR (" & NTSCStr(dtrTmp2(0)!tb_fieldname) & " IS NULL)" & _
                          " OR (" & NTSCStr(dtrTmp2(0)!tb_fieldname) & " = '' ) "
              Else
                strNull = "" 'altrimenti continua a proporlo anche nelle condizioni successive
              End If
            End If
            '-------------------------------------------------------------------------------------
            If bFine Then
              If nGruppo = 5 Then
                strWhere = strWhere & " ) "
              Else
                strWhere = strWhere & strNull & " ) "
              End If
              bFine = False
            Else
              If nGruppo = 5 Then strWhere = strWhere & strNull
            End If
            If nGruppo = 5 Then
              AccodadttPar(dttPar, "strStringaHaving", strHaving & strNull)
            Else
              AccodadttPar(dttPar, "strWHERE2", strWhere)
            End If
          End If
        End With
      Next
      '-----------------------------------------------------------------------------------------

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function Analisi(ByVal nTipoScenario As Integer) As String
    Dim strSQL As String = ""
    Try
      Select Case nTipoScenario
        Case 1 : Return "VEN"
        Case 2 : Return "LEA"
        Case 3 : Return "OPP"
        Case 4 : Return "CRA"
        Case 5 : Return "OFF"
        Case 7 : Return "COA"
        Case 8 : Return "COG"
        Case 9 : Return "CS1"
        Case 10 : Return "MOF"
        Case 11 : Return "SCA"
        Case 12 : Return "CAE"
        Case 13 : Return "CAT"
        Case 14 : Return "FLF"
        Case Else : Return ""
      End Select

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
      Return ""
    End Try
  End Function
  Public Overridable Function ConvertiInData(ByRef dttPar As DataTable, ByVal strData As String, ByRef strErrore As String) As String
    Dim strPart() As String
    Try
      strData = strData.Trim

      'Data odierna o oggi è quella del giorno di stampa
      If strData.ToLower = "data odierna" Or strData.ToLower = "oggi" Or _
         strData.ToLower = "today" Or strData.ToLower = "now" Or _
         strData.ToLower = oApp.Tr(Me, 129223786674775598, "data odierna").ToLower Or _
         strData.ToLower = oApp.Tr(Me, 129223786568213098, "oggi").ToLower Then Return Now.ToShortDateString

      'Non è di tipo "+1 Giorno", ecc... tutti con uno spazio in mezzo, quindi è una data classica
      strPart = strData.Split(" "c)

      If strPart.Length = 1 Then Return strData

      'dovrebbero essere supportati solo i nomi italiani, ma aggiungerne anche altri non costa niente.
      Select Case strPart(1).ToLower
        Case "giorni", "giorno", "g", "day", "days", "d", oApp.Tr(Me, 129223783990556848, "giorni").ToLower, oApp.Tr(Me, 129223784107275598, "giorno").ToLower
          Return Now.AddDays(NTSCInt(strPart(0))).ToShortDateString
        Case "mese", "mesi", "month", "months", "m", oApp.Tr(Me, 129223784378681848, "mese").ToLower, oApp.Tr(Me, 129223784395244348, "mesi").ToLower
          Return Now.AddMonths(NTSCInt(strPart(0))).ToShortDateString
        Case "anno", "anni", "a", "year", "years", "y", oApp.Tr(Me, 129223784387119348, "anno").ToLower, oApp.Tr(Me, 129223784402744348, "anni").ToLower
          Return Now.AddYears(NTSCInt(strPart(0))).ToShortDateString
      End Select

      'Se non riesce a codificarla in nulla ritorna la data di oggi e avvisa
      If dttPar Is Nothing Then 'se richiamato da entity o altro
        strErrore = oApp.Tr(Me, 129228798831185876, "Non è stato possibile tradurre la data '|" & strData & "|'")
      Else 'se richiamato da elaborazione query
        AccodadttPar(dttPar, "strErr", "Non è stato possibile tradurre la data '|" & strData & "|'" & vbCrLf)
      End If

      Return Now.ToShortDateString
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Function TestEsistenza(ByVal dttPar As DataTable, ByVal strStringa As String) As String
    'OBSOLETA
    Try
      Return TestEsistenza(dttPar, "OB", strStringa)
    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Function TestEsistenza(ByVal dttPar As DataTable, ByVal strTipo As String, ByVal strStringa As String) As String
    'se è già presente la stringa ritorna vuoto cosi' accoda vuoto alla order by o gruop by
    'se no da errore se ci sono 2 identici campi nella order by o group by
    'attenzione stringa sempre "strstringa , " cioè spazio , spazio
    Try
      '----------------
      'per compatibilità con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {dttPar, strTipo, strStringa})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return NTSCStr(oOut)
      End If
      '----------------


      If strTipo = "GB" Then
        If LeggidttPar(dttPar, "strStringaGroupby") = "" Then Return strStringa

        If InStr(LeggidttPar(dttPar, "strStringaGroupby"), strStringa) > 0 Then
          Return ""
        Else
          Return strStringa
        End If
      ElseIf strTipo = "OB" Then
        If LeggidttPar(dttPar, "strStringaOrderby") = "" Then Return strStringa

        If InStr(LeggidttPar(dttPar, "strStringaOrderby"), strStringa) > 0 Then
          Return ""
        Else
          Return strStringa
        End If
      Else
        Return ""
      End If


    Catch ex As Exception
      '---------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '---------------------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Function GetTabdime(ByVal nTipoScenario As Integer, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim strIn As String = ""
    Try
      If CLN__STD.FRIENDLY And nTipoScenario = 1 Then
        strIn = " AND ((tb_coddimd = 403 AND tb_alias = 'Quantita_ordinata')" & _
                " OR (tb_coddimd = 402 AND tb_alias = 'Colli_ordinati')" & _
                " OR (tb_coddimd = 405 AND tb_alias = 'Quantita_fatt')" & _
                " OR (tb_coddimd = 404 AND tb_alias = 'Colli_fatt')" & _
                " OR (tb_coddimd = 401 AND tb_alias = 'Quantita_cons')" & _
                " OR (tb_coddimd = 400 AND tb_alias = 'Colli_cons')" & _
                " OR (tb_coddimd = 409 AND tb_alias = 'Valore_ordinato')" & _
                " OR (tb_coddimd = 410 AND tb_alias = 'Valore_ord_val')" & _
                " OR (tb_coddimd = 406 AND tb_alias = 'Valore_cons')" & _
                " OR (tb_coddimd = 412 AND tb_alias = 'Valore_fatturato')" & _
                " OR (tb_coddimd = 413 AND tb_alias = 'Valore_fatt_val')" & _
                " OR (tb_coddimd = 418 AND tb_alias = 'Costo_fatturato')" & _
                " OR (tb_coddimd = 417 AND tb_alias = 'Costo_consegnato')" & _
                " OR (tb_coddimd = 440 AND tb_alias = 'Quantita_ordinata_acq')" & _
                " OR (tb_coddimd = 439 AND tb_alias = 'Colli_ordinati_acq')" & _
                " OR (tb_coddimd = 438 AND tb_alias = 'Quantita_cons_acq')" & _
                " OR (tb_coddimd = 437 AND tb_alias = 'Colli_cons_acq')" & _
                " OR (tb_coddimd = 443 AND tb_alias = 'Quantita_fatt_acq')" & _
                " OR (tb_coddimd = 441 AND tb_alias = 'Colli_fatt_acq')" & _
                " OR (tb_coddimd = 447 AND tb_alias = 'Valore_ordinato_acq')" & _
                " OR (tb_coddimd = 444 AND tb_alias = 'Valore_cons_acq')" & _
                " OR (tb_coddimd = 450 AND tb_alias = 'Valore_fatturato_acq')" & _
                " OR (tb_coddimd = 448 AND tb_alias = 'Valore_ord_val_acq')" & _
                " OR (tb_coddimd = 451 AND tb_alias = 'Valore_fatt_val_acq')" & _
                " OR (tb_coddimd = 101 AND tb_alias = 'Prodotto')" & _
                " OR (tb_coddimd = 102 AND tb_alias = 'Gruppo_prod')" & _
                " OR (tb_coddimd = 103 AND tb_alias = 'Sottogruppo_prod')" & _
                " OR (tb_coddimd = 104 AND tb_alias = 'Famiglia_prod')" & _
                " OR (tb_coddimd = 111 AND tb_alias = 'Fornitore_prod')" & _
                " OR (tb_coddimd = 112 AND tb_alias = 'Fornitore2_prod')" & _
                " OR (tb_coddimd = 113 AND tb_alias = 'Tipo_prod')" & _
                " OR (tb_coddimd = 1 AND tb_alias = 'Cliente_forn')" & _
                " OR (tb_coddimd = 2 AND tb_alias = 'Citta')" & _
                " OR (tb_coddimd = 3 AND tb_alias = 'Zona')" & _
                " OR (tb_coddimd = 5 AND tb_alias = 'Provincia')" & _
                " OR (tb_coddimd = 6 AND tb_alias = 'Cap')" & _
                " OR (tb_coddimd = 7 AND tb_alias = 'Stato')" & _
                " OR (tb_coddimd = 201 AND tb_alias = 'Tipo_record')" & _
                " OR (tb_coddimd = 202 AND tb_alias = 'Serie_doc')" & _
                " OR (tb_coddimd = 203 AND tb_alias = 'Numero_doc')" & _
                " OR (tb_coddimd = 204 AND tb_alias = 'Riga_doc')" & _
                " OR (tb_coddimd = 205 AND tb_alias = 'Tipo_bf')" & _
                " OR (tb_coddimd = 206 AND tb_alias = 'Unita_mis_conf')" & _
                " OR (tb_coddimd = 207 AND tb_alias = 'Valuta')" & _
                " OR (tb_coddimd = 208 AND tb_alias = 'Agente_doc')" & _
                " OR (tb_coddimd = 210 AND tb_alias = 'Causale')" & _
                " OR (tb_coddimd = 211 AND tb_alias = 'Magazzino')" & _
                " OR (tb_coddimd = 212 AND tb_alias = 'Descr_articolo')" & _
                " OR (tb_coddimd = 213 AND tb_alias = 'Pagamento')" & _
                " OR (tb_coddimd = 217 AND tb_alias = 'Destinazione_merce')" & _
                " OR (tb_coddimd = 218 AND tb_alias = 'Destinazione_merce2')" & _
                " OR (tb_coddimd = 221 AND tb_alias = 'Unita_mis_prin')" & _
                " OR (tb_coddimd = 232 AND tb_alias = 'Stampa_riga')" & _
                " OR (tb_coddimd = 226 AND tb_alias = 'Promozione')" & _
                " OR (tb_coddimd = 301 AND tb_alias = 'Anno')" & _
                " OR (tb_coddimd = 302 AND tb_alias = 'Mese')" & _
                " OR (tb_coddimd = 303 AND tb_alias = 'Giorno')" & _
                " OR (tb_coddimd = 304 AND tb_alias = 'Data')" & _
                " OR (tb_coddimd = 305 AND tb_alias = 'Semestre')" & _
                " OR (tb_coddimd = 306 AND tb_alias = 'Quadrimestre')" & _
                " OR (tb_coddimd = 307 AND tb_alias = 'Trimestre')" & _
                " OR (tb_coddimd = 308 AND tb_alias = 'Settimana')" & _
                " OR (tb_coddimd = 309 AND tb_alias = 'Giorno_sett')" & _
                " OR (tb_coddimd = 311 AND tb_alias = 'Anno_ord')" & _
                " OR (tb_coddimd = 312 AND tb_alias = 'Mese_ord')" & _
                " OR (tb_coddimd = 313 AND tb_alias = 'Giorno_ord')" & _
                " OR (tb_coddimd = 314 AND tb_alias = 'Data_ord')" & _
                " OR (tb_coddimd = 315 AND tb_alias = 'Semestre_ord')" & _
                " OR (tb_coddimd = 316 AND tb_alias = 'Quadrim_ord')" & _
                " OR (tb_coddimd = 317 AND tb_alias = 'Trimestre_ord')" & _
                " OR (tb_coddimd = 318 AND tb_alias = 'Settimana_ord')" & _
                " OR (tb_coddimd = 319 AND tb_alias = 'Gg_sett_ord'))"

      ElseIf CLN__STD.FRIENDLY And nTipoScenario <> 1 Then
        strIn = " AND 1=2"
      End If

      strSQL = "SELECT TABDIME.*, 'N' as xx_selez, '' as xx_order, ' ' as xx_valcombo, 'S' as xx_visdet, 'N' as xx_nascondi FROM TABDIME" & _
        " WHERE tb_scenario = " & nTipoScenario & _
        strIn & _
        " ORDER BY tb_coddimd"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABDIME")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function GetTabdwim(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                       ByVal nTipoScenario As Integer, ByVal bSoloCod As Boolean, _
                                       ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Dim strSQLSelect As String = ""
    Try
      If bSoloCod Then
        strSQLSelect = "tb_coddwim"
      Else
        strSQLSelect = "tabdwim.*"
      End If
      strSQL = "SELECT " & strSQLSelect & " FROM tabdwim WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_coddwim = " & lCoddwim
      If nTipoScenario <> 0 Then strSQL += " AND tb_dwscenario = " & nTipoScenario

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABDWIM")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function GetDwselect(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                        ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT dwselect.* FROM dwselect" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ds_coddwim = " & lCoddwim

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DWSELECT")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CaricaFiltri(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                      ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DWFILT.*, '' as xx_valore, '' as xx_alias FROM DWFILT" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND fi_coddwim = " & lCoddwim & _
        " ORDER BY fi_coddimd, fi_progr"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DWFILT")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CaricaCalc(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                    ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DWCAMCAL.*, '' as xx_tipocalcolo FROM DWCAMCAL" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND cc_coddwim = " & lCoddwim & _
        " ORDER BY cc_coddwim"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DWCAMCAL")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function GetDwselvis(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                        ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DWSELVIS.* FROM DWSELVIS" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND dsv_coddwim = " & lCoddwim & _
        " ORDER BY dsv_coddimd"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DWSELVIS")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function GetDwcrus(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                        ByVal bNonApplicaFiltro As Boolean, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT dwcrus.*" & _
        " FROM dwcrus" & _
        " WHERE dwcrus.codditt = " & CStrSQL(strDitta)

      If bNonApplicaFiltro = False Then strSQL = strSQL & " AND dd_coddwim = " & CStrSQL(NTSCStr(lCoddwim))

      strSQL = strSQL & " ORDER BY dwcrus.codditt, dd_coddwim, dd_alias"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DWCRUS")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function GetDwlayoute(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                      ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT dwlayoute.* FROM dwlayoute" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND dl_coddwim = " & lCoddwim

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DWLAYOUTE")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function GetTabaext(ByVal strDitta As String, ByVal strTipo As String, _
                                         ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tabaext.* FROM tabaext" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_tipork = " & CStrSQL(strTipo)


      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "TABAEXT")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function GetDwimsto(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                         ByVal strDataoraStoricoCarica As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT dwimsto.* FROM dwimsto WHERE codditt = " & CStrSQL(strDitta) & _
        " AND tb_coddwim = " & lCoddwim & _
        " AND tb_dataora = " & CDataOraSQL(strDataoraStoricoCarica)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DWIMSTO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function GetDwselsto(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                          ByVal strDataoraStoricoCarica As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT dwselsto.* FROM dwselsto" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND ds_coddwim = " & lCoddwim & _
        " AND ds_dataora = " & CDataOraSQL(strDataoraStoricoCarica)

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DWSELSTO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CaricaDwfilsto(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                             ByVal strDataoraStoricoCarica As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DWFILSTO.*, '' as xx_valore, '' as xx_alias FROM DWFILSTO" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND fi_coddwim = " & lCoddwim & _
        " AND fi_dataora = " & CDataOraSQL(strDataoraStoricoCarica) & _
        " ORDER BY fi_coddimd, fi_progr"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DWFILSTO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function CaricaDwCamsto(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                             ByVal strDataoraStoricoCarica As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DWCAMSTO.*, '' as xx_tipocalcolo FROM DWCAMSTO" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND cc_coddwim = " & lCoddwim & _
        " AND cc_dataora = " & CDataOraSQL(strDataoraStoricoCarica) & _
        " ORDER BY cc_coddwim"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DWCAMSTO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function GetDwsevsto(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                          ByVal strDataoraStoricoCarica As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DWSEVSTO.* FROM DWSEVSTO" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND dsv_coddwim = " & lCoddwim & _
        " AND dsv_dataora = " & CDataOraSQL(strDataoraStoricoCarica) & _
        " ORDER BY dsv_coddimd"

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DWSEVSTO")
      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function GetDwlaysto(ByVal strDitta As String, ByVal lCoddwim As Integer, _
                                          ByVal strDataoraStoricoCarica As String, ByRef ds As DataSet) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT dwlaysto.* FROM dwlaysto" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND dl_dataora = " & CDataOraSQL(strDataoraStoricoCarica) & _
        " AND dl_coddwim = " & lCoddwim

      ds = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, "DWLAYSTO")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

#End Region

#Region "Funzioni per personalizzazioni Datawarehouse"
  Public Overridable Function RidefinisciQuerySelectFieldName(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQuerySelect(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQueryFrom(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQueryOnArtico(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQueryInnerLeftJoin(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQueryBeforeComponiSelectFrom(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQueryWhere(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQueryWhereGroupbyHavingOrderby(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQueryGroupBy(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQueryHaving(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQueryOrderBy(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQueryGenericaInizioFrom(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQueryAggiungiJoin(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciStringaNumerica(ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function

  Public Overridable Function RidefinisciQuery_(ByVal strTb_alias As String, ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      Dim strFunction As String = "RidefinisciQuery_" & strTb_alias
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing And strTb_alias <> "" Then
        Dim parametri As Object()
        parametri = New Object() {strTb_alias, dttPar}
        'chiamo il metodo
        fun.Invoke(Me, parametri)
        dttPar = CType(parametri(1), DataTable)
      End If

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  Public Overridable Function RidefinisciQuery_Mese(ByVal strTb_alias As String, ByRef dttPar As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      'esempio personalizzazione
      'AccodadttPar(dttPar, "strStringa", " AND dw_mese = 1 ")

      Return True

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
#End Region

#Region "Funzioni per Datatable parametri"
  'scrittura Boolean 0 -1
  '<DebuggerStepThrough()> _
  Public Overridable Function LeggidttPar(ByRef dttPar As DataTable, ByVal strNomevar As String) As String
    Dim strSQL As String = ""
    Dim dtrTmp() As DataRow
    Try
      dtrTmp = dttPar.Select("cod = " & CStrSQL(strNomevar))
      If dtrTmp.Length > 0 Then
        Return NTSCStr(dtrTmp(0)!val)
      Else
        Return ""
      End If

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  '  <DebuggerStepThrough()> _
  Public Overridable Function ScrividttPar(ByRef dttPar As DataTable, ByVal strNomevar As String, _
                                             ByVal strValorevar As String) As Boolean
    Dim strSQL As String = ""
    Dim dtrTmp() As DataRow
    Try
      dtrTmp = dttPar.Select("cod = " & CStrSQL(strNomevar))
      If dtrTmp.Length > 0 Then
        dtrTmp(0)!val = NTSCStr(strValorevar)
        Return True
      Else
        Return False
      End If

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  '<DebuggerStepThrough()> _
  Public Overridable Function AccodadttPar(ByRef dttPar As DataTable, ByVal strNomevar As String, _
                                           ByVal strValorevar As String) As Boolean
    Dim strSQL As String = ""
    Dim dtrTmp() As DataRow
    Try
      dtrTmp = dttPar.Select("cod = " & CStrSQL(strNomevar))
      If dtrTmp.Length > 0 Then
        dtrTmp(0)!val = NTSCStr(dtrTmp(0)!val) & NTSCStr(strValorevar)
        Return True
      Else
        Return False
      End If

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  ' <DebuggerStepThrough()> _
  Public Overridable Function CreaRigadttPar(ByRef dttPar As DataTable, ByVal strNomevar As String, _
                                               ByVal strValorevar As String, ByVal strTipoVar As String) As Boolean
    Dim strSQL As String = ""
    Try
      dttPar.Rows.Add(New Object() {strNomevar, strValorevar, strTipoVar})
      dttPar.AcceptChanges()

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  ' <DebuggerStepThrough()> _
  Public Overridable Function ControllaPresenzadttPar(ByRef dttPar As DataTable, ByVal strNomevar As String) As Boolean
    Dim strSQL As String = ""
    Dim dtrTmp() As DataRow
    Try
      dtrTmp = dttPar.Select("cod = " & CStrSQL(strNomevar))
      If dtrTmp.Length > 0 Then
        Return True
      Else
        Return False
      End If

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
  ' <DebuggerStepThrough()> _
  Public Overridable Function SvuotaCreaRigadttPar(ByRef dttPar As DataTable, ByVal strNomevar As String, _
                                             ByVal strValorevar As String, ByVal strTipoVar As String) As Boolean
    Dim strSQL As String = ""
    Try
      'se la riga è presente la svuoto se no la creo
      If ControllaPresenzadttPar(dttPar, strNomevar) Then
        ScrividttPar(dttPar, strNomevar, "")
      Else
        CreaRigadttPar(dttPar, strNomevar, strValorevar, strTipoVar)
      End If

    Catch ex As Exception
      '-------------------------------------------------------------- 
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------------------- 
    End Try
  End Function
#End Region

#Region "AGGIORNAMENTO LISTINI (VB6 --> AGLI)"
  Public Overridable Function DataListinoPosteriore(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal nCodvalu As Integer, ByVal strDatavalidita As String, ByVal nListino As Integer, _
    ByVal nCodlavo As Integer, ByVal strUnitaMisura As String, ByVal nFase As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try
      strSQL = "SELECT TOP 1 lc_datagg FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart) & _
        " AND lc_codlavo = " & nCodlavo & _
        " AND lc_conto = 0" & _
        " AND lc_codvalu = " & nCodvalu & _
        " AND lc_codtpro = 0" & _
        " AND lc_listino = " & nListino & _
        " AND lc_datagg > " & CDataSQL(strDatavalidita) & _
        " AND lc_daquant = 0" & _
        " AND lc_unmis = " & CStrSQL(strUnitaMisura) & _
        " AND lc_fase = " & nFase & _
        " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_codvalu, lc_codtpro," & _
        " lc_listino, lc_datagg ASC, lc_daquant, lc_unmis, lc_fase"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        Return DateAdd("d", -1, NTSCDate(dttTmp.Rows(0)!lc_datagg)).ToString
      Else
        Return IntSetDate("31/12/2099")
      End If
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function InsertLISTINI(ByVal strDitta As String, ByVal strCodart As String, _
  ByVal nCodlavo As Integer, ByVal lConto As Integer, ByVal nCodvalu As Integer, ByVal nCodtpro As Integer, _
  ByVal nListino As Integer, ByVal strDatagg As String, ByVal strTipo As String, ByVal dPrezzo As Decimal, _
  ByVal strDatscad As String, ByVal dDaquant As Decimal, ByVal dAquant As Decimal, ByVal dPerqta As Decimal, _
  ByVal strUnmis As String, ByVal strNote As String, ByVal strNetto As String, ByVal nFase As Integer) As Boolean
    Try
      'obsoelta
      Return InsertLISTINI(strDitta, strCodart, nCodlavo, lConto, nCodvalu, nCodtpro, nListino, strDatagg, strTipo, dPrezzo, _
                          strDatscad, dDaquant, dAquant, dPerqta, strUnmis, strNote, strNetto, nFase, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function InsertLISTINI(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal nCodlavo As Integer, ByVal lConto As Integer, ByVal nCodvalu As Integer, ByVal nCodtpro As Integer, _
    ByVal nListino As Integer, ByVal strDatagg As String, ByVal strTipo As String, ByVal dPrezzo As Decimal, _
    ByVal strDatscad As String, ByVal dDaquant As Decimal, ByVal dAquant As Decimal, ByVal dPerqta As Decimal, _
    ByVal strUnmis As String, ByVal strNote As String, ByVal strNetto As String, ByVal nFase As Integer, _
    ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nCodlavo, lConto, nCodvalu, nCodtpro, nListino, strDatagg, strTipo, dPrezzo, _
                          strDatscad, dDaquant, dAquant, dPerqta, strUnmis, strNote, strNetto, nFase, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "INSERT INTO listini (codditt, lc_codart, lc_codlavo, lc_conto, lc_codvalu, lc_codtpro," & _
        " lc_listino, lc_datagg, lc_tipo, lc_prezzo, lc_datscad, lc_daquant, lc_aquant, lc_perqta, lc_unmis," & _
        " lc_note, lc_netto, lc_fase, lc_ultagg, lc_coddest)" & _
        " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodart) & ", " & nCodlavo & ", " & lConto & ", " & _
        nCodvalu & ", " & nCodtpro & ", " & nListino & ", " & CDataSQL(strDatagg) & ", " & CStrSQL(strTipo) & ", " & _
        CDblSQL(dPrezzo) & ", " & CDataSQL(strDatscad) & ", " & CDblSQL(dDaquant) & ", " & CDblSQL(dAquant) & ", " & _
        CDblSQL(dPerqta) & ", " & CStrSQL(strUnmis) & ", " & CStrSQL(strNote) & ", " & CStrSQL(strNetto) & ", " & _
        nFase & ", " & CDataOraSQL(Now) & ", " & lCoddest & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function ListinoGiaPresente(ByVal strDitta As String, ByVal strCodart As String, _
  ByVal nCodlavo As Integer, ByVal lConto As Integer, ByVal nListino As Integer, ByVal nCodvalu As Integer, _
  ByVal nCodtpro As Integer, ByVal strDatagg As String, ByVal dDaquant As Decimal, ByVal nFase As Integer) As Boolean
    Try
      'obsoleta
      Return ListinoGiaPresente(strDitta, strCodart, nCodlavo, lConto, nListino, nCodvalu, nCodtpro, strDatagg, dDaquant, nFase, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try

  End Function
  Public Overridable Function ListinoGiaPresente(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal nCodlavo As Integer, ByVal lConto As Integer, ByVal nListino As Integer, ByVal nCodvalu As Integer, _
    ByVal nCodtpro As Integer, ByVal strDatagg As String, ByVal dDaquant As Decimal, ByVal nFase As Integer, _
    ByVal lCoddest As Integer) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nCodlavo, lConto, nListino, nCodvalu, nCodtpro, strDatagg, dDaquant, nFase, 0, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT lc_progr FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart) & _
        " AND lc_codlavo = " & nCodlavo & _
        " AND lc_conto = " & lConto & _
        " AND lc_coddest = " & lCoddest & _
        " AND lc_listino = " & nListino & _
        " AND lc_codvalu = " & nCodvalu & _
        " AND lc_codtpro = " & nCodtpro & _
        " AND lc_datagg = " & CDataSQL(strDatagg) & _
        " AND lc_daquant = " & CDblSQL(dDaquant) & _
        " AND lc_fase = " & nFase
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

  Public Overridable Function SelectLISTINIQ1(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal nCodvalu As Integer, ByVal strDatavalidita As String, ByVal nListino As Integer, _
    ByVal nCodlavo As Integer, ByVal strUnitaMisura As String, ByVal nFase As Integer, _
    ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT * FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart) & _
        " AND lc_codlavo = " & nCodlavo & _
        " AND lc_codvalu = " & nCodvalu & _
        " AND lc_listino = " & nListino & _
        " AND lc_codtpro = 0" & _
        " AND lc_datagg <= " & CDataSQL(strDatavalidita) & _
        " AND lc_datscad >= " & CDataSQL(strDatavalidita) & _
        " AND lc_unmis = " & CStrSQL(strUnitaMisura) & _
        " AND lc_fase = " & nFase & _
        " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_listino, lc_codvalu, lc_codtpro," & _
        " lc_datagg DESC, lc_daquant, lc_unmis, lc_fase"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        dttTmp.Clear()
        dttTmp.Dispose()
        Return False
      End If
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function SelectLISTINIQ2(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal nCodlavo As Integer, ByVal lConto As Integer, ByVal nCodvalu As Integer, ByVal nCodtpro As Integer, _
    ByVal nListino As Integer, ByVal strDatavalidita As String, ByVal dDaquant As Decimal, _
    ByVal strUnitaMisura As String, ByVal nFase As Integer) As String
    Try
      'obsoleta
      Return SelectLISTINIQ2(strDitta, strCodart, nCodlavo, lConto, nCodvalu, nCodtpro, _
                             nListino, strDatavalidita, dDaquant, strUnitaMisura, nFase, 0)

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function SelectLISTINIQ2(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal nCodlavo As Integer, ByVal lConto As Integer, ByVal nCodvalu As Integer, ByVal nCodtpro As Integer, _
    ByVal nListino As Integer, ByVal strDatavalidita As String, ByVal dDaquant As Decimal, _
    ByVal strUnitaMisura As String, ByVal nFase As Integer, ByVal lCoddest As Integer) As String
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable

    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, nCodlavo, lConto, nCodvalu, nCodtpro, _
                             nListino, strDatavalidita, dDaquant, strUnitaMisura, nFase, lCoddest})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '----------------

      strSQL = "SELECT TOP 1 lc_datagg FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart) & _
        " AND lc_codlavo = " & nCodlavo & _
        " AND lc_conto = " & lConto & _
        " AND lc_coddest = " & lCoddest & _
        " AND lc_codvalu = " & nCodvalu & _
        " AND lc_codtpro = " & nCodtpro & _
        " AND lc_listino = " & nListino & _
        " AND lc_datagg > " & CDataSQL(strDatavalidita) & _
        " AND lc_daquant = " & CDblSQL(dDaquant) & _
        " AND lc_unmis = " & CStrSQL(strUnitaMisura) & _
        " AND lc_fase = " & nFase & _
        " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_coddest, lc_codvalu, lc_codtpro, lc_listino," & _
        " lc_datagg ASC, lc_daquant, lc_unmis, lc_fase"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then Return DateAdd("d", -1, NTSCDate(dttTmp.Rows(0)!lc_datagg)).ToString
      Return IntSetDate("31/12/2099")
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function SelezionaNuovoPrezzo(ByVal strDitta As String, ByVal strCodart As String, _
      ByVal nCodvalu As Integer, ByVal strDtinvigore As String, ByVal nListino As Integer, _
      ByVal nCodlavo As Integer, ByVal strUnitaMisura As String, ByVal nFase As Integer, _
      ByRef dttTmp As DataTable) As Boolean
    Dim strSQL As String = ""

    Try
      strSQL = "SELECT lc_prezzo, lc_perqta, lc_note FROM listini" & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_codart = " & CStrSQL(strCodart) & _
        " AND lc_codvalu = " & nCodvalu & _
        " AND lc_datagg = " & CDataSQL(strDtinvigore) & _
        " AND lc_listino = " & nListino & _
        " AND lc_codtpro = 0" & _
        " AND lc_codlavo = " & nCodlavo & _
        " AND lc_unmis = " & CStrSQL(strUnitaMisura) & _
        " AND lc_fase = " & nFase & _
        " ORDER BY codditt, lc_codart, lc_codlavo, lc_conto, lc_listino, lc_codvalu, lc_codtpro," & _
        " lc_datagg DESC, lc_daquant, lc_unmis, lc_fase"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count = 0 Then
        dttTmp.Clear()
        dttTmp.Dispose()
        Return False
      End If
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function

  Public Overridable Function UpdateLISTINIQ1(ByVal strDitta As String, ByVal strDatavalidita As String, _
    ByVal lProgr As Integer) As Boolean
    Dim strSQL As String = ""

    Try
      strSQL = "UPDATE listini" & _
        " SET lc_datscad = " & CDataSQL(DateAdd("d", -1, strDatavalidita)) & "," & _
        " lc_ultagg = " & CDataOraSQL(DateTime.Now) & _
        " WHERE codditt = " & CStrSQL(strDitta) & _
        " AND lc_progr = " & lProgr
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
      Return True
    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
    End Try
  End Function
#End Region


  Public Overridable Function CreaAnalottiDaLottox(ByVal strDitta As String, ByVal strCodart As String, ByVal strDatdoc As String, _
                                                 ByVal strLottox As String, ByVal strStatoProv As String, ByRef lId As Integer, ByRef strErr As String) As Boolean
    Try
      Return CreaAnalottiDaLottox(strDitta, strCodart, strDatdoc, strLottox, strStatoProv, lId, strErr, strDatdoc, "")

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaAnalottiDaLottox(ByVal strDitta As String, ByVal strCodart As String, ByVal strDatdoc As String, _
                                                   ByVal strLottox As String, ByVal strStatoProv As String, ByRef lId As Integer, _
                                                   ByRef strErr As String, ByVal strDatProd As String, ByVal strDatScad As String) As Boolean

    'crea in automatico l'anagrafica lotti
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim bLottoNew As Boolean = False
    Dim nGGscad As Integer = 0
    Dim nCodlotx As Integer = 0
    Dim lParQual As Integer = 0
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strDitta, strCodart, strDatdoc, strLottox, strStatoProv, lId, strErr, _
                                             strDatdoc, strDatScad})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        lId = NTSCInt(oIn(5))
        strErr = NTSCStr(oIn(6))
        Return CBool(oOut)
      End If
      '----------------

      '----------------------
      'cerco di calcolare la data di scadenza, se non è stata passata in input
      If strDatProd = "" Then strDatProd = strDatdoc
      If strDatScad = "" Then
        strDatScad = strDatdoc
        ValCodiceDb(strCodart, strDitta, "ARTICO", "S", "", dttTmp)
        If dttTmp.Rows.Count > 0 Then
          nCodlotx = NTSCInt(dttTmp.Rows(0)!ar_codtlox)
          If NTSCInt(dttTmp.Rows(0)!ar_sotgru) > 0 Then
            If ValCodiceDb(NTSCStr(dttTmp.Rows(0)!ar_sotgru), strDitta, "TABPQUA", "N") Then
              lParQual = NTSCInt(dttTmp.Rows(0)!ar_sotgru)
            End If
          End If
        End If
        dttTmp.Clear()
        If nCodlotx <> 0 Then
          ValCodiceDb(nCodlotx.ToString, strDitta, "TABLOTX", "N", "", dttTmp)
          If dttTmp.Rows.Count > 0 Then nGGscad = NTSCInt(dttTmp.Rows(0)!tb_ggscad)
          dttTmp.Clear()
        End If
        If nGGscad <> 0 Then strDatScad = NTSCDate(strDatdoc).AddDays(nGGscad).ToShortDateString
      End If

      '----------------------
      'determino il codice numerico del lotto
      ValCodiceDb(strDitta, strDitta, "ANADITAC", "S", "", dttTmp)
      If dttTmp.Rows.Count > 0 Then bLottoNew = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_lotti2) = "S", True, False))
      dttTmp.Clear()

      If bLottoNew = False Then
        lId = NTSCInt(strLottox)
        'Se il progressivo esiste già allora non ha senso crearne un altro. Restituisce l'ID del lotto esistente.
        If ValCodiceDb(lId.ToString, strDitta, "ANALOTTI", "N", , , strCodart) Then Return True
      Else
        If Not LottoxCreaID(strDitta, lId) Then Return False
      End If

      If lId <= 0 Or lId > 999999999 Then
        strErr = oApp.Tr(Me, 129513958100999965, "Impossibile crare l'anagrafica lotto: l'ID lotto minore di 0 o superiore a 999999999")
        Return False
      End If

      strSQL = "INSERT INTO analotti (codditt, alo_codart, alo_lotto, " & _
               "alo_lottox, alo_codlotx, alo_dtprep, alo_dtscad, alo_nazprov, alo_dtcarbus, alo_codpqua) " & _
               " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodart) & ", " & _
               lId & ", " & CStrSQL(strLottox) & ", 0, " & CDataSQL(strDatProd) & ", " & _
               CDataSQL(strDatScad) & ", " & CStrSQL(strStatoProv) & ", " & CDataSQL(strDatdoc) & ", " & _
               lParQual.ToString & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function CreaAnalottiDaLottoxDoc(ByVal strDitta As String, ByVal strCodart As String, ByVal strDatdoc As String, _
                                                      ByVal strLottox As String, ByVal strDatscad As String, ByVal nCodlotx As Integer, _
                                                      ByVal lId As Integer, ByVal strStatoProv As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lParQual As Integer = 0
    Try
      ValCodiceDb(strCodart, strDitta, "ARTICO", "S", "", dttTmp)

      If dttTmp.Rows.Count > 0 Then
        If NTSCInt(dttTmp.Rows(0)!ar_sotgru) > 0 Then
          If ValCodiceDb(NTSCStr(dttTmp.Rows(0)!ar_sotgru), strDitta, "TABPQUA", "N") Then
            lParQual = NTSCInt(dttTmp.Rows(0)!ar_sotgru)
          End If
        End If
      End If

      strSQL = "INSERT INTO analotti (codditt, alo_codart, alo_lotto, " & _
               "alo_lottox, alo_codlotx, alo_dtprep, alo_dtscad, alo_nazprov, alo_dtcarbus, alo_codpqua) " & _
               " VALUES (" & CStrSQL(strDitta) & ", " & CStrSQL(strCodart) & ", " & _
               lId & ", " & CStrSQL(strLottox) & ", " & nCodlotx & ", " & CDataSQL(strDatdoc) & ", " & _
               CDataSQL(strDatscad) & ", " & CStrSQL(strStatoProv) & ", " & CDataSQL(strDatdoc) & ", " & _
               lParQual.ToString & ")"
      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CreaAnalottiDaLottoxDoc_GetProgr(ByVal strDitta As String, ByVal strPrefisso As String) As Integer
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    CreaAnalottiDaLottoxDoc_GetProgr = 1
    Try
      'wss 102042:
      'potrei avere degli articoli gestiti  alotto assegnato manualmente, ed altri con creazione lotto automatico con modalità 'solo progressivo'
      'se automaticamente assegno il lotto 00001, 00002, poi registro sull'articolo con lotto manuale 50000, il nuovo lotto calcolato in automatico sarebbe stato 50003
      'per migliorare la cosa, faccio la MAX solo dei record di analotti collegati ad articoli gestiti a lotti la cui modalità di creazione del lotto prevede il progressivo
      'NB: il progressivo non è detto che debba essere = 'lotto univoco!!!' che viene gestito dopo con un'altra opzione
      strSQL = "SELECT max(alo_lottox) as alo_lottox " & _
               " FROM analotti INNER JOIN artico ON analotti.codditt = artico.codditt AND analotti.alo_codart = artico.ar_codart " & _
               " INNER JOIN tablotx ON artico.codditt = tablotx.codditt AND artico.ar_codtlox = tablotx.tb_codlotx " & _
               " WHERE analotti.codditt = " & CStrSQL(strDitta) & _
               " AND alo_lottox like " & CStrSQL(strPrefisso & "%") & _
               " AND len(alo_lottox) = " & strPrefisso.Length + 5 & _
               " AND artico.ar_geslotti = 'S' " & _
               " AND (tablotx.tb_tipocod1 = 'P' OR tablotx.tb_tipocod2 = 'P' OR tablotx.tb_tipocod3 = 'P' OR tablotx.tb_tipocod4 = 'P')"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
      If dttTmp.Rows.Count > 0 Then
        If NTSCStr(dttTmp.Rows(0)!alo_lottox) <> "" Then
          CreaAnalottiDaLottoxDoc_GetProgr = NTSCInt(NTSCStr(dttTmp.Rows(0)!alo_lottox).Substring(strPrefisso.Length)) + 1
        End If
      End If

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function
  Public Overridable Function ScaricaLottox_CercaLotti(ByVal strDitta As String, ByVal strCodart As String, _
                                                      ByVal lFase As Integer, ByVal strTipscarlotx As String, _
                                                      ByVal strDatdoc As String, ByVal nCodmaga As Integer, _
                                                      ByRef dttOut As DataTable) As Boolean
    'dato un articolo, cerca i lotti da scaricare
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT lp_esist - lp_prenot AS lp_esist, alo_lotto, alo_lottox, alo_dtscad, alo_dtcarbus " & _
               " FROM lotcpro INNER JOIN analotti ON lotcpro.codditt = analotti.codditt " & _
               " AND lotcpro.lp_codart = analotti.alo_codart " & _
               " AND lotcpro.lp_lotto = analotti.alo_lotto " & _
               " WHERE lotcpro.codditt = " & CStrSQL(strDitta) & _
               " AND lp_codart = " & CStrSQL(strCodart) & _
               " AND lp_fase = " & lFase & _
               " AND lp_magaz = " & nCodmaga & _
               " AND lp_lotto <> 0 " & _
               " AND alo_dtcarbus <= " & CDataSQL(strDatdoc) & _
               " AND lp_esist - lp_prenot > 0.00001"
      Select Case strTipscarlotx
        Case "D", "U" 'data scadenza, data scadenza lotto unico
          strSQL += " ORDER BY alo_dtscad ASC, lp_lotto ASC "
        Case "L" 'lifo
          strSQL += " ORDER BY alo_dtcarbus DESC, lp_lotto DESC "
        Case "F" 'fifo
          strSQL += " ORDER BY alo_dtcarbus ASC, lp_lotto ASC "
        Case "M" ' manuale
          strSQL += " AND lp_lotto = -1 AND lp_magaz = -1"
      End Select
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function AccconfGetBlocchi(ByVal strDitta As String, ByVal strTipork As String, _
                                                ByRef dttOut As DataTable, Optional ByRef dbConn As DbConnection = Nothing) As Boolean
    'cerca in accconf i vincoli impostati per l'operatore su visib/modif/cancellaz.
    'per ditta e/o tipork 
    'tipork: 0 = tutti tranne la CG, 1 = tutta la CG, ! = offerte, mo_tipork = ordini/prop. d'ordine, mm_tipork per doc. di magazzino e note prel.
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM accconf" & _
               " WHERE (ac_tipork = " & CStrSQL(strTipork) & IIf(strTipork = "1", "", " OR ac_tipork = '0'").ToString & ") " & _
               " AND (codditt = " & CStrSQL(strDitta) & " OR codditt = '*')" & _
               " AND (ac_opnome = " & CStrSQL(oApp.User.Nome) & " OR ac_ruolo = " & CStrSQL(oApp.User.Gruppo) & ")" & _
               " ORDER BY codditt DESC, ac_opnome DESC, ac_ruolo DESC"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function GetProcedPreload(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = " SELECT pr_testo FROM proced WHERE pr_preload = 'S' AND pr_scriptnet = 'N'"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function GetConfPrcPreload(ByVal strDitta As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = " SELECT pr_testo FROM confproc WHERE codditt = " & CStrSQL(strDitta) & " AND pr_context = 'F' AND pr_scriptnet = 'N'"
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

#Region "GPV Disconnesso"
  Public Overridable Function GPVSerieAmmessaServer(ByVal strDitta As String, ByVal strSerie As String) As Boolean
    Dim strSQL As String = ""
    Try
      'Se trovo la serie nel negozio NON posso usarla
      'La inner join è per prendere solo i negozi disconnessi
      strSQL = "SELECT tb_seriedef FROM tabstab " & _
               " INNER JOIN reoffconf ON tabstab.codditt = reoffconf.codditt AND rc_codstab = tb_codstab " & _
               " WHERE tabstab.codditt = " & CStrSQL(strDitta) & _
               " AND tb_seriedef = " & CStrSQL(strSerie)

      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return False

      'Altrimenti va cercata nella configurazione per punto cassa
      strSQL = "SELECT rc_serie FROM repcrkconf " & _
               " INNER JOIN tabrepc ON repcrkconf.codditt = tabrepc.codditt AND rc_codrepc = tabrepc.tb_codrepc " & _
               " INNER JOIN tabrere ON repcrkconf.codditt = tabrere.codditt AND tabrere.tb_codrere = tabrepc.tb_codrere " & _
               " INNER JOIN reoffconf ON repcrkconf.codditt = reoffconf.codditt AND reoffconf.rc_codstab = tb_codstab " & _
               " WHERE repcrkconf.codditt = " & CStrSQL(strDitta) & _
               " AND rc_serie = " & CStrSQL(strSerie)

      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return False

      Return True 'Non ho trovato la serie disponbile per quel negozio
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GPVSerieAmmessaNegozio(ByVal strDitta As String, ByVal strSerie As String, ByVal lNegozio As Integer) As Boolean
    Dim strSQL As String = ""
    Try
      'Se trovo la serie nel negozio posso usarla
      strSQL = "SELECT tb_seriedef FROM tabstab " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND tb_codstab = " & lNegozio & _
               " AND tb_seriedef = " & CStrSQL(strSerie)

      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return True

      'Altrimenti va cercata nella configurazione per punto cassa
      strSQL = "SELECT rc_serie FROM repcrkconf " & _
               " INNER JOIN tabrepc ON repcrkconf.codditt = tabrepc.codditt AND rc_codrepc = tabrepc.tb_codrepc " & _
               " INNER JOIN tabrere ON repcrkconf.codditt = tabrere.codditt AND tabrere.tb_codrere = tabrepc.tb_codrere " & _
               " WHERE repcrkconf.codditt = " & CStrSQL(strDitta) & _
               " AND tb_codstab = " & lNegozio & _
               " AND rc_serie = " & CStrSQL(strSerie)

      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return True

      Return False 'Non ho trovato la serie disponbile per quel negozio
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function GPVCheckNegozioDisconnesso(ByVal strDitta As String, ByVal lCodStab As Integer, ByRef strTipo As String) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT rc_codstab FROM reoffconf " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND rc_codstab = " & lCodStab

      'Se non c'è la riga su REOFFCONF vuol dire che non è disconesso, quindi il negozio fa parte del server.
      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count = 0 Then
        strTipo = "S"
      Else
        strTipo = "C"
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "Gestione Contatti\Organig"
  Public Overridable Function CaricaOrganizzazioni(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM organig "

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function VerificaPresenzaOrganizzazioni() As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 * FROM organig"

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function VerificaPresenzaContatti() As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT TOP 1 * FROM contatti"

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function RitornaOrganizzazioniOrfane(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      'Cerca le organizzazioni non collegate ai contatti.
      strSQL = "SELECT * FROM organig " & _
               " WHERE og_contatto = 0"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function RitornaOrganizzazioniCollegamentiInesistenti(ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      'Cerca le organizzazioni che puntano a dei contatti inesistenti.
      strSQL = "SELECT * FROM organig " & _
               " WHERE NOT EXISTS (SELECT co_progr FROM contatti WHERE co_progr = og_contatto)"

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Function AzzeraContattiSuOrganizzazioni() As Boolean
    Dim strSQL As String = ""
    Try
      'Pulisce tutti i contatti indicati sulle organizzazioni
      strSQL = "UPDATE organig SET og_contatto = 0"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CercaContattiPerNomeCognome(ByVal strCognome As String, ByVal strNome As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strCognome = strCognome.Trim
      strNome = strNome.Trim
      'Cerca i contatti a parità di Nome\Cognome o viceversa
      If strCognome = "" AndAlso strNome = "" Then
        strSQL = "SELECT * FROM contatti"
      ElseIf strCognome = "" Then
        strSQL = "SELECT * FROM contatti " & _
                 " WHERE " & CStrSQL(strNome) & " IN (co_descont, co_descont2)"
      ElseIf strNome = "" Then
        strSQL = "SELECT * FROM contatti " & _
                 " WHERE " & CStrSQL(strCognome) & " IN (co_descont, co_descont2)"
      Else
        strSQL = "SELECT * FROM contatti " & _
                 " WHERE co_descont IN (" & CStrSQL(strCognome) & ", " & CStrSQL(strNome) & ")" & _
                 "   AND co_descont2 IN  (" & CStrSQL(strCognome) & ", " & CStrSQL(strNome) & ")"
      End If

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CercaContattiPerNomeCognomeEsatto(ByVal strCognome As String, ByVal strNome As String, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strCognome = strCognome.Trim
      strNome = strNome.Trim
      'Cerca i contatti a parità di Nome\Cognome o viceversa
      strSQL = "SELECT * FROM contatti " & _
               " WHERE co_descont IN (" & CStrSQL(strCognome) & ", " & CStrSQL(strNome) & ")" & _
               "   AND co_descont2 IN  (" & CStrSQL(strCognome) & ", " & CStrSQL(strNome) & ")"
      
      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CreaNuovoContatto(ByVal dtrOrganig As DataRow) As Integer
    Dim dbConn As DbConnection = Nothing
    Dim strSQL As String = ""
    Dim lProgr As Integer
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      lProgr = RitornaNumeratoreContatti(dbConn)

      'Crea il nuovo contatto
      With dtrOrganig
        strSQL = "INSERT INTO contatti (co_progr , co_descont, co_descont2, co_titolo, co_indir, co_cap, co_citta, co_prov, co_stato, co_datnasc, " & _
                 "                      co_sesso, co_coperat, co_fbuser, co_fbpwd, co_twitteruser, co_twitterpwd, co_skypeuser, co_skypepwd, " & _
                 "                      co_telefpers, co_faxpers, co_emailpers, co_cellpers, co_codstco, co_referente) " & _
                 " VALUES (" & lProgr & ", " & CStrSQL(!og_descont) & ", " & CStrSQL(!og_descont2) & ", " & CStrSQL(!og_titolo) & ", " & _
                               CStrSQL(!og_indir) & ", " & CStrSQL(!og_cap) & ", " & CStrSQL(!og_citta) & ", " & _
                               CStrSQL(!og_prov) & ", " & CStrSQL(!og_stato) & ", " & IIf(!og_datnasc Is DBNull.Value, "Null", CDataSQL(NTSCDate(!og_datnasc))) & ", " & _
                               CStrSQL(!og_sesso) & ", " & CStrSQL(!og_coperat) & ", " & CStrSQL(!og_fbuser) & ", " & _
                               CStrSQL(!og_fbpwd) & ", " & CStrSQL(!og_twitteruser) & ", " & CStrSQL(!og_twitterpwd) & ", " & _
                               CStrSQL(!og_skypeuser) & ", " & CStrSQL(!og_skypepwd) & ", " & CStrSQL(!og_telefpers) & ", " & _
                               CStrSQL(!og_faxpers) & ", " & CStrSQL(!og_emailpers) & ", " & CStrSQL(!og_cellpers) & ", " & _
                               NTSCInt(!og_codstco) & ", " & NTSCInt(!og_referente) & ")"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        'Lo aggancia all'organizzazione
        strSQL = "UPDATE organig SET og_contatto = " & lProgr & _
                 " WHERE codditt = " & CStrSQL(!codditt) & _
                 "   AND og_progr = " & NTSCInt(!og_progr)

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End With

      '----------------------------------
      'chiudo la transazione ed il database
      ChiudiTrans()
      dbConn.Close()
    Catch ex As Exception
      '--------------------------------------------------------------
      'se sono in transazione la annullo
      If IsInTrans Then AnnullaTrans()
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return lProgr
  End Function
  Public Overridable Function RitornaNumeratoreContatti(ByVal dbConn As DbConnection) As Integer
    Dim dttTmp As New DataTable
    Dim strSQL As String = ""
    Dim lProgr As Integer = 1
    Try
      'Cerco il primo numeratore libero
      strSQL = "SELECT MAX(co_progr) AS NUM FROM contatti"

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      If dttTmp.Rows.Count > 0 Then lProgr = NTSCInt(dttTmp.Rows(0)!NUM) + 1
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return lProgr
  End Function

  Public Overridable Function CollegaOrganizzazioneAContatto(ByVal dtrOrganig As DataRow, ByVal dtrContatto As DataRow) As Boolean
    Dim dbConn As DbConnection = Nothing
    Dim strSQL As String = ""
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      'Controlla se ci sono dei campi interessanti da riversare sul contatto (che ancora il contatto non ha)
      If NTSCStr(dtrOrganig!og_descont) <> "" AndAlso NTSCStr(dtrContatto!co_descont).Trim = "" Then strSQL &= "co_descont = " & CStrSQL(dtrOrganig!og_descont) & ", "
      If NTSCStr(dtrOrganig!og_descont2) <> "" AndAlso NTSCStr(dtrContatto!co_descont2).Trim = "" Then strSQL &= "co_descont2 = " & CStrSQL(dtrOrganig!og_descont2) & ", "
      If NTSCStr(dtrOrganig!og_titolo) <> "" AndAlso NTSCStr(dtrContatto!co_titolo).Trim = "" Then strSQL &= "co_titolo = " & CStrSQL(dtrOrganig!og_titolo) & ", "
      If NTSCStr(dtrOrganig!og_indir) <> "" AndAlso NTSCStr(dtrContatto!co_indir).Trim = "" Then strSQL &= "co_indir = " & CStrSQL(dtrOrganig!og_indir) & ", "
      If NTSCStr(dtrOrganig!og_cap) <> "" AndAlso NTSCStr(dtrContatto!co_cap).Trim = "" Then strSQL &= "co_cap = " & CStrSQL(dtrOrganig!og_cap) & ", "
      If NTSCStr(dtrOrganig!og_citta) <> "" AndAlso NTSCStr(dtrContatto!co_citta).Trim = "" Then strSQL &= "co_citta = " & CStrSQL(dtrOrganig!og_citta) & ", "
      If NTSCStr(dtrOrganig!og_prov) <> "" AndAlso NTSCStr(dtrContatto!co_prov).Trim = "" Then strSQL &= "co_prov = " & CStrSQL(dtrOrganig!og_prov) & ", "
      If NTSCStr(dtrOrganig!og_stato) <> "" AndAlso NTSCStr(dtrContatto!co_stato).Trim = "" Then strSQL &= "co_stato = " & CStrSQL(dtrOrganig!og_stato) & ", "
      If NTSCStr(dtrOrganig!og_datnasc) <> "" AndAlso NTSCStr(dtrContatto!co_datnasc).Trim = "" Then strSQL &= "co_datnasc = " & CDataSQL(NTSCDate(dtrOrganig!og_datnasc)) & ", "
      'If NTSCStr(dtrOrganig!og_sesso) <> "" AndAlso NTSCStr(dtrContatto!co_sesso).Trim = "" Then strSQL &= "co_sesso = " & CStrSQL(dtrOrganig!og_sesso) & ", "
      If NTSCStr(dtrOrganig!og_coperat) <> "" AndAlso NTSCStr(dtrContatto!co_coperat).Trim = "" Then strSQL &= "co_coperat = " & CStrSQL(dtrOrganig!og_coperat) & ", "
      If NTSCStr(dtrOrganig!og_fbuser) <> "" AndAlso NTSCStr(dtrContatto!co_fbuser).Trim = "" Then strSQL &= "co_fbuser = " & CStrSQL(dtrOrganig!og_fbuser) & ", "
      If NTSCStr(dtrOrganig!og_fbpwd) <> "" AndAlso NTSCStr(dtrContatto!co_fbpwd).Trim = "" Then strSQL &= "co_fbpwd = " & CStrSQL(dtrOrganig!og_fbpwd) & ", "
      If NTSCStr(dtrOrganig!og_twitteruser) <> "" AndAlso NTSCStr(dtrContatto!co_twitteruser).Trim = "" Then strSQL &= "co_twitteruser = " & CStrSQL(dtrOrganig!og_twitteruser) & ", "
      If NTSCStr(dtrOrganig!og_twitterpwd) <> "" AndAlso NTSCStr(dtrContatto!co_twitterpwd).Trim = "" Then strSQL &= "co_twitterpwd = " & CStrSQL(dtrOrganig!og_twitterpwd) & ", "
      If NTSCStr(dtrOrganig!og_skypeuser) <> "" AndAlso NTSCStr(dtrContatto!co_skypeuser).Trim = "" Then strSQL &= "co_skypeuser = " & CStrSQL(dtrOrganig!og_skypeuser) & ", "
      If NTSCStr(dtrOrganig!og_skypepwd) <> "" AndAlso NTSCStr(dtrContatto!co_skypepwd).Trim = "" Then strSQL &= "co_skypepwd = " & CStrSQL(dtrOrganig!og_skypepwd) & ", "
      If NTSCStr(dtrOrganig!og_telefpers) <> "" AndAlso NTSCStr(dtrContatto!co_telefpers).Trim = "" Then strSQL &= "co_telefpers = " & CStrSQL(dtrOrganig!og_telefpers) & ", "
      If NTSCStr(dtrOrganig!og_faxpers) <> "" AndAlso NTSCStr(dtrContatto!co_faxpers).Trim = "" Then strSQL &= "co_faxpers = " & CStrSQL(dtrOrganig!og_faxpers) & ", "
      If NTSCStr(dtrOrganig!og_emailpers) <> "" AndAlso NTSCStr(dtrContatto!co_emailpers).Trim = "" Then strSQL &= "co_emailpers = " & CStrSQL(dtrOrganig!og_emailpers) & ", "
      If NTSCStr(dtrOrganig!og_cellpers) <> "" AndAlso NTSCStr(dtrContatto!co_cellpers).Trim = "" Then strSQL &= "co_cellpers = " & CStrSQL(dtrOrganig!og_cellpers) & ", "
      If NTSCInt(dtrOrganig!og_codstco) <> 0 AndAlso NTSCInt(dtrContatto!co_codstco) = 0 Then strSQL &= "co_codstco = " & NTSCInt(dtrOrganig!og_codstco) & ", "
      If NTSCInt(dtrOrganig!og_referente) <> 0 AndAlso NTSCInt(dtrContatto!co_referente) = 0 Then strSQL &= "co_referente = " & NTSCInt(dtrOrganig!og_referente) & ", "

      If strSQL <> "" Then
        strSQL = "UPDATE contatti SET " & strSQL.Remove(strSQL.Length - 2) & _
                 " WHERE co_progr = " & NTSCInt(dtrContatto!co_progr)

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

      'Aggancia l'organizzazione al contatto
      strSQL = "UPDATE organig SET og_contatto = " & NTSCInt(dtrContatto!co_progr) & _
               " WHERE codditt = " & CStrSQL(dtrOrganig!codditt) & _
               "   AND og_progr = " & NTSCInt(dtrOrganig!og_progr)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      AggiornaOrganizzazioniDaContatto(NTSCInt(dtrContatto!co_progr), dbConn)

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
  Public Overridable Function AggiornaContattoDaOrganizzazione(ByVal strDitta As String, ByVal lOrganig As Integer) As Boolean
    Dim dbConn As DbConnection = Nothing
    Dim dttOrga As DataTable = Nothing
    Dim strSQL As String = ""
    Try
      RitornaOrganizzazioneDaProgressivo(strDitta, lOrganig, dttOrga)

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      strSQL = "UPDATE contatti SET co_descont = og_descont, " & _
               "                    co_descont2 = og_descont2, " & _
               "                    co_titolo = og_titolo, " & _
               "                    co_indir = og_indir, " & _
               "                    co_cap = og_cap, " & _
               "                    co_citta = og_citta, " & _
               "                    co_prov = og_prov, " & _
               "                    co_stato = og_stato, " & _
               "                    co_datnasc = og_datnasc, " & _
               "                    co_sesso = og_sesso, " & _
               "                    co_coperat = CASE WHEN og_codlead = 0 AND og_conto = 0 THEN og_coperat ELSE co_coperat END, " & _
               "                    co_fbuser = og_fbuser, " & _
               "                    co_fbpwd = og_fbpwd, " & _
               "                    co_twitteruser = og_twitteruser, " & _
               "                    co_twitterpwd = og_twitterpwd, " & _
               "                    co_skypeuser = og_skypeuser, " & _
               "                    co_skypepwd = og_skypepwd, " & _
               "                    co_telefpers = og_telefpers, " & _
               "                    co_faxpers = og_faxpers, " & _
               "                    co_emailpers = og_emailpers, " & _
               "                    co_cellpers = og_cellpers, " & _
               "                    co_codstco = og_codstco, " & _
               "                    co_referente = og_referente " & _
               " FROM organig INNER JOIN contatti ON og_contatto = co_progr " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND og_progr = " & NTSCInt(lOrganig)

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      AggiornaOrganizzazioniDaContatto(NTSCInt(dttOrga.Rows(0)!og_contatto), dbConn)

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

  Public Overridable Function AggiornaOrganizzazioniDaContatto(ByVal lContatto As Integer, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      EliminaOrganizzazioniLeadFittizio(lContatto, dbConn)

      'Aggiorna tutte le organizzazioni con i dati del contatto aggiornato.
      strSQL = "UPDATE organig SET og_descont = co_descont, " & _
               "                   og_descont2 = co_descont2, " & _
               "                   og_titolo = co_titolo, " & _
               "                   og_indir = co_indir, " & _
               "                   og_cap = co_cap, " & _
               "                   og_citta = co_citta, " & _
               "                   og_prov = co_prov, " & _
               "                   og_stato = co_stato, " & _
               "                   og_datnasc = co_datnasc, " & _
               "                   og_sesso = co_sesso, " & _
               "                   og_coperat = CASE WHEN og_codlead = 0 AND og_conto = 0 THEN co_coperat ELSE og_coperat END, " & _
               "                   og_fbuser = co_fbuser, " & _
               "                   og_fbpwd = co_fbpwd, " & _
               "                   og_twitteruser = co_twitteruser, " & _
               "                   og_twitterpwd = co_twitterpwd, " & _
               "                   og_skypeuser = co_skypeuser, " & _
               "                   og_skypepwd = co_skypepwd, " & _
               "                   og_telefpers = co_telefpers, " & _
               "                   og_faxpers = co_faxpers, " & _
               "                   og_emailpers = co_emailpers, " & _
               "                   og_cellpers = co_cellpers, " & _
               "                   og_codstco = co_codstco, " & _
               "                   og_referente = co_referente " & _
               " FROM organig INNER JOIN contatti ON og_contatto = co_progr " & _
               " WHERE og_contatto = " & lContatto

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function EliminaOrganizzazioniLeadFittizio(ByVal lContatto As Integer, ByVal dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "DELETE FROM organig " & _
               " WHERE og_contatto = " & lContatto & _
               "   AND og_codlead = -1 " & _
               "   AND 1 < (SELECT COUNT(codditt) FROM organig AS o2 " & _
               "             WHERE o2.codditt = organig.codditt " & _
               "               AND o2.og_contatto = organig.og_contatto) "

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function RitornaOrganizzazioniCollegateAContatto(ByVal lContatto As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      'Ritorna tutte le organizzazioni collegate ad un contatto
      strSQL = "SELECT * FROM organig " & _
               " WHERE og_contatto = " & lContatto

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function RitornaOrganizzazioneDaProgressivo(ByVal strDitta As String, ByVal lOrganig As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      'Ritorna tutte le organizzazioni collegate ad un contatto
      strSQL = "SELECT * FROM organig " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND og_progr = " & lOrganig

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "Trasferimento Configurazione Business E-mail da Organig a EmailConf"
  Public Overridable Function VerificaDatiTrasferiti() As Boolean
    Dim dttTmp As DataTable = Nothing
    Dim strSQL As String = ""
    Try
      'Controlla se in emailconf sono presenti delle configurazioni
      strSQL = "SELECT TOP 1 * FROM emailconf "

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return (dttTmp.Rows.Count > 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function


  Public Overridable Function RitornaConfigurazioniPostaOrganig() As DataTable
    Dim strSQL As String = ""
    Try
      'Considera solo le configurazioni che hanno o il nome del server in arrivo o il nome del server in uscita.
      strSQL = "SELECT * FROM organig " & _
               " WHERE (og_nomeserv IS NOT NULL OR og_nomeservu IS NOT NULL) AND og_email IS NOT NULL "

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return Nothing
  End Function

  Public Overridable Function AggiungiConfigurazioneEmailDaOrganig(ByVal dtrOrganig As DataRow) As Boolean
    Dim strSQL As String = ""
    Try
      With dtrOrganig
        'Verifica se è già presente un indirizzo uguale per lo stesso operatore
        strSQL = "SELECT * FROM emailconf " & _
                 " WHERE em_user = " & CStrSQL(!og_coperat) & _
                 "   AND em_email = " & CStrSQL(!og_email)

        If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI).Rows.Count > 0 Then Return True ' Scarta i record già inseriti.

        strSQL = "INSERT INTO emailconf (em_user, em_email, em_exchuser, em_pwdmail, em_authsecure, em_useimap, em_nomeserv, em_portaserv, " & _
                 "                       em_securec, em_configu, em_nomeservu, em_portaservu, em_securecu, em_exchuseru, em_pwdmailu, em_ultaggmail, " & _
                 "                       em_firmamail) " & _
                 " VALUES (" & CStrSQL(!og_coperat) & "," & CStrSQL(!og_email) & "," & CStrSQL(!og_exchuser) & "," & _
                               CStrSQL(!og_pwdmail) & "," & CStrSQL(!og_authsecure) & "," & CStrSQL(!og_useimap) & "," & CStrSQL(!og_nomeserv) & "," & _
                               NTSCInt(!og_portaserv) & ", " & CStrSQL(!og_securec) & "," & CStrSQL(!og_configu) & ", " & CStrSQL(!og_nomeservu) & ", " & _
                               NTSCInt(!og_portaservu) & ", " & CStrSQL(!og_securecu) & ", " & CStrSQL(!og_exchuseru) & ", " & CStrSQL(!og_pwdmailu) & ", " & _
                               CDataSQL(NTSCDate(!og_ultaggmail)) & ", " & CStrSQL(!og_firmamail) & ")"
      End With

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function SvuotaConfigurazionePostaSuOrganig() As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE organig SET og_nomeserv = NULL, " & _
               "                   og_portaserv = 0, " & _
               "                   og_nomeservu = NULL, " & _
               "                   og_portaservu = 0,  " & _
               "                   og_pwdmail = NULL, " & _
               "                   og_securec = 'N', " & _
               "                   og_ultaggmail = NULL, " & _
               "                   og_securecu = 'N', " & _
               "                   og_firmamail = NULL, " & _
               "                   og_exchuser = NULL, " & _
               "                   og_configu = NULL, " & _
               "                   og_exchuseru = NULL, " & _
               "                   og_pwdmailu = NULL, " & _
               "                   og_authsecure = NULL, " & _
               "                   og_useimap = 'N'"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CancellaOpzioniRegistroConfigurazionePosta() As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT * FROM REGPROP " & _
               " WHERE rp_liv1 IN ('BSEMGMAI', 'BSXXWMAI') " & _
               "   AND rp_liv2 = 'OPZIONIUT' " & _
               "   AND rp_nomprop IN ('AUTHSECURE', 'AUTH', 'LOG', 'PWD') "

      Execute(strSQL, CLE__APP.DBTIPO.DBPRC)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "Creazione automatica attività per diario"
  Public Overridable Function RitornaTipoAttivitaDefault(ByVal strDitta As String, ByVal strTipoAttivita As String) As Integer
    Dim dttTmp As New DataTable
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tb_codtaco FROM tabtaco " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               "   AND tb_def" & strTipoAttivita & " = 'S'"

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If dttTmp.Rows.Count > 0 Then Return NTSCInt(dttTmp.Rows(0)!tb_codtaco)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return 0
  End Function


  Public Overridable Function SalvaNuovaAttivita(ByVal dttCract As DataTable) As Boolean
    Dim dbConn As DbConnection = Nothing
    Dim strSQL As String = ""
    Dim lProgr As Integer
    Try
      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      lProgr = LegNuma(NTSCStr(dttCract.Rows(0)!codditt), "C7", " ", 0, True, dbConn)
      lProgr = AggNuma(NTSCStr(dttCract.Rows(0)!codditt), "C7", " ", 0, lProgr, True, False, "", dbConn)

      dttCract.Rows(0)!ca_codcrac = lProgr

      strSQL = "INSERT INTO cract " & GetQueryInsertField(dttCract, "ca_") & _
               " VALUES " & GetQueryInsertValue(dttCract, dttCract.Rows(0), "ca_")

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

      With dttCract.Rows(0)
        strSQL = "INSERT INTO cractopp (cap_codcrac, cap_opcrmincpr, cap_status, cap_dataes, cap_oraes) " & _
                 " VALUES (" & lProgr & ", " & CStrSQL(!ca_opnome) & ", 'E', " & CDataSQL(NTSCDate(!ca_dataes)) & ", " & _
                 "         " & CDblSQL(NTSCDec(!ca_oraes)) & ")"
      End With

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

#End Region

#Region "Generazione Lead Fittizio"
  Public Overridable Function GeneraLeadFittizio() As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "INSERT INTO leads (codditt, le_codlead, le_descr1, le_ultagg, le_privacy) " & _
               " SELECT codditt, -1, 'Lead Fittizio', GetDate(), 'S' " & _
               "  FROM tabanaz " & _
               " WHERE NOT EXISTS (SELECT leads.codditt FROM leads " & _
               "                    WHERE le_codlead = -1 " & _
               "                      AND leads.codditt = tabanaz.codditt)"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "Collegamento Guest e utenti CRM"
  Public Overridable Function TrovaAccessiLeadDaLead(ByVal strDitta As String, ByVal lLead As Integer) As DataTable
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DISTINCT acclead.* FROM acclead	" & _
               " WHERE acclead.codditt = " & CStrSQL(strDitta) & _
               "   AND opcr_codlead = " & lLead

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return Nothing
  End Function
  Public Overridable Function TrovaOperatoriGuestDaAccessiLead(ByVal strUtenteCRM As String) As DataTable
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT DISTINCT og_coperat, acclead.* FROM acclead	" & _
               " INNER JOIN organig ON organig.codditt = acclead.codditt AND og_codlead = opcr_codlead " & _
               " WHERE opcr_opnome = " & CStrSQL(strUtenteCRM) & _
               "   AND og_coperat <> ''"

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return Nothing
  End Function

  Public Overridable Function CreaRelazione(ByVal strUser1 As String, ByVal strUser2 As String) As Boolean
    Dim dbConn As DbConnection = Nothing
    Dim dttTmp As New DataTable
    Dim strSQL As String = ""
    Dim lRelaz As Integer = 0
    Try
      'Prima di tutto controlla che entrambi gli utenti sia B4P
      strSQL = "SELECT * FROM operat " & _
               " WHERE OpNome IN (" & CStrSQL(strUser1) & ", " & CStrSQL(strUser2) & ")" & _
               "   AND OpSuTipoUser <> 'N'"

      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBPRC).Rows.Count <> 2 Then Return False

      '---------------------------------
      'apro il database e la transazione
      dbConn = ApriDB(CLE__APP.DBTIPO.DBAZI)
      ApriTrans(dbConn)

      'Verifica se hanno già una relazione
      strSQL = "SELECT TOP 1 * FROM surelaz " & _
               " WHERE sur_opnome  = " & CStrSQL(strUser2) & _
               "   AND sur_opnome2 = " & CStrSQL(strUser1)

      If OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn).Rows.Count = 0 Then
        'Crea la relazione tra i 2 utenti
        strSQL = "SELECT MAX(sur_relid) AS sur_relid FROM surelaz"

        dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        If dttTmp.Rows.Count = 0 Then
          lRelaz = 1
        Else
          lRelaz = NTSCInt(dttTmp.Rows(0)!sur_relid) + 1
        End If

        strSQL = "INSERT INTO surelaz (sur_relid, sur_opnome, sur_opnome2) " & _
                 " VALUES (" & lRelaz & ", " & CStrSQL(strUser1) & ", " & CStrSQL(strUser2) & ")"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)

        strSQL = "INSERT INTO surelaz (sur_relid, sur_opnome, sur_opnome2) " & _
                 " VALUES (" & (lRelaz + 1) & ", " & CStrSQL(strUser2) & ", " & CStrSQL(strUser1) & ")"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      Else
        'C'era già la relazione, aggiorno lo stato di attiva
        strSQL = "UPDATE surelaz SET sur_attiva = 'S' " & _
                 " WHERE sur_opnome  IN (" & CStrSQL(strUser1) & ", " & CStrSQL(strUser2) & ")" & _
                 "   AND sur_opnome2 IN (" & CStrSQL(strUser1) & ", " & CStrSQL(strUser2) & ")"

        Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      End If

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
#End Region

#Region "Promozioni"
  Public Overridable Function CaricaContrattiCliente(ByVal strDitta As String, ByVal lConto As Integer, ByVal lCoddest As Integer, _
                                                     ByVal dtDatPromo As Date, ByVal dOraPromo As Decimal) As DataTable
    Dim strSQL As String = ""
    Dim strInDest As String = ""
    Try
      'Se la destinazione non è passata non ha senso gestire l'opzione per considerare solo le promozioni per destinazione.
      If lCoddest > 0 Then
        If GetSettingBus("BSREPROM", "OPZIONI", ".", "SoloPromozioniDestinazione", "0", ".", "0") = "0" Then strInDest = "0, "
      End If

      If oApp.oGvar.bRilevaContrattiDaContoFatt AndAlso lConto > 0 Then
        strSQL = "SELECT an_contfatt FROM anagra WHERE codditt = " & CStrSQL(strDitta) & " AND an_conto = " & lConto
        Dim dttContoFatt As DataTable = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
        If dttContoFatt.Rows.Count > 0 Then
          If NTSCInt(dttContoFatt.Rows(0)!an_contfatt) > 0 Then lConto = NTSCInt(dttContoFatt.Rows(0)!an_contfatt) 'trovato: lo usa al posto del conto passato alla funzione
        End If
      End If

      'Carica solo i contratti di gestione documenti\ordini\altri o disponibili anche in GPV.
      'Esclude le tessere di GPV
      'Tiene conto della data validità del contratto\tipologia di contratto
      strSQL = "SELECT TOP 6 refites.* FROM refites " & _
               " INNER JOIN tabrett ON tabrett.codditt = refites.codditt AND tb_codrett = ret_codrett " & _
               " WHERE refites.codditt = " & CStrSQL(strDitta) & _
               "   AND ret_conto = " & lConto & _
               "   AND ret_coddest IN (" & strInDest & lCoddest & ") " & _
               "   AND tb_sologpv <> 'S' " & _
               "   AND " & CDataSQL(dtDatPromo) & " BETWEEN ret_dtiniz AND ret_dtscad " & _
               "   AND tb_dtscad >= " & CDataSQL(dtDatPromo) & _
               " ORDER BY ret_coddest DESC, ret_codtes"

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return Nothing
  End Function
  Public Overridable Function CaricaContrattiAgente(ByVal strDitta As String, ByVal lAgente As Integer, _
                                                    ByVal dtDatPromo As Date, ByVal dOraPromo As Decimal) As DataTable
    Dim strSQL As String = ""
    Try
      'Carica solo i contratti associati agli agenti
      'Tiene conto della data validità del contratto\tipologia di contratto
      strSQL = "SELECT TOP 6 refites.* FROM refites " & _
               " INNER JOIN tabrett ON tabrett.codditt = refites.codditt AND tb_codrett = ret_codrett " & _
               " WHERE refites.codditt = " & CStrSQL(strDitta) & _
               "   AND ret_codcage = " & lAgente & _
               "   AND " & CDataSQL(dtDatPromo) & " BETWEEN ret_dtiniz AND ret_dtscad " & _
               "   AND tb_dtscad >= " & CDataSQL(dtDatPromo) & _
               " ORDER BY ret_coddest DESC, ret_codtes"

      Return OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
    Return Nothing
  End Function


  Public Overridable Function ArticoliInListaSelezionata(ByVal strDitta As String, ByVal lCodlsar As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT listsar.* FROM listsar " & _
               "WHERE codditt = " & CStrSQL(strDitta) & _
               " AND lsa_codlsar = " & NTSCInt(lCodlsar)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function ControllaPromoArticoli(ByVal strDitta As String, ByVal dtrPromo1 As DataRow, ByVal dtrPromo2 As DataRow) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As DataTable
    Try
      strSQL = "SELECT ar_codart FROM artico " & _
               " WHERE artico.codditt = " & CStrSQL(strDitta)

      'Metto gli articoli delle promozioni nella where, se la query trova degli articoli, vuol dire che ci sono degli articoli in comune
      If Not WherePromozione(strDitta, dtrPromo1, strSQL) Then Return False
      If Not WherePromozione(strDitta, dtrPromo2, strSQL) Then Return False

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If dttTmp.Rows.Count > 0 Then Return False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function ControllaPromoSingoloArticolo(ByVal strDitta As String, ByVal dtrPromo1 As DataRow, ByVal dtrPromo2 As DataRow, ByVal strCodart As String) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As DataTable
    Try
      strSQL = "SELECT ar_codart FROM artico " & _
               " WHERE artico.codditt = " & CStrSQL(strDitta) & _
               " AND artico.ar_codart = " & CStrSQL(strCodart)

      'Metto gli articoli delle promozioni nella where, se la query trova degli articoli, vuol dire che ci sono degli articoli in comune
      If Not WherePromozione(strDitta, dtrPromo1, strSQL) Then Return False
      If Not WherePromozione(strDitta, dtrPromo2, strSQL) Then Return False

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      If dttTmp.Rows.Count > 0 Then Return False

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function WherePromozione(ByVal strDitta As String, ByVal dtrPromo As DataRow, ByRef strSQL As String) As Boolean
    Try
      'In base al tipo promozione applico una condizione diversa nella where per verificare la compatibilità tra gli articoli
      If NTSCStr(dtrPromo!tb_codart) <> "" Then
        strSQL &= " AND ar_codart = " & CStrSQL(dtrPromo!tb_codart)
      ElseIf NTSCInt(dtrPromo!tb_codlsar) > 0 Then
        strSQL &= " AND ar_codart IN (SELECT lsa_codart FROM listsar " & _
                                       "WHERE codditt = " & CStrSQL(strDitta) & _
                                       " AND lsa_codlsar = " & NTSCInt(dtrPromo!tb_codlsar) & ")"
      Else
        If NTSCInt(dtrPromo!tb_gruppo) > 0 Then
          strSQL &= " AND ar_gruppo = " & NTSCInt(dtrPromo!tb_gruppo)
        End If
        If NTSCInt(dtrPromo!tb_sotgru) > 0 Then
          strSQL &= " AND ar_sotgru = " & NTSCInt(dtrPromo!tb_sotgru)
        End If
        If NTSCStr(dtrPromo!tb_codcfam).Trim <> "" Then
          strSQL &= " AND ar_famprod = " & CStrSQL(dtrPromo!tb_codcfam)
        End If
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaTipoTesseraDaTessera(ByVal strDitta As String, ByVal lTessera As Integer, ByRef dttTipoTessera As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      If lTessera = 0 Then
        strSQL = "SELECT tabrett.* FROM tabrett " & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND tb_tipo = 'N'"
      Else
        strSQL = "SELECT tabrett.* FROM tabrett " & _
                 " INNER JOIN refites ON tabrett.codditt = refites.codditt AND tb_codrett = ret_codrett " & _
                 " WHERE tabrett.codditt = " & CStrSQL(strDitta) & _
                 " AND refites.ret_codtes = " & lTessera
      End If

      dttTipoTessera = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CaricaPromoDaTipoTessera(ByVal strDitta As String, ByVal lTipoTessera As Integer, ByVal lReparto As Integer, _
                                                       ByVal lStabilimento As Integer, ByVal bApplicabiliAdesso As Boolean, ByVal dtDate As Date, _
                                                       ByVal dOra As Decimal, ByRef dttPromo As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT tabrepr.*, 0 AS xx_npromo, 0 AS xx_nprclie, 0 AS xx_contratto, 0 AS xx_agente, 0 AS xx_nagente, rel_prior AS xx_prior, -1 AS backcolor_row " & _
               " FROM tabrepr " & _
               " LEFT JOIN relinkpt ON tabrepr.codditt = relinkpt.codditt AND tabrepr.tb_codrepr = relinkpt.rel_codrepr " & _
               " WHERE tabrepr.codditt = " & CStrSQL(strDitta) & _
               " AND relinkpt.rel_codrett = " & lTipoTessera & _
               " AND rel_abilitato = 'S'"

      If lReparto <> 0 Or lStabilimento <> 0 Then
        strSQL &= " AND ((tb_codrere = 0 AND (SELECT count(*) " & _
                  "                            FROM rereprom " & _
                  "                           WHERE rereprom.codditt = tabrepr.codditt " & _
                  "                             AND rp_codrepr = tb_codrepr) = 0) " & _
                  "       OR tb_codrere = " & lReparto & " OR (SELECT count(*) " & _
                  "                                             FROM rereprom " & _
                  "                                            WHERE rereprom.codditt = tabrepr.codditt " & _
                  "                                              AND rp_codrepr = tb_codrepr " & _
                  "                                              AND (rp_codrere = " & lReparto & _
                  "                                                OR (rp_codstab = " & lStabilimento & _
                  "                                                AND rp_codrere = 0))) > 0)"
      End If

      If bApplicabiliAdesso Then
        strSQL &= " AND " & CDataSQL(dtDate) & " BETWEEN tb_datini AND tb_datfin " & _
                  " AND " & CDblSQL(dOra) & " BETWEEN tb_orini AND tb_orfin "

        Select Case dtDate.DayOfWeek
          Case DayOfWeek.Monday : strSQL &= " AND tb_fllun = 'S'"
          Case DayOfWeek.Tuesday : strSQL &= " AND tb_flmar = 'S'"
          Case DayOfWeek.Wednesday : strSQL &= " AND tb_flmer = 'S'"
          Case DayOfWeek.Thursday : strSQL &= " AND tb_flgio = 'S'"
          Case DayOfWeek.Friday : strSQL &= " AND tb_flven = 'S'"
          Case DayOfWeek.Saturday : strSQL &= " AND tb_flsab = 'S'"
          Case DayOfWeek.Sunday : strSQL &= " AND tb_fldom = 'S'"
        End Select
      End If

      strSQL &= " ORDER BY rel_prior "

      dttPromo = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CaricaPromoSpecificheCliente(ByVal strDitta As String, ByVal lConto As Integer, ByVal lReparto As Integer, ByVal dtDate As Date, _
                                                           ByVal dOra As Decimal, ByRef dttPromo As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      'Tira su una sola riga per ogni promozione valida, con indicato anche il numero di volte che quella promozione è presente.
      strSQL = "SELECT (tb_npromo * " & _
               "(SELECT COUNT(codditt) FROM relinkpc AS relinkpc_2 " & _
               " WHERE(relinkpc_2.codditt = tabrepr.codditt And relinkpc_2.rel_codrepr = tb_codrepr) " & _
               " AND relinkpc_2.rel_conto = " & lConto & ")) AS tb_npromo,  " & _
               " rel_datini AS tb_datini, rel_datfin AS tb_datfin, " & _
               " tabrepr.*, 0 AS xx_npromo, 0 AS xx_nprclie, 0 AS xx_contratto, 0 AS xx_prior " & _
               " FROM tabrepr " & _
               " LEFT JOIN relinkpc ON tabrepr.codditt = relinkpc.codditt AND tabrepr.tb_codrepr = relinkpc.rel_codrepr " & _
               " WHERE tabrepr.codditt = " & CStrSQL(strDitta) & _
               " AND relinkpc.rel_conto = " & lConto & _
               " AND relinkpc.rel_progr = (SELECT Min(rel_progr) FROM relinkpc AS relinkpc_3 " & _
               "                           WHERE relinkpc_3.codditt = tabrepr.codditt And relinkpc_3.rel_codrepr = tb_codrepr " & _
               "                           AND relinkpc_3.rel_conto = " & lConto & ") "

      If lReparto <> 0 Then strSQL &= " AND (tb_codrere = 0 OR tb_codrere = " & lReparto & ")"

      strSQL &= " AND " & CDataSQL(dtDate) & " BETWEEN rel_datini AND rel_datfin " & _
                " AND " & CDblSQL(dOra) & " BETWEEN tb_orini AND tb_orfin "

      Select Case dtDate.DayOfWeek
        Case DayOfWeek.Monday : strSQL &= " AND tb_fllun = 'S'"
        Case DayOfWeek.Tuesday : strSQL &= " AND tb_flmar = 'S'"
        Case DayOfWeek.Wednesday : strSQL &= " AND tb_flmer = 'S'"
        Case DayOfWeek.Thursday : strSQL &= " AND tb_flgio = 'S'"
        Case DayOfWeek.Friday : strSQL &= " AND tb_flven = 'S'"
        Case DayOfWeek.Saturday : strSQL &= " AND tb_flsab = 'S'"
        Case DayOfWeek.Sunday : strSQL &= " AND tb_fldom = 'S'"
      End Select

      dttPromo = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function TrovaNumeroUsiPromo(ByRef dtrRow As DataRow, ByRef dsPromoOmaggi As DataSet, ByVal lConto As Integer) As Boolean
    Dim strSQL As String = ""
    Dim strWhere As String
    Dim dttTmp As DataTable
    Dim strFilter As String
    Try
      strWhere = " WHERE " & NTSCInt(dtrRow!tb_codrepr) & " IN (mm_codrepr1, mm_codrepr2, mm_codrepr3, mm_codrepr4, mm_codrepr5, mm_codrepr6)"
      If lConto <> 0 Then strWhere &= " AND tm_conto = " & lConto

      strFilter = " SELECT codditt, tm_anno, tm_tipork, tm_serie, tm_numdoc FROM testmagc " & _
                  " WHERE codditt = " & CStrSQL(dtrRow!codditt) & _
                  " AND tm_retail = 'S' AND tm_tipork IN ('A', 'B', 'C', 'Z', 'N', 'F', 'S') " & _
                  " AND tm_datpromo BETWEEN " & CDataSQL(NTSCDate(dtrRow!tb_datini)) & " AND " & CDataSQL(NTSCDate(dtrRow!tb_datfin))

      Select Case NTSCStr(dtrRow!tb_tipopr)
        Case "V" ' Punti a valore
          strSQL = "SELECT SUM(DOC.QTADOC) AS QUANT FROM " & _
                   "          (SELECT FLOOR(SUM(mm_valore) / " & CDblSQL(NTSCDec(dtrRow!tb_valric)) & ") AS QTADOC FROM movmag " & _
                   "           INNER JOIN (" & strFilter & ") AS testmagc ON movmag.codditt = testmagc.codditt AND mm_tipork = testmagc.tm_tipork " & _
                   "           AND mm_anno = testmagc.tm_anno AND mm_serie = testmagc.tm_serie AND mm_numdoc = testmagc.tm_numdoc " & _
                   "           INNER JOIN testmag ON movmag.codditt = testmagc.codditt AND mm_tipork = testmag.tm_tipork " & _
                   "           AND mm_anno = testmag.tm_anno AND mm_serie = testmag.tm_serie AND mm_numdoc = testmag.tm_numdoc " & _
                               strWhere & " GROUP BY testmag.tm_anno, testmag.tm_tipork, testmag.tm_serie, testmag.tm_numdoc) AS DOC"
        Case "Q" ' Punti a quantità
          strSQL = "SELECT SUM(DOC.QTADOC) AS QUANT FROM " & _
                   "          (SELECT SUM(FLOOR(mm_quant / " & CDblSQL(NTSCDec(dtrRow!tb_qtaric)) & ") AS QTADOC FROM movmag " & _
                   "           INNER JOIN (" & strFilter & ") AS testmagc ON movmag.codditt = testmagc.codditt AND mm_tipork = testmagc.tm_tipork " & _
                   "           AND mm_anno = testmagc.tm_anno AND mm_serie = testmagc.tm_serie AND mm_numdoc = testmagc.tm_numdoc " & _
                   "           INNER JOIN testmag ON movmag.codditt = testmagc.codditt AND mm_tipork = testmag.tm_tipork " & _
                   "           AND mm_anno = testmag.tm_anno AND mm_serie = testmag.tm_serie AND mm_numdoc = testmag.tm_numdoc " & _
                               strWhere & " GROUP BY testmag.tm_anno, testmag.tm_tipork, testmag.tm_serie, testmag.tm_numdoc) AS DOC"
        Case "P" ' Sconto di riga a %
          strSQL = "SELECT SUM(mm_quant) AS QUANT FROM movmag " & _
                   "           INNER JOIN (" & strFilter & ") AS testmagc ON movmag.codditt = testmagc.codditt AND mm_tipork = testmagc.tm_tipork " & _
                   "           AND mm_anno = testmagc.tm_anno AND mm_serie = testmagc.tm_serie AND mm_numdoc = testmagc.tm_numdoc " & _
                   "           INNER JOIN testmag ON movmag.codditt = testmagc.codditt AND mm_tipork = testmag.tm_tipork " & _
                   "           AND mm_anno = testmag.tm_anno AND mm_serie = testmag.tm_serie AND mm_numdoc = testmag.tm_numdoc " & _
                   strWhere
        Case "L" ' Listino di riga
          strSQL = "SELECT SUM(mm_quant) AS QUANT FROM movmag " & _
                   "           INNER JOIN (" & strFilter & ") AS testmagc ON movmag.codditt = testmagc.codditt AND mm_tipork = testmagc.tm_tipork " & _
                   "           AND mm_anno = testmagc.tm_anno AND mm_serie = testmagc.tm_serie AND mm_numdoc = testmagc.tm_numdoc " & _
                   "           INNER JOIN testmag ON movmag.codditt = testmagc.codditt AND mm_tipork = testmag.tm_tipork " & _
                   "           AND mm_anno = testmag.tm_anno AND mm_serie = testmag.tm_serie AND mm_numdoc = testmag.tm_numdoc " & _
                   strWhere
        Case "T" ' Sconto di piede a %
          strSQL = "SELECT COUNT(*) AS QUANT FROM (" & _
                   "  SELECT mm_tipork FROM movmag " & _
                   "           INNER JOIN (" & strFilter & ") AS testmagc ON movmag.codditt = testmagc.codditt AND mm_tipork = testmagc.tm_tipork " & _
                   "           AND mm_anno = testmagc.tm_anno AND mm_serie = testmagc.tm_serie AND mm_numdoc = testmagc.tm_numdoc " & _
                   "           INNER JOIN testmag ON movmag.codditt = testmagc.codditt AND mm_tipork = testmag.tm_tipork " & _
                   "           AND mm_anno = testmag.tm_anno AND mm_serie = testmag.tm_serie AND mm_numdoc = testmag.tm_numdoc " & _
                   strWhere & _
                   " GROUP BY mm_tipork, mm_anno, mm_serie, mm_numdoc) AS TMP"
        Case "U" ' Sconto di piede a valore
          strSQL = "SELECT COUNT(*) AS QUANT FROM (" & _
                   "  SELECT mm_tipork FROM movmag " & _
                   "           INNER JOIN (" & strFilter & ") AS testmagc ON movmag.codditt = testmagc.codditt AND mm_tipork = testmagc.tm_tipork " & _
                   "           AND mm_anno = testmagc.tm_anno AND mm_serie = testmagc.tm_serie AND mm_numdoc = testmagc.tm_numdoc " & _
                   "           INNER JOIN testmag ON movmag.codditt = testmagc.codditt AND mm_tipork = testmag.tm_tipork " & _
                   "           AND mm_anno = testmag.tm_anno AND mm_serie = testmag.tm_serie AND mm_numdoc = testmag.tm_numdoc " & _
                   strWhere & _
                   " GROUP BY mm_tipork, mm_anno, mm_serie, mm_numdoc) AS TMP"
        Case "M" ' MxN Semplice
          strSQL = "SELECT SUM(mm_quant) AS QUANT FROM movmag " & _
                   "           INNER JOIN (" & strFilter & ") AS testmagc ON movmag.codditt = testmagc.codditt AND mm_tipork = testmagc.tm_tipork " & _
                   "           AND mm_anno = testmagc.tm_anno AND mm_serie = testmagc.tm_serie AND mm_numdoc = testmagc.tm_numdoc " & _
                   "           INNER JOIN testmag ON movmag.codditt = testmagc.codditt AND mm_tipork = testmag.tm_tipork " & _
                   "           AND mm_anno = testmag.tm_anno AND mm_serie = testmag.tm_serie AND mm_numdoc = testmag.tm_numdoc " & _
                   strWhere & " AND mm_stasino <> 'S'"
        Case "N" ' MxN Misto
          strSQL = "SELECT SUM(mm_quant) AS QUANT FROM movmag " & _
                   "           INNER JOIN (" & strFilter & ") AS testmagc ON movmag.codditt = testmagc.codditt AND mm_tipork = testmagc.tm_tipork " & _
                   "           AND mm_anno = testmagc.tm_anno AND mm_serie = testmagc.tm_serie AND mm_numdoc = testmagc.tm_numdoc " & _
                   "           INNER JOIN testmag ON movmag.codditt = testmagc.codditt AND mm_tipork = testmag.tm_tipork " & _
                   "           AND mm_anno = testmag.tm_anno AND mm_serie = testmag.tm_serie AND mm_numdoc = testmag.tm_numdoc " & _
                   strWhere & " AND mm_stasino <> 'S'"
        Case "F" ' Omaggio a valore
          strSQL = "SELECT mm_codart, SUM(mm_quant) AS QUANT FROM movmag " & _
                   "           INNER JOIN (" & strFilter & ") AS testmagc ON movmag.codditt = testmagc.codditt AND mm_tipork = testmagc.tm_tipork " & _
                   "           AND mm_anno = testmagc.tm_anno AND mm_serie = testmagc.tm_serie AND mm_numdoc = testmagc.tm_numdoc " & _
                   "           INNER JOIN testmag ON movmag.codditt = testmagc.codditt AND mm_tipork = testmag.tm_tipork " & _
                   "           AND mm_anno = testmag.tm_anno AND mm_serie = testmag.tm_serie AND mm_numdoc = testmag.tm_numdoc " & _
                   strWhere & " AND mm_stasino <> 'S' " & _
                   "GROUP BY mm_codart"
        Case "A" ' Omaggio se acquistati certi articoli
          strSQL = "SELECT mm_codart, SUM(mm_quant) AS QUANT FROM movmag " & _
                   "           INNER JOIN (" & strFilter & ") AS testmagc ON movmag.codditt = testmagc.codditt AND mm_tipork = testmagc.tm_tipork " & _
                   "           AND mm_anno = testmagc.tm_anno AND mm_serie = testmagc.tm_serie AND mm_numdoc = testmagc.tm_numdoc " & _
                   "           INNER JOIN testmag ON movmag.codditt = testmagc.codditt AND mm_tipork = testmag.tm_tipork " & _
                   "           AND mm_anno = testmag.tm_anno AND mm_serie = testmag.tm_serie AND mm_numdoc = testmag.tm_numdoc " & _
                   strWhere & " AND mm_stasino <> 'S' " & _
                   "GROUP BY mm_codart"
      End Select

      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      'Se la promozioni è a omaggi, il numero di volte che è stata applicata 
      'corrisponde agli articoli da distribuire x il numero massimo di volte da distribuire.
      'Quindi serve portarsi dietro un DataSet per ogni promozione, in ogni dataset c'è il risultato della query di selezione,
      'Ovvero ARTICOLO - QUANTITA DISTRIBUITA
      If NTSCStr(dtrRow!tb_tipopr) = "F" Or NTSCStr(dtrRow!tb_tipopr) = "A" Then
        'nel dataset le tabelle le chiama: "C" per i clienti, "P" per tutte, seguito dal numero di promozione
        If lConto = 0 Then
          dttTmp.TableName = "P" & NTSCInt(dtrRow!tb_codrepr)
        Else
          dttTmp.TableName = "C" & NTSCInt(dtrRow!tb_codrepr)
        End If
        dsPromoOmaggi.Tables.Add(dttTmp.Copy())
      Else
        If dttTmp.Rows.Count > 0 Then
          If lConto = 0 Then
            dtrRow!xx_npromo = dttTmp.Rows(0)!QUANT
          Else
            dtrRow!xx_nprclie = dttTmp.Rows(0)!QUANT
          End If
        End If
      End If

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaDatiOmaggioDaLista(ByVal strDitta As String, ByVal lCodlsar As Integer, ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT 'S' AS xx_sel, ar_codart, ar_descr, ar_unmis, lsa_esist AS xx_quant, lsa_esist AS xx_maxquant, 0 AS xx_codrepr, ' ' AS xx_desrepr, ' ' AS xx_tipo " & _
               " FROM artico INNER JOIN listsar ON artico.codditt = listsar.codditt AND ar_codart = lsa_codart " & _
               " WHERE artico.codditt = " & CStrSQL(strDitta) & _
               " AND lsa_codlsar = " & lCodlsar

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function CaricaDatiOmaggioDaArticolo(ByVal strDitta As String, ByVal strCodart As String, ByVal bSelect As Boolean, _
                                                        ByRef dttOut As DataTable) As Boolean
    Dim strSQL As String = ""
    Try
      strSQL = "SELECT '" & NTSCStr(IIf(bSelect, "S", "N")) & "' AS xx_sel, ar_codart, ar_descr, ar_unmis, " & _
               " 0 AS xx_quant, 0 AS xx_maxquant, 0 AS xx_codrepr, ' ' AS xx_desrepr, ' ' AS xx_tipo " & _
               " FROM artico " & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND ar_codart = " & CStrSQL(strCodart)

      dttOut = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI)

      Return False
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Function
#End Region

#Region "Dichiarazioni di intento"
  Public Overridable Function GestisciFlagChiusura(ByVal strState As String, ByVal strDitta As String, ByVal strTipo As String, ByVal nAnno As Integer, ByVal nNumero As Integer, ByRef dbConn As DbConnection) As Boolean
    Dim strSQL As String = ""
    Dim dttTmp As New DataTable
    Dim lResult As Integer
    Try

      '--------------------------------------------------------------------------------------------------------------
      strSQL = " SELECT * FROM dichint" & _
               " WHERE codditt = " & CStrSQL(strDitta) & _
               " AND di_tipo = " & CStrSQL(strTipo) & _
               " AND di_anno = " & nAnno & _
               " AND di_numero = " & nNumero & _
               " AND di_tipodich = '1'"
      dttTmp = OpenRecordset(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
      '--------------------------------------------------------------------------------------------------------------
      If dttTmp.Rows.Count = 0 Then
        Return True
      End If

      If strState = "C" Then
        'la riapro...
        strSQL = " UPDATE dichint SET di_chiusa = 'N'" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND di_tipo = " & CStrSQL(strTipo) & _
                 " AND di_anno = " & nAnno & _
                 " AND di_numero = " & nNumero
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = 0 Then Return False
      Else
        'la chiudo
        strSQL = " UPDATE dichint SET di_chiusa = 'S'" & _
                 " WHERE codditt = " & CStrSQL(strDitta) & _
                 " AND di_tipo = " & CStrSQL(strTipo) & _
                 " AND di_anno = " & nAnno & _
                 " AND di_numero = " & nNumero
        lResult = Execute(strSQL, CLE__APP.DBTIPO.DBAZI, dbConn)
        If lResult = 0 Then Return False
      End If

      Return True

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      Return False
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function
#End Region

#Region "Attivazione moduli Friendly"
  Public Overridable Sub AttivaModuliFriendly()
    Dim strSQL As String = ""
    Try
      strSQL = "UPDATE anazmod SET am_abilit = 'S'" & _
               " WHERE (am_modulo = 39 AND am_abilit = 'N')" & _
               "    OR (am_modulo = 45 AND am_abilit = 'N')"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)

      strSQL = "UPDATE tabinsg SET tb_mod3_9 = 'S', tb_mod3_15 = 'S' " & _
               " WHERE tb_mod3_9 = 'N' OR tb_mod3_15 = 'N'"

      Execute(strSQL, CLE__APP.DBTIPO.DBAZI)
    Catch ex As Exception
      '--------------------------------------------------------------
      Throw (New NTSException(GestError(ex, Me, strSQL, oApp.InfoError, "", False)))
      '--------------------------------------------------------------
    End Try
  End Sub
#End Region
End Class