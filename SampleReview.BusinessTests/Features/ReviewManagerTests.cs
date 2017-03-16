using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        Mock<IRepo<IDbContext, AnyItem>> mockRepo;
        Mock<IRepo<IDbContext, Item>> mockItemsRepo;
        Mock<IRepo<IDbContext, AnalyzedItem>> mockAnalyzedItemsRepo;
        IItemCatalog itemCatalog;
        
        [TestInitialize]
        public void InitializeReviewManagerTests () {
            mockContext = new Mock<IDbContext>();
            mockRepo = new Mock<IRepo<IDbContext, AnyItem>>();
            mockAnalyzedItemsRepo = new Mock<IRepo<IDbContext, AnalyzedItem>>();
            mockItemsRepo = new Mock<IRepo<IDbContext, Item>>();
            itemCatalog = new ItemCatalog(mockContext.Object, mockRepo.Object);
        }

        [TestMethod]
        public void AllTest() {
            var mockResult = new List<AnalyzedItem> {
                new AnalyzedItem { Id = 1, Name = "item" }, new AnalyzedItem { Id = 4, Name = "page1" },
                new AnalyzedItem { Id = 2, Name = "page1" },new AnalyzedItem { Id = 5, Name = "item3" },
                new AnalyzedItem { Id = 3, Name = "page1" },new AnalyzedItem { Id = 6, Name = "item3" }
            };
            Expression<Func<IRepo<IDbContext, AnalyzedItem>, IRepo<IDbContext, AnalyzedItem>>> queryCallSignature
                = (repo) => repo.Query(1, 3, "id");

            mockRepo.Setup(repo => repo.ToRepo<AnalyzedItem>()).Returns(mockAnalyzedItemsRepo.Object).Verifiable();
            mockAnalyzedItemsRepo.Setup(queryCallSignature).Returns(mockAnalyzedItemsRepo.Object).Verifiable();
            mockAnalyzedItemsRepo.Setup(repo=>repo.Result()).Returns(mockResult).Verifiable();
            mockAnalyzedItemsRepo.SetupGet(repo => repo.Details).Returns(new QueryDetails
            {
                TotalRecords = 6,
                RecordsReturned = 3
            }).Verifiable();

            var page = itemCatalog.All(1,3, new string[] { "id" });
            mockRepo.Verify(repo => repo.ToRepo<AnalyzedItem>(), Times.Once);
            mockAnalyzedItemsRepo.Verify(queryCallSignature, Times.Once);
            mockAnalyzedItemsRepo.Verify(repo => repo.Result(), Times.Once);
            mockAnalyzedItemsRepo.Verify(repo => repo.Details, Times.Once);
        }

        [TestMethod]
        public void ByIdTest() {
            var expected = new AnalyzedItem { Id = 5, Name = "item3" };
            Expression<Func<IRepo<IDbContext, AnalyzedItem>, AnalyzedItem>> queryCallSignature
                = (repo) => repo.Find(1);

            mockRepo.Setup(repo => repo.ToRepo<AnalyzedItem>()).Returns(mockAnalyzedItemsRepo.Object).Verifiable();
            mockAnalyzedItemsRepo.Setup(queryCallSignature).Returns(expected).Verifiable();

            var actual = itemCatalog.ById(1);
            mockRepo.Verify(repo => repo.ToRepo<AnalyzedItem>(), Times.Once);
            mockAnalyzedItemsRepo.Verify(queryCallSignature, Times.Once);
        }

        [TestMethod]
        public void ByNameTest() {
            var expected = new AnalyzedItem { Id = 1, Name = "item" };
            var mockResult = new List<AnalyzedItem> { expected };

            Expression<Func<IRepo<IDbContext, AnalyzedItem>, IRepo<IDbContext, AnalyzedItem>>> queryCallSignature
                = (repo) => repo.Query(It.IsAny<Expression<Func<AnalyzedItem, bool>>>(), 0, 0);

            mockRepo.Setup(repo => repo.ToRepo<AnalyzedItem>()).Returns(mockAnalyzedItemsRepo.Object).Verifiable();
            mockAnalyzedItemsRepo.Setup(queryCallSignature).Returns(mockAnalyzedItemsRepo.Object).Verifiable();
            mockAnalyzedItemsRepo.Setup(repo => repo.Result()).Returns(mockResult).Verifiable();

            var page = itemCatalog.ByName("item");
            mockRepo.Verify(repo => repo.ToRepo<AnalyzedItem>(), Times.Once);
            mockAnalyzedItemsRepo.Verify(queryCallSignature, Times.Once);
            mockAnalyzedItemsRepo.Verify(repo => repo.Result(), Times.Once);
        }

        [TestMethod]
        public void SaveTest() {
            mockRepo.Setup(repo => repo.ToRepo<Item>()).Returns(mockItemsRepo.Object).Verifiable();
            mockItemsRepo.Setup(items => items.Upsert(It.IsAny<Item>())).Verifiable();
            itemCatalog.Save(new Business.Models.Item {});
            mockRepo.Verify(repo => repo.ToRepo<Item>(), Times.Once);
            mockItemsRepo.Verify(items => items.Upsert(It.IsAny<Item>()), Times.Once);
        }
    }
}