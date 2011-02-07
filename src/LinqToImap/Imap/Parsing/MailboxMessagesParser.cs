namespace LinqToImap.Imap.Parsing
{
    using System.Collections.Generic;
    using System.Linq;
    using Commands;

    public class MailboxMessagesParser : IParser<IEnumerable<MailboxMessage>>
    {
        public IEnumerable<MailboxMessage> Parse(Command command, IEnumerable<string> input)
        {
            return (from response in input
                    where response.HasInfo()
                    let messageParser = new MailboxMessageParser()
                    let mailboxMessage = messageParser.Parse(response)
                    select mailboxMessage).ToList();
        }
    }
}