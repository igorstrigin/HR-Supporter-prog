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
                    IDEmployee = dr.GetInt32(5);
                }
            }
            CalcSalary.Connection.Close();
            switch (lGroup.Text) {
                case "Работник":
                    lSalary.Text = Convert.ToString(Convert.ToInt32(lStartSalary.Text) + (DateTime.Now.Year - dateTime.Year) * Convert.ToInt32(lStartSalary.Text) * 0.03);
                    break;
                case "Менеджер": break;
                case "Продавец": break;
                default: break;
            }
        }

        private void CalcSalary_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int datecalc = dtSalary.Value.Year - dateTime.Year;
            if (dtSalary.Value.Month - dateTime.Month < 0) {datecalc--; }
            switch (lGroup.Text)
            {
                case "Работник":
                    lSalary.Text = Convert.ToString(Convert.ToInt32(lStartSalary.Text) + datecalc * Convert.ToInt32(lStartSalary.Text) * 0.03);
                    if (Convert.ToInt32(lSalary.Text) > Convert.ToInt32(lStartSalary.Text) * 1.3) { lSalary.Text = Convert.ToString(Convert.ToInt32(lStartSalary.Text) * 1.3); }
                    break;
                case "Менеджер":
                    SQLiteConnection DB = new SQLiteConnection(@"Data Source=Salary.db;Pooling=true;FailIfMissing=false;Version=3");
                    SQLiteCommand ManagerPlus = new SQLiteCommand(DB);
                    ManagerPlus.Connection = DB;
                    ManagerPlus.CommandText = "select Salary as Количество from MainInfo where ParentId = @ParentId";
                    ManagerPlus.Parameters.AddWithValue("@ParentID", ParentID);
                    try
                    {
                        ManagerPlus.Connection.Open();
                        SQLiteDataReader dr1 = ManagerPlus.ExecuteReader();
                        if (dr1.HasRows)
                        {
                            while (dr1.Read())
                            {
                                Salary = Salary + Convert.ToInt32(dr1.GetDouble(0) * 0.005);
                                MessageBox.Show(Salary.ToString());
                            }
                        }
                        dr1.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString());}
                    finally
                    {

                        ManagerPlus.Connection.Close();
                        lSalary.Text = Convert.ToString(Convert.ToInt32(lStartSalary.Text) + datecalc * Convert.ToInt32(lStartSalary.Text) * 0.05 + Salary);
                        if (Convert.ToInt32(lSalary.Text) > Convert.ToInt32(lStartSalary.Text) * 1.4)
                        { lSalary.Text = Convert.ToString(Convert.ToInt32(lStartSalary.Text) * 1.4); }
                        Salary = 0;
                    }
                        break;
                case "Продавец":
                    SQLiteConnection DB1 = new SQLiteConnection(@"Data Source=Salary.db;Pooling=true;FailIfMissing=false;Version=3");
                    SQLiteCommand SalerPlus = new SQLiteCommand(DB1);
                    SalerPlus.Connection = DB1;
                    SalerPlus.CommandText = "with NewTable as (select MainInfo.IDEmployee, MainInfo.ParentID, MainInfo.Salary from MainInfo where IDEmployee = @IDEmployee union all select N.IDEmployee, N.ParentID, N.Salary from NewTable inner join MainInfo N on NewTable.IDEmployee = N.ParentID) select Salary from NewTable";
                    SalerPlus.Parameters.AddWithValue("@IDEmployee", IDEmployee);
                    try
                    {
                        SalerPlus.Connection.Open();
                        SQLiteDataReader dr2 = SalerPlus.ExecuteReader();
                        if (dr2.HasRows)
                        {
                            while (dr2.Read())
                            {
                                Salary = Salary + Convert.ToInt32(dr2.GetDouble(0) * 0.003);
                                MessageBox.Show(Salary.ToString());
                            }
                        }
                        dr2.Close();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                    finally
                    {
                        SalerPlus.Connection.Close();
                        lSalary.Text = Convert.ToString(Convert.ToInt32(lStartSalary.Text) + datecalc * Convert.ToInt32(lStartSalary.Text) * 0.01 + Salary);
                        if (Convert.ToInt32(lSalary.Text) > Convert.ToInt32(lStartSalary.Text) * 1.4)
                        { lSalary.Text = Convert.ToString(Convert.ToInt32(lStartSalary.Text) * 1.4); }
                        Salary = 0;
                    }
                    break;
                default: break;
            }
        }
    }
}
