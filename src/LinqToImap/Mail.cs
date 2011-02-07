namespace LinqToImap
{
    using System;
    using Imap;
    using Imap.Commands;

    public abstract class Mail : IDisposable
    {
        private readonly IImapClient client;
        protected readonly ICommandExecutor CommandExecutor;

        protected Mail(string username, string password)
        {
            client = new ImapSslClient(Host, Port);
            CommandExecutor = new CommandExecutor(client);
            CommandExecutor.Execute(new Login(username, password));
        }

        protected abstract int Port { get; }
        protected abstract string Host { get; }

        public void Dispose()
        {
            CommandExecutor.Execute(new Logout());
            client.Dispose();
        }
    }
}