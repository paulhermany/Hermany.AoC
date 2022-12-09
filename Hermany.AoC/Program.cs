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
            var currentDay = "10";
            
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
            var inputTest = File.ReadAllLines(inputTestPath);

            // initialize timer
            var sw = new Stopwatch();

            // create output file
            File.WriteAllText(outputPath, string.Empty);
            Console.Clear();

            var output = string.Empty;

            try
            {
                sw.Restart();
                output = solution.P1(inputTest);
                sw.Stop();

                Console.WriteLine($"Part 1 Test: {output}");
                Console.WriteLine($"{sw.Elapsed:g}");

                if (output != solution.P1Assertion)
                {
                    Console.WriteLine($"Part 1 Test Failed. Expected: {solution.P1Assertion}\n");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Part 1 Test Passed.");
                    Console.WriteLine();

                    sw.Restart();
                    output = solution.P1(input);
                    sw.Stop();

                    File.AppendAllText(outputPath, $"Part 1: {output}\n");
                    File.AppendAllText(outputPath, $"{sw.Elapsed:g}\n");
                    File.AppendAllText(outputPath, "\n");

                    Console.WriteLine($"Part 1: {output}");
                    Console.WriteLine($"{sw.Elapsed:g}");
                    Console.WriteLine();
                }
            }
            catch (NotImplementedException)
            {
                Console.WriteLine($"Part 1 Not Implemented.");
                Console.WriteLine();
            }

            try
            {
                sw.Restart();
                output = solution.P2(inputTest);
                sw.Stop();

                Console.WriteLine($"Part 2 Test: {output}");
                Console.WriteLine($"{sw.Elapsed:g}");

                if (output != solution.P2Assertion)
                {
                    Console.WriteLine($"Part 2 Test Failed. Expected: {solution.P2Assertion}\n");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Part 2 Test Passed.");
                    Console.WriteLine();

                    sw.Restart();
                    output = solution.P2(input);
                    sw.Stop();

                    File.AppendAllText(outputPath, $"Part 2: {output}\n");
                    File.AppendAllText(outputPath, $"{sw.Elapsed:g}");
                    File.AppendAllText(outputPath, "\n");

                    Console.WriteLine($"Part 2: {output}");
                    Console.WriteLine($"{sw.Elapsed:g}");
                    Console.WriteLine();
                }
            }
            catch (NotImplementedException)
            {
                Console.WriteLine($"Part 2 Not Implemented.");
                Console.WriteLine();
            }
        }
    }
}
