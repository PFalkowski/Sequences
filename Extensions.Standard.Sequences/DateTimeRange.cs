using System;
using System.Collections.Generic;
using System.Globalization;

namespace Extensions.Standard.Sequences
{
    public class DateTimeRange
    {
        public DateTime MinInclusive { get; }
        public DateTime MaxInclusive { get; }
        public TimeSpan Step { get; }

        public DateTimeRange(DateTime fromInclusive, DateTime toInclusive, TimeSpan step)
        {
            MinInclusive = fromInclusive;
            MaxInclusive = toInclusive;
            Step = step;
        }

        public bool IsWellFormed => MinInclusive <= MaxInclusive;

        public TimeSpan Length => MaxInclusive - MinInclusive;

        public ulong Count => 1 + (ulong)Math.Abs((double)Length.Ticks / Step.Ticks);

        public IEnumerable<DateTime> GetFullSequence()
        {
            var currentValue = MinInclusive;
            while (currentValue <= MaxInclusive)
            {
                yield return currentValue;
                currentValue += Step;
            }
        }

        public override string ToString()
        {
            var ic = CultureInfo.InvariantCulture;
            return $"{nameof(MinInclusive)} = {MinInclusive.ToString(ic)}, {nameof(MaxInclusive)} = {MaxInclusive.ToString(ic)}, {nameof(Step)} = {Step}";
        }
    }
}
