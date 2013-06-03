using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyUtils.Functions
{
	class Function1 : IFunction
	{
		public double evaluate(double[] x)
		{
			return Math.Sin(x[0]) + noise();
		}

		private double noise()
		{
			return FunctionsHelper.random.NextDouble() * (2 * FunctionsHelper.NOISE_LEVEL) - FunctionsHelper.NOISE_LEVEL;
		}
	}
}
