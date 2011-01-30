namespace LinqToGmail.Imap.Commands
{
    /// <summary>
    /// Opens a mailbox with a summary of its status
    /// </summary>
    public class Select : BaseCommand
    {
        public Select(ImapSslClient client) : base(client)
        {
        }

        public Mailbox Execute(string mailboxName)
        {
            Client.Write("SELECT \"" + mailboxName + "\"\r\n");
            return ParseMailbox(mailboxName);
        }
    }
}