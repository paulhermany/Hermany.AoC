using System;
using System.Collections.Generic;

namespace Hermany.AoC
{
    public class GraphTraversal
    {

        //public static TValue Dijkstra<TValue, TKey, TCompare>(TValue start, Func<TValue, bool> isEnd, Func<TValue, IEnumerable<TValue>> getNeighbors, Func<TValue, TCompare> getCost, Func<TValue, TKey> getKey)
        //{
        //    var notVisited = new PriorityQueue<TValue>();

        //    var d = new SortedDictionary<TValue, TKey>();


        //}

        //public static TValue FindPath<TValue, TKey>(Action<TValue> push, Func<TValue> pop, Func<TValue> peek, TValue start, Func<TValue, bool> isEnd, Func<TValue, IEnumerable<TValue>> getNeighbors, Func<TValue, TKey> getKey)
        //{
        //    push(start);
        //    var seen = new HashSet<TKey>();
        //    while (null != peek())
        //    {
        //        var current = pop();
        //        var key = getKey(current);
        //        if (seen.Contains(key)) continue;
        //        seen.Add(key);
        //        if (isEnd(current)) return current;
        //        foreach (var neighbor in getNeighbors(current))
        //            push(neighbor);
        //    }
        //    return default(TValue);
        //}
    }
}
