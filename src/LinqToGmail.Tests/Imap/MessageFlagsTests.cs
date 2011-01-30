namespace LinqToGmail.Tests.Imap
{
    using LinqToGmail.Imap;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class MessageFlagsTests
    {
        [Test]
        public void Should_parse_a_string_with_all_flags()
        {
            MessageFlags flags = MessageFlags.Parse("\\Flagged   \\Answered \\Recent \\Deleted \\Seen    \\Draft");

            flags.Answered.ShouldBeTrue();
            flags.Flagged.ShouldBeTrue();
            flags.Draft.ShouldBeTrue();
            flags.Deleted.ShouldBeTrue();
            flags.Seen.ShouldBeTrue();
            flags.Recent.ShouldBeTrue();
        }

        [Test]
        public void Should_parse_a_string_with_four_flags()
        {
            MessageFlags flags = MessageFlags.Parse("\\Answered \\Draft \\Deleted \\Seen");

            flags.Answered.ShouldBeTrue();
            flags.Draft.ShouldBeTrue();
            flags.Deleted.ShouldBeTrue();
            flags.Seen.ShouldBeTrue();

            flags.Flagged.ShouldBeFalse();
            flags.Recent.ShouldBeFalse();
        }

        [Test]
        public void Should_parse_an_empty_string()
        {
            MessageFlags flags = MessageFlags.Parse(string.Empty);

            flags.Answered.ShouldBeFalse();
            flags.Flagged.ShouldBeFalse();
            flags.Draft.ShouldBeFalse();
            flags.Deleted.ShouldBeFalse();
            flags.Seen.ShouldBeFalse();
            flags.Recent.ShouldBeFalse();
        }
    }
}