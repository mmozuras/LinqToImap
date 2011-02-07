namespace LinqToImap.Imap.Parsing
{
    public class MessageFlagsParser : SingleLineParser<MessageFlags>
    {
        public override MessageFlags Parse(string input)
        {
            var imapMessageFlags = new MessageFlags();

            foreach (var flag in input.Split())
            {
                switch (flag.Trim())
                {
                    case "\\Draft":
                        imapMessageFlags.Draft = true;
                        break;
                    case "\\Answered":
                        imapMessageFlags.Answered = true;
                        break;
                    case "\\Flagged":
                        imapMessageFlags.Flagged = true;
                        break;
                    case "\\Deleted":
                        imapMessageFlags.Deleted = true;
                        break;
                    case "\\Seen":
                        imapMessageFlags.Seen = true;
                        break;
                    case "\\Recent":
                        imapMessageFlags.Recent = true;
                        break;
                }
            }
            return imapMessageFlags;
        }
    }
}