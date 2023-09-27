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
    private IFileService _fileService;

    public UserService(IUserRepository repository,
        IPaginator paginator, IFileService fileService)
    {
        this._repository = repository;
        this._paginator = paginator;
        this._fileService = fileService;
    }
    public async Task<long> CountAsync()
    {
        var db = await _repository.CountAsync();
        return db;
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
        var js = await _repository.GetByIdAsync(id);
        if (js == null) throw new UserNotFoundException();
        var dbResult = await _repository.DeleteAsync(id);

        return dbResult > 0;
    }


    public async Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params)
    {
        var users1 = await _repository.GetAllAsync(@params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return users1;
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        var users = await _repository.GetByIdAsync(id);
        if (users is null) throw new UserNotFoundException();

        return users;
    }
    public async Task<IList<UserViewModel>> SearchUserAsync(string search, PaginationParams @params)
    {
        var SearchedUsers = await _repository.SearchUserAsync(search, @params);
        var count = await _repository.CountAsync();
        _paginator.Paginate(count, @params);

        return SearchedUsers;
    }
    public async Task<bool> UpdateAsync(long id, UserUpdateDto dto)
    {
        var user1 = await _repository.GetByIdAsync(id);
        if (user1 is null) throw new UserNotFoundException();
        user1.FirstName = dto.FirstName;
        user1.LastName = dto.LastName;
        user1.PhoneNumber = dto.PhoneNumber;
        user1.PhoneNumberConfirmed = true;

        if (dto.ImagePath is not null && user1.ImagePath == "media/avatarmaster/admin.jpg")
        {
            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);
            user1.ImagePath = newImagePath;
        }
        else if (user1.ImagePath is not null && dto.ImagePath is not null)
        {
            await _fileService.DeleteImageAsync(user1.ImagePath);
            string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath!);
            user1.ImagePath = newImagePath;
        }
        user1.UpdatedAt = TimeHelper.GetDateTime();
        var dbRes = await _repository.UpdateAsync(id, user1);

        return dbRes > 0;
    }


}
