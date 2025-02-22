﻿using Microsoft.Extensions.Options;
using TT.DataAccessLayer.DataProviders;

namespace TT.Api.Services;

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
        if (options == null)
            throw new ArgumentNullException(nameof(options));
        if (options.Value == null)
            throw new ArgumentNullException(nameof(options.Value));

        Config = options.Value;

        _connectionString = options.Value.ConnectionString;
//        _storageProvider = new NpgSQLDataProvider(_connectionString);
    }

    public string GetDatabaseVendorName()
    {
        throw new NotImplementedException();
  //      return _storageProvider.GetDatabaseVendorName();
    }
}
