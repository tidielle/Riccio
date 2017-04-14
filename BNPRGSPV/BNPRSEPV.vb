#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class FRMPRSEPV

#Region "Moduli"
  Private Moduli_P As Integer = bsModPR
  Private ModuliExt_P As Integer = 0
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property
#End Region

#Region "Variabili"
  'parametri passati dal child che mi ha chiamato: sempre tramite questa classe
  'gli restituisceo il valore
  Public oCallParam As CLE__CLDP
  'oCallParam.strPar1 - restituisco lo stato T = tutte, M = solo maturato, P = solo pagato, N = no maturato no pagato
  'oCallParam.strPar2 - restituisco data documento da
  'oCallParam.strPar3 - restituisco data documento a
  'oCallParam.strPar4 - restituisco scadenza documento da
  'oCallParam.strPar5 - restituisco scadenza documento a
  'oCallParam.dPar1 - restituisco il codice agente
  'oCallParam.strParam - descrizione dell'agente
  'oCallParam.dPar2 - restituisco il codice conto
  'oCallParam.dPar3 - restituisco il codice pagamento
  'oCallParam.dPar4 - restituisco il tipo pagamento
  'oCallParam.bPar1 - se confermato vale True, altrimenti False

  Public oCleSepv As CLEPRGSPV

  Private components As System.ComponentModel.IContainer
  Public WithEvents lbCodage As NTSInformatica.NTSLabel
  Public WithEvents edCodage As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDesage As NTSInformatica.NTSLabel
  Public WithEvents pnNupv As NTSInformatica.NTSPanel
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents pnButton As NTSInformatica.NTSPanel
  Public WithEvents optTutte As NTSInformatica.NTSRadioButton
  Public WithEvents cbTipo As NTSInformatica.NTSComboBox
  Public WithEvents fmDataDocumenti As NTSInformatica.NTSGroupBox
  Public WithEvents edDataDocumentiDa As NTSInformatica.NTSTextBoxData
  Public WithEvents lbCodconto As NTSInformatica.NTSLabel
  Public WithEvents edCodconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbDesconto As NTSInformatica.NTSLabel
  Public WithEvents edDataDocumentiA As NTSInformatica.NTSTextBoxData
  Public WithEvents lbDataDocumentiA As NTSInformatica.NTSLabel
  Public WithEvents lbDataDocumentiDa As NTSInformatica.NTSLabel
  Public WithEvents fmScadenzaDocumenti As NTSInformatica.NTSGroupBox
  Public WithEvents edScadenzaDocumentiA As NTSInformatica.NTSTextBoxData
  Public WithEvents edScadenzaDocumentiDa As NTSInformatica.NTSTextBoxData
  Public WithEvents lbScadenzaDocumentiA As NTSInformatica.NTSLabel
  Public WithEvents lbScadenzaDocumentiDa As NTSInformatica.NTSLabel
  Public WithEvents fmPagamento As NTSInformatica.NTSGroupBox
  Public WithEvents lbTipo As NTSInformatica.NTSLabel
  Public WithEvents fmStato As NTSInformatica.NTSGroupBox
  Public WithEvents lbDespag As NTSInformatica.NTSLabel
  Public WithEvents edCodpag As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbCodpag As NTSInformatica.NTSLabel
  Public WithEvents optNoMaturatoNoPagato As NTSInformatica.NTSRadioButton
  Public WithEvents optSoloPagato As NTSInformatica.NTSRadioButton
  Public WithEvents optSoloMaturato As NTSInformatica.NTSRadioButton
  Public WithEvents pnDate As NTSInformatica.NTSPanel
#End Region

  Public Overridable Sub InitializeComponent()
    Me.lbCodage = New NTSInformatica.NTSLabel
    Me.edCodage = New NTSInformatica.NTSTextBoxNum
    Me.lbDesage = New NTSInformatica.NTSLabel
    Me.pnNupv = New NTSInformatica.NTSPanel
    Me.pnDate = New NTSInformatica.NTSPanel
    Me.fmDataDocumenti = New NTSInformatica.NTSGroupBox
    Me.edDataDocumentiA = New NTSInformatica.NTSTextBoxData
    Me.edDataDocumentiDa = New NTSInformatica.NTSTextBoxData
    Me.lbDataDocumentiA = New NTSInformatica.NTSLabel
    Me.lbDataDocumentiDa = New NTSInformatica.NTSLabel
    Me.fmScadenzaDocumenti = New NTSInformatica.NTSGroupBox
    Me.edScadenzaDocumentiA = New NTSInformatica.NTSTextBoxData
    Me.edScadenzaDocumentiDa = New NTSInformatica.NTSTextBoxData
    Me.lbScadenzaDocumentiA = New NTSInformatica.NTSLabel
    Me.lbScadenzaDocumentiDa = New NTSInformatica.NTSLabel
    Me.fmStato = New NTSInformatica.NTSGroupBox
    Me.optNoMaturatoNoPagato = New NTSInformatica.NTSRadioButton
    Me.optSoloPagato = New NTSInformatica.NTSRadioButton
    Me.optSoloMaturato = New NTSInformatica.NTSRadioButton
    Me.optTutte = New NTSInformatica.NTSRadioButton
    Me.fmPagamento = New NTSInformatica.NTSGroupBox
    Me.cbTipo = New NTSInformatica.NTSComboBox
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.lbDespag = New NTSInformatica.NTSLabel
    Me.edCodpag = New NTSInformatica.NTSTextBoxNum
    Me.lbCodpag = New NTSInformatica.NTSLabel
    Me.lbCodconto = New NTSInformatica.NTSLabel
    Me.edCodconto = New NTSInformatica.NTSTextBoxNum
    Me.lbDesconto = New NTSInformatica.NTSLabel
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnButton = New NTSInformatica.NTSPanel
    CType(Me.edCodage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnNupv, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnNupv.SuspendLayout()
    CType(Me.pnDate, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDate.SuspendLayout()
    CType(Me.fmDataDocumenti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmDataDocumenti.SuspendLayout()
    CType(Me.edDataDocumentiA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataDocumentiDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmScadenzaDocumenti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmScadenzaDocumenti.SuspendLayout()
    CType(Me.edScadenzaDocumentiA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edScadenzaDocumentiDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmStato, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmStato.SuspendLayout()
    CType(Me.optNoMaturatoNoPagato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optSoloPagato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optSoloMaturato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optTutte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmPagamento, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPagamento.SuspendLayout()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodpag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnButton.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'DevXDefaultLookAndFeel
    '
    
    '
    'lbCodage
    '
    Me.lbCodage.AutoSize = True
    Me.lbCodage.BackColor = System.Drawing.Color.Transparent
    Me.lbCodage.Location = New System.Drawing.Point(12, 20)
    Me.lbCodage.Name = "lbCodage"
    Me.lbCodage.NTSDbField = ""
    Me.lbCodage.Size = New System.Drawing.Size(77, 13)
    Me.lbCodage.TabIndex = 0
    Me.lbCodage.Text = "Codice Agente"
    '
    'edCodage
    '
    Me.edCodage.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodage.EditValue = "0"
    Me.edCodage.Location = New System.Drawing.Point(103, 17)
    Me.edCodage.Name = "edCodage"
    Me.edCodage.NTSDbField = ""
    Me.edCodage.NTSFormat = "0"
    Me.edCodage.NTSForzaVisZoom = False
    Me.edCodage.NTSOldValue = ""
    Me.edCodage.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodage.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodage.Properties.MaxLength = 65536
    Me.edCodage.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodage.Size = New System.Drawing.Size(81, 20)
    Me.edCodage.TabIndex = 1
    '
    'lbDesage
    '
    Me.lbDesage.BackColor = System.Drawing.Color.Transparent
    Me.lbDesage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesage.Location = New System.Drawing.Point(190, 17)
    Me.lbDesage.Name = "lbDesage"
    Me.lbDesage.NTSDbField = ""
    Me.lbDesage.Size = New System.Drawing.Size(322, 20)
    Me.lbDesage.TabIndex = 2
    '
    'pnNupv
    '
    Me.pnNupv.AllowDrop = True
    Me.pnNupv.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnNupv.Appearance.Options.UseBackColor = True
    Me.pnNupv.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnNupv.Controls.Add(Me.pnDate)
    Me.pnNupv.Controls.Add(Me.fmStato)
    Me.pnNupv.Controls.Add(Me.fmPagamento)
    Me.pnNupv.Controls.Add(Me.lbCodconto)
    Me.pnNupv.Controls.Add(Me.edCodconto)
    Me.pnNupv.Controls.Add(Me.lbDesconto)
    Me.pnNupv.Controls.Add(Me.lbCodage)
    Me.pnNupv.Controls.Add(Me.edCodage)
    Me.pnNupv.Controls.Add(Me.lbDesage)
    Me.pnNupv.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnNupv.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnNupv.Location = New System.Drawing.Point(0, 0)
    Me.pnNupv.Name = "pnNupv"
    Me.pnNupv.Size = New System.Drawing.Size(518, 298)
    Me.pnNupv.TabIndex = 0
    Me.pnNupv.Text = "NtsPanel1"
    '
    'pnDate
    '
    Me.pnDate.AllowDrop = True
    Me.pnDate.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDate.Appearance.Options.UseBackColor = True
    Me.pnDate.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDate.Controls.Add(Me.fmDataDocumenti)
    Me.pnDate.Controls.Add(Me.fmScadenzaDocumenti)
    Me.pnDate.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnDate.Location = New System.Drawing.Point(10, 69)
    Me.pnDate.Name = "pnDate"
    Me.pnDate.Size = New System.Drawing.Size(316, 137)
    Me.pnDate.TabIndex = 8
    Me.pnDate.Text = "NtsPanel1"
    '
    'fmDataDocumenti
    '
    Me.fmDataDocumenti.AllowDrop = True
    Me.fmDataDocumenti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmDataDocumenti.Appearance.Options.UseBackColor = True
    Me.fmDataDocumenti.Controls.Add(Me.edDataDocumentiA)
    Me.fmDataDocumenti.Controls.Add(Me.edDataDocumentiDa)
    Me.fmDataDocumenti.Controls.Add(Me.lbDataDocumentiA)
    Me.fmDataDocumenti.Controls.Add(Me.lbDataDocumentiDa)
    Me.fmDataDocumenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmDataDocumenti.Location = New System.Drawing.Point(5, 3)
    Me.fmDataDocumenti.Name = "fmDataDocumenti"
    Me.fmDataDocumenti.Size = New System.Drawing.Size(306, 62)
    Me.fmDataDocumenti.TabIndex = 7
    Me.fmDataDocumenti.Text = "Data Documenti"
    '
    'edDataDocumentiA
    '
    Me.edDataDocumentiA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataDocumentiA.Location = New System.Drawing.Point(188, 28)
    Me.edDataDocumentiA.Name = "edDataDocumentiA"
    Me.edDataDocumentiA.NTSDbField = ""
    Me.edDataDocumentiA.NTSForzaVisZoom = False
    Me.edDataDocumentiA.NTSOldValue = ""
    Me.edDataDocumentiA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataDocumentiA.Properties.MaxLength = 65536
    Me.edDataDocumentiA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataDocumentiA.Size = New System.Drawing.Size(100, 20)
    Me.edDataDocumentiA.TabIndex = 6
    '
    'edDataDocumentiDa
    '
    Me.edDataDocumentiDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataDocumentiDa.Location = New System.Drawing.Point(44, 28)
    Me.edDataDocumentiDa.Name = "edDataDocumentiDa"
    Me.edDataDocumentiDa.NTSDbField = ""
    Me.edDataDocumentiDa.NTSForzaVisZoom = False
    Me.edDataDocumentiDa.NTSOldValue = ""
    Me.edDataDocumentiDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataDocumentiDa.Properties.MaxLength = 65536
    Me.edDataDocumentiDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataDocumentiDa.Size = New System.Drawing.Size(100, 20)
    Me.edDataDocumentiDa.TabIndex = 6
    '
    'lbDataDocumentiA
    '
    Me.lbDataDocumentiA.AutoSize = True
    Me.lbDataDocumentiA.BackColor = System.Drawing.Color.Transparent
    Me.lbDataDocumentiA.Location = New System.Drawing.Point(165, 31)
    Me.lbDataDocumentiA.Name = "lbDataDocumentiA"
    Me.lbDataDocumentiA.NTSDbField = ""
    Me.lbDataDocumentiA.Size = New System.Drawing.Size(17, 13)
    Me.lbDataDocumentiA.TabIndex = 0
    Me.lbDataDocumentiA.Text = "a:"
    '
    'lbDataDocumentiDa
    '
    Me.lbDataDocumentiDa.AutoSize = True
    Me.lbDataDocumentiDa.BackColor = System.Drawing.Color.Transparent
    Me.lbDataDocumentiDa.Location = New System.Drawing.Point(15, 31)
    Me.lbDataDocumentiDa.Name = "lbDataDocumentiDa"
    Me.lbDataDocumentiDa.NTSDbField = ""
    Me.lbDataDocumentiDa.Size = New System.Drawing.Size(23, 13)
    Me.lbDataDocumentiDa.TabIndex = 0
    Me.lbDataDocumentiDa.Text = "da:"
    '
    'fmScadenzaDocumenti
    '
    Me.fmScadenzaDocumenti.AllowDrop = True
    Me.fmScadenzaDocumenti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmScadenzaDocumenti.Appearance.Options.UseBackColor = True
    Me.fmScadenzaDocumenti.Controls.Add(Me.edScadenzaDocumentiA)
    Me.fmScadenzaDocumenti.Controls.Add(Me.edScadenzaDocumentiDa)
    Me.fmScadenzaDocumenti.Controls.Add(Me.lbScadenzaDocumentiA)
    Me.fmScadenzaDocumenti.Controls.Add(Me.lbScadenzaDocumentiDa)
    Me.fmScadenzaDocumenti.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmScadenzaDocumenti.Location = New System.Drawing.Point(5, 71)
    Me.fmScadenzaDocumenti.Name = "fmScadenzaDocumenti"
    Me.fmScadenzaDocumenti.Size = New System.Drawing.Size(306, 62)
    Me.fmScadenzaDocumenti.TabIndex = 7
    Me.fmScadenzaDocumenti.Text = "Scadenza Documenti"
    '
    'edScadenzaDocumentiA
    '
    Me.edScadenzaDocumentiA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edScadenzaDocumentiA.Location = New System.Drawing.Point(188, 28)
    Me.edScadenzaDocumentiA.Name = "edScadenzaDocumentiA"
    Me.edScadenzaDocumentiA.NTSDbField = ""
    Me.edScadenzaDocumentiA.NTSForzaVisZoom = False
    Me.edScadenzaDocumentiA.NTSOldValue = ""
    Me.edScadenzaDocumentiA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edScadenzaDocumentiA.Properties.MaxLength = 65536
    Me.edScadenzaDocumentiA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edScadenzaDocumentiA.Size = New System.Drawing.Size(100, 20)
    Me.edScadenzaDocumentiA.TabIndex = 6
    '
    'edScadenzaDocumentiDa
    '
    Me.edScadenzaDocumentiDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edScadenzaDocumentiDa.Location = New System.Drawing.Point(44, 28)
    Me.edScadenzaDocumentiDa.Name = "edScadenzaDocumentiDa"
    Me.edScadenzaDocumentiDa.NTSDbField = ""
    Me.edScadenzaDocumentiDa.NTSForzaVisZoom = False
    Me.edScadenzaDocumentiDa.NTSOldValue = ""
    Me.edScadenzaDocumentiDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edScadenzaDocumentiDa.Properties.MaxLength = 65536
    Me.edScadenzaDocumentiDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edScadenzaDocumentiDa.Size = New System.Drawing.Size(100, 20)
    Me.edScadenzaDocumentiDa.TabIndex = 6
    '
    'lbScadenzaDocumentiA
    '
    Me.lbScadenzaDocumentiA.AutoSize = True
    Me.lbScadenzaDocumentiA.BackColor = System.Drawing.Color.Transparent
    Me.lbScadenzaDocumentiA.Location = New System.Drawing.Point(165, 31)
    Me.lbScadenzaDocumentiA.Name = "lbScadenzaDocumentiA"
    Me.lbScadenzaDocumentiA.NTSDbField = ""
    Me.lbScadenzaDocumentiA.Size = New System.Drawing.Size(17, 13)
    Me.lbScadenzaDocumentiA.TabIndex = 0
    Me.lbScadenzaDocumentiA.Text = "a:"
    '
    'lbScadenzaDocumentiDa
    '
    Me.lbScadenzaDocumentiDa.AutoSize = True
    Me.lbScadenzaDocumentiDa.BackColor = System.Drawing.Color.Transparent
    Me.lbScadenzaDocumentiDa.Location = New System.Drawing.Point(15, 31)
    Me.lbScadenzaDocumentiDa.Name = "lbScadenzaDocumentiDa"
    Me.lbScadenzaDocumentiDa.NTSDbField = ""
    Me.lbScadenzaDocumentiDa.Size = New System.Drawing.Size(23, 13)
    Me.lbScadenzaDocumentiDa.TabIndex = 0
    Me.lbScadenzaDocumentiDa.Text = "da:"
    '
    'fmStato
    '
    Me.fmStato.AllowDrop = True
    Me.fmStato.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmStato.Appearance.Options.UseBackColor = True
    Me.fmStato.Controls.Add(Me.optNoMaturatoNoPagato)
    Me.fmStato.Controls.Add(Me.optSoloPagato)
    Me.fmStato.Controls.Add(Me.optSoloMaturato)
    Me.fmStato.Controls.Add(Me.optTutte)
    Me.fmStato.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmStato.Location = New System.Drawing.Point(328, 72)
    Me.fmStato.Name = "fmStato"
    Me.fmStato.Size = New System.Drawing.Size(185, 130)
    Me.fmStato.TabIndex = 7
    Me.fmStato.Text = "Stato"
    '
    'optNoMaturatoNoPagato
    '
    Me.optNoMaturatoNoPagato.Cursor = System.Windows.Forms.Cursors.Default
    Me.optNoMaturatoNoPagato.Location = New System.Drawing.Point(17, 101)
    Me.optNoMaturatoNoPagato.Name = "optNoMaturatoNoPagato"
    Me.optNoMaturatoNoPagato.NTSCheckValue = "S"
    Me.optNoMaturatoNoPagato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optNoMaturatoNoPagato.Properties.Appearance.Options.UseBackColor = True
    Me.optNoMaturatoNoPagato.Properties.Caption = "No maturato no pagato"
    Me.optNoMaturatoNoPagato.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optNoMaturatoNoPagato.Size = New System.Drawing.Size(148, 18)
    Me.optNoMaturatoNoPagato.TabIndex = 9
    '
    'optSoloPagato
    '
    Me.optSoloPagato.Cursor = System.Windows.Forms.Cursors.Default
    Me.optSoloPagato.Location = New System.Drawing.Point(17, 77)
    Me.optSoloPagato.Name = "optSoloPagato"
    Me.optSoloPagato.NTSCheckValue = "S"
    Me.optSoloPagato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optSoloPagato.Properties.Appearance.Options.UseBackColor = True
    Me.optSoloPagato.Properties.Caption = "Solo pagato"
    Me.optSoloPagato.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optSoloPagato.Size = New System.Drawing.Size(148, 18)
    Me.optSoloPagato.TabIndex = 9
    '
    'optSoloMaturato
    '
    Me.optSoloMaturato.Cursor = System.Windows.Forms.Cursors.Default
    Me.optSoloMaturato.Location = New System.Drawing.Point(17, 53)
    Me.optSoloMaturato.Name = "optSoloMaturato"
    Me.optSoloMaturato.NTSCheckValue = "S"
    Me.optSoloMaturato.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optSoloMaturato.Properties.Appearance.Options.UseBackColor = True
    Me.optSoloMaturato.Properties.Caption = "Solo maturato"
    Me.optSoloMaturato.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optSoloMaturato.Size = New System.Drawing.Size(148, 18)
    Me.optSoloMaturato.TabIndex = 9
    '
    'optTutte
    '
    Me.optTutte.Cursor = System.Windows.Forms.Cursors.Default
    Me.optTutte.EditValue = True
    Me.optTutte.Location = New System.Drawing.Point(17, 29)
    Me.optTutte.Name = "optTutte"
    Me.optTutte.NTSCheckValue = "S"
    Me.optTutte.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optTutte.Properties.Appearance.Options.UseBackColor = True
    Me.optTutte.Properties.Caption = "Tutte"
    Me.optTutte.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optTutte.Size = New System.Drawing.Size(148, 18)
    Me.optTutte.TabIndex = 9
    '
    'fmPagamento
    '
    Me.fmPagamento.AllowDrop = True
    Me.fmPagamento.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPagamento.Appearance.Options.UseBackColor = True
    Me.fmPagamento.Controls.Add(Me.cbTipo)
    Me.fmPagamento.Controls.Add(Me.lbTipo)
    Me.fmPagamento.Controls.Add(Me.lbDespag)
    Me.fmPagamento.Controls.Add(Me.edCodpag)
    Me.fmPagamento.Controls.Add(Me.lbCodpag)
    Me.fmPagamento.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPagamento.Location = New System.Drawing.Point(15, 208)
    Me.fmPagamento.Name = "fmPagamento"
    Me.fmPagamento.Size = New System.Drawing.Size(497, 79)
    Me.fmPagamento.TabIndex = 7
    Me.fmPagamento.Text = "Pagamento"
    '
    'cbTipo
    '
    Me.cbTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo.DataSource = Nothing
    Me.cbTipo.DisplayMember = ""
    Me.cbTipo.Location = New System.Drawing.Point(40, 49)
    Me.cbTipo.Name = "cbTipo"
    Me.cbTipo.NTSDbField = ""
    Me.cbTipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo.Properties.DropDownRows = 30
    Me.cbTipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo.SelectedValue = ""
    Me.cbTipo.Size = New System.Drawing.Size(142, 20)
    Me.cbTipo.TabIndex = 8
    Me.cbTipo.ValueMember = ""
    '
    'lbTipo
    '
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Location = New System.Drawing.Point(7, 52)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(27, 13)
    Me.lbTipo.TabIndex = 0
    Me.lbTipo.Text = "Tipo"
    '
    'lbDespag
    '
    Me.lbDespag.BackColor = System.Drawing.Color.Transparent
    Me.lbDespag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDespag.Location = New System.Drawing.Point(188, 23)
    Me.lbDespag.Name = "lbDespag"
    Me.lbDespag.NTSDbField = ""
    Me.lbDespag.Size = New System.Drawing.Size(304, 20)
    Me.lbDespag.TabIndex = 5
    '
    'edCodpag
    '
    Me.edCodpag.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodpag.EditValue = "0"
    Me.edCodpag.Location = New System.Drawing.Point(113, 23)
    Me.edCodpag.Name = "edCodpag"
    Me.edCodpag.NTSDbField = ""
    Me.edCodpag.NTSFormat = "0"
    Me.edCodpag.NTSForzaVisZoom = False
    Me.edCodpag.NTSOldValue = ""
    Me.edCodpag.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodpag.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodpag.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodpag.Properties.MaxLength = 65536
    Me.edCodpag.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodpag.Size = New System.Drawing.Size(69, 20)
    Me.edCodpag.TabIndex = 4
    '
    'lbCodpag
    '
    Me.lbCodpag.AutoSize = True
    Me.lbCodpag.BackColor = System.Drawing.Color.Transparent
    Me.lbCodpag.Location = New System.Drawing.Point(7, 26)
    Me.lbCodpag.Name = "lbCodpag"
    Me.lbCodpag.NTSDbField = ""
    Me.lbCodpag.Size = New System.Drawing.Size(100, 13)
    Me.lbCodpag.TabIndex = 3
    Me.lbCodpag.Text = "Cod. pag. (0=tutti)"
    '
    'lbCodconto
    '
    Me.lbCodconto.AutoSize = True
    Me.lbCodconto.BackColor = System.Drawing.Color.Transparent
    Me.lbCodconto.Location = New System.Drawing.Point(12, 49)
    Me.lbCodconto.Name = "lbCodconto"
    Me.lbCodconto.NTSDbField = ""
    Me.lbCodconto.Size = New System.Drawing.Size(85, 13)
    Me.lbCodconto.TabIndex = 3
    Me.lbCodconto.Text = "Cliente (0=tutti)"
    '
    'edCodconto
    '
    Me.edCodconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodconto.EditValue = "0"
    Me.edCodconto.Location = New System.Drawing.Point(103, 46)
    Me.edCodconto.Name = "edCodconto"
    Me.edCodconto.NTSDbField = ""
    Me.edCodconto.NTSFormat = "0"
    Me.edCodconto.NTSForzaVisZoom = False
    Me.edCodconto.NTSOldValue = ""
    Me.edCodconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodconto.Properties.MaxLength = 65536
    Me.edCodconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodconto.Size = New System.Drawing.Size(81, 20)
    Me.edCodconto.TabIndex = 4
    '
    'lbDesconto
    '
    Me.lbDesconto.BackColor = System.Drawing.Color.Transparent
    Me.lbDesconto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesconto.Location = New System.Drawing.Point(190, 46)
    Me.lbDesconto.Name = "lbDesconto"
    Me.lbDesconto.NTSDbField = ""
    Me.lbDesconto.Size = New System.Drawing.Size(322, 20)
    Me.lbDesconto.TabIndex = 5
    '
    'cmdConferma
    '
    Me.cmdConferma.Location = New System.Drawing.Point(9, 17)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(100, 23)
    Me.cmdConferma.TabIndex = 8
    Me.cmdConferma.Text = "&Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Location = New System.Drawing.Point(9, 43)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(100, 23)
    Me.cmdAnnulla.TabIndex = 9
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnButton
    '
    Me.pnButton.AllowDrop = True
    Me.pnButton.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnButton.Appearance.Options.UseBackColor = True
    Me.pnButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnButton.Controls.Add(Me.cmdConferma)
    Me.pnButton.Controls.Add(Me.cmdAnnulla)
    Me.pnButton.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnButton.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnButton.Location = New System.Drawing.Point(519, 0)
    Me.pnButton.Name = "pnButton"
    Me.pnButton.Size = New System.Drawing.Size(128, 298)
    Me.pnButton.TabIndex = 1
    Me.pnButton.Text = "NtsPanel1"
    '
    'FRMPRSEPV
    '
    Me.ClientSize = New System.Drawing.Size(647, 298)
    Me.Controls.Add(Me.pnButton)
    Me.Controls.Add(Me.pnNupv)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.Name = "FRMPRSEPV"
    Me.Text = "APRI PROVVIGIONI AGENTE"
    CType(Me.edCodage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnNupv, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnNupv.ResumeLayout(False)
    Me.pnNupv.PerformLayout()
    CType(Me.pnDate, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDate.ResumeLayout(False)
    CType(Me.fmDataDocumenti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmDataDocumenti.ResumeLayout(False)
    Me.fmDataDocumenti.PerformLayout()
    CType(Me.edDataDocumentiA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataDocumentiDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmScadenzaDocumenti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmScadenzaDocumenti.ResumeLayout(False)
    Me.fmScadenzaDocumenti.PerformLayout()
    CType(Me.edScadenzaDocumentiA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edScadenzaDocumentiDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmStato, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmStato.ResumeLayout(False)
    Me.fmStato.PerformLayout()
    CType(Me.optNoMaturatoNoPagato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optSoloPagato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optSoloMaturato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optTutte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmPagamento, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPagamento.ResumeLayout(False)
    Me.fmPagamento.PerformLayout()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodpag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnButton.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef CallParam As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    oMenu = Menu
    oApp = oMenu.App
    oCallParam = CallParam
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

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'completo le informazioni dei controlli
      edCodage.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128727944642343750, "Codice Agente"), tabcage)
      edCodconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128727944642343751, "Cliente"), tabanagrac)
      edDataDocumentiDa.NTSSetParam(oMenu, oApp.Tr(Me, 128727944642343752, "Data documento da"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edDataDocumentiA.NTSSetParam(oMenu, oApp.Tr(Me, 128727944642343753, "Data documento a"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edScadenzaDocumentiDa.NTSSetParam(oMenu, oApp.Tr(Me, 128727944642343754, "Scadenza documento da"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      edScadenzaDocumentiA.NTSSetParam(oMenu, oApp.Tr(Me, 128727944642343755, "Scadenza documento a"), False, "D", NTSCDate("01/01/1900"), NTSCDate("31/12/2099"))
      cbTipo.NTSSetParam(oApp.Tr(Me, 128230023478978827, "Tipo pagamento"))
      Dim dttTipPag As New DataTable()
      dttTipPag.Columns.Add("cod", GetType(Integer))
      dttTipPag.Columns.Add("val", GetType(String))
      dttTipPag.Rows.Add(New Object() {0, "Tutti"})
      dttTipPag.Rows.Add(New Object() {1, "Tratta"})
      dttTipPag.Rows.Add(New Object() {2, "R.B. o RIBA"})
      dttTipPag.Rows.Add(New Object() {3, "Rim.Diretta"})
      dttTipPag.Rows.Add(New Object() {4, "Contanti"})
      dttTipPag.Rows.Add(New Object() {5, "Accr.Bancario"})
      dttTipPag.AcceptChanges()
      cbTipo.DataSource = dttTipPag
      cbTipo.ValueMember = "cod"
      cbTipo.DisplayMember = "val"
      edCodpag.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128727944642343756, "Codice pagamento"), tabpaga)

      edCodage.NTSSetRichiesto()
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

  Public Overridable Sub FRMMGCOAE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      oCallParam.bPar1 = False

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      edDataDocumentiDa.Text = "01/01/1900"
      edDataDocumentiA.Text = "31/12/2099"
      edScadenzaDocumentiDa.Text = "01/01/1900"
      edScadenzaDocumentiA.Text = "31/12/2099"

      '-------------------------------------------------
      'applico le regole della gctl
      GctlSetRoules()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Try
      Me.ValidaLastControl()

      If NTSCInt(edCodage.Text) = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128727951996093750, "Codice agente obbligatorio."))
        Return
      End If

      'Caricamento filtri di ritorno
      oCallParam.strPar1 = edDataDocumentiDa.Text
      oCallParam.strPar2 = edDataDocumentiA.Text
      oCallParam.strPar3 = edScadenzaDocumentiDa.Text
      oCallParam.strPar4 = edScadenzaDocumentiA.Text
      If optTutte.Checked Then oCallParam.strPar5 = "T"
      If optSoloMaturato.Checked Then oCallParam.strPar5 = "M"
      If optSoloPagato.Checked Then oCallParam.strPar5 = "P"
      If optNoMaturatoNoPagato.Checked Then oCallParam.strPar5 = "N"
      oCallParam.dPar1 = NTSCInt(edCodage.Text)
      oCallParam.dPar2 = NTSCInt(edCodconto.Text)
      oCallParam.dPar3 = NTSCInt(edCodpag.Text)
      oCallParam.dPar4 = NTSCInt(cbTipo.SelectedValue)
      oCallParam.bPar1 = True

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodage_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodage.Validated
    Try
      oCallParam.strParam = ""
      If edCodage.Text <> "0" Then
        If Not oCleSepv.VerificaAgente(edCodage.Text, oCallParam.strParam) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128729577196406250, "Codice agente inesistente."))
          edCodage.Text = "0"
        End If
      End If
      lbDesage.Text = oCallParam.strParam

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodconto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodconto.Validated
    Dim strTmp As String = ""
    Try
      If edCodconto.Text <> "0" Then
        If Not oCleSepv.VerificaConto(edCodconto.Text, strTmp) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128727954767500000, "Codice conto inesistente."))
          edCodconto.Text = "0"
        End If
      End If
      lbDesconto.Text = strTmp

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edCodpag_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodpag.Validated
    Dim strTmp As String = ""
    Try
      If edCodpag.Text <> "0" Then
        If Not oCleSepv.VerificaPaga(edCodpag.Text, strTmp) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 128727955329218750, "Codice pagamento inesistente."))
          edCodpag.Text = "0"
        End If
      End If
      lbDespag.Text = strTmp

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edDataDocumentiDa_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDataDocumentiDa.Validated
    Try
      If NTSCDate(edDataDocumentiDa.Text) > NTSCDate(edDataDocumentiA.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128727977803593750, "La 'Data documenti da' non può essere maggiore della 'Data documenti a'."))
        edDataDocumentiDa.Text = "01/01/1900"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edDataDocumentiA_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDataDocumentiA.Validated
    Try
      If NTSCDate(edDataDocumentiA.Text) < NTSCDate(edDataDocumentiDa.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128727978781250000, "La 'Data documenti a' non può essere minore della 'Data documenti da'."))
        edDataDocumentiA.Text = "31/12/2099"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edScadenzaDocumentiDa_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edScadenzaDocumentiDa.Validated
    Try
      If NTSCDate(edScadenzaDocumentiDa.Text) > NTSCDate(edScadenzaDocumentiA.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128727979814531250, "La 'Scadenza documenti da' non può essere maggiore della 'Scadenza documenti a'."))
        edScadenzaDocumentiDa.Text = "01/01/1900"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edScadenzaDocumentiA_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edScadenzaDocumentiA.Validated
    Try
      If NTSCDate(edScadenzaDocumentiA.Text) < NTSCDate(edScadenzaDocumentiDa.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128727979852500000, "La 'Scadenza documenti a' non può essere minore della 'Scadenza documenti da'."))
        edScadenzaDocumentiA.Text = "31/12/2099"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class

