namespace LinqToGmail.Imap
{
    using System;
    using System.IO;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Text;

    public class ImapSslClient : IImapSslClient
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
        }

        #region IDisposable Members

        public void Dispose()
        {
            tcpClient.Close();
        }

        #endregion

        public void Write(string message)
        {
            string tagNumber = (tag++).ToString("D4");
            var taggedMessage = string.Format("kw{0} {1}{2}", tagNumber, message, Environment.NewLine);
            byte[] command = Encoding.ASCII.GetBytes(taggedMessage);
            sslStream.Write(command, 0, command.Length);
        }

        public string Read()
        {            
            return streamReader.ReadLine();
        }
    }
}