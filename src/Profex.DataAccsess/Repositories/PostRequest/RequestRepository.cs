using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.PostRequests;
using Profex.DataAccsess.ViewModels;
using Profex.Domain.Entities.postRequests;
using Profex.Domain.Entities.users;

namespace Profex.DataAccsess.Repositories.PostRequest;

public class RequestRepository : BaseRepository, IRequestRepository
{
    public async Task<bool> AcceptRequest(long userId,long masterId, long postId )
    {
        try
        {
            await _connection.OpenAsync();
            string query = $@"UPDATE public.requests
	                    SET  is_accepted = true
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

    public async Task<long> DeleteRequestAsync(long masterId, long postId, long user_id)
    {
        try
        { 
            await _connection.OpenAsync();
            string query = $"DELETE FROM public.requests WHERE master_id = {masterId} and post_id = {postId} and user_id = {user_id}";
            
            var result = await _connection.ExecuteAsync(query);
            
            return (long)result;
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

    public async Task<IList<Request>> GetMasterRequestedAllPostsAsync(long masterId, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $" SELECT * FROM public.requests WHERE master_id = {masterId} " +
                $" ORDER BY id DESC OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

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

    public async Task<IList<Request>> GetUserAllPostWithRequestAsync(long userId, PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.requests WHERE user_id = {userId} " +
                $" ORDER BY id DESC OFFSET {@params.GetSkipCount()} LIMIT {@params.PageSize}";

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
