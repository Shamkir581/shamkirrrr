Imports System.Data.OleDb
Imports System.Data

Public Class hadiseler
    Dim baglanti As New OleDbConnection(" Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Silolar.accdb")
    Dim tablo As New DataTable
    Dim tablo1 As New DataTable
    Dim miqdarmehsul(0 To 3) As Integer
    Private Sub listele()
        baglanti.Open()
        Dim adtr As New OleDbDataAdapter("select *from sinaq", baglanti)
        adtr.Fill(tablo)
        DataGridView1.DataSource = tablo
        baglanti.Close()
    End Sub
    Private Sub hadiseler_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        listele()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim tar As Date
        Dim SiloN, Dolummiq, BosMiq, QalMiq As Integer
        tar = Now.ToString
        SiloN = ComboBox1.SelectedIndex + 1
        If ComboBox2.SelectedIndex = 0 Then
            Dolummiq = Int(Val(TextBox1.Text))
            BosMiq = 0
        ElseIf ComboBox2.SelectedIndex = 1 Then
            Dolummiq = 0
            BosMiq = Int(Val(TextBox1.Text))
        End If
        QalMiq = miqdarmehsul(SiloN - 1) + Dolummiq - BosMiq
        baglanti.Open()
        Dim Komanda As New OleDbCommand("insert into sinaq(tarix, SiloNomre,DolumMiqdar,BoshaltmaMiqdar,QalanMiqdar,Qeyd)values('" + tar + "','" + SiloN.ToString + "','" + Dolummiq.ToString + "','" + BosMiq.ToString + "','" + QalMiq.ToString + "','" + TextBox2.Text + "')", baglanti)
        Komanda.ExecuteNonQueryAsync()
        MsgBox("bazaya qeyd olundu")
        baglanti.Close()
        miqdarmehsul(SiloN - 1) = QalMiq
        tablo.Clear()
        listele()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        baglanti.Open()
        Dim komand As New OleDbCommand("delete *from sinaq where tarix='" + DataGridView1.CurrentRow.Cells("tarix").Value.ToString() + "'", baglanti)
        komand.ExecuteNonQueryAsync()
        baglanti.Close()
        tablo.Clear()
        listele()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        tablo1.Clear()
        Dim silosecilmis As Integer
        silosecilmis = ComboBox3.SelectedIndex
        baglanti.Open()
        If ComboBox3.SelectedIndex = 0 Then
            Dim adtrt As New OleDbDataAdapter("select *from sinaq where cdate(tarix)>=cdate(""" + DateTimePicker1.Value.ToString + """) and tarix<=(""" + DateTimePicker2.Value.ToString + """)", baglanti)
            adtrt.Fill(tablo1)
            DataGridView1.DataSource = tablo1
        Else
            Dim adtrtt As New OleDbDataAdapter("select *from sinaq where SiloNomre=(""" + silosecilmis.ToString + """)", baglanti)
            adtrtt.Fill(tablo1)
            DataGridView1.DataSource = tablo1
        End If
        baglanti.Close()
    End Sub
End Class