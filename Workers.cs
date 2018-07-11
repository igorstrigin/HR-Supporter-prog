using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace SalaryGUI
{
    public class Workers
    {
        public static int tempID { get; set; }
        public int IDEmployee = 0;
        public DateTime EntryDate;
        public int StartSalary = 0;
        public string FIO = "";
        public string Password = "";
        public int ParentID = 0;


        public Workers(string fio, int idEmployee, DateTime entrydate, int startSalary, string password, int parentID) 
        {
            IDEmployee = idEmployee;
            EntryDate = entrydate;
            StartSalary = startSalary;
            FIO = fio;
            Password = password;
            ParentID = parentID;
        }
    }


         class Employee : Workers
         {
            public int Salary;
            public static string Group = "Работник";
            public Employee(int salary,string fio, int idEmployee, DateTime entrydate, int startSalary, string Group, string Password, int parentID) : base(fio, idEmployee, entrydate, startSalary, Password,parentID)
            {
            salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - EntryDate.Year) * StartSalary * 0.03);
            if (salary > StartSalary * 1.3) { salary = Convert.ToInt32(StartSalary * 1.3); }
            Salary = salary;
            ParentID = parentID;
            }
         }

        class Manager : Workers
        {

            public int Salary;
           // public int GetManagerplus()
            //{


             //   return s;
            //}


            
            public static string Group = "Менеджер";
            public Manager(int salary, string fio, int idEmployee, DateTime entrydate, int startSalary, string Group, string Password, int parentID) : base(fio, idEmployee, entrydate, startSalary,Password,parentID)
            {
            salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - EntryDate.Year) * StartSalary * 0.05);
                if (salary > StartSalary * 1.4) { salary = Convert.ToInt32(StartSalary * 1.4); }
                Salary = salary;
            }
        }

        class Salesman : Workers
        {
            public int Salary;
            public static string Group = "Продавец";
            public Salesman(int salary, string fio, int idEmployee, DateTime entrydate, int startSalary, string Group, string Password, int parentID) : base(fio, idEmployee, entrydate, startSalary,Password,parentID)
            {
            salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - EntryDate.Year) * StartSalary * 0.01);
            if (salary > StartSalary * 1.35) { salary = Convert.ToInt32(StartSalary * 1.35); }
            Salary = salary;
        }
        }
}
