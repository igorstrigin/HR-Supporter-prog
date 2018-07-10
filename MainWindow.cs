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
            OpenTable.CommandText = "select * from MainInfo order by FIO desc";
            OpenTable.Connection.Open();
            OpenTable.ExecuteNonQuery();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(OpenTable);
            dataAdapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            OpenTable.Connection.Close();
            
        }
    }
}
