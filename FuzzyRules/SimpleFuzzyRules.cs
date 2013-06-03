using FuzzyUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyRules
{
    public class SimpleFuzzyRules
    {
        private static double ALPHA = 10;

        private double[][] dataIn;
        private IFunction function;
        private double[] dataOut;
        private Dictionary<FuzzySet, Double> rules;

        public SimpleFuzzyRules(double[][] dataIn, IFunction function, List<FuzzySet> fuzzySets)
        {
            this.function = function;
            this.dataIn = dataIn;
            dataOut = getDataOutputs();
            rules = generateRules(fuzzySets);
        }

        private double[] getDataOutputs()
        {
            double[] dataOut = new double[dataIn.Length];
            for (int i = 0; i < dataIn.Length; i++)
            {
                dataOut[i] = function.evaluate(dataIn[i]);
            }
            return dataOut;
        }

        private Dictionary<FuzzySet, Double> generateRules(List<FuzzySet> fuzzySets)
        {
            Dictionary<FuzzySet, Double> rules = new Dictionary<FuzzySet, Double>();
            double sum;
            foreach (FuzzySet fuzzySet in fuzzySets)
            {
                sum = 0;
                double b = 0;
                for (int j = 0; j < dataIn.Length; j++)
                {
                    double membershipMul = 1;
                    for (int k = 0; k < dataIn[j].Length; k++)
                    {
                        double membership = fuzzySet.membership(dataIn[j][k]);
                        membershipMul *= membership;
                    }
                    double w = Math.Pow(membershipMul, ALPHA);
                    sum += w;
                    double y = dataOut[j];
                    b += w * y;
                }

                b /= sum;

                rules.Add(fuzzySet, b);
            }
            return rules;
        }

        public double getOutput(double[] x)
        {
            double nominator = 0;
            double denominator = 0;
            foreach (KeyValuePair<FuzzySet, Double> rule in rules)
            {
                FuzzySet fuzzySet = rule.Key;
                double b = rule.Value;
                double membershipMul = 1;

                for (int i = 0; i < x.Length; i++)
                {
                    double membership = fuzzySet.membership(x[i]);
                    membershipMul *= membership;
                }

                nominator += membershipMul * b;
                denominator += membershipMul;
            }

            return nominator / denominator;
        }

        public double getError(double[][] xs)
        {

            double error = 0;

            foreach (double[] x in xs)
            {
                double expected = function.evaluate(x);
                double actual = getOutput(x);
                error += Math.Pow(actual - expected, 2);
            }

            return Math.Sqrt(error);
        }
    }
}
