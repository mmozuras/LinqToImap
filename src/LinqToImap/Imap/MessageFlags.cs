namespace LinqToImap.Imap
{
    public class MessageFlags
    {
        public bool Seen { get; internal set; }
        public bool Deleted { get; internal set; }
        public bool Draft { get; internal set; }
        public bool Answered { get; internal set; }
        public bool Flagged { get; internal set; }
        public bool Recent { get; internal set; }
    }
}