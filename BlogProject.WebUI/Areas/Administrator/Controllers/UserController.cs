using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator"), Authorize]
    public class UserController : Controller
    {
        private readonly ICoreService<User> _userService;

        public UserController(ICoreService<User> userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            user.Status = Core.Entity.Enum.Status.None;
            user.Comments = null;
            user.Posts = null;

            if (ModelState.IsValid)
            {
                bool result = _userService.Add(user);
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

            return View(user);
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            return View(_userService.GetById(id));
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            if (ModelState.IsValid)
            {
                var updatedUser = _userService.GetById(user.Id);
                updatedUser.Comments = user.Comments;
                updatedUser.Posts = user.Posts;
                bool result = _userService.Add(updatedUser);

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
            _userService.Activate(id);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            _userService.Remove(_userService.GetById(id));
            return RedirectToAction("Index");
        }
    }
}
