using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyUtils
{
    public interface IFunction
    {
        double evaluate(double[] x);
    }
}