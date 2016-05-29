using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.REVP
{
	public class RevpTest : BaseTest
	{
		private readonly Revp _sut = new Revp();
		private const string SAMPLE_DATASET = @">Rosalind_24
TCAATGCATGCGGGTCTATATGCAT";

		public RevpTest(ITestOutputHelper output) : base(output)
		{
		}
	}

	public class Revp
	{
	}
}
