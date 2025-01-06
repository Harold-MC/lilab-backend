using Lilab.Data.Contract;

namespace Lilab.Data.Entity;

public class Permission : IAuditableEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<Role> Roles { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}