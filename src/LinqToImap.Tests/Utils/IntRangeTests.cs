namespace LinqToImap.Tests.Utils
{
    using System;
    using LinqToImap.Utils;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class IntRangeTests
    {
        [Test]
        public void Should_override_to_string()
        {
            new IntRange(1, 10).ToString().ShouldEqual("1:10");
        }

        [Test]
        public void Should_equal_when_from_and_to_are_the_same()
        {
            new IntRange(1, 5).ShouldEqual(new IntRange(1, 5));
        }

        [Test]
        public void Should_not_equal_when_from_and_to_are_different()
        {
            new IntRange(1, 5).ShouldNotEqual(new IntRange(1, 3));
        }

        [Test]
        public void Should_not_equal_null()
        {
            new IntRange(1, 5).ShouldNotEqual(null);
        }

        [Test]
        public void Should_equal_itself()
        {
            var range = new IntRange(1, 2);
            range.ShouldEqual(range);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Should_not_let_create_an_int_range_where_from_is_greater_than_to()
        {
            new IntRange(5, 1);
        }

        [Test]
        public void Should_create_an_int_range_where_from_and_to_are_equal()
        {
            var range = new IntRange(1, 1);
            range.To.ShouldEqual(1);
            range.From.ShouldEqual(1);
        }
    }
}