using System.IO;
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

		[Fact]
		public void ShowResultForFile()
		{
			const string filePath = @".\REVC\rosalind_revc.txt";
			string dataset = File.ReadAllText(filePath);

			string result = _sut.ReverseComplementDataset(dataset);

			_output.WriteLine(result);
		}
	}
}
