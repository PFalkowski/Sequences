using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Extensions.Standard.Sequences.Test
{
    public class DateTimeRangeTests
    {
        [Fact]
        public void TestOneYear()
        {
            DateTime minIncl = new DateTime(2020, 01, 01);
            DateTime maxIncl = new DateTime(2021, 01, 01);
            TimeSpan step = TimeSpan.FromDays(365);
            var tested = new DateTimeRange(minIncl, maxIncl, step);
            var enumeratedSequence = tested.GetFullSequence().ToList();
            Assert.Equal(enumeratedSequence.Count, (int)tested.Count);
        }

        [Fact]
        public void TestOneYearNotFull()
        {
            DateTime minIncl = new DateTime(2020, 01, 01);
            DateTime maxIncl = new DateTime(2020, 12, 31);
            TimeSpan step = TimeSpan.FromDays(365);
            var tested = new DateTimeRange(minIncl, maxIncl, step);
            var enumeratedSequence = tested.GetFullSequence().ToList();
            Assert.Equal(enumeratedSequence.Count, (int)tested.Count);
        }

        [Fact]
        public void Test12Months()
        {
            DateTime minIncl = new DateTime(2020, 01, 01);
            DateTime maxIncl = new DateTime(2021, 01, 01);
            TimeSpan step = TimeSpan.FromDays(31);
            var tested = new DateTimeRange(minIncl, maxIncl, step);
            var enumeratedSequence = tested.GetFullSequence().ToList();
            Assert.Equal(enumeratedSequence.Count, (int)tested.Count);
        }

        [Fact]
        public void Test10YearsPerMonth()
        {
            DateTime minIncl = new DateTime(2011, 01, 01);
            DateTime maxIncl = new DateTime(2021, 01, 01);
            TimeSpan step = TimeSpan.FromDays(30);
            var tested = new DateTimeRange(minIncl, maxIncl, step);
            var enumeratedSequence = tested.GetFullSequence().ToList();
            Assert.Equal(enumeratedSequence.Count, (int)tested.Count);
            Assert.Equal("MinInclusive = 01.01.2011 00:00:00, MaxInclusive = 01.01.2021 00:00:00, Step = 30.00:00:00", tested.ToString());
        }
    }
}
