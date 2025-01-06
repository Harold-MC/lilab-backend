using AutoMapper;
using Lilab.Data.Contract;
using Lilab.Data.Entity;
using Lilab.Data.ViewModel;
using Lilab.Service.Contract;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Lilab.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        
        public async Task<IPagedList<User>> GetPagedAsync(UserParamsViewModel filters)
        {
            var whereBuilder = PredicateBuilder.New<User>(query => true);
            
            if (filters.RoleId != null) whereBuilder = whereBuilder.And(user => user.Role.Id == filters.RoleId);
            
            if (filters.IsActive != null) whereBuilder = whereBuilder.And(user => user.IsActive == filters.IsActive);
            
            if (filters.SearchText != null) whereBuilder = whereBuilder.And(user => 
                user.Name.ToLower().Contains(filters.SearchText!.ToLower()) || 
                user.Email.ToLower().Contains(filters.SearchText.ToLower())
            );
            

            var userPagedList = await _userRepository.GetPagedAsync(query => query
                    .Include(user => user.Role),
                whereBuilder,
                filters.Page,
                filters.PageSize);

            return userPagedList;
        }
        
        public async Task<User> CreateAsync(User user)
        {
            Console.WriteLine(user);
            return null;
            return await _userRepository.AddAsync(user);
        }
        
        public async Task<User> UpdateAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }
        
        public async Task<bool> RemoveAsync(User user)
        {
            await _userRepository.RemoveAsync(user);
            return true;
        }
    }
}