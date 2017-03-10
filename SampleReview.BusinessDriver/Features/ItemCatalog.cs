using System;
using SampleReview.Business.Features;
using SampleReview.Data.Context;
using SampleReview.Data.Repo;
using SampleReview.Business.Models;
using System.Linq;
using SampleReview.Data.Domain;

namespace SampleReview.BusinessDriver.Features {
    public class ItemCatalog : Feature, IItemCatalog {
        public ItemCatalog(IDbContextFactory contextFactory, IRepo<IDbContext, AnyItem> itemRepo) : base(contextFactory) {            
            this.itemRepo = itemRepo;
            random = new Random();
        }

        protected Random random;
        protected IRepo<IDbContext, AnyItem> itemRepo;

        public Page<Business.Models.Item> All(int page, int perPage, string[] orderBy) {
            var results = itemRepo.ToRepo<AnalyzedItem>()
                            .Query(page, perPage, orderBy)
                            .Result()
                            .Select(itm=>ToViewModel(itm));

            return new Page<Business.Models.Item> {
                Collection = results,
                TotalItems = itemRepo.Details.TotalRecords
            };
        }

        public Business.Models.Item ById(int id) {
            return ToViewModel(itemRepo.ToRepo<AnalyzedItem>().Find(id));
        }

        public Business.Models.Item ByName(string name) {
            return  itemRepo.ToRepo<AnalyzedItem>()
                            .Query(itm=>itm.Name == name, 0, 0)
                            .Result()
                            .Select(itm=>ToViewModel<Business.Models.Item,AnalyzedItem>(itm))
                            .Single();
        }

        public void Save(Business.Models.Item item) {
            itemRepo.ToRepo<Data.Domain.Item>()
                .Upsert(ToDomainModel<Data.Domain.Item, Business.Models.Item>(item));
        }

        protected Business.Models.Item ToViewModel(Data.Domain.AnalyzedItem domain) {
            
            var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
            var viewModel = ToViewModel<Business.Models.Item, AnalyzedItem>(domain);
            viewModel.Color = color;
            return viewModel;
        }
    }
}
