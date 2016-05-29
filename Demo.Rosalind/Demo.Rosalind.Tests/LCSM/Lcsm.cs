using System.Collections.Generic;
using System.Linq;
using Rosalind.Lib.Util;

namespace Demo.Rosalind.Tests.LCSM
{
	public class Lcsm
	{
		public IEnumerable<string> GetLongestCommonDenominatorStrings2(string fastaString)
		{
			var fasta = new FastaReader().ParseDataset(fastaString);

			var allStrings = fasta.Select(pair => pair.Value).ToList();
			string shortest = allStrings.OrderBy(s => s.Length).First();
			IEnumerable<string> shortedSubstrings = GetAllSubstrings(shortest).OrderByDescending(s => s.Length);
			string[] others = allStrings.Where(s => s != shortest).ToArray();

			List<string> longestSubstrings = new List<string>();
			foreach (string longestSubstring in shortedSubstrings)
			{
				bool allContains = others.All(s => s.Contains(longestSubstring));
				if (allContains)
					longestSubstrings.Add(longestSubstring);
			}

			var maxLength = longestSubstrings.Max(str => str.Length);
			return longestSubstrings.Where(str => str.Length == maxLength);
		}

		// http://stackoverflow.com/a/13509460/4035
		private static IEnumerable<string> GetAllSubstrings(string word)
		{
			return from charIndex1 in Enumerable.Range(0, word.Length)
				from charIndex2 in Enumerable.Range(0, word.Length - charIndex1 + 1)
				where charIndex2 > 0
				select word.Substring(charIndex1, charIndex2);
		}

		//public List<string> GetLongestCommonDenominatorStrings(string value1, string value2)
		//{
		//	string comparisonString = string.Empty;
		//	List<string> candidateDenominators = new List<string>();

		//	// working version.
		//	for (int i = 0; i < value1.Length; i++)
		//	{
		//		for (int length = 1; length <= value1.Length - i; length++)
		//		{
		//			comparisonString = value1.Substring(i, length);
		//			if (value2.IndexOf(comparisonString) >= 0)
		//			{
		//				candidateDenominators.Add(comparisonString);
		//			}
		//			else
		//				break;
		//		}
		//	}

		//	// Backwards good
		//	//for (int i = 0; i < value1.Length; i++)
		//	//{
		//	//	for (int length = value1.Length - i; length >= 1; length--)
		//	//	{
		//	//		comparisonString = value1.Substring(i, length);
		//	//		if (value2.IndexOf(comparisonString) >= 0)
		//	//		{
		//	//			candidateDenominators.Add(comparisonString);
		//	//		}
		//	//	}
		//	//}

		//	// Find the longest length of demoninators
		//	var maxLength = candidateDenominators.Max(candidateDenominator => candidateDenominator.Length);
		//	return candidateDenominators
		//		.Where(candidateDenominator => candidateDenominator.Length == maxLength)
		//		.Distinct()
		//		.ToList();
		//}

		//public string GetCommonLongestString(string fastaString)
		//{
		//	var fasta = new FastaReader().ParseDataset(fastaString);
		//	var commonDenominators = new List<string>();

		//	foreach (KeyValuePair<string, string> pair1 in fasta)
		//	{
		//		foreach (KeyValuePair<string, string> pair2 in fasta)
		//		{
		//			if (pair1.Value == pair2.Value)
		//				commonDenominators.Add(pair1.Value);

		//			commonDenominators.AddRange(GetLongestCommonDenominatorStrings(pair1.Value, pair2.Value));
		//		}
		//	}

		//	var maxLength = commonDenominators.Max(candidateDenominator => candidateDenominator.Length);
		//	return commonDenominators
		//		.First(candidateDenominator => candidateDenominator.Length == maxLength);
		//}
	}
}