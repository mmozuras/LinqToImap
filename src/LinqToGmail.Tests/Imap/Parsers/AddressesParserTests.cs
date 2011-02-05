namespace LinqToGmail.Tests.Imap.Parsers
{
    using System.IO;
    using System.Linq;
    using LinqToGmail.Imap.Parsing;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class AddressesParserTests
    {
        [Test]
        public void Should_be_able_to_parse_from_string()
        {
            string imapAddress = File.ReadAllLines(".\\Imap\\addressCollection.txt")[0];

            var parser = new AddressesParser();
            var imapAddresses = parser.Parse(new[] {imapAddress});

            imapAddresses.To.Single().ToString().ShouldEqual("\"jane.doe@mail.com\" <jane.doe@mail.com>");

            const string johnDoe = "\"John\" <John.Doe@mail.com>";
            imapAddresses.From.ToString().ShouldEqual(johnDoe);
            imapAddresses.ReplyTo.ToString().ShouldEqual(johnDoe);
            imapAddresses.Sender.ToString().ShouldEqual(johnDoe);
        }
    }
}