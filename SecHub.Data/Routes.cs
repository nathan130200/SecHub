using SecHub.Rest.Abstractions;

namespace SecHub;

public static class Routes
{
    public const string BaseRouteUrl = "api/v1";
    public const string Alunos = $"{BaseRouteUrl}/aluno";
    public const string Responsaveis = $"{BaseRouteUrl}/responsavel";
    public const string Usuarios = $"{BaseRouteUrl}/usuario";
}

public static class LinqUtilities
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> source, int pagina)
    {
        return source.Skip((pagina - 1) * PagedQuery.PageSize)
            .Take(PagedQuery.PageSize);
    }
}