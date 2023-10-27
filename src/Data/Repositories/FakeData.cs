using Bogus;
using Scroll.Domain.Entities;
using Scroll.Domain.InputModels;

namespace Scroll.Data.Repositories;

internal static class FakeData
{
    static FakeData() => Randomizer.Seed = new(28);

    private static readonly Faker<Category> _categoryFaker =
        new Faker<Category>("en_US")
            .RuleFor(c => c.Id,          f => f.Database.Random.Guid())
            .RuleFor(c => c.Name,        f => f.Commerce.Categories(1)[0])
            .RuleFor(c => c.Description, f => f.Lorem.Paragraph());

    private static readonly List<Category> _top100Categories = GenerateCategories(100);

    private static List<Category> GetRandomNCategories(int n)
    {
        var categories = _top100Categories.ToArray();
        Randomizer.Seed.Shuffle(categories);
        return [.. categories[..n]];
    }

    public static List<Category> GenerateCategories(int count) =>
        _categoryFaker
            .Generate(10000)
            .DistinctBy(c => c.Name)
            .Take(count)
            .ToList();

    private static readonly Faker<Product> _productFaker =
        new Faker<Product>("en_US")
            .RuleFor(c => c.Id,             f => f.Database.Random.Guid())
            .RuleFor(c => c.Title,          f => f.Commerce.ProductName())
            .RuleFor(c => c.Description,    f => f.Commerce.ProductDescription())
            .RuleFor(c => c.Price,          f => Convert.ToDecimal(f.Commerce.Price()))
            .RuleFor(c => c.AddedOn,        f => f.Date.PastOffset())
            .RuleFor(c => c.Link,           f => new Uri(f.Internet.Url()))
            .RuleFor(c => c.ImageName,      f => f.Image.PicsumUrl())
            .RuleFor(c => c.ClickCount,     f => f.Random.Int(0, 100))
            .RuleFor(c => c.FavoriteCount,  f => f.Random.Int(0, 100))
            .RuleFor(c => c.Favorites,      f => new List<Favorite>())
            .RuleFor(c => c.Categories,     f => GetRandomNCategories(f.Random.Int(1, 7)));

    public static List<Product> GenerateProducts(int count) =>
        _productFaker
            .Generate(1000)
            .DistinctBy(p => p.Title)
            .Take(count)
            .ToList();

    class Person
    {
        public string? FullName { get; set; }
    }

    private static readonly Faker<Person> _userRegistrationFaker =
        new Faker<Person>()
            .RuleFor(p => p.FullName, f => f.Name.FullName());

    public static List<UserRegistrationModel> GenerateUsers(int count) =>
        _userRegistrationFaker
            .Generate(1000)
            .Distinct()
            .Take(count)
            .Select(u => u.FullName!)
            .Select(name =>
            {
                var username = name.Where(ch => char.IsLetterOrDigit(ch)).ToString()!;
                return new UserRegistrationModel(
                    FullName : name,
                    UserName : username,
                    Email    : $"{username}@example.com",
                    Password : "P@ssw0rd"
                );
            })
            .ToList();
}