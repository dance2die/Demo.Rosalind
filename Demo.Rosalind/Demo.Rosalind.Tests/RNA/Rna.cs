using System.IO;
using System.Text;

namespace Demo.Rosalind.Tests.RNA
{
	public class Rna
	{
		public string TranscribeDnaToRnaFromFile(string filePath)
		{
			string dataset = File.ReadAllText(filePath);
			return TranscribeDnaToRna(dataset);
		}

		public string TranscribeDnaToRna(string dataset)
		{
			const char fromChar = 'T';
			const char toChar = 'U';

			StringBuilder buffer = new StringBuilder(dataset.Length);
			foreach (char c in dataset)
			{
				char charToAppend = c;
				if (c == fromChar)
					charToAppend = toChar;

				buffer.Append(charToAppend);
			}

			return buffer.ToString();
		}
	}
}