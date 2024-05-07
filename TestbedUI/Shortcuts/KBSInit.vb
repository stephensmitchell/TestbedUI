Imports System.Threading
Imports AutoIt
Public Module KBSInit
    Public Sub LoadCalca()
        Task.Run(Sub()
                     'Thread.Sleep(TimeSpan.FromSeconds(10))
                     AutoItX.AutoItSetOption("WinTitleMatchMode", 2)
                     Dim a = AutoItX.WinGetHandle("TestbedUI Design", "")
                     AutoItX.WinActivate(a)
                     'MsgBox("Hello from AutoItX!")
                 End Sub)
    End Sub
    Public Sub RunNotepadAndWaitAsync()
        Task.Run(Sub()
                     Thread.Sleep(TimeSpan.FromSeconds(10))
                     'MsgBox("Hello from AutoItX!")
                 End Sub)
    End Sub
    Public Sub RunNotepadAndSendTextAsync()
        Task.Run(Sub()
                     Thread.Sleep(TimeSpan.FromSeconds(10))
                     ' MsgBox("Hello from AutoItX!")
                 End Sub)
    End Sub
End Module