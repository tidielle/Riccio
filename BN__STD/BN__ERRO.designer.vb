<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRM__ERRO
  Inherits DevExpress.XtraEditors.XtraForm

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__ERRO))
    Me.printPreview = New System.Windows.Forms.PrintPreviewControl
    Me.simplePrintDoc = New System.Drawing.Printing.PrintDocument
    Me.pnBottom = New System.Windows.Forms.Panel
    Me.lbCopia = New System.Windows.Forms.LinkLabel
    Me.lbStampa = New System.Windows.Forms.LinkLabel
    Me.cmdOk = New System.Windows.Forms.Button
    Me.edErrore = New System.Windows.Forms.RichTextBox
    Me.pnTop = New System.Windows.Forms.Panel
    Me.edDettagli = New System.Windows.Forms.TextBox
    Me.pcError = New System.Windows.Forms.PictureBox
    Me.pnBottom.SuspendLayout()
    Me.pnTop.SuspendLayout()
    CType(Me.pcError, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'printPreview
    '
    Me.printPreview.AutoZoom = False
    Me.printPreview.Document = Me.simplePrintDoc
    Me.printPreview.Location = New System.Drawing.Point(404, 0)
    Me.printPreview.Name = "printPreview"
    Me.printPreview.Size = New System.Drawing.Size(29, 33)
    Me.printPreview.TabIndex = 0
    Me.printPreview.UseAntiAlias = True
    Me.printPreview.Visible = False
    Me.printPreview.Zoom = 1
    '
    'simplePrintDoc
    '
    '
    'pnBottom
    '
    Me.pnBottom.Controls.Add(Me.lbCopia)
    Me.pnBottom.Controls.Add(Me.printPreview)
    Me.pnBottom.Controls.Add(Me.lbStampa)
    Me.pnBottom.Controls.Add(Me.cmdOk)
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 399)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.Size = New System.Drawing.Size(548, 36)
    Me.pnBottom.TabIndex = 5
    '
    'lbCopia
    '
    Me.lbCopia.AutoSize = True
    Me.lbCopia.Location = New System.Drawing.Point(68, 10)
    Me.lbCopia.Name = "lbCopia"
    Me.lbCopia.Size = New System.Drawing.Size(98, 13)
    Me.lbCopia.TabIndex = 6
    Me.lbCopia.TabStop = True
    Me.lbCopia.Text = "Copia negli appunti"
    '
    'lbStampa
    '
    Me.lbStampa.AutoSize = True
    Me.lbStampa.Location = New System.Drawing.Point(9, 10)
    Me.lbStampa.Name = "lbStampa"
    Me.lbStampa.Size = New System.Drawing.Size(43, 13)
    Me.lbStampa.TabIndex = 4
    Me.lbStampa.TabStop = True
    Me.lbStampa.Text = "Stampa"
    '
    'cmdOk
    '
    Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdOk.Location = New System.Drawing.Point(461, 5)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(75, 23)
    Me.cmdOk.TabIndex = 0
    Me.cmdOk.Text = "&Ok"
    Me.cmdOk.UseVisualStyleBackColor = True
    '
    'edErrore
    '
    Me.edErrore.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edErrore.BackColor = System.Drawing.Color.White
    Me.edErrore.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.edErrore.Location = New System.Drawing.Point(61, 5)
    Me.edErrore.Name = "edErrore"
    Me.edErrore.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth
    Me.edErrore.Size = New System.Drawing.Size(484, 391)
    Me.edErrore.TabIndex = 6
    Me.edErrore.Text = "asdfsadfsdaf  sadfasdfasdf asdfasdf              asdfasdfasdfasfd xxxxxxxxxxxxxxx" & _
        "xxxxxxxxxxxxxxxxxxx xxxxxxxxxxxxxxxxxxxxxxxxxxx xxxxxxxxxxxxxxxxxxxxxxxxxxxx"
    '
    'pnTop
    '
    Me.pnTop.BackColor = System.Drawing.Color.White
    Me.pnTop.Controls.Add(Me.edDettagli)
    Me.pnTop.Controls.Add(Me.pcError)
    Me.pnTop.Controls.Add(Me.edErrore)
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.Size = New System.Drawing.Size(548, 399)
    Me.pnTop.TabIndex = 7
    '
    'edDettagli
    '
    Me.edDettagli.BackColor = System.Drawing.Color.White
    Me.edDettagli.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.edDettagli.Location = New System.Drawing.Point(12, 62)
    Me.edDettagli.Multiline = True
    Me.edDettagli.Name = "edDettagli"
    Me.edDettagli.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.edDettagli.Size = New System.Drawing.Size(52, 45)
    Me.edDettagli.TabIndex = 4
    Me.edDettagli.TabStop = False
    Me.edDettagli.Text = "asdfsadfsdaf  sadfasdfasdf asdfasdf              asdfasdfasdfasfd xxxxxxxxxxxxxxx" & _
        "xxxxxxxxxxxxxxxxxxx xxxxxxxxxxxxxxxxxxxxxxxxxxx xxxxxxxxxxxxxxxxxxxxxxxxxxxx" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & _
        "manca l'icona!!!!!!!!"
    Me.edDettagli.Visible = False
    '
    'pcError
    '
    Me.pcError.BackColor = System.Drawing.Color.White
    Me.pcError.Image = CType(resources.GetObject("pcError.Image"), System.Drawing.Image)
    Me.pcError.Location = New System.Drawing.Point(12, 12)
    Me.pcError.Name = "pcError"
    Me.pcError.Size = New System.Drawing.Size(43, 44)
    Me.pcError.TabIndex = 0
    Me.pcError.TabStop = False
    '
    'FRM__ERRO
    '
    Me.AcceptButton = Me.cmdOk
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(548, 435)
    Me.Controls.Add(Me.pnTop)
    Me.Controls.Add(Me.pnBottom)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__ERRO"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Messaggio da Business NET"
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.pcError, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public WithEvents pnBottom As System.Windows.Forms.Panel
  Public WithEvents cmdOk As System.Windows.Forms.Button
  Public WithEvents edErrore As System.Windows.Forms.RichTextBox
  Public WithEvents pnTop As System.Windows.Forms.Panel
  Public WithEvents pcError As System.Windows.Forms.PictureBox
  Public WithEvents edDettagli As System.Windows.Forms.TextBox
  Public WithEvents lbStampa As System.Windows.Forms.LinkLabel
  Public WithEvents printPreview As System.Windows.Forms.PrintPreviewControl
  Public WithEvents simplePrintDoc As System.Drawing.Printing.PrintDocument
  Public WithEvents lbCopia As System.Windows.Forms.LinkLabel

End Class