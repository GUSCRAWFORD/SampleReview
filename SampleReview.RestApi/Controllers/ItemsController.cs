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
        // GET: api/Items
        public override IEnumerable<string> Get()
        {
            using(_contextFactory.Instance) {

            }
            return new string[] { "value1", "value2" };
        }

        // GET: api/Items/5
        public override string Get(int id)
        {
            return "value";
        }

        // POST: api/Items
        public override void Post([FromBody]string value)
        {
        }

        // PUT: api/Items/5
        public override void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Items/5
        public override void Delete(int id)
        {
        }
    }
}
