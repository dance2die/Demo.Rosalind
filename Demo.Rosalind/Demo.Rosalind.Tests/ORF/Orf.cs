using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Rosalind.Lib;

namespace Demo.Rosalind.Tests.ORF
{
	public class Orf
	{
		private const string PATTERN = @"(?=(M[^\|]*)\|).";

		public IEnumerable<string> GetDistinctCandidateProtineStringsFromOrf(IEnumerable<string> proteinStringsWithDelimiters)
		{
			Regex regex = new Regex(PATTERN, RegexOptions.IgnoreCase | RegexOptions.Compiled);

			List<Match> matchResult = new List<Match>();
			foreach (var proteinStringsWithDelimiter in proteinStringsWithDelimiters)
			{
				var matches = regex.Matches(proteinStringsWithDelimiter);
				matchResult.AddRange(matches.OfType<Match>());
			}

			return matchResult
				.Select(match => match.Groups[1].Value)
				.Distinct();
		}

		public IEnumerable<string> GetProteinStrings(string dnaString)
		{
			// Return in-order strings
			foreach (string proteinString in GetProteinStringsWithDelimiters(dnaString))
			{
				yield return proteinString;
			}

			// Return reverse complement strings
			ReverseComplement reverseComplement = new ReverseComplement();
			var reverseComplementedDnaString = reverseComplement.ReverseComplementDnaString(dnaString);
			foreach (string proteinString in GetProteinStringsWithDelimiters(reverseComplementedDnaString))
			{
				yield return proteinString;
			}
		}

		public IEnumerable<string> GetProteinStringsWithDelimiters(string dnaString, string delimiter = "|")
		{
			var rna = new Rna();

			List<string> proteinStrings = new List<string>();
			for (int i = 0; i < dnaString.Length; i++)
			{
				for (int j = dnaString.Length - 1; j - i > 0; j--)
				{
					var substring = dnaString.Substring(i, j - i);
					var rnaString = rna.ConvertDnaToRna(substring);
					var proteinString = rna.ConvertRnaToProtein(rnaString, delimiter);

					if (proteinString.StartsWith("M") &&
					    !ListContainsSubstring(proteinStrings, proteinString) &&
					    proteinString.IndexOf(delimiter) > 0)
					{
						proteinStrings.Add(proteinString);
						yield return proteinString;
					}
				}
			}
		}

		private bool ListContainsSubstring(IEnumerable<string> proteinStrings, string proteinString)
		{
			foreach (string processedProteinString in proteinStrings)
			{
				if (processedProteinString.IndexOf(proteinString) >= 0)
					return true;
			}

			return false;
		}
	}
}