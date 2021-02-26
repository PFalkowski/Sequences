using System;
using System.Collections.Generic;

namespace Extensions.Standard.Sequences
{
    public class DateTimeRange
    {
        public DateTime MinInclusive { get; }
        private DateTime MaxInclusive { get; }
        private TimeSpan Step { get; }

        public DateTimeRange(DateTime fromInclusive, DateTime toInclusive, TimeSpan step)
        {
            MinInclusive = fromInclusive;
            MaxInclusive = toInclusive;
            Step = step;
        }

        public bool IsWellFormed => MinInclusive <= MaxInclusive;

        public TimeSpan Length => MaxInclusive - MinInclusive;

        public ulong Count => 1 + (ulong)Math.Abs(Length / Step);

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
            return $"{nameof(MinInclusive)} = {MinInclusive}, {nameof(MaxInclusive)} = {MaxInclusive}, {nameof(Step)} = {Step}";
        }
    }
}
