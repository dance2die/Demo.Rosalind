using System.Collections.Generic;
using System.Linq;

namespace Rosalind.Lib
{
	public class ReverseComplement
	{
		public string ReverseComplementDataset(string dnaString)
		{
			var reverseComplementMap = new Dictionary<string, string>
			{
				{"A", "T"},
				{"T", "A"},
				{"C", "G"},
				{"G", "C"},
			};

			var sequence =
				from oneCharText in dnaString.Reverse().Select(c => c.ToString())
				select oneCharText.Replace(oneCharText, reverseComplementMap[oneCharText]);
			return string.Join("", sequence);
		}
	}
}