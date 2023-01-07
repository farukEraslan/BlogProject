using BlogProject.Entities.Entities;

namespace BlogProject.WebUI.Models.ViewModel
{
    public class PostDetailVM
    {
        public Post Post { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
        
        // Sağ tarafta görünecek kategori listesi
        public List<Category> Categories { get; set; }


        public List<Post> RelatedPost { get; set; } = new List<Post>(3);

    }
}
