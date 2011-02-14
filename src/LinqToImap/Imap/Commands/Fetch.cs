namespace LinqToImap.Imap.Commands
{
    using System.Collections.Generic;
    using System.Linq;
    using Utils;
    
    public sealed class Fetch : Command<IEnumerable<ImapMessage>>
    {
        public Fetch(IntRange range, params FetchItem[] items)
        {
            Ensure.IsNotNull(range, "range");

            Text = string.Format("FETCH {0} {1}", range, ToString(items));
        }

        public Fetch(IEnumerable<int> ids, params FetchItem[] items)
        {
            if (ids.Any())
            {
                Text = string.Format("FETCH {0} {1}", string.Join(",", ids), ToString(items));
            }
            else
            {
                Text = string.Empty;
            }
        }

        public Fetch(int id, params FetchItem[] items)
            : this(new[] { id }, items)
        {
        }

        private string ToString(IEnumerable<FetchItem> items)
        {
            if (!items.Any())
            {
                return "ALL";
            }

            if (items.Count() == 1)
            {
                return itemToStringHash[items.Single()];
            }

            return "(" + string.Join(" ", items.Select(x => itemToStringHash[x])) + ")";
        }

        private readonly Dictionary<FetchItem, string> itemToStringHash = new Dictionary<FetchItem, string>
                                                                              {
                                                                                  {FetchItem.Flags, "FLAGS"},
                                                                                  {FetchItem.Internaldate, "INTERNALDATE"},
                                                                                  {FetchItem.Size, "RFC822.SIZE"},
                                                                                  {FetchItem.Envelope, "ENVELOPE"},
                                                                                  {FetchItem.Bodystructure, "BODYSTRUCTURE"},
                                                                              };

        protected override string Text { get; set; }

        public bool IsEmpty
        {
            get { return string.IsNullOrWhiteSpace(Text); }
        }
    }
}