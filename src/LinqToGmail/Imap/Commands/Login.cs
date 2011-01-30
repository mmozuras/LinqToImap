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
            Client.Write("LOGIN " + username + " " + password + "\r\n");
            Client.Read();

            string result = Client.Read();
            if (!Client.IsOk(result))
            {
                throw new AuthenticationException("Authentication failed: " + result);
            }
        }
    }
}