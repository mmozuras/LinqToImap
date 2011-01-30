namespace LinqToGmail.IntegrationTests
{
    using System;

    public static class GmailLogin
    {
        private const string username = "";
        private const string password = "";

        public static string Username
        {
            get
            {
                CheckIfSet();
                return username;
            }
        }

        private static void CheckIfSet()
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("To run integration tests, set GmailLogin.username and GmailLogin.password.");
            }
        }

        public static string Password
        {
            get
            {
                CheckIfSet();
                return password;
            }
        }
    }
}