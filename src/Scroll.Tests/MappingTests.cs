using AutoMapper;
using Scroll.Library.Models.DTOs;
using Scroll.Library.Models.EditModels;
using Scroll.Library.Models.Entities;
using Scroll.Library.Models.Mappers;
using Scroll.Library.Utils;
using System;
using System.Linq;
using Xunit;

namespace Scroll.Tests
{
    public class MappingTests
    {
        private static Guid Id1 = Guid.Parse("00000000-0000-0000-0000-000000000001");
        private static Guid Id2 = Guid.Parse("00000000-0000-0000-0000-000000000002");
        private static Guid Id3 = Guid.Parse("00000000-0000-0000-0000-000000000003");
        
        public readonly MapperConfiguration _mappingConfig;

        public readonly IMapper _mapper;

        public MappingTests()
        {
            _mappingConfig =
                new MapperConfiguration(cfg =>
                    cfg.AddProfile<MappingProfile>());

            _mapper =
                _mappingConfig.CreateMapper();
        }

        [Fact]
        public void MappingConfigIsValid() =>
            _mappingConfig.AssertConfigurationIsValid();

        [Fact]
        public void CategoryDtoMappingTest()
        {
            var entity =
                new Category
                {
                    Id = Id1,
                    Name = "Food"
                };

            var dto =
                new CategoryDto
                {
                    Id = Id1,
                    Name = "Food"
                };

            Assert.Equal(
                dto,
                _mapper.Map<CategoryDto>(entity));
        }

        [Fact]
        public void CategoryEditModelMappingTest()
        {
            var entity =
                new Category
                {
                    Id = Id1,
                    Name = "Food"
                };

            var editModel =
                new CategoryEditModel
                {
                    Id = Id1,
                    Name = "Food"
                };

            Assert.Equal(
                entity.ToJson(),
                _mapper.Map<Category>(editModel).ToJson());

            var newEditModel =
                editModel with { Name = "Junk Food" };

            var clonedEntity = entity.Clone()!;
            clonedEntity.Name = newEditModel.Name;

            var mappedUpdatedEntity =
                _mapper.Map(newEditModel, entity);

            Assert.True(ReferenceEquals(entity, mappedUpdatedEntity));
            Assert.False(ReferenceEquals(entity, clonedEntity));
            Assert.Equal(clonedEntity.ToJson(), mappedUpdatedEntity.ToJson());
        }

        [Fact]
        public void ProductDtoMappingTest()
        {
            var product =
                new Product
                {
                    Id            = Id1,
                    Title         = "Cheese Puffs",
                    Description   = "Cheese pufss is very nice chips",
                    Price         = 20m,
                    AddedOn       = new DateTimeOffset(
                                        year: 2020, month:  4, day:    1,
                                        hour: 0,    minute: 0, second: 0,
                                        offset: TimeSpan.FromHours(6)),
                    Link          = new Uri("https://google.com"),
                    ImageName     = "cheese-puffs",
                    ClickCount    = 420,
                    FavoriteCount = 69,
                    Favorites     = new(),
                    Categories    = new()
                                    {
                                        new() { Id = Id1, Name = "Food" },
                                        new() { Id = Id2, Name = "Snacks" },
                                        new() { Id = Id3, Name = "Unhealthy" }
                                    }
                };

            var manualMappedDto =
                new ProductDto
                {
                    Id            = product.Id,
                    Title         = product.Title,
                    Description   = product.Description,
                    Price         = product.Price,
                    AddedOn       = product.AddedOn,
                    Link          = product.Link.ToString(),
                    ImageName     = product.ImageName,
                    ClickCount    = product.ClickCount,
                    FavoriteCount = product.FavoriteCount,
                    Categories    =
                        product.Categories
                            .Select(c => _mapper.Map<CategoryDto>(c))
                            .ToComparableList()
                };

            var automappedDto =
                _mapper.Map<ProductDto>(product);

            Assert.Equal(manualMappedDto, automappedDto);
        }

        [Fact]
        public void ProductEditModelMappingTest()
        {
            var editModel =
                new ProductEditModel
                {
                    Id          = Id1,
                    Title       = "CBL Munchee",
                    Description = "CBL Munchee Wafer Rolls",
                    Price       = 200m,
                    Link        = "https://www.cblmuncheebd.com/",
                    ImageName   = "cbl-munchee"
                };

            var entityModel =
                new Product
                {
                    Id          = editModel.Id,
                    Title       = editModel.Title,
                    Description = editModel.Description,
                    Price       = editModel.Price,
                    Link        = new Uri(editModel.Link),
                    ImageName   = editModel.ImageName,
                    AddedOn     = DateTimeOffset.Now
                };

            var mappedEntity =
                _mapper.Map<Product>(editModel);

            mappedEntity.AddedOn =
                entityModel.AddedOn;

            Assert.Equal(
                entityModel.ToJson(),
                mappedEntity.ToJson());

            var newEditModel =
                editModel with
                {
                    Title       = "CBL Munchee Pro",
                    ImageName   = "cbl-munchee-pro"
                };

            // Simulate other updates on the entity
            entityModel.Favorites.Add(
                new Favorite { UserId = Id1, ProductId = Id1 });

            entityModel.FavoriteCount = 1;
            entityModel.ClickCount = 1;
            entityModel.Categories.Add(
                new Category { Id = Id1, Name = "Food" });

            var clonedEntity       = entityModel.Clone()!;
            clonedEntity.Title     = newEditModel.Title;
            clonedEntity.ImageName = newEditModel.ImageName;

            mappedEntity =
                _mapper.Map(newEditModel, entityModel);

            Assert.True(ReferenceEquals(entityModel, mappedEntity));
            Assert.False(ReferenceEquals(mappedEntity, clonedEntity));
            Assert.Equal(mappedEntity.ToJson(), clonedEntity.ToJson());
        }
    }
}