namespace LinqToGmail.Imap.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public abstract string Text { get; protected set; }

        public override string ToString()
        {
            return Text;
        }
    }
}