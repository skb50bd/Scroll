using Scroll.Library.Models.EditModels;
using Scroll.Library.Models.Entities;

namespace Scroll.Library.Models.Mappers;

public static class ProductMapping
{
    public static Product ToEntity(
        this ProductEditModel editModel) =>
            new()
            {
                Id          = editModel.Id,
                Title       = editModel.Title,
                Description = editModel.Description,
                Price       = editModel.Price,
                Link        = new Uri(editModel.Link),
                ImageName   = editModel.ImageName
            };

    public static Product ToEntity(
        this ProductEditModel editModel,
        Product original)
    {
        original.Title       = editModel.Title;
        original.Description = editModel.Description;
        original.Price       = editModel.Price;
        original.Link        = new Uri(editModel.Link);
        original.ImageName   = editModel.ImageName;

        return original;
    }

    public static ProductEditModel ToEditModel(
        this Product entity) =>
            new()
            {
                Id          = entity.Id,
                Title       = entity.Title,
                Description = entity.Description,
                Price       = entity.Price,
                Link        = entity.Link?.AbsoluteUri ?? string.Empty,
                ImageName   = entity.ImageName
            };
}