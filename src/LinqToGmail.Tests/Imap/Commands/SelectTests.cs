namespace LinqToGmail.Tests.Imap.Commands
{
    using System;
    using LinqToGmail.Imap.Commands;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class SelectTests
    {
        [Test]
        public void Should_create_a_valid_command()
        {
            new Select("inbox").Text.ShouldEqual("SELECT inbox");
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void Should_throw_exception_if_mailbox_name_is_empty()
        {
            new Select(string.Empty);
        }
    }
}