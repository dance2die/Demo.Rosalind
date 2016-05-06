using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Demo.Rosalind.Tests.GC
{
	public class GCContentTest
	{
		private readonly ITestOutputHelper _output;
		private readonly GCContent _sut;
		const string SAMPLE_INPUT = @">Rosalind_6404
CCTGCGGAAGATCGGCACTAGAATAGCCAGAACCGTTTCTCTGAGGCTTCCGGCCTTCCC
TCCCACTAATAATTCTGAGG
>Rosalind_5959
CCATCGGTAGCGCATCCTTAGTCCAATTAAGTCCCTATCCAGGCGCTCCGCCGAAGGTCT
ATATCCATTTGTCAGCAGACACGC
>Rosalind_0808
CCACCCTCGTGGTATGGCTAGGCATTCAGGAACCGGAGAACGCTTCAGACCAGCCCGGAC
TGGGAACCTGCGGGCAGTAGGTGGAAT";

		public GCContentTest(ITestOutputHelper output)
		{
			_output = output;
			_sut = new GCContent();
		}

		[Fact]
		public void TestParsingFastaFormat()
		{
			var expected = new Dictionary<string, string>
			{
				{">Rosalind_6404", "CCTGCGGAAGATCGGCACTAGAATAGCCAGAACCGTTTCTCTGAGGCTTCCGGCCTTCCCTCCCACTAATAATTCTGAGG"},
				{">Rosalind_5959", "CCATCGGTAGCGCATCCTTAGTCCAATTAAGTCCCTATCCAGGCGCTCCGCCGAAGGTCTATATCCATTTGTCAGCAGACACGC"},
				{">Rosalind_0808", "CCACCCTCGTGGTATGGCTAGGCATTCAGGAACCGGAGAACGCTTCAGACCAGCCCGGACTGGGAACCTGCGGGCAGTAGGTGGAAT"}
			};

			Dictionary<string, string> actual = _sut.ComputeGCContent(SAMPLE_INPUT);

			Assert.True(actual.SequenceEqual(expected));
		}

		[Fact]
		public void TestGetFastaIds()
		{
			var expected = new List<string>
			{
				">Rosalind_6404",
				">Rosalind_5959",
				">Rosalind_0808"
			};

			var gcContent = _sut.ComputeGCContent(SAMPLE_INPUT);
			var actual = _sut.GetFastaIds(gcContent);

			Assert.True(actual.SequenceEqual(expected));
		}
	}

	public class GCContent
	{
		public IEnumerable<string> GetFastaIds(Dictionary<string, string> gcContent)
		{
			return gcContent.Keys;
		}

		public Dictionary<string, string> ComputeGCContent(string input)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			var splitted = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			var currentKey = string.Empty;
			// if a line starts with this, then it's a key
			const string keyPrefix = ">";

			foreach (var line in splitted)
			{
				if (line.StartsWith(keyPrefix))
				{
					currentKey = line;
					result.Add(line, string.Empty);
				}
				else
				{
					result[currentKey] += line;
				}
			}

			return result;
		}
	}
}
