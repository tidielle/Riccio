'iniziato a tradurre il 08/09/08
'finito                 16/11/08

Imports DevExpress.XtraGrid.Views.Base
Imports NTSInformatica

Public Class FRODBDIBA
    Public WithEvents fm_hhtot As NTSGroupBox
    Public WithEvents ed_hhvaluso As NTSTextBoxNum
    Public WithEvents ed_hhcostocav As NTSTextBoxNum
    Public WithEvents ed_hhcostocoll As NTSTextBoxNum
    Public WithEvents ed_hhcostoconf As NTSTextBoxNum
    Public WithEvents ed_hhcfcad As NTSTextBoxNum
    Public WithEvents ed_hhcscfn As NTSTextBoxNum
    Public WithEvents ed_hhtl As NTSTextBoxNum
    Public WithEvents ed_hhtul As NTSTextBoxNum
    Public WithEvents ed_hhtun As NTSTextBoxNum
    Public WithEvents ed_hhcavul As NTSTextBoxNum
    Public WithEvents ed_hhcavun As NTSTextBoxNum
    Public WithEvents ed_hhccoll As NTSTextBoxNum
    Public WithEvents ed_hhccoln As NTSTextBoxNum
    Public WithEvents ed_hhsc As NTSTextBoxStr
    Public WithEvents opt_hhtike As NTSRadioButton
    Public WithEvents opt_hhtial As NTSRadioButton
    Public WithEvents opt_hhtend As NTSRadioButton
    Public WithEvents opt_hhtess As NTSRadioButton
    Public WithEvents opt_hhigni As NTSRadioButton
    Public WithEvents md_hhcodartf As NTSGridColumn
    Public WithEvents md_hhdescr As NTSGridColumn
    Public WithEvents tlb_hhaggdb As NTSBarButtonItem
    Dim frmparent As FRMDBDIBA
    Public Overrides Sub FRMDBDIBA_Load(sender As Object, e As EventArgs)
        MyBase.FRMDBDIBA_Load(sender, e)
        tlb_hhaggdb = CType(NTSFindControlByName(Me, "tlb_hhaggdb"), NTSBarButtonItem)
        fm_hhtot = CType(NTSFindControlByName(Me, "fm_hhtot"), NTSGroupBox)
        ed_hhvaluso = CType(NTSFindControlByName(Me, "ed_hhvaluso"), NTSTextBoxNum)
        ed_hhcostocav = CType(NTSFindControlByName(Me, "ed_hhcostocav"), NTSTextBoxNum)
        ed_hhcostocoll = CType(NTSFindControlByName(Me, "ed_hhcostocoll"), NTSTextBoxNum)
        ed_hhcostoconf = CType(NTSFindControlByName(Me, "ed_hhcostoconf"), NTSTextBoxNum)
        ed_hhcfcad = CType(NTSFindControlByName(Me, "ed_hhcfcad"), NTSTextBoxNum)
        ed_hhcscfn = CType(NTSFindControlByName(Me, "ed_hhcscfn"), NTSTextBoxNum)
        ed_hhtl = CType(NTSFindControlByName(Me, "ed_hhtl"), NTSTextBoxNum)
        ed_hhtul = CType(NTSFindControlByName(Me, "ed_hhtul"), NTSTextBoxNum)
        ed_hhtun = CType(NTSFindControlByName(Me, "ed_hhtun"), NTSTextBoxNum)
        ed_hhcavul = CType(NTSFindControlByName(Me, "ed_hhcavul"), NTSTextBoxNum)
        ed_hhcavun = CType(NTSFindControlByName(Me, "ed_hhcavun"), NTSTextBoxNum)
        ed_hhccoll = CType(NTSFindControlByName(Me, "ed_hhccoll"), NTSTextBoxNum)
        ed_hhccoln = CType(NTSFindControlByName(Me, "ed_hhccoln"), NTSTextBoxNum)
        ed_hhsc = CType(NTSFindControlByName(Me, "ed_hhsc"), NTSTextBoxStr)
        opt_hhtike = CType(NTSFindControlByName(Me, "opt_hhtike"), NTSRadioButton)
        opt_hhtial = CType(NTSFindControlByName(Me, "opt_hhtial"), NTSRadioButton)
        opt_hhtend = CType(NTSFindControlByName(Me, "opt_hhtend"), NTSRadioButton)
        opt_hhtess = CType(NTSFindControlByName(Me, "opt_hhtess"), NTSRadioButton)
        opt_hhigni = CType(NTSFindControlByName(Me, "opt_hhigni"), NTSRadioButton)
        md_hhcodartf = grvMovdis.NTSGetColumnByName("md_hhcodartf")
        md_hhcodartf.NTSSetRichiesto()
        md_hhdescr = grvMovdis.NTSGetColumnByName("md_hhdescr")
        md_hhdescr.NTSSetRichiesto()


        ed_hhvaluso.Enabled = False
        ed_hhcostocav.Enabled = False
        ed_hhcostocoll.Enabled = False
        ed_hhcostoconf.Enabled = False
        ed_hhcfcad.Enabled = False
        ed_hhcscfn.Enabled = False
        ed_hhtl.Enabled = False
        ed_hhtul.Enabled = False
        ed_hhtun.Enabled = False
        ed_hhcavul.Enabled = False
        ed_hhcavun.Enabled = False
        ed_hhccoll.Enabled = False
        ed_hhccoln.Enabled = False
        ed_hhsc.NTSSetParam(oMenu, "Sconto", 40, False)
        ed_hhsc.NTSSetRichiesto()


    End Sub
    Public Overridable Sub opt_hhtike_CheckStateChanged(sender As Object, e As EventArgs) Handles opt_hhtike.CheckStateChanged
        '        MsgBox("test")
        If opt_hhtike.CheckState = CheckState.Checked Or opt_hhtial.CheckState = CheckState.Checked Then
            grvMovdis.Columns("md_hhalt").Visible = False
            grvMovdis.Columns("md_hhfin").Visible = False
            grvMovdis.Columns("md_hhcomp").Visible = False
            grvMovdis.Columns("md_hhmolt").Visible = False
            grvMovdis.Columns("md_hhscomin").Visible = False
            grvMovdis.Columns("md_hhscomax").Visible = False
            grvMovdis.Columns("md_hhtirella").Visible = True
            grvMovdis.Columns("md_hhlc").Visible = True
        Else
            grvMovdis.Columns("md_hhalt").Visible = True
            grvMovdis.Columns("md_hhfin").Visible = True
            grvMovdis.Columns("md_hhcomp").Visible = True
            grvMovdis.Columns("md_hhmolt").Visible = True
            grvMovdis.Columns("md_hhscomin").Visible = True
            grvMovdis.Columns("md_hhscomax").Visible = True
            grvMovdis.Columns("md_hhtirella").Visible = False
            grvMovdis.Columns("md_hhlc").Visible = False
        End If



    End Sub
    Public Overridable Sub opt_hhtial_CheckStateChanged(sender As Object, e As EventArgs) Handles opt_hhtial.CheckStateChanged
        '        MsgBox("test")
        If opt_hhtike.CheckState = CheckState.Checked Or opt_hhtial.CheckState = CheckState.Checked Then
            grvMovdis.Columns("md_hhalt").Visible = False
            grvMovdis.Columns("md_hhfin").Visible = False
            grvMovdis.Columns("md_hhcomp").Visible = False
            grvMovdis.Columns("md_hhmolt").Visible = False
            grvMovdis.Columns("md_hhscomin").Visible = False
            grvMovdis.Columns("md_hhscomax").Visible = False
            grvMovdis.Columns("md_hhtirella").Visible = True
            grvMovdis.Columns("md_hhlc").Visible = True
        Else
            grvMovdis.Columns("md_hhalt").Visible = True
            grvMovdis.Columns("md_hhfin").Visible = True
            grvMovdis.Columns("md_hhcomp").Visible = True
            grvMovdis.Columns("md_hhmolt").Visible = True
            grvMovdis.Columns("md_hhscomin").Visible = True
            grvMovdis.Columns("md_hhscomax").Visible = True
            grvMovdis.Columns("md_hhtirella").Visible = False
            grvMovdis.Columns("md_hhlc").Visible = False
        End If



    End Sub
    Public Overridable Sub tlb_hhaggdb_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlb_hhaggdb.ItemClick
        'MsgBox("test")

        'Dim timeStamp As String = DateTime.Now.ToString("yyyyMMddhhmmss")
        'Dim oldRow As DataRow = oCleDiba.dsMovdisShared.Tables(0).Rows(CType(oCleDiba, CLFDBDIBA).row)
        'Dim newRow As DataRow = oCleDiba.dsMovdisShared.Tables(0).NewRow
        'oldRow.Item("ts") = ""
        'newRow.Item(CType(oCleDiba, CLFDBDIBA).row) = oldRow

        'oCleDiba.dsMovdisShared.Tables(0).Rows.Add(newRow)

        'oCleDiba.dsMovdisShared.Tables(0).Rows.Add()

        'For index As Int32 = 0 To oCleDiba.dsMovdisShared.Tables(0).Columns.Count
        '    oCleDiba.dsMovdisShared.Tables(0).Rows(CType(oCleDiba, CLFDBDIBA).row).Item(index).Value = DGV1.Rows(CInt(CopyRowIndex)).Cells(index).Value

        'Next
    End Sub
    Public Overrides Sub MovdisApri()
        MyBase.MovdisApri()
        RipartisciTotali()


    End Sub

    Public Overrides Sub grvMovdis_NTSBeforeRowUpdate(sender As Object, e As RowAllowEventArgs)
        MyBase.grvMovdis_NTSBeforeRowUpdate(sender, e)
        RipartisciTotali()
    End Sub
    Public Overridable Function RipartisciTotali() As Boolean
        Dim hhsc As Double = 0
        Dim hhcotoconf As Double = 0
        Try

            hhcotoconf = Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(3))
            hhsc = Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaSconti(ed_hhsc.Text))

            ed_hhtl.Text = CType(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(1), String)
            ed_hhvaluso.Text = CType(Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(0)) - Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(1)), String)
            ed_hhcostocav.Text = CType(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(2), String)
            'ed_hhcostoconf.Text = CType(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(3), String)
            ed_hhcostoconf.Text = CType(hhcotoconf - (hhcotoconf * hhsc), String)
            ed_hhcostocoll.Text = CType(Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(0)) - Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(1)) + Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(2)) + Convert.ToDouble(hhcotoconf - (hhcotoconf * hhsc)), String)
            '        ed_hhcostocoll.Text = CType(Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(0)) - Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(1)) + Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(2)) + Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(3)), String)
            ed_hhtul.Text = CType(Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(0)) / Convert.ToDouble(oCleDiba.dsCicliShared.Tables(0).Rows(0)("dd_perqta")), String)
            ed_hhtun.Text = CType((Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(0)) - Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(1))) / Convert.ToDouble(oCleDiba.dsCicliShared.Tables(0).Rows(0)("dd_perqta")), String)
            ed_hhcavul.Text = CType(Convert.ToDouble(ed_hhcostocav.Text) / Convert.ToDouble(oCleDiba.dsCicliShared.Tables(0).Rows(0)("dd_perqta")), String)
            ed_hhcavun.Text = CType(Convert.ToDouble(ed_hhcostocav.Text) / Convert.ToDouble(oCleDiba.dsCicliShared.Tables(0).Rows(0)("dd_perqta")), String)
            ed_hhccoll.Text = CType(Convert.ToDouble(ed_hhcfcad.Text) + Convert.ToDouble(ed_hhtul.Text) + Convert.ToDouble(ed_hhcavul.Text), String)
            ed_hhccoln.Text = CType(Convert.ToDouble(ed_hhcscfn.Text) + Convert.ToDouble(ed_hhtun.Text) + Convert.ToDouble(ed_hhcavun.Text), String)
        Catch ex As Exception

        End Try
    End Function
    Private Sub ed_hhsc_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles ed_hhsc.KeyPress

        Dim allowedChars As String = "0123456789+." & vbBack

        'get a reference to the text box that fired this event
        Dim tText As NTSTextBoxStr
        tText = CType(sender, NTSTextBoxStr)

        If allowedChars.IndexOf(e.KeyChar) = -1 Then
            ' Invalid Character
            e.Handled = True
        End If

        'If tText.Text.Length >= 12 Then
        '    e.Handled = True
        'End If
    End Sub
    Public Overridable Sub ed_hhsc_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles ed_hhsc.Validated
        recall()
    End Sub
    Public Overridable Sub ed_hhvaluso_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles ed_hhvaluso.Validated
        recall()
    End Sub
    Public Overridable Sub recall()
        Dim hhsc As Double = 0
        Dim hhtot As Double = 0
        Dim perqta As Double = 0
        Dim hhcostoconf As Double = 0
        hhtot = Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaTotali("")(3))
        hhsc = Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaSconti(ed_hhsc.Text))
        hhcostoconf = Convert.ToDouble(CType(oCleDiba, CLFDBDIBA).CalcolaSconti(ed_hhcostoconf.Text))
        perqta = Convert.ToDouble(oCleDiba.dsCicliShared.Tables(0).Rows(0)("dd_perqta"))
        'ed_hhcscfn.Text = CType(((hhcostoconf - (hhcostoconf * hhsc)) / perqta) * 100, String)
        'ed_hhcfcad.Text = CType((hhcostoconf / perqta) * 100, String)
        ed_hhcfcad.Text = CType((hhtot / perqta), String)
        'ed_hhcscfn.Text = CType((hhtot / perqta) * 100, String)
        ed_hhcscfn.Text = CType(((hhtot - (hhtot * hhsc)) / perqta), String)
        RipartisciTotali()
    End Sub


    Public Overridable Sub ed_hhcostoconf_Validated() Handles ed_hhcostoconf.Validated
        recall()
    End Sub

End Class
