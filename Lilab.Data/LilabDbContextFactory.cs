using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lilab.Data
{
    public class LilabDbContextFactory: IDesignTimeDbContextFactory<LilabContext>
    {
        public LilabContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LilabContext>();
            
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            builder.UseMySql(connectionString!, new MySqlServerVersion(new Version(8, 0, 31)));
                
            return new LilabContext(builder.Options);
        }
    }
}