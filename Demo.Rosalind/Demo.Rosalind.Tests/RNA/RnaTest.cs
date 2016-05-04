using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.RNA
{
	public class RnaTest
	{
		private readonly ITestOutputHelper _output;
		private const string SAMPLE_DATASET = "GATGGAACTTGACTACGTAAATT";
		private readonly Rna _sut;

		public RnaTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Rna();
		}

		[Fact]
		public void TestSampleDataset()
		{
			// from the web site http://rosalind.info/problems/rna/
			const string expected = "GAUGGAACUUGACUACGUAAAUU";

			string actual = _sut.TranscribeDnaToRna(SAMPLE_DATASET);

			Assert.Equal(expected, actual);
		}
	}

	public class Rna
	{
		public string TranscribeDnaToRna(string dataset)
		{
			const char fromChar = 'T';
			const char toChar = 'U';

			StringBuilder buffer = new StringBuilder(dataset.Length);
			foreach (char c in dataset)
			{
				char charToAppend = c;
				if (c == fromChar)
					charToAppend = toChar;

				buffer.Append(charToAppend);
			}

			return buffer.ToString();
		}
	}
}
