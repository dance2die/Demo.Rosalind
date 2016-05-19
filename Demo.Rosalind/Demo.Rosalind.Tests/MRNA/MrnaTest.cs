using System.IO;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.MRNA
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// Can't figure out how sample data is converted to sample output.
	/// http://recologia.com.br/2013/05/problema-rosalind-inferring-mrna-from-protein-mrna/
	/// </remarks>
	public class MrnaTest
	{
		private readonly ITestOutputHelper _output;
		private readonly Mrna _sut;
		private const string SAMPLE_DATASET = "MA";

		public MrnaTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Mrna();
		}

		[Fact]
		public void TestSampleDataSet()
		{
			// from the website: http://rosalind.info/problems/mrna/
			const int expected = 12;

			BigInteger actual = _sut.GetPossibleRnaStringCountFromProtein(SAMPLE_DATASET);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ShowResult()
		{
			string inputText = File.ReadAllText(@".\MRNA\rosalind_mrna.txt");

			BigInteger result = _sut.GetPossibleRnaStringCountFromProtein(inputText);

			_output.WriteLine(result.ToString());
		}
	}
}
