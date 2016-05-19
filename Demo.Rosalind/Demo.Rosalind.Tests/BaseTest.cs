using System.Linq;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests
{
	public class BaseTest
	{
		protected readonly ITestOutputHelper _output;

		public BaseTest(ITestOutputHelper output)
		{
			_output = output;
		}


		// http://stackoverflow.com/a/17545215/4035
		protected bool EqualsExcludingWhitespace(string a, string b)
		{
			return a.Where(c => !char.IsWhiteSpace(c))
			   .SequenceEqual(b.Where(c => !char.IsWhiteSpace(c)));
		}

	}
}