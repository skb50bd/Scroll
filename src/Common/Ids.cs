using StronglyTypedIds;

[assembly:StronglyTypedIdDefaults(Template.Guid)]

namespace Scroll.Common;

[StronglyTypedId]
public partial struct ProductId { }

[StronglyTypedId]
public partial struct CategoryId { }

[StronglyTypedId]
public partial struct FileId { }
