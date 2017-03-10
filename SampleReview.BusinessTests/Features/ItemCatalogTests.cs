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
    public class ItemCatalogTests {
        Mock<IDbContextFactory> mockContextFactory;
        Mock<IDbContext> mockContext;
        Mock<IRepo<IDbContext, AnyItem>> mockRepo;
        Mock<IRepo<IDbContext, AnalyzedItem>> mockAnalyzedItemsRepo;
        IItemCatalog itemCatalog;
        
        [TestInitialize]
        public void InitializeItemCatalogTests () {
            mockContextFactory = new Mock<IDbContextFactory>();
            mockContext = new Mock<IDbContext>();
            mockContextFactory.Setup(fac=>fac.Instance).Returns(mockContext.Object);
            mockRepo = new Mock<IRepo<IDbContext, AnyItem>>();
            mockAnalyzedItemsRepo = new Mock<IRepo<IDbContext, AnalyzedItem>>();
            itemCatalog = new ItemCatalog(mockContextFactory.Object, mockRepo.Object);
        }

        [TestMethod]
        public void ItemCatalogTest() {
            var mockResult = new List<AnalyzedItem> {
                new AnalyzedItem { Id = 1, Name = "item" }, new AnalyzedItem { Id = 4, Name = "page1" },
                new AnalyzedItem { Id = 2, Name = "page1" },new AnalyzedItem { Id = 5, Name = "item3" },
                new AnalyzedItem { Id = 3, Name = "page1" },new AnalyzedItem { Id = 6, Name = "item3" }
            };
            Expression<Func<IRepo<IDbContext, AnyItem>, IRepo<IDbContext, AnyItem>>> callPattern
                    =  (repo)=>repo
                                .Query(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            mockRepo.Setup(callPattern).Returns(mockRepo.Object).Verifiable();
            mockRepo.Setup(repo=>repo.Result()).Returns(mockResult);
            mockRepo.Verify(callPattern, Times.Once);
        }

        [TestMethod()]
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

        [TestMethod()]
        public void ByIdTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void ByNameTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveTest() {
            Assert.Fail();
        }
    }
}