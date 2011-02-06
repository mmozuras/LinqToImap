namespace LinqToGmail.Utils
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

        public static void IsNotNull<T>(T obj, string paramName) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}