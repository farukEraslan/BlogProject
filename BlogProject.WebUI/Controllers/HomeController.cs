using BlogProject.Core.Entity.Enum;
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
        private readonly ICoreService<Comment> _commentService;

        public HomeController(ILogger<HomeController> logger, ICoreService<Category> catService, ICoreService<User> UserService, ICoreService<Post> postService, ICoreService<Comment> commentService)
        {
            _logger = logger;
            _catService = catService;
            _userService = UserService;
            _postService = postService;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            // Aktif olan postları döndürelim.

            // Her bir post için yazarın bilgilerini getirdik.
            var postList = _postService.GetActive();
            foreach (var item in postList)
            {
                var postUserId = item.UserId;
                var postUsername = _userService.GetById(postUserId);
            }
            return View(_postService.GetActive());
        }

        // Kullanılmıyor
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
            vm.Comments = _commentService.GetDefault(e => e.Status == Status.Active && e.PostId == okunanPost.Id);

            Random r = new Random();    // Random nesnesi oluşturduk.
            for (int i = 0; i < 3; i++)
            {
                vm.RelatedPost.Add(_postService.GetActive().ElementAt(r.Next(0, _postService.GetActive().Count())));
            }

            return View(vm); // View'a döndürürken ilgili postu, kategorisini, yazarını(kullanıcıyı) döndürmemiz gerekecektir (birden fazla model). Bu sebeple "Tuple" ya da "ViewModel" yapısını kullanmalıyız.
        }

        public IActionResult SearchResult(string q) // Kategorinin Id'si
        {
            // Gelen string'e göre başlık veya içerikte gelen ifadede geçen aktif postları döndürelim.
            //return View(_postService.GetDefault(x=>x.Title.Contains(q) || x.PostDetail.Contains(q)));

            ViewData["Title"] = "Ara: " + q;
            return View(_postService.GetActive(t0=>t0.Kullanici, t2=>t2.Comments).Where(x => x.Title.Contains(q)).ToList());
        }
    }
}