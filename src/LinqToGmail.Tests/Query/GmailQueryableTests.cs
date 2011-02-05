namespace LinqToGmail.Tests.Query
{
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using LinqToGmail.Imap;
    using LinqToGmail.Imap.Commands;
    using LinqToGmail.Query;
    using NUnit.Framework;

    [TestFixture]
    public class GmailQueryableTests
    {
        private GmailQueryable<MailboxMessage> gmailQueryable;
        private ICommandExecutor commandExecutor;

        [SetUp]
        public void SetUp()
        {
            commandExecutor = A.Fake<ICommandExecutor>();

            gmailQueryable = new GmailQueryable<MailboxMessage>(commandExecutor);
        }

        [Test]
        public void Should_support_where_clause()
        {
            gmailQueryable.Where(x => x.Subject.Contains("an")).ToList();

            var search = new Search(new Dictionary<string, string> { { "Subject", "an" } });
            A.CallTo(() => commandExecutor.Execute<IEnumerable<int>>(search))
                .WhenArgumentsMatch(x => x.ElementAt(0).ToString() == search.Text)
                .MustHaveHappened();

            A.CallTo(() => commandExecutor.Execute<IEnumerable<MailboxMessage>>(null))
                .WithAnyArguments()
                .MustHaveHappened();
        }
    }
}