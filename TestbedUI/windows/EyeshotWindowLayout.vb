Imports System.Windows.Controls
Imports System.Threading
Imports Syncfusion.SfSkinManager
Imports System.Windows
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports devDept.Eyeshot
Imports devDept.Eyeshot.Entities
Imports devDept.Geometry
Imports devDept.Graphics
Public Module EyeshotWindowLayout
    Sub BuildGrid2(grid As System.Windows.Controls.Grid)
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
        grid.SetRow(headerControl, 0)
        grid.SetColumnSpan(headerControl, 5)
        grid.Children.Add(headerControl)
        Dim gridSplitterTop = New GridSplitter With {
            .Height = 5,
            .ResizeDirection = GridResizeDirection.Rows,
            .HorizontalAlignment = HorizontalAlignment.Stretch
}
        grid.SetRow(gridSplitterTop, 1)
        grid.SetColumnSpan(gridSplitterTop, 5)
        grid.Children.Add(gridSplitterTop)
        Dim listBox = CreateListBox2()
        grid.SetRow(listBox, 2)
        grid.SetColumn(listBox, 0)
        grid.Children.Add(listBox)
        Dim gridSplitter1 = New GridSplitter With {
            .Width = 5,
            .ResizeBehavior = GridResizeBehavior.PreviousAndNext,
            .HorizontalAlignment = HorizontalAlignment.Stretch
}
        grid.SetRow(gridSplitter1, 2)
        grid.SetColumn(gridSplitter1, 1)
        grid.Children.Add(gridSplitter1)
        Dim treeViewUserControl = New TreeViewUserControl()
        grid.SetRow(treeViewUserControl, 2)
        grid.SetColumn(treeViewUserControl, 2)
        grid.Children.Add(treeViewUserControl)
        Dim gridSplitter2 = New GridSplitter With {
            .Width = 5,
            .ResizeBehavior = GridResizeBehavior.PreviousAndNext,
            .HorizontalAlignment = HorizontalAlignment.Stretch
        }
        grid.SetRow(gridSplitter2, 2)
        grid.SetColumn(gridSplitter2, 3)
        grid.Children.Add(gridSplitter2)
        Dim eyeShotUserControl = New EyeShotUserControl()
        grid.SetRow(eyeShotUserControl, 2)
        grid.SetColumn(eyeShotUserControl, 4)
        grid.Children.Add(eyeShotUserControl)
        Dim gridSplitterBottom = New GridSplitter With {
            .Height = 5,
            .ResizeDirection = GridResizeDirection.Rows,
            .HorizontalAlignment = HorizontalAlignment.Stretch
        }
        grid.SetRow(gridSplitterBottom, 3)
        grid.SetColumnSpan(gridSplitterBottom, 5)
        grid.Children.Add(gridSplitterBottom)
        Dim footerControl = New UserControl()
        footerControl.Content = New Label With {.Content = "STATUS AREA", .HorizontalAlignment = HorizontalAlignment.Center}
        grid.SetRow(footerControl, 4)
        grid.SetColumnSpan(footerControl, 5)
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
        '"C:\Users\steph\Desktop\logo_0.png"
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
        Public ViewportLayout1 As ViewportLayout
        Public Sub New()
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
            Me.Content = ViewportLayout1
        End Sub
        Sub TestModel()
            Dim hr1 As HexagonalRegion = New HexagonalRegion(Plane.XY, 27.7128129)
            hr1.Rotate(Utility.DegToRad(30), devDept.Geometry.Vector3D.AxisZ, devDept.Geometry.Point3D.Origin)
            Dim ext1 As Solid3D = hr1.ExtrudeAsSolid3D(50)
            Dim lp1 As LinearPath = New LinearPath(Plane.XZ, New Point2D(-82, 0), New Point2D(0, 0), New Point2D(0, 30), New Point2D(-62, 30), New Point2D(-82, 0))
            Dim r1 As devDept.Eyeshot.Entities.Region = New devDept.Eyeshot.Entities.Region(lp1, Plane.XZ, False)
            ext1.ExtrudeAdd(r1, New Interval(-24, 24))
            Dim cr1 As CircularRegion = New CircularRegion(Plane.XY, 8)
            ViewportLayout1.Entities.Add(cr1, 0, System.Drawing.Color.Red)
            ext1.ExtrudeRemove(cr1, 50)
            Dim cr2 As CircularRegion = New CircularRegion(New Plane(New devDept.Geometry.Point3D(0, 0, 10), devDept.Geometry.Vector3D.AxisX, devDept.Geometry.Vector3D.AxisY), 20)
            ext1.ExtrudeRemove(cr2, 50)
            Dim rr2 As RectangularRegion = New RectangularRegion(Plane.XZ, -80, 10, 38, 30)
            ext1.ExtrudeRemove(rr2, New Interval(-12, 12))
            ViewportLayout1.Entities.Add(ext1, 0, System.Drawing.Color.Aqua)
        End Sub
        Sub AdditionalSetup()
        End Sub
    End Class
End Module
