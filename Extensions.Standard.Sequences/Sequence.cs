using System;
using System.Collections.Generic;

namespace Extensions.Standard.Sequences
{
    public class Sequence
    {
        /// <summary>
        /// Ordered set of elements &lt;<paramref name="minInclusive"/>..<paramref name="maxInclusive"/>) by <paramref name="step"/>
        /// </summary>
        public Sequence(decimal minInclusive, decimal maxInclusive, decimal step)
        {
            MinInclusive = minInclusive;
            MaxInclusive = maxInclusive;
            Step = step;
            if (!IsWellFormed) throw new ArgumentException(nameof(maxInclusive));
            if (step <= 0) throw new ArgumentOutOfRangeException(nameof(step), "Step must be positive.");
        }

        public decimal MinInclusive { get; }
        public decimal MaxInclusive { get; }
        public decimal Step { get; }
        public bool IsWellFormed => MinInclusive <= MaxInclusive;
        /// <summary>
        /// Length of the range
        /// https://people.richland.edu/james/lecture/m170/ch03-var.html
        /// </summary>
        public decimal Length => MaxInclusive - MinInclusive;
        /// <summary>
        /// Number of elements in the range
        /// </summary>
        public ulong Count => 1 + (ulong)Math.Abs(Length / Step);
        // Mean of the actual elements, which equals Sum / Count. For ranges where
        // (Max - Min) is not a whole multiple of Step the last element is below
        // MaxInclusive, so (Min + Max) / 2 would be wrong; this stays consistent
        // with Sum, Count and Variance.
        public decimal Average => MinInclusive + (Count - 1) * Step / 2;

        public decimal Sum => Count * (2 * MinInclusive + (Count - 1) * Step) / 2;
        public double Variance => Math.Pow((double)Step, 2) * ((double)Count * (double)Count - 1) / 12.0;

        public double StandardDeviation => Math.Sqrt(Variance);

        [Obsolete("Variance now returns the correct population variance (step² × (n²−1) / 12). " +
                  "The previous implementation returned MaxInclusive − MinInclusive (the range). " +
                  "If you need the old value, use (double)(MaxInclusive - MinInclusive) directly.")]
        public double DeprecatedRangeVariance => (double)(MaxInclusive - MinInclusive);

        public IEnumerable<decimal> GetFullSequence()
        {
            var currentValue = MinInclusive;
            while (currentValue <= MaxInclusive)
            {
                yield return currentValue;
                currentValue += Step;
            }
        }
        public bool Contains(decimal x)
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

        public Sequence? GetIntersection(Sequence other)
        {
            if (!IsOverlapping(other)) return null;
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