namespace LinqToImap.Tests.Imap.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using LinqToImap.Imap;
    using LinqToImap.Imap.Parsing;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class ImapMessageParserTests
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            parser = new ImapMessageParser();
        }

        #endregion

        private ImapMessageParser parser;

        private static string GetFullMessage()
        {
            return File.ReadAllText(".\\Imap\\Parsing\\imapMessage.txt");
        }

        private static Response Message(string message)
        {
            return new Response(new[] {message, "li0001 OK"});
        }

        [Test]
        public void Should_not_throw_an_exception_if_the_second_line_is_ok()
        {
            parser.Parse(null, Message(GetFullMessage()));
        }

        [Test]
        public void Should_parse_a_normal_message()
        {
            ImapMessage imapMessage = parser.Parse(null, Message(GetFullMessage()));

            imapMessage.Id.ShouldEqual(20951);
            imapMessage.Flags.Answered.ShouldBeFalse();
            imapMessage.Flags.Seen.ShouldBeTrue();
            imapMessage.Subject.ShouldEqual("Subject");
            imapMessage.Addresses.From.Address.ShouldEqual("John.Doe@mail.com");
            imapMessage.Received.ShouldEqual(DateTime.Parse("2011.01.28 11:55:59"));
            imapMessage.Sent.ShouldEqual(DateTime.Parse("2011.01.28 10:00:24"));
            imapMessage.TimeZone.ShouldEqual("+0000");
        }

        [Test]
        public void Should_parse_flags()
        {
            ImapMessage imapMessage = parser.Parse(null, Message("* 1 FETCH (FLAGS (\\Seen))"));

            imapMessage.Flags.Seen.ShouldBeTrue();
            imapMessage.Id.ShouldEqual(1);

            imapMessage.Subject.ShouldBeNull();
            imapMessage.Size.ShouldBeNull();
            imapMessage.Sent.ShouldBeNull();
            imapMessage.Received.ShouldBeNull();
        }

        [Test]
        public void Should_parse_internaldate()
        {
            ImapMessage imapMessage = parser.Parse(null, Message("* 1 FETCH (INTERNALDATE \"15-Nov-2010 02:32:20 +0000\")"));

            imapMessage.Id.ShouldEqual(1);
            imapMessage.Received.ShouldEqual(new DateTime(2010, 11, 15, 4, 32, 20));

            imapMessage.Flags.ShouldBeNull();
            imapMessage.Subject.ShouldBeNull();
            imapMessage.Size.ShouldBeNull();
            imapMessage.Sent.ShouldBeNull();
        }

        [Test]
        public void Should_parse_size()
        {
            ImapMessage imapMessage = parser.Parse(null, Message("* 1 FETCH (RFC822.SIZE 22648)"));

            imapMessage.Id.ShouldEqual(1);
            imapMessage.Size.ShouldEqual(22648);

            imapMessage.Flags.ShouldBeNull();
            imapMessage.Subject.ShouldBeNull();
            imapMessage.Received.ShouldBeNull();
            imapMessage.Sent.ShouldBeNull();
        }

        [Test]
        public void Should_parse_envelope()
        {
            const string envelope = "* 1 FETCH (ENVELOPE (\"Mon, 15 Nov 2010 04:36:44 +0200\" \"=?ISO-8859-13?Q?Message?=\" ((NIL NIL \"email\" \"mail.com\")) ((NIL NIL \"email\" \"mail.com\")) ((NIL NIL \"email\" \"mail.com\")) ((NIL NIL \"email\" \"mail.com\")) ((NIL NIL \"someone\" \"mail.com\")) NIL \"<AcuEbfFZtTgf1iT2RUSxMY9WcXijsw==>\" \"<795805744FEC4B808EB1233C4704B5EA@somewhere.lan>\"))";
            ImapMessage imapMessage = parser.Parse(null, Message(envelope));

            imapMessage.Id.ShouldEqual(1);

            imapMessage.Size.ShouldBeNull();
            imapMessage.Flags.ShouldBeNull();

            imapMessage.Subject.ShouldEqual("Message");
            imapMessage.Sent.ShouldEqual(new DateTime(2010, 11, 15, 4, 36, 44));
            imapMessage.TimeZone.ShouldEqual("+0200");

            var addresses = imapMessage.Addresses;
            addresses.Sender.ToString().ShouldEqual("email@mail.com");
            addresses.ReplyTo.ToString().ShouldEqual("email@mail.com");
            addresses.From.ToString().ShouldEqual("email@mail.com");
            addresses.To.Single().ToString().ShouldEqual("email@mail.com");
            addresses.Cc.Single().ToString().ShouldEqual("someone@mail.com");
        }

        [Test]
        public void Should_parse_bodystructure()
        {
            //TODO: Parse bodystructure
        }

        [Test, ExpectedException(typeof (ArgumentException))]
        public void Should_throw_exception_if_more_than_one_line_is_passed_as_input()
        {
            var lines = new[] {"1", "2", "li0001 OK"};
            parser.Parse(null, new Response(lines));
        }
    }
}