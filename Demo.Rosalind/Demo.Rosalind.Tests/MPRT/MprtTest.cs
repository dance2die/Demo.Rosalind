using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.MPRT
{
	public class MprtTest : BaseTest
	{
		private readonly Mprt _sut;

		public MprtTest(ITestOutputHelper output) : base(output)
		{
			_sut = new Mprt();
		}

		/*
			Test Scenarios

			Download sample FASTA from www.uniprot.org.
				Parse FASTA and find N-glycosylation motif locations in FASTA sequence
				Combine found locations into a string.

			Parse N-glycosylation motif

			Parse Sample DataSet.

			Build UniProt URL for FASTA File

			Download FASTA files
				Parse FASTA file
		*/

	}

	public class Mprt
	{
	}
}
