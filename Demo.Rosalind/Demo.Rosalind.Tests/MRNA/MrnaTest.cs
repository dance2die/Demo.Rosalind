using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.MRNA
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// Can't figure out how sample data is converted to sample output.
	/// http://recologia.com.br/2013/05/problema-rosalind-inferring-mrna-from-protein-mrna/
	/// </remarks>
	public class MrnaTest
	{
		private readonly ITestOutputHelper _output;
		private readonly Mrna _sut;
		private const string SAMPLE_DATASET = "MA";

		public MrnaTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Mrna();
		}

		[Fact]
		public void TestSampleDataSet()
		{
			// from the website: http://rosalind.info/problems/mrna/
			const int expected = 12;

			BigInteger actual = _sut.GetPossibleRnaStringCountFromProtein(SAMPLE_DATASET);

			Assert.Equal(expected, actual);
		}
	}

	public class Mrna
	{
		private readonly Dictionary<string, string> _rnaCodonTable = new Dictionary<string, string>
		{
			{"UUU", "F"}, {"UUC", "F"}, {"UUA", "L"}, {"UUG", "L"}, {"UCU", "S"},
			{"UCC", "S"}, {"UCA", "S"}, {"UCG", "S"}, {"UAU", "Y"}, {"UAC", "Y"},
			{"UAA", "Stop"}, {"UAG", "Stop"}, {"UGU", "C"}, {"UGC", "C"}, {"UGA", "Stop"},
			{"UGG", "W"}, {"CUU", "L"}, {"CUC", "L"}, {"CUA", "L"}, {"CUG", "L"},
			{"CCU", "P"}, {"CCC", "P"}, {"CCA", "P"}, {"CCG", "P"}, {"CAU", "H"},
			{"CAC", "H"}, {"CAA", "Q"}, {"CAG", "Q"}, {"CGU", "R"}, {"CGC", "R"},
			{"CGA", "R"}, {"CGG", "R"}, {"AUU", "I"}, {"AUC", "I"}, {"AUA", "I"},
			{"AUG", "M"}, {"ACU", "T"}, {"ACC", "T"}, {"ACA", "T"}, {"ACG", "T"},
			{"AAU", "N"}, {"AAC", "N"}, {"AAA", "K"}, {"AAG", "K"}, {"AGU", "S"},
			{"AGC", "S"}, {"AGA", "R"}, {"AGG", "R"}, {"GUU", "V"}, {"GUC", "V"},
			{"GUA", "V"}, {"GUG", "V"}, {"GCU", "A"}, {"GCC", "A"}, {"GCA", "A"},
			{"GCG", "A"}, {"GAU", "D"}, {"GAC", "D"}, {"GAA", "E"}, {"GAG", "E"},
			{"GGU", "G"}, {"GGC", "G"}, {"GGA", "G"}, {"GGG", "G"},
		};

		private const int MODULO_VALUE = 1000000;   // Given on the website.
		private const int STOP_COUNT = 3;

		public BigInteger GetPossibleRnaStringCountFromProtein(string input)
		{
			// Count codon value count;
			Dictionary<string, int> codonValueCount = new Dictionary<string, int>();
			foreach (KeyValuePair<string, string> pair in _rnaCodonTable)
			{
				if (codonValueCount.ContainsKey(pair.Value))
					codonValueCount[pair.Value]++;
				else
					codonValueCount[pair.Value] = 1;
			}

			// Multiply each character's code value count
			BigInteger possibleCombinations = 1;
			foreach (char c in input)
			{
				possibleCombinations *= codonValueCount[c.ToString()];
			}

			// Multiple Stop count (3 of them)
			possibleCombinations *= STOP_COUNT;

			BigInteger result = possibleCombinations % MODULO_VALUE;

			return result;
		}
	}
}
