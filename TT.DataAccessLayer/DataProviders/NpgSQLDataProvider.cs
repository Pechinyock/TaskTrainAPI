using Npgsql;

namespace TT.DataAccessLayer.DataProviders;

public class NpgSQLDataProvider
{
    private readonly string _connetionString;

    public NpgSQLDataProvider(string connectionString)
    {
        _connetionString = connectionString;
    }

    public string GetCurrentDatabase() 
    {
        try
        {
            var result = String.Empty;
            using (var connection = new NpgsqlConnection(_connetionString))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = $"select current_database();";
                    var reader = cmd.ExecuteReader();
                    
                    while (reader.Read()) 
                    {
                        result = (string)reader["current_database"];
                    }
                }
                connection.Close();
            }
            return result;
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return String.Empty;
        }
    }
}
