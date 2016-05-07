using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.HAMM
{
	public class HammTest
	{
		private readonly ITestOutputHelper _output;
		private readonly Hamm _sut;
		private const string SAMPLE_INPUT = @"GAGCCTACTAACGGGAT
CATCGTAATGACGGCCT";

		public HammTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Hamm();
		}

		[Fact]
		public void TestParsingTwoStringsToCompare()
		{
			var expected = new[] {"GAGCCTACTAACGGGAT", "CATCGTAATGACGGCCT"};

			IEnumerable<string> actual = _sut.ParseInput(SAMPLE_INPUT);

			Assert.True(expected.SequenceEqual(actual));
		}

		[Fact]
		public void TestCalculatingHammingDistance()
		{
			const int expected = 7;

			int actual = _sut.CalculateHammingDistance(SAMPLE_INPUT);

			Assert.Equal(expected, actual);
		}
	}

	public class Hamm
	{
		public int CalculateHammingDistance(string input)
		{
			var texts = ParseInput(input).ToArray();
			var s = texts[0];
			var t = texts[1];

			int distance = 0;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] != t[i])
					distance++;
			}

			return distance;
		}

		public IEnumerable<string> ParseInput(string input)
		{
			return input.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
