using Xunit.Abstractions;

namespace Demo.Rosalind.Tests
{
	public class BaseTest
	{
		private readonly ITestOutputHelper _output;

		public BaseTest(ITestOutputHelper output)
		{
			_output = output;
		}
	}
}