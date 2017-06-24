using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Extensions.Standard.Randomization;

namespace Extensions.Standard.Sequences
{
    public static class GetSequence
    {
        public static T[] OccupyMemory<T>(long howManyBytes) where T : struct
        {
            var sizeOfT = Marshal.SizeOf<T>();
            var howManyObjectsCreate = howManyBytes / sizeOfT;
            return new T[howManyObjectsCreate];
        }

        public static bool[] RandomBools(this Random rng, int n)
        {
            var result = new bool[n];
            for (var i = 0; i < n; ++i)
            {
                result[i] = rng.NextBool();
            }
            return result;
        }

        public static string RandomString(this Random rng, int length, char lowerInclusive = '0', char upperExclusive = '{')
        {
            var stb = new StringBuilder(length);
            for (var i = 0; i < length; ++i)
            {
                stb.Append(rng.NextChar(lowerInclusive, upperExclusive));
            }
            return stb.ToString();
        }

        public static string RandomString(this Random rng, int lengthMin, int lengthMax, char lowerInclusive = '0', char upperExclusive = '{')
        {
            return RandomString(rng, rng.Next(lengthMin, lengthMax), lowerInclusive, upperExclusive);
        }

        public static string RandomStringUpperLetters(this Random rng, int length)
        {
            return RandomString(rng, length, 'A', '[');
        }

        public static string RandomStringLowerLetters(this Random rng, int length)
        {
            return RandomString(rng, length, 'a');
        }

        public static List<int> RandomInts(this Random rng, int n, int min = 0, int max = int.MaxValue)
        {
            if (n < 0 || n > 2147483647L)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }
            var num = new List<int>(n);
            for (var i = 0; i < n; ++i)
            {
                num.Add(rng.Next(min, max));
            }
            return num;
        }

        public static List<int> ConsecutiveInts(int start = 0, int count = 100)
        {
            return Enumerable.Range(start, count).ToList();
        }

        public static double[] RandomDoubles(this Random rng, int n, double min = 0.0, double max = 1.0)
        {
            var num = new double[n];
            for (var i = 0; i < n; ++i)
            {
                num[i] = rng.NextDouble(min, max);
            }
            return num;
        }
        
        public static float[] RandomFloats(this Random rng, int n)
        {
            var temp = new float[n];
            for (var i = 0; i < n; ++i)
            {
                temp[i] = rng.NextFloat();
            }
            return temp;
        }

        public static List<double> ConsecutiveDoubles(double @from = 0, double to = 100, double step = 1.0)
        {
            var lengthPessimistic = (int)((to - @from) / step) + 1;
            var temp = new List<double>(lengthPessimistic);
            for (; @from < to; @from += step)
            {
                temp.Add(@from);
            }
            return temp;
        }

        /// <summary>
        ///     Normally distributed randoms for given mean and SD.
        /// </summary>
        /// <param name="rng"></param>
        /// <param name="n"></param>
        /// <param name="mean"></param>
        /// <param name="sd"></param>
        /// <returns></returns>
        public static double[] NormRandoms(this Random rng, int n, double mean = 0, double sd = 1)
        {
            var temp = new double[n];
            for (var i = 0; i < n; ++i)
            {
                temp[i] = rng.NextNormal(mean, sd);
            }
            return temp;
        }
    }
}
