Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports mshtml
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class FRM__HIMG

  Public oPar As CLE__CLDP      'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public strHelpDirectoryLocal As String = ""

#Region "Inizializzazione"
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

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__HIMG))
    Me.pnDescr = New NTSInformatica.NTSPanel
    Me.tsHelp = New NTSInformatica.NTSTabControl
    Me.TabPage0 = New NTSInformatica.NTSTabPage
    Me.pnOrigine = New NTSInformatica.NTSPanel
    Me.pnIMG = New NTSInformatica.NTSPanel
    Me.imArtGif = New NTSInformatica.NTSPictureBox
    Me.TabPage1 = New NTSInformatica.NTSTabPage
    Me.pnDestinazione = New NTSInformatica.NTSPanel
    Me.pnIMGDest = New NTSInformatica.NTSPanel
    Me.imArtGifDest = New NTSInformatica.NTSPictureBox
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.pnAction = New NTSInformatica.NTSPanel
    Me.lbSostitusci = New NTSInformatica.NTSLabel
    Me.cmdClipSost = New NTSInformatica.NTSButton
    Me.lbInserisci = New NTSInformatica.NTSLabel
    Me.cmdSfogliaSost = New NTSInformatica.NTSButton
    Me.cmdInsIMGAppunti = New NTSInformatica.NTSButton
    Me.cmdInserisci = New NTSInformatica.NTSButton
    Me.cmdPrec = New NTSInformatica.NTSButton
    Me.cmdSucc = New NTSInformatica.NTSButton
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDescr.SuspendLayout()
    CType(Me.tsHelp, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsHelp.SuspendLayout()
    Me.TabPage0.SuspendLayout()
    CType(Me.pnOrigine, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnOrigine.SuspendLayout()
    CType(Me.pnIMG, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnIMG.SuspendLayout()
    CType(Me.imArtGif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage1.SuspendLayout()
    CType(Me.pnDestinazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDestinazione.SuspendLayout()
    CType(Me.pnIMGDest, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnIMGDest.SuspendLayout()
    CType(Me.imArtGifDest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAction.SuspendLayout()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'pnDescr
    '
    Me.pnDescr.AllowDrop = True
    Me.pnDescr.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDescr.Appearance.Options.UseBackColor = True
    Me.pnDescr.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDescr.Controls.Add(Me.tsHelp)
    Me.pnDescr.Controls.Add(Me.pnAction)
    Me.pnDescr.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDescr.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnDescr.Location = New System.Drawing.Point(0, 0)
    Me.pnDescr.Name = "pnDescr"
    Me.pnDescr.Size = New System.Drawing.Size(788, 486)
    Me.pnDescr.TabIndex = 1
    '
    'tsHelp
    '
    Me.tsHelp.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsHelp.Location = New System.Drawing.Point(0, 0)
    Me.tsHelp.Name = "tsHelp"
    Me.tsHelp.SelectedTabPage = Me.TabPage0
    Me.tsHelp.Size = New System.Drawing.Size(788, 386)
    Me.tsHelp.TabIndex = 9
    Me.tsHelp.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabPage0, Me.TabPage1})
    '
    'TabPage0
    '
    Me.TabPage0.AllowDrop = True
    Me.TabPage0.Controls.Add(Me.pnOrigine)
    Me.TabPage0.Enable = True
    Me.TabPage0.Name = "TabPage0"
    Me.TabPage0.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage0.Size = New System.Drawing.Size(779, 356)
    Me.TabPage0.Text = "Già usate nell'argomento"
    '
    'pnOrigine
    '
    Me.pnOrigine.AllowDrop = True
    Me.pnOrigine.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnOrigine.Appearance.Options.UseBackColor = True
    Me.pnOrigine.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnOrigine.Controls.Add(Me.pnIMG)
    Me.pnOrigine.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnOrigine.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnOrigine.Location = New System.Drawing.Point(3, 3)
    Me.pnOrigine.Name = "pnOrigine"
    Me.pnOrigine.Size = New System.Drawing.Size(773, 350)
    Me.pnOrigine.TabIndex = 0
    Me.pnOrigine.Text = "NtsPanel1"
    '
    'pnIMG
    '
    Me.pnIMG.AllowDrop = True
    Me.pnIMG.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnIMG.Appearance.Options.UseBackColor = True
    Me.pnIMG.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnIMG.Controls.Add(Me.imArtGif)
    Me.pnIMG.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnIMG.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnIMG.Location = New System.Drawing.Point(0, 0)
    Me.pnIMG.Name = "pnIMG"
    Me.pnIMG.Size = New System.Drawing.Size(773, 350)
    Me.pnIMG.TabIndex = 4
    Me.pnIMG.Text = "NtsPanel1"
    '
    'imArtGif
    '
    Me.imArtGif.AllowDrop = True
    Me.imArtGif.Cursor = System.Windows.Forms.Cursors.Default
    Me.imArtGif.Location = New System.Drawing.Point(0, 0)
    Me.imArtGif.Name = "imArtGif"
    Me.imArtGif.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.imArtGif.Properties.Appearance.Options.UseBackColor = True
    Me.imArtGif.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.imArtGif.Properties.ShowMenu = False
    Me.imArtGif.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
    Me.imArtGif.Size = New System.Drawing.Size(188, 142)
    Me.imArtGif.TabIndex = 5
    '
    'TabPage1
    '
    Me.TabPage1.AllowDrop = True
    Me.TabPage1.Controls.Add(Me.pnDestinazione)
    Me.TabPage1.Enable = False
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage1.PageEnabled = False
    Me.TabPage1.Size = New System.Drawing.Size(779, 356)
    '
    'pnDestinazione
    '
    Me.pnDestinazione.AllowDrop = True
    Me.pnDestinazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDestinazione.Appearance.Options.UseBackColor = True
    Me.pnDestinazione.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDestinazione.Controls.Add(Me.pnIMGDest)
    Me.pnDestinazione.Controls.Add(Me.cmdAnnulla)
    Me.pnDestinazione.Controls.Add(Me.cmdConferma)
    Me.pnDestinazione.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDestinazione.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnDestinazione.Location = New System.Drawing.Point(3, 3)
    Me.pnDestinazione.Name = "pnDestinazione"
    Me.pnDestinazione.Size = New System.Drawing.Size(773, 350)
    Me.pnDestinazione.TabIndex = 1
    Me.pnDestinazione.Text = "NtsPanel1"
    '
    'pnIMGDest
    '
    Me.pnIMGDest.AllowDrop = True
    Me.pnIMGDest.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnIMGDest.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnIMGDest.Appearance.Options.UseBackColor = True
    Me.pnIMGDest.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnIMGDest.Controls.Add(Me.imArtGifDest)
    Me.pnIMGDest.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnIMGDest.Location = New System.Drawing.Point(0, 0)
    Me.pnIMGDest.Name = "pnIMGDest"
    Me.pnIMGDest.Size = New System.Drawing.Size(773, 318)
    Me.pnIMGDest.TabIndex = 4
    Me.pnIMGDest.Text = "NtsPanel1"
    '
    'imArtGifDest
    '
    Me.imArtGifDest.AllowDrop = True
    Me.imArtGifDest.Cursor = System.Windows.Forms.Cursors.Default
    Me.imArtGifDest.Location = New System.Drawing.Point(0, 0)
    Me.imArtGifDest.Name = "imArtGifDest"
    Me.imArtGifDest.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.imArtGifDest.Properties.Appearance.Options.UseBackColor = True
    Me.imArtGifDest.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.imArtGifDest.Properties.ShowMenu = False
    Me.imArtGifDest.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
    Me.imArtGifDest.Size = New System.Drawing.Size(188, 142)
    Me.imArtGifDest.TabIndex = 5
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(110, 322)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(98, 27)
    Me.cmdAnnulla.TabIndex = 10
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdConferma
    '
    Me.cmdConferma.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdConferma.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(6, 322)
    Me.cmdConferma.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(98, 27)
    Me.cmdConferma.TabIndex = 10
    Me.cmdConferma.Text = "Conferma"
    '
    'pnAction
    '
    Me.pnAction.AllowDrop = True
    Me.pnAction.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAction.Appearance.Options.UseBackColor = True
    Me.pnAction.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAction.Controls.Add(Me.lbSostitusci)
    Me.pnAction.Controls.Add(Me.cmdClipSost)
    Me.pnAction.Controls.Add(Me.lbInserisci)
    Me.pnAction.Controls.Add(Me.cmdSfogliaSost)
    Me.pnAction.Controls.Add(Me.cmdInsIMGAppunti)
    Me.pnAction.Controls.Add(Me.cmdInserisci)
    Me.pnAction.Controls.Add(Me.cmdPrec)
    Me.pnAction.Controls.Add(Me.cmdSucc)
    Me.pnAction.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAction.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnAction.Location = New System.Drawing.Point(0, 386)
    Me.pnAction.Name = "pnAction"
    Me.pnAction.Size = New System.Drawing.Size(788, 100)
    Me.pnAction.TabIndex = 3
    '
    'lbSostitusci
    '
    Me.lbSostitusci.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbSostitusci.AutoSize = True
    Me.lbSostitusci.BackColor = System.Drawing.Color.Transparent
    Me.lbSostitusci.Enabled = False
    Me.lbSostitusci.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbSostitusci.Location = New System.Drawing.Point(423, 14)
    Me.lbSostitusci.Name = "lbSostitusci"
    Me.lbSostitusci.NTSDbField = ""
    Me.lbSostitusci.Size = New System.Drawing.Size(62, 13)
    Me.lbSostitusci.TabIndex = 11
    Me.lbSostitusci.Text = "Sostitusci"
    Me.lbSostitusci.Tooltip = ""
    Me.lbSostitusci.UseMnemonic = False
    Me.lbSostitusci.Visible = False
    '
    'cmdClipSost
    '
    Me.cmdClipSost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdClipSost.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdClipSost.Enabled = False
    Me.cmdClipSost.ImageText = ""
    Me.cmdClipSost.Location = New System.Drawing.Point(400, 62)
    Me.cmdClipSost.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdClipSost.Name = "cmdClipSost"
    Me.cmdClipSost.Size = New System.Drawing.Size(98, 27)
    Me.cmdClipSost.TabIndex = 10
    Me.cmdClipSost.Text = "Da appunti"
    Me.cmdClipSost.Visible = False
    '
    'lbInserisci
    '
    Me.lbInserisci.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lbInserisci.AutoSize = True
    Me.lbInserisci.BackColor = System.Drawing.Color.Transparent
    Me.lbInserisci.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.lbInserisci.Location = New System.Drawing.Point(31, 14)
    Me.lbInserisci.Name = "lbInserisci"
    Me.lbInserisci.NTSDbField = ""
    Me.lbInserisci.Size = New System.Drawing.Size(55, 13)
    Me.lbInserisci.TabIndex = 11
    Me.lbInserisci.Text = "Inserisci"
    Me.lbInserisci.Tooltip = ""
    Me.lbInserisci.UseMnemonic = False
    '
    'cmdSfogliaSost
    '
    Me.cmdSfogliaSost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSfogliaSost.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdSfogliaSost.Enabled = False
    Me.cmdSfogliaSost.ImageText = ""
    Me.cmdSfogliaSost.Location = New System.Drawing.Point(400, 33)
    Me.cmdSfogliaSost.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdSfogliaSost.Name = "cmdSfogliaSost"
    Me.cmdSfogliaSost.Size = New System.Drawing.Size(98, 27)
    Me.cmdSfogliaSost.TabIndex = 10
    Me.cmdSfogliaSost.Text = "Sfoglia ..."
    Me.cmdSfogliaSost.Visible = False
    '
    'cmdInsIMGAppunti
    '
    Me.cmdInsIMGAppunti.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdInsIMGAppunti.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdInsIMGAppunti.ImageText = ""
    Me.cmdInsIMGAppunti.Location = New System.Drawing.Point(8, 62)
    Me.cmdInsIMGAppunti.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdInsIMGAppunti.Name = "cmdInsIMGAppunti"
    Me.cmdInsIMGAppunti.Size = New System.Drawing.Size(98, 27)
    Me.cmdInsIMGAppunti.TabIndex = 10
    Me.cmdInsIMGAppunti.Text = "Da appunti"
    '
    'cmdInserisci
    '
    Me.cmdInserisci.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdInserisci.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdInserisci.ImageText = ""
    Me.cmdInserisci.Location = New System.Drawing.Point(8, 33)
    Me.cmdInserisci.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdInserisci.Name = "cmdInserisci"
    Me.cmdInserisci.Size = New System.Drawing.Size(98, 27)
    Me.cmdInserisci.TabIndex = 10
    Me.cmdInserisci.Text = "Sfoglia ..."
    '
    'cmdPrec
    '
    Me.cmdPrec.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdPrec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdPrec.Image = CType(resources.GetObject("cmdPrec.Image"), System.Drawing.Image)
    Me.cmdPrec.ImageText = ""
    Me.cmdPrec.Location = New System.Drawing.Point(504, 33)
    Me.cmdPrec.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdPrec.Name = "cmdPrec"
    Me.cmdPrec.Size = New System.Drawing.Size(134, 56)
    Me.cmdPrec.TabIndex = 10
    Me.cmdPrec.Text = "Precedente"
    '
    'cmdSucc
    '
    Me.cmdSucc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSucc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdSucc.Image = CType(resources.GetObject("cmdSucc.Image"), System.Drawing.Image)
    Me.cmdSucc.ImageText = ""
    Me.cmdSucc.Location = New System.Drawing.Point(642, 33)
    Me.cmdSucc.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdSucc.Name = "cmdSucc"
    Me.cmdSucc.Size = New System.Drawing.Size(134, 56)
    Me.cmdSucc.TabIndex = 7
    Me.cmdSucc.Text = "Successiva"
    '
    'FRM__HIMG
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.ClientSize = New System.Drawing.Size(788, 486)
    Me.Controls.Add(Me.pnDescr)
    Me.Name = "FRM__HIMG"
    Me.Text = "GESTIONE IMMAGINI"
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDescr.ResumeLayout(False)
    CType(Me.tsHelp, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsHelp.ResumeLayout(False)
    Me.TabPage0.ResumeLayout(False)
    CType(Me.pnOrigine, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnOrigine.ResumeLayout(False)
    CType(Me.pnIMG, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnIMG.ResumeLayout(False)
    CType(Me.imArtGif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage1.ResumeLayout(False)
    CType(Me.pnDestinazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDestinazione.ResumeLayout(False)
    CType(Me.pnIMGDest, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnIMGDest.ResumeLayout(False)
    CType(Me.imArtGifDest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAction.ResumeLayout(False)
    Me.pnAction.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
#End Region

  Public docHTML As IHTMLDocument2
  Public WithEvents WebBrows As NTSInformatica.NTSWebBrowser
  Public nImmaginiValide As Integer = 0
#Region "Eventi di FORM"
  Public Overridable Sub FRM__HIMG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      InitControls()


      docHTML = CType(WebBrows.Document.DomDocument, IHTMLDocument2)
      If docHTML.images.length > 0 Then
        Dim oimageHTMLTmp(docHTML.images.length - 1) As mshtml.HTMLImgClass
        For i As Integer = 0 To docHTML.images.length - 1
          If Not CType(docHTML.images.item(i, 0), mshtml.HTMLImgClass).nameProp.StartsWith("help_home_") Then
            oimageHTMLTmp(nImmaginiValide) = CType(docHTML.images.item(i, 0), mshtml.HTMLImgClass)
            nImmaginiValide = nImmaginiValide + 1
          End If
        Next
        oimageHTML = oimageHTMLTmp
      Else
        oimageHTML = Nothing
      End If

      nPosIMG = 0
      imArtGif.Visible = False
      VisualizzaImmagine()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region
  Public oimageHTML() As mshtml.HTMLImgClass
  Public fs As System.IO.FileStream
  Public nPosIMG As Integer
  Public Overridable Sub VisualizzaImmagine()
    Dim strGif1 As String = ""

    Try
      If Not nImmaginiValide > 0 Then Return
      imArtGif.Visible = False

      strGif1 = oimageHTML(nPosIMG).src
      If strGif1.Contains("file:///") Then
        strGif1 = strGif1.Replace("file:///", "").Replace("/", "\")
      End If
      If oimageHTML(nPosIMG).height <= pnIMG.Height Then
        If oimageHTML(nPosIMG).width <= pnIMG.Width Then
          imArtGif.Height = oimageHTML(nPosIMG).height
          imArtGif.Width = oimageHTML(nPosIMG).width
        Else
          If NTSCInt((oimageHTML(nPosIMG).height / oimageHTML(nPosIMG).width) * pnIMG.Width) <= pnIMG.Height Then
            imArtGif.Width = pnIMG.Width
            imArtGif.Height = NTSCInt((oimageHTML(nPosIMG).height / oimageHTML(nPosIMG).width) * pnIMG.Width)
          Else
            imArtGif.Height = pnIMG.Height
            imArtGif.Width = NTSCInt((oimageHTML(nPosIMG).width / oimageHTML(nPosIMG).height) * pnIMG.Height)
          End If
        End If
      Else
        If NTSCInt((oimageHTML(nPosIMG).height / oimageHTML(nPosIMG).width) * pnIMG.Width) <= pnIMG.Height Then
          imArtGif.Width = pnIMG.Width
          imArtGif.Height = NTSCInt((oimageHTML(nPosIMG).height / oimageHTML(nPosIMG).width) * pnIMG.Width)
        Else
          imArtGif.Height = pnIMG.Height
          imArtGif.Width = NTSCInt((oimageHTML(nPosIMG).width / oimageHTML(nPosIMG).height) * pnIMG.Height)
        End If
      End If

      If File.Exists(strGif1) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      fs = New System.IO.FileStream(strGif1, IO.FileMode.Open, IO.FileAccess.Read)
      imArtGif.Image = System.Drawing.Image.FromStream(fs)
      imArtGif.Visible = True
      fs.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Try
        fs.Close()
      Catch ex2 As Exception
      End Try
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdPrec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrec.Click
    If nPosIMG > 0 Then nPosIMG = nPosIMG - 1
    VisualizzaImmagine()
  End Sub
  Public Overridable Sub cmdSucc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSucc.Click
    If nPosIMG < nImmaginiValide - 1 Then nPosIMG = nPosIMG + 1
    VisualizzaImmagine()
  End Sub

  Public strIMGDaElaborare As String = ""
  Public bIMGDaAppunti As Boolean = False
  Public bIMGSostituisci As Boolean = False
  Private Sub cmdInserisci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInserisci.Click
    Dim ofdImg As New NTSOpenFileDialog
    Dim strPageHTML As String
    Try
      bIMGSostituisci = False
      strPageHTML = WebBrows.Document.Url.LocalPath.Substring(WebBrows.Document.Url.LocalPath.LastIndexOf("\") + 1)
      strPageHTML = strPageHTML.Substring(0, strPageHTML.IndexOf("."))
      ofdImg.Filter = "Immagini GIF o JPG|*.gif;*.jpg"
      'ofdImg.Filter = "Immagini GIF|*.gif"
      ofdImg.InitialDirectory = strHelpDirectoryLocal & "\Help\" & "images"
      ofdImg.oMenu = oMenu
      If ofdImg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        If File.Exists(ofdImg.FileName) = False Then Return

        Dim oFileImgIns As New System.IO.FileInfo(ofdImg.FileName)
        strIMGDaElaborare = oFileImgIns.FullName

        TabPage1.Text = "Inserisci"
        TabPage1.Enable = True
        TabPage0.Enable = False
        tsHelp.SelectedTabPageIndex = 1
        pnAction.Enabled = False
        '--------------------------------------------------------------------------------------------------------------
        fs = New System.IO.FileStream(strIMGDaElaborare, IO.FileMode.Open, IO.FileAccess.Read)
        Dim bMap As Image = Image.FromStream(fs)

        If bMap.Height <= pnIMGDest.Height Then
          If bMap.Width <= pnIMGDest.Width Then
            imArtGifDest.Height = bMap.Height
            imArtGifDest.Width = bMap.Width
          Else
            If NTSCInt((bMap.Height / bMap.Width) * pnIMGDest.Width) <= pnIMGDest.Height Then
              imArtGifDest.Width = pnIMGDest.Width
              imArtGifDest.Height = NTSCInt((bMap.Height / bMap.Width) * pnIMGDest.Width)
            Else
              imArtGifDest.Height = pnIMGDest.Height
              imArtGifDest.Width = NTSCInt((bMap.Width / bMap.Height) * pnIMGDest.Height)
            End If
          End If
        Else
          If NTSCInt((bMap.Height / bMap.Width) * pnIMGDest.Width) <= pnIMGDest.Height Then
            imArtGifDest.Width = pnIMGDest.Width
            imArtGifDest.Height = NTSCInt((bMap.Height / bMap.Width) * pnIMGDest.Width)
          Else
            imArtGifDest.Height = pnIMGDest.Height
            imArtGifDest.Width = NTSCInt((bMap.Width / bMap.Height) * pnIMGDest.Height)
          End If
        End If

        imArtGifDest.Image = bMap
        fs.Close()

      End If
      ofdImg.Dispose()
      ofdImg = Nothing
    Catch ex As Exception
      Try
        fs.Close()
      Catch ex2 As Exception
      End Try
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Function OttinieniNomeFileDisponibile(ByVal strTipo As String, ByRef strNome As String) As Boolean
    Dim strFileGIF As String
    Dim strFileJPG As String
    Dim strPageHTML As String
    Try
      strPageHTML = WebBrows.Document.Url.LocalPath.Substring(WebBrows.Document.Url.LocalPath.LastIndexOf("\") + 1)
      strPageHTML = strPageHTML.Substring(0, strPageHTML.IndexOf("."))
      For i As Integer = 1 To 99
        strFileGIF = strHelpDirectoryLocal & "\Help\" & "images" & "\help_" & strPageHTML & "_" & i.ToString.PadLeft(2, "0"c) & ".gif"
        strFileJPG = strHelpDirectoryLocal & "\Help\" & "images" & "\help_" & strPageHTML & "_" & i.ToString.PadLeft(2, "0"c) & ".jpg"
        If File.Exists(strFileGIF) = False And File.Exists(strFileJPG) = False Then
          Select Case strTipo
            Case ".gif"
              strNome = strFileGIF
              Return True
            Case ".jpg"
              strNome = strFileJPG
              Return True
            Case Else
          End Select
          Exit For
        End If
      Next
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Sub cmdInsIMGAppunti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInsIMGAppunti.Click
    Try
      bIMGSostituisci = False
      Dim data As IDataObject
      data = Clipboard.GetDataObject()
      Dim bmap As Bitmap
      If data.GetDataPresent(GetType(Bitmap)) Then
        bmap = CType(data.GetData(GetType(Bitmap)), Bitmap)
        Dim myImageCodecInfo As ImageCodecInfo
        Dim myEncoder As Encoder
        ' Dim myEncoderParameter As EncoderParameter
        Dim myEncoderParameters As EncoderParameters
        ' Get an ImageCodecInfo object that represents the JPEG codec.
        myImageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg)

        ' Create an Encoder object based on the GUID
        ' for the Quality parameter category.
        myEncoder = Encoder.Quality

        ' Create an EncoderParameters object.
        ' An EncoderParameters object has an array of EncoderParameter
        ' objects. In this case, there is only one
        ' EncoderParameter object in the array.
        myEncoderParameters = New EncoderParameters(2)

        '' Save the bitmap as a JPEG file with quality level 25.
        'myEncoderParameter = New EncoderParameter(myEncoder, CType(100L, Int32))
        'myEncoderParameters.Param(0) = myEncoderParameter

        myEncoderParameters.Param(0) = New EncoderParameter(Encoder.ColorDepth, CType(24L, Int32))
        myEncoderParameters.Param(1) = New EncoderParameter(Encoder.Quality, CType(100L, Int32))
        'myEncoderParameters.Param(1) = New EncoderParameter(Encoder.Version, EncoderValue.VersionGif89)
        'myEncoderParameters.Param(2) = New EncoderParameter(Encoder.ScanMethod, EncoderValue.ScanMethodNonInterlaced)

        strIMGDaElaborare = oApp.AscDir & "\help_tmp.jpg"
        bIMGDaAppunti = True
        If File.Exists(strIMGDaElaborare) Then File.Delete(strIMGDaElaborare)
        bmap.Save(strIMGDaElaborare, myImageCodecInfo, myEncoderParameters)

        If bmap.Height <= pnIMGDest.Height Then
          If bmap.Width <= pnIMGDest.Width Then
            imArtGifDest.Height = bmap.Height
            imArtGifDest.Width = bmap.Width
          Else
            If NTSCInt((bmap.Height / bmap.Width) * pnIMGDest.Width) <= pnIMGDest.Height Then
              imArtGifDest.Width = pnIMGDest.Width
              imArtGifDest.Height = NTSCInt((bmap.Height / bmap.Width) * pnIMGDest.Width)
            Else
              imArtGifDest.Height = pnIMGDest.Height
              imArtGifDest.Width = NTSCInt((bmap.Width / bmap.Height) * pnIMGDest.Height)
            End If
          End If
        Else
          If NTSCInt((bmap.Height / bmap.Width) * pnIMGDest.Width) <= pnIMGDest.Height Then
            imArtGifDest.Width = pnIMGDest.Width
            imArtGifDest.Height = NTSCInt((bmap.Height / bmap.Width) * pnIMGDest.Width)
          Else
            imArtGifDest.Height = pnIMGDest.Height
            imArtGifDest.Width = NTSCInt((bmap.Width / bmap.Height) * pnIMGDest.Height)
          End If
        End If

        If File.Exists(strIMGDaElaborare) = False Then Return
        TabPage1.Text = "Inserisci"
        TabPage1.Enable = True
        tsHelp.SelectedTabPageIndex = 1
        TabPage0.Enable = False
        pnAction.Enabled = False
        '--------------------------------------------------------------------------------------------------------------
        fs = New System.IO.FileStream(strIMGDaElaborare, IO.FileMode.Open, IO.FileAccess.Read)
        imArtGifDest.Image = Image.FromStream(fs)
        fs.Close()

      End If
    Catch ex As Exception
      Try
        fs.Close()
      Catch ex2 As Exception
      End Try
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Function GetEncoderInfo(ByVal format As ImageFormat) As ImageCodecInfo
    Dim j As Integer
    Dim encoders() As ImageCodecInfo
    encoders = ImageCodecInfo.GetImageEncoders()

    j = 0
    While j < encoders.Length
      If encoders(j).FormatID = format.Guid Then
        Return encoders(j)
      End If
      j += 1
    End While
    Return Nothing

  End Function 'GetEncoderInfo

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Dim strFile As String = ""
    Try
      Dim oFileImgIns As New IO.FileInfo(strIMGDaElaborare)
      If bIMGDaAppunti Then
        OttinieniNomeFileDisponibile(oFileImgIns.Extension.ToLower, strFile)
        oFileImgIns.CopyTo(strFile)

      Else

        'da file
        If Not (oFileImgIns.FullName.ToLower.StartsWith((strHelpDirectoryLocal & "\Help\" & "images").ToLower)) Then
          If bIMGSostituisci Then

            strFile = oimageHTML(nPosIMG).src
            If strFile.Contains("file:///") Then strFile = strFile.Replace("file:///", "").Replace("/", "\")
            If File.Exists(strFile) = False Then Return

          Else
            OttinieniNomeFileDisponibile(oFileImgIns.Extension.ToLower, strFile)
          End If
          oFileImgIns.CopyTo(strFile)
        Else
          If Not (oFileImgIns.Name.ToLower.StartsWith("help_".ToLower)) Then
            If bIMGSostituisci Then

              strFile = oimageHTML(nPosIMG).src
              If strFile.Contains("file:///") Then strFile = strFile.Replace("file:///", "").Replace("/", "\")
              If File.Exists(strFile) = False Then Return

            Else
              OttinieniNomeFileDisponibile(oFileImgIns.Extension.ToLower, strFile)
            End If
            oFileImgIns.CopyTo(strFile)
            oFileImgIns.IsReadOnly = False
            oFileImgIns.Delete()
          Else
            'è già ok
            If bIMGSostituisci Then

              strFile = oimageHTML(nPosIMG).src
              If strFile.Contains("file:///") Then strFile = strFile.Replace("file:///", "").Replace("/", "\")
              If File.Exists(strFile) = False Then Return

            Else

              strFile = oFileImgIns.FullName

            End If
          End If
        End If

      End If

      oFileImgIns = New IO.FileInfo(strFile)
      If oFileImgIns.Exists Then
        oPar.strPar2 = oFileImgIns.FullName
        Dim strWidht As String = ""
        If oFileImgIns.Extension.ToLower = ".jpg" Then
          Try
            Dim fsW As System.IO.FileStream
            fsW = New System.IO.FileStream(strFile, IO.FileMode.Open, IO.FileAccess.Read)
            Dim bMap As Image = Image.FromStream(fsW)
            If bMap.Width > 987 Then
              strWidht = " width=987"
            End If
            fsW.Close()
          Catch exW As Exception
          End Try
        End If
        oPar.strPar1 = "<IMG class=calone alt="""" src=""../images/" & oFileImgIns.Name & """" & strWidht & ">"
        oPar.bPar1 = True
        Me.Close()
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oPar.bPar1 = False
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdSfogliaSost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSfogliaSost.Click
    Dim ofdImg As New NTSOpenFileDialog
    Dim strPageHTML As String
    Try
      bIMGSostituisci = True
      strPageHTML = WebBrows.Document.Url.LocalPath.Substring(WebBrows.Document.Url.LocalPath.LastIndexOf("\") + 1)
      strPageHTML = strPageHTML.Substring(0, strPageHTML.IndexOf("."))
      'ofdImg.Filter = "Immagini GIF o JPG|*.gif;*.jpg"
      ofdImg.Filter = "Immagini GIF|*.gif"
      ofdImg.InitialDirectory = strHelpDirectoryLocal & "\Help\" & "images"
      ofdImg.oMenu = oMenu
      If ofdImg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        If File.Exists(ofdImg.FileName) = False Then Return

        Dim oFileImgIns As New System.IO.FileInfo(ofdImg.FileName)
        strIMGDaElaborare = oFileImgIns.FullName

        TabPage1.Text = "Sostituisci"
        TabPage1.Enable = True
        TabPage0.Enable = True
        tsHelp.SelectedTabPageIndex = 1
        pnAction.Enabled = False
        '--------------------------------------------------------------------------------------------------------------
        fs = New System.IO.FileStream(strIMGDaElaborare, IO.FileMode.Open, IO.FileAccess.Read)
        Dim bMap As Image = Image.FromStream(fs)

        If bMap.Height <= pnIMGDest.Height Then
          If bMap.Width <= pnIMGDest.Width Then
            imArtGifDest.Height = bMap.Height
            imArtGifDest.Width = bMap.Width
          Else
            If NTSCInt((bMap.Height / bMap.Width) * pnIMGDest.Width) <= pnIMGDest.Height Then
              imArtGifDest.Width = pnIMGDest.Width
              imArtGifDest.Height = NTSCInt((bMap.Height / bMap.Width) * pnIMGDest.Width)
            Else
              imArtGifDest.Height = pnIMGDest.Height
              imArtGifDest.Width = NTSCInt((bMap.Width / bMap.Height) * pnIMGDest.Height)
            End If
          End If
        Else
          If NTSCInt((bMap.Height / bMap.Width) * pnIMGDest.Width) <= pnIMGDest.Height Then
            imArtGifDest.Width = pnIMGDest.Width
            imArtGifDest.Height = NTSCInt((bMap.Height / bMap.Width) * pnIMGDest.Width)
          Else
            imArtGifDest.Height = pnIMGDest.Height
            imArtGifDest.Width = NTSCInt((bMap.Width / bMap.Height) * pnIMGDest.Height)
          End If
        End If

        imArtGifDest.Image = bMap
        fs.Close()

      End If
      ofdImg.Dispose()
      ofdImg = Nothing
    Catch ex As Exception
      Try
        fs.Close()
      Catch ex2 As Exception
      End Try
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdClipSost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClipSost.Click
    Try
      bIMGSostituisci = True

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
End Class
