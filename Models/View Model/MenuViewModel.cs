
using RestaurantMVCDBFirst.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantMVCCodeFirst.View_Model
{
    public class MenuViewModel
    {
        public int MenuId { get; set; }

        [Required(ErrorMessage = "Menu name is required.")]
        [StringLength(255, ErrorMessage = "Menu name cannot exceed 255 characters.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Required(ErrorMessage = "Menu description is required.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.00, 10000, ErrorMessage = "Price must be between $0.00 and $10,000.")]
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Category selection is required.")]
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }
        public IEnumerable<Category>? Categories { get; set; }
    }
}
