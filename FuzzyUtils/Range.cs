using System;

namespace FuzzyUtils
{
    public class Range
    {
        private double begin;

        private double end;

        public Range(double begin, double end)
        {
            if (begin > end)
            {
                throw new ArgumentException("Invalid range");
            }
            this.begin = begin;
            this.end = end;
        }

        public double getBegin()
        {
            return begin;
        }

        public double getEnd()
        {
            return end;
        }

        public double getWidth()
        {
            return end - begin;
        }
    }
}
