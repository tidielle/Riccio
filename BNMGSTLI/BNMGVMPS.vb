Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGVMPS
  Public oCleStli As CLEMGSTLI
  Public strColName As String = ""
  Public grvVmps As NTSGridView
  Public bPrz As Boolean      'true = setta il prezzo, false setto lo sconto
  Public dValoreOut As Decimal = -1   'prezzo/sconto da applicare

  Public oCallParams As CLE__CLDP

  Private components As System.ComponentModel.IContainer

  Public Overridable Sub InitializeComponent()
    Me.pnPrezzo = New NTSInformatica.NTSPanel
    Me.edPrezzo = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.pnSconto = New NTSInformatica.NTSPanel
    Me.edSconto = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel2 = New NTSInformatica.NTSLabel
    Me.pnVmps = New NTSInformatica.NTSPanel
    Me.opAggiungi = New NTSInformatica.NTSRadioButton
    Me.opSostituisci = New NTSInformatica.NTSRadioButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnPrezzo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnPrezzo.SuspendLayout()
    CType(Me.edPrezzo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSconto, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSconto.SuspendLayout()
    CType(Me.edSconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnVmps, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnVmps.SuspendLayout()
    CType(Me.opAggiungi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opSostituisci.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnPrezzo
    '
    Me.pnPrezzo.AllowDrop = True
    Me.pnPrezzo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnPrezzo.Appearance.Options.UseBackColor = True
    Me.pnPrezzo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnPrezzo.Controls.Add(Me.edPrezzo)
    Me.pnPrezzo.Controls.Add(Me.NtsLabel1)
    Me.pnPrezzo.Location = New System.Drawing.Point(4, 4)
    Me.pnPrezzo.Name = "pnPrezzo"
    Me.pnPrezzo.NTSActiveTrasparency = True
    Me.pnPrezzo.Size = New System.Drawing.Size(168, 32)
    Me.pnPrezzo.TabIndex = 0
    Me.pnPrezzo.Text = "NtsPanel1"
    '
    'edPrezzo
    '
    Me.edPrezzo.Location = New System.Drawing.Point(58, 3)
    Me.edPrezzo.Name = "edPrezzo"
    Me.edPrezzo.NTSDbField = ""
    Me.edPrezzo.NTSFormat = "0"
    Me.edPrezzo.NTSForzaVisZoom = False
    Me.edPrezzo.NTSOldValue = ""
    Me.edPrezzo.Properties.Appearance.Options.UseTextOptions = True
    Me.edPrezzo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPrezzo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPrezzo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPrezzo.Properties.AutoHeight = False
    Me.edPrezzo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPrezzo.Properties.MaxLength = 65536
    Me.edPrezzo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPrezzo.Size = New System.Drawing.Size(100, 20)
    Me.edPrezzo.TabIndex = 1
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(4, 6)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(39, 13)
    Me.NtsLabel1.TabIndex = 0
    Me.NtsLabel1.Text = "Prezzo"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'pnSconto
    '
    Me.pnSconto.AllowDrop = True
    Me.pnSconto.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSconto.Appearance.Options.UseBackColor = True
    Me.pnSconto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSconto.Controls.Add(Me.edSconto)
    Me.pnSconto.Controls.Add(Me.NtsLabel2)
    Me.pnSconto.Location = New System.Drawing.Point(4, 42)
    Me.pnSconto.Name = "pnSconto"
    Me.pnSconto.NTSActiveTrasparency = True
    Me.pnSconto.Size = New System.Drawing.Size(168, 31)
    Me.pnSconto.TabIndex = 1
    Me.pnSconto.Text = "NtsPanel1"
    '
    'edSconto
    '
    Me.edSconto.Location = New System.Drawing.Point(58, 3)
    Me.edSconto.Name = "edSconto"
    Me.edSconto.NTSDbField = ""
    Me.edSconto.NTSFormat = "0"
    Me.edSconto.NTSForzaVisZoom = False
    Me.edSconto.NTSOldValue = ""
    Me.edSconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edSconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSconto.Properties.AutoHeight = False
    Me.edSconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSconto.Properties.MaxLength = 65536
    Me.edSconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSconto.Size = New System.Drawing.Size(100, 20)
    Me.edSconto.TabIndex = 1
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(4, 6)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(40, 13)
    Me.NtsLabel2.TabIndex = 0
    Me.NtsLabel2.Text = "Sconto"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'pnVmps
    '
    Me.pnVmps.AllowDrop = True
    Me.pnVmps.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnVmps.Appearance.Options.UseBackColor = True
    Me.pnVmps.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnVmps.Controls.Add(Me.opAggiungi)
    Me.pnVmps.Controls.Add(Me.opSostituisci)
    Me.pnVmps.Location = New System.Drawing.Point(178, 4)
    Me.pnVmps.Name = "pnVmps"
    Me.pnVmps.NTSActiveTrasparency = True
    Me.pnVmps.Size = New System.Drawing.Size(81, 69)
    Me.pnVmps.TabIndex = 2
    Me.pnVmps.Text = "NtsPanel1"
    '
    'opAggiungi
    '
    Me.opAggiungi.Location = New System.Drawing.Point(3, 29)
    Me.opAggiungi.Name = "opAggiungi"
    Me.opAggiungi.NTSCheckValue = "S"
    Me.opAggiungi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opAggiungi.Properties.Appearance.Options.UseBackColor = True
    Me.opAggiungi.Properties.AutoHeight = False
    Me.opAggiungi.Properties.Caption = "Aggiungi"
    Me.opAggiungi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opAggiungi.Size = New System.Drawing.Size(75, 19)
    Me.opAggiungi.TabIndex = 1
    '
    'opSostituisci
    '
    Me.opSostituisci.EditValue = True
    Me.opSostituisci.Location = New System.Drawing.Point(4, 4)
    Me.opSostituisci.Name = "opSostituisci"
    Me.opSostituisci.NTSCheckValue = "S"
    Me.opSostituisci.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opSostituisci.Properties.Appearance.Options.UseBackColor = True
    Me.opSostituisci.Properties.AutoHeight = False
    Me.opSostituisci.Properties.Caption = "Sostituisci"
    Me.opSostituisci.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opSostituisci.Size = New System.Drawing.Size(75, 19)
    Me.opSostituisci.TabIndex = 0
    '
    'cmdConferma
    '
    Me.cmdConferma.ImagePath = ""
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(265, 4)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(85, 23)
    Me.cmdConferma.TabIndex = 3
    Me.cmdConferma.Text = "Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(265, 29)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(85, 23)
    Me.cmdAnnulla.TabIndex = 4
    Me.cmdAnnulla.Text = "Annulla"
    '
    'FRMMGVMPS
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(356, 79)
    Me.Controls.Add(Me.cmdAnnulla)
    Me.Controls.Add(Me.cmdConferma)
    Me.Controls.Add(Me.pnVmps)
    Me.Controls.Add(Me.pnSconto)
    Me.Controls.Add(Me.pnPrezzo)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGVMPS"
    Me.Text = "VARIAZIONE MASSIVA PREZZI/SCONTI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnPrezzo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnPrezzo.ResumeLayout(False)
    Me.pnPrezzo.PerformLayout()
    CType(Me.edPrezzo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSconto, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSconto.ResumeLayout(False)
    Me.pnSconto.PerformLayout()
    CType(Me.edSconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnVmps, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnVmps.ResumeLayout(False)
    Me.pnVmps.PerformLayout()
    CType(Me.opAggiungi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opSostituisci.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipo, dttTipoSc As New DataTable
    Dim i As Integer = 0
    Try
      edPrezzo.NTSSetParam(oMenu, "Variazione prezzo", oApp.FormatPrzUn, 12, 0, 9999999999999)
      edSconto.NTSSetParam(oMenu, "Variazione sconto", oApp.FormatSconti, 6, 0, 100)

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

    '''------------------------------------------------
    '''creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    ''Dim strErr As String = ""
    ''Dim oTmp As Object = Nothing
    ''If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGSTLI", "BEMGSTLI", oTmp, strErr, False, "", "") = False Then
    ''  oApp.MsgBoxErr(oApp.Tr(Me, 128496233436616000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
    ''  Return False
    ''End If
    ''oCleStli = CType(oTmp, CLEMGSTLI)
    '''------------------------------------------------
    ''bRemoting = Menu.Remoting("BNMGSTLI", strRemoteServer, strRemotePort)
    ''AddHandler oCleStli.RemoteEvent, AddressOf GestisciEventiEntity
    ''If oCleStli.Init(oApp, oScript, oMenu.oCleComm, "MOVMAG", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Sub FRMMGVMPS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      'abilito il pannnello sconto o prezzo in base alla colonna su cui sono posizionato
      If strColName = "ls_prz1" Or strColName = "ls_prz2" Or strColName = "ls_prz3" Or _
         strColName = "ls_prz4" Or strColName = "ls_prz5" Then

        pnSconto.Visible = False
      Else
        pnPrezzo.Visible = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try

  End Sub

  Public Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try 'per il prezzo:
      If pnPrezzo.Visible Then
        bPrz = True
        dValoreOut = NTSCDec(edPrezzo.Text) 'variazione del prezzo
      ElseIf pnSconto.Visible Then 'per lo sconto
        bPrz = False
        dValoreOut = NTSCDec(edSconto.Text) 'variazione dello sconto
        For i As Integer = grvVmps.RowCount - 1 To 0 Step -1
          'controllo che lo sconto totale non sia maggiore di 100
          If opAggiungi.Checked = True And dValoreOut + NTSCDec(grvVmps.GetRowCellValue(i, strColName)) > 100 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 131061344575630068, "In almeno una cella, lo sconto totale risulta maggiore di 100, diminuire la quantità da aggiungere!"))
            Return
          End If
        Next
      End If

      Me.Close()
      Return

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    dValoreOut = -1 'ho fatto annulla
    Me.Close()
    Return
  End Sub
End Class
