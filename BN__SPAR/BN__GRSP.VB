Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__GRSP
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property

#Region "Variabili"
  Public dsGrsp As DataSet
  Public oCleSpar As CLE__SPAR
  Public oCallParams As CLE__CLDP
  Public dcGrsp As BindingSource = New BindingSource

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents grGrsp As NTSInformatica.NTSGrid
  Public WithEvents grvGrsp As NTSInformatica.NTSGridView

  Private components As System.ComponentModel.IContainer
#End Region

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__GRSP))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grGrsp = New NTSInformatica.NTSGrid
    Me.grvGrsp = New NTSInformatica.NTSGridView
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grGrsp, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvGrsp, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 6
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
    Me.tlbGuida.Id = 4
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 5
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grGrsp
    '
    Me.grGrsp.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grGrsp.EmbeddedNavigator.Name = ""
    Me.grGrsp.Location = New System.Drawing.Point(0, 30)
    Me.grGrsp.MainView = Me.grvGrsp
    Me.grGrsp.Name = "grGrsp"
    Me.grGrsp.Size = New System.Drawing.Size(523, 362)
    Me.grGrsp.TabIndex = 4
    Me.grGrsp.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvGrsp})
    '
    'grvGrsp
    '
    Me.grvGrsp.ActiveFilterEnabled = False
    Me.grvGrsp.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvGrsp.Enabled = True
    Me.grvGrsp.GridControl = Me.grGrsp
    Me.grvGrsp.Name = "grvGrsp"
    Me.grvGrsp.NTSAllowDelete = True
    Me.grvGrsp.NTSAllowInsert = True
    Me.grvGrsp.NTSAllowUpdate = True
    Me.grvGrsp.NTSMenuContext = Nothing
    Me.grvGrsp.OptionsCustomization.AllowRowSizing = True
    Me.grvGrsp.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvGrsp.OptionsNavigation.UseTabKey = False
    Me.grvGrsp.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvGrsp.OptionsView.ColumnAutoWidth = False
    Me.grvGrsp.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvGrsp.OptionsView.ShowGroupPanel = False
    Me.grvGrsp.RowHeight = 14
    '
    'FRM__GRSP
    '
    Me.ClientSize = New System.Drawing.Size(523, 392)
    Me.Controls.Add(Me.grGrsp)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.HelpContext = "*"
    Me.Name = "FRM__GRSP"
    Me.NTSLastControlFocussed = Me.grGrsp
    Me.Text = ""
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grGrsp, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvGrsp, System.ComponentModel.ISupportInitialize).EndInit()
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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      grvGrsp.NTSSetParam(oMenu, oApp.Tr(Me, 128311437002944026, ""))
      grvGrsp.NTSAllowDelete = False
      grvGrsp.NTSAllowInsert = False
      grvGrsp.NTSAllowUpdate = False
      grvGrsp.Enabled = False

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
  Public Overridable Sub InitEntity(ByRef cleSpar As CLE__SPAR)
    oCleSpar = cleSpar
    AddHandler oCleSpar.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRM__GRSP_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
  

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__GRSP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleSpar.GrspApri(DittaCorrente, dsGrsp) Then
        Me.Close()
        Exit Sub
      End If

      CreaColonneGriglia()

      dcGrsp.DataSource = dsGrsp.Tables("TAB")
      grGrsp.DataSource = dcGrsp

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__GRSP__FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcGrsp.Dispose()
      dsGrsp.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "EventiToolbar"
  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub CreaColonneGriglia()
    Dim i As Integer
    Dim oColNew As NTSGridColumn
    Try
      For i = 0 To dsGrsp.Tables("TAB").Columns.Count - 1
        oColNew = New NTSGridColumn
        oColNew.Name = dsGrsp.Tables("TAB").Columns(i).ColumnName.ToString.ToLower
        oColNew.EXT = False
        oColNew.AppearanceCell.Options.UseBackColor = True
        oColNew.AppearanceCell.Options.UseTextOptions = True
        oColNew.Caption = dsGrsp.Tables("TAB").Columns(i).ColumnName.ToString
        oColNew.Enabled = True
        oColNew.FieldName = dsGrsp.Tables("TAB").Columns(i).ColumnName.ToString.ToLower
        oColNew.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
        oColNew.NTSRepositoryComboBox = Nothing
        oColNew.NTSRepositoryItemCheck = Nothing
        oColNew.NTSRepositoryItemText = Nothing
        oColNew.Visible = True
        oColNew.Width = 100
        oColNew.VisibleIndex = i
        grvGrsp.Columns.Add(oColNew)
        If dsGrsp.Tables("TAB").Columns(i).ColumnName.ToString.ToLower.IndexOf("prezzo") > -1 Then
          oColNew.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129545811579697265, ""), TrovaFmtPrz(0), 0)
        Else
          If dsGrsp.Tables("TAB").Columns(i).DataType.ToString.ToLower.IndexOf("dou") > -1 Or _
             dsGrsp.Tables("TAB").Columns(i).DataType.ToString.ToLower.IndexOf("flo") > -1 Or _
             dsGrsp.Tables("TAB").Columns(i).DataType.ToString.ToLower.IndexOf("dec") > -1 Then
            oColNew.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129545811613447265, ""), oApp.FormatImporti, 15)
          ElseIf dsGrsp.Tables("TAB").Columns(i).DataType.ToString.ToLower.IndexOf("int") > -1 Then
            oColNew.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129616144856606071, ""), "0", 15)
          ElseIf dsGrsp.Tables("TAB").Columns(i).DataType.ToString.ToLower.IndexOf("date") > -1 Then
            oColNew.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128735676344896035, ""), True)
          Else
            oColNew.NTSSetParamSTR(oMenu, dsGrsp.Tables("TAB").Columns(i).ColumnName.ToString, 0)
          End If

        End If
        If i = 0 Then grvGrsp.FocusedColumn = oColNew
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function TrovaFmtPrz(Optional ByVal nCodvalu As Integer = 0) As String
    Dim i As Integer
    Try
      TrovaFmtPrz = "#,##0"
      If nCodvalu = 0 Then
        If oApp.NDecPrzUn > 0 Then TrovaFmtPrz = TrovaFmtPrz & "."
        For i = 1 To oApp.NDecPrzUn
          TrovaFmtPrz = TrovaFmtPrz & "0"
        Next
      Else
        If oApp.NDecPrzUnVal > 0 Then
          TrovaFmtPrz = TrovaFmtPrz & "."
          For i = 1 To oApp.NDecPrzUnVal
            TrovaFmtPrz = TrovaFmtPrz & "0"
          Next
        End If
      End If

    Catch ex As Exception
      Throw (New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False)))
    End Try
  End Function

End Class