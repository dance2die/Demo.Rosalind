using System.Collections.Generic;
using System.Linq;

namespace Demo.Rosalind.Tests.DNA
{
	public class Dna
	{
		private readonly string[] _dnaSymbols = { "A", "C", "G", "T" };

		public string BuildOutputString(Dictionary<string, int> output)
		{
			var sortedOutput = new SortedDictionary<string, int>(output);
			var values = sortedOutput.Select(pair => pair.Value.ToString());
			return string.Join(" ", values);
		}

		public Dictionary<string, int> CountDnaSymbols(string dataset)
		{
			var result = new Dictionary<string, int>();

			foreach (string dnaSymbol in _dnaSymbols)
			{
				int symbolCount = dataset.ToCharArray().Select(c => c.ToString()).Count(s => s == dnaSymbol);
				result.Add(dnaSymbol, symbolCount);
			}

			return result;
		}
	}
}