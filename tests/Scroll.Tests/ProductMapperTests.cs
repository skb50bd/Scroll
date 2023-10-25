using Scroll.Core.ObjectMapping;
using Scroll.Domain.FakeData;
using Scroll.Domain.Utils;

namespace Scroll.Tests;

public class ProductMapperTests
{
    [Fact]
    public void EntityToDto()
    {
        // arrange
        var product = FakeData.Products.First();

        // act
        var dto = product.ToDto();

        // assert
        Assert.NotNull(dto);
        Assert.Equal(product.Id, dto.Id);
        Assert.Equal(product.Title, dto.Title);
        Assert.Equal(product.Description, dto.Description);
        Assert.Equal(product.Price, dto.Price);
        Assert.Equal(product.ImageName, dto.ImageName);
        Assert.Equal(
            product.Categories.ProjectToDto().ToComparableList(),
            dto.Categories
        );
    }
}