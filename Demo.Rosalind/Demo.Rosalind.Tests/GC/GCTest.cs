using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.GC
{
	public class GCTest
	{
		private readonly ITestOutputHelper _output;

		public GCTest(ITestOutputHelper output)
		{
			_output = output;
		}

		[Theory]
		public void TestParsingFastaFormat()
		{
			
		}
	}
}
