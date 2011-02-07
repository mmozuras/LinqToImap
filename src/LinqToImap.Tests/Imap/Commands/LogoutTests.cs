namespace LinqToImap.Tests.Imap.Commands
{
    using LinqToImap.Imap.Commands;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class LogoutTests
    {
        [Test]
        public void Should_create_a_valid_command()
        {
            new Logout().ToString().ShouldEqual("LOGOUT");
        }
    }
}