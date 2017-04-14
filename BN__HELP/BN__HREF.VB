Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO



Public Class FRM__HREF

#Region "Variabili"

  Private components As System.ComponentModel.IContainer
  Public oPar As CLE__CLDP
  Public WithEvents cmdProcedi As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents pnHref As NTSInformatica.NTSPanel
  Public WithEvents tsHref As NTSInformatica.NTSTabControl
  Public WithEvents TabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnCerca As NTSInformatica.NTSPanel
  Public WithEvents TabPage0 As NTSInformatica.NTSTabPage
  Public WithEvents pnContenuto As NTSInformatica.NTSPanel
  Public WithEvents trContenuto As NTSInformatica.NTSTreeView
  Public WithEvents opCollegamentoLinkManuale As NTSInformatica.NTSRadioButton
  Public WithEvents edLink As NTSInformatica.NTSTextBoxStr
  Public WithEvents edNome As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTesto As System.Windows.Forms.Label
  Public bControlBoxExit As Boolean
  Public strMainDir As String = ""
  Public strSep As String = ""

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
    Me.pnHref = New NTSInformatica.NTSPanel
    Me.lbTesto = New System.Windows.Forms.Label
    Me.edNome = New NTSInformatica.NTSTextBoxStr
    Me.tsHref = New NTSInformatica.NTSTabControl
    Me.TabPage0 = New NTSInformatica.NTSTabPage
    Me.pnContenuto = New NTSInformatica.NTSPanel
    Me.trContenuto = New NTSInformatica.NTSTreeView
    Me.TabPage1 = New NTSInformatica.NTSTabPage
    Me.pnCerca = New NTSInformatica.NTSPanel
    Me.edLink = New NTSInformatica.NTSTextBoxStr
    Me.opCollegamentoLinkManuale = New NTSInformatica.NTSRadioButton
    CType(Me.pnHref, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnHref.SuspendLayout()
    CType(Me.edNome.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsHref, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsHref.SuspendLayout()
    Me.TabPage0.SuspendLayout()
    CType(Me.pnContenuto, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnContenuto.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    CType(Me.pnCerca, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCerca.SuspendLayout()
    CType(Me.edLink.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opCollegamentoLinkManuale.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.cmdProcedi.Location = New System.Drawing.Point(304, 452)
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
    Me.cmdAnnulla.Location = New System.Drawing.Point(376, 452)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(66, 23)
    Me.cmdAnnulla.TabIndex = 2
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnHref
    '
    Me.pnHref.AllowDrop = True
    Me.pnHref.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnHref.Appearance.Options.UseBackColor = True
    Me.pnHref.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnHref.Controls.Add(Me.lbTesto)
    Me.pnHref.Controls.Add(Me.edNome)
    Me.pnHref.Controls.Add(Me.tsHref)
    Me.pnHref.Controls.Add(Me.cmdAnnulla)
    Me.pnHref.Controls.Add(Me.cmdProcedi)
    Me.pnHref.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnHref.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnHref.Location = New System.Drawing.Point(0, 0)
    Me.pnHref.Name = "pnHref"
    Me.pnHref.Size = New System.Drawing.Size(454, 487)
    Me.pnHref.TabIndex = 21
    Me.pnHref.Text = "NtsPanel1"
    '
    'lbTesto
    '
    Me.lbTesto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbTesto.AutoSize = True
    Me.lbTesto.Location = New System.Drawing.Point(12, 457)
    Me.lbTesto.Name = "lbTesto"
    Me.lbTesto.Size = New System.Drawing.Size(34, 13)
    Me.lbTesto.TabIndex = 17
    Me.lbTesto.Text = "Testo"
    '
    'edNome
    '
    Me.edNome.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edNome.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNome.Location = New System.Drawing.Point(52, 454)
    Me.edNome.Name = "edNome"
    Me.edNome.NTSDbField = ""
    Me.edNome.NTSForzaVisZoom = False
    Me.edNome.NTSOldValue = ""
    Me.edNome.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNome.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNome.Properties.MaxLength = 65536
    Me.edNome.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNome.Size = New System.Drawing.Size(246, 20)
    Me.edNome.TabIndex = 10
    '
    'tsHref
    '
    Me.tsHref.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.tsHref.Location = New System.Drawing.Point(0, 0)
    Me.tsHref.Name = "tsHref"
    Me.tsHref.SelectedTabPage = Me.TabPage1
    Me.tsHref.Size = New System.Drawing.Size(454, 440)
    Me.tsHref.TabIndex = 9
    Me.tsHref.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabPage0, Me.TabPage1})
    '
    'TabPage0
    '
    Me.TabPage0.AllowDrop = True
    Me.TabPage0.Controls.Add(Me.pnContenuto)
    Me.TabPage0.Enable = True
    Me.TabPage0.Name = "TabPage0"
    Me.TabPage0.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage0.Size = New System.Drawing.Size(445, 410)
    Me.TabPage0.Text = "Business"
    '
    'pnContenuto
    '
    Me.pnContenuto.AllowDrop = True
    Me.pnContenuto.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnContenuto.Appearance.Options.UseBackColor = True
    Me.pnContenuto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnContenuto.Controls.Add(Me.trContenuto)
    Me.pnContenuto.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.pnContenuto.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnContenuto.Location = New System.Drawing.Point(3, 3)
    Me.pnContenuto.Name = "pnContenuto"
    Me.pnContenuto.Size = New System.Drawing.Size(439, 404)
    Me.pnContenuto.TabIndex = 0
    Me.pnContenuto.Text = "NtsPanel1"
    '
    'trContenuto
    '
    Me.trContenuto.Dock = System.Windows.Forms.DockStyle.Fill
    Me.trContenuto.HideSelection = False
    Me.trContenuto.Location = New System.Drawing.Point(0, 0)
    Me.trContenuto.Name = "trContenuto"
    Me.trContenuto.Size = New System.Drawing.Size(439, 404)
    Me.trContenuto.TabIndex = 0
    '
    'TabPage1
    '
    Me.TabPage1.AllowDrop = True
    Me.TabPage1.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.TabPage1.Appearance.Header.Options.UseFont = True
    Me.TabPage1.Controls.Add(Me.pnCerca)
    Me.TabPage1.Enable = True
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage1.Size = New System.Drawing.Size(445, 410)
    Me.TabPage1.Text = "Altro"
    '
    'pnCerca
    '
    Me.pnCerca.AllowDrop = True
    Me.pnCerca.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCerca.Appearance.Options.UseBackColor = True
    Me.pnCerca.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCerca.Controls.Add(Me.edLink)
    Me.pnCerca.Controls.Add(Me.opCollegamentoLinkManuale)
    Me.pnCerca.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCerca.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCerca.Location = New System.Drawing.Point(3, 3)
    Me.pnCerca.Name = "pnCerca"
    Me.pnCerca.Size = New System.Drawing.Size(439, 404)
    Me.pnCerca.TabIndex = 1
    Me.pnCerca.Text = "NtsPanel1"
    '
    'edLink
    '
    Me.edLink.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edLink.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLink.Location = New System.Drawing.Point(114, 21)
    Me.edLink.Name = "edLink"
    Me.edLink.NTSDbField = ""
    Me.edLink.NTSForzaVisZoom = False
    Me.edLink.NTSOldValue = ""
    Me.edLink.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLink.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLink.Properties.MaxLength = 65536
    Me.edLink.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLink.Size = New System.Drawing.Size(302, 20)
    Me.edLink.TabIndex = 6
    '
    'opCollegamentoLinkManuale
    '
    Me.opCollegamentoLinkManuale.Cursor = System.Windows.Forms.Cursors.Default
    Me.opCollegamentoLinkManuale.EditValue = True
    Me.opCollegamentoLinkManuale.Location = New System.Drawing.Point(20, 21)
    Me.opCollegamentoLinkManuale.Name = "opCollegamentoLinkManuale"
    Me.opCollegamentoLinkManuale.NTSCheckValue = "S"
    Me.opCollegamentoLinkManuale.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opCollegamentoLinkManuale.Properties.Appearance.Options.UseBackColor = True
    Me.opCollegamentoLinkManuale.Properties.Caption = "Link manuale"
    Me.opCollegamentoLinkManuale.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opCollegamentoLinkManuale.Size = New System.Drawing.Size(97, 19)
    Me.opCollegamentoLinkManuale.TabIndex = 0
    '
    'FRM__HREF
    '
    Me.AcceptButton = Me.cmdProcedi
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(454, 487)
    Me.Controls.Add(Me.pnHref)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__HREF"
    Me.Text = "INSERISCI COLLEGAMENTO"
    CType(Me.pnHref, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnHref.ResumeLayout(False)
    Me.pnHref.PerformLayout()
    CType(Me.edNome.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsHref, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsHref.ResumeLayout(False)
    Me.TabPage0.ResumeLayout(False)
    CType(Me.pnContenuto, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnContenuto.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    CType(Me.pnCerca, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCerca.ResumeLayout(False)
    Me.pnCerca.PerformLayout()
    CType(Me.edLink.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opCollegamentoLinkManuale.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Form"

  Public Overridable Sub FRM__SRPT_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    'Cerco di uscure dal Control-Box
    If bControlBoxExit Then
      e.Cancel = True
    End If
  End Sub

  Public Overridable Sub FRM__SRPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    GctlSetRoules()
    GctlApplicaDefaultValue()

    trContenuto.Visible = False
    trContenuto.Nodes.Clear()
    Load_CaricaLocale()
    trContenuto.CollapseAll()
    trContenuto.Visible = True

    edLink.Text = "http:\\"
    edNome.Text = ""
    tsHref.SelectedTabPageIndex = 0
  End Sub

#End Region

  Public Overridable Sub cmdProcedi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcedi.Click
    If edNome.Text.Trim = "" Then Return
    If tsHref.SelectedTabPage.Name = TabPage0.Name Then
      If trContenuto.SelectedNode Is Nothing Then Return
      oPar.strPar1 = " <a href=""..\" & trContenuto.SelectedNode.Tag.ToString & """>" & edNome.Text.Trim & "</a> "
    Else
      If edLink.Text.Trim = "" Then Return
      oPar.strPar1 = " <a href=""" & edLink.Text.Trim & """>" & edNome.Text.Trim & "</a> "
    End If

    oPar.bPar1 = True
    Me.Close()
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oPar.bPar1 = False
    Me.Close()
  End Sub


  Public Overridable Sub Load_CaricaLocale()
    Dim r1 As StreamReader = Nothing
    Dim strFile As String = ""
    Dim strTxt As String = ""
    Dim strTitle As String = ""
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim oNode As TreeNode = Nothing
    Dim oCurrNode As TreeNode = Nothing
    Dim oLastNodeAdd As TreeNode = Nothing

    Try
      Me.Cursor = Cursors.WaitCursor

      'carico il menu del tab 'contenuto'
      r1 = New StreamReader(strMainDir & "sommario.hhc", System.Text.Encoding.Default)
      Do While Not r1.EndOfStream
        strTxt = r1.ReadLine

        'aggiungo un nodo finale
        If strTxt.IndexOf("<LI>") > -1 Then
          oNode = New TreeNode
          oNode.ImageIndex = 0
          oNode.SelectedImageIndex = 0

          strTxt = r1.ReadLine 'caption
          strTxt = strTxt.Substring(strTxt.IndexOf("value="""))
          strTxt = strTxt.Substring(7, strTxt.Length - 9)
          oNode.Text = ReplaceText(strTxt)

          strTxt = r1.ReadLine() 'nome del file
          strTxt = strTxt.Substring(strTxt.IndexOf("value="""))
          strTxt = strTxt.Substring(7, strTxt.Length - 9)
          oNode.Tag = strTxt
          oNode.Name = strTxt.Replace("html" & strSep, "")

          If oCurrNode Is Nothing Then
            trContenuto.Nodes.Add(oNode)
          Else
            oCurrNode.Nodes.Add(oNode)
            oCurrNode.ImageIndex = 1
            oCurrNode.SelectedImageIndex = 1
          End If
        End If

        'aggiungo un nodo menu
        If strTxt.IndexOf("<UL>") > -1 Then
          oCurrNode = oNode
        End If

        'torno indietro un nodo menu
        If strTxt.IndexOf("</UL>") > -1 Then
          If oCurrNode Is Nothing Then
            oCurrNode = Nothing
          Else
            oCurrNode = oCurrNode.Parent
          End If
        End If
      Loop
      r1.Close()


    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Function ReplaceText(ByVal strIn As String) As String
    Return strIn.Replace("&egrave;", "è").Replace("&agrave;", "à").Replace("&ugrave;", "ù").Replace("&igrave;", "ì").Replace("&ograve;", "ò")
  End Function
  Public Overridable Sub trContenuto_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trContenuto.AfterSelect
    Try
      edNome.Text = trContenuto.SelectedNode.Text
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub edLink_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edLink.ValidatedAndChanged
    Try
      If edNome.Text.Trim = "" Then edNome.Text = edLink.Text
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
End Class
