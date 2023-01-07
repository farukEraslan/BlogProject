using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICoreService<Category> _categoryService;
        private readonly ICoreService<Post> _postService;

        public CategoryController(ICoreService<Category> categoryService, ICoreService<Post> postService)
        {
            _categoryService = categoryService;
            _postService = postService;
        }

        public IActionResult Index()
        {
            return View(_categoryService.GetActive(t0 => t0.Posts).ToList());
        }
        public IActionResult PostsByCategory(Guid id)
        {
            return View(_postService.GetActive(t0 => t0.Kullanici, t2 => t2.Comments).Where(t0 => t0.CategoryId == id).ToList());
        }
    }
}
