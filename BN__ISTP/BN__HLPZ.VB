Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HLPZ

#Region "Variabili"
  Public oCleIstp As CLE__ISTP
  Public oCallParams As CLE__CLDP
  Public dsHlpz As New DataSet
  Public dcHlpz As New BindingSource()

  Public WithEvents pnHlpz As NTSInformatica.NTSPanel
  Public WithEvents grHlpz As NTSInformatica.NTSGrid
  Public WithEvents grvHlpz As NTSInformatica.NTSGridView
  Public WithEvents Pz_Codform As NTSInformatica.NTSGridColumn
  Public WithEvents Pz_Titstam As NTSInformatica.NTSGridColumn
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton

  Private components As System.ComponentModel.IContainer
#End Region

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
        grvHlpz.NTSSetParam(oMenu, oApp.Tr(Me, 128309722050644032, "Zoom Formati di Stampa"))
        Pz_Codform.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128898040669076019, "Form. Stampa"), "0", 4, 0, 9999)
        Pz_Titstam.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128898040687670007, "Titolo"), 0, True)
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      grvHlpz.NTSAllowInsert = False

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
  Public Overridable Sub InitializeComponent()
    Me.pnHlpz = New NTSInformatica.NTSPanel
    Me.grHlpz = New NTSInformatica.NTSGrid
    Me.grvHlpz = New NTSInformatica.NTSGridView
    Me.Pz_Codform = New NTSInformatica.NTSGridColumn
    Me.Pz_Titstam = New NTSInformatica.NTSGridColumn
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    CType(Me.pnHlpz, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnHlpz.SuspendLayout()
    CType(Me.grHlpz, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvHlpz, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'pnHlpz
    '
    Me.pnHlpz.AllowDrop = True
    Me.pnHlpz.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnHlpz.Appearance.Options.UseBackColor = True
    Me.pnHlpz.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnHlpz.Controls.Add(Me.grHlpz)
    Me.pnHlpz.Controls.Add(Me.cmdAnnulla)
    Me.pnHlpz.Controls.Add(Me.cmdOk)
    Me.pnHlpz.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnHlpz.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnHlpz.Location = New System.Drawing.Point(0, 0)
    Me.pnHlpz.Name = "pnHlpz"
    Me.pnHlpz.Size = New System.Drawing.Size(388, 244)
    Me.pnHlpz.TabIndex = 13
    Me.pnHlpz.Text = "NtsPanel1"
    '
    'grHlpz
    '
    Me.grHlpz.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '
    '
    '
    Me.grHlpz.EmbeddedNavigator.Name = ""
    Me.grHlpz.Location = New System.Drawing.Point(3, 3)
    Me.grHlpz.MainView = Me.grvHlpz
    Me.grHlpz.Name = "grHlpz"
    Me.grHlpz.Size = New System.Drawing.Size(300, 238)
    Me.grHlpz.TabIndex = 14
    Me.grHlpz.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHlpz})
    '
    'grvHlpz
    '
    Me.grvHlpz.ActiveFilterEnabled = False
    Me.grvHlpz.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.Pz_Codform, Me.Pz_Titstam})
    Me.grvHlpz.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvHlpz.Enabled = True
    Me.grvHlpz.GridControl = Me.grHlpz
    Me.grvHlpz.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvHlpz.Name = "grvHlpz"
    Me.grvHlpz.NTSAllowDelete = True
    Me.grvHlpz.NTSAllowInsert = True
    Me.grvHlpz.NTSAllowUpdate = True
    Me.grvHlpz.NTSMenuContext = Nothing
    Me.grvHlpz.OptionsCustomization.AllowRowSizing = True
    Me.grvHlpz.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvHlpz.OptionsNavigation.UseTabKey = False
    Me.grvHlpz.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvHlpz.OptionsView.ColumnAutoWidth = False
    Me.grvHlpz.OptionsView.EnableAppearanceEvenRow = True
    Me.grvHlpz.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvHlpz.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvHlpz.OptionsView.ShowGroupPanel = False
    Me.grvHlpz.RowHeight = 14
    '
    'Pz_Codform
    '
    Me.Pz_Codform.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.Pz_Codform.AppearanceCell.Options.UseBackColor = True
    Me.Pz_Codform.AppearanceCell.Options.UseTextOptions = True
    Me.Pz_Codform.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.Pz_Codform.Caption = "Form. Stampa"
    Me.Pz_Codform.Enabled = False
    Me.Pz_Codform.FieldName = "Pz_Codform"
    Me.Pz_Codform.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.Pz_Codform.Name = "Pz_Codform"
    Me.Pz_Codform.NTSRepositoryComboBox = Nothing
    Me.Pz_Codform.NTSRepositoryItemCheck = Nothing
    Me.Pz_Codform.NTSRepositoryItemMemo = Nothing
    Me.Pz_Codform.NTSRepositoryItemText = Nothing
    Me.Pz_Codform.OptionsColumn.AllowEdit = False
    Me.Pz_Codform.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.Pz_Codform.OptionsColumn.ReadOnly = True
    Me.Pz_Codform.OptionsFilter.AllowFilter = False
    Me.Pz_Codform.Visible = True
    Me.Pz_Codform.VisibleIndex = 0
    '
    'Pz_Titstam
    '
    Me.Pz_Titstam.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.Pz_Titstam.AppearanceCell.Options.UseBackColor = True
    Me.Pz_Titstam.AppearanceCell.Options.UseTextOptions = True
    Me.Pz_Titstam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.Pz_Titstam.Caption = "Titolo"
    Me.Pz_Titstam.Enabled = False
    Me.Pz_Titstam.FieldName = "Pz_Titstam"
    Me.Pz_Titstam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.Pz_Titstam.Name = "Pz_Titstam"
    Me.Pz_Titstam.NTSRepositoryComboBox = Nothing
    Me.Pz_Titstam.NTSRepositoryItemCheck = Nothing
    Me.Pz_Titstam.NTSRepositoryItemMemo = Nothing
    Me.Pz_Titstam.NTSRepositoryItemText = Nothing
    Me.Pz_Titstam.OptionsColumn.AllowEdit = False
    Me.Pz_Titstam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.Pz_Titstam.OptionsColumn.ReadOnly = True
    Me.Pz_Titstam.OptionsFilter.AllowFilter = False
    Me.Pz_Titstam.Visible = True
    Me.Pz_Titstam.VisibleIndex = 1
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.Location = New System.Drawing.Point(310, 41)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(71, 23)
    Me.cmdAnnulla.TabIndex = 13
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdOk.Location = New System.Drawing.Point(310, 12)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(71, 23)
    Me.cmdOk.TabIndex = 12
    Me.cmdOk.Text = "&Conferma"
    '
    'FRM__HLPZ
    '
    Me.ClientSize = New System.Drawing.Size(388, 244)
    Me.Controls.Add(Me.pnHlpz)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__HLPZ"
    Me.NTSLastControlFocussed = Me.grHlpz
    Me.Text = "ZOOM FORMATI DI STAMPA"
    CType(Me.pnHlpz, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnHlpz.ResumeLayout(False)
    CType(Me.grHlpz, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvHlpz, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitEntity(ByRef cleIstp As CLE__ISTP)
    oCleIstp = cleIstp
    AddHandler oCleIstp.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRM__HLPZ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleIstp.HlpzApri(DittaCorrente, dsHlpz) Then Me.Close()

      dcHlpz.DataSource = dsHlpz.Tables("PARSTAG")
      grHlpz.DataSource = dcHlpz

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__HLPZ_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcHlpz.Dispose()
      dsHlpz.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleIstp.nHlpzCodform = 0
      oCleIstp.strHlpzTitstam = ""
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      oCleIstp.nHlpzCodform = NTSCInt(dsHlpz.Tables("PARSTAG").Rows(dcHlpz.Position)!Pz_Codform)
      oCleIstp.strHlpzTitstam = NTSCStr(dsHlpz.Tables("PARSTAG").Rows(dcHlpz.Position)!Pz_Titstam)
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvHlpz_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvHlpz.DoubleClick
    Try
      If cmdOk.Enabled Then cmdOk_Click(cmdOk, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class