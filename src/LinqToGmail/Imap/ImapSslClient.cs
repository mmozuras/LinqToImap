namespace LinqToGmail.Imap
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Text;
    using Commands;
    using Parsing;

    public class ImapSslClient : IDisposable
    {
        private readonly SslStream sslStream;
        private readonly StreamReader streamReader;
        private readonly TcpClient tcpClient;
        private int tag;

        public ImapSslClient(string hostname, int port)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(hostname, port);

            sslStream = new SslStream(tcpClient.GetStream(), false);
            sslStream.AuthenticateAsClient(hostname);
            streamReader = new StreamReader(sslStream, Encoding.ASCII);

            string response = Read();
            if (!response.IsOk())
            {
                throw new ApplicationException(response);
            }

            Current = this;
        }

        public static ImapSslClient Current { get; private set; }

        public IEnumerable<string> Execute(ICommand command)
        {
            Write(command.Text + "\r\n");
            var responses = ReadRespone().ToList();

            if (!responses.Last().IsOk())
            {
                throw new ApplicationException(string.Join(Environment.NewLine, responses));
            }
            
            return responses;
        }

        public T Execute<T>(ICommand command)
        {
            var responses = Execute(command);

            var parserType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(typeof (IParser<T>).IsAssignableFrom)
                .Single();

            var parser = (IParser<T>) Activator.CreateInstance(parserType);
            return parser.Parse(responses);
        }

        private IEnumerable<string> ReadRespone()
        {
            string response;
            do
            {
                response = Read();
                yield return response;
            } while (response.HasInfo() && !streamReader.EndOfStream);
        }

        #region IDisposable Members

        public void Dispose()
        {
            tcpClient.Close();
        }

        #endregion

        private void Write(string message)
        {
            string tagNumber = (tag++).ToString("D4");
            message = string.Format("kw{0} {1}", tagNumber, message);
            byte[] command = Encoding.ASCII.GetBytes(message.ToCharArray());
            sslStream.Write(command, 0, command.Length);
        }

        private string Read()
        {            
            return streamReader.ReadLine();
        }
    }
}