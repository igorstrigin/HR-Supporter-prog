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
            SQLiteConnection DB = new SQLiteConnection(@"Data Source=Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand AddNewEmployee = new SQLiteCommand(DB);
            AddNewEmployee.Connection = DB;
            AddNewEmployee.CommandText = "select IDEmployee from MainInfo order by IDEmployee desc";
            AddNewEmployee.Connection.Open();
            var result = Convert.ToInt32(AddNewEmployee.ExecuteScalar());
            if (result > 0)
            {
                result++;
            }
            else { result = 1; }
            AddNewEmployee.Connection.Close();
            MessageBox.Show("ID =" + result);
            return result;
        }



        private void btAdd_Click(object sender, EventArgs e)
        {
            string FIO = tbSurname.Text + " " + tbName.Text + " " + tbSecondName.Text;
            int StartSalary = Convert.ToInt32(tbStartSalary.Text);
            int idEmployee = GetEmployeeId();
            DateTime Startdt = dtStart.Value;
            Startdt = Startdt.Date;
            int salary;
            string Group;
            string Password = tbPassword.Text;
            int ParentID = 0;
            SQLiteConnection DB = new SQLiteConnection(@"Data Source=Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand AddInBase = new SQLiteCommand(DB);
            AddInBase.Connection = DB;
            AddInBase.CommandText = "insert into MainInfo(IDEmployee,FIO,EntryDate,StartSalary,'Group',Password,ParentID) values (@IDEmployee,@FIO,@EntryDate,@StartSalary,@Group,@Password,@ParentID)";
            //AddInBase.Parameters.AddWithValue("@IDEmployee", idEmployee);
            //AddInBase.Parameters.AddWithValue("@FIO", FIO);
            //AddInBase.Parameters.AddWithValue("@EntryDate", Startdt);
            //AddInBase.Parameters.AddWithValue("@StartSalary", StartSalary);
            //AddInBase.Parameters.AddWithValue("@Password", Password);
            //AddInBase.Parameters.AddWithValue("@ParentID", ParentID);

            switch (cbGroup.Text)
            {
                case "Работник":
                    Group = "Работник";
                    ParentID = Workers.tempID;
                    salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - Startdt.Year) * StartSalary * 0.03);
                    Employee employee = new Employee(salary, FIO, idEmployee, Startdt, StartSalary, Group,Password,ParentID);
                    AddInBase.Parameters.AddWithValue("@IDEmployee", employee.IDEmployee);
                    AddInBase.Parameters.AddWithValue("@FIO", employee.FIO);
                    AddInBase.Parameters.AddWithValue("@EntryDate", employee.EntryDate);
                    AddInBase.Parameters.AddWithValue("@StartSalary", employee.StartSalary);
                    AddInBase.Parameters.AddWithValue("@Password", employee.Password);
                    AddInBase.Parameters.AddWithValue("@ParentID", employee.ParentID);
                    AddInBase.Parameters.AddWithValue("@Group", Employee.Group);
                    break;
                case "Менеджер":
                    Group = "Менеджер";
                    salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - Startdt.Year) * StartSalary * 0.05);
                    ParentID = Workers.tempID;
                    Manager Manager = new Manager(salary, FIO, idEmployee, Startdt, StartSalary, Group, Password, ParentID);
                    AddInBase.Parameters.AddWithValue("@IDEmployee",    Manager.IDEmployee);
                    AddInBase.Parameters.AddWithValue("@FIO",           Manager.FIO);
                    AddInBase.Parameters.AddWithValue("@EntryDate",     Manager.EntryDate);
                    AddInBase.Parameters.AddWithValue("@StartSalary",   Manager.StartSalary);
                    AddInBase.Parameters.AddWithValue("@Password",      Manager.Password);
                    AddInBase.Parameters.AddWithValue("@ParentID",      Manager.ParentID);
                    AddInBase.Parameters.AddWithValue("@Group",         Manager.Group);
                    break;
                case "Продавец":
                    Group = "Продавец";
                    salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - Startdt.Year) * StartSalary * 0.03);
                    ParentID = Workers.tempID;
                    Salesman Salesman = new Salesman(salary, FIO, idEmployee, Startdt, StartSalary, Group, Password, ParentID);
                    AddInBase.Parameters.AddWithValue("@IDEmployee",        Salesman.IDEmployee);
                    AddInBase.Parameters.AddWithValue("@FIO",               Salesman.FIO);
                    AddInBase.Parameters.AddWithValue("@EntryDate",         Salesman.EntryDate);
                    AddInBase.Parameters.AddWithValue("@StartSalary",       Salesman.StartSalary);
                    AddInBase.Parameters.AddWithValue("@Password",          Salesman.Password);
                    AddInBase.Parameters.AddWithValue("@ParentID",          Salesman.ParentID);
                    AddInBase.Parameters.AddWithValue("@Group",             Salesman.Group);
                    break;
                default: MessageBox.Show("значение 'Группа' не должно быть пустым");
                    break;
            }
            AddInBase.Connection.Open();
            AddInBase.ExecuteNonQuery();
            AddInBase.Connection.Close();
            MessageBox.Show("Сотрудник" + FIO + " добавлен");
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

        private void btAddEmployer_Click(object sender, EventArgs e)
        {
            Form newwind = new AddEmployer();
            newwind.Show();
        }
    }
}
