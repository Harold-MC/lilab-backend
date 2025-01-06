using Lilab.Data.Contract;
using Lilab.Data.Enums;

namespace Lilab.Data.Entity;

public class Access : IAuditableEntity
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public Customer Customer { get; set; }
    public AccessType Type { get; set; }

    #region Auditable
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    #endregion
}