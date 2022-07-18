using _3_hafta.DataAccess.Abstract;
using _3_hafta.DataAccess.Concrete.Dapper;
using _3_hafta.DataAccess.Concrete.EntityFramework;
using Autofac;

namespace _3_hafta.Business.DepedencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfEmployeeDal>().As<IEmployeeDal>();
            builder.RegisterType<EfFolderDal>().As<IFolderDal>();
            builder.RegisterType<DpCountryDal>().As<ICountryDal>();
            builder.RegisterType<DpDepartmentDal>().As<IDepartmentDal>();
        }
    }
}
