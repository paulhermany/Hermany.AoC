using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermany.AoC._2022
{
    public class _12 : ISolution
    {
        public string P1Assertion { get => "31"; }
        public string P2Assertion { get => "29"; }

        /*
        --- Day 12: Hill Climbing Algorithm ---
        You try contacting the Elves using your handheld device, but the river you're following must be too low to get a decent signal.

        You ask the device for a heightmap of the surrounding area (your puzzle input). The heightmap shows the local area from above broken into a grid; the elevation of each square of the grid is given by a single lowercase letter, where a is the lowest elevation, b is the next-lowest, and so on up to the highest elevation, z.

        Also included on the heightmap are marks for your current position (S) and the location that should get the best signal (E). Your current position (S) has elevation a, and the location that should get the best signal (E) has elevation z.

        You'd like to reach E, but to save energy, you should do it in as few steps as possible. During each step, you can move exactly one square up, down, left, or right. To avoid needing to get out your climbing gear, the elevation of the destination square can be at most one higher than the elevation of your current square; that is, if your current elevation is m, you could step to elevation n, but not to elevation o. (This also means that the elevation of the destination square can be much lower than the elevation of your current square.)

        For example:

        Sabqponm
        abcryxxl
        accszExk
        acctuvwj
        abdefghi
        Here, you start in the top-left corner; your goal is near the middle. You could start by moving down or right, but eventually you'll need to head toward the e at the bottom. From there, you can spiral around to the goal:

        v..v<<<<
        >v.vv<<^
        .>vv>E^^
        ..v>>>^^
        ..>>>>>^
        In the above diagram, the symbols indicate whether the path exits each square moving up (^), down (v), left (<), or right (>). The location that should get the best signal is still E, and . marks unvisited squares.

        This path reaches the goal in 31 steps, the fewest possible.

        What is the fewest steps required to move from your current position to the location that should get the best signal?

        Your puzzle answer was 350.
        */
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

        /*
        --- Part Two ---
        As you walk up the hill, you suspect that the Elves will want to turn this into a hiking trail. The beginning isn't very scenic, though; perhaps you can find a better starting point.

        To maximize exercise while hiking, the trail should start as low as possible: elevation a. The goal is still the square marked E. However, the trail should still be direct, taking the fewest steps to reach its goal. So, you'll need to find the shortest path from any square at elevation a to the square marked E.

        Again consider the example from above:

        Sabqponm
        abcryxxl
        accszExk
        acctuvwj
        abdefghi
        Now, there are six choices for starting position (five marked a, plus the square marked S that counts as being at elevation a). If you start at the bottom-left square, you can reach the goal most quickly:

        ...v<<<<
        ...vv<<^
        ...v>E^^
        .>v>>>^^
        >^>>>>>^
        This path reaches the goal in only 29 steps, the fewest possible.

        What is the fewest steps required to move starting from any square with elevation a to the location that should get the best signal?

        Your puzzle answer was 349.
        */
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
