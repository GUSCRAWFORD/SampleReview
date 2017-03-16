using SampleReview.Business.Features;
using SampleReview.Business.Models;
using SampleReview.Business.Rules;
using SampleReview.BusinessDriver.Features;
using SampleReview.Data.Context;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SampleReview.RestApi.Controllers
{
    [EnableCors(origins: "http://localhost:54672,http://guscrawford.com", headers: "*", methods: "*")]
    public class ReviewsController : AnyController
    {
        public ReviewsController(IDbContext context, IReviewManager reviewManager) : base (context) {
            this.reviewManager = reviewManager;
        }
        protected IReviewManager reviewManager;
        // GET: api/Reviews?item=1&page=1&perPage=10&orderBy
        public Page<Review> Get(int item, int page=0, int perPage=0, string orderBy = "")
        {
            return reviewManager.All(item, page, perPage, orderBy.Split(','));
        }

        // GET: api/Items/5
        public Review Get(int id)
        {
            return reviewManager.ById(id);
        }

        // POST: api/Items
        public void Post([FromBody]Review value)
        {
            reviewManager.Save(value);
        }

        // PUT: api/Items/5
        public void Put(int id, [FromBody]Review value)
        {
            Post(value);
        }

        // DELETE: api/Items/5
        public void Delete(int id)
        {
        }
    }
}
