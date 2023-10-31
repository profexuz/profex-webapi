using Npgsql;

namespace Profex.DataAccsess.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;

    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

        _connection = new NpgsqlConnection("Host=db-postgresql-nyc3-62486-do-user-14993247-0.c.db.ondigitalocean.com; Port=25060; Database=profex-db; User Id=doadmin; Password=AVNS_6X-S9VpP5KFVSzOvFXt;");
    }
}
