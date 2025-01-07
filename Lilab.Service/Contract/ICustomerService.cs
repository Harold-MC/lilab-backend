using Lilab.Data.Entity;
using Lilab.Data.ViewModel;

namespace Lilab.Service.Contract;

public interface ICustomerService
{
    Task<IPagedList<Customer>> GetPagedAsync(CustomerParamsViewModel filters);
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer> UpdateAsync(Customer customer);
    Task<Customer> RemoveAsync(Guid userId);
}