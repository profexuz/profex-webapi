using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Post_Images;
using Profex.Domain.Entities.post_images;

namespace Profex.DataAccsess.Repositories.Post_images
{
    public class PostImageRepository : BaseRepository, IPostImageRepository
    {
        public async Task<long> CountAsync()
        {
            //throw new NotImplementedException();
            try
            {
                await _connection.OpenAsync();
                string query = $"select count(*) from post_images";
                var result = await _connection.QuerySingleAsync<long>(query);

                return result;
            }
            catch { return 0; }
            finally 
            { 
                await _connection.CloseAsync(); 
            }
        }


        public async Task<int> CreateAsync(Post_image entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.post_images(post_id, image_path, created_at, updated_at)" +
                    "VALUES (@PostId, @ImagePath, @CreatedAt, @UpdatedAt);";
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
                string query = "DELETE FROM post_images WHERE id=@Id";
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

        public async Task<IList<Post_image>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM public.post_images ORDER BY id desc offset {@params.GetSkipCount()} " +
                    $"limit {@params.PageSize}";
                var result = (await _connection.QueryAsync<Post_image>(query)).ToList();

                return result;
            }
            catch
            {
                return new List<Post_image>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<Post_image?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string qeury = $"SELECT * FROM post_images where id=@Id";
                var result = await _connection.QuerySingleAsync<Post_image>(qeury, new { Id = id });

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

        public async Task<IList<Post_image>> GetByPostIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM public.post_images WHERE post_id = {id} ";
                var result = (await _connection.QueryAsync<Post_image>(query)).ToList();

                return result;
            }
            catch
            {
                return new List<Post_image>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> UpdateAsync(long id, Post_image entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"UPDATE public.post_images " +
                    $" SET post_id=@PostId, image_path=@ImagePath, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                        $" WHERE id = {id};";
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
