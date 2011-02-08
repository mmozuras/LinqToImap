namespace LinqToImap.Tests.Imap.Commands
{
    using System;
    using System.Collections.Generic;
    using LinqToImap.Imap.Commands;
    using LinqToImap.Utils;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class FetchTests
    {
        [Test]
        public void Should_create_a_command_that_fetches_nothing_from_empty_collection()
        {
            //TODO: Figure out if there's a better way to fetch nothing, instead of using max value.
            new Fetch(new int[] { }).Text.ShouldEqual("FETCH 4294967295 ALL");
        }

        [Test]
        public void Should_create_a_command_that_fetches_all_messages_if_no_parameters_specified()
        {
            new Fetch().Text.ShouldEqual("FETCH 1:* ALL");
        }

        [Test]
        public void Should_create_a_valid_command_from_a_collection_of_ids()
        {
            new Fetch(new[] {1, 2, 3}).Text.ShouldEqual("FETCH 1,2,3 ALL");
        }

        [Test]
        public void Should_create_a_valid_command_from_range_of_ids()
        {
            new Fetch(1.To(10)).Text.ShouldEqual("FETCH 1:10 ALL");
        }

        [Test]
        public void Should_create_a_valid_command_from_one_id()
        {
            new Fetch(3).Text.ShouldEqual("FETCH 3 ALL");
        }

        [Test, ExpectedException(typeof (ArgumentNullException))]
        public void Should_ensure_that_ids_are_not_null()
        {
            new Fetch((IEnumerable<int>) null);
        }

        [Test, ExpectedException(typeof (ArgumentNullException))]
        public void Should_ensure_that_range_is_not_null()
        {
            new Fetch((IntRange) null);
        }
    }
}