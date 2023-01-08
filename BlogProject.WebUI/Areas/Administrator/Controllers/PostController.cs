using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using BlogProject.WebUI.Areas.Administrator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BlogProject.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator"), Authorize]
    public class PostController : Controller
    {
        private readonly ICoreService<Post> _postService;
        private readonly ICoreService<Category> _catService;
        private readonly IHostingEnvironment _env;

        public PostController(ICoreService<Post> postService, ICoreService<Category> catService, IHostingEnvironment env)
        {
            _postService = postService;
            _catService = catService;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_postService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_catService.GetActive(), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post post, List<IFormFile> files)
        {
            ViewBag.Categories = new SelectList(_catService.GetActive(), "Id", "CategoryName");
            post.UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            bool imgResult;
            string imgPath = Upload.ImageUpload(files, _env, out imgResult);
            if (imgResult)
            {
                post.ImagePath = imgPath; // eğer imgResult true ise ilgili property'e resmin yolunu ekle.
            }
            else
            {
                ViewBag.MessageError = $"Resim yükleme işleminde bir hata oluştu!";
            }

            post.Status = Core.Entity.Enum.Status.None;
            post.ViewCount = 0;

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
        public IActionResult Update(Post post, List<IFormFile> files)
        {
            ViewBag.Categories = new SelectList(_catService.GetActive(), "Id", "CategoryName");
            post.UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            if (ModelState.IsValid)
            {			
				bool imgResult;
				string imgPath = Upload.ImageUpload(files, _env, out imgResult);
				if (imgResult)
				{
					post.ImagePath = imgPath; // eğer imgResult true ise ilgili property'e resmin yolunu ekle.
				}
				else
				{
					ViewBag.MessageError = $"Resim yükleme işleminde bir hata oluştu!";
				}

				bool result = _postService.Update(post);
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

