using SampleReview.Data.Context;
namespace SampleReview.BusinessDriver.Features {
    public abstract class Feature {
        protected IDbContext _context;
        public Feature(IDbContext context) {
            _context = context;
        }

        private TModelB ToModel<TModelB, TModelA>(TModelA a) where TModelB : new() {
            TModelB b = new TModelB();
            foreach (var property in a.GetType().GetProperties()) {
                var domainProperty = a.GetType().GetProperty(property.Name);
                if (domainProperty != null && domainProperty.PropertyType == property.PropertyType) {
                    b.GetType().GetProperty(property.Name).SetValue(b, domainProperty.GetValue(a));
                }
            }
            return b;
        }
        protected TViewModel ToViewModel<TViewModel, TDomainModel>(TDomainModel domain) where TViewModel : new() {
            return ToModel<TViewModel, TDomainModel>(domain);
        }
        protected TDomainModel ToDomainModel<TDomainModel, TViewModel>(TViewModel model) where TDomainModel : new() {
            return ToModel<TDomainModel, TViewModel>(model);
        }
    }
}
