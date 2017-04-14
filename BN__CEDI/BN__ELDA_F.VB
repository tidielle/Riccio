Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__ELDA_F

#Region "Variabili"
  Public oCleCedi As CLE__CEDI
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer

  Public WithEvents frDatiEliminare As NTSInformatica.NTSGroupBox
  Public WithEvents cmdSelAll As NTSInformatica.NTSButton
  Public WithEvents cmdDesAll As NTSInformatica.NTSButton
  Public WithEvents pnElda As NTSInformatica.NTSPanel
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdOK As NTSInformatica.NTSButton
  Public WithEvents ckPR As NTSInformatica.NTSCheckBox
  Public WithEvents ckOR As NTSInformatica.NTSCheckBox
  Public WithEvents ckMG As NTSInformatica.NTSCheckBox
  Public WithEvents ckCG_1 As NTSInformatica.NTSCheckBox
  Public WithEvents ckMantieni As NTSInformatica.NTSCheckBox
  Public WithEvents ckTT As NTSInformatica.NTSCheckBox
  Public WithEvents ckDWH As NTSInformatica.NTSCheckBox
#End Region

  Private Sub InitializeComponent()
    Me.frDatiEliminare = New NTSInformatica.NTSGroupBox
    Me.ckStatven = New NTSInformatica.NTSCheckBox
    Me.ckMantieni = New NTSInformatica.NTSCheckBox
    Me.ckTT = New NTSInformatica.NTSCheckBox
    Me.ckDWH = New NTSInformatica.NTSCheckBox
    Me.ckPR = New NTSInformatica.NTSCheckBox
    Me.ckOR = New NTSInformatica.NTSCheckBox
    Me.ckMG = New NTSInformatica.NTSCheckBox
    Me.ckCG_1 = New NTSInformatica.NTSCheckBox
    Me.cmdSelAll = New NTSInformatica.NTSButton
    Me.cmdDesAll = New NTSInformatica.NTSButton
    Me.pnElda = New NTSInformatica.NTSPanel
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdOK = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.frDatiEliminare, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.frDatiEliminare.SuspendLayout()
    CType(Me.ckStatven.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMantieni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckDWH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckMG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCG_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnElda, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnElda.SuspendLayout()
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
    'frDatiEliminare
    '
    Me.frDatiEliminare.AllowDrop = True
    Me.frDatiEliminare.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.frDatiEliminare.Appearance.Options.UseBackColor = True
    Me.frDatiEliminare.Controls.Add(Me.ckStatven)
    Me.frDatiEliminare.Controls.Add(Me.ckMantieni)
    Me.frDatiEliminare.Controls.Add(Me.ckTT)
    Me.frDatiEliminare.Controls.Add(Me.ckDWH)
    Me.frDatiEliminare.Controls.Add(Me.ckPR)
    Me.frDatiEliminare.Controls.Add(Me.ckOR)
    Me.frDatiEliminare.Controls.Add(Me.ckMG)
    Me.frDatiEliminare.Controls.Add(Me.ckCG_1)
    Me.frDatiEliminare.Cursor = System.Windows.Forms.Cursors.Default
    Me.frDatiEliminare.Location = New System.Drawing.Point(12, 13)
    Me.frDatiEliminare.Name = "frDatiEliminare"
    Me.frDatiEliminare.Size = New System.Drawing.Size(553, 167)
    Me.frDatiEliminare.TabIndex = 0
    Me.frDatiEliminare.Text = "Dati da eliminare: "
    '
    'ckStatven
    '
    Me.ckStatven.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckStatven.Location = New System.Drawing.Point(17, 123)
    Me.ckStatven.Name = "ckStatven"
    Me.ckStatven.NTSCheckValue = "S"
    Me.ckStatven.NTSUnCheckValue = "N"
    Me.ckStatven.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckStatven.Properties.Appearance.Options.UseBackColor = True
    Me.ckStatven.Properties.AutoHeight = False
    Me.ckStatven.Properties.Caption = "Statistiche (vecchio modulo)"
    Me.ckStatven.Size = New System.Drawing.Size(169, 19)
    Me.ckStatven.TabIndex = 15
    '
    'ckMantieni
    '
    Me.ckMantieni.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMantieni.Location = New System.Drawing.Point(373, 43)
    Me.ckMantieni.Name = "ckMantieni"
    Me.ckMantieni.NTSCheckValue = "S"
    Me.ckMantieni.NTSUnCheckValue = "N"
    Me.ckMantieni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMantieni.Properties.Appearance.Options.UseBackColor = True
    Me.ckMantieni.Properties.AutoHeight = False
    Me.ckMantieni.Properties.Caption = "Mantieni &ultimi costi d'acquisto"
    Me.ckMantieni.Size = New System.Drawing.Size(175, 19)
    Me.ckMantieni.TabIndex = 14
    '
    'ckTT
    '
    Me.ckTT.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTT.Location = New System.Drawing.Point(17, 143)
    Me.ckTT.Name = "ckTT"
    Me.ckTT.NTSCheckValue = "S"
    Me.ckTT.NTSUnCheckValue = "N"
    Me.ckTT.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTT.Properties.Appearance.Options.UseBackColor = True
    Me.ckTT.Properties.AutoHeight = False
    Me.ckTT.Properties.Caption = "&Tabelle Temporanee"
    Me.ckTT.Size = New System.Drawing.Size(350, 19)
    Me.ckTT.TabIndex = 13
    '
    'ckDWH
    '
    Me.ckDWH.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckDWH.Location = New System.Drawing.Point(17, 103)
    Me.ckDWH.Name = "ckDWH"
    Me.ckDWH.NTSCheckValue = "S"
    Me.ckDWH.NTSUnCheckValue = "N"
    Me.ckDWH.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckDWH.Properties.Appearance.Options.UseBackColor = True
    Me.ckDWH.Properties.AutoHeight = False
    Me.ckDWH.Properties.Caption = "Datawarehouse"
    Me.ckDWH.Size = New System.Drawing.Size(169, 19)
    Me.ckDWH.TabIndex = 10
    '
    'ckPR
    '
    Me.ckPR.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckPR.Location = New System.Drawing.Point(17, 83)
    Me.ckPR.Name = "ckPR"
    Me.ckPR.NTSCheckValue = "S"
    Me.ckPR.NTSUnCheckValue = "N"
    Me.ckPR.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPR.Properties.Appearance.Options.UseBackColor = True
    Me.ckPR.Properties.AutoHeight = False
    Me.ckPR.Properties.Caption = "&Provvigioni e progressivi agenti"
    Me.ckPR.Size = New System.Drawing.Size(350, 19)
    Me.ckPR.TabIndex = 6
    '
    'ckOR
    '
    Me.ckOR.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOR.Location = New System.Drawing.Point(17, 63)
    Me.ckOR.Name = "ckOR"
    Me.ckOR.NTSCheckValue = "S"
    Me.ckOR.NTSUnCheckValue = "N"
    Me.ckOR.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOR.Properties.Appearance.Options.UseBackColor = True
    Me.ckOR.Properties.AutoHeight = False
    Me.ckOR.Properties.Caption = "&Ordini, Impegni, preventivi"
    Me.ckOR.Size = New System.Drawing.Size(169, 19)
    Me.ckOR.TabIndex = 4
    '
    'ckMG
    '
    Me.ckMG.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckMG.Location = New System.Drawing.Point(17, 43)
    Me.ckMG.Name = "ckMG"
    Me.ckMG.NTSCheckValue = "S"
    Me.ckMG.NTSUnCheckValue = "N"
    Me.ckMG.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckMG.Properties.Appearance.Options.UseBackColor = True
    Me.ckMG.Properties.AutoHeight = False
    Me.ckMG.Properties.Caption = "Mo&vimenti  e progressivi di magazzino"
    Me.ckMG.Size = New System.Drawing.Size(236, 19)
    Me.ckMG.TabIndex = 2
    '
    'ckCG_1
    '
    Me.ckCG_1.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCG_1.Location = New System.Drawing.Point(17, 23)
    Me.ckCG_1.Name = "ckCG_1"
    Me.ckCG_1.NTSCheckValue = "S"
    Me.ckCG_1.NTSUnCheckValue = "N"
    Me.ckCG_1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCG_1.Properties.Appearance.Options.UseBackColor = True
    Me.ckCG_1.Properties.AutoHeight = False
    Me.ckCG_1.Properties.Caption = "&Movimenti contabili, IVA, scadenziario, progressivi di contabilità, progressivi " & _
        "IVA"
    Me.ckCG_1.Size = New System.Drawing.Size(436, 19)
    Me.ckCG_1.TabIndex = 0
    '
    'cmdSelAll
    '
    Me.cmdSelAll.ImagePath = ""
    Me.cmdSelAll.ImageText = ""
    Me.cmdSelAll.Location = New System.Drawing.Point(12, 186)
    Me.cmdSelAll.Name = "cmdSelAll"
    Me.cmdSelAll.NTSContextMenu = Nothing
    Me.cmdSelAll.Size = New System.Drawing.Size(139, 23)
    Me.cmdSelAll.TabIndex = 1
    Me.cmdSelAll.Text = "&Seleziona tutto"
    '
    'cmdDesAll
    '
    Me.cmdDesAll.ImagePath = ""
    Me.cmdDesAll.ImageText = ""
    Me.cmdDesAll.Location = New System.Drawing.Point(157, 186)
    Me.cmdDesAll.Name = "cmdDesAll"
    Me.cmdDesAll.NTSContextMenu = Nothing
    Me.cmdDesAll.Size = New System.Drawing.Size(139, 23)
    Me.cmdDesAll.TabIndex = 2
    Me.cmdDesAll.Text = "&Deseleziona tutto"
    '
    'pnElda
    '
    Me.pnElda.AllowDrop = True
    Me.pnElda.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnElda.Appearance.Options.UseBackColor = True
    Me.pnElda.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnElda.Controls.Add(Me.cmdAnnulla)
    Me.pnElda.Controls.Add(Me.cmdOK)
    Me.pnElda.Controls.Add(Me.cmdDesAll)
    Me.pnElda.Controls.Add(Me.cmdSelAll)
    Me.pnElda.Controls.Add(Me.frDatiEliminare)
    Me.pnElda.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnElda.Location = New System.Drawing.Point(0, -1)
    Me.pnElda.Name = "pnElda"
    Me.pnElda.NTSActiveTrasparency = True
    Me.pnElda.Size = New System.Drawing.Size(583, 247)
    Me.pnElda.TabIndex = 0
    Me.pnElda.Text = "NtsPanel1"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(471, 186)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(94, 23)
    Me.cmdAnnulla.TabIndex = 4
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdOK
    '
    Me.cmdOK.ImagePath = ""
    Me.cmdOK.ImageText = ""
    Me.cmdOK.Location = New System.Drawing.Point(371, 186)
    Me.cmdOK.Name = "cmdOK"
    Me.cmdOK.NTSContextMenu = Nothing
    Me.cmdOK.Size = New System.Drawing.Size(94, 23)
    Me.cmdOK.TabIndex = 3
    Me.cmdOK.Text = "O&k"
    '
    'FRM__ELDA_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(581, 217)
    Me.Controls.Add(Me.pnElda)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Name = "FRM__ELDA_F"
    Me.Text = "ELIMINAZIONE DATI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.frDatiEliminare, System.ComponentModel.ISupportInitialize).EndInit()
    Me.frDatiEliminare.ResumeLayout(False)
    Me.frDatiEliminare.PerformLayout()
    CType(Me.ckStatven.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMantieni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckDWH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckMG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCG_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnElda, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnElda.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      ckMantieni.NTSSetParam(oMenu, oApp.Tr(Me, 128345123272992397, "Mantieni &ultimi costi d'acquisto"), "S", "N")
      ckTT.NTSSetParam(oMenu, oApp.Tr(Me, 128345123273148647, "&Tabelle Temporanee"), "S", "N")
      ckDWH.NTSSetParam(oMenu, oApp.Tr(Me, 128345123273617397, "Datawarehouse"), "S", "N")
      ckStatven.NTSSetParam(oMenu, oApp.Tr(Me, 131006979630039012, "Statistiche (vecchio modulo)"), "S", "N")
      ckPR.NTSSetParam(oMenu, oApp.Tr(Me, 128345123274242397, "&Provvigioni e progressivi agenti"), "S", "N")
      ckOR.NTSSetParam(oMenu, oApp.Tr(Me, 128345123274554897, "&Ordini, Impegni, preventivi, proposte d'ordine e attività collegate, offerte CRM"), "S", "N")
      ckMG.NTSSetParam(oMenu, oApp.Tr(Me, 128345123274867397, "Mo&vimenti  e progressivi di magazzino, dati Lifo, Collaudi e RNC"), "S", "N")
      ckCG_1.NTSSetParam(oMenu, oApp.Tr(Me, 128345123275179897, "&Movimenti contabili, IVA, scadenziario, progressivi di contabilità, progressivi IVA e movimenti di C.A. "), "S", "N")
      ''non faccio lanciare lo zoom dal controllo visto che non devo cambiare il contenuto del campo ma riposizionare il dataset!!!

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
  Public Overridable Sub InitEntity(ByRef cleCedi As CLE__CEDI)
    oCleCedi = cleCedi
    AddHandler oCleCedi.RemoteEvent, AddressOf GestisciEventiEntity
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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function

#Region "EventiForm"
  Public Overridable Sub FRM__ELDA_F_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------


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

  Public Overridable Sub cmdSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelAll.Click
    Try
      ckCG_1.Checked = True
      ckMG.Checked = True
      ckMantieni.Checked = True
      ckOR.Checked = True
      ckPR.Checked = True
      ckDWH.Checked = True
      ckStatven.Checked = True
      ckTT.Checked = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdDesAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDesAll.Click
    Try
      ckCG_1.Checked = False
      ckMG.Checked = False
      ckMantieni.Checked = False
      ckOR.Checked = False
      ckPR.Checked = False
      ckDWH.Checked = False
      ckStatven.Checked = False
      ckTT.Checked = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
    Try
      '-----------------------------------------------------------------------------------------
      oCleCedi.bEldaCG_1 = CBool(ckCG_1.Checked)
      oCleCedi.bEldaCG_2 = False
      oCleCedi.bEldaMG = CBool(ckMG.Checked)
      oCleCedi.bEldaMantieni = CBool(ckMantieni.Checked)
      oCleCedi.bEldaPRB = False
      oCleCedi.bEldaOR = CBool(ckOR.Checked)
      oCleCedi.bEldaBUD = False
      oCleCedi.bEldaPR = CBool(ckPR.Checked)
      oCleCedi.bEldaPM_1 = False
      oCleCedi.bEldaPM_2 = False
      oCleCedi.bEldaCRM = False
      oCleCedi.bEldaDWH = CBool(ckDWH.Checked)
      oCleCedi.bEldaStatistiche = CBool(ckStatven.Checked)
      oCleCedi.bEldaTT = CBool(ckTT.Checked)
      oCleCedi.bEldaAS_1 = False
      oCleCedi.bEldaAS_2 = False
      oCleCedi.bEldaMovc = False
      oCleCedi.bEldaAnac = False
      oCleCedi.bEldaEmail = False
      oCleCedi.bEldaScadEx = False
      '-----------------------------------------------------------------------------------------
      '--- Se non è stato selezionato niente esce come se si fosse annullato
      '-----------------------------------------------------------------------------------------
      If (oCleCedi.bEldaCG_1 = False) And (oCleCedi.bEldaCG_2 = False) And (oCleCedi.bEldaMG = False) And _
         (oCleCedi.bEldaMantieni = False) And (oCleCedi.bEldaPRB = False) And (oCleCedi.bEldaOR = False) And _
         (oCleCedi.bEldaBUD = False) And (oCleCedi.bEldaPR = False) And (oCleCedi.bEldaPM_1 = False) And _
         (oCleCedi.bEldaPM_2 = False) And (oCleCedi.bEldaCRM = False) And (oCleCedi.bEldaDWH = False) And _
         (oCleCedi.bEldaTT = False) And _
         (oCleCedi.bEldaMovc = False) And (oCleCedi.bEldaAnac = False) And (oCleCedi.bEldaEmail = False) And _
         (oCleCedi.bEldaScadEx = False) And (oCleCedi.bEldaStatistiche = False) Then
        oCleCedi.bEldaAnnullato = True
      Else
        oCleCedi.bEldaAnnullato = False
      End If
      '-----------------------------------------------------------------------------------------
      oCleCedi.strEldaDesogglog = _
        "          . Movimenti contabili, Iva, scadenziario, progressivi, ecc...........: " & NTSCStr(IIf(ckCG_1.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Movimenti e progressivi di magazzino...............................: " & NTSCStr(IIf(ckMG.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Mantieni ultimi costi d'acquisto...................................: " & NTSCStr(IIf(ckMantieni.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Ordini, Impegni, preventivi........................................: " & NTSCStr(IIf(ckOR.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Provvigioni e progressivi agenti...................................: " & NTSCStr(IIf(ckPR.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Datawarehouse......................................................: " & NTSCStr(IIf(ckDWH.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Statistiche (vecchio modulo).......................................: " & NTSCStr(IIf(ckStatven.Checked = True, "'Sì'", "'No'")) & vbCrLf & _
        "          . Tabelle Temporanee.................................................: " & NTSCStr(IIf(ckTT.Checked = True, "'Sì'", "'No'")) & vbCrLf
      '-----------------------------------------------------------------------------------------
      Me.Close()
      '-----------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      oCleCedi.bEldaAnnullato = True
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

End Class