using Lilab.Data.Contract;
using Lilab.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Lilab.Data
{
	public class LilabContext : DbContext
	{
		public LilabContext(DbContextOptions<LilabContext> options) : base(options){}

		public DbSet<User> Users { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<Access> Accesses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
			/*modelBuilder.Entity<AdminUser>().HasIndex(u => u.Email).IsUnique();*/
		}

		#region Audit Entities
		public override int SaveChanges()
		{
			UpdateSoftDeleteStatuses();
			return base.SaveChanges();
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			UpdateSoftDeleteStatuses();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
        
		private void UpdateSoftDeleteStatuses()
		{
			foreach (var entry in ChangeTracker.Entries())
			{
				if (entry.Entity is IAuditableEntity)
				{
					switch (entry.State)
					{
						case EntityState.Added:
							entry.CurrentValues["CreatedAt"] = DateTime.Now;
							break;
						case EntityState.Modified:
							entry.State = EntityState.Modified;
							entry.CurrentValues["UpdateAt"] = DateTime.Now;
							break;
					}
				}
			}
		}
		#endregion

	}
}


