namespace Extensions.Standard.Sequences
{
    public class Range : Sequence
    {
        public Range(double min, double max) : base(min:min, max:max, step:1.0) { }
    }
}
