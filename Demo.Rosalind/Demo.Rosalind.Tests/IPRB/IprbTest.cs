using System;
using System.Globalization;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.IPRB
{
	public class IprbTest : BaseTest
	{
		private readonly Iprb _sut = new Iprb();
		private const string SAMPLE_DATASET = "2 2 2";

		public IprbTest(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public void TestResultAgainstSampleDataset()
		{
			const double expected = 0.78333D;

			double actual = _sut.GetProbabilityForDominantAllele(SAMPLE_DATASET);

			const int precision = 5;    // 0.00001
			Assert.Equal(expected, actual, precision);
		}

		[Fact]
		public void TestResultAgainstSampleDatasetUsingDifferentAlogorithm()
		{
			const double expected = 0.78333D;

			var actual = _sut.GetProbabilityForDominantAllele2(SAMPLE_DATASET);

			const int precision = 5;    // 0.00001
			Assert.Equal(expected, actual, precision);
		}

		[Fact]
		public void ShowResult()
		{
			string input = File.ReadAllText(@".\IPRB\rosalind_iprb.txt");

			double result = _sut.GetProbabilityForDominantAllele2(input);

			const int precision = 5;
			decimal result2 = Math.Round((decimal) result, precision);
			_output.WriteLine(result2.ToString(CultureInfo.InvariantCulture));
		}
	}
}
