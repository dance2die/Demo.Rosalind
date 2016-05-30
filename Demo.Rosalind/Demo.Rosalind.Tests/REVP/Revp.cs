using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosalind.Lib;
using Rosalind.Lib.Util;

namespace Demo.Rosalind.Tests.REVP
{
	public class Revp
	{
		public string GetDnaReversePalindromeIndexesOutput(string fastaString)
		{
			var indexes = GetDnaReversePalindromeIndexes(fastaString);
			StringBuilder buffer = new StringBuilder();

			foreach (Tuple<int, int> index in indexes)
			{
				buffer.AppendFormat("{0} {1}", index.Item1, index.Item2).AppendLine();
			}

			// Need to trim the last newline characters added by "AppenedLine" for the last item.
			return buffer.ToString().TrimEnd();
		}

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