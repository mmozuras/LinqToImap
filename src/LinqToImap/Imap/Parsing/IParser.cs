namespace LinqToImap.Imap.Parsing
{
    using System.Collections.Generic;
    using Commands;

    public interface IParser<out T>
    {
        T Parse(Command command, IEnumerable<string> input);
    }
}