using Rosalind.Lib;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.ORF
{
	public class OrfTest : BaseTest
	{
		private readonly Orf _sut;

		public OrfTest(ITestOutputHelper output) : base(output)
		{
			_sut = new Orf();
		}

		[Fact]
		public void TestConvertDnaToRnaString()
		{
			const string dnaString = "AGCCATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCCGAGTAGCATCTCAG";
			//const string dnaString = "ATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCC";
			var rna = new Rna();
			var rnaString = rna.ConvertDnaToRna(dnaString);
			string proteinString = rna.ConvertRnaToProtein(rnaString, "|");

			//_output.WriteLine("rnaString: {0}", rnaString);
			_output.WriteLine("proteinString: {0}", proteinString);

			for (int i = 0; i < dnaString.Length; i++)
			{
				for (int j = dnaString.Length - 1; j >= 0; j--)
				{
					try
					{
						var substring = dnaString.Substring(i, j - i);
						//_output.WriteLine("substring: {0}", substring);
						rnaString = rna.ConvertDnaToRna(substring);
						proteinString = rna.ConvertRnaToProtein(rnaString, "|");

						//_output.WriteLine("rnaString: {0}", rnaString);
						if (proteinString.StartsWith("M") && proteinString.IndexOf("|") > 0)
						{
							_output.WriteLine("proteinString: {0}", proteinString);
							//_output.WriteLine(new string('=', 80));
						}
						break;
					}
					catch (Exception e)
					{
						//_output.WriteLine("Nothing in position: {0}", i);
					}

				}
			}



			ReverseComplement reverseComplement = new ReverseComplement();
			var reverseComplementedDnaString = reverseComplement.ReverseComplementDataset(dnaString);

			rnaString = rna.ConvertDnaToRna(reverseComplementedDnaString);
			proteinString = rna.ConvertRnaToProtein(rnaString, "|");

			//_output.WriteLine("Reverse Completed rnaString: {0}", rnaString);
			//_output.WriteLine("Reverse Completed proteinString: {0}", proteinString);

			for (int i = 0; i < dnaString.Length; i++)
			{
				for (int j = dnaString.Length - 1; j >= 0; j--)
				{
					try
					{
						var substring = dnaString.Substring(i, j - i);
						//_output.WriteLine("substring: {0}", substring);
						rnaString = rna.ConvertDnaToRna(substring);
						proteinString = rna.ConvertRnaToProtein(rnaString, "|");

						//_output.WriteLine("rnaString: {0}", rnaString);
						if (proteinString.StartsWith("M") && proteinString.IndexOf("|") > 0)
						{
							_output.WriteLine("Reverse Completed proteinString: {0}", proteinString);
							//_output.WriteLine(new string('=', 80));
						}
						break;
					}
					catch (Exception e)
					{
						//_output.WriteLine("Nothing in position: {0}", i);
					}
					
				}
			}
		}

		[Fact]
		public void TestGettingProteinStringsWithDelimiters()
		{
			const string dnaString = "AGCCATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCCGAGTAGCATCTCAG";

			foreach (string proteinString in GetProteinStringsWithDelimiters(dnaString))
			{
				_output.WriteLine("proteinString: {0}", proteinString);
			}

			ReverseComplement reverseComplement = new ReverseComplement();
			var reverseComplementedDnaString = reverseComplement.ReverseComplementDataset(dnaString);

			foreach (string proteinString in GetProteinStringsWithDelimiters(reverseComplementedDnaString))
			{
				_output.WriteLine("Reverse Completed proteinString: {0}", proteinString);
			}
		}

		public IEnumerable<string> GetProteinStringsWithDelimiters(string dnaString, string delimiter = "|")
		{
			var rna = new Rna();

			for (int i = 0; i < dnaString.Length; i++)
			{
				for (int j = dnaString.Length - 1; j >= 0; j--)
				{
					string proteinString = string.Empty;
					try
					{
						var substring = dnaString.Substring(i, j - i);
						var rnaString = rna.ConvertDnaToRna(substring);
						proteinString = rna.ConvertRnaToProtein(rnaString, "|");
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}

					//_output.WriteLine("rnaString: {0}", rnaString);
					if (proteinString.StartsWith("M") && proteinString.IndexOf("|") > 0)
					{
						//_output.WriteLine("proteinString: {0}", proteinString);
						//_output.WriteLine(new string('=', 80));
						yield return proteinString;
					}
					break;
				}
			}

			for (int i = 0; i < dnaString.Length; i++)
			{
				var substring = dnaString.Substring(i, dnaString.Length - i);
				var rnaString = rna.ConvertDnaToRna(substring);
				var proteinString = rna.ConvertRnaToProtein(rnaString, "|");

				if (proteinString.StartsWith("M") && proteinString.IndexOf("|") > 0)
					yield return proteinString;
			}
		}
	}

	public class Orf
	{
	}
}
