using Rosalind.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rosalind.Lib.Util;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.ORF
{
	public class OrfTest : BaseTest
	{
		private readonly Orf _sut;

		public OrfTest(ITestOutputHelper output) : base(output)
		{
			_sut = new Orf();
		}

		[Fact]
		public void TestConvertDnaToRnaString()
		{
			const string dnaString = "ATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCCGAGTAGCATCTC";
			//const string dnaString = "ATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCC";
			var rna = new Rna();
			var rnaString = rna.ConvertDnaToRna(dnaString);
			string proteinString = rna.ConvertRnaToProtein(rnaString, "|");

			_output.WriteLine("rnaString: {0}", rnaString);
			_output.WriteLine("proteinString: {0}", proteinString);

			ReverseComplement reverseComplement = new ReverseComplement();
			var reverseComplementedDnaString = reverseComplement.ReverseComplementDnaString(dnaString);

			rnaString = rna.ConvertDnaToRna(reverseComplementedDnaString);
			proteinString = rna.ConvertRnaToProtein(rnaString, "|");

			_output.WriteLine("Reverse Completed rnaString: {0}", rnaString);
			_output.WriteLine("Reverse Completed proteinString: {0}", proteinString);

			for (int i = 0; i < dnaString.Length; i++)
			{
				for (int j = dnaString.Length - 1; j >= 0; j--)
				{
					try
					{
						var substring = dnaString.Substring(i, j);
						//_output.WriteLine("substring: {0}", substring);
						rnaString = rna.ConvertDnaToRna(substring);
						proteinString = rna.ConvertRnaToProtein(rnaString, "|");

						//_output.WriteLine("rnaString: {0}", rnaString);
						if (proteinString.StartsWith("M") && proteinString.IndexOf("|") > 0)
						{
							_output.WriteLine("proteinString: {0}", proteinString);
							//_output.WriteLine(new string('=', 80));
						}
						break;
					}
					catch (Exception e)
					{
						//_output.WriteLine("Nothing in position: {0}", i);
					}

				}
			}
		}

		[Fact]
		public void TestGettingProteinStringsWithDelimiters()
		{
			const string dnaString = "AGCCATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCCGAGTAGCATCTCAG";

			foreach (string proteinString in GetProteinStringsWithDelimiters(dnaString))
			{
				_output.WriteLine("proteinString: {0}", proteinString);
			}

			ReverseComplement reverseComplement = new ReverseComplement();
			var reverseComplementedDnaString = reverseComplement.ReverseComplementDnaString(dnaString);

			foreach (string proteinString in GetProteinStringsWithDelimiters(reverseComplementedDnaString))
			{
				_output.WriteLine("Reverse Completed proteinString: {0}", proteinString);
			}
		}

		public IEnumerable<string> GetProteinStringsWithDelimiters(string dnaString, string delimiter = "|")
		{
			var rna = new Rna();

			List<string> proteinStrings = new List<string>();
			for (int i = 0; i < dnaString.Length; i++)
			{
				for (int j = dnaString.Length - 1; j - i > 0; j--)
				{
					var substring = dnaString.Substring(i, j - i);
					var rnaString = rna.ConvertDnaToRna(substring);
					var proteinString = rna.ConvertRnaToProtein(rnaString, delimiter);

					if (proteinString.StartsWith("M") &&
						!ListContainsSubstring(proteinStrings, proteinString) &&
						proteinString.IndexOf(delimiter) > 0)
					{
						proteinStrings.Add(proteinString);
						yield return proteinString;
					}
				}
			}

		}

		private bool ListContainsSubstring(IEnumerable<string> proteinStrings, string proteinString)
		{
			foreach (string processedProteinString in proteinStrings)
			{
				if (processedProteinString.IndexOf(proteinString) >= 0)
					return true;
			}

			return false;
		}

		[Fact]
		public void TestReturningOnlyNeededProteinStrings()
		{
			var expected = new []
			{
				"M|LTQVTWG|PRDLD|SLFWNKPE|SE|HL",
				"MGMTPRLGLESLLE|A|MIRVAS",
				"MLLGSFRLIPKETLIQVAGSSPCNLS|LHG",
				"M|PELATW"
			};

			const string dnaString = "AGCCATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCCGAGTAGCATCTCAG";
			IEnumerable<string> actual = _sut.GetProteinStrings(dnaString);

			Assert.True(expected.SequenceEqual(actual));
		}

		[Fact]
		public void TestParsingProteinStringsWithDelimeters()
		{
			var expected = new[]
			{
				"M",
				"MGMTPRLGLESLLE",
				"MTPRLGLESLLE",
				"MLLGSFRLIPKETLIQVAGSSPCNLS",
			};

			const string dnaString = "AGCCATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCCGAGTAGCATCTCAG";
			IEnumerable<string> proteinStringsWithDelimiters = _sut.GetProteinStrings(dnaString);
			IEnumerable<string> actual = _sut.GetDistinctCandidateProtineStringsFromOrf(proteinStringsWithDelimiters).ToList();

			Assert.True(expected.SequenceEqual(actual));
		}

		[Fact]
		public void ShowResult()
		{
			string fastaString = File.ReadAllText(@".\ORF\rosalind_orf.txt");
			FastaReader reader = new FastaReader();
			Dictionary<string, string> dictionary = reader.ParseDataset(fastaString);
			string dnaString = dictionary.First().Value;

			IEnumerable<string> proteinStringsWithDelimiters = _sut.GetProteinStrings(dnaString);
			IEnumerable<string> candidateStrings = _sut.GetDistinctCandidateProtineStringsFromOrf(proteinStringsWithDelimiters);

			string result = string.Join("\r\n", candidateStrings);
			_output.WriteLine(result);
		}
	}
}
