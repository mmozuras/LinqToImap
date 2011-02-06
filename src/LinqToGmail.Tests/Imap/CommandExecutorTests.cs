namespace LinqToGmail.Tests.Imap
{
    using FakeItEasy;
    using LinqToGmail.Imap;
    using LinqToGmail.Imap.Commands;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class CommandExecutorTests
    {
        private CommandExecutor executor;
        private IImapSslClient imapSslClient;

        [SetUp]
        public void SetUp()
        {
            imapSslClient = A.Fake<IImapSslClient>();

            A.CallTo(() => imapSslClient.Read())
                .Returns("kw0001 OK Success");

            executor = new CommandExecutor(imapSslClient);
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
            executor.Execute(new FetchAll()).ShouldBeEmpty();
        }

        [Test]
        public void Should_execute_fetch_uids()
        {
            executor.Execute(new FetchUids()).ShouldBeEmpty();
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