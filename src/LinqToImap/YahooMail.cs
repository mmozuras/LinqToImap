namespace LinqToImap
{
    using Imap;
    using Imap.Commands;

    public class YahooMail : Mail
    {
        public YahooMail(string username, string password) : base(username, password)
        {
        }

        public IMailbox Inbox
        {
            get { return CommandExecutor.Execute(new Select("Inbox")); }
        }

        protected override int Port
        {
            get { return 993; }
        }

        protected override string Host
        {
            get { return "imap.mail.yahoo.com"; }
        }
    }
}