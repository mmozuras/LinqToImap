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

        protected override string Text { get; set; }
    }
}