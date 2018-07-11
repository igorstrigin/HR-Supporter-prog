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
            OpenTable.CommandText = "select FIO as ФИО, StartSalary as 'Начальная зарплата', EntryDate as 'Дата вступления на работу', MainInfo.'Group' as 'Рабочая группа' from MainInfo order by FIO desc";
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
            /* DatSender.Posechenie = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
             DatSender.SurnamePac = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
             DatSender.NamePac = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
             DatSender.PathronumicPac = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
             DatSender.DiagnosisPac = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
             DatSender.WorkingTimePac = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
             DatSender.DoctorPac = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
             InfoRow i = new InfoRow();
             i.ShowDialog(); */
        }
    }
}
