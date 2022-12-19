using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermany.AoC._2022
{
    public class _18 : ISolution
    {
        public string P1Assertion { get => "64"; }
        public string P2Assertion { get => "58"; }

        /*
        --- Day 18: Boiling Boulders ---
        You and the elephants finally reach fresh air. You've emerged near the base of a large volcano that seems to be actively erupting! Fortunately, the lava seems to be flowing away from you and toward the ocean.

        Bits of lava are still being ejected toward you, so you're sheltering in the cavern exit a little longer. Outside the cave, you can see the lava landing in a pond and hear it loudly hissing as it solidifies.

        Depending on the specific compounds in the lava and speed at which it cools, it might be forming obsidian! The cooling rate should be based on the surface area of the lava droplets, so you take a quick scan of a droplet as it flies past you (your puzzle input).

        Because of how quickly the lava is moving, the scan isn't very good; its resolution is quite low and, as a result, it approximates the shape of the lava droplet with 1x1x1 cubes on a 3D grid, each given as its x,y,z position.

        To approximate the surface area, count the number of sides of each cube that are not immediately connected to another cube. So, if your scan were only two adjacent cubes like 1,1,1 and 2,1,1, each cube would have a single side covered and five sides exposed, a total surface area of 10 sides.

        Here's a larger example:

        2,2,2
        1,2,2
        3,2,2
        2,1,2
        2,3,2
        2,2,1
        2,2,3
        2,2,4
        2,2,6
        1,2,5
        3,2,5
        2,1,5
        2,3,5
        In the above example, after counting up all the sides that aren't connected to another cube, the total surface area is 64.

        What is the surface area of your scanned lava droplet?

        Your puzzle answer was 4504.
        */
        public string P1(string[] input)
        {
            var cubes = input
                .Select(_ => _.Split(',').Select(int.Parse).ToArray())
                .Select(_ => (x: _[0], y: _[1], z: _[2]))
                .ToHashSet();

            var visited = new HashSet<(int x, int y, int z)>();
            var faces = 0;

            var neighbors = new (int x, int y, int z)[] { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) };

            foreach (var c in cubes)
            {
                visited.Add((c.x, c.y, c.z));
                faces += 6;
                foreach (var (dx, dy, dz) in neighbors)
                    if (visited.Contains((c.x + dx, c.y + dy, c.z + dz))) faces -= 2;
            }

            return faces.ToString();
        }

        /*
        --- Part Two ---
        Something seems off about your calculation. The cooling rate depends on exterior surface area, but your calculation also included the surface area of air pockets trapped in the lava droplet.

        Instead, consider only cube sides that could be reached by the water and steam as the lava droplet tumbles into the pond. The steam will expand to reach as much as possible, completely displacing any air on the outside of the lava droplet but never expanding diagonally.

        In the larger example above, exactly one cube of air is trapped within the lava droplet (at 2,2,5), so the exterior surface area of the lava droplet is 58.

        What is the exterior surface area of your scanned lava droplet?

        Your puzzle answer was 2556. 
        */
        public string P2(string[] input)
        {
            var cubes = input
                .Select(_ => _.Split(',').Select(int.Parse).ToArray())
                .Select(_ => (x: _[0], y: _[1], z: _[2]))
                .ToHashSet();
            var visited = new HashSet<(int x, int y, int z)>();

            var neighbors = new (int x, int y, int z)[] { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) };

            var bounds = (
                x: (min: cubes.Min(_ => _.x), max: cubes.Max(_ => _.x)),
                y: (min: cubes.Min(_ => _.y), max: cubes.Max(_ => _.y)),
                z: (min: cubes.Min(_ => _.z), max: cubes.Max(_ => _.z))
            );

            var stack = new Stack<(int x, int y, int z)>();
            stack.Push((bounds.x.min - 1, bounds.y.min - 1, bounds.z.min - 1));

            while (stack.TryPop(out var current))
            {
                foreach (var n in neighbors.Select(n => (x: current.x + n.x, y: current.y + n.y, z: current.z + n.z)))
                {
                    if (cubes.Contains(n) || !InBounds(bounds, n)) continue;
                    if (visited.Add(n))
                        stack.Push(n);
                }
            }

            return cubes
                .SelectMany(c => neighbors.Select(n => (c.x + n.x, c.y + n.y, c.z + n.z)))
                .Count(_ => visited.Contains(_))
                .ToString();
        }

        public bool InBounds(((int min, int max) x, (int min, int max) y, (int min, int max) z) bounds, (int x, int y, int z) point)
        {
            if (point.x < bounds.x.min - 1 || point.x > bounds.x.max + 1) return false;
            if (point.y < bounds.y.min - 1 || point.y > bounds.y.max + 1) return false;
            if (point.z < bounds.z.min - 1 || point.z > bounds.z.max + 1) return false;
            return true;
        }
    }
}
