namespace LinqToImap.Tests.Imap.Commands
{
    using LinqToImap.Imap.Commands;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class CommandTests
    {
        [Test]
        public void Should_equal_when_text_is_the_same()
        {
            new Select("inbox").ShouldEqual(new Select("inbox"));
        }

        [Test]
        public void Should_not_equal_when_different_types_of_commands()
        {
            new Select("inbox").ShouldNotEqual<Command>(new Logout());
        }

        [Test]
        public void Should_be_equatable_with_equal_operator()
        {
            var first = new Select("inbox");
            var second = new Select("inbox");
            (first == second).ShouldBeTrue();
        }

        [Test]
        public void Should_be_equatable_with_not_equal_operator()
        {
            var first = new Select("inbox");
            var second = new Select("inbox");
            (first != second).ShouldBeFalse();
        }
    }
}