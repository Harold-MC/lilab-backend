using AutoMapper;
using Lilab.Api.Contracts;
using Lilab.Data;
using Lilab.Data.Contract;
using Lilab.Data.Repository;
using Lilab.Data.Setting;
using Lilab.Service;
using Lilab.Service.Contract;
using Lilab.Service.Utils;
using Microsoft.EntityFrameworkCore;

namespace Lilab.Api.Installers;

public class ServiceInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LilabContext>(optionsBuilder =>
        {
            var connString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            
            optionsBuilder.UseMySql(
                connString,
                new MySqlServerVersion(new Version(8, 0, 31))
            );
        });

        var jwtSetting = configuration.GetSection("Jwt").Get<JwtSetting>();
        services.AddSingleton(jwtSetting);

        
        services.AddTransient(typeof(IRepository<>), typeof(LilabRepository<>));
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IAccessService, AccessService>();
        services.AddTransient<PasswordHasher>();
        
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfileConfiguration());
        });
        

        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);
    }
}