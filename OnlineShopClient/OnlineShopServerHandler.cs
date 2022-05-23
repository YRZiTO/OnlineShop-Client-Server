using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineShopClient
{
    public class OnlineShopServerHandler : IShopperData
    {
        public string HostName { get; set; } = "localhost";
        public int HostPort { get; set; } = 44044;
        public string shopperName { get; set; }
        public string shopperAccountNo { get; set; }

        private TcpClient m_tcpClient = null;
        private StreamReader m_reader;
        private StreamWriter m_writer;
        public bool IsClosed => null == m_tcpClient;
        public async Task<bool> StartAsync()
        {
            try
            {
                m_tcpClient = new TcpClient();
                m_tcpClient.Connect(HostName, HostPort);
                NetworkStream stream = m_tcpClient.GetStream();
                m_reader = new StreamReader(stream);
                m_writer = new StreamWriter(stream);
                await m_writer.WriteLineAsync($"CONNECT:{shopperAccountNo}");
                await m_writer.FlushAsync();
                string strReader = await m_reader.ReadLineAsync();

                if (strReader.StartsWith("CONNECTED"))
                {
                    shopperName = strReader.Split(':')[1];
                    return true;
                }
                else if (strReader.Contains("CONNECT_ERROR"))
                {
                    MessageBox.Show($"Client number {shopperAccountNo} is invalid!", "Client Number Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }
            catch (SocketException se)
            {
                m_tcpClient = null;
                throw new InvalidOperationException("Server Unavailable", se);
            }
        }

        private void Close()
        {
            m_tcpClient?.Close();
            m_tcpClient = null;
        }

        public async void Exit()
        {
            if (!IsClosed)
            {
                try
                {
                    m_writer.WriteLine("DISCONNECT" + ":" + shopperName);
                    await m_writer.FlushAsync();
                }
                catch (IOException ioe)
                {
                    throw new InvalidOperationException("Server Unavailable", ioe);
                }
                finally
                {
                    Close();
                }
            }
        }

        public async Task<string[]> GetOrdersAsync()
        {
            if (IsClosed)
            {
                throw new InvalidOperationException("Client Closed");
            }
            try
            {
                await m_writer.WriteLineAsync("GET_ORDERS");
                await m_writer.FlushAsync();
                string line = await m_reader.ReadLineAsync();
                return line.Split('|');
            }
            catch (IOException ioe)
            {
                Close(); // Close on error
                throw new InvalidOperationException("Server Unavailable", ioe);
            }
        }

        public async Task<string> Purchase(string item)
        {
            if (IsClosed)
            {
                throw new InvalidOperationException("Client Closed");
            }
            try
            {
                await m_writer.WriteLineAsync("PURCHASE:" + item);
                await m_writer.FlushAsync();
                return await m_reader.ReadLineAsync();

            }
            catch (IOException ioe)
            {
                Close(); // Close on error
                throw new InvalidOperationException("Server Unavailable", ioe);
            }
        }

        public async Task<IList<string>> GetProductsAsync()
        {
            if (IsClosed)
                throw new InvalidOperationException("Client Closed");
            try
            {
                List<string> getProducts = new List<string>();
                await m_writer.WriteLineAsync("GET_PRODUCTS");
                await m_writer.FlushAsync();
                string line = await m_reader.ReadLineAsync();
                if ((line?.Length ?? 0) > 0) // A null value may indicate that the connection to the server has been closed
                {
                    foreach (string prod in line.Split('|'))
                    {
                        string[] productInfo = prod.Split(',');
                        if (productInfo.Length == 2)
                        {
                            getProducts.Add(productInfo[0]);
                        }
                    }
                }
                return getProducts;
            }
            catch (IOException ioe)
            {
                Close(); // Close on error
                throw new InvalidOperationException("Server Unavailable", ioe);
            }
        }
    }
}
