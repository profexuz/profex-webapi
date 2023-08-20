using Profex.Application.Utils;
using Profex.DataAccsess.ViewModels.Users;
using Profex.Persistance.Dtos.User1;

namespace Profex.Service.Interfaces.User1;

public interface IUser1Service
{

    public Task<long> CountAsync();
    public Task<bool> DeleteAsync(long id);
    public Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params);
    public Task<UserViewModel> GetByIdAsync(long id);
    public Task<bool> UpdateAsync(long id, User1UpateDto dto);


}
