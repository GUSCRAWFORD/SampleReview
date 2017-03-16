using SampleReview.Common;
using SampleReview.Data.Context;
using System.Web.Http;

namespace SampleReview.RestApi.Controllers
{
    public abstract class AnyController : ApiController
    {
        public AnyController (IDbContext context) {
            this.context = context;
        }
        protected IDbContext context;

    }
}
