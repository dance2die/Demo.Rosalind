using System;
using System.Collections.Generic;
using System.Linq;
using Rosalind.Lib.Util;

namespace Demo.Rosalind.Tests.CONS
{
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
				else if (maxValue == valueC) yield return 'C';
				else if (maxValue == valueG) yield return 'G';
				else if (maxValue == valueT) yield return 'T';
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
}