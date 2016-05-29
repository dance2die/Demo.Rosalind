using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			int[,] expected =
			{
				{1, 2, 3},
				{1, 3, 2},
				{2, 1, 3},
				{2, 3, 1},
				{3, 1, 2},
				{3, 2, 1}
			};

			IEnumerable<int[]> actual = _sut.GetPermutations(SAMPLE_DATASET).ToList();

			
		}
	}

	public class Perm
	{
		public IEnumerable<int[]> GetPermutations(int permutationCount)
		{
			int[] permutationList = Enumerable.Range(1, permutationCount).ToArray();
			List<int[]> result = GeneratePermutations(permutationList, 0, permutationCount - 1).ToList();

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
					Swap(ref permutationList, startCount, i);
					GeneratePermutations(permutationList, startCount + 1, permutationCount).ToList();
					Swap(ref permutationList, startCount, i);
				}
				
			}
		}

		// http://stackoverflow.com/a/2094316/4035
		public static void Swap(ref int[] list, int index1, int index2)
		{
			int tmp = list[index1];
			list[index1] = list[index2];
			list[index2] = tmp;
		}

	}
}
