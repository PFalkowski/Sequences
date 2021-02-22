namespace Extensions.Standard.Sequences
{
    public class Range : Sequence
    {
        public Range(double min, double max) : base(minInclusive:min, maxInclusive:max, step:1.0) { }
    }
}
