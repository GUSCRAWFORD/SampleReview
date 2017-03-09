using System;
using SampleReview.Business.Features;
using SampleReview.Data.Context;
using SampleReview.Data.Repo;
using SampleReview.Business.Models;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using SampleReview.Common;

namespace SampleReview.BusinessDriver.Features {
    public class ItemCatalog : Feature, IItemCatalog {
        public ItemCatalog(IFactory<IDbContext> contextFactory) : base(contextFactory) {            
            itemRepo = new Repo<IDbContext, Data.Domain.AnalyzedItem>(context);
        }

        protected Repo<IDbContext, Data.Domain.AnalyzedItem> itemRepo;
        public Page<Item> All(int page, int perPage, string[] orderBy) {
            var results = itemRepo
                            .Query(page, perPage, orderBy)
                            .Result()
                            .Select(itm=>ToViewModel<Item,Data.Domain.AnalyzedItem>(itm));

            return new Page<Item> {
                Collection = results,
                OfTotalItems = itemRepo.Details.TotalRecords
            };
        }

        public Item ById(int id) {
            return ToViewModel<Item,Data.Domain.AnalyzedItem>(itemRepo.Find(id));
        }

        public Item ByName(string name) {
            return  itemRepo
                            .Query(itm=>itm.Name == name, 0, 0)
                            .Result()
                            .Select(itm=>ToViewModel<Item,Data.Domain.AnalyzedItem>(itm))
                            .Single();
        }
    }
}
