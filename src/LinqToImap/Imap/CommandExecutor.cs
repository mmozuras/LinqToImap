namespace LinqToImap.Imap
{
    using System;
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

        public Response Execute(Command command)
        {
            imapClient.Write(command.ToString());
            return Response.ReadFrom(imapClient);
        }

        public T Execute<T>(Command<T> command)
        {
            return Execute<T>((Command) command);
        }

        private T Execute<T>(Command command)
        {
            var responses = Execute(command);

            Type parserType = Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Contains(typeof (IParser<T>))).Single();

            var parser = (IParser<T>)Activator.CreateInstance(parserType);
            return parser.Parse(command, responses);
        }
    }
}