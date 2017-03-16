using SampleReview.Business.Features;
using SampleReview.Business.Models;
using SampleReview.Business.Rules;
using SampleReview.BusinessDriver.Features;
using SampleReview.Data.Context;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SampleReview.RestApi.Controllers
{
    
    public class ItemsController : AnyController
    {
        public ItemsController(IDbContext context, ItemCatalog itemCatalog) : base (context) {
            this.itemCatalog = itemCatalog;
        }
        protected IItemCatalog itemCatalog;
        // GET: api/Items?page=1&perPage=10&orderBy
        public Page<Item> Get(int page=0, int perPage=0, string orderBy = "")
        {
            return itemCatalog.All(page, perPage, orderBy.Split(','));
        }

        // GET: api/Items/5
        public Item Get(int id)
        {
            return itemCatalog.ById(id);
        }

        // POST: api/Items
        public void Post([FromBody]Item value)
        {
            itemCatalog.Save(value);
        }

        // PUT: api/Items/5
        public void Put(int id, [FromBody]Item value)
        {
            Post(value);
        }

        // DELETE: api/Items/5
        public void Delete(int id)
        {
        }
    }
}
