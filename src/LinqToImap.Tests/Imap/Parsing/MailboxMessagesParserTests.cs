namespace LinqToImap.Tests.Imap.Parsing
{
    using System.IO;
    using System.Linq;
    using LinqToImap.Imap;
    using LinqToImap.Imap.Parsing;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class MailboxMessagesParserTests
    {
        [Test]
        public void Should_parse_one_normal_message()
        {
            var message = File.ReadAllLines(".\\Imap\\Parsing\\mailboxMessage.txt");

            var parser = new MailboxMessagesParser();
            var mailboxMessages = parser.Parse(null, new Response(message));

            mailboxMessages.Count().ShouldEqual(1);
        }
    }
}