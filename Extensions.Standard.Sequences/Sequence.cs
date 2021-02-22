using System;
using System.Collections.Generic;

namespace Extensions.Standard.Sequences
{
    public class Sequence
    {
        /// <summary>
        /// Ordered set of elements &lt;<paramref name="minInclusive"/>..<paramref name="maxInclusive"/>) by <paramref name="step"/>
        /// </summary>
        public Sequence(double minInclusive, double maxInclusive, double step)
        {
            MinInclusive = minInclusive;
            MaxInclusive = maxInclusive;
            Step = step;
            if (!IsWellFormed) throw new ArgumentException(nameof(maxInclusive));
        }

        public double MinInclusive { get; }
        public double MaxInclusive { get; }
        public double Step { get; }
        public bool IsWellFormed => MinInclusive <= MaxInclusive;
        /// <summary>
        /// Length of the range
        /// https://people.richland.edu/james/lecture/m170/ch03-var.html
        /// </summary>
        public double Length => MaxInclusive - MinInclusive;
        /// <summary>
        /// Number of elements in the range
        /// </summary>
        public ulong Count => 1 + (ulong)Math.Abs(Length / Step);
        public double Average => (MaxInclusive - (MinInclusive < 0 ? -MinInclusive : MinInclusive)) / 2;

        public double Sum => Count * (2 * MinInclusive + (Count - 1) * Step) / 2;
        public double Variance => Math.Pow(MaxInclusive - MinInclusive, 2) / Math.Abs(Length);

        public double StandardDeviation => Math.Sqrt(Variance);

        public IEnumerable<double> GetFullSequence()
        {
            var currentValue = MinInclusive;
            while (currentValue <= MaxInclusive)
            {
                yield return currentValue;
                currentValue += Step;
            }
        }
        public bool Contains(double x)
        {
            return x <= MaxInclusive && x >= MinInclusive;
        }

        public bool Contains(Sequence x)
        {
            return Contains(x.MinInclusive) && Contains(x.MaxInclusive);
        }

        public bool IsOverlapping(Sequence x)
        {
            if (IsWellFormed && x.IsWellFormed)
                return MinInclusive <= x.MaxInclusive && x.MinInclusive <= MaxInclusive;
            else
                return Contains(x.MinInclusive) || Contains(x.MaxInclusive) || x.Contains(MinInclusive) || x.Contains(MaxInclusive);
        }

        public Sequence GetIntersection(Sequence other)
        {
            var min = Math.Max(MinInclusive, other.MinInclusive);
            var max = Math.Min(MaxInclusive, other.MaxInclusive);
            return new Sequence(min, max, Step);
        }

        public override string ToString()
        {
            return $"{nameof(MinInclusive)} = {MinInclusive}, {nameof(MaxInclusive)} = {MaxInclusive}, {nameof(Step)} = {Step}";
        }
    }
}