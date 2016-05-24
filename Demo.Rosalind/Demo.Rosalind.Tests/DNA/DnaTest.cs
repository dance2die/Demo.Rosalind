using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.DNA
{
	public class DnaTest
	{
		private readonly ITestOutputHelper _output;
		private const string SAMPLE_DATASET = "AGCTTTTCATTCTGACTGCAACGGGCAATATGTCTCTGTGTGGATTAAAAAAAGAGTGTCTGATAGCAGC";
		private readonly Dna _sut;

		public DnaTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Dna();
		}

		[Fact]
		public void TestSampleDataSet()
		{
			var expected = new Dictionary<string, int>
			{
				{"A", 20}, {"C", 12}, {"G", 17}, {"T", 21} 
			};

			var actual = _sut.CountDnaSymbols(SAMPLE_DATASET);

			Assert.True(actual.SequenceEqual(expected));
		}

		[Fact]
		public void TestOutputStringOfDnaSymbolCount()
		{
			const string expected = "20 12 17 21";

			var output = _sut.CountDnaSymbols(SAMPLE_DATASET);
			var actual = _sut.BuildOutputString(output);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ShowResultForDownloadedDataSet()
		{
			string fileContent = File.ReadAllText(@".\DNA\rosalind_dna.txt");
			var output = _sut.CountDnaSymbols(fileContent);
			var result = _sut.BuildOutputString(output);

			_output.WriteLine(result);
		}
	}
}
