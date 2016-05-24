using Rosalind.Lib;
using System;
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
			const string dnaString = "ATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCCGAGTAGCATCTC";
			//const string dnaString = "ATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCC";
			var rna = new Rna();
			var rnaString = rna.ConvertDnaToRna(dnaString);
			string proteinString = rna.ConvertRnaToProtein(rnaString, "|");

			_output.WriteLine("rnaString: {0}", rnaString);
			_output.WriteLine("proteinString: {0}", proteinString);

			ReverseComplement reverseComplement = new ReverseComplement();
			var reverseComplementedDnaString = reverseComplement.ReverseComplementDataset(dnaString);

			rnaString = rna.ConvertDnaToRna(reverseComplementedDnaString);
			proteinString = rna.ConvertRnaToProtein(rnaString, "|");

			_output.WriteLine("Reverse Completed rnaString: {0}", rnaString);
			_output.WriteLine("Reverse Completed proteinString: {0}", proteinString);

			for (int i = 0; i < dnaString.Length; i++)
			{
				for (int j = dnaString.Length - 1; j >= 0; j--)
				{
					try
					{
						var substring = dnaString.Substring(i, j);
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
		}
	}

	public class Orf
	{
	}
}
