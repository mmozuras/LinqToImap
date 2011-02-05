namespace LinqToGmail.Imap
{
    using System.Collections.Generic;
    using System.Net.Mail;

    public class Addresses
    {
        public Addresses(MailAddress from, MailAddress sender, MailAddress replyTo,
                          IEnumerable<MailAddress> to, IEnumerable<MailAddress> cc, IEnumerable<MailAddress> bcc)
        {
            From = from;
            Sender = sender;
            ReplyTo = replyTo;

            To = to;
            Cc = cc;
            Bcc = bcc;
        }

        public MailAddress From { get; private set; }
        public MailAddress Sender { get; private set; }
        public MailAddress ReplyTo { get; private set; }

        public IEnumerable<MailAddress> To { get; private set; }
        public IEnumerable<MailAddress> Cc { get; private set; }
        public IEnumerable<MailAddress> Bcc { get; private set; }        
    }
}