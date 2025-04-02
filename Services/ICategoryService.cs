
using RestaurantMVCDBFirst.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantMVCCodeFirst.Services
{
    public interface ICategoryService  
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
    }
}
