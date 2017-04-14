Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMCIANA2
  Public oCleSotc As CLE__SOTC
  Public oCallParams As CLE__CLDP
  Public dsAna2 As DataSet
  Public dcAna2 As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    '----------------------------------------------------------------------------------------------------------------
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    '----------------------------------------------------------------------------------------------------------------
    Me.GctlTipoDoc = ""
    '----------------------------------------------------------------------------------------------------------------
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      '--------------------------------------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      grvAna2.NTSSetParam(oMenu, oApp.Tr(Me, 129000663747146197, "Ripartizioni in Contabilità Analitica"))
      '--------------------------------------------------------------------------------------------------------------
      codditt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129000664114828330, "Ditta"), 12, False)
      anc2_contoca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129000663849653413, "Conto CA"), tabanagca2)
      xx_contoca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129000663886218487, "Descrizione conto CA"), 0, True)
      anc2_codcena.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129000663922314778, "Centro"), tabcena)
      xx_codcena.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129000663962161333, "Descrizione centro"), 0, True)
      anc2_codcfam.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 129000663998257624, "Linea/prodotto"), tabcfam, False)
      xx_codcfam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129000664026228343, "Descrizione linea/prodotto"), 0, True)
      anc2_coddivi.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129187376197044630, "Divisione"), tabdivi)
      xx_coddivi.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129187376393770320, "Descrizione divisione"), 0, True)
      anc2_codstab.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129187376650707000, "Stabilimento/filiale/negozio"), tabstab)
      xx_codstab.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129187376925445275, "Stabilimento/filiale/negozio"), 0, True)
      anc2_perc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129187377004444125, "Percentuale"), oApp.FormatSconti, 6, 0, 100)
      '--------------------------------------------------------------------------------------------------------------
      codditt.NTSForzaVisZoom = True
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub InitEntity(ByRef cleSotc As CLE__SOTC)
    oCleSotc = cleSotc
    AddHandler oCleSotc.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di FORM"
  Public Overridable Sub FRMCIANA2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      If oCleSotc.IsMultiDitta = False Then
        tlbDuplica.Enabled = False
        tlbDuplica.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleSotc.GetAnacent2(dsAna2) Then Me.Close()
      dcAna2.DataSource = dsAna2.Tables("ANACENT2")
      dsAna2.AcceptChanges()
      '--------------------------------------------------------------------------------------------------------------
      grAna2.DataSource = dcAna2
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMCIANA2_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRMCIANA2_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcAna2.Dispose()
      dsAna2.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi TOOLBAR"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvAna2.NTSNuovo()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbDuplica_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbDuplica.ItemClick
    Dim i As Integer = 0
    Dim dlgRes As DialogResult
    Dim dtrRow As DataRow

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not Salva() Then Return
      '--------------------------------------------------------------------------------------------------------------
      If IsNothing(grvAna2.NTSGetCurrentDataRow) Then Return
      '--------------------------------------------------------------------------------------------------------------
      dtrRow = grvAna2.NTSGetCurrentDataRow
      '--------------------------------------------------------------------------------------------------------------
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129188227434723760, _
        "Procedere con la duplicazione della riga corrente sulle altre ditte che posseggono:" & vbCrLf & _
        " --> Piano dei Conti '|" & NTSCStr(grvAna2.NTSGetCurrentDataRow()!anc2_codpcon) & "|'" & vbCrLf & _
        " --> Piando dei Conto di CA '|" & NTSCStr(grvAna2.NTSGetCurrentDataRow()!anc2_codpcca) & "|'"))
      If dlgRes = Windows.Forms.DialogResult.No Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oCleSotc.DuplicaDitta(dtrRow) Then
        '------------------------------------------------------------------------------------------------------------
        oCleSotc.GetAnacent2(dsAna2)
        dcAna2.DataSource = dsAna2.Tables("ANACENT2")
        dsAna2.AcceptChanges()
        grAna2.DataSource = dcAna2
        '------------------------------------------------------------------------------------------------------------
        For i = 0 To (dcAna2.List.Count - 1)
          If CType(dcAna2.Item(i), DataRowView)!codditt.ToString = NTSCStr(dtrRow!codditt) And _
             CType(dcAna2.Item(i), DataRowView)!anc2_codpcon.ToString = NTSCStr(dtrRow!anc2_codpcon) And _
             CType(dcAna2.Item(i), DataRowView)!anc2_conto.ToString = NTSCStr(dtrRow!anc2_conto) And _
             CType(dcAna2.Item(i), DataRowView)!anc2_codpcca.ToString = NTSCStr(dtrRow!anc2_codpcca) And _
             CType(dcAna2.Item(i), DataRowView)!anc2_contoca.ToString = NTSCStr(dtrRow!anc2_contoca) And _
             CType(dcAna2.Item(i), DataRowView)!anc2_codcena.ToString = NTSCStr(dtrRow!anc2_codcena) And _
             CType(dcAna2.Item(i), DataRowView)!anc2_codcfam.ToString = NTSCStr(dtrRow!anc2_codcfam) And _
             CType(dcAna2.Item(i), DataRowView)!anc2_coddivi.ToString = NTSCStr(dtrRow!anc2_coddivi) And _
             CType(dcAna2.Item(i), DataRowView)!anc2_codstab.ToString = NTSCStr(dtrRow!anc2_codstab) Then
            dcAna2.Position = i
            Exit For
          End If
        Next
        '------------------------------------------------------------------------------------------------------------
        oApp.MsgBoxInfo(oApp.Tr(Me, 129188364090464185, "Duplicazione terminata."))
        '------------------------------------------------------------------------------------------------------------
      Else
        oApp.MsgBoxInfo(oApp.Tr(Me, 129188363294792455, "Attenzione!" & vbCrLf & _
          "Duplicazione riga corrente non effettuata." & vbCrLf & _
          "motivi:" & vbCrLf & _
          " --> le altre ditte non posseggono gli stessi Piani dei conti della ditta '|" & NTSCStr(grvAna2.NTSGetCurrentDataRow()!codditt) & "|' corrente;" & vbCrLf & _
          " --> le altre ditte non posseggono dati coerenti (Conto, Centro, Famiglia/Prodotto, ecc.);" & vbCrLf & _
          " --> esistono già dati con le caratteristiche di riga."))
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim strDitta As String = NTSCStr(grvAna2.NTSGetCurrentDataRow()!codditt)

    Try
      If Not grvAna2.NTSDeleteRigaCorrente(dcAna2, True) Then Return
      oCleSotc.Anacent2Salva(strDitta, True)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvAna2.NTSRipristinaRigaCorrenteBefore(dcAna2, True) Then Return
      oCleSotc.Anacent2Ripristina(dcAna2.Position, dcAna2.Filter)
      grvAna2.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB

    Try
      Select Case grvAna2.FocusedColumn.Name.ToString.ToLower
        Case "codditt"
          SetFastZoom(NTSCStr(grvAna2.EditingValue), oParam)
          NTSZOOM.strIn = NTSCStr(grvAna2.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMTABANAZ", NTSCStr(grvAna2.EditingValue), oParam)
          If NTSCStr(NTSZOOM.strIn) <> NTSCStr(grvAna2.EditingValue) Then grvAna2.SetFocusedValue(NTSZOOM.strIn)
        Case "anc2_codcena", "anc2_codcfam", "anc2_coddivi", "anc2_codstab"
          '----------------------------------------------------------------------------------------------------------
          If NTSCStr(grvAna2.NTSGetCurrentDataRow()!codditt).Trim = "" Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 129188134268098915, "Indicare una ditta valida prima di passare alla selezione"))
            Return
          End If
          '----------------------------------------------------------------------------------------------------------
          SetFastZoom(NTSCStr(grvAna2.EditingValue), oParam)
          NTSZOOM.strIn = NTSCStr(grvAna2.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMTAB" & Microsoft.VisualBasic.Right(grvAna2.FocusedColumn.Name.ToString.ToUpper, 4), NTSCStr(grvAna2.NTSGetCurrentDataRow()!codditt), oParam)
          If NTSCStr(NTSZOOM.strIn) <> NTSCStr(grvAna2.EditingValue) Then grvAna2.SetFocusedValue(NTSZOOM.strIn)
          '----------------------------------------------------------------------------------------------------------        
        Case Else
          NTSCallStandardZoom()
      End Select
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    If Not Salva() Then Return
    Me.Close()
  End Sub
#End Region

#Region "Eventi di GRIGLIA"
  Public Overridable Sub grvAna2_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvAna2.NTSBeforeRowUpdate
    Try
      If Not Salva() Then e.Allow = False
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grvAna2_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAna2.NTSFocusedRowChanged
    Try
      If IsNothing(grvAna2.NTSGetCurrentDataRow) Then
        GctlSetVisEnab(codditt, False)
        GctlSetVisEnab(anc2_contoca, False)
        GctlSetVisEnab(anc2_codcena, False)
        GctlSetVisEnab(anc2_codcfam, False)
        GctlSetVisEnab(anc2_coddivi, False)
        GctlSetVisEnab(anc2_codstab, False)
      Else
        If grvAna2.NTSGetCurrentDataRow.RowState <> DataRowState.Added Then
          codditt.Enabled = False
          anc2_contoca.Enabled = False
          anc2_codcena.Enabled = False
          anc2_codcfam.Enabled = False
          anc2_coddivi.Enabled = False
          anc2_codstab.Enabled = False
        Else
          GctlSetVisEnab(codditt, False)
          GctlSetVisEnab(anc2_contoca, False)
          GctlSetVisEnab(anc2_codcena, False)
          GctlSetVisEnab(anc2_codcfam, False)
          GctlSetVisEnab(anc2_coddivi, False)
          GctlSetVisEnab(anc2_codstab, False)
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grvAna2_NTSCellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvAna2.NTSCellValueChanged
    Try
      Select Case grvAna2.FocusedColumn.Name.ToLower
        Case "codditt"
          With grvAna2.NTSGetCurrentDataRow
            !anc2_contoca = 0
            !anc2_codcena = 0
            !anc2_codcfam = " "
            !anc2_coddivi = 0
            !anc2_codstab = 0
            !anc2_perc = 0
          End With
      End Select
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      Dim dRes As DialogResult
      dRes = grvAna2.NTSSalvaRigaCorrente(dcAna2, oCleSotc.Anacent2RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          If GctlControllaOutNotEqual() = False Then Return False
          If Not oCleSotc.Anacent2Salva(NTSCStr(grvAna2.NTSGetCurrentDataRow()!codditt), False) Then Return False
        Case System.Windows.Forms.DialogResult.No : oCleSotc.Anacent2Ripristina(dcAna2.Position, dcAna2.Filter)
        Case System.Windows.Forms.DialogResult.Cancel : Return False
        Case System.Windows.Forms.DialogResult.Abort
      End Select
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

End Class