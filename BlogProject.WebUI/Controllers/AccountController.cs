using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICoreService<User> _userService;

        public AccountController(ICoreService<User> userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            // Kullanıcının DB'de olup olmadığını kontrol ediyoruz.
            if (_userService.Any(x=>x.EmailAddress == user.EmailAddress && x.Password == user.Password))
            {
                // Eğer kullanıcı DB'de var ise kullanıcımızı yakalıyoruz.
                User loggedUser = _userService.GetByDefault(x => x.EmailAddress == user.EmailAddress && x.Password == user.Password);

                // Kullanıcımızın saklayacağımız bilgilerini Claim'ler olarak tutabiliriz.
                var claims = new List<Claim>()
                {
                    new Claim("Id", loggedUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, loggedUser.FirstName),
                    new Claim(ClaimTypes.Surname, loggedUser.LastName),
                    new Claim(ClaimTypes.Email, loggedUser.EmailAddress),
                    new Claim("ImageURL", loggedUser.ImageURL),
                };

                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                // Yönetici Home/Index sayfasına yönlendireceğiz.
                return RedirectToAction("Index", "Home", new { area = "Administrator" });
            }

            // Eğer giriş yapamazsa kullanıcı bilgileri ile forma dönsün.
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = " " });   // Çıkış yapıldıktan sonra Blog Anasayfasına yönlendirmek için.
        }
    }
}
