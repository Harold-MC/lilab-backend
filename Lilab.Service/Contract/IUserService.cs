using Lilab.Data.Entity;
using Lilab.Data.ViewModel;

namespace Lilab.Service.Contract;

    public interface IUserService
    {
        Task<User> UpdateAsync(User user);
        Task<User> CreateAsync(User model, string password);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IPagedList<User>> GetPagedAsync(UserParamsViewModel filters);
        Task<User> RemoveAsync(Guid userId);
        
    }
