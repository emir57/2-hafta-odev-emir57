using _3_hafta.Business.Abstract;
using _3_hafta.Business.Validation.FluentValidation;
using _3_hafta.DataAccess.Abstract;
using _3_hafta.Dto.Concrete;
using _3_hafta.Entity.Concrete;
using AutoMapper;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result;

namespace _3_hafta.Business.Concrete
{
    public class DepartmentManager : BaseManager<Department, DepartmentDto>, IDepartmentService
    {
        public DepartmentManager(IDepartmentDal entityRepository, IMapper mapper) : base(entityRepository, mapper)
        {
        }
        [ValidationAspect(typeof(DepartmentValidator))]
        public override Task<IResult> AddAsync(DepartmentDto entity)
        {
            return base.AddAsync(entity);
        }
        [ValidationAspect(typeof(DepartmentValidator))]
        public override Task<IResult> UpdateAsync(int id, DepartmentDto entity)
        {
            return base.UpdateAsync(id, entity);
        }
    }
}
