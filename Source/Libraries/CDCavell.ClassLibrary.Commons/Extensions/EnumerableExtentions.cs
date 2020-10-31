using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    /// Extension methods for existing Enumerable types.
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 07/05/2020 | Initial build |~ 
    /// </revision>
    public static class EnumerableExtentions
    {
        /// <summary>
        /// Method to determine if string is a valid email address
        /// </summary>
        /// <param name="source">this IEnumerable&lt;TSource&gt;</param>
        /// <param name="keySelector">Func&lt;TSource, TKey&gt;</param>
        /// <returns>IEnumerable&lt;TSource&gt;</returns>
        /// <method>DistinctBy&lt;TSource, TKey&gt;(this IEnumerable&lt;TSource&gt; source, Func&lt;TSource, TKey&gt; keySelector)</method>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
