using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer;

namespace AppLayer
{
    public class DatabaseController
    {
        private readonly string ConnectionString;

        public DatabaseController()
        {
            //string tempsting = Path.GetFullPath("ConnectionString.txt");
            StreamReader reader = new StreamReader(@"..\..\ConnectionString.txt");
            ConnectionString = reader.ReadLine();
            reader.Close();
        }

        public bool CreateEvent(CalendarEntry events)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                if (events.Hearse == null)
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "EXEC dbo.insert_event2 @START_AT, @END_AT, @AT_ADDRESS, @COMMENT";
                    command.Parameters.AddWithValue("@START_AT", events.Start);
                    command.Parameters.AddWithValue("@END_AT", events.End);
                    command.Parameters.AddWithValue("@AT_ADDRESS", events.Address);
                    command.Parameters.AddWithValue("@COMMENT", events.Comment);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "EXEC dbo.insert_event @START_AT, @END_AT, @VEHICLE, @AT_ADDRESS, @COMMENT";
                    command.Parameters.AddWithValue("@START_AT", events.Start);
                    command.Parameters.AddWithValue("@END_AT", events.End);
                    command.Parameters.AddWithValue("@VEHICLE", events.Hearse.Key);
                    command.Parameters.AddWithValue("@AT_ADDRESS", events.Address);
                    command.Parameters.AddWithValue("@COMMENT", events.Comment);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return true;
        }

        public bool AlterEvent(CalendarEntry events)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "EXEC dbo.update_event @KEY, @START_AT, @END_AT, @VEHICLE, @AT_ADDRESS, @COMMENT";
                command.Parameters.AddWithValue("@KEY", events.Key);
                command.Parameters.AddWithValue("@START_AT", events.Start.ToString());
                command.Parameters.AddWithValue("@END_AT", events.End.ToString());
                command.Parameters.AddWithValue("@VEHICLE", events.Hearse.Key);
                command.Parameters.AddWithValue("@AT_ADDRESS", events.Address);
                command.Parameters.AddWithValue("@COMMENT", events.Comment);
                command.Connection = connection;
                //Console.WriteLine(events.Key + " " + events.Start.ToString() + " " + events.End.ToString() + " " + events.Hearse.Key + " " + events.Address + " " + events.Comment);
                connection.Open();
                command.ExecuteNonQuery();
            }
            return true;
        }

        public bool DeleteEvent(int key)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "EXEC dbo.delete_event @KEY";
                command.Parameters.AddWithValue("@KEY", key);
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
            return true;
        }       

        public bool Update(CalendarEntryRepo eventRepo, HearseRepo rustvognRepo)
        {
            foreach (Hearse item in rustvognRepo.GetListOfHearses())
            {

            }
            foreach (CalendarEntry item in eventRepo.GetCopyEvents())
            {
                if (item.Status == Status.Changed)
                {
                    AlterEvent(item);
                    item.Status = Status.UnChanged;
                }
                else if (item.Status == Status.Deleted)
                {
                    DeleteEvent(item.Key);
                }
                else if (item.Status == Status.NewlyMade)
                {
                    CreateEvent(item);
                    item.Status = Status.UnChanged;
                }
                else if (item.Status == Status.UnChanged)
                {
                    continue;
                }
                else
                {
                    throw new InvalidDataException("Ukendt status enum. Fil: 'DatabaseController.Update'");
                }
            }

            return true;
        }

        public List<Tuple<int, int>> StartUpHearse()
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GET_ALL_HEARSE", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int pri = (int)reader["PRIORITY_"];
                    int key = (int)reader["SURROGATE_KEY"];
                    Tuple<int, int> tuple = new Tuple<int, int>(key, pri);
                    result.Add(tuple);
                }
            }
            return result;
        }

        public List<Tuple<int, DateTime, DateTime, int, string, string>> StartUpEvents()
        {
            List<Tuple<int, DateTime, DateTime, int, string, string>> result =
                new List<Tuple<int, DateTime, DateTime, int, string, string>>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GET_ALL_EVENTS", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int surrogateKey = (int)reader["SURROGATE_KEY"];
                    DateTime start = (DateTime)reader["START_AT"];
                    DateTime end = (DateTime)reader["END_AT"];
                    int vehicle = reader["VEHICLE"] == System.DBNull.Value ? default(int) : (int)reader["VEHICLE"];
                    string address = (string)reader["AT_ADDRESS"];
                    string comment = (string)reader["COMMENT"];

                    Tuple<int, DateTime, DateTime, int, string, string> tuple =
                        new Tuple<int, DateTime, DateTime, int, string, string>
                        (surrogateKey, start, end, vehicle, address, comment);

                    result.Add(tuple);
                }
            }
            return result;
        }
    }
}
