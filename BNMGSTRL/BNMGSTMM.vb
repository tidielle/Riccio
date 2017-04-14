Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSTMM
  Public oCleStrl As CLEMGSTRL
  Public oCallParams As CLE__CLDP
  Public dsWarehouse As DataSet
  Public dcWarehouse As BindingSource = New BindingSource()

  Public mstrWarehouseSelected As String 'Lista dei magazzini per cui eseguire la stampa
  Public mstrPrinterType As String 'V= Stampa a video; C= Stampa su carta; P= Stampa PDF
  Public mbCancel As Boolean 'Flag per annullamento stampa

  Private components As System.ComponentModel.IContainer

#Region "Inizializzazione"
  Private Sub InitializeComponent()
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.cmdCancel = New NTSInformatica.NTSButton
    Me.fmPrinterType = New NTSInformatica.NTSGroupBox
    Me.opPrintPdf = New NTSInformatica.NTSRadioButton
    Me.opPrintPaper = New NTSInformatica.NTSRadioButton
    Me.opPrintMonitor = New NTSInformatica.NTSRadioButton
    Me.cmdSelectAll = New NTSInformatica.NTSButton
    Me.cmdDeselectAll = New NTSInformatica.NTSButton
    Me.grWarehouse = New NTSInformatica.NTSGrid
    Me.grvWarehouse = New NTSInformatica.NTSGridView
    Me.codditt = New NTSInformatica.NTSGridColumn
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.tb_codmaga = New NTSInformatica.NTSGridColumn
    Me.tb_desmaga = New NTSInformatica.NTSGridColumn
    CType(Me.fmPrinterType, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPrinterType.SuspendLayout()
    CType(Me.opPrintPdf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opPrintPaper.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opPrintMonitor.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvWarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.Color.Black
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'cmdOk
    '
    Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(346, 298)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(100, 23)
    Me.cmdOk.TabIndex = 14
    Me.cmdOk.Text = "Ok"
    '
    'cmdCancel
    '
    Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.ImageText = ""
    Me.cmdCancel.Location = New System.Drawing.Point(234, 298)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(100, 23)
    Me.cmdCancel.TabIndex = 13
    Me.cmdCancel.Text = "Annulla"
    '
    'fmPrinterType
    '
    Me.fmPrinterType.AllowDrop = True
    Me.fmPrinterType.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPrinterType.Appearance.Options.UseBackColor = True
    Me.fmPrinterType.Controls.Add(Me.opPrintPdf)
    Me.fmPrinterType.Controls.Add(Me.opPrintPaper)
    Me.fmPrinterType.Controls.Add(Me.opPrintMonitor)
    Me.fmPrinterType.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPrinterType.Location = New System.Drawing.Point(316, 8)
    Me.fmPrinterType.Name = "fmPrinterType"
    Me.fmPrinterType.Size = New System.Drawing.Size(130, 100)
    Me.fmPrinterType.TabIndex = 27
    Me.fmPrinterType.Text = "Tipo di stampa:"
    '
    'opPrintPdf
    '
    Me.opPrintPdf.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPrintPdf.Location = New System.Drawing.Point(6, 74)
    Me.opPrintPdf.Name = "opPrintPdf"
    Me.opPrintPdf.NTSCheckValue = "S"
    Me.opPrintPdf.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPrintPdf.Properties.Appearance.Options.UseBackColor = True
    Me.opPrintPdf.Properties.Caption = "Stampa su PDF"
    Me.opPrintPdf.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPrintPdf.Size = New System.Drawing.Size(114, 19)
    Me.opPrintPdf.TabIndex = 2
    '
    'opPrintPaper
    '
    Me.opPrintPaper.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPrintPaper.Location = New System.Drawing.Point(6, 49)
    Me.opPrintPaper.Name = "opPrintPaper"
    Me.opPrintPaper.NTSCheckValue = "S"
    Me.opPrintPaper.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPrintPaper.Properties.Appearance.Options.UseBackColor = True
    Me.opPrintPaper.Properties.Caption = "Stampa su carta"
    Me.opPrintPaper.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPrintPaper.Size = New System.Drawing.Size(114, 19)
    Me.opPrintPaper.TabIndex = 1
    '
    'opPrintMonitor
    '
    Me.opPrintMonitor.Cursor = System.Windows.Forms.Cursors.Default
    Me.opPrintMonitor.EditValue = True
    Me.opPrintMonitor.Location = New System.Drawing.Point(6, 24)
    Me.opPrintMonitor.Name = "opPrintMonitor"
    Me.opPrintMonitor.NTSCheckValue = "S"
    Me.opPrintMonitor.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opPrintMonitor.Properties.Appearance.Options.UseBackColor = True
    Me.opPrintMonitor.Properties.Caption = "Stampa a video"
    Me.opPrintMonitor.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opPrintMonitor.Size = New System.Drawing.Size(114, 19)
    Me.opPrintMonitor.TabIndex = 0
    '
    'cmdSelectAll
    '
    Me.cmdSelectAll.ImageText = ""
    Me.cmdSelectAll.Location = New System.Drawing.Point(316, 115)
    Me.cmdSelectAll.Name = "cmdSelectAll"
    Me.cmdSelectAll.Size = New System.Drawing.Size(130, 23)
    Me.cmdSelectAll.TabIndex = 28
    Me.cmdSelectAll.Text = "Seleziona tutti"
    '
    'cmdDeselectAll
    '
    Me.cmdDeselectAll.ImageText = ""
    Me.cmdDeselectAll.Location = New System.Drawing.Point(316, 144)
    Me.cmdDeselectAll.Name = "cmdDeselectAll"
    Me.cmdDeselectAll.Size = New System.Drawing.Size(130, 23)
    Me.cmdDeselectAll.TabIndex = 29
    Me.cmdDeselectAll.Text = "Deseleziona tutti"
    '
    'grWarehouse
    '
    Me.grWarehouse.EmbeddedNavigator.Name = ""
    Me.grWarehouse.Location = New System.Drawing.Point(8, 8)
    Me.grWarehouse.MainView = Me.grvWarehouse
    Me.grWarehouse.Name = "grWarehouse"
    Me.grWarehouse.Size = New System.Drawing.Size(302, 284)
    Me.grWarehouse.TabIndex = 30
    Me.grWarehouse.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvWarehouse})
    '
    'grvWarehouse
    '
    Me.grvWarehouse.ActiveFilterEnabled = False
    Me.grvWarehouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.codditt, Me.xx_seleziona, Me.tb_codmaga, Me.tb_desmaga})
    Me.grvWarehouse.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvWarehouse.Enabled = True
    Me.grvWarehouse.GridControl = Me.grWarehouse
    Me.grvWarehouse.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvWarehouse.MinRowHeight = 14
    Me.grvWarehouse.Name = "grvWarehouse"
    Me.grvWarehouse.NTSAllowDelete = True
    Me.grvWarehouse.NTSAllowInsert = True
    Me.grvWarehouse.NTSAllowUpdate = True
    Me.grvWarehouse.NTSMenuContext = Nothing
    Me.grvWarehouse.OptionsCustomization.AllowRowSizing = True
    Me.grvWarehouse.OptionsFilter.AllowFilterEditor = False
    Me.grvWarehouse.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvWarehouse.OptionsNavigation.UseTabKey = False
    Me.grvWarehouse.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvWarehouse.OptionsView.ColumnAutoWidth = False
    Me.grvWarehouse.OptionsView.EnableAppearanceEvenRow = True
    Me.grvWarehouse.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvWarehouse.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvWarehouse.OptionsView.ShowGroupPanel = False
    Me.grvWarehouse.RowHeight = 16
    '
    'codditt
    '
    Me.codditt.AppearanceCell.Options.UseBackColor = True
    Me.codditt.AppearanceCell.Options.UseTextOptions = True
    Me.codditt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.codditt.Enabled = False
    Me.codditt.FieldName = "codditt"
    Me.codditt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.codditt.Name = "codditt"
    Me.codditt.NTSRepositoryComboBox = Nothing
    Me.codditt.NTSRepositoryItemCheck = Nothing
    Me.codditt.NTSRepositoryItemMemo = Nothing
    Me.codditt.NTSRepositoryItemText = Nothing
    Me.codditt.OptionsColumn.AllowEdit = False
    Me.codditt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.codditt.OptionsColumn.ReadOnly = True
    Me.codditt.OptionsFilter.AllowFilter = False
    '
    'xx_seleziona
    '
    Me.xx_seleziona.AppearanceCell.Options.UseBackColor = True
    Me.xx_seleziona.AppearanceCell.Options.UseTextOptions = True
    Me.xx_seleziona.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_seleziona.Caption = "Seleziona"
    Me.xx_seleziona.Enabled = True
    Me.xx_seleziona.FieldName = "xx_seleziona"
    Me.xx_seleziona.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_seleziona.Name = "xx_seleziona"
    Me.xx_seleziona.NTSRepositoryComboBox = Nothing
    Me.xx_seleziona.NTSRepositoryItemCheck = Nothing
    Me.xx_seleziona.NTSRepositoryItemMemo = Nothing
    Me.xx_seleziona.NTSRepositoryItemText = Nothing
    Me.xx_seleziona.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_seleziona.OptionsFilter.AllowFilter = False
    Me.xx_seleziona.Visible = True
    Me.xx_seleziona.VisibleIndex = 0
    Me.xx_seleziona.Width = 55
    '
    'tb_codmaga
    '
    Me.tb_codmaga.AppearanceCell.Options.UseBackColor = True
    Me.tb_codmaga.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codmaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codmaga.Caption = "Codice"
    Me.tb_codmaga.Enabled = False
    Me.tb_codmaga.FieldName = "tb_codmaga"
    Me.tb_codmaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_codmaga.Name = "tb_codmaga"
    Me.tb_codmaga.NTSRepositoryComboBox = Nothing
    Me.tb_codmaga.NTSRepositoryItemCheck = Nothing
    Me.tb_codmaga.NTSRepositoryItemMemo = Nothing
    Me.tb_codmaga.NTSRepositoryItemText = Nothing
    Me.tb_codmaga.OptionsColumn.AllowEdit = False
    Me.tb_codmaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_codmaga.OptionsColumn.ReadOnly = True
    Me.tb_codmaga.OptionsFilter.AllowFilter = False
    Me.tb_codmaga.Visible = True
    Me.tb_codmaga.VisibleIndex = 1
    Me.tb_codmaga.Width = 76
    '
    'tb_desmaga
    '
    Me.tb_desmaga.AppearanceCell.Options.UseBackColor = True
    Me.tb_desmaga.AppearanceCell.Options.UseTextOptions = True
    Me.tb_desmaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_desmaga.Caption = "Descrizione"
    Me.tb_desmaga.Enabled = False
    Me.tb_desmaga.FieldName = "tb_desmaga"
    Me.tb_desmaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tb_desmaga.Name = "tb_desmaga"
    Me.tb_desmaga.NTSRepositoryComboBox = Nothing
    Me.tb_desmaga.NTSRepositoryItemCheck = Nothing
    Me.tb_desmaga.NTSRepositoryItemMemo = Nothing
    Me.tb_desmaga.NTSRepositoryItemText = Nothing
    Me.tb_desmaga.OptionsColumn.AllowEdit = False
    Me.tb_desmaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tb_desmaga.OptionsColumn.ReadOnly = True
    Me.tb_desmaga.OptionsFilter.AllowFilter = False
    Me.tb_desmaga.Visible = True
    Me.tb_desmaga.VisibleIndex = 2
    Me.tb_desmaga.Width = 146
    '
    'FRMMGSTMM
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(452, 333)
    Me.Controls.Add(Me.grWarehouse)
    Me.Controls.Add(Me.cmdDeselectAll)
    Me.Controls.Add(Me.cmdSelectAll)
    Me.Controls.Add(Me.fmPrinterType)
    Me.Controls.Add(Me.cmdOk)
    Me.Controls.Add(Me.cmdCancel)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGSTMM"
    Me.NTSLastControlFocussed = Me.cmdCancel
    Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
    Me.Text = "STAMPA INVENTARIO MULTI MAGAZZINO"
    CType(Me.fmPrinterType, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPrinterType.ResumeLayout(False)
    Me.fmPrinterType.PerformLayout()
    CType(Me.opPrintPdf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opPrintPaper.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opPrintMonitor.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvWarehouse, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    Me.GctlTipoDoc = ""

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Return True
  End Function

  Public Sub Initentity(ByVal cleStrl As CLEMGSTRL)
    oCleStrl = cleStrl
    AddHandler oCleStrl.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      grvWarehouse.NTSSetParam(oMenu, "STAMPA INVENTARIO MULTI MAGAZZINO")
      grvWarehouse.NTSAllowInsert = False

      codditt.NTSSetParamSTR(oMenu, "Ditta", 0, False)
      xx_seleziona.NTSSetParamCHK(oMenu, "Seleziona", "S", "N")
      tb_codmaga.NTSSetParamSTRTabe(oMenu, "Codice", tabmaga, False)
      tb_desmaga.NTSSetParamSTRTabe(oMenu, "Descrizione", tabmaga, False)

      opPrintPdf.NTSSetParam(oMenu, oApp.Tr(Me, 130779811654090561, "Stampa su PDF"), "P")
      opPrintPaper.NTSSetParam(oMenu, oApp.Tr(Me, 130779811654168681, "Stampa su carta"), "A")
      opPrintMonitor.NTSSetParam(oMenu, oApp.Tr(Me, 130779811654246801, "Stampa a video"), "M")

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
#End Region

#Region "Eventi di Form e Controlli"
  Public Overridable Sub FRMMGSTMM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      'predispongo i controlli
      InitControls()

      'inizializzo le variabili
      mstrWarehouseSelected = ""
      mstrPrinterType = ""
      mbCancel = True

      'leggo eventuali RECENT
      mstrWarehouseSelected = oMenu.GetSettingBusDitt(DittaCorrente, "BSMGSTRL", "RECENT", ".", "ListaMagazzini", "", " ", "")
      mstrPrinterType = oMenu.GetSettingBusDitt(DittaCorrente, "BSMGSTRL", "RECENT", ".", "TipoStampa", "", " ", "")

      'leggo dal database i dati e collego il NTSBindingNavigator
      If oCleStrl.LoadWarehouse(dsWarehouse) Then
        dcWarehouse.DataSource = dsWarehouse.Tables("tabmaga")
        dsWarehouse.AcceptChanges()
        grWarehouse.DataSource = dcWarehouse

        SetRecent()

        '-------------------------------------------------
        'sempre alla fine di questa funzione: applico le regole della gctl
        GctlSetRoules()
      Else
        CloseForm()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectAll.Click
    ChangeAllSelectionItem(True)
  End Sub

  Public Overridable Sub cmdDeselectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeselectAll.Click
    ChangeAllSelectionItem(False)
  End Sub

  Public Overridable Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
    CloseForm()
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    ExecutePrint()
  End Sub
#End Region

#Region "Funzioni/Metodi"
  ''' <summary>
  ''' Modifica la selezione dei magazzini presenti in griglia, come indicato dal parametro.
  ''' </summary>
  ''' <param name="pbSelect">True= Seleziona tutti i magazzini; False= Deseleziona tutti i magazzini</param>
  ''' <remarks></remarks>
  Public Overridable Sub ChangeAllSelectionItem(ByVal pbSelect As Boolean)
    Dim strSeleziona As String = "N"

    Try
      'Imposto lo stato da assegnare
      If pbSelect Then
        strSeleziona = "S"
      End If

      'Applico lo stato indicato
      For i As Integer = 0 To (dsWarehouse.Tables("tabmaga").Rows.Count - 1)
        dsWarehouse.Tables("tabmaga").Rows(i).Item("xx_seleziona") = strSeleziona
      Next
      dsWarehouse.Tables("tabmaga").AcceptChanges()

      dcWarehouse.MoveFirst()
      grvWarehouse.LeftCoord = 0
      grvWarehouse.Focus()

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  ''' <summary>
  ''' Chiude la form.
  ''' </summary>
  Public Overridable Sub CloseForm()
    Try
      If Not (dcWarehouse Is Nothing) Then
        dcWarehouse.Dispose()
        dcWarehouse = Nothing
      End If

      If Not (dsWarehouse Is Nothing) Then
        dsWarehouse.Dispose()
        dsWarehouse = Nothing
      End If

      Me.Close()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  ''' <summary>
  ''' Imposta l'elenco dei magazzini da stampare e il tipo di stampa
  ''' </summary>
  Public Overridable Function SetWarehouseSelected() As Boolean
    Dim bRetVal As Boolean = False

    Try
      'Resetto l'elenco dei magazzini da gestire
      mstrWarehouseSelected = ""

      'Setto l'elenco dei magazzini per cui eseguire la stampa
      For Each dtrRow As DataRow In dsWarehouse.Tables("tabmaga").Select("xx_seleziona = 'S'")
        If (mstrWarehouseSelected.Length = 0) Then
          mstrWarehouseSelected = NTSCStr(dtrRow.Item("tb_codmaga")) & "§" & NTSCStr(dtrRow.Item("tb_desmaga"))
        Else
          mstrWarehouseSelected += "|" & NTSCStr(dtrRow.Item("tb_codmaga")) & "§" & NTSCStr(dtrRow.Item("tb_desmaga"))
        End If
      Next

      'Setto il tipo di stampa da eseguire
      If opPrintMonitor.Checked Then
        mstrPrinterType = "V"
      Else
        If opPrintPaper.Checked Then
          mstrPrinterType = "C"
        Else
          mstrPrinterType = "P"
        End If
      End If

      bRetVal = True

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    Return bRetVal
  End Function

  ''' <summary>
  ''' Imposta le condizioni di stampa per i magazzini selezionati
  ''' </summary>
  Public Overridable Sub ExecutePrint()
    Try
      If SetWarehouseSelected() Then
        'Salva nei recent le condizioni impostate
        oMenu.SaveSettingBus("BSMGSTRL", "RECENT", ".", "ListaMagazzini", mstrWarehouseSelected, " ", "NS.", "...", "...")
        oMenu.SaveSettingBus("BSMGSTRL", "RECENT", ".", "TipoStampa", mstrPrinterType, " ", "NS.", "...", "...")

        mbCancel = False

        CloseForm()
      End If
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub SetRecent()
    Dim i As Integer
    Dim arrstrWarehouseSelected() As String = Nothing
    Dim strWarehouseCode As String

    Try
      'Spunto i magazzini precedentemente selezionati
      If (mstrWarehouseSelected.Length > 0) Then
        arrstrWarehouseSelected = mstrWarehouseSelected.Split(CType("|", Char))
        For i = 0 To arrstrWarehouseSelected.Length - 1
          strWarehouseCode = arrstrWarehouseSelected(i).Substring(0, arrstrWarehouseSelected(i).IndexOf(CType("§", Char)))
          dsWarehouse.Tables("tabmaga").Select("tb_codmaga = " & NTSCInt(strWarehouseCode))(0).Item("xx_seleziona") = "S"
        Next

      End If

      'Gestisco il tipo di stampa
      If (mstrPrinterType.Length > 0) Then
        Select Case mstrPrinterType
          Case "V" 'Stampa a video
            opPrintMonitor.Checked = True
          Case "C" 'Stampa su carta
            opPrintPaper.Checked = True
          Case "P" 'Stampa PDF
            opPrintPdf.Checked = True
        End Select
      End If

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region


End Class
