using Microsoft.EntityFrameworkCore;
using RestaurantMVCDBFirst.Context;
using RestaurantMVCDBFirst.Models;

namespace RestaurantMVCCodeFirst.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly RestaurantDBContext _context;

        public MenuRepository(RestaurantDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            return await _context.Menus.Include(m => m.Category).Where(m => m.IsActive).ToListAsync();
        }

        public async Task<Menu> GetMenuByIdAsync(int menuId)
        {
            return await _context.Menus.Include(m => m.Category).FirstOrDefaultAsync(m => m.MenuId == menuId && m.IsActive);
        }

        public async Task AddMenuAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuAsync(int menuId)
        {
            var menu = await _context.Menus.FindAsync(menuId);
            if (menu != null)
            {
                menu.IsActive = false; // Soft delete
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> MenuExistsAsync(int id)
        {
            return await _context.Menus.AnyAsync(m => m.MenuId == id && m.IsActive);
        }
    }
}
