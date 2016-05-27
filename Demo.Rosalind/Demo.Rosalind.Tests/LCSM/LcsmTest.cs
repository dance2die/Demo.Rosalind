using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rosalind.Lib.Util;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions;

namespace Demo.Rosalind.Tests.LCSM
{
	public class LcsmTest : BaseTest
	{
		private readonly Lcsm _sut;
		private const string SAMPLE_DATASET = @">Rosalind_1
GATTACA
>Rosalind_2
TAGACCA
>Rosalind_3
ATACA";

		public static IEnumerable<object[]> DnaStrings
		{
			get
			{
				return new []
				{
					new object[] { "AAABBB", "AAACCCBBB", new []{"AAA", "BBB"} },
					new object[] { "AAACCCBBB", "AAADDDBBB", new []{"AAA", "BBB"} },
				};
			}
		}

		public LcsmTest(ITestOutputHelper output) : base(output)
		{
			_sut = new Lcsm();
		}

		[Theory]
		//[MemberData(nameof(DnaStrings))]
		[InlineData("AAABBB", "AAACCCBBB", new [] { "AAA", "BBB" })]
		[InlineData("AAACCCBBB", "AAADDDBBB", new[] { "AAA", "BBB" })]
		[InlineData("XXXTTT", "XXXTBBB", new[] { "XXXT" })]
		public void TestGettingCommonLongestStringList(string value1, string value2, IEnumerable<string> expected)
		{
			var actual = _sut.GetLongestCommonDenominatorStrings(value1, value2);

			Assert.True(expected.SequenceEqual(actual));
		}

		[Fact]
		public void TestGettingTheCommonLongestString()
		{
			const string expected = "AC";

			string actual = _sut.GetCommonLongestString(SAMPLE_DATASET);

			Assert.Equal(expected, actual);
		}
	}

	public class Lcsm
	{
		public List<string> GetLongestCommonDenominatorStrings(string value1, string value2)
		{
			string comparisonString1 = string.Empty;
			string comparisonString2 = string.Empty;

			List<string> candidateDenominators = new List<string>();
			for (int i = 0; i < value1.Length; i++)
			{
				for (int j = 0; j < value2.Length; j++)
				{
					// start comparison
					if (value1[i] == value2[j])
					{
						comparisonString1 += value1[i];
						comparisonString2 += value2[j];
					}

					if (value1[i] == value2[j] && comparisonString1 != comparisonString2)
					{
						candidateDenominators.Add(comparisonString2);
						comparisonString1 = string.Empty;
					}
				}

				comparisonString2 = string.Empty;
			}

			// Find the longest length of demoninators
			var maxLength = candidateDenominators.Max(candidateDenominator => candidateDenominator.Length);
			return candidateDenominators
				.Where(candidateDenominator => candidateDenominator.Length == maxLength)
				.Distinct()
				.ToList();
		}

		public string GetCommonLongestString(string fastaString)
		{
			var fasta = new FastaReader().ParseDataset(fastaString);
			var commonDenominators = new List<string>();

			foreach (KeyValuePair<string, string> pair1 in fasta)
			{
				foreach (KeyValuePair<string, string> pair2 in fasta)
				{
					if (pair1.Value == pair2.Value)
						commonDenominators.Add(pair1.Value);

					commonDenominators.AddRange(GetLongestCommonDenominatorStrings(pair1.Value, pair2.Value));
				}
			}

			var maxLength = commonDenominators.Max(candidateDenominator => candidateDenominator.Length);
			return commonDenominators
				.First(candidateDenominator => candidateDenominator.Length == maxLength);

			//string commonDenominator = fasta.First().Value;
			//// Start from the 2nd element
			//foreach (KeyValuePair<string, string> pair in fasta.Skip(1))
			//{
			//	commonDenominators.AddRange(GetLongestCommonDenominatorStrings(commonDenominator, pair.Value));
			//}

			//return commonDenominator;
		}
	}
}
