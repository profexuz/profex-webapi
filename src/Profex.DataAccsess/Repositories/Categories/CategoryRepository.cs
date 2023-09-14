﻿using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.Categories;
using Profex.DataAccsess.ViewModels.Posts;
using Profex.Domain.Entities.Categories;
using Profex.Domain.Entities.posts;
using Profex.Domain.Entities.skills;

namespace Profex.DataAccsess.Repositories.Categories;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from categories";
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

    public async Task<int> CreateAsync(Category entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.categories(name, description, created_at, updated_at) " +
                "VALUES (@Name, @Description, @CreatedAt, @UpdatedAt);";

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
            string query = "DELETE FROM categories WHERE id=@Id";

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

    public async Task<IList<Category>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM categories order by id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Category>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<Category>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<Skill>> GetAllSkillByCategoryId(long categoryId, PaginationParams @params)
    {

        try
        {
            await _connection.OpenAsync();
            string query = "select * from skills where category_id=@categoryId " +
                             $"offset {@params.GetSkipCount()} limit {@params.PageSize}";
            var skills = await _connection.QueryAsync<Skill>(query, new { categoryId = categoryId });

            return skills.ToList();
        }
        catch { return new List<Skill>(); }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<IList<PostViewModel>> GetPostsByCategory(long category, PaginationParams @params)
    {
        try
        {
            string query = "SELECT * FROM posts WHERE category_id = @Category " +
                             $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var parameters = new { Category = category };

            var posts = await _connection.QueryAsync<PostViewModel>(query, parameters);

            return posts.ToList();
        }
        catch { return new List<PostViewModel>(); }
        finally { await _connection.CloseAsync(); }
    }

    public async Task<Category?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM categories where id=@Id";
            var result = await _connection.QuerySingleAsync<Category>(query, new { Id = id });

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





    public async Task<int> UpdateAsync(long id, Category entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"UPDATE public.categories " +
                $"SET name=@Name, description=@Description, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                    $"WHERE id={id};";

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
