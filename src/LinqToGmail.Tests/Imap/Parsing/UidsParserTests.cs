namespace LinqToGmail.Tests.Imap.Parsing
{
    using System.Linq;
    using LinqToGmail.Imap;
    using LinqToGmail.Imap.Parsing;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class UidsParserTests
    {
        private UidsParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new UidsParser();
        }

        [Test]
        public void Should_parse_an_empty_collection()
        {
            parser.Parse(Enumerable.Empty<string>()).ShouldEqual(Enumerable.Empty<Uid>());
        }

        [Test]
        public void Should_parse_a_couple_of_uids()
        {
            parser.Parse(new[]
                             {
                                 "1 FETCH (UID 123)",
                                 "2 FETCH (UID 124)",
                                 "kw0001 OK Success"
                             }).ShouldEqual(new[] {new Uid(123), new Uid(124)});
        }
    }
}