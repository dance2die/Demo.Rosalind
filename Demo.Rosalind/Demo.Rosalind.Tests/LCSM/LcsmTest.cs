using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.LCSM
{
	public class LcsmTest : BaseTest
	{
		private readonly Lcsm _sut;

		public LcsmTest(ITestOutputHelper output) : base(output)
		{
			_sut = new Lcsm();
		}
	}

	public class Lcsm
	{
	}
}
