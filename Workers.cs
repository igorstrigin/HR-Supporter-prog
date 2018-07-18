using System;
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
            public static string Group = "1)Работник";
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
            public static string Group = "2)Менеджер";

            public static int GetManagerplusInCreate(int ParentID)
            {
                int tempSalary = 0;
                
                    SQLiteConnection DB = new SQLiteConnection(@"Data Source=x86\\Salary.db;Pooling=true;FailIfMissing=false;Version=3");
                    SQLiteCommand ManagerPlus = new SQLiteCommand(DB);
                    ManagerPlus.Connection = DB;
                    ManagerPlus.CommandText = "select MainInfo.StartSalary,MainInfo.'Group',MainInfo.EntryDate, MainInfo.IDEmployee, MainInfo.ParentID from MainInfo where ParentId = @ParentID";
                    ManagerPlus.Parameters.AddWithValue("@ParentID", ParentID);
                try
                {
                    ManagerPlus.Connection.Open();
                    SQLiteDataReader dr = ManagerPlus.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            int datecalc = DateTime.Now.Year - dr.GetDateTime(2).Year;
                            if (DateTime.Now.Year - dr.GetDateTime(2).Month < 0) { datecalc--; }
                            switch (dr.GetString(1))
                            {
                                case "1)Работник":
                                    tempSalary = tempSalary + (dr.GetInt32(0) + Convert.ToInt32(datecalc * dr.GetInt32(0) * 0.03));
                                    break;
                                case "2)Менеджер":
                                    tempSalary = tempSalary + dr.GetInt32(0) + Convert.ToInt32(datecalc * dr.GetInt32(0) * 0.05) + GetManagerplusInCreate(dr.GetInt32(4));
                                    break;
                                case "3)Продавец":
                                tempSalary = tempSalary + dr.GetInt32(0) + Convert.ToInt32(datecalc * dr.GetInt32(0) * 0.01) + Salesman.GetSalesManplusInCreate(dr.GetInt32(3));
                                break;
                            }
                        }
                    }
                    dr.Close();
                }
                catch (Exception ex) { ex.ToString(); }
                finally
                {
                    ManagerPlus.Connection.Close();
                }
                return tempSalary;
            }

            public static int GetManagerplusInCalc(int ParentID, DateTime time)
            {
                int tempSalary = 0;
                int temp = 0;
            SQLiteConnection DB = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + "/x86/Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand ManagerPlus = new SQLiteCommand(DB);
                ManagerPlus.Connection = DB;
                ManagerPlus.CommandText = "select MainInfo.StartSalary,MainInfo.'Group',MainInfo.EntryDate, MainInfo.IDEmployee, MainInfo.ParentID from MainInfo where ParentId = @ParentID order by MainInfo.'Group' desc";
                ManagerPlus.Parameters.AddWithValue("@ParentID", ParentID);
                try
                {
                    ManagerPlus.Connection.Open();
                    SQLiteDataReader dr = ManagerPlus.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            int datecalc = DateTime.Now.Year - dr.GetDateTime(2).Year;
                            if (DateTime.Now.Year - dr.GetDateTime(2).Month < 0) { datecalc--; if (datecalc < 0) datecalc = 0; }
                            switch (dr.GetString(1))
                            {
                                case "1)Работник":
                                    int exp = dr.GetInt32(0) + Convert.ToInt32(datecalc * dr.GetInt32(0) * 0.03);
                                    if (exp > dr.GetInt32(0) * 1.3) { exp = Convert.ToInt32(dr.GetInt32(0) * 1.3); }
                                    tempSalary = tempSalary + exp;
                                    break;
                                case "2)Менеджер":
                                int check = dr.GetInt32(0);
                                int summary = 0;
                                    summary = Convert.ToInt32((GetManagerplusInCalc(dr.GetInt32(3), time) + tempSalary) * 0.005);
                                    int exp1 = dr.GetInt32(0) + Convert.ToInt32(datecalc * dr.GetInt32(0) * 0.03);
                                    if (exp1 > dr.GetInt32(0) * 1.4) { exp = Convert.ToInt32(dr.GetInt32(0) * 1.4); };
                                    tempSalary = exp1 + summary;
                                    break;
                                case "3)Продавец":
                                    int summary1 = 0;
                                    int exp2 = dr.GetInt32(0) + Convert.ToInt32(datecalc * dr.GetInt32(0) * 0.01);
                                    if (exp2 > dr.GetInt32(0) * 1.35) { exp = Convert.ToInt32(dr.GetInt32(0) * 1.35); }
                                    summary1 = Convert.ToInt32((Salesman.GetSalesManplusInCalc(dr.GetInt32(3), time) + tempSalary) * 0.003);
                                    tempSalary = exp2 + summary1;
                                    break;
                            }
                        }
                    }
                    dr.Close();
                }
                catch (Exception ex) { ex.ToString(); }
                finally
                {
                    ManagerPlus.Connection.Close();
                }
                return tempSalary;
            }


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
            public static string Group = "3)Продавец";


        public static int GetSalesManplusInCreate(int IDEmployee)
        {
            int tempSalary = 0;

            SQLiteConnection DB = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + "/x86/Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand SalesPlus = new SQLiteCommand(DB);
            SalesPlus.Connection = DB;
            SalesPlus.CommandText = "with NewTable as (select MainInfo.IDEmployee, MainInfo.ParentID, MainInfo.StartSalary, MainInfo.EntryDate, MainInfo.'Group' from MainInfo where IDEmployee = @IDEmployee union all select N.IDEmployee, N.ParentID, N.StartSalary, N.EntryDate, N.'Group' from NewTable inner join MainInfo N on NewTable.IDEmployee = N.ParentID) select StartSalary, NewTable.'Group', EntryDate, NewTable.IDEmployee ,NewTable.ParentID from NewTable where IDEmployee <> @IDEmployee";
            SalesPlus.Parameters.AddWithValue("@IDEmployee", IDEmployee);
            try
            {
                SalesPlus.Connection.Open();
                SQLiteDataReader dr = SalesPlus.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int datecalc = DateTime.Now.Year - dr.GetDateTime(2).Year;
                        if (DateTime.Now.Year - dr.GetDateTime(2).Month < 0) {datecalc--; }
                        switch (dr.GetString(1))
                        {
                            case "1)Работник":
                                int exp = dr.GetInt32(0) + Convert.ToInt32(datecalc * dr.GetInt32(0) * 0.03);
                                if (exp > dr.GetInt32(0) * 1.3) { exp = Convert.ToInt32(dr.GetInt32(0) * 1.3); }
                                tempSalary = tempSalary + exp;
                                break;
                            case "2)Менеджер":
                                tempSalary = tempSalary + dr.GetInt32(0) + Convert.ToInt32(datecalc * dr.GetInt32(0) * 0.05) + Manager.GetManagerplusInCreate(dr.GetInt32(3));
                                break;
                            case "3)Продавец":
                                tempSalary = tempSalary + dr.GetInt32(0) + Convert.ToInt32(datecalc * dr.GetInt32(0) * 0.01) + GetSalesManplusInCreate(dr.GetInt32(4));
                                break;
                        }
                    }
                }
                dr.Close();
            }
            catch (Exception ex) { ex.ToString(); }
            finally
            {
                SalesPlus.Connection.Close();
            }
            return tempSalary;
        }

        public static int GetSalesManplusInCalc(int IDEmployee, DateTime time)
        {
            int tempSalary = 0;

            SQLiteConnection DB = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + "/x86/Salary.db;Pooling=true;FailIfMissing=false;Version=3");
            SQLiteCommand ManagerPlus = new SQLiteCommand(DB);
            ManagerPlus.Connection = DB;
            ManagerPlus.CommandText = "with NewTable as (select MainInfo.IDEmployee, MainInfo.ParentID, MainInfo.StartSalary, MainInfo.EntryDate, MainInfo.'Group' from MainInfo where IDEmployee = @IDEmployee union all select N.IDEmployee, N.ParentID, N.StartSalary, N.EntryDate, N.'Group' from NewTable inner join MainInfo N on NewTable.IDEmployee = N.ParentID) select StartSalary, NewTable.'Group', EntryDate, NewTable.IDEmployee ,NewTable.ParentID from NewTable where IDEmployee <> @IDEmployee";
            ManagerPlus.Parameters.AddWithValue("@IDEmployee", IDEmployee);
            try
            {
                ManagerPlus.Connection.Open();
                SQLiteDataReader dr = ManagerPlus.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int datecalc = DateTime.Now.Year - dr.GetDateTime(2).Year;
                        if (DateTime.Now.Year - dr.GetDateTime(2).Month < 0) { datecalc--; }
                        switch (dr.GetString(1))
                        {
                            case "1)Работник":
                                int exp = dr.GetInt32(0) + Convert.ToInt32(datecalc * dr.GetInt32(0) * 0.03);
                                if (exp > dr.GetInt32(0) * 1.3) { exp = Convert.ToInt32(dr.GetInt32(0) * 1.3); }
                                tempSalary = exp;
                                break;
                            case "2)Менеджер":
                                int check = dr.GetInt32(0);
                                int exp1 = dr.GetInt32(0) + Convert.ToInt32(datecalc * dr.GetInt32(0) * 0.03);
                                if (exp1 > dr.GetInt32(0) * 1.4) { exp = Convert.ToInt32(dr.GetInt32(0) * 1.4); }
                                tempSalary = tempSalary + exp1 + Convert.ToInt32(Manager.GetManagerplusInCalc(dr.GetInt32(3), time) * 0.005);
                                break;
                            case "3)Продавец":
                                int exp2 = dr.GetInt32(0) + Convert.ToInt32(datecalc * dr.GetInt32(0) * 0.01);
                                if (exp2 > dr.GetInt32(0) * 1.35) { exp = Convert.ToInt32(dr.GetInt32(0) * 1.35); }
                                tempSalary = tempSalary + exp2 + Convert.ToInt32(GetSalesManplusInCalc(dr.GetInt32(3), time)*0.003);
                                break;
                        }
                    }
                }
                dr.Close();
            }
            catch (Exception ex) { ex.ToString(); }
            finally
            {
                ManagerPlus.Connection.Close();
            }
            return tempSalary;
        }


        public Salesman(int salary, string fio, int idEmployee, DateTime entrydate, int startSalary, string Group, string Password, int parentID) : base(fio, idEmployee, entrydate, startSalary,Password,parentID)
            {
            salary = Convert.ToInt32(StartSalary + (DateTime.Now.Year - EntryDate.Year) * StartSalary * 0.01);
            if (salary > StartSalary * 1.35) { salary = Convert.ToInt32(StartSalary * 1.35); }
            Salary = salary;
        }
        }
}
