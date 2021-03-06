using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Rosalind.Tests.PERM
{
	public class Perm
	{
		public string GetPermutationOutputString(int permutationLength)
		{
			var permutations = GetPermutations(permutationLength).ToList();
			//permutations.Sort(new ArrayComparison());

			StringBuilder buffer = new StringBuilder();
			buffer.AppendLine(permutations.Count.ToString());

			//foreach (int[] permutation in permutations)
			foreach (var permutation in permutations)
			{
				buffer.AppendLine(string.Join(" ", permutation.Select(i => i.ToString()).ToArray()));
			}

			return buffer.ToString().Trim();
		}

		//public IEnumerable<int[]> GetPermutations(int permutationLength)
		public IEnumerable<IEnumerable<int>> GetPermutations(int permutationLength)
		{
			int[] permutationList = Enumerable.Range(1, permutationLength).ToArray();
			IEnumerable<int[]> result = GeneratePermutations(permutationList, 0, permutationLength - 1).ToList();
			//var result = GetPermutations(permutationList, permutationLength);

			return result;
		}

		// http://stackoverflow.com/a/10630026/4035
		// This has a problem if there is a duplicate number or character like for 'HALLOWEEN', it'd not work.
		public IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> enumerable, int length)
		{
			var list = enumerable.ToList();
			if (length == 1) return list.Select(t => new [] { t });
			
			return GetPermutations(list, length - 1)
				.SelectMany(
					t => list.Where(e => !t.Contains(e)),
					(t1, t2) => t1.Concat(new [] { t2 }));
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

					// This extra step is NOT needed.
					//Swap(permutationList, startCount, i);
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