using Rosalind.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
			//const string dnaString = "ATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCCGAGTAGCATCTC";
			const string dnaString = "ATGTAGCTAACTCAGGTTACATGGGGATGACCCCGCGACTTGGATTAGAGTCTCTTTTGGAATAAGCCTGAATGATCC";
			var rna = new Rna();
			var rnaString = rna.ConvertDnaToRna(dnaString);
			string proteinString = rna.ConvertRnaToProtein(rnaString);

			_output.WriteLine("rnaString: {0}", rnaString);
			_output.WriteLine("proteinString: {0}", proteinString);

			//for (int i = 0; i < dnaString.Length; i++)
			//{
			//	try
			//	{
			//		var substring = dnaString.Substring(i);
			//		//_output.WriteLine("substring: {0}", substring);
			//		var rnaString = rna.ConvertDnaToRna(substring);
			//		string proteinString = rna.ConvertRnaToProtein(rnaString);

			//		//_output.WriteLine("rnaString: {0}", rnaString);
			//		//if (proteinString.StartsWith("M"))
			//		if (true)
			//		{
			//			_output.WriteLine("proteinString: {0}", proteinString);
			//			//_output.WriteLine(new string('=', 80));
			//		}
			//	}
			//	catch (Exception e)
			//	{
			//		//_output.WriteLine("Nothing in position: {0}", i);
			//	}
			//}
		}
	}

	public class Orf
	{
	}
}
