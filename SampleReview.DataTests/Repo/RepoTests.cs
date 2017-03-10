using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleReview.Data.Context;
using SampleReview.Data.Domain;
using SampleReview.DataTests;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SampleReview.Data.Repo.Tests {
    [TestClass]
    public class RepoTests {
        Mock<IDbContext> mockContext;
        IRepo<IDbContext, Item> repo;
        Mock<DbSet<Item>> mockSet;

        [TestInitialize]
        public void RepoTestsInitialize() {
            mockContext = new Mock<IDbContext>();           
            mockSet = Util.MockDbSet(new List<Item> {
                new Item { Id = 1, Name = "item" }, new Item { Id = 4, Name = "page1" },
                new Item { Id = 2, Name = "page1" },new Item { Id = 5, Name = "item3" },
                new Item { Id = 3, Name = "page1" },new Item { Id = 6, Name = "item3" }
            });
            mockContext.Setup(ctx=>ctx.Set<Item>()).Returns(mockSet.Object);
            repo = new Repo<IDbContext, Item>(mockContext.Object);
        }

        [TestMethod]
        public void QueryOrdersAscendingByDefaultTest() {
            var result = repo
                            .Query(itm=>itm.Name == "page1", 1, 3, "id")
                            .Result()
                            .ToList();
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.First().Id < result.Last().Id);

        }
        [TestMethod]
        public void QueryOrdersAscendingTest() {
            var result = repo
                            .Query(itm=>itm.Name == "page1", 1, 3, "+id")
                            .Result()
                            .ToList();
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.First().Id < result.Last().Id);
        }
        [TestMethod]
        public void QueryOrdersDescendingTest() {
            var result = repo
                            .Query(itm=>itm.Name == "page1", 1, 3, "-id")
                            .Result()
                            .ToList();
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue(result.First().Id > result.Last().Id);
        }
        [TestMethod]
        public void QueryTransformsAndOrdersByThisTest() {
            var result = repo
                .Query<object>(itm=>itm.Id,itm=>itm.Name == "page1", 1, 3, "this")
                            .Result<object>()
                            .ToList();
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue((int)result.First() < (int)result.Last());
            result = repo
                            .Query<object>(itm=>itm.Id,itm=>itm.Name == "page1", 1, 3, "+this")
                            .Result<object>()
                            .ToList();
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue((int)result.First() < (int)result.Last());
            result = repo
                            .Query<object>(itm=>itm.Id,itm=>itm.Name == "page1", 1, 3, "-this")
                            .Result<object>()
                            .ToList();
            Assert.IsTrue(result.Count() == 3);
            Assert.IsTrue((int)result.First() > (int)result.Last());
        }
    }
}