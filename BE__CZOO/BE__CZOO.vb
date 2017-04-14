Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Windows.Forms

'ENTITY UTILIZZATO PER PASSARE INFORMAZIONI TRA CHILD

Public Class CLE__CZOO
  Public oMenu As CLE__MENU
  Public strIn As String = ""

  Public CustomClass As Boolean = False             'se TRUE la classe istanziata non è quella standard NTS ma una ereditata


  Public Overridable Function Init(ByRef Menu As CLE__MENU, ByRef oScript As INT__SCRIPT) As Boolean
    oMenu = Menu
    Return True
  End Function

  Public Overridable Function GetNomeProgForGest(ByVal strPrgZoom As String) As String
    Dim strTmp As String = ""
    Dim strT() As String
    Dim i As Integer = 0
    Dim dttTmp As New DataTable
    Try
      If oMenu.oApp.bGPVDisconnesso Then Return "" 'In modalità disconnessa non devo MAI andare nei programmi di gestione

      'dato il nome dello zoom restituisce il programma per andare in gestione della tabella zoomata
      'prima del ; c'è il programma da usare per lo zoom, dopo il ; il programma per la gestione
      strTmp = oMenu.GetSettingBus("OPZIONI", ".", ".", strPrgZoom, "", " ", "")
      If strTmp <> "" Then
        strT = strTmp.Split(CType(";", Char))
        If strT.Length > 1 Then Return strT(1)
      End If

      Select Case strPrgZoom
        'zoom COM
        Case "ZOOMTABCAUM" : Return "CLSMGCAUM"
        Case "ZOOMTABASPE" : Return "CLSVEASPE"
        Case "ZOOMABI" : Return "CLS__ABIA"
        Case "ZOOMABICAB" : Return "CLS__ABIC"
        Case "ZOOMTABSTAG" : Return "CLSTCSTAG"
        Case "ZOOMANAGRA" : Return "CLS--CLIE"
        Case "ZOOMANAGRAC" : Return "CLS--CLIE"
        Case "ZOOMANAGRAF" : Return "CLS--CLIE"
        Case "ZOOMCOMMESS" : Return "CLSCICOMM"
        Case "ZOOMSUBCOMM" : Return "CLSCICOMM"
        Case "ZOOMANAGCA" : Return "CLSCISCCA"
        Case "ZOOMANAGCA2" : Return "BNCXSCCA"
        Case "ZOOMTABCFAM" : Return "CLSMGCFAM"
        Case "ZOOMTABCENA" : Return "CLSCICENA"
        Case "ZOOMTABCAGE" : Return "CLSPRCAGE"
        Case "ZOOMTABCAUS" : Return "CLSPECAUS"
        Case "ZOOMTABTRIB" : Return "CLSCFTRIB"
        Case "ZOOMTABSTAT" : Return "CLS__STAT"
        Case "ZOOMTABCATE" : Return "CLS__CATE"
        Case "ZOOMTABCANA" : Return "CLS__CANA"
        Case "ZOOMTABLIST" : Return "CLSMGLIST"
        Case "ZOOMTABPORT" : Return "CLSVEPORT"
        Case "ZOOMTABTPBF" : Return "CLSVETPBF"
        Case "ZOOMTABCSCL" : Return "CLS__CSCL"
        Case "ZOOMTABCPCL" : Return "CLS__CPCL"
        Case "ZOOMTABRTAC" : Return "CLSPERTAC"
        Case "ZOOMOPPORTUN" : Return "BNCROPPO"
        Case "ZOOMLEADS" : Return "BNCRLEAD"
        Case "ZOOMCRACT" : Return "BNCRCRAC"
        Case "ZOOMTABRUAZ" : Return "CLSPMRUAZ"
        Case "ZOOMTABCONT" : Return "CLSPMCONT"
        Case "ZOOMTABCOPE" : Return "CLSCICOPE"
        Case "ZOOMBUDGETCADACONTO" : Return "CLSCIBUDG"
        Case "ZOOMTABCPTR" : Return "CLSCRCPTR"
        Case "ZOOMTABPERV" : Return "CLSCRPERV"
        Case "ZOOMTABVALU" : Return "CLSVEVALU"
        Case "ZOOMTABVETT" : Return "CLSVEVETT"
        Case "ZOOMTABBANC" : Return "CLSVEBANC"
        Case "ZOOMTABMAGA" : Return "CLSMGMAGA"
        Case "ZOOMTABPAGA" : Return "CLS__PAGA"
        Case "ZOOMTABZONE" : Return "CLS__ZONE"
        Case "ZOOMTABLING" : Return "CLS__LING"
        Case "ZOOMTABLINGP" : Return "CLS__LIOP"
        Case "ZOOMTABPCON" : Return "CLS__PCON"
        Case "ZOOMTABCIVA" : Return "CLS__CIVA"
        Case "ZOOMTABCOVE" : Return "CLSVECOVE"
        Case "ZOOMTABCOVP" : Return "CLSVECOVE" '"CLSVECOVP"
        Case "ZOOMANAGEN" : Return "CLS__ANAG"
        Case "ZOOMCOMUNI" : Return "CLS__GCAP"
        Case "ZOOMTABCAUC" : Return "CLSCGCAUC"
        Case "ZOOMTABUSAT" : Return "CLSCGUSAT"
        Case "ZOOMTABTRIC" : Return "CLS__TRIC"
        Case "ZOOMTABATTI" : Return "CLS__ANAZ"
        Case "ZOOMTABESCO" : Return "CLS__ANAZ"
        Case "ZOOMTABBILM" : Return "CLSCGMDBI"
        Case "ZOOMANAGPC" : Return "CLS__SOTC" '"CLS__ANPC"
        Case "ZOOMTABCOVG" : Return "CLSVECOVE" '"CLSVECOVG"
        Case "ZOOMTABDIRA" : Return "CLSCFDIRA"
        Case "ZOOMTABSMEL" : Return "CLSSMSMEL"
        Case "ZOOMANAGRAS" ' Se c'è una sola ditta avvia Sottoconti PDC, altrimenti Sottoconti Ditta
          'If oMenu.GetNumeroDitte() = 1 And _
          '   CBool(oMenu.GetSettingBus("BS--SOTC", "OPZIONI", ".", "AllowNew", "0", " ", "0")) = False Then
          '  Return "CLS__ANPC"
          'Else
          Return "CLS__SOTC"
          'End If
        Case "ZOOMTABRSTA" : Return "CLSICRSTA"
        Case "ZOOMTABMPOR" : Return "CLSICMPOR"
        Case "ZOOMTABNTRA" : Return "CLSICNTRA"
        Case "ZOOMTARIC" : Return "CLSICNOMC"
        Case "ZOOMANACESP" : Return "CLSCEANCE"
        Case "ZOOMARTICO" : Return "CLSMGARTI"
        Case "ZOOMARTICOV" : Return "CLSMGARTV"
        Case "ZOOMARTICOTC" : Return "CLSTCARTV"
        Case "ZOOMTABQPRO" : Return "CLSCSQPRO"
        Case "ZOOMD7PERCIP" : Return "CLSPEANPE"
        Case "ZOOMANALOTTI" : Return "CLSMGANLO"
        Case "ZOOMARTEST" : Return "CLSMGCTFO"
        Case "ZOOMARTRIGHEOFF" : Return "CLSCRGSOF"
        Case "ZOOMTABTPRO" : Return "CLSMGPROM"
        Case "ZOOMTABCPAR" : Return "CLS__CPAR"
        Case "ZOOMNOTE" : Return "CLS__NOTE"
        Case "ZOOMTIPIALERT" : Return "CLS__GALS"
        Case "ZOOMTABPROC" : Return "CLSIEPROC"
        Case "ZOOMVALVARI" : Return "CLSMGTVAR"
        Case "ZOOMVALVARIESPL" : Return "CLSMGARTV"
        Case "ZOOMVALVARIESPLTC" : Return "CLSTCARTV"
        Case "ZOOMPROCEDCONFIG" : Return "CLSCPPROC"
        Case "ZOOMTABVARI" : Return "CLSMGTVAR"
        Case "ZOOMTABREPA" : Return "CLSDBREPA"

          'zoom NET
        Case "ZOOMTABGRUC" : Return "BNCGPCON"
        Case "ZOOMTABCLAS" : Return "BNCGPCON"
        Case "ZOOMTABMAST" : Return "BNCGPCON"
        Case "ZOOMCONTIRECENTI" : Return "BN__SOTC"
        Case "ZOOMORDLIST" : Return "BNRAHLOL"

        Case "ZOOMDICHINT" : Return "BNDIGEDI"

        Case "ZOOMDESTDIVA" : Return "BN__ANAZ"
        Case "ZOOMDESTDIV" : Return "BN__CLIE"
        Case "ZOOMDESTDIVC" : Return "BN__CLIE"
        Case "ZOOMDESTDIVF" : Return "BN__CLIE"
        Case "ZOOMCHIAMATE" : Return "BNCSGCHI"
        Case "ZOOMPROFIL" : Return "CLSIEPROF"
        Case "ZOOMPROCED" : Return "CLSIEPROC"
        Case "ZOOMTRCFLD" : Return "CLSIETRAS"
        Case "ZOOMTABPCCA" : Return "BNCXPCCA"
        Case "ZOOMTABCHIA" : Return "CLSCPCHIA"
        Case "ZOOMCHIPERS" : Return "CLSCPCHIA"
        Case "ZOOMDWCRUS" : Return "BNDWQVDA"
        Case "ZOOMTABDICV" : Return "BNCXDICV"
        Case "ZOOMCAMPIDIMENSIONI" : Return "BNDWDIME"
        Case "ZOOMTABCOCE" : Return "BNCECOCE"
        Case "ZOOMTABRERE" : Return "BNREREPA"
        Case "ZOOMTABREEC" : Return "BNRERECA"
        Case "ZOOMTABREPR" : Return "BNREPROM"
        Case "ZOOMRETAILARTICO" : Return "BNMGARTI"
        Case "ZOOMRETAILANAGRA" : Return "BNRECLIE"
        Case "ZOOMRUOLI" : Return "BN__GROL"
        Case "ZOOMARTCLAS" : Return "BNMGCLAS"
        Case "ZOOMCALOP" : Return "BNDBCALA"
        Case "ZOOMARTFASI" : Return "BNMGARTIF"
        Case "ZOOMDISTBAS" : Return "BNDBDIBA"
        Case "ZOOMTABRETT" : Return "BNRETIPT"
        Case "ZOOMTABUMIS" : Return "BNMGUMIS"
        Case "ZOOMORGANIG" : Return "BN__ORGA"
        Case Else
          '---------------------------------
          'vale solo per programmi tipo ZOOMTABxxxx: cerco se c'è una dll che finisca per xxxx: se c'è la istanzio
          'prediligo il programma in VB6, visto che in be__menu c'è una routine che cerca un programma .net 
          'a partire dal programma vb6 e se lo trova lo avvia al posto di quello vecchio
          If strPrgZoom.Length = 11 Then
            If strPrgZoom.ToUpper.Substring(0, 7) = "ZOOMTAB" Then
              strT = System.IO.Directory.GetFiles(oMenu.App.NetDir, "B*" & strPrgZoom.ToUpper.Substring(7) & ".DLL")
              For i = 0 To strT.Length - 1
                If strT(i).ToString.ToUpper.Substring(strT(i).ToString.Length - 12, 2) = "BN" Then
                  strTmp = strT(i).ToString.ToUpper.Substring(strT(i).ToString.Length - 12, 8).Replace("-", "_")
                End If
                If strT(i).ToString.ToUpper.Substring(strT(i).ToString.Length - 12, 2) = "BS" Then
                  Return "CLS" & strT(i).ToString.ToUpper.Substring(strT(i).ToString.Length - 10, 6).Replace("-", "_")
                End If
              Next
              If strTmp <> "" Then Return strTmp
            End If
          End If
      End Select

      '---------------------------------
      'non è stato settato il programma per entrare in gestione child
      Return ""

    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", "", "", False))
      Return ""
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Sub OpenChildGest(ByRef oNTSTextBox As Control, ByVal strPrgZoom As String, ByVal strDitta As String, _
                                       ByVal bNew As Boolean, Optional ByRef oPar As CLE__CLDP = Nothing)
    Dim strServer As String = ""
    Dim strNomeDll As String = ""
    Dim strPrgGest As String = ""
    Dim oParam As CLE__CLDP = Nothing
    Dim strParam As String = ""

    Try
      If oPar Is Nothing Then
        oParam = New CLE__CLDP
      Else
        oParam = oPar
      End If

      '--------------------------
      'esco se non è configurato il programma per la gestione 
      strPrgGest = GetNomeProgForGest(strPrgZoom)
      If strPrgGest = "" Then
        'non do il messaggio: Su SBC è fuorviante perchè si attiva la openchildgest anche solo tenendo premuto a lungo il button
        'Throw New NTSException("CLE__CZOO.OpenChildGest: *Non è stato associato nessun programma per la gestione del campo su cui si è posizionati (programma: " + strPrgZoom + ")*" & vbCrLf)
        Return
      End If

      '--------------------------
      If oParam.strParam = "" Then
        Select Case strPrgZoom
          Case "ZOOMANAGRAC", "ZOOMANAGRAF", "ZOOMANAGRAS"
            If bNew Then
              strParam = "NUOV;" & Microsoft.VisualBasic.Right(strPrgZoom, 1) & ";" & "0"
            Else
              If oMenu.GetNumeroDitte = 1 And Microsoft.VisualBasic.Right(strPrgZoom, 1) = "S" Then
                strParam = "APRI;" & oNTSTextBox.Text
              Else
                strParam = "APRI;" & Microsoft.VisualBasic.Right(strPrgZoom, 1) & ";" & oNTSTextBox.Text
                If Not oNTSTextBox.Tag Is Nothing Then
                  strParam += ";" & Right("".PadLeft(9, "0"c) & NTSCStr(oNTSTextBox.Tag), 9)
                End If
              End If
            End If
          Case "ZOOMANAGRA"
            'non so se il codice passato è un cliente/fornitore o sottoconto ...
            'per default imposto cliente
            If bNew Then
              strParam = "NUOV;C;" & "0"
            Else
              strParam = "APRI;C;" & oNTSTextBox.Text
            End If
          Case "ZOOMARTICO"
            If bNew Then
              strParam = "NUOV;0§" & oNTSTextBox.Text 'Caso particolare, per distiungerlo da altre chiamate, vedi metodo Apri dell'anagrafica articoli.
            Else
              strParam = "APRI;" & oNTSTextBox.Text
            End If
          Case Else
            If bNew Then
              strParam = "NUOV;" & "0"
            Else
              strParam = "APRI;" & oNTSTextBox.Text
            End If
        End Select
      Else
        strParam = oPar.strParam
      End If

      '--------------------------
      'verifico se programma child è COM o NET    (so stesso codice è presente solo in BN__HLTB)
      If strPrgGest.Substring(0, 3).ToUpper <> "CLS" Then
        'QUI PASSA SOLO PER I Child NET MAI SCTRITTI IN vb6
        strNomeDll = strPrgGest
        strPrgGest = "FRM" & strPrgGest.Substring(2)
        strServer = "NTSInformatica"
        oParam.Ditta = strDitta

        oMenu.RunChild(strServer, strPrgGest, "", strDitta, "", strNomeDll, Nothing, strParam, True, True)
      Else
        'Child COM
        strServer = strPrgGest.Replace("CLS", "BS")
        oMenu.RunChild(strServer, strPrgGest, "", strDitta, "", "", Nothing, strParam, True, True)
      End If
      If Not oPar Is Nothing Then oPar.strParam = strParam
    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", "", "", False))
    End Try
  End Sub

  ''' <summary>
  ''' Zoom che sfrutta la variabile NTSZOOM.strIn per accettare il valore in input e per restituirlo modificato
  ''' </summary>
  ''' <param name="strPrgZoom"></param>
  ''' <param name="strDitta"></param>
  ''' <param name="oFiltriTb"></param>
  ''' <remarks></remarks>
  Public Overridable Sub ZoomStrIn(ByVal strPrgZoom As String, ByVal strDitta As String, ByRef oFiltriTb As CLE__PATB)
    Dim edTxtIn As New DevExpress.XtraEditors.ButtonEdit
    Try
      'chiama la Zoom: è un modo per poter passare meno parametri ed ottenere lo stesso risultato in .NET
      edTxtIn.Text = strIn
      If oFiltriTb Is Nothing Then
        'NON POSSO FARLO!!!!!!
        'Zoom(strPrgZoom, edTxtIn, strDitta, "", New CLE__PATB)
        Throw New NTSException("NTSZOOM.ZoomStrIn chiamato senza passaggio di parametri tramite classe CLE_PATB")
      Else
        If oFiltriTb.CANCELZOOM Then
          'dal child sono stato avvisato di non visualizzare lo zoom (ad esempio perchè l'F5 è stato fatto su una cella di griglia readonly
          edTxtIn.Text = strIn
        Else
          'visualizzo lo zoom
          Zoom(strPrgZoom, edTxtIn, strDitta, "", oFiltriTb)
        End If
      End If
      strIn = edTxtIn.Text
    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", "", "", False))
    End Try
  End Sub

  'classe per la scelta di quale zoom .NET lanciare
  Public Overridable Sub Zoom(ByVal strPrgZoom As String, ByRef edCtrl As DevExpress.XtraEditors.ButtonEdit, _
                              ByVal strDitta As String, ByVal strFiltriVb6 As String, ByVal oFiltriTb As CLE__PATB)
    'funzione standard per lanciare tutti gli zoom: in base al tipo di zoom lancio il programma specifico
    'per gli zoom fatti in .NET chiamo il child esterno specializzato per la gestione degli zoom
    Dim strOut As String = ""
    Dim strTmp As String = ""
    Dim strT() As String

    Dim strFile As String = ""
    Dim strPart() As String
    Dim strForm As String = ""

    Try
      'dato il nome dello zoom restituisce il programma da usare per visualizzare i dati
      'prima del ; c'è il programma da usare per lo zoom, dopo il ; il programma per la gestione
      strTmp = oMenu.GetSettingBus("OPZIONI", ".", ".", strPrgZoom, "", " ", "")
      If strTmp <> "" Then
        strT = strTmp.Split(CType(";", Char))
        If strT.Length > 0 Then
          strPart = strT(0).Split("."c)
          If strPart.length = 1 Then
            strFile = strT(0)
            strForm = "FRM" & strFile.Substring(2)
          Else
            strFile = strpart(0)
            strForm = strPart(1)
          End If
        End If

      End If

      If strPrgZoom.Length < 7 Then
        Throw New NTSException("CLE__CZOO.Zoom: strPrgZoom deve esere un nome più lungo di 7 caratteri (" & strPrgZoom & ")")
        Return
      End If
      If oFiltriTb Is Nothing Then oFiltriTb = New CLE__PATB
      Select Case strPrgZoom.Substring(0, 7).ToUpper
        Case "ZOOMTAB"
          If strPrgZoom.ToUpper = "ZOOMTABDICV" Then
            '-------------------------------------------------------
            If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
            If strFile = "" Then
              strFile = "BNCXHLDV"
              strForm = "FRMCXHLDV"
            End If
          Else
            '-------------------------------------------------------
            'ZOOM GENERICO BN__HLTB
            If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
            If strFile = "" Then
              strFile = "BN__HLTB"
              strForm = "FRM__HLTB"
            End If
          End If
        Case Else
          '------------------------
          Select Case strPrgZoom.ToUpper
            Case "ZOOMARTICO"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              'oFiltriTb.bVisGriglia
              'oFiltriTb.nMagaz
              'oFiltriTb.nListino
              'oFiltriTb.lContoCF
              If oFiltriTb.nTipologia <= 0 Then oFiltriTb.nTipologia = 1
              If oFiltriTb.strTipoArticolo = "" Then oFiltriTb.strTipoArticolo = "N" ' nè succedanei nè accessori
              'oFiltriTb.strCodartAcc
              'oFiltriTb.bLiv2 = true solo se opzione visulizza_2_liveli = S per impostare secondo livello
              If strFile = "" Then
                strFile = "BNMGHLAR"
                strForm = "FRMMGHLAR"
              End If
            Case "ZOOMHLCE"
              '-------------------------------------------------------
              'ZOOM file .XLS
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLCE"
                strForm = "FRM__HLCE"
              End If
            Case "ZOOMABI"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMABICAB"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLAB"
                strForm = "FRM__HLAB"
              End If
            Case "ZOOMANAGPC"
              '-------------------------------------------------------
              'ZOOM SOTTOCONTI PDC
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMANAGEN"
              '-------------------------------------------------------
              'ZOOM ANAGRAFICHE GENERALI
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLAG"
                strForm = "FRM__HLAG"
              End If
            Case "ZOOMANAGRA", "ZOOMANAGRAC", "ZOOMANAGRAF", "ZOOMANAGRAS"
              '-------------------------------------------------------
              'ZOOM CLIENTI/FORNITORI/SOTTOCONTI
              Select Case Right(strPrgZoom.ToUpper, 1)
                Case "C" : oFiltriTb.strTipo = "C"
                Case "F" : oFiltriTb.strTipo = "F"
                Case "S" : oFiltriTb.strTipo = "S"
              End Select
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLAN"
                strForm = "FRM__HLAN"
              End If
            Case "ZOOMCONTIRECENTI"
              '-------------------------------------------------------
              'ZOOM SOTTOCONTI RECENTI
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLSR"
                strForm = "FRM__HLSR"
              End If
            Case "ZOOMANAGCA"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCIHLAC"
                strForm = "FRMCIHLAC"
              End If
            Case "ZOOMANAGCA2"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCXHLAC"
                strForm = "FRMCXHLAC"
              End If
            Case "ZOOMANALINK"
              If oFiltriTb Is Nothing Then
                Throw New NTSException("Zoom sottoconti CA specifici non avviabile in quanto chiamato senza passaggio del conto di CG")
                Return
              Else
                oFiltriTb.strIn = edCtrl.Text
              End If
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMFXDI"
              '-------------------------------------------------------
              'seleziona ditte
              If strFile = "" Then
                strFile = "BN__FXDI"
                strForm = "FRM__FXDI"
              End If
            Case "ZOOMTARIC"
              '-------------------------------------------------------
              'seleziona ditte

              If strFile = "" Then
                strFile = "BNICHLNC"
                strForm = "FRMICHLNC"
              End If
            Case "ZOOMCOMUNI"
              '-------------------------------------------------------
              'seleziona ditte
              If strFile = "" Then
                strFile = "BN__HLCA"
                strForm = "FRM__HLCA"
              End If
            Case "ZOOMDESTDIV"
              '-------------------------------------------------------
              'ZOOM DESTINAZIONI DIVERSE
              If oFiltriTb Is Nothing Then
                Throw New NTSException("Zoom destinazioni diverse non avviabile in quanto chiamato senza passaggio del conto Cliente/Fornitore")
                Return
              Else
                oFiltriTb.strIn = edCtrl.Text
              End If
              If strFile = "" Then
                strFile = "BN__HLDD"
                strForm = "FRM__HLDD"
              End If
            Case "ZOOMDICHINT"
              '-------------------------------------------------------
              'ZOOM DICHIARAZIONI INTENTO
              If oFiltriTb Is Nothing Then
                Throw New NTSException("Zoom dichiarazioni di intento non avviabile in quanto chiamato senza passaggio del conto Cliente/Fornitore")
                Return
              Else
                oFiltriTb.strIn = edCtrl.Text
              End If
              If strFile = "" Then
                strFile = "BNDIHLDI"
                strForm = "FRMDIHLDI"
              End If
            Case "ZOOMARTFASI"
              '-------------------------------------------------------
              'ZOOM ARTICOLI A FASI
              If oFiltriTb Is Nothing Then
                Throw New NTSException("Zoom fasi articolo non avviabile in quanto chiamato senza passaggio del codice articolo")
                Return
              Else
                oFiltriTb.strIn = edCtrl.Text
              End If
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMCOMMESS"
              '-------------------------------------------------------
              'ZOOM COMMESSE

              If strFile = "" Then
                strFile = "BNCIHLCO"
                strForm = "FRMCIHLCO"
              End If
            Case "ZOOMSUBCOMM"
              '-------------------------------------------------------
              'ZOOM SOTTOCOMMESSE
              If oFiltriTb Is Nothing Then
                Throw New NTSException("Zoom sottocommesse non avviabile in quanto chiamato senza passaggio del codice commessa")
                Return
              Else
                oFiltriTb.strIn = edCtrl.Text
              End If
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMBUDGETCADACONTO"
              '-------------------------------------------------------
              'ZOOM BUDGET utilizzato da BNCIGSPA per visualizzare solo centri/linee/commesse di un determinato sottoconto di CA
              If oFiltriTb Is Nothing Then
                Throw New NTSException("Zoom budget non avviabile in quanto chiamato senza passaggio di parametri")
                Return
              Else
                oFiltriTb.strIn = edCtrl.Text
              End If
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMCLIBANC"
              '--------------------------------------------------------
              'zoom elenco banche aggiuntive cliente
              If oFiltriTb Is Nothing Then
                Throw New NTSException("Zoom banche cliente non avviabile in quanto chiamato senza passaggio del codice cliente")
                Return
              Else
                oFiltriTb.lContoCF = NTSCInt(edCtrl.Text)
              End If
              If strFile = "" Then
                strFile = "BN__HLCB"
                strForm = "FRM__HLCB"
              End If
            Case "ZOOMPARTITARI"
              '--------------------------------------------------------
              'zoom partitari e seleziona registrazioni di pn
              If oFiltriTb Is Nothing Then
                Throw New NTSException("Zoom partitari non avviabile in quanto chiamato senza passaggio di alcun parametro")
                Return
              End If
              If strFile = "" Then
                strFile = "BNCGHLPA"
                strForm = "FRMCGHLPA"
              End If
            Case "ZOOMLEADS"
              '----------------------------------------------------------
              'zoom leads
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCRHLLE"
                strForm = "FRMCRHLLE"
              End If
            Case "ZOOMNOMPROP"
              If oFiltriTb Is Nothing Then
                Throw New NTSException("Zoom NOMPROP non avviabile in quanto chiamato senza passaggio di parametri")
                Return
              Else
                oFiltriTb.strIn = edCtrl.Text
              End If
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMVALPROP"
              If oFiltriTb Is Nothing Then
                Throw New NTSException("Zoom VALPROP non avviabile in quanto chiamato senza passaggio di parametri")
                Return
              Else
                oFiltriTb.strIn = edCtrl.Text
              End If
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMORGANIG"
              If oFiltriTb Is Nothing Then
                Throw New NTSException("Zoom organizzazione/risorse non avviabile in quanto chiamato senza passaggio di parametri")
                Return
              Else
                oFiltriTb.strIn = edCtrl.Text
              End If
              If strFile = "" Then
                strFile = "BNPMHLOG"
                strForm = "FRMPMHLOG"
              End If
            Case "ZOOMANACESP"
              '-------------------------------------------------------
              'ZOOM CESPITI
              If strFile = "" Then
                strFile = "BNCEHLCS"
                strForm = "FRMCEHLCS"
              End If
            Case "ZOOMD7PERCIP"
              '-------------------------------------------------------
              'ZOOM CESPITI
              If strFile = "" Then
                strFile = "BNPEHLPE"
                strForm = "FRMPEHLPE"
              End If
            Case "ZOOMANALOTTI"
              '-------------------------------------------------------
              'ZOOM LOTTI
              If strFile = "" Then
                strFile = "BNCIHLLO"
                strForm = "FRMCIHLLO"
              End If
            Case "ZOOMUBICAZ"
              '-------------------------------------------------------
              'ZOOM UBICAZIONI
              If strFile = "" Then
                strFile = "BNMGHLUB"
                strForm = "FRMMGHLUB"
              End If
            Case "ZOOMARTEST"
              '----------------------------------------------------------
              'zoom artest
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNMGHLAE"
                strForm = "FRMMGHLAE"
              End If
            Case "ZOOMDOCUMENTI"
              '----------------------------------------------------------
              'zoom documenti di magazzino (per selezione con F3)
              If strFile = "" Then
                strFile = "BNMGDOCU"
                strForm = "FRMVEFIDO"
              End If
            Case "ZOOMMOVORD"
              '----------------------------------------------------------
              'zoom righe d'ordine (dohlmo)
              If strFile = "" Then
                strFile = "BNORHLMO"
                strForm = "FRMORHLMO"
              End If
            Case "ZOOMMOVORDBARCODE"
              '----------------------------------------------------------
              'zoom righe d'ordine con barcode (bsvebolx chiamato solo da bsveboll)
              If strFile = "" Then
                strFile = "BNMGDOCU"
                strForm = "FRMVEHLBC"
              End If
            Case "ZOOMTESTORD"
              '----------------------------------------------------------
              'zoom righe d'ordine (dohltd)
              If strFile = "" Then
                strFile = "BNORHLTD"
                strForm = "FRMORHLTD"
              End If
            Case "ZOOMTESTMAG"
              '----------------------------------------------------------
              'zoom righe d'ordine (dohltm)
              If strFile = "" Then
                strFile = "BNMGHLTM"
                strForm = "FRMMGHLTM"
              End If
            Case "ZOOMTESTMAGRI"
              '----------------------------------------------------------
              'zoom ricevute fiscali da riemettere
              If strFile = "" Then
                strFile = "BNMGHLRI"
                strForm = "FRMMGHLRI"
              End If
            Case "ZOOMTESTOFF"
              '----------------------------------------------------------
              'zoom testata offerte
              If strFile = "" Then
                strFile = "BNCRHLOF"
                strForm = "FRMCRHLOF"
              End If
            Case "ZOOMARTRIGHEOFF"
              '----------------------------------------------------------
              'zoom articolo righe offerte CRHLMF
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCRHLMF"
                strForm = "FRMCRHLMF"
              End If
            Case "ZOOMLISTINIHLFP"
              '----------------------------------------------------------
              'zoom fornitori prezzi
              If strFile = "" Then
                strFile = "BNMGHLFP"
                strForm = "FRMMGHLFP"
              End If
            Case "ZOOMHLVL"
              '----------------------------------------------------------
              'zoom generico dei valori del campo su cui si è ... (tipo zoom stampe parametriche query)
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLVL"
                strForm = "FRM__HLVL"
              End If
            Case "ZOOMNOTE"
              '-------------------------------------------------------
              'seleziona ditte
              If strFile = "" Then
                strFile = "BN__HLNT"
                strForm = "FRM__HLNT"
              End If
            Case "ZOOMSCHEDEARTMATR"
              '----------------------------------------------------------
              'zoom stampa schede articolo per matricola
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNMGHLMT"
                strForm = "FRMMGHLMT"
              End If
            Case "ZOOMRUOLI"
              '----------------------------------------------------------
              'zoom ruoli
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMARTCLAS"
              '----------------------------------------------------------
              'zoom classificazione articoli
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMOPERAT"
              '----------------------------------------------------------
              'zoom operatori
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLOP"
                strForm = "FRM__HLOP"
              End If
            Case "ZOOMORUOLIPERAT"
              '----------------------------------------------------------
              'zoom operatori
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMAZIENDE"
              '----------------------------------------------------------
              'zoom aziende
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMTIPIALERT"
              '----------------------------------------------------------
              'zoom tipi alert
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMORDLIST"
              '----------------------------------------------------------
              'zoom tipi alert
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNRAHLOL"
                strForm = "FRMRAHLOL"
              End If
            Case "ZOOMDISTBAS"
              '----------------------------------------------------------
              'zoom tipi alert
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNDBHLDB"
                strForm = "FRMDBHLDB"
              End If
            Case "ZOOMVALVARI", "ZOOMVALVARIESPL", "ZOOMVALVARIESPLTC"
              '----------------------------------------------------------
              'zoom valori varianti
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMPROCEDCONFIG"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCPHLPD"
                strForm = "FRMCPHLPD"
              End If
            Case "ZOOMVARIABCONFIG"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCPHLVA"
                strForm = "FRMCPHLVA"
              End If
            Case "ZOOMOPPORTUN"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCRHOPP"
                strForm = "FRMCRHOPP"
              End If
            Case "ZOOMCRACT"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCRHAAT"
                strForm = "FRMCRHAAT"
              End If
            Case "ZOOMCHIAMATE"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCSHCHI"
                strForm = "FRMCSHCHI"
              End If
            Case "ZOOMTRIBUTI"
              '----------------------------------------------------------
              'zoom TABTRIB
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMFILE"
              'chiamato da busweb per visualizzare i files in una dir del server 
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__GESF"
                strForm = "FRM__GESF"
              End If
            Case "ZOOMFILTRIGLEA"
              'chiamato da busweb per visualizzare i files in una dir del server 
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCRHLFL"
                strForm = "FRMCRHLFL"
              End If
            Case "ZOOMPROFIL"
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNIEHLPR"
                strForm = "FRMIEHLPR"
              End If
            Case "ZOOMPROCED"
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNIEHLPC"
                strForm = "FRMIEHLPC"
              End If
            Case "ZOOMTRCFLD"
              oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMCAMPI"
              '-------------------------------------------------------
              'ZOOM CAMPI PER IMPORT/EXPORT
              If oFiltriTb Is Nothing Then
                Throw New NTSException("Zoom campi Import/Export non avviabile in quanto chiamato senza passaggio della tabella")
                Return
              Else
                oFiltriTb.strIn = edCtrl.Text
              End If
              If strFile = "" Then
                strFile = "BNIEHLCM"
                strForm = "FRMIEHLCM"
              End If
            Case "ZOOMESCOMP"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCXHSEA"
                strForm = "FRMCXHSEA"
              End If
            Case "ZOOMCHIPERS"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMCASELLX"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCPHLCL"
                strForm = "FRMCPHLCL"
              End If
            Case "ZOOMDWCRUS"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNDWHLCR"
                strForm = "FRMDWHLCR"
              End If
            Case "ZOOMCXHLXL"
              '-------------------------------------------------------
              'ZOOM file .XLS C.A.
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCXHLXL"
                strForm = "FRMCXHLXL"
              End If
            Case "ZOOMCAMPIDIMENSIONI"
              '-------------------------------------------------------
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNDWHLDE"
                strForm = "FRMDWHLDE"
              End If
            Case "ZOOMDATISTORICIZZATI"
              '-------------------------------------------------------
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNDWHSTO"
                strForm = "FRMDWHSTO"
              End If
            Case "ZOOMTASK"
              '-------------------------------------------------------
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                If CBool(oMenu.ModuliDittaDitt(strDitta) And CLN__STD.bsModPM) Then
                  'zoom PM
                  strFile = "BNPMHLTK"
                  strForm = "FRMPMHLTK"
                Else
                  'zoom standard su COMWBS
                  strFile = "BN__HLTB"
                  strForm = "FRM__HLTB"
                End If
              End If
            Case "ZOOMPREVENTIVI"
              '-------------------------------------------------------
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCVHLPV"
                strForm = "FRMCVHLPV"
              End If
            Case "ZOOMTESTATECONTRATTI"
              '-------------------------------------------------------
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCSHLCR"
                strForm = "FRMCSHLCR"
              End If
            Case "ZOOMMATRICOLE"
              '-------------------------------------------------------
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCSHLMA"
                strForm = "FRMCSHLMA"
              End If
            Case "ZOOMTESTATEPREPAGATI"
              '-------------------------------------------------------
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCSHLPM"
                strForm = "FRMCSHLPM"
              End If
            Case "ZOOMFILTRIGSER"
              'chiamato da busweb per visualizzare i files in una dir del server 
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCSHLFL"
                strForm = "FRMCSHLFL"
              End If
            Case "ZOOMCONTRATTIQUADRO"
              'chiamato da busweb per visualizzare i files in una dir del server 
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNPMHLQU"
                strForm = "FRMPMHLQU"
              End If
            Case "ZOOMSCHEMICOMMESSA"
              'chiamato da busweb per visualizzare i files in una dir del server 
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNPMHLSP"
                strForm = "FRMPMHLSP"
              End If
            Case "ZOOMRETAILARTICO"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNREHLAR"
                strForm = "FRMREHLAR"
              End If
            Case "ZOOMRETAILANAGRA"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNREHLAN"
                strForm = "FRMREHLAN"
              End If
            Case "ZOOMANADITASD"
              'zoom punto attività (da bncgprin)
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCGHLSD"
                strForm = "FRMCGHLSD"
              End If
            Case "ZOOMCALENDARIO"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNCSCAOP"
                strForm = "FRMCSCAOP"
              End If
            Case "ZOOMPMHLVL"
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BNPMHLVL"
                strForm = "FRMPMHLVL"
              End If
            Case "ZOOMMOVATT"
              '----------------------------------------------------------
              'zoom attività (parcellazione) richiamato da bnpaboll
              If strFile = "" Then
                strFile = "BNPAHLAV"
                strForm = "FRMPAHLAV"
              End If
            Case "ZOOMPREVENT"
              '----------------------------------------------------------
              'zoom preventivi (Project Management) richiamato da bnpmgcom
              If strFile = "" Then
                strFile = "BNPMHLPR"
                strForm = "FRMPMHLPR"
              End If
            Case "ZOOMCALOP"
              '----------------------------------------------------------
              'zoom calendari
              If strFile = "" Then
                strFile = "BNDBHLCO"
                strForm = "FRMDBHLCO"
              End If
            Case "ZOOMDISTINTE"
              '----------------------------------------------------------
              'zoom distinte di presentaz. in banca
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMCONTATTI"
              '----------------------------------------------------------
              'zoom distinte di presentaz. in banca
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLCO"
                strForm = "FRM__HLCO"
              End If
            Case "ZOOMMODRICH"
              '----------------------------------------------------------
              'zoom distinte ricambi per modello (cs)
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case "ZOOMSERIE"
              '-------------------------------------------------------
              'ZOOM SERIE
              If strFile = "" Then
                strFile = "BN__HSER"
                strForm = "FRM__HSER"
              End If
            Case "ZOOMPARSTAG"
              '----------------------------------------------------------
              'zoom stampe parametriche
              If Not edCtrl Is Nothing Then oFiltriTb.strIn = edCtrl.Text
              If strFile = "" Then
                strFile = "BN__HLTB"
                strForm = "FRM__HLTB"
              End If
            Case Else
              '--------------------------------------------------------
              'zoom com generico in VB6... di qui non passa mai visto che il rpimo elemento del select case è proprio ZOOMTAB
              If strPrgZoom.Length = 11 And strPrgZoom.Substring(0, 7) = "ZOOMTAB" Then
                oMenu.oCld.DoHltb(edCtrl, strPrgZoom.Substring(7, 4), 0)
              Else
                Throw New NTSException(oMenu.oApp.Tr(Me, 127792165965781250, "Programma '|" & strPrgZoom & "|' per lo Zoom non trovato/gestito"))
                'oMenu.oApp.MsgBoxErr(oMenu.oApp.Tr(Me, 127791412516406250, "Programma '" & strPrgZoom & "' per lo Zoom non trovato/gestito"))
              End If
              Return
          End Select
      End Select

      oMenu.RunZoomNet("NTSInformatica", strForm, "", strFile, strPrgZoom, strDitta, CObj(oFiltriTb))

      If strPrgZoom.ToUpper = "ZOOMCLIBANC" OrElse strPrgZoom.ToUpper = "ZOOMPARTITARI" Then oFiltriTb.strOut = ""

      '--------------------------------------------------------
      'ho chiamato lo zoom: ora se non ho fatto 'annulla' riporto il valore nel texstbox e lancio la validazione
      If Not edCtrl Is Nothing And oFiltriTb.strOut <> "" Then
        'cambio il dato solo se non ho fatto annulla
        edCtrl.Text = oFiltriTb.strOut
        'devo scatenare l'evento di textchange per far validare il dato e, se necessario, aggiornare un eventoale descrizione del campo collegata al controllo
        'devo aggiornare anche il dato nel datatable sottostante, diversamnte potrei avere un disallineamento, 
        'in più in questo modo scatena anche la decodifica del codice nell'eventuale labe (è l'entity che fa il tutto)
        WriteTextDB(edCtrl)
      End If

    Catch ex As Exception
      Throw New NTSException(GestError(ex, Me, "", "", "", False))
      'oMenu.oApp.MsgBoxErr(oMenu.oApp.Tr(Me, 127791412764062500, "Errore in fase di avvio zoom per programma '" & strPrgZoom & "' CLE__GESTC.Zoom:" & vbCrLf & ex.ToString()))
    End Try
  End Sub

  Public Overridable Sub WriteTextDB(ByVal edText As DevExpress.XtraEditors.ButtonEdit)
    'se sono collegato ad una tabella scrivo il dato anche nella tabella 
    If edText.DataBindings.Count > 0 Then
      Dim dcn As BindingSource = CType(edText.DataBindings(0).DataSource, BindingSource)
      Dim dtrTmp As DataRowView = CType(dcn.Current, DataRowView)
      Select Case dtrTmp.Item(edText.DataBindings(0).BindingMemberInfo.BindingField.ToString).GetType.ToString
        Case "System.Int16", "System.Int32", "System.Int64"
          dtrTmp.Item(edText.DataBindings(0).BindingMemberInfo.BindingField.ToString) = NTSCInt(edText.Text)
        Case "System.Double", "System.Decimal"
          dtrTmp.Item(edText.DataBindings(0).BindingMemberInfo.BindingField.ToString) = NTSCDec(edText.Text)
        Case "System.Data"
          dtrTmp.Item(edText.DataBindings(0).BindingMemberInfo.BindingField.ToString) = NTSCDate(edText.Text)
        Case Else
          dtrTmp.Item(edText.DataBindings(0).BindingMemberInfo.BindingField.ToString) = edText.Text
      End Select
    End If
  End Sub

End Class
