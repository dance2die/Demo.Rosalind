﻿using System.Collections.Generic;
using System.IO;
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
			var expected = new List<IEnumerable<int>> 
			{
				new [] {1, 2, 3},
				new [] {1, 3, 2},
				new [] {2, 1, 3},
				new [] {2, 3, 1},
				new [] {3, 1, 2},
				new [] {3, 2, 1}
			};

			var actual = _sut.GetPermutations(SAMPLE_DATASET).ToList();
			//actual.Sort(new ArrayComparison());

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

		[Fact]
		public void ShowResult()
		{
			string permutationLength = File.ReadAllText(@".\PERM\rosalind_perm.txt");

			string result = _sut.GetPermutationOutputString(int.Parse(permutationLength));

			_output.WriteLine(result);
		}

		/// <summary>
		/// Compare a List of integer arrays.
		/// </summary>
		//private bool IsMultidimensionalArraySequenceEqual(List<int[]> list1, List<int[]> list2)
		private bool IsMultidimensionalArraySequenceEqual(List<IEnumerable<int>> list1, List<IEnumerable<int>> list2)
		{
			for (int i = 0; i < list1.Count; i++)
			{
				if (!list1[i].SequenceEqual(list2[i]))
					return false;
			}

			return true;
		}
	}
}
