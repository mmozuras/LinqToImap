namespace LinqToGmail.Tests.Utils
{
    using System;
    using LinqToGmail.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class EnsureTests
    {
        [Test]
        public void Should_ensure_that_string_is_not_null_or_white_space()
        {
            Ensure.IsNotNullOrWhiteSpace("text");
            Assert.Throws(typeof (ArgumentException), () => Ensure.IsNotNullOrWhiteSpace(null));
            Assert.Throws(typeof (ArgumentException), () => Ensure.IsNotNullOrWhiteSpace(""));
            Assert.Throws(typeof (ArgumentException), () => Ensure.IsNotNullOrWhiteSpace("    "));
        }

        [Test, ExpectedException(typeof (ArgumentException), ExpectedMessage = "Parameter should not be null or whitespace.\r\nParameter name: Parameter")]
        public void Should_use_parameter_name_in_exception_message()
        {
            Ensure.IsNotNullOrWhiteSpace(null, "Parameter");
        }
    }
}