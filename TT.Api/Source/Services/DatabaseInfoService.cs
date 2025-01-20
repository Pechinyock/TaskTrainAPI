using Microsoft.Extensions.Options;
using TT.DataAccessLayer.DataProviders;
using TT.Services.Interafces;

namespace TT.Services;

public class DatabaseInfoOptions
{
    public string ConnectionString { get; set; }
}

public class DatabaseInfoService : IDatabaseInfoService
{
    protected DatabaseInfoOptions Config { get; set; }

    private readonly string _connectionString;

    public DatabaseInfoService(IOptions<DatabaseInfoOptions> options)
    {
        /* Validate */
        if (options == null)
            throw new ArgumentNullException(nameof(options));
        if(options.Value == null)
            throw new ArgumentNullException(nameof(options.Value));

        Config = options.Value;

        _connectionString = options.Value.ConnectionString;
    }

    public string GetDatabaseName()
    {
        var dbConnection = new NpgSQLDataProvider(_connectionString);
        var result = dbConnection.GetCurrentDatabase();
        return result;
    }
}
