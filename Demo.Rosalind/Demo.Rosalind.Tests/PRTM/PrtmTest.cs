using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.PRTM
{
	public class PrtmTest
	{
		private readonly ITestOutputHelper _output;

		public PrtmTest(ITestOutputHelper output)
		{
			_output = output;
		}
	}
}
