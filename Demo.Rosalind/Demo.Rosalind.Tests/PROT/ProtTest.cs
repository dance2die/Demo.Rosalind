using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.PROT
{
	public class ProtTest
	{
		private readonly ITestOutputHelper _output;
		private readonly Prot _sut;
		private const string SAMPLE_DATASET = "AUGGCCAUGGCGCCCAGAACUGAGAUCAAUAGUACCCGUAUUAACGGGUGA";

		public ProtTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Prot();
		}

		[Fact]
		public void TestSampleDataSetResult()
		{
			const string expected = "MAMAPRTEINSTRING";

			string actual = _sut.EncodeRnaString(SAMPLE_DATASET);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestSampleDatasetFromFile()
		{
			const string expected = "MAMAPRTEINSTRING";

			string inputText = File.ReadAllText(@".\PROT\rosalind_prot.txt");
			string actual = _sut.EncodeRnaString(inputText);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ShowResult()
		{
			string inputText = File.ReadAllText(@".\PROT\rosalind_prot.txt");
			string result = _sut.EncodeRnaString(inputText);

			_output.WriteLine(result);
		}
	}
}
 