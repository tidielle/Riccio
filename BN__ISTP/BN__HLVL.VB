Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HLVL
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
  Public dsHlvl As DataSet
  Public oCleIstp As CLE__ISTP
  Public oCallParams As CLE__CLDP
  Public dcHlvl As BindingSource = New BindingSource

  Public WithEvents pnHlvl As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents grHlvl As NTSInformatica.NTSGrid
  Public WithEvents grvHlvl As NTSInformatica.NTSGridView
  Public WithEvents xx_valori As NTSInformatica.NTSGridColumn

  Private components As System.ComponentModel.IContainer
#End Region

  Public Overridable Sub InitializeComponent()
    Me.pnHlvl = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.grHlvl = New NTSInformatica.NTSGrid
    Me.grvHlvl = New NTSInformatica.NTSGridView
    Me.xx_valori = New NTSInformatica.NTSGridColumn
    CType(Me.pnHlvl, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnHlvl.SuspendLayout()
    CType(Me.grHlvl, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvHlvl, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'pnHlvl
    '
    Me.pnHlvl.AllowDrop = True
    Me.pnHlvl.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnHlvl.Appearance.Options.UseBackColor = True
    Me.pnHlvl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnHlvl.Controls.Add(Me.cmdAnnulla)
    Me.pnHlvl.Controls.Add(Me.cmdOk)
    Me.pnHlvl.Controls.Add(Me.grHlvl)
    Me.pnHlvl.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnHlvl.Location = New System.Drawing.Point(-1, 0)
    Me.pnHlvl.Name = "pnHlvl"
    Me.pnHlvl.Size = New System.Drawing.Size(382, 282)
    Me.pnHlvl.TabIndex = 16
    Me.pnHlvl.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(299, 41)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(71, 23)
    Me.cmdAnnulla.TabIndex = 18
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.Location = New System.Drawing.Point(299, 12)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(71, 23)
    Me.cmdOk.TabIndex = 17
    Me.cmdOk.Text = "&Conferma"
    '
    'grHlvl
    '
    '
    '
    '
    Me.grHlvl.EmbeddedNavigator.Name = ""
    Me.grHlvl.Location = New System.Drawing.Point(13, 12)
    Me.grHlvl.MainView = Me.grvHlvl
    Me.grHlvl.Name = "grHlvl"
    Me.grHlvl.Size = New System.Drawing.Size(274, 259)
    Me.grHlvl.TabIndex = 16
    Me.grHlvl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHlvl})
    '
    'grvHlvl
    '
    Me.grvHlvl.ActiveFilterEnabled = False
    Me.grvHlvl.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_valori})
    Me.grvHlvl.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvHlvl.Enabled = True
    Me.grvHlvl.GridControl = Me.grHlvl
    Me.grvHlvl.Name = "grvHlvl"
    Me.grvHlvl.NTSAllowDelete = True
    Me.grvHlvl.NTSAllowInsert = False
    Me.grvHlvl.NTSAllowUpdate = True
    Me.grvHlvl.NTSMenuContext = Nothing
    Me.grvHlvl.OptionsCustomization.AllowRowSizing = True
    Me.grvHlvl.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvHlvl.OptionsNavigation.UseTabKey = False
    Me.grvHlvl.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvHlvl.OptionsView.ColumnAutoWidth = False
    Me.grvHlvl.OptionsView.EnableAppearanceEvenRow = True
    Me.grvHlvl.OptionsView.ShowGroupPanel = False
    Me.grvHlvl.RowHeight = 14
    '
    'xx_valori
    '
    Me.xx_valori.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
    Me.xx_valori.AppearanceCell.Options.UseBackColor = True
    Me.xx_valori.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valori.Caption = "Possibili valori"
    Me.xx_valori.Enabled = False
    Me.xx_valori.FieldName = "xx_valori"
    Me.xx_valori.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valori.Name = "xx_valori"
    Me.xx_valori.NTSRepositoryComboBox = Nothing
    Me.xx_valori.NTSRepositoryItemCheck = Nothing
    Me.xx_valori.NTSRepositoryItemText = Nothing
    Me.xx_valori.OptionsColumn.AllowEdit = False
    Me.xx_valori.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_valori.OptionsColumn.ReadOnly = True
    Me.xx_valori.Visible = True
    Me.xx_valori.VisibleIndex = 0
    '
    'FRM__HLVL
    '
    Me.ClientSize = New System.Drawing.Size(381, 281)
    Me.Controls.Add(Me.pnHlvl)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRM__HLVL"
    Me.NTSLastControlFocussed = Me.grHlvl
    Me.Text = "Valori possibili dei campi"
    CType(Me.pnHlvl, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnHlvl.ResumeLayout(False)
    CType(Me.grHlvl, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvHlvl, System.ComponentModel.ISupportInitialize).EndInit()
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
      Catch ex As Exception
        '  'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      grvHlvl.NTSSetParam(oMenu, oApp.Tr(Me, 128898039853909541, "Valori possibili dei campi"))
      xx_valori.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128311437002944026, "Possibili valori"), 0, True)

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
  Public Overridable Sub InitEntity(ByRef cleIstp As CLE__ISTP)
    oCleIstp = cleIstp
    AddHandler oCleIstp.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRM__HLVL_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleIstp.HlvlApri(DittaCorrente, dsHlvl) Then Me.Close()

      dcHlvl.DataSource = dsHlvl.Tables("TAB")
      grHlvl.DataSource = dcHlvl

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
  Public Overridable Sub FRMCGMOUS_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcHlvl.Dispose()
      dsHlvl.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      cmdOkClick()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleIstp.strHlvlValore = ""
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvHlvl_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvHlvl.DoubleClick
    Try
      cmdOkClick()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdOkClick()
    Try
      oCleIstp.strHlvlValore = grvHlvl.NTSGetCurrentDataRow!xx_valori.ToString
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class