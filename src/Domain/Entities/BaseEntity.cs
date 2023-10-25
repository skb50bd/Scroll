namespace Scroll.Domain.Entities;

public interface IBaseEntity
{
    Guid Id { get; set; }
}

public abstract class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
}