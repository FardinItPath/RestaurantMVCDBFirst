using RestaurantMVCDBFirst.Models;

namespace RestaurantMVCCodeFirst.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAllMenusAsync(); // Renamed from GetAllMenuItemsAsync
        Task<Menu> GetMenuByIdAsync(int menuId);
        Task AddMenuAsync(Menu menu); // Renamed from CreateMenuItemAsync
        Task UpdateMenuAsync(Menu menu);
        Task DeleteMenuAsync(int menuId);
        //Task<bool> MenuExistsAsync(int menuId); // Added this method
        
    }
}
