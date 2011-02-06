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

        [Test]
        public void Should_create_a_valid_command_from_zero_query_arguments()
        {
            new Search().Text.ShouldEqual("SEARCH ALL");
        }

        [Test]
        public void Should_create_a_valid_command_from_range_of_ids()
        {
            new Search(1, 10, new Dictionary<string, string> {{"SUBJECT", "s"}}).Text.ShouldEqual("SEARCH 1:10 SUBJECT s");
        }

        [Test]
        public void Should_create_a_valid_command_from_collection_of_ids()
        {
            new Search(new[]{1,2,3}, new Dictionary<string,string>{{"SUBJECT", "s"}}).Text.ShouldEqual("SEARCH 1,2,3 SUBJECT s");
        }
    }
}