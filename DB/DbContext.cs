using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace DevelopersToday_TestTask.DB;


public abstract class DbContext : IDisposable
{
    protected DbSettings _dbSettings;

    protected IDbConnection _dbConnection;

    public DbContext(DbSettings dbSettings)
    {
        _dbSettings = dbSettings;
    }
    
    public DbContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }

    public virtual IDbConnection CreateConnection()
    {
        var connectionString = $"Server={_dbSettings.Server}; Database={_dbSettings.Database}; " +
                               $"User ID={_dbSettings.UserId}; Password={_dbSettings.Password};";
        _dbConnection = new SqlConnection(connectionString);
        return _dbConnection;
    }

    public void Dispose()
    {
        if (_dbConnection.State != ConnectionState.Closed)
            _dbConnection.Close();
        _dbConnection.Dispose();
    }
}