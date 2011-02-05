namespace LinqToGmail.Imap.Commands
{
    /// <summary>
    /// Opens a mailbox with a summary of its status
    /// </summary>
    public sealed class Select : BaseCommand
    {
        public Select(string mailboxName)
        {
            Text = string.Format("SELECT {0}", mailboxName);
        }

        public override string Text { get; protected set; }
    }
}