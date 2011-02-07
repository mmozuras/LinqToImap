namespace LinqToImap.Imap
{
    using System.Collections.Generic;
    using Commands;

    public interface ICommandExecutor
    {
        IEnumerable<string> Execute(Command command);
        T Execute<T>(Command command);
        T Execute<T>(Command<T> command);
    }
}