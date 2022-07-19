using Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_hafta.Business.Abstract
{
    public interface IBaseService<TEntity, TDto>
    {
        Task<IResult> AddAsync(TEntity entity);
        Task<IResult> UpdateAsync(TEntity entity);
        Task<IResult> DeleteAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        Task<IDataResult<List<TDto>>> GetListAsync();
        Task<IDataResult<TDto>> GetByIdAsync(int id);
    }
}
