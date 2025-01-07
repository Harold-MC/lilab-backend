using Lilab.Data.Contract;
using Lilab.Data.Enums;

namespace Lilab.Data.Entity;

public class Customer : IAuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public CustomerType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}