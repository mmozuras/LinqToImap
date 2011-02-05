namespace LinqToGmail.Imap.Parsing
{
    using System.Collections.Generic;
    using System.Linq;

    public class IdsParser : SingleLineParser<IEnumerable<int>>
    {
        public override IEnumerable<int> Parse(string input)
        {
            var result = 0;
            return input.Split()
                .Where(value => int.TryParse(value, out result))
                .Select(value => result);
        }
    }
}