using Lilab.Data.Entity;
using Lilab.Data.ViewModel;

namespace Lilab.Service.Contract;

public interface IAccessService
{
    Task<IPagedList<Access>> GetPagedAsync(AccessParamsViewModel filters);
    Task<Access> CreateAsync(Access access);
}