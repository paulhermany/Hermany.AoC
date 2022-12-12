using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermany.AoC._2022
{
    public class _12 : ISolution
    {
        public string P1Assertion { get => "31"; }
        public string P2Assertion { get => "29"; }
        public string P1(string[] input)
        {
            _map = input.Select(_ => _.ToArray()).ToArray();
            _width = input[0].Length;
            _height = input.Length;

            var start = input.Select((line, row) => (row, line.IndexOf('S'))).OrderByDescending(_ => _.Item2).First();
            var end = input.Select((line, row) => (row, line.IndexOf('E'))).OrderByDescending(_ => _.Item2).First();

            _map[start.row][start.Item2] = 'a';
            _map[end.row][end.Item2] = 'z';

            return MinPath(start, end).ToString();
        }
        public string P2(string[] input)
        {
            _map = input.Select(_ => _.ToArray()).ToArray();
            _width = input[0].Length;
            _height = input.Length;

            var start = input.Select((line, row) => (row, line.IndexOf('S'))).OrderByDescending(_ => _.Item2).First();
            var end = input.Select((line, row) => (row, line.IndexOf('E'))).OrderByDescending(_ => _.Item2).First();

            _map[start.row][start.Item2] = 'a';
            _map[end.row][end.Item2] = 'z';

            var minPaths = new List<int>();
            
            for(var row = 0; row < _height; row++)
                for(var col = 0; col < _width; col++)
                    if (_map[row][col] == 'a' || _map[row][col] == 'S')
                        minPaths.Add(MinPath((row, col), end));

            return minPaths.Where(_ => _ > 0).Min().ToString();
        }

        public int MinPath((int, int) start, (int, int) end)
        {
            var q = new PriorityQueue<(int, (int, int))>();
            q.Enqueue((1, start));

            var visited = new HashSet<(int, int)>();

            while (q.Length > 0)
            {
                var current = q.Dequeue();

                if (visited.Contains((current.Item2))) continue;
                visited.Add(current.Item2);

                var r = current.Item2.Item1;
                var c = current.Item2.Item2;

                var neighbors = new[] { (r - 1, c), (r + 1, c), (r, c - 1), (r, c + 1) }
                .Where(_ => _.Item1 >= 0 && _.Item1 < _height && _.Item2 >= 0 && _.Item2 < _width);

                foreach (var neighbor in neighbors)
                {
                    var currentValue = _map[r][c] - 97;
                    var neighborValue = _map[neighbor.Item1][neighbor.Item2] - 97;

                    if (neighborValue <= currentValue + 1)
                    {
                        if (neighbor == end)
                            return current.Item1;

                        q.Enqueue((current.Item1 + 1, neighbor));
                    }
                }

            }

            return -1;
        }

        private char[][] _map;
        private int _width;
        private int _height;
    }

    public class PriorityQueue<T> where T : IComparable<T>
    {
        public int Length => _length;

        public PriorityQueue()
        {
            _data = new List<T>();
            _length = 0;
        }

        public void Enqueue(T item)
        {
            _data.Add(item);
            _length++;
            var ci = _data.Count - 1;
            while (ci > 0)
            {
                var pi = (ci - 1) / 2;
                if (_data[ci].CompareTo(_data[pi]) >= 0)
                    break;
                T tmp = _data[ci]; _data[ci] = _data[pi]; _data[pi] = tmp;
                ci = pi;
            }
        }

        public T Dequeue()
        {
            var li = _data.Count - 1;
            T frontItem = _data[0];
            _data[0] = _data[li];
            _data.RemoveAt(li);
            _length--;

            --li;
            var pi = 0;
            while (true)
            {
                var ci = pi * 2 + 1;
                if (ci > li) break;
                var rc = ci + 1;
                if (rc <= li && _data[rc].CompareTo(_data[ci]) < 0)
                    ci = rc;
                if (_data[pi].CompareTo(_data[ci]) <= 0) break;
                T tmp = _data[pi]; _data[pi] = _data[ci]; _data[ci] = tmp;
                pi = ci;
            }
            return frontItem;
        }

        private List<T> _data;
        private int _length;
    }
}
