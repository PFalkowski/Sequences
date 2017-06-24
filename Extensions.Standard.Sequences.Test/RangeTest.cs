using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Extensions.Standard.Sequences.Test
{
    public class RangeTest
    {
        [Fact]
        public void CtorInitializesRangeWithStepEqualOne()
        {
            var tested = new Range(-1, 12);
            Assert.Equal(1, tested.Step);
        }
    }
}
