
using SampleReview.Business.Features;
using SampleReview.Data.Context;
using SampleReview.Data.Repo;
using SampleReview.Business.Models;
using System.Linq;
using System;

namespace SampleReview.BusinessDriver.Features {
    public class ReviewManager : Feature, IReviewManager {
        public ReviewManager (IDbContextFactory contextFactory, IRepo<IDbContext, Data.Domain.Review> reviewRepo) : base(contextFactory) {            
            this.reviewRepo = reviewRepo;
        }

        protected IRepo<IDbContext, Data.Domain.Review> reviewRepo;

        public Page<Review> All(int item, int page, int perPage, string[] orderBy) {
            var results = reviewRepo
                            .Query(rvw=>rvw.Reviewing == item, page, perPage, orderBy)
                            .Result()
                            .Select(rvw=>ToViewModel<Review,Data.Domain.Review>(rvw));

            return new Page<Review> {
                Collection = results,
                TotalItems = reviewRepo.Details.TotalRecords
            };
        }

        public Review ById(int id) {
            return ToViewModel<Review,Data.Domain.Review>(reviewRepo.Find(id));
        }


        public void Save(Review review) {
            review.Date = DateTime.Now.ToUniversalTime();
            Business.Rules.Reviews.HaveValidRating(review);
            Business.Rules.Reviews.HaveCommentWithMinLen(review);
            reviewRepo
                .Upsert(ToDomainModel<Data.Domain.Review, Review>(review));
            context.SaveChanges();
        }
    }
}
