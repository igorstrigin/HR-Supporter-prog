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
    public partial class AddEmployer : Form
    {
        public AddEmployer()
        {
            InitializeComponent();
        }

        int ParentID;

        private void AddEmployer_Load(object sender, EventArgs e)
        {
            SQLiteConnection DB = new SQLiteConnection(@"Data Source=Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand CheckFIO = new SQLiteCommand(DB);
            CheckFIO.Connection = DB;
            CheckFIO.CommandText = "select MainInfo.FIO from MainInfo where MainInfo.'Group' <> 'Работник'";
            try
            {
                CheckFIO.Connection.Open();
                SQLiteDataReader DR = CheckFIO.ExecuteReader();
                int i=1;
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        string getFIO = DR.GetValue(0).ToString();
                        cbSelectEmployer.Items.Add(getFIO);
                        i++;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { CheckFIO.Connection.Close(); }
        }

        private void btAddEmployer_Click(object sender, EventArgs e)
        {
            SQLiteConnection DB = new SQLiteConnection(@"Data Source=Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand AddEmployer = new SQLiteCommand(DB);
            AddEmployer.Connection = DB;
            AddEmployer.CommandText = "select IDEmployee from MainInfo where FIO = @FIO";
            AddEmployer.Parameters.AddWithValue("@FIO", cbSelectEmployer.Text);
            AddEmployer.Connection.Open();
            ParentID = Convert.ToInt32(AddEmployer.ExecuteScalar());
            AddEmployer.Connection.Close();
            Workers.tempID = ParentID;
            //MessageBox.Show(Convert.ToString(Workers.tempID));
            this.Close();
        }
    }
}
