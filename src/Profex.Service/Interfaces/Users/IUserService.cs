using Profex.Application.Utils;
using Profex.DataAccsess.ViewModels.Users;
using Profex.Persistance.Dtos.Users;

namespace Profex.Service.Interfaces.Users;

public interface IUserService
{
    public Task<bool> CreateAsync(UserCreateDto dto);
    public Task<bool> DeleteAsync(long id);
    public Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params);
    public Task<UserViewModel> GetByIdAsync(long id);
    public Task<bool> UpdateAsync(long id, UserUpdateDto dto);
    public Task<long> CountAsync();
    public Task<IList<UserViewModel>> SearchUserAsync(string search, PaginationParams @params);
}
