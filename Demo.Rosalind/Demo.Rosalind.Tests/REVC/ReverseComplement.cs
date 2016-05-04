using System.Collections.Generic;
using System.Linq;

namespace Demo.Rosalind.Tests.REVC
{
	public class ReverseComplement
	{
		public string ReverseComplementDataset(string dataset)
		{
			var reverseComplementMap = new Dictionary<string, string>
			{
				{"A", "T"},
				{"T", "A"},
				{"C", "G"},
				{"G", "C"},
			};

			var sequence =
				from oneCharText in dataset.Reverse().Select(c => c.ToString())
				select oneCharText.Replace(oneCharText, reverseComplementMap[oneCharText]);
			return string.Join("", sequence);
		}
	}
}