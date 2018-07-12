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
    public partial class CalcSalary : Form
    {
        DateTime dateTime;
        int Salary;
        int ParentID;
        int IDEmployee;
        public CalcSalary(Workers obj1)
        {
            InitializeComponent();
            SQLiteConnection DB = new SQLiteConnection(@"Data Source=Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand CalcSalary = new SQLiteCommand(DB);
            CalcSalary.Connection = DB;
            CalcSalary.CommandText = "select FIO, StartSalary, EntryDate, MainInfo.'Group',ParentID,IDEmployee from MainInfo where FIO=@FIO and StartSalary=@StartSalary and EntryDate=@EntryDate";
            CalcSalary.Parameters.AddWithValue("@FIO", obj1.FIO);
            CalcSalary.Parameters.AddWithValue("@StartSalary", obj1.StartSalary);
            CalcSalary.Parameters.AddWithValue("@EntryDate", obj1.EntryDate);
            CalcSalary.Connection.Open();
            SQLiteDataReader dr = CalcSalary.ExecuteReader();
            
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    dateTime = dr.GetDateTime(2);
                    lFIO.Text = dr.GetValue(0).ToString();
                    lStartSalary.Text = dr.GetValue(1).ToString();
                    lStartDateTime.Text = dr.GetValue(2).ToString();
                    lGroup.Text = dr.GetValue(3).ToString();
                    obj1.ParentID= dr.GetInt32(4);
                    obj1.IDEmployee = dr.GetInt32(5);
                }
            }
            CalcSalary.Connection.Close();
        }

        private void CalcSalary_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int datecalc = dtSalary.Value.Year - dateTime.Year;
            int id = 0;
            SQLiteConnection DB = new SQLiteConnection(@"Data Source=Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand getid = new SQLiteCommand(DB);
            getid.CommandText = "select IDEmployee from MainInfo where FIO=@FIO";
            getid.Parameters.AddWithValue("@FIO", lFIO.Text);
            getid.Connection.Open();
            id = Convert.ToInt32(getid.ExecuteScalar());


            if (dtSalary.Value.Month - dateTime.Month < 0) {datecalc--; }
            switch (lGroup.Text)
            {
                case "1)Работник":
                    int exp = Convert.ToInt32( Convert.ToUInt32(lStartSalary.Text) + (dtSalary.Value.Year - Convert.ToDateTime(lStartDateTime.Text).Year) * Convert.ToUInt32(lStartSalary.Text) * 0.03);
                    if (exp > Convert.ToUInt32(lStartSalary.Text) * 1.3) { exp = Convert.ToInt32(Convert.ToUInt32(lStartSalary.Text) * 1.35); }
                    lSalary.Text = exp.ToString();
                    break;
                case "2)Менеджер":
                    int exp1 = Convert.ToInt32(Convert.ToUInt32(lStartSalary.Text) + (dtSalary.Value.Year - Convert.ToDateTime(lStartDateTime.Text).Year) * Convert.ToUInt32(lStartSalary.Text) * 0.05);
                    if (exp1 > Convert.ToUInt32(lStartSalary.Text) * 1.35) { exp = Convert.ToInt32(Convert.ToUInt32(lStartSalary.Text) * 1.35); }
                    lSalary.Text =Convert.ToString(exp1 + Convert.ToInt32(Manager.GetManagerplusInCalc(id, dtSalary.Value) * 0.005));
                    break;
                case "3)Продавец":
                    int exp2 = Convert.ToInt32(Convert.ToUInt32(lStartSalary.Text) + (dtSalary.Value.Year - Convert.ToDateTime(lStartDateTime.Text).Year) * Convert.ToUInt32(lStartSalary.Text) * 0.01);
                    if (exp2 > Convert.ToUInt32(lStartSalary.Text) * 1.3) { exp = Convert.ToInt32(Convert.ToUInt32(lStartSalary.Text) * 1.3); }
                    lSalary.Text = Convert.ToString(exp2 + Convert.ToInt32(Salesman.GetSalesManplusInCalc(id, DateTime.Now) * 0.003));
                    break;
                default: break;
            }
        }
    }
}
