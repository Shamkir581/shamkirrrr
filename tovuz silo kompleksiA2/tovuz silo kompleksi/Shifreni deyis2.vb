Imports System.Data.OleDb
Imports System.Data

Public Class Shifreni_deyis2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox2.TextLength < 4 Or TextBox3.TextLength < 4 Then
            MsgBox("Şifrə 4 simvoldan az olmamalidir")
            Exit Sub
        End If
        If TextBox1.Text.Trim = sifrebilinmeyen2 Then
            If TextBox2.Text.Trim = TextBox3.Text.Trim Then
                Dim baglanti As New OleDbConnection
                baglanti.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='|DataDirectory|\Silolar.accdb'"
                baglanti.Open()
                Dim komanda As New OleDbCommand
                komanda.CommandType = CommandType.Text
                komanda.Connection = baglanti
                komanda.CommandText = "Update Indiki set Shifre2=""" + TextBox2.Text.Trim + """  "
                'baglanti.Close()
                komanda.ExecuteNonQueryAsync()
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                Me.Close()
            Else
                MsgBox("Şifrələr uyğun deyil")
            End If
        Else
            MsgBox("Əvvəlki şifrəni düzgün daxil edin")
        End If
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim baglanti1 As New OleDbConnection
        baglanti1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='|DataDirectory|\Silolar.accdb'"
        baglanti1.Open()
        Dim comand As New OleDbCommand
        comand.CommandText = "SELECT  * FROM Indiki "
        comand.CommandType = CommandType.Text
        comand.Connection = baglanti1
        Dim reader As OleDbDataReader
        reader = comand.ExecuteReader()
        reader.Read()
        sifrebilinmeyen2 = reader("Shifre2").ToString
        reader.Close()
        Timer1.Enabled = False
    End Sub
End Class