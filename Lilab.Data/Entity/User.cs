using Lilab.Data.Contract;
using Newtonsoft.Json;

namespace Lilab.Data.Entity;
public class User : IAuditableEntity
{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public Role Role { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
}
