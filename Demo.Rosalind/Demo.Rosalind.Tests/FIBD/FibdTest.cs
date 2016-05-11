using System.IO;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.FIBD
{
	public class FibdTest
	{
		private readonly ITestOutputHelper _output;
		private readonly Fibd _sut;

		public FibdTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Fibd();
		}

		[Theory]
		[InlineData("6 3", 4)]
		[InlineData("7 3", 5)]
		[InlineData("8 3", 7)]
		[InlineData("9 3", 9)]
		[InlineData("10 3", 12)]
		// http://metalpolyglot.com/dev/ruby/wascally-wabbits-season/
		// Cache
		// http://webcache.googleusercontent.com/search?q=cache:6CvYeQ6O-mQJ:metalpolyglot.com/dev/ruby/wascally-wabbits-season/+&cd=4&hl=en&ct=clnk&gl=us
		[InlineData("11 3", 16)]
		[InlineData("12 3", 21)]
		[InlineData("13 3", 28)]
		[InlineData("14 3", 37)]
		[InlineData("15 3", 49)]
		[InlineData("16 3", 65)]
		[InlineData("17 3", 86)]
		[InlineData("18 3", 114)]
		[InlineData("19 3", 151)]
		[InlineData("20 3", 200)]
		[InlineData("21 3", 265)]
		public void TestSampleDataSet(string input, int expected)
		{
			BigInteger actual = _sut.MortalFibonacci2(input);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestSampleDataInFile()
		{
			string inputText = File.ReadAllText(@".\FIBD\rosalind_fibd_sample.txt");
			BigInteger actual = _sut.MortalFibonacci2(inputText);
			const int expected = 4;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ShowResult()
		{
			string inputText = File.ReadAllText(@".\FIBD\rosalind_fibd.txt");
			var result = _sut.MortalFibonacci2(inputText);

			_output.WriteLine(result.ToString());
		}
	}
}
