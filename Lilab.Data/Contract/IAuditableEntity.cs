namespace Lilab.Data.Contract;

public interface IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}