using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.PRTM
{
	public class PrtmTest
	{
		private readonly ITestOutputHelper _output;
		private readonly Prtm _sut;
		private const string SAMPLE_DATASET = "SKADYEK";

		public PrtmTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Prtm();
		}

		[Fact]
		public void TestSampleDataSet()
		{
			const double expected = 821.392;

			double actual = _sut.GetProteineMass(SAMPLE_DATASET);

			const int precision = 6;
			Assert.Equal(expected, actual, precision);
		}

		[Fact]
		public void ShowResult()
		{
			string inputText = File.ReadAllText(@".\PRTM\rosalind_prtm.txt");

			double result = _sut.GetProteineMass(inputText);

			_output.WriteLine(result.ToString());
		}
	}
}
