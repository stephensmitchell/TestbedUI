﻿Imports System.Windows.Controls
Imports System.Threading
Imports Syncfusion.SfSkinManager
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports devDept.Eyeshot
Imports devDept.Eyeshot.Entities
Imports devDept.Geometry
Imports devDept.Graphics
Imports devDept.Eyeshot.Translators
Public Module WindowLayout
    Sub BuildGrid(grid As System.Windows.Controls.Grid)
        SfSkinManager.SetTheme(grid, New Theme("Windows11Dark"))
        grid.Background = Brushes.Black
        grid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(100)})
        grid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(5)})
        grid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
        grid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(5)})
        grid.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(100)})
        grid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(3, GridUnitType.Star)})
        grid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(5)})
        grid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(2, GridUnitType.Star)})
        grid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(5)})
        grid.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(5, GridUnitType.Star)})
        Dim headerControl = New UserControl()
        headerControl.Content = New Label With {.Content = "HEADER AREA", .HorizontalAlignment = HorizontalAlignment.Center}
        Grid.SetRow(headerControl, 0)
        Grid.SetColumnSpan(headerControl, 5)
        grid.Children.Add(headerControl)
        Dim gridSplitterTop = New GridSplitter With {
            .Height = 5,
            .ResizeDirection = GridResizeDirection.Rows,
            .HorizontalAlignment = HorizontalAlignment.Stretch
}
        Grid.SetRow(gridSplitterTop, 1)
        Grid.SetColumnSpan(gridSplitterTop, 5)
        grid.Children.Add(gridSplitterTop)
        Dim listBox = CreateListBox2()
        Grid.SetRow(listBox, 2)
        Grid.SetColumn(listBox, 0)
        grid.Children.Add(listBox)
        Dim gridSplitter1 = New GridSplitter With {
            .Width = 5,
            .ResizeBehavior = GridResizeBehavior.PreviousAndNext,
            .HorizontalAlignment = HorizontalAlignment.Stretch
}
        Grid.SetRow(gridSplitter1, 2)
        Grid.SetColumn(gridSplitter1, 1)
        grid.Children.Add(gridSplitter1)
        Dim treeViewUserControl = New TreeViewUserControl()
        Grid.SetRow(treeViewUserControl, 2)
        Grid.SetColumn(treeViewUserControl, 2)
        grid.Children.Add(treeViewUserControl)
        Dim gridSplitter2 = New GridSplitter With {
            .Width = 5,
            .ResizeBehavior = GridResizeBehavior.PreviousAndNext,
            .HorizontalAlignment = HorizontalAlignment.Stretch
        }
        Grid.SetRow(gridSplitter2, 2)
        Grid.SetColumn(gridSplitter2, 3)
        grid.Children.Add(gridSplitter2)
        Dim eyeShotUserControl = New EyeShotUserControl()
        Grid.SetRow(eyeShotUserControl, 2)
        Grid.SetColumn(eyeShotUserControl, 4)
        grid.Children.Add(eyeShotUserControl)
        Dim gridSplitterBottom = New GridSplitter With {
            .Height = 5,
            .ResizeDirection = GridResizeDirection.Rows,
            .HorizontalAlignment = HorizontalAlignment.Stretch
        }
        Grid.SetRow(gridSplitterBottom, 3)
        Grid.SetColumnSpan(gridSplitterBottom, 5)
        grid.Children.Add(gridSplitterBottom)
        Dim footerControl = New UserControl()
        footerControl.Content = New Label With {.Content = "STATUS AREA", .HorizontalAlignment = HorizontalAlignment.Center}
        Grid.SetRow(footerControl, 4)
        Grid.SetColumnSpan(footerControl, 5)
        grid.Children.Add(footerControl)
    End Sub
    Function CreateListBox2() As ListBox
        Dim listBox = New ListBox With {
            .DisplayMemberPath = "Text",
            .Margin = New Thickness(10)
        }
        AddHandler listBox.SelectionChanged, AddressOf OnListBoxSelectionChanged
        LoadItemsAsync(listBox)
        Return listBox
    End Function
    Private Sub OnListBoxSelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim listBox As ListBox = DirectCast(sender, ListBox)
        Dim selectedItem As ListBoxItem = TryCast(listBox.SelectedItem, ListBoxItem)
        Dim detailUserControl = DirectCast(DirectCast(listBox.Parent, System.Windows.Controls.Grid).Children.OfType(Of DetailUserControl)().FirstOrDefault(), DetailUserControl)
        If detailUserControl IsNot Nothing Then
            detailUserControl.DataContext = selectedItem.Content
            detailUserControl.UpdateDetails()
        End If
        Dim treeViewUserControl = DirectCast(DirectCast(listBox.Parent, System.Windows.Controls.Grid).Children.OfType(Of TreeViewUserControl)().FirstOrDefault(), TreeViewUserControl)
        If treeViewUserControl IsNot Nothing AndAlso TypeOf selectedItem.Content Is StackPanel Then
            Dim stackPanel = DirectCast(selectedItem.Content, StackPanel)
            Dim model = TryCast(stackPanel.DataContext, ItemViewModel)
            If model IsNot Nothing Then
                treeViewUserControl.UpdateTreeView(model)
            End If
        End If
    End Sub
    'TODO: Implement a more realistic data loading mechanism
    Async Function LoadItemsAsync(listBox As ListBox) As Task
        Await Task.Delay(1000)
        Dim items = Enumerable.Range(1, 10).Select(Function(i) New ItemViewModel With {.Text = $"Item {i}", .ImagePath = $"C:\Users\steph\Desktop\logo_{i Mod 2}.png"}).ToList()
        listBox.Dispatcher.Invoke(Sub() PopulateListBox(items, listBox))
    End Function
    Private Sub PopulateListBox(items As List(Of ItemViewModel), listBox As ListBox)
        For Each item In items
            Dim stackPanel = New StackPanel With {
                .Orientation = Orientation.Horizontal,
                .DataContext = item
            }
            Dim image = New System.Windows.Controls.Image With {
                .Source = New BitmapImage(New Uri(item.ImagePath, UriKind.Absolute)),
                .Width = 50,
                .Height = 50
            }
            Dim textBlock = New TextBlock With {
                .Text = item.Text,
                .VerticalAlignment = VerticalAlignment.Center
            }
            stackPanel.Children.Add(image)
            stackPanel.Children.Add(textBlock)
            listBox.Items.Add(New ListBoxItem With {.Content = stackPanel})
        Next
    End Sub
    Public Class ItemViewModel
        Public Property Text As String
        Public Property ImagePath As String
    End Class
    Public Class DetailUserControl
        Inherits UserControl
        Public Sub New()
            AddHandler Me.Loaded, AddressOf OnDetailUserControlLoaded
        End Sub
        Private Sub OnDetailUserControlLoaded(sender As Object, e As RoutedEventArgs)
            UpdateDetails()
        End Sub
        Public Sub UpdateDetails()
            Me.Content = New Label With {
                .Content = "Details loading...",
                .Margin = New Thickness(20)
            }
            Task.Delay(500).ContinueWith(Sub(t) UpdateDetailsContinuation())
        End Sub
        Private Sub UpdateDetailsContinuation()
            Dispatcher.Invoke(Sub() DisplayDetailContent())
        End Sub
        Private Sub DisplayDetailContent()
            If TypeOf DataContext Is StackPanel Then
                Dim panel = DirectCast(DataContext, StackPanel)
                Dim text = DirectCast(panel.Children(1), TextBlock).Text
                DirectCast(Me.Content, Label).Content = $"Details loaded for {text}!"
            Else
                DirectCast(Me.Content, Label).Content = "No item selected."
            End If
        End Sub
    End Class
    Public Class TreeViewUserControl
        Inherits UserControl
        Private treeView As TreeView
        Public Sub New()
            InitializeTreeView()
        End Sub
        Private Sub InitializeTreeView()
            treeView = New TreeView()
            Me.Content = treeView
        End Sub
        Public Sub UpdateTreeView(selectedItem As ItemViewModel)
            treeView.Items.Clear()
            If selectedItem IsNot Nothing Then
                Dim rootNode = New TreeViewItem With {
                    .Header = $"Details for {selectedItem.Text}"
                }
                Dim childNode1 = New TreeViewItem With {
                    .Header = $"{selectedItem.Text} - Child 1"
                }
                Dim childNode2 = New TreeViewItem With {
                    .Header = $"{selectedItem.Text} - Child 2"
                }
                rootNode.Items.Add(childNode1)
                rootNode.Items.Add(childNode2)
                treeView.Items.Add(rootNode)
                rootNode.IsExpanded = True
            End If
        End Sub
    End Class
    Public Class EyeShotUserControl
        Inherits UserControl
        Friend WithEvents ViewportLayout1 As New ViewportLayout
        Public Sub New()
            AddHandler ViewportLayout1.InitializeScene, AddressOf OnInitializeScene
            ViewportLayout1 = New ViewportLayout()
            Dim key = Environment.GetEnvironmentVariable("EyeshotV10WPF")
            ViewportLayout1.Unlock(key)
            ViewportLayout1.InitializeViewports()
            ViewportLayout1.Viewports(0).Grids.Add(New devDept.Eyeshot.Grid())
            ViewportLayout1.ShowFps = False
            ViewportLayout1.DisplayMode = displayType.Shaded
            ViewportLayout1.GetBackground().TopColor = Brushes.Black
            ViewportLayout1.GetBackground().BottomColor = Brushes.DarkGray
            ViewportLayout1.GetBackground().StyleMode = backgroundStyleType.Solid
            ViewportLayout1.Camera = New devDept.Eyeshot.Camera()
            ViewportLayout1.Camera.Distance = 600
            ViewportLayout1.Camera.ProjectionMode = projectionType.Orthographic
            ViewportLayout1.Camera.Rotation = New devDept.Geometry.Quaternion(0.0868240888334652, 0.150383733180435, 0.492403876506104, 0.852868531952443)
            ViewportLayout1.Camera.Target = New devDept.Geometry.Point3D(0, 0, 50)
            ViewportLayout1.Camera.ZoomFactor = 2
            ViewportLayout1.ShowVertices = False
            ViewportLayout1.AllowDrop = True
            Dim toolbar As New devDept.Eyeshot.ToolBar()
            toolbar.Buttons.Add(New HomeToolBarButton())
            toolbar.Buttons.Add(New ZoomWindowToolBarButton())
            toolbar.Buttons.Add(New ZoomToolBarButton())
            toolbar.Buttons.Add(New PanToolBarButton())
            toolbar.Buttons.Add(New RotateToolBarButton())
            toolbar.Buttons.Add(New ZoomFitToolBarButton())
            ViewportLayout1.HorizontalAlignment = HorizontalAlignment.Stretch
            ViewportLayout1.VerticalAlignment = VerticalAlignment.Stretch
            ViewportLayout1.GetToolBars.Add(toolbar)
            TestModel()
            AdditionalSetup()
            Dim ro As New ReadSTEP("D:\Desktop\05-02-2024\test-model-2 - Copy.stp")
            ro.DoWork()
            ro.AddToScene(ViewportLayout1)
            ViewportLayout1.Invalidate()
            Me.Content = ViewportLayout1
        End Sub
        Sub TestModel()
        End Sub
        Sub AdditionalSetup()
        End Sub
        Sub OnInitializeScene(sender As Object, e As EventArgs)
        End Sub
        Private Sub LoadSTEPFile(filePath As String)
            ViewportLayout1.Clear()
            Dim ro As New ReadSTEP(filePath)
            ro.DoWork()
            ro.AddToScene(ViewportLayout1)
            ViewportLayout1.Invalidate()
        End Sub
        Private Sub EyeShotUserControl_Drop(sender As Object, e As DragEventArgs) Handles Me.Drop
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                Dim files As String() = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
                If files IsNot Nothing AndAlso files.Length > 0 Then
                    LoadSTEPFile(files(0))  ' Assume the first file is the STEP file
                    e.Handled = True
                End If
            End If
        End Sub
        Private Sub EyeShotUserControl_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                e.Effects = DragDropEffects.Copy
            Else
                e.Effects = DragDropEffects.None
            End If
            e.Handled = True
        End Sub
    End Class
End Module