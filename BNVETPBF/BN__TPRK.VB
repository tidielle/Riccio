Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__TPRK
  Public oCallParams As CLE__CLDP
  Public bOk As Boolean = False

  Public Overridable Sub InitializeComponent()
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOk = New NTSInformatica.NTSButton
    Me.NtsPanel1 = New NTSInformatica.NTSPanel
    Me.CkR1 = New NTSInformatica.NTSCheckBox
    Me.ckX = New NTSInformatica.NTSCheckBox
    Me.ckV = New NTSInformatica.NTSCheckBox
    Me.ckR = New NTSInformatica.NTSCheckBox
    Me.ckQ = New NTSInformatica.NTSCheckBox
    Me.ckS = New NTSInformatica.NTSCheckBox
    Me.ckN = New NTSInformatica.NTSCheckBox
    Me.ckI = New NTSInformatica.NTSCheckBox
    Me.ckF = New NTSInformatica.NTSCheckBox
    Me.ckE = New NTSInformatica.NTSCheckBox
    Me.ckC = New NTSInformatica.NTSCheckBox
    Me.ckB = New NTSInformatica.NTSCheckBox
    Me.ckA = New NTSInformatica.NTSCheckBox
    Me.NtsPanel2 = New NTSInformatica.NTSPanel
    Me.ckH = New NTSInformatica.NTSCheckBox
    Me.ckO1 = New NTSInformatica.NTSCheckBox
    Me.ckO = New NTSInformatica.NTSCheckBox
    Me.ckZ = New NTSInformatica.NTSCheckBox
    Me.ckW = New NTSInformatica.NTSCheckBox
    Me.ckT = New NTSInformatica.NTSCheckBox
    Me.ckM = New NTSInformatica.NTSCheckBox
    Me.ckJ = New NTSInformatica.NTSCheckBox
    Me.ckL = New NTSInformatica.NTSCheckBox
    Me.cmdSelAll = New NTSInformatica.NTSButton
    Me.cmdDeselAll = New NTSInformatica.NTSButton
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel1.SuspendLayout()
    CType(Me.CkR1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckX.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckV.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckQ.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckF.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckC.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsPanel2.SuspendLayout()
    CType(Me.ckH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckO1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckZ.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckW.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckJ.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(436, 37)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(105, 26)
    Me.cmdAnnulla.TabIndex = 1
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOk
    '
    Me.cmdOk.ImageText = ""
    Me.cmdOk.Location = New System.Drawing.Point(436, 8)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(105, 26)
    Me.cmdOk.TabIndex = 2
    Me.cmdOk.Text = "&Conferma"
    '
    'NtsPanel1
    '
    Me.NtsPanel1.AllowDrop = True
    Me.NtsPanel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel1.Appearance.Options.UseBackColor = True
    Me.NtsPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel1.Controls.Add(Me.CkR1)
    Me.NtsPanel1.Controls.Add(Me.ckX)
    Me.NtsPanel1.Controls.Add(Me.ckV)
    Me.NtsPanel1.Controls.Add(Me.ckR)
    Me.NtsPanel1.Controls.Add(Me.ckQ)
    Me.NtsPanel1.Controls.Add(Me.ckS)
    Me.NtsPanel1.Controls.Add(Me.ckN)
    Me.NtsPanel1.Controls.Add(Me.ckI)
    Me.NtsPanel1.Controls.Add(Me.ckF)
    Me.NtsPanel1.Controls.Add(Me.ckE)
    Me.NtsPanel1.Controls.Add(Me.ckC)
    Me.NtsPanel1.Controls.Add(Me.ckB)
    Me.NtsPanel1.Controls.Add(Me.ckA)
    Me.NtsPanel1.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel1.Location = New System.Drawing.Point(2, 8)
    Me.NtsPanel1.Name = "NtsPanel1"
    Me.NtsPanel1.NTSActiveTrasparency = True
    Me.NtsPanel1.Size = New System.Drawing.Size(207, 262)
    Me.NtsPanel1.TabIndex = 9
    Me.NtsPanel1.Text = "NtsPanel1"
    '
    'CkR1
    '
    Me.CkR1.Cursor = System.Windows.Forms.Cursors.Default
    Me.CkR1.Location = New System.Drawing.Point(3, 237)
    Me.CkR1.Name = "CkR1"
    Me.CkR1.NTSCheckValue = "S"
    Me.CkR1.NTSUnCheckValue = "N"
    Me.CkR1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.CkR1.Properties.Appearance.Options.UseBackColor = True
    Me.CkR1.Properties.Caption = "Impegno di commessa"
    Me.CkR1.Size = New System.Drawing.Size(192, 19)
    Me.CkR1.TabIndex = 82
    '
    'ckX
    '
    Me.ckX.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.ckX.Location = New System.Drawing.Point(3, 218)
    Me.ckX.Name = "ckX"
    Me.ckX.NTSCheckValue = "S"
    Me.ckX.NTSUnCheckValue = "N"
    Me.ckX.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckX.Properties.Appearance.Options.UseBackColor = True
    Me.ckX.Properties.Caption = "Impegni di trasferimento"
    Me.ckX.Size = New System.Drawing.Size(192, 19)
    Me.ckX.TabIndex = 81
    '
    'ckV
    '
    Me.ckV.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckV.Location = New System.Drawing.Point(3, 199)
    Me.ckV.Name = "ckV"
    Me.ckV.NTSCheckValue = "S"
    Me.ckV.NTSUnCheckValue = "N"
    Me.ckV.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckV.Properties.Appearance.Options.UseBackColor = True
    Me.ckV.Properties.Caption = "Impegni clienti aperti"
    Me.ckV.Size = New System.Drawing.Size(192, 19)
    Me.ckV.TabIndex = 80
    '
    'ckR
    '
    Me.ckR.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckR.Location = New System.Drawing.Point(3, 180)
    Me.ckR.Name = "ckR"
    Me.ckR.NTSCheckValue = "S"
    Me.ckR.NTSUnCheckValue = "N"
    Me.ckR.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckR.Properties.Appearance.Options.UseBackColor = True
    Me.ckR.Properties.Caption = "Impegni clienti"
    Me.ckR.Size = New System.Drawing.Size(192, 19)
    Me.ckR.TabIndex = 79
    '
    'ckQ
    '
    Me.ckQ.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckQ.Location = New System.Drawing.Point(3, 161)
    Me.ckQ.Name = "ckQ"
    Me.ckQ.NTSCheckValue = "S"
    Me.ckQ.NTSUnCheckValue = "N"
    Me.ckQ.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckQ.Properties.Appearance.Options.UseBackColor = True
    Me.ckQ.Properties.Caption = "Preventivi/offerte"
    Me.ckQ.Size = New System.Drawing.Size(192, 19)
    Me.ckQ.TabIndex = 78
    '
    'ckS
    '
    Me.ckS.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckS.Location = New System.Drawing.Point(3, 136)
    Me.ckS.Name = "ckS"
    Me.ckS.NTSCheckValue = "S"
    Me.ckS.NTSUnCheckValue = "N"
    Me.ckS.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckS.Properties.Appearance.Options.UseBackColor = True
    Me.ckS.Properties.Caption = "Fatture/Ric. fiscali emesse"
    Me.ckS.Size = New System.Drawing.Size(192, 19)
    Me.ckS.TabIndex = 72
    '
    'ckN
    '
    Me.ckN.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckN.Location = New System.Drawing.Point(3, 117)
    Me.ckN.Name = "ckN"
    Me.ckN.NTSCheckValue = "S"
    Me.ckN.NTSUnCheckValue = "N"
    Me.ckN.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckN.Properties.Appearance.Options.UseBackColor = True
    Me.ckN.Properties.Caption = "Note accred. emesse"
    Me.ckN.Size = New System.Drawing.Size(192, 19)
    Me.ckN.TabIndex = 71
    '
    'ckI
    '
    Me.ckI.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckI.Location = New System.Drawing.Point(3, 98)
    Me.ckI.Name = "ckI"
    Me.ckI.NTSCheckValue = "S"
    Me.ckI.NTSUnCheckValue = "N"
    Me.ckI.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckI.Properties.Appearance.Options.UseBackColor = True
    Me.ckI.Properties.Caption = "Riemissione ric. fiscali"
    Me.ckI.Size = New System.Drawing.Size(192, 19)
    Me.ckI.TabIndex = 70
    '
    'ckF
    '
    Me.ckF.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckF.Location = New System.Drawing.Point(3, 79)
    Me.ckF.Name = "ckF"
    Me.ckF.NTSCheckValue = "S"
    Me.ckF.NTSUnCheckValue = "N"
    Me.ckF.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckF.Properties.Appearance.Options.UseBackColor = True
    Me.ckF.Properties.Caption = "Ric. fiscali emesse"
    Me.ckF.Size = New System.Drawing.Size(192, 19)
    Me.ckF.TabIndex = 69
    '
    'ckE
    '
    Me.ckE.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckE.Location = New System.Drawing.Point(3, 60)
    Me.ckE.Name = "ckE"
    Me.ckE.NTSCheckValue = "S"
    Me.ckE.NTSUnCheckValue = "N"
    Me.ckE.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckE.Properties.Appearance.Options.UseBackColor = True
    Me.ckE.Properties.Caption = "Note addebito emesse"
    Me.ckE.Size = New System.Drawing.Size(192, 19)
    Me.ckE.TabIndex = 68
    '
    'ckC
    '
    Me.ckC.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckC.Location = New System.Drawing.Point(3, 41)
    Me.ckC.Name = "ckC"
    Me.ckC.NTSCheckValue = "S"
    Me.ckC.NTSUnCheckValue = "N"
    Me.ckC.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckC.Properties.Appearance.Options.UseBackColor = True
    Me.ckC.Properties.Caption = "Corrispettivi emessi"
    Me.ckC.Size = New System.Drawing.Size(192, 19)
    Me.ckC.TabIndex = 67
    '
    'ckB
    '
    Me.ckB.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckB.Location = New System.Drawing.Point(3, 22)
    Me.ckB.Name = "ckB"
    Me.ckB.NTSCheckValue = "S"
    Me.ckB.NTSUnCheckValue = "N"
    Me.ckB.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckB.Properties.Appearance.Options.UseBackColor = True
    Me.ckB.Properties.Caption = "DDT emessi"
    Me.ckB.Size = New System.Drawing.Size(192, 19)
    Me.ckB.TabIndex = 66
    '
    'ckA
    '
    Me.ckA.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckA.Location = New System.Drawing.Point(3, 3)
    Me.ckA.Name = "ckA"
    Me.ckA.NTSCheckValue = "S"
    Me.ckA.NTSUnCheckValue = "N"
    Me.ckA.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckA.Properties.Appearance.Options.UseBackColor = True
    Me.ckA.Properties.Caption = "Fatture emesse"
    Me.ckA.Size = New System.Drawing.Size(192, 19)
    Me.ckA.TabIndex = 65
    '
    'NtsPanel2
    '
    Me.NtsPanel2.AllowDrop = True
    Me.NtsPanel2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.NtsPanel2.Appearance.Options.UseBackColor = True
    Me.NtsPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.NtsPanel2.Controls.Add(Me.ckH)
    Me.NtsPanel2.Controls.Add(Me.ckO1)
    Me.NtsPanel2.Controls.Add(Me.ckO)
    Me.NtsPanel2.Controls.Add(Me.ckZ)
    Me.NtsPanel2.Controls.Add(Me.ckW)
    Me.NtsPanel2.Controls.Add(Me.ckT)
    Me.NtsPanel2.Controls.Add(Me.ckM)
    Me.NtsPanel2.Controls.Add(Me.ckJ)
    Me.NtsPanel2.Controls.Add(Me.ckL)
    Me.NtsPanel2.Cursor = System.Windows.Forms.Cursors.Default
    Me.NtsPanel2.Location = New System.Drawing.Point(215, 8)
    Me.NtsPanel2.Name = "NtsPanel2"
    Me.NtsPanel2.NTSActiveTrasparency = True
    Me.NtsPanel2.Size = New System.Drawing.Size(202, 262)
    Me.NtsPanel2.TabIndex = 10
    Me.NtsPanel2.Text = "NtsPanel2"
    '
    'ckH
    '
    Me.ckH.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckH.Location = New System.Drawing.Point(3, 199)
    Me.ckH.Name = "ckH"
    Me.ckH.NTSCheckValue = "S"
    Me.ckH.NTSUnCheckValue = "N"
    Me.ckH.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckH.Properties.Appearance.Options.UseBackColor = True
    Me.ckH.Properties.Caption = "Ordini di produzione"
    Me.ckH.Size = New System.Drawing.Size(193, 19)
    Me.ckH.TabIndex = 81
    '
    'ckO1
    '
    Me.ckO1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckO1.Location = New System.Drawing.Point(3, 161)
    Me.ckO1.Name = "ckO1"
    Me.ckO1.NTSCheckValue = "S"
    Me.ckO1.NTSUnCheckValue = "N"
    Me.ckO1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckO1.Properties.Appearance.Options.UseBackColor = True
    Me.ckO1.Properties.Caption = "Ordini fornitore aperti"
    Me.ckO1.Size = New System.Drawing.Size(193, 19)
    Me.ckO1.TabIndex = 80
    '
    'ckO
    '
    Me.ckO.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckO.Location = New System.Drawing.Point(3, 180)
    Me.ckO.Name = "ckO"
    Me.ckO.NTSCheckValue = "S"
    Me.ckO.NTSUnCheckValue = "N"
    Me.ckO.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckO.Properties.Appearance.Options.UseBackColor = True
    Me.ckO.Properties.Caption = "Ordini fornitore"
    Me.ckO.Size = New System.Drawing.Size(193, 19)
    Me.ckO.TabIndex = 79
    '
    'ckZ
    '
    Me.ckZ.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckZ.Location = New System.Drawing.Point(3, 79)
    Me.ckZ.Name = "ckZ"
    Me.ckZ.NTSCheckValue = "S"
    Me.ckZ.NTSUnCheckValue = "N"
    Me.ckZ.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckZ.Properties.Appearance.Options.UseBackColor = True
    Me.ckZ.Properties.Caption = "Bolle di movim. interne"
    Me.ckZ.Size = New System.Drawing.Size(193, 19)
    Me.ckZ.TabIndex = 78
    '
    'ckW
    '
    Me.ckW.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckW.Location = New System.Drawing.Point(3, 237)
    Me.ckW.Name = "ckW"
    Me.ckW.NTSCheckValue = "S"
    Me.ckW.NTSUnCheckValue = "N"
    Me.ckW.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckW.Properties.Appearance.Options.UseBackColor = True
    Me.ckW.Properties.Caption = "Note di prelievo"
    Me.ckW.Size = New System.Drawing.Size(193, 19)
    Me.ckW.TabIndex = 77
    '
    'ckT
    '
    Me.ckT.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckT.Location = New System.Drawing.Point(3, 60)
    Me.ckT.Name = "ckT"
    Me.ckT.NTSCheckValue = "S"
    Me.ckT.NTSUnCheckValue = "N"
    Me.ckT.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckT.Properties.Appearance.Options.UseBackColor = True
    Me.ckT.Properties.Caption = "Carichi da produzione"
    Me.ckT.Size = New System.Drawing.Size(193, 19)
    Me.ckT.TabIndex = 76
    '
    'ckM
    '
    Me.ckM.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckM.Location = New System.Drawing.Point(3, 41)
    Me.ckM.Name = "ckM"
    Me.ckM.NTSCheckValue = "S"
    Me.ckM.NTSUnCheckValue = "N"
    Me.ckM.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckM.Properties.Appearance.Options.UseBackColor = True
    Me.ckM.Properties.Caption = "DDT ricevuti"
    Me.ckM.Size = New System.Drawing.Size(193, 19)
    Me.ckM.TabIndex = 75
    '
    'ckJ
    '
    Me.ckJ.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckJ.Location = New System.Drawing.Point(3, 22)
    Me.ckJ.Name = "ckJ"
    Me.ckJ.NTSCheckValue = "S"
    Me.ckJ.NTSUnCheckValue = "N"
    Me.ckJ.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckJ.Properties.Appearance.Options.UseBackColor = True
    Me.ckJ.Properties.Caption = "Note accredito ricevute"
    Me.ckJ.Size = New System.Drawing.Size(193, 19)
    Me.ckJ.TabIndex = 74
    '
    'ckL
    '
    Me.ckL.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckL.Location = New System.Drawing.Point(3, 3)
    Me.ckL.Name = "ckL"
    Me.ckL.NTSCheckValue = "S"
    Me.ckL.NTSUnCheckValue = "N"
    Me.ckL.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckL.Properties.Appearance.Options.UseBackColor = True
    Me.ckL.Properties.Caption = "Fatture ricevute"
    Me.ckL.Size = New System.Drawing.Size(193, 19)
    Me.ckL.TabIndex = 73
    '
    'cmdSelAll
    '
    Me.cmdSelAll.ImageText = ""
    Me.cmdSelAll.Location = New System.Drawing.Point(437, 207)
    Me.cmdSelAll.Name = "cmdSelAll"
    Me.cmdSelAll.Size = New System.Drawing.Size(105, 26)
    Me.cmdSelAll.TabIndex = 11
    Me.cmdSelAll.Text = "Seleziona tutti"
    '
    'cmdDeselAll
    '
    Me.cmdDeselAll.ImageText = ""
    Me.cmdDeselAll.Location = New System.Drawing.Point(437, 238)
    Me.cmdDeselAll.Name = "cmdDeselAll"
    Me.cmdDeselAll.Size = New System.Drawing.Size(105, 26)
    Me.cmdDeselAll.TabIndex = 12
    Me.cmdDeselAll.Text = "Deseleziona tutti"
    '
    'FRM__TPRK
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(554, 271)
    Me.Controls.Add(Me.cmdDeselAll)
    Me.Controls.Add(Me.cmdSelAll)
    Me.Controls.Add(Me.NtsPanel2)
    Me.Controls.Add(Me.NtsPanel1)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdOk)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__TPRK"
    Me.Text = "VISIBILE IN ZOOM SOLO IN DOCUMENTI DI TIPO"
    CType(Me.NtsPanel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel1.ResumeLayout(False)
    Me.NtsPanel1.PerformLayout()
    CType(Me.CkR1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckX.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckV.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckQ.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckF.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckC.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsPanel2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsPanel2.ResumeLayout(False)
    Me.NtsPanel2.PerformLayout()
    CType(Me.ckH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckO1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckZ.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckW.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckJ.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

    Return True
  End Function

  Public Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      ckH.NTSSetParam(oMenu, oApp.Tr(Me, 130184602901809370, "Ordini di produzione"), "S", "N")
      ckO1.NTSSetParam(oMenu, oApp.Tr(Me, 130184602901965626, "Ordini fornitore aperti"), "S", "N")
      ckO.NTSSetParam(oMenu, oApp.Tr(Me, 130184602902121882, "Ordini fornitore"), "S", "N")
      ckZ.NTSSetParam(oMenu, oApp.Tr(Me, 130184602902278138, "Bolle di movim. interne"), "S", "N")
      ckW.NTSSetParam(oMenu, oApp.Tr(Me, 130184602902434394, "Note di prelievo"), "S", "N")
      ckT.NTSSetParam(oMenu, oApp.Tr(Me, 130184602902590650, "Carichi da produzione"), "S", "N")
      ckM.NTSSetParam(oMenu, oApp.Tr(Me, 130184602902746919, "DDT ricevuti"), "S", "N")
      ckJ.NTSSetParam(oMenu, oApp.Tr(Me, 130184602902903171, "Note accredito ricevute"), "S", "N")
      ckL.NTSSetParam(oMenu, oApp.Tr(Me, 130184603585640517, "Fatture ricevute"), "S", "N")
      CkR1.NTSSetParam(oMenu, oApp.Tr(Me, 130184602903059431, "Impegno di commessa"), "S", "N")
      ckX.NTSSetParam(oMenu, oApp.Tr(Me, 130184602903215687, "Impegni di trasferimento"), "S", "N")
      ckV.NTSSetParam(oMenu, oApp.Tr(Me, 130184602903371956, "Impegni clienti aperti"), "S", "N")
      ckR.NTSSetParam(oMenu, oApp.Tr(Me, 130184602903528204, "Impegni clienti"), "S", "N")
      ckQ.NTSSetParam(oMenu, oApp.Tr(Me, 130184602903684460, "Preventivi/offerte"), "S", "N")
      ckS.NTSSetParam(oMenu, oApp.Tr(Me, 130184602903840720, "Fatture/Ric. fiscali emesse"), "S", "N")
      ckN.NTSSetParam(oMenu, oApp.Tr(Me, 130184602903996976, "Note accred. emesse"), "S", "N")
      ckI.NTSSetParam(oMenu, oApp.Tr(Me, 130184602904153228, "Riemissione ric. fiscali"), "S", "N")
      ckF.NTSSetParam(oMenu, oApp.Tr(Me, 130184602904309488, "Ric. fiscali emesse"), "S", "N")
      ckE.NTSSetParam(oMenu, oApp.Tr(Me, 130184602904465740, "Note addebito emesse"), "S", "N")
      ckC.NTSSetParam(oMenu, oApp.Tr(Me, 130184602904621996, "Corrispettivi emessi"), "S", "N")
      ckB.NTSSetParam(oMenu, oApp.Tr(Me, 130184602904778252, "DDT emessi"), "S", "N")
      ckA.NTSSetParam(oMenu, oApp.Tr(Me, 130184602904934508, "Fatture emesse"), "S", "N")

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

  Public Overridable Sub FRM__TPRK_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      bOk = False

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
      GctlApplicaDefaultValue()

      If CLN__STD.FRIENDLY Then
        'tipi documento non gestiti
        ckV.Visible = False
        ckX.Visible = False
        CkR1.Visible = False
        ckZ.Visible = False
        ckW.Visible = False
        ckO1.Visible = False
        ckH.Visible = False
      End If


    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Try
      bOk = True
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      bOk = False
      Me.Close()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelAll.Click
    Try
      ckH.Checked = True
      ckO1.Checked = True
      ckO.Checked = True
      ckZ.Checked = True
      ckW.Checked = True
      ckT.Checked = True
      ckM.Checked = True
      ckJ.Checked = True
      ckL.Checked = True
      CkR1.Checked = True
      ckX.Checked = True
      ckV.Checked = True
      ckR.Checked = True
      ckQ.Checked = True
      ckS.Checked = True
      ckN.Checked = True
      ckI.Checked = True
      ckF.Checked = True
      ckE.Checked = True
      ckC.Checked = True
      ckB.Checked = True
      ckA.Checked = True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdDeselAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeselAll.Click
    Try
      ckH.Checked = False
      ckO1.Checked = False
      ckO.Checked = False
      ckZ.Checked = False
      ckW.Checked = False
      ckT.Checked = False
      ckM.Checked = False
      ckJ.Checked = False
      ckL.Checked = False
      CkR1.Checked = False
      ckX.Checked = False
      ckV.Checked = False
      ckR.Checked = False
      ckQ.Checked = False
      ckS.Checked = False
      ckN.Checked = False
      ckI.Checked = False
      ckF.Checked = False
      ckE.Checked = False
      ckC.Checked = False
      ckB.Checked = False
      ckA.Checked = False
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

End Class
