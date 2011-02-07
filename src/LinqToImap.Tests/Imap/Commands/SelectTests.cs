namespace LinqToImap.Tests.Imap.Commands
{
    using System;
    using LinqToImap.Imap.Commands;
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
        public void Should_ensure_that_mailbox_name_is_not_empty()
        {
            new Select(string.Empty);
        }
    }
}