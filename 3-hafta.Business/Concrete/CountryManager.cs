using _3_hafta.Business.Abstract;
using _3_hafta.DataAccess.Abstract;
using _3_hafta.Dto.Concrete;
using _3_hafta.Entity.Concrete;
using AutoMapper;

namespace _3_hafta.Business.Concrete
{
    public class CountryManager : BaseManager<Country, CountryDto>, ICountryService
    {
        public CountryManager(ICountryDal entityRepository, IMapper mapper) : base(entityRepository, mapper)
        {
        }
    }
}
