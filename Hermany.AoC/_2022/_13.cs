using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hermany.AoC._2022
{
    public class _13 : ISolution
    {
        public string P1Assertion { get => "13"; }
        public string P2Assertion { get => "140"; }
        public string P1(string[] input)
        {
			var packets = input.Where(_ => string.Empty != _).Select(_ => ParsePacket(_)).ToList();

			var sum = 0;

			for (int i = 0; i < packets.Count; i+=2)
				if (Compare(packets[i], packets[i + 1]) == -1) sum += i / 2 + 1;

			return sum.ToString();
        }
        public string P2(string[] input)
        {
			var packets = input.Where(_ => string.Empty != _).Select(_ => ParsePacket(_)).ToList();

			var dividerPacket1 = new List<object> { new List<object> { 2 } };
			var dividerPacket2 = new List<object> { new List<object> { 6 } };

			packets.Add(dividerPacket1);
			packets.Add(dividerPacket2);
			packets.Sort((a, b) => Compare(a, b));

			var dividerPacket1Index = packets.IndexOf(dividerPacket1) + 1;
			var dividerPacket2Index = packets.IndexOf(dividerPacket2) + 1;

			return (dividerPacket1Index * dividerPacket2Index).ToString();
		}

		public List<object> ParsePacket(string line)
		{
			var stack = new Stack<List<object>>();
			var currentList = new List<object>();
			stack.Push(currentList);

			for (int i = 1; i < line.Length; i++)
			{
				switch(line[i])
                {
					case '[':
						var newList = new List<object>();
						currentList.Add(newList);
						stack.Push(currentList);
						currentList = newList;
						break;
					case ']':
						currentList = stack.Pop();
						break;
					case ',':
						break;
					default:
						var sb = new StringBuilder();
						while(line[i] >= '0' && line[i] <= '9')
							sb.Append(line[i++]);
						i--;
						currentList.Add(int.Parse(sb.ToString()));
						break;
                }
			}
			return currentList;
		}

		public int Compare(object a, object b)
		{
			if (a is int && b is int)
			{
				var ret = CompareNumbers(a, b);
				if (ret != 0) return ret;
			}
			else if (a is List<object> && b is List<object>)
			{
				var ret = CompareLists(a, b);
				if (ret != 0) return ret;
			}
			else
			{
				if (a is int)
					return Compare(new List<object>() { a }, b);
				else
					return Compare(a, new List<object>() { b });
			}
			return 0;
		}

		public int CompareNumbers(object a, object b)
        {
			if ((int)a < (int)b) return -1;
			if ((int)a > (int)b) return 1;
			return 0;
		}

		public int CompareLists(object a, object b)
		{
			var _a = a as List<object>;
			var _b = b as List<object>;

			for (var i = 0; i < _a.Count; i++)
			{
				if (i >= _b.Count) return 1;
				
				var ret = Compare(_a[i], _b[i]);
				if (ret != 0) return ret;
			}

			if (_a.Count < _b.Count) return -1;

			return 0;
		}
	}
}
