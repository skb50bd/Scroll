using StronglyTypedIds;

[assembly:StronglyTypedIdDefaults(
    backingType: StronglyTypedIdBackingType.Guid,
    converters: StronglyTypedIdConverter.TypeConverter
        | StronglyTypedIdConverter.SystemTextJson
)]

namespace Scroll.Common;

[StronglyTypedId]
public partial struct ProductId { }

[StronglyTypedId]
public partial struct CategoryId { }

[StronglyTypedId]
public partial struct FileId { }
