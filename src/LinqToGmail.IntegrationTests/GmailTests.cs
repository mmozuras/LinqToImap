namespace LinqToGmail.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Imap;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class GmailTests
    {
        [Test]
        public void Should_login_and_get_messages_from_inbox()
        {
            using (Gmail gmail = Gmail.Login(GmailLogin.Username, GmailLogin.Password))
            {
                IList<MailboxMessage> messages = gmail.Inbox.Messages;

                Console.WriteLine(string.Join(Environment.NewLine, messages));
                messages.Count().ShouldBeInRange(1, 1000);
            }
        }
    }
}