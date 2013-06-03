using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyRules;
using FuzzyUtils;
using FuzzyUtils.Functions;
using System.IO;

namespace Tests
{
    class FuzzyRulesTest
    {
        private static int RANDOM_POINTS_COUNT = 100;
        private static int FUZZY_SETS_COUNT = 10;

        private Random random = new Random();

        string outputDirectory = "C://FuzzyTests";

        public FuzzyRulesTest()
        {
            Console.WriteLine("\n===============================================================================");
            Console.WriteLine("\tWarning: All test results will be saved in " + outputDirectory + " directory!");
            Console.WriteLine("===============================================================================\n");
        }

        public FuzzyRulesTest(string testFilesOutputDirectory)
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
            testOneDimensionalFunction(FunctionsHelper.GetConcreteFunction(FunNumber.F2), new Range(0, 1), "Function2");
        }

        public void Experiment3() 
        {
            String filename = "Function3";
            StringBuilder expected = new StringBuilder();
            StringBuilder result = new StringBuilder();

            int fuzzySetsCount = 10;

			Range range = new Range(-5, 5);
			IFunction function = FunctionsHelper.GetConcreteFunction(FunNumber.F3);			
			List<FuzzySet> fuzzySets = FuzzySet.coverRangeWithFuzzySets(range, fuzzySetsCount);

			SimpleFuzzyRules3D fuzzyRules = new SimpleFuzzyRules3D(generateRandom2DPoints(range, RANDOM_POINTS_COUNT), function, fuzzySets);

			double[][] testPoints = generateRandom2DPoints(range, RANDOM_POINTS_COUNT);

			foreach (double[] xs in testPoints) 
            {
				double output = fuzzyRules.getOutput(xs);

	//			Console.WriteLine(String.format("f(" + Arrays.toString(xs) + ") = %.2f, expected %.2f", output, function.evaluate(xs)));

				expected.Append(StringUtils.Join(xs, " ") + " " + output + "\n");
				result.Append(StringUtils.Join(xs, " ") + " " + function.evaluate(xs) + "\n");
			}

            // Save results to files.
            FileManagement.WriteToFile(outputDirectory + "//fuzzy_" + filename + "_expected.dat", expected.ToString());
            FileManagement.WriteToFile(outputDirectory + "//fuzzy_" + filename + "_result.dat", result.ToString());

			double error = fuzzyRules.getError(testPoints);
			Console.WriteLine("Error = " + error);
		}

        private void testOneDimensionalFunction(IFunction function, Range range, String filename)
        {
            double[][] testPoints = to2DArray(generateRandomPoints(range, RANDOM_POINTS_COUNT));

            List<FuzzySet> fuzzySets = FuzzySet.coverRangeWithFuzzySets(range, FUZZY_SETS_COUNT);
            SimpleFuzzyRules fuzzyRules = new SimpleFuzzyRules(to2DArray(generateRandomPoints(range, RANDOM_POINTS_COUNT)), function, fuzzySets);

            WriteResults(function, fuzzyRules, testPoints, filename);
        }

        private void WriteResults(IFunction function, SimpleFuzzyRules fuzzyRules, double[][] testPoints, String filename)
        {
            StringBuilder expected = new StringBuilder();
            StringBuilder result = new StringBuilder();

            foreach (double[] xs in testPoints)
            {
                double output = fuzzyRules.getOutput(xs);

                //			Console.WriteLine(String.format("f(" + Arrays.toString(xs) + ") = %.2f, expected %.2f", output, function.evaluate(xs)));

                expected.Append(StringUtils.Join(xs, " ") + " " + output + "\n");
                result.Append(StringUtils.Join(xs, " ") + " " + function.evaluate(xs) + "\n");
            }

            // Save results to files.
            FileManagement.WriteToFile(outputDirectory + "//fuzzy_" + filename + "_expected.dat", expected.ToString());
            FileManagement.WriteToFile(outputDirectory + "//fuzzy_" + filename + "_result.dat", result.ToString());

            // Print Error message.
            double error = fuzzyRules.getError(testPoints);
            Console.WriteLine("Error = " + error);
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