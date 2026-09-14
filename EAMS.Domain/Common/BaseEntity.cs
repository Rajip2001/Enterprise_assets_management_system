

namespace EAMS.Domain.Common;

public abstract class BaseEntity
{
    //Every table in the database needs a primary key.
    // The Id property is used as the primary key for the entity.
    //Instead of writing,inside every entity, we place it in one base class.
    public Guid Id { get; set; } = Guid.NewGuid();
}