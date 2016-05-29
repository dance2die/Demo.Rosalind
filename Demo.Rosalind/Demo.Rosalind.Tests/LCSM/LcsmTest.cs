using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

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
				return new[]
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

		//[Fact]
		//public void TestGettingTheCommonLongestString()
		//{
		//	const string expected = "AC";

		//	string actual = _sut.GetCommonLongestString(SAMPLE_DATASET);

		//	Assert.Equal(expected, actual);
		//}

		//[Theory]
		////[MemberData(nameof(DnaStrings))]
		//[InlineData("AAABBB", "AAACCCBBB", new[] { "AAA", "BBB" })]
		//[InlineData("AAACCCBBB", "AAADDDBBB", new[] { "AAA", "BBB" })]
		//[InlineData("XXXTTT", "XXXTBBB", new[] { "XXXT" })]
		//[InlineData("XXXTTAAAB", "XXXBBBAAAB", new[] { "AAAB" })]
		//[InlineData("GATTACA", "TAGACCA", new[] { "GA", "TA", "AC", "CA" })]
		//[InlineData("TAGACCA", "ATACA", new[] { "TA", "AC", "CA" })]
		//public void TestGettingCommonLongestStringList(string value1, string value2, IEnumerable<string> expected)
		//{
		//	var actual = _sut.GetLongestCommonDenominatorStrings(value1, value2);

		//	Assert.True(expected.SequenceEqual(actual));
		//}

		//[Fact]
		//public void TestSampleDataWithOutput()
		//{
		//	// answer given on the website.
		//	string[] expected = { "TA", "AC", "CA" };

		//	var fasta = new FastaReader().ParseDataset(SAMPLE_DATASET);

		//	//List<string> commonStrings = new List<string>();
		//	//var firstValue = fasta.First().Value;
		//	//foreach (KeyValuePair<string, string> pair in fasta.Skip(1))
		//	//{
		//	//	List<string> longestCommonDenominatorStrings = 
		//	//		_sut.GetLongestCommonDenominatorStrings(firstValue, pair.Value);
		//	//	commonStrings.AddRange(longestCommonDenominatorStrings);
		//	//}
		//	//Dictionary<int, List<string>> commonDenominatorDictionary = new Dictionary<int, List<string>>();
		//	//int key = 0;
		//	//foreach (KeyValuePair<string, string> pair1 in fasta)
		//	//{
		//	//	foreach (KeyValuePair<string, string> pair2 in fasta)
		//	//	{
		//	//		if (pair1.Value == pair2.Value)
		//	//			continue;
		//	//		else
		//	//			key++;

		//	//		List<string> longestCommonDenominatorStrings =
		//	//			_sut.GetLongestCommonDenominatorStrings(pair1.Value, pair2.Value);
		//	//		//commonStrings.AddRange(longestCommonDenominatorStrings);
		//	//		commonDenominatorDictionary.Add(key, longestCommonDenominatorStrings);
		//	//	}
		//	//}

		//	//List<string> finalCommonDenominators = commonDenominatorDictionary.First().Value;
		//	//foreach (KeyValuePair<int, List<string>> pair in commonDenominatorDictionary.Skip(1))
		//	//{
		//	//	finalCommonDenominators = finalCommonDenominators.Union(pair.Value).ToList();
		//	//}



		//	//List<string> union = new List<string>();
		//	//foreach (string final1 in finalCommonDenominators)
		//	//{
		//	//	foreach (string final2 in finalCommonDenominators)
		//	//	{
		//	//		List<string> longestCommonDenominatorStrings = _sut.GetLongestCommonDenominatorStrings(final1, final2);
		//	//		if (union.Count == 0)
		//	//			union.AddRange(longestCommonDenominatorStrings);
		//	//		else
		//	//			union = union.Union(longestCommonDenominatorStrings).ToList();
		//	//	}
		//	//}

		//	Assert.True(expected.SequenceEqual(union.Distinct()));
		//}

		[Fact]
		public void TestSampleDataWithOutput2()
		{
			string[] expected = { "TA", "AC", "CA" };

			IEnumerable<string> actual = _sut.GetLongestCommonDenominatorStrings2(SAMPLE_DATASET);

			Assert.True(expected.SequenceEqual(actual));
		}

		[Fact]
		public void ShowResult()
		{
			string fastaString = File.ReadAllText(@".\LCSM\rosalind_lcsm.txt");

			IEnumerable<string> result = _sut.GetLongestCommonDenominatorStrings2(fastaString);

			foreach (string value in result)
			{
				_output.WriteLine(value);
			}
		}
	}
}
