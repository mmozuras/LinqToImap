namespace LinqToGmail.Imap.Commands
{
    /// <summary>
    /// Closes the connection.
    /// </summary>
    public class Logout : BaseCommand
    {
        public void Execute()
        {
            Write("LOGOUT");
            Read();
        }
    }
}