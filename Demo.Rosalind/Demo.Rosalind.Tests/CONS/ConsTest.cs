using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosalind.Lib.Util;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.CONS
{
	public class ConsTest : BaseTest
	{
		private readonly Cons _sut;
		private const string SAMPLE_DATASET = @">Rosalind_1
ATCCAGCT
>Rosalind_2
GGGCAACT
>Rosalind_3
ATGGATCT
>Rosalind_4
AAGCAACC
>Rosalind_5
TTGGAACT
>Rosalind_6
ATGCCATT
>Rosalind_7
ATGGCACT";

		public ConsTest(ITestOutputHelper output) : base(output)
		{
			_sut = new Cons();
		}

		[Fact]
		public void TestGettingDnaStringsFromFastaString()
		{
			var expected = new List<string>
			{
				"ATCCAGCT",
				"GGGCAACT",
				"ATGGATCT",
				"AAGCAACC",
				"TTGGAACT",
				"ATGCCATT",
				"ATGGCACT"
			};

			IEnumerable<string> actual = _sut.GetDnaStringsFromFasta(SAMPLE_DATASET);

			Assert.True(expected.SequenceEqual(actual));
		}
	}

	public class Cons
	{
		public IEnumerable<string> GetDnaStringsFromFasta(string fastaString)
		{
			FastaReader reader = new FastaReader();
			return reader.GetDnaStrings(fastaString);
		}
	}
}
