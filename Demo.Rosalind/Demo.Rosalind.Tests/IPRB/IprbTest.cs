using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	}

	public class Iprb
	{
		/// <summary>
		/// The probability that two randomly selected mating organisms will produce an individual possessing 
		/// a dominant allele (and thus displaying the dominant phenotype). 
		/// Assume that any two organisms can mate.
		/// </summary>
		/// <remarks>
		/// Answer explanation
		/// http://www.thagomizer.com/blog/2014/11/19/approaching-rosalind-problems.html
		/// <see cref="http://www.thagomizer.com/blog/2014/11/19/approaching-rosalind-problems.html"/>
		/// </remarks>
		public double GetProbabilityForDominantAllele(string input)
		{
			var splitted = input.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			double dominant = double.Parse(splitted[0]);
			double hetero = double.Parse(splitted[1]);
			double recessive = double.Parse(splitted[2]);

			double total = dominant + hetero + recessive;

			double recessiveToRecessive = (recessive / total) * ((recessive - 1) / (total - 1));
			double hetroToHetero = (hetero / total) * ((hetero - 1) / (total - 1));
			double hetroToRecessive = (hetero / total) * (recessive / (total - 1)) +
				(recessive / total) * (hetero / (total - 1));

			// 1/4 according to Punnet Square
			const double recessiveProbability = 0.25;
			// 1/2 according to Punnet Square
			const double hetroToRecessiveProbability = 0.5;
			double recessiveTotal = recessiveToRecessive +
				(hetroToHetero * recessiveProbability) +
				(hetroToRecessive * hetroToRecessiveProbability);

			return 1 - recessiveTotal;
		}
	}
}
