using Profex.Application.Exceptions.MasterSkills;
using Profex.Application.Exceptions.PostImages;
using Profex.Application.Exceptions.Posts;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Post_Images;
using Profex.DataAccsess.Interfaces.Posts;
using Profex.DataAccsess.Interfaces.Users1;
using Profex.Domain.Entities.post_images;
using Profex.Persistance.Dtos.PostImages;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.Identity;
using Profex.Service.Interfaces.PostImages;

namespace Profex.Service.Services.PostImages
{
    public class PostImagesService : IPostImagesService
    {
        private readonly IPostImageRepository _repository;
        private readonly IPaginator _paginator;
        private readonly IPostRepository _post;
        private readonly IIdentityService _identity;
        private readonly IUser1Repository _user;
        private IFileService _fileService;
        public PostImagesService(IPostImageRepository repository,
                                 IPaginator paginator, IFileService fileService, 
                                 IPostRepository post, IIdentityService identity,
                                 IUser1Repository user)
                                 
        {
            this._repository = repository;
            this._paginator = paginator;
            _fileService = fileService;
            this._post = post;
            this._identity = identity;
            this._user = user;

        }
        public async Task<bool> CreateAsync(PostImageCreateDto dto)
        {
            var countImages = await _repository.CountPostImagesAsync(dto.PostId);
            if (countImages < 5)
            {
                string imagepath = await _fileService.UploadImageAsync(dto.ImagePath);
                Post_image ps = new Post_image()
                {
                    PostId = dto.PostId,
                    ImagePath = imagepath,
                    CreatedAt = TimeHelper.GetDateTime(),
                    UpdatedAt = TimeHelper.GetDateTime()
                };
                ps.PostId = dto.PostId;
                var natija = await _post.GetByIdAsync(ps.PostId);
                if (natija == null) throw new PostNotFoundException();
                var res = await _repository.CreateAsync(ps);
                return res > 0;
            }
            else
            {
                throw new PostImageLimitException();
            }

        }

        public async Task<bool> DeleteAsync(long id)
        {
            var rp =await _repository.GetByIdAsync(id);
            var post = await _post.GetByIdAsync(rp.PostId);
            var user = await _user.GetByIdAsync(post.UserId);
            if (_identity.UserId != user.Id)
            {
                throw new UnauthorizedAccessException();
            }
            if (rp == null) throw new PostImageNotFoundException();
            var dbResult = await _repository.DeleteAsync(id);

            return dbResult > 0;

        }

        public async Task<IList<Post_image>> GetAllAsync(PaginationParams @params)
        {
            var mss = await _repository.GetAllAsync(@params);
            var count = await _repository.CountAsync();
            _paginator.Paginate(count, @params);

            return mss;
        }
        public async Task<IList<Post_image>> GetByPostIdAsync(long id)
        {
            var mss = await _repository.GetByPostIdAsync(id);
            if (mss is null) throw new PostImageNotFoundException();
            
            else  return mss;
        }

        public async Task<Post_image> GetByIdAsync(long id)
        {
            var ms = await _repository.GetByIdAsync(id);
            if (ms is null) throw new PostImageNotFoundException();

            return ms;

        }

        public async Task<bool> UpdateAsync(long id, PostImageCreateDto dto)
        {
            var ms = await _repository.GetByIdAsync(id);
            var post = await _post.GetByIdAsync(ms.PostId);
            var user = await _user.GetByIdAsync(post.UserId);
            if (_identity.UserId != user.Id) 
            {
                throw new UnauthorizedAccessException();
            }
            if (ms is null) throw new MasterSkilNotFoundException();
            ms.PostId = dto.PostId;
            if (dto.ImagePath is not null)
            {
                string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);
                ms.ImagePath = newImagePath;
            }
            ms.UpdatedAt = TimeHelper.GetDateTime();
            var dbRes = await _repository.UpdateAsync(id, ms);

            return dbRes > 0;
        }
    }
}
