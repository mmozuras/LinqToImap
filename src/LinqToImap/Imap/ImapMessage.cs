namespace LinqToImap.Imap
{
    using System;
    using System.Net.Mail;

    public class ImapMessage
    {
        internal ImapMessage()
        {
            Received = null;
            Sent = null;
        }

        public int Id { get; internal set; }

        public string Subject { get; internal set; }

        public MessageFlags Flags { get; internal set; }
        public Addresses Addresses { get; internal set; }

        public DateTime? Received { get; internal set; }
        public DateTime? Sent { get; internal set; }
        public string TimeZone { get; set; }

        public int? Size { get; internal set; }

        public MailMessage ToMailMessage()
        {
            throw new NotImplementedException();
        }
    }
}