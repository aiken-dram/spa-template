using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Helpers;

/// <summary>
/// Static class for working with List
/// </summary>
public static class ListHelper
{
    /// <summary>
    /// Returns list of id which are to be added to dbcontext
    /// </summary>
    /// <param name="requestId">id of entity</param>
    /// <param name="list">provided list</param>
    /// <param name="existing">existing list</param>
    /// <returns>list to be added to dbcontext</returns>
    public static IEnumerable<long>? Add(long? requestId, long[]? list, IEnumerable<long> existing)
    {
        if (requestId.HasValue)
        {
            if (existing == null || existing.Count() == 0)
            {
                //add all to entity that has no list
                return list;
            }
            else
            {
                if (list == null || list.Length == 0)
                {
                    //add none
                    return null;
                }
                else
                {
                    //need to select from list which are not in existing
                    return list.Where(p => existing.Count(q => q == p) == 0);
                }
            }
        }
        else
        {
            //new entity, add all
            return list;
        }
    }

    /// <summary>
    /// Returns list of id which are to be removed from dbcontext
    /// </summary>
    /// <param name="requestId">id of entity</param>
    /// <param name="list">provided list</param>
    /// <param name="existing">existing list</param>
    /// <returns>list to be removed from dbcontext</returns>
    public static IEnumerable<long>? Remove(long? requestId, long[]? list, IEnumerable<long> existing)
    {
        if (requestId.HasValue)
        {
            if (existing == null || existing.Count() == 0)
            {
                //remove none from entity that has no list
                return null;
            }
            else
            {
                if (list == null || list.Length == 0)
                {
                    //remove all
                    return existing;
                }
                else
                {
                    //need to select existing that are not in list
                    return existing.Where(p => list.Count(q => q == p) == 0);
                }
            }
        }
        else
        {
            //new entity, remove none
            return null;
        }
    }

    public static Tuple<IEnumerable<long>?, IEnumerable<long>?> Split(long? requestId, long[]? list, IEnumerable<long> existing)
    => new(Add(requestId, list, existing), Remove(requestId, list, existing));

    /// <summary>
    /// Processing list for many-to-many relations table
    /// </summary>
    /// <remarks>
    /// Hmm not sure where to put this function, helper?
    /// </remarks>
    /// <param name="requestId">Id of entity in request (can be null for new)</param>
    /// <param name="list">list of elements in request</param>
    /// <param name="existing">list of elements in entity</param>
    /// <param name="dbset">DbSet of list elements</param>
    /// <param name="newEntity">expression to create new list element</param>
    /// <param name="delEntity">expression to delete elements for id</param>
    /// <param name="_audit">AuditBuilder</param>
    /// <param name="audit">Audit</param>
    /// <param name="field">Name of audit field</param>
    /// <param name="dictionary">Dictionary of value-description pairs</param>
    /// <typeparam name="Tentity">type of entity</typeparam>
    public static void Process<Tentity>(long? requestId, long[]? list, IEnumerable<long> existing, DbSet<Tentity> dbset, Expression<Func<long, Tentity>> newEntity, Expression<Func<long, Expression<Func<Tentity, bool>>>> delEntity, IAuditBuilder _audit, ref Audit audit, string field, IDictionary<long, string>? dictionary = null)
        where Tentity : class, new()
    {
        (var add, var remove) = Split(requestId, list, existing);

        if (add != null)
            foreach (var a in add)
            {
                var e = (newEntity.Compile())(a);
                dbset.Add(e);
                audit.Add(_audit.DataFieldOperationValue(field, eAuditDataOperations.AddList, dictionary != null ? dictionary[a] : a.ToString()));
            }

        if (remove != null)
            foreach (var r in remove)
            {
                var pred = (delEntity.Compile())(r);
                dbset.RemoveRange(dbset.Where(pred));
                audit.Add(_audit.DataFieldOperationValue(field, eAuditDataOperations.RemoveList, dictionary != null ? dictionary[r] : r.ToString()));
            }
    }

    /// <summary>
    /// Splits original list into two based on predicate
    /// </summary>
    /// <param name="source">Original list</param>
    /// <param name="predicate">Condition to split list</param>
    /// <typeparam name="T">type of list</typeparam>
    /// <returns>Pair of lists (condition is true), (condition is false)</returns>
    public static Tuple<IEnumerable<T>, IEnumerable<T>> Segment<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var left = new List<T>();
        var right = new List<T>();

        foreach (var item in source)
        {
            if (predicate(item))
            {
                left.Add(item);
            }
            else
            {
                right.Add(item);
            }
        }

        return new(left, right);
    }
}
