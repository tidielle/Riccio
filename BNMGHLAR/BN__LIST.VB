Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__LIST

#Region "Variabili"
  Public oCleHlar As CLEMGHLAR
  Public oCallParams As CLE__CLDP
  Public strCodart As String = ""
  Public nFase As Integer = 0

  Public WithEvents pnList As NTSInformatica.NTSPanel

  Private components As System.ComponentModel.IContainer
#End Region

  Public Overridable Sub InitializeComponent()
    Me.pnList = New NTSInformatica.NTSPanel
    Me.ceListini = New NTSInformatica.NTSXXLIST
    CType(Me.pnList, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnList.SuspendLayout()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(231, Byte), Integer))
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'pnList
    '
    Me.pnList.AllowDrop = True
    Me.pnList.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnList.Appearance.Options.UseBackColor = True
    Me.pnList.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnList.Controls.Add(Me.ceListini)
    Me.pnList.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnList.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnList.Location = New System.Drawing.Point(0, 0)
    Me.pnList.Name = "pnList"
    Me.pnList.Size = New System.Drawing.Size(511, 301)
    Me.pnList.TabIndex = 16
    Me.pnList.Text = "NtsPanel1"
    '
    'ceListini
    '
    Me.ceListini.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceListini.LcCodart = ""
    Me.ceListini.LcConto = 0
    Me.ceListini.LcTipo = ""
    Me.ceListini.Location = New System.Drawing.Point(0, 0)
    Me.ceListini.MinimumSize = New System.Drawing.Size(504, 294)
    Me.ceListini.Name = "ceListini"
    Me.ceListini.Size = New System.Drawing.Size(511, 301)
    Me.ceListini.strNomeCampo = ""
    Me.ceListini.TabIndex = 0
    '
    'FRM__LIST
    '
    Me.ClientSize = New System.Drawing.Size(511, 301)
    Me.Controls.Add(Me.pnList)
    Me.MinimizeBox = False
    Me.Name = "FRM__LIST"
    Me.Text = "Listini relativi all'articolo"
    CType(Me.pnList, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnList.ResumeLayout(False)
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

    '------------------------------
    'collego il datatable agli altri oggetti di form

    Return True
  End Function
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try

      ceListini.NTSSetParam(oMenu, "Listini", "BNMGHLAR", Nothing)
      ceListini.LcTipo = " "
      ceListini.LcCodart = ""
      ceListini.LcFaseArticolo = -1
      ceListini.LcConto = 0
      ceListini.tlbListNuovo.Enabled = False
      ceListini.tlbListRipristina.Enabled = False
      ceListini.tlbListSalva.Enabled = False
      ceListini.tlbListCancella.Enabled = False
      ceListini.tlbListZoom.Enabled = False
      ceListini.grvList.Enabled = False
      ceListini.grvList.NTSAllowInsert = False

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
  Public Overridable Sub InitEntity(ByRef cleHlar As CLEMGHLAR)
    oCleHlar = cleHlar
    AddHandler oCleHlar.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub FRM__HLVL_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      Me.Text = oApp.Tr(Me, 128496100178116000, "Listini relativi all'articolo |" & strCodart & "|")
      ceListini.LcCodart = strCodart
      ceListini.LcFaseArticolo = nFase
      If Not ceListini.ApriListini() Then
        Me.Close()
        Return
      End If
      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlTipoDoc = "X"
      GctlSetRoules()

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


End Class