using System;
using System.Collections.Generic;
using System.IO;
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

		public FibdTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new Fibd();
		}

		[Theory]
		[InlineData("6 3", 4)]
		[InlineData("7 3", 5)]
		[InlineData("8 3", 7)]
		[InlineData("9 3", 9)]
		[InlineData("10 3", 12)]
		public void TestSampleDataSet(string input, int expected)
		{
			int actual = _sut.MortalFibonacci(input);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestSampleDataInFile()
		{
			string inputText = File.ReadAllText(@".\FIBD\rosalind_fibd_sample.txt");
			int actual = _sut.MortalFibonacci(inputText);
			const int expected = 4;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ShowResult()
		{
			string inputText = File.ReadAllText(@".\FIBD\rosalind_fibd.txt");
			int result = _sut.MortalFibonacci(inputText);

			_output.WriteLine(result.ToString());
		}
	}

	public class Fibd
	{
		public int MortalFibonacci(string input)
		{
			var splited = input.Split(' ');
			int count = int.Parse(splited[0]);
			int life = int.Parse(splited[1]);
			const int initialAge = 1;

			Rabbit rootRabbit = new Rabbit(null, initialAge);
			for (int i = 1; i <= count; i++)
			{
				var leafRabbits = GetLeafRabbits(rootRabbit, life).ToList();
				foreach (Rabbit rabbit in leafRabbits)
				{
					if (i == count)
						return GetLeafRabbits(rootRabbit, life).Count(r => r.Age <= life);

					if (2 <= rabbit.Age && rabbit.Age <= life)
						rabbit.Add(new Rabbit(rabbit, initialAge));

					rabbit.Age++;
				}

				//RemoveDeadRabbits(leafRabbits, life);
			}

			return 1;
		}

		//private void RemoveDeadRabbits(IEnumerable<Rabbit> rabbits, int life)
		//{
		//	var rabbitList = rabbits.ToList();
		//	for (int i = 0; i < rabbitList.Count; i++)
		//	{
		//		var rabbit = rabbitList[i];
		//		if (rabbit.Parent != null && rabbit.Age > life)
		//			rabbit.Parent.Remove(rabbit);
		//	}
		//}

		private IEnumerable<Rabbit> GetLeafRabbits(Rabbit rabbits, int life)
		{
			if (rabbits.Age <= life)
				yield return rabbits;

			foreach (Component component in rabbits.GetChildren())
			{
				var rabbit = component as Rabbit;
				if (rabbit != null)
				{
					foreach (var leafRabbit in GetLeafRabbits(rabbit, life))
					{
						yield return leafRabbit;
					}
				}
			}

			yield break;
		}
	}

	/// <summary>
	/// The 'Component' abstract class
	/// </summary>
	public abstract class Component
	{
		public abstract void Add(Component c);
		public abstract void Remove(Component c);
		public abstract IEnumerable<Component> GetChildren();
	}

	public class Rabbit : Component
	{
		private readonly List<Component> _children = new List<Component>();

		public int Age { get; set; }
		public Rabbit Parent { get; set; }

		public Rabbit(Rabbit parent, int age)
		{
			Parent = parent;
			Age = age;
		}

		public override void Add(Component c)
		{
			_children.Add(c);
		}

		public override void Remove(Component c)
		{
			_children.Remove(c);
		}

		public override IEnumerable<Component> GetChildren()
		{
			return _children;
		}

		public override string ToString()
		{
			return string.Format("Rabbit.Age: {0}; Children: {1}", Age, _children.Count);
		}
	}
}
