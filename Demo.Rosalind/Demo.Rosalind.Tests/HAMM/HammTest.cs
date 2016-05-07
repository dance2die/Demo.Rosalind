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
	}

	public class Hamm
	{
		public IEnumerable<string> ParseInput(string input)
		{
			return input.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
