namespace EAMS.Domain.Common;

public abstract class BaseSoftDeleteEntity : BaseAuditableEntity
{
    //Every important table should know:Who deleted it?When?
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
}