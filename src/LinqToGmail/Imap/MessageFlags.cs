namespace LinqToGmail.Imap
{
    public class MessageFlags
    {
        public bool Seen { get; private set; }
        public bool Deleted { get; private set; }
        public bool Draft { get; private set; }
        public bool Answered { get; private set; }
        public bool Flagged { get; private set; }
        public bool Recent { get; private set; }

        /// <summary>
        /// Converts the string representation of a message flags to its <see cref="MessageFlags"/> equivalent.
        /// </summary>
        /// <param name="flags">A string containing the list of message flags.</param>
        public static MessageFlags Parse(string flags)
        {
            var imapMessageFlags = new MessageFlags();

            foreach (string flag in flags.Split())
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