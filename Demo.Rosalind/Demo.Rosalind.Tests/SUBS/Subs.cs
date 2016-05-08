using System;
using System.Collections.Generic;

namespace Demo.Rosalind.Tests.SUBS
{
	public class Subs
	{
		public IEnumerable<int> GetSubstringCounts(string input)
		{
			var tuple = SeparateInput(input);
			string s = tuple.Item1;
			string t = tuple.Item2;

			for (int i = 0; i < s.Length - t.Length; i++)
			{
				if (s.Substring(i, t.Length) == t)
					yield return i + 1;	// Result is 1-based.
			}
		}

		/// <summary>
		/// Separate the input into string "s" and the substring "t".
		/// </summary>
		/// <returns>A tuple where item1 is "s" and item2 is "t".</returns>
		public Tuple<string, string> SeparateInput(string input)
		{
			string[] split = input.Split(new [] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
			return new Tuple<string, string>(split[0], split[1]);
		}
	}
}