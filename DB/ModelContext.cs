using System.Data;
using Dapper;

using Microsoft.Extensions.Options;
using Z.Dapper.Plus;

namespace DevelopersToday_TestTask.DB;

public sealed class ModelContext: DbContext, IModelContext
{
    public ModelContext(DbSettings dbSettings) : base(dbSettings)
    {
        CreateConnection();
    }

    public ModelContext(IOptions<DbSettings> dbSettings) : base(dbSettings)
    {
        CreateConnection();
    }

    public override IDbConnection CreateConnection()
    {
        _dbConnection = base.CreateConnection();
        return _dbConnection;
    }

    public void AddModels(List<DefaulModel> models)
    {
        DapperPlusManager.Entity<DefaulModel>().Table("Items");
        _dbConnection.BulkInsert(models);
    }

    public long GetAmount()
    {
        long amount = 0;
            
        amount = _dbConnection.QueryFirstOrDefault<long>(""" select count(*) from Items""");
        return amount;
    }
}