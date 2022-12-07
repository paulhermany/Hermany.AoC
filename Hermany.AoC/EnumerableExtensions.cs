using System;
using System.Collections.Generic;
using System.Text;

namespace Hermany.AoC
{
    public static class EnumerableUtilities
    {
        public static IEnumerable<TResult> SelectTwo<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> selector)
        {
            if (null == source) throw new ArgumentNullException(nameof(source));
            if (null == selector) throw new ArgumentNullException(nameof(selector));

            using (var iterator = source.GetEnumerator())
            {
                var current = default(TSource);
                var i = 0;
                while (iterator.MoveNext())
                {
                    var previous = current;
                    current = iterator.Current;
                    
                    if (i++ >= 1)
                    {
                        yield return selector(previous, current);
                    }
                }
            }
        }

        public static IEnumerable<TResult> SelectThree<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource, TResult> selector)
        {
            if (null == source) throw new ArgumentNullException(nameof(source));
            if (null == selector) throw new ArgumentNullException(nameof(selector));

            using (var iterator = source.GetEnumerator())
            {
                var current = default(TSource);
                var prev1 = default(TSource);
                var i = 0;
                while (iterator.MoveNext())
                {
                    var prev2 = prev1;
                    prev1 = current;
                    current = iterator.Current;

                    if (i++ >= 2)
                    {
                        yield return selector(prev2, prev1, current);
                    }
                }
            }
        }
    }
}
