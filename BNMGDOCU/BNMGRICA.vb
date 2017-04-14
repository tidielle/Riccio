Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGRICA
  Public oCallParams As CLE__CLDP
  Public oCleDocu As CLEMGDOCU
  Public bOk As Boolean = False
  Public strParent As String = ""

  Private components As System.ComponentModel.IContainer
  Public WithEvents pnAll As NTSInformatica.NTSPanel

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

      oCallParams = Param

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub InitializeComponent()
    Me.pnAll = New NTSInformatica.NTSPanel
    Me.fmCorpo = New NTSInformatica.NTSGroupBox
    Me.ckPrezziListino = New NTSInformatica.NTSCheckBox
    Me.opRigheSelezionate = New NTSInformatica.NTSRadioButton
    Me.opRigaCorrente = New NTSInformatica.NTSRadioButton
    Me.opTutteRighe = New NTSInformatica.NTSRadioButton
    Me.ckProvvigioni = New NTSInformatica.NTSCheckBox
    Me.ckSconti = New NTSInformatica.NTSCheckBox
    Me.ckPrezzi = New NTSInformatica.NTSCheckBox
    Me.fmTestata = New NTSInformatica.NTSGroupBox
    Me.ckRileggiDaAnangra = New NTSInformatica.NTSCheckBox
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.ckAncheRigheEvase = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAll, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAll.SuspendLayout()
    CType(Me.fmCorpo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCorpo.SuspendLayout()
    CType(Me.ckPrezziListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opRigheSelezionate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opRigaCorrente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opTutteRighe.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckProvvigioni.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSconti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckPrezzi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTestata, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTestata.SuspendLayout()
    CType(Me.ckRileggiDaAnangra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAncheRigheEvase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnAll
    '
    Me.pnAll.AllowDrop = True
    Me.pnAll.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAll.Appearance.Options.UseBackColor = True
    Me.pnAll.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAll.Controls.Add(Me.fmCorpo)
    Me.pnAll.Controls.Add(Me.fmTestata)
    Me.pnAll.Controls.Add(Me.cmdAnnulla)
    Me.pnAll.Controls.Add(Me.cmdConferma)
    Me.pnAll.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAll.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAll.Location = New System.Drawing.Point(0, 0)
    Me.pnAll.Name = "pnAll"
    Me.pnAll.NTSActiveTrasparency = True
    Me.pnAll.Size = New System.Drawing.Size(354, 231)
    Me.pnAll.TabIndex = 13
    Me.pnAll.Text = "NtsPanel1"
    '
    'fmCorpo
    '
    Me.fmCorpo.AllowDrop = True
    Me.fmCorpo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCorpo.Appearance.Options.UseBackColor = True
    Me.fmCorpo.Controls.Add(Me.ckAncheRigheEvase)
    Me.fmCorpo.Controls.Add(Me.ckPrezziListino)
    Me.fmCorpo.Controls.Add(Me.opRigheSelezionate)
    Me.fmCorpo.Controls.Add(Me.opRigaCorrente)
    Me.fmCorpo.Controls.Add(Me.opTutteRighe)
    Me.fmCorpo.Controls.Add(Me.ckProvvigioni)
    Me.fmCorpo.Controls.Add(Me.ckSconti)
    Me.fmCorpo.Controls.Add(Me.ckPrezzi)
    Me.fmCorpo.Location = New System.Drawing.Point(4, 54)
    Me.fmCorpo.Name = "fmCorpo"
    Me.fmCorpo.Size = New System.Drawing.Size(346, 145)
    Me.fmCorpo.TabIndex = 1
    Me.fmCorpo.Text = "Ricalcola valori di riga"
    '
    'ckPrezziListino
    '
    Me.ckPrezziListino.EditValue = True
    Me.ckPrezziListino.Location = New System.Drawing.Point(258, 23)
    Me.ckPrezziListino.Name = "ckPrezziListino"
    Me.ckPrezziListino.NTSCheckValue = "S"
    Me.ckPrezziListino.NTSUnCheckValue = "N"
    Me.ckPrezziListino.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPrezziListino.Properties.Appearance.Options.UseBackColor = True
    Me.ckPrezziListino.Properties.AutoHeight = False
    Me.ckPrezziListino.Properties.Caption = "Prezzi Listino"
    Me.ckPrezziListino.Size = New System.Drawing.Size(85, 19)
    Me.ckPrezziListino.TabIndex = 2
    '
    'opRigheSelezionate
    '
    Me.opRigheSelezionate.Location = New System.Drawing.Point(6, 95)
    Me.opRigheSelezionate.Name = "opRigheSelezionate"
    Me.opRigheSelezionate.NTSCheckValue = "S"
    Me.opRigheSelezionate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opRigheSelezionate.Properties.Appearance.Options.UseBackColor = True
    Me.opRigheSelezionate.Properties.AutoHeight = False
    Me.opRigheSelezionate.Properties.Caption = "Solo le righe selezionate"
    Me.opRigheSelezionate.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opRigheSelezionate.Size = New System.Drawing.Size(331, 19)
    Me.opRigheSelezionate.TabIndex = 1
    '
    'opRigaCorrente
    '
    Me.opRigaCorrente.Location = New System.Drawing.Point(6, 71)
    Me.opRigaCorrente.Name = "opRigaCorrente"
    Me.opRigaCorrente.NTSCheckValue = "S"
    Me.opRigaCorrente.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opRigaCorrente.Properties.Appearance.Options.UseBackColor = True
    Me.opRigaCorrente.Properties.AutoHeight = False
    Me.opRigaCorrente.Properties.Caption = "Solo la riga corrente"
    Me.opRigaCorrente.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opRigaCorrente.Size = New System.Drawing.Size(331, 19)
    Me.opRigaCorrente.TabIndex = 1
    '
    'opTutteRighe
    '
    Me.opTutteRighe.EditValue = True
    Me.opTutteRighe.Location = New System.Drawing.Point(6, 47)
    Me.opTutteRighe.Name = "opTutteRighe"
    Me.opTutteRighe.NTSCheckValue = "S"
    Me.opTutteRighe.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opTutteRighe.Properties.Appearance.Options.UseBackColor = True
    Me.opTutteRighe.Properties.AutoHeight = False
    Me.opTutteRighe.Properties.Caption = "Tutte le righe"
    Me.opTutteRighe.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opTutteRighe.Size = New System.Drawing.Size(331, 19)
    Me.opTutteRighe.TabIndex = 1
    '
    'ckProvvigioni
    '
    Me.ckProvvigioni.EditValue = True
    Me.ckProvvigioni.Location = New System.Drawing.Point(258, 23)
    Me.ckProvvigioni.Name = "ckProvvigioni"
    Me.ckProvvigioni.NTSCheckValue = "S"
    Me.ckProvvigioni.NTSUnCheckValue = "N"
    Me.ckProvvigioni.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckProvvigioni.Properties.Appearance.Options.UseBackColor = True
    Me.ckProvvigioni.Properties.AutoHeight = False
    Me.ckProvvigioni.Properties.Caption = "Provvigioni"
    Me.ckProvvigioni.Size = New System.Drawing.Size(81, 19)
    Me.ckProvvigioni.TabIndex = 0
    '
    'ckSconti
    '
    Me.ckSconti.EditValue = True
    Me.ckSconti.Location = New System.Drawing.Point(132, 23)
    Me.ckSconti.Name = "ckSconti"
    Me.ckSconti.NTSCheckValue = "S"
    Me.ckSconti.NTSUnCheckValue = "N"
    Me.ckSconti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSconti.Properties.Appearance.Options.UseBackColor = True
    Me.ckSconti.Properties.AutoHeight = False
    Me.ckSconti.Properties.Caption = "Sconti"
    Me.ckSconti.Size = New System.Drawing.Size(81, 19)
    Me.ckSconti.TabIndex = 0
    '
    'ckPrezzi
    '
    Me.ckPrezzi.EditValue = True
    Me.ckPrezzi.Location = New System.Drawing.Point(6, 23)
    Me.ckPrezzi.Name = "ckPrezzi"
    Me.ckPrezzi.NTSCheckValue = "S"
    Me.ckPrezzi.NTSUnCheckValue = "N"
    Me.ckPrezzi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckPrezzi.Properties.Appearance.Options.UseBackColor = True
    Me.ckPrezzi.Properties.AutoHeight = False
    Me.ckPrezzi.Properties.Caption = "Prezzi"
    Me.ckPrezzi.Size = New System.Drawing.Size(81, 19)
    Me.ckPrezzi.TabIndex = 0
    '
    'fmTestata
    '
    Me.fmTestata.AllowDrop = True
    Me.fmTestata.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTestata.Appearance.Options.UseBackColor = True
    Me.fmTestata.Controls.Add(Me.ckRileggiDaAnangra)
    Me.fmTestata.Location = New System.Drawing.Point(4, 4)
    Me.fmTestata.Name = "fmTestata"
    Me.fmTestata.Size = New System.Drawing.Size(346, 46)
    Me.fmTestata.TabIndex = 1
    Me.fmTestata.Text = "Dati di testata"
    '
    'ckRileggiDaAnangra
    '
    Me.ckRileggiDaAnangra.EditValue = True
    Me.ckRileggiDaAnangra.Location = New System.Drawing.Point(6, 23)
    Me.ckRileggiDaAnangra.Name = "ckRileggiDaAnangra"
    Me.ckRileggiDaAnangra.NTSCheckValue = "S"
    Me.ckRileggiDaAnangra.NTSUnCheckValue = "N"
    Me.ckRileggiDaAnangra.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckRileggiDaAnangra.Properties.Appearance.Options.UseBackColor = True
    Me.ckRileggiDaAnangra.Properties.AutoHeight = False
    Me.ckRileggiDaAnangra.Properties.Caption = "Rileggi agente, codice pagamento e banca da anagrafica cliente"
    Me.ckRileggiDaAnangra.Size = New System.Drawing.Size(333, 19)
    Me.ckRileggiDaAnangra.TabIndex = 0
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(4, 202)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(75, 26)
    Me.cmdAnnulla.TabIndex = 0
    Me.cmdAnnulla.Text = "Annulla"
    '
    'cmdConferma
    '
    Me.cmdConferma.ImagePath = ""
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(275, 202)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(75, 26)
    Me.cmdConferma.TabIndex = 0
    Me.cmdConferma.Text = "Conferma"
    '
    'ckAncheRigheEvase
    '
    Me.ckAncheRigheEvase.EditValue = True
    Me.ckAncheRigheEvase.Location = New System.Drawing.Point(6, 120)
    Me.ckAncheRigheEvase.Name = "ckAncheRigheEvase"
    Me.ckAncheRigheEvase.NTSCheckValue = "S"
    Me.ckAncheRigheEvase.NTSUnCheckValue = "N"
    Me.ckAncheRigheEvase.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAncheRigheEvase.Properties.Appearance.Options.UseBackColor = True
    Me.ckAncheRigheEvase.Properties.AutoHeight = False
    Me.ckAncheRigheEvase.Properties.Caption = "Ricalcola anche su righe che evadono ordini o note di prelievo"
    Me.ckAncheRigheEvase.Size = New System.Drawing.Size(337, 19)
    Me.ckAncheRigheEvase.TabIndex = 3
    '
    'FRMMGRICA
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(354, 231)
    Me.Controls.Add(Me.pnAll)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRMMGRICA"
    Me.Text = "RICALCOLO CONDIZIONI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAll, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAll.ResumeLayout(False)
    CType(Me.fmCorpo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCorpo.ResumeLayout(False)
    Me.fmCorpo.PerformLayout()
    CType(Me.ckPrezziListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opRigheSelezionate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opRigaCorrente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opTutteRighe.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckProvvigioni.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSconti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckPrezzi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTestata, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTestata.ResumeLayout(False)
    Me.fmTestata.PerformLayout()
    CType(Me.ckRileggiDaAnangra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAncheRigheEvase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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

#Region "Eventi Form"
  Public Overridable Sub FRMMGRICA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      If oCallParams Is Nothing OrElse oCallParams.strNomProg.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130409054787796165, "Programma aperto senza il pessaggio dei valori necessari all'apertura"))
        Me.Close()
      End If

      strParent = oCallParams.strNomProg

      ckRileggiDaAnangra.Checked = (oMenu.GetSettingBus(strParent, "RECENT", ".", "RicalcRileggiDaAnangra", "S", ".", "S") = "S")
      ckPrezzi.Checked = (oMenu.GetSettingBus(strParent, "RECENT", ".", "RicalcPrezzi", "S", ".", "S") = "S")
      ckSconti.Checked = (oMenu.GetSettingBus(strParent, "RECENT", ".", "RicalcSconti", "S", ".", "S") = "S")
      ckProvvigioni.Checked = (oMenu.GetSettingBus(strParent, "RECENT", ".", "RicalcProvvigioni", "S", ".", "S") = "S")
      ckPrezziListino.Checked = (oMenu.GetSettingBus(strParent, "RECENT", ".", "RicalcPrezziListino", "S", ".", "S") = "S")
      ckAncheRigheEvase.Checked = (oMenu.GetSettingBus(strParent, "RECENT", ".", "AncheRigheEvase", "S", ".", "S") = "S")
      Select Case oMenu.GetSettingBus(strParent, "RECENT", ".", "RicalcRigheElab", "T", ".", "T")
        Case "C" : opRigaCorrente.Checked = True
        Case "S" : opRigheSelezionate.Checked = True
        Case Else : opTutteRighe.Checked = True
      End Select
      'In gestione offerte non ci sono le provvigioni.
      If strParent = "BSCRGSOF" Then
        ckProvvigioni.Visible = False
        GctlSetVisEnab(ckPrezziListino, True)
      Else
        GctlSetVisEnab(ckProvvigioni, True)
        ckPrezziListino.Visible = False
      End If
      '--------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi"
  Public Overridable Sub ckProvvigioni_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckProvvigioni.CheckedChanged
    Try
      AbilitaDisabilitaRigheDaTrattare()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckSconti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSconti.CheckedChanged
    Try
      AbilitaDisabilitaRigheDaTrattare()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckPrezzi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckPrezzi.CheckedChanged
    Try
      AbilitaDisabilitaRigheDaTrattare()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Dim strRileggi As String
    Try
      oMenu.SaveSettingBus(strParent, "RECENT", ".", "RicalcRileggiDaAnangra", NTSCStr(IIf(ckRileggiDaAnangra.Checked, "S", "N")), ".", "NS.", "...", "...")
      oMenu.SaveSettingBus(strParent, "RECENT", ".", "RicalcPrezzi", NTSCStr(IIf(ckPrezzi.Checked, "S", "N")), ".", "NS.", "...", "...")
      oMenu.SaveSettingBus(strParent, "RECENT", ".", "RicalcSconti", NTSCStr(IIf(ckSconti.Checked, "S", "N")), ".", "NS.", "...", "...")
      oMenu.SaveSettingBus(strParent, "RECENT", ".", "RicalcProvvigioni", NTSCStr(IIf(ckProvvigioni.Checked, "S", "N")), ".", "NS.", "...", "...")
      oMenu.SaveSettingBus(strParent, "RECENT", ".", "RicalcPrezziListino", NTSCStr(IIf(ckPrezziListino.Checked, "S", "N")), ".", "NS.", "...", "...")
      oMenu.SaveSettingBus(strParent, "RECENT", ".", "AncheRigheEvase", NTSCStr(IIf(ckAncheRigheEvase.Checked, "S", "N")), ".", "NS.", "...", "...")

      Select Case True
        Case opRigaCorrente.Checked : strRileggi = "C"
        Case opRigheSelezionate.Checked : strRileggi = "S"
        Case Else : strRileggi = "T"
      End Select
      oMenu.SaveSettingBus(strParent, "RECENT", ".", "RicalcRigheElab", strRileggi, ".", "NS.", "...", "...")

      oCallParams.bPar1 = True
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Sub AbilitaDisabilitaRigheDaTrattare()
    Try
      If ckPrezzi.Checked OrElse ckProvvigioni.Checked OrElse ckSconti.Checked Then
        GctlSetVisEnab(opRigaCorrente, False)
        GctlSetVisEnab(opRigheSelezionate, False)
        GctlSetVisEnab(opTutteRighe, False)
      Else
        opRigaCorrente.Enabled = False
        opRigheSelezionate.Enabled = False
        opTutteRighe.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
End Class
