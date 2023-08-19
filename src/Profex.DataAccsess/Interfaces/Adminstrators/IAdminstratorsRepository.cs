using Profex.DataAccsess.Common;
using Profex.DataAccsess.ViewModels.Adminstrators;
using Profex.Domain.Entities.adminstrators;

namespace Profex.DataAccsess.Interfaces.Adminstrators;

public interface IAdminstratorsRepository : IRepository<Adminstrator, AdminstratorsViewModel>, IGetAll<AdminstratorsViewModel>
{ }
