namespace LinqToGmail.Imap
{
    using System;

    public interface IImapSslClient : IDisposable
    {
        void Write(string message);
        string Read();
    }
}