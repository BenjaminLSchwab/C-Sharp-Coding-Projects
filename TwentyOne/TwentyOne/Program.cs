using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Casino;
using System.Data.SqlClient;
using System.Data;

namespace TwentyOne
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to back alley no-limit to-the-death black jack. What is your name?");
            string playerName = Console.ReadLine();

            if (playerName.ToLower() == "admin")
            {
                List<ExceptionEntity> exceptions = ReadExceptions();
                foreach (var exception in exceptions)
                {
                    Console.Write(exception.Id + " | ");
                    Console.Write(exception.ExceptionType + " | ");
                    Console.Write(exception.ExceptionMessage + " | ");
                    Console.Write(exception.TimeStamp + " | ");
                    Console.WriteLine();
                }
                Console.ReadLine();
                return;
            }

            Console.WriteLine("How much money do you have? (honor system)");
            bool validInput = false;
            int bank = 0;
            while (!validInput)
            {
                validInput = int.TryParse(Console.ReadLine(), out bank);
                if (!validInput) Console.WriteLine("Please input a whole number. This is the big leagues we dont care about cents!");
                //try
                //{
                //    bank = Convert.ToInt32(Console.ReadLine());
                //    validInput = true;
                //}
                //catch
                //{
                //    Console.WriteLine("Please input a whole number. This is the big leagues we dont care about cents!");
                //    validInput = false;
                //}
            }

            Console.WriteLine("Well, {0}, are you ready to begin?", playerName);
            string input = Console.ReadLine().ToLower();

            if (!input.Contains("y"))
            {
                Console.WriteLine("Have a nice day!");
                Console.ReadLine();
                return;
            }

            Player player = new Player(playerName, bank);
            Game game = new TwentyOneGame();
            game += player;
            player.IsActivleyPlaying = true;

            while (player.IsActivleyPlaying && player.Balance > 0)
            {
                try
                {
                    game.StartLog();
                    game.Play();
                    game.EndLog();
                }
                catch(FraudException ex)
                {
                    Console.WriteLine(ex.Message);
                    UpdateDatabaseWithException(ex);
                    Console.ReadLine();
                    return;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("There was an error, please run around in circles.");
                    UpdateDatabaseWithException(ex);
                    Console.ReadLine();
                    return;
                }
            }

            game -= player;
            Console.WriteLine("Thanks for playing!");
            Console.ReadLine();

        }

        private static void UpdateDatabaseWithException(Exception ex)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TwentyOneGame;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string queryString = @"INSERT INTO Exceptions(ExceptionType, ExceptionMessage, TimeStamp) VALUES
                                    (@ExceptionType, @ExceptionMessage, @TimeStamp)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@ExceptionType", SqlDbType.VarChar);
                command.Parameters.Add("@ExceptionMessage", SqlDbType.VarChar);
                command.Parameters.Add("@TimeStamp", SqlDbType.DateTime);

                command.Parameters["@ExceptionType"].Value = ex.GetType().ToString();
                command.Parameters["@ExceptionMessage"].Value = ex.Message;
                command.Parameters["@TimeStamp"].Value = DateTime.Now;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();


            }
        }

        private static List<ExceptionEntity> ReadExceptions()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TwentyOneGame;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string queryString = @"SELECT Id, ExceptionType, ExceptionMessage, TimeStamp FROM Exceptions";

            List<ExceptionEntity> exceptions = new List<ExceptionEntity>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ExceptionEntity exception = new ExceptionEntity();
                    exception.Id = Convert.ToInt32(reader["Id"]);
                    exception.ExceptionType = reader["ExceptionType"].ToString();
                    exception.ExceptionMessage = reader["ExceptionMessage"].ToString();
                    exception.TimeStamp = Convert.ToDateTime(reader["TimeStamp"]);
                    exceptions.Add(exception);

                }
                connection.Close();
            }
                return exceptions;

        }


    }
}
