using System;
using System.Diagnostics;
using System.IO;

namespace Hermany.AoC
{
    class Program
    {
        static void Main(string[] args)
        {
            // set current year/day
            var currentYear = "2022";
            var currentDay = "08";
            
            // define I/O paths
            var prefixPath = @$"..\..\..\_{currentYear}\_{currentDay}-";
            var inputPath = string.Concat(prefixPath, "input.txt");
            var part1Path = string.Concat(prefixPath, "part1.txt");
            var part2Path = string.Concat(prefixPath, "part2.txt");

            // get instance of current solution
            var ns = typeof(Program).Namespace;
            var solution = (ISolution)Activator.CreateInstance(ns, $"{ns}._{currentYear}._{currentDay}").Unwrap();

            // read input
            var input = File.ReadAllLines(inputPath);

            // initialize timer
            var sw = new Stopwatch();

            try
            {
                sw.Restart();
                var p1 = solution.P1(input);
                sw.Stop();

                File.WriteAllText(part1Path, p1);
                Console.WriteLine(p1);
                Console.WriteLine($"Part 1: {sw.Elapsed:g}");
            }
            catch (NotImplementedException) { }

            Console.WriteLine();

            try
            {
                sw.Restart();
                var p2 = solution.P2(input);
                sw.Stop();

                File.WriteAllText(part2Path, p2);
                Console.WriteLine(p2);
                Console.WriteLine($"Part 2: {sw.Elapsed:g}");
            }
            catch (NotImplementedException) { }
        }
    }
}
