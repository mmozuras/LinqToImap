namespace LinqToGmail
{
    using System;
    using Imap;
    using Imap.Commands;

    public class Gmail : IDisposable
    {
        private const string host = "imap.gmail.com";
        private const int port = 993;

        private ImapSslClient client;

        public Mailbox Inbox
        {
            get
            {
                var @select = new Select(client);
                var fetch = new Fetch(client);

                var mailbox = @select.Execute("INBOX");

                return fetch.Execute(mailbox, mailbox.MessagesCount - 50, mailbox.MessagesCount);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            new Logout(client).Execute();
            client.Dispose();
        }

        #endregion

        public static Gmail Login(string username, string password)
        {
            var connection = new ImapSslClient(host, port);

            new Login(connection).Execute(username, password);
            return new Gmail
                       {
                           client = connection,
                       };
        }
    }
}