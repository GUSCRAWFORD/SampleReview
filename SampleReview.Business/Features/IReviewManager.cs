using SampleReview.Business.Models;

namespace SampleReview.Business.Features {
    public interface IReviewManager {
        Page<Review> All(int item, int page, int perPage, string[] orderBy);
        Review ById(int id);
        void Save(Review item);
    }
}
