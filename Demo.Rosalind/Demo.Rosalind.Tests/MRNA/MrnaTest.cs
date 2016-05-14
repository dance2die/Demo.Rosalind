using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.MRNA
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// Can't figure out how sample data is converted to sample output.
	/// http://recologia.com.br/2013/05/problema-rosalind-inferring-mrna-from-protein-mrna/
	/// </remarks>
	public class MrnaTest
	{
		private readonly ITestOutputHelper _output;

		public MrnaTest(ITestOutputHelper output)
		{
			_output = output;
		}
	}
}
