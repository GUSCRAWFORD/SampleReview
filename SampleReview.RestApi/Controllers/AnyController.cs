using SampleReview.Common;
using SampleReview.Data.Context;
using System.Web.Http;

namespace SampleReview.RestApi.Controllers
{
    public abstract class AnyController : ApiController
    {
        public AnyController (IDbContextFactory contextFactory) {
            this.contextFactory = contextFactory;
        }
        protected IDbContextFactory contextFactory;

    }
}
