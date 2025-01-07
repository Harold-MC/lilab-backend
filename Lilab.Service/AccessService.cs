using Lilab.Data.Contract;
using Lilab.Data.Entity;
using Lilab.Data.ViewModel;
using Lilab.Service.Contract;
using LinqKit;
using Microsoft.EntityFrameworkCore;

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
        var whereBuilder = PredicateBuilder.New<Access>(vm => true);
        if (filters.Since != null) whereBuilder = whereBuilder.And(query => query.Date >= filters.Since);
        if (filters.Until != null) whereBuilder = whereBuilder.And(query => query.Date <= filters.Until);
        if (filters.CustomerId != null) whereBuilder = whereBuilder.And(query => query.Customer.Id.Equals(filters.CustomerId));
        
        var pagedList = await _accessRepository.GetPagedAsync(query => query
                .OrderByDescending(access => access.Date)
                .Include(access => access.Customer),
            whereBuilder,
            filters.Page,
            filters.PageSize);

        return pagedList;
    }
    
    public async Task<Access> CreateAsync(Access access)
    {
        _accessRepository.Attach(access);
        return await _accessRepository.AddAsync(access);
    }
}