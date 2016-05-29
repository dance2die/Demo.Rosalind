using System;
using System.Collections.Generic;

namespace Demo.Rosalind.Tests.PERM
{
	/// <summary>
	/// To compare arrays for sorting.
	/// </summary>
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
}