using System.Linq.Expressions;

namespace Infrastructure.Common.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIfElse<T>(
        this IQueryable<T> query,
        bool condition,
        Expression<Func<T, bool>> predicateWhenTrue,
        Expression<Func<T, bool>> predicateWhenFalse)
    {
        return condition
            ? query.Where(predicateWhenTrue)
            : query.Where(predicateWhenFalse);
    }
}
