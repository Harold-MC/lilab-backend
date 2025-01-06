using Lilab.Data.Contract;
using Lilab.Data.Entity;
using Lilab.Data.ViewModel;
using Lilab.Service.Contract;

namespace Lilab.Service;
public class AccessService : IAccessService
{
    private readonly IRepository<Access> _accessRepository;

    public AccessService(IRepository<Access> accessRepository)
    {
        _accessRepository = accessRepository;
    }

    public async Task<IPagedList<Access>> GetPagedAsync(AccessParamsViewModel filters)
    {
        var pagedList = await _accessRepository.GetPagedAsync(null!,
            null!,
            filters.Page,
            filters.PageSize);

        return pagedList;
    }
    
    public async Task<Access> CreateAsync(Access access)
    {
        return await _accessRepository.AddAsync(access);
    }
}