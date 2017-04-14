Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class NTSXXNODO

  Public WithEvents pnNodo As NTSInformatica.NTSPanel
  Public IdNodo As Integer = 0
  Public oParent As FRM__CHIL = Nothing
  Public WithEvents lbTesto As NTSInformatica.NTSLabel
  Public WithEvents lbNote As NTSInformatica.NTSLabel
  Public WithEvents pcImage As NTSInformatica.NTSPictureBox

  Public Overridable Sub InitializeComponent()
    Me.pnNodo = New NTSInformatica.NTSPanel
    Me.lbNote = New NTSInformatica.NTSLabel
    Me.lbTesto = New NTSInformatica.NTSLabel
    Me.pcImage = New NTSInformatica.NTSPictureBox
    CType(Me.pnNodo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNodo.SuspendLayout()
    CType(Me.pcImage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'pnNodo
    '
    Me.pnNodo.AllowDrop = True
    Me.pnNodo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNodo.Appearance.Options.UseBackColor = True
    Me.pnNodo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNodo.Controls.Add(Me.lbNote)
    Me.pnNodo.Controls.Add(Me.lbTesto)
    Me.pnNodo.Controls.Add(Me.pcImage)
    Me.pnNodo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNodo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnNodo.Location = New System.Drawing.Point(0, 0)
    Me.pnNodo.Name = "pnNodo"
    Me.pnNodo.Size = New System.Drawing.Size(174, 46)
    Me.pnNodo.TabIndex = 0
    Me.pnNodo.Text = "NtsPanel1"
    '
    'lbNote
    '
    Me.lbNote.BackColor = System.Drawing.SystemColors.Info
    Me.lbNote.Location = New System.Drawing.Point(0, 47)
    Me.lbNote.Name = "lbNote"
    Me.lbNote.NTSDbField = ""
    Me.lbNote.Size = New System.Drawing.Size(174, 103)
    Me.lbNote.TabIndex = 2
    '
    'lbTesto
    '
    Me.lbTesto.BackColor = System.Drawing.SystemColors.Info
    Me.lbTesto.Location = New System.Drawing.Point(50, 0)
    Me.lbTesto.Name = "lbTesto"
    Me.lbTesto.NTSDbField = ""
    Me.lbTesto.Size = New System.Drawing.Size(126, 46)
    Me.lbTesto.TabIndex = 1
    '
    'pcImage
    '
    Me.pcImage.AllowDrop = True
    Me.pcImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
    Me.pcImage.Cursor = System.Windows.Forms.Cursors.Default
    Me.pcImage.Location = New System.Drawing.Point(0, 0)
    Me.pcImage.Name = "pcImage"
    Me.pcImage.Padding = New System.Windows.Forms.Padding(5)
    Me.pcImage.Properties.Appearance.BackColor = System.Drawing.Color.Black
    Me.pcImage.Properties.Appearance.Options.UseBackColor = True
    Me.pcImage.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pcImage.Properties.ShowMenu = False
    Me.pcImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
    Me.pcImage.Size = New System.Drawing.Size(50, 46)
    Me.pcImage.TabIndex = 0
    '
    'NTSXXNODO
    '
    Me.BackColor = System.Drawing.SystemColors.Info
    Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.Controls.Add(Me.pnNodo)
    Me.Name = "NTSXXNODO"
    Me.Size = New System.Drawing.Size(174, 46)
    CType(Me.pnNodo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNodo.ResumeLayout(False)
    CType(Me.pcImage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Sub New()
    InitializeComponent()
    Me.MinimumSize = Me.Size
  End Sub

  Public Sub NTSSetParam(ByRef Menu As CLE__MENU, ByVal NomeCampo As String, ByVal ChildChiamante As String, ByRef Param As CLE__CLDP)
    Try
      '------------------------------------------------
      oMenu = Menu
      oApp = oMenu.oApp
      strNomeCampo = NomeCampo
      oParent = CType(Me.FindForm, FRM__CHIL)

      'InitControls()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public WriteOnly Property Note() As String
    Set(ByVal value As String)
      lbNote.Text = value
    End Set
  End Property
  Public WriteOnly Property Caption() As String
    Set(ByVal value As String)
      lbTesto.Text = value
    End Set
  End Property
  Public WriteOnly Property Picture() As String
    Set(ByVal value As String)
      'value = ttn_tipoogg
      Dim strImage As String = ""
      Select Case value
        Case "1" : strImage = "ordine"
        Case "2" : strImage = "ddt"
        Case "3" : strImage = "fatimm"
        Case "4" : strImage = "fatdiff"
        Case "5" : strImage = "contab"
        Case "6" : strImage = "scadenza"
        Case "7" : strImage = "offerta"
        Case "8" : strImage = "notaprel"
      End Select
      If IdNodo = 1 Then strImage += "start" 'se è il nodo di partenza lo identifico 
      If Not System.IO.File.Exists(oApp.ChildImageDir & "\" & strImage & ".gif") Then strImage = ""
      If strImage <> "" Then
        pcImage.Image = Bitmap.FromFile(oApp.ChildImageDir & "\" & strImage & ".gif")
      End If
    End Set
  End Property

  Public Property VisNote() As Boolean
    Get
      If Me.Height < 100 Then Return False Else Return True
    End Get
    Set(ByVal value As Boolean)
      If value Then Me.Height = 150 Else Me.Height = 46
    End Set
  End Property

  Public Overridable Sub NTSXXNODO_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Enter
    pcImage.BackColor = Color.LightGreen
  End Sub

  Public Overridable Sub NTSXXNODO_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave
    pcImage.BackColor = Color.Black
  End Sub
End Class
