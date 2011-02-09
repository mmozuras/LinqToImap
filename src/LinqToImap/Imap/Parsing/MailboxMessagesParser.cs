namespace LinqToImap.Imap.Parsing
{
    using System.Collections.Generic;
    using System.Linq;
    using Commands;

    public class MailboxMessagesParser : IParser<IEnumerable<MailboxMessage>>
    {
        public IEnumerable<MailboxMessage> Parse(Command command, Response response)
        {
            var messageParser = new MailboxMessageParser();
            return response.Data.Select(messageParser.Parse).ToList();
        }
    }
}