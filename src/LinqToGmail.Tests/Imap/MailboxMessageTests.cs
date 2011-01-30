namespace LinqToGmail.Tests.Imap
{
    using System;
    using System.IO;
    using LinqToGmail.Imap;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class MailboxMessageTests
    {
        [Test]
        public void Should_be_able_to_parse_from_string()
        {
            string mailboxMessage = File.ReadAllText(".\\Imap\\mailboxMessage.txt");

            MailboxMessage message = MailboxMessage.Parse(mailboxMessage);

            message.Id.ShouldEqual(20951);
            message.Received.ShouldEqual(DateTime.Parse("2011.01.28 11:55:59"));
            message.Subject.ShouldEqual("ES: EventSentry:Service Monitoring:10100 by Email Critical Events");
            message.ReferenceId.ShouldEqual("26352748l.92612859l19179628l147l@mail.com");
        }
    }
}