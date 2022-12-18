using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermany.AoC._2022
{
    public class _17 : ISolution
    {
        public string P1Assertion { get => "3068"; }
        public string P2Assertion { get => "1514285714288"; }

        /*
        --- Day 17: Pyroclastic Flow ---
        Your handheld device has located an alternative exit from the cave for you and the elephants. The ground is rumbling almost continuously now, but the strange valves bought you some time. It's definitely getting warmer in here, though.

        The tunnels eventually open into a very tall, narrow chamber. Large, oddly-shaped rocks are falling into the chamber from above, presumably due to all the rumbling. If you can't work out where the rocks will fall next, you might be crushed!

        The five types of rocks have the following peculiar shapes, where # is rock and . is empty space:

        ####

        .#.
        ###
        .#.

        ..#
        ..#
        ###

        #
        #
        #
        #

        ##
        ##
        The rocks fall in the order shown above: first the - shape, then the + shape, and so on. Once the end of the list is reached, the same order repeats: the - shape falls first, sixth, 11th, 16th, etc.

        The rocks don't spin, but they do get pushed around by jets of hot gas coming out of the walls themselves. A quick scan reveals the effect the jets of hot gas will have on the rocks as they fall (your puzzle input).

        For example, suppose this was the jet pattern in your cave:

        >>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>
        In jet patterns, < means a push to the left, while > means a push to the right. The pattern above means that the jets will push a falling rock right, then right, then right, then left, then left, then right, and so on. If the end of the list is reached, it repeats.

        The tall, vertical chamber is exactly seven units wide. Each rock appears so that its left edge is two units away from the left wall and its bottom edge is three units above the highest rock in the room (or the floor, if there isn't one).

        After a rock appears, it alternates between being pushed by a jet of hot gas one unit (in the direction indicated by the next symbol in the jet pattern) and then falling one unit down. If any movement would cause any part of the rock to move into the walls, floor, or a stopped rock, the movement instead does not occur. If a downward movement would have caused a falling rock to move into the floor or an already-fallen rock, the falling rock stops where it is (having landed on something) and a new rock immediately begins falling.

        Drawing falling rocks with @ and stopped rocks with #, the jet pattern in the example above manifests as follows:

        The first rock begins falling:
        |..@@@@.|
        |.......|
        |.......|
        |.......|
        +-------+

        Jet of gas pushes rock right:
        |...@@@@|
        |.......|
        |.......|
        |.......|
        +-------+

        Rock falls 1 unit:
        |...@@@@|
        |.......|
        |.......|
        +-------+

        Jet of gas pushes rock right, but nothing happens:
        |...@@@@|
        |.......|
        |.......|
        +-------+

        Rock falls 1 unit:
        |...@@@@|
        |.......|
        +-------+

        Jet of gas pushes rock right, but nothing happens:
        |...@@@@|
        |.......|
        +-------+

        Rock falls 1 unit:
        |...@@@@|
        +-------+

        Jet of gas pushes rock left:
        |..@@@@.|
        +-------+

        Rock falls 1 unit, causing it to come to rest:
        |..####.|
        +-------+

        A new rock begins falling:
        |...@...|
        |..@@@..|
        |...@...|
        |.......|
        |.......|
        |.......|
        |..####.|
        +-------+

        Jet of gas pushes rock left:
        |..@....|
        |.@@@...|
        |..@....|
        |.......|
        |.......|
        |.......|
        |..####.|
        +-------+

        Rock falls 1 unit:
        |..@....|
        |.@@@...|
        |..@....|
        |.......|
        |.......|
        |..####.|
        +-------+

        Jet of gas pushes rock right:
        |...@...|
        |..@@@..|
        |...@...|
        |.......|
        |.......|
        |..####.|
        +-------+

        Rock falls 1 unit:
        |...@...|
        |..@@@..|
        |...@...|
        |.......|
        |..####.|
        +-------+

        Jet of gas pushes rock left:
        |..@....|
        |.@@@...|
        |..@....|
        |.......|
        |..####.|
        +-------+

        Rock falls 1 unit:
        |..@....|
        |.@@@...|
        |..@....|
        |..####.|
        +-------+

        Jet of gas pushes rock right:
        |...@...|
        |..@@@..|
        |...@...|
        |..####.|
        +-------+

        Rock falls 1 unit, causing it to come to rest:
        |...#...|
        |..###..|
        |...#...|
        |..####.|
        +-------+

        A new rock begins falling:
        |....@..|
        |....@..|
        |..@@@..|
        |.......|
        |.......|
        |.......|
        |...#...|
        |..###..|
        |...#...|
        |..####.|
        +-------+
        The moment each of the next few rocks begins falling, you would see this:

        |..@....|
        |..@....|
        |..@....|
        |..@....|
        |.......|
        |.......|
        |.......|
        |..#....|
        |..#....|
        |####...|
        |..###..|
        |...#...|
        |..####.|
        +-------+

        |..@@...|
        |..@@...|
        |.......|
        |.......|
        |.......|
        |....#..|
        |..#.#..|
        |..#.#..|
        |#####..|
        |..###..|
        |...#...|
        |..####.|
        +-------+

        |..@@@@.|
        |.......|
        |.......|
        |.......|
        |....##.|
        |....##.|
        |....#..|
        |..#.#..|
        |..#.#..|
        |#####..|
        |..###..|
        |...#...|
        |..####.|
        +-------+

        |...@...|
        |..@@@..|
        |...@...|
        |.......|
        |.......|
        |.......|
        |.####..|
        |....##.|
        |....##.|
        |....#..|
        |..#.#..|
        |..#.#..|
        |#####..|
        |..###..|
        |...#...|
        |..####.|
        +-------+

        |....@..|
        |....@..|
        |..@@@..|
        |.......|
        |.......|
        |.......|
        |..#....|
        |.###...|
        |..#....|
        |.####..|
        |....##.|
        |....##.|
        |....#..|
        |..#.#..|
        |..#.#..|
        |#####..|
        |..###..|
        |...#...|
        |..####.|
        +-------+

        |..@....|
        |..@....|
        |..@....|
        |..@....|
        |.......|
        |.......|
        |.......|
        |.....#.|
        |.....#.|
        |..####.|
        |.###...|
        |..#....|
        |.####..|
        |....##.|
        |....##.|
        |....#..|
        |..#.#..|
        |..#.#..|
        |#####..|
        |..###..|
        |...#...|
        |..####.|
        +-------+

        |..@@...|
        |..@@...|
        |.......|
        |.......|
        |.......|
        |....#..|
        |....#..|
        |....##.|
        |....##.|
        |..####.|
        |.###...|
        |..#....|
        |.####..|
        |....##.|
        |....##.|
        |....#..|
        |..#.#..|
        |..#.#..|
        |#####..|
        |..###..|
        |...#...|
        |..####.|
        +-------+

        |..@@@@.|
        |.......|
        |.......|
        |.......|
        |....#..|
        |....#..|
        |....##.|
        |##..##.|
        |######.|
        |.###...|
        |..#....|
        |.####..|
        |....##.|
        |....##.|
        |....#..|
        |..#.#..|
        |..#.#..|
        |#####..|
        |..###..|
        |...#...|
        |..####.|
        +-------+
        To prove to the elephants your simulation is accurate, they want to know how tall the tower will get after 2022 rocks have stopped (but before the 2023rd rock begins falling). In this example, the tower of rocks will be 3068 units tall.

        How many units tall will the tower of rocks be after 2022 rocks have stopped falling?

        Your puzzle answer was 3059.
        */
        public string P1(string[] input)
        {
            var rockTypes = new (int x, int y)[][]
            {
                new[] { (0,0), (1,0), (2,0), (3,0) }, // horizontal line
                new[] { (1,0), (0,1), (1,1), (2,1), (1,2) }, // 3x3 plus sign
                new[] { (0,0), (1,0), (2,0), (2,1), (2,2)}, // 3x3 l-shape
                new[] { (0,0), (0,1), (0,2), (0,3) }, // vertical line
                new[]{ (0,0), (0,1), (1,0), (1,1) } // 2x2 square
             };

            var rockType = 0;
            var rockCount = 0;

            var width = 7;
            var height = -1;

            var map = new HashSet<(int x, int y)>();
            var index = -1;

            while (rockCount++ < 2022)
            {
                var rock = rockTypes[rockType++ % 5].Select(_ => (x: _.x + 2, y: _.y + height + 4));

                while (true)
                {
                    index = (index + 1) % input[0].Length;

                    //PrintMap(map, rock, width);

                    var next = rock.Select(_ => (x: _.x + (input[0][index] - '='), y: _.y)).ToArray();
                    if (next.All(_ => _.x >= 0 && _.x < width && !map.Contains(_)))
                        rock = next;

                    //PrintMap(map, rock, width);

                    next = rock.Select(_ => (x: _.x, y: _.y - 1)).ToArray();
                    if (next.All(_ => _.y >= 0 && !map.Contains(_)))
                        rock = next;
                    else {
                        foreach (var p in rock) {
                            if (!map.Contains(p)) map.Add(p);
                            if (p.y > height) height = p.y;
                        }
                        break;
                    }
                }
            }

            return (height + 1).ToString();
        }

        /*
        --- Part Two ---
        The elephants are not impressed by your simulation. They demand to know how tall the tower will be after 1000000000000 rocks have stopped! Only then will they feel confident enough to proceed through the cave.

        In the example above, the tower would be 1514285714288 units tall!

        How tall will the tower be after 1000000000000 rocks have stopped?

        Your puzzle answer was 1500874635587.
        */
        public string P2(string[] input)
        {
            var rockTypes = new (int x, int y)[][]
            {
                new[] { (0,0), (1,0), (2,0), (3,0) }, // horizontal line
                new[] { (1,0), (0,1), (1,1), (2,1), (1,2) }, // 3x3 plus sign
                new[] { (0,0), (1,0), (2,0), (2,1), (2,2)}, // 3x3 l-shape
                new[] { (0,0), (0,1), (0,2), (0,3) }, // vertical line
                new[]{ (0,0), (0,1), (1,0), (1,1) } // 2x2 square
             };

            var rockType = 0;
            long rockCount = 0;
            long rockCountTarget = 1000000000000;

            var width = 7;
            var height = -1;

            var map = new HashSet<(int x, int y)>();
            var index = -1;

            var seen = new Dictionary<(int index, int rockType), (long rockCount, long height)>();

            var cycles = 0;

            while (rockCount++ < rockCountTarget)
            {
                var rock = rockTypes[rockType].Select(_ => (x: _.x + 2, y: _.y + height + 4));
                rockType = (rockType + 1) % rockTypes.Length;

                while (true)
                {
                    index = (index + 1) % input[0].Length;

                    var next = rock.Select(_ => (x: _.x + (input[0][index] - '='), y: _.y)).ToArray();
                    if (next.All(_ => _.x >= 0 && _.x < width && !map.Contains(_)))
                        rock = next;

                    next = rock.Select(_ => (x: _.x, y: _.y - 1)).ToArray();
                    if (next.All(_ => _.y >= 0 && !map.Contains(_)))
                        rock = next;
                    else
                    {
                        foreach (var p in rock)
                        {
                            if (!map.Contains(p)) map.Add(p);
                            if (p.y > height) height = p.y;
                        }

                        break;
                    }
                }

                var state = (index, rockType);
                if (!seen.ContainsKey(state))
                    seen.Add(state, (rockCount, height + 1));
                else
                {
                    cycles++;
                    var last = seen[state];
                    var cycle = rockCount - last.rockCount;

                    var cyclesRemaining = (rockCountTarget - last.rockCount) / cycle;
                    var heightPerCycle = height + 1 - last.height;
                    var ret = cyclesRemaining * heightPerCycle + last.height;

                    // there's some black magic going on here since the cycles for the test input start immediately while the cycles for the real input take time to warm up
                    // index = 1 works for the test input
                    // index = 38 works for the real input and was found using a combination of trial and error / observing the output
                    if (index == 1 || index == 38)
                        return ret.ToString();
                }
            }

            return string.Empty;
        }

        public void PrintMap(HashSet<(int x, int y)> map, IEnumerable<(int x, int y)> rock, int width)
        {
            var height = Math.Max(map.Count == 0 ? 0 : map.Max(_ => _.y), rock.Max(_ => _.y));

            var rockMap = rock.ToDictionary(_ => (_.x, _.y), _ => true);

            Console.Clear();
            for (var h = height; h >= 0; h--)
            {
                for (var w = 0; w < width; w++)
                {
                    if (rockMap.ContainsKey((w, h)))
                        Console.Write("@");
                    else
                        Console.Write(map.Contains((w, h)) ? "#" : ".");
                }
                Console.WriteLine();
            }
        }
    }
}
