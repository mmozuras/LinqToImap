namespace LinqToGmail.IntegrationTests
{
    using System;
    using System.Linq;
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
                var messages = gmail.Inbox.Messages.Take(50).ToList();

                Console.WriteLine(string.Join(Environment.NewLine, messages));
                messages.Count().ShouldBeInRange(1, 1000);
            }
        }
    }
}