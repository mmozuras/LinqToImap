namespace LinqToImap.Tests.Linq
{
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using LinqToImap.Imap;
    using LinqToImap.Imap.Commands;
    using LinqToImap.Linq;
    using LinqToImap.Utils;
    using NUnit.Framework;
    using Should;

   
    [TestFixture]
    public class ImapQueryableTests
    {
        [SetUp]
        public void SetUp()
        {
            executor = A.Fake<ICommandExecutor>();

            queryable = new ImapQueryable<MailboxMessage>("Inbox", executor);

            var mailbox = A.Fake<IMailbox>();

            //TODO: Current tests probably know too much about implementation
            A.CallTo(() => mailbox.MessagesCount).Returns(5);
            A.CallTo(() => executor.Execute(new Select("Inbox"))).Returns(mailbox);

            var search = new Search(new IntRange(1, 5), new Dictionary<string, string> { { "Subject", "an" } });
            A.CallTo(() => executor.Execute(search))
                .Returns(new[] { 1, 2, 3 });

            A.CallTo(() => executor.Execute(new Fetch(new[] { 1 })))
               .Returns(new List<MailboxMessage> { null });
            A.CallTo(() => executor.Execute(new Fetch(new[] { 5 })))
                .Returns(new List<MailboxMessage> { null });
        }

        private ImapQueryable<MailboxMessage> queryable;
        private ICommandExecutor executor;

        [Test]
        public void Should_support_first()
        {
            queryable.First();

            A.CallTo(() => executor.Execute(new Fetch(new[] {1})))
                .MustHaveHappened();
        }

        [Test]
        public void Should_support_last()
        {
            queryable.Last();

            A.CallTo(() => executor.Execute(new Fetch(new[] {5})))
                .MustHaveHappened();
        }

        [Test]
        public void Should_support_no_expressions()
        {
            queryable.ToList();

            A.CallTo(() => executor.Execute(new Fetch(new IntRange(1, 5))))
                .MustHaveHappened();
        }

        [Test]
        public void Should_support_take()
        {
            queryable.Take(20).ToList();

            A.CallTo(() => executor.Execute(new Fetch(new IntRange(1, 20))))
                .MustHaveHappened();
        }

        [Test]
        public void Should_support_take_and_where_together()
        {
            var ids = new[] {1};

            var search = new Search(new IntRange(1, 2), new Dictionary<string, string> {{"Subject", "an"}});
            A.CallTo(() => executor.Execute(search))
                .Returns(ids);

            queryable.Take(2).Where(x => x.Subject.Contains("an")).ToList();

            A.CallTo(() => executor.Execute(new Fetch(ids)))
                .MustHaveHappened();
        }

        [Test]
        public void Should_support_where_contains()
        {
            queryable.Where(x => x.Subject.Contains("an")).ToList();

            A.CallTo(() => executor.Execute(new Fetch(new[] {1, 2, 3})))
                .MustHaveHappened();
        }

        [Test]
        public void Should_support_where_contains_and_take_together()
        {
            queryable.Where(x => x.Subject.Contains("an")).Take(2).ToList();

            A.CallTo(() => executor.Execute(new Fetch(new[] {1, 2})))
                .MustHaveHappened();
        }

        [Test]
        public void Should_support_count()
        {
            queryable.Count().ShouldEqual(5);
        }

        [Test]
        public void Should_support_count_after_a_predicate()
        {
            queryable.Where(x => x.Subject.Contains("an")).Count().ShouldEqual(3);
        }
    }
}