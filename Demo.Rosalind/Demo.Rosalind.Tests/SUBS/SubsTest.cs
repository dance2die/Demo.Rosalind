using System;
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

			Tuple<string, string> actual = _sut.SeparateDataset(SAMPLE_DATASET);

			Assert.True(expected.Equals(actual));
		}

		//[Fact]
		//public void TestSampleDataSet()
		//{
		//	var expected = new[] {2, 4, 10};

		//	IEnumerable<int> actual = _sut.GetSubstringCounts();

		//	Assert.True(expected.SequenceEqual(actual));
		//}
	}

	public class Subs
	{
		/// <summary>
		/// Separate the input into string "s" and the substring "t".
		/// </summary>
		/// <returns>A tuple where item1 is "s" and item2 is "t".</returns>
		public Tuple<string, string> SeparateDataset(string input)
		{
			string[] split = input.Split(new [] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
			return new Tuple<string, string>(split[0], split[1]);
		}

		//public IEnumerable<int> GetSubstringCounts()
		//{

		//}
	}
}
