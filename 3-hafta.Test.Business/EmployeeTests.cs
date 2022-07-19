using _3_hafta.Business.Abstract;
using _3_hafta.Dto.Concrete;
using _3_hafta.Entity.Concrete;
using Core.Utilities.Result;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_hafta.Test.Business
{
    public class EmployeeTests
    {
        Mock<IEmployeeService> _mockEmployeeService;
        List<EmployeeDto> _dbEmployee;
        public EmployeeTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _dbEmployee = getEmployeeList();

            _mockEmployeeService.Setup(m => m.GetListAsync()).ReturnsAsync(new SuccessDataResult<List<EmployeeDto>>(_dbEmployee));
        }
        [Fact]
        public async void Get_all_employees()
        {
            var result = await _mockEmployeeService.Object.GetListAsync();
            Assert.Equal(4, result.Data.Count);
        }

        private List<EmployeeDto> getEmployeeList()
        {
            return new List<EmployeeDto>
            {
                new EmployeeDto{EmpName="Emir",DeptId=1},
                new EmployeeDto{EmpName="Yasin",DeptId=5},
                new EmployeeDto{EmpName="Enes",DeptId=4},
                new EmployeeDto{EmpName="Oğuzhan",DeptId=3},
            };
        }
    }
}
