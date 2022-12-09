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
            var inputTestPath = string.Concat(prefixPath, "input-test.txt");
            var outputPath = string.Concat(prefixPath, "output.txt");

            // get instance of current solution
            var ns = typeof(Program).Namespace;
            var solution = (ISolution)Activator.CreateInstance(ns, $"{ns}._{currentYear}._{currentDay}").Unwrap();

            // read input
            var input = File.ReadAllLines(inputPath);
            
            // initialize timer
            var sw = new Stopwatch();

            File.WriteAllText(outputPath, string.Empty);
            Console.Clear();

            var output = string.Empty;

            try
            {
                sw.Restart();
                output= solution.P1(input);
                sw.Stop();

                File.AppendAllText(outputPath, $"Part 1: {output}\n");
                File.AppendAllText(outputPath, $"{sw.Elapsed:g}\n");
                Console.WriteLine($"Part 1: {output}");
                Console.WriteLine($"{sw.Elapsed:g}");
            }
            catch (NotImplementedException) { }

            File.AppendAllText(outputPath, "\n");
            Console.WriteLine();

            try
            {
                sw.Restart();
                output = solution.P2(input);
                sw.Stop();

                File.AppendAllText(outputPath, $"Part 2: {output}\n");
                File.AppendAllText(outputPath, $"{sw.Elapsed:g}");
                Console.WriteLine($"Part 2: {output}");
                Console.WriteLine($"{sw.Elapsed:g}");
            }
            catch (NotImplementedException) { }
        }
    }
}
