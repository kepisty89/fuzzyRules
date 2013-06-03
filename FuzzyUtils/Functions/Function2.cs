using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyUtils.Functions
{
    class Function2 : IFunction
    {
        public double evaluate(double[] x)
        {
            double exp = Math.Exp(-2 * Math.Log(2) * Math.Pow((x[0] - 0.08) / 0.854, 2));
            double sin = Math.Pow(Math.Sin(5 * Math.PI * (Math.Pow(x[0], 0.75) - 0.05)), 6);
            return exp * sin;
        }
    }
}
