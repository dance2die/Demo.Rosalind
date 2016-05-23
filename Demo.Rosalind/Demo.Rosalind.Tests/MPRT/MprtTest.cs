using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Rosalind.Lib.Util;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.MPRT
{
	/// <summary>
	/// Test Scenarios
	/// 
	/// Download sample FASTA from www.uniprot.org.
	///		Parse FASTA and find N-glycosylation motif locations in FASTA sequence
	///		Combine found locations into a string.
	/// 
	/// Parse N-glycosylation motif
	/// 
	/// Parse Sample DataSet.
	/// 
	/// Build UniProt URL for FASTA File
	/// 
	/// Download FASTA files
	///		Parse FASTA file
	///		Find locations of N-glycosylation motifs
	///		Combine locations into a string
	/// </summary>
	public class MprtTest : BaseTest
	{
		private readonly Mprt _sut;
		private const string SAMPLE_DATASET = @"A2Z669
B5ZC00
P07204_TRBM_HUMAN
P20840_SAG1_YEAST";

		public MprtTest(ITestOutputHelper output) : base(output)
		{
			_sut = new Mprt();
		}

		[Fact]
		public void TestNGetNGlycosylationLocations()
		{
			const string sampleInput = "MKNKFKTQEELVNHLKTVGFVFANSEIYNGLANAWDYGPLGVLLKNNLKNLWWKEFVTKQKDVVGLDSAIILNPLVWKASGHLDNFSDPLIDCKNCKARYRADKLIESFDENIHIAENSSNEEFAKVLNDYEISCPTCKQFNWTEIRHFNLMFKTYQGVIEDAKNVVYLRPETAQGIFVNFKNVQRSMRLHLPFGIAQIGKSFRNEITPGNFIFRTREFEQMEIEFFLKEESAYDIFDKYLNQIENWLVSACGLSLNNLRKHEHPKEELSHYSKKTIDFEYNFLHGFSELYGIAYRTNYDLSVHMNLSKKDLTYFDEQTKEKYVPHVIEPSVGVERLLYAILTEATFIEKLENDDERILMDLKYDLAPYKIAVMPLVNKLKDKAEEIYGKILDLNISATFDNSGSIGKRYRRQDAIGTIYCLTIDFDSLDDQQDPSFTIRERNSMAQKRIKLSELPLYLNQKAHEDFQRQCQK";

			var expected = new List<int> {85, 118, 142, 306, 395};

			var actual = _sut.GetNGlycosylationLocations(sampleInput);

			Assert.True(expected.SequenceEqual(actual));
		}

		[Theory]
		[InlineData("A2Z669", "./MPRT/A2Z669.fasta.txt", "")]
		[InlineData("B5ZC00", "./MPRT/B5ZC00.fasta.txt", "85 118 142 306 395")]
		[InlineData("P07204_TRBM_HUMAN", "./MPRT/P07204_TRBM_HUMAN.fasta.txt", "47 115 116 382 409")]
		[InlineData("P20840_SAG1_YEAST", "./MPRT/P20840_SAG1_YEAST.fasta.txt", "79 109 135 248 306 348 364 402 485 501 614")]
		public void TestNGlycosylationLocations(string uniprotId, string fastaFilePath, string expected)
		{
			string fastaText = File.ReadAllText(fastaFilePath);

			string actual = _sut.GetNGlycosylationLocationString(uniprotId, fastaText);

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("A2Z669", "http://www.uniprot.org/uniprot/A2Z669.fasta")]
		[InlineData("B5ZC00", "http://www.uniprot.org/uniprot/B5ZC00.fasta")]
		[InlineData("P07204_TRBM_HUMAN", "http://www.uniprot.org/uniprot/P07204_TRBM_HUMAN.fasta")]
		[InlineData("P20840_SAG1_YEAST", "http://www.uniprot.org/uniprot/P20840_SAG1_YEAST.fasta")]
		public void TestUniprotFastaLink(string uniprotId, string expected)
		{
			string actual = _sut.GetUniprotUrl(uniprotId);

			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("http://www.uniprot.org/uniprot/A2Z669.fasta", "./MPRT/A2Z669.fasta.txt")]
		[InlineData("http://www.uniprot.org/uniprot/B5ZC00.fasta", "./MPRT/B5ZC00.fasta.txt")]
		[InlineData("http://www.uniprot.org/uniprot/P07204_TRBM_HUMAN.fasta", "./MPRT/P07204_TRBM_HUMAN.fasta.txt")]
		[InlineData("http://www.uniprot.org/uniprot/P20840_SAG1_YEAST.fasta", "./MPRT/P20840_SAG1_YEAST.fasta.txt")]
		public async void TestDownloadingFastaFilesFromUniprot(string uniprotUrl, string fastaFilePath)
		{
			// Download FASTA file.
			var remoteFastaString = await _sut.GetFastaFileByUniprotUrl(uniprotUrl);

			// Get local FASTA file.
			var localFastaString = File.ReadAllText(fastaFilePath);

			// Parse remote and local FASTA strings
			FastaReader reader = new FastaReader();
			var remoteDictionary = reader.ParseDataset(remoteFastaString);
			var localDictionary = reader.ParseDataset(localFastaString);

			// Compare sequence strings of first item in the dictionary.
			string remoteSequence = remoteDictionary.First().Value;
			string localSequence = localDictionary.First().Value;

			Assert.Equal(localSequence, remoteSequence);
		}

		[Fact]
		public async void TestSampleDataset()
		{
			const string expected = @"B5ZC00
85 118 142 306 395
P07204_TRBM_HUMAN
47 115 116 382 409
P20840_SAG1_YEAST
79 109 135 248 306 348 364 402 485 501 614";

			string actual = await _sut.BuildNGlycosylationOutput(SAMPLE_DATASET);

			Assert.Equal(expected, actual);
		}
	}

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
