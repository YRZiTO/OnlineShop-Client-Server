using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace OnlineShopClient
{
    public interface IShopperData
    {
        string HostName { get; set; }
        int HostPort { get; set; }
        string shopperName { get; set; }
        string shopperAccountNo { get; set; }

        bool IsClosed { get; }

        void Exit();
        Task<IList<string>> GetProductsAsync();
        Task<bool> StartAsync();
        Task<string> Purchase(string item);
        Task<string[]> GetOrdersAsync();
    }
}
