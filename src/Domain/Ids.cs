using StronglyTypedIds;

[assembly:StronglyTypedIdDefaults(
    backingType: StronglyTypedIdBackingType.Guid,
    converters: StronglyTypedIdConverter.TypeConverter
        | StronglyTypedIdConverter.EfCoreValueConverter
        | StronglyTypedIdConverter.SystemTextJson
)]

namespace Scroll.Domain;

[StronglyTypedId]
public partial struct ProductId { }

[StronglyTypedId]
public partial struct CategoryId { }

[StronglyTypedId]
public partial struct FileId { }
