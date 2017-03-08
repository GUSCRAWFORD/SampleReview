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
    public abstract class AnyController : ApiController
    {
        protected IFactory<IDbContext> _contextFactory;
        // GET: api/Items
        public abstract IEnumerable<string> Get();

        // GET: api/Items/5
        public abstract string Get(int id);

        // POST: api/Items
        public abstract void Post([FromBody]string value);

        // PUT: api/Items/5
        public abstract void Put(int id, [FromBody]string value);

        // DELETE: api/Items/5
        public abstract void Delete(int id);
    }
}
