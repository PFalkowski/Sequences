using System;
using System.Collections.Generic;

namespace Extensions.Standard.Sequences
{
    public class Sequence
    {
        /// <summary>
        /// Ordered set of elements &lt;<paramref name="min"/>..<paramref name="max"/>) by <paramref name="step"/>
        /// </summary>
        public Sequence(double min, double max, double step)
        {
            Min = min;
            Max = max;
            Step = step;
            if (!IsSequencePossible(min, max, step) || !IsWellFormed) throw new ArgumentException(nameof(max));
        }

        public double Min { get; }
        public double Max { get; }
        public double Step { get; }
        public bool IsWellFormed => Min <= Max;
        /// <summary>
        /// Length of the range
        /// https://people.richland.edu/james/lecture/m170/ch03-var.html
        /// </summary>
        public double Length => Max - Min;
        /// <summary>
        /// Number of elements in the range
        /// </summary>
        public ulong Count => 1 + (ulong)Math.Abs(Length / Step);
        public double Average => (Max - (Min < 0 ? -Min : Min)) / 2;

        public double Sum => Count * (2 * Min + (Count - 1) * Step) / 2;
        public double Variance => Math.Pow(Max - Min, 2) / Math.Abs(Length);

        public double StandardDeviation => Math.Sqrt(Variance);

        public IEnumerable<double> GetFullSequence()
        {
            var currentValue = Min;
            while (currentValue <= Max)
            {
                yield return currentValue;
                currentValue += Step;
            }
        }
        public bool Contains(double x)
        {
            return x <= Max && x >= Min;
        }
        public bool Contains(Sequence x)
        {
            return Contains(x.Min) && Contains(x.Max);
        }
        public bool IsOverlapping(Sequence x)
        {
            if (IsWellFormed && x.IsWellFormed)
                return Min <= x.Max && x.Min <= Max;
            else
                return Contains(x.Min) || Contains(x.Max) || x.Contains(Min) || x.Contains(Max);
        }

        private static bool IsSequencePossible(double min, double max, double step)
        {
            var rem = (max - min) % step;
            const double precision = 0.000001;
            return Math.Abs(rem) <= precision;
        }
    }
}