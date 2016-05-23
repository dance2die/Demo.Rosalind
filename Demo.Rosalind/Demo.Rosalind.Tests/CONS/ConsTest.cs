using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

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

		/// <remarks>
		/// A: 5 1 0 0 5 5 0 0
		/// C: 0 0 1 4 2 0 6 1
		/// G: 1 1 6 3 0 1 0 0
		/// T: 1 5 0 0 0 1 1 6
		/// </remarks>
		[Fact]
		public void TestProfileMatrixValues()
		{
			ProfileMatrix expected = new ProfileMatrix
			{
				A = new [] { 5, 1, 0, 0, 5, 5, 0, 0 },
				C = new [] { 0, 0, 1, 4, 2, 0, 6, 1 },
				G = new [] { 1, 1, 6, 3, 0, 1, 0, 0 },
				T = new [] { 1, 5, 0, 0, 0, 1, 1, 6 }
			};

			ProfileMatrix actual = _sut.GetProfileMatrix(SAMPLE_DATASET);

			Assert.True(expected.A.SequenceEqual(actual.A));
			Assert.True(expected.C.SequenceEqual(actual.C));
			Assert.True(expected.G.SequenceEqual(actual.G));
			Assert.True(expected.T.SequenceEqual(actual.T));
		}

		[Fact]
		public void TestConsensusString()
		{
			const string expected = "ATGCAACT";

			ProfileMatrix profileMatrix = _sut.GetProfileMatrix(SAMPLE_DATASET);
			string actual = new string(_sut.GetConsensusString(profileMatrix).ToArray());

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestProfileMatrixString()
		{
			const string expected = @"A: 5 1 0 0 5 5 0 0
C: 0 0 1 4 2 0 6 1
G: 1 1 6 3 0 1 0 0
T: 1 5 0 0 0 1 1 6";

			ProfileMatrix profileMatrix = _sut.GetProfileMatrix(SAMPLE_DATASET);
			string actual = profileMatrix.ToString();

			Assert.True(EqualsExcludingWhitespace(expected, actual));
		}

		[Fact]
		public void TestOutput()
		{
			const string expected = @"ATGCAACT
A: 5 1 0 0 5 5 0 0
C: 0 0 1 4 2 0 6 1
G: 1 1 6 3 0 1 0 0
T: 1 5 0 0 0 1 1 6";

			string actual = _sut.GetOutput(SAMPLE_DATASET);

			Assert.True(EqualsExcludingWhitespace(expected, actual));
		}

		[Fact]
		public void ShowResult()
		{
			string input = File.ReadAllText(@".\CONS\rosalind_cons.txt");

			string result = _sut.GetOutput(input);

			_output.WriteLine(result);
		}
	}
}
