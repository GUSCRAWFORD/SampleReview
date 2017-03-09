using SampleReview.Common;
using SampleReview.Data.Context;
using System.Web.Http;

namespace SampleReview.RestApi.Controllers
{
    public abstract class AnyController : ApiController
    {
        public AnyController (IFactory<IDbContext> contextFactory) {
            this.contextFactory = contextFactory;
        }
        protected IFactory<IDbContext> contextFactory;

    }
}
