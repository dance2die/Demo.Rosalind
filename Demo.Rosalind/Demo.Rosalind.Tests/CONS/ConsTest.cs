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
	}

	public class Cons
	{
		public ProfileMatrix GetProfileMatrix(string fastaString)
		{
			ProfileMatrix result = new ProfileMatrix();

			var dnaStrings = GetDnaStringsFromFasta(fastaString).ToList();
			int columnCount = dnaStrings.First().Length;
			result.InitializeMatrix(columnCount);

			for (int colIndex = 0; colIndex < columnCount; colIndex++)
			{
				for (int rowIndex = 0; rowIndex < dnaStrings.Count; rowIndex++)
				{
					char currentChar = dnaStrings[rowIndex][colIndex];
					switch (currentChar)
					{
						case 'A':
							result.A[colIndex]++;
							break;
						case 'C':
							result.C[colIndex]++;
							break;
						case 'G':
							result.G[colIndex]++;
							break;
						case 'T':
							result.T[colIndex]++;
							break;
					}
				}
			}

			return result;
		}

		public IEnumerable<string> GetDnaStringsFromFasta(string fastaString)
		{
			FastaReader reader = new FastaReader();
			return reader.GetDnaStrings(fastaString);
		}
	}

	public class ProfileMatrix
	{
		public int[] A { get; set; }
		public int[] C { get; set; }
		public int[] G { get; set; }
		public int[] T { get; set; }

		public void InitializeMatrix(int columnCount)
		{
			A = new int[columnCount];
			C = new int[columnCount];
			G = new int[columnCount];
			T = new int[columnCount];

			for (int i = 0; i < columnCount; i++)
			{
				A[i] = 0;
				C[i] = 0;
				G[i] = 0;
				T[i] = 0;
			}
		}
	}
}
