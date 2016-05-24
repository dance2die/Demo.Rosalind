using System.Collections.Generic;
using System.Text;

namespace Rosalind.Lib
{
	public class Rna
	{
		public static Dictionary<string, string> RnaCodenTable { get; set; }

		static Rna()
		{
			RnaCodenTable = new Dictionary<string, string>
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
		}

		public string ConvertDnaToRna(string dnaString)
		{
			return dnaString.Replace("T", "U");
		}

		/// <summary>
		/// Convert RNA string to Protein string
		/// </summary>
		/// <remarks>Copied from "Prot.EncodeRnaString(...)"</remarks>
		public string ConvertRnaToProtein(string rnaString, string stopCharacter = "")
		{
			// length of a key in RNA Code table
			const int keyLength = 3;

			if (rnaString.Length % keyLength != 0)
				return string.Empty;

			StringBuilder buffer = new StringBuilder();

			for (int i = 0; i < rnaString.Length; i += 3)
			{
				string key = rnaString.Substring(i, keyLength);
				string encodedValue = RnaCodenTable[key];
				if (encodedValue.ToUpper() == "STOP")
					encodedValue = stopCharacter;

				buffer.Append(encodedValue);
			}

			return buffer.ToString();
		}

	}
}