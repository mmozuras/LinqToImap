namespace GmailImapClient
{
    using System;
    using System.Collections.Generic;
    using LinqToGmail.Imap;

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("*GmailImapClient*");
            Console.WriteLine("Created it, so I could test various imap commands, examine responses.");
            Console.WriteLine("Sure, there are other ways to do it, but it's much more fun to use something you wrote yourself ;)");
            Console.WriteLine("Enter 'quit' if you wanna exit.");
            Console.WriteLine();

            using (var client = new ImapSslClient("imap.gmail.com", 993))
            {
                while (true)
                {
                    string line = Console.ReadLine();
                    if (line == "quit")
                    {
                        break;
                    }

                    client.Write(line);

                    foreach (var response in ReadRespones(client))
                    {
                        Console.WriteLine(response);
                    }
                    Console.WriteLine();
                }
            }
        }

        private static IEnumerable<string> ReadRespones(IImapSslClient client)
        {
            string response;
            do
            {
                response = client.Read();
                yield return response;
            } while (response.StartsWith("*"));
        }
    }
}