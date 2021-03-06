﻿namespace LinqToImap.Tests.Imap.Parsing
{
    using LinqToImap.Imap;
    using LinqToImap.Imap.Commands;
    using LinqToImap.Imap.Parsing;
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
                                @"li0001 OK [READ-WRITE] Inbox selected. (Success)",
                            };
            var parser = new MailboxParser();
            var mailbox = parser.Parse(new Select("Inbox"), new Response(input));

            mailbox.Flags.Answered.ShouldEqual(true);
            mailbox.MessagesCount.ShouldEqual(1242);
            mailbox.RecentMessagesCount.ShouldEqual(5);
            mailbox.ReadableAndWritable.ShouldEqual(true);
        }
    }
}