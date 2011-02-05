namespace LinqToGmail
{
    using System;

    public static class Ensure
    {
        public static void IsNotNullOrWhiteSpace(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Parameter should not be null or whitespace.");
            }
        }

        public static void IsNotNullOrWhiteSpace(string input, string paramName)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Parameter should not be null or whitespace.", paramName);
            }
        }
    }
}