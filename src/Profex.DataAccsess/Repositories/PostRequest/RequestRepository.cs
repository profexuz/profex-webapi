using Dapper;
using Profex.Application.Utils;
using Profex.DataAccsess.Interfaces.PostRequests;
using Profex.DataAccsess.ViewModels.Posts;
using Profex.Domain.Entities.post_images;
using Profex.Domain.Entities.postRequests;

namespace Profex.DataAccsess.Repositories.PostRequest;

internal class RequestRepository : BaseRepository, IRequestRepository
{
    public Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }

    public Task<long> CountPostRequestMasterCheck(long masterId, long postId, long userId)
    {
        throw new NotImplementedException();
    }

    public Task<long> CountPostRequestUserPost(long postId)
    {
        throw new NotImplementedException();
    }

    public Task<int> CreateAsync(Request entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Request>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<Request?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Request>> GetPostRequestsAsync(long userId, PaginationParams @params)
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

    public Task<int> UpdateAsync(long id, Request entity)
    {
        throw new NotImplementedException();
    }
}
