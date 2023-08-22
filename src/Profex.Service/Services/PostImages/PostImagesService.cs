using Profex.Application.Exceptions.MasterSkills;
using Profex.Application.Exceptions.PostImages;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Post_Images;
using Profex.Domain.Entities.master_skills;
using Profex.Domain.Entities.masters;
using Profex.Domain.Entities.post_images;
using Profex.Persistance.Dtos.PostImages;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.PostImages;
using Profex.Service.Services.Common;

namespace Profex.Service.Services.PostImages
{
    public class PostImagesService : IPostImagesService
    {
        private readonly IPostImageRepository _repository;
        private readonly IPaginator _paginator;
        private IFileService _fileService;
        public PostImagesService(IPostImageRepository repository,
            IPaginator paginator, IFileService fileService)
        {
            this._repository = repository;
            this._paginator = paginator;
            _fileService = fileService;

        }
        public async Task<bool> CreateAsync(PostImageCreateDto dto)
        {
            string imagepath = await _fileService.UploadImageAsync(dto.ImagePath);
            Post_image ps = new Post_image()
            {
                PostId = dto.PostId,
                ImagePath = imagepath,
                CreatedAt = TimeHelper.GetDateTime(),
                UpdatedAt = TimeHelper.GetDateTime()
            };
            var res = await _repository.CreateAsync(ps);

            return res > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
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

        public async Task<Post_image> GetByIdAsync(long id)
        {
            var ms = await _repository.GetByIdAsync(id);
            if (ms is null) throw new PostImageNotFoundException();

            return ms;

        }

        public async Task<bool> UpdateAsync(long id, PostImageUpdateDto dto)
        {
            var ms = await _repository.GetByIdAsync(id);
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
