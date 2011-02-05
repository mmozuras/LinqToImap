namespace LinqToGmail.Tests.Imap.Parsing
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
        public void Should_parse_normal_addresses()
        {
            string imapAddress = File.ReadAllLines(".\\Imap\\Parsing\\addresses.txt")[0];

            var parser = new AddressesParser();
            var imapAddresses = parser.Parse(imapAddress);

            imapAddresses.To.Single().ToString().ShouldEqual("\"jane.doe@mail.com\" <jane.doe@mail.com>");

            const string johnDoe = "\"John\" <John.Doe@mail.com>";
            imapAddresses.From.ToString().ShouldEqual(johnDoe);
            imapAddresses.ReplyTo.ToString().ShouldEqual(johnDoe);
            imapAddresses.Sender.ToString().ShouldEqual(johnDoe);
            imapAddresses.Cc.ShouldBeEmpty();
            imapAddresses.Bcc.ShouldBeEmpty();
        }
    }
}