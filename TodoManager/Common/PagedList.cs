namespace TodoManager.Common
{
    public class PagedList<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
