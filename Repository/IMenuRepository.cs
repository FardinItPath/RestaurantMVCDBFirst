
using RestaurantMVCDBFirst.Models;

namespace RestaurantMVCCodeFirst.Repository
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllMenusAsync();
        Task<Menu> GetMenuByIdAsync(int menuId);
        Task AddMenuAsync(Menu menu);
        Task UpdateMenuAsync(Menu menu);
        Task DeleteMenuAsync(int menuId);
        Task<bool> MenuExistsAsync(int id); // Add this line
    }
}
