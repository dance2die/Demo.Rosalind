using System.Collections.Generic;

namespace Demo.Rosalind.Tests.FIBD
{
	/// <summary>
	/// The 'Component' abstract class
	/// </summary>
	public abstract class Component
	{
		public abstract void Add(Component c);
		public abstract void Remove(Component c);
		public abstract IEnumerable<Component> GetChildren();
	}
}