namespace LinqToImap.Imap
{
    using Commands;

    public interface ICommandExecutor
    {
        Response Execute(Command command);
        T Execute<T>(Command<T> command);
    }
}