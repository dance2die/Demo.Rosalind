﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
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
				{"Rosalind_6404", "CCTGCGGAAGATCGGCACTAGAATAGCCAGAACCGTTTCTCTGAGGCTTCCGGCCTTCCCTCCCACTAATAATTCTGAGG"},
				{"Rosalind_5959", "CCATCGGTAGCGCATCCTTAGTCCAATTAAGTCCCTATCCAGGCGCTCCGCCGAAGGTCTATATCCATTTGTCAGCAGACACGC"},
				{"Rosalind_0808", "CCACCCTCGTGGTATGGCTAGGCATTCAGGAACCGGAGAACGCTTCAGACCAGCCCGGACTGGGAACCTGCGGGCAGTAGGTGGAAT"}
			};

			Dictionary<string, string> actual = _sut.ComputeGCContents(SAMPLE_INPUT);

			Assert.True(actual.SequenceEqual(expected));
		}

		[Fact]
		public void TestGetFastaIds()
		{
			var expected = new List<string>
			{
				"Rosalind_6404",
				"Rosalind_5959",
				"Rosalind_0808"
			};

			var gcContent = _sut.ComputeGCContents(SAMPLE_INPUT);
			var actual = _sut.GetFastaIds(gcContent);

			Assert.True(actual.SequenceEqual(expected));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void TestParseFourDigitCodeFromFastaIdThrowsExceptionForEmptyFastaId(string emptyFastaId)
		{
			Assert.Throws<ArgumentException>(() => _sut.ParseFourDigitCode(emptyFastaId));
		}

		[Theory]
		[InlineData("aaa")]
		[InlineData("Rosalind")]
		public void TestParseFourDigitCodeFromFastaIdThrowsExceptionForFastaIdWithoutCorrectPrefix(string badFastaId)
		{
			Assert.Throws<ArgumentException>(() => _sut.ParseFourDigitCode(badFastaId));
		}

		[Theory]
		[InlineData("Rosalind_123")]
		[InlineData("Rosalind_12")]
		[InlineData("Rosalind_1")]
		public void ThrowExceptionIfFastaIdDoesntHaveFourDigitCode(string badFastaId)
		{
			Assert.Throws<ArgumentException>(() => _sut.ParseFourDigitCode(badFastaId));
		}

		[Theory]
		[InlineData("Rosalind_abc")]
		[InlineData("Rosalind_a")]
		[InlineData("Rosalind_xx")]
		public void ThrowExceptionIfFourDigitCodeIsNotNumeric(string badFastaId)
		{
			Assert.Throws<ArgumentException>(() => _sut.ParseFourDigitCode(badFastaId));
		}

		[Theory]
		[InlineData("Rosalind_6404", 6404)]
		[InlineData("Rosalind_5959", 5959)]
		[InlineData("Rosalind_0808", 0808)]
		public void TestParseFourDigitCodeFromFastaId(string fastaId, int expected)
		{
			var actual =_sut.ParseFourDigitCode(fastaId);

			Assert.Equal(expected, actual);
		}
		
		[Theory]
		[InlineData("AGCTATAG", 37.5)]
		[InlineData("CCACCCTCGTGGTATGGCTAGGCATTCAGGAACCGGAGAACGCTTCAGACCAGCCCGGACTGGGAACCTGCGGGCAGTAGGTGGAAT", 60.919540)]
		public void TestGetGCContent(string dnaString, decimal expected)
		{
			decimal actual = _sut.GetGCContent(dnaString);

			const int precision = 3;	// 0.001
			Assert.Equal(expected, actual, precision);
		}

		[Fact]
		public void GetHighestGCContent()
		{
			const string expected = "Rosalind_0808";

			Dictionary<string, string> gcContentMap = _sut.ComputeGCContents(SAMPLE_INPUT);
			string actual = _sut.GetHighestGCContent(gcContentMap);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestOutputHiestGCContent()
		{
			const string expected = @"Rosalind_0808
60.919540";

			string actual = _sut.GetHighestGCContentText(SAMPLE_INPUT);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ShowResult()
		{
			string inputText = File.ReadAllText(@".\GC\rosalind_gc.txt");
			string result = _sut.GetHighestGCContentText(inputText);

			_output.WriteLine(result);
		}
	}

	public class GCContent
	{
		public string GetHighestGCContentText(string input)
		{
			Dictionary<string, string> gcContentMap = ComputeGCContents(input);
			string key = GetHighestGCContent(gcContentMap);

			const int validDeicmals = 6;
			var gcContent = Math.Round(GetGCContent(gcContentMap[key]), validDeicmals);
			return string.Format("{0}{1}{2}", key, Environment.NewLine, gcContent);
		}

		public string GetHighestGCContent(Dictionary<string, string> gcContentMap)
		{
			string maxGCContentKey = string.Empty;
			decimal maxGCContent = 0;
			foreach (KeyValuePair<string, string> content in gcContentMap)
			{
				decimal gcContent = GetGCContent(content.Value);
				if (maxGCContent < gcContent)
				{
					maxGCContent = gcContent;
					maxGCContentKey = content.Key;
				}
			}

			return maxGCContentKey;
		}

		public decimal GetGCContent(string dnaString)
		{
			int gCount = 0;
			int cCount = 0;
			foreach (var dnaChar in dnaString)
			{
				if (dnaChar == 'C') cCount++;
				if (dnaChar == 'G') gCount++;
			}

			int countSum = gCount + cCount;
			if (countSum == 0)
				return 0;

			decimal result = (decimal) (countSum * 100) / dnaString.Length;
			return result;
		}

		public int ParseFourDigitCode(string fastaId)
		{
			if (string.IsNullOrWhiteSpace(fastaId))
				throw new ArgumentException();

			const string fastaIdPrefix = "Rosalind_";
			if (!fastaId.StartsWith(fastaIdPrefix)) 
				throw new ArgumentException();

			string code = fastaId.Substring(fastaIdPrefix.Length);
			if (code.Length != 4)
				throw new ArgumentException();

			int result;
			bool isParsed = int.TryParse(code, out result);
			if (!isParsed)
				throw new ArgumentException();

			return result;
		}

		public IEnumerable<string> GetFastaIds(Dictionary<string, string> gcContent)
		{
			return gcContent.Keys;
		}

		public Dictionary<string, string> ComputeGCContents(string input)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			var splitted = input.Split(new[] { '\n', '\r', '>'}, StringSplitOptions.RemoveEmptyEntries);
			var currentKey = string.Empty;
			// if a line starts with this, then it's a key
			const string keyPrefix = "Rosalind";

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
