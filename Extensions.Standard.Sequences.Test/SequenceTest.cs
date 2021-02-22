using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Extensions.Standard.Sequences.Test
{
    public class SequenceTest
    {
        [Fact]
        public void TestCtor()
        {
            var min = 0.0m;
            var max = 10.0m;
            var step = 1.0m;
            var tested = new Sequence(min, max, step);
            Assert.Equal(min, tested.MinInclusive);
            Assert.Equal(max, tested.MaxInclusive);
            Assert.Equal(step, tested.Step);
        }

        [Fact]
        public void TestGetFullSequence()
        {
            var min = 0.0m;
            var max = 10.0m;
            var step = 1.0m;
            var tested = new Sequence(min, max, step);
            var expected = new List<decimal> { 0.0m, 1.0m, 2.0m, 3.0m, 4.0m, 5.0m, 6.0m, 7.0m, 8.0m, 9.0m, 10.0m };
            var actual = tested.GetFullSequence();
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void TestLength()
        {
            var min = -5.0m;
            var max = 5.0m;
            var step = 1.0m;
            var tested = new Sequence(min, max, step);
            var temp = new List<decimal> { -5, -4, -3, -2, -1.0m, 0.0m, 1.0m, 2.0m, 3.0m, 4.0m, 5.0m };
            var expected = temp.Count - 1;
            var actual = tested.Length;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestCount()
        {
            var min = -5.0m;
            var max = 5.0m;
            var step = 1.0m;
            var tested = new Sequence(min, max, step);
            var temp = new List<decimal> { -5, -4, -3, -2, -1.0m, 0.0m, 1.0m, 2.0m, 3.0m, 4.0m, 5.0m };
            var expected = temp.Count;
            var actual = tested.Count;
            Assert.Equal((ulong)expected, actual);

            min = 0.0m;
            max = 0.0m;
            step = 1.0m;
            tested = new Sequence(min, max, step);
            expected = 1;
            actual = tested.Count;
            Assert.Equal((ulong)expected, actual);

            min = 1000.0m;
            max = 1000.0m;
            step = 1.0m;
            tested = new Sequence(min, max, step);
            expected = 1;
            actual = tested.Count;
            Assert.Equal((ulong)expected, actual);
        }

        [Theory]
        [InlineData(-0.02, 0.02, 0.01)]
        [InlineData(-0.002, 0.02, 0.01)]
        [InlineData(-0.001, 0.05, 0.01)]
        [InlineData(-0.2, 0.2, 0.1)]
        [InlineData(-2, 2, 0.1)]
        public void TestCount2(decimal minIncl, decimal maxIncl, decimal step)
        {
            var tested = new Sequence(minIncl, maxIncl, step);
            var enumeratedSequence = tested.GetFullSequence().ToList();
            Assert.Equal(enumeratedSequence.Count, (int)tested.Count);
        }

        private static decimal EnumerateAndSumNumbers(decimal min, decimal max, decimal step)
        {
            var result = 0.0m;
            for (decimal i = min; i <= max; i += step)
            {
                result += i;
            }
            return result;
        }

        [Fact]
        public void TestSum()
        {
            var min = 0.0m;
            var max = 10.0m;
            var step = 1.0m;
            var tested = new Sequence(min, max, step);
            var expected = 55.0m;
            var actual = tested.Sum;
            Assert.Equal(expected, actual);


            min = -10.0m;
            max = 0.0m;
            step = 1.0m;
            tested = new Sequence(min, max, step);
            expected = -55;
            actual = tested.Sum;
            Assert.Equal(expected, actual);


            min = -101.0m;
            max = -100.0m;
            step = 1.0m;
            tested = new Sequence(min, max, step);
            expected = -201;
            actual = tested.Sum;
            Assert.Equal(expected, actual);

            min = -20000.0m;
            max = -1000.0m;
            step = 20.0m;
            tested = new Sequence(min, max, step);
            expected = -9985500;
            actual = tested.Sum;
            Assert.Equal(expected, actual);

            min = -2;
            max = 2;
            step = 0.5m;
            tested = new Sequence(min, max, step);
            expected = 0.0m;
            actual = tested.Sum;
            Assert.Equal(expected, actual);

            min = -2;
            max = 1;
            step = 1;
            tested = new Sequence(min, max, step);
            expected = -2;
            actual = tested.Sum;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void PrcisionIsNotLost()
        {
            var min = 0.1m;
            var max = 0.2m;
            var step = 0.01m;
            var tested = new Sequence(min, max, step);
            var expected = 1.65m;
            var actual = tested.Sum;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PrcisionIsNotLost2Test()
        {
            var min = 0.0m;
            var max = 10000000m;
            var step = 0.1m;
            var tested = new Sequence(min, max, step);
            var expected = 500000005000000.0m;
            var actual = tested.Sum;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAverage()
        {
            var min = 0.0m;
            var max = 10.0m;
            var step = 1.0m;
            var tested = new Sequence(min, max, step);
            var temp = new List<decimal> { 0.0m, 1.0m, 2, 3, 4.0m, 5.0m, 6.0m, 7.0m, 8.0m, 9.0m, 10.0m };
            var expected = temp.Average();
            var actual = tested.Average;
            Assert.Equal(expected, actual);


            min = -10;
            max = -9;
            step = 1;
            tested = new Sequence(min, max, step);
            temp = new List<decimal> { -10, -9 };
            expected = temp.Average();
            actual = tested.Average;
            Assert.Equal(expected, actual);

            min = -100;
            max = 100;
            step = 100;
            tested = new Sequence(min, max, step);
            temp = new List<decimal> { -100, 0, 100 };
            expected = temp.Average();
            actual = tested.Average;
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// results compared addittionally to http://www.alcula.com/calculators/statistics/variance/
        /// </summary>
        [Fact]
        public void TestVariance()
        {
            var min = 0.0m;
            var max = 10.0m;
            var step = 1.0m;
            var tested = new Sequence(min, max, step);
            var actual = tested.Variance;
            var expected = 10;
            Assert.Equal(expected, actual);

            min = -10.0m;
            max = -0.0m;
            step = 1.0m;
            tested = new Sequence(min, max, step);
            actual = tested.Variance;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestIsWellFormed()
        {
            var min = -0.0m;
            var max = 111111211.0m;
            var step = 1.0m;
            var tested = new Sequence(min, max, step);
            Assert.True(tested.IsWellFormed);


            min = -123123.0m;
            max = -32;
            step = 1.0m;
            tested = new Sequence(min, max, step);
            Assert.True(tested.IsWellFormed);
        }

        [Fact]
        public void TestIsWellFormed2()
        {
            var min = 4;
            var max = 3;
            var step = 1.0m;
            Assert.Throws<ArgumentException>(() => new Sequence(min, max, step));
        }

        [Fact]
        public void TestContainsNumber()
        {
            decimal number = 10;
            var tested = new Sequence(0, 15, 1);
            Assert.True(tested.Contains(number));

            number = 333;
            tested = new Sequence(0, number, 3);
            Assert.True(tested.Contains(number));

            number = 0.3m;
            tested = new Sequence(0, 3, 1);
            Assert.True(tested.Contains(number));

            number = 0.0m;
            tested = new Sequence(number, number, 1);
            Assert.True(tested.Contains(number));

            number = -200.0m;
            tested = new Sequence(number + 2, number + 3, 1);
            Assert.False(tested.Contains(number));
        }

        [Fact]
        public void TestContainsSequence()
        {
            var number = 123.912m;
            var tested = new Sequence(0, 124, 1);
            Assert.True(tested.Contains(number));
        }

        [Fact]
        public void IsOverlappingRetuirnsTrueForOverlappingSequences()
        {
            var sequence1 = new Sequence(0, 124, 1);
            var sequence2 = new Sequence(-100, 129, 1);
            Assert.True(sequence1.IsOverlapping(sequence2));
        }

        [Fact]
        public void IsOverlappingRetuirnsTrueForOverlappingSequences2()
        {
            var sequence1 = new Sequence(0, 124, 1);
            var sequence2 = new Sequence(-10, 129, 1);
            Assert.True(sequence1.IsOverlapping(sequence2));
        }

        [Fact]
        public void IsOverlappingRetuirnsTrueForOverlappingSequences3()
        {
            var sequence1 = new Sequence(0, 124, 1);
            var sequence2 = new Sequence(-10, 10, 1);
            Assert.True(sequence1.IsOverlapping(sequence2));
        }

        [Fact]
        public void IsOverlappingRetuirnsTrueForOverlappingSequences4()
        {
            var sequence1 = new Sequence(0, 124, 1);
            var sequence2 = new Sequence(10, 10, 1);
            Assert.True(sequence1.IsOverlapping(sequence2));
        }

        [Fact]
        public void GetIntersectionReturnsIntersection()
        {
            var sequence1 = new Sequence(0, 124, 1);
            var sequence2 = new Sequence(1, 15, 1);
            var received = sequence1.GetIntersection(sequence2);
            Assert.Equal(15, received.MaxInclusive);
            Assert.Equal(1, received.MinInclusive);
        }

        [Fact]
        public void ToStringReturnsCrucialInfo()
        {
            var sequence1 = new Sequence(0, 124, 1);
            var result = sequence1.ToString();

            Assert.Contains("0", result);
            Assert.Contains("124", result);
            Assert.Contains("1", result);
        }
    }
}
