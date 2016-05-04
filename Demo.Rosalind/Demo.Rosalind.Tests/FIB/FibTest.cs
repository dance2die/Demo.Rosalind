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
		[InlineData(5, 3, 19)]
		public void TestFibonacciWithSequenceSpecified(int maxIterations, int rabbitPairs, int expected)
		{
			int actual = _sut.GetFibonacciSequence(maxIterations, rabbitPairs);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ShowResult()
		{
			const int maxIterations = 29;
			const int rabbitPairs = 3;

			int result = _sut.GetFibonacciSequence(maxIterations, rabbitPairs);

			_output.WriteLine(result.ToString());
		}
	}

	public class Fib
	{
		public int GetFibonacciSequence(int maxIterations, int rabbitPairs)
		{
			if (maxIterations <= 2) return 1;

			int sequence1 = 1;
			int sequence2 = 1;

			int nextValue = (sequence1 * rabbitPairs) + sequence2;
			for (int i = 3; i < maxIterations; i++)
			{
				sequence1 = sequence2;
				sequence2 = nextValue;

				nextValue = (sequence1 * rabbitPairs) + sequence2;
			}

			return nextValue;
		}
	}
}
