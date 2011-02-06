namespace LinqToGmail.Tests.Imap.Parsing
{
    using System;
    using System.IO;
    using LinqToGmail.Imap.Parsing;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class MailboxMessageParserTests
    {
        private MailboxMessageParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new MailboxMessageParser();
        }

        [Test]
        public void Should_parse_a_normal_message()
        {
            var mailboxMessage = parser.Parse(new[] {GetMessage()});

            mailboxMessage.Id.ShouldEqual(20951);
            mailboxMessage.Flags.Answered.ShouldEqual(false);
            mailboxMessage.Subject.ShouldEqual("Subject");
            mailboxMessage.Addresses.From.Address.ShouldEqual("John.Doe@mail.com");
            mailboxMessage.Received.ShouldEqual(DateTime.Parse("2011.01.28 11:55:59"));
            mailboxMessage.Sent.ShouldEqual(DateTime.Parse("2011.01.28 10:00:24"));
            mailboxMessage.TimeZone.ShouldEqual("+0000");
        }

        private string GetMessage()
        {
            return File.ReadAllLines(".\\Imap\\Parsing\\mailboxMessage.txt")[0];
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Should_throw_exception_if_more_than_one_line_is_passed_as_input()
        {
            parser.Parse(new[] {"1", "2"});
        }

        [Test]
        public void Should_not_throw_an_exception_if_the_second_line_is_ok()
        {
            parser.Parse(new[] {GetMessage(), "kw0001 OK Success"});
        }
    }
}