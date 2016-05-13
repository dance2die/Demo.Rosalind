using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.MRNA
{
	public class MrnaTest
	{
		private readonly ITestOutputHelper _output;

		public MrnaTest(ITestOutputHelper output)
		{
			_output = output;
		}
	}
}
