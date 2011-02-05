namespace LinqToGmail
{
    using System;
    using Imap;
    using Imap.Commands;

    public class Gmail : IDisposable
    {
        private const string host = "imap.gmail.com";
        private const int port = 993;

        private IImapSslClient client;
        private ICommandExecutor commandExecutor;

        public Mailbox Inbox
        {
            get
            {
                return commandExecutor.Execute<Mailbox>(new Select("INBOX"));
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            commandExecutor.Execute(new Logout());
            client.Dispose();
        }

        #endregion

        public static Gmail Login(string username, string password)
        {
            var imapSslClient = new ImapSslClient(host, port);
            var executor = new CommandExecutor(imapSslClient);
            executor.Execute(new Login(username, password));

            return new Gmail
                       {
                           client = imapSslClient,
                           commandExecutor = executor
                       };
        }
    }
}