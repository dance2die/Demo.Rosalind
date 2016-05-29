using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.PERM
{
	public class PermTest : BaseTest
	{
		private readonly Perm _sut;
		private const int SAMPLE_DATASET = 3;

		public PermTest(ITestOutputHelper output) : base(output)
		{
			_sut = new Perm();
		}

		[Fact]
		public void TestSampleDataSetPermutation()
		{
			List<int[]> expected = new List<int[]> 
			{
				new [] {1, 2, 3},
				new [] {1, 3, 2},
				new [] {2, 1, 3},
				new [] {2, 3, 1},
				new [] {3, 1, 2},
				new [] {3, 2, 1}
			};

			var actual = _sut.GetPermutations(SAMPLE_DATASET).ToList();
			actual.Sort(new ArrayComparison());

			Assert.True(IsMultidimensionalArraySequenceEqual(expected, actual));
		}

		[Fact]
		public void TestOutputingResult()
		{
			const string expected = @"6
1 2 3
1 3 2
2 1 3
2 3 1
3 1 2
3 2 1";

			string actual = _sut.GetPermutationOutputString(SAMPLE_DATASET);

			Assert.True(string.CompareOrdinal(expected, actual) == 0);
		}

		/// <summary>
		/// Compare a List of integer arrays.
		/// </summary>
		private bool IsMultidimensionalArraySequenceEqual(List<int[]> list1, List<int[]> list2)
		{
			for (int i = 0; i < list1.Count; i++)
			{
				if (!list1[i].SequenceEqual(list2[i]))
					return false;
			}

			return true;
		}
	}

	public class Perm
	{
		public string GetPermutationOutputString(int permutationLength)
		{
			var permutations = GetPermutations(permutationLength).ToList();
			permutations.Sort(new ArrayComparison());

			StringBuilder buffer = new StringBuilder();
			buffer.AppendLine(permutations.Count.ToString());

			foreach (int[] permutation in permutations)
			{
				buffer.AppendLine(string.Join(" ", permutation.Select(i => i.ToString()).ToArray()));
			}

			return buffer.ToString().Trim();
		}

		public IEnumerable<int[]> GetPermutations(int permutationLength)
		{
			int[] permutationList = Enumerable.Range(1, permutationLength).ToArray();
			IEnumerable<int[]> result = GeneratePermutations(permutationList, 0, permutationLength - 1).ToList();

			return result;
		}

		// http://stackoverflow.com/a/756083/4035
		private IEnumerable<int[]> GeneratePermutations(int[] permutationList, int startCount, int permutationCount)
		{
			if (startCount == permutationCount)
			{
				yield return permutationList;
			}
			else
			{
				for (int i = startCount; i <= permutationCount; i++)
				{
					Swap(permutationList, startCount, i);

					var permutationListCopy = new List<int>(permutationList).ToArray();
					var permutations = GeneratePermutations(permutationListCopy, startCount + 1, permutationCount);
					foreach (int[] permutation in permutations)
					{
						yield return permutation;
					}

					Swap(permutationList, startCount, i);
				}

			}
		}

		// http://stackoverflow.com/a/2094316/4035
		public static void Swap(int[] list, int index1, int index2)
		{
			int tmp = list[index1];
			list[index1] = list[index2];
			list[index2] = tmp;
		}
	}
}
