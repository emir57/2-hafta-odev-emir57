using _3_hafta.Business.Abstract;
using _3_hafta.DataAccess.Abstract;
using _3_hafta.Dto.Concrete;
using _3_hafta.Entity.Concrete;
using AutoMapper;

namespace _3_hafta.Business.Concrete
{
    public class DepartmentManager : BaseManager<Department, DepartmentDto>, IDepartmentService
    {
        public DepartmentManager(IDepartmentDal entityRepository, IMapper mapper) : base(entityRepository, mapper)
        {
        }
    }
}
