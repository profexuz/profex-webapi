using Profex.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profex.DataAccsess.Common
{
    public interface IGetAll<TModel>
    {
        public Task<IList<TModel>> GetAllAsync(PaginationParams @params);
    }
}
