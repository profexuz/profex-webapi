using Npgsql;

namespace Profex.DataAccsess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;

    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        this._connection = new NpgsqlConnection("Host=db-postgresql-lon1-56814-do-user-14588545-0.b.db.ondigitalocean.com; Port=25060; Database=gettalim-db; User Id=doadmin; Password=AVNS_9n5XkthWFqLIltZLhLQ");
    }
}
