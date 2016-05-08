using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.PROT
{
	public class ProtTest
	{
		private readonly ITestOutputHelper _output;
		private readonly Prot _sut;

		public ProtTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Prot();
		}


	}

	public class Prot
	{
	}
}
