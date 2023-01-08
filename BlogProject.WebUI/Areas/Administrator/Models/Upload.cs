using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BlogProject.WebUI.Areas.Administrator.Models
{
    public class Upload
    {
        public static string ImageUpload(List<IFormFile> files, IHostingEnvironment _env, out bool result)
        {
            // Resim yükleme işlemlerimizi gerçekleştireceğiz. Geriye resim yolunu veya hata mesajını döndüreceğiz. Ayrıca, dönen string'in başarı bilgisini mi yoksa hata mesajı mı olduğunu anlamak için dışarıya result değeri fırlatacağız.

            result = false;
            var uploads = Path.Combine(_env.WebRootPath, "Uploads");

            foreach (var file in files)
            {
                if (file.ContentType.Contains("image")) // Dosya tipinde image geçiyorsa
                {
                    if (file.Length <= 2097152) // Dosya boyutu 2mb'dan küçük ise
                    {
                        string uniqueName = $"{Guid.NewGuid().ToString().Replace("-", "_").ToLower()}.{file.ContentType.Split('/')[1]}";

                        var filePath = Path.Combine(uploads, uniqueName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            result = true;
                            string newFilePath = filePath.Substring(filePath.IndexOf("Uploads\\"));
                            return newFilePath.Replace("\\","/");
                        }
                    }
                    else
                    {
                        return $"2 MB'tan büyük boyutta resim yükleyemezsiniz.";
                    }
                }
                else
                {
                    return $"Lütfen sadece resim dosyası yükleyin.";
                }
            }
            return "Dosya bulunamadı! Lütfen en az bir tane dosya seçin";
        }
    }
}
