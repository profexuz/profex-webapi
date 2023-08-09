using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Skills;
using Profex.Domain.Entities.skills;

namespace Profex.DataAccsess.Repositories.Skills;

public class SkillRepository : BaseRepository, ISkillRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from skills";
            var result = await _connection.QuerySingleAsync<long>(query);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(Skill entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.skills(category_id, title, description, created_at, updated_at) " +
                "VALUES (@CategoryId, @Title, @Description, @CreatedAt, @UpdatedAt);";

            var result = await _connection.ExecuteAsync(query, entity);

            return result;
        }
        catch 
        { 
            return 0; 
        }
        finally 
        { 
            await _connection.CloseAsync(); 
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM skills WHERE id=@Id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync(); 
        }
    }

    public async Task<IList<Skill>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM public.masters ORDER BY id desc offset {@params.GetSkipCount} " +
                $"limit {@params.PageSize}";

            var resMas = (await _connection.QueryAsync<Skill>(query)).ToList();

            return resMas;
        }
        catch
        {
            return new List<Skill>();
        }
        finally
        { 
            await _connection.CloseAsync(); 
        }
    }

    public async Task<Skill?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string qeury = $"SELECT * FROM skills where id=@Id";
            var res = await _connection.QuerySingleAsync<Skill>(qeury, new { Id = id });

            return res;
        }
        catch
        {
            return null;
        }
        finally 
        {
            await _connection.CloseAsync(); 
        }
    }

    public async Task<int> UpdateAsync(long id, Skill entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.skills" +
                $"SET category_id=@CategoryId, title=@Title, description=@Description, created_at=@CreatedAt, " +
                    $"updated_at=@UpdatedAt WHERE id = {id}";

            var result = await _connection.ExecuteAsync(query, entity);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
