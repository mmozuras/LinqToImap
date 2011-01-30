namespace LinqToGmail.Imap.Commands
{
    using System.Security.Authentication;

    /// <summary>
    /// Opens the connection.
    /// </summary>
    public class Login : BaseCommand
    {
        public Login(ImapSslClient client) : base(client)
        {
        }

        public void Execute(string username, string password)
        {
            //TODO: Gmail supports AUTH and XAUTH
            Write("LOGIN " + username + " " + password + "\r\n");
            Read();

            string result = Read();
            //TODO: client should be private
            if (!client.IsOk(result))
            {
                throw new AuthenticationException("Authentication failed: " + result);
            }
        }
    }
}