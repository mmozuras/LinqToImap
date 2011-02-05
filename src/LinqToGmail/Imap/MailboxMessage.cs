namespace LinqToGmail.Imap
{
    using System;

    public class MailboxMessage
    {
        public MailboxMessage(int id, string referenceId, string subject, MessageFlags flags, Addresses addresses,
                              DateTime received, DateTime sent, string timeZone, int size)
        {
            Id = id;
            ReferenceId = referenceId;

            Subject = subject;

            Flags = flags;
            Addresses = addresses;

            Received = received;
            Sent = sent;
            TimeZone = timeZone;

            Size = size;
        }

        public int Id { get; private set; }
        public string ReferenceId { get; private set; }

        public string Subject { get; private set; }

        public MessageFlags Flags { get; private set; }
        public Addresses Addresses { get; private set; }

        public DateTime Received { get; private set; }
        public DateTime Sent { get; private set; }
        public string TimeZone { get; private set; }

        public int Size { get; private set; }

        public override string ToString()
        {
            return Subject;
        }
    }
}