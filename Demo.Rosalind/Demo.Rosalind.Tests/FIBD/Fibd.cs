using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Demo.Rosalind.Tests.FIBD
{
	public class Fibd
	{
		/// <summary>
		/// https://github.com/ldgarcia/rosalind/blob/master/FIBD/solution.c
		/// </summary>
		public BigInteger MortalFibonacci2(string input)
		{
			var splited = input.Split(' ');
			int count = int.Parse(splited[0]);
			int life = int.Parse(splited[1]);

			var answers = new List<BigInteger>{1, 1, 1};
			for (int i = 3; i <= count; i++)
			{
				answers.Add(answers[i - 1] + answers[i - 2]);
				if (i > life)
				{
					answers[i] -= answers[i - life - 1];
				}
			}

			return answers.Last();
		}

		public int MortalFibonacci(string input)
		{
			var splited = input.Split(' ');
			int count = int.Parse(splited[0]);
			int life = int.Parse(splited[1]);
			const int initialAge = 1;

			Rabbit rootRabbit = new Rabbit(null, initialAge);
			List<Rabbit> leafRabbits = GetLeafRabbits(rootRabbit, life).ToList();
			List<Rabbit> rabbits = new List<Rabbit>
			{
				new Rabbit(null, 1),
				new Rabbit(null, 2)
			};


			for (int i = 4; i <= count; i++)
			{
				var tempRabbits = rabbits.Count > 0 ? new List<Rabbit>(rabbits.Where(r => r.Age <= life)) : leafRabbits;

				foreach (Rabbit rabbit in tempRabbits)
				{
					if (i == count)
					{
						//return GetLeafRabbits(rootRabbit, life).Count(r => r.Age <= life);
						return rabbits.Count(r => r.Age <= life);
					}

					if (2 <= rabbit.Age && rabbit.Age <= life)
					{
						//rabbit.Add(new Rabbit(rabbit, initialAge));
						rabbits.Add(new Rabbit(null, initialAge));
					}

					rabbit.Age++;
				}
			}

			return 1;
		}

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
}