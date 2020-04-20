using System;
using System.Data;
using System.Data.SqlClient;

namespace HW_12
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlCon RealSql = new SqlCon();
            
            {
                Console.Write("Пожалуйста дайте нужную комнаду\n\n1.Дабавить новий ползователь\n\n2.Удалить ползовтелья из БД\n\n3.Выбрать всё записи базаго данныха\n\n4.Выбрать один запис по Id\n\n5.Обновить запис по Id\n\n6.Закрыт программу\n\n");
                var userCmd = Console.ReadLine();

                if(userCmd == "1")
                    {
                        Console.Write("LastName: ");
                        string LastName = Console.ReadLine();
                        Console.Write("FirstName: ");
                        string FirstName = Console.ReadLine();
                        Console.Write("MiddleName: ");
                        string MiddleName = Console.ReadLine();
                        Console.Write("BirthDate: ");
                        string BirthDate = Console.ReadLine();
                        RealSql.InsertNewPer(FirstName, LastName, MiddleName, BirthDate);
                    }
                else if(userCmd == "2")
                    {
                        Console.Write("Id: ");
                        int Id=int.Parse(Console.ReadLine());
                        RealSql.DeleteIdPer(Id);
                    }
                else if(userCmd == "3")
                    {
                        RealSql.SelectAllPer();
                    }
                else if(userCmd == "4")
                    {
                    Console.Write("Id: ");
                    int Id=int.Parse(Console.ReadLine());
                    RealSql.SelectIdPer(Id);
                    }
                else if(userCmd == "5")
                    {
                        Console.Write("LastName: ");
                        string LastName = Console.ReadLine();
                        Console.Write("FirstName: ");
                        string FirstName = Console.ReadLine();
                        Console.Write("MiddleName: ");
                        string MiddleName = Console.ReadLine();
                        Console.Write("BirthDate ");
                        string BirthDate = Console.ReadLine();
                        Console.Write("Id: ");
                        int Id=int.Parse(Console.ReadLine());
                        RealSql.UpdateIdPer(Id,FirstName,LastName,MiddleName,BirthDate);
                    }
                else if(userCmd == "6")
                    { return; }
                else
                {
                        Console.WriteLine("Вы выбрали неправилную комманду");
                }
                
            }
        }
        class SqlCon
        {
            SqlConnection ConMyDb = new SqlConnection(@"Data source=SHOHINJON; Initial catalog=AllifAcademy; Integrated security=true");
            public void OpenConMyDb()
            {
                if (ConMyDb.State == ConnectionState.Closed)
                    ConMyDb.Open();
            }
            public void CloseConMyDb()
            {
                ConMyDb.Close();
            }
            public void InsertNewPer(string LastName, string FirstName, string MiddleName, string BirthDate)
            {
                OpenConMyDb();
                using (SqlCommand NewCmd = new SqlCommand("insert into Person([LastName],[FirstName],[MiddleName],[BirthDate]) Values ('" + LastName + "','" + FirstName + "','" + MiddleName + "','" + Convert.ToDateTime(BirthDate) + "')", ConMyDb))
                {
                    int a = NewCmd.ExecuteNonQuery();
                    if (a > 0) { Console.WriteLine("Данные успешно добавлен в базу данных!"); }
                }
                CloseConMyDb();
            }
            public void SelectAllPer()
            {
                OpenConMyDb();
                using (SqlCommand NewCmd = new SqlCommand("Select * from Person", ConMyDb))
                {
                    using (SqlDataReader reader = NewCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            System.Console.WriteLine($"Id: {reader.GetValue(0)} \t LastName: {reader.GetValue(1)} \t FirstName: {reader.GetValue(2)} \t MiddleName: {reader.GetValue(3)} \t BirthDate: {reader.GetValue(4).ToString().Substring(0, 10)}");
                        }
                    }
                }
                CloseConMyDb();
            }
            public void SelectIdPer(int Id)
            {
                OpenConMyDb();
                using (SqlCommand NewCmd = new SqlCommand("Select * from Person where Id =" + Id, ConMyDb))
                {
                    using (SqlDataReader reader = NewCmd.ExecuteReader())
                    {
                        int c = 0;
                        while (reader.Read())
                        {
                            System.Console.WriteLine($"Id: {reader.GetValue(0)} \t LastName: {reader.GetValue(1)} \t FirstName: {reader.GetValue(2)} \t MiddleName: {reader.GetValue(3)} \t BirthDate: {reader.GetValue(4).ToString().Substring(0, 10)}");
                            c += (reader.GetValue(0).ToString() == Id.ToString()) ? 1 : 0;
                        }
                        if (c == 0)
                            System.Console.WriteLine("Id которого вы выбрали не найдено!");
                    }
                }
                CloseConMyDb();
            }
            public void UpdateIdPer(int Id, string LastName, string FirstName, string MiddleName, string BirthDate)
            {
                OpenConMyDb();
                using (SqlCommand NewCmd = new SqlCommand("UPDATE Person set LastName = '" + LastName + "',FirstName ='" + FirstName + "',MiddleName ='" + MiddleName + "','"+Convert.ToDateTime(BirthDate)+"') where Id =" + Id, ConMyDb))
                {
                    if (NewCmd.ExecuteNonQuery() > 0)
                        Console.WriteLine("Updated Person with " + Id + " Id!");
                    else
                        Console.WriteLine("Id которого вы выбрали не найдено!");
                }
                CloseConMyDb();
            }
            public void DeleteIdPer(int Id)
            {
                OpenConMyDb();
                using (SqlCommand NewCmd = new SqlCommand("Delete Person where Id ='"+Id+"'", ConMyDb))
                {
                    if (NewCmd.ExecuteNonQuery() > 0)
                        Console.WriteLine("Запис удалено!");
                    else
                        Console.WriteLine("Id которого вы выбрали не найдено!");
                }
                CloseConMyDb();
            }
        }
    }
}