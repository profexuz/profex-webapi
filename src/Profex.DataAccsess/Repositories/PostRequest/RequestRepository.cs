using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.PostRequests;
using Profex.DataAccsess.ViewModels;
using Profex.DataAccsess.ViewModels.Posts;
using Profex.Domain.Entities.masters;
using Profex.Domain.Entities.post_images;
using Profex.Domain.Entities.postRequests;

namespace Profex.DataAccsess.Repositories.PostRequest;

public class RequestRepository : BaseRepository, IRequestRepository
{
    public async Task<bool> AcceptRequest(long masterId, long postId, long userId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $@"UPDATE public.requests
	                    SET  is_accepted = true, updated_at = now()
	                    WHERE master_id={masterId} and post_id={postId} and user_id={userId} ";
            var result = await _connection.ExecuteAsync(query);
            
            return result > 0;
        }
        catch
        {
            return false;
        }
        finally 
        { 
            _connection.Close(); 
        }
    }

    public async Task<int> CheckPostExixts(long postId, long userId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = @$" SELECT COUNT(*)
                            FROM public.requests
                            WHERE  post_id = {postId} AND user_id = {userId} ;";
            var result = await _connection.QuerySingleAsync<int>(query);

            return result;
        }
        catch
        {
            return -1;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CountPostRequestMasterCheck(long masterId, long postId, long userId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = @$" SELECT COUNT(*)
                            FROM public.requests
                            WHERE master_id = {masterId} AND post_id = {postId} AND user_id = {userId} ;";

            var result = await _connection.QuerySingleAsync<int>(query);
            
            return result;
        }
        catch
        {
            return -1;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<long> CountPostRequestUserPost(long postId)
    {
        throw new NotImplementedException();
    }

    

    public async Task<IList<RequestViewModel>> GetUserAllPostWithRequestAsync(long userId, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.requests WHERE user_id = {userId} " +
                $" ORDER BY id DESC OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

            query = $@" SELECT post_id,  user_id, 
                        ARRAY_AGG(master_id) AS masters_id 
                        FROM requests WHERE  user_id = {userId} 
                        GROUP BY   post_id, user_id ";
                             

            var result = (await _connection.QueryAsync<RequestViewModel>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<RequestViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }

    }

    public async Task<IList<Request>> GetUserPostWithRequestAsync(long userId, long postId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.requests WHERE user_id = {userId} and post_id = {postId} ";
            var result = (await _connection.QueryAsync<Request>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<Request>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<long> RequestToPost(Request entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = @"INSERT INTO public.requests(
	                         master_id, post_id, user_id, is_accepted, created_at, updated_at)
	                        VALUES ( @MasterId, @PostId, @UserId,@IsAccepted, @CreatedAt, @UpdatedAt) ;";

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
