namespace PagedList
{
    public interface IPagedList
    {
        int TotalCount { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}