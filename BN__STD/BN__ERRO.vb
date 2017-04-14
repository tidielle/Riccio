Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Public Class FRM__ERRO
  Public Overridable Sub FRM__ERRO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    edErrore.Text += vbCrLf & vbCrLf & edDettagli.Text
    edDettagli.Visible = False
    Dim i As Integer = 0

    Try
      Me.Icon = Nothing
      If CLN__STD.FRIENDLY Then
        Me.Text = "Messaggio da Friendly"
        Me.Icon = New Icon(CLN__STD.Dir & "\bnimages\friend.ico")
      Else
        Me.Text = "Messaggio da Business NET"
        Me.Icon = New Icon(CLN__STD.Dir & "\bnimages\child.ico")
      End If

      'se posso riduco il testo dello stack trace
      i = edErrore.Text.IndexOf("Error occurred in")
      If i > -1 Then
        edErrore.Select(i, edErrore.Text.Length - 1)
        edErrore.SelectionFont = New Font("Thaoma", 8, FontStyle.Regular)
        edErrore.SelectionColor = System.Drawing.Color.DarkBlue
      End If

      'se posso riduco il testo da *Reference fino alla fine
      i = edErrore.Text.IndexOf("*Reference")
      If i > -1 Then
        i -= 90
        edErrore.Select(i, edErrore.Text.Length - 1)
        edErrore.SelectionFont = New Font("Courier New", 7, FontStyle.Regular)
        edErrore.SelectionColor = System.Drawing.Color.Black
      End If


      'Me.WindowState = FormWindowState.Maximized
    Catch ex As Exception
      'Non do errori
    End Try
    'If edDettagli.Text.Trim = "" Then lbDettagli.Visible = False
    'If edErrore.Text.Substring(0, 15) = "Error occured: " Then edErrore.Text = edErrore.Text.Substring(15).Trim()
    'If edDettagli.Text.Trim <> "" Then
    '  If edErrore.Text.Substring(edErrore.Text.Length - 80) = "".PadLeft(80, CChar("-")) Then
    '    edErrore.Text = edErrore.Text.Substring(0, edErrore.Text.Length - 80).Trim
    '    edDettagli.Text = "".PadLeft(80, CChar("-")) & vbCrLf & edDettagli.Text.Trim
    '  End If
    'End If
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
    Me.Close()
  End Sub

  Public Overridable Sub lbStampa_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbStampa.LinkClicked
    Dim printDlg As New PrintDialog

    printDlg.AllowPrintToFile = False
    printDlg.AllowSelection = False
    printDlg.AllowSomePages = False
    printDlg.PrintToFile = False
    printDlg.AllowCurrentPage = False
    printDlg.UseEXDialog = True
    If printDlg.ShowDialog() = DialogResult.OK Then
      Me.printPreview.InvalidatePreview()
      'Me.printPreview.Zoom = 0.2
      'Me.printPreview.Visible = True
      Me.simplePrintDoc.PrinterSettings = printDlg.PrinterSettings
      Me.Cursor = Cursors.WaitCursor
      Me.simplePrintDoc.Print()
      Me.Cursor = Cursors.Default
    End If
  End Sub

  Public Overridable Sub simplePrintDoc_PrintPage( _
    ByVal sender As System.Object, _
    ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles simplePrintDoc.PrintPage

    Dim g As Graphics
    g = e.Graphics

    g.DrawString(edErrore.Text, _
        New Font("Verdana", 8, FontStyle.Regular, GraphicsUnit.Point), _
        Brushes.Black, New RectangleF(e.MarginBounds.X, _
                                      e.MarginBounds.Y, _
                                      e.MarginBounds.Width, _
                                      e.MarginBounds.Height))
  End Sub

  Public Overridable Sub lbCopia_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbCopia.LinkClicked
    Try
      Clipboard.Clear()
      Clipboard.SetText(edErrore.Text)
    Catch
    End Try
  End Sub

End Class
