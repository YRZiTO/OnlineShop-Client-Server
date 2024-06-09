using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace OnlineShopClient
{
    public partial class ShopperForm : Form
    {
        private readonly IShopperData m_shopperData;

        public ShopperForm(IShopperData shopperData)
        {
            InitializeComponent();
            m_shopperData = shopperData;
        }

        private async void ShopperForm_LoadAsync(object sender, EventArgs e)
        {
            if (Connect())
            {
                this.Text = "ShopClient, User: " + m_shopperData.shopperName;
                listBoxOrders.Items.AddRange(await m_shopperData.GetOrdersAsync());
                comboBoxList.DataSource = await m_shopperData.GetProductsAsync();
                comboBoxList.SelectedIndex = -1;
            }
            else
            {
                Application.Exit();
            }
        }

        private bool Connect() => new LoginForm(m_shopperData).ShowDialog(this) == DialogResult.OK;

        private void ShopperForm_FormClosed(object sender, FormClosedEventArgs e) => m_shopperData.Exit();
        private void btnTimerSwitch_Click(object sender, EventArgs e) => timerUpdate.Enabled = true;
        private void timerUpdate_Tick(object sender, EventArgs e) => LoadOrders();

        private void comboBoxList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxList.SelectedItem == null || (string)comboBoxList.SelectedItem == "")
            {
                btnPurchase.Enabled = false;
            }
            else
            {
                btnPurchase.Enabled = true;
            }
        }

        private async void btnPurchase_ClickAsync(object sender, EventArgs e)
        {
            string itemsList = (string)comboBoxList.SelectedItem;
            string resProduct = await m_shopperData.Purchase(itemsList);

            if (resProduct == "DONE")
            {
                LoadOrders();
            }
            else if (resProduct == "NOT_AVAILABLE")
            {
                MessageBox.Show("The product is not available!", "Unavailable Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resProduct == "NOT_VALID")
            {
                MessageBox.Show("The specified product is not valid!", "Invalid Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            timerUpdate.Stop();
            string[] ordersArray = await m_shopperData.GetOrdersAsync();
            listBoxOrders.Items.Clear();
            listBoxOrders.Items.AddRange(ordersArray);
        }

        private async void LoadOrders()
        {
            string[] ordersArray = await m_shopperData.GetOrdersAsync();
            listBoxOrders.Items.Clear();
            listBoxOrders.Items.AddRange(ordersArray);
        }
    }
}
