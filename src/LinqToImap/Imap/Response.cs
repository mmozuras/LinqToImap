namespace LinqToImap.Imap
{
    using System.Collections.Generic;
    using System.Linq;

    public class Response
    {
        public Response(IEnumerable<string> lines)
        {
            var status = lines.Last();

            if (!IsOk(status))
            {
                throw new ImapException(lines);
            }

            Data = lines.Where(HasData);
            Status = status;
        }

        public IEnumerable<string > Data { get; private set; }
        public string Status { get; private set; }

        public static Response ReadFrom(IImapClient imapClient)
        {
            var lines = ReadLinesFrom(imapClient).ToList();
            return new Response(lines);
        }

        private static IEnumerable<string> ReadLinesFrom(IImapClient imapClient)
        {
            string response;
            do
            {
                response = imapClient.Read();
                yield return response;
            } while (HasData(response));
        }

        private static bool IsOk(string line)
        {
            return line.StartsWith("* OK") || (line.Length > 7 && line.Substring(7, 2) == "OK");
        }

        private static bool HasData(string response)
        {
            return response.StartsWith("*");
        }
    }
}