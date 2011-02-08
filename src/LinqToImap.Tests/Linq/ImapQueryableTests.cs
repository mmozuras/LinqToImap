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
            //TODO: Current tests probably know too much about implementation
            CommandExtensions.Executor = A.Fake<ICommandExecutor>();

            queryable = new ImapQueryable<MailboxMessage>("Inbox", CommandExtensions.Executor);

            var mailbox = A.Fake<IMailbox>();
            A.CallTo(() => mailbox.MessagesCount).Returns(5);

            new Select("Inbox").Returns(mailbox);
            new Search(1.To(5), "Subject an").Returns(new[] {1, 2, 3});
            new Fetch(new[] {1}).Returns(new List<MailboxMessage> {null});
            new Fetch(new[] {5}).Returns(new List<MailboxMessage> {null});
        }

        private ImapQueryable<MailboxMessage> queryable;

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

        [Test]
        public void Should_support_first()
        {
            queryable.First();

            new Fetch(new[] {1}).MustHaveHappened();
        }

        [Test]
        public void Should_support_last()
        {
            queryable.Last();

            new Fetch(new[] {5}).MustHaveHappened();
        }

        [Test]
        public void Should_support_no_expressions()
        {
            queryable.ToList();

            new Fetch(1.To(5)).MustHaveHappened();
        }

        [Test]
        public void Should_support_take()
        {
            queryable.Take(20).ToList();

            new Fetch(1.To(20)).MustHaveHappened();
        }

        [Test]
        public void Should_support_take_and_where_together()
        {
            new Search(1.To(2), "Subject an").Returns(new[] {1});

            queryable.Take(2).Where(x => x.Subject.Contains("an")).ToList();

            new Fetch(new[] {1}).MustHaveHappened();
        }

        [Test]
        public void Should_support_where_answered()
        {
            new Search(1.To(5), "Answered").Returns(new[] { 1, 2, 5 });

            queryable.Where(x => x.Flags.Answered).ToList();

            new Fetch(new[] {1, 2, 5}).MustHaveHappened();
        }

        [Test]
        public void Should_support_where_not_answered()
        {
            new Search(1.To(5), "Not Answered").Returns(new[] {3, 4});

            queryable.Where(x => !x.Flags.Answered).ToList();

            new Fetch(new[] {3, 4}).MustHaveHappened();
        }

        [Test]
        public void Should_support_and()
        {
            new Search(1.To(5), "Answered Subject an").Returns(new[] { 1, 2 });

            queryable.Where(x => x.Flags.Answered && x.Subject.Contains("an")).ToList();

            new Fetch(new[] {1, 2}).MustHaveHappened();
        }

        [Test]
        public void Should_support_where_contains()
        {
            queryable.Where(x => x.Subject.Contains("an")).ToList();

            new Fetch(new[] {1, 2, 3}).MustHaveHappened();
        }

        [Test]
        public void Should_support_where_contains_and_take_together()
        {
            queryable.Where(x => x.Subject.Contains("an")).Take(2).ToList();

            new Fetch(new[] {1, 2}).MustHaveHappened();
        }
    }
}