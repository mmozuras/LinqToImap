namespace LinqToGmail.Tests.Imap.Parsing
{
    using System.Linq;
    using LinqToGmail.Imap.Parsing;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class IdsParserTests
    {
        private IdsParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new IdsParser();
        }

        [Test]
        public void Should_parse_an_empty_string()
        {
            parser.Parse(string.Empty).ShouldEqual(Enumerable.Empty<int>());
        }

        [Test]
        public void Should_parse_a_couple_of_ids()
        {
            parser.Parse("1 2 3").ShouldEqual(new[] {1, 2, 3});
        }

        [Test]
        public void Should_ignore_text_that_is_not_numbers()
        {
            parser.Parse("Text 1 2 ").ShouldEqual(new[] {1, 2});
        }
    }
}