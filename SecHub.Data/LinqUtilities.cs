using SecHub.Rest.Abstractions;

namespace SecHub;

public static class LinqUtilities
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> source, int pagina)
    {
        return source.Skip((pagina - 1) * PagedQuery.PageSize)
            .Take(PagedQuery.PageSize);
    }
}