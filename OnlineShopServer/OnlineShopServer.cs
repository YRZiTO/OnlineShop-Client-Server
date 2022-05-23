using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShopServer
{
    public class OnlineShopServer
    {
        private readonly CancellationToken m_cancellationToken;
        public IPAddress ServerIp { get; set; } = IPAddress.Any;
        public int ServerPort { get; set; } = 44044;
        public string shopperAccountNo { get; set; }

        public OnlineShopServer(CancellationToken cancellationToken) => m_cancellationToken = cancellationToken;

        public void Start()
        {
            try
            {
                TcpListener listener = new TcpListener(ServerIp, ServerPort);
                listener.Start();
                m_cancellationToken.Register(listener.Stop);
                while (!m_cancellationToken.IsCancellationRequested)
                {
                    TcpClient tcpClient = listener.AcceptTcpClient();
                    new Thread(new OnlineShopClientHandler(shopperAccountNo, tcpClient, m_cancellationToken).Run).Start();
                    NetworkStream stream = tcpClient.GetStream();
                    StreamReader reader = new StreamReader(stream);
                    StreamWriter writer = new StreamWriter(stream);
                }
            }
            catch (SocketException)
            {
                // SocketException may occur when listener is started or stopped
            }
        }
    }
}
