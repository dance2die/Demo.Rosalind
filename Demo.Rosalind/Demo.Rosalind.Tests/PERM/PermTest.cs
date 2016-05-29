using System;
using System.Collections.Generic;
using System.Linq;
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

	public class ArrayComparison : IComparer<int[]>
	{
		public int Compare(int[] a1, int[] a2)
		{
			int sortValue1 = 0;
			int sortValue2 = 0;
			int powerCount = a1.Length - 1;
			for (int i = 0; i < a1.Length; i++)
			{
				int power = (int)Math.Pow(10, powerCount);
				sortValue1 += a1[i] * power;
				sortValue2 += a2[i] * power;

				powerCount--;
			}

			if (sortValue1 > sortValue2) return 1;
			if (sortValue1 == sortValue2) return 0;
			if (sortValue1 < sortValue2) return -1;

			return 0;
		}
	}

	public class Perm
	{
		public IEnumerable<int[]> GetPermutations(int permutationCount)
		{
			int[] permutationList = Enumerable.Range(1, permutationCount).ToArray();
			IEnumerable<int[]> result = GeneratePermutations(permutationList, 0, permutationCount - 1).ToList();

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
