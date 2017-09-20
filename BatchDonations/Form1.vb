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
End Class
