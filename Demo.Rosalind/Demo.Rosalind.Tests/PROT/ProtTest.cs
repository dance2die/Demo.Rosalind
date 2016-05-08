﻿using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.PROT
{
	public class ProtTest
	{
		private readonly ITestOutputHelper _output;
		private readonly Prot _sut;
		private const string SAMPLE_DATASET = "AUGGCCAUGGCGCCCAGAACUGAGAUCAAUAGUACCCGUAUUAACGGGUGA";

		public ProtTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Prot();
		}

		[Fact]
		public void TestSampleDataSetResult()
		{
			const string expected = "MAMAPRTEINSTRING";

			string actual = _sut.EncodeRnaString(SAMPLE_DATASET);

			Assert.Equal(expected, actual);
		}
	}

	public class Prot
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

		public string EncodeRnaString(string input)
		{
			StringBuilder buffer = new StringBuilder();

			const int keyLength = 3;	// length of a key in RNA Code table
			for (int i = 0; i < input.Length; i += 3)
			{
				string key = input.Substring(i, keyLength);
				string encodedValue = _rnaCodonTable[key];
				if (encodedValue.ToUpper() != "STOP")
					buffer.Append(encodedValue);
			}

			return buffer.ToString();
		}
	}
}
 