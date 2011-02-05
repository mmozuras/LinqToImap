namespace LinqToGmail.Imap.Commands
{
    /// <summary>
    /// Opens the connection.
    /// </summary>
    public sealed class Login : BaseCommand
    {
        public Login(string username, string password)
        {
            //TODO: Gmail supports AUTH and XAUTH
            Text = string.Format("LOGIN {0} {1}", username, password);
        }

        public override string Text { get; protected set; }
    }
}