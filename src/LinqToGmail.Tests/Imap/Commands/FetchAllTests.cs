namespace LinqToGmail.Tests.Imap.Commands
{
    using LinqToGmail.Imap.Commands;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class FetchAllTests
    {
        [Test]
        public void Should_create_a_valid_command_from_a_collection_of_ids()
        {
            new FetchAll(new[] {1, 2, 3}).Text.ShouldEqual("FETCH 1,2,3 ALL");
        }

        [Test]
        public void Should_create_a_valid_command_from_range_of_ids()
        {
            new FetchAll(1, 10).Text.ShouldEqual("FETCH 1:10 ALL");
        }

        [Test]
        public void Should_create_a_command_that_fetches_all_messages_if_no_parameters_specified()
        {
            new FetchAll().Text.ShouldEqual("FETCH 1:* ALL");
        }

        [Test]
        public void Should_create_a_command_that_fetches_all_messages_from_empty_collection()
        {
            new FetchAll(new int[] { }).Text.ShouldEqual("FETCH 1:* ALL");
        }
    }
}