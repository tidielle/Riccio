Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HLCP

#Region "Variabili"
  Public oCleIstp As CLE__ISTP
  Public oCallParams As CLE__CLDP
  Public dsHlcp As New DataSet

  Public WithEvents pnHlcp As NTSInformatica.NTSPanel
  Public WithEvents liShowFields As NTSInformatica.NTSListBox
  Public WithEvents liTableList As NTSInformatica.NTSListBox
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOk As NTSInformatica.NTSButton
  Public WithEvents lbCampi As NTSInformatica.NTSLabel
  Public WithEvents lbTabelle As NTSInformatica.NTSLabel

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
  Public Overridable Sub InitializeComponent()
    Me.pnHlcp = New NTSInformatica.NTSPanel
    Me.liShowFields = New NTSInformatica.NTSListBox
    Me.liTableList = New NTSInformatica.NTSListBox
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.lbCampi = New NTSInformatica.NTSLabel
    Me.lbTabelle = New NTSInformatica.NTSLabel
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.edCampo = New NTSInformatica.NTSTextBoxStr
    Me.lbCampo = New NTSInformatica.NTSLabel
    Me.edTabella = New NTSInformatica.NTSTextBoxStr
    Me.lbTabella = New NTSInformatica.NTSLabel
    Me.cmdTabella = New NTSInformatica.NTSButton
    Me.cmdCampo = New NTSInformatica.NTSButton
    CType(Me.pnHlcp, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnHlcp.SuspendLayout()
    CType(Me.liShowFields, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liTableList, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.edCampo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTabella.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'pnHlcp
    '
    Me.pnHlcp.AllowDrop = True
    Me.pnHlcp.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnHlcp.Appearance.Options.UseBackColor = True
    Me.pnHlcp.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnHlcp.Controls.Add(Me.liShowFields)
    Me.pnHlcp.Controls.Add(Me.liTableList)
    Me.pnHlcp.Controls.Add(Me.cmdAnnulla)
    Me.pnHlcp.Controls.Add(Me.cmdOk)
    Me.pnHlcp.Controls.Add(Me.lbCampi)
    Me.pnHlcp.Controls.Add(Me.lbTabelle)
    Me.pnHlcp.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnHlcp.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnHlcp.Location = New System.Drawing.Point(0, 42)
    Me.pnHlcp.Name = "pnHlcp"
    Me.pnHlcp.Size = New System.Drawing.Size(886, 312)
    Me.pnHlcp.TabIndex = 6
    Me.pnHlcp.Text = "NtsPanel1"
    '
    'liShowFields
    '
    Me.liShowFields.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.liShowFields.Cursor = System.Windows.Forms.Cursors.Default
    Me.liShowFields.ItemHeight = 14
    Me.liShowFields.Location = New System.Drawing.Point(384, 25)
    Me.liShowFields.Name = "liShowFields"
    Me.liShowFields.NTSDbField = ""
    Me.liShowFields.Size = New System.Drawing.Size(367, 275)
    Me.liShowFields.TabIndex = 11
    '
    'liTableList
    '
    Me.liTableList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.liTableList.Cursor = System.Windows.Forms.Cursors.Default
    Me.liTableList.ItemHeight = 14
    Me.liTableList.Location = New System.Drawing.Point(11, 25)
    Me.liTableList.Name = "liTableList"
    Me.liTableList.NTSDbField = ""
    Me.liTableList.Size = New System.Drawing.Size(367, 275)
    Me.liTableList.TabIndex = 10
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.Location = New System.Drawing.Point(757, 54)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(123, 23)
    Me.cmdAnnulla.TabIndex = 9
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdOk.Location = New System.Drawing.Point(757, 25)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(123, 23)
    Me.cmdOk.TabIndex = 8
    Me.cmdOk.Text = "&Conferma"
    '
    'lbCampi
    '
    Me.lbCampi.AutoSize = True
    Me.lbCampi.BackColor = System.Drawing.Color.Transparent
    Me.lbCampi.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbCampi.Location = New System.Drawing.Point(381, 9)
    Me.lbCampi.Name = "lbCampi"
    Me.lbCampi.NTSDbField = ""
    Me.lbCampi.Size = New System.Drawing.Size(105, 13)
    Me.lbCampi.TabIndex = 7
    Me.lbCampi.Text = "Selezione Campi :"
    Me.lbCampi.UseMnemonic = False
    '
    'lbTabelle
    '
    Me.lbTabelle.AutoSize = True
    Me.lbTabelle.BackColor = System.Drawing.Color.Transparent
    Me.lbTabelle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbTabelle.Location = New System.Drawing.Point(8, 9)
    Me.lbTabelle.Name = "lbTabelle"
    Me.lbTabelle.NTSDbField = ""
    Me.lbTabelle.Size = New System.Drawing.Size(111, 13)
    Me.lbTabelle.TabIndex = 6
    Me.lbTabelle.Text = "Selezione Tabelle :"
    Me.lbTabelle.UseMnemonic = False
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.cmdCampo)
    Me.pnTop.Controls.Add(Me.cmdTabella)
    Me.pnTop.Controls.Add(Me.edCampo)
    Me.pnTop.Controls.Add(Me.lbCampo)
    Me.pnTop.Controls.Add(Me.edTabella)
    Me.pnTop.Controls.Add(Me.lbTabella)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(886, 42)
    Me.pnTop.TabIndex = 7
    Me.pnTop.Text = "NtsPanel1"
    '
    'edCampo
    '
    Me.edCampo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCampo.Location = New System.Drawing.Point(471, 12)
    Me.edCampo.Name = "edCampo"
    Me.edCampo.NTSDbField = ""
    Me.edCampo.NTSForzaVisZoom = False
    Me.edCampo.NTSOldValue = ""
    Me.edCampo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCampo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCampo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCampo.Properties.MaxLength = 65536
    Me.edCampo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCampo.Size = New System.Drawing.Size(184, 20)
    Me.edCampo.TabIndex = 12
    '
    'lbCampo
    '
    Me.lbCampo.AutoSize = True
    Me.lbCampo.BackColor = System.Drawing.Color.Transparent
    Me.lbCampo.Location = New System.Drawing.Point(381, 15)
    Me.lbCampo.Name = "lbCampo"
    Me.lbCampo.NTSDbField = ""
    Me.lbCampo.Size = New System.Drawing.Size(69, 13)
    Me.lbCampo.TabIndex = 11
    Me.lbCampo.Text = "Cerca campo"
    Me.lbCampo.UseMnemonic = False
    '
    'edTabella
    '
    Me.edTabella.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTabella.Location = New System.Drawing.Point(112, 12)
    Me.edTabella.Name = "edTabella"
    Me.edTabella.NTSDbField = ""
    Me.edTabella.NTSForzaVisZoom = False
    Me.edTabella.NTSOldValue = ""
    Me.edTabella.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTabella.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTabella.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTabella.Properties.MaxLength = 65536
    Me.edTabella.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTabella.Size = New System.Drawing.Size(184, 20)
    Me.edTabella.TabIndex = 10
    '
    'lbTabella
    '
    Me.lbTabella.AutoSize = True
    Me.lbTabella.BackColor = System.Drawing.Color.Transparent
    Me.lbTabella.Location = New System.Drawing.Point(14, 15)
    Me.lbTabella.Name = "lbTabella"
    Me.lbTabella.NTSDbField = ""
    Me.lbTabella.Size = New System.Drawing.Size(70, 13)
    Me.lbTabella.TabIndex = 9
    Me.lbTabella.Text = "Cerca tabella"
    Me.lbTabella.UseMnemonic = False
    '
    'cmdTabella
    '
    Me.cmdTabella.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdTabella.Location = New System.Drawing.Point(302, 12)
    Me.cmdTabella.Name = "cmdTabella"
    Me.cmdTabella.Size = New System.Drawing.Size(45, 20)
    Me.cmdTabella.TabIndex = 13
    Me.cmdTabella.Text = "Vai"
    '
    'cmdCampo
    '
    Me.cmdCampo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdCampo.Location = New System.Drawing.Point(661, 12)
    Me.cmdCampo.Name = "cmdCampo"
    Me.cmdCampo.Size = New System.Drawing.Size(45, 20)
    Me.cmdCampo.TabIndex = 14
    Me.cmdCampo.Text = "Vai"
    '
    'FRM__HLCP
    '
    Me.ClientSize = New System.Drawing.Size(886, 354)
    Me.Controls.Add(Me.pnHlcp)
    Me.Controls.Add(Me.pnTop)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__HLCP"
    Me.Text = "ZOOM CAMPI PER SQL"
    CType(Me.pnHlcp, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnHlcp.ResumeLayout(False)
    Me.pnHlcp.PerformLayout()
    CType(Me.liShowFields, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liTableList, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.edCampo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTabella.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '--------------------------------------------------------------------------------------------------------------
      Try

      Catch ex As Exception

      End Try
      edTabella.NTSSetParam(oMenu, oApp.Tr(Me, 128306284872414723, "Cerca tabella"), 0)
      edCampo.NTSSetParam(oMenu, oApp.Tr(Me, 129144925455894965, "Cerca campo"), 0)
      edTabella.NTSForzaVisZoom = False
      edCampo.NTSForzaVisZoom = False
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub
  Public Overridable Sub InitEntity(ByRef cleIstp As CLE__ISTP)
    oCleIstp = cleIstp
    AddHandler oCleIstp.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

#Region "EventiForm"
  Public Overridable Sub FRM__HLCP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If Not oCleIstp.leggiTabelle(dsHlcp) Then Me.Close()

      liTableList.DataSource = dsHlcp.Tables("TABELLE")
      liTableList.ValueMember = "cod"
      liTableList.DisplayMember = "val"

      liTableList.SelectedValue = -1

      cmdOk.Enabled = False

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
#End Region

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleIstp.strParstagDescam = ""
      oCleIstp.strParstagColumn = ""
      oCleIstp.nParstagTipo = 0
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      cmdOkClick()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdTabella_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTabella.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      If edTabella.Text.Trim = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If liTableList.ItemCount = 0 Then Return
      '--------------------------------------------------------------------------------------------------------------
      Dim dtrT() As DataRow = CType(liTableList.DataSource, DataTable).Select("cod like '%" & edTabella.Text.Trim & "%'", "cod")
      If dtrT.Length > 0 Then edTabella.Text = dtrT(0)!cod.ToString
      liTableList.SelectedValue = edTabella.Text.Trim
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdCampo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCampo.Click
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If edCampo.Text.Trim = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If liShowFields.ItemCount = 0 Then Return
      '--------------------------------------------------------------------------------------------------------------
      strTmp = edCampo.Text.Trim
      Dim dtrT() As DataRow = CType(liShowFields.DataSource, DataTable).Select("cod like '%" & edCampo.Text.Trim & "%'", "val")
      If dtrT.Length > 0 Then edCampo.Text = dtrT(0)!cod.ToString
      liShowFields.SelectedValue = edCampo.Text.Trim
      edCampo.Text = strTmp
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub liTableList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles liTableList.SelectedIndexChanged
    Dim i As Integer
    Dim dttCampi As New DataTable()
    Dim strTabella As String

    Try

      If liTableList.SelectedIndex > -1 Then

        strTabella = NTSCStr(liTableList.SelectedValue)

        If Not oCleIstp.leggiCampi(strTabella, dsHlcp) Then Me.Close()

        dttCampi.Columns.Add("cod", GetType(String))
        dttCampi.Columns.Add("val", GetType(String))

        For i = 0 To dsHlcp.Tables("CAMPI").Rows.Count - 1
          dttCampi.Rows.Add(New Object() {NTSCStr(dsHlcp.Tables("CAMPI").Rows(i)!cb_nomcampo) & "|" & NTSCStr(dsHlcp.Tables("CAMPI").Rows(i)!cb_tipocampo) & "|" & NTSCStr(dsHlcp.Tables("CAMPI").Rows(i)!cb_descampo), NTSCStr(dsHlcp.Tables("CAMPI").Rows(i)!cb_descampo)})
        Next
        dttCampi.AcceptChanges()

        liShowFields.DataSource = dttCampi
        liShowFields.ValueMember = "cod"
        liShowFields.DisplayMember = "val"
        liShowFields.SelectedValue = -1
      End If

      cmdOk.Enabled = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub liShowFields_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles liShowFields.SelectedIndexChanged
    Try
      cmdOk.Enabled = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub liShowFields_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles liShowFields.DoubleClick
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
      Dim str1 As String = ""
      Dim strT() As String = Nothing
      str1 = NTSCStr(liShowFields.SelectedValue)
      strT = str1.Split(CChar("|"))

      oCleIstp.strParstagDescam = Microsoft.VisualBasic.Left(Trim(NTSCStr(strT(2))), 40)
      oCleIstp.strParstagColumn = strT(0)
      oCleIstp.nParstagTipo = NTSCInt(strT(1))
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  
End Class