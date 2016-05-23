using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Rosalind.Lib.Util;

namespace Demo.Rosalind.Tests.MPRT
{
	public class Mprt
	{
		// http://stackoverflow.com/a/37370533/4035
		private const string NGLYCOSYLATION_REGEX_PATTERN = "(?=N[^P][ST][^P]).";
		private const string UNIPROT_URL_FORMAT = "http://www.uniprot.org/uniprot/{0}.fasta";

		public async Task<string> BuildNGlycosylationOutput(string uniprotIdsString)
		{
			// Parse Uniprot IDs
			string[] uniprotIds = uniprotIdsString.Split(
				new [] { Environment.NewLine, "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

			// Foreach ID in uniprot IDs,
			//	Download remote FASTA file.
			//	Parse the FASTA file.
			//	Calculate N-Glycosylation Location String
			//	Add the uniprot ID as a key and location string as value
			StringBuilder buffer = new StringBuilder();
			foreach (string uniprotId in uniprotIds)
			{
				// Download remote FASTA file.
				string uniprotUrl = GetUniprotUrl(uniprotId);
				string remoteFastaString = await GetFastaFileByUniprotUrl(uniprotUrl);

				// Calculate N-Glycosylation Location String
				var locationString = GetNGlycosylationLocationString(uniprotId, remoteFastaString);

				// Add the result to the buffer
				if (!string.IsNullOrWhiteSpace(locationString))
					buffer.AppendLine(uniprotId).AppendLine(locationString);
			}

			return buffer.ToString();
		}

		public string GetUniprotUrl(string uniprotId)
		{
			return string.Format(UNIPROT_URL_FORMAT, uniprotId);
		}

		public string GetNGlycosylationLocationString(string uniprotId, string fastaString)
		{
			FastaReader reader = new FastaReader();
			Dictionary<string, string> dictionary = reader.ParseDataset(fastaString);
			string input = dictionary.First().Value;

			var locations = GetNGlycosylationLocations(input).Select(location => location.ToString());
			return string.Join(" ", locations);
		}

		public IEnumerable<int> GetNGlycosylationLocations(string proteinSequence)
		{
			Regex regex = new Regex(NGLYCOSYLATION_REGEX_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase);
			MatchCollection matches = regex.Matches(proteinSequence);
			foreach (Match match in matches)
			{
				// Need to add 1 to because match index is 0 based
				const int offset = 1;
				yield return match.Index + offset;
			}
		}

		public async Task<string> GetFastaFileByUniprotUrl(string uniprotUrl)
		{
			HttpClient httpClient = new HttpClient();
			return await httpClient.GetStringAsync(uniprotUrl);
		}
	}
}