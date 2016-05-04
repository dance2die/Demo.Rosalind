using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.REVC
{
	public class ReverseComplementTest
	{
		private readonly ITestOutputHelper _output;
		private const string SAMPLE_DATASET = "AAAACCCGGT";
		private readonly ReverseComplement _sut;

		public ReverseComplementTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new ReverseComplement();
		}

		[Fact]
		public void TestSampleDataset()
		{
			// from the web site http://rosalind.info/problems/rna/
			const string expected = "ACCGGGTTTT";

			string actual = _sut.ReverseComplementDataset(SAMPLE_DATASET);

			Assert.Equal(expected, actual);
		}
	}

	public class ReverseComplement
	{
		public string ReverseComplementDataset(string dataset)
		{
			var reverseComplementMap = new Dictionary<string, string>
			{
				{"A", "T"},
				{"T", "A"},
				{"C", "G"},
				{"G", "C"},
			};

			var sequence =
				from oneCharText in dataset.Reverse().Select(c => c.ToString())
				select oneCharText.Replace(oneCharText, reverseComplementMap[oneCharText]);
			return string.Join("", sequence);
		}
	}
}
