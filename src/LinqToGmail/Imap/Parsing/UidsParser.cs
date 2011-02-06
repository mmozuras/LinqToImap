namespace LinqToGmail.Imap.Parsing
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class UidsParser : IParser<IEnumerable<Uid>>
    {
        public IEnumerable<Uid> Parse(IEnumerable<string> input)
        {
            return input.Select(item => Regex.Match(item, "(?<=UID )(\\d+)").Groups[0].Value)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => int.Parse(x))
                .Select(value => new Uid(value));
        }
    }
}