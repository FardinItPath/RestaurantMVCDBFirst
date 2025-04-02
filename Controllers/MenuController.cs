
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantMVCCodeFirst.Services;
using RestaurantMVCCodeFirst.View_Model;
using RestaurantMVCDBFirst.Models;

namespace RestaurantMVCCodeFirst.Controllers
{
    //[Route("menu")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly ICategoryService _categoryService;

        public MenuController(IMenuService menuService, ICategoryService categoryService)
        {
            _menuService = menuService;
            _categoryService = categoryService;
        }

        private bool IsUserLoggedIn() => HttpContext.Session.GetString("UserId") != null;
        private bool IsAdmin() => HttpContext.Session.GetString("UserRole") == "Admin";

        [Route("menu/list")]
        public async Task<IActionResult> Index()
        {
            //if (!IsUserLoggedIn()) return RedirectToAction("Login", "Account");
           //if (!IsAdmin()) return Unauthorized();
            ViewBag.id = HttpContext.Session.GetInt32("RoleId");

            var menuItems = await _menuService.GetAllMenusAsync();
            return View(menuItems);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsAdmin()) return Unauthorized();

            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null || !categories.Any())
            {
                TempData["Error"] = "No categories found! Please add categories first.";
                return RedirectToAction("list", "menu"); // Redirect if no categories
            }

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            return View(new MenuViewModel()); // Return a new ViewModel instance
        }



        [HttpPost]
[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "CategoryName");
                return View(model); 
            }

            var newMenu = new Menu
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                IsActive = model.IsActive,
                CategoryId = model.CategoryId,
                CreatedDT = DateTime.Now, 
                UpdatedDT = null
            };

            await _menuService.AddMenuAsync(newMenu);
            TempData["Success"] = "Menu created successfully!";
            return RedirectToAction("list");
        }

        [Route("menu/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsAdmin()) return Unauthorized();

            var menuItem = await _menuService.GetMenuByIdAsync(id);
            if (menuItem == null) return NotFound();

            var categories = await _categoryService.GetAllCategoriesAsync();

            if (categories == null || !categories.Any())
            {
                TempData["Error"] = "No categories found! Please add categories first.";
                return RedirectToAction("List", "Menu");
            }

            var menuViewModel = new MenuViewModel
            {
                MenuId = menuItem.MenuId,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                IsActive = menuItem.IsActive,
                CategoryId = menuItem.CategoryId,
                Categories = categories
            };

            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View(menuViewModel);
        }

        [HttpPost]
        [Route("menu/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuViewModel model)
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsAdmin()) return Unauthorized();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "CategoryName");
                return View(model);
            }

            var existingMenu = await _menuService.GetMenuByIdAsync(model.MenuId);
            if (existingMenu == null) return NotFound();

            existingMenu.Name = model.Name;
            existingMenu.Description = model.Description;
            existingMenu.Price = model.Price;
            existingMenu.IsActive = model.IsActive;
            existingMenu.CategoryId = model.CategoryId;

            await _menuService.UpdateMenuAsync(existingMenu);
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsUserLoggedIn()) return RedirectToAction("Login", "Account");
            if (!IsAdmin()) return Unauthorized();

            await _menuService.DeleteMenuAsync(id);
            return RedirectToAction("Index");
        }
    }
}
