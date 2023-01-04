using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using BlogProject.WebUI.Models;
using BlogProject.WebUI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogProject.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICoreService<User> _userService;
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<Category> _catService;

        public HomeController(ILogger<HomeController> logger, ICoreService<Category> catService, ICoreService<User> UserService, ICoreService<Post> postService)
        {
            _logger = logger;
            _catService  = catService;
            _userService = UserService;
            _postService = postService;
        }

        public IActionResult Index()
        {
            // Aktif olan postları döndürelim.
            return View(_postService.GetActive());
        }

        public IActionResult PostByCatId(Guid catId)
        {
            // Kategori Id'ye göre aktif postları döndürelim.
            return View(_postService.GetDefault(e => e.CategoryId == catId));
        }

        public IActionResult Post(Guid postId)
        {
            // Post'u göstereceğiz. Gösterirkende okunma sayısını(ViewCount) 1 arttıralım.
            Post okunanPost = _postService.GetById(postId);
            okunanPost.ViewCount++;
            _postService.Update(okunanPost);

            PostDetailVM vm = new PostDetailVM();
            vm.Post = okunanPost;
            vm.Category = _catService.GetById(okunanPost.CategoryId);
            vm.User = _userService.GetById(okunanPost.UserId);

            return View(vm); // View'a döndürürken ilgili postu, kategorisini, yazarını(kullanıcıyı) döndürmemiz gerekecektir (birden fazla model). Bu sebeple "Tuple" ya da "ViewModel" yapısını kullanmalıyız.
        }
    }
}