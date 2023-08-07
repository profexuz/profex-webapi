using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Masters;
using Profex.DataAccsess.ViewModels.Masters;
using Profex.Domain.Entities.masters;

namespace Profex.DataAccsess.Repositories.Masters
{
    public class MasterRepository : BaseRepository, IMasterRepository
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select count(*) from masters";
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

        public async Task<int> CreateAsync(Master entity)
        {
            
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.masters(first_name, last_name, phone_number, phone_number_confirmed, image_path, password_hash, salt, is_free, created_at, updated_at)" +
                    "VALUES (@FirstName, @LastName, @PhoneNumber, @PhoneNumberConfirmed, @ImagePath, @PasswordHash, @Salt, @IsFree, @CreatedAt, @UpdatedAt);";
                var result = await _connection.ExecuteAsync(query, entity);
                return result;
            }
            catch { return 0;  }
            finally { await _connection.CloseAsync(); }
        }

        public async Task<int> DeleteAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "DELETE FROM masters WHERE id=@Id";
                var result = await _connection.ExecuteAsync(query, new { Id = id });
                return result;
            }
            catch
            {
                return 0;
            }
            finally
            { await _connection.CloseAsync(); }

        }

        public async Task<IList<MasterViewModel>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM public.masters ORDER BY id desc offset {@params.GetSkipCount} limit {@params.PageSize}";
                var resMas = (await _connection.QueryAsync<Master>(query)).ToList();
                return (IList<MasterViewModel>)resMas;
            }
            catch
            {
                return new List<MasterViewModel>();
            }
            finally
            { await _connection.CloseAsync(); }
        }

        public async Task<MasterViewModel?> GetByIdAsync(long id)
        {
            //throw new NotImplementedException();
            try
            {
                await _connection.OpenAsync();
                string qeury = $"SELECT * FROM masters where id=@Id";
                var res = await _connection.QuerySingleAsync<MasterViewModel>(qeury, new { Id = id });
                return res;

            }
            catch
            {
                return null;
            }
            finally { await _connection.CloseAsync(); }
        }

        public async Task<IList<MasterViewModel>> SearchAsync(string search, PaginationParams @params)
        {
            //throw new NotImplementedException();
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM public.masters WHERE name ILIKE '%{search}%' ORDER BY id DESC OFFSET {@params.GetSkipCount} LIMIT {@params.PageSize}";
                var master = await _connection.QueryAsync<MasterViewModel>(query);
                return master.ToList();
            }
            catch
            {
                return new List<MasterViewModel>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> SearchCountAsync(string search)
        {
            //throw new NotImplementedException();
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT COUNT(*) FROM public.masters WHERE name ILIKE '%{search}%'";
                var count = await _connection.ExecuteScalarAsync<int>(query);
                return count;
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

        public async Task<int> UpdateAsync(long id, Master entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"UPDATE public.masters" +
                    $"SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, phone_number_confirmed=@PhoneNumberConfirmed, image_path=@ImagePath, password_hash=@PasswordHash, salt=@Salt, is_free=@IsFree, created_at=@CreatedAt, updated_at=@UpdatedAt" +
                    $"WHERE id = {id}";
                var res = await _connection.ExecuteAsync(query, entity);
                return res;


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
}
