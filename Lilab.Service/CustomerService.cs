using Lilab.Data.Contract;
using Lilab.Data.Entity;
using Lilab.Data.ViewModel;
using Lilab.Service.Contract;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Lilab.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }
        
        public async Task<IPagedList<Customer>> GetPagedAsync(CustomerParamsViewModel filters)
        {
            var whereBuilder = PredicateBuilder.New<Customer>(query => true);
            
            if (filters.SearchText != null) whereBuilder = whereBuilder.And(customer => 
                customer.Name.ToLower().Contains(filters.SearchText.ToLower()) || 
                customer.Phone.ToLower().Contains(filters.SearchText.ToLower()) || 
                customer.Email.ToLower().Contains(filters.SearchText.ToLower())
            );
            
            var pagedList = await _customerRepository.GetPagedAsync(query => query
                    .OrderBy(customer => customer.Name),
                whereBuilder,
                filters.Page,
                filters.PageSize);
            
            return pagedList;
        }
        
        public async Task<Customer> CreateAsync(Customer customer)
        {
            return await _customerRepository.AddAsync(customer);
        }
        
        public async Task<Customer> UpdateAsync(Customer customer)
        {
            return await _customerRepository.UpdateAsync(customer);
        }
        public async Task<Customer> RemoveAsync(Guid userId)
        {
            var customer = await _customerRepository.GetAsync(query => query.Where(user => user.Id == userId));
            await _customerRepository.RemoveAsync(customer);
            return customer;
        }
    }
}