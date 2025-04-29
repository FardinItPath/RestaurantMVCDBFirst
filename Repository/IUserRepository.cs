
using RestaurantMVCDBFirst.Models;

namespace RestaurantMVCCodeFirst.Repository
{
    public interface IUserRepository
    {
        Task<bool> RegisterUser(User user); 
        Task<User> AuthenticateUser(string username, string password); 
        Task<bool> IsUsernameExists(string username); 
        Task<User> GetUserById(int userId); 
        Task<User> GetUserByUsername(string username); 
        Task<List<User>> GetAllUsers();
        Task<List<Role>> GetRoles();
        Task Update(User user);

        //Task<bool> UpdateUser(UserModel user); 
        //Task<bool> DeleteUser(int userId); 
    }
}
