using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Users;
using Profex.Domain.Entities.users;

namespace Profex.DataAccsess.Repositories.Users
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public async Task<long> CountAsync()
        {            
            try
            {
                await _connection.OpenAsync();
                string query = $"select count(*) from users";
                var res = await _connection.QuerySingleAsync<long>(query);
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

        public async Task<int> CreateAsync(User entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.users(first_name, last_name, phone_number, phone_number_confirmed, image_path, password_hash, salt, created_at, updated_at)" +
                    "VALUES (@FirstName, @LastName, @PhoneNumber, @PhoneNumberConfirmed, @Image_path, @PasswordHash, @Salt, @CreatedAt, @UpdatedAt);";
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
                string query = $"delete from users where id = {id}";
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

        public Task<IList<User>> GetAllAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByPhoneAsync(string phone)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "SELECT * FROM users phone_number= @PhoneNumber";
                var data = await _connection.QuerySingleAsync<User>(query, new { PhoneNumber = phone });
                return data;
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
        public async Task<int> UpdateAsync(long id, User entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"UPDATE public.users" +
                    $"SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, phone_number_confirmed=@PhoneNumberConfirmed, image_path=@ImagePath, password_hash=@PasswordHash, salt=@Salt, created_at=@CreatedAt, updated_at=@UpdatedAt" +
                    $"WHERE id = {id};";
                var res = await _connection.ExecuteAsync(query,entity);
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
