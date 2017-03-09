using SampleReview.Common;
using SampleReview.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleReview.DataDriver.Context {
    public class ContextFactory : IFactory<IDbContext> {
        protected IDbContext _singleContext;
        public IDbContext Instance { get {return _singleContext ?? (_singleContext = new ReviewContext()); } }
    }
}
