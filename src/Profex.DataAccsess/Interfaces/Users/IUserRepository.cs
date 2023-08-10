using Profex.DataAccsess.Common;
using Profex.DataAccsess.ViewModels.Users;
using Profex.Domain.Entities.users;

namespace Profex.DataAccsess.Interfaces.Users;

public interface IUserRepository : IRepository<User, UserViewModel>, IGetAll<UserViewModel>, ISearchable<UserViewModel>
{
    public Task<User?> GetByPhoneAsync(string phone);  
}



