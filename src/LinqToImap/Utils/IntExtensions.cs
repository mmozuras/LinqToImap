namespace LinqToImap.Utils
{
    public static class IntExtensions
    {
        public static IntRange To(this int from, int to)
        {
            return new IntRange(from, to);
        }
    }
}