using FuzzyUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyUtils.Functions
{
    public enum FunNumber
    {
        F1,
        F1A,
        F2,
        F3
    }

	public class FunctionsHelper
	{
		public static double NOISE_LEVEL = 0.05;
		public static double NOISE_LEVEL2 = 0.1;

		public static Random random = new Random();

        public static IFunction GetConcreteFunction(FunNumber functionNumber)
        {
            switch (functionNumber)
            {
                case FunNumber.F1:
                    return new Function1();
                case FunNumber.F1A:
                    return new Function1A();
                case FunNumber.F2:
                    return new Function2();
                case FunNumber.F3:
                    return new Function3();
                default:
                    throw new NotImplementedException("This kind of function is not supported.");
            }
        }

	}
}
