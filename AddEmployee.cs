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
    public partial class AddEmployee : Form
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        public int GetEmployeeId()
        {
            SQLiteConnection DB = new SQLiteConnection("Data Sourse = Salary.db; Version = 3");
            SQLiteCommand AddNewEmployee = new SQLiteCommand(DB);
            AddNewEmployee.Connection = DB;
            AddNewEmployee.CommandText = "select top (1) IDEmployee from MainInfo order by IDEmployee desc";
            AddNewEmployee.Connection.Open();
            var result = Convert.ToInt32(AddNewEmployee.ExecuteScalar());
            if (result > 0)
            {
                result++;
            }
            else { result = 1; }
            AddNewEmployee.Connection.Close();
            return result;
        }



        private void btAdd_Click(object sender, EventArgs e)
        {
            string FIO = tbSurname.Text + " " + tbName.Text + " " + tbSecondName.Text;
            int StartSalary = Convert.ToInt32(tbStartSalary.Text);
            int idEmployee = GetEmployeeId();
            DateTime Startdt = dtStart.Value;
            int salary;
            string Group;
            SQLiteConnection DB = new SQLiteConnection("Data Sourse = Salary.db; Version = 3");
            SQLiteCommand AddInBase = new SQLiteCommand(DB);
            AddInBase.Connection = DB;
            AddInBase.CommandText = "insert into MainInfo(IDEmployee,FIO,EntryDate,StartSalary,Group) values (@IDEmployee,@FIO,@EntryDate,@StartSalary,@Group)";
            AddInBase.Parameters.AddWithValue("@IDEmployee", idEmployee);
            AddInBase.Parameters.AddWithValue("@FIO", FIO);
            AddInBase.Parameters.AddWithValue("@EntryDate", Startdt);
            AddInBase.Parameters.AddWithValue("@StartSalary", StartSalary);


            switch (cbGroup.Text)
            {
                case "Работник":
                    Group = "Работник";
                    salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - Startdt.Year) * StartSalary * 0.03);
                    Workers employee = new Employee(salary, FIO, idEmployee, Startdt, StartSalary, Group);
                    AddInBase.Parameters.AddWithValue("@Group", Group);
                    break;
                case "Менеджер":
                    Group = "Менеджер";
                    salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - Startdt.Year) * StartSalary * 0.05);
                    Workers Manager = new Manager(salary, FIO, idEmployee, Startdt, StartSalary, Group);
                    AddInBase.Parameters.AddWithValue("@Group", Group);
                    break;
                case "Продавец":
                    Group = "Продавец";
                    salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - Startdt.Year) * StartSalary * 0.03);
                    Workers Salesman = new Salesman(salary, FIO, idEmployee, Startdt, StartSalary, Group);
                    AddInBase.Parameters.AddWithValue("@Group", Group);
                    break;
                default: MessageBox.Show("значение 'Группа' не должно быть пустым");
                    break;
            }
            AddInBase.Connection.Open();
            AddInBase.ExecuteNonQuery();
            AddInBase.Connection.Close();
        }

        private void tbStartSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
