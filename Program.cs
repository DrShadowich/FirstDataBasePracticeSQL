using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
namespace SQL_test
{
    internal class Program
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;

        private static SqlConnection _sqlConnection = null;
        
        static void Main(string[] args)
        {
            _sqlConnection = new SqlConnection(_connectionString);
            _sqlConnection.Open();

            Console.WriteLine("Welcome to my first console app with database.");

            SqlDataReader sqlDataReader = null;

            string command = string.Empty;

            while (true)
            {
                try {
                    Console.Write("> ");
                    command = Console.ReadLine();
                    #region EXIT
                    if (command.ToLower().Equals("exit"))
                    {
                        if (_sqlConnection.State == System.Data.ConnectionState.Open) _sqlConnection.Close();

                        sqlDataReader?.Close();

                        break;
                    }
                    #endregion

                    SqlCommand sqlCommand = new SqlCommand(command, _sqlConnection);

                    switch (command.Split(' ')[0].ToLower())
                    {
                        case "select":
                            sqlDataReader = sqlCommand.ExecuteReader();

                            while (sqlDataReader.Read())
                            {
                                Console.WriteLine($"{sqlDataReader["Name"]} {sqlDataReader["BirthDay"]} {sqlDataReader["Status"]}");
                                Console.WriteLine(new string('-', 30));
                            }
                            sqlDataReader?.Close();
                            break;

                        case "insert":

                            Console.WriteLine($"Added: {sqlCommand.ExecuteNonQuery()} strings");

                            break;

                        case "update":
                            Console.WriteLine($"Updated: {sqlCommand.ExecuteNonQuery()} strings");

                            break;

                        case "delete":
                            Console.WriteLine($"Deleted: {sqlCommand.ExecuteNonQuery()} strings");
                            break;

                        default:
                            Console.WriteLine("Invalid Command");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Console.WriteLine("Enter any button...");
            Console.ReadKey();
        }
    }
}

