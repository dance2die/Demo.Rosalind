using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

		[Fact]
		public void ShowResult()
		{
			string inputText = File.ReadAllText(@".\HAMM\rosalind_hamm.txt");

			int result = _sut.CalculateHammingDistance(inputText);

			_output.WriteLine(result.ToString());
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
