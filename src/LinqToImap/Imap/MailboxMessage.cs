namespace LinqToImap.Imap
{
    using System;

    public class MailboxMessage
    {
        internal MailboxMessage()
        {
        }

        public int Id { get; internal set; }
        public string ReferenceId { get; internal set; }

        public string Subject { get; internal set; }

        public MessageFlags Flags { get; internal set; }
        public Addresses Addresses { get; internal set; }

        public DateTime Received { get; internal set; }
        public DateTime Sent { get; internal set; }
        public string TimeZone { get; set; }

        public int Size { get; internal set; }

        public override string ToString()
        {
            return Subject;
        }
    }
}