using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.FIBD
{
	public class FibdTest
	{
		private readonly ITestOutputHelper _output;
		private readonly Fibd _sut;
		private const string SAMPLE_DATASET = "6 3";

		public FibdTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Fibd();
		}

		[Theory]
		[InlineData("6 3", 4)]
		[InlineData("7 3", 5)]
		[InlineData("8 3", 5)]
		[InlineData("9 3", 6)]
		[InlineData("10 3", 10)]
		public void TestSampleDataSet(string input, int expected)
		{
			int actual = _sut.MortalFibonacci(input);

			Assert.Equal(expected, actual);
		}
	}

	public class Fibd
	{
		public int MortalFibonacci(string input)
		{
			var splited = input.Split(' ');
			int count = int.Parse(splited[0]);
			int life = int.Parse(splited[1]);

			var sum = new List<Rabbit> {new Rabbit(2)};
			for (int i = 2; i <= count; i++)
			{
				sum.ForEach(rabbit => rabbit.Age++);
				sum.Add(new Rabbit(1));
				sum.RemoveAll(rabbit => rabbit.Age >= 3);
			}

			return 1;
		}
	}

	public class Rabbit
	{
		public int Age { get; set; }

		public Rabbit(int age)
		{
			Age = age;
		}

		public override string ToString()
		{
			return string.Format("Rabbit.Age: {0}", Age);
		}
	}
}
