namespace LinqToImap.Tests.Imap
{
    using FakeItEasy;
    using LinqToImap.Imap;
    using LinqToImap.Imap.Commands;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class CommandExecutorTests
    {
        private CommandExecutor executor;
        private IImapClient imapClient;

        [SetUp]
        public void SetUp()
        {
            imapClient = A.Fake<IImapClient>();

            A.CallTo(() => imapClient.Read())
                .Returns("kw0001 OK Success");

            executor = new CommandExecutor(imapClient);
        }

        [Test]
        public void Should_execute_login()
        {
            executor.Execute(new Login("username", "password")).ShouldNotBeEmpty();
        }

        [Test]
        public void Should_execute_logout()
        {
            executor.Execute(new Logout()).ShouldNotBeEmpty();
        }

        [Test]
        public void Should_execute_fetch_all()
        {
            executor.Execute(new Fetch()).ShouldBeEmpty();
        }

        [Test]
        public void Should_execute_search()
        {
            executor.Execute(new Search()).ShouldBeEmpty();
        }

        [Test]
        public void Should_execute_select()
        {
            executor.Execute(new Select("Inbox")).ShouldNotBeNull();
        }
    }
}