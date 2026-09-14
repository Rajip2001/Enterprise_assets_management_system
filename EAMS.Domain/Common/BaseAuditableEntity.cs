namespace EAMS.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    //Every important table should know:Who created it?When?Who modified it?When?
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
}