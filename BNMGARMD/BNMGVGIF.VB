Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGVGIF
#Region "Variabili"
  Public oCleArmd As CLEMGARMD
  Public oCallParams As CLE__CLDP
  Public components As System.ComponentModel.IContainer

  Public nimgW As Integer
  Public nimgH As Integer
  Public dProp As Decimal

  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents pnTop As NTSInformatica.NTSPanel
  Public WithEvents cmdEsci As NTSInformatica.NTSButton

  Public WithEvents imArtGif As NTSInformatica.NTSPictureBox
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
    'creo e attivo l'entity e inizializzo la funzione che dovr√† rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGARMD", "BEMGARMD", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128550728307822408, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
      Return False
    End If
    oCleArmd = CType(oTmp, CLEMGARMD)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGARMD", strRemoteServer, strRemotePort)
    AddHandler oCleArmd.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleArmd.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    oCleArmd.strPrgParent = oCallParams.strPar1
    oCleArmd.dVGifQuale = oCallParams.dPar1
    oCleArmd.strVGifPath = oCallParams.strPar2
    oCleArmd.nAltezzaMaxImg = NTSCInt(oCallParams.strPar3)

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGVGIF))
    Me.imArtGif = New NTSInformatica.NTSPictureBox
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.cmdEsci = New NTSInformatica.NTSButton
    CType(Me.imArtGif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
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
    'imArtGif
    '
    Me.imArtGif.AllowDrop = True
    Me.imArtGif.Cursor = System.Windows.Forms.Cursors.Default
    Me.imArtGif.Dock = System.Windows.Forms.DockStyle.Fill
    Me.imArtGif.Location = New System.Drawing.Point(0, 34)
    Me.imArtGif.Name = "imArtGif"
    Me.imArtGif.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.imArtGif.Properties.Appearance.Options.UseBackColor = True
    Me.imArtGif.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.imArtGif.Properties.ShowMenu = False
    Me.imArtGif.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
    Me.imArtGif.Size = New System.Drawing.Size(389, 198)
    Me.imArtGif.TabIndex = 4
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.cmdEsci)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(389, 34)
    Me.pnTop.TabIndex = 5
    Me.pnTop.Text = "NtsPanel1"
    '
    'cmdEsci
    '
    Me.cmdEsci.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEsci.ImageText = ""
    Me.cmdEsci.Location = New System.Drawing.Point(311, 3)
    Me.cmdEsci.Name = "cmdEsci"
    Me.cmdEsci.Size = New System.Drawing.Size(75, 26)
    Me.cmdEsci.TabIndex = 0
    Me.cmdEsci.Text = "Esci"
    '
    'FRMMGVGIF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(389, 232)
    Me.Controls.Add(Me.imArtGif)
    Me.Controls.Add(Me.pnTop)
    Me.Name = "FRMMGVGIF"
    Me.Text = "IMMAGINE"
    CType(Me.imArtGif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try


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

#Region "EventiForm"
  Public Overridable Sub FRMMGVGIF_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      '-------------------------------------------------
      'carico l' immagine nella form
      Try
        'per pilotare l'avvio in touch mode, facendo apparire la tastiera a richiesta e non sempre in basso
        Dim oControls As Object = Me.NTSFindControlByName(Me, "pnKeyb")
        If Not oControls Is Nothing Then
          Dim pnKeyb As NTSPanel = CType(oControls, NTSPanel)

          CType(Me.NTSFindControlByName(Me, "pnTouch"), NTSPanel).Height += 42

          pnKeyb.Height = 0
        End If

        'imArtGif.Image = Bitmap.FromFile(oCleArmd.strVGifPath)
        Dim fs As System.IO.FileStream
        fs = New System.IO.FileStream(oCleArmd.strVGifPath, IO.FileMode.Open, IO.FileAccess.Read)
        imArtGif.Image = System.Drawing.Image.FromStream(fs)
        fs.Close()

        'memorizzo le dimensioni dell'immagine(fisse)
        If oApp.InputMode <> "STD" And Me.TouchMode Then Return

        nimgW = imArtGif.Image.Width
        nimgH = imArtGif.Image.Height

        dProp = NTSCDec(nimgW / nimgH)

        If oCleArmd.nAltezzaMaxImg > 0 Then
          Me.Height = oCleArmd.nAltezzaMaxImg
          RidimensionaLarghezza()
        Else
          Me.Height = nimgH
          RidimensionaLarghezza()
        End If

      Catch ex As Exception
        Me.Close()
        '  se non c'Ë una immagine esco
      End Try

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub FRMMGVGIF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      If CLN__STD.IsBis = False Then pnTop.Visible = False
      If oApp.InputMode <> "STD" Then Me.bTouchModeManual = True
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If oCleArmd.dVGifQuale = 1 Then
        Me.Text = "Immagine di tipo Catalogo"
      Else
        Me.Text = "Immagine di tipo Scheda"
      End If

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  'Public Overridable Sub FRMMGVGIF_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
  '  Try

  '    'Me.Width = nimgW
  '    'Me.Height = nimgH
  '    If imArtGif.Image Is Nothing Then Return

  '    imgW = imArtGif.Image.Width
  '    imgH = imArtGif.Image.Height

  '    'Dim maxImage As Integer = 200
  '    'Dim newimgw As Integer
  '    'Dim newimgh As Integer
  '    'If imgw > imgh Then
  '    '  newimgw = maxImage
  '    '  newimgh = NTSCInt((imgH * maxImage) / imgW)
  '    'ElseIf imgh > imgw Then
  '    '  newimgw = NTSCInt((imgW * maxImage) / imgH)
  '    '  newimgh = maxImage
  '    'Else
  '    '  newimgw = maxImage
  '    '  newimgh = maxImage
  '    'End If

  '  Catch ex As Exception
  '    Me.Cursor = Cursors.Default
  '    '-------------------------------------------------
  '    Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
  '    '-------------------------------------------------
  '  End Try
  'End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
    Me.Close()
  End Sub
#End Region

  'Public Overridable Sub FRMMGVGIF_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
  '  Try

  '  Catch ex As Exception
  '    '-------------------------------------------------
  '    Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
  '    '-------------------------------------------------
  '  End Try
  'End Sub

  Public Overridable Sub FRMMGVGIF_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
    Try
      RidimensionaLarghezza()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub RidimensionaLarghezza()
    Try
      'controllo se eccede le dimensioni max
      If oCleArmd.nAltezzaMaxImg > 0 And Me.Height > oCleArmd.nAltezzaMaxImg Then Me.Height = oCleArmd.nAltezzaMaxImg

      If dProp <> NTSCDec(Me.Width / Me.Height) Then
        Me.Width = nimgW + NTSCInt(dProp * (Me.Height - nimgH))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub cmdEsci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEsci.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class