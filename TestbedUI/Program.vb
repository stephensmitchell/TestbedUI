Imports System.Threading
Imports System.Windows
Imports System.Windows.Media
Imports Syncfusion.SfSkinManager
Module Program
    Dim app As Application
    Dim grid As System.Windows.Controls.Grid
    Dim window As Window
    <STAThread>
    Public Sub Main()
        'TODO: Add mini logger
        Dim keepRunning As Boolean = True
        Console.WriteLine("Enter command ('/h' for list of commands):")
        While keepRunning
            Dim command As String = Console.ReadLine().ToLower()
            Select Case command
                Case "u1"
                    Dim WindowTitleDesignName As String = "TestbedUI Design 01 - DEV MODE"
                    Console.WriteLine(WindowTitleDesignName & " is ready...")
                    Console.WriteLine(WindowTitleDesignName & " WindowState = WindowState.Minimized")
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
                                                WindowLayout1.BuildGrid2(grid)
                                                window.Content = grid
                                                app.Run(window)
                                            End Sub)
                    thread.SetApartmentState(ApartmentState.STA)
                    thread.Start()
                Case "u2"
                    Dim WindowTitleDesignName As String = "TestbedUI Design 02 - DEV MODE"
                    Console.WriteLine(WindowTitleDesignName & " is ready...")
                    Console.WriteLine(WindowTitleDesignName & " WindowState = WindowState.Minimized")
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
                                                Dim newWebView As New Layout1()
                                                ' grid.Children.Add(newWebView)
                                                window.Content = newWebView
                                                app.Run(window)
                                            End Sub)
                    thread.SetApartmentState(ApartmentState.STA)
                    thread.Start()
                Case "u3"
                    Dim WindowTitleDesignName As String = "TestbedUI Design 03 - DEV MODE"
                    Console.WriteLine(WindowTitleDesignName & " is ready...")
                    Console.WriteLine(WindowTitleDesignName & " WindowState = WindowState.Minimized")
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
                                                Dim newWebView As New Layout2()
                                                ' grid.Children.Add(newWebView)
                                                window.Content = newWebView
                                                app.Run(window)
                                            End Sub)
                    thread.SetApartmentState(ApartmentState.STA)
                    thread.Start()
                Case "u4"
                    Dim WindowTitleDesignName As String = "TestbedUI Design 04 - DEV MODE"
                    Console.WriteLine(WindowTitleDesignName & " is ready...")
                    Console.WriteLine(WindowTitleDesignName & " WindowState = WindowState.Minimized")
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
                                                Dim newWebView As New Layout3()
                                                ' grid.Children.Add(newWebView)
                                                window.Content = newWebView
                                                app.Run(window)
                                            End Sub)
                    thread.SetApartmentState(ApartmentState.STA)
                    thread.Start()
                Case "u5"
                    Dim WindowTitleDesignName As String = "TestbedUI Design 05 - DEV MODE"
                    Console.WriteLine(WindowTitleDesignName & " is ready...")
                    Console.WriteLine(WindowTitleDesignName & " WindowState = WindowState.Minimized")
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
                                                WindowLayout.BuildGrid(grid)
                                                window.Content = grid
                                                app.Run(window)
                                            End Sub)
                    thread.SetApartmentState(ApartmentState.STA)
                    thread.Start()
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
                    Console.WriteLine("Invalid command. Type 'help' to see the list of valid commands.")
            End Select
        End While
    End Sub
    Private Sub ShowHelp()
        ' This function shows the help instructions.
        Console.WriteLine("Available commands:")
        Console.WriteLine("  u1 - UI/Window Layout")
        Console.WriteLine("  u2 - UI/Window Layout")
        Console.WriteLine("  u3 - UI/Window Layout")
        Console.WriteLine("  u4 - UI/Window Layout")
        Console.WriteLine("  /r - Restarts the application (not implemented)")
        Console.WriteLine("  /c - Macros")
        Console.WriteLine("  /h    - Shows this help message")
        Console.WriteLine("  /e    - Kills the application")
    End Sub
    Private Sub Window_Closed(sender As Object, e As EventArgs)
        ' Check if there are no more windows open, then close the app
        If Application.Current.Windows.Count = 0 Then
            Application.Current.Shutdown()
            Environment.Exit(0)
        End If
    End Sub
End Module