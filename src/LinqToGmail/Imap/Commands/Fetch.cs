namespace LinqToGmail.Imap.Commands
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Fetch<T> : Command<T>
    {
        protected Fetch()
        {
            Text = string.Format("FETCH 1:* {0}", Modifier);
        }

        protected Fetch(int begin, int end)
        {
            Text = string.Format("FETCH {0}:{1} {2}", begin, end, Modifier);
        }

        protected Fetch(IEnumerable<int> ids)
        {
            if (ids.Any())
            {
                Text = string.Format("FETCH {0} {1}", string.Join(",", ids), Modifier);
            }
            else
            {
                Text = string.Format("FETCH 1:* {0}", Modifier);
            }
        }

        protected abstract string Modifier { get; }
        public override string Text { get; protected set; }
    }    
}