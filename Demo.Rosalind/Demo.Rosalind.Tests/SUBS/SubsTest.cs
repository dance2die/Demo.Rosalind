using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.SUBS
{
	public class SubsTest
	{
		private readonly ITestOutputHelper _output;
		private readonly Subs _sut;
		private const string SAMPLE_DATASET = @"GATATATGCATATACTT
ATAT";

		public SubsTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Subs();
		}

		[Fact]
		public void TestSeparateSampleDataSet()
		{
			var expected = new Tuple<string, string>("GATATATGCATATACTT", "ATAT");

			Tuple<string, string> actual = _sut.SeparateInput(SAMPLE_DATASET);

			Assert.True(expected.Equals(actual));
		}

		[Fact]
		public void TestSampleDataSet()
		{
			var expected = new[] { 2, 4, 10 };

			IEnumerable<int> actual = _sut.GetSubstringCounts(SAMPLE_DATASET);

			Assert.True(expected.SequenceEqual(actual));
		}
	}

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
