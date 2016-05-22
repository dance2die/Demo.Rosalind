using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
	}

	public class Mprt
	{
		private const string NGLYCOSYLATION_REGEX_PATTERN = "(?<NGrlyosylation>N[^P][ST][^P])";

		public string GetNGlycosylationLocationString(string uniprotId, string fastaText)
		{
			FastaReader reader = new FastaReader();
			Dictionary<string, string> dictionary = reader.ParseDataset(fastaText);
			string input = dictionary.First().Value;

			var locations = GetNGlycosylationLocations(input).Select(location => location.ToString());
			return string.Join(" ", locations);
		}

		//public IEnumerable<int> GetNGlycosylationLocations(string input)
		//{
		//	Regex regex = new Regex(NGLYCOSYLATION_REGEX_PATTERN, RegexOptions.Compiled | RegexOptions.IgnoreCase);
		//	MatchCollection matches = regex.Matches(input);
		//	foreach (Match match in matches)
		//	{
		//		// Need to add 1 to because match index is 0 based
		//		const int offset = 1;
		//		yield return match.Index + offset;
		//	}
		//}

		public IEnumerable<int> GetNGlycosylationLocations(string input)
		{
			input = "NNTSY";
			Regex regex = new Regex("(?<NGrlyosylation>N[^P][ST][^P])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			MatchCollection matches = regex.Matches(input);
			foreach (Match match in matches)
			{
				// Need to add 1 to because match index is 0 based
				const int offset = 1;
				yield return match.Index + offset;
			}
		}
	}
}
