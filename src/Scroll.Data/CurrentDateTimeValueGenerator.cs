using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Scroll.Service.Data;

public class CurrentDateTimeValueGenerator : ValueGenerator<DateTimeOffset>
{
    public override DateTimeOffset Next(EntityEntry entry) => DateTimeOffset.UtcNow;
    public override bool GeneratesTemporaryValues { get; } = false;
}