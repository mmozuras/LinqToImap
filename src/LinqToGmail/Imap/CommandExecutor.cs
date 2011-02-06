namespace LinqToGmail.Imap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Commands;
    using Parsing;

    public class CommandExecutor : ICommandExecutor
    {
        private readonly IImapSslClient imapSslClient;

        public CommandExecutor(IImapSslClient imapSslClient)
        {
            this.imapSslClient = imapSslClient;
        
            Current = this;
        }

        //TODO: This could be done better (CommandExecutor.Current)
        public static ICommandExecutor Current { get; private set; }

        public IEnumerable<string> Execute(Command command)
        {
            imapSslClient.Write(command.Text);
            var responses = ReadResponses().ToList();

            if (!responses.Last().IsOk())
            {
                throw new ApplicationException(string.Join(Environment.NewLine, responses));
            }

            return responses;
        }

        public T Execute<T>(Command command)
        {
            var responses = Execute(command);

            var parserType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(typeof(IParser<T>).IsAssignableFrom).Single();

            var parser = (IParser<T>)Activator.CreateInstance(parserType);
            return parser.Parse(responses);
        }

        public T Execute<T>(Command<T> command)
        {
            return Execute<T>((Command) command);
        }

        private IEnumerable<string> ReadResponses()
        {
            string response;
            do
            {
                response = imapSslClient.Read();
                yield return response;
            } while (response.HasInfo());
        }
    }
}