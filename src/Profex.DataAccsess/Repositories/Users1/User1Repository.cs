using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Users1;
using Profex.DataAccsess.ViewModels.Users;
using Profex.Domain.Entities.users;

namespace Profex.DataAccsess.Repositories.Users1
{
    public class User1Repository : BaseRepository, IUser1Repository
    {

        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select count(*) from users";
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

        public Task<int> CreateAsync(User entity)
        {
            throw new NotImplementedException();
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

                //string query = $"SELECT * FROM public.users ORDER BY id desc offset {@params.GetSkipCount()} " +
                //    $"limit {@params.PageSize}";
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
                //return (IList<UserViewModel>)resUser;
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

        public async Task<IList<UserViewModel>> SearchAsync(string search, PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"SELECT * FROM public.users WHERE first_name ILIKE '%{search}%' " +
                    $"ORDER BY id DESC OFFSET {@params.GetSkipCount} LIMIT {@params.PageSize}";

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
                       $"phone_number_confirmed=@PhoneNumberConfirmed, image_path=@ImagePath, " +
                            $"updated_at=@UpdatedAt " +
                                $"WHERE id = @Id";

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

        public async Task<int> UpdateAsync(long id, User entity)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"UPDATE public.users" +
                    $"SET first_name=@FirstName, last_name=@LastName, phone_number=@PhoneNumber, " +
                        $"phone_number_confirmed=@PhoneNumberConfirmed, image_path=@ImagePath, password_hash=@PasswordHash, " +
                            $"salt=@Salt, is_free=@IsFree, created_at=@CreatedAt, updated_at=@UpdatedAt" +
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
