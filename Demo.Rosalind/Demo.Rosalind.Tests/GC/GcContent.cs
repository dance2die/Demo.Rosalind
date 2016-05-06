using System;
using System.Collections.Generic;

namespace Demo.Rosalind.Tests.GC
{
	public class GCContent
	{
		public string GetHighestGCContentText(string input)
		{
			Dictionary<string, string> gcContentMap = ComputeGCContents(input);
			string key = GetHighestGCContent(gcContentMap);

			const int validDeicmals = 6;
			var gcContent = Math.Round(GetGCContent(gcContentMap[key]), validDeicmals);
			return string.Format("{0}{1}{2}", key, Environment.NewLine, gcContent);
		}

		public string GetHighestGCContent(Dictionary<string, string> gcContentMap)
		{
			string maxGCContentKey = string.Empty;
			decimal maxGCContent = 0;
			foreach (KeyValuePair<string, string> content in gcContentMap)
			{
				decimal gcContent = GetGCContent(content.Value);
				if (maxGCContent < gcContent)
				{
					maxGCContent = gcContent;
					maxGCContentKey = content.Key;
				}
			}

			return maxGCContentKey;
		}

		public decimal GetGCContent(string dnaString)
		{
			int gCount = 0;
			int cCount = 0;
			foreach (var dnaChar in dnaString)
			{
				if (dnaChar == 'C') cCount++;
				if (dnaChar == 'G') gCount++;
			}

			int countSum = gCount + cCount;
			if (countSum == 0)
				return 0;

			decimal result = (decimal) (countSum * 100) / dnaString.Length;
			return result;
		}

		public int ParseFourDigitCode(string fastaId)
		{
			if (string.IsNullOrWhiteSpace(fastaId))
				throw new ArgumentException();

			const string fastaIdPrefix = "Rosalind_";
			if (!fastaId.StartsWith(fastaIdPrefix)) 
				throw new ArgumentException();

			string code = fastaId.Substring(fastaIdPrefix.Length);
			if (code.Length != 4)
				throw new ArgumentException();

			int result;
			bool isParsed = int.TryParse(code, out result);
			if (!isParsed)
				throw new ArgumentException();

			return result;
		}

		public IEnumerable<string> GetFastaIds(Dictionary<string, string> gcContent)
		{
			return gcContent.Keys;
		}

		public Dictionary<string, string> ComputeGCContents(string input)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			var splitted = input.Split(new[] { '\n', '\r', '>'}, StringSplitOptions.RemoveEmptyEntries);
			var currentKey = string.Empty;
			// if a line starts with this, then it's a key
			const string keyPrefix = "Rosalind";

			foreach (var line in splitted)
			{
				if (line.StartsWith(keyPrefix))
				{
					currentKey = line;
					result.Add(line, string.Empty);
				}
				else
				{
					result[currentKey] += line;
				}
			}

			return result;
		}
	}
}