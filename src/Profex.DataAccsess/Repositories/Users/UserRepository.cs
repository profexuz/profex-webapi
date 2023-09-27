using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Users;
using Profex.DataAccsess.ViewModels.Users;
using Profex.Domain.Entities.users;
using static Dapper.SqlMapper;

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

                string query = "INSERT INTO public.users(first_name, last_name, phone_number, phone_numer_confirmed, " +
                    "image_path, password_hash, salt, created_at, updated_at)" +
                        "VALUES (@FirstName, @LastName, @PhoneNumber, @PhoneNumberConfirmed, @ImagePath, @PasswordHash, " +
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
                string query = "DELETE FROM users WHERE id=@Id";
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

        public async Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"SELECT * FROM public.users WHERE LENGTH(last_name) > 0 ORDER BY id DESC OFFSET {@params.GetSkipCount()} " +
                    $"LIMIT {@params.PageSize}";

                var resUser = (await _connection.QueryAsync<User>(query)).ToList();

                var userViewModels = resUser.Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    ImagePath = user.ImagePath,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                }).ToList();

                return userViewModels;
            }
            catch
            {
                return new List<UserViewModel>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IList<User1ViewModel>> GetALlPostByUserId(long userId)
        {
            throw new NotImplementedException();
            //try
            //{
            //    await _connection.OpenAsync();
            //    string query 
            //}
        }

        public async Task<UserViewModel?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string qeury = $"SELECT * FROM users where id=@Id";
                var result = await _connection.QuerySingleAsync<UserViewModel>(qeury, new { Id = id });

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

        public async Task<User?> GetByPhoneAsync(string phone)
        {
            try
            {
                await _connection.OpenAsync();

                string query = "SELECT * FROM public.users WHERE phone_number=@PhoneNumber;";
                var result = await _connection.QueryFirstOrDefaultAsync<User>(query, new { PhoneNumber = phone });

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }


        public async Task<IList<UserViewModel>> SearchUserAsync(string search, PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"SELECT * FROM public.users WHERE first_name ILIKE '%{search}%' OR last_name ILIKE '%{search}%' " +
                  $"  ORDER BY id DESC OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";


                //   query =     $"SELECT * FROM public.masters WHERE first_name ILIKE '%{search}%' " +
                //    $"ORDER BY id DESC OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";



                var user = await _connection.QueryAsync<UserViewModel>(query);

                return user.ToList();
            }
            catch
            {
                return new List<UserViewModel>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

       

        public async Task<int> SearchCountAsync(string search)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT COUNT(*) FROM public.users WHERE first_name ILIKE '%{search}%'";
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
        public async Task<int> UpdateAsync(long id, UserViewModel users)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"UPDATE public.users " +
                   $"SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, " +
                       $"phone_numer_confirmed=@PhoneNumberConfirmed, image_path=@ImagePath, " +
                            $"updated_at=@UpdatedAt " +
                                $"WHERE id = @Id";
                // string query = "";
                var parameters = new User()
                {
                    Id = id,
                    FirstName = users.FirstName,
                    LastName = users.LastName,
                    PhoneNumber = users.PhoneNumber,
                    PhoneNumberConfirmed = users.PhoneNumberConfirmed,
                    ImagePath = users.ImagePath,
                    UpdatedAt = users.UpdatedAt,
                };

                var res = await _connection.ExecuteAsync(query, parameters);

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
        
        public Task<int> UpdateAsync(long id, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
