Public Class DaxilOl2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If sifrebilinmeyen2 = TextBox1.Text.Trim Then
            hadiseler.ShowDialog()
            TextBox1.Clear()
            Me.Close()
        Else
            MsgBox("Shifreni duzgun daxil edin")
            TextBox1.Clear()

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Shifreni_deyis2.ShowDialog()
        Me.Close()
    End Sub
End Class