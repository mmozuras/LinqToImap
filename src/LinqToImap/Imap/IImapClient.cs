namespace LinqToImap.Imap
{
    using System;

    public interface IImapClient : IDisposable
    {
        void Write(string message);
        string Read();
    }
}