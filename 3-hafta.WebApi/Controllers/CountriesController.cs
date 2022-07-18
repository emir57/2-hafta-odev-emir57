using _3_hafta.DataAccess.Abstract;
using _3_hafta.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace _3_hafta.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryDal _countryDal;

        public CountriesController(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Country country)
        {
            var result = await _countryDal.AddAsync(country);
            return Ok(result);
        }

    }
}
