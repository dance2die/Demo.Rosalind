using System.Collections.Generic;

namespace Demo.Rosalind.Tests.FIBD
{
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