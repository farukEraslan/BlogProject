using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebUI.Areas.Administrator.Controllers
{
	[Area("Administrator")]
	public class CategoryController : Controller
	{
		private readonly ICoreService<Category> _categoryService;

		public CategoryController(ICoreService<Category> categoryService)
		{
			_categoryService= categoryService;
		}
		public IActionResult Index()
		{
			return View(_categoryService.GetAll());
		}

		[HttpGet] // Create Sayfasını Gösterecek
        public IActionResult Create()
        {
            return View();
        }

		[HttpPost] // Create Sayfasında gelen veriyi DB'ye gönderecek.
        public IActionResult Create(Category category)
        {
            category.Status = Core.Entity.Enum.Status.None;

            bool result = _categoryService.Add(category);
            if (result)
            {
                TempData["Message"] = $"Kayıt işlemi başarılı.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = $"Kayıt işlemi sırasında bir hata meydana geldi. Lütfen tüm alanları kontrol edip tekrar deneyin.";
            }
   //         if (ModelState.IsValid)
   //         {
   //             bool result = _categoryService.Add(category);
   //             if (result)
   //                 return RedirectToAction("Index");
   //             else
   //             {
   //                 TempData["Message"] = $"Kayıt işlemi sırasında bir hata meydana geldi. Lütfen tüm alanları kontrol edip tekrar deneyin.";
   //             }
			//}
			//else
			//{
			//	TempData["Message"] = $"Kayıt işlemi sırasında bir hata meydana geldi. Lütfen tüm alanları kontrol edip tekrar deneyin.";
			//}

			return View(category);  // Ekleme işlemi sırasında kullanılan kategori bilgileriyle View'a döndürmesini sağlayabilir.
        }

        [HttpGet] // İlgili nesne ile update Sayfasını Gösterecek
        public IActionResult Update(Guid id)
        {
            return View();
        }

        [HttpPost] // Update Sayfasında gelen veriyi DB'ye gönderecek.
        public IActionResult Update(Category category)
        {
            return View();
        }

        // gelen Id'ye göre ilgili nesneyi aktifleştirecek
        public IActionResult Activate(Guid id)
        {
            // View göstermeyecek Index'e yönlendirecek
            _categoryService.Activate(id);
            return RedirectToAction("Index");
        }
        
        //  gelen Id'ye göre ilgili nesneyi silecek
        public IActionResult Delete(Guid id)
        {
            // View göstermeyecek Index'e yönlendirecek
            _categoryService.Remove(id);
            return View();
        }
    }
}
