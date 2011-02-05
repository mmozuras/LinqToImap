namespace LinqToGmail.Tests.Imap.Commands
{
    using System.Collections.Generic;
    using LinqToGmail.Imap.Commands;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class SearchTests
    {
        [Test]
        public void Should_create_a_valid_command()
        {
            var search = new Search(new Dictionary<string, string> {{"SUBJECT", "s"}});
            search.Text.ShouldEqual("SEARCH SUBJECT s");
        }
    }
}