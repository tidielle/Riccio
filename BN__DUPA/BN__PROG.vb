#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRM__PROG

#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = 0
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
#End Region

#Region "Variabili"
  Public oCleDupa As CLE__DUPA
  Public oCallParams As CLE__CLDP
  Public strTabella As String = "AZIENDE"
  Public oSql7 As Object
  Public nRis As Integer
  Public strOperazione As String

  Public strEdAdoconnectGlob As String
  Public dttAziendeGlob As DataTable
  Public strDirNewDbGlob As String
  Public strEdCodazGlob As String
  Public strDirNewDbLogGlob As String
  Public strEdExtGlob As String
  Public bUnicode As Boolean = False
#End Region

#Region "Variaribli Componenti Form e InitializeComponent"
  Private components As System.ComponentModel.IContainer
  Public WithEvents als_mitt As NTSInformatica.NTSGridColumn
  Public WithEvents als_tipop As NTSInformatica.NTSGridColumn
  Public WithEvents als_tipom As NTSInformatica.NTSGridColumn
  Public WithEvents als_tipoc As NTSInformatica.NTSGridColumn
  Public WithEvents als_tipoe As NTSInformatica.NTSGridColumn
  Public WithEvents pnProg As NTSInformatica.NTSPanel
  Public WithEvents lbStep As NTSInformatica.NTSLabel
  Public WithEvents lbAction As NTSInformatica.NTSLabel
  Public WithEvents pbProgProgressBar As System.Windows.Forms.ProgressBar
  Public WithEvents tmProgTimer As System.Windows.Forms.Timer

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Me.als_mitt = New NTSInformatica.NTSGridColumn
    Me.als_tipop = New NTSInformatica.NTSGridColumn
    Me.als_tipom = New NTSInformatica.NTSGridColumn
    Me.als_tipoc = New NTSInformatica.NTSGridColumn
    Me.als_tipoe = New NTSInformatica.NTSGridColumn
    Me.pnProg = New NTSInformatica.NTSPanel
    Me.pbProgProgressBar = New System.Windows.Forms.ProgressBar
    Me.lbAction = New NTSInformatica.NTSLabel
    Me.lbStep = New NTSInformatica.NTSLabel
    Me.tmProgTimer = New System.Windows.Forms.Timer(Me.components)
    CType(Me.pnProg, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnProg.SuspendLayout()
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
    'als_mitt
    '
    Me.als_mitt.AppearanceCell.Options.UseBackColor = True
    Me.als_mitt.AppearanceCell.Options.UseTextOptions = True
    Me.als_mitt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.als_mitt.Caption = "Operatore"
    Me.als_mitt.Enabled = True
    Me.als_mitt.FieldName = "als_mitt"
    Me.als_mitt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.als_mitt.Name = "als_mitt"
    Me.als_mitt.NTSRepositoryComboBox = Nothing
    Me.als_mitt.NTSRepositoryItemCheck = Nothing
    Me.als_mitt.NTSRepositoryItemMemo = Nothing
    Me.als_mitt.NTSRepositoryItemText = Nothing
    Me.als_mitt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.als_mitt.OptionsFilter.AllowFilter = False
    Me.als_mitt.Visible = True
    Me.als_mitt.VisibleIndex = 0
    Me.als_mitt.Width = 62
    '
    'als_tipop
    '
    Me.als_tipop.AppearanceCell.Options.UseBackColor = True
    Me.als_tipop.AppearanceCell.Options.UseTextOptions = True
    Me.als_tipop.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.als_tipop.Caption = "Tipo alert popup"
    Me.als_tipop.Enabled = True
    Me.als_tipop.FieldName = "als_tipop"
    Me.als_tipop.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.als_tipop.Name = "als_tipop"
    Me.als_tipop.NTSRepositoryComboBox = Nothing
    Me.als_tipop.NTSRepositoryItemCheck = Nothing
    Me.als_tipop.NTSRepositoryItemMemo = Nothing
    Me.als_tipop.NTSRepositoryItemText = Nothing
    Me.als_tipop.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.als_tipop.OptionsFilter.AllowFilter = False
    Me.als_tipop.Visible = True
    Me.als_tipop.VisibleIndex = 1
    Me.als_tipop.Width = 90
    '
    'als_tipom
    '
    Me.als_tipom.AppearanceCell.Options.UseBackColor = True
    Me.als_tipom.AppearanceCell.Options.UseTextOptions = True
    Me.als_tipom.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.als_tipom.Caption = "Tipo alert e-mail"
    Me.als_tipom.Enabled = True
    Me.als_tipom.FieldName = "als_tipom"
    Me.als_tipom.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.als_tipom.Name = "als_tipom"
    Me.als_tipom.NTSRepositoryComboBox = Nothing
    Me.als_tipom.NTSRepositoryItemCheck = Nothing
    Me.als_tipom.NTSRepositoryItemMemo = Nothing
    Me.als_tipom.NTSRepositoryItemText = Nothing
    Me.als_tipom.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.als_tipom.OptionsFilter.AllowFilter = False
    Me.als_tipom.Visible = True
    Me.als_tipom.VisibleIndex = 2
    Me.als_tipom.Width = 88
    '
    'als_tipoc
    '
    Me.als_tipoc.AppearanceCell.Options.UseBackColor = True
    Me.als_tipoc.AppearanceCell.Options.UseTextOptions = True
    Me.als_tipoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.als_tipoc.Caption = "Tipo alert attività C.R.M."
    Me.als_tipoc.Enabled = True
    Me.als_tipoc.FieldName = "als_tipoc"
    Me.als_tipoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.als_tipoc.Name = "als_tipoc"
    Me.als_tipoc.NTSRepositoryComboBox = Nothing
    Me.als_tipoc.NTSRepositoryItemCheck = Nothing
    Me.als_tipoc.NTSRepositoryItemMemo = Nothing
    Me.als_tipoc.NTSRepositoryItemText = Nothing
    Me.als_tipoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.als_tipoc.OptionsFilter.AllowFilter = False
    Me.als_tipoc.Visible = True
    Me.als_tipoc.VisibleIndex = 3
    Me.als_tipoc.Width = 131
    '
    'als_tipoe
    '
    Me.als_tipoe.AppearanceCell.Options.UseBackColor = True
    Me.als_tipoe.AppearanceCell.Options.UseTextOptions = True
    Me.als_tipoe.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.als_tipoe.Caption = "Tipo alert procedura da eseguire"
    Me.als_tipoe.Enabled = True
    Me.als_tipoe.FieldName = "als_tipoe"
    Me.als_tipoe.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.als_tipoe.Name = "als_tipoe"
    Me.als_tipoe.NTSRepositoryComboBox = Nothing
    Me.als_tipoe.NTSRepositoryItemCheck = Nothing
    Me.als_tipoe.NTSRepositoryItemMemo = Nothing
    Me.als_tipoe.NTSRepositoryItemText = Nothing
    Me.als_tipoe.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.als_tipoe.OptionsFilter.AllowFilter = False
    Me.als_tipoe.Visible = True
    Me.als_tipoe.VisibleIndex = 4
    Me.als_tipoe.Width = 168
    '
    'pnProg
    '
    Me.pnProg.AllowDrop = True
    Me.pnProg.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnProg.Appearance.Options.UseBackColor = True
    Me.pnProg.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnProg.Controls.Add(Me.pbProgProgressBar)
    Me.pnProg.Controls.Add(Me.lbAction)
    Me.pnProg.Controls.Add(Me.lbStep)
    Me.pnProg.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnProg.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnProg.Location = New System.Drawing.Point(0, 0)
    Me.pnProg.Name = "pnProg"
    Me.pnProg.NTSActiveTrasparency = True
    Me.pnProg.Size = New System.Drawing.Size(384, 50)
    Me.pnProg.TabIndex = 6
    Me.pnProg.Text = "NtsPanel1"
    '
    'pbProgProgressBar
    '
    Me.pbProgProgressBar.Location = New System.Drawing.Point(12, 56)
    Me.pbProgProgressBar.Name = "pbProgProgressBar"
    Me.pbProgProgressBar.Size = New System.Drawing.Size(357, 10)
    Me.pbProgProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
    Me.pbProgProgressBar.TabIndex = 520
    '
    'lbAction
    '
    Me.lbAction.AutoSize = True
    Me.lbAction.BackColor = System.Drawing.Color.Transparent
    Me.lbAction.Location = New System.Drawing.Point(12, 32)
    Me.lbAction.Name = "lbAction"
    Me.lbAction.NTSDbField = ""
    Me.lbAction.Size = New System.Drawing.Size(0, 13)
    Me.lbAction.TabIndex = 519
    Me.lbAction.Tooltip = ""
    Me.lbAction.UseMnemonic = False
    '
    'lbStep
    '
    Me.lbStep.BackColor = System.Drawing.Color.Transparent
    Me.lbStep.Location = New System.Drawing.Point(3, 3)
    Me.lbStep.Name = "lbStep"
    Me.lbStep.NTSDbField = ""
    Me.lbStep.Size = New System.Drawing.Size(378, 42)
    Me.lbStep.TabIndex = 519
    Me.lbStep.Text = "Elaborazioni preliminari in corso..."
    Me.lbStep.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbStep.Tooltip = ""
    Me.lbStep.UseMnemonic = False
    '
    'tmProgTimer
    '
    '
    'FRM__PROG
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(384, 50)
    Me.Controls.Add(Me.pnProg)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.LookAndFeel.SkinName = "Money Twins"
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__PROG"
    Me.Text = "CREA STRUTTURA"
    Me.TopMost = True
    CType(Me.pnProg, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnProg.ResumeLayout(False)
    Me.pnProg.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
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

    Return True
  End Function
  Public Overridable Sub InitEntity(ByRef oCleDupa As CLE__DUPA, ByRef oSql7 As Object, ByVal strOperazione As String)
    Try
      Me.oCleDupa = oCleDupa
      'Me.oSql7 = oSql7
      'Me.strOperazione = strOperazione
      AddHandler Me.oCleDupa.RemoteEvent, AddressOf GestisciEventiEntity
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRM__PROG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()


      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()


    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRM__PROG_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
  End Sub
  Public Overridable Sub FRM__PROG_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      Select Case strOperazione
        Case "N", "C"
          Me.Text = UCase(oApp.Tr(Me, 129024303952048733, "CREA DATABASE"))
        Case "T"
          Me.Text = UCase(oApp.Tr(Me, 128345816743546737, "CREA TABELLE ALLEGATE"))
        Case "S"
          Me.Text = UCase(oApp.Tr(Me, 128345816743546738, "RICARICA STORED PROCEDURE"))
        Case "D"
          Me.Text = UCase(oApp.Tr(Me, 128345816743546739, "CANCELLA DATABASE"))
        Case Else
          Me.Text = UCase(oApp.Tr(Me, 129024303086559014, "CREA STRUTTURA"))
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub FRM__PROG_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
    Try
      'nRis = 1

      'Select Case strOperazione
      '  Case "N"
      '    tmProgTimer.Interval = 200
      '    tmProgTimer.Start()
      '    lbStep.Text = oApp.Tr(Me, 128230023372629760, "Creazione azienda/database in corso...")
      '    'nRis = oSql7.crea()
      '    tmProgTimer.Stop()
      '  Case "C", "T"
      '    If strOperazione = "C" Then
      '      lbStep.Text = oApp.Tr(Me, 128230023372629700, "Collegamento azienda/database in corso...")
      '      'Collegamento del database
      '      'If Not oSql7.AttachDB() Then
      '      '  oApp.MsgBoxErr(oApp.Tr(Me, 129024304114866443, "Errore durante il collegamento del database."))
      '      '  Me.Close()
      '      '  Return
      '      'End If
      '    End If

      '    lbStep.Text = oApp.Tr(Me, 128230023372629701, "Creazione database delle tabelle allegate in corso...")
      '    'Creazione database delle tabelle allegate
      '    'If System.IO.File.Exists(oApp.Dir & "\" & oSql7.nomeDB & ".MDB") Then
      '    '  Kill(oApp.Dir & "\" & oSql7.nomeDB & ".MDB")
      '    'End If
      '    'nRis = oSql7.creaDBAllegate
      '    If nRis <> 0 Then
      '      oApp.MsgBoxErr(oApp.Tr(Me, 129024304256745983, "Creazione del database delle tabelle allegate non avvenuta."))
      '      Me.Close()
      '      Return
      '    End If
      'Case "S"
      '  'aggiorno le stored procedure
      '  lbStep.Text = oApp.Tr(Me, 128230023372633301, "Aggiornamento stored procedure in corso...")
      '  If Not oSql7.CreaStoredProceduresEx(IIf(oSql7.nomeDB = oApp.DbAp.Nome, 2, False), oSql7.nomeDB) Then
      '    oApp.MsgBoxErr(oApp.Tr(Me, 128230023372622319, "Errore in fase di ricreazione Stored Procedure."))
      '    Me.Close()
      '    Return
      '  End If
      '  nRis = 0
      'Case "D"
      '  Dim strErrTmp As String = "", strInfoTmp As String = ""
      '  'Cancellazione Database
      '  nRis = oSql7.CancellaDB
      '  If nRis <> 0 Then
      '    strErrTmp = oApp.Tr(Me, 128230023372629459, "Cancellazione del Database NON avvenuta.")
      '  Else
      '    strInfoTmp = oApp.Tr(Me, 12823002337262390, "Cancellazione del Database: " & oSql7.nomeDB & " nel Server: " & oSql7.Server & " avvenuta Correttamente.")
      '    oCleDupa.DeleteSQLServerSystemDSN(oSql7.nomeDB, oSql7.Server, oSql7.nomeDB, oSql7.Integrated)

      '    'Cancellazione DB Tabelle Allegate
      '    If System.IO.File.Exists(oApp.Dir & "\" & oSql7.nomeDB & ".MDB") Then
      '      oSql7.allegate = True
      '    Else
      '      oSql7.allegate = False
      '    End If
      '    If oSql7.allegate Then
      '      nRis = oSql7.CancellaAllegate
      '      If nRis <> 0 Then
      '        strErrTmp = NTSCStr(IIf(strErrTmp <> "", strErrTmp & vbCrLf, strErrTmp)) & oApp.Tr(Me, 128230023372626459, "Cancellazione del Database delle Tabelle Allegate NON avvenuta.")
      '      Else
      '        strInfoTmp = NTSCStr(IIf(strInfoTmp <> "", strInfoTmp & vbCrLf, strInfoTmp)) & oApp.Tr(Me, 12823002337262395, "Cancellazione del Database delle Tabelle Allegate " & oApp.Dir & "\" & oSql7.nomeDB & ".MDB" & " avvenuta Correttamente.")
      '      End If
      '    End If
      '  End If


      '  If strErrTmp = "" Then
      '    oApp.MsgBoxInfo(strInfoTmp)
      '  Else
      '    oApp.MsgBoxErr(strErrTmp & NTSCStr(IIf(strInfoTmp <> "", vbCrLf & strInfoTmp, strInfoTmp)))
      '  End If
      'End Select

      Dim oSqlTools As BN__ADTL = Nothing
      Dim strSqlServer As String = ""
      Dim strSqlUser As String = ""
      Dim strSqlPwd As String = ""
      Dim bIntSec As Boolean = False

      Try
        strSqlServer = oCleDupa.EstraiParametroDaConnectionString(IIf(strEdAdoconnectGlob = "", oApp.PrcConnect, strEdAdoconnectGlob), "SERVER")
        strSqlUser = oCleDupa.EstraiParametroDaConnectionString(IIf(strEdAdoconnectGlob = "", oApp.PrcConnect, strEdAdoconnectGlob), "UID")
        strSqlPwd = oCleDupa.EstraiParametroDaConnectionString(IIf(strEdAdoconnectGlob = "", oApp.PrcConnect, strEdAdoconnectGlob), "PWD")
        If strSqlUser.IndexOf("=") > -1 Then
          strSqlUser = ""
          strSqlPwd = ""
          bIntSec = True
        End If

        oSqlTools = New BN__ADTL
        oSqlTools.strProfilo = oApp.Profilo
        oSqlTools.DirMod = Application.StartupPath
        oSqlTools.DirLog = oApp.Dir
        oSqlTools.strPrcName = oApp.DbAp.Nome
        oSqlTools.dttAzi = dttAziendeGlob
        oSqlTools.bDaDupa = True
        AddHandler oSqlTools.lbStatus.TextChanged, AddressOf lbStatus_TextChanged

        If strDirNewDbGlob.Substring(strDirNewDbGlob.Length - 1) <> "\" Then strDirNewDbGlob += "\"
        If Not oSqlTools.CreaDB(strEdCodazGlob, strDirNewDbGlob, strDirNewDbLogGlob, strSqlServer, strSqlUser, strSqlPwd, bIntSec, strEdExtGlob, bUnicode) Then
          nRis = -1
        Else
          nRis = 0
        End If
        Me.Close()
      Catch ex As Exception
        Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Finally
        If Not oSqlTools Is Nothing Then
          oSqlTools.Dispose()
          oSqlTools = Nothing
        End If
      End Try

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    Me.Close()
  End Sub

  Public Overridable Sub FRM__PROG_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      e.Cancel = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region


  Public Overridable Sub tmProgTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmProgTimer.Tick
    Try
      'lbStep.Text = oSql7.RitornaNomeStepPerNET() & ".."
      If lbStep.Text = ".." Then
        lbStep.Text = oApp.Tr(Me, 128230023312629760, "Creazione azienda/database in corso...")
        lbStep.Refresh()
      End If
    Catch ex As Exception
    End Try
    Try
      ' lbAction.Text = oSql7.RitornaNomeActionPerNET()
    Catch ex As Exception
    End Try
  End Sub

  Public Overridable Sub lbStatus_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Try
      'refresh della label per lo stato di avanzamento creazione DB
      Me.Refresh()
      lbStep.Text = CType(sender, Label).Text
      Me.BringToFront()
      Me.TopMost = True
      lbStep.Refresh()
      Me.Refresh()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Function PassaDatiCreaDB(ByVal strEdAdoconnect As String, ByVal dttAziende As DataTable, ByVal strDirNewDb As String, _
                                              ByVal strEdCodaz As String, ByVal strDirNewDbLog As String, ByVal strEdExt As String) As Boolean
    Try
      strEdAdoconnectGlob = strEdAdoconnect
      dttAziendeGlob = dttAziende
      strDirNewDbGlob = strDirNewDb
      strEdCodazGlob = strEdCodaz
      strDirNewDbLogGlob = strDirNewDbLog
      strEdExtGlob = strEdExt
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
End Class

