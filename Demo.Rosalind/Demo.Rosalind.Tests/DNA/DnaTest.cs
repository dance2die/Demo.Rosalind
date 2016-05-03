using System.Collections.Generic;
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
		public void SatisfySampleDataSet()
		{
			var expected = new Dictionary<string, int>
			{
				{"A", 20}, {"C", 12}, {"G", 17}, {"T", 21} 
			};

			var actual = _sut.CountDnaSymbols(SAMPLE_DATASET);

			Assert.True(actual.SequenceEqual(expected));
		}
	}

	public class Dna
	{
		private readonly string[] _dnaSymbols = { "A", "C", "G", "T" };

		public Dictionary<string, int> CountDnaSymbols(string dataset)
		{
			var result = new Dictionary<string, int>();

			foreach (string dnaSymbol in _dnaSymbols)
			{
				int symbolCount = dataset.ToCharArray().Select(c => c.ToString()).Count(s => s == dnaSymbol);
				result.Add(dnaSymbol, symbolCount);
			}

			return result;
		}
	}
}
