namespace SecHub.Rest.Abstractions;

public static class PagedQuery
{
    public const int PageSize = 15;
}

public class PagedQuery<T>
{
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public IEnumerable<T> Items { get; set; }
}
