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
            var fetch = new Fetch(new int[] { });
            fetch.IsEmpty.ShouldBeTrue();
        }

        [Test]
        public void Should_create_a_valid_command_from_a_collection_of_ids()
        {
            new Fetch(new[] {1, 2, 3}).ToString().ShouldEqual("FETCH 1,2,3 ALL");
        }

        [Test]
        public void Should_create_a_valid_command_from_range_of_ids()
        {
            new Fetch(1.To(10)).ToString().ShouldEqual("FETCH 1:10 ALL");
        }

        [Test]
        public void Should_create_a_valid_command_from_one_id()
        {
            new Fetch(3).ToString().ShouldEqual("FETCH 3 ALL");
        }

        [Test]
        public void Should_create_a_valid_fetch_for_flags()
        {
            new Fetch(3, FetchItem.Flags).ToString().ShouldEqual("FETCH 3 FLAGS");
        }

        [Test]
        public void Should_create_a_valid_fetch_for_date()
        {
            new Fetch(1.To(3), FetchItem.Internaldate).ToString().ShouldEqual("FETCH 1:3 INTERNALDATE");
        }

        [Test]
        public void Should_create_a_valid_fetch_for_size()
        {
            new Fetch(new[] { 1, 2, 3 }, FetchItem.Size).ToString().ShouldEqual("FETCH 1,2,3 RFC822.SIZE");
        }

        [Test]
        public void Should_create_a_valid_fetch_for_envelope()
        {
            new Fetch(17, FetchItem.Envelope).ToString().ShouldEqual("FETCH 17 ENVELOPE");
        }

        [Test]
        public void Should_create_a_valid_fetch_for_body()
        {
            new Fetch(9.To(10), FetchItem.Bodystructure).ToString().ShouldEqual("FETCH 9:10 BODYSTRUCTURE");
        }

        [Test]
        public void Should_create_a_valid_fetch_for_date_flags_and_size()
        {
            new Fetch(3, new[] { FetchItem.Internaldate, FetchItem.Flags, FetchItem.Size }).ToString().ShouldEqual("FETCH 3 (INTERNALDATE FLAGS RFC822.SIZE)");
        }

        [Test]
        public void Should_create_a_valid_fetch_for_envelope_and_bodystructure()
        {
            new Fetch(3, FetchItem.Envelope, FetchItem.Bodystructure).ToString().ShouldEqual("FETCH 3 (ENVELOPE BODYSTRUCTURE)");
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