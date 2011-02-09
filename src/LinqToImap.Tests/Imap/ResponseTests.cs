namespace LinqToImap.Tests.Imap
{
    using System.Linq;
    using LinqToImap.Imap;
    using NUnit.Framework;
    using Should;

    [TestFixture]
    public class ResponseTests
    {
        [Test, ExpectedException(typeof(ImapException))]
        public void Should_throw_exception_if_response_status_is_bad()
        {
            new Response(new[] {"* BAD Empty command line"});
        }

        [Test, ExpectedException(typeof(ImapException))]
        public void Should_throw_exception_if_response_with_tag_is_bad()
        {
            new Response(new[] { "li0001 BAD Command line too long" });
        }

        [Test]
        public void Should_create_response_from_valid_lines()
        {
            var response = new Response(new[] {"* Data", "li0001 OK"});
            response.Data.First().ShouldEqual("* Data");
            response.Status.ShouldEqual("li0001 OK");
        }
    }
}