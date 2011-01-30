namespace LinqToGmail.Imap.Commands
{
    /// <summary>
    /// Closes the connection.
    /// </summary>
    public class Logout : BaseCommand
    {
        public Logout(ImapSslClient client) : base(client)
        {
        }

        public void Execute()
        {
            Client.Write("LOGOUT\r\n");
            Client.Read();
        }
    }
}