﻿Imports System.Data.OleDb
Public Class Sifrenideyis1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox2.TextLength < 4 Or TextBox3.TextLength < 4 Then
            MsgBox("Şifrə 4 simvoldan az olmamalidir")
            Exit Sub
        End If
        If TextBox1.Text.Trim = sifrebilinmeyen Then
            If TextBox2.Text.Trim = TextBox3.Text.Trim Then
                Dim baglanti As New OleDbConnection
                baglanti.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='|DataDirectory|\Silolar.accdb'"
                baglanti.Open()
                Dim komanda As New OleDbCommand
                komanda.CommandType = CommandType.Text
                komanda.Connection = baglanti
                komanda.CommandText = "Update Indiki set Shifre1=""" + TextBox2.Text.Trim + """  "
                'baglanti.Close()
                komanda.ExecuteNonQueryAsync()
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                Me.Close()
            Else
                MsgBox("Sifreler eyni deyil")
            End If
        Else
            MsgBox("Evvelki sifreni duzgun daxil edin")
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
        sifrebilinmeyen = reader("Shifre1").ToString
        reader.Close()
        Timer1.Enabled = False
    End Sub

    Private Sub Sifrenideyis1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()

    End Sub
End Class