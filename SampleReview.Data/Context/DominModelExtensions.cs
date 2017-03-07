using SampleReview.Data.Domain;

namespace SampleReview.Data.Context {
    public static class DomainModelExtensions {
        public static bool HasEmptyId(this AnyDomainModel model) {
            return model.Id == 0;
        }
    }
}
