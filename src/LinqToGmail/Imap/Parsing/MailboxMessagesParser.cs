namespace LinqToGmail.Imap.Parsing
{
    using System.Collections.Generic;
    using System.Linq;

    public class MailboxMessagesParser : IParser<IEnumerable<MailboxMessage>>
    {
        #region IParser<IEnumerable<MailboxMessage>> Members

        public IEnumerable<MailboxMessage> Parse(IEnumerable<string> input)
        {
            return (from response in input
                    where response.HasInfo()
                    let messageParser = new MailboxMessageParser()
                    let mailboxMessage = messageParser.Parse(new[] {response})
                    select mailboxMessage).ToList();
        }

        #endregion
    }
}