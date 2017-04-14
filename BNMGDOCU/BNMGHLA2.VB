Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMDBHLA2

#Region "Variabili"
  Public oCleDocu As CLEMGDOCU
  Public oCallParams As CLE__CLDP
  Public dsHla2 As New DataSet
  Public dcHla2 As New BindingSource()
  Public components As System.ComponentModel.IContainer

  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents lbXx_Codart As NTSInformatica.NTSLabel
  Public WithEvents lbCodart As NTSInformatica.NTSLabel
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents grHla2 As NTSInformatica.NTSGrid
  Public WithEvents grvHla2 As NTSInformatica.NTSGridView
  Public WithEvents ar_codart As NTSInformatica.NTSGridColumn
  Public WithEvents ar_descr As NTSInformatica.NTSGridColumn
  Public WithEvents edCodart As NTSInformatica.NTSTextBoxStr
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

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGDOCU", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128550728307822408, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGDOCU", strRemoteServer, strRemotePort)
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleDocu.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMDBHLA2))
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.edCodart = New NTSInformatica.NTSTextBoxStr
    Me.lbXx_Codart = New NTSInformatica.NTSLabel
    Me.lbCodart = New NTSInformatica.NTSLabel
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.grHla2 = New NTSInformatica.NTSGrid
    Me.grvHla2 = New NTSInformatica.NTSGridView
    Me.ar_codart = New NTSInformatica.NTSGridColumn
    Me.ar_descr = New NTSInformatica.NTSGridColumn
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.grHla2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvHla2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Image = CType(resources.GetObject("frmPopup.Appearance.Image"), System.Drawing.Image)
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
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.edCodart)
    Me.pnTop.Controls.Add(Me.lbXx_Codart)
    Me.pnTop.Controls.Add(Me.lbCodart)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(410, 40)
    Me.pnTop.TabIndex = 4
    Me.pnTop.Text = "NtsPanel1"
    '
    'edCodart
    '
    Me.edCodart.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodart.Location = New System.Drawing.Point(69, 10)
    Me.edCodart.Name = "edCodart"
    Me.edCodart.NTSDbField = ""
    Me.edCodart.NTSForzaVisZoom = False
    Me.edCodart.NTSOldValue = ""
    Me.edCodart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodart.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodart.Properties.MaxLength = 65536
    Me.edCodart.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodart.Size = New System.Drawing.Size(138, 20)
    Me.edCodart.TabIndex = 676
    '
    'lbXx_Codart
    '
    Me.lbXx_Codart.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_Codart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_Codart.Location = New System.Drawing.Point(213, 10)
    Me.lbXx_Codart.Name = "lbXx_Codart"
    Me.lbXx_Codart.NTSDbField = ""
    Me.lbXx_Codart.Size = New System.Drawing.Size(182, 20)
    Me.lbXx_Codart.TabIndex = 669
    Me.lbXx_Codart.Tooltip = ""
    Me.lbXx_Codart.UseMnemonic = False
    '
    'lbCodart
    '
    Me.lbCodart.AutoSize = True
    Me.lbCodart.BackColor = System.Drawing.Color.Transparent
    Me.lbCodart.Location = New System.Drawing.Point(3, 13)
    Me.lbCodart.Name = "lbCodart"
    Me.lbCodart.NTSDbField = ""
    Me.lbCodart.Size = New System.Drawing.Size(60, 13)
    Me.lbCodart.TabIndex = 667
    Me.lbCodart.Text = " Cod/Descr"
    Me.lbCodart.Tooltip = ""
    Me.lbCodart.UseMnemonic = False
    '
    'cmdRicerca
    '
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(242, 15)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.Size = New System.Drawing.Size(113, 24)
    Me.cmdRicerca.TabIndex = 1
    Me.cmdRicerca.Text = "&Ricerca"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(4, 15)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(113, 24)
    Me.cmdAnnulla.TabIndex = 0
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.cmdSeleziona)
    Me.pnBottom.Controls.Add(Me.cmdAnnulla)
    Me.pnBottom.Controls.Add(Me.cmdRicerca)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 216)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.Size = New System.Drawing.Size(410, 47)
    Me.pnBottom.TabIndex = 6
    Me.pnBottom.Text = "NtsPanel1"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(123, 15)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(113, 24)
    Me.cmdSeleziona.TabIndex = 2
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'grHla2
    '
    Me.grHla2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grHla2.EmbeddedNavigator.Name = ""
    Me.grHla2.Location = New System.Drawing.Point(0, 40)
    Me.grHla2.MainView = Me.grvHla2
    Me.grHla2.Name = "grHla2"
    Me.grHla2.Size = New System.Drawing.Size(410, 176)
    Me.grHla2.TabIndex = 7
    Me.grHla2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHla2})
    '
    'grvHla2
    '
    Me.grvHla2.ActiveFilterEnabled = False
    Me.grvHla2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ar_codart, Me.ar_descr})
    Me.grvHla2.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvHla2.Enabled = True
    Me.grvHla2.GridControl = Me.grHla2
    Me.grvHla2.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvHla2.Name = "grvHla2"
    Me.grvHla2.NTSAllowDelete = True
    Me.grvHla2.NTSAllowInsert = True
    Me.grvHla2.NTSAllowUpdate = True
    Me.grvHla2.NTSMenuContext = Nothing
    Me.grvHla2.OptionsCustomization.AllowRowSizing = True
    Me.grvHla2.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvHla2.OptionsNavigation.UseTabKey = False
    Me.grvHla2.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvHla2.OptionsView.ColumnAutoWidth = False
    Me.grvHla2.OptionsView.EnableAppearanceEvenRow = True
    Me.grvHla2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvHla2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvHla2.OptionsView.ShowGroupPanel = False
    Me.grvHla2.RowHeight = 16
    '
    'ar_codart
    '
    Me.ar_codart.AppearanceCell.Options.UseBackColor = True
    Me.ar_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codart.Caption = "Articolo"
    Me.ar_codart.Enabled = True
    Me.ar_codart.FieldName = "ar_codart"
    Me.ar_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codart.Name = "ar_codart"
    Me.ar_codart.NTSRepositoryComboBox = Nothing
    Me.ar_codart.NTSRepositoryItemCheck = Nothing
    Me.ar_codart.NTSRepositoryItemMemo = Nothing
    Me.ar_codart.NTSRepositoryItemText = Nothing
    Me.ar_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codart.OptionsFilter.AllowFilter = False
    Me.ar_codart.Visible = True
    Me.ar_codart.VisibleIndex = 0
    '
    'ar_descr
    '
    Me.ar_descr.AppearanceCell.Options.UseBackColor = True
    Me.ar_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ar_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_descr.Caption = "Descrizione"
    Me.ar_descr.Enabled = True
    Me.ar_descr.FieldName = "ar_descr"
    Me.ar_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_descr.Name = "ar_descr"
    Me.ar_descr.NTSRepositoryComboBox = Nothing
    Me.ar_descr.NTSRepositoryItemCheck = Nothing
    Me.ar_descr.NTSRepositoryItemMemo = Nothing
    Me.ar_descr.NTSRepositoryItemText = Nothing
    Me.ar_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_descr.OptionsFilter.AllowFilter = False
    Me.ar_descr.Visible = True
    Me.ar_descr.VisibleIndex = 1
    '
    'FRMDBHLA2
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(410, 263)
    Me.Controls.Add(Me.grHla2)
    Me.Controls.Add(Me.pnBottom)
    Me.Controls.Add(Me.pnTop)
    Me.HelpContext = ""
    Me.Name = "FRMDBHLA2"
    Me.Text = "ZOOM ARTICOLI"
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edCodart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    CType(Me.grHla2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvHla2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      grvHla2.NTSSetParam(oMenu, oApp.Tr(Me, 128828969839942259, "Lotti ubicazioni"))



      edCodart.Enabled = False
      lbXx_Codart.Enabled = False

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
  Public Overridable Sub InitEntity(ByRef cleDocu As CLEMGDOCU)
    oCleDocu = cleDocu
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub Apri()
    Try
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleDocu.Hla2Apri(DittaCorrente, edCodart.Text, lbXx_Codart.Text, dsHla2) Then Me.Close()

      dcHla2.DataSource = Nothing
      dcHla2.DataSource = dsHla2.Tables("ARTICO")
      grHla2.DataSource = dcHla2

      grvHla2.NTSAllowInsert = False
      grvHla2.NTSAllowUpdate = False
      grvHla2.NTSAllowDelete = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRMDBHLA2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMDBHLA2_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcHla2.Dispose()
      dsHla2.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleDocu.strCodart = ""
      Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Try
      Apri()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Try
      If grvHla2.NTSGetCurrentDataRow() Is Nothing Then Return
      oCleDocu.strCodart = NTSCStr(grvHla2.NTSGetCurrentDataRow()!ar_codart)
      Me.Close()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class