﻿using System;
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
		[InlineData("8 3", 7)]
		[InlineData("9 3", 9)]
		[InlineData("10 3", 12)]
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
			const int initialAge = 1;

			Rabbit rootRabbit = new Rabbit(null, initialAge);
			for (int i = 1; i <= count; i++)
			{
				var leafRabbits = GetLeafRabbits(rootRabbit).ToList();
				foreach (Rabbit rabbit in leafRabbits)
				{
					if (i == count)
						return GetLeafRabbits(rootRabbit).Count(r => r.Age <= 3);

					if (2 <= rabbit.Age && rabbit.Age <= 3)
						rabbit.Add(new Rabbit(rabbit, initialAge));

					rabbit.Age++;
				}
			}

			return 1;
		}

		private IEnumerable<Rabbit> GetLeafRabbits(Rabbit rabbits)
		{
			if (rabbits.Age <= 3)
				yield return rabbits;

			foreach (Component component in rabbits.GetChildren())
			{
				var rabbit = component as Rabbit;
				if (rabbit != null)
					foreach (var leafRabbit in GetLeafRabbits(rabbit))
					{
						yield return leafRabbit;
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
