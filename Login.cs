using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SalaryGUI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btEnter_Click(object sender, EventArgs e)
        {
            string Group = "";
            SQLiteConnection DB = new SQLiteConnection(@"Data Source=Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand Enter = new SQLiteCommand(DB);
            Enter.Connection = DB;
            Enter.CommandText = "select MainInfo.'Group', StartSalary, EntryDate from MainInfo  where FIO = @FIO and Password = @Password";
            Enter.Parameters.AddWithValue("@FIO", tbLogin);
            Enter.Parameters.AddWithValue("@Password", tbPassword);
                Enter.Connection.Open();
                SQLiteDataReader dr = Enter.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Group = dr.GetString(0);
                    }
                    
                }
                else { MessageBox.Show("Проверьте имя пользователя и пароль"); }
                Workers work = new Workers(tbLogin.Text, 0, dr.GetDateTime(2), dr.GetInt32(1), tbPassword.Text, 0);
                dr.Close();
                Enter.Connection.Close(); 
            switch (Group)
            {
                case "Работник":
                    Form form = new CalcSalary(work);
                    break;
                case "Менеджер":
                    Form form1 = new CalcSalary(work);
                    break;
                case "Продавец":
                    Form form3 = new CalcSalary(work);
                    break;
                case "": break;
                default: Form form4 = new MainWindow();
                    break;
            }
        }
    }
}
