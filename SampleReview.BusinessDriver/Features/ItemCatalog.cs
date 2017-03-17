using System;
using SampleReview.Business.Features;
using SampleReview.Data.Context;
using SampleReview.Data.Repo;
using SampleReview.Business.Models;
using System.Linq;
using SampleReview.Data.Domain;

namespace SampleReview.BusinessDriver.Features {
    public class ItemCatalog : Feature, IItemCatalog {
        public ItemCatalog(IDbContext context, IRepo<IDbContext, AnyItem> itemRepo) : base(context) {            
            this.itemRepo = itemRepo;
            random = new Random();
        }

        protected Random random;
        protected IRepo<IDbContext, AnyItem> itemRepo;

        public Page<Business.Models.Item> All(int page, int perPage, string[] orderBy) {
            var analyzedItemRepo = itemRepo.ToRepo<AnalyzedItem>();
            var results = analyzedItemRepo
                            .Query(page, perPage, orderBy)
                            .Result()
                            .Select(itm=>ToViewModel(itm));

            return new Page<Business.Models.Item> {
                Collection = results,
                TotalItems = analyzedItemRepo.Details.TotalRecords
            };
        }

        public Business.Models.Item ById(int id) {
            return ToViewModel(itemRepo.ToRepo<AnalyzedItem>().Find(id));
        }

        public Business.Models.Item ByName(string name) {
            var analyzedItemRepo = itemRepo.ToRepo<AnalyzedItem>();
            return analyzedItemRepo
                            .Query(itm=>itm.Name == name, 0, 0)
                            .Result()
                            .Select(itm=>ToViewModel<Business.Models.Item,AnalyzedItem>(itm))
                            .Single();
        }

        public void Save(Business.Models.Item item) {
            itemRepo.ToRepo<Data.Domain.Item>()
                .Upsert(ToDomainModel<Data.Domain.Item, Business.Models.Item>(item));
        }

        protected Business.Models.Item ToViewModel(AnalyzedItem domain) {
            var viewModel = ToViewModel<Business.Models.Item, AnalyzedItem>(domain);
            if (viewModel.Color == null || viewModel.Color == string.Empty)
                viewModel.Color = String.Format("{0:X6}", random.Next(0x1000000));
            return viewModel;
        }
    }
}
