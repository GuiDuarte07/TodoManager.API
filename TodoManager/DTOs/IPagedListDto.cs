namespace TodoManager.DTOs
{
    public interface IPagedListDto<T>
    {
        IEnumerable<T> Items { get; }
        int TotalCount { get; }
        int PageSize { get; }
        int CurrentPage { get; }
        int TotalPages { get; }
    }
}
