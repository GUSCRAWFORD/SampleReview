using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleReview.Business.Exceptions;
using SampleReview.Business.Features;
using SampleReview.BusinessDriver.Features;
using SampleReview.Data.Context;
using SampleReview.Data.Domain;
using SampleReview.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SampleReview.BusinessDriver.Features.Tests {
    [TestClass]
    public class ReviewManagerTests {
        Mock<IDbContext> mockContext;
        Mock<IRepo<IDbContext, Review>> mockRepo;
        IReviewManager reviewManager;
        
        [TestInitialize]
        public void InitializeReviewManagerTests () {
            mockContext = new Mock<IDbContext>();
            mockRepo = new Mock<IRepo<IDbContext, Review>>();
            reviewManager = new ReviewManager(mockContext.Object, mockRepo.Object);
        }

        [TestMethod]
        public void AllTest() {
            var mockResult = new List<Review> {
                new Review { Id = 1, Rating = 5, Reviewing = 1}, new Review { Id = 2, Rating = 4, Reviewing = 1},
                new Review { Id = 3, Rating = 3, Reviewing = 1}, new Review { Id = 4, Rating = 2, Reviewing = 1},
                new Review { Id = 5, Rating = 1, Reviewing = 1}, new Review { Id = 6, Rating = 1, Reviewing = 1}
            };
            Expression<Func<IRepo<IDbContext, Review>, IRepo<IDbContext, Review>>> queryCallSignature
                = (repo) => repo.Query(It.IsAny<Expression<Func<Review, bool>>>(), 1, 3, "rating");

            mockRepo.Setup(queryCallSignature).Returns(mockRepo.Object).Verifiable();
            mockRepo.SetupGet(repo => repo.Details).Returns(new QueryDetails
            {
                TotalRecords = 6,
                RecordsReturned = 3
            }).Verifiable();

            var page = reviewManager.All(1, 1, 3, new string[] { "rating" });
            mockRepo.Verify(queryCallSignature, Times.Once);
            mockRepo.Verify(repo => repo.Result(), Times.Once);
            mockRepo.Verify(repo => repo.Details, Times.Once);
        }

        [TestMethod]
        public void ByIdTest() {
            var expected = new Review { Id = 1, Reviewing = 1,  };
            Expression<Func<IRepo<IDbContext, Review>, Review>> queryCallSignature
                = (repo) => repo.Find(1);

            mockRepo.Setup(queryCallSignature).Returns(expected).Verifiable();

            var actual = reviewManager.ById(1);
            mockRepo.Verify(queryCallSignature, Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(RatingOutOfBoundsException))]
        public void SaveRatingOutOfBoundsExceptionTest() {
            var expected = new Business.Models.Review { };
            mockRepo.Setup(revs => revs.Upsert(It.IsAny<Review>())).Verifiable();
            reviewManager.Save(expected);
        }
        [TestMethod]
        [ExpectedException(typeof(CommentRequiredException))]
        public void SaveCommentRequiredExceptionTest()
        {
            var expected = new Business.Models.Review { Rating = 1 };
            mockRepo.Setup(revs => revs.Upsert(It.IsAny<Review>())).Verifiable();
            reviewManager.Save(expected);
        }
        [TestMethod]
        [ExpectedException(typeof(CommentRequiredException))]
        public void SaveBlankCommentRequiredExceptionTest()
        {
            var expected = new Business.Models.Review { Rating = 1 , Comment = "" };
            mockRepo.Setup(revs => revs.Upsert(It.IsAny<Review>())).Verifiable();
            mockContext.Setup(ctx => ctx.SaveChanges()).Verifiable();
            reviewManager.Save(expected);
        }
        [TestMethod]
        [ExpectedException(typeof(CommentTooShortException))]
        public void SaveCommentTooShortExceptionTest()
        {
            var expected = new Business.Models.Review { Rating = 1, Comment = "Umm"};
            mockRepo.Setup(revs => revs.Upsert(It.IsAny<Review>())).Verifiable();
            mockContext.Setup(ctx => ctx.SaveChanges()).Verifiable();
            reviewManager.Save(expected);
        }
        [TestMethod]
        public void SaveTest()
        {
            var expected = new Business.Models.Review { Rating = 1, Comment = "Ahhh" };
            mockRepo.Setup(revs => revs.Upsert(It.IsAny<Review>())).Verifiable();
            mockContext.Setup(ctx => ctx.SaveChanges()).Verifiable();
            reviewManager.Save(expected);
            mockRepo.Verify(revs => revs.Upsert(It.IsAny<Review>()), Times.Once);
            mockContext.Verify(ctx => ctx.SaveChanges(), Times.Once);
        }
    }
}