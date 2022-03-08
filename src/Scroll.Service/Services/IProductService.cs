using Scroll.Library.Models;
using Scroll.Library.Models.EditModels;
using Scroll.Library.Models.Entities;

namespace Scroll.Service.Services
{
    public interface IProductService
    {
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
        Task<Product?> Get(int id);
        Task<PagedList<Product>> GetPaged(int pageIndex = 0, int pageSize = 40);
        Task<int?> IncrementClickedCount(int productId);
        Task<Product> Insert(ProductEditModel editModel);
        Task<int> NewProductFavorite(int productId, string userName);
        Task<Product> Update(ProductEditModel editModel);
    }
}