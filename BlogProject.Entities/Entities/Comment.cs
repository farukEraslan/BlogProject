using BlogProject.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Entities.Entities
{
    public class Comment : CoreEntity
    {
        public string CommentText { get; set; }


        [ForeignKey("Kullanici")]
        public Guid UserId { get; set; }
        public virtual User Kullanici { get; set; }


        [ForeignKey("Gonderi")]
        public Guid PostId { get; set; }
        public virtual Post Gonderi { get; set; }

    }
}
