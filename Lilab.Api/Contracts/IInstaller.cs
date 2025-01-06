namespace Lilab.Api.Contracts;

public interface IInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration);
}