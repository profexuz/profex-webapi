using Profex.Application.Utils;
using Profex.DataAccsess.ViewModels.Users;
using Profex.Domain.Entities.post_images;
using Profex.Persistance.Dtos.PostImages;
using Profex.Persistance.Dtos.Users;

namespace Profex.Service.Interfaces.PostImages
{
    public interface IPostImagesService
    {
        public Task<bool> CreateAsync(PostImageCreateDto dto);
        public Task<bool> DeleteAsync(long id);
        public Task<IList<Post_image>> GetAllAsync(PaginationParams @params);
        public Task<Post_image> GetByIdAsync(long id);
        public Task<bool> UpdateAsync(long id, PostImageUpdateDto dto);
    }
}
