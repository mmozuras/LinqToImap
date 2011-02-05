namespace LinqToGmail.Tests
{
    using System;
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

        [Test, ExpectedException(typeof (ArgumentException), ExpectedMessage = "Parameter should not be null or whitespace.\r\nParameter name: Param")]
        public void Should_use_paramName_in_exception_message()
        {
            Ensure.IsNotNullOrWhiteSpace(null, "Param");
        }
    }
}