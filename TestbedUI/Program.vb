Imports System.Threading
Imports System.Windows
Imports System.Windows.Media
Imports Syncfusion.SfSkinManager
Module Program
    Dim app As Application
    Dim grid As System.Windows.Controls.Grid
    Dim window As Window
    <STAThread>
    Public Sub Main(args As String())
        If args.Length > 0 Then
            ProcessCommand(args(0).ToLower())
        Else
            Console.WriteLine("Enter command ('/h' for list of commands):")
            Dim keepRunning As Boolean = True
            While keepRunning
                Dim command As String = Console.ReadLine().ToLower()
                ProcessCommand(command)
            End While
        End If
    End Sub
    Private Sub ProcessCommand(command As String)
        Select Case command
            Case "u1"
                StartUIWindow("TestbedUI Design 01 - DEV MODE", AddressOf WindowLayout1.BuildGrid2)
            Case "u2"
                StartUIWindow("TestbedUI Design 02 - DEV MODE", Function() New Layout1())
            Case "u3"
                StartUIWindow("TestbedUI Design 03 - DEV MODE", Function() New Layout2())
            Case "u4"
                StartUIWindow("TestbedUI Design 04 - DEV MODE", Function() New Layout3())
            Case "u5"
                StartUIWindow("TestbedUI Design 05 - DEV MODE", AddressOf WindowLayout.BuildGrid)
            Case "/r" 'TODO: Add restart functionality
                Console.WriteLine("Restarting the application...")
            Case "/c"
                LoadCalca()
                RunNotepadAndWaitAsync()
                RunNotepadAndSendTextAsync()
            Case "/h"
                ShowHelp()
            Case "/e"
                Environment.Exit(0)
            Case Else
                Console.WriteLine("Invalid command. Type '/h' to see the list of valid commands.")
        End Select
    End Sub
    Private Sub StartUIWindow(WindowTitleDesignName As String, layoutAction As Action(Of System.Windows.Controls.Grid))
        Console.WriteLine($"{WindowTitleDesignName} is ready...")
        Console.WriteLine($"{WindowTitleDesignName} WindowState = WindowState.Minimized")
        Dim thread = New Thread(Sub()
                                    SfSkinManager.ApplyStylesOnApplication = True
                                    app = New Application()
                                    window = New Window With {
                                        .Title = WindowTitleDesignName,
                                        .Width = 1920,
                                        .Height = 1080,
                                        .WindowStartupLocation = WindowStartupLocation.CenterScreen,
                                        .WindowState = WindowState.Minimized,
                                        .WindowStyle = WindowStyle.SingleBorderWindow,
                                        .Background = Brushes.Black
                                    }
                                    AddHandler window.Closed, AddressOf Window_Closed
                                    grid = New System.Windows.Controls.Grid()
                                    layoutAction(grid)
                                    window.Content = grid
                                    app.Run(window)
                                End Sub)
        thread.SetApartmentState(ApartmentState.STA)
        thread.Start()
    End Sub
    Private Sub StartUIWindow(WindowTitleDesignName As String, layoutControl As Func(Of System.Windows.Controls.UserControl))
        Console.WriteLine($"{WindowTitleDesignName} is ready...")
        Console.WriteLine($"{WindowTitleDesignName} WindowState = WindowState.Minimized")
        Dim thread = New Thread(Sub()
                                    SfSkinManager.ApplyStylesOnApplication = True
                                    app = New Application()
                                    window = New Window With {
                                        .Title = WindowTitleDesignName,
                                        .Width = 1920,
                                        .Height = 1080,
                                        .WindowStartupLocation = WindowStartupLocation.CenterScreen,
                                        .WindowState = WindowState.Minimized,
                                        .WindowStyle = WindowStyle.SingleBorderWindow,
                                        .Background = Brushes.Black
                                    }
                                    AddHandler window.Closed, AddressOf Window_Closed
                                    Dim newWebView = layoutControl()
                                    window.Content = newWebView
                                    app.Run(window)
                                End Sub)
        thread.SetApartmentState(ApartmentState.STA)
        thread.Start()
    End Sub
    Private Sub ShowHelp()
        Console.WriteLine("Available commands:")
        Console.WriteLine("  u1 - UI/Window Layout")
        Console.WriteLine("  u2 - UI/Window Layout")
        Console.WriteLine("  u3 - UI/Window Layout")
        Console.WriteLine("  u4 - UI/Window Layout")
        Console.WriteLine("  u5 - UI/Window Layout")
        Console.WriteLine("  /r - Restarts the application (not implemented)")
        Console.WriteLine("  /c - Macros")
        Console.WriteLine("  /h - Shows this help message")
        Console.WriteLine("  /e - Kills the application")
    End Sub
    Private Sub Window_Closed(sender As Object, e As EventArgs)
        If Application.Current.Windows.Count = 0 Then
            Application.Current.Shutdown()
            Environment.Exit(0)
        End If
    End Sub
End Module
