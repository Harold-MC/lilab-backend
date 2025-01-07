using AutoMapper;
using Lilab.Data.Entity;
using Lilab.Data.ViewModel;

namespace Lilab.Data;
public class AutoMapperProfileConfiguration : Profile
{
    public AutoMapperProfileConfiguration()
        : this("AutoMapperProfile")
    {
    }
    AutoMapperProfileConfiguration(string profileName)
        : base(profileName)
    {
        CreateMap<CreateUserViewModel, User>();
    }
}