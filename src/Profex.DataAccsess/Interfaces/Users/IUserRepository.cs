using Profex.Application.Utils;
using Profex.DataAccsess.Common;
using Profex.DataAccsess.ViewModels.Users;
using Profex.Domain.Entities.users;

namespace Profex.DataAccsess.Interfaces.Users;

public interface IUserRepository : IRepository<User, UserViewModel>, IGetAll<UserViewModel>, ISearchable<UserViewModel>
{
    public Task<User?> GetByPhoneAsync(string phone);
    Task<int> UpdateAsync(long id, UserViewModel user);
    public Task<IList<UserViewModel>> SearchUserAsync(string search, PaginationParams @params);
    public Task<int> SearchCountAsync(string search);
    public Task<IList<User1ViewModel>> GetALlPostByUserId(long userId);


}



