using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.GRPH
{
	public class GrphTest : BaseTest
	{
		private readonly Grph _sut;
		private const string SAMPLE_DATESET = @">Rosalind_0498
AAATAAA
>Rosalind_2391
AAATTTT
>Rosalind_2323
TTTTCCC
>Rosalind_0442
AAATCCC
>Rosalind_5013
GGGTGGG";

		private const string SAMPLE_DATESET2 = @">Rosalind_1111
AAATAAA
CCCTTT
>Rosalind_2222
AAATTTT
ABCAAA
>Rosalind_3333
TTTTCCC
XXXAAA
>Rosalind_4444
AAATCCC
GGGGGG
>Rosalind_5555
GGGTGGG
ABCDEF";

		public GrphTest(ITestOutputHelper output) 
			: base(output)
		{
			_sut = new Grph();
		}

		[Fact]
		public void ParseDataset()
		{
			Dictionary<string, string> expected = new Dictionary<string, string>
			{
				{"Rosalind_0498", "AAATAAA" },
				{"Rosalind_2391", "AAATTTT" },
				{"Rosalind_2323", "TTTTCCC" },
				{"Rosalind_0442", "AAATCCC" },
				{"Rosalind_5013", "GGGTGGG" }
			};

			Dictionary<string, string> actual = _sut.ParseDataset(SAMPLE_DATESET);

			Assert.True(expected.SequenceEqual(actual));
		}

		[Fact]
		public void ParseDataset2()
		{
			Dictionary<string, string> expected = new Dictionary<string, string>
			{
				{"Rosalind_1111", "AAATAAA" +
"CCCTTT" },
				{"Rosalind_2222", "AAATTTT" +
"ABCAAA" },
				{"Rosalind_3333", "TTTTCCC" +
"XXXAAA" },
				{"Rosalind_4444", "AAATCCC" +
"GGGGGG" },
				{"Rosalind_5555", "GGGTGGG" +
"ABCDEF" }
			};

			Dictionary<string, string> actual = _sut.ParseDataset(SAMPLE_DATESET2);

			Assert.True(expected.SequenceEqual(actual));
		}

		[Fact]
		public void TestSampleDataSet()
		{
			const string expected = "Rosalind_0498 Rosalind_2391\r\nRosalind_0498 Rosalind_0442\r\nRosalind_2391 Rosalind_2323";

			string actual = _sut.OverlapGraphs(SAMPLE_DATESET);

			//Assert.True(string.Compare(expected, actual, StringComparison.InvariantCultureIgnoreCase) == 0);
			Assert.True(EqualsExcludingWhitespace(expected, actual));
		}

		[Fact]
		public void ShowResult()
		{
			string inputText = File.ReadAllText(@".\GRPH\rosalind_grph.txt");

			string result = _sut.OverlapGraphs(inputText);

			_output.WriteLine(result);
		}
	}
}
