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
        private readonly IEmployeeService _employeeService;
        public DepartmentManager(IDepartmentDal entityRepository, IMapper mapper, IEmployeeService employeeService) : base(entityRepository, mapper)
        {
            _employeeService = employeeService;
        }
        [ValidationAspect(typeof(DepartmentValidator))]
        public override Task<IResult> AddAsync(DepartmentDto entity)
        {
            return base.AddAsync(entity);
        }

        public async Task<IDataResult<List<DepartmentDto>>> GetDepartmentsAsync(int employeeId)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            var departments = await _entityRepository.GetAllAsync();
            List<DepartmentDto> result = Mapper.Map<List<DepartmentDto>>(departments.Where(x => x.DepartmentId == employee.Data.DeptId).ToList());
            return new SuccessDataResult<List<DepartmentDto>>(result);
        }

        [ValidationAspect(typeof(DepartmentValidator))]
        public override Task<IResult> UpdateAsync(int id, DepartmentDto entity)
        {
            return base.UpdateAsync(id, entity);
        }
    }
}
