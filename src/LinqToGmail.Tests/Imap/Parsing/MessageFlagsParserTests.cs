namespace LinqToGmail.Tests.Imap.Parsing
{
    using LinqToGmail.Imap;
    using LinqToGmail.Imap.Parsing;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class MessageFlagsParserTests
    {
        private MessageFlagsParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new MessageFlagsParser();
        }

        [Test]
        public void Should_parse_a_string_with_all_flags()
        {
            MessageFlags flags = parser.Parse("\\Flagged   \\Answered \\Recent \\Deleted \\Seen    \\Draft");

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
            MessageFlags flags = parser.Parse("\\Answered \\Draft \\Deleted \\Seen");

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
            MessageFlags flags = parser.Parse(string.Empty);

            flags.Answered.ShouldBeFalse();
            flags.Flagged.ShouldBeFalse();
            flags.Draft.ShouldBeFalse();
            flags.Deleted.ShouldBeFalse();
            flags.Seen.ShouldBeFalse();
            flags.Recent.ShouldBeFalse();
        }
    }
}