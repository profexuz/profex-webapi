using Profex.Application.Exceptions.Masters;
using Profex.Application.Utils;
using Profex.DataAccsess.Common.Helpers;
using Profex.DataAccsess.Interfaces.Masters1;
using Profex.DataAccsess.ViewModels.Masters;
using Profex.Persistance.Dtos.Master1;
using Profex.Service.Interfaces.Common;
using Profex.Service.Interfaces.Master1;

namespace Profex.Service.Services.Master1
{
    public class Master1Service : IMaster1Service
    {
        private readonly IMaster1Repository _repository;
        private readonly IPaginator _paginator;
        private IFileService _fileService;

        public Master1Service(IMaster1Repository master1Repository, IPaginator paginator, IFileService fileService)
        {
            this._repository = master1Repository;
            this._paginator = paginator;
            this._fileService = fileService;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var dbResult = await _repository.DeleteAsync(id);

            return dbResult > 0;
        }

        public async Task<IList<MasterViewModel>> GetAllAsync(PaginationParams @params)
        {
            var masters1 = await _repository.GetAllAsync(@params);
            var count = await _repository.CountAsync();
            _paginator.Paginate(count, @params);

            return masters1;
        }

        public async Task<MasterViewModel> GetByIdAsync(long id)
        {
            var masters1 = await _repository.GetByIdAsync(id);
            if (masters1 is null) throw new MasterNotFoundException();

            return masters1;
        }

        public async Task<bool> UpdateAsync(long id, Master1UpdateDto dto)
        {
            var master1 = await _repository.GetByIdAsync(id);
            if (master1 is null) throw new MasterNotFoundException();
            master1.FirstName = dto.FirstName;
            master1.LastName = dto.LastName;
            master1.PhoneNumber = dto.PhoneNumber;
            master1.PhoneNumberConfirmed = true;
            
            if(dto.ImagePath is not null)
            {
                //var deleteRes = await _fileService.DeleteImageAsync(master1.ImagePath);
                //if (deleteRes is false) throw new BadImageFormatException();
                string newImagePath = await _fileService.UploadImageAsync(dto.ImagePath);
                master1.ImagePath = newImagePath;
            }
            master1.UpdatedAt = TimeHelper.GetDateTime();
            var dbRes = await _repository.UpdateAsync(id, master1);

            return dbRes > 0;
        }
    }
}
