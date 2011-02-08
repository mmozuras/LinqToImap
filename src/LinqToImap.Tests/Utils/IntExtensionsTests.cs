namespace LinqToImap.Tests.Utils
{
    using LinqToImap.Utils;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class IntExtensionsTests
    {
        [Test]
        public void Should_create_a_valid_int_range_from_1_to_5()
        {
            var intRange = 1.To(5);
            intRange.From.ShouldEqual(1);
            intRange.To.ShouldEqual(5);
        }

        [Test]
        public void Should_create_a_valid_int_range_from_min_to_max()
        {
            var intRange = int.MinValue.To(int.MaxValue);
            intRange.From.ShouldEqual(int.MinValue);
            intRange.To.ShouldEqual(int.MaxValue);
        }
    }
}