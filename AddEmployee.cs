﻿using System;
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
            SQLiteConnection DB = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + "/x86/Salary.db;Pooling=true;FailIfMissing=false;Version=3");
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
            //MessageBox.Show("ID =" + result);
            return result;
        }



        private void btAdd_Click(object sender, EventArgs e)
        {
            string FIO = tbSurname.Text + " " + tbName.Text + " " + tbSecondName.Text;
            int StartSalary = Convert.ToInt32(tbStartSalary.Text);
            int idEmployee = GetEmployeeId();
            DateTime Startdt = dtStart.Value;
            Startdt = Startdt.Date;
            int salary = 0;
            string Group;
            string Password = tbPassword.Text;
            int ParentID = 0;
            SQLiteConnection DB = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + "/x86/Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand AddInBase = new SQLiteCommand(DB);
            AddInBase.Connection = DB;
            AddInBase.CommandText = "insert into MainInfo(IDEmployee,FIO,EntryDate,StartSalary,'Group',Password,ParentID,Salary) values (@IDEmployee,@FIO,@EntryDate,@StartSalary,@Group,@Password,@ParentID,@Salary)";
            try
            {
                switch (cbGroup.Text)
                {
                    case "1)Работник":
                        Group = "1)Работник";
                        ParentID = Workers.tempID;
                        salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - Startdt.Year) * StartSalary * 0.03);
                        Employee employee = new Employee(salary, FIO, idEmployee, Startdt, StartSalary, Group, Password, ParentID);
                        AddInBase.Parameters.AddWithValue("@IDEmployee", employee.IDEmployee);
                        AddInBase.Parameters.AddWithValue("@FIO", employee.FIO);
                        AddInBase.Parameters.AddWithValue("@EntryDate", employee.EntryDate);
                        AddInBase.Parameters.AddWithValue("@StartSalary", employee.StartSalary);
                        AddInBase.Parameters.AddWithValue("@Password", employee.Password);
                        AddInBase.Parameters.AddWithValue("@ParentID", employee.ParentID);
                        AddInBase.Parameters.AddWithValue("@Group", Employee.Group);
                        AddInBase.Parameters.AddWithValue("@Salary", employee.Salary);
                        break;
                    case "2)Менеджер":
                        Group = "2)Менеджер";
                        ParentID = Workers.tempID;
                        salary = Manager.GetManagerplusInCreate(ParentID); ;
                        Manager manager = new Manager(salary, FIO, idEmployee, Startdt, StartSalary, Group, Password, ParentID);
                        AddInBase.Parameters.AddWithValue("@IDEmployee", manager.IDEmployee);
                        AddInBase.Parameters.AddWithValue("@FIO", manager.FIO);
                        AddInBase.Parameters.AddWithValue("@EntryDate", manager.EntryDate);
                        AddInBase.Parameters.AddWithValue("@StartSalary", manager.StartSalary);
                        AddInBase.Parameters.AddWithValue("@Password", manager.Password);
                        AddInBase.Parameters.AddWithValue("@ParentID", manager.ParentID);
                        AddInBase.Parameters.AddWithValue("@Group", Manager.Group);
                        AddInBase.Parameters.AddWithValue("@Salary", manager.Salary);
                        break;
                    case "3)Продавец":
                        Group = "3)Продавец";
                        salary = Salesman.GetSalesManplusInCreate(idEmployee);
                        ParentID = Workers.tempID;
                        Salesman salesman = new Salesman(salary, FIO, idEmployee, Startdt, StartSalary, Group, Password, ParentID);
                        AddInBase.Parameters.AddWithValue("@IDEmployee", salesman.IDEmployee);
                        AddInBase.Parameters.AddWithValue("@FIO", salesman.FIO);
                        AddInBase.Parameters.AddWithValue("@EntryDate", salesman.EntryDate);
                        AddInBase.Parameters.AddWithValue("@StartSalary", salesman.StartSalary);
                        AddInBase.Parameters.AddWithValue("@Password", salesman.Password);
                        AddInBase.Parameters.AddWithValue("@ParentID", salesman.ParentID);
                        AddInBase.Parameters.AddWithValue("@Group", Salesman.Group);
                        AddInBase.Parameters.AddWithValue("@Salary", salesman.Salary);
                        break;
                    default:
                        MessageBox.Show("значение 'Группа' не должно быть пустым");
                        break;
                }
                AddInBase.Connection.Open();
                AddInBase.ExecuteNonQuery();
                MessageBox.Show("Сотрудник: " + FIO + " добавлен \r\nЗарплата: " + salary.ToString());
            }
            catch (Exception) { MessageBox.Show("Проверьте заполнение всех ячеек"); }
            finally { AddInBase.Connection.Close(); }
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
