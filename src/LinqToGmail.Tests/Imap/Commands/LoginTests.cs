namespace LinqToGmail.Tests.Imap.Commands
{
    using System;
    using LinqToGmail.Imap.Commands;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class LoginTests
    {
        [Test]
        public void Should_create_a_valid_command()
        {
            new Login("username", "password").Text.ShouldEqual("LOGIN username password");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Should_ensure_that_username_is_not_empty()
        {
            new Login(null, "password");
        }

        [Test, ExpectedException(typeof (ArgumentException))]
        public void Should_ensure_that_password_is_not_empty()
        {
            new Login("username", string.Empty);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Should_ensure_that_password_is_not_whitespace()
        {
            new Login("   ", "password");
        }
    }
}