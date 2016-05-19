using System;
using System.Collections.Generic;
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
				{"Rosalind_5013", "GGGTGGG" },
			};

			Dictionary<string, string> actual = _sut.ParseDataset(SAMPLE_DATESET);

			Assert.True(expected.SequenceEqual(actual));
		}

//		[Fact]
//		public void TestSampleDataSet()
//		{
//			const string expected = @"Rosalind_0498 Rosalind_2391
//Rosalind_0498 Rosalind_0442
//Rosalind_2391 Rosalind_2323";

//			string actual = _sut.OverlapGraphs(SAMPLE_DATESET);

//			Assert.Equal(expected, actual);
//		}
	}

	public class Grph
	{
		public Dictionary<string, string> ParseDataset(string dataset)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			var lines = dataset.Split(new [] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

			string previousKey = "";
			foreach (string line in lines)
			{
				if (line.StartsWith(">"))
				{
					// Remove ">" from the key
					var key = line.Split('>')[1];
					result[key] = string.Empty;
					previousKey = key;
				}
				else
				{
					result[previousKey] = line;
					previousKey = string.Empty;
				}
			}

			return result;
		}

		//public string OverlapGraphs(string input)
		//{

		//}
	}
}
