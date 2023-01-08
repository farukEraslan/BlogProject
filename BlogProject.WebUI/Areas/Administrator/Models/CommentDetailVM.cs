using BlogProject.Entities.Entities;

namespace BlogProject.WebUI.Areas.Administrator.Models
{
    public class CommentDetailVM
    {
        public Comment Comment { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
