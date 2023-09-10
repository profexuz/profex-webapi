using Profex.Application.Utils;
using Profex.Domain.Entities.post_images;
using Profex.Persistance.Dtos.PostImages;

namespace Profex.Service.Interfaces.PostImages
{
    public interface IPostImagesService
    {
        public Task<bool> CreateAsync(PostImageCreateDto dto);
        public Task<bool> DeleteAsync(long id);
        public Task<IList<Post_image>> GetAllAsync(PaginationParams @params);
        public Task<IList<Post_image>> GetByPostIdAsync(long id);
        public Task<Post_image> GetByIdAsync(long id);
        public Task<bool> UpdateAsync(long id, PostImageCreateDto dto);
    }
}
