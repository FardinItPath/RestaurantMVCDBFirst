
using RestaurantMVCCodeFirst.View_Model;
using RestaurantMVCDBFirst.Models;

namespace RestaurantMVCCodeFirst.Services
{
    public interface IUserService
    {
        object Role { get; }

        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int userId); 
        Task<User> GetUserByUsername(string username); 
        Task<bool> IsUsernameExists(string username); 
        Task<bool> RegisterUser(User user);
        Task<User> AuthenticateUser(string username, string password);
        Task<List<Role>> GetRoles();
        Task ResetPassword(ResetPasswordViewModel model);
    }
}
 