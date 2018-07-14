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
            string Log = "";
            string Password = "";
            int startsalary = 0;
            DateTime dateTime = DateTime.Now;
            SQLiteConnection DB = new SQLiteConnection(@"Data Source=Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand Enter = new SQLiteCommand(DB);
            Enter.Connection = DB;
            Enter.CommandText = "select MainInfo.'Group', StartSalary, EntryDate, FIO,Password from MainInfo  where FIO = @FIO and Password = @Password";
            Enter.Parameters.AddWithValue("@FIO", tbLogin.Text);
            Enter.Parameters.AddWithValue("@Password", tbPassword.Text);
                Enter.Connection.Open();
                SQLiteDataReader dr = Enter.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dateTime = dr.GetDateTime(2);
                        startsalary = dr.GetInt32(1);
                        Group = dr.GetString(0);
                        Log = dr.GetString(3);
                        Password = dr.GetString(4);
                    }
                    
                }
                else { MessageBox.Show("Проверьте имя пользователя и пароль"); }
                Workers work = new Workers(Log, 0, dateTime, startsalary, Password, 0);
                dr.Close();
                Enter.Connection.Close(); 
            switch (Group)
            {
                case "1)Работник":
                    this.Visible = false;
                    Form form = new CalcSalary(work);
                    form.Show();
                    break;
                case "2)Менеджер":
                    Form form1 = new CalcSalary(work);
                    this.Visible = false;
                    form1.Show();
                    break;
                case "3)Продавец":
                    Form form3 = new CalcSalary(work);
                    this.Visible = false;
                    form3.Show();
                    break;
                case "": break;
                default:
                    this.Visible = false;
                    Form form4 = new MainWindow();
                    form4.Show();
                    break;
            }
        }
    }
}
