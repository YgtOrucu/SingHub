namespace SignHub.Domain.Bases;

public abstract class BaseEntity
{
    public int Id { get; set; }

}

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; }
}
