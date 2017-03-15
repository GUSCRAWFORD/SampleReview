using SampleReview.Data.Context;

namespace SampleReview.DataDriver.Context {
    public class ContextFactory : IDbContextFactory {
        protected IDbContext singleContext;
        public IDbContext Instance {
            get {return singleContext ?? (singleContext = new ReviewContext()); }
        }

        public void Dispose() {
            if (singleContext != null) singleContext.Dispose();
        }
    }
}
