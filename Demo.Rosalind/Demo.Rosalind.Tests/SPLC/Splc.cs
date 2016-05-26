using System.Collections.Generic;
using System.Linq;
using Rosalind.Lib;
using Rosalind.Lib.Util;

namespace Demo.Rosalind.Tests.SPLC
{
	public class Splc
	{
		public string ConvertToProteinAfterRemovingIntrons(string fastaString)
		{
			string dnaString = TranscribeDnaString(fastaString);

			var rna = new Rna();
			string rnaString = rna.ConvertDnaToRna(dnaString);

			return rna.ConvertRnaToProtein(rnaString);
		}

		public string TranscribeDnaString(string fastaString)
		{
			FastaReader reader = new FastaReader();
			var fasta = reader.ParseDataset(fastaString);

			string dnaString = fasta.First().Value;
			const int skipCount = 1;	// skip the first one
			var introns = fasta.Skip(skipCount);

			// Remove introns
			foreach (KeyValuePair<string, string> intron in introns)
			{
				dnaString = dnaString.Replace(intron.Value, "");
			}

			return dnaString;
		}
	}
}