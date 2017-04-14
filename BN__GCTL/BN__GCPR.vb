Imports System.Windows.Forms
Imports system.Drawing
Imports NTSInformatica
Imports NTSInformatica.CLN__STD
Imports System.Data

Public Class FRM__GCPR

  Public ctlIn As Control = Nothing
  Public barItmIn As DevExpress.XtraBars.BarItem = Nothing
  Public tbpIn As NTSTabPage = Nothing

  Public griCol As NTSGridColumn = Nothing
  Public oList(10) As Object

  Public oCleGctl As CLE__GCTL
  Public strChild As String
  Public oParam As CLE__CLDP
  Public cltBackColor As Color
  Public bCtlExt As Boolean = False     'se TRUE vuol dire che il controllo passatomi  stato caricato nel child con il source extender
  Public strPTmp As String = ""


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

      '---------------------------------
      'creo e attivo l'entity
      oCleGctl = New CLE__GCTL
      bRemoting = Menu.Remoting("BN__GCTL", strRemoteServer, strRemotePort)
      oCleGctl.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

      '---------------------------------
      'inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      AddHandler oCleGctl.RemoteEvent, AddressOf GestisciEventiEntity

      '---------------------------------
      'memorizzo i parametri passati dal child

      oParam = Param
      strChild = Param.strPar1
      If Param.tbpPar1 Is Nothing Then
        If Param.ctlPar1.GetType.ToString.IndexOf("NTSBar") > -1 Then
          barItmIn = CType(Param.ctlPar1, DevExpress.XtraBars.BarItem)
        ElseIf Param.ctlPar1.GetType.ToString.IndexOf("NTSGridColumn") > -1 Then
          griCol = CType(Param.ctlPar1, NTSGridColumn)
        ElseIf Param.ctlPar1.GetType.ToString.IndexOf("NTSGridView") > -1 Then
          ctlIn = CType(Param.ctlPar1, NTSGridView).GridControl
        Else
          ctlIn = CType(Param.ctlPar1, Control)
        End If
      Else
        tbpIn = CType(Param.tbpPar1, NTSTabPage)
      End If

      bCtlExt = Param.bPar1

      If Not ctlIn Is Nothing Then
        cltBackColor = ctlIn.BackColor
        If ctlIn.GetType.ToString.ToUpper.IndexOf("NTSINFORMATICA.FRM") = -1 Then
          oList(0) = ctlIn.Top
          oList(1) = ctlIn.Left
          oList(2) = ctlIn.Height
          oList(3) = ctlIn.Width
          Select Case ctlIn.GetType.ToString
            Case "NTSInformatica.NTSLabel"
              oList(5) = CType(ctlIn, NTSLabel).AutoSize
            Case "NTSInformatica.NTSCheckBox"
              oList(5) = CType(ctlIn, NTSCheckBox).AutoSize
            Case "NTSInformatica.NTSRadioButton"
              oList(5) = CType(ctlIn, NTSRadioButton).AutoSize
          End Select

          Select Case ctlIn.GetType.ToString
            Case "NTSInformatica.NTSLabel"
              oList(6) = CType(ctlIn, NTSLabel).BorderStyle
            Case "NTSInformatica.NTSPanel"
              oList(6) = CType(ctlIn, NTSPanel).BorderStyle
            Case "NTSInformatica.NTSPictureBox"
              oList(6) = CType(ctlIn, NTSPictureBox).BorderStyle
          End Select

          If ctlIn.Tag Is Nothing Then
            lbSavePosition.ForeColor = System.Drawing.SystemColors.InactiveCaption
          Else
            If ctlIn.Tag.ToString.IndexOf("G-") = -1 Then
              lbSavePosition.ForeColor = System.Drawing.SystemColors.InactiveCaption
            End If
          End If
          oList(8) = ctlIn.Anchor
          oList(9) = ctlIn.Dock
        Else
          oList(0) = ctlIn.Top
          oList(1) = ctlIn.Left
          oList(2) = CType(ctlIn, Form).Height
          oList(3) = CType(ctlIn, Form).Width
        End If
      End If

      If bCtlExt = False Then lbCancella.ForeColor = System.Drawing.SystemColors.InactiveCaption

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
      'completo le informazioni dei controlli
      edWidth.NTSSetParam(oMenu, oApp.Tr(Me, 128230023391365774, "Width"), "0", 5, 0, 99999)
      edHeight.NTSSetParam(oMenu, oApp.Tr(Me, 128230023391521947, "Height"), "0", 5, 0, 99999)
      edLeft.NTSSetParam(oMenu, oApp.Tr(Me, 128230023391678120, "Left"), "0", 5, 0, 99999)
      edTop.NTSSetParam(oMenu, oApp.Tr(Me, 128230023391834293, "Top"), "0", 5, 0, 99999)
      ckBorder.NTSSetParam(oMenu, oApp.Tr(Me, 128230023391990466, "Border"), "S", "N")
      ckAutosize.NTSSetParam(oMenu, oApp.Tr(Me, 128230023392146639, "Autosize"), "S", "N")
      ckMultiline.NTSSetParam(oMenu, oApp.Tr(Me, 128230023392302812, "Multiline"), "S", "N")
      ckVisLabel.NTSSetParam(oMenu, oApp.Tr(Me, 128230023392458985, "Visualizza Label"), "S", "N")
      edParent.NTSSetParam(oMenu, "Oggetto contenitore", 255, True)

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
    Me.lbAnchor = New System.Windows.Forms.Label
    Me.lbEsci = New System.Windows.Forms.Label
    Me.cmdAtop = New System.Windows.Forms.Label
    Me.pnAnchor = New NTSInformatica.NTSPanel
    Me.cmdAbottom = New System.Windows.Forms.Label
    Me.cmdAleft = New System.Windows.Forms.Label
    Me.cmdAright = New System.Windows.Forms.Label
    Me.lbDock = New System.Windows.Forms.Label
    Me.pnDock = New NTSInformatica.NTSPanel
    Me.cmdDnone = New System.Windows.Forms.Label
    Me.cmdDfill = New System.Windows.Forms.Label
    Me.cmdDbottom = New System.Windows.Forms.Label
    Me.cmdDleft = New System.Windows.Forms.Label
    Me.cmdDright = New System.Windows.Forms.Label
    Me.cmdDtop = New System.Windows.Forms.Label
    Me.lbSavePosition = New System.Windows.Forms.Label
    Me.lbName = New System.Windows.Forms.Label
    Me.Label1 = New System.Windows.Forms.Label
    Me.Label2 = New System.Windows.Forms.Label
    Me.Label3 = New System.Windows.Forms.Label
    Me.Label4 = New System.Windows.Forms.Label
    Me.ckMultiline = New NTSInformatica.NTSCheckBox
    Me.edTop = New NTSInformatica.NTSTextBoxNum
    Me.edLeft = New NTSInformatica.NTSTextBoxNum
    Me.edHeight = New NTSInformatica.NTSTextBoxNum
    Me.edWidth = New NTSInformatica.NTSTextBoxNum
    Me.ckAutosize = New NTSInformatica.NTSCheckBox
    Me.ckBorder = New NTSInformatica.NTSCheckBox
    Me.lbCancella = New System.Windows.Forms.Label
    Me.ckVisLabel = New NTSInformatica.NTSCheckBox
    Me.edParent = New NTSInformatica.NTSTextBoxStr
    Me.Label5 = New System.Windows.Forms.Label
    CType(Me.pnAnchor, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAnchor.SuspendLayout()
    CType(Me.pnDock, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDock.SuspendLayout()
    CType(Me.ckMultiline.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTop.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLeft.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edHeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edWidth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAutosize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckBorder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckVisLabel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edParent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'lbAnchor
    '
    Me.lbAnchor.BackColor = System.Drawing.Color.Transparent
    Me.lbAnchor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAnchor.Location = New System.Drawing.Point(0, 0)
    Me.lbAnchor.Name = "lbAnchor"
    Me.lbAnchor.Size = New System.Drawing.Size(134, 25)
    Me.lbAnchor.TabIndex = 2
    Me.lbAnchor.Text = "Anchor"
    Me.lbAnchor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lbEsci
    '
    Me.lbEsci.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbEsci.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbEsci.Location = New System.Drawing.Point(145, 244)
    Me.lbEsci.Name = "lbEsci"
    Me.lbEsci.Size = New System.Drawing.Size(123, 18)
    Me.lbEsci.TabIndex = 3
    Me.lbEsci.Text = "Esci"
    Me.lbEsci.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'cmdAtop
    '
    Me.cmdAtop.BackColor = System.Drawing.SystemColors.Control
    Me.cmdAtop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.cmdAtop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.cmdAtop.Location = New System.Drawing.Point(38, 3)
    Me.cmdAtop.Name = "cmdAtop"
    Me.cmdAtop.Size = New System.Drawing.Size(49, 22)
    Me.cmdAtop.TabIndex = 4
    Me.cmdAtop.Text = "Top"
    Me.cmdAtop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'pnAnchor
    '
    Me.pnAnchor.AllowDrop = True
    Me.pnAnchor.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAnchor.Appearance.Options.UseBackColor = True
    Me.pnAnchor.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.pnAnchor.Controls.Add(Me.cmdAbottom)
    Me.pnAnchor.Controls.Add(Me.cmdAleft)
    Me.pnAnchor.Controls.Add(Me.cmdAright)
    Me.pnAnchor.Controls.Add(Me.cmdAtop)
    Me.pnAnchor.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAnchor.Location = New System.Drawing.Point(2, 21)
    Me.pnAnchor.Name = "pnAnchor"
    Me.pnAnchor.Size = New System.Drawing.Size(127, 85)
    Me.pnAnchor.TabIndex = 5
    '
    'cmdAbottom
    '
    Me.cmdAbottom.BackColor = System.Drawing.SystemColors.Control
    Me.cmdAbottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.cmdAbottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.cmdAbottom.Location = New System.Drawing.Point(38, 59)
    Me.cmdAbottom.Name = "cmdAbottom"
    Me.cmdAbottom.Size = New System.Drawing.Size(49, 22)
    Me.cmdAbottom.TabIndex = 7
    Me.cmdAbottom.Text = "Bottom"
    Me.cmdAbottom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'cmdAleft
    '
    Me.cmdAleft.BackColor = System.Drawing.SystemColors.Control
    Me.cmdAleft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.cmdAleft.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.cmdAleft.Location = New System.Drawing.Point(3, 31)
    Me.cmdAleft.Name = "cmdAleft"
    Me.cmdAleft.Size = New System.Drawing.Size(41, 22)
    Me.cmdAleft.TabIndex = 6
    Me.cmdAleft.Text = "Left"
    Me.cmdAleft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'cmdAright
    '
    Me.cmdAright.BackColor = System.Drawing.SystemColors.Control
    Me.cmdAright.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.cmdAright.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.cmdAright.Location = New System.Drawing.Point(81, 31)
    Me.cmdAright.Name = "cmdAright"
    Me.cmdAright.Size = New System.Drawing.Size(41, 22)
    Me.cmdAright.TabIndex = 5
    Me.cmdAright.Text = "Right"
    Me.cmdAright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lbDock
    '
    Me.lbDock.BackColor = System.Drawing.Color.Transparent
    Me.lbDock.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbDock.Location = New System.Drawing.Point(0, 119)
    Me.lbDock.Name = "lbDock"
    Me.lbDock.Size = New System.Drawing.Size(134, 24)
    Me.lbDock.TabIndex = 6
    Me.lbDock.Text = "Dock"
    Me.lbDock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'pnDock
    '
    Me.pnDock.AllowDrop = True
    Me.pnDock.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDock.Appearance.Options.UseBackColor = True
    Me.pnDock.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
    Me.pnDock.Controls.Add(Me.cmdDnone)
    Me.pnDock.Controls.Add(Me.cmdDfill)
    Me.pnDock.Controls.Add(Me.cmdDbottom)
    Me.pnDock.Controls.Add(Me.cmdDleft)
    Me.pnDock.Controls.Add(Me.cmdDright)
    Me.pnDock.Controls.Add(Me.cmdDtop)
    Me.pnDock.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDock.Location = New System.Drawing.Point(1, 140)
    Me.pnDock.Name = "pnDock"
    Me.pnDock.Size = New System.Drawing.Size(128, 96)
    Me.pnDock.TabIndex = 7
    '
    'cmdDnone
    '
    Me.cmdDnone.BackColor = System.Drawing.SystemColors.Control
    Me.cmdDnone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.cmdDnone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.cmdDnone.Location = New System.Drawing.Point(3, 74)
    Me.cmdDnone.Name = "cmdDnone"
    Me.cmdDnone.Size = New System.Drawing.Size(122, 17)
    Me.cmdDnone.TabIndex = 9
    Me.cmdDnone.Text = "None"
    Me.cmdDnone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'cmdDfill
    '
    Me.cmdDfill.BackColor = System.Drawing.SystemColors.Control
    Me.cmdDfill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.cmdDfill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.cmdDfill.Location = New System.Drawing.Point(39, 25)
    Me.cmdDfill.Name = "cmdDfill"
    Me.cmdDfill.Size = New System.Drawing.Size(49, 22)
    Me.cmdDfill.TabIndex = 8
    Me.cmdDfill.Text = "Fill"
    Me.cmdDfill.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'cmdDbottom
    '
    Me.cmdDbottom.BackColor = System.Drawing.SystemColors.Control
    Me.cmdDbottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.cmdDbottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.cmdDbottom.Location = New System.Drawing.Point(39, 47)
    Me.cmdDbottom.Name = "cmdDbottom"
    Me.cmdDbottom.Size = New System.Drawing.Size(49, 22)
    Me.cmdDbottom.TabIndex = 7
    Me.cmdDbottom.Text = "Bottom"
    Me.cmdDbottom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'cmdDleft
    '
    Me.cmdDleft.BackColor = System.Drawing.SystemColors.Control
    Me.cmdDleft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.cmdDleft.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.cmdDleft.Location = New System.Drawing.Point(3, 25)
    Me.cmdDleft.Name = "cmdDleft"
    Me.cmdDleft.Size = New System.Drawing.Size(38, 22)
    Me.cmdDleft.TabIndex = 6
    Me.cmdDleft.Text = "Left"
    Me.cmdDleft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'cmdDright
    '
    Me.cmdDright.BackColor = System.Drawing.SystemColors.Control
    Me.cmdDright.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.cmdDright.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.cmdDright.Location = New System.Drawing.Point(85, 25)
    Me.cmdDright.Name = "cmdDright"
    Me.cmdDright.Size = New System.Drawing.Size(40, 22)
    Me.cmdDright.TabIndex = 5
    Me.cmdDright.Text = "Right"
    Me.cmdDright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'cmdDtop
    '
    Me.cmdDtop.BackColor = System.Drawing.SystemColors.Control
    Me.cmdDtop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.cmdDtop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    Me.cmdDtop.Location = New System.Drawing.Point(39, 3)
    Me.cmdDtop.Name = "cmdDtop"
    Me.cmdDtop.Size = New System.Drawing.Size(49, 22)
    Me.cmdDtop.TabIndex = 4
    Me.cmdDtop.Text = "Top"
    Me.cmdDtop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'lbSavePosition
    '
    Me.lbSavePosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbSavePosition.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbSavePosition.Location = New System.Drawing.Point(145, 218)
    Me.lbSavePosition.Name = "lbSavePosition"
    Me.lbSavePosition.Size = New System.Drawing.Size(123, 18)
    Me.lbSavePosition.TabIndex = 8
    Me.lbSavePosition.Text = "Salva impostazioni"
    Me.lbSavePosition.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'lbName
    '
    Me.lbName.AutoSize = True
    Me.lbName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbName.Location = New System.Drawing.Point(140, 4)
    Me.lbName.Name = "lbName"
    Me.lbName.Size = New System.Drawing.Size(45, 13)
    Me.lbName.TabIndex = 9
    Me.lbName.Text = "Name: "
    Me.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(140, 33)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(29, 13)
    Me.Label1.TabIndex = 10
    Me.Label1.Text = "Top:"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(140, 55)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(30, 13)
    Me.Label2.TabIndex = 11
    Me.Label2.Text = "Left:"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(139, 76)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(42, 13)
    Me.Label3.TabIndex = 12
    Me.Label3.Text = "Height:"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(140, 98)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(39, 13)
    Me.Label4.TabIndex = 13
    Me.Label4.Text = "Width:"
    '
    'ckMultiline
    '
    Me.ckMultiline.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMultiline.Location = New System.Drawing.Point(242, 196)
    Me.ckMultiline.Name = "ckMultiline"
    Me.ckMultiline.NTSCheckValue = "S"
    Me.ckMultiline.NTSUnCheckValue = "N"
    Me.ckMultiline.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMultiline.Properties.Appearance.Options.UseBackColor = True
    Me.ckMultiline.Properties.Caption = "Multiline (non più usato)"
    Me.ckMultiline.Size = New System.Drawing.Size(30, 19)
    Me.ckMultiline.TabIndex = 4
    Me.ckMultiline.Visible = False
    '
    'edTop
    '
    Me.edTop.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edTop.EditValue = "0"
    Me.edTop.Location = New System.Drawing.Point(221, 30)
    Me.edTop.Name = "edTop"
    Me.edTop.NTSDbField = ""
    Me.edTop.NTSFormat = "0"
    Me.edTop.NTSForzaVisZoom = False
    Me.edTop.NTSOldValue = ""
    Me.edTop.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTop.Properties.Appearance.Options.UseBackColor = True
    Me.edTop.Properties.Appearance.Options.UseTextOptions = True
    Me.edTop.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTop.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTop.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTop.Properties.MaxLength = 65536
    Me.edTop.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTop.Size = New System.Drawing.Size(48, 20)
    Me.edTop.TabIndex = 0
    '
    'edLeft
    '
    Me.edLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLeft.EditValue = "0"
    Me.edLeft.Location = New System.Drawing.Point(221, 52)
    Me.edLeft.Name = "edLeft"
    Me.edLeft.NTSDbField = ""
    Me.edLeft.NTSFormat = "0"
    Me.edLeft.NTSForzaVisZoom = False
    Me.edLeft.NTSOldValue = ""
    Me.edLeft.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edLeft.Properties.Appearance.Options.UseBackColor = True
    Me.edLeft.Properties.Appearance.Options.UseTextOptions = True
    Me.edLeft.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLeft.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLeft.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLeft.Properties.MaxLength = 65536
    Me.edLeft.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLeft.Size = New System.Drawing.Size(48, 20)
    Me.edLeft.TabIndex = 1
    '
    'edHeight
    '
    Me.edHeight.Cursor = System.Windows.Forms.Cursors.Default
    Me.edHeight.EditValue = "0"
    Me.edHeight.Location = New System.Drawing.Point(221, 73)
    Me.edHeight.Name = "edHeight"
    Me.edHeight.NTSDbField = ""
    Me.edHeight.NTSFormat = "0"
    Me.edHeight.NTSForzaVisZoom = False
    Me.edHeight.NTSOldValue = ""
    Me.edHeight.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edHeight.Properties.Appearance.Options.UseBackColor = True
    Me.edHeight.Properties.Appearance.Options.UseTextOptions = True
    Me.edHeight.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edHeight.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edHeight.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edHeight.Properties.MaxLength = 65536
    Me.edHeight.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edHeight.Size = New System.Drawing.Size(48, 20)
    Me.edHeight.TabIndex = 2
    '
    'edWidth
    '
    Me.edWidth.Cursor = System.Windows.Forms.Cursors.Default
    Me.edWidth.EditValue = "0"
    Me.edWidth.Location = New System.Drawing.Point(221, 95)
    Me.edWidth.Name = "edWidth"
    Me.edWidth.NTSDbField = ""
    Me.edWidth.NTSFormat = "0"
    Me.edWidth.NTSForzaVisZoom = False
    Me.edWidth.NTSOldValue = ""
    Me.edWidth.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edWidth.Properties.Appearance.Options.UseBackColor = True
    Me.edWidth.Properties.Appearance.Options.UseTextOptions = True
    Me.edWidth.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edWidth.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edWidth.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edWidth.Properties.MaxLength = 65536
    Me.edWidth.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edWidth.Size = New System.Drawing.Size(48, 20)
    Me.edWidth.TabIndex = 3
    '
    'ckAutosize
    '
    Me.ckAutosize.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAutosize.Location = New System.Drawing.Point(140, 121)
    Me.ckAutosize.Name = "ckAutosize"
    Me.ckAutosize.NTSCheckValue = "S"
    Me.ckAutosize.NTSUnCheckValue = "N"
    Me.ckAutosize.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAutosize.Properties.Appearance.Options.UseBackColor = True
    Me.ckAutosize.Properties.Caption = "Autosize"
    Me.ckAutosize.Size = New System.Drawing.Size(132, 19)
    Me.ckAutosize.TabIndex = 5
    '
    'ckBorder
    '
    Me.ckBorder.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckBorder.Location = New System.Drawing.Point(140, 146)
    Me.ckBorder.Name = "ckBorder"
    Me.ckBorder.NTSCheckValue = "S"
    Me.ckBorder.NTSUnCheckValue = "N"
    Me.ckBorder.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckBorder.Properties.Appearance.Options.UseBackColor = True
    Me.ckBorder.Properties.Caption = "Border"
    Me.ckBorder.Size = New System.Drawing.Size(132, 19)
    Me.ckBorder.TabIndex = 6
    '
    'lbCancella
    '
    Me.lbCancella.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lbCancella.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbCancella.Location = New System.Drawing.Point(1, 244)
    Me.lbCancella.Name = "lbCancella"
    Me.lbCancella.Size = New System.Drawing.Size(125, 18)
    Me.lbCancella.TabIndex = 21
    Me.lbCancella.Text = "Cancella"
    Me.lbCancella.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'ckVisLabel
    '
    Me.ckVisLabel.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckVisLabel.Location = New System.Drawing.Point(207, 196)
    Me.ckVisLabel.Name = "ckVisLabel"
    Me.ckVisLabel.NTSCheckValue = "S"
    Me.ckVisLabel.NTSUnCheckValue = "N"
    Me.ckVisLabel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckVisLabel.Properties.Appearance.Options.UseBackColor = True
    Me.ckVisLabel.Properties.Caption = "Visualizza label (non usato)"
    Me.ckVisLabel.Size = New System.Drawing.Size(29, 19)
    Me.ckVisLabel.TabIndex = 7
    Me.ckVisLabel.Visible = False
    '
    'edParent
    '
    Me.edParent.Cursor = System.Windows.Forms.Cursors.Default
    Me.edParent.Location = New System.Drawing.Point(145, 189)
    Me.edParent.Name = "edParent"
    Me.edParent.NTSDbField = ""
    Me.edParent.NTSForzaVisZoom = False
    Me.edParent.NTSOldValue = ""
    Me.edParent.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edParent.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edParent.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edParent.Properties.MaxLength = 65536
    Me.edParent.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edParent.Size = New System.Drawing.Size(123, 20)
    Me.edParent.TabIndex = 22
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(142, 170)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(43, 13)
    Me.Label5.TabIndex = 23
    Me.Label5.Text = "Parent:"
    '
    'FRM__GCPR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(280, 271)
    Me.ControlBox = False
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.edParent)
    Me.Controls.Add(Me.ckVisLabel)
    Me.Controls.Add(Me.lbCancella)
    Me.Controls.Add(Me.ckBorder)
    Me.Controls.Add(Me.ckAutosize)
    Me.Controls.Add(Me.edWidth)
    Me.Controls.Add(Me.edHeight)
    Me.Controls.Add(Me.edLeft)
    Me.Controls.Add(Me.edTop)
    Me.Controls.Add(Me.ckMultiline)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.lbName)
    Me.Controls.Add(Me.lbSavePosition)
    Me.Controls.Add(Me.pnDock)
    Me.Controls.Add(Me.lbDock)
    Me.Controls.Add(Me.pnAnchor)
    Me.Controls.Add(Me.lbEsci)
    Me.Controls.Add(Me.lbAnchor)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Name = "FRM__GCPR"
    Me.Text = "   GESTIONE CONTROLLO"
    CType(Me.pnAnchor, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAnchor.ResumeLayout(False)
    CType(Me.pnDock, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDock.ResumeLayout(False)
    CType(Me.ckMultiline.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTop.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLeft.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edHeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edWidth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAutosize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckBorder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckVisLabel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edParent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub FRM__GCPR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If ctlIn Is Nothing And barItmIn Is Nothing And tbpIn Is Nothing And griCol Is Nothing Then Me.Close()

      '--------------------------------
      'reset dei colori
      ResetAnchor()
      ResetDockColor()


      edParent.Enabled = False
      edParent.Text = ""

      If Not ctlIn Is Nothing Then
        If ctlIn.GetType.ToString.ToUpper.IndexOf("NTSINFORMATICA.FRM") > -1 Then
          '---------------------------------
          'la form
          pnAnchor.Enabled = False
          pnDock.Enabled = False
          edTop.Enabled = False
          edLeft.Enabled = False
          ckAutosize.Enabled = False
          ckBorder.Enabled = False
          ckMultiline.Enabled = False

          Compilaproprietà()
          ctlIn.BackColor = cltBackColor
        Else
          '--------------------------------
          'gestione controlli standard
          'imposto i controlli in form con lo stato iniziale del controllo passatomi
          edParent.Enabled = True
          If Not ctlIn.Parent Is Nothing Then edParent.Text = ctlIn.Parent.Name.ToString

          If CType(ctlIn.Anchor And AnchorStyles.Top, Boolean) = True Then cmdAtop.BackColor = SystemColors.ControlDark
          If CType(ctlIn.Anchor And AnchorStyles.Bottom, Boolean) = True Then cmdAbottom.BackColor = SystemColors.ControlDark
          If CType(ctlIn.Anchor And AnchorStyles.Left, Boolean) = True Then cmdAleft.BackColor = SystemColors.ControlDark
          If CType(ctlIn.Anchor And AnchorStyles.Right, Boolean) = True Then cmdAright.BackColor = SystemColors.ControlDark

          Select Case ctlIn.Dock
            Case DockStyle.Top : cmdDtop.BackColor = SystemColors.ControlDark
            Case DockStyle.Bottom : cmdDbottom.BackColor = SystemColors.ControlDark
            Case DockStyle.Left : cmdDleft.BackColor = SystemColors.ControlDark
            Case DockStyle.Right : cmdDright.BackColor = SystemColors.ControlDark
            Case DockStyle.Fill : cmdDfill.BackColor = SystemColors.ControlDark
            Case DockStyle.None : cmdDnone.BackColor = SystemColors.ControlDark
          End Select

          '---------------------------------
          'aggiorno con le proprietà del controllo la form
          Compilaproprietà()

          ctlIn.BackColor = cltBackColor

          If ctlIn.GetType.ToString <> "NTSInformatica.NTSComboBox" Then
            ckVisLabel.Enabled = False
          End If
        End If

      ElseIf Not barItmIn Is Nothing Then
        '---------------------------------
        'gestione specifica dei button della toolbat e menu
        pnAnchor.Enabled = False
        pnDock.Enabled = False
        edTop.Enabled = False
        edLeft.Enabled = False
        edHeight.Enabled = False
        edWidth.Enabled = False
        ckAutosize.Enabled = False
        ckBorder.Enabled = False
        ckMultiline.Enabled = False
        ckVisLabel.Enabled = False
        lbName.Text = "Name: " & barItmIn.Name

      ElseIf Not tbpIn Is Nothing Then
        '---------------------------------
        'gestione specifica dei TabPage del tabcontrol
        pnAnchor.Enabled = False
        pnDock.Enabled = False
        edTop.Enabled = False
        edLeft.Enabled = False
        edHeight.Enabled = False
        edWidth.Enabled = False
        ckAutosize.Enabled = False
        ckBorder.Enabled = False
        ckMultiline.Enabled = False
        ckVisLabel.Enabled = False
        lbName.Text = "Name: " & tbpIn.Name

      Else
        '---------------------------------
        'gestione specifica delle colonne della griglia
        pnAnchor.Enabled = False
        pnDock.Enabled = False
        edTop.Enabled = False
        edLeft.Enabled = False
        edHeight.Enabled = False
        edWidth.Enabled = False
        ckAutosize.Enabled = False
        ckBorder.Enabled = False
        ckMultiline.Enabled = False
        ckVisLabel.Enabled = False
        lbName.Text = "Name: " & griCol.Name

      End If    'If Not ctlIn Is Nothing Then

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ResetAnchor()
    Try
      cmdAtop.BackColor = Color.Transparent
      cmdAbottom.BackColor = Color.Transparent
      cmdAleft.BackColor = Color.Transparent
      cmdAright.BackColor = Color.Transparent
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ResetDockColor()
    Try
      cmdDtop.BackColor = Color.Transparent
      cmdDbottom.BackColor = Color.Transparent
      cmdDleft.BackColor = Color.Transparent
      cmdDright.BackColor = Color.Transparent
      cmdDfill.BackColor = Color.Transparent
      cmdDnone.BackColor = Color.Transparent
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ApplicaStile()
    Try
      If ctlIn Is Nothing Then Return
      ctlIn.Anchor = AnchorStyles.None
      ctlIn.Dock = DockStyle.None
      If cmdAtop.BackColor = SystemColors.ControlDark Then ctlIn.Anchor = ctlIn.Anchor Or AnchorStyles.Top
      If cmdAbottom.BackColor = SystemColors.ControlDark Then ctlIn.Anchor = ctlIn.Anchor Or AnchorStyles.Bottom
      If cmdAleft.BackColor = SystemColors.ControlDark Then ctlIn.Anchor = ctlIn.Anchor Or AnchorStyles.Left
      If cmdAright.BackColor = SystemColors.ControlDark Then ctlIn.Anchor = ctlIn.Anchor Or AnchorStyles.Right

      If cmdDtop.BackColor = SystemColors.ControlDark Then ctlIn.Dock = DockStyle.Top
      If cmdDbottom.BackColor = SystemColors.ControlDark Then ctlIn.Dock = DockStyle.Bottom
      If cmdDleft.BackColor = SystemColors.ControlDark Then ctlIn.Dock = DockStyle.Left
      If cmdDright.BackColor = SystemColors.ControlDark Then ctlIn.Dock = DockStyle.Right
      If cmdDfill.BackColor = SystemColors.ControlDark Then ctlIn.Dock = DockStyle.Fill

      If ctlIn.Dock = DockStyle.None Then
        If ctlIn.Size = New Size(0, 0) Then
          ctlIn.Size = New Size(10, 20)
        End If
        If ctlIn.Left > ctlIn.Parent.Width Or ctlIn.Top > ctlIn.Parent.Height Then
          ctlIn.Top = CInt(ctlIn.Parent.Height / 2)
          ctlIn.Left = CInt(ctlIn.Parent.Width / 2)
        End If
        If ctlIn.Width > ctlIn.Parent.Width Then ctlIn.Width = ctlIn.Parent.Width - 10
        If ctlIn.Height > ctlIn.Parent.Height Then ctlIn.Height = ctlIn.Parent.Height - 50
      End If

      ctlIn.BackColor = Color.Red

      Compilaproprietà()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub cmdAtop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAtop.Click
    Try
      If cmdAtop.BackColor = SystemColors.ControlDark Then
        cmdAtop.BackColor = Color.Transparent
      Else
        cmdAtop.BackColor = SystemColors.ControlDark
        ResetDockColor()
        cmdDnone.BackColor = SystemColors.ControlDark
      End If
      ApplicaStile()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAbottom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAbottom.Click
    Try
      If cmdAbottom.BackColor = SystemColors.ControlDark Then
        cmdAbottom.BackColor = Color.Transparent
      Else
        cmdAbottom.BackColor = SystemColors.ControlDark
        ResetDockColor()
        cmdDnone.BackColor = SystemColors.ControlDark
      End If
      ApplicaStile()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAleft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAleft.Click
    Try
      If cmdAleft.BackColor = SystemColors.ControlDark Then
        cmdAleft.BackColor = Color.Transparent
      Else
        cmdAleft.BackColor = SystemColors.ControlDark
        ResetDockColor()
        cmdDnone.BackColor = SystemColors.ControlDark
      End If
      ApplicaStile()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdAright_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAright.Click
    Try
      If cmdAright.BackColor = SystemColors.ControlDark Then
        cmdAright.BackColor = Color.Transparent
      Else
        cmdAright.BackColor = SystemColors.ControlDark
        ResetDockColor()
        cmdDnone.BackColor = SystemColors.ControlDark
      End If
      ApplicaStile()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub cmdDtop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDtop.Click
    Try
      ResetAnchor()
      ResetDockColor()
      cmdDtop.BackColor = SystemColors.ControlDark
      ApplicaStile()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdDbottom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDbottom.Click
    Try
      ResetAnchor()
      ResetDockColor()
      cmdDbottom.BackColor = SystemColors.ControlDark
      ApplicaStile()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdDleft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDleft.Click
    Try
      ResetAnchor()
      ResetDockColor()
      cmdDleft.BackColor = SystemColors.ControlDark
      ApplicaStile()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdDright_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDright.Click
    Try
      ResetAnchor()
      ResetDockColor()
      cmdDright.BackColor = SystemColors.ControlDark
      ApplicaStile()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdDfill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDfill.Click
    Try
      ResetAnchor()
      ResetDockColor()
      cmdDfill.BackColor = SystemColors.ControlDark
      ApplicaStile()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdDnone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDnone.Click
    Try
      ResetDockColor()
      cmdDnone.BackColor = SystemColors.ControlDark
      ApplicaStile()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Sub lbCancella_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbCancella.Click
    Try
      If bCtlExt = False Then Return
      Dim mnuParent As NTSBarSubItem = Nothing

      If Not ctlIn Is Nothing Then
        '-------------------------------------
        'cancello un controllo
        If ctlIn.Controls.Count > 0 Then
          If (ctlIn.GetType.ToString = "NTSInformatica.NTSCheckBox" Or _
              ctlIn.GetType.ToString = "NTSInformatica.NTSRadioButton") Then
            If ctlIn.Controls.Count > 1 Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128799346971855000, "Prima di cancellare il controllo occorre cancellare tutti i controlli in esso contenuti"))
              Return
            End If
          ElseIf ctlIn.GetType.ToString = "NTSInformatica.NTSComboBox" Then
            If ctlIn.Controls.Count > 3 Then
              'contiene label, pannello di sfondo e combobox
              oApp.MsgBoxErr(oApp.Tr(Me, 128799346989327000, "Prima di cancellare il controllo occorre cancellare tutti i controlli in esso contenuti"))
              Return
            End If
          ElseIf ctlIn.GetType.ToString = "NTSInformatica.NTSListBox" Then
            If ctlIn.Controls.Count > 2 Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128799347004771000, "Prima di cancellare il controllo occorre cancellare tutti i controlli in esso contenuti"))
              Return
            End If
          ElseIf ctlIn.GetType.ToString.IndexOf("NTSInformatica.NTSTextBox") > -1 Then
            If ctlIn.Controls.Count > 1 Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128799347018499000, "Prima di cancellare il controllo occorre cancellare tutti i controlli in esso contenuti"))
              Return
            End If
          ElseIf ctlIn.GetType.ToString.IndexOf("NTSInformatica.NTSMemoBox") > -1 Then
            If ctlIn.Controls.Count > 4 Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128799347034255000, "Prima di cancellare il controllo occorre cancellare tutti i controlli in esso contenuti"))
              Return
            End If
          ElseIf ctlIn.GetType.ToString.IndexOf("NTSInformatica.NTSGrid") > -1 Then
            If CType(CType(ctlIn, NTSGrid).DefaultView, NTSGridView).Columns.Count > 0 Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128799347034255001, "Prima di cancellare la griglia occorre rimuovere tutte le colonne in essa contenute"))
              Return
            End If
          Else
            oApp.MsgBoxErr(oApp.Tr(Me, 128799347050011000, "Prima di cancellare il controllo occorre cancellare tutti i controlli in esso contenuti"))
            Return
          End If
        End If

        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791222140468750, "Confermi la rimozione del controllo?")) = MsgBoxResult.No Then Return

        oCleGctl.SalvaPosition(strChild, ctlIn.FindForm.Name, ctlIn.Name, ctlIn.GetType.ToString, _
                                      "", "", -1, -1, -1, -1, 0, 0, False, False, False, bCtlExt, False, "")

        If ctlIn.GetType.ToString.IndexOf("NTSInformatica.NTSGrid") > -1 Then
          'lo cancello dopo, altrimenti da un errore non gestito
          ctlIn.Tag = "DELETE"
        Else
          ctlIn.Dispose()
        End If

      ElseIf Not barItmIn Is Nothing Then
        '-------------------------------------
        'cancello un bottone della toolbar o una voce di menu
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791222140781250, "Confermi la rimozione del Button della Toolbar?")) = MsgBoxResult.No Then Return

        If barItmIn.Links(0).LinkedObject.GetType.ToString.IndexOf("NTSBarSubItem") > -1 Then
          mnuParent = CType(barItmIn.Links(0).LinkedObject, NTSBarSubItem)
          If mnuParent.Name <> "MENUCUSTOM" Or mnuParent.ItemLinks.Count > 1 Then mnuParent = Nothing
        End If

        oCleGctl.SalvaPosition(strChild, barItmIn.Manager.Form.FindForm.Name, barItmIn.Name, barItmIn.GetType.ToString, _
                                      "", "", -1, -1, -1, -1, 0, 0, False, False, False, bCtlExt, False, "")
        barItmIn.Dispose()
        barItmIn.Links.Clear()

        '-------------------------------------
        'se il sottomenu era contenuto nel menu custom ed in questo menu non ci sono altre voci rimuovo anche questo
        If Not mnuParent Is Nothing Then
          mnuParent.Dispose()
          mnuParent.Links.Clear()
        End If

      ElseIf Not tbpIn Is Nothing Then
        '-------------------------------------
        'cancello una linguetta del TabControl
        If tbpIn.Controls.Count > 0 Then
          oApp.MsgBoxErr(oApp.Tr(Me, 127791222140937500, "Prima di cancellare il controllo occorre cancellare tutti i controlli in esso contenuti"))
          Return
        End If

        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791222141093750, "Confermi la rimozione della linguetta dal TabControl?")) = MsgBoxResult.No Then Return
        oCleGctl.SalvaPosition(strChild, tbpIn.FindForm.Name, tbpIn.Name, tbpIn.GetType.ToString, _
                                      "", "", -1, -1, -1, -1, 0, 0, False, False, False, bCtlExt, False, "")
        tbpIn.Dispose()

      ElseIf Not griCol Is Nothing Then
        '-------------------------------------
        'cancello una colonna di griglia
        If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791222141250000, "Confermi la rimozione della colonna |'" & griCol.Name & "'|?")) = MsgBoxResult.No Then Return
        oCleGctl.SalvaPosition(strChild, griCol.View.GridControl.FindForm.Name, griCol.Name, griCol.GetType.ToString, _
                                      "", "", -1, -1, -1, -1, 0, 0, False, False, False, bCtlExt, False, "")

        griCol.View.FocusedColumn = griCol.View.Columns(0)   'mi sposto su un'altra colonna, altrimenti darebbe errore dopo la rimozione nella BN__GRID.Me_RowCellStyle
        griCol.View.Columns.Remove(griCol)

      End If    'If Not ctlIn Is Nothing Then

      Me.Close()

    Catch ex As Exception

    End Try
  End Sub
  Public Overridable Sub lbCancella_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbCancella.MouseLeave
    lbCancella.BackColor = System.Drawing.SystemColors.Control
  End Sub
  Public Overridable Sub lbCancella_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbCancella.MouseEnter
    If bCtlExt Then lbCancella.BackColor = System.Drawing.SystemColors.MenuHighlight
  End Sub


  Public Overridable Sub lbEsci_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbEsci.Click
    Try
      'Tolto altrimenti si era costretti a salvare per uscire.

      'If Not ctlIn Is Nothing Then
      '  If ctlIn.BackColor = Color.Red Then
      '    oApp.MsgBoxErr(oApp.Tr(Me, 127791222141718750, "Salvare le modifiche prima di uscire"))
      '    Return
      '  End If
      'End If
      If Not ctlIn Is Nothing Then
        If ctlIn.GetType.ToString.ToUpper.IndexOf("NTSINFORMATICA.FRM") = -1 Then
          ctlIn.Anchor = CType(oList(8), AnchorStyles)
          ctlIn.Dock = CType(oList(9), DockStyle)
        End If

        ctlIn.Top = NTSCInt(oList(0))
        ctlIn.Left = NTSCInt(oList(1))
        ctlIn.Height = NTSCInt(oList(2))
        ctlIn.Width = NTSCInt(oList(3))
        Select Case ctlIn.GetType.ToString
          Case "NTSInformatica.NTSLabel"
            CType(ctlIn, NTSLabel).AutoSize = CBool(oList(5))
          Case "NTSInformatica.NTSCheckBox"
            CType(ctlIn, NTSCheckBox).AutoSize = CBool(oList(5))
          Case "NTSInformatica.NTSRadioButton"
            CType(ctlIn, NTSRadioButton).AutoSize = CBool(oList(5))
        End Select

        Select Case ctlIn.GetType.ToString
          Case "NTSInformatica.NTSLabel"
            CType(ctlIn, NTSLabel).BorderStyle = CType(oList(6), BorderStyle)
          Case "NTSInformatica.NTSPanel"
            CType(ctlIn, NTSPanel).BorderStyle = CType(oList(6), DevExpress.XtraEditors.Controls.BorderStyles)
          Case "NTSInformatica.NTSPictureBox"
            CType(ctlIn, NTSPictureBox).BorderStyle = CType(oList(6), DevExpress.XtraEditors.Controls.BorderStyles)
        End Select
      End If

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub lbEsci_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbEsci.MouseLeave
    lbEsci.BackColor = System.Drawing.SystemColors.Control
  End Sub
  Public Overridable Sub lbEsci_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbEsci.MouseEnter
    lbEsci.BackColor = System.Drawing.SystemColors.MenuHighlight
  End Sub


  Public Overridable Sub lbSavePosition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbSavePosition.Click
    'salvo la posiizone del controllo ed esco
    Dim strStandardParents As String = ""
    Dim strParent As String = ""
    Dim strParams As String = ""
    Try
      Me.ValidaLastControl()

      If Not ctlIn Is Nothing Then

        If ctlIn.GetType.ToString.ToUpper.IndexOf("NTSINFORMATICA.FRM") > -1 Then
          '-------------------------------------
          'salvo le impostazioni di form 
          strParent = ctlIn.Name.ToString
          strStandardParents = strParent
          oCleGctl.SalvaPosition(strChild, ctlIn.Name, ctlIn.Name, ctlIn.GetType.ToString, _
                                        strParent, "*form*", 0, 0, _
                                        ctlIn.Height, ctlIn.Width, 0, 0, _
                                        ckMultiline.Checked, ckAutosize.Checked, ckBorder.Checked, _
                                        False, ckVisLabel.Checked, "")
          ctlIn.BackColor = cltBackColor
          Me.Close()
          Return
        End If

        '-------------------------------------
        'salvo un controllo
        'If Not ctlIn.Parent Is Nothing Then strParent = ctlIn.Parent.Name.ToString
        strParent = edParent.Text

        If Not ctlIn.Tag Is Nothing Then
          If ctlIn.Tag.ToString.IndexOf("G-") > -1 Then
            strStandardParents = ctlIn.Tag.ToString.Substring(2)
            strParams = CompilaParams(ctlIn, Nothing)
            oCleGctl.SalvaPosition(strChild, ctlIn.FindForm.Name, ctlIn.Name, ctlIn.GetType.ToString, _
                                          strParent, strStandardParents, ctlIn.Top, ctlIn.Left, _
                                          ctlIn.Height, ctlIn.Width, ctlIn.Anchor, ctlIn.Dock, _
                                          ckMultiline.Checked, ckAutosize.Checked, ckBorder.Checked, _
                                          bCtlExt, ckVisLabel.Checked, strParams)
            ctlIn.BackColor = cltBackColor
            Me.Close()
          End If
        End If
      ElseIf Not barItmIn Is Nothing Then
        '-------------------------------------
        'salvo un bottone della toolbar o menu
        strStandardParents = ""
        If barItmIn.Links(0).LinkedObject.GetType.ToString.IndexOf("NTSBarSubItem") = -1 Then
          strStandardParents = CType(barItmIn.Links(0).LinkedObject, NTSBar).Manager.Form.Name & "." & _
                               CType(barItmIn.Links(0).LinkedObject, NTSBar).BarName
        Else
          strStandardParents = CType(barItmIn.Links(0).LinkedObject, NTSBarSubItem).Manager.Form.Name & "." & _
                               CType(barItmIn.Links(0).LinkedObject, NTSBarSubItem).Name
          'per le voci contenute nel menu custom memorizzo il nome della toolbar
          If CType(barItmIn.Links(0).LinkedObject, NTSBarSubItem).Name = "MENUCUSTOM" Then
            strStandardParents = CType(CType(barItmIn.Links(0).LinkedObject, NTSBarSubItem).Links(0).LinkedObject, NTSBar).Manager.Form.Name & "." & _
                                 CType(CType(barItmIn.Links(0).LinkedObject, NTSBarSubItem).Links(0).LinkedObject, NTSBar).BarName
          End If
        End If

        strParams = "GRIC:0|" & _
                    "TEXT:" & barItmIn.Caption & "|" & _
                    "NOME:|" & _
                    "VMIN:|" & _
                    "VMAX:|" & _
                    "MLEN:|" & _
                    "FORM:|" & _
                    "NULL:|" & _
                    "CHEC:|" & _
                    "UNCH:|" & _
                    "DBAS:|" & _
                    "ZOOM:|" & _
                    "ITEM:"
        oCleGctl.SalvaPosition(strChild, barItmIn.Manager.Form.FindForm.Name, barItmIn.Name, barItmIn.GetType.ToString, _
                                      barItmIn.Manager.Form.Name, strStandardParents, 0, 0, _
                                      0, 0, 0, 0, _
                                      ckMultiline.Checked, ckAutosize.Checked, ckBorder.Checked, _
                                      bCtlExt, ckVisLabel.Checked, strParams)
        Me.Close()
      ElseIf Not tbpIn Is Nothing Then
        '-------------------------------------
        'salvo una linguetta del tabcontrol
        strParams = "GRIC:0|" & _
                    "TEXT:" & tbpIn.Text & "|" & _
                    "NOME:|" & _
                    "VMIN:|" & _
                    "VMAX:|" & _
                    "MLEN:|" & _
                    "FORM:|" & _
                    "NULL:|" & _
                    "CHEC:|" & _
                    "UNCH:|" & _
                    "DBAS:|" & _
                    "ZOOM:|" & _
                    "ITEM:"
        oCleGctl.SalvaPosition(strChild, tbpIn.FindForm.Name, tbpIn.Name, tbpIn.GetType.ToString, _
                                      tbpIn.Parent.Name, tbpIn.Parent.Name, 0, 0, _
                                      0, 0, 0, 0, _
                                      ckMultiline.Checked, ckAutosize.Checked, ckBorder.Checked, _
                                      bCtlExt, ckVisLabel.Checked, strParams)
        Me.Close()
      ElseIf Not griCol Is Nothing Then
        '-------------------------------------
        'salvo una colonna di griglia
        If bCtlExt Then
          strParams = CompilaParams(Nothing, griCol)
          oCleGctl.SalvaPosition(strChild, griCol.View.GridControl.FindForm.Name, griCol.Name, griCol.GetType.ToString, _
                                        griCol.View.Name, griCol.View.Name, 0, 0, _
                                        0, 0, 0, 0, _
                                        ckMultiline.Checked, ckAutosize.Checked, ckBorder.Checked, _
                                        bCtlExt, ckVisLabel.Checked, strParams)
        End If
        Me.Close()

      End If    'If Not ctlIn Is Nothing Then

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function CompilaParams(ByVal ctrlIn As Control, ByVal griCol As NTSGridColumn) As String
    Dim strOut As String = ""
    Dim strCB As String = ""
    Dim i As Integer = 0
    Try
      'strOut = "GRIC:0" & cbGridColType.SelectedValue.ToString & "|" & _
      '        "TEXT:" & edText.Text & "|" & _
      '        "NOME:" & edNomeCampo.Text & "|" & _
      '        "VMIN:" & edMin.Text.Trim & "|" & _
      '        "VMAX:" & edMax.Text.Trim & "|" & _
      '        "MLEN:" & edMaxLen.Text.Trim & "|" & _
      '        "FORM:" & edFormat.Text.Trim & "|" & _
      '        "NULL:" & ckAllowNull.NTSText.Text & "|" & _
      '        "CHEC:" & edValueCheck.Text.Trim & "|" & _
      '        "UNCH:" & edValueUnCheck.Text.Trim & "|" & _
      '        "DBAS:" & edDatabase.Text.Trim & "|" & _
      '        "ZOOM:" & edPrgZoom.Text.Trim & "|" & _
      '        "ITEM:" & edCombo.Text.Trim.Replace(vbCrLf, "§")
      If ctrlIn Is Nothing Then GoTo GRIGLIA
      Select Case ctrlIn.GetType.ToString
        Case "NTSInformatica.NTSTextBoxNum"
          strOut = "GRIC:0|" & _
                  "TEXT:|" & _
                  "NOME:" & CType(ctrlIn, NTSTextBox).strNomeCampo & "|" & _
                  "VMIN:" & CType(ctrlIn, NTSTextBoxNum).dMin.ToString & "|" & _
                  "VMAX:" & CType(ctrlIn, NTSTextBoxNum).dMax.ToString & "|" & _
                  "MLEN:" & CType(ctrlIn, NTSTextBox).nMaxLen.ToString & "|" & _
                  "FORM:" & CType(ctrlIn, NTSTextBoxNum).strFormat.ToString & "|" & _
                  "NULL:|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:" & CType(ctrlIn, NTSTextBoxNum).NTSDbField & "|" & _
                  "ZOOM:" & CType(ctrlIn, NTSTextBoxNum).NTSProgZoom.ToUpper.Trim & "|" & _
                  "ITEM:"
        Case "NTSInformatica.NTSTextBoxStr"
          strOut = "GRIC:0|" & _
                  "TEXT:|" & _
                  "NOME:" & CType(ctrlIn, NTSTextBox).strNomeCampo & "|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:" & CType(ctrlIn, NTSTextBox).nMaxLen.ToString & "|" & _
                  "FORM:|" & _
                  "NULL:" & IIf(CType(ctrlIn, NTSTextBoxStr).bAllowNull, "-1", "0").ToString & "|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:" & CType(ctrlIn, NTSTextBoxStr).NTSDbField & "|" & _
                  "ZOOM:" & CType(ctrlIn, NTSTextBoxStr).NTSProgZoom.ToUpper.Trim & "|" & _
                  "ITEM:"
        Case "NTSInformatica.NTSTextBoxData"
          strOut = "GRIC:0|" & _
                  "TEXT:|" & _
                  "NOME:" & CType(ctrlIn, NTSTextBox).strNomeCampo & "|" & _
                  "VMIN:" & CType(ctrlIn, NTSTextBoxData).dtValoreMin.ToString & "|" & _
                  "VMAX:" & CType(ctrlIn, NTSTextBoxData).dtValoreMax.ToString & "|" & _
                  "MLEN:" & CType(ctrlIn, NTSTextBox).nMaxLen.ToString & "|" & _
                  "FORM:|" & _
                  "NULL:" & IIf(CType(ctrlIn, NTSTextBoxData).bAllowNull, "-1", "0").ToString & "|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:" & CType(ctrlIn, NTSTextBoxData).NTSDbField & "|" & _
                  "ZOOM:|" & _
                  "ITEM:"
        Case "NTSInformatica.NTSMemoBox"
          strOut = "GRIC:0|" & _
                  "TEXT:|" & _
                  "NOME:" & CType(ctrlIn, NTSMemoBox).strNomeCampo & "|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:" & CType(ctrlIn, NTSMemoBox).nMaxLen.ToString & "|" & _
                  "FORM:|" & _
                  "NULL:" & IIf(CType(ctrlIn, NTSMemoBox).bAllowNull, "-1", "0").ToString & "|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:" & CType(ctrlIn, NTSMemoBox).NTSDbField & "|" & _
                  "ZOOM:|" & _
                  "ITEM:"
        Case "NTSInformatica.NTSLabel"
          strOut = "GRIC:0|" & _
                  "TEXT:" & ctrlIn.Text & "|" & _
                  "NOME:|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:" & CType(ctrlIn, NTSLabel).NTSDbField & "|" & _
                  "ZOOM:|" & _
                  "ITEM:"
        Case "NTSInformatica.NTSComboBox"
          For i = 0 To CType(CType(ctrlIn, NTSComboBox).DataSource, DataTable).Rows.Count - 1
            strCB += CType(CType(ctrlIn, NTSComboBox).DataSource, DataTable).Rows(i)(CType(ctrlIn, NTSComboBox).ValueMember).ToString & ";" & CType(CType(ctrlIn, NTSComboBox).DataSource, DataTable).Rows(i)(CType(ctrlIn, NTSComboBox).DisplayMember).ToString & "§"
          Next
          If strCB.Length > 0 Then strCB = strCB.Substring(0, strCB.Length - 1)
          strOut = "GRIC:0|" & _
                  "TEXT:|" & _
                  "NOME:" & CType(ctrlIn, NTSComboBox).strNomeCampo & "|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:" & CType(ctrlIn, NTSComboBox).NTSDbField & "|" & _
                  "ZOOM:|" & _
                  "ITEM:" & strCB
        Case "NTSInformatica.NTSListBox"
          For i = 0 To CType(CType(ctrlIn, NTSListBox).DataSource, DataTable).Rows.Count - 1
            strCB += CType(CType(ctrlIn, NTSListBox).DataSource, DataTable).Rows(i)(CType(ctrlIn, NTSListBox).ValueMember).ToString & ";" & CType(CType(ctrlIn, NTSListBox).DataSource, DataTable).Rows(i)(CType(ctrlIn, NTSListBox).DisplayMember).ToString & "§"
          Next
          If strCB.Length > 0 Then strCB = strCB.Substring(0, strCB.Length - 1)
          strOut = "GRIC:0|" & _
                  "TEXT:|" & _
                  "NOME:" & CType(ctrlIn, NTSListBox).strNomeCampo & "|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:" & CType(ctrlIn, NTSListBox).NTSDbField & "|" & _
                  "ZOOM:|" & _
                  "ITEM:" & strCB
        Case "NTSInformatica.NTSCheckBox"
          strOut = "GRIC:0|" & _
                  "TEXT:" & CType(ctrlIn, NTSCheckBox).Text & "|" & _
                  "NOME:" & CType(ctrlIn, NTSCheckBox).strNomeCampo & "|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:" & CType(ctrlIn, NTSCheckBox).NTSCheckValue & "|" & _
                  "UNCH:" & CType(ctrlIn, NTSCheckBox).NTSUnCheckValue & "|" & _
                  "DBAS:" & CType(ctrlIn, NTSCheckBox).NTSText.NTSDbField & "|" & _
                  "ZOOM:|" & _
                  "ITEM:"
        Case "NTSInformatica.NTSRadioButton"
          strOut = "GRIC:0|" & _
                  "TEXT:" & CType(ctrlIn, NTSRadioButton).Text & "|" & _
                  "NOME:" & CType(ctrlIn, NTSRadioButton).strNomeCampo & "|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:" & CType(ctrlIn, NTSRadioButton).strCheck & "|" & _
                  "UNCH:|" & _
                  "DBAS:" & CType(ctrlIn, NTSRadioButton).NTSText.NTSDbField & "|" & _
                  "ZOOM:|" & _
                  "ITEM:"
        Case "NTSInformatica.NTSButton"
          strOut = "GRIC:0|" & _
                  "TEXT:" & ctrlIn.Text & "|" & _
                  "NOME:|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:|" & _
                  "ZOOM:|" & _
                  "ITEM:"
        Case "NTSInformatica.NTSPictureBox"
          strOut = "GRIC:0|" & _
                  "TEXT:" & ctrlIn.Text & "|" & _
                  "NOME:|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:|" & _
                  "ZOOM:|" & _
                  "ITEM:"
        Case "NTSInformatica.NTSPanel"
          strOut = "GRIC:0|" & _
                  "TEXT:|" & _
                  "NOME:|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:|" & _
                  "ZOOM:|" & _
                  "ITEM:"
        Case "NTSInformatica.NTSPictureBox"
          strOut = "GRIC:0|" & _
                  "TEXT:|" & _
                  "NOME:|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:|" & _
                  "ZOOM:|" & _
                  "ITEM:"
        Case "NTSInformatica.NTSGroupBox"
          strOut = "GRIC:0|" & _
                  "TEXT:" & ctrlIn.Text & "|" & _
                  "NOME:|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:|" & _
                  "ZOOM:|" & _
                  "ITEM:"
        Case "NTSInformatica.NTSGrid"
          strOut = "GRIC:0|" & _
                  "TEXT:|" & _
                  "NOME:" & CType(CType(ctrlIn, NTSGrid).DefaultView, NTSGridView).strNomeCampo & "|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:" & CType(ctrlIn, NTSGrid).Ext_DataSoruce & "|" & _
                  "ZOOM:|" & _
                  "ITEM:"
      End Select

      Return strOut

GRIGLIA:
      'S = stringa, N = numero, D = data/ora, C = checkbox, B = combobox, M = scriga multilinea (memo)
      Select Case griCol.NTSTipoColonna
        Case "N"
          strOut = "GRIC:1|" & _
                  "TEXT:" & griCol.Caption & "|" & _
                  "NOME:" & griCol.strNomeCampo & "|" & _
                  "VMIN:" & griCol.dValoreMin.ToString & "|" & _
                  "VMAX:" & griCol.dValoreMax.ToString & "|" & _
                  "MLEN:" & griCol.nMaxLen.ToString & "|" & _
                  "FORM:" & griCol.DisplayFormat.FormatString & "|" & _
                  "NULL:|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:" & griCol.strNomeCampo & "|" & _
                  "ZOOM:" & griCol.NTSProgZoom.ToUpper.Trim & "|" & _
                  "ITEM:"
        Case "S", "M"
          strOut = "GRIC:" & IIf(griCol.NTSTipoColonna = "S", "2", "6").ToString & "|" & _
                  "TEXT:" & griCol.Caption & "|" & _
                  "NOME:" & griCol.strNomeCampo & "|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:" & griCol.nMaxLen.ToString & "|" & _
                  "FORM:|" & _
                  "NULL:" & IIf(griCol.bAllowNull, "-1", "0").ToString & "|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:" & griCol.strNomeCampo & "|" & _
                  "ZOOM:" & griCol.NTSProgZoom.ToUpper.Trim & "|" & _
                  "ITEM:"
        Case "D"
          strOut = "GRIC:3|" & _
                  "TEXT:" & griCol.Caption & "|" & _
                  "NOME:" & griCol.strNomeCampo & "|" & _
                  "VMIN:" & griCol.dtValoreMin.ToString & "|" & _
                  "VMAX:" & griCol.dtValoreMax.ToString & "|" & _
                  "MLEN:" & griCol.nMaxLen.ToString & "|" & _
                  "FORM:|" & _
                  "NULL:" & IIf(griCol.bAllowNull, "-1", "0").ToString & "|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:" & griCol.strNomeCampo & "|" & _
                  "ZOOM:|" & _
                  "ITEM:"
        Case "C"
          strOut = "GRIC:4|" & _
                  "TEXT:" & griCol.Caption & "|" & _
                  "NOME:" & griCol.strNomeCampo & "|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:" & CType(griCol.ColumnEdit, NTSRepositoryItemCheckEdit).ValueChecked.ToString & "|" & _
                  "UNCH:" & CType(griCol.ColumnEdit, NTSRepositoryItemCheckEdit).ValueUnchecked.ToString & "|" & _
                  "DBAS:" & griCol.strNomeCampo & "|" & _
                  "ZOOM:|" & _
                  "ITEM:"
        Case "B"
          For i = 0 To CType(CType(griCol.ColumnEdit, NTSRepositoryItemComboBox).DataSource, DataTable).Rows.Count - 1
            strCB += CType(CType(griCol.ColumnEdit, NTSRepositoryItemComboBox).DataSource, DataTable).Rows(i)(CType(griCol.ColumnEdit, NTSRepositoryItemComboBox).ValueMember).ToString & ";" & _
                     CType(CType(griCol.ColumnEdit, NTSRepositoryItemComboBox).DataSource, DataTable).Rows(i)(CType(griCol.ColumnEdit, NTSRepositoryItemComboBox).DisplayMember).ToString & "§"
          Next
          If strCB.Length > 0 Then strCB = strCB.Substring(0, strCB.Length - 1)
          strOut = "GRIC:5|" & _
                  "TEXT:" & griCol.Caption & "|" & _
                  "NOME:" & griCol.strNomeCampo & "|" & _
                  "VMIN:|" & _
                  "VMAX:|" & _
                  "MLEN:|" & _
                  "FORM:|" & _
                  "NULL:|" & _
                  "CHEC:|" & _
                  "UNCH:|" & _
                  "DBAS:" & griCol.strNomeCampo & "|" & _
                  "ZOOM:|" & _
                  "ITEM:" & strCB
      End Select

      Return strOut
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function

  Public Overridable Sub lbSavePosition_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbSavePosition.MouseLeave
    lbSavePosition.BackColor = System.Drawing.SystemColors.Control
  End Sub
  Public Overridable Sub lbSavePosition_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbSavePosition.MouseEnter
    Try
      If Not ctlIn Is Nothing Then
        If Not ctlIn.Tag Is Nothing Then
          If ctlIn.Tag.ToString.IndexOf("G-") > -1 Then
            lbSavePosition.BackColor = System.Drawing.SystemColors.MenuHighlight
          End If
        ElseIf ctlIn.GetType.ToString.ToUpper.IndexOf("NTSINFORMATICA.FRM") > -1 Then
          lbSavePosition.BackColor = System.Drawing.SystemColors.MenuHighlight
        End If
      Else
        lbSavePosition.BackColor = System.Drawing.SystemColors.MenuHighlight
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edTop_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTop.Leave
    Settaproprietà(0)
  End Sub
  Public Overridable Sub edLeft_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edLeft.Leave
    Settaproprietà(1)
  End Sub
  Public Overridable Sub edHeight_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edHeight.Leave
    Settaproprietà(2)
  End Sub
  Public Overridable Sub edWidth_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edWidth.Leave
    Settaproprietà(3)
  End Sub
  Public Overridable Sub ckMultiline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckMultiline.CheckedChanged
    Settaproprietà(4)
  End Sub
  Public Overridable Sub ckAutosize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckAutosize.CheckedChanged
    Settaproprietà(5)
  End Sub
  Public Overridable Sub ckBorder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckBorder.CheckedChanged
    Settaproprietà(6)
  End Sub
  Public Overridable Sub ckVisLabel_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckVisLabel.CheckedChanged
    Settaproprietà(7)
  End Sub

  Public Overridable Sub Settaproprietà(ByVal nProp As Integer)
    'nProp: 0=top, 1=left, 2=height, 3=width, 4=multiline, 5=autosize, 6=border 7=labelvisible
    Dim bsTmp As BorderStyle = Nothing

    Try
      Select Case nProp
        Case 0 : ctlIn.Top = NTSCInt(edTop.Text)
        Case 1 : ctlIn.Left = NTSCInt(edLeft.Text)
        Case 2
          If ctlIn.GetType.ToString.ToUpper.IndexOf("NTSINFORMATICA.FRM") > -1 Then
            CType(ctlIn, Form).MaximumSize = New Size(0, 0)
            CType(ctlIn, Form).MinimumSize = New Size(0, 0)
            CType(ctlIn, Form).Height = NTSCInt(edHeight.Text)
          Else
            ctlIn.Height = NTSCInt(edHeight.Text)
          End If
        Case 3
          If ctlIn.GetType.ToString.ToUpper.IndexOf("NTSINFORMATICA.FRM") > -1 Then
            CType(ctlIn, Form).MaximumSize = New Size(0, 0)
            CType(ctlIn, Form).MinimumSize = New Size(0, 0)
            CType(ctlIn, Form).Width = NTSCInt(edWidth.Text)
          Else
            ctlIn.Width = NTSCInt(edWidth.Text)
          End If
        Case 4 'If CType(ctlIn, NTSTextBox).Multiline <> ckMultiline.Checked Then CType(ctlIn, NTSTextBox).Multiline = ckMultiline.Checked
        Case 5
          Select Case ctlIn.GetType.ToString
            Case "NTSInformatica.NTSLabel"
              If CType(ctlIn, NTSLabel).AutoSize <> ckAutosize.Checked Then CType(ctlIn, NTSLabel).AutoSize = ckAutosize.Checked
            Case "NTSInformatica.NTSCheckBox"
              If CType(ctlIn, NTSCheckBox).AutoSize <> ckAutosize.Checked Then CType(ctlIn, NTSCheckBox).AutoSize = ckAutosize.Checked
            Case "NTSInformatica.NTSRadioButton"
              If CType(ctlIn, NTSRadioButton).AutoSize <> ckAutosize.Checked Then CType(ctlIn, NTSRadioButton).AutoSize = ckAutosize.Checked
          End Select
        Case 6
          bsTmp = CType(IIf(ckBorder.Checked, BorderStyle.Fixed3D, BorderStyle.None), BorderStyle)
          Select Case ctlIn.GetType.ToString
            Case "NTSInformatica.NTSLabel"
              If CType(ctlIn, NTSLabel).BorderStyle <> bsTmp Then CType(ctlIn, NTSLabel).BorderStyle = bsTmp

            Case "NTSInformatica.NTSPanel"
              If ckBorder.Checked And CType(ctlIn, NTSPanel).BorderStyle <> DevExpress.XtraEditors.Controls.BorderStyles.Default Then
                CType(ctlIn, NTSPanel).BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default
              End If
              If ckBorder.Checked = False And CType(ctlIn, NTSPanel).BorderStyle <> DevExpress.XtraEditors.Controls.BorderStyles.NoBorder Then
                CType(ctlIn, NTSPanel).BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
              End If

            Case "NTSInformatica.NTSPictureBox"
              If ckBorder.Checked And CType(ctlIn, NTSPictureBox).BorderStyle <> DevExpress.XtraEditors.Controls.BorderStyles.Default Then
                CType(ctlIn, NTSPictureBox).BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default
              End If
              If ckBorder.Checked = False And CType(ctlIn, NTSPictureBox).BorderStyle <> DevExpress.XtraEditors.Controls.BorderStyles.NoBorder Then
                CType(ctlIn, NTSPictureBox).BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
              End If

          End Select
        Case 7
          'Select Case ctlIn.GetType.ToString
          '  Case "NTSInformatica.NTSComboBox"
          '    CType(ctlIn, NTSComboBox).LabelVisible = ckVisLabel.Checked
          'End Select
      End Select

      'rileggo le proprietà dal controllo, visto che potrei aver settato dei valori non corretti
      Compilaproprietà()

      ctlIn.BackColor = Color.Red
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub Compilaproprietà()
    Try
      lbName.Text = "Name: " & ctlIn.Name
      edTop.Text = ctlIn.Top.ToString
      edLeft.Text = ctlIn.Left.ToString
      edHeight.Text = ctlIn.Height.ToString
      edWidth.Text = ctlIn.Width.ToString

      ckAutosize.Enabled = False
      ckMultiline.Enabled = False
      ckBorder.Enabled = False
      ckVisLabel.Enabled = False

      Select Case ctlIn.GetType.ToString
        Case "NTSInformatica.NTSTextBoxStr"
          'ckMultiline.Enabled = True     'i controlli DevExpress non hanno la multilinea  
          'ckMultiline.Checked = CType(ctlIn, NTSTextBoxStr).Multiline
          ckAutosize.Checked = False
          ckBorder.Checked = False

        Case "NTSInformatica.NTSMemoBox"
          ckAutosize.Checked = False
          ckBorder.Checked = False

        Case "NTSInformatica.NTSLabel"
          ckAutosize.Enabled = True
          ckAutosize.Checked = CType(ctlIn, NTSLabel).AutoSize
          ckBorder.Enabled = True
          If CType(ctlIn, Label).BorderStyle <> BorderStyle.None Then ckBorder.Checked = True Else ckBorder.Checked = False
          ckMultiline.Checked = False

        Case "NTSInformatica.NTSCheckBox"
          'ckAutosize.Enabled = True
          'ckAutosize.Checked = CType(ctlIn, NTSCheckBox).AutoSize
          ckAutosize.Checked = False
          ckMultiline.Checked = False
          ckBorder.Checked = False

        Case "NTSInformatica.NTSRadioButton"
          'ckAutosize.Enabled = True
          'ckAutosize.Checked = CType(ctlIn, NTSRadioButton).AutoSize
          ckAutosize.Checked = False
          ckMultiline.Checked = False
          ckBorder.Checked = False

        Case "NTSInformatica.NTSPanel"
          ckAutosize.Checked = False
          ckMultiline.Checked = False
          ckBorder.Enabled = True
          If CType(ctlIn, Panel).BorderStyle <> BorderStyle.None Then ckBorder.Checked = True Else ckBorder.Checked = False

        Case "NTSInformatica.NTSPictureBox"
          ckAutosize.Checked = False
          ckMultiline.Checked = False
          ckBorder.Enabled = True
          If CType(ctlIn, NTSPictureBox).BorderStyle <> DevExpress.XtraEditors.Controls.BorderStyles.NoBorder Then ckBorder.Checked = True Else ckBorder.Checked = False

        Case "NTSInformatica.NTSComboBox"
          ckAutosize.Checked = False
          ckMultiline.Checked = False
          ckBorder.Checked = False
          ckVisLabel.Enabled = True
          'ckVisLabel.Checked = CType(ctlIn, NTSComboBox).LabelVisible

        Case Else
          ckAutosize.Checked = False
          ckMultiline.Checked = False
          ckBorder.Checked = False
      End Select

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edParent_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edParent.Enter
    strPTmp = edParent.Text
  End Sub

  Public Overridable Sub edParent_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edParent.Leave
    If edParent.Text.Trim = "" Then
      edParent.Text = strPTmp
      Return
    End If

    If strPTmp <> edParent.Text Then
      Dim oPar As Object = Me.NTSFindControlByName(ctlIn.FindForm, edParent.Text)
      If Not oPar Is Nothing Then
        ctlIn.Parent = CType(oPar, Control)
        edParent.Text = ctlIn.Parent.Name
        ctlIn.Top = 0
        ctlIn.Left = 0
        edTop.Text = "0"
        edLeft.Text = "0"
      Else
        oApp.MsgBoxErr("Parent inesistente")
        edParent.Text = strPTmp
      End If
    End If
  End Sub
End Class

