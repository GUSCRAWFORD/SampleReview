using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using SampleReview.Data.Context;
using SampleReview.DataDriver.Context;
using SampleReview.BusinessDriver.Features;
using SampleReview.Business.Features;
using SampleReview.Common;
using SampleReview.Data.Domain;
using SampleReview.Data.Repo;

namespace SampleReview.RestApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            //  container.LoadConfiguration(); // Move type mappings to transormable config
            container.RegisterType<IDbContext, ReviewContext>(new PerThreadLifetimeManager());
            container.RegisterType<IItemCatalog, ItemCatalog>();
            container.RegisterType<IReviewManager, ReviewManager>();
            container.RegisterType<IRepo<IDbContext, AnyItem>,Repo<IDbContext, AnyItem>>();
            container.RegisterType<IRepo<IDbContext, Review>,Repo<IDbContext, Review>>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}