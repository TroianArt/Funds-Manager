using System.Net.NetworkInformation;
using BLL.Interfaces;
using BLL.Services;
using DAL.Context;
using DAL.Domain;
using DAL.Interfaces;
using DAL.Repositories;
using log4net;
using Ninject.Modules;

namespace PL
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().ToMethod(cfg => LoggerConfig.GetLogger());
            Bind<FundsContext>().ToSelf().InSingletonScope();
            Bind<IRepository<BankAccount>>().To<Repository<BankAccount>>().InSingletonScope();
            Bind<IRepository<Currency>>().To<Repository<Currency>>().InSingletonScope();
            Bind<IRepository<Transaction>>().To<Repository<Transaction>>().InSingletonScope();
            Bind<IRepository<User>>().To<Repository<User>>().InSingletonScope();
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IBankAccountService>().To<BankAccountService>().InSingletonScope();
            Bind<ICurrencyService>().To<CurrencyService>().InSingletonScope();
            Bind<IStatisticsService >().To<StatisticsService>().InSingletonScope();
        }
    }
}