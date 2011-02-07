namespace LinqToImap
{
    using Imap;
    using Imap.Commands;

    public class Gmail : Mail
    {
        public Gmail(string username, string password) : base(username, password)
        {
        }

        protected override int Port
        {
            get { return 993; }
        }

        protected override string Host
        {
            get { return "imap.gmail.com"; }
        }

        public IMailbox Inbox
        {
            get { return CommandExecutor.Execute(new Select("Inbox")); }
        }
    }
}