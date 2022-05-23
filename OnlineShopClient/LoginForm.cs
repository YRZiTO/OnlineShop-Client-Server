using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineShopClient
{
    public partial class LoginForm : Form
    {
        private readonly IShopperData m_session;
        public LoginForm(IShopperData session)
        {
            InitializeComponent();
            m_session = session;
        }
        private void textBoxHostName_TextChanged(object sender, EventArgs e) => btnFunc();
        private void textBoxAccountNo_TextChanged(object sender, EventArgs e) => btnFunc();

        void btnFunc()
        {
            if (!string.IsNullOrEmpty(textBoxHostName.Text) && !string.IsNullOrEmpty(textBoxAccountNo.Text))
            {
                btnConnect.Enabled = true;
            }
            if (string.IsNullOrEmpty(textBoxHostName.Text) || string.IsNullOrEmpty(textBoxAccountNo.Text))
            {
                btnConnect.Enabled = false;
            }
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                m_session.shopperAccountNo = textBoxAccountNo.Text;

                btnConnect.Enabled = textBoxHostName.Enabled = textBoxAccountNo.Enabled = false;
                if (!m_session.IsClosed && textBoxHostName.Text != m_session.HostName)
                {
                    m_session.Exit();
                }
                if (m_session.IsClosed)
                {
                    m_session.HostName = textBoxHostName.Text;
                    m_session.shopperAccountNo = textBoxAccountNo.Text;
                    bool loginAttempt = await m_session.StartAsync();
                    if (loginAttempt)
                    {
                        btnConnect.Enabled = textBoxHostName.Enabled = textBoxAccountNo.Enabled = true;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        btnConnect.Enabled = textBoxHostName.Enabled = textBoxAccountNo.Enabled = true;
                        btnConnect.Enabled = true;
                        m_session.Exit();
                    }
                }
            }
            catch (InvalidOperationException)
            {
                if (!m_session.IsClosed)
                    MessageBox.Show("Already Connected", "Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Server Unavailable", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBoxHostName.Text = m_session.HostName;
        }
    }
}
