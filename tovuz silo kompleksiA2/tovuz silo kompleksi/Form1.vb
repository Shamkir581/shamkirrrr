Imports EasyModbus
Imports System.Data
Imports System.Data.OleDb


Public Class elek
    Dim baglanti As New OleDbConnection
    Dim modbusclient1 As ModbusClient = New ModbusClient("192.168.0.10", 502)
    Dim indconnect1 As Boolean
    Public Rhregisters(0 To 20) As Integer
    Public Rhregisters2(0 To 20) As Integer
    Public indLoad As Boolean
    Dim IndButtonDolumStart As Boolean
    Dim IndButtonDolumStop As Boolean
    Dim IndButtonSSStart As Boolean
    Dim IndButtonSSStop As Boolean
    Dim IndButtonBoshaltStart As Boolean
    Dim IndButtonBoshaltStop As Boolean
    Dim IndButtonReset As Boolean
    Dim programyolu As String
    Dim strveziyyet As String

    Dim VentRejim(0 To 3) As Integer
    Dim VentIshVaxti(0 To 3) As Integer
    Dim VentDayanmaVaxti(0 To 3) As Integer
    Dim VentVaxt(0 To 3) As Integer
    Dim Vent_Ishleyir(0 To 3) As Boolean
    Dim VentIshVaxtisaat(0 To 3) As Integer
    Dim ventisvaxtiumumi(0 To 3) As Integer
    Dim ventdayanmavaxtiumumi(0 To 3) As Integer
    Dim ventdayanmavaxtisaat(0 To 3) As Integer

    Dim komandaqurgu As Integer
    Dim Kod_Komanda(0 To 42, 0 To 3) As Integer
    Dim qurgu As Integer



    Public Sub UmumiKomanda(ByVal KomandaQurgu As Integer, ByVal AmrTipi As Integer)
        GonderModbus(1, Kod_Komanda(KomandaQurgu, AmrTipi))

    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        For i = 0 To 42
            For j = 0 To 3
                Kod_Komanda(i, j) = 0
            Next
        Next
        Kod_Komanda(1, 1) = 10840
        Kod_Komanda(1, 2) = 10841
        Kod_Komanda(2, 1) = 10842
        Kod_Komanda(2, 2) = 10843
        Kod_Komanda(3, 1) = 10844
        Kod_Komanda(3, 2) = 10845
        Kod_Komanda(4, 1) = 10846
        Kod_Komanda(4, 2) = 10847
        Kod_Komanda(5, 1) = 10848
        Kod_Komanda(5, 2) = 10849
        Kod_Komanda(6, 1) = 10850
        Kod_Komanda(6, 2) = 10851
        Kod_Komanda(7, 1) = 10852
        Kod_Komanda(7, 2) = 10853
        Kod_Komanda(8, 1) = 10854
        Kod_Komanda(8, 2) = 10855
        Kod_Komanda(9, 1) = 10856
        Kod_Komanda(9, 2) = 10857
        Kod_Komanda(10, 1) = 10858
        Kod_Komanda(10, 2) = 10859
        Kod_Komanda(11, 1) = 10860
        Kod_Komanda(11, 2) = 10861
        Kod_Komanda(12, 1) = 10862
        Kod_Komanda(12, 2) = 10863
        Kod_Komanda(13, 1) = 10864
        Kod_Komanda(13, 2) = 10865
        Kod_Komanda(14, 1) = 10866
        Kod_Komanda(14, 2) = 10867
        Kod_Komanda(15, 1) = 10868
        Kod_Komanda(15, 2) = 10869
        Kod_Komanda(16, 1) = 10870
        Kod_Komanda(16, 2) = 10871
        Kod_Komanda(17, 1) = 10872
        Kod_Komanda(17, 2) = 10873
        Kod_Komanda(18, 1) = 10874
        Kod_Komanda(18, 2) = 10875
        Kod_Komanda(19, 1) = 10876
        Kod_Komanda(19, 2) = 10877
        Kod_Komanda(20, 1) = 10878
        Kod_Komanda(20, 2) = 10879








        indLoad = False
        IndButtonDolumStart = False
        IndButtonDolumStop = False
        IndButtonSSStart = False
        IndButtonSSStop = False
        IndButtonBoshaltStart = False
        IndButtonBoshaltStart = False


        Try
            modbusclient1.ConnectionTimeout = 100
            modbusclient1.Connect()
            indconnect1 = True
        Catch ex As Exception
            KontElaqe.Text = "Plc ile elaqe yoxdur"
            KontElaqe.BackColor = Color.Red
            modbusclient1.Disconnect()
            indconnect1 = False
            tezele1()



        End Try
        baglanti.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='|DataDirectory|\Silolar.accdb'"
        baglanti.Open()
        Dim comand As New OleDbCommand
        comand.CommandText = "SELECT  * FROM Indiki "
        comand.CommandType = CommandType.Text
        comand.Connection = baglanti
        Dim reader As OleDbDataReader
        reader = comand.ExecuteReader()
        reader.Read()
        strveziyyet = reader("Veziyyet").ToString

        TextBox1.Text = reader("Mehsul1").ToString
        TextBox2.Text = reader("Mehsul2").ToString
        TextBox3.Text = reader("Mehsul3").ToString
        sifrebilinmeyen = reader("Shifre1").ToString
        sifrebilinmeyen2 = reader("shifre2".ToString)
        programyolu = Application.StartupPath
        reader.Close()

        For i = 0 To 9
            Rhregisters(i) = 0
            Rhregisters2(i) = 0

        Next
        'tezele1()
        indLoad = True
        TimerMB.Enabled = True


    End Sub
    Private Sub GonderModbus(ByVal Unvan, ByVal Kod)
        Try
            modbusclient1.WriteSingleRegister(Unvan, Kod)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub TimerMB_Tick(sender As Object, e As EventArgs) Handles TimerMB.Tick
        '  Exit Sub

        If indconnect1 = False Then
            Try
                modbusclient1.Connect()
                indconnect1 = True
            Catch ex As Exception
                KontElaqe.Text = "PLC ilə əlaqə yoxdur"
                KontElaqe.BackColor = Color.Red
                indconnect1 = False
                tezele1()
                Exit Sub
            End Try
        End If
        Try
            If modbusclient1.Connected = True Then
                modbusclient1.Connect()
                indconnect1 = True
                KontElaqe.Text = "PLC ilə əlaqə var"
                KontElaqe.BackColor = Color.LawnGreen
                Rhregisters = modbusclient1.ReadHoldingRegisters(0, 8)
                tezele1()
                Exit Sub
            Else
                modbusclient1.Connect()
                indconnect1 = True
            End If
        Catch ex As Exception
            KontElaqe.Text = "PLC ilə əlaqə yoxdur"
            KontElaqe.BackColor = Color.Red
            modbusclient1.Disconnect()
            indconnect1 = False
            tezele1()
            Exit Sub
        End Try

    End Sub
    Private Sub tezele1()
        Dim r0 As Integer
        Dim Vez, VaxtS As String
        Dim V1 As DateTime
        Dim IndH, KodH As Integer
        Dim i As Integer
        Dim kod As Integer
        Dim RHReg32(0 To 10) As Long
        Dim WHReg32(0 To 10) As Long
        Dim V(0 To 144) As Boolean

        V1 = Now
        VaxtS = Format(V1.Year, "0000") + "-" + Format(V1.Month, "00") + "-" + Format(V1.Day, "00") + " " + Format(V1.Hour, "00") + ":" + Format(V1.Minute, "00") + ":" + Format(V1.Second, "00")

        For i = 0 To 4
            If Rhregisters(i) >= 0 Then
                RHReg32(i) = Rhregisters(i)
            Else
                RHReg32(i) = 65536 + Rhregisters(i)
            End If
        Next
        Vez = ""
        For i = 0 To 4
            r0 = RHReg32(i)
            For j = 0 To 15
                If r0 Mod 2 = 1 Then
                    V(i * 16 + j) = True
                    Vez = Vez + "1"

                Else
                    V(i * 16 + j) = False
                    Vez = Vez + "0"

                End If
                r0 = Int(r0 / 2)
            Next
        Next

        Dim con As New OleDbConnection
        Dim Cumla As String
        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='|DataDirectory|\Silolar.accdb'"
        con.Open()
        Dim comand1 As New OleDbCommand
        Cumla = "Update Indiki set Vaxt=cdate(""" + VaxtS + """), Veziyyet=""" + Vez + """  "
        comand1.CommandText = Cumla
        comand1.CommandType = CommandType.Text
        comand1.Connection = con
        comand1.ExecuteNonQuery()

        For i = 1 To 112
            If Mid(strveziyyet, i, 1) <> Mid(Vez, i, 1) Then
                kod = i * 10 + 10000
                KodH = kod
                If Mid(Vez, i, 1) = "1" Then
                    kod = kod + 1
                    IndH = 1
                Else
                    kod = kod + 2
                    IndH = 2
                End If
                Cumla = "Insert into Hadiseler (Vaxt,KodHad) Values (cdate(""" + VaxtS + """), " + kod.ToString + ") "
                comand1.CommandText = Cumla
                comand1.CommandType = CommandType.Text
                comand1.Connection = con
                comand1.ExecuteNonQuery()
            End If
        Next
        con.Close()
        strveziyyet = Vez
        'If V(0) Then Termik1.Visible = True Else Termik1.Visible = False
        'If V(1) Then M1aciq.Visible = True Else M1aciq.Visible = False
        'If V(2) Then M1bagli.Visible = True Else M1bagli.Visible = False
        'If V(3) Then m2termik.Visible = True Else m2termik.Visible = False
        'If V(4) Then M2cavab.Visible = True Else M2cavab.Visible = False
        'If V(5) Then m2zincirQeza.visible=True Else m2zincirqeza.visible=False
        'If V(6) Then m2qapaqQeza.Visible = True Else m2qapaqQeza.Visible = False
        'If V(7) Then M3Termik.Visible = True Else M3Termik.Visible = False
        'If V(8) Then M3Sol.Visible = True Else M3Sol.Visible = False
        'If V(9) Then M3Sag.Visible = True Else M3Sag.Visible = False
        'If V(10) Then M4Termik.Visible = True Else M4Termik.Visible = False
        'If V(11) Then
        '    M4cavabyuxari.Visible = True
        '    M4cavaborta.Visible = True
        '    M4cavabasagi.Visible = True
        'Else
        '    M4cavabyuxari.Visible = False
        '    M4cavaborta.Visible = False
        '    M4cavabasagi.Visible = False

        'End If
        'If V(12) Then M4DevirQeza.Visible = True Else M4DevirQeza.Visible = False
        'If V(13) Then m5Termik.Visible = True Else m5Termik.Visible = False
        'If V(14) Then m5Cavab.Visible = True Else m5Cavab.Visible = False
        'If V(15) Then m6Termik.Visible = True Else m6Termik.Visible = False
        'If V(16) Then M6Cavab.Visible = True Else M6Cavab.Visible = False
        'If V(17) Then M7Termik.Visible = True Else M7Termik.Visible = False
        'If V(18) Then M7Cavab.Visible = True Else M7Cavab.Visible = False
        'If V(19) Then M8Termik.Visible = True Else M8Termik.Visible = False
        'If V(20) Then M8Cavab.Visible = True Else M8Cavab.Visible = False
        'If V(21) Then M9Termik.Visible = True Else M9Termik.Visible = False
        'If V(22) Then M9Cavab.Visible = True Else M9Cavab.Visible = False
        'If V(23) Then M9ZincirQeza.Visible = True Else M9ZincirQeza.Visible = False
        'If V(24) Then M9QapaqQeza.Visible = True Else M9QapaqQeza.Visible = False
        'If V(25) Then M10Termik.Visible = True Else M10Termik.Visible = False
        'If V(26) Then
        '    M10CavabAsagi.Visible = True
        '    M10CavabOrta.Visible = True
        '    M10CavabYuxari.Visible = True
        'Else
        '    M10CavabAsagi.Visible = False
        '    M10CavabOrta.Visible = False
        '    M10CavabYuxari.Visible = False
        'End If
        'If V(27) Then M10DevirQeza.Visible = True Else M10DevirQeza.Visible = False
        'If V(28) Then M11Termik.Visible = True Else M11Termik.Visible = False
        'If V(29) Then M11Sol.Visible = True Else M11Sol.Visible = False
        'If V(30) Then M11Sag.Visible = True Else M11Sag.Visible = False
        'If V(31) Then M12Termik.Visible = True Else M12Termik.Visible = False
        'If V(32) Then M12Cavab.Visible = True Else M12Cavab.Visible = False
        'If V(33) Then M12ZincirQeza.Visible = True Else M12ZincirQeza.Visible = False
        'If V(34) Then M12Qapaqqeza.Visible = True Else M12Qapaqqeza.Visible = False
        'If V(35) Then M13Termik.Visible = True Else M13Termik.Visible = False
        'If V(36) Then M13Achiq.Visible = True Else M13Achiq.Visible = False
        'If V(37) Then M13Bagli.Visible = True Else M13Bagli.Visible = False
        'If V(38) Then M14Termik.Visible = True Else M14Termik.Visible = False
        'If V(39) Then M14Achiq.Visible = True Else M14Achiq.Visible = False
        'If V(40) Then M14Bagli.Visible = True Else M14Bagli.Visible = False
        'If V(41) Then Silo3Axish.Visible = True Else Silo3Axish.Visible = False
        'If V(42) Then M15Termik.Visible = True Else M15Termik.Visible = False
        'If V(43) Then M15Achiq.Visible = True Else M15Achiq.Visible = False
        'If V(44) Then M15Bagli.Visible = True Else M15Bagli.Visible = False
        'If V(45) Then M16Termik.Visible = True Else M16Termik.Visible = False
        'If V(46) Then M16Achiq.Visible = True Else M16Achiq.Visible = False
        'If V(47) Then M16Bagli.Visible = True Else M16Bagli.Visible = False
        'If V(48) Then M17Termik.Visible = True Else M17Termik.Visible = False
        'If V(49) Then M17Achiq.Visible = True Else M17Achiq.Visible = False
        'If V(50) Then M17Bagli.Visible = True Else M17Bagli.Visible = False
        'If V(51) Then M18Termik.Visible = True Else M18Termik.Visible = False
        'If V(52) Then M18Cavab.Visible = True Else M18Cavab.Visible = False
        'If V(53) Then M18ZincirQeza.Visible = True Else M18ZincirQeza.Visible = False

        'If V(54) Then M18QapaqQeza.Visible = True Else M18QapaqQeza.Visible = False
        'If V(55) Then M19Termik.Visible = True Else M19Termik.Visible = False
        'If V(56) Then M19Cavab.Visible = True Else M19Cavab.Visible = False
        'If V(57) Then M20Termik.Visible = True Else M20Termik.Visible = False
        'If V(58) Then M20Cavab.Visible = True Else M20Cavab.Visible = False
        'If V(59) Then M21Termik.Visible = True Else M21Termik.Visible = False
        'If V(60) Then M21Cavab.Visible = True Else M21Cavab.Visible = False
        'If V(61) Then M22Termik.Visible = True Else M22Termik.Visible = False
        'If V(62) Then M22Cavab.Visible = True Else M22Cavab.Visible = False
        'If V(63) Then M23Termik.Visible = True Else M23Termik.Visible = False
        'If V(64) Then M23Cavab.Visible = True Else M23Cavab.Visible = False
        'If V(65) Then M24Termik.Visible = True Else M24Termik.Visible = False
        'If V(66) Then M24Cavab.Visible = True Else M24Cavab.Visible = False
        ''If V(67) Then
        ''    M25Termik.Visible = True
        ''    M26Termik.Visible = True
        ''    M27Termik.Visible = True
        ''Else
        ''    M25Termik.Visible = False
        ''    M26Termik.Visible = False
        ''    M27Termik.Visible = False
        ''End If
        ''If V(68) Then m25cavab
        ''If V(69) Then m26cavab
        ''If V(70) Then m27cavab
        'If V(71) Then Sev1.Visible = True Else Sev1.Visible = False
        'If V(72) Then Sev2.Visible = True Else Sev2.Visible = False
        'If V(73) Then sev3.Visible = True Else sev3.Visible = False
        'If V(74) Then Sev4yuxari.Visible = True Else Sev4yuxari.Visible = False
        'If V(75) Then Sev5asagi.Visible = True Else Sev5asagi.Visible = False
        'If V(76) Then M4Yukcox.Visible = True Else M4Yukcox.Visible = False
        'If V(77) Then m10Yukcox.Visible = True Else m10Yukcox.Visible = False





















    End Sub


    Private Sub dolum0_CheckedChanged(sender As Object, e As EventArgs) Handles dolum0.CheckedChanged, dolum1.CheckedChanged, dolum2.CheckedChanged, dolum3.CheckedChanged
        Dim KodKomanda As Integer
        Dim j As Integer
        KodKomanda = -1
        If indLoad Then
            If dolum0.Checked Then
                KodKomanda = 10511
            Else
                If dolum1.Checked Then KodKomanda = 10512
                If dolum2.Checked Then KodKomanda = 10513
                If dolum3.Checked Then KodKomanda = 10514
                If dolum4.Checked Then KodKomanda = 10515

            End If
            If KodKomanda < 0 Then
                MsgBox("Bele secim mumkun deyil")
            End If
            GonderModbus(0, KodKomanda)
            For i = 0 To 20000
                j = 0
            Next
            GonderModbus(0, KodKomanda)
        End If
    End Sub

    Private Sub CheckBox_Elek_CheckedChanged(sender As Object, e As EventArgs) Handles Eleksecim.CheckedChanged
        Dim KodKomanda As Integer

        If Eleksecim.Checked Then
            KodKomanda = 10560
        Else
            KodKomanda = 10561
        End If
        GonderModbus(0, KodKomanda)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim j As Integer
        GonderModbus(2, 10520)
        For i = 0 To 20000
            j = 0
        Next
        GonderModbus(2, 10520)
        IndButtonDolumStart = True
        TimerDolum.Enabled = True

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim j As Integer
        GonderModbus(2, 10521)
        For i = 0 To 20000
            j = 0
        Next
        GonderModbus(2, 10521)
        IndButtonDolumStop = True
        TimerDolum.Enabled = True
    End Sub

    Private Sub TimerDolum_Tick(sender As Object, e As EventArgs) Handles TimerDolum.Tick
        Dim j As Integer
        If IndButtonDolumStart Then
            For i = 0 To 20000
                j = 0
            Next
            GonderModbus(2, 10520)
            For i = 0 To 20000
                j = 0
            Next
            GonderModbus(2, 10520)
            IndButtonDolumStart = False

        End If
        If IndButtonDolumStop = True Then
            GonderModbus(2, 10521)
            For i = 0 To 2000
                j = 0
            Next
            GonderModbus(2, 10521)
            For i = 0 To 2000
                j = 0
            Next
            GonderModbus(2, 10521)
            IndButtonDolumStop = False
            TimerDolum.Enabled = False

        End If
    End Sub



    Private Sub boshalt0_CheckedChanged(sender As Object, e As EventArgs) Handles boshalt0.CheckedChanged, boshalt1.CheckedChanged, boshalt2.CheckedChanged, boshalt3.CheckedChanged, boshalt4.CheckedChanged
        Dim KodKomanda1 As Integer
        KodKomanda1 = -1
        If indLoad = True Then
            If boshalt0.Checked Then
                KodKomanda1 = 10610   ' 0
            Else
                If boshalt1.Checked Then KodKomanda1 = 10611
                If boshalt2.Checked Then KodKomanda1 = 10612
                If boshalt3.Checked Then KodKomanda1 = 10613
                If boshalt4.Checked Then KodKomanda1 = 10614
                ' If Boshalt5.Checked Then KodKomanda1 = 10615
                ' If Boshalt6.Checked Then KodKomanda1 = 10616
            End If
            If KodKomanda1 < 0 Then
                MsgBox("Belə seçim mümkün deyil")
                Exit Sub
            End If
            GonderModbus(0, KodKomanda1)
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim j As Integer
        GonderModbus(2, 10620)
        For i = 0 To 20000
            j = 0
        Next
        GonderModbus(2, 10620)
        IndButtonBoshaltStart = True
        TimerBoshalt.Enabled = True

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim j As Integer
        GonderModbus(2, 10621)
        For i = 0 To 20000
            j = 0

        Next
        GonderModbus(2, 10621)
        IndButtonBoshaltStop = True
        TimerBoshalt.Enabled = True
    End Sub

    Private Sub TimerBoshalt_Tick(sender As Object, e As EventArgs) Handles TimerBoshalt.Tick
        Dim j As Integer

        If IndButtonBoshaltStart Then
            GonderModbus(2, 10620)
        End If
        For i = 0 To 20000
            j = 0
        Next
        GonderModbus(2, 10620)
        IndButtonBoshaltStart = False
        If IndButtonBoshaltStop Then
            GonderModbus(2, 10621)
            For i = 0 To 20000
                j = 0
            Next
            GonderModbus(2, 10621)
            IndButtonBoshaltStop = False
            TimerBoshalt.Enabled = False

        End If
    End Sub

    Private Sub SS0_CheckedChanged(sender As Object, e As EventArgs) Handles SS0.CheckedChanged, SS10.CheckedChanged, S01.CheckedChanged, S02.CheckedChanged, S03.CheckedChanged, S11.CheckedChanged, S12.CheckedChanged, S13.CheckedChanged
        Dim j As Integer
        Dim Kodkomanda As Integer
        Kodkomanda = -1
        If indLoad Then
            If SS0.Checked Or SS10.Checked Then
                Kodkomanda = 10410
            Else
                If S01.Checked And S11.Checked Then Kodkomanda = 10411
                If S01.Checked And S12.Checked Then Kodkomanda = 10412
                If S01.Checked And S13.Checked Then Kodkomanda = 10413

                If S02.Checked And S11.Checked Then Kodkomanda = 10414
                If S02.Checked And S12.Checked Then Kodkomanda = 10415
                If S02.Checked And S13.Checked Then Kodkomanda = 10416

                If S03.Checked And S11.Checked Then Kodkomanda = 10417
                If S03.Checked And S12.Checked Then Kodkomanda = 10418
                If S03.Checked And S13.Checked Then Kodkomanda = 10419
            End If
        End If
        If Kodkomanda < 0 Then

            Exit Sub
        End If
        GonderModbus(0, Kodkomanda)
        For i = 0 To 2000
            j = 0
        Next
        GonderModbus(0, Kodkomanda)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim j As Integer
        GonderModbus(2, 10451)
        For i = 0 To 20000
            j = 0
        Next
        GonderModbus(2, 10451)
        IndButtonSSStart = True
        TimerSS.Enabled = True

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim j As Integer
        For i = 0 To 20000
            j = 0
        Next
        GonderModbus(2, 10452)
        IndButtonSSStop = True
        TimerSS.Enabled = True
    End Sub

    Private Sub TimerSS_Tick(sender As Object, e As EventArgs) Handles TimerSS.Tick
        Dim j As Integer
        If IndButtonSSStart Then
            GonderModbus(0, 10451)
            For i = 0 To 20000
                j = 0
            Next
            GonderModbus(0, 10451)
            IndButtonSSStart = False

        End If
        If IndButtonSSStop Then
            GonderModbus(2, 10452)
            For i = 0 To 20000
                j = 0

            Next
            GonderModbus(2, 10452)
            IndButtonSSStop = False
            TimerSS.Enabled = False

        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim j As Integer
        GonderModbus(0, 10500)
        For i = 0 To 20000
            j = 0
        Next
        GonderModbus(0, 2000)
        Button7.BackColor = Color.Green
        IndButtonReset = True
        TimerReset.Enabled = True
    End Sub

    Private Sub TimerReset_Tick(sender As Object, e As EventArgs) Handles TimerReset.Tick
        Dim j As Integer

        If IndButtonReset = True Then
            GonderModbus(0, 10500)
            For i = 0 To 20000
                j = 0
            Next
            GonderModbus(0, 10500)
            Button7.BackColor = Color.Red
            IndButtonReset = False
            TimerReset.Enabled = True
        End If
    End Sub



    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        DaxilOl1.ShowDialog()
    End Sub

    Private Sub M10CavabOrta_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub button8_Click(sender As Object, e As EventArgs) Handles button8.Click
        Dim A1, A2, B, B1 As Integer

        If RadioButton1.Checked Then
            If Not IsNumeric(vsi1.Text) And IsNumeric(saatvent.Text) Then
                MsgBox("Çalışma vaxtı ədəd olmalıdır")
                Exit Sub
            End If
            If Not IsNumeric(vsd1.Text) Then
                MsgBox("Dayanma vaxtı ədəd olmalıdır")
                Exit Sub
            End If
            B = Int(Val(saatvent.Text))
            A1 = Int(Val(vsi1.Text))
            A2 = Int(Val(vsd1.Text))
            B1 = Int(Val(dayanmasaat.Text))
            VentIshVaxtisaat(0) = B
            VentIshVaxti(0) = A1
            VentDayanmaVaxti(0) = A2
            ventdayanmavaxtisaat(0) = B1

            ventisvaxtiumumi(0) = VentIshVaxtisaat(0) * 60 + VentIshVaxti(0)
            ventdayanmavaxtiumumi(0) = ventdayanmavaxtisaat(0) * 60 + VentDayanmaVaxti(0)
            If ventisvaxtiumumi(0) > 0 Then
                VentRejim(0) = 1
                ' saatvent.Text = "1"
                GonderModbus(1, 10147)
                VentVaxt(0) = ventisvaxtiumumi(0)
                vaxtvent1.Text = VentVaxt(0).ToString
                Timervent1.Enabled = True
                vaxtvent1.BackColor = vsi1.BackColor
            End If
        Else
            GonderModbus(1, 10147)
            ' saatvent.Text = "1"
        End If
        'If RadioButton2.Checked = True Then
        '    Vent_Ishleyir(1) = True
        'End If

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim A1, A2 As Integer
        'If RadioButton1.Checked Then
        If Not IsNumeric(vsi1.Text) Then
            vsi1.Text = "0"
        End If
        If Not IsNumeric(vsd1.Text) Then
            vsd1.Text = "0"
        End If
        A1 = Int(Val(vsi1.Text))
        A2 = Int(Val(vsd1.Text))
        Timervent1.Enabled = False
        VentIshVaxti(0) = A1
        VentDayanmaVaxti(0) = A2
        VentRejim(0) = 0
        VentVaxt(0) = 0
        vaxtvent1.Text = VentVaxt(0).ToString
        ' saatvent.Text = "0"
        GonderModbus(1, 12147)
        'Else
        'GonderModbus(1, 12147)
        'End If

    End Sub

    Private Sub Timervent1_Tick(sender As Object, e As EventArgs) Handles Timervent1.Tick
        Dim j As Integer

        If VentVaxt(0) > 0 Then
            VentVaxt(0) = VentVaxt(0) - 1
            vaxtvent1.Text = VentVaxt(0).ToString
            If VentVaxt(0) > 0 Then
                Exit Sub
            End If
        End If
        If VentRejim(0) = 1 Then
            VentRejim(0) = 2
            If ventdayanmavaxtiumumi(0) > 0 Then
                GonderModbus(1, 12147)
                ' saatvent.Text = "0"
                For i = 0 To 20000
                    j = 0
                Next
                GonderModbus(1, 12147)
                'saatvent.Text = "0"
                For i = 0 To 20000
                    j = 0
                Next
                GonderModbus(1, 12147)
                ' saatvent.Text = "0"
                VentVaxt(0) = ventdayanmavaxtiumumi(0)
                vaxtvent1.Text = VentVaxt(0).ToString
                Timervent1.Enabled = True
                vaxtvent1.BackColor = vsd1.BackColor
            End If
        Else
            VentRejim(0) = 1
            If ventisvaxtiumumi(0) > 0 Then
                GonderModbus(1, 10147)
                ' saatvent.Text = "1"
                For i = 0 To 20000
                    j = 0
                Next
                GonderModbus(1, 10147)
                'saatvent.Text = "1"
                For i = 0 To 20000
                    j = 0
                Next
                GonderModbus(1, 10147)
                ' saatvent.Text = "1"
                VentVaxt(0) = ventisvaxtiumumi(0)
                vaxtvent1.Text = VentVaxt(0).ToString
                vaxtvent1.BackColor = vsi1.BackColor
                Timervent1.Enabled = True
            End If
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim A1, A2, B, B1 As Integer

        If RadioButton1.Checked Then
            If Not IsNumeric(vsi2.Text) And IsNumeric(saatvent2.Text) Then
                MsgBox("Çalışma vaxtı ədəd olmalıdır")
                Exit Sub
            End If
            If Not IsNumeric(vsd2.Text) Then
                MsgBox("Dayanma vaxtı ədəd olmalıdır")
                Exit Sub
            End If
            B = Int(Val(saatvent2.Text))
            A1 = Int(Val(vsi2.Text))
            A2 = Int(Val(vsd2.Text))
            B1 = Int(Val(dayanmasaat2.Text))
            VentIshVaxtisaat(2) = B
            VentIshVaxti(2) = A1
            VentDayanmaVaxti(2) = A2
            ventdayanmavaxtisaat(2) = B1

            ventisvaxtiumumi(2) = VentIshVaxtisaat(2) * 60 + VentIshVaxti(2)
            ventdayanmavaxtiumumi(2) = ventdayanmavaxtisaat(2) * 60 + VentDayanmaVaxti(2)
            If ventisvaxtiumumi(2) > 0 Then
                VentRejim(2) = 1
                ' saatvent.Text = "1"
                GonderModbus(1, 10148)
                VentVaxt(2) = ventisvaxtiumumi(2)
                vaxtvent2.Text = VentVaxt(2).ToString
                TimerVent2.Enabled = True
                vaxtvent2.BackColor = vsi2.BackColor
            End If
        Else
            GonderModbus(1, 10148)
            ' saatvent.Text = "1"
        End If
        Vent_Ishleyir(2) = True
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim A1, A2 As Integer
        'If RadioButton1.Checked Then
        If Not IsNumeric(vsi2.Text) Then
            vsi2.Text = "0"
        End If
        If Not IsNumeric(vsd2.Text) Then
            vsd2.Text = "0"
        End If
        A1 = Int(Val(vsi2.Text))
        A2 = Int(Val(vsd2.Text))
        TimerVent2.Enabled = False
        VentIshVaxti(2) = A1
        VentDayanmaVaxti(2) = A2
        VentRejim(2) = 0
        VentVaxt(2) = 0
        vaxtvent2.Text = VentVaxt(2).ToString
        ' saatvent.Text = "0"
        GonderModbus(1, 12148)
        'Else
        'GonderModbus(1, 12147)
        'End If

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim A1, A2 As Integer
        'If RadioButton1.Checked Then
        If Not IsNumeric(vsi3.Text) Then
            vsi3.Text = "0"
        End If
        If Not IsNumeric(vsd3.Text) Then
            vsd3.Text = "0"
        End If
        A1 = Int(Val(vsi3.Text))
        A2 = Int(Val(vsd3.Text))
        TimerVent3.Enabled = False
        VentIshVaxti(3) = A1
        VentDayanmaVaxti(3) = A2
        VentRejim(3) = 0
        VentVaxt(3) = 0
        Vaxtvent3.Text = VentVaxt(3).ToString
        ' saatvent.Text = "0"
        GonderModbus(1, 12149)
        'Else
        'GonderModbus(1, 12147)
        'End If

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim A1, A2, B, B1 As Integer

        If RadioButton1.Checked Then
            If Not IsNumeric(vsi3.Text) And IsNumeric(saatvent3.Text) Then
                MsgBox("Çalışma vaxtı ədəd olmalıdır")
                Exit Sub
            End If
            If Not IsNumeric(vsd3.Text) Then
                MsgBox("Dayanma vaxtı ədəd olmalıdır")
                Exit Sub
            End If
            B = Int(Val(saatvent3.Text))
            A1 = Int(Val(vsi3.Text))
            A2 = Int(Val(vsd3.Text))
            B1 = Int(Val(dayanmasaat3.Text))
            VentIshVaxtisaat(3) = B
            VentIshVaxti(3) = A1
            VentDayanmaVaxti(3) = A2
            ventdayanmavaxtisaat(3) = B1

            ventisvaxtiumumi(3) = VentIshVaxtisaat(3) * 60 + VentIshVaxti(3)
            ventdayanmavaxtiumumi(3) = ventdayanmavaxtisaat(3) * 60 + VentDayanmaVaxti(3)
            If ventisvaxtiumumi(3) > 0 Then
                VentRejim(3) = 1
                ' saatvent.Text = "1"
                GonderModbus(1, 10149)
                VentVaxt(3) = ventisvaxtiumumi(3)
                Vaxtvent3.Text = VentVaxt(3).ToString
                TimerVent3.Enabled = True
                Vaxtvent3.BackColor = vsi3.BackColor
            End If
        Else
            GonderModbus(1, 10148)
            ' saatvent.Text = "1"
        End If
        Vent_Ishleyir(3) = True
    End Sub

    Private Sub TimerVent2_Tick(sender As Object, e As EventArgs) Handles TimerVent2.Tick
        Dim j As Integer

        If VentVaxt(2) > 0 Then
            VentVaxt(2) = VentVaxt(2) - 1
            vaxtvent2.Text = VentVaxt(2).ToString
            If VentVaxt(2) > 0 Then
                Exit Sub
            End If
        End If
        If VentRejim(2) = 1 Then
            VentRejim(2) = 2
            If ventdayanmavaxtiumumi(2) > 0 Then
                GonderModbus(1, 12148)
                ' saatvent.Text = "0"
                For i = 0 To 20000
                    j = 0
                Next
                GonderModbus(1, 12148)
                'saatvent.Text = "0"
                For i = 0 To 20000
                    j = 0
                Next
                GonderModbus(1, 12148)
                ' saatvent.Text = "0"
                VentVaxt(2) = ventdayanmavaxtiumumi(2)
                vaxtvent2.Text = VentVaxt(2).ToString
                TimerVent2.Enabled = True
                vaxtvent2.BackColor = vsd2.BackColor
            End If
        Else
            VentRejim(2) = 1
            If ventisvaxtiumumi(2) > 0 Then
                GonderModbus(1, 10148)
                ' saatvent.Text = "1"
                For i = 0 To 20000
                    j = 0
                Next
                GonderModbus(1, 10148)
                'saatvent.Text = "1"
                For i = 0 To 20000
                    j = 0
                Next
                GonderModbus(1, 10148)
                ' saatvent.Text = "1"
                VentVaxt(2) = ventisvaxtiumumi(2)
                vaxtvent2.Text = VentVaxt(2).ToString
                vaxtvent2.BackColor = vsi2.BackColor
                TimerVent2.Enabled = True
            End If
        End If
    End Sub

    Private Sub TimerVent3_Tick(sender As Object, e As EventArgs) Handles TimerVent3.Tick
        Dim j As Integer

        If VentVaxt(3) > 0 Then
            VentVaxt(3) = VentVaxt(3) - 1
            Vaxtvent3.Text = VentVaxt(3).ToString
            If VentVaxt(3) > 0 Then
                Exit Sub
            End If
        End If
        If VentRejim(3) = 1 Then
            VentRejim(3) = 2
            If ventdayanmavaxtiumumi(3) > 0 Then
                GonderModbus(1, 12149)
                ' saatvent.Text = "0"
                For i = 0 To 20000
                    j = 0
                Next
                GonderModbus(1, 12149)
                'saatvent.Text = "0"
                For i = 0 To 20000
                    j = 0
                Next
                GonderModbus(1, 12149)
                ' saatvent.Text = "0"
                VentVaxt(3) = ventdayanmavaxtiumumi(3)
                Vaxtvent3.Text = VentVaxt(3).ToString
                TimerVent3.Enabled = True
                Vaxtvent3.BackColor = vsd3.BackColor
            End If
        Else
            VentRejim(3) = 1
            If ventisvaxtiumumi(3) > 0 Then
                GonderModbus(1, 10149)
                ' saatvent.Text = "1"
                For i = 0 To 20000
                    j = 0
                Next
                GonderModbus(1, 10149)
                'saatvent.Text = "1"
                For i = 0 To 20000
                    j = 0
                Next
                GonderModbus(1, 10149)
                ' saatvent.Text = "1"
                VentVaxt(3) = ventisvaxtiumumi(3)
                Vaxtvent3.Text = VentVaxt(3).ToString
                Vaxtvent3.BackColor = vsi3.BackColor
                TimerVent3.Enabled = True
            End If
        End If
    End Sub

    Private Sub TimerGun_Tick(sender As Object, e As EventArgs) Handles TimerGun.Tick
        'Dim carisaat, carideq As Integer
        'Dim vsaat, vdeq As Integer
        'If RadioButton2.Checked Then
        '    carisaat = Now.Hour
        '    carideq = Now.Minute

        '    'ventilyator11111111
        '    If Vent_Ishleyir(1) = True Then
        '        vsaat = Val(gis1.Text)
        '        vdeq = Val(gid1.Text)
        '        If carisaat = vsaat And carideq = vdeq Then
        '            GonderModbus(1, 10147)
        '        Else
        '            vsaat = Val(gds1.Text)
        '            vdeq = Val(gdd1.Text)
        '            If carisaat = vsaat And carideq = vdeq Then
        '                GonderModbus(1, 12147)
        '            End If

        '        End If
        '    End If
        'End If

    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            gosterici1.Visible = False
            gosterici2.Visible = False
            gosterici3.Visible = False
            saatvent.Visible = False
            saatvent2.Visible = False
            saatvent3.Visible = False
            dayanmasaat.Visible = False
            dayanmasaat2.Visible = False
            dayanmasaat3.Visible = False
            vsi1.Visible = False
            vsi2.Visible = False
            vsi3.Visible = False
            vsd1.Visible = False
            vsd2.Visible = False
            vsd3.Visible = False
            Label6.Visible = False
            Label8.Visible = False
            Label5.Visible = False
            Label7.Visible = False
            Label9.Visible = False
            Label10.Visible = False
            vaxtvent1.Visible = False
            vaxtvent2.Visible = False
            Vaxtvent3.Visible = False
        Else
            If RadioButton1.Checked = True Then
                gosterici1.Visible = True
                gosterici2.Visible = True
                gosterici3.Visible = True
                saatvent.Visible = True
                saatvent2.Visible = True
                saatvent3.Visible = True
                dayanmasaat.Visible = True
                dayanmasaat2.Visible = True
                dayanmasaat3.Visible = True
                vsi1.Visible = True
                vsi2.Visible = True
                vsi3.Visible = True
                vsd1.Visible = True
                vsd2.Visible = True
                vsd3.Visible = True
                Label6.Visible = True
                Label8.Visible = True
                Label5.Visible = True
                Label7.Visible = True
                Label9.Visible = True
                Label10.Visible = True
                vaxtvent1.Visible = True
                vaxtvent2.Visible = True
                Vaxtvent3.Visible = True
            End If



        End If
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        DaxilOl2.ShowDialog()
    End Sub



    Private Sub İşəSalmaqToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles İşəSalmaqToolStripMenuItem.Click
        UmumiKomanda(komandaqurgu, 1)
    End Sub

    Private Sub DayandırmaqToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DayandırmaqToolStripMenuItem.Click
        UmumiKomanda(komandaqurgu, 2)
    End Sub



    Private Sub Noriya1_Click_1(sender As Object, e As EventArgs) Handles Noriya1.Click, Noriya1Yuxari.Click, Noriya1asagi.Click, M4Noriya.Click
        komandaqurgu = 4
        Snekler.Show(MousePosition)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        Dim j As Integer
        If ComboBox1.SelectedIndex = 0 Then
            GonderModbus(1, 10700)
            For i = 0 To 20000
                j = 0
            Next
            GonderModbus(1, 10700)
            For i = 0 To 200000
                j = 0
            Next
            GonderModbus(1, 10700)
            'EMELIYYATLAR AVTOMATIK REJIM
            Snekler.Enabled = False
            Qapaqlar.Enabled = False
            Shalvarlar.Enabled = False
            elekler.Enabled = False

            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
            Button5.Enabled = True
            Button6.Enabled = True


        Else
            Snekler.Enabled = True
            GonderModbus(1, 10701)
            For i = 0 To 20000
                j = 0
            Next
            GonderModbus(1, 10701)
            For i = 0 To 20000
                j = 0
            Next
            GonderModbus(1, 10701)
            'EMELIYYATLAR MANUAL REJIM
            Snekler.Enabled = True
            Qapaqlar.Enabled = True '
            Shalvarlar.Enabled = True
            elekler.Enabled = True

            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False

        End If
    End Sub

    Private Sub Noriya2_Click_1(sender As Object, e As EventArgs) Handles Noriya2.Click, m10Noriya.Click
        komandaqurgu = 10
        Snekler.Show(MousePosition)
    End Sub

    Private Sub M2Snek_Click(sender As Object, e As EventArgs) Handles M2Snek.Click, M2Shnek.Click
        komandaqurgu = 2
        Snekler.Show(MousePosition)
    End Sub

    Private Sub m9snek_Click(sender As Object, e As EventArgs) Handles m9snek.Click, m9shnek.Click
        komandaqurgu = 9
        Snekler.Show(MousePosition)
    End Sub



    Private Sub m12snek_Click(sender As Object, e As EventArgs) Handles m12snek.Click, m12shnek.Click
        komandaqurgu = 12
        Snekler.Show(MousePosition)
    End Sub

    Private Sub Shnek14_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub m18snek_Click(sender As Object, e As EventArgs) Handles m18snek.Click, m18shnek.Click
        komandaqurgu = 18
        Snekler.Show(MousePosition)
    End Sub

    Private Sub SağSeçimToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SağSeçimToolStripMenuItem.Click
        UmumiKomanda(komandaqurgu, 1)
    End Sub

    Private Sub SolSeçimToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SolSeçimToolStripMenuItem.Click
        UmumiKomanda(komandaqurgu, 2)
    End Sub

    Private Sub m3Shalvar_Click(sender As Object, e As EventArgs) Handles m3Shalvar.Click
        komandaqurgu = 3
        Shalvarlar.Show(MousePosition)
    End Sub

    Private Sub M11Shalvar_Click(sender As Object, e As EventArgs) Handles M11Shalvar.Click
        komandaqurgu = 11
        Shalvarlar.Show(MousePosition)
    End Sub

    Private Sub AçmaqToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AçmaqToolStripMenuItem.Click
        UmumiKomanda(komandaqurgu, 1)
    End Sub

    Private Sub BaglamaqToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BaglamaqToolStripMenuItem.Click
        UmumiKomanda(komandaqurgu, 2)
    End Sub

    Private Sub İşəSalmaqToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles İşəSalmaqToolStripMenuItem1.Click
        UmumiKomanda(komandaqurgu, 1)

    End Sub

    Private Sub DayandirmaqToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DayandirmaqToolStripMenuItem.Click
        UmumiKomanda(komandaqurgu, 2)
    End Sub

    Private Sub M13qapaq_Click(sender As Object, e As EventArgs) Handles M13qapaq.Click
        komandaqurgu = 13
        Qapaqlar.Show(MousePosition)
    End Sub

    Private Sub M14Qapaq_Click(sender As Object, e As EventArgs) Handles M14Qapaq.Click
        komandaqurgu = 14
        Qapaqlar.Show(MousePosition)
    End Sub

    Private Sub M15Qapaq_Click(sender As Object, e As EventArgs) Handles M15Qapaq.Click
        komandaqurgu = 15
        Qapaqlar.Show(MousePosition)
    End Sub

    Private Sub M16Qapaq_Click(sender As Object, e As EventArgs) Handles M16Qapaq.Click
        komandaqurgu = 16
        Qapaqlar.Show(MousePosition)
    End Sub

    Private Sub M17Qapaq_Click(sender As Object, e As EventArgs) Handles M17Qapaq.Click
        komandaqurgu = 17
        Qapaqlar.Show(MousePosition)
    End Sub

    Private Sub M1Qapaq_Click(sender As Object, e As EventArgs) Handles M1Qapaq.Click
        komandaqurgu = 1
        Qapaqlar.Show(MousePosition)
    End Sub

    Private Sub m7Motor_Click(sender As Object, e As EventArgs) Handles m7Motor.Click
        komandaqurgu = 7
        elekler.Show(MousePosition)
    End Sub

    Private Sub M8motor_Click(sender As Object, e As EventArgs) Handles M8motor.Click
        komandaqurgu = 8
        elekler.Show(MousePosition)
    End Sub

    Private Sub m5Motor_Click(sender As Object, e As EventArgs) Handles m5Motor.Click
        komandaqurgu = 5
        elekler.Show(MousePosition)
    End Sub

    Private Sub M6motor_Click(sender As Object, e As EventArgs) Handles M6motor.Click
        komandaqurgu = 6
        elekler.Show(MousePosition)
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs)

    End Sub


End Class
