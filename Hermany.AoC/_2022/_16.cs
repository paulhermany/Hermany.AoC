using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hermany.AoC._2022
{
    public class _16 : ISolution
    {
        public string P1Assertion { get => "1651"; }
        public string P2Assertion { get => "1707"; }
        public string P1(string[] input)
        {
            var valves = new Dictionary<string, (int index, int mask, int rate, string[] destinations)>();

            for (var i = 0; i < input.Length; i++)
            {
                var pattern = new Regex(@"\s(\w{2})\s.*=(\d+).*valves? (\w{2}(?:,*\s*\w{2})*)+");
                var m = pattern.Match(input[i]);
                valves.Add(m.Groups[1].Value, (i, 1 << i, int.Parse(m.Groups[2].Value), m.Groups[3].Value.Split(", ").ToArray()));
            }

            var maxRate = valves.Max(_ => _.Value.rate);
            var maxOpen = valves.Sum(_ => _.Value.rate);

            var stack = new Stack<(string key, int timeRemaining, int pressure, int opened, int openrate)>();
            stack.Push(("AA", 30, 0, 0, maxOpen));

            var maxPressure = 0;

            var states = new HashSet<(string key, int timeRemaining, int pressure, int opened)>();

            while(stack.TryPop(out var current))
            {
                var state = (current.key, current.timeRemaining, current.pressure, current.opened);
                if (states.Contains(state)) continue;
                states.Add(state);

                var v = valves[current.key];

                if ((current.timeRemaining - 1) * current.openrate + current.pressure < maxPressure) continue;

                if(current.timeRemaining > 0 && v.rate != 0 && (current.opened & v.mask) != v.mask)
                {
                    var pressure = current.pressure + v.rate * (current.timeRemaining - 1);
                    
                    if(current.timeRemaining > 1)
                        stack.Push((current.key, current.timeRemaining - 1, pressure, current.opened | v.mask, current.openrate - v.rate));

                    if (pressure > maxPressure)
                        maxPressure = pressure;
                }

                if(current.timeRemaining > 1)
                {
                    foreach(var destination in v.destinations)
                    {
                        stack.Push((destination, current.timeRemaining - 1, current.pressure, current.opened, current.openrate));
                    }
                }
                
            }

            return maxPressure.ToString();
        }
        public string P2(string[] input)
        {
            var valves = new Dictionary<string, (int index, int mask, int rate, string[] destinations)>();

            for (var i = 0; i < input.Length; i++)
            {
                var pattern = new Regex(@"\s(\w{2})\s.*=(\d+).*valves? (\w{2}(?:,*\s*\w{2})*)+");
                var m = pattern.Match(input[i]);
                valves.Add(m.Groups[1].Value, (i, 1 << i, int.Parse(m.Groups[2].Value), m.Groups[3].Value.Split(", ").ToArray()));
            }

            var maxRate = valves.Max(_ => _.Value.rate);
            var maxOpen = valves.Sum(_ => _.Value.rate);

            var stack = new Stack<(string key, string key2, int timeRemaining, int pressure, int opened, int openrate)>();
            stack.Push(("AA", "AA", 26, 0, 0, maxOpen));

            var maxPressure = 0;

            var states = new HashSet<(string key, string key2, int timeRemaining, int pressure, int opened)>();

            while (stack.TryPop(out var current))
            {   
                if (states.Contains((current.key, current.key2, current.timeRemaining, current.pressure, current.opened))) continue;
                if (states.Contains((current.key2, current.key, current.timeRemaining, current.pressure, current.opened))) continue;
                states.Add((current.key, current.key2, current.timeRemaining, current.pressure, current.opened));

                var v1 = valves[current.key];
                var v2 = valves[current.key2];

                var nextKey1 = current.key;
                var nextKey2 = current.key2;
                var nextPressure = current.pressure;
                var nextOpened = current.opened;
                var nextOpenrate = current.openrate;

                var _pressure = (current.timeRemaining - 1) * current.openrate + current.pressure;
                if (_pressure < maxPressure) continue;

                if (current.timeRemaining > 0)
                {
                    if (v1.rate != 0 && (nextOpened & v1.mask) != v1.mask && v2.rate != 0 && (nextOpened & v2.mask) != v2.mask && nextKey1 != nextKey2)
                    {
                        nextPressure += v1.rate * (current.timeRemaining - 1);
                        nextOpened |= v1.mask;
                        nextOpenrate -= v1.rate;

                        nextPressure += v2.rate * (current.timeRemaining - 1);
                        nextOpened |= v2.mask;
                        nextOpenrate -= v2.rate;

                        if(current.timeRemaining > 1)
                            stack.Push((nextKey1, nextKey2, current.timeRemaining - 1, nextPressure, nextOpened, nextOpenrate));
                    }
                    
                    else if (v1.rate != 0 && (nextOpened & v1.mask) != v1.mask)
                    {
                        nextPressure += v1.rate * (current.timeRemaining - 1);
                        nextKey1 = current.key;
                        nextOpened |= v1.mask;
                        nextOpenrate -= v1.rate;

                        if(current.timeRemaining > 1)
                            foreach (var v2Dest in v2.destinations)
                                if(v2Dest != current.key2)
                                    stack.Push((nextKey1, v2Dest, current.timeRemaining - 1, nextPressure, nextOpened, nextOpenrate));
                    }
                    
                    else if (v2.rate != 0 && (nextOpened & v2.mask) != v2.mask)
                    {
                        nextPressure += v2.rate * (current.timeRemaining - 1);
                        nextKey2 = current.key2;
                        nextOpened |= v2.mask;
                        nextOpenrate -= v2.rate;

                        if (current.timeRemaining > 1)
                            foreach (var v1Dest in v1.destinations)
                            {
                                if(v1Dest != current.key)
                                    stack.Push((v1Dest, nextKey2, current.timeRemaining - 1, nextPressure, nextOpened, nextOpenrate));
                            }
                    }

                    if (nextPressure > maxPressure)
                    {
                        maxPressure = nextPressure;
                        Console.WriteLine(maxPressure);
                    }
                }

                if (current.timeRemaining > 1)
                {
                    foreach (var v1Dest in v1.destinations)
                    {
                        if (v1Dest != current.key)
                        {
                            foreach (var v2Dest in v2.destinations)
                            {
                                if (v2Dest != current.key2)
                                {
                                    stack.Push((v1Dest, v2Dest, current.timeRemaining - 1, current.pressure, current.opened, current.openrate));
                                }
                            }
                        }
                    }
                }
            }

            return maxPressure.ToString();
        }
    }
}