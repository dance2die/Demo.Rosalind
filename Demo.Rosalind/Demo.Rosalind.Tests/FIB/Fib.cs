using System.Numerics;

namespace Demo.Rosalind.Tests.FIB
{
	public class Fib
	{
		public BigInteger GetFibonacciSequence(int maxIterations, int rabbitPairs)
		{
			if (maxIterations <= 2) return 1;

			BigInteger sequence1 = 1;
			BigInteger sequence2 = 1;

			BigInteger nextValue = (sequence1 * rabbitPairs) + sequence2;
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