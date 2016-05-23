using System.Linq;
using System.Text;

namespace Demo.Rosalind.Tests.CONS
{
	public class ProfileMatrix
	{
		public int[] A { get; set; }
		public int[] C { get; set; }
		public int[] G { get; set; }
		public int[] T { get; set; }

		public void InitializeMatrix(int columnCount)
		{
			A = new int[columnCount];
			C = new int[columnCount];
			G = new int[columnCount];
			T = new int[columnCount];

			for (int i = 0; i < columnCount; i++)
			{
				A[i] = 0;
				C[i] = 0;
				G[i] = 0;
				T[i] = 0;
			}
		}

		public override string ToString()
		{
			var lineA = string.Format("A: {0}", string.Join(" ", A.Select(intValue => intValue.ToString())));
			var lineC = string.Format("C: {0}", string.Join(" ", C.Select(intValue => intValue.ToString())));
			var lineG = string.Format("G: {0}", string.Join(" ", G.Select(intValue => intValue.ToString())));
			var lineT = string.Format("T: {0}", string.Join(" ", T.Select(intValue => intValue.ToString())));

			StringBuilder buffer = new StringBuilder();
			buffer.AppendLine(lineA).AppendLine(lineC).AppendLine(lineG).AppendLine(lineT);

			return buffer.ToString();
		}
	}
}