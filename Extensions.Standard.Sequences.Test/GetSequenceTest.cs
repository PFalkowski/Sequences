using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace Extensions.Standard.Sequences.Test
{
    public class GetSequenceTest
    {
        [Fact]
        public void RandomStringTestLength1()
        {
            var rng = new Random();
            var result1 = rng.RandomString(1);
            var result2 = rng.RandomString(10);
            var result3 = rng.RandomString(100);
            var result4 = rng.RandomString(1000);
            Trace.WriteLine($"result1={result1}\nresult2={result2}\nresult3={result3}\nresult4={result4}");

            Assert.Equal(1, result1.Length);
            Assert.Equal(10, result2.Length);
            Assert.Equal(100, result3.Length);
            Assert.Equal(1000, result4.Length);
        }

        [Fact]
        public void RandomStringTestCharacters()
        {
            var rng = new Random();
            var firstInclusive = 'a';
            var lastExclusive = '}';
            var result4 = rng.RandomString(1000, firstInclusive, lastExclusive);
            foreach (var ch in result4)
            {
                Assert.True(ch >= firstInclusive && ch < lastExclusive);
            }
        }

        [Fact]
        public void RandomStringTestCharacters2()
        {
            var rng = new Random();
            var firstInclusive = (char)32;
            var lastExclusive = (char)127;
            var result4 = rng.RandomString(1000, firstInclusive, lastExclusive);
            foreach (var ch in result4)
            {
                Assert.True(ch >= firstInclusive && ch < lastExclusive);
            }
        }

        [Fact]
        public void RandomStringTestLength2()
        {
            var rng = new Random();
            var result1 = rng.RandomString(1, 2);
            var result2 = rng.RandomString(10, 20);
            var result3 = rng.RandomString(100, 200);
            var result4 = rng.RandomString(1000, 2000);
            Trace.WriteLine($"result1={result1}\nresult2={result2}\nresult3={result3}\nresult4={result4}");

            Assert.Equal(1, result1.Length);
            Assert.True(result1.Length >= 1 && result1.Length < 2);
            Assert.True(result2.Length >= 10 && result2.Length < 20);
            Assert.True(result3.Length >= 100 && result3.Length < 200);
            Assert.True(result4.Length >= 1000 && result4.Length < 2000);
        }

        [Fact]
        public void RandomStringTestCharacters3()
        {
            var rng = new Random();
            var firstInclusive = 'a';
            var lastExclusive = '}';
            var result4 = rng.RandomString(1000, 1000, firstInclusive, lastExclusive);
            foreach (var ch in result4)
            {
                Assert.True(ch >= firstInclusive && ch < lastExclusive);
            }
        }

        [Fact]
        public void RandomBoolTest()
        {
            var rng = new Random();
            var length = 10;
            var expected = rng.RandomBools(length);
            var actual = new bool[length];
            var i = 0;
            var enumerable = expected as IList<bool> ?? expected.ToList();
            foreach (var item in enumerable)
            {
                actual[i++] = item;
            }

            Assert.Equal(length, enumerable.Count);
            Assert.Equal(length, actual.Length);
            Assert.True(enumerable.SequenceEqual(actual));
        }

        [Fact]
        public void RandomIntsTest()
        {
            var rng = new Random();
            var length = 10;
            var expected = rng.RandomInts(length);
            var actual = new int[length];
            var i = 0;
            var enumerable = expected as IList<int> ?? expected.ToList();
            foreach (var item in enumerable)
            {
                actual[i++] = item;
            }

            Assert.Equal(length, enumerable.Count);
            Assert.Equal(length, actual.Length);
            Assert.True(enumerable.SequenceEqual(actual));
        }

        [Fact]
        public void RandomDoublesTest()
        {
            var rng = new Random();
            var length = 10;
            var expected = rng.RandomDoubles(length);
            var actual = new double[length];
            var i = 0;
            foreach (var item in expected)// check whether enumerable is not randomized every enumeration.
            {
                actual[i++] = item;
            }

            Assert.Equal(length, expected.Length);
            Assert.Equal(length, actual.Length);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void StrongRandomFloatsTest()
        {
            var rng = new Random();
            var length = 10;
            var expected = rng.RandomFloats(length);
            var actual = new float[length];
            var i = 0;
            foreach (var item in expected)// check whether enumerable is not randomized every enumeration.
            {
                actual[i++] = item;
            }

            Assert.Equal(length, expected.Length);
            Assert.Equal(length, actual.Length);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void StrongRandomDoublesTest()
        {
            var rng = new Random();
            var length = 10;
            var expected = rng.RandomDoubles(length);
            var actual = new double[length];
            var i = 0;
            foreach (var item in expected)// check whether enumerable is not randomized every enumeration.
            {
                actual[i++] = item;
            }

            Assert.Equal(length, expected.Length);
            Assert.Equal(length, actual.Length);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void ConsecutiveIntsTest1()
        {
            var length = 10;
            var expected = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var actual = GetSequence.ConsecutiveInts(0, length);
            Assert.True(expected.SequenceEqual(actual));
            Assert.Equal(length, expected.Length);// iterate over enumerable
            Assert.Equal(length, actual.Count);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void ConsecutiveIntsTest3()
        {
            var length = 10;
            var expected = Enumerable.Range(0, length).ToArray();
            var actual = GetSequence.ConsecutiveInts(0, length);
            Assert.True(expected.SequenceEqual(actual));
            Assert.Equal(length, expected.Length);// iterate over enumerable
            Assert.Equal(length, actual.Count);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void ConsecutiveDoubleTest()
        {
            var length = 10;
            var expected = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var actual = GetSequence.ConsecutiveDoubles(0, length, 1);
            Assert.True(expected.SequenceEqual(actual));
            Assert.Equal(length, expected.Length);// iterate over enumerable
            Assert.Equal(length, actual.Count);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void NormalRandomsTest()
        {
            var rng = new Random();
            var length = 100;
            var mean = 0.0;
            var sd = 1.0;

            var tested = rng.NormRandoms(length, mean, sd);

            var received = tested.Average();
        }

        [Fact]
        public void OccupyMemorytest()
        {
            var howManyBytes = 1024;
            var tested = GetSequence.OccupyMemory<byte>(howManyBytes).ToList();
            Assert.Equal(howManyBytes, tested.Count);
            var tested2 = GetSequence.OccupyMemory<double>(howManyBytes).ToList();
            Assert.Equal(howManyBytes / 8, tested2.Count);
        }
    }
}
