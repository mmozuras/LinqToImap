namespace LinqToImap.Imap.Parsing
{
    using Commands;

    public interface IParser<out T>
    {
        T Parse(Command command, Response response);
    }
}