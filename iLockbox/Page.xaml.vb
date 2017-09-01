Imports iLockbox.ServiceReference1
Imports System.Data.Services.Client
Imports C1.Silverlight
Imports System.ComponentModel
Imports System.Globalization

Partial Public Class Page
    Inherits UserControl



    Private ServiceURI As Uri





    Public Sub New()
        InitializeComponent()
        'FillHyperPanel(_hp1)

        'Dim ilockbox
        ' _WorkSrc.ItemsSource = clsWS.GetWorkSources
        ''
    End Sub

    Private Sub book_CurrentPageChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _book.CurrentPageChanged

    End Sub

    Private Sub book_CurrentZoneChanged(ByVal sender As Object, ByVal e As C1.Silverlight.PropertyChangedEventArgs(Of C1.Silverlight.Extended.BookZone)) Handles _book.CurrentZoneChanged

    End Sub

    Private Sub book_DragPageFinished(ByVal sender As Object, ByVal e As C1.Silverlight.Extended.BookDragPageFinishedEventArgs) Handles _book.DragPageFinished

    End Sub

    Private Sub book_DragPageStarted(ByVal sender As Object, ByVal e As System.EventArgs) Handles _book.DragPageStarted

    End Sub

    Private Sub book_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles _book.MouseMove

    End Sub

    'Private Sub book_SizeChanged(ByVal sender As Object, ByVal e As System.Windows.SizeChangedEventArgs) Handles book.SizeChanged
    '    book.SetValue(Canvas.WidthProperty, LayoutRoot.ActualWidth / 2)
    '    book.SetValue(Canvas.LeftProperty, (LayoutRoot.ActualWidth - book.ActualWidth) / 2)
    'End Sub

    'Public Class Employee

    '        public Name (get, set) as string
    '        public string Phone { get; set; }
    '        public string JobTitle { get; set; }
    '        public string BirthDate { get; set; }
    '        public string Email { get; set; }
    '        public string City { get; set; }
    '        public string AddressLine { get; set; }
    '        public string HireDate { get; set; }
    '        public string Photo { get; set; }

    'End Class

    'Private Sub Page_SizeChanged(ByVal sender As Object, ByVal e As System.Windows.SizeChangedEventArgs) Handles Me.SizeChanged
    '    book.SetValue(ActualWidthProperty, LayoutRoot.ActualWidth - book.Margin.Left - book.Margin.Right)
    '    'book.SetValue(Leftroperty, (LayoutRoot.ActualWidth - book.ActualWidth) / 2)
    'End Sub
    'Public Sub FillHyperPanel(ByVal hp As C1HyperPanel)
    '    hp.Children.Clear()
    '    Dim ix As Int32 = 0
    '    For p As Double = 0 To 1 Step 0.05

    '        ' create Color
    '        Dim red As Int32 = CInt(255 * p)
    '        Dim green As Int32 = (red + 255) / 3 Mod 255
    '        Dim blue As Int32 = (green + 255) / 3 Mod 255
    '        Dim c As Color = Color.FromArgb(255, red, green, blue)

    '        ' create Rectangle element
    '        Dim r As Button = New Button()
    '        'r.Width = 30
    '        'r.Height = 30
    '        r.Background = New SolidColorBrush(c)
    '        r.Margin = New Thickness(4)
    '        r.BorderBrush = New SolidColorBrush(Colors.Black)
    '        r.BorderThickness = New Thickness(2)
    '        r.VerticalAlignment = VerticalAlignment.Center
    '        r.FontSize = 30
    '        'r.Stretch = Stretch.UniformToFill
    '        'add rectangle to C1HyperPanel
    '        ix += 1
    '        r.Content = ix.ToString("D3")
    '        hp.Children.Add(r)
    '    Next
    '    hp.Center = 100
    '    hp.InvalidateArrange()
    '    hp.UpdateLayout()

    'End Sub


    Private Sub Page_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        ServiceURI = New Uri("/iLockboxDataService.svc/", UriKind.Relative)
        Dim ctx As New ilockboxEntities(ServiceURI)
        Dim uriRel As New Uri("GetActiveWorkSources", UriKind.Relative)

        Dim callback As AsyncCallback = AddressOf GetActiveWorkSourcesComplete
        ctx.BeginExecute(Of Wrksrctbl)(uriRel, callback, ctx)

    End Sub
    Private Sub GetActiveWorkSourcesComplete(ByVal ar As IAsyncResult)

        Dim ctx As DataServiceContext = CType(ar.AsyncState, DataServiceContext)

        _WorkSrc.ItemsSource = ctx.EndExecute(Of Wrksrctbl)(ar)

    End Sub
    Private Sub _WorkSrc_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles _WorkSrc.SelectionChanged

        'Private Sub _WorkSrc_SelectedItemChanged(ByVal sender As Object, ByVal e As windows.proerlight    tyChangedEventArgs(Of Object)) Handles _WorkSrc.SelectionChanged
        Dim mystring As String = e.ToString
        ServiceURI = New Uri("/iLockboxDataService.svc/", UriKind.Relative)
        Dim ctx As New ilockboxEntities(ServiceURI)
        Dim uriRel As New Uri("GetBatches", UriKind.Relative)


        Dim callback As AsyncCallback = AddressOf GetBatchesComplete
        ctx.BeginExecute(Of Batchtbl)(uriRel, callback, ctx)
    End Sub
    Private Sub GetBatchesComplete(ByVal ar As IAsyncResult)

        Dim ctx As DataServiceContext = CType(ar.AsyncState, DataServiceContext)


        _Batches.ItemsSource = ctx.EndExecute(Of Batchtbl)(ar)

    End Sub

    'Private Sub _WorkSrc_SelectedValueChanged(ByVal sender As Object, ByVal e As C1.Silverlight.PropertyChangedEventArgs(Of Object)) Handles _WorkSrc.SelectedValueChanged

    'End Sub
    'Private Sub LoadBatches()

    '        Dim batchQuery As DataServiceQuery<Batchtbl>  = (DataServiceQuery<Product>)(from p in _awDataContext.Product
    '                                                                             select p);
    '        _Batches.IsLoading = true
    '        batchQuery.BeginExecute(new AsyncCallback(OnLoadBatchesComplete), batchQuery)
    'End Sub

    'Private Sub OnLoadProductsComplete(ByVal result As IAsyncResult)

    '        Dim batchQuery As DataServiceQuery<Batchtbl> = Ctype(result.AsyncState,DataServiceQuery<Batchtbl>)

    '        Dim batches As IEnumerable<Batchtbl> = batchQuery.EndExecute(result)

    '    _Batches.ItemsSource = New batches

    'End Sub

  

    Private Sub _Batches_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles _Batches.SelectionChanged
        If Not _Batches.SelectedItem Is Nothing Then
            '_CoverTitle.Text = String.Format("{0} {1}", "Batch", _Batches.SelectedItem.BatchNumber)
            '_BackCoverTitle.Text = String.Format("{0} {1}", "End of Batch", _Batches.SelectedItem.BatchNumber)


            Dim mystring As String = e.ToString
            ServiceURI = New Uri("/iLockboxDataService.svc/", UriKind.Relative)
            Dim ctx As New ilockboxEntities(ServiceURI)
            Dim uriRel As New Uri("GetBatchDetail", UriKind.Relative)

            Dim callback As AsyncCallback = AddressOf GetBatchDetailComplete
            ctx.BeginExecute(Of DetailTbl)(uriRel, callback, ctx)
        End If

    End Sub
   

    Private Sub GetBatchDetailComplete(ByVal ar As IAsyncResult)

        Dim ctx As DataServiceContext = CType(ar.AsyncState, DataServiceContext)
        _book.Items.Clear()
        '_book.ItemsSource = ctx.EndExecute(Of DetailTbl)(ar)
        For Each Detail In ctx.EndExecute(Of DetailTbl)(ar)
            If Not Detail Is Nothing Then
                Dim myLeft As New LeftPage
                myLeft.DataContext = Detail
                Dim myRight As New RightPage
                myRight.DataContext = Detail
                _book.Items.Add(myLeft)
                _book.Items.Add(myRight)
            End If
        Next

    End Sub
End Class

'Public Class WorkSrcConverter
'    Inherits TypeConverter
'    Public Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
'        If sourceType.Equals(GetType(String)) Then Return False
'        Return MyBase.CanConvertFrom(context, sourceType)
'    End Function
'    Public Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
'        If destinationType.Equals(GetType(String)) Then Return True
'        Return MyBase.CanConvertTo(context, destinationType)
'    End Function
'    Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object
'        If destinationType.Equals(GetType(System.String)) Then
'            If IsNothing(value) Then
'                Return String.Empty
'            Else
'                If value.worksource Is Nothing Then Return String.Empty
'                Return value.ToString
'            End If

'            'If value.Equals(String.Empty) Then Return String.Empty
'            'If value.GetType.Equals(GetType(Wrksrctbl)) Then
'            '    Return String.Concat(value.worksource, " ", value.name.trim)
'            'End If
'        End If


'        Return MyBase.ConvertTo(context, culture, value, destinationType)

'    End Function

'    Public Overloads Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object)
'        If TypeOf value Is String Then Return New Wrksrctbl
'        'Return String.Concat(value.worksource, " ", value.name.trim)
'        Return MyBase.ConvertFrom(context, culture, value)
'    End Function

'End Class
'Public Class BatchConverter
'    Inherits TypeConverter
'    Public Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
'        If sourceType.Equals(GetType(String)) Then Return False
'        Return MyBase.CanConvertFrom(context, sourceType)
'    End Function
'    Public Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
'        If destinationType.Equals(GetType(String)) Then Return True
'        Return MyBase.CanConvertTo(context, destinationType)
'    End Function
'    Public Overloads Overrides Function ConvertTo(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object, ByVal destinationType As Type) As Object

'        If destinationType.Equals(GetType(System.String)) Then
'            If IsNothing(value) Then
'                Return String.Empty
'            Else
'                If value.batchnumber Is Nothing Then Return String.Empty
'                Return value.ToString
'            End If

'            'If value.Equals(String.Empty) Then Return String.Empty
'            'If value.GetType.Equals(GetType(Wrksrctbl)) Then
'            '    Return String.Concat(value.worksource, " ", value.name.trim)
'            'End If
'        End If


'        Return MyBase.ConvertTo(context, culture, value, destinationType)

'    End Function

'    Public Overloads Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As CultureInfo, ByVal value As Object)
'        If TypeOf value Is String Then Return New Batchtbl
'        'Return String.Concat(value.worksource, " ", value.name.trim)
'        Return MyBase.ConvertFrom(context, culture, value)

'    End Function

'End Class
'Partial Public Class Wrksrctbl

'    Public Overrides Function ToString() As String
'        If IsNothing(Me.WorkSource) Then Return String.Empty
'        If IsNothing(MyBase.Name) Then Return MyBase.WorkSource.ToString
'        Return String.Concat(MyBase.WorkSource, " ", MyBase.Name.Trim)
'    End Function
'End Class
'Partial Public Class Batchtbl

'    Public Overrides Function ToString() As String
'        Dim tmpobj As Batchtbl = CType(MyBase.MemberwiseClone, Batchtbl)
'        If IsNothing(iLockbox.Batchtbltmpobj.BatchNumber) Then Return String.Empty
'        If IsNothing(MyBase.Status) Then Return MyBase.BatchNumber.ToString
'        Return String.Concat(MyBase.BatchNumber, " ", MyBase.Status.Trim)
'    End Function
'End Class