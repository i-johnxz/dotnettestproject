using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogsCore.Models.EntityTypeConfigurations
{
    public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(p => p.Timestamp).IsRowVersion();
            builder.Property<DateTime>("LastUpdated");

            builder.HasIndex(b => b.Url).IsUnique();

        }
    }
}