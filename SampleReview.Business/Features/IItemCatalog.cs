using SampleReview.Business.Models;

namespace SampleReview.Business.Features {
    public interface IItemCatalog {
        Page<Item> All(int page, int perPage, string[] orderBy);
        Item ById(int id);
        Item ByName(string name);
    }
}
