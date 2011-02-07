namespace LinqToImap.Imap.Commands
{
    using Utils;

    public sealed class Select : Command<IMailbox>
    {
        public Select(string mailboxName)
        {
            Ensure.IsNotNullOrWhiteSpace(mailboxName);

            Text = string.Format("SELECT {0}", mailboxName);
        }

        public override string Text { get; protected set; }
    }
}