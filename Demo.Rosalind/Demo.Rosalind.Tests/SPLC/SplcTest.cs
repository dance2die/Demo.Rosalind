using System.IO;
using Rosalind.Lib;
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

		[Fact]
		public void ShowResult()
		{
			string fastaString = File.ReadAllText(@".\SPLC\rosalind_splc.txt");

			string result = _sut.ConvertToProteinAfterRemovingIntrons(fastaString);

			_output.WriteLine(result);
		}
	}
}
