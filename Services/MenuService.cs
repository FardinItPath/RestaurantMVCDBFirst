
using RestaurantMVCCodeFirst.Repository;
using RestaurantMVCDBFirst.Models;

namespace RestaurantMVCCodeFirst.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            return await _menuRepository.GetAllMenusAsync();
        }

        public async Task<Menu> GetMenuByIdAsync(int id)
        {
            return await _menuRepository.GetMenuByIdAsync(id);
        }

        public async Task AddMenuAsync(Menu  menu)
        {
            menu.IsActive = true;
            await _menuRepository.AddMenuAsync(menu);
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            await _menuRepository.UpdateMenuAsync(menu);
        }

        public async Task DeleteMenuAsync(int id)
        {
            await _menuRepository.DeleteMenuAsync(id);
        }
    }
}
