using SampleReview.Business.Models;
using SampleReview.Data.Context;

namespace SampleReview.BusinessDriver.Features {
    public abstract class Feature {
        protected IDbContext context;
        public Feature(IDbContextFactory contextFactory) {
            context = contextFactory.Instance;
        }

        private TModelB ToModel<TModelB, TModelA>(TModelA a) where TModelB : new() {
            TModelB b = new TModelB();
            foreach (var property in a.GetType().GetProperties()) {
                var targetProperty = b.GetType().GetProperty(property.Name);
                if (targetProperty != null && targetProperty.PropertyType == property.PropertyType) {
                    targetProperty.SetValue(b, property.GetValue(a));
                }
            }
            return b;
        }
        protected TViewModel ToViewModel<TViewModel, TDomainModel>(TDomainModel domain)
                where TViewModel : AnyModel, new()
                where TDomainModel : Data.Domain.AnyDomainModel {
            return ToModel<TViewModel, TDomainModel>(domain);
        }
        protected TDomainModel ToDomainModel<TDomainModel, TViewModel>(TViewModel model)
                where TViewModel : AnyModel
                where TDomainModel : Data.Domain.AnyDomainModel, new() {
            return ToModel<TDomainModel, TViewModel>(model);
        }
    }
}
