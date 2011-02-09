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
                .Returns("li0001 OK");

            executor = new CommandExecutor(imapClient);
        }

        [Test]
        public void Should_execute_login()
        {
            executor.Execute(new Login("username", "password")).ShouldNotBeNull();
        }

        [Test]
        public void Should_execute_logout()
        {
            executor.Execute(new Logout()).ShouldNotBeNull();
        }

        [Test]
        public void Should_execute_fetch_all()
        {
            executor.Execute(new Fetch()).ShouldBeEmpty();
        }

        [Test]
        public void Should_execute_select()
        {
            executor.Execute(new Select("Inbox")).ShouldNotBeNull();
        }
    }
}