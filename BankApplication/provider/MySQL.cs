using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BankApplication.provider
{
    public class MySQL
    {
        public MySqlConnection connection;
        
        public MySQL()
        {
            try {
                connection = new MySqlConnection("Server=localhost;Database=userTable;user=root;Pwd=semih28");
            }
            catch (MySqlException error){ throw error; };
        }

        public bool hasUser(string userName)
        {
            isConnectionControl();
            MySqlCommand command = new MySqlCommand("SELECT userName FROM moneyTable WHERE userName = '"+userName+"'", connection);
            MySqlDataReader reader = command.ExecuteReader();
            return reader.Read();
        }

        public void addUser(string userName, string password)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO moneyTable(userName, userPassword, userId, createTime) VALUES('"+userName+"', '"+password+"', '"+getUserId()+"', '"+DateTime.Now.ToString("dd/MM/yyyy")+ "')", connection);
            isConnectionControl();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public string getPassword(string userName)
        {
            MySqlCommand command = new MySqlCommand("SELECT userPassword FROM moneyTable WHERE userName = '"+userName+"'", connection);
            isConnectionControl();
            MySqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return "";
            string password = reader["userPassword"].ToString();
            return password;
        }

        public void passwordUpdate(string userName, string newPassword)
        {
            MySqlCommand command = new MySqlCommand("UPDATE moneyTable SET userPassword = '"+newPassword+"' WHERE userName = '"+userName+"'", connection);
            isConnectionControl();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public string getDate(string userName)
        {
            MySqlCommand command = new MySqlCommand("SELECT createTime FROM moneyTable WHERE userName = '" + userName + "'", connection);
            isConnectionControl();
            MySqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return "00/00/1980";
            return reader["createTime"].ToString();
        }

        public double getMoney(string userName)
        {
            MySqlCommand command = new MySqlCommand("SELECT userMoney FROM moneyTable WHERE userName = '" + userName + "'", connection);
            isConnectionControl();
            MySqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return 0.0;
            return Convert.ToDouble(reader["userMoney"]);
        }

        public int getUserId()
        {
            List<int> idList = new List<int>();
            MySqlCommand command = new MySqlCommand("SELECT userId FROM moneyTable", connection);
            isConnectionControl();
            MySqlDataReader reader = command.ExecuteReader();
            if (!reader.Read()) return 1;
            while (reader.Read())
                idList.Add(Convert.ToInt16(reader["userId"]));
            return idList[idList.Count - 1]+1;
        }

        private void isConnectionControl()
        {
            connection.Close();
            if (connection.State.ToString() != "Open") connection.Open();
        }
    }
}
