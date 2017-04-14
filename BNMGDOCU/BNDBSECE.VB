Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMDBSECE

#Region "Variabili"
  Public oCleDocu As CLEMGDOCU
  Public oCallParams As CLE__CLDP
  Public dsSece As New DataSet
  Public dcSece As New BindingSource()
  Public dttSece As DataTable = Nothing
  Public dtrSece() As DataRow = Nothing
  Public components As System.ComponentModel.IContainer

  Public WithEvents grSece As NTSInformatica.NTSGrid
  Public WithEvents grvSece As NTSInformatica.NTSGridView

  Public WithEvents pnSece As NTSInformatica.NTSPanel
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents tb_codcent As NTSInformatica.NTSGridColumn
  Public WithEvents tb_descent As NTSInformatica.NTSGridColumn
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
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
      oApp.MsgBoxErr(oApp.Tr(Me, 130086828230054425, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGDOCU", strRemoteServer, strRemotePort)
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleDocu.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    oCleDocu.lSeceIITTTabcent = NTSCInt(oCallParams.dPar1)
    oCleDocu.nSeceCodcent = NTSCInt(oCallParams.dPar2)
    oCleDocu.bSeceAnnullato = oCallParams.bPar1

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMDBSECE))
    Me.grSece = New NTSInformatica.NTSGrid
    Me.grvSece = New NTSInformatica.NTSGridView
    Me.tb_codcent = New NTSInformatica.NTSGridColumn
    Me.tb_descent = New NTSInformatica.NTSGridColumn
    Me.pnSece = New NTSInformatica.NTSPanel
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    CType(Me.grSece, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvSece, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSece, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSece.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Image = CType(resources.GetObject("frmPopup.Appearance.Image"), System.Drawing.Image)
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    Me.frmPopup.ClientSize = New System.Drawing.Size(77, 21)
    '
    'grSece
    '
    Me.grSece.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grSece.EmbeddedNavigator.Name = ""
    Me.grSece.Location = New System.Drawing.Point(0, 0)
    Me.grSece.MainView = Me.grvSece
    Me.grSece.Name = "grSece"
    Me.grSece.Size = New System.Drawing.Size(326, 366)
    Me.grSece.TabIndex = 0
    Me.grSece.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvSece})
    '
    'grvSece
    '
    Me.grvSece.ActiveFilterEnabled = False
    '
    'tb_codcent
    '
    Me.tb_codcent.AppearanceCell.Options.UseBackColor = True
    Me.tb_codcent.AppearanceCell.Options.UseTextOptions = True
    Me.tb_codcent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_codcent.Caption = "Centro"
    Me.tb_codcent.Enabled = True
    Me.tb_codcent.FieldName = "tb_codcent"
    Me.tb_codcent.Name = "tb_codcent"
    Me.tb_codcent.NTSRepositoryComboBox = Nothing
    Me.tb_codcent.NTSRepositoryItemCheck = Nothing
    Me.tb_codcent.NTSRepositoryItemMemo = Nothing
    Me.tb_codcent.NTSRepositoryItemText = Nothing
    Me.tb_codcent.Visible = True
    Me.tb_codcent.VisibleIndex = 0
    '
    'tb_descent
    '
    Me.tb_descent.AppearanceCell.Options.UseBackColor = True
    Me.tb_descent.AppearanceCell.Options.UseTextOptions = True
    Me.tb_descent.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tb_descent.Caption = "Descrizione"
    Me.tb_descent.Enabled = True
    Me.tb_descent.FieldName = "tb_descent"
    Me.tb_descent.Name = "tb_descent"
    Me.tb_descent.NTSRepositoryComboBox = Nothing
    Me.tb_descent.NTSRepositoryItemCheck = Nothing
    Me.tb_descent.NTSRepositoryItemMemo = Nothing
    Me.tb_descent.NTSRepositoryItemText = Nothing
    Me.tb_descent.Visible = True
    Me.tb_descent.VisibleIndex = 1
    Me.grvSece.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codcent, Me.tb_descent})
    Me.grvSece.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvSece.Enabled = True
    Me.grvSece.GridControl = Me.grSece
    Me.grvSece.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvSece.Name = "grvSece"
    Me.grvSece.NTSAllowDelete = True
    Me.grvSece.NTSAllowInsert = True
    Me.grvSece.NTSAllowUpdate = True
    Me.grvSece.NTSMenuContext = Nothing
    Me.grvSece.OptionsCustomization.AllowRowSizing = True
    Me.grvSece.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvSece.OptionsNavigation.UseTabKey = False
    Me.grvSece.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvSece.OptionsView.ColumnAutoWidth = False
    Me.grvSece.OptionsView.EnableAppearanceEvenRow = True
    Me.grvSece.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvSece.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvSece.OptionsView.ShowGroupPanel = False
    Me.grvSece.RowHeight = 16
    '
    'pnSece
    '
    Me.pnSece.AllowDrop = True
    Me.pnSece.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSece.Appearance.Options.UseBackColor = True
    Me.pnSece.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSece.Controls.Add(Me.grSece)
    Me.pnSece.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnSece.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnSece.Location = New System.Drawing.Point(0, 0)
    Me.pnSece.Name = "pnSece"
    Me.pnSece.Size = New System.Drawing.Size(326, 366)
    Me.pnSece.TabIndex = 1
    Me.pnSece.Text = "pnSece"
    '
    'cmdOk
    '
    Me.cmdOk.Location = New System.Drawing.Point(332, 12)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(86, 27)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "Seleziona"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(332, 45)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(86, 27)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'FRMDBSECE
    '
    Me.ClientSize = New System.Drawing.Size(430, 366)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.Controls.Add(Me.pnSece)
    Me.HelpContext = ""
    Me.Name = "FRMDBSECE"
    Me.NTSLastControlFocussed = Me.grSece
    Me.Text = "SELEZIONE CENTRO DA VERSARE"
    CType(Me.grSece, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvSece, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSece, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSece.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      grvSece.NTSSetParam(oMenu, oApp.Tr(Me, 128883417016704182, "Centro"))
      tb_codcent.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128883417016860432, "Centro"), "0", 4, 0, 9999)
      tb_descent.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128883417017016682, "Descrizione"), 40, True)
      grvSece.Enabled = False

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

  Public Overridable Sub Apri()
    Try
      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleDocu.SeceApri(DittaCorrente, dsSece) Then Me.Close()

      dcSece.DataSource = Nothing
      dcSece.DataSource = dsSece.Tables("TTTABCENT")
      grSece.DataSource = dcSece

      grvSece.NTSAllowInsert = False
      grvSece.NTSAllowUpdate = False
      grvSece.NTSAllowDelete = False

      oCleDocu.bSeceAnnullato = True
      oCleDocu.nSeceCodcent = 0

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
  Public Overridable Sub FRMDBSECE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Apri()

    Catch ex As Exception
      Me.Cursor = Cursors.Default
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMDBSECE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcSece.Dispose()
      dsSece.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      If grvSece.NTSGetCurrentDataRow() Is Nothing Then Return
      oCleDocu.bSeceAnnullato = False
      oCleDocu.nSeceCodcent = NTSCInt(grvSece.NTSGetCurrentDataRow()!tb_codcent)

      oCallParams.dPar2 = NTSCDec(oCleDocu.nSeceCodcent)
      oCallParams.bPar1 = oCleDocu.bSeceAnnullato
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleDocu.bSeceAnnullato = True
      oCleDocu.nSeceCodcent = 0
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class