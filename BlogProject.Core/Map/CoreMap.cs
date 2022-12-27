using BlogProject.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Core.Map
{
    public class CoreMap<T> : IEntityTypeConfiguration<T> where T : CoreEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Status).IsRequired(true);
            builder.Property(e => e.CreatedDate).IsRequired(false);
            builder.Property(e => e.CreatedComputerName).IsRequired(false).HasMaxLength(255);
            builder.Property(e => e.CreatedIP).IsRequired(false).HasMaxLength(15);
            builder.Property(e => e.ModifiedDate).IsRequired(false);
            builder.Property(e => e.ModifiedComputerName).IsRequired(false).HasMaxLength(255);
            builder.Property(e => e.ModifiedIP).IsRequired(false).HasMaxLength(15);
        }
    }
}
