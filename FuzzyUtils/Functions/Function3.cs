using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyUtils.Functions
{
    class Function3 : IFunction
    {
        public double evaluate(double[] x)
        {
            return 200 - Math.Pow((Math.Pow(x[0], 2) + x[1] - 11), 2) - (x[0] + Math.Pow(x[1], 2) - 7);
        }
    }
}
