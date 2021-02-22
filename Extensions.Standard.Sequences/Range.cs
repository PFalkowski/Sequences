namespace Extensions.Standard.Sequences
{
    public class Range : Sequence
    {
        public Range(decimal min, decimal max) : base(minInclusive:min, maxInclusive:max, step:1.0m) { }
    }
}
