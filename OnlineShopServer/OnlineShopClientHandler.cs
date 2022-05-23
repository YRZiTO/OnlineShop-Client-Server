using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShopServer
{
    public class OnlineShopClientHandler : Shopper
    {
        private readonly TcpClient m_tcpClient;
        private readonly CancellationToken m_cancellationToken;
        private Shopper shopper = new Shopper();
        public static List<string> orders = new List<string>();
        public static Random rnd = new Random();
        public static List<Product> products = new List<Product>
        {
            new Product() { productName = "Apple", quantityNum = rnd.Next(1, 3) },
            new Product() { productName = "Orange", quantityNum = rnd.Next(1, 3) },
            new Product() { productName = "Pineapple", quantityNum = rnd.Next(1, 3)},
            new Product() { productName = "Printer", quantityNum = rnd.Next(1, 3) },
            new Product() { productName = "Keyboard", quantityNum = rnd.Next(1, 3) },
            new Product() { productName = "Shampoo", quantityNum = rnd.Next(1, 3) }
        };


        private static ConcurrentDictionary<int, OnlineShopClientHandler> CurrentClients { get; } = new ConcurrentDictionary<int, OnlineShopClientHandler>();

        public OnlineShopClientHandler(string AccountNo, TcpClient tcpClient, CancellationToken cancellationToken)
        {
            shopper.shopperAccountNo = AccountNo;
            m_tcpClient = tcpClient;
            m_cancellationToken = cancellationToken;
            CurrentClients.TryAdd(Convert.ToInt32(AccountNo), this); // Add self to list of current client sessions
        }
        public void Run()
        {
            using (m_tcpClient)
            {
                try
                {
                    NetworkStream stream = m_tcpClient.GetStream();
                    StreamReader reader = new StreamReader(stream);
                    StreamWriter writer = new StreamWriter(stream);
                    m_cancellationToken.Register(stream.Close); // Close the stream when a cancellation is requested - May cause IOException

                    while (!m_cancellationToken.IsCancellationRequested)
                    {
                        string line = reader.ReadLine();
                        Console.WriteLine(line);

                        if (null == line || line.StartsWith("DISCONNECT")) { break; }
                        else
                        {
                            if (line.StartsWith("CONNECT"))
                            {
                                if (line.EndsWith("1111"))
                                {
                                    shopper.shopperAccountNo = line.Split(':')[1];
                                    shopper.shopperName = "John";
                                    writer.WriteLine($"CONNECTED: {shopper.shopperName}");
                                    Console.WriteLine($"CONNECTED: {shopper.shopperName}");
                                    writer.Flush();
                                }
                                else if (line.EndsWith("2222"))
                                {
                                    shopper.shopperAccountNo = line.Split(':')[1];
                                    shopper.shopperName = "Jane";
                                    writer.WriteLine($"CONNECTED: {shopper.shopperName}");
                                    Console.WriteLine($"CONNECTED: {shopper.shopperName}");
                                    writer.Flush();

                                }
                                else if (line.EndsWith("3333"))
                                {
                                    shopper.shopperAccountNo = line.Split(':')[1];
                                    shopper.shopperName = "Bob";
                                    writer.WriteLine($"CONNECTED: {shopper.shopperName}");
                                    Console.WriteLine($"CONNECTED: {shopper.shopperName}");
                                    writer.Flush();

                                }
                                else
                                {
                                    Console.WriteLine("CONNECT_ERROR");
                                    writer.WriteLine("CONNECT_ERROR");
                                    writer.Flush();
                                }
                            }

                            else if (line.StartsWith("GET_PRODUCTS"))
                            {
                                List<Product> items = new List<Product>();
                                string selectedProduct = "";
                                string listedProducts = "";
                                foreach (var item in products)
                                {
                                    selectedProduct += $"{item.productName},{item.quantityNum}|";
                                    listedProducts = selectedProduct.Remove(selectedProduct.Length - 1); // To remove last pipe from the list of products
                                }
                                writer.WriteLine(listedProducts);
                                Console.WriteLine("PRODUCTS: " + listedProducts);
                                writer.Flush(); // Push out any text that is still in the buffer
                            }
                            else if ("GET_ORDERS" == line)
                            {
                                string userOrder = String.Join("|", orders.ToArray());
                                Console.WriteLine("ORDERS: " + userOrder);
                                writer.WriteLine(userOrder);
                                writer.Flush(); // Push out any text that is still in the buffer
                            }
                            else if (line.StartsWith("PURCHASE"))
                            {
                                bool validPurchase = false;
                                string product = line.Split(':')[1];
                                foreach (Product p in products)
                                {
                                    if (p.productName == product)
                                    {
                                        validPurchase = true;
                                        if (p.quantityNum > 0)
                                        {
                                            orders.Add(product + ",1," + shopper.shopperName);
                                            p.quantityNum = p.quantityNum - 1;
                                            writer.WriteLine("DONE");
                                            Console.WriteLine("DONE");
                                        }
                                        else
                                        {
                                            writer.WriteLine("NOT_AVAILABLE");
                                            Console.WriteLine("NOT_AVAILABLE");
                                        }
                                    }
                                }
                                if (!validPurchase)
                                {
                                    Console.WriteLine("NOT_VALID");
                                    writer.WriteLine("NOT_VALID");
                                }
                                writer.Flush(); // Push out any text that is still in the buffer
                            }
                            else
                            {
                                writer.WriteLine($"CONNECT_ERROR: The client's connection attempt is unsuccessful. The {shopper.shopperAccountNo} is not valid.");
                                writer.Flush();
                            }
                        }
                    }
                }
                catch (IOException) // Exception takes us out of the loop, so client handler thread will end
                {
                    Console.WriteLine("***Network Error***");
                }
                catch (ObjectDisposedException)
                {
                    Console.WriteLine("***Network Error***"); // May occur if read  or write is attempted after stream is closed
                }
                catch (OutOfMemoryException)
                {
                    // Catch buffer overflow exception
                    // Connection will close upon leaving the using block
                }
                finally
                {
                    CurrentClients.TryRemove(Convert.ToInt32(shopper.shopperAccountNo), out _); // Client is no longer current, so remove
                }
            }
        }

    }
}
