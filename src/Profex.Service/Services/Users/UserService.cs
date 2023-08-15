using Profex.Application.Exceptions.Users;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Users;
using Profex.DataAccsess.ViewModels.Users;
using Profex.Domain.Entities.users;
using Profex.Persistance.Dtos.Users;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.Users;

namespace Profex.Service.Services.Users;

public class UserService : IUserService
{     
    private readonly IUserRepository _repository;
    private readonly IPaginator _paginator;

    public UserService(IUserRepository repository,
        IPaginator paginator)
    {
        this._repository = repository;
        this._paginator = paginator;
    }
    public async Task<bool> CreateAsync(UserCreateDto dto)
    {
        User user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            PhoneNumberConfirmed = dto.PhoneNumberConfirmed,
            ImagePath = dto.ImagePath.FileName,
            PasswordHash = dto.PasswordHash,
            Salt = dto.Salt,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };
        var res = await _repository.CreateAsync(user);
        return res > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var dbResult = await _repository.DeleteAsync(id);

        return dbResult > 0;
    }

    public async Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params)
    {
        var users = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return users;
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        var users = await _repository.GetByIdAsync(id);
        if (users is null) throw new UserNotFoundException();

        return users;
    }

    public async Task<bool> UpdateAsync(long id, UserUpdateDto dto)
    {
        var userss = await _repository.GetByIdAsync(id);
        if (userss is null) throw new UserNotFoundException();
        userss.FirstName = dto.FirstName;
        userss.LastName = dto.LastName;
        userss.PhoneNumber = dto.PhoneNumber;
        userss.PhoneNumberConfirmed = dto.PhoneNumberConfirmed;
        userss.ImagePath = dto.ImagePath.FileName;
        userss.UpdatedAt = TimeHelper.GetDateTime();
        var dbRes = await _repository.UpdateAsync(id, userss);

        return dbRes > 0;
    }
}
