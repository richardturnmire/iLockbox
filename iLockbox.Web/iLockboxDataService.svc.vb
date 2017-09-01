Imports System.Data.Services
Imports System.Linq
Imports System.ServiceModel.Web

Public Class iLockboxDataService
    ' TODO: replace [[class name]] with your data class name
    Inherits DataService(Of ilockboxEntities)

    ' This method is called only once to initialize service-wide policies.
    Public Shared Sub InitializeService(ByVal config As IDataServiceConfiguration)
        config.SetEntitySetAccessRule("Batchmodetbl", EntitySetRights.AllRead)
        config.SetEntitySetAccessRule("ImgTbl", EntitySetRights.AllRead)
        config.SetEntitySetAccessRule("ItemSelectTbl", EntitySetRights.AllRead)
        config.SetEntitySetAccessRule("Wrksrctbl", EntitySetRights.AllRead)
        config.SetEntitySetAccessRule("BatchValue", EntitySetRights.AllRead)
        config.SetEntitySetAccessRule("DetailTbl", EntitySetRights.All)
        config.SetEntitySetAccessRule("Batchtbl", EntitySetRights.All)

        config.SetServiceOperationAccessRule("GetActiveWorkSources", ServiceOperationRights.AllRead)
        config.SetServiceOperationAccessRule("GetBatches", ServiceOperationRights.AllRead)
        config.SetServiceOperationAccessRule("GetBatchDetail", ServiceOperationRights.AllRead)
        ' Use verbose errors for development and debugging.
        config.UseVerboseErrors = True

    End Sub
    <WebGet()> Public Function GetActiveWorkSources() As IEnumerable(Of Wrksrctbl)

        Dim de As New ilockboxEntities()
        Dim tempwork = de.Wrksrctbl.Where(Function(ws) ws.IsItActive = "Y")
        Return tempwork

    End Function
    <WebGet()> Public Function GetBatches(ByVal wrksrc As String, ByVal subset As String, ByVal batchdate As String) As IQueryable(Of Batchtbl)
        Dim tmpwrksrc As String = String.Empty
        Dim tmpsubset As String = String.Empty
        Dim tmpdate As String = String.Empty

        If IsNothing(wrksrc) Then tmpwrksrc = "0100080000" Else tmpwrksrc = wrksrc
        If IsNothing(subset) Then tmpsubset = "01" Else tmpsubset = subset
        If IsNothing(batchdate) Then tmpdate = "2009-04-30" Else tmpdate = batchdate

        Dim wrkdate As Date = CDate(tmpdate)
        'tmpdate = wrkdate.ToString("MM/dd/yyyy")
        Dim de As New ilockboxEntities()
        Dim tempwork As IQueryable(Of Batchtbl) = _
        From batch In de.Batchtbl _
        Where batch.WorkSource = tmpwrksrc And batch.SubSet = tmpsubset _
        And batch.ProcessDt = wrkdate _
        Order By batch.BatchNumber _
        Select batch

        Return tempwork
        '
    End Function
    <WebGet()> Public Function GetBatchDetail(ByVal wrksrc As String, ByVal subset As String, ByVal batchdate As String, ByVal batchno As String) As IQueryable(Of DetailTbl)
        Dim tmpwrksrc As String = String.Empty
        Dim tmpsubset As String = String.Empty
        Dim tmpdate As String = String.Empty
        Dim tmpbatch As String = String.Empty

        If IsNothing(wrksrc) Then tmpwrksrc = "0100080000" Else tmpwrksrc = wrksrc
        If IsNothing(subset) Then tmpsubset = "01" Else tmpsubset = subset
        If IsNothing(batchdate) Then tmpdate = "2009-04-30" Else tmpdate = batchdate
        If IsNothing(batchno) Then tmpbatch = "0080000084" Else tmpbatch = batchno

        Dim wrkdate As Date = CDate(tmpdate)
        'tmpdate = wrkdate.ToString("MM/dd/yyyy")
        Dim de As New ilockboxEntities()
        Dim tempwork As IQueryable(Of DetailTbl) = _
        From detail In de.DetailTbl _
        Where detail.WorkSource = tmpwrksrc And detail.SubSet = tmpsubset _
        And detail.ProcessDt = wrkdate And detail.BatchNumber = tmpbatch _
        Order By detail.BatchNumber _
        Select detail

        Return tempwork
        ''
    End Function
End Class
