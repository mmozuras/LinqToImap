namespace LinqToImap.Imap
{
    using System;
    using System.IO;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Text;

    public class ImapSslClient : IImapClient
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
            streamReader = new StreamReader(sslStream);

            string response = Read();
            if (!response.StartsWith("* OK"))
            {
                throw new ImapException(response);
            }
        }

        public void Dispose()
        {
            tcpClient.Close();
        }

        public void Write(string message)
        {
            string tagNumber = (tag++).ToString("D4");
            string taggedMessage = string.Format("li{0} {1}{2}", tagNumber, message, Environment.NewLine);
            byte[] command = Encoding.ASCII.GetBytes(taggedMessage);
            sslStream.Write(command, 0, command.Length);
        }

        public string Read()
        {
            return streamReader.ReadLine();
        }
    }
}