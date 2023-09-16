using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Admins;
using Profex.DataAccsess.ViewModels.Adminstrators;
using Profex.Domain.Entities.admins;

namespace Profex.DataAccsess.Repositories.Adminstrators;

public class AdminsRepository : BaseRepository, IAdminsRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from public.admins";
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

    public async Task<int> CreateAsync(Admin entity)
    {
        try
        {
            await _connection.OpenAsync();



            string query = "INSERT INTO public.admins (first_name, last_name, phone_number, password_hash, salt, created_at, updated_at) " +
                " VALUES (@FirstName, @LastName, @PhoneNumber, @PasswordHash, @Salt, @CreatedAt, @UpdatedAt )";
            return await _connection.ExecuteAsync(query, entity);
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
            string query = $"DELETE FROM public.admins WHERE id = {id}";

            return await _connection.ExecuteAsync(query);
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

    public async Task<IList<AdminstratorsViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM public.admins ORDER BY id DESC " +
                    $" OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

            var result = (await _connection.QueryAsync<Admin>(query)).ToList();

            return (IList<AdminstratorsViewModel>)result;
        }
        catch
        {
            return new List<AdminstratorsViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<AdminstratorsViewModel?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.admins WHERE id = {id}";
            var result = await _connection.QuerySingleAsync<AdminstratorsViewModel>(query);

            return result;
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


    public async Task<Admin> GetByPhoneAsync(string phone)

    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.admins WHERE phone_number = '{phone}' ";
            var result = await _connection.QuerySingleAsync<Admin>(query);
            
            return result;
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

    public async Task<int> UpdateAsync(long id, Admin entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.admins" +
                $"SET first_name = @FirstName, last_name = @LastName, phone_number = @PhoneNumber, " +
                        $" password_hash=@PasswordHash, salt=@Salt, updated_at=@UpdatedAt " +
                            $" WHERE id = {id};";

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
