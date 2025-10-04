namespace TodoManager.DTOs
{
    public class PagedListDto<T> : IPagedListDto<T>
    {
        public IEnumerable<T> Items { get; init; }
        public int TotalCount { get; init; }
        public int PageSize { get; init; }
        public int CurrentPage { get; init; }
        public int TotalPages { get; init; }
    }
}
