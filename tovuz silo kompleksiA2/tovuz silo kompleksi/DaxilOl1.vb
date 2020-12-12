Public Class DaxilOl1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If sifrebilinmeyen = TextBox1.Text.Trim Then
            MehsulAdiDeyis.ShowDialog()
            TextBox1.Clear()
        Else
            MsgBox("Shifreni duzgun daxil edin")
            TextBox1.Clear()

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Sifrenideyis1.ShowDialog()
        Me.Close()

    End Sub

    Private Sub DaxilOl1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Clear()
    End Sub
End Class