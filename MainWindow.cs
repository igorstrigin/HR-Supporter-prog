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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btAddEmploee_Click(object sender, EventArgs e)
        {
            Form newwindow = new AddEmployee();
            newwindow.Show();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SQLiteConnection DB = new SQLiteConnection(@"Data Source=Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand OpenTable = new SQLiteCommand(DB);
            OpenTable.Connection = DB;
            OpenTable.CommandText = "select FIO as ФИО, StartSalary as 'Начальная зарплата', EntryDate as 'Дата вступления на работу', MainInfo.'Group' as 'Рабочая группа', Salary as 'Зарплата' from MainInfo order by MainInfo.'Group' asc";
            OpenTable.Connection.Open();
            OpenTable.ExecuteNonQuery();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(OpenTable);
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            OpenTable.Connection.Close();
        }

        private void btCalcSalary_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Workers workers = new Workers(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), 0, Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()), Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()), "",0);
            CalcSalary calcSalary = new CalcSalary(workers);
            calcSalary.Owner = this;
            calcSalary.ShowDialog();
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            List<int> idemployee = new List<int>();
            List<int> parentid = new List<int>();
            List<int> salary = new List<int>();
            List<string> group = new List<string>();
            List<DateTime> dates = new List<DateTime>();
            List<int> startsalary = new List<int>();
            List<string> FIO = new List<string>();

            SQLiteConnection DB = new SQLiteConnection(@"Data Source=Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            DataSet ds = new DataSet();
            
            SQLiteCommand UntilReset = new SQLiteCommand(DB);
            UntilReset.Connection = DB;
            UntilReset.CommandText = "select IDEmployee, ParentID, Salary, MainInfo.'Group', EntryDate, StartSalary, FIO from MainInfo order by MainInfo.'Group' asc";
            try
            {
                UntilReset.Connection.Open();
                SQLiteDataReader dataReader = UntilReset.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        idemployee.Add(dataReader.GetInt32(0));
                        parentid.Add(dataReader.GetInt32(1));
                        salary.Add(dataReader.GetInt32(2));
                        group.Add(dataReader.GetString(3));
                        dates.Add(dataReader.GetDateTime(4));
                        startsalary.Add(dataReader.GetInt32(5));
                        FIO.Add(dataReader.GetString(6));
                    }
                }
                dataReader.Close();
            }
            catch (Exception ex) { ex.ToString(); }
            finally
            {
                UntilReset.Connection.Close();
            }
            for (int i = 0; i < group.Count; i++)
            {
                MessageBox.Show(FIO[i]);
                MessageBox.Show(idemployee[i].ToString());

                switch (group[i])
                {
                    case "1)Работник":
                        int exp = Convert.ToInt32(startsalary[i] + (DateTime.Now.Year - dates[i].Year) * startsalary[i] * 0.03);
                        if (exp > startsalary[i] * 1.3) { exp = Convert.ToInt32(startsalary[i] * 1.3); }
                        salary[i] = exp;
                        break;
                    case "2)Менеджер":
                        int exp1 = Convert.ToInt32(startsalary[i] + (DateTime.Now.Year - dates[i].Year) * startsalary[i] * 0.05);
                        if (exp1 > startsalary[i] * 1.3) { exp = Convert.ToInt32(startsalary[i] * 1.3); }
                        salary[i] = exp1 + Convert.ToInt32(Manager.GetManagerplusInCalc(idemployee[i], DateTime.Now) * 0.005);
                        break;
                    case "3)Продавец":
                        int exp2 = Convert.ToInt32(startsalary[i] + (DateTime.Now.Year - dates[i].Year) * startsalary[i] * 0.01);
                        if (exp2 > startsalary[i] * 1.3) { exp = Convert.ToInt32(startsalary[i] * 1.3); }
                        salary[i] = exp2 +  Convert.ToInt32(Salesman.GetSalesManplusInCalc(idemployee[i], DateTime.Now) * 0.003);
                        break;
                    default: salary[i] = salary[i]; break;
                }
            }

            SQLiteCommand Reset = new SQLiteCommand(DB);
            Reset.Connection = DB;
            Reset.CommandText = "UPDATE MainInfo SET Salary = @Salary where IDEmployee = @IDEmployee";
            try
            {
                Reset.Connection.Open();
                for (int i = 0; i < idemployee.Count; i++)
                {
                    Reset.Parameters.AddWithValue("@Salary", salary[i]);
                    Reset.Parameters.AddWithValue("@IDEmployee", idemployee[i]);
                    Reset.ExecuteNonQuery();   
                }
                }
            catch (Exception ex) { ex.ToString(); }
            finally
            {
                Reset.Connection.Close();
            }

            SQLiteCommand OpenTable = new SQLiteCommand(DB);
            OpenTable.Connection = DB;
            OpenTable.CommandText = "select FIO as ФИО, StartSalary as 'Начальная зарплата', EntryDate as 'Дата вступления на работу', MainInfo.'Group' as 'Рабочая группа', Salary as 'Зарплата' from MainInfo order by MainInfo.'Group' asc";
            OpenTable.Connection.Open();
            OpenTable.ExecuteNonQuery();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(OpenTable);
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            OpenTable.Connection.Close();
        }
    }
}
