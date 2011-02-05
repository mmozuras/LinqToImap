namespace LinqToGmail.Imap.Commands
{
    public interface ICommand
    {
        //TODO: Needs a better name.
        string Text { get; }
    }
}