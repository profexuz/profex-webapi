using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Posts;
using Profex.DataAccsess.ViewModels.Posts;
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
                    "district, longitude, latitude, phone_number, created_at, updated_at, status, address, jobtime)" +
                        "VALUES (@CategoryId, @UserId, @Title, @Price, @Description, @Region, @District, @Longitude, " +
                            "@Latidute, @PhoneNumber, @CreatedAt, @UpdatedAt, @Status, @Address, @JobTime);";

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

        public async Task<IList<PostViewModel>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $@"SELECT p.id, p.category_id, p.user_id, p.title, p.price,
                                         p.description, p.region, p.district, p.longitude,
                                         p.latitude, p.phone_number, p.created_at, p.updated_at,
                                    ARRAY_AGG(DISTINCT pi.image_path) AS image_path,
                                    u.first_name,
                                    u.last_name,
                                    c.name AS category_name,
                                    p.status,
                                    p.jobtime,
                                    p.address
                                FROM
                                    posts p
                                LEFT JOIN
                                    post_images pi ON p.id = pi.post_id
                                LEFT JOIN
                                    users u ON p.user_id = u.id
                                LEFT JOIN
                                    categories c ON p.category_id = c.id
                                WHERE
                                    pi.image_path IS NULL OR pi.image_path != ''
                                GROUP BY
                                    p.id, u.id, c.id " +

                    $"ORDER BY id DESC OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize};";

                var result = (await _connection.QueryAsync<PostViewModel>(query)).ToList();


                /*string query = $@"SELECT p.id, p.category_id, p.user_id, p.title, p.price,
                             p.description, p.region, p.district, p.longitude,
                             p.latitude, p.phone_number, p.created_at, p.updated_at,
                        ARRAY_AGG(DISTINCT pi.image_path) AS image_path,
                        u.first_name,
                        u.last_name,
                        c.name AS category_name,
                        p.status,
                        p.jobtime,
                        p.address
                    FROM
                        posts p
                    LEFT JOIN
                        post_images pi ON p.id = pi.post_id
                    LEFT JOIN
                        users u ON p.user_id = u.id
                    LEFT JOIN
                        categories c ON p.category_id = c.id
                    WHERE
                        (pi.image_path IS NULL OR pi.image_path != '') AND
                        p.created_at > (SELECT MIN(created_at) FROM posts)
                    GROUP BY
                        p.id, u.id, c.id " +

        $" ORDER BY p.created_at DESC OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize};";

                var result = (await _connection.QueryAsync<PostViewModel>(query)).ToList();
*/
                return result;

            }
            catch
            {
                return new List<PostViewModel>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IList<Post>> GetUserAllPost(PaginationParams @params,long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "SELECT * FROM posts where user_id=@Id";
                var res = await _connection.QueryAsync<Post>(query, new { Id = id });
                
                return (IList<Post>)res;
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

        public async Task<Post?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();

                string query = $"SELECT * FROM posts WHERE id = @Id";
                var res = await _connection.QueryAsync<Post>(query, new { Id = id });
                var post = res.SingleOrDefault();

                return post;

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

        public async Task<PostViewModel> GetByIdJoin(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $@"WITH aggregated_data AS (SELECT p.id,  p.category_id,   p.user_id,  p.title,
                                                           p.price,   p.description,  p.region,   p.district, 
                                                           p.longitude,  p.latitude,  p.phone_number,   p.created_at, p.updated_at,
                                 ARRAY_AGG(DISTINCT pi.image_path) AS image_path,         u.first_name,     u.last_name,                          
                                c.name AS category_name,  p.status,  p.jobtime, p.address  FROM  posts p   LEFT JOIN
                                                                        post_images pi ON p.id = pi.post_id
                                                                        LEFT JOIN
                                                                        users u ON p.user_id = u.id
                                                                        LEFT JOIN
                                                                        categories c ON p.category_id = c.id
                                                            WHERE      
                                                                pi.image_path IS NULL OR pi.image_path != ''
                                                            GROUP BY  p.id, u.id, c.id  )
                                                        SELECT *  FROM aggregated_data
                                                        WHERE id = @Id";

               // query = $"SELECT * FROM posts WHERE id = @Id";
                var result = await _connection.QuerySingleAsync<PostViewModel>(query, new { Id = id });

                return result;
            }
            catch { return null; }
            finally { await _connection.CloseAsync(); }
        }

        public async Task<IList<PostViewModel>> SearchAsync(string search, PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT    p.id,    p.category_id,    p.user_id,    p.title,    p.price,    " +
                    $"p.description,    p.region,    p.district,    p.longitude,    p.latitude,    p.phone_number,   " +
                        $"p.created_at,    p.updated_at,    Array_agg(pi.image_path) as image_path,    u.first_name,    u.last_name,   " +
                            $"c.name AS category_name,    Array_agg(s.title) AS skill_title " +
                                $"FROM    posts p LEFT JOIN    post_images pi ON p.id = pi.post_id LEFT JOIN     " +
                                    $"users u ON p.user_id = u.id LEFT JOIN     categories c ON p.category_id = c.id LEFT JOIN    " +
                                        $" skills s ON p.category_id = s.category_id  WHERE  p.title ILIKE '%{search}%' AND (pi.image_path IS NULL OR pi.image_path != '') " +
                                            $"GROUP BY p.id, u.id, c.id, s.id ";
               
                query = $@"SELECT   p.id, p.category_id, p.user_id,   p.title, p.price, p.description, p.region, p.district,
                            p.longitude,  p.latitude,    p.phone_number,  p.created_at,  p.updated_at,    
                             ARRAY_AGG(DISTINCT pi.image_path) AS image_path,  u.first_name,  u.last_name,        
                            c.name AS category_name FROM  posts p
                                  LEFT JOIN   post_images pi ON p.id = pi.post_id
                                  LEFT JOIN   users u ON p.user_id = u.id 
                                  LEFT JOIN   categories c ON p.category_id = c.id
                                  WHERE     (pi.image_path IS NULL OR pi.image_path != '')
                                  AND p.title ILIKE '{search}%'  
                                   GROUP BY    p.id,   u.id,  c.id " +  
                                  $" ORDER BY p.id DESC OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize};";  
                var posts = await _connection.QueryAsync<PostViewModel>(query);
                return (IList<PostViewModel>)posts;
            }
            catch
            {
                return new List<PostViewModel>();
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
                            "created_at = @CreatedAt, updated_at = @UpdatedAt, status = @Status, jobtime = @JobTime, address = @Address WHERE id = @Id;";

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

 

        public async Task<IList<PostViewModel>> GetUserAllPostAsync(long id, PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $@"SELECT p.id, p.category_id, p.user_id, p.title,
                                        p.price, p.description, p.region, p.district,
                                        p.longitude, p.latitude, p.phone_number, p.created_at,
                                         p.updated_at, ARRAY_AGG(DISTINCT pi.image_path) AS image_path,
                                        u.first_name, u.last_name, c.name AS category_name
                                       
                                    FROM
                                        posts p
                                    LEFT JOIN
                                        post_images pi ON p.id = pi.post_id
                                    LEFT JOIN
                                        users u ON p.user_id = u.id
                                    LEFT JOIN
                                        categories c ON p.category_id = c.id
                                    WHERE
                                        (pi.image_path IS NULL OR pi.image_path != '')
                                        AND p.user_id = {id}
                                    GROUP BY
                                        p.id, u.id, c.id " +
                                    

                    $" OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize};";

                var result = await _connection.QueryAsync<PostViewModel>(query);

                return result.ToList();

            }
            catch
            {
                return new List<PostViewModel>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }
    }
}
