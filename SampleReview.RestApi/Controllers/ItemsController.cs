using Microsoft.Practices.ServiceLocation;
using SampleReview.Business.Features;
using SampleReview.Business.Models;
using SampleReview.BusinessDriver.Features;
using SampleReview.Common;
using SampleReview.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleReview.RestApi.Controllers
{
    public class ItemsController : AnyController
    {
        public ItemsController(IFactory<IDbContext> contextFactory, ItemCatalog itemCatalog) : base (contextFactory) {
            this.itemCatalog = itemCatalog;
        }
        protected IItemCatalog itemCatalog;
        // GET: api/Items
        public Page<Item> Get(int page=0, int perPage=0, string orderBy="")
        {
            return itemCatalog.All(page, perPage, orderBy.Split(','));
        }

        // GET: api/Items/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Items
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Items/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Items/5
        public void Delete(int id)
        {
        }
    }
}
