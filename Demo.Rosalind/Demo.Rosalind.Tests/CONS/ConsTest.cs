using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosalind.Lib.Util;
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
	}

	public class Cons
	{
		public string GetOutput(string fastaString)
		{
			ProfileMatrix profileMatrix = GetProfileMatrix(fastaString);
			string consensusString = new string(GetConsensusString(profileMatrix).ToArray());

			return string.Format("{0}{1}{2}", consensusString, Environment.NewLine, profileMatrix);
		}

		public IEnumerable<char> GetConsensusString(ProfileMatrix profileMatrix)
		{
			int columnCount = profileMatrix.A.Length;
			for (int i = 0; i < columnCount; i++)
			{
				int valueA = profileMatrix.A[i];
				int valueC = profileMatrix.C[i];
				int valueG = profileMatrix.G[i];
				int valueT = profileMatrix.T[i];

				var maxValue = new [] {valueA, valueC, valueG, valueT}.Max();
				if (maxValue == valueA) yield return 'A';
				if (maxValue == valueC) yield return 'C';
				if (maxValue == valueG) yield return 'G';
				if (maxValue == valueT) yield return 'T';
			}
		}

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

		public override string ToString()
		{
			var lineA = string.Format("A: {0}", string.Join(" ", A.Select(intValue => intValue.ToString())));
			var lineC = string.Format("C: {0}", string.Join(" ", C.Select(intValue => intValue.ToString())));
			var lineG = string.Format("G: {0}", string.Join(" ", G.Select(intValue => intValue.ToString())));
			var lineT = string.Format("T: {0}", string.Join(" ", T.Select(intValue => intValue.ToString())));

			StringBuilder buffer = new StringBuilder();
			buffer.AppendLine(lineA).AppendLine(lineC).AppendLine(lineG).AppendLine(lineT);

			return buffer.ToString();
		}
	}
}
