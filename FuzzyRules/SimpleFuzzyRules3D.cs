using FuzzyUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyRules
{
    public class SimpleFuzzyRules3D
    {
        private static double ALPHA = 10;

        private double[][] dataIn;
        private IFunction function;
        private double[] dataOut;
        private Dictionary<List<FuzzySet>, Double> rules;

        public SimpleFuzzyRules3D(double[][] dataIn, IFunction function, List<FuzzySet> fuzzySets)
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

        private Dictionary<List<FuzzySet>, Double> generateRules(List<FuzzySet> fuzzySets) {
		    Dictionary<List<FuzzySet>, Double> rules = new Dictionary<List<FuzzySet>, Double>();
		    double sum;
		    foreach (FuzzySet fuzzySet1 in fuzzySets) {
			    foreach (FuzzySet fuzzySet2 in fuzzySets) {
				    sum = 0;
				    double b = 0;
				    for (int j = 0; j < dataIn.Length; j++) {
					    double x1 = dataIn[j][0];
					    double x2 = dataIn[j][1];
					    double membershipMul = fuzzySet1.membership(x1) * fuzzySet2.membership(x2);
					    double w = Math.Pow(membershipMul, ALPHA);
					    sum += w;
					    double y = dataOut[j];
					    b += w * y;
				    }
				    b = sum == 0 ? 0 : b / sum;
                    rules.Add(new List<FuzzySet>() { fuzzySet1, fuzzySet2 }, b);
			    }
		    }
		    return rules;
	    }

        public double getOutput(double[] x)
        {
            double nominator = 0;
            double denominator = 0;
            foreach (KeyValuePair<List<FuzzySet>, Double> rule in rules)
            {
                List<FuzzySet> fuzzySets = rule.Key;
                double b = rule.Value;
                double membershipMul = 1;

                for (int i = 0; i < x.Length; i++)
                {
                    double membership = fuzzySets[i].membership(x[i]);
                    membershipMul *= membership;
                }

                nominator += membershipMul * b;
                denominator += membershipMul;
            }

            return nominator / denominator;
        }

        public double getError(double[][] xs) {
		    double error = 0;
		    foreach (double[] x in xs) {
			    double expected = function.evaluate(x);
			    double actual = getOutput(x);
			    error += Math.Pow(actual - expected, 2);
		    }
		    return Math.Sqrt(error);
	    }
    }
}
