namespace SignHub.Application.Bases;


public class BaseDto
{
    public int Id { get; set; }
}


public abstract class AuditableDto : BaseDto
{
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; }
}
