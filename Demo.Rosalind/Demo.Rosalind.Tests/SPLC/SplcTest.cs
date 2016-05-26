using System.Collections.Generic;
using System.Linq;
using Rosalind.Lib;
using Rosalind.Lib.Util;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.SPLC
{
	/// <summary>
	/// Parse FASTA input.
	/// First item value is a DNA string to translate.
	/// Rest of item values are introns that need to be removed from DNA string before transcribing.
	/// 
	/// Remove all instances of introns from the DNA string.
	/// Convert the DNA string to RNA string.
	/// Convert RNA to protein string.
	/// 
	/// Compare the protein string with the expected result.
	/// </summary>
	public class SplcTest : BaseTest
	{
		private readonly Splc _sut;
		private const string SAMPLE_DATASET = @">Rosalind_10
ATGGTCTACATAGCTGACAAACAGCACGTAGCAATCGGTCGAATCTCGAGAGGCATATGGTCACATGATCGGTCGAGCGTGTTTCAAAGTTTGCGCCTAG
>Rosalind_12
ATCGGTCGAA
>Rosalind_15
ATCGGTCGAGCGTGT";

		public SplcTest(ITestOutputHelper output) : base(output)
		{
			_sut = new Splc();
		}

		[Fact]
		public void TestConvertingStringIntoProtein()
		{
			// Removed introns manually from the given input on the web.
			const string dnaString = "ATGGTCTACATAGCTGACAAACAGCACGTAGCATCTCGAGAGGCATATGGTCACATGTTCAAAGTTTGCGCCTAG";

			Rna rna = new Rna();
			var rnaString = rna.ConvertDnaToRna(dnaString);
			string actual = rna.ConvertRnaToProtein(rnaString);

			_output.WriteLine(actual);
			const string expected = "MVYIADKQHVASREAYGHMFKVCA";
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestDnaStringAfterRemovingIntrons()
		{
			// Removed introns manually from the given input on the web.
			const string expected = "ATGGTCTACATAGCTGACAAACAGCACGTAGCATCTCGAGAGGCATATGGTCACATGTTCAAAGTTTGCGCCTAG";

			string actual = _sut.TranscribeDnaString(SAMPLE_DATASET);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TranscribeAndTranslateDnaToProteinAfterRemovingIntrons()
		{
			// Given on the website.
			const string expected = "MVYIADKQHVASREAYGHMFKVCA";

			string actual = _sut.ConvertToProteinAfterRemovingIntrons(SAMPLE_DATASET);

			Assert.Equal(expected, actual);
		}
	}

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
