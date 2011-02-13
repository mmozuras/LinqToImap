namespace LinqToImap.Tests.Imap.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using LinqToImap.Imap;
    using LinqToImap.Imap.Parsing;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class ImapMessageParserTests
    {
        [SetUp]
        public void SetUp()
        {
            parser = new ImapMessageParser();
        }

        private ImapMessageParser parser;

        private static IEnumerable<string> GetMessage()
        {
            return File.ReadAllLines(".\\Imap\\Parsing\\imapMessage.txt");
        }

        [Test]
        public void Should_not_throw_an_exception_if_the_second_line_is_ok()
        {
            parser.Parse(null, new Response(GetMessage()));
        }

        [Test]
        public void Should_parse_a_normal_message()
        {
            ImapMessage imapMessage = parser.Parse(null, new Response(GetMessage()));

            imapMessage.Id.ShouldEqual(20951);
            imapMessage.Flags.Answered.ShouldBeFalse();
            imapMessage.Flags.Seen.ShouldBeTrue();
            imapMessage.Subject.ShouldEqual("Subject");
            imapMessage.Addresses.From.Address.ShouldEqual("John.Doe@mail.com");
            imapMessage.Received.ShouldEqual(DateTime.Parse("2011.01.28 11:55:59"));
            imapMessage.Sent.ShouldEqual(DateTime.Parse("2011.01.28 10:00:24"));
            imapMessage.TimeZone.ShouldEqual("+0000");
        }

        [Test, ExpectedException(typeof (ArgumentException))]
        public void Should_throw_exception_if_more_than_one_line_is_passed_as_input()
        {
            var lines = new[] { "1", "2", "li0001 OK" };
            parser.Parse(null, new Response(lines));
        }
    }
}