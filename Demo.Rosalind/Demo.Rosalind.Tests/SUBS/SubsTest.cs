using System;
using System.Collections.Generic;
using System.IO;
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

		[Fact]
		public void ShowResult()
		{
			string inputText = File.ReadAllText(@".\SUBS\rosalind_subs.txt");
			IEnumerable<int> actual = _sut.GetSubstringCounts(inputText);
			string result = string.Join(" ", actual.Select(i => i.ToString()));

			_output.WriteLine(result);
		}
	}
}
