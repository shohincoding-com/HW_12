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
            
        }
    }
}