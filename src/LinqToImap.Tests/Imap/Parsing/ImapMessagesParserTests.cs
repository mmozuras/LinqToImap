namespace LinqToImap.Tests.Imap.Parsing
{
    using System.IO;
    using System.Linq;
    using LinqToImap.Imap;
    using LinqToImap.Imap.Parsing;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class ImapMessagesParserTests
    {
        [Test]
        public void Should_parse_one_normal_message()
        {
            var message = File.ReadAllText(".\\Imap\\Parsing\\imapMessage.txt");

            var parser = new ImapMessagesParser();
            var messages = parser.Parse(null, new Response(new[] {message, "li0001 OK"}));

            messages.Count().ShouldEqual(1);
        }
    }
}