using BlogProject.Entities.Entities;

namespace BlogProject.WebUI.Models.ViewModel
{
    public class PostDetailVM
    {
        public Post Post { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }

    }
}
