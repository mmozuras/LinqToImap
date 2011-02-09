namespace LinqToImap.Imap
{
    using System;
    using System.Collections.Generic;

    public class ImapException : Exception
    {
        public ImapException(string message) : base(message)
        {
        }

        public ImapException(IEnumerable<string> response) : this(string.Join(Environment.NewLine, response))
        {
        }
    }
}