using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using BankApplication.provider;

namespace BankApplication
{
    public partial class BankMainForm : Form
    {
        MySQL mysql;
        public BankMainForm()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string userName = userNameText.Text;
            string password = passwordText.Text;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("You must enter all the information completely!");
                return;
            }
            if (password.ToCharArray().Where(i => i!=' ').Count<char>() < 8 || password.ToCharArray().Where(i => i != ' ').Count<char>() > 25)
            {
                MessageBox.Show("Your password must be between 8-25 characters!");
                return;
            }
            switch (button.Text)
            {
                case "LOGIN":
                    if(!mysql.hasUser(userName))
                    {
                        MessageBox.Show("User not found, you can register!");
                        return;
                    }
                    if(mysql.getPassword(userName) != password)
                    {
                        MessageBox.Show("Password is wrong!");
                        return;
                    }
                    BankAccountForm accountForm = new BankAccountForm();
                    accountForm.userName = userName;
                    accountForm.ShowDialog();
                    break;
                case "REGISTER":
                    if(mysql.hasUser(userName))
                    {
                        MessageBox.Show("Such a user is registered, you can log in!");
                        return;
                    }
                    mysql.addUser(userName, password);
                    MessageBox.Show("You are registered!");
                    break;
            }
        }

        private void BankMain_Load(object sender, EventArgs e)
        {
            mysql = new MySQL();
        }
    }
}