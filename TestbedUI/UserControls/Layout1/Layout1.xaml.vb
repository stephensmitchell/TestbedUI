Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports Microsoft.Web.WebView2.Core
Public Class Layout1
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        'InitializeAsync()
        Dim key = Environment.GetEnvironmentVariable("EyeshotV10WPF")
        mainViewportLayout.Unlock(key)
        ' Add any initialization after the InitializeComponent() call.
        'LeftLayoutWebview.s
    End Sub
    <STAThread>
    Private Async Sub InitializeAsync()
        Await LeftLayoutWebview.EnsureCoreWebView2Async(Nothing)
        Await LeftLayoutWebview.CoreWebView2.ExecuteScriptAsync("window.addEventListener('contextmenu', window => {window.preventDefault();});")
        AddHandler LeftLayoutWebview.CoreWebView2.WebMessageReceived, AddressOf WebView_CoreWebView2_WebMessageReceived
        'AddHandler webView.CoreWebView2
        Await LeftLayoutWebview.CoreWebView2.CallDevToolsProtocolMethodAsync("Log.enable", "{}")
        'Dim helper as DevToolsProtocolHelper()  = webView.CoreWebView2.GetDevToolsProtocolHelper();
        'Dim dd As DevToolsProtocolHelper = LeftLayoutWebview.CoreWebView2.GetDevToolsProtocolHelper
        Dim log As CoreWebView2DevToolsProtocolEventReceiver = LeftLayoutWebview.CoreWebView2.GetDevToolsProtocolEventReceiver("Log.LogEntry")
        LeftLayoutWebview.CoreWebView2.GetDevToolsProtocolEventReceiver("Log.entryAdded")
        LeftLayoutWebview.CoreWebView2.OpenDevToolsWindow()
    End Sub
    Private Async Sub InitializeAsync2()
        Dim options = New CoreWebView2EnvironmentOptions("--disable-web-security")
        Dim env = Await CoreWebView2Environment.CreateAsync(Nothing, Nothing, options)
        Await LeftLayoutWebview.EnsureCoreWebView2Async(env)
        AddHandler LeftLayoutWebview.CoreWebView2.WebMessageReceived, AddressOf WebView_CoreWebView2_WebMessageReceived
        Await LeftLayoutWebview.CoreWebView2.ExecuteScriptAsync("window.addEventListener('contextmenu', window => {window.preventDefault();});")
        Dim assemblyLocation As String = Assembly.GetExecutingAssembly().Location
        Dim directoryPath As String = IO.Path.GetDirectoryName(assemblyLocation)
        LeftLayoutWebview.CoreWebView2.Navigate(Path.Combine(directoryPath, "dist\index.html"))
        LeftLayoutWebview.CoreWebView2.OpenDevToolsWindow()
    End Sub
    Private Sub WebView_CoreWebView2_WebMessageReceived(sender As Object, e As CoreWebView2WebMessageReceivedEventArgs)
        Try
            Dim message = e.WebMessageAsJson
            Dim json = Newtonsoft.Json.Linq.JObject.Parse(message)
            Dim id = Convert.ToInt32(json("id"))
            Select Case id
                Case 1
                    Dim num1 = Convert.ToInt32(json("Input1"))
                    Dim num2 = Convert.ToInt32(json("Input2"))
                    Dim result = Add(num1, num2)
                    LeftLayoutWebview.CoreWebView2.PostWebMessageAsString(result.ToString())
                    System.Console.WriteLine("Result:::::::: " & result)
                Case 2
                    Dim num1a = Convert.ToInt32(json("Input3"))
                    Dim num2a = Convert.ToInt32(json("Input4"))
                    Dim result = Subtract(num1a, num2a)
                    LeftLayoutWebview.CoreWebView2.PostWebMessageAsString(result.ToString())
                    System.Console.WriteLine("Result:::::::: " & result)
            End Select
            '{"firstName":"John", "lastName":"Doe"}
            Dim message2 As String = e.WebMessageAsJson
            Dim json2 = Newtonsoft.Json.Linq.JObject.Parse(message2)
            ' Do something with the message, e.g., display it in a MessageBox
            MessageBox.Show($"Message received from WebView2: {message2}", "Message Received")
            'txtblock1.Text = message2
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Private Async Sub Layout1_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim options = New CoreWebView2EnvironmentOptions("--disable-web-security")
        Dim env = Await CoreWebView2Environment.CreateAsync(Nothing, Nothing, options)
        Await LeftLayoutWebview.EnsureCoreWebView2Async(env)
        AddHandler LeftLayoutWebview.CoreWebView2.WebMessageReceived, AddressOf WebView_CoreWebView2_WebMessageReceived
        Await LeftLayoutWebview.CoreWebView2.ExecuteScriptAsync("window.addEventListener('contextmenu', window => {window.preventDefault();});")
        Dim assemblyLocation As String = Assembly.GetExecutingAssembly().Location
        Dim directoryPath As String = IO.Path.GetDirectoryName(assemblyLocation)
        LeftLayoutWebview.CoreWebView2.Navigate(Path.Combine(directoryPath, "dist\index.html"))
        LeftLayoutWebview.CoreWebView2.OpenDevToolsWindow()
    End Sub
    Public Function Add(x, y) As Double
        Return x + y
    End Function
    Public Function Subtract(x, y) As Double
        Return x - y
    End Function
End Class