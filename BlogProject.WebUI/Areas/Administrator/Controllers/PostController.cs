using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator"), Authorize]
    public class PostController : Controller
    {
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<User> _userService;

        public PostController(ICoreService<Post> postService, ICoreService<User> userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_postService.GetAll());
        }

        [HttpGet]
        public IActionResult Create(Guid id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            post.Status = Core.Entity.Enum.Status.None;
            post.ViewCount = 0;
            //post.UserId = ;

            if (ModelState.IsValid)
            {
                bool result = _postService.Add(post);
                if (result)
                {
                    TempData["MessageSuccess"] = $"Kayıt işlemi başarılı.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MessageError"] = $"Kayıt işlemi sırasında bir hata meydana geldi. Lütfen tüm alanları kontrol edip tekrar deneyin.";
                }
            }
            else
            {
                TempData["MessageError"] = $"Kayıt işlemi sırasında bir hata meydana geldi. Lütfen tüm alanları kontrol edip tekrar deneyin.";
            }

            return View(post);
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            return View(_postService.GetById(id));
        }

        [HttpPost]
        public IActionResult Update(Post post)
        {
            if (ModelState.IsValid)
            {
                var updatedPost = _postService.GetById(post.Id);
                updatedPost.ImagePath = post.ImagePath;
                updatedPost.Title = post.Title;
                updatedPost.Kategori = post.Kategori;
                updatedPost.Status = Core.Entity.Enum.Status.Updated;
                updatedPost.PostDetail = post.PostDetail;

                bool result = _postService.Update(updatedPost);
                if (result)
                {
                    TempData["MessageSuccess"] = $"Kayıt işlemi başarılı";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MessageError"] = $"Kayıt işlemi sırasında bir hata meydana geldi. Lütfen tüm alanları kontrol edip tekrar deneyin.";
                }
            }
            else
            {
                TempData["MessageError"] = $"Kayıt işlemi sırasında bir hata meydana geldi. Lütfen tüm alanları kontrol edip tekrar deneyin.";
            }

            return View();
        }

        public IActionResult Activate(Guid id)
        {
            _postService.Activate(id);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            _postService.Remove(_postService.GetById(id));
            return RedirectToAction("Index");
        }

    }
}

