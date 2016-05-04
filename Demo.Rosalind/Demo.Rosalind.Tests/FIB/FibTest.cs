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

		[Fact]
		public void TestFibonacci()
		{
			// From http://rosalind.info/problems/fib/
			const int expected = 5;
			const int maxIterations = 5;    // k <= 5

			const int sequence1 = 1;
			const int sequence2 = 1;

			int actual = _sut.GetFibonacciSequence(sequence1, sequence2, maxIterations);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestSampleData()
		{
			// From http://rosalind.info/problems/fib/
			const int expected = 19;
			const int maxIterations = 5;	// k <= 5

			const int sequence1 = 5;
			const int sequence2 = 3;

			int actual = _sut.GetFibonacciSequence(sequence1, sequence2, maxIterations);

			Assert.Equal(expected, actual);
		}

	}

	public class Fib
	{
		public int GetFibonacciSequence(int sequence1, int sequence2, int maxIterations)
		{
			if (maxIterations <= 2) return 1;

			int nextValue = sequence1 + sequence2;
			for (int i = 3; i < maxIterations; i++)
			{
				sequence1 = sequence2;
				sequence2 = nextValue;

				nextValue = sequence1 + sequence2;
			}

			return nextValue;
		}
	}
}
