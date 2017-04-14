Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__GRSF
  Public oCleSpaf As CLE__SPAF
  Public oCallParams As CLE__CLDP

  Private components As System.ComponentModel.IContainer

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    Me.GctlTipoDoc = ""

    InitializeComponent()
    Me.MinimumSize = Me.Size

    Return True
  End Function

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__GRSF))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbCambioDitta = New NTSInformatica.NTSBarMenuItem
    Me.grGrsf = New NTSInformatica.NTSGrid
    Me.grvGrsf = New NTSInformatica.NTSGridView
    Me.xx_nome = New NTSInformatica.NTSGridColumn
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGrsf, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGrsf, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbImpostaStampante, Me.tlbCambioDitta})
    Me.NtsBarManager1.MaxItemId = 36
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Id = 20
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 25
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbCambioDitta
    '
    Me.tlbCambioDitta.Caption = "Cambia ditta"
    Me.tlbCambioDitta.Id = 28
    Me.tlbCambioDitta.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbCambioDitta.Name = "tlbCambioDitta"
    Me.tlbCambioDitta.NTSIsCheckBox = False
    Me.tlbCambioDitta.Visible = True
    '
    'grGrsf
    '
    Me.grGrsf.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGrsf.EmbeddedNavigator.Name = ""
    Me.grGrsf.Location = New System.Drawing.Point(0, 30)
    Me.grGrsf.MainView = Me.grvGrsf
    Me.grGrsf.Name = "grGrsf"
    Me.grGrsf.Size = New System.Drawing.Size(637, 300)
    Me.grGrsf.TabIndex = 9
    Me.grGrsf.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGrsf})
    '
    'grvGrsf
    '
    Me.grvGrsf.ActiveFilterEnabled = False
    Me.grvGrsf.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_nome})
    Me.grvGrsf.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGrsf.Enabled = False
    Me.grvGrsf.GridControl = Me.grGrsf
    Me.grvGrsf.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvGrsf.Name = "grvGrsf"
    Me.grvGrsf.NTSAllowDelete = True
    Me.grvGrsf.NTSAllowInsert = True
    Me.grvGrsf.NTSAllowUpdate = True
    Me.grvGrsf.NTSMenuContext = Nothing
    Me.grvGrsf.OptionsBehavior.Editable = False
    Me.grvGrsf.OptionsCustomization.AllowRowSizing = True
    Me.grvGrsf.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGrsf.OptionsNavigation.UseTabKey = False
    Me.grvGrsf.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGrsf.OptionsView.ColumnAutoWidth = False
    Me.grvGrsf.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGrsf.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvGrsf.OptionsView.ShowGroupPanel = False
    Me.grvGrsf.RowHeight = 14
    '
    'xx_nome
    '
    Me.xx_nome.AppearanceCell.Options.UseBackColor = True
    Me.xx_nome.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nome.Enabled = True
    Me.xx_nome.FieldName = "xx_nome"
    Me.xx_nome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nome.Name = "xx_nome"
    Me.xx_nome.NTSRepositoryComboBox = Nothing
    Me.xx_nome.NTSRepositoryItemCheck = Nothing
    Me.xx_nome.NTSRepositoryItemMemo = Nothing
    Me.xx_nome.NTSRepositoryItemText = Nothing
    Me.xx_nome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_nome.OptionsFilter.AllowFilter = False
    Me.xx_nome.Visible = True
    Me.xx_nome.VisibleIndex = 0
    Me.xx_nome.Width = 20
    '
    'FRM__GRSF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(637, 330)
    Me.Controls.Add(Me.grGrsf)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.MinimizeBox = False
    Me.Name = "FRM__GRSF"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGrsf, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGrsf, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Dim i As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
      End Try
      '--------------------------------------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      grvGrsf.NTSSetParam(oMenu, oApp.Tr(Me, 128858942669109371, "Griglia"))
      xx_nome.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128859860373570671, "Colonne"), 0, True)
      '--------------------------------------------------------------------------------------------------------------
      grvGrsf.NTSAllowDelete = False
      grvGrsf.NTSAllowUpdate = False
      grvGrsf.NTSAllowInsert = False
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub

  Public Overridable Sub InitEntity(ByVal cleSpaf As CLE__SPAF)
    oCleSpaf = cleSpaf
    AddHandler oCleSpaf.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__ART2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      '--- Intestazione della form
      '--------------------------------------------------------------------------------------------------------------
      Me.Text = oCleSpaf.strTitle
      '--------------------------------------------------------------------------------------------------------------
      InizializzaGriglia()
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
      GctlApplicaDefaultValue()
      '--------------------------------------------------------------------------------------------------------------
      grvGrsf.BestFitColumns()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi ToolBar"
  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub InizializzaGriglia()
    Dim bColonnaMemo As Boolean = False
    Dim i As Integer = 0
    Dim lLarghezza As Integer = 0
    Dim strFormat As String = ""
    Dim dttTmp As New DataTable
    Dim dcGrsf As New BindingSource

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Do il focus alla colonna che non viene mai cancellata, 
      '--- altrimenti darebbe errore perchè l'ultima colonna selezionata non esiste
      '--------------------------------------------------------------------------------------------------------------
      grvGrsf.FocusedColumn = grvGrsf.Columns(0)
      '--------------------------------------------------------------------------------------------------------------
      grGrsf.DataSource = Nothing
      '--------------------------------------------------------------------------------------------------------------
      '--- Cancello tutte le colonne tranne la prima
      '--------------------------------------------------------------------------------------------------------------
      While grvGrsf.Columns.Count <> 1
        grvGrsf.Columns.Remove(grvGrsf.Columns(1))
      End While
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiungo le nuove colonne
      '--------------------------------------------------------------------------------------------------------------
      If oCleSpaf.ApriParstca(dttTmp) = True Then
        For i = 0 To (dttTmp.Rows.Count - 1)
          xx_col = New NTSInformatica.NTSGridColumn
          xx_col.AppearanceCell.Options.UseBackColor = True
          xx_col.AppearanceCell.Options.UseTextOptions = True
          xx_col.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
          xx_col.Enabled = True
          xx_col.Caption = NTSCStr(dttTmp.Rows(i)!pfc_desstamp)
          xx_col.FieldName = oCleSpaf.dttDatiGriglia.Columns(i).ColumnName
          xx_col.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
          xx_col.Name = oCleSpaf.dttDatiGriglia.Columns(i).ColumnName
          xx_col.EXT = False
          xx_col.NTSRepositoryComboBox = Nothing
          xx_col.NTSRepositoryItemCheck = Nothing
          xx_col.NTSRepositoryItemMemo = Nothing
          xx_col.NTSRepositoryItemText = Nothing
          xx_col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
          xx_col.OptionsFilter.AllowFilter = False
          xx_col.Visible = True
          xx_col.VisibleIndex = i
          Select Case NTSCInt(dttTmp.Rows(i)!pfc_tipcampo)
            Case 3 : lLarghezza = 60             '--- dbInteger
            Case 4, 5, 7, 8 : lLarghezza = 100   '--- dbLong, dbDouble, dbCurrency, dbDate
            Case 10 : lLarghezza = 150           '--- dbText
            Case 12 : lLarghezza = 200           '--- dbMemo
          End Select
          xx_col.Width = lLarghezza
          '----------------------------------------------------------------------------------------------------------
          grvGrsf.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_col})
          '----------------------------------------------------------------------------------------------------------
        Next
        '------------------------------------------------------------------------------------------------------------
        grvGrsf.Columns(0).Width = 0
        '------------------------------------------------------------------------------------------------------------
        '--- Creo l'initcontrols
        '------------------------------------------------------------------------------------------------------------
        For i = 1 To (grvGrsf.Columns.Count - 1)
          '----------------------------------------------------------------------------------------------------------
          '--- Determino il formato
          '----------------------------------------------------------------------------------------------------------
          strFormat = NTSCStr(dttTmp.Rows(i - 1)!pfc_format)
          If NTSCStr(dttTmp.Rows(i - 1)!pfc_nomcampo).ToLower = "listini.lc_prezzo" Then
            Select Case oCleSpaf.nDecSuPrzUn
              Case 2, 3, 4 : strFormat = "#,##0." & "".PadLeft(oCleSpaf.nDecSuPrzUn, "0"c)
            End Select
          End If
          '----------------------------------------------------------------------------------------------------------
          bColonnaMemo = CBool(IIf(NTSCInt(dttTmp.Rows(i - 1)!pfc_tipcampo) = 12, True, False))
          '----------------------------------------------------------------------------------------------------------
          Select Case NTSCInt(dttTmp.Rows(i - 1)!pfc_tipcampo)
            Case 3, 4, 5, 7     '--- Colonna numerica (dbInteger, dblong, dbCurrency, dbDouble, dbDecimal)
              CType(grvGrsf.Columns(i), NTSInformatica.NTSGridColumn).NTSSetParamNUM(oMenu, oApp.Tr(Me, 129726705507050196, "Colonna"), strFormat, 0)
            Case 8              '--- Colonna data (dbDate)
              CType(grvGrsf.Columns(i), NTSInformatica.NTSGridColumn).NTSSetParamDATA(oMenu, oApp.Tr(Me, 129726705524199292, "Colonna"), True)
            Case Else           '--- Colonna testo (dbText, dbMemo)
              CType(grvGrsf.Columns(i), NTSInformatica.NTSGridColumn).NTSSetParamSTR(oMenu, oApp.Tr(Me, 129726705538447886, "Colonna"), 0, True, bColonnaMemo)
          End Select
        Next
        '------------------------------------------------------------------------------------------------------------
      End If
      '--------------------------------------------------------------------------------------------------------------
      grvGrsf.Columns(0).Visible = False
      '--------------------------------------------------------------------------------------------------------------
      dcGrsf.DataSource = oCleSpaf.dttDatiGriglia
      grGrsf.DataSource = dcGrsf
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

End Class