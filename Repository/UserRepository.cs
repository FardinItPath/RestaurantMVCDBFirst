using System.Data;
using Microsoft.EntityFrameworkCore;

using RestaurantMVCDBFirst.Context;
using RestaurantMVCDBFirst.Models;

namespace RestaurantMVCCodeFirst.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly RestaurantDBContext _context;

        public UserRepository (RestaurantDBContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateUser(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
          
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            //return await _context.Users.FindAsync(userId);
        }

        //public async Task<UserModel> GetUserByUsername(string username)
        //{
        //    return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        //}
        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users
                .Include(u => u.Role)  
                .FirstOrDefaultAsync(u => u.Username == username);
        }


        public async Task<bool> IsUsernameExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
           
        }

        public async Task<bool> RegisterUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
            
        }

        public async Task Update(User user)
        {
            user.UpdatedDT = DateTime.UtcNow;
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
