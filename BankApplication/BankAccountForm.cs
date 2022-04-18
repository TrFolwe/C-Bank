using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankApplication.provider;

namespace BankApplication
{
    public partial class BankAccountForm : Form
    {

        public string userName;
        public BankAccountForm()
        {
            InitializeComponent();
        }

        private void BankAccountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        MySQL mysql;

        private void BankAccountForm_Load(object sender, EventArgs e)
        {
            mysql = new MySQL();
            Text = userName + " User Information";
            titleText.Text = userName + " Information";
            accountInfoText.Text = "User: " + userName + "\nMoney amount: " + mysql.getMoney(userName) + "\nAccount creation date: " + mysql.getDate(userName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "Remittance Transactions")
            {
                MessageBox.Show("TODO");
                return;
            }
            PasswordChangeForm passwordForm = new PasswordChangeForm();
            passwordForm.userName = userName;
            passwordForm.Show();
            Hide();
        }
    }
}