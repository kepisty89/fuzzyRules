using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyUtils
{
    public class StringUtils
    {
        private StringUtils() { }

        public static String Join<T>(List<T> input, String separator)
        {

            StringBuilder output = new StringBuilder("");
            if (input.Count > 0)
            {

                for (int i = 0; i < input.Count - 1; i++)
                {
                    output.Append(input.ElementAt(i));
                    output.Append(separator);
                }

                output.Append(input.ElementAt(input.Count - 1));
            }
            return output.ToString();
        }

        public static String Join<T>(T[] input, String separator)
        {
            return StringUtils.Join(input.ToList(), separator);
        }

        public static String Join(double[] input, String separator)
        {
            List<Double> list = new List<Double>();
            foreach (double x in input)
            {
                list.Add(x);
            }
            return StringUtils.Join(list, separator);
        }
    }
}
