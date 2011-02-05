namespace LinqToGmail.Imap.Commands
{
    /// <summary>
    /// Closes the connection.
    /// </summary>
    public sealed class Logout : BaseCommand
    {
        public Logout()
        {
            Text = "LOGOUT";
        }

        public override string Text { get; protected set; }
    }
}