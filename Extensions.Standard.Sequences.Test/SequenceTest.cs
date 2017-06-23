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
            var min = 0.0;
            var max = 10.0;
            var step = 1.0;
            var tested = new Sequence(min, max, step);
            Assert.Equal(min, tested.Min);
            Assert.Equal(max, tested.Max);
            Assert.Equal(step, tested.Step);
        }
        [Fact]
        public void TestCtorThrowsWhenSeqNotConvergent()
        {
            var min = 0.0;
            var max = 10.02;
            var step = 1.0;

            Assert.Throws<ArgumentException>(() => new Sequence(min, max, step));
        }

        [Fact]
        public void TestGetFullSequence()
        {
            var min = 0.0;
            var max = 10.0;
            var step = 1.0;
            var tested = new Sequence(min, max, step);
            var expected = new List<double> { 0.0, 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0 };
            var actual = tested.GetFullSequence();
            Assert.True(expected.SequenceEqual(actual));
        }
        [Fact]
        public void TestLength()
        {
            var min = -5.0;
            var max = 5.0;
            var step = 1.0;
            var tested = new Sequence(min, max, step);
            var temp = new List<double> { -5, -4, -3, -2, -1.0, 0.0, 1.0, 2.0, 3.0, 4.0, 5.0 };
            var expected = temp.Count - 1;
            var actual = tested.Length;
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TestCount()
        {
            var min = -5.0;
            var max = 5.0;
            var step = 1.0;
            var tested = new Sequence(min, max, step);
            var temp = new List<double> { -5, -4, -3, -2, -1.0, 0.0, 1.0, 2.0, 3.0, 4.0, 5.0 };
            var expected = temp.Count;
            var actual = tested.Count;
            Assert.Equal((ulong)expected, actual);

            min = 0.0;
            max = 0.0;
            step = 1.0;
            tested = new Sequence(min, max, step);
            expected = 1;
            actual = tested.Count;
            Assert.Equal((ulong)expected, actual);

            min = 1000.0;
            max = 1000.0;
            step = 1.0;
            tested = new Sequence(min, max, step);
            expected = 1;
            actual = tested.Count;
            Assert.Equal((ulong)expected, actual);
        }
        private static double EnumerateAndSumNumbers(double min, double max, double step)
        {
            var result = 0.0;
            for (double i = min; i <= max; i += step)
            {
                result += i;
            }
            return result;
        }
        [Fact]
        public void TestSum()
        {
            var min = 0.0;
            var max = 10.0;
            var step = 1.0;
            var tested = new Sequence(min, max, step);
            var expected = 55.0;
            var actual = tested.Sum;
            Assert.Equal(expected, actual);


            min = -10.0;
            max = 0.0;
            step = 1.0;
            tested = new Sequence(min, max, step);
            expected = -55;
            actual = tested.Sum;
            Assert.Equal(expected, actual);


            min = -101.0;
            max = -100.0;
            step = 1.0;
            tested = new Sequence(min, max, step);
            expected = -201;
            actual = tested.Sum;
            Assert.Equal(expected, actual);

            min = -20000.0;
            max = -1000.0;
            step = 20.0;
            tested = new Sequence(min, max, step);
            expected = -9985500;
            actual = tested.Sum;
            Assert.Equal(expected, actual);

            min = -2;
            max = 2;
            step = 0.5;
            tested = new Sequence(min, max, step);
            expected = 0.0;
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
            var min = 0.1;
            var max = 0.2;
            var step = 0.01;
            var tested = new Sequence(min, max, step);
            var expected = 1.65;
            var actual = tested.Sum;

            Assert.Equal(Math.Round(expected, 5), Math.Round(actual, 5));
        }
        [Fact]
        public void PrcisionIsNotLost2Test()
        {
            var min = 0.0;
            var max = 10000000;
            var step = 0.1;
            var tested = new Sequence(min, max, step);
            var expected = 50000005000000.0;
            var actual = tested.Sum;

            Assert.Equal(Math.Round(expected, 5), Math.Round(actual, 5));
        }
        [Fact]
        public void TestAverage()
        {
            var min = 0.0;
            var max = 10.0;
            var step = 1.0;
            var tested = new Sequence(min, max, step);
            var temp = new List<double> { 0.0, 1.0, 2, 3, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0 };
            var expected = temp.Average();
            var actual = tested.Average;
            Assert.Equal(expected, actual);


            min = -10;
            max = -9;
            step = 1;
            tested = new Sequence(min, max, step);
            temp = new List<double> { -10, -9 };
            expected = temp.Average();
            actual = tested.Average;
            Assert.Equal(expected, actual);

            min = -100;
            max = 100;
            step = 100;
            tested = new Sequence(min, max, step);
            temp = new List<double> { -100, 0, 100 };
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
            var min = 0.0;
            var max = 10.0;
            var step = 1.0;
            var tested = new Sequence(min, max, step);
            var actual = tested.Variance;
            var expected = 10;
            Assert.Equal(expected, actual);

            min = -10.0;
            max = -0.0;
            step = 1.0;
            tested = new Sequence(min, max, step);
            actual = tested.Variance;
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TestIsWellFormed()
        {
            var min = -0.0;
            var max = 111111211.0;
            var step = 1.0;
            var tested = new Sequence(min, max, step);
            Assert.True(tested.IsWellFormed);


            min = -123123.0;
            max = -32;
            step = 1.0;
            tested = new Sequence(min, max, step);
            Assert.True(tested.IsWellFormed);
        }
        [Fact]
        public void TestIsWellFormed2()
        {
            var min = 4;
            var max = 3;
            var step = 1.0;
            Assert.Throws<ArgumentException>(() => new Sequence(min, max, step));
        }
        [Fact]
        public void TestIsWellFormed3()
        {
            var min = 3;
            var max = 4;
            var step = 0.7;
            Assert.Throws<ArgumentException>(() => new Sequence(min, max, step));
        }
        [Fact]
        public void TestContainsNumber()
        {
            double number = 10;
            var tested = new Sequence(0, 15, 1);
            Assert.True(tested.Contains(number));

            number = 333;
            tested = new Sequence(0, number, 3);
            Assert.True(tested.Contains(number));

            number = 0.3;
            tested = new Sequence(0, 3, 1);
            Assert.True(tested.Contains(number));

            number = 0.0;
            tested = new Sequence(number, number, 1);
            Assert.True(tested.Contains(number));

            number = -200.0;
            tested = new Sequence(number + 2, number + 3, 1);
            Assert.False(tested.Contains(number));
        }
        [Fact]
        public void TestContainsSequence()
        {
            var number = 123.912;
            var tested = new Sequence(0, 124, 1);
            Assert.True(tested.Contains(number));
        }
    }
}
