Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO

Public Class FRM__HELO

#Region "Variabili"

  Private components As System.ComponentModel.IContainer
  Public oPar As CLE__CLDP
  Public WithEvents cmdProcedi As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents pnHelo As NTSInformatica.NTSPanel
  Public WithEvents edCerca As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbCerca As System.Windows.Forms.Label
  Public WithEvents grOpzi As NTSInformatica.NTSGrid
  Public WithEvents grvOpzi As NTSInformatica.NTSGridView
  Public WithEvents xx_name As NTSInformatica.NTSGridColumn
  Public WithEvents xx_opzione As NTSInformatica.NTSGridColumn
  Public WithEvents xx_testo As NTSInformatica.NTSGridColumn
  Public dcOpzi As BindingSource = New BindingSource()


#End Region

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    Try
      oMenu = Menu
      oApp = oMenu.App
      If Ditta <> "" Then
        DittaCorrente = Ditta
      Else
        DittaCorrente = oApp.Ditta
      End If

      InitializeComponent()
      Me.MinimumSize = Me.Size

      oPar = Param

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub InitializeComponent()
    Me.cmdProcedi = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnHelo = New NTSInformatica.NTSPanel
    Me.grOpzi = New NTSInformatica.NTSGrid
    Me.grvOpzi = New NTSInformatica.NTSGridView
    Me.xx_name = New NTSInformatica.NTSGridColumn
    Me.xx_opzione = New NTSInformatica.NTSGridColumn
    Me.xx_testo = New NTSInformatica.NTSGridColumn
    Me.lbCerca = New System.Windows.Forms.Label
    Me.edCerca = New NTSInformatica.NTSTextBoxStr
    CType(Me.pnHelo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnHelo.SuspendLayout()
    CType(Me.grOpzi, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvOpzi, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCerca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'cmdProcedi
    '
    Me.cmdProcedi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdProcedi.ImageText = ""
    Me.cmdProcedi.Location = New System.Drawing.Point(524, 406)
    Me.cmdProcedi.Name = "cmdProcedi"
    Me.cmdProcedi.Size = New System.Drawing.Size(66, 23)
    Me.cmdProcedi.TabIndex = 1
    Me.cmdProcedi.Text = "&Procedi"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(596, 406)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(66, 23)
    Me.cmdAnnulla.TabIndex = 2
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnHelo
    '
    Me.pnHelo.AllowDrop = True
    Me.pnHelo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnHelo.Appearance.Options.UseBackColor = True
    Me.pnHelo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnHelo.Controls.Add(Me.grOpzi)
    Me.pnHelo.Controls.Add(Me.lbCerca)
    Me.pnHelo.Controls.Add(Me.edCerca)
    Me.pnHelo.Controls.Add(Me.cmdAnnulla)
    Me.pnHelo.Controls.Add(Me.cmdProcedi)
    Me.pnHelo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnHelo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnHelo.Location = New System.Drawing.Point(0, 0)
    Me.pnHelo.Name = "pnHelo"
    Me.pnHelo.NTSActiveTrasparency = True
    Me.pnHelo.Size = New System.Drawing.Size(674, 441)
    Me.pnHelo.TabIndex = 21
    Me.pnHelo.Text = "NtsPanel1"
    '
    'grOpzi
    '
    Me.grOpzi.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grOpzi.EmbeddedNavigator.Name = ""
    Me.grOpzi.Location = New System.Drawing.Point(0, 0)
    Me.grOpzi.MainView = Me.grvOpzi
    Me.grOpzi.Name = "grOpzi"
    Me.grOpzi.Size = New System.Drawing.Size(674, 400)
    Me.grOpzi.TabIndex = 18
    Me.grOpzi.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvOpzi})
    '
    'grvOpzi
    '
    Me.grvOpzi.ActiveFilterEnabled = False
    Me.grvOpzi.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_name, Me.xx_opzione, Me.xx_testo})
    Me.grvOpzi.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvOpzi.Enabled = True
    Me.grvOpzi.GridControl = Me.grOpzi
    Me.grvOpzi.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvOpzi.GroupPanelText = "Per raggruppare: Tasto DX sulla colonna -> Group by this column *** Per totali pa" & _
        "rziali/generali: sul piede di gruppo della colonna tasto DX -> SUM "
    Me.grvOpzi.MinRowHeight = 14
    Me.grvOpzi.Name = "grvOpzi"
    Me.grvOpzi.NTSAllowDelete = False
    Me.grvOpzi.NTSAllowInsert = False
    Me.grvOpzi.NTSAllowUpdate = False
    Me.grvOpzi.NTSMenuContext = Nothing
    Me.grvOpzi.OptionsCustomization.AllowRowSizing = True
    Me.grvOpzi.OptionsFilter.AllowFilterEditor = False
    Me.grvOpzi.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvOpzi.OptionsNavigation.UseTabKey = False
    Me.grvOpzi.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvOpzi.OptionsView.ColumnAutoWidth = False
    Me.grvOpzi.OptionsView.EnableAppearanceEvenRow = True
    Me.grvOpzi.OptionsView.ShowGroupPanel = False
    Me.grvOpzi.RowHeight = 16
    '
    'xx_name
    '
    Me.xx_name.AppearanceCell.Options.UseBackColor = True
    Me.xx_name.AppearanceCell.Options.UseTextOptions = True
    Me.xx_name.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_name.Caption = "Name"
    Me.xx_name.Enabled = True
    Me.xx_name.FieldName = "xx_name"
    Me.xx_name.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_name.Name = "xx_name"
    Me.xx_name.NTSRepositoryComboBox = Nothing
    Me.xx_name.NTSRepositoryItemCheck = Nothing
    Me.xx_name.NTSRepositoryItemMemo = Nothing
    Me.xx_name.NTSRepositoryItemText = Nothing
    Me.xx_name.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_name.Width = 82
    '
    'xx_opzione
    '
    Me.xx_opzione.AppearanceCell.Options.UseBackColor = True
    Me.xx_opzione.AppearanceCell.Options.UseTextOptions = True
    Me.xx_opzione.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_opzione.Caption = "Opzione"
    Me.xx_opzione.Enabled = False
    Me.xx_opzione.FieldName = "xx_opzione"
    Me.xx_opzione.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_opzione.Name = "xx_opzione"
    Me.xx_opzione.NTSRepositoryComboBox = Nothing
    Me.xx_opzione.NTSRepositoryItemCheck = Nothing
    Me.xx_opzione.NTSRepositoryItemMemo = Nothing
    Me.xx_opzione.NTSRepositoryItemText = Nothing
    Me.xx_opzione.OptionsColumn.AllowEdit = False
    Me.xx_opzione.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_opzione.OptionsColumn.ReadOnly = True
    Me.xx_opzione.Visible = True
    Me.xx_opzione.VisibleIndex = 0
    Me.xx_opzione.Width = 247
    '
    'xx_testo
    '
    Me.xx_testo.AppearanceCell.Options.UseBackColor = True
    Me.xx_testo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_testo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_testo.Caption = "Testo"
    Me.xx_testo.Enabled = False
    Me.xx_testo.FieldName = "xx_testo"
    Me.xx_testo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_testo.Name = "xx_testo"
    Me.xx_testo.NTSRepositoryComboBox = Nothing
    Me.xx_testo.NTSRepositoryItemCheck = Nothing
    Me.xx_testo.NTSRepositoryItemMemo = Nothing
    Me.xx_testo.NTSRepositoryItemText = Nothing
    Me.xx_testo.OptionsColumn.AllowEdit = False
    Me.xx_testo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_testo.OptionsColumn.ReadOnly = True
    Me.xx_testo.Visible = True
    Me.xx_testo.VisibleIndex = 1
    Me.xx_testo.Width = 480
    '
    'lbCerca
    '
    Me.lbCerca.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbCerca.AutoSize = True
    Me.lbCerca.Location = New System.Drawing.Point(12, 411)
    Me.lbCerca.Name = "lbCerca"
    Me.lbCerca.Size = New System.Drawing.Size(35, 13)
    Me.lbCerca.TabIndex = 17
    Me.lbCerca.Text = "Cerca"
    '
    'edCerca
    '
    Me.edCerca.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edCerca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCerca.Location = New System.Drawing.Point(53, 408)
    Me.edCerca.Name = "edCerca"
    Me.edCerca.NTSDbField = ""
    Me.edCerca.NTSForzaVisZoom = False
    Me.edCerca.NTSOldValue = ""
    Me.edCerca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCerca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCerca.Properties.MaxLength = 65536
    Me.edCerca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCerca.Size = New System.Drawing.Size(465, 20)
    Me.edCerca.TabIndex = 10
    '
    'FRM__HELO
    '
    Me.AcceptButton = Me.cmdProcedi
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(674, 441)
    Me.Controls.Add(Me.pnHelo)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__HELO"
    Me.Text = "INSERISCI OPZIONE"
    CType(Me.pnHelo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnHelo.ResumeLayout(False)
    Me.pnHelo.PerformLayout()
    CType(Me.grOpzi, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvOpzi, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCerca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Form"
  Public Overridable Sub InitControls()

    Try


      'esempio per colonna combo
      'Dim dttTipo As New DataTable()
      'dttTipo.Columns.Add("cod", GetType(String))
      'dttTipo.Columns.Add("val", GetType(String))
      'dttTipo.Rows.Add(New Object() {" ", "Propria"})
      'dttTipo.Rows.Add(New Object() {"C", "Altrui"})
      'dttTipo.AcceptChanges()

      grvOpzi.NTSSetParam(oMenu, "OPZIONI")
      xx_name.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130081727027475047, "Name"), 0)
      xx_opzione.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130081727027485048, "Opzione"), 0)
      xx_testo.NTSSetParamSTR(oMenu, oApp.Tr(Me, 130081727027495052, "Testo"), 0)

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HELO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      InitControls()

      CaricaOpzioni()

      dcOpzi.DataSource = dttXmlOpzio
      grOpzi.DataSource = dcOpzi



      GctlSetRoules()
      GctlApplicaDefaultValue()

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)

    End Try

  End Sub

#End Region

  Public Overridable Sub cmdProcedi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcedi.Click
    Try

      If Not grvOpzi.NTSGetCurrentDataRow Is Nothing Then
        oPar.strPar1 = "<A href=""ampers07.htm#" & grvOpzi.NTSGetCurrentDataRow!xx_name.ToString & _
        """><SPAN title=""" & grvOpzi.NTSGetCurrentDataRow!xx_opzione.ToString & grvOpzi.NTSGetCurrentDataRow!xx_testo.ToString & """ style=""BACKGROUND: #e9edf4; COLOR: #000000"" opzione=""1"">" & grvOpzi.NTSGetCurrentDataRow!xx_opzione.ToString & "</SPAN></A>"
        oPar.bPar1 = True
        Me.Close()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oPar.bPar1 = False
    Me.Close()
  End Sub


  Public dttXmlOpzio As DataTable
  Public Overridable Sub CaricaOpzioni()


    Try

      dttXmlOpzio = New DataTable
      dttXmlOpzio.Columns.Add("xx_name", GetType(System.String))
      dttXmlOpzio.Columns.Add("xx_opzione", GetType(System.String))
      dttXmlOpzio.Columns.Add("xx_testo", GetType(System.String))
      dttXmlOpzio.TableName = "OPZIONI"

      If System.IO.File.Exists("\\s01\Utenti\Filippucci\guida\Script\opzioni.xml") Then
        dttXmlOpzio.ReadXml("\\s01\Utenti\Filippucci\guida\Script\opzioni.xml")
      End If

      dttXmlOpzio.AcceptChanges()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edCerca_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCerca.EditValueChanged
    Try
      Dim dttXmlOpzio2 As New DataTable
      dttXmlOpzio2 = New DataTable
      dttXmlOpzio2.Columns.Add("xx_name", GetType(System.String))
      dttXmlOpzio2.Columns.Add("xx_opzione", GetType(System.String))
      dttXmlOpzio2.Columns.Add("xx_testo", GetType(System.String))
      dttXmlOpzio2.TableName = "OPZIONI"

      For Each dtrtmp As DataRow In dttXmlOpzio.Select("xx_opzione like " & CStrSQL("%" & edCerca.Text & "%"))
        dttXmlOpzio2.Rows.Add(New Object() {dtrtmp!xx_name, dtrtmp!xx_opzione, dtrtmp!xx_testo})
      Next

      dttXmlOpzio2.AcceptChanges()

      grOpzi.DataSource = Nothing
      dcOpzi.DataSource = dttXmlOpzio2
      grOpzi.DataSource = dcOpzi

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub


  Public Overridable Sub grOpzi_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grOpzi.DoubleClick
    Try
      cmdProcedi_Click(Nothing, Nothing)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
End Class
