Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Runtime.InteropServices
'iniziato a tradurre il 08/09/08
'finito                 16/11/08
Imports System.IO
Imports System

Namespace NTSInformatica
    Public Class FRODBDIBA
        Inherits FRMDBDIBA
        ' Methods

        Public WithEvents tlb_hhaggdb As NTSBarButtonItem
        Public Overrides Sub FRMDBDIBA_Load(sender As Object, e As EventArgs)
            MyBase.FRMDBDIBA_Load(sender, e)
            tlb_hhaggdb = CType(NTSFindControlByName(Me, "tlb_hhaggdb"), NTSBarButtonItem)
        End Sub
        Public Overridable Sub tlb_hhaggdb_Click(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlb_hhaggdb.ItemClick
            oApp.MsgBoxErr("Controllare le numerazioni!")
        End Sub
    End Class
End Namespace

