namespace LinqToGmail.Tests.Imap.Parsing
{
    using LinqToGmail.Imap.Parsing;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class MailboxParserTests
    {
        [Test]
        public void Should_parse_a_normal_mailbox()
        {
            var input = new[]
                            {
                                @"* FLAGS (\Answered \Flagged \Draft \Deleted \Seen)",
                                @"* OK [PERMANENTFLAGS (\Answered \Flagged \Draft \Deleted \Seen \*)]",
                                @"* 1242 EXISTS",
                                @"* 5 RECENT",
                                @"* OK [UIDVALIDITY 1062186210]",
                                @"* OK [UIDNEXT 1246]",
                                @"a03 OK [READ-WRITE] Completed",
                            };

            var parser = new MailboxParser();
            var mailbox = parser.Parse(input);

            mailbox.Flags.Answered.ShouldEqual(true);
            mailbox.MessagesCount.ShouldEqual(1242);
            mailbox.RecentMessagesCount.ShouldEqual(5);
            mailbox.ReadableAndWritable.ShouldEqual(true);
        }
    }
}