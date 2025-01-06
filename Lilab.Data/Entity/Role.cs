using Lilab.Data.Contract;

namespace Lilab.Data.Entity;

public class Role : IAuditableEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<Permission> Permissions { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}