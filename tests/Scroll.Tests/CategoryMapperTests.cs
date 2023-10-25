using Scroll.Core.Extensions;
using Scroll.Core.ObjectMapping;
using Scroll.Domain.FakeData;

namespace Scroll.Tests;

public class CategoryMapperTests
{
    [Fact]
    public void EntityToDto()
    {
        // arrange
        var category = FakeData.Categories.First();

        // act
        var dto = category.ToDto();

        // assert
        Assert.NotNull(dto);
        Assert.Equal(category.Id, dto.Id);
        Assert.Equal(category.Name, dto.Name);
        Assert.Equal(category.Description, dto.Description);
    }

    [Fact]
    public void EntityToEditModel()
    {
        // arrange
        var category = FakeData.Categories.First();

        // act
        var editModel = category.ToEditModel();

        // assert
        Assert.NotNull(editModel);
        Assert.Equal(category.Id, editModel.Id);
        Assert.Equal(category.Name, editModel.Name);
        Assert.Equal(category.Description, editModel.Description);
    }

    [Fact]
    public void EditModelToEntity()
    {
        // arrange
        var editModel =
            FakeData.Categories.First()
                .ToEditModel()
                .RequireNotNull();

        // act
        var category = editModel.ToEntity();

        // assert
        Assert.NotNull(category);
        Assert.Equal(editModel.Id, category.Id);
        Assert.Equal(editModel.Name, category.Name);
        Assert.Equal(editModel.Description, category.Description);
    }
}