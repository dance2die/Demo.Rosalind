using System.Numerics;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.FIB
{
	public class FibTest
	{
		private readonly ITestOutputHelper _output;
		private readonly Fib _sut;

		public FibTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Fib();
		}

		[Theory]
		[InlineData(5, 1, 5)]
		[InlineData(10, 1, 55)]
		[InlineData(5, 3, 19)]
		public void TestFibonacciWithSequenceSpecified(int maxIterations, int rabbitPairs, int expected)
		{
			BigInteger actual = _sut.GetFibonacciSequence(maxIterations, rabbitPairs);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ShowResult()
		{
			const int maxIterations = 33;
			const int rabbitPairs = 2;

			BigInteger result = _sut.GetFibonacciSequence(maxIterations, rabbitPairs);

			_output.WriteLine(result.ToString());
		}
	}
}
