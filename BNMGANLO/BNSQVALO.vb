Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMSQVALO
  Public oCleAnlo As CLEMGANLO
  Public oCallParams As CLE__CLDP
  Public dsVall As New DataSet
  Public dcVall As New BindingSource
  Public strValoCodart As String
  Public lValoLotto, nValoCodpqua As Integer
  Public bValoAnnulla As Boolean
  Public bNew As Boolean
  Public dttCmb As New DataTable

  Private components As System.ComponentModel.IContainer

#Region "Inizializzazione Controlli"
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

  Public Overridable Sub InitEntity(ByRef cleAnlo As CLEMGANLO)
    oCleAnlo = cleAnlo
    AddHandler oCleAnlo.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMSQVALO))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.grVall = New NTSInformatica.NTSGrid
    Me.grvVall = New NTSInformatica.NTSGridView
    Me.xx_combo = New NTSInformatica.NTSGridColumn
    Me.alv_descamp = New NTSInformatica.NTSGridColumn
    Me.xx_valore = New NTSInformatica.NTSGridColumn
    Me.alv_valcombo = New NTSInformatica.NTSGridColumn
    Me.alv_desval = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.grVall, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvVall, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 4
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 0
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 1
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 2
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 3
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.cmdAnnulla)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 312)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.NTSActiveTrasparency = True
    Me.pnBottom.Size = New System.Drawing.Size(588, 34)
    Me.pnBottom.TabIndex = 4
    Me.pnBottom.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(336, 6)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(116, 23)
    Me.cmdAnnulla.TabIndex = 0
    Me.cmdAnnulla.Text = "&Annulla impostazioni"
    '
    'grVall
    '
    Me.grVall.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grVall.EmbeddedNavigator.Name = ""
    Me.grVall.Location = New System.Drawing.Point(0, 30)
    Me.grVall.MainView = Me.grvVall
    Me.grVall.Name = "grVall"
    Me.grVall.Size = New System.Drawing.Size(588, 282)
    Me.grVall.TabIndex = 5
    Me.grVall.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvVall})
    '
    'grvVall
    '
    Me.grvVall.ActiveFilterEnabled = False
    '
    'xx_combo
    '
    Me.xx_combo.AppearanceCell.Options.UseBackColor = True
    Me.xx_combo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_combo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_combo.Caption = "Valore combo"
    Me.xx_combo.Enabled = True
    Me.xx_combo.FieldName = "xx_combo"
    Me.xx_combo.Name = "xx_combo"
    Me.xx_combo.NTSRepositoryComboBox = Nothing
    Me.xx_combo.NTSRepositoryItemCheck = Nothing
    Me.xx_combo.NTSRepositoryItemMemo = Nothing
    Me.xx_combo.NTSRepositoryItemText = Nothing
    Me.xx_combo.Visible = True
    Me.xx_combo.VisibleIndex = 2
    Me.grvVall.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.alv_descamp, Me.xx_valore, Me.alv_valcombo, Me.alv_desval, Me.xx_combo})
    Me.grvVall.Enabled = True
    Me.grvVall.GridControl = Me.grVall
    Me.grvVall.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvVall.MinRowHeight = 14
    Me.grvVall.Name = "grvVall"
    Me.grvVall.NTSAllowDelete = True
    Me.grvVall.NTSAllowInsert = True
    Me.grvVall.NTSAllowUpdate = True
    Me.grvVall.NTSMenuContext = Nothing
    Me.grvVall.OptionsCustomization.AllowRowSizing = True
    Me.grvVall.OptionsFilter.AllowFilterEditor = False
    Me.grvVall.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvVall.OptionsNavigation.UseTabKey = False
    Me.grvVall.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvVall.OptionsView.ColumnAutoWidth = False
    Me.grvVall.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvVall.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvVall.OptionsView.ShowGroupPanel = False
    Me.grvVall.RowHeight = 14
    '
    'alv_descamp
    '
    Me.alv_descamp.AppearanceCell.Options.UseBackColor = True
    Me.alv_descamp.AppearanceCell.Options.UseTextOptions = True
    Me.alv_descamp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_descamp.Caption = "Descrizione"
    Me.alv_descamp.Enabled = False
    Me.alv_descamp.FieldName = "alv_descamp"
    Me.alv_descamp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_descamp.Name = "alv_descamp"
    Me.alv_descamp.NTSRepositoryComboBox = Nothing
    Me.alv_descamp.NTSRepositoryItemCheck = Nothing
    Me.alv_descamp.NTSRepositoryItemMemo = Nothing
    Me.alv_descamp.NTSRepositoryItemText = Nothing
    Me.alv_descamp.OptionsColumn.AllowEdit = False
    Me.alv_descamp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_descamp.OptionsColumn.ReadOnly = True
    Me.alv_descamp.OptionsFilter.AllowFilter = False
    Me.alv_descamp.Visible = True
    Me.alv_descamp.VisibleIndex = 0
    '
    'xx_valore
    '
    Me.xx_valore.AppearanceCell.Options.UseBackColor = True
    Me.xx_valore.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valore.Caption = "Valore"
    Me.xx_valore.Enabled = True
    Me.xx_valore.FieldName = "xx_valore"
    Me.xx_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valore.Name = "xx_valore"
    Me.xx_valore.NTSRepositoryComboBox = Nothing
    Me.xx_valore.NTSRepositoryItemCheck = Nothing
    Me.xx_valore.NTSRepositoryItemMemo = Nothing
    Me.xx_valore.NTSRepositoryItemText = Nothing
    Me.xx_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valore.OptionsFilter.AllowFilter = False
    Me.xx_valore.Visible = True
    Me.xx_valore.VisibleIndex = 1
    '
    'alv_valcombo
    '
    Me.alv_valcombo.AppearanceCell.Options.UseBackColor = True
    Me.alv_valcombo.AppearanceCell.Options.UseTextOptions = True
    Me.alv_valcombo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_valcombo.Enabled = False
    Me.alv_valcombo.FieldName = "alv_valcombo"
    Me.alv_valcombo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_valcombo.Name = "alv_valcombo"
    Me.alv_valcombo.NTSRepositoryComboBox = Nothing
    Me.alv_valcombo.NTSRepositoryItemCheck = Nothing
    Me.alv_valcombo.NTSRepositoryItemMemo = Nothing
    Me.alv_valcombo.NTSRepositoryItemText = Nothing
    Me.alv_valcombo.OptionsColumn.AllowEdit = False
    Me.alv_valcombo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_valcombo.OptionsColumn.ReadOnly = True
    Me.alv_valcombo.OptionsFilter.AllowFilter = False
    '
    'alv_desval
    '
    Me.alv_desval.AppearanceCell.Options.UseBackColor = True
    Me.alv_desval.AppearanceCell.Options.UseTextOptions = True
    Me.alv_desval.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.alv_desval.Enabled = False
    Me.alv_desval.FieldName = "alv_desval"
    Me.alv_desval.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.alv_desval.Name = "alv_desval"
    Me.alv_desval.NTSRepositoryComboBox = Nothing
    Me.alv_desval.NTSRepositoryItemCheck = Nothing
    Me.alv_desval.NTSRepositoryItemMemo = Nothing
    Me.alv_desval.NTSRepositoryItemText = Nothing
    Me.alv_desval.OptionsColumn.AllowEdit = False
    Me.alv_desval.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.alv_desval.OptionsColumn.ReadOnly = True
    Me.alv_desval.OptionsFilter.AllowFilter = False
    '
    'FRMSQVALO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(588, 346)
    Me.Controls.Add(Me.grVall)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMSQVALO"
    Me.Text = "VALORI PARAMETRI QUALITÀ LOTTO"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    CType(Me.grVall, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvVall, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '----------------------------------------------------------------------------------
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\salva.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      '----------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()

      grvVall.NTSSetParam(oMenu, oApp.Tr(Me, 129379098137381918, "Griglia"))
      alv_descamp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129379098137538089, "Descrizione"), 0, True)
      xx_valore.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129379098137694260, "Valore"), 0, True)

      oCleAnlo.CaricaCombo(nValoCodpqua, 0, dttCmb)
      xx_combo.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129379098137850431, "Valore combo"), dttCmb, "val", "cod")
      alv_desval.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130220872192080923, ""), 0, True)
      alv_valcombo.NTSSetParamSTR(oMenu, "", 0)

      grvVall.NTSAllowInsert = False

      '----------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
#End Region

#Region "Form"
  Public Overridable Sub FRMSQVALO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim lProgr As Integer = 0

    Try
      InitControls()
      '--------------------------------------------------------------	
      oCleAnlo.ImportaDati(strValoCodart, lValoLotto, nValoCodpqua)
      If Not oCleAnlo.ApriVall(strValoCodart, lValoLotto, dsVall) Then Me.Close() : Return

      If dsVall.Tables("VALLOTTI").Rows.Count <> 0 Then
        pnBottom.Visible = False
        pnBottom.Enabled = False
      End If

      dcVall.DataSource = dsVall.Tables("VALLOTTI")
      grVall.DataSource = dcVall

      GctlSetRoules()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi"
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      bValoAnnulla = True
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Toolbar"
  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If grvVall.NTSRipristinaRigaCorrenteBefore(dcVall, CType(IIf(sender Is Nothing, False, True), Boolean)) Then
        oCleAnlo.RipristinaVall(dcVall.Position, dcVall.Filter)
        grvVall.NTSRipristinaRigaCorrenteAfter()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    Try
      SendKeys.SendWait("{F1}")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      If Salva() Then Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

#Region "Eventi Griglia"
  Public Overridable Function GrvRighe_RowColChange() As Boolean
    Dim dttTmp As New DataTable
    Dim dcTmp As New BindingSource
    Try
      If grvVall.NTSGetCurrentDataRow Is Nothing Then Return False

      '--------------------------------------
      'compilo il combo delle unità di misura
      If Not xx_combo.ColumnEdit Is Nothing Then
        CType(xx_combo.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttCmb
        If grvVall.FocusedColumn.Name = "xx_combo" And NTSCInt(grvVall.NTSGetCurrentDataRow!alv_tipcamp) = 20 Then
          grvVall.liListCmb.Visible = False    'lo nascondo, visto che contiene tutte le unita di misura del db ...
          grVall.Refresh()
          oCleAnlo.CaricaCombo(nValoCodpqua, NTSCInt(grvVall.NTSGetCurrentDataRow!alv_ncampo), dttTmp)
          dttTmp.AcceptChanges()
          CType(xx_combo.ColumnEdit, NTSRepositoryItemComboBox).DataSource = dttTmp
          'attenzione: riagganciando a liListCmb il nuovo datatable viene resettato il valore contenuto nella cella della griglia:
          'memorizzo il valore corrente, disabilito la before/aftercolupdate, associo il datatable e reimposto il valore precedente
          dcTmp.DataSource = dttTmp
          For i As Integer = 0 To dcTmp.List.Count - 1
            If NTSCStr(CType(dcTmp.Current, DataRowView).Row!cod).ToUpper <> NTSCStr(grvVall.GetFocusedRowCellValue(xx_combo)).ToUpper Then
              dcTmp.MoveNext()
            Else
              Exit For
            End If
          Next
          grvVall.liListCmb.DataSource = dcTmp
          grvVall.liListCmb.Visible = True
        End If
      End If    'If Not xx_combo.ColumnEdit Is Nothing Then

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub grvVall_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvVall.NTSFocusedColumnChanged
    Try
      GrvRighe_RowColChange()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvVall_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvVall.NTSFocusedRowChanged
    Dim lWidth As Integer
    Try
      If grvVall.NTSGetCurrentDataRow Is Nothing Then Return

      If NTSCStr(grvVall.NTSGetCurrentDataRow!alv_valcombo).Trim = "" Then
        xx_combo.Enabled = False
        xx_valore.Enabled = True
      Else
        xx_combo.Enabled = True
        xx_valore.Enabled = False
      End If

      GrvRighe_RowColChange()

      lWidth = xx_valore.Width
      Select Case NTSCInt(grvVall.NTSGetCurrentDataRow!alv_tipcamp)
        Case 7 : xx_valore.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129379131508763497, "Valore numerico"), oApp.FormatQta, 10, 0, 9999999999)
        Case 8 : xx_valore.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129379132758675937, "Valore data"), False)
        Case 10 : xx_valore.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129379133053443053, "Valore testo"), 30, False)
      End Select
      xx_valore.Width = lWidth
      xx_valore.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Dim dtrT() As DataRow = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      Select Case grvVall.NTSSalvaRigaCorrente(dcVall, oCleAnlo.bHasChangesVall, False)
        Case System.Windows.Forms.DialogResult.Yes
          '----------------------------------------------------------------------------------------------------------
          If GctlControllaOutNotEqual() = False Then Return False
          If Not grvVall.FocusedColumn Is Nothing AndAlso grvVall.FocusedColumn.Name = "xx_combo" Then
            grvVall.FocusedColumn = alv_descamp 'altrimenti i combo delle altre righe si sbiancano!!!
          End If
          '----------------------------------------------------------------------------------------------------------
          For i As Integer = 0 To (dsVall.Tables("VALLOTTI").Rows.Count - 1)
            With dsVall.Tables("VALLOTTI").Rows(i)
              If NTSCStr(!xx_combo).Trim <> "" Then
                dtrT = dttCmb.Select("cod = " & CStrSQL(!xx_combo))
                If Not dtrT Is Nothing Then
                  If dtrT.Length > 0 Then !alv_desval = dtrT(0)!val
                End If
              End If
            End With
          Next
          '----------------------------------------------------------------------------------------------------------
          If Not oCleAnlo.SalvaVall() Then Return False
          '----------------------------------------------------------------------------------------------------------
        Case System.Windows.Forms.DialogResult.No : tlbRipristina_ItemClick(Nothing, Nothing)
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