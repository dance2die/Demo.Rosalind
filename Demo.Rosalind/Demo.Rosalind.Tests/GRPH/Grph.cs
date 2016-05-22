using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosalind.Lib.Util;

namespace Demo.Rosalind.Tests.GRPH
{
	public class Grph
	{
		private const int SUFFIX_LENGTH = 3;	// given on website.

		public string OverlapGraphs(string input)
		{
			Dictionary<string, string> rows = ParseDataset(input);
			StringBuilder buffer = new StringBuilder();

			foreach (KeyValuePair<string, string> row in rows)
			{
				//var unprocessedRows = rows.Where(p => !processedKeys.Contains(p.Key));
				var unprocessedRows = rows.Where(p => p.Key != row.Key);
				foreach (var unprocessedRow in unprocessedRows)
				{
					if (SuffixMatchesPrefix(row.Value, unprocessedRow.Value))
						buffer.AppendLine(string.Format("{0} {1}", row.Key, unprocessedRow.Key));
				}
			}

			return buffer.ToString();
		}

		private bool SuffixMatchesPrefix(string value1, string value2)
		{
			string suffix = value1.Substring(value1.Length - SUFFIX_LENGTH);
			string prefix = value2.Substring(0, SUFFIX_LENGTH);

			return string.Equals(suffix, prefix, StringComparison.InvariantCultureIgnoreCase);
		}

		public Dictionary<string, string> ParseDataset(string dataset)
		{
			FastaReader reader = new FastaReader();
			return reader.ParseDataset(dataset);
		}
	}
}