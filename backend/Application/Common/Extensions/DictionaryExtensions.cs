using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extensions;

/// <summary>
/// Extension for DbSet
/// </summary>
public static class DbSetExtensions
{
    /// <summary>
    /// Returns id of type from dictionary by providing type name
    /// </summary>
    /// <param name="source">Db set of dictionary</param>
    /// <param name="dict">Type name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Id of type</returns>
    public static async Task<int> DictionaryTypeAsync<TSource>(this DbSet<TSource> source,
        string dict,
        CancellationToken cancellationToken)
        where TSource : class, IDictionaryType
    {
        var d = await source.FirstAsync(p => p.Type == dict, cancellationToken);
        return d.IdType;
    }

    /// <summary>
    /// Returns id of state from dictionary by providing state name
    /// </summary>
    /// <param name="source">Db set of dictionary</param>
    /// <param name="dict">State name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Id of state</returns>
    public static async Task<int> DictionaryStateAsync<TSource>(this DbSet<TSource> source,
        string dict,
        CancellationToken cancellationToken)
        where TSource : class, IDictionaryState
    {
        var d = await source.FirstAsync(p => p.State == dict, cancellationToken);
        return d.IdState;
    }

    /// <summary>
    /// Returns id of action from dictionary by providing action name
    /// </summary>
    /// <param name="source">Db set of dictionary</param>
    /// <param name="dict">State name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Id of state</returns>
    public static async Task<int> DictionaryActionAsync<TSource>(this DbSet<TSource> source,
        string dict,
        CancellationToken cancellationToken)
        where TSource : class, IDictionaryAction
    {
        var d = await source.FirstAsync(p => p.Action == dict, cancellationToken);
        return d.IdAction;
    }
}