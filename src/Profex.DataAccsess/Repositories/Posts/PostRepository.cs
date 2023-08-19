using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Posts;
using Profex.Domain.Entities.posts;

namespace Profex.DataAccsess.Repositories.Posts
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select count(*) from posts";
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

        public async Task<int> CreateAsync(Post entity)
        {
            try
            {
                await _connection.OpenAsync();

                string query = "INSERT INTO public.posts(category_id, user_id, title, price, description, region, " +
                    "district, longitude, latitude, phone_number, created_at, updated_at)" +
                        "VALUES (@CategoryId, @UserId, @Title, @Price, @Description, @Region, @District, @Longitude, " +
                            "@Latidute, @PhoneNumber, @CreatedAt, @UpdatedAt);";

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
                string query = "DELETE FROM posts WHERE id=@Id";
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

        public async Task<IList<Post>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM public.posts ORDER BY id DESC OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

                var resMas = (await _connection.QueryAsync<Post>(query)).ToList();

                return resMas;
            }
            catch
            {
                return new List<Post>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<Post?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string qeury = $"SELECT * FROM posts where id=@Id";
                var res = await _connection.QuerySingleAsync<Post>(qeury, new { Id = id });

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

        public async Task<IList<Post>> SearchAsync(string search, PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"SELECT * FROM public.posts WHERE title ILIKE '%{search}%' " +
                    $"ORDER BY id DESC OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

                var posts = await _connection.QueryAsync<Post>(query);

                return posts.ToList();



                //string query = $"SELECT * FROM public.posts WHERE title ILIKE '%{search}%' " +
                //    $"ORDER BY id DESC OFFSET {@params.GetSkipCount} LIMIT {@params.PageSize}";

                //var post = await _connection.QueryAsync<Post>(query);

                //return post.ToList();
            }

            catch
            {
                return new List<Post>();
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
                string query = $"SELECT COUNT(*) FROM public.posts WHERE title ILIKE '%{search}%'";
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

        public async Task<int> UpdateAsync(long id, Post entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "UPDATE public.posts SET category_id = @CategoryId, user_id = @UserId, title = @Title, " +
                    "price = @Price, description = @Description, region = @Region, district = @District, " +
                        "longitude = @Longitude, latitude = @Latitude, phone_number = @PhoneNumber, " +
                            "created_at = @CreatedAt, updated_at = @UpdatedAt WHERE id = @Id;";

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
