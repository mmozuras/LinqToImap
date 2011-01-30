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
                return new Select().Execute("INBOX");
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            new Logout().Execute();
            client.Dispose();
        }

        #endregion

        public static Gmail Login(string username, string password)
        {
            var connection = new ImapSslClient(host, port);

            new Login().Execute(username, password);
            return new Gmail
                       {
                           client = connection,
                       };
        }
    }
}