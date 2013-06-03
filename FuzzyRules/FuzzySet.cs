using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyUtils;

namespace FuzzyRules
{
    public class FuzzySet
    {
        private double a;
        private double b;
        private double c;

        public FuzzySet(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public double membership(double x)
        {
            if (a <= x && x <= b)
            {
                if (a == Double.NegativeInfinity)
                {
                    return 1;
                }
                else
                {
                    return (x - a) / (b - a);
                }
            }
            else if (b <= x && x <= c)
            {
                if (c == Double.PositiveInfinity)
                {
                    return 1;
                }
                else
                {
                    return (c - x) / (c - b);
                }
            }
            else
            {
                return 0;
            }
        }

        public static List<FuzzySet> coverRangeWithFuzzySets(Range range, int fuzzySetsCount)
        {
            double halfFuzzySetWidth = range.getWidth() / (fuzzySetsCount - 1);
            
            List<FuzzySet> fuzzySets = new List<FuzzySet>();
            
            fuzzySets.Add(new FuzzySet(Double.NegativeInfinity, range.getBegin(), range.getBegin() + halfFuzzySetWidth));

            for (double a = range.getBegin(); a + 2 * halfFuzzySetWidth <= range.getEnd();
                    a += halfFuzzySetWidth)
            {
                double b = a + halfFuzzySetWidth;
                double c = b + halfFuzzySetWidth;
                fuzzySets.Add(new FuzzySet(a, b, c));
            }

            fuzzySets.Add(new FuzzySet(range.getEnd() - halfFuzzySetWidth, range.getEnd(), Double.PositiveInfinity));

            return fuzzySets;
        }

        public override String ToString()
        {
            return this.GetType().Name + "(" + a + ", " + b + ", " + c + ")";
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            long temp;
            temp = BitConverter.DoubleToInt64Bits(a);
            result = prime * result + (int)(temp ^ (int)((uint)temp >> 32));
            temp = BitConverter.DoubleToInt64Bits(b);
            result = prime * result + (int)(temp ^ (int)((uint)temp >> 32));
            temp = BitConverter.DoubleToInt64Bits(c);
            result = prime * result + (int)(temp ^ (int)((uint)temp >> 32));
            return result;
        }

        public override bool Equals(Object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;
            FuzzySet other = (FuzzySet)obj;
            if (BitConverter.DoubleToInt64Bits(a) != BitConverter.DoubleToInt64Bits(other.a))
                return false;
            if (BitConverter.DoubleToInt64Bits(b) != BitConverter.DoubleToInt64Bits(other.b))
                return false;
            if (BitConverter.DoubleToInt64Bits(c) != BitConverter.DoubleToInt64Bits(other.c))
                return false;
            return true;
        }


    }
}
