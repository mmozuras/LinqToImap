namespace LinqToImap.Imap.Parsing
{
    using System.Collections.Generic;
    using System.Linq;
    using Commands;

    public class ImapMessagesParser : IParser<IEnumerable<ImapMessage>>
    {
        public IEnumerable<ImapMessage> Parse(Command command, Response response)
        {
            var messageParser = new ImapMessageParser();
            return response.Data.Select(messageParser.Parse).ToList();
        }
    }
}