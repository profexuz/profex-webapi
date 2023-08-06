using Profex.DataAccsess.Common;
using Profex.Domain.Entities.users;

namespace Profex.DataAccsess.Interfaces.Users;

public interface IUserRepository : IRepository<User, User>, IGetAll<User>
{

}
