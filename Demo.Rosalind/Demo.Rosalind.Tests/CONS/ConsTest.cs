using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.CONS
{
	public class ConsTest : BaseTest
	{
		private readonly Cons _sut;

		public ConsTest(ITestOutputHelper output) : base(output)
		{
			_sut = new Cons();
		}
	}

	public class Cons
	{
	}
}
