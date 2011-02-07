namespace LinqToImap
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

        public IMailbox Inbox
        {
            get
            {
                return commandExecutor.Execute(new Select("INBOX"));
            }
        }

        public void Dispose()
        {
            commandExecutor.Execute(new Logout());
            client.Dispose();
        }

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