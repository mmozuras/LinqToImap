namespace LinqToGmail.Imap.Commands
{
    /// <summary>
    /// Opens a mailbox with a summary of its status
    /// </summary>
    public class Select : BaseCommand
    {
        public Mailbox Execute(string mailboxName)
        {
            Write("SELECT {0}", mailboxName);
            return ParseMailbox(mailboxName);
        }
    }
}