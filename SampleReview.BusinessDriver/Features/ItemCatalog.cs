using System;
using SampleReview.Business.Features;
using SampleReview.Data.Context;
using SampleReview.Data.Repo;
using SampleReview.Business.Models;
using System.Linq;

namespace SampleReview.BusinessDriver.Features {
    public class ItemCatalog : Feature, IItemCatalog {
        public ItemCatalog(IDbContextFactory contextFactory) : base(contextFactory) {            
            itemRepo = new Repo<IDbContext, Data.Domain.AnalyzedItem>(context);
            random = new Random();
        }

        protected Random random;
        protected Repo<IDbContext, Data.Domain.AnalyzedItem> itemRepo;

        public Page<Item> All(int page, int perPage, string[] orderBy) {
            var results = itemRepo
                            .Query(page, perPage, orderBy)
                            .Result()
                            .Select(itm=>ToViewModel(itm));

            return new Page<Item> {
                Collection = results,
                TotalItems = itemRepo.Details.TotalRecords
            };
        }

        public Item ById(int id) {
            return ToViewModel(itemRepo.Find(id));
        }

        public Item ByName(string name) {
            return  itemRepo
                            .Query(itm=>itm.Name == name, 0, 0)
                            .Result()
                            .Select(itm=>ToViewModel<Item,Data.Domain.AnalyzedItem>(itm))
                            .Single();
        }

        public void Save(Item item) {
            (new Repo<IDbContext, Data.Domain.Item>(context))
                .Upsert(ToDomainModel<Data.Domain.Item, Item>(item));
        }

        protected Item ToViewModel(Data.Domain.AnalyzedItem domain) {
            
            var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
            var viewModel = ToViewModel<Item, Data.Domain.AnalyzedItem>(domain);
            viewModel.Color = color;
            return viewModel;
        }
    }
}
