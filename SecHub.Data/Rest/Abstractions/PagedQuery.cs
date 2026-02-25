namespace SecHub.Rest.Abstractions;

public static class PagedQuery
{
    public const int PageSize = 15;
}

public class PagedQuery<T>
{
    public int MaxPages { get; set; }
    public int CurrentPage { get; set; }

    public int MaxItems { get; set; }
    public int CurrentItems { get; set; }

    public IEnumerable<T> Items { get; set; }
}
