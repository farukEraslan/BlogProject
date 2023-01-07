using BlogProject.Core.Service;
using BlogProject.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebUI.Areas.Administrator.Controllers
{
    [Area("Administrator"), Authorize]
    public class CategoryController : Controller
    {
        private readonly ICoreService<Category> _categoryService;

        public CategoryController(ICoreService<Category> categoryService)
        {
            _categoryService = categoryService;
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

            if (ModelState.IsValid)
            {
                bool result = _categoryService.Add(category);
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

            return View(category);  // Ekleme işlemi sırasında kullanılan kategori bilgileriyle View'a döndürmesini sağlayabilir.
        }

        [HttpGet] // İlgili nesne ile update Sayfasını Gösterecek
        public IActionResult Update(Guid id)
        {

            return View(_categoryService.GetById(id));
        }

        [HttpPost] // Update Sayfasında gelen veriyi DB'ye gönderecek.
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                var updatedCategory = _categoryService.GetById(category.Id);
                updatedCategory.Description = category.Description;
                updatedCategory.CategoryName = category.CategoryName;
                updatedCategory.Status = Core.Entity.Enum.Status.Updated;

                bool result = _categoryService.Update(updatedCategory);
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
            _categoryService.Remove(_categoryService.GetById(id));
            return RedirectToAction("Index");
        }
    }
}
