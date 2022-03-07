namespace Scroll.Service.Data;

public class ProductRepository
{
    //private readonly IMongoCollection<Product> _products;

    //public ProductRepository(
    //    IMongoDatabase database)
    //{
    //    _products =
    //        database.GetCollection<Product>(nameof(Product));
    //}

    //public async Task<PagedList<Product>> Get(
    //    int pageIndex = 0,
    //    int pageSize = 40)
    //{
    //    var items =
    //        await _products
    //            .Find(Builders<Product>.Filter.Empty)
    //            .Skip(pageIndex * pageSize)
    //            .Limit(pageSize)
    //            .Sort(Builders<Product>.Sort.Descending(p => p.ClickCount))
    //            .ToListAsync();

    //    var totalCount =
    //        await _products.EstimatedDocumentCountAsync();

    //    return new(items, pageSize, pageIndex, (int)totalCount);
    //}

    //public async Task<Product?> Get(int id)
    //{
    //    var filter =
    //        Builders<Product>.Filter
    //            .Where(x => x.Id == id);

    //    var cursor =
    //        await _products.FindAsync(filter);

    //    var product =
    //        await cursor.FirstOrDefaultAsync();

    //    return product;
    //}

    //public async Task<Product> Upsert(ProductEditModel editModel)
    //{
    //    var original =
    //        await Get(editModel.Id);

    //    if (original is not null)
    //    {
    //        var filter =
    //            Builders<Product>.Filter
    //                .Where(x => x.Id == original.Id);

    //        var replacement =
    //            editModel.ToEntity(original);

    //        await _products.ReplaceOneAsync(filter, replacement);

    //        return replacement;
    //    }
    //    else
    //    {
    //        var entity = editModel.ToEntity();

    //        await _products.InsertOneAsync(entity);

    //        return entity;
    //    }
    //}

    //public async Task<bool> Delete(int id)
    //{
    //    var filter =
    //        Builders<Product>.Filter
    //            .Where(x => x.Id == id);

    //    var result =
    //        await _products.DeleteOneAsync(filter);

    //    return result is { IsAcknowledged: true, DeletedCount: 1 };
    //}

    //public async Task<int> Clicked(int id)
    //{
    //    var filter =
    //        Builders<Product>.Filter
    //            .Where(x => x.Id == id);

    //    var update =
    //        Builders<Product>.Update
    //            .Inc(p => p.ClickCount, 1);

    //    var updateResult =
    //        await _products.UpdateOneAsync(filter, update);

    //    if (updateResult is { IsAcknowledged: true, ModifiedCount: 1 })
    //    {
    //        var project =
    //            Builders<Product>.Projection
    //                .Expression(p => p.ClickCount);

    //        var clickedCount =
    //            await _products
    //                .Find(filter)
    //                .Project(project)
    //                .FirstOrDefaultAsync();

    //        return clickedCount;
    //    }

    //    return 0;
    //}
}