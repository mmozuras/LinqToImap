namespace LinqToGmail.Tests.Linq
{
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using LinqToGmail.Imap;
    using LinqToGmail.Imap.Commands;
    using LinqToGmail.Linq;
    using NUnit.Framework;

    //TODO: Current tests probably know too much about implementation
    [TestFixture]
    public class GmailQueryableTests
    {
        private GmailQueryable<MailboxMessage> queryable;
        private ICommandExecutor executor;

        [SetUp]
        public void SetUp()
        {
            executor = A.Fake<ICommandExecutor>();

            queryable = new GmailQueryable<MailboxMessage>("Inbox", executor);

            var mailbox = A.Fake<IMailbox>();
            A.CallTo(() => mailbox.MessagesCount).Returns(5);
            A.CallTo(() => executor.Execute(new Select("Inbox"))).Returns(mailbox);

            A.CallTo(() => executor.Execute(new FetchUids(new[] {1}))).Returns(new[] {new Uid(1)});
            A.CallTo(() => executor.Execute(new FetchUids(new[] {2}))).Returns(new[] {new Uid(2)});
            A.CallTo(() => executor.Execute(new FetchUids(new[] {5}))).Returns(new[] {new Uid(5)});
        }

        [Test]
        public void Should_support_no_expressions()
        {
            queryable.ToList();

            A.CallTo(() => executor.Execute(new FetchAll(1, 5)))
                .MustHaveHappened();
        }

        [Test]
        public void Should_support_take()
        {
            queryable.Take(20).ToList();

            A.CallTo(() => executor.Execute(new FetchAll(1, 20)))
                .MustHaveHappened();
        }

        [Test]
        public void Should_support_where_contains()
        {
            var ids = new[] { 1, 2, 3 };

            var search = new Search(new Dictionary<string, string> {{"Subject", "an"}, {"UID", "1:5"}});
            A.CallTo(() => executor.Execute(search))
                .Returns(ids);

            queryable.Where(x => x.Subject.Contains("an")).ToList();

            A.CallTo(() => executor.Execute(new FetchAll(ids)))
                .MustHaveHappened();
        }

        [Test]
        public void Should_support_where_contains_and_take_together()
        {
            var search = new Search(new Dictionary<string, string> { { "Subject", "an" }, { "UID", "1:5" } });
            A.CallTo(() => executor.Execute(search))
                .Returns(new[] { 1, 2, 3 });

            queryable.Where(x => x.Subject.Contains("an")).Take(2).ToList();

            A.CallTo(() => executor.Execute(new FetchAll(new[] { 1, 2 })))
                .MustHaveHappened();
        }

        [Test]
        public void Should_support_take_and_where_together()
        {
            var ids = new[] { 1 };

            var search = new Search(new Dictionary<string, string> { { "Subject", "an" }, { "UID", "1:2" } });
            A.CallTo(() => executor.Execute(search))
                .Returns(ids);

            queryable.Take(2).Where(x => x.Subject.Contains("an")).ToList();

            A.CallTo(() => executor.Execute(new FetchAll(ids)))
                .MustHaveHappened();
        }
    }
}