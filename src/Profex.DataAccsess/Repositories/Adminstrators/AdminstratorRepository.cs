using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Adminstrators;
using Profex.DataAccsess.ViewModels.Adminstrators;
using Profex.DataAccsess.ViewModels.Users;
using Profex.Domain.Entities.adminstrators;
using Profex.Domain.Entities.users;

namespace Profex.DataAccsess.Repositories.Adminstrators;

public class AdminstratorRepository : BaseRepository, IAdminstratorsRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from adminstrators";
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

    public async Task<int> CreateAsync(Adminstrator entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.users(first_name, last_name, phone_number, phone_number_confirmed, " +
                "image_path, password_hash, salt, created_at, updated_at)" +
                    "VALUES (@FirstName, @LastName, @PhoneNumber, @PhoneNumberConfirmed, @Image_path, @PasswordHash, " +
                        "@Salt, @CreatedAt, @UpdatedAt);";

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
            string query = $"delete from adminstrators where id = {id}";

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

            string query = $"select * from adminstrators " +
                $"order by id desc " +
                    $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Adminstrator>(query)).ToList();

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
            string query = $"select * from adminstrators where id = {id}";
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

    public async Task<int> UpdateAsync(long id, Adminstrator entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.adminstrators" +
                $"SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, " +
                    $"phone_number_confirmed=@PhoneNumberConfirmed, image_path=@ImagePath, " +
                        $"password_hash=@PasswordHash, salt=@Salt, created_at=@CreatedAt, updated_at=@UpdatedAt" +
                            $"WHERE id = {id};";

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
