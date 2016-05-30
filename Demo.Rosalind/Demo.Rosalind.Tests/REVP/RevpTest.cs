using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rosalind.Lib;
using Rosalind.Lib.Util;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.REVP
{
	/// <summary>
	/// Test case description:
	///		Find All DNA reverse compliments with length between 4 and 12.
	///		Find All indexes of DNA reverse compliments with length between 4 and 12.
	/// </summary>
	public class RevpTest : BaseTest
	{
		private readonly Revp _sut = new Revp();
		private const string SAMPLE_DATASET = @">Rosalind_24
TCAATGCATGCGGGTCTATATGCAT";

		public RevpTest(ITestOutputHelper output) : base(output)
		{
		}

		[Fact]
		public void FindAllDnaReverseComplimentsIndexesAndLengthsBetween4and12()
		{
			var expected = new List<Tuple<int, int>>
			{
				new Tuple<int, int>(4, 6),
				new Tuple<int, int>(5, 4),
				new Tuple<int, int>(6, 6),
				new Tuple<int, int>(7, 4),
				new Tuple<int, int>(17, 4),
				new Tuple<int, int>(18, 4),
				new Tuple<int, int>(20, 6),
				new Tuple<int, int>(21, 4)
			};

			IEnumerable<Tuple<int, int>> actual = _sut.GetDnaReversePalindromeIndexes(SAMPLE_DATASET).ToList();

			Assert.True(expected.SequenceEqual(actual));
		}
	}

	public class Revp
	{
		public IEnumerable<Tuple<int, int>> GetDnaReversePalindromeIndexes(string fastaString)
		{
			// We are finding compliments with length between 4 and 12.
			const int minimumLength = 4;
			const int maximumLength = 12;

			var reverseComplementUtil = new ReverseComplement();
			string dnaString = new FastaReader().ParseDataset(fastaString).First().Value;

			for (int startIndex = 0; startIndex <= dnaString.Length - minimumLength; startIndex++)
			{
				var lengthLimit = Math.Min(maximumLength, dnaString.Length - startIndex);
				for (int length = minimumLength; length <= lengthLimit ; length++)
				{
					var substring = dnaString.Substring(startIndex, length);
					var reverseComplement = reverseComplementUtil.ReverseComplementDnaString(substring);

					if (string.Compare(substring, reverseComplement, StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						// startIndex + 1 because answer requires a 1-base index
						yield return new Tuple<int, int>(startIndex + 1, substring.Length);
					}
				}
			}
		}
	}
}
