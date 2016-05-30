using System;

namespace Demo.Rosalind.Tests.IPRB
{
	public class Iprb
	{
		/// <summary>
		/// Answer using "Susannah Go"'s formula on <see cref="https://susannahgo.files.wordpress.com/2015/11/rosalind-iprb.pdf"/>
		/// </summary>
		/// <remarks>
		/// This runs slightly faster than Thagomizer's algorithm.
		/// ( 4k(k − 1) + 3m(m − 1) + 4(2km + 2kn + mn) ) / 4p(p − 1)
		/// </remarks>
		public double GetProbabilityForDominantAllele2(string input)
		{
			var splitted = input.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			double k = double.Parse(splitted[0]);
			double m = double.Parse(splitted[1]);
			double n = double.Parse(splitted[2]);
			double p = k + m + n;

			double result = ((4*k*(k - 1)) + (3*m*(m - 1)) + (4*(2*k*m + 2*k*n + m*n)))/(4*p*(p - 1));
			return result;
		}

		/// <summary>
		/// The probability that two randomly selected mating organisms will produce an individual possessing 
		/// a dominant allele (and thus displaying the dominant phenotype). 
		/// Assume that any two organisms can mate.
		/// </summary>
		/// <remarks>
		/// Answer explanation on Thagomizer.
		/// http://www.thagomizer.com/blog/2014/11/19/approaching-rosalind-problems.html
		/// <see cref="http://www.thagomizer.com/blog/2014/11/19/approaching-rosalind-problems.html"/>
		/// </remarks>
		public double GetProbabilityForDominantAllele(string input)
		{
			var splitted = input.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
			double dominant = double.Parse(splitted[0]);
			double hetero = double.Parse(splitted[1]);
			double recessive = double.Parse(splitted[2]);

			double total = dominant + hetero + recessive;

			double recessiveToRecessive = (recessive / total) * ((recessive - 1) / (total - 1));
			double hetroToHetero = (hetero / total) * ((hetero - 1) / (total - 1));
			double hetroToRecessive = (hetero / total) * (recessive / (total - 1)) +
			                          (recessive / total) * (hetero / (total - 1));

			// 1/4 according to Punnet Square
			const double recessiveProbability = 0.25;
			// 1/2 according to Punnet Square
			const double hetroToRecessiveProbability = 0.5;
			double recessiveTotal = recessiveToRecessive +
			                        (hetroToHetero * recessiveProbability) +
			                        (hetroToRecessive * hetroToRecessiveProbability);

			return 1 - recessiveTotal;
		}
	}
}