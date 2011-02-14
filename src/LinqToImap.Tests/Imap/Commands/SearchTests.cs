namespace LinqToImap.Tests.Imap.Commands
{
    using System;
    using System.Collections.Generic;
    using LinqToImap.Imap.Commands;
    using LinqToImap.Utils;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class SearchTests
    {
        [Test]
        public void Should_create_a_valid_command()
        {
            var search = new Search("SUBJECT s");
            search.ToString().ShouldEqual("SEARCH SUBJECT s");
        }

        [Test]
        public void Should_create_a_valid_command_from_range_of_ids()
        {
            new Search(1.To(10), "SUBJECT s").ToString().ShouldEqual("SEARCH 1:10 SUBJECT s");
        }

        [Test]
        public void Should_create_a_valid_command_from_collection_of_ids()
        {
            new Search(new[] { 1, 2, 3 }, "SUBJECT s").ToString().ShouldEqual("SEARCH 1,2,3 SUBJECT s");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void Should_ensure_that_range_is_not_null()
        {
            new Search((IntRange) null, string.Empty);
        }

        [Test, ExpectedException(typeof (ArgumentNullException))]
        public void Should_ensure_that_ids_are_not_null()
        {
            new Search((IEnumerable<int>) null, string.Empty);
        }
    }
}