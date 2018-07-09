using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace SalaryGUI
{
    class Workers
    {
        public int IDEmployee { get; set; }
        public DateTime EntryDate { get; set; }
        public int StartSalary { get; set; }
        public string FIO { get; set; }
        public string Group { get; set; }

        public Workers(string fio, int idEmployee, DateTime entrydate, int startSalary, string Group) 
        {
            IDEmployee = idEmployee;
            EntryDate = entrydate;
            StartSalary = startSalary;
            FIO = fio;
        }
    }


         class Employee : Workers
         {
            private int Salary;
            public Employee(int salary,string fio, int idEmployee, DateTime entrydate, int startSalary, string Group) : base(fio, idEmployee, entrydate, startSalary, Group)
            {
            salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - EntryDate.Year) * StartSalary * 0.03);
            if (salary > StartSalary * 1.3) { salary = Convert.ToInt32(StartSalary * 1.3); }
            Salary = salary;
            Group = "Работник";
            }
         }

        class Manager : Workers
        {
            private int Salary;
            public Manager(int salary, string fio, int idEmployee, DateTime entrydate, int startSalary, string Group) : base(fio, idEmployee, entrydate, startSalary,Group)
            {
                salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - EntryDate.Year) * StartSalary * 0.05);
                if (salary > StartSalary * 1.4) { salary = Convert.ToInt32(StartSalary * 1.4); }
                Salary = salary;
                Group = "Менеджер";
            }
        }

        class Salesman : Workers
        {
            private int Salary;
            public Salesman(int salary, string fio, int idEmployee, DateTime entrydate, int startSalary, string Group) : base(fio, idEmployee, entrydate, startSalary,Group)
            {
            salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - EntryDate.Year) * StartSalary * 0.01);
            if (salary > StartSalary * 1.35) { salary = Convert.ToInt32(StartSalary * 1.35); }
            Salary = salary;
            Group = "Продавец";
            }
    }
}
