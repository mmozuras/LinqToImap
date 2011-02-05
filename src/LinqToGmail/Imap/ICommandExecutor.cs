namespace LinqToGmail.Imap
{
    using System.Collections.Generic;
    using Commands;

    public interface ICommandExecutor
    {
        IEnumerable<string> Execute(ICommand command);
        T Execute<T>(ICommand command);
    }
}