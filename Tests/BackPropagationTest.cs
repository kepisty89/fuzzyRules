using FuzzyUtils;
using FuzzyUtils.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackPropagation;

namespace Tests
{
	class BackPropagationTest
	{
		private static int RANDOM_POINTS_COUNT = 100;

		private Random random = new Random();

		string outputDirectory = "C://FuzzyTests";

		public BackPropagationTest()
		{
			Console.WriteLine("\n===============================================================================");
			Console.WriteLine("\tWarning: All test results will be saved in " + outputDirectory + " directory!");
			Console.WriteLine("===============================================================================\n");
		}

		public BackPropagationTest(string testFilesOutputDirectory)
		{
			outputDirectory = testFilesOutputDirectory;
		}

		public void Experiment1()
		{
			testOneDimensionalFunction(FunctionsHelper.GetConcreteFunction(FunNumber.F1), new Range(0, 2 * Math.PI), "Function1");
			testOneDimensionalFunction(FunctionsHelper.GetConcreteFunction(FunNumber.F1A), new Range(0, 2 * Math.PI), "Function1A");
		}

		public void Experiment2()
		{
			testOneDimensionalFunction(FunctionsHelper.GetConcreteFunction(FunNumber.F2), new Range(0, 1), "Function3");
		}

		public void Experiment3()
		{

			Range range = new Range(-5, 5);
			double[][] testPoints = generateRandom2DPoints(range, RANDOM_POINTS_COUNT);

			IFunction function = FunctionsHelper.GetConcreteFunction(FunNumber.F3);
			NeuroBackPropagation backPropagation = new NeuroBackPropagation(generateRandom2DPoints(range, RANDOM_POINTS_COUNT), function);

			WriteResults(function, backPropagation, testPoints, "Function3");
		}

		public void testOneDimensionalFunction(IFunction function, Range range, String filename)
		{
			NeuroBackPropagation backPropagation = new NeuroBackPropagation(to2DArray(generateRandomPoints(range, RANDOM_POINTS_COUNT)), function);
			double[][] testPoints = to2DArray(generateRandomPoints(range, RANDOM_POINTS_COUNT));
			WriteResults(function, backPropagation, testPoints, filename);
		}

		double[][] to2DArray(double[] xs)
		{
			double[][] result = new double[xs.Length][];
			for (int i = 0; i < xs.Length; i++)
			{
				result[i] = new double[] { xs[i] };
			}
			return result;
		}

		private void WriteResults(IFunction function, NeuroBackPropagation backPropagation, double[][] testPoints, String filename)
		{
			StringBuilder expected = new StringBuilder();
			StringBuilder result = new StringBuilder();

			foreach (double[] xs in testPoints)
			{
				double output = backPropagation.getOutput(xs);

				//			Console.WriteLine(String.format("f(" + Arrays.toString(xs) + ") = %.2f, expected %.2f", output, function.evaluate(xs)));

				expected.Append(StringUtils.Join(xs, " ") + " " + output + "\n");
				result.Append(StringUtils.Join(xs, " ") + " " + function.evaluate(xs) + "\n");
			}

			// Save results to files.
			FileManagement.WriteToFile(outputDirectory + "//neuro_" + filename + "_expected.dat", expected.ToString());
			FileManagement.WriteToFile(outputDirectory + "//neuro_" + filename + "_result.dat", result.ToString());

			// Print Error message.
			double error = backPropagation.getError(testPoints);
			Console.WriteLine("Error = " + error);
		}

		private double[] generateRandomPoints(Range range, int randomPointsCount)
		{
			return generateRandomPoints(range, randomPointsCount, true);
		}

		private double[] generateRandomPoints(Range range, int randomPointsCount, bool sort)
		{
			double[] randomPoints = new double[randomPointsCount];
			for (int i = 0; i < randomPoints.Length; i++)
			{
				randomPoints[i] = randomPoint(range);
			}
			if (sort)
			{
				Array.Sort(randomPoints);
			}
			return randomPoints;
		}

		private double[][] generateRandom2DPoints(Range range, int randomPointsCount)
		{
			double[] xs = generateRandomPoints(range, randomPointsCount);
			double[] ys = generateRandomPoints(range, randomPointsCount, false);
			double[][] result = new double[randomPointsCount][];
			for (int i = 0; i < xs.Length; i++)
			{
				result[i] = new double[] { xs[i], ys[i] };
			}
			return result;
		}

		private double randomPoint(Range range)
		{
			return range.getBegin() + (random.NextDouble() * range.getWidth());
		}
	}
}
