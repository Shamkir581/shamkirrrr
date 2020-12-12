Imports System.Data.OleDb


Public Class MehsulAdiDeyis
    Dim baglanti As New OleDbConnection
    Private Sub MehsulAdiDeyis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DaxilOl1.Close()
        ' DaxilOl1.Enabled = False
        baglanti.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='|DataDirectory|\Silolar.accdb'"
        baglanti.Open()
        Dim comand As New OleDbCommand
        comand.CommandText = "SELECT  * FROM Indiki "
        comand.CommandType = CommandType.Text
        comand.Connection = baglanti
        Dim reader As OleDbDataReader
        reader = comand.ExecuteReader()
        reader.Read()
        TextBox1.Text = reader("Mehsul1").ToString
        TextBox2.Text = reader("Mehsul2").ToString
        TextBox3.Text = reader("Mehsul3").ToString
        sifrebilinmeyen = reader("Shifre1").ToString
        reader.Close()
        baglanti.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cumla As String
        baglanti.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='|DataDirectory|\Silolar.accdb'"
        baglanti.Open()
        Dim komanda As New OleDbCommand
        cumla = "Update Indiki set Mehsul1=""" + TextBox1.Text.Trim + """"
        cumla = cumla + ", Mehsul2=""" + TextBox2.Text.Trim + """"
        cumla = cumla + ", Mehsul3=""" + TextBox3.Text.Trim + """"
        komanda.CommandText = cumla
        komanda.CommandType = CommandType.Text
        komanda.Connection = baglanti
        komanda.ExecuteNonQuery()
        Timer1.Enabled = True
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False

        Dim comand As New OleDbCommand
        comand.CommandText = "SELECT  * FROM Indiki "
        comand.CommandType = CommandType.Text
        comand.Connection = baglanti
        Dim reader As OleDbDataReader
        reader = comand.ExecuteReader()
        reader.Read()
        elek.TextBox1.Text = reader("Mehsul1").ToString
        elek.TextBox2.Text = reader("Mehsul2").ToString
        elek.TextBox3.Text = reader("Mehsul3").ToString
        baglanti.Close()


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        elek.TextBox1.Text = TextBox1.Text
        elek.TextBox2.Text = TextBox2.Text
        elek.TextBox3.Text = TextBox3.Text
        Me.Close()
    End Sub
End Class