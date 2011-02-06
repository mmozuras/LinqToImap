namespace LinqToGmail.Tests.Imap.Commands
{
    using LinqToGmail.Imap.Commands;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class FetchUidsTests
    {
        [Test]
        public void Should_create_a_valid_command_from_a_collection_of_ids()
        {
            new FetchUids(new[] { 9, 10, 11 }).Text.ShouldEqual("FETCH 9,10,11 UID");
        }

        [Test]
        public void Should_create_a_valid_command_from_range_of_ids()
        {
            new FetchUids(1, 10).Text.ShouldEqual("FETCH 1:10 UID");
        }

        [Test]
        public void Should_create_a_command_that_fetches_all_messages_if_no_parameters_specified()
        {
            new FetchUids().Text.ShouldEqual("FETCH 1:* UID");
        }

        [Test]
        public void Should_create_a_command_that_fetches_all_messages_from_empty_collection()
        {
            new FetchUids(new int[] { }).Text.ShouldEqual("FETCH 1:* UID");
        }
    }
}