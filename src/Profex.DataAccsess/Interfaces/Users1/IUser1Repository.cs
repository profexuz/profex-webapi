using Profex.Application.Utils;
using Profex.DataAccsess.Common;
using Profex.DataAccsess.ViewModels.Users;
using Profex.Domain.Entities.users;

namespace Profex.DataAccsess.Interfaces.Users1
{
    public interface IUser1Repository : IRepository<User, UserViewModel>, IGetAll<UserViewModel>, ISearchable<UserViewModel>
    {
        public Task<IList<UserViewModel>> SearchAsync(string search, PaginationParams @params);
        public Task<int> SearchCountAsync(string search);
        public Task<User?> GetByPhoneAsync(string phone);
        public Task<int> UpdateAsync(long id, UserViewModel users);
        public Task<IList<User1ViewModel>> GetALlPostByUserId(long userId);
    }
}
