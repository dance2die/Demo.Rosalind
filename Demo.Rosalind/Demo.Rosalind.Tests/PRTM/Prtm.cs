using System;
using System.Collections.Generic;

namespace Demo.Rosalind.Tests.PRTM
{
	public class Prtm
	{
		private readonly Dictionary<string, double> _monoisotopicMassTable = new Dictionary<string, double>
		{
			{"A", 71.03711}, {"C", 103.00919}, {"D", 115.02694}, {"E", 129.04259}, {"F", 147.06841},
			{"G", 57.02146}, {"H", 137.05891}, {"I", 113.08406}, {"K", 128.09496}, {"L", 113.08406},
			{"M", 131.04049}, {"N", 114.04293}, {"P", 97.05276}, {"Q", 128.05858}, {"R", 156.10111},
			{"S", 87.03203}, {"T", 101.04768}, {"V", 99.06841}, {"W", 186.07931}, {"Y", 163.06333}
		};

		public double GetProteineMass(string input)
		{
			double result = 0;
			foreach (char c in input)
			{
				result += _monoisotopicMassTable[c.ToString()];
			}
			//double result = input.Sum(c => _monoisotopicMassTable[c.ToString()]);

			const int digits = 3;
			return Math.Round(result, digits);
		}
	}
}