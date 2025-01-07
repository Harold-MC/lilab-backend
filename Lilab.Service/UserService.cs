using AutoMapper;
using Lilab.Data.Contract;
using Lilab.Data.Entity;
using Lilab.Data.ViewModel;
using Lilab.Service.Contract;
using Lilab.Service.Utils;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Lilab.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly PasswordHasher _passwordHasher;
        public UserService(IRepository<User> userRepository, 
            IRepository<Role> roleRepository,
            PasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
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
        
        public async Task<User> CreateAsync(User model, string password)
        {
            model.Password = _passwordHasher.Hash(password);
            _roleRepository.Attach(model.Role);
            
            return await _userRepository.AddAsync(model);
        }
        
        public async Task<User> UpdateAsync(User user)
        {
            var existingUser = await _userRepository
                .GetAsync(query => query.Where(usr => usr.Id == user.Id));

            if (existingUser == null)
                throw new KeyNotFoundException("User not found.");

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.IsActive = user.IsActive;
            existingUser.Role = user.Role;

            await _userRepository.UpdateAsync(existingUser);
            
            return existingUser;
        }
        
        public async Task<User> RemoveAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(query => query.Where(user => user.Id == userId));
            await _userRepository.RemoveAsync(user);
            return user;
        }
    }
}