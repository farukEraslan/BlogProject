using BlogProject.Core.Map;
using BlogProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Entities.Map
{
    public class CategoryConfig : CoreMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.Property(x=>x.CategoryName).IsRequired(true).HasMaxLength(50);
            builder.Property(x=>x.Description).IsRequired(false).HasMaxLength(250);
        }
    }
}
