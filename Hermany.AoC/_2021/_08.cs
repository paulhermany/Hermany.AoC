using System;
using System.Linq;

namespace Hermany.AoC._2021
{
    public class _08 : ISolution
    {
        public string P1Assertion { get => "26"; }
        public string P2Assertion { get => "5353"; }
        public string P1(string[] input)
        {
            return input
                .Select(_ => 
                    _.Split(" | ")[1]
                    .Split(' ')
                    .Count(s => s.Length == 2 || s.Length == 4 || s.Length == 3 || s.Length == 7)
                )
                .Sum().ToString();
        }
        public string P2(string[] input)
        {
            throw new NotImplementedException();
        }
    }
}
