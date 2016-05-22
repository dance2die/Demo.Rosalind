using System;
using System.Collections.Generic;

namespace Rosalind.Lib.Util
{
	public class FastaReader
	{
		/// <summary>
		/// Parse FASTA texts into a dictoinary object instance.
		/// </summary>
		/// <param name="fastaText">Text containing Fasta Text</param>
		/// <returns>
		/// Parsed FASTA information in a dictionary
		/// 
		/// Key: Identifier
		/// Value: Sequence
		/// </returns>
		public Dictionary<string, string> ParseDataset(string fastaText)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			var lines = fastaText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			string previousKey = "";
			foreach (string line in lines)
			{
				if (line.StartsWith(">"))
				{
					// Remove ">" from the key
					var key = line.Split('>')[1];
					result[key] = string.Empty;

					previousKey = key;
				}
				else
				{
					result[previousKey] += line;
				}
			}

			return result;
		}

	}
}
