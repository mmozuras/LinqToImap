namespace LinqToGmail.Imap
{
    using System;
    using System.IO;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Text;

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

            string read = Read();
            if (!IsOk(read))
            {
                throw new ApplicationException(read);
            }
        }

        private string Tag
        {
            get { return "kw" + (tag++).ToString().PadLeft(4, '0') + " "; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            tcpClient.Close();
        }

        #endregion

        internal void Write(string message)
        {
            message = Tag + message;
            byte[] command = Encoding.ASCII.GetBytes(message.ToCharArray());
            sslStream.Write(command, 0, command.Length);
        }

        internal string Read()
        {
            return streamReader.ReadLine();
        }

        internal bool IsOk(string line)
        {
            return line.StartsWith("* OK") || line.Substring(7, 2) == "OK";
        }
    }
}