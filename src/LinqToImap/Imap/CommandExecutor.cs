namespace LinqToImap.Imap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Commands;
    using Parsing;

    public class CommandExecutor : ICommandExecutor
    {
        private readonly IImapClient imapClient;

        public CommandExecutor(IImapClient imapClient)
        {
            this.imapClient = imapClient;
        
            Current = this;
        }

        //TODO: This could be done better (CommandExecutor.Current)
        public static ICommandExecutor Current { get; private set; }

        public IEnumerable<string> Execute(Command command)
        {
            imapClient.Write(command.Text);
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

            Type parserType;
            //NOTE: Workaround - needed, cause IMailbox is also IEnumerable<MailboxMessage>, so it finds two parsers.
            if (typeof(T) == typeof(IEnumerable<MailboxMessage>))
            {
                parserType = typeof (MailboxMessagesParser);
            }
            else
            {
                parserType = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(typeof (IParser<T>).IsAssignableFrom).Single();
            }

            var parser = (IParser<T>)Activator.CreateInstance(parserType);
            return parser.Parse(command, responses);
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
                response = imapClient.Read();
                yield return response;
            } while (response.HasInfo());
        }
    }
}