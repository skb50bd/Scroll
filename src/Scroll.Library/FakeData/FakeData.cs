using Scroll.Library.Models.Entities;

namespace Scroll.Library.FakeData;

public static class FakeData
{
    public static Category[] Categories = new Category[]
    {
        new()
        {
            Id = 1,
            Name = "Toy",
            Description = "This is a toy category. Toys are nice, aren't they?"
        },
        new()
        {
            Id = 2,
            Name = "Food",
            Description = "This is a food category. Food is tasty."
        },
        new()
        {
            Id = 3,
            Name = "Geek",
            Description = "These are products for geeks. If you are a geek, you're gonna love these."
        },
        new()
        {
            Id = 4,
            Name = "Lifestyle",
            Description = "These products will improve your lifestyle. And will help you enjoy a better life."
        },
        new()
        {
            Id = 5,
            Name = "Pranks",
            Description = "You’re a funny guy, but if you really want to ramp up the LOLs at your next party, you need one of these hilarious and downright evil prank gifts."
        },
        new()
        {
            Id = 6,
            Name = "Gamer",
            Description =
                "Level up your gift giving powers and find the perfect gift for gamers in this hand curated list. "
                + "Whether you're looking for something for console gamer, "
                + "the PC gamer, "
                + "the old school gaming type who loves everything in 8-bit pixelation or the youngster who loves virtual reality and microtransactions, "
                + "this collection of gifts features all of the best sellers from this year."
        },
        new()
        {
            Id = 7,
            Name = "Photography",
            Description =
                "Capture the heart of your photographer friend with one of these delightful gifts for photographers. "
                + "Professionals and shutterbugs who enjoy it as a hobby alike will light up brighter than a flash when they unwrap something fun or functional from this list of great gift ideas."
        }
    };

    public static Product[] Products = new Product[]
    {
        new()
        {
            Id = 1,
            Title = "The Impossible To Open Frustration Box",
            Description = "If you need to get even with someone you love, the impossible to open frustration box is just what you need.",
            Price = 17.99m,
            AddedOn = DateTime.Now,
            ClickCount = 10,
            Link = new Uri("https://mynextdig.com/1"),
            ImageName = "abc-xyz.webp"
        },
        new()
        {
            Id = 2,
            Title = "Prank Mail Packages",
            Description = "Nothing will embarrass your poor unsuspecting victim quite like receiving one of these prank mail packages at their place or work.",
            Price = 11.86m,
            AddedOn = DateTime.Now,
            ClickCount = 3,
            Link = new Uri("https://mynextdig.com/2"),
            ImageName = "abc-xyz.webp"
        },
        new()
        {
            Id = 3,
            Title = "1500 Live Ladybugs",
            Description = "Ensure your buddy has nightmares for years to come by sending him 1500 live ladybugs.",
            Price = 30.96m,
            AddedOn = DateTime.Now,
            ClickCount = 69,
            Link = new Uri("https://mynextdig.com/3"),
            ImageName = "abc-xyz.webp"
        },
        new()
        {
            Id = 4,
            Title = "100 Must Play Video Games Scratch Off Poster",
            Description = "Gamers rejoice! Ensure no good game slips under your nose by playing everything on the 100 Video Games Bucket List poster.",
            Price = 17.47m,
            AddedOn = DateTime.Now,
            ClickCount = 3,
            Link = new Uri("https://mynextdig.com/4"),
            ImageName = "abc-xyz.webp"
        },
        new()
        {
            Id = 5,
            Title = "Personalized Gamertag Backlit LED",
            Description = "For both streamers and gamers, this backlit LED is the perfect way to give a customized gift to any gamer.",
            Price = 58.49m,
            AddedOn = DateTime.Now,
            ClickCount = 1,
            Link = new Uri("https://mynextdig.com/5"),
            ImageName = "abc-xyz.webp"
        },
        new()
        {
            Id = 6,
            Title = "Video Game Rage Candle",
            Description = "After an intense gaming session getting pwned by kids half your age, you need a way to relax and calm your nerves.",
            Price = 7.96m,
            AddedOn = DateTime.Now,
            ClickCount = 4,
            Link = new Uri("https://mynextdig.com/6"),
            ImageName = "abc-xyz.webp"
        },
        new()
        {
            Id = 7,
            Title = "Photography Prism",
            Description = "Take the rainbow with a photography prism.",
            Price = 16.49m,
            AddedOn = DateTime.Now,
            ClickCount = 420,
            Link = new Uri("https://mynextdig.com/7"),
            ImageName = "abc-xyz.webp"
        }
    };

    public static ProductCategoryMapping[] ProductCategories = new ProductCategoryMapping[]
    {
        new()
        {
            ProductId = 1,
            CategoryId = 1
        },
        new()
        {
            ProductId = 1,
            CategoryId = 2
        },
        new()
        {
            ProductId = 1,
            CategoryId = 3
        },
        new()
        {
            ProductId = 2,
            CategoryId = 2
        },
        new()
        {
            ProductId = 2,
            CategoryId = 3
        },
        new()
        {
            ProductId = 3,
            CategoryId = 3
        },
        new()
        {
            ProductId = 4,
            CategoryId = 4
        },new()
        {
            ProductId = 5,
            CategoryId = 1
        },
        new()
        {
            ProductId = 5,
            CategoryId = 2
        },
        new()
        {
            ProductId = 6,
            CategoryId = 7
        },new()
        {
            ProductId = 7,
            CategoryId = 2
        },new()
        {
            ProductId = 7,
            CategoryId = 7
        }
    };
}
