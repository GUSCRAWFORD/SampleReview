using System;
using SampleReview.Business.Features;
using SampleReview.Data.Context;
using SampleReview.Data.Repo;
using SampleReview.Business.Models;
using System.Linq;
namespace SampleReview.BusinessDriver.Features {
    public class ItemCatalog : Feature, IItemCatalog {
        public ItemCatalog(IDbContext context) : base(context) {
            _itemRepo = new GenRepo<IDbContext, Data.Domain.AnalyzedItem>(context);
        }

        protected GenRepo<IDbContext, Data.Domain.AnalyzedItem> _itemRepo;
        public Page<Item> All(int page, int perPage, string[] orderBy) {
            var results = _itemRepo
                            .Query(page, perPage, orderBy)
                            .Result()
                            .Select(itm=>ToViewModel<Item,Data.Domain.AnalyzedItem>(itm));

            return new Page<Item> {
                Collection = results,
                OfTotalItems = _itemRepo.Details.TotalRecords
            };
        }

        public Item ById(int id) {
            return ToViewModel<Item,Data.Domain.AnalyzedItem>(_itemRepo.Find(id));
        }

        public Item ByName(string name) {
            return  _itemRepo
                            .Query(itm=>itm.Name == name, 0, 0)
                            .Result()
                            .Select(itm=>ToViewModel<Item,Data.Domain.AnalyzedItem>(itm))
                            .Single();
        }
    }
}
